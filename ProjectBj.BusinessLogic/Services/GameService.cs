using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly IGameProvider _gameProvider;
        private readonly ILogProvider _logProvider;
        private readonly IPlayerProvider _playerProvider;
        private readonly ISessionProvider _sessionProvider;
        
        public GameService(IDeckProvider deckProvider, IGameProvider gameProvider, 
            ILogProvider logProvider, IPlayerProvider playerProvider, ISessionProvider sessionProvider)
        {
            _deckProvider = deckProvider;
            _gameProvider = gameProvider;
            _logProvider = logProvider;
            _playerProvider = playerProvider;
            _sessionProvider = sessionProvider;
        }
        
        public async Task<GameViewModel> GetGameViewModel()
        {
            var player = await _playerProvider.GetPlayerViewModel(_playerName);
            var session = await _sessionProvider.GetSessionByPlayerId(player.Id);
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
            var result = await _gameProvider.GetGameResult(playerId, playerScore, dealerScore, bet);
            gameViewModel.Player.GameResult = (int)result;
            gameViewModel.Player.GameResultMessage = result.ToString();

            foreach(var bot in gameViewModel.Bots)
            {
                var botScore = bot.Hand.Score;
                var botBet = ValueHelper.BotBetValue;
                result = await _gameProvider.GetGameResult(bot.Id, botScore, dealerScore, botBet);
                bot.GameResult = (int)result;
                bot.GameResultMessage = result.ToString();
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
            await _deckProvider.DealFirstTwoCards(playerIds, model.SessionId);
            await UpdateViewModel(model);
            return model;
        }

        public async Task<GameViewModel> NewGame(string playerName, int botsNumber)
        {
            _playerName = playerName;
            _botNumber = botsNumber;
            var gameViewModel = await DealFirstCards();
            return gameViewModel;
        }

        public async Task<GameViewModel> LoadGame(int playerId, int sessionId)
        {
            var gameViewModel = await UpdateViewModel(playerId, sessionId);
            return gameViewModel;
        }

        public async Task<GameViewModel> Hit(int playerId, int sessionId)
        {
            await _deckProvider.DealCard(playerId, sessionId);
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
            await BotsTurn(playerId, sessionId);
            var gameViewModel = await UpdateGameResult(playerId, sessionId);
            return gameViewModel;
        }

        private async Task<GameViewModel> BotsTurn(int playerId, int sessionId)
        {
            var gameViewModel = await UpdateViewModel(playerId, sessionId);

            foreach (var bot in gameViewModel.Bots)
            {
                await BotTurn(bot.Id, sessionId);
            }
            await DealerTurn(gameViewModel.Dealer.Id, sessionId);
            await UpdateGameResult(playerId, sessionId);
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
            await _deckProvider.DealCard(botId, sessionId);
            return await BotTurn(botId, sessionId);
        }

        public async Task<bool> DealerTurn(int dealerId, int sessionId)
        {
            int handValue = await _playerProvider.GetHandValue(dealerId, sessionId);
            if (handValue > ValueHelper.MinDealerHandValue)
            {
                return false;
            }
            await _deckProvider.DealCard(dealerId, sessionId);
            return await DealerTurn(dealerId, sessionId);
        }

        public async Task CloseGameSession(int sessionId)
        {
            await _sessionProvider.CloseSession(sessionId);
        }
    }
}