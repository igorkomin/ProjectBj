﻿using ProjectBj.BusinessLogic.Managers.Interfaces;
using ProjectBj.BusinessLogic.Mappers;
using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Managers
{
    internal class GameViewManager : IGameViewManager
    {
        private readonly IGameManager _gameManager;
        private readonly IGameResultManager _gameResultManager;

        public GameViewManager(IGameManager gameManager, IGameResultManager gameResultManager)
        {
            _gameManager = gameManager;
            _gameResultManager = gameResultManager;
        }

        public async Task<ResponseStartGameView> GetStartGameView(long playerId, long sessionId)
        {
            (Player player, Player dealer, IEnumerable<Player> bots) = await _gameManager.GetAllGamePlayers(playerId, sessionId);
            ResponseStartGameView gameView = StartGameViewMapper.GetStartGameView(sessionId, dealer, player, bots);

            IEnumerable<Card> playerCards = await _gameManager.GetCards(player.Id, sessionId);
            IEnumerable<Card> dealerCards = await _gameManager.GetCards(dealer.Id, sessionId);
            int playerScore = await _gameManager.GetHandScore(player.Id, sessionId);
            int dealerScore = await _gameManager.GetHandScore(dealer.Id, sessionId);

            gameView.Player.Hand = StartGameViewMapper.GetHandLoadGameViewItem(playerCards, playerScore);
            gameView.Dealer.Hand = StartGameViewMapper.GetHandLoadGameViewItem(dealerCards, dealerScore);

            foreach (var bot in gameView.Bots)
            {
                IEnumerable<Card> botCards = await _gameManager.GetCards(bot.Id, sessionId);
                int botScore = await _gameManager.GetHandScore(bot.Id, sessionId);
                bot.Hand = StartGameViewMapper.GetHandLoadGameViewItem(botCards, botScore);
            }

            return gameView;
        }

        public async Task<ResponseLoadGameView> GetLoadGameView(long playerId, long sessionId)
        {
            (Player player, Player dealer, IEnumerable<Player> bots) = await _gameManager.GetAllGamePlayers(playerId, sessionId);
            ResponseLoadGameView gameView = LoadGameViewMapper.GetLoadGameView(sessionId, dealer, player, bots);

            IEnumerable<Card> playerCards = await _gameManager.GetCards(player.Id, sessionId);
            IEnumerable<Card> dealerCards = await _gameManager.GetCards(dealer.Id, sessionId);
            int playerScore = await _gameManager.GetHandScore(player.Id, sessionId);
            int dealerScore = await _gameManager.GetHandScore(dealer.Id, sessionId);

            gameView.Player.Hand = LoadGameViewMapper.GetHandLoadGameViewItem(playerCards, playerScore);
            gameView.Dealer.Hand = LoadGameViewMapper.GetHandLoadGameViewItem(dealerCards, dealerScore);

            foreach (var bot in gameView.Bots)
            {
                IEnumerable<Card> botCards = await _gameManager.GetCards(bot.Id, sessionId);
                int botScore = await _gameManager.GetHandScore(bot.Id, sessionId);
                bot.Hand = LoadGameViewMapper.GetHandLoadGameViewItem(botCards, botScore);
            }

            return gameView;
        }

        public async Task<ResponseHitGameView> GetHitGameView(long playerId, long sessionId, bool isLastAction)
        {
            (Player player, Player dealer, IEnumerable<Player> bots) = await _gameManager.GetAllGamePlayers(playerId, sessionId);
            ResponseHitGameView gameView = HitGameViewMapper.GetHitGameView(sessionId, dealer, player, bots);

            IEnumerable<Card> playerCards = await _gameManager.GetCards(player.Id, sessionId);
            IEnumerable<Card> dealerCards = await _gameManager.GetCards(dealer.Id, sessionId);
            int playerScore = await _gameManager.GetHandScore(player.Id, sessionId);
            int dealerScore = await _gameManager.GetHandScore(dealer.Id, sessionId);

            gameView.Player.Hand = HitGameViewMapper.GetHandHitGameViewItem(playerCards, playerScore);
            gameView.Dealer.Hand = HitGameViewMapper.GetHandHitGameViewItem(dealerCards, dealerScore);

            if (isLastAction)
            {
                (int playerGameState, string playerGameResult) = _gameResultManager.GetGameStateResult(playerScore, dealerScore);
                gameView.Player.GameResult.State = playerGameState;
                gameView.Player.GameResult.Result = playerGameResult;
            }

            foreach (var bot in gameView.Bots)
            {
                IEnumerable<Card> botCards = await _gameManager.GetCards(bot.Id, sessionId);
                int botScore = await _gameManager.GetHandScore(bot.Id, sessionId);
                bot.Hand = HitGameViewMapper.GetHandHitGameViewItem(botCards, botScore);
                if (isLastAction)
                {
                    (int botGameState, string botGameResult) = _gameResultManager.GetGameStateResult(botScore, dealerScore);
                    bot.GameResult.State = botGameState;
                    bot.GameResult.Result = botGameResult;
                }
            }

            return gameView;
        }

        public async Task<ResponseStandGameView> GetStandGameView(long playerId, long sessionId)
        {
            (Player player, Player dealer, IEnumerable<Player> bots) = await _gameManager.GetAllGamePlayers(playerId, sessionId);
            ResponseStandGameView gameView = StandGameViewMapper.GetStandGameView(sessionId, dealer, player, bots);

            IEnumerable<Card> playerCards = await _gameManager.GetCards(player.Id, sessionId);
            IEnumerable<Card> dealerCards = await _gameManager.GetCards(dealer.Id, sessionId);
            int playerScore = await _gameManager.GetHandScore(player.Id, sessionId);
            int dealerScore = await _gameManager.GetHandScore(dealer.Id, sessionId);

            gameView.Player.Hand = StandGameViewMapper.GetHandStandGameViewItem(playerCards, playerScore);
            gameView.Dealer.Hand = StandGameViewMapper.GetHandStandGameViewItem(dealerCards, dealerScore);

            (int playerGameState, string playerGameResult) = _gameResultManager.GetGameStateResult(playerScore, dealerScore);
            gameView.Player.GameResult.State = playerGameState;
            gameView.Player.GameResult.Result = playerGameResult;

            foreach (var bot in gameView.Bots)
            {
                IEnumerable<Card> botCards = await _gameManager.GetCards(bot.Id, sessionId);
                int botScore = await _gameManager.GetHandScore(bot.Id, sessionId);
                (int botGameState, string botGameResult) = _gameResultManager.GetGameStateResult(botScore, dealerScore);
                bot.Hand = StandGameViewMapper.GetHandStandGameViewItem(botCards, botScore);
                bot.GameResult.State = botGameState;
                bot.GameResult.Result = botGameResult;
            }

            return gameView;
        }

        public async Task<ResponseDoubleGameView> GetDoubleGameView(long playerId, long sessionId)
        {
            (Player player, Player dealer, IEnumerable<Player> bots) = await _gameManager.GetAllGamePlayers(playerId, sessionId);
            ResponseDoubleGameView gameView = DoubleGameViewMapper.GetDoubleGameView(sessionId, dealer, player, bots);

            IEnumerable<Card> playerCards = await _gameManager.GetCards(player.Id, sessionId);
            IEnumerable<Card> dealerCards = await _gameManager.GetCards(dealer.Id, sessionId);
            int playerScore = await _gameManager.GetHandScore(player.Id, sessionId);
            int dealerScore = await _gameManager.GetHandScore(dealer.Id, sessionId);

            gameView.Player.Hand = DoubleGameViewMapper.GetHandDoubleGameViewItem(playerCards, playerScore);
            gameView.Dealer.Hand = DoubleGameViewMapper.GetHandDoubleGameViewItem(dealerCards, dealerScore);

            (int playerGameState, string playerGameResult) = _gameResultManager.GetGameStateResult(playerScore, dealerScore);
            gameView.Player.GameResult.State = playerGameState;
            gameView.Player.GameResult.Result = playerGameResult;

            foreach (var bot in gameView.Bots)
            {
                IEnumerable<Card> botCards = await _gameManager.GetCards(bot.Id, sessionId);
                int botScore = await _gameManager.GetHandScore(bot.Id, sessionId);
                (int botGameState, string botGameResult) = _gameResultManager.GetGameStateResult(botScore, dealerScore);
                bot.Hand = DoubleGameViewMapper.GetHandDoubleGameViewItem(botCards, botScore);
                bot.GameResult.State = botGameState;
                bot.GameResult.Result = botGameResult;
            }

            return gameView;
        }

        public async Task<ResponseSurrenderGameView> GetSurrenderGameView(long playerId, long sessionId)
        {
            (Player player, Player dealer, IEnumerable<Player> bots) = await _gameManager.GetAllGamePlayers(playerId, sessionId);
            ResponseSurrenderGameView gameView = SurrenderGameViewMapper.GetSurrenderGameView(sessionId, dealer, player, bots);

            IEnumerable<Card> playerCards = await _gameManager.GetCards(player.Id, sessionId);
            IEnumerable<Card> dealerCards = await _gameManager.GetCards(dealer.Id, sessionId);
            int playerScore = await _gameManager.GetHandScore(player.Id, sessionId);
            int dealerScore = await _gameManager.GetHandScore(dealer.Id, sessionId);

            gameView.Player.Hand = SurrenderGameViewMapper.GetHandSurrenderGameViewItem(playerCards, playerScore);
            gameView.Dealer.Hand = SurrenderGameViewMapper.GetHandSurrenderGameViewItem(dealerCards, dealerScore);

            (int playerGameState, string playerGameResult) = _gameResultManager.GetGameStateResult(playerScore, dealerScore);
            gameView.Player.GameResult.State = playerGameState;
            gameView.Player.GameResult.Result = playerGameResult;

            foreach (var bot in gameView.Bots)
            {
                IEnumerable<Card> botCards = await _gameManager.GetCards(bot.Id, sessionId);
                int botScore = await _gameManager.GetHandScore(bot.Id, sessionId);
                (int botGameState, string botGameResult) = _gameResultManager.GetGameStateResult(botScore, dealerScore);
                bot.Hand = SurrenderGameViewMapper.GetHandSurrenderGameViewItem(botCards, botScore);
                bot.GameResult.State = botGameState;
                bot.GameResult.Result = botGameResult;
            }

            return gameView;
        }
    }
}