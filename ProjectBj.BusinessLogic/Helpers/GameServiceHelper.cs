using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.BusinessLogic.Mappers;
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

        public async Task<ResponseStartGameView> GetStartGameView(long playerId, long sessionId)
        {
            Game game = await GetGame(playerId, sessionId);
            ResponseStartGameView gameView = StartGameViewMapper.GetStartGameView(sessionId, game.Dealer, game.Player, game.Bots);

            IEnumerable<Card> playerCards = await GetCards(game.Player.Id, sessionId);
            IEnumerable<Card> dealerCards = await GetCards(game.Dealer.Id, sessionId);
            int playerScore = await GetHandScore(game.Player.Id, sessionId);
            int dealerScore = await GetHandScore(game.Dealer.Id, sessionId);

            gameView.Player.Hand = StartGameViewMapper.GetHandLoadGameViewItem(playerCards, playerScore);
            gameView.Dealer.Hand = StartGameViewMapper.GetHandLoadGameViewItem(dealerCards, dealerScore);

            foreach (var bot in gameView.Bots)
            {
                IEnumerable<Card> botCards = await GetCards(bot.Id, sessionId);
                int botScore = await GetHandScore(bot.Id, sessionId);
                bot.Hand = StartGameViewMapper.GetHandLoadGameViewItem(botCards, botScore);
            }

            return gameView;
        }

        public async Task<ResponseLoadGameView> GetLoadGameView(long playerId, long sessionId)
        {
            Game game = await GetGame(playerId, sessionId);
            ResponseLoadGameView gameView = LoadGameViewMapper.GetLoadGameView(sessionId, game.Dealer, game.Player, game.Bots);

            IEnumerable<Card> playerCards = await GetCards(game.Player.Id, sessionId);
            IEnumerable<Card> dealerCards = await GetCards(game.Dealer.Id, sessionId);
            int playerScore = await GetHandScore(game.Player.Id, sessionId);
            int dealerScore = await GetHandScore(game.Dealer.Id, sessionId);

            gameView.Player.Hand = LoadGameViewMapper.GetHandLoadGameViewItem(playerCards, playerScore);
            gameView.Dealer.Hand = LoadGameViewMapper.GetHandLoadGameViewItem(dealerCards, dealerScore);

            foreach (var bot in gameView.Bots)
            {
                IEnumerable<Card> botCards = await GetCards(bot.Id, sessionId);
                int botScore = await GetHandScore(bot.Id, sessionId);
                bot.Hand = LoadGameViewMapper.GetHandLoadGameViewItem(botCards, botScore);
            }

            return gameView;
        }

        public async Task<ResponseHitGameView> GetHitGameView(long playerId, long sessionId, bool isLastAction)
        {
            Game game = await GetGame(playerId, sessionId);
            ResponseHitGameView gameView = HitGameViewMapper.GetHitGameView(sessionId, game.Dealer, game.Player, game.Bots);

            IEnumerable<Card> playerCards = await GetCards(game.Player.Id, sessionId);
            IEnumerable<Card> dealerCards = await GetCards(game.Dealer.Id, sessionId);
            int playerScore = await GetHandScore(game.Player.Id, sessionId);
            int dealerScore = await GetHandScore(game.Dealer.Id, sessionId);

            gameView.Player.Hand = HitGameViewMapper.GetHandHitGameViewItem(playerCards, playerScore);
            gameView.Dealer.Hand = HitGameViewMapper.GetHandHitGameViewItem(dealerCards, dealerScore);

            if (isLastAction)
            {
                (int playerGameState, string playerGameResult) = GetGameStateResult(playerScore, dealerScore);
                gameView.Player.GameResult.State = playerGameState;
                gameView.Player.GameResult.Result = playerGameResult;
            }

            foreach (var bot in gameView.Bots)
            {
                IEnumerable<Card> botCards = await GetCards(bot.Id, sessionId);
                int botScore = await GetHandScore(bot.Id, sessionId);
                bot.Hand = HitGameViewMapper.GetHandHitGameViewItem(botCards, botScore);
                if (isLastAction)
                {
                    (int botGameState, string botGameResult) = GetGameStateResult(botScore, dealerScore);
                    bot.GameResult.State = botGameState;
                    bot.GameResult.Result = botGameResult;
                }
            }

            return gameView;
        }

        public async Task<ResponseStandGameView> GetStandGameView(long playerId, long sessionId)
        {
            Game game = await GetGame(playerId, sessionId);
            ResponseStandGameView gameView = StandGameViewMapper.GetStandGameView(sessionId, game.Dealer, game.Player, game.Bots);

            IEnumerable<Card> playerCards = await GetCards(game.Player.Id, sessionId);
            IEnumerable<Card> dealerCards = await GetCards(game.Dealer.Id, sessionId);
            int playerScore = await GetHandScore(game.Player.Id, sessionId);
            int dealerScore = await GetHandScore(game.Dealer.Id, sessionId);

            gameView.Player.Hand = StandGameViewMapper.GetHandStandGameViewItem(playerCards, playerScore);
            gameView.Dealer.Hand = StandGameViewMapper.GetHandStandGameViewItem(dealerCards, dealerScore);

            (int playerGameState, string playerGameResult) = GetGameStateResult(playerScore, dealerScore);
            gameView.Player.GameResult.State = playerGameState;
            gameView.Player.GameResult.Result = playerGameResult;

            foreach (var bot in gameView.Bots)
            {
                IEnumerable<Card> botCards = await GetCards(bot.Id, sessionId);
                int botScore = await GetHandScore(bot.Id, sessionId);
                (int botGameState, string botGameResult) = GetGameStateResult(botScore, dealerScore);
                bot.Hand = StandGameViewMapper.GetHandStandGameViewItem(botCards, botScore);
                bot.GameResult.State = botGameState;
                bot.GameResult.Result = botGameResult;
            }

            return gameView;
        }

        public async Task<ResponseDoubleGameView> GetDoubleGameView(long playerId, long sessionId)
        {
            Game game = await GetGame(playerId, sessionId);
            ResponseDoubleGameView gameView = DoubleGameViewMapper.GetDoubleGameView(sessionId, game.Dealer, game.Player, game.Bots);

            IEnumerable<Card> playerCards = await GetCards(game.Player.Id, sessionId);
            IEnumerable<Card> dealerCards = await GetCards(game.Dealer.Id, sessionId);
            int playerScore = await GetHandScore(game.Player.Id, sessionId);
            int dealerScore = await GetHandScore(game.Dealer.Id, sessionId);

            gameView.Player.Hand = DoubleGameViewMapper.GetHandDoubleGameViewItem(playerCards, playerScore);
            gameView.Dealer.Hand = DoubleGameViewMapper.GetHandDoubleGameViewItem(dealerCards, dealerScore);

            (int playerGameState, string playerGameResult) = GetGameStateResult(playerScore, dealerScore);
            gameView.Player.GameResult.State = playerGameState;
            gameView.Player.GameResult.Result = playerGameResult;

            foreach (var bot in gameView.Bots)
            {
                IEnumerable<Card> botCards = await GetCards(bot.Id, sessionId);
                int botScore = await GetHandScore(bot.Id, sessionId);
                (int botGameState, string botGameResult) = GetGameStateResult(botScore, dealerScore);
                bot.Hand = DoubleGameViewMapper.GetHandDoubleGameViewItem(botCards, botScore);
                bot.GameResult.State = botGameState;
                bot.GameResult.Result = botGameResult;
            }

            return gameView;
        }

        public async Task<ResponseSurrenderGameView> GetSurrenderGameView(long playerId, long sessionId)
        {
            Game game = await GetGame(playerId, sessionId);
            ResponseSurrenderGameView gameView = SurrenderGameViewMapper.GetSurrenderGameView(sessionId, game.Dealer, game.Player, game.Bots);

            IEnumerable<Card> playerCards = await GetCards(game.Player.Id, sessionId);
            IEnumerable<Card> dealerCards = await GetCards(game.Dealer.Id, sessionId);
            int playerScore = await GetHandScore(game.Player.Id, sessionId);
            int dealerScore = await GetHandScore(game.Dealer.Id, sessionId);

            gameView.Player.Hand = SurrenderGameViewMapper.GetHandSurrenderGameViewItem(playerCards, playerScore);
            gameView.Dealer.Hand = SurrenderGameViewMapper.GetHandSurrenderGameViewItem(dealerCards, dealerScore);

            (int playerGameState, string playerGameResult) = GetGameStateResult(playerScore, dealerScore);
            gameView.Player.GameResult.State = playerGameState;
            gameView.Player.GameResult.Result = playerGameResult;

            foreach (var bot in gameView.Bots)
            {
                IEnumerable<Card> botCards = await GetCards(bot.Id, sessionId);
                int botScore = await GetHandScore(bot.Id, sessionId);
                (int botGameState, string botGameResult) = GetGameStateResult(botScore, dealerScore);
                bot.Hand = SurrenderGameViewMapper.GetHandSurrenderGameViewItem(botCards, botScore);
                bot.GameResult.State = botGameState;
                bot.GameResult.Result = botGameResult;
            }

            return gameView;
        }

        public async Task<int> GetHandScore(long playerId, long sessionId)
        {
            int totalScore = 0;
            int aceCount = 0;

            IEnumerable<Card> cards = await GetCards(playerId, sessionId);

            foreach (var card in cards)
            {
                int aceCardRank = (int)CardRanks.Ace;
                int tenCardRank = (int)CardRanks.Ten;

                if ((int)card.Rank == aceCardRank)
                {
                    totalScore += ValueHelper.AceCardValue;
                    aceCount++;
                    continue;
                }
                if ((int)card.Rank > tenCardRank)
                {
                    totalScore += ValueHelper.FaceCardValue;
                    continue;
                }
                totalScore += (int)card.Rank;
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

        private GameResults GetGameResult(int playerScore, int dealerScore)
        {
            if (playerScore == ValueHelper.BlackjackValue)
            {
                return GameResults.Blackjack;
            }
            if (playerScore > ValueHelper.BlackjackValue)
            {
                return GameResults.Bust;
            }
            if (playerScore == dealerScore)
            {
                return GameResults.Win;
            }
            if (playerScore == 0)
            {
                return GameResults.Surrender;
            }
            if (playerScore > dealerScore || dealerScore > ValueHelper.BlackjackValue)
            {
                return GameResults.Win;
            }

            return GameResults.Lose;
        }

        private async Task<Game> GetGame(long playerId, long sessionId)
        {
            Player player = await _playerProvider.GetPlayerById(playerId);
            Player dealer = await _playerProvider.GetDealer();
            IEnumerable<Player> bots = await _playerProvider.GetSessionBots(sessionId);

            Game game = new Game
            {
                Player = player,
                Dealer = dealer,
                Bots = bots
            };

            return game;
        }

        private async Task<IEnumerable<Card>> GetCards(long playerId, long sessionId)
        {
            IEnumerable<Card> cards = await _cardProvider.GetPlayerHand(playerId, sessionId);
            return cards;
        }
    }
}