using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Logger;
using ProjectBj.ViewModels.Game;
using ProjectBj.BusinessLogic.Enums;
using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;

namespace ProjectBj.BusinessLogic.Services
{
    public class GameService : IGameService
    {
        public string _playerName { get; set; }
        public int _botNumber { get; set; }
        private readonly IDeckProvider _deckProvider;
        private readonly ILogProvider _logProvider;
        private readonly IPlayerProvider _playerProvider;
        private readonly ISessionProvider _sessionProvider;
        
        public GameService(IDeckProvider deckProvider, ILogProvider logProvider, 
            IPlayerProvider playerProvider, ISessionProvider sessionProvider)
        {
            _deckProvider = deckProvider;
            _logProvider = logProvider;
            _playerProvider = playerProvider;
            _sessionProvider = sessionProvider;
        }
        
        public async Task<GameViewModel> GetGameViewModel()
        {
            var player = await _playerProvider.GetPlayerViewModel(_playerName);
            var session = await _sessionProvider.CreateSession();
            var bots = await _playerProvider.GetBotViewModels(_botNumber, session.Id);
            var dealer = await _playerProvider.GetDealer();
            
            GameViewModel gameViewModel = new GameViewModel
            {
                Player = player,
                Dealer = dealer,
                Bots = bots,
                SessionId = session.Id
            };

            await UpdateViewModel(gameViewModel);
            return gameViewModel;
        }

        public async Task UpdateViewModel(GameViewModel gameViewModel)
        {
            var session = gameViewModel.SessionId;
            var player = gameViewModel.Player;
            var dealer = gameViewModel.Dealer;
            var bots = gameViewModel.Bots;

            gameViewModel.Player.Hand = await _playerProvider.GetHandViewModel(player.Id, session);
            gameViewModel.Dealer.Hand = await _playerProvider.GetHandViewModel(dealer.Id, session);

            foreach (var bot in bots)
            {
                bot.Hand = await _playerProvider.GetHandViewModel(bot.Id, session);
            }
        }

        public async Task<GameViewModel> UpdateViewModel(int playerId, int sessionId)
        {
            var session = sessionId;
            var player = await _playerProvider.GetPlayerById(playerId);
            var dealer = await _playerProvider.GetDealer();
            var bots = await _playerProvider.GetSessionBotViewModels(session);

            player.Hand = await _playerProvider.GetHandViewModel(player.Id, session);
            dealer.Hand = await _playerProvider.GetHandViewModel(dealer.Id, session);

            foreach (var bot in bots)
            {
                bot.Hand = await _playerProvider.GetHandViewModel(bot.Id, session);
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

        public async Task<GameViewModel> MakeBet(int playerId, int sessionId, int betValue)
        {
            var gameViewModel = await UpdateViewModel(playerId, sessionId);
            gameViewModel.Player.Bet = betValue;
            return gameViewModel;
        }

        public async Task<GameViewModel> UpdateGameResult(int playerId, int sessionId)
        {
            var gameViewModel = await UpdateViewModel(playerId, sessionId);
            var playerScore = gameViewModel.Player.Hand.Score;
            var dealerScore = gameViewModel.Dealer.Hand.Score;
            var bet = gameViewModel.Player.Bet;
            var result = await GetGameResult(playerId, playerScore, dealerScore, bet);
            gameViewModel.Player.GameResult = (int)result;
            gameViewModel.Player.GameResultMessage = result.ToString();
            await _logProvider.CreateLogEntry(StringHelper.PlayerScore(gameViewModel.Dealer.Name, dealerScore), sessionId);
            await _logProvider.CreateLogEntry(StringHelper.PlayerScore(gameViewModel.Player.Name, playerScore), sessionId);

            foreach (var bot in gameViewModel.Bots)
            {
                var botScore = bot.Hand.Score;
                var botBet = ValueHelper.BotBetValue;
                result = await GetGameResult(bot.Id, botScore, dealerScore, botBet);
                bot.GameResult = (int)result;
                bot.GameResultMessage = result.ToString();
                await _logProvider.CreateLogEntry(StringHelper.PlayerScore(bot.Name, botScore), sessionId);
            }
            return gameViewModel;
        }

        public async Task<GameViewModel> DealFirstCards()
        {
            GameViewModel model = await GetGameViewModel();
            List<int> playerIds = new List<int>
            {
                model.Player.Id,
                model.Dealer.Id
            };
            foreach (var bot in model.Bots)
            {
                playerIds.Add(bot.Id);
            }
            await _playerProvider.DealFirstTwoCards(playerIds, model.SessionId);
            await UpdateViewModel(model);
            return model;
        }

        public async Task<GameViewModel> NewGame(string playerName, int botsNumber)
        {
            if (playerName == StringHelper.DealerName)
            {
                throw new Exception(StringHelper.NameReserved);
            }
            _playerName = playerName;
            _botNumber = botsNumber;
            var gameViewModel = await DealFirstCards();
            Log.Info(StringHelper.GameStarted(gameViewModel.SessionId));
            return gameViewModel;
        }

        public async Task<GameViewModel> LoadGame(string playerName)
        {
            if(playerName == StringHelper.DealerName)
            {
                throw new Exception(StringHelper.NameReserved);
            }
            var player = await _playerProvider.PullPlayer(playerName);
            var lastSession = await _sessionProvider.GetSessionByPlayerId(player.Id);
            var gameViewModel = await UpdateViewModel(player.Id, lastSession.Id);
            Log.Info(StringHelper.GameLoaded(lastSession.Id));
            return gameViewModel;
        }

        public async Task<GameViewModel> Hit(int playerId, int sessionId)
        {
            Log.Info(StringHelper.PlayerHits(playerId));
            await _playerProvider.DealCard(playerId, sessionId);
            GameViewModel gameViewModel = await UpdateViewModel(playerId, sessionId);
            int handValue = await _playerProvider.GetHandValue(playerId, sessionId);
            if (handValue >= ValueHelper.BlackjackValue)
            {
                gameViewModel = await BotsTurn(playerId, sessionId);
            }
            return gameViewModel;
        }

        public async Task<GameViewModel> Stand(int playerId, int sessionId)
        {
            GameViewModel gameViewModel = await BotsTurn(playerId, sessionId);
            Log.Info(StringHelper.PlayerStands(playerId));
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
            int handValue = await _playerProvider.GetHandValue(botId, sessionId);
            if (handValue > ValueHelper.MinBotHandValue)
            {
                return false;
            }
            await _playerProvider.DealCard(botId, sessionId);
            return await BotTurn(botId, sessionId);
        }

        public async Task<bool> DealerTurn(int dealerId, int sessionId)
        {
            Log.Info(StringHelper.DealerTurn);
            int handValue = await _playerProvider.GetHandValue(dealerId, sessionId);
            if (handValue > ValueHelper.MinDealerHandValue)
            {
                return false;
            }
            await _playerProvider.DealCard(dealerId, sessionId);
            return await DealerTurn(dealerId, sessionId);
        }

        public async Task CloseGameSession(int sessionId)
        {
            await _sessionProvider.CloseSession(sessionId);
            Log.Info(StringHelper.GameEnded(sessionId));
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
                    Message = entry.Message
                };
                logEntryViewModels.Add(logEntryViewModel);
            }
            return logEntryViewModels;
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