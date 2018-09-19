using ProjectBj.BusinessLogic.Helpers.ViewMapHelpers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Entities.Enums;
using ProjectBj.ViewModels.Game;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Helpers
{
    public class GameServiceHelper : IGameServiceHelper
    {
        private readonly ICardProvider _cardProvider;
        private readonly IPlayerProvider _playerProvider;

        public GameServiceHelper(ICardProvider cardProvider, IPlayerProvider playerProvider)
        {
            _cardProvider = cardProvider;
            _playerProvider = playerProvider;
        }

        public async Task<ResponseStartGameView> GetStartGameView(int playerId, int sessionId)
        {
            Game game = await GetGame(playerId, sessionId);
            ResponseStartGameView gameView = StartGameViewHelper.GetStartGameView(sessionId, game.Dealer, game.Player, game.Bots);

            List<Card> playerCards = await GetCards(game.Player.Id, sessionId);
            List<Card> dealerCards = await GetCards(game.Dealer.Id, sessionId);
            int playerScore = await GetHandScore(game.Player.Id, sessionId);
            int dealerScore = await GetHandScore(game.Dealer.Id, sessionId);

            gameView.Player.Hand = StartGameViewHelper.GetHandLoadGameViewItem(playerCards, playerScore);
            gameView.Dealer.Hand = StartGameViewHelper.GetHandLoadGameViewItem(dealerCards, dealerScore);

            foreach (var bot in gameView.Bots.Bots)
            {
                List<Card> botCards = await GetCards(bot.Id, sessionId);
                int botScore = await GetHandScore(bot.Id, sessionId);
                bot.Hand = StartGameViewHelper.GetHandLoadGameViewItem(botCards, botScore);
            }

            return gameView;
        }

        public async Task<ResponseLoadGameView> GetLoadGameView(int playerId, int sessionId)
        {
            Game game = await GetGame(playerId, sessionId);
            ResponseLoadGameView gameView = LoadGameViewHelper.GetLoadGameView(sessionId, game.Dealer, game.Player, game.Bots);

            List<Card> playerCards = await GetCards(game.Player.Id, sessionId);
            List<Card> dealerCards = await GetCards(game.Dealer.Id, sessionId);
            int playerScore = await GetHandScore(game.Player.Id, sessionId);
            int dealerScore = await GetHandScore(game.Dealer.Id, sessionId);

            gameView.Player.Hand = LoadGameViewHelper.GetHandLoadGameViewItem(playerCards, playerScore);
            gameView.Dealer.Hand = LoadGameViewHelper.GetHandLoadGameViewItem(dealerCards, dealerScore);

            foreach (var bot in gameView.Bots.Bots)
            {
                List<Card> botCards = await GetCards(bot.Id, sessionId);
                int botScore = await GetHandScore(bot.Id, sessionId);
                bot.Hand = LoadGameViewHelper.GetHandLoadGameViewItem(botCards, botScore);
            }

            return gameView;
        }

        public async Task<ResponseHitGameView> GetHitGameView(int playerId, int sessionId, bool isLastAction)
        {
            Game game = await GetGame(playerId, sessionId);
            ResponseHitGameView gameView = HitGameViewHelper.GetHitGameView(sessionId, game.Dealer, game.Player, game.Bots);

            List<Card> playerCards = await GetCards(game.Player.Id, sessionId);
            List<Card> dealerCards = await GetCards(game.Dealer.Id, sessionId);
            int playerScore = await GetHandScore(game.Player.Id, sessionId);
            int dealerScore = await GetHandScore(game.Dealer.Id, sessionId);

            gameView.Player.Hand = HitGameViewHelper.GetHandHitGameViewItem(playerCards, playerScore);
            gameView.Dealer.Hand = HitGameViewHelper.GetHandHitGameViewItem(dealerCards, dealerScore);

            if (isLastAction)
            {
                (int playerGameState, string playerGameResult) = GetGameStateResult(playerScore, dealerScore);
                gameView.Player.GameResult.State = playerGameState;
                gameView.Player.GameResult.Result = playerGameResult;
            }

            foreach (var bot in gameView.Bots.Bots)
            {
                List<Card> botCards = await GetCards(bot.Id, sessionId);
                int botScore = await GetHandScore(bot.Id, sessionId);
                bot.Hand = HitGameViewHelper.GetHandHitGameViewItem(botCards, botScore);
                if (isLastAction)
                {
                    (int botGameState, string botGameResult) = GetGameStateResult(botScore, dealerScore);
                    bot.GameResult.State = botGameState;
                    bot.GameResult.Result = botGameResult;
                }
            }

            return gameView;
        }

        public async Task<ResponseStandGameView> GetStandGameView(int playerId, int sessionId)
        {
            Game game = await GetGame(playerId, sessionId);
            ResponseStandGameView gameView = StandGameViewHelper.GetStandGameView(sessionId, game.Dealer, game.Player, game.Bots);

            List<Card> playerCards = await GetCards(game.Player.Id, sessionId);
            List<Card> dealerCards = await GetCards(game.Dealer.Id, sessionId);
            int playerScore = await GetHandScore(game.Player.Id, sessionId);
            int dealerScore = await GetHandScore(game.Dealer.Id, sessionId);

            gameView.Player.Hand = StandGameViewHelper.GetHandStandGameViewItem(playerCards, playerScore);
            gameView.Dealer.Hand = StandGameViewHelper.GetHandStandGameViewItem(dealerCards, dealerScore);

            (int playerGameState, string playerGameResult) = GetGameStateResult(playerScore, dealerScore);
            gameView.Player.GameResult.State = playerGameState;
            gameView.Player.GameResult.Result = playerGameResult;

            foreach (var bot in gameView.Bots.Bots)
            {
                List<Card> botCards = await GetCards(bot.Id, sessionId);
                int botScore = await GetHandScore(bot.Id, sessionId);
                (int botGameState, string botGameResult) = GetGameStateResult(botScore, dealerScore);
                bot.Hand = StandGameViewHelper.GetHandStandGameViewItem(botCards, botScore);
                bot.GameResult.State = botGameState;
                bot.GameResult.Result = botGameResult;
            }

            return gameView;
        }

        public async Task<ResponseDoubleGameView> GetDoubleGameView(int playerId, int sessionId)
        {
            Game game = await GetGame(playerId, sessionId);
            ResponseDoubleGameView gameView = DoubleGameViewHelper.GetDoubleGameView(sessionId, game.Dealer, game.Player, game.Bots);

            List<Card> playerCards = await GetCards(game.Player.Id, sessionId);
            List<Card> dealerCards = await GetCards(game.Dealer.Id, sessionId);
            int playerScore = await GetHandScore(game.Player.Id, sessionId);
            int dealerScore = await GetHandScore(game.Dealer.Id, sessionId);

            gameView.Player.Hand = DoubleGameViewHelper.GetHandDoubleGameViewItem(playerCards, playerScore);
            gameView.Dealer.Hand = DoubleGameViewHelper.GetHandDoubleGameViewItem(dealerCards, dealerScore);

            (int playerGameState, string playerGameResult) = GetGameStateResult(playerScore, dealerScore);
            gameView.Player.GameResult.State = playerGameState;
            gameView.Player.GameResult.Result = playerGameResult;

            foreach (var bot in gameView.Bots.Bots)
            {
                List<Card> botCards = await GetCards(bot.Id, sessionId);
                int botScore = await GetHandScore(bot.Id, sessionId);
                (int botGameState, string botGameResult) = GetGameStateResult(botScore, dealerScore);
                bot.Hand = DoubleGameViewHelper.GetHandDoubleGameViewItem(botCards, botScore);
                bot.GameResult.State = botGameState;
                bot.GameResult.Result = botGameResult;
            }

            return gameView;
        }

        public async Task<ResponseSurrenderGameView> GetSurrenderGameView(int playerId, int sessionId)
        {
            Game game = await GetGame(playerId, sessionId);
            ResponseSurrenderGameView gameView = SurrenderGameViewHelper.GetSurrenderGameView(sessionId, game.Dealer, game.Player, game.Bots);

            List<Card> playerCards = await GetCards(game.Player.Id, sessionId);
            List<Card> dealerCards = await GetCards(game.Dealer.Id, sessionId);
            int playerScore = await GetHandScore(game.Player.Id, sessionId);
            int dealerScore = await GetHandScore(game.Dealer.Id, sessionId);

            gameView.Player.Hand = SurrenderGameViewHelper.GetHandSurrenderGameViewItem(playerCards, playerScore);
            gameView.Dealer.Hand = SurrenderGameViewHelper.GetHandSurrenderGameViewItem(dealerCards, dealerScore);

            (int playerGameState, string playerGameResult) = GetGameStateResult(playerScore, dealerScore);
            gameView.Player.GameResult.State = playerGameState;
            gameView.Player.GameResult.Result = playerGameResult;

            foreach (var bot in gameView.Bots.Bots)
            {
                List<Card> botCards = await GetCards(bot.Id, sessionId);
                int botScore = await GetHandScore(bot.Id, sessionId);
                (int botGameState, string botGameResult) = GetGameStateResult(botScore, dealerScore);
                bot.Hand = SurrenderGameViewHelper.GetHandSurrenderGameViewItem(botCards, botScore);
                bot.GameResult.State = botGameState;
                bot.GameResult.Result = botGameResult;
            }

            return gameView;
        }

        public async Task<int> GetHandScore(int playerId, int sessionId)
        {
            int totalScore = 0;
            int aceCount = 0;

            List<Card> cards = await GetCards(playerId, sessionId);

            foreach (var card in cards)
            {
                int aceCardRank = (int)CardRanks.Rank.Ace;
                int tenCardRank = (int)CardRanks.Rank.Ten;

                if (card.Rank == aceCardRank)
                {
                    totalScore += ValueHelper.AceCardValue;
                    aceCount++;
                    continue;
                }
                if (card.Rank > tenCardRank)
                {
                    totalScore += ValueHelper.FaceCardValue;
                    continue;
                }
                totalScore += card.Rank;
            }

            if (totalScore > ValueHelper.BlackjackValue)
            {
                totalScore -= aceCount * ValueHelper.AceDelta;
            }

            return totalScore;
        }

        private (int gameState, string gameResult) GetGameStateResult(int playerScore, int dealerScore)
        {
            int gameState = (int)GetGameResult(playerScore, dealerScore);
            string gameResult = GetGameResult(playerScore, dealerScore).ToString();
            return (gameState, gameResult);
        }

        private GameResults.Result GetGameResult(int playerScore, int dealerScore)
        {
            if (playerScore == ValueHelper.BlackjackValue)
            {
                return GameResults.Result.Blackjack;
            }
            if (playerScore > ValueHelper.BlackjackValue)
            {
                return GameResults.Result.Bust;
            }
            if (playerScore == dealerScore)
            {
                return GameResults.Result.Win;
            }
            if (playerScore == 0)
            {
                return GameResults.Result.Surrender;
            }
            if (playerScore > dealerScore || dealerScore > ValueHelper.BlackjackValue)
            {
                return GameResults.Result.Win;
            }

            return GameResults.Result.Lose;
        }

        private async Task<Game> GetGame(int playerId, int sessionId)
        {
            Player player = await _playerProvider.GetPlayerById(playerId);
            Player dealer = await _playerProvider.GetDealer();
            List<Player> bots = await _playerProvider.GetSessionBots(sessionId);

            Game game = new Game
            {
                Player = player,
                Dealer = dealer,
                Bots = bots
            };

            return game;
        }

        private async Task<List<Card>> GetCards(int playerId, int sessionId)
        {
            List<Card> cards = await _cardProvider.GetPlayerHand(playerId, sessionId);
            return cards;
        }
    }
}