using ProjectBj.BusinessLogic.Enums;
using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;
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
        public int _bet { get; set; }
        private readonly ICardProvider _cardProvider;
        private readonly ILogProvider _logProvider;
        private readonly IPlayerProvider _playerProvider;
        private readonly ISessionProvider _sessionProvider;
        
        public GameService(ICardProvider cardProvider, ILogProvider logProvider, 
            IPlayerProvider playerProvider, ISessionProvider sessionProvider)
        {
            _cardProvider = cardProvider;
            _logProvider = logProvider;
            _playerProvider = playerProvider;
            _sessionProvider = sessionProvider;
        }
        
        public async Task<GameViewModel> CreateGameViewModel()
        {
            var player = await _playerProvider.GetPlayerViewModel(_playerName);
            var session = await _sessionProvider.CreateSession();
            var bots = await _playerProvider.GetBotViewModels(_botNumber, session.Id);
            var dealer = await _playerProvider.GetDealer();

            await _playerProvider.SetBet(player.Id, _bet);

            GameViewModel gameViewModel = new GameViewModel
            {
                Player = player,
                Dealer = dealer,
                Bots = bots,
                SessionId = session.Id
            };

            return gameViewModel;
        }

        public async Task UpdateViewModel(GameViewModel gameViewModel)
        {
            var session = gameViewModel.SessionId;
            var player = gameViewModel.Player;
            var dealer = gameViewModel.Dealer;
            var bots = gameViewModel.Bots;

            gameViewModel.Player.Hand = await GetHandViewModel(player.Id, session);
            gameViewModel.Dealer.Hand = await GetHandViewModel(dealer.Id, session);

            foreach (var bot in bots)
            {
                bot.Hand = await GetHandViewModel(bot.Id, session);
            }
        }

        public async Task<GameViewModel> UpdateViewModel(int playerId, int sessionId)
        {
            var session = sessionId;
            var player = await _playerProvider.GetPlayerById(playerId);
            var dealer = await _playerProvider.GetDealer();
            var bots = await _playerProvider.GetSessionBotViewModels(session);

            player.Hand = await GetHandViewModel(player.Id, session);
            dealer.Hand = await GetHandViewModel(dealer.Id, session);

            foreach (var bot in bots)
            {
                bot.Hand = await GetHandViewModel(bot.Id, session);
            }

            GameViewModel gameViewModel = new GameViewModel
            {
                Bots = bots,
                Dealer = dealer,
                Player = player,
                SessionId = session
            };
            return gameViewModel;
        }

        public async Task<GameViewModel> UpdateGameResult(int playerId, int sessionId)
        {
            var gameViewModel = await UpdateViewModel(playerId, sessionId);
            var playerScore = gameViewModel.Player.Hand.Score;
            var dealerScore = gameViewModel.Dealer.Hand.Score;
            var playerBet = gameViewModel.Player.Bet;
            var result = await GetGameResult(playerId, playerScore, dealerScore, playerBet);
            gameViewModel.Player.GameResult = (int)result;
            gameViewModel.Player.GameResultMessage = result.ToString();
            await _logProvider.CreateLogEntry(gameViewModel.Dealer.Name, StringHelper.GetPlayerScoreMessage(dealerScore), sessionId);
            await _logProvider.CreateLogEntry(gameViewModel.Player.Name, StringHelper.GetPlayerScoreMessage(playerScore), sessionId);

            foreach (var bot in gameViewModel.Bots)
            {
                var botScore = bot.Hand.Score;
                var botBet = ValueHelper.BotBetValue;
                result = await GetGameResult(bot.Id, botScore, dealerScore, botBet);
                bot.GameResult = (int)result;
                bot.GameResultMessage = result.ToString();
                await _logProvider.CreateLogEntry(bot.Name, StringHelper.GetPlayerScoreMessage(botScore), sessionId);
            }
            return gameViewModel;
        }

        public async Task<GameViewModel> DealFirstCards()
        {
            GameViewModel model = await CreateGameViewModel();
            List<int> playerIds = new List<int>
            {
                model.Player.Id,
                model.Dealer.Id
            };
            foreach (var bot in model.Bots)
            {
                playerIds.Add(bot.Id);
            }
            await DealFirstTwoCards(playerIds, model.SessionId);
            await UpdateViewModel(model);
            return model;
        }

        public async Task<GameViewModel> NewGame(string playerName, int botsNumber, int bet)
        {
            if (playerName == StringHelper.DealerName)
            {
                throw new ArgumentException(StringHelper.NameReserved);
            }
            _playerName = playerName;
            _botNumber = botsNumber;
            _bet = bet;
            var gameViewModel = await DealFirstCards();
            Log.Info(StringHelper.GetGameStartedMessage(gameViewModel.SessionId));
            return gameViewModel;
        }

        public async Task<GameViewModel> LoadGame(string playerName)
        {
            if(playerName == StringHelper.DealerName)
            {
                throw new ArgumentException(StringHelper.NameReserved);
            }
            var player = await _playerProvider.PullPlayer(playerName);
            var lastSession = await _sessionProvider.GetSessionByPlayerId(player.Id);
            var gameViewModel = await UpdateViewModel(player.Id, lastSession.Id);
            Log.Info(StringHelper.GetGameLoadedMessage(lastSession.Id));
            return gameViewModel;
        }

        public async Task<GameViewModel> Hit(int playerId, int sessionId)
        {
            Log.Info(StringHelper.GetPlayerHitsMessage(playerId));
            await DealCard(playerId, sessionId);
            GameViewModel gameViewModel = await UpdateViewModel(playerId, sessionId);
            int handValue = await GetHandValue(playerId, sessionId);
            if (handValue >= ValueHelper.BlackjackValue)
            {
                gameViewModel = await BotsTurn(playerId, sessionId);
            }
            return gameViewModel;
        }

        public async Task<GameViewModel> Stand(int playerId, int sessionId)
        {
            GameViewModel gameViewModel = await BotsTurn(playerId, sessionId);
            Log.Info(StringHelper.GetPlayerStandsMessage(playerId));
            return gameViewModel;
        }

        public async Task<GameViewModel> DoubleDown(int playerId, int sessionId)
        {
            int playerBet = await _playerProvider.GetBet(playerId);
            await _playerProvider.SetBet(playerId, playerBet * 2);
            await DealCard(playerId, sessionId);
            GameViewModel gameViewModel = await BotsTurn(playerId, sessionId);
            return gameViewModel;
        }

        private async Task<GameViewModel> BotsTurn(int playerId, int sessionId)
        {
            Log.Info(StringHelper.BotsTurn);
            var gameViewModel = await UpdateViewModel(playerId, sessionId);

            foreach (var bot in gameViewModel.Bots)
            {
                await BotTurn(bot.Id, sessionId);
            }
            await DealerTurn(gameViewModel.Dealer.Id, sessionId);
            await CloseGameSession(sessionId);
            gameViewModel = await UpdateGameResult(playerId, sessionId);
            return gameViewModel;
        }

        private async Task<bool> BotTurn(int botId, int sessionId)
        {
            int handValue = await GetHandValue(botId, sessionId);
            if (handValue > ValueHelper.MinBotHandValue)
            {
                return false;
            }
            await DealCard(botId, sessionId);
            return await BotTurn(botId, sessionId);
        }

        public async Task<bool> DealerTurn(int dealerId, int sessionId)
        {
            Log.Info(StringHelper.DealerTurn);
            int handValue = await GetHandValue(dealerId, sessionId);
            if (handValue > ValueHelper.MinDealerHandValue)
            {
                return false;
            }
            await DealCard(dealerId, sessionId);
            return await DealerTurn(dealerId, sessionId);
        }

        public async Task DealFirstTwoCards(List<int> playerIds, int sessionId)
        {
            foreach (var playerId in playerIds)
            {
                await DealCard(playerId, sessionId);
                await DealCard(playerId, sessionId);
            }
        }

        public async Task DealCard(int playerId, int sessionId)
        {
            var deck = await _cardProvider.GetShuffledDeck();
            var card = deck[0];
            var player = await _playerProvider.GetPlayerById(playerId);
            await _logProvider.CreateLogEntry(
                player.Name, StringHelper.GetPlayerTakesCardMessage(
                    EnumHelper.GetRankName(card.Rank), card.Suit), sessionId);
            await _playerProvider.GivePlayerCard(playerId, sessionId, card.Id);
        }

        public async Task CloseGameSession(int sessionId)
        {
            await _sessionProvider.CloseSession(sessionId);
            Log.Info(StringHelper.GetGameEndedMessage(sessionId));
        }

        public async Task<List<LogEntryViewModel>> GetLogs(int sessionId)
        {
            var logEntryViewModels = new List<LogEntryViewModel>();
            var logs = await _logProvider.GetLogs(sessionId);
            foreach (var entry in logs)
            {
                LogEntryViewModel logEntryViewModel = new LogEntryViewModel
                {
                    SessionId = entry.SessionId,
                    Time = entry.Time,
                    Player = entry.Player,
                    Message = entry.Message
                };
                logEntryViewModels.Add(logEntryViewModel);
            }
            return logEntryViewModels;
        }

        private async Task<List<CardViewModel>> GetCardViewModels(int playerId, int sessionId)
        {
            try
            {
                var player = await _playerProvider.GetPlayerById(playerId);
                var cards = await _cardProvider.GetPlayerCards(playerId, sessionId);
                List<CardViewModel> cardViewModels = new List<CardViewModel>();
                foreach (var card in cards)
                {
                    CardViewModel cardViewModel = new CardViewModel
                    {
                        Suit = card.Suit,
                        Rank = EnumHelper.GetRankName(card.Rank),
                        RankValue = card.Rank
                    };
                    cardViewModels.Add(cardViewModel);
                }
                return cardViewModels;
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                throw exception;
            }
        }

        public async Task<HandViewModel> GetHandViewModel(int playerId, int sessionId)
        {
            List<CardViewModel> cardViewModels = await GetCardViewModels(playerId, sessionId);
            HandViewModel handViewModel = new HandViewModel
            {
                Cards = cardViewModels,
                Score = await GetHandValue(playerId, sessionId)
            };
            return handViewModel;
        }

        public async Task<int> GetHandValue(int playerId, int sessionId)
        {
            int totalValue = 0;
            int aceCount = 0;

            List<CardViewModel> cards = await GetCardViewModels(playerId, sessionId);

            foreach (var card in cards)
            {
                int aceCardRank = (int)CardRanks.Rank.Ace;
                int tenCardRank = (int)CardRanks.Rank.Ten;

                if (card.RankValue == aceCardRank)
                {
                    totalValue += ValueHelper.AceCardValue;
                    aceCount++;
                    continue;
                }
                if (card.RankValue > tenCardRank)
                {
                    totalValue += ValueHelper.FaceCardValue;
                    continue;
                }
                totalValue += card.RankValue;
            }

            return totalValue > ValueHelper.BlackjackValue ?
                totalValue - aceCount * ValueHelper.AceDelta : totalValue;
        }

        public async Task<GameResults.Result> GetGameResult(int playerId, int playerScore, int dealerScore, int bet)
        {
            if (playerScore == ValueHelper.BlackjackValue)
            {
                int winAmount = (bet * 2) + (bet / 2);
                await _playerProvider.ChangePlayerBalance(playerId, winAmount);
                return GameResults.Result.Blackjack;
            }

            if (playerScore > ValueHelper.BlackjackValue)
            {
                await _playerProvider.ChangePlayerBalance(playerId, -bet);
                return GameResults.Result.Bust;
            }

            if (playerScore > dealerScore || dealerScore > ValueHelper.BlackjackValue)
            {
                await _playerProvider.ChangePlayerBalance(playerId, bet);
                return GameResults.Result.Won;
            }

            await _playerProvider.ChangePlayerBalance(playerId, -bet);
            return GameResults.Result.Lost;
        }
    }
}