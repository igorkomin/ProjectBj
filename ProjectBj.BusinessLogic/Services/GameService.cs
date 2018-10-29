using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Managers.Interfaces;
using ProjectBj.BusinessLogic.Mappers;
using ProjectBj.BusinessLogic.Services.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Enums;
using ProjectBj.ViewModels.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Services
{
    public class GameService : IGameService
    {
        private readonly ICardManager _cardManager;
        private readonly IHistoryManager _historyManager;
        private readonly IPlayerManager _playerManager;
        private readonly IGameSessionManager _sessionManager;
        private readonly IGameManager _gameManager;
        private readonly IGameViewManager _gameViewManager;

        public GameService(ICardManager cardManager, IHistoryManager historyManager,
            IPlayerManager playerManager, IGameSessionManager sessionManager,
            IGameManager gameManager, IGameViewManager gameViewManager)
        {
            _cardManager = cardManager;
            _historyManager = historyManager;
            _playerManager = playerManager;
            _sessionManager = sessionManager;
            _gameManager = gameManager;
            _gameViewManager = gameViewManager;
        }

        public async Task<ResponseStartGameView> Start(string playerName, int botsNumber)
        {
            if (playerName == PlayerType.Dealer.ToString())
            {
                throw new ArgumentException(UserMessages.NameReservedMessage);
            }
            if (botsNumber < 0)
            {
                throw new ArgumentException(UserMessages.BotsNumberMustBePositiveMessage);
            }
            ResponseStartGameView gameView = await GiveFirstCards(playerName, botsNumber);
            return gameView;
        }

        public async Task<ResponseLoadGameView> Load(string playerName)
        {
            if (playerName == PlayerType.Dealer.ToString())
            {
                throw new ArgumentException(UserMessages.NameReservedMessage);
            }
            Player player = await _playerManager.GetExistingPlayer(playerName);
            GameSession lastSession = await _sessionManager.GetByPlayerId(player.Id);
            ResponseLoadGameView gameView = await _gameViewManager.GetLoadGameView(player.Id, lastSession.Id);
            return gameView;
        }

        public async Task<ResponseHitGameView> Hit(long playerId, long sessionId)
        {
            bool isLastAction = false;
            Player player = await _playerManager.GetPlayerById(playerId);
            await _historyManager.Create(player.Name,
                UserMessages.ChoseToHitMessage, sessionId);
            await GiveCards(1, playerId, sessionId);

            int playerScore = await _gameManager.GetHandScore(playerId, sessionId);
            if (playerScore >= Constants.BlackjackValue)
            {
                isLastAction = true;
                await GiveCardsToBots(sessionId);
                await GiveCardsToDealer(sessionId);
            }

            ResponseHitGameView gameView = await _gameViewManager.GetHitGameView(playerId, sessionId, isLastAction);
            if (isLastAction)
            {
                await _sessionManager.Close(sessionId);
            }
            return gameView;
        }

        public async Task<ResponseStandGameView> Stand(long playerId, long sessionId)
        {
            Player player = await _playerManager.GetPlayerById(playerId);
            await _historyManager.Create(player.Name,
                UserMessages.ChoseToStandMessage, sessionId);
            await GiveCardsToBots(sessionId);
            await GiveCardsToDealer(sessionId);
            ResponseStandGameView gameView = await _gameViewManager.GetStandGameView(playerId, sessionId);
            await _sessionManager.Close(sessionId);
            return gameView;
        }

        public async Task<ResponseDoubleGameView> Double(long playerId, long sessionId)
        {
            Player player = await _playerManager.GetPlayerById(playerId);
            await _historyManager.Create(player.Name,
                UserMessages.ChoseToDoubleMessage, sessionId);
            await GiveCards(1, playerId, sessionId);
            await GiveCardsToBots(sessionId);
            await GiveCardsToDealer(sessionId);
            ResponseDoubleGameView gameView = await _gameViewManager.GetDoubleGameView(playerId, sessionId);
            await _sessionManager.Close(sessionId);
            return gameView;
        }

        public async Task<ResponseSurrenderGameView> Surrender(long playerId, long sessionId)
        {
            Player player = await _playerManager.GetPlayerById(playerId);
            await _historyManager.Create(player.Name,
                UserMessages.ChoseToSurrenderMessage, sessionId);
            await _cardManager.ClearPlayerHand(playerId, sessionId);
            await GiveCardsToBots(sessionId);
            await GiveCardsToDealer(sessionId);
            ResponseSurrenderGameView gameView = await _gameViewManager.GetSurrenderGameView(playerId, sessionId);
            await _sessionManager.Close(sessionId);
            return gameView;
        }

        private async Task<ResponseStartGameView> CreateGameView(string playerName, int botsNumber)
        {
            Player player = await _playerManager.GetPlayerByName(playerName);
            GameSession session = await _sessionManager.GetNew();
            IEnumerable<Player> bots = await _playerManager.GetBots(botsNumber);
            Player dealer = await _playerManager.GetDealer();

            ResponseStartGameView gameView = StartGameViewMapper.GetStartGameView(session.Id, dealer, player, bots);
            return gameView;
        }

        private async Task<ResponseStartGameView> GiveFirstCards(string playerName, int botsNumber)
        {
            ResponseStartGameView gameView = await CreateGameView(playerName, botsNumber);
            List<long> playerIds = new List<long>
            {
                gameView.Player.Id,
                gameView.Dealer.Id
            };
            playerIds.AddRange(gameView.Bots.Select(bot => bot.Id));
            await GiveFirstTwoCards(playerIds, gameView.SessionId);
            gameView = await _gameViewManager.GetStartGameView(gameView.Player.Id, gameView.SessionId);
            return gameView;
        }

        private async Task GiveCardsToBots(long sessionId)
        {
            IEnumerable<Player> bots = await _playerManager.GetSessionBots(sessionId);
            foreach (var bot in bots)
            {
                await GiveCardsToBot(bot.Id, sessionId);
            }
        }

        private async Task<bool> GiveCardsToBot(long botId, long sessionId)
        {
            int botScore = await _gameManager.GetHandScore(botId, sessionId);
            if (botScore > Constants.MinimumBotHandValue)
            {
                return false;
            }
            await GiveCards(1, botId, sessionId);
            return await GiveCardsToBot(botId, sessionId);
        }

        private async Task<bool> GiveCardsToDealer(long sessionId)
        {
            Player dealer = await _playerManager.GetDealer();
            int dealerScore = await _gameManager.GetHandScore(dealer.Id, sessionId);
            if (dealerScore > Constants.MinimumDealerHandValue)
            {
                return false;
            }
            await GiveCards(1, dealer.Id, sessionId);
            return await GiveCardsToDealer(sessionId);
        }

        private async Task GiveFirstTwoCards(List<long> playerIds, long sessionId)
        {
            foreach (var playerId in playerIds)
            {
                await GiveCards(2, playerId, sessionId);
            }
        }
        
        private async Task GiveCards(int count, long playerId, long sessionId)
        {
            Player player = await _playerManager.GetPlayerById(playerId);
            IEnumerable<Card> cards = await _cardManager.GetRandomCards(count);
            IEnumerable<long> cardIds = cards.Select(c => c.Id);
            await _playerManager.GiveCardsToPlayer(playerId, sessionId, cardIds);

            foreach (var card in cards)
            {
                await _historyManager.Create(
                    player.Name, UserMessages.GetPlayerTakesCardMessage(
                        EnumHelper.GetCardRankName(card.Rank), card.Suit.ToString()), sessionId);
            }
        }
    }
}