using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Entities.Enums;
using ProjectBj.Logger;
using ProjectBj.ViewModels.Game;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Services
{
    public class GameService : IGameService
    {
        public string _playerName { get; set; }
        public int _botNumber { get; set; }
        private readonly ICardProvider _cardProvider;
        private readonly IHistoryProvider _historyProvider;
        private readonly IPlayerProvider _playerProvider;
        private readonly IGameSessionProvider _sessionProvider;

        public GameService(ICardProvider cardProvider, IHistoryProvider historyProvider,
            IPlayerProvider playerProvider, IGameSessionProvider sessionProvider)
        {
            _cardProvider = cardProvider;
            _historyProvider = historyProvider;
            _playerProvider = playerProvider;
            _sessionProvider = sessionProvider;
        }

        private async Task<GameViewModel> CreateGame()
        {
            var player = await _playerProvider.GetPlayerByName(_playerName);
            var session = await _sessionProvider.GetNew();
            var bots = await _playerProvider.GetBots(_botNumber, session.Id);
            var dealer = await _playerProvider.GetDealer();

            GameViewModel gameViewModel = MapHelper.GetGameViewModel(session.Id, dealer, player, bots);
            return gameViewModel;
        }

        private async Task<GameViewModel> GetGame(int playerId, int sessionId)
        {
            var player = await _playerProvider.GetPlayerById(playerId);
            var dealer = await _playerProvider.GetDealer();
            var bots = await _playerProvider.GetSessionBots(sessionId);

            GameViewModel gameViewModel = MapHelper.GetGameViewModel(sessionId, dealer, player, bots);

            gameViewModel.Player.Hand = await GetHand(player.Id, sessionId);
            gameViewModel.Dealer.Hand = await GetHand(dealer.Id, sessionId);

            foreach (var bot in gameViewModel.Bots)
            {
                bot.Hand = await GetHand(bot.Id, sessionId);
            }

            return gameViewModel;
        }

        public async Task<GameViewModel> GetNewGame(string playerName, int botsNumber)
        {
            if (playerName == StringHelper.DealerName)
            {
                throw new ArgumentException(StringHelper.NameReservedMessage);
            }
            _playerName = playerName;
            _botNumber = botsNumber;
            var gameViewModel = await GiveFirstCards();
            Log.Info(StringHelper.GetGameStartedMessage(gameViewModel.SessionId));
            return gameViewModel;
        }

        public async Task<GameViewModel> GetUnfinishedGame(string playerName)
        {
            if (playerName == StringHelper.DealerName)
            {
                throw new ArgumentException(StringHelper.NameReservedMessage);
            }
            var player = await _playerProvider.GetExistingPlayer(playerName);
            var lastSession = await _sessionProvider.GetByPlayerId(player.Id);
            var gameViewModel = await GetGame(player.Id, lastSession.Id);
            Log.Info(StringHelper.GetGameLoadedMessage(lastSession.Id));
            return gameViewModel;
        }

        private async Task<GameViewModel> GetFinishedGame(int playerId, int sessionId)
        {
            Log.Info(StringHelper.BotsTurnMessage);
            var gameViewModel = await GetGame(playerId, sessionId);
            await GiveCardsToDealer(gameViewModel.Dealer.Id, sessionId);
            var dealerScore = gameViewModel.Dealer.Hand.Score;

            foreach (var bot in gameViewModel.Bots)
            {
                await GiveCardsToBot(bot.Id, sessionId);
            }

            await CloseGameSession(sessionId);
            gameViewModel = await UpdateGameResult(playerId, sessionId);
            await _playerProvider.DeleteSessionBots(sessionId);
            return gameViewModel;
        }

        private async Task<GameViewModel> UpdateGameResult(int playerId, int sessionId)
        {
            var gameViewModel = await GetGame(playerId, sessionId);
            var playerScore = gameViewModel.Player.Hand.Score;
            var dealerScore = gameViewModel.Dealer.Hand.Score;
            var playerGameResult = GetGameResult(playerScore, dealerScore);
            gameViewModel.Player.GameResult = (int)playerGameResult;
            gameViewModel.Player.GameResultMessage = playerGameResult.ToString();
            await _historyProvider.Create(gameViewModel.Dealer.Name, 
                StringHelper.GetScoreMessage(dealerScore), sessionId);
            await _historyProvider.Create(gameViewModel.Player.Name, 
                StringHelper.GetScoreMessage(playerScore), sessionId);

            foreach (var bot in gameViewModel.Bots)
            {
                var botScore = bot.Hand.Score;
                var botGameResult = GetGameResult(botScore, dealerScore);
                bot.GameResult = (int)botGameResult;
                bot.GameResultMessage = botGameResult.ToString();
                await _historyProvider.Create(bot.Name, 
                    StringHelper.GetScoreMessage(botScore), sessionId);
            }
            return gameViewModel;
        }

        private async Task<GameViewModel> GiveFirstCards()
        {
            GameViewModel gameViewModel = await CreateGame();
            List<int> playerIds = new List<int>
            {
                gameViewModel.Player.Id,
                gameViewModel.Dealer.Id
            };
            foreach (var bot in gameViewModel.Bots)
            {
                playerIds.Add(bot.Id);
            }
            await GiveFirstTwoCards(playerIds, gameViewModel.SessionId);
            gameViewModel = await GetGame(gameViewModel.Player.Id, gameViewModel.SessionId);
            return gameViewModel;
        }

        public async Task<GameViewModel> MakeHitDecision(int playerId, int sessionId)
        {
            Player player = await _playerProvider.GetPlayerById(playerId);
            await _historyProvider.Create(player.Name, 
                StringHelper.ChoseToHitMessage, sessionId);
            Log.Info(StringHelper.GetPlayerIdHitsMessage(playerId));
            await GiveCard(playerId, sessionId);
            GameViewModel gameViewModel = await GetGame(playerId, sessionId);
            int handValue = await GetHandScore(playerId, sessionId);
            if (handValue >= ValueHelper.BlackjackValue)
            {
                gameViewModel = await GetFinishedGame(playerId, sessionId);
            }
            return gameViewModel;
        }

        public async Task<GameViewModel> MakeStandDecision(int playerId, int sessionId)
        {
            Player player = await _playerProvider.GetPlayerById(playerId);
            await _historyProvider.Create(player.Name, 
                StringHelper.ChoseToStandMessage, sessionId);
            Log.Info(StringHelper.GetPlayerIdStandsMessage(playerId));
            GameViewModel gameViewModel = await GetFinishedGame(playerId, sessionId);
            return gameViewModel;
        }

        public async Task<GameViewModel> MakeDoubleDownDecision(int playerId, int sessionId)
        {
            Player player = await _playerProvider.GetPlayerById(playerId);
            await _historyProvider.Create(player.Name, 
                StringHelper.ChoseToDoubleMessage, sessionId);
            Log.Info(StringHelper.GetPlayerIdDoubleDownMessage(playerId));
            await GiveCard(playerId, sessionId);
            GameViewModel gameViewModel = await GetFinishedGame(playerId, sessionId);
            return gameViewModel;
        }

        public async Task<GameViewModel> MakeSurrenderDecision(int playerId, int sessionId)
        {
            Player player = await _playerProvider.GetPlayerById(playerId);
            await _historyProvider.Create(player.Name, 
                StringHelper.ChoseToSurrenderMessage, sessionId);
            Log.Info(StringHelper.GetPlayerIdSurrenderMessage(playerId));
            await _cardProvider.ClearPlayerHand(playerId, sessionId);
            GameViewModel gameViewModel = await GetFinishedGame(playerId, sessionId);
            return gameViewModel;
        }

        private async Task<bool> GiveCardsToBot(int botId, int sessionId)
        {
            int handValue = await GetHandScore(botId, sessionId);
            if (handValue > ValueHelper.MinimumBotHandValue)
            {
                return false;
            }
            await GiveCard(botId, sessionId);
            return await GiveCardsToBot(botId, sessionId);
        }

        private async Task<bool> GiveCardsToDealer(int dealerId, int sessionId)
        {
            Log.Info(StringHelper.DealerTurnMessage);
            int handValue = await GetHandScore(dealerId, sessionId);
            if (handValue > ValueHelper.MinimumDealerHandValue)
            {
                return false;
            }
            await GiveCard(dealerId, sessionId);
            return await GiveCardsToDealer(dealerId, sessionId);
        }

        private async Task GiveFirstTwoCards(List<int> playerIds, int sessionId)
        {
            foreach (var playerId in playerIds)
            {
                await GiveCard(playerId, sessionId);
                await GiveCard(playerId, sessionId);
            }
        }

        private async Task GiveCard(int playerId, int sessionId)
        {
            var deck = await _cardProvider.GetShuffledDeck();
            var card = deck[0];
            var player = await _playerProvider.GetPlayerById(playerId);
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

        private async Task<List<CardInfo>> GetCards(int playerId, int sessionId)
        {
            var player = await _playerProvider.GetPlayerById(playerId);
            var cards = await _cardProvider.GetPlayerHand(playerId, sessionId);
            List<CardInfo> cardInfos = MapHelper.GetCardsInfo(cards);
            return cardInfos;
        }

        private async Task<HandInfo> GetHand(int playerId, int sessionId)
        {
            List<CardInfo> cardInfos = await GetCards(playerId, sessionId);
            HandInfo handInfo = new HandInfo
            {
                Cards = cardInfos,
                Score = await GetHandScore(playerId, sessionId)
            };
            return handInfo;
        }

        private async Task<int> GetHandScore(int playerId, int sessionId)
        {
            int totalScore = 0;
            int aceCount = 0;

            List<CardInfo> cards = await GetCards(playerId, sessionId);

            foreach (var card in cards)
            {
                int aceCardRank = (int)CardRanks.Rank.Ace;
                int tenCardRank = (int)CardRanks.Rank.Ten;

                if (card.RankValue == aceCardRank)
                {
                    totalScore += ValueHelper.AceCardValue;
                    aceCount++;
                    continue;
                }
                if (card.RankValue > tenCardRank)
                {
                    totalScore += ValueHelper.FaceCardValue;
                    continue;
                }
                totalScore += card.RankValue;
            }

            if (totalScore > ValueHelper.BlackjackValue)
            {
                totalScore -= aceCount * ValueHelper.AceDelta;
            }

            return totalScore;
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
    }
}