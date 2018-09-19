﻿using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Helpers.ViewMapHelpers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Logger;
using ProjectBj.ViewModels.Game;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Services
{
    public class GameService : IGameService
    {
        private readonly ICardProvider _cardProvider;
        private readonly IHistoryProvider _historyProvider;
        private readonly IPlayerProvider _playerProvider;
        private readonly IGameSessionProvider _sessionProvider;
        private readonly IGameServiceHelper _gameServiceHelper;

        public GameService(ICardProvider cardProvider, IHistoryProvider historyProvider,
            IPlayerProvider playerProvider, IGameSessionProvider sessionProvider, IGameServiceHelper gameServiceHelper)
        {
            _cardProvider = cardProvider;
            _historyProvider = historyProvider;
            _playerProvider = playerProvider;
            _sessionProvider = sessionProvider;
            _gameServiceHelper = gameServiceHelper;
        }

        public async Task<ResponseStartGameView> GetNewGame(string playerName, int botsNumber)
        {
            if (playerName == StringHelper.DealerName)
            {
                throw new ArgumentException(StringHelper.NameReservedMessage);
            }
            ResponseStartGameView gameView = await GiveFirstCards(playerName, botsNumber);
            Log.Info(StringHelper.GetGameStartedMessage(gameView.SessionId));
            return gameView;
        }

        public async Task<ResponseLoadGameView> GetUnfinishedGame(string playerName)
        {
            if (playerName == StringHelper.DealerName)
            {
                throw new ArgumentException(StringHelper.NameReservedMessage);
            }
            Player player = await _playerProvider.GetExistingPlayer(playerName);
            GameSession lastSession = await _sessionProvider.GetByPlayerId(player.Id);
            ResponseLoadGameView gameView = await _gameServiceHelper.GetLoadGameView(player.Id, lastSession.Id);
            Log.Info(StringHelper.GetGameLoadedMessage(lastSession.Id));
            return gameView;
        }

        public async Task<ResponseHitGameView> MakeHitDecision(int playerId, int sessionId)
        {
            bool isLastAction = false;
            Player player = await _playerProvider.GetPlayerById(playerId);
            await _historyProvider.Create(player.Name,
                StringHelper.ChoseToHitMessage, sessionId);
            Log.Info(StringHelper.GetPlayerIdHitsMessage(playerId));
            await GiveCard(playerId, sessionId);

            int playerScore = await _gameServiceHelper.GetHandScore(playerId, sessionId);
            if (playerScore >= ValueHelper.BlackjackValue)
            {
                isLastAction = true;
                await GiveCardsToBots(sessionId);
                await GiveCardsToDealer(sessionId);
            }

            ResponseHitGameView gameView = await _gameServiceHelper.GetHitGameView(playerId, sessionId, isLastAction);
            if (isLastAction)
            {
                await EndGameSession(sessionId);
            }
            return gameView;
        }

        public async Task<ResponseStandGameView> MakeStandDecision(int playerId, int sessionId)
        {
            Player player = await _playerProvider.GetPlayerById(playerId);
            await _historyProvider.Create(player.Name,
                StringHelper.ChoseToStandMessage, sessionId);
            Log.Info(StringHelper.GetPlayerIdStandsMessage(playerId));
            await GiveCardsToBots(sessionId);
            await GiveCardsToDealer(sessionId);
            ResponseStandGameView gameView = await _gameServiceHelper.GetStandGameView(playerId, sessionId);
            await EndGameSession(sessionId);
            return gameView;
        }

        public async Task<ResponseDoubleGameView> MakeDoubleDownDecision(int playerId, int sessionId)
        {
            Player player = await _playerProvider.GetPlayerById(playerId);
            await _historyProvider.Create(player.Name,
                StringHelper.ChoseToDoubleMessage, sessionId);
            Log.Info(StringHelper.GetPlayerIdDoubleDownMessage(playerId));
            await GiveCard(playerId, sessionId);
            await GiveCardsToBots(sessionId);
            await GiveCardsToDealer(sessionId);
            ResponseDoubleGameView gameView = await _gameServiceHelper.GetDoubleGameView(playerId, sessionId);
            await EndGameSession(sessionId);
            return gameView;
        }

        public async Task<ResponseSurrenderGameView> MakeSurrenderDecision(int playerId, int sessionId)
        {
            Player player = await _playerProvider.GetPlayerById(playerId);
            await _historyProvider.Create(player.Name,
                StringHelper.ChoseToSurrenderMessage, sessionId);
            Log.Info(StringHelper.GetPlayerIdSurrenderMessage(playerId));
            await _cardProvider.ClearPlayerHand(playerId, sessionId);
            await GiveCardsToBots(sessionId);
            await GiveCardsToDealer(sessionId);
            ResponseSurrenderGameView gameView = await _gameServiceHelper.GetSurrenderGameView(playerId, sessionId);
            await EndGameSession(sessionId);
            return gameView;
        }

        private async Task<ResponseStartGameView> CreateGame(string playerName, int botsNumber)
        {
            Player player = await _playerProvider.GetPlayerByName(playerName);
            GameSession session = await _sessionProvider.GetNew();
            List<Player> bots = await _playerProvider.GetBots(botsNumber, session.Id);
            Player dealer = await _playerProvider.GetDealer();

            ResponseStartGameView gameView = StartGameViewHelper.GetStartGameView(session.Id, dealer, player, bots);
            return gameView;
        }

        private async Task<ResponseStartGameView> GiveFirstCards(string playerName, int botsNumber)
        {
            ResponseStartGameView gameView = await CreateGame(playerName, botsNumber);
            List<int> playerIds = new List<int>
            {
                gameView.Player.Id,
                gameView.Dealer.Id
            };
            foreach (var bot in gameView.Bots)
            {
                playerIds.Add(bot.Id);
            }
            await GiveFirstTwoCards(playerIds, gameView.SessionId);
            gameView = await _gameServiceHelper.GetStartGameView(gameView.Player.Id, gameView.SessionId);
            return gameView;
        }

        private async Task GiveCardsToBots(int sessionId)
        {
            List<Player> bots = await _playerProvider.GetSessionBots(sessionId);
            foreach (var bot in bots)
            {
                await GiveCardsToBot(bot.Id, sessionId);
            }
        }

        private async Task<bool> GiveCardsToBot(int botId, int sessionId)
        {
            Log.Info(StringHelper.BotsTurnMessage);
            int botScore = await _gameServiceHelper.GetHandScore(botId, sessionId);
            if (botScore > ValueHelper.MinimumBotHandValue)
            {
                return false;
            }
            await GiveCard(botId, sessionId);
            return await GiveCardsToBot(botId, sessionId);
        }

        private async Task<bool> GiveCardsToDealer(int sessionId)
        {
            Player dealer = await _playerProvider.GetDealer();
            Log.Info(StringHelper.DealerTurnMessage);
            int dealerScore = await _gameServiceHelper.GetHandScore(dealer.Id, sessionId);
            if (dealerScore > ValueHelper.MinimumDealerHandValue)
            {
                return false;
            }
            await GiveCard(dealer.Id, sessionId);
            return await GiveCardsToDealer(sessionId);
        }

        private async Task GiveFirstTwoCards(List<int> playerIds, int sessionId)
        {
            foreach (var playerId in playerIds)
            {
                await GiveCard(playerId, sessionId);
                await GiveCard(playerId, sessionId);
            }
        }

        private async Task EndGameSession(int sessionId)
        {
            await CloseGameSession(sessionId);
            await _playerProvider.DeleteSessionBots(sessionId);
        }

        private async Task GiveCard(int playerId, int sessionId)
        {
            List<Card> deck = await _cardProvider.GetShuffledDeck();
            Card card = deck[0];
            Player player = await _playerProvider.GetPlayerById(playerId);
            await _historyProvider.Create(
                player.Name, StringHelper.GetPlayerTakesCardMessage(
                    EnumHelper.GetCardRankName(card.Rank), card.Suit), sessionId);
            await _playerProvider.GiveCardToPlayer(playerId, sessionId, card.Id);
        }

        private async Task CloseGameSession(int sessionId)
        {
            await _sessionProvider.Close(sessionId);
            Log.Info(StringHelper.GetGameEndedMessage(sessionId));
        }
    }
}