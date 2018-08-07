using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.ViewModels.Game;
using ProjectBj.Service.Enums;
using ProjectBj.Service.Helpers;
using ProjectBj.Service.Interfaces;

namespace ProjectBj.Service.Providers
{
    public class GameProvider : IGameProvider
    {
        public string _playerName { get; set; }
        public int _botNumber { get; set; }
        private IDeckService _deckService;
        private IGameService _gameService;
        private ILogService _logService;
        private IPlayerService _playerService;
        private ISessionService _sessionService;
        
        public GameProvider(IDeckService deckService, IGameService gameService, 
            ILogService logService, IPlayerService playerService, ISessionService sessionService)
        {
            _deckService = deckService;
            _gameService = gameService;
            _logService = logService;
            _playerService = playerService;
            _sessionService = sessionService;
        }
        
        public async Task<GameViewModel> GetGameViewModel()
        {
            var player = await _playerService.GetPlayerViewModel(_playerName);
            var session = await _sessionService.GetSessionByPlayerId(player.Id);
            var dealer = await _playerService.GetDealerViewModel();
            var bots = await _playerService.GetBotViewModelList(_botNumber, session);
            
            GameViewModel gameViewModel = new GameViewModel
            {
                Player = player,
                Dealer = dealer,
                Bots = bots,
                SessionId = session
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

            gameViewModel.Player.Hand = await _playerService.GetHandViewModel(player.Id, session);
            gameViewModel.Dealer.Hand = await _playerService.GetHandViewModel(dealer.Id, session);

            foreach (var bot in bots)
            {
                bot.Hand = await _playerService.GetHandViewModel(bot.Id, session);
            }
        }

        public async Task<GameViewModel> MakeBet(int playerId, int betValue)
        {
            var gameViewModel = await GetGameViewModel();
            gameViewModel.Player.Bet = betValue;
            return gameViewModel;
        }

        public async Task UpdateGameResult()
        {
            var gameViewModel = await GetGameViewModel();

            var sessionId = gameViewModel.SessionId;
            var playerId = gameViewModel.Player.Id;
            
            var playerScore = gameViewModel.Player.Hand.Score;
            var dealerScore = gameViewModel.Dealer.Hand.Score;

            var bet = gameViewModel.Player.Bet;

            var result = await _gameService.GetGameResult(playerId, playerScore, dealerScore, bet);
            gameViewModel.Player.GameResult = (int)result;

            foreach(var bot in gameViewModel.Bots)
            {
                var botScore = bot.Hand.Score;
                var botBet = ValueHelper.BotBetValue;
                result = await _gameService.GetGameResult(bot.Id, botScore, dealerScore, botBet);
                bot.GameResult = (int)result;
            }
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
            await _deckService.DealFirstTwoCards(playerIds, model.SessionId);
            await UpdateViewModel(model);
            return model;
        }

        public async Task<GameViewModel> NewGame()
        {
            var gameViewModel = await DealFirstCards();
            return gameViewModel;
        }

        public async Task<GameViewModel> Hit(int playerId, int sessionId)
        {
            await _deckService.DealCard(playerId, sessionId);
            var gameViewModel = await GetGameViewModel();
            var handValue = await _playerService.GetHandValue(playerId, sessionId);
            if (handValue > ValueHelper.BlackjackValue)
            {
                await BotsTurn(sessionId);
            }
            return gameViewModel;
        }

        public async Task<GameViewModel> Stand(int playerId, int sessionId)
        {
            var gameViewModel = await GetGameViewModel();
            await BotsTurn(sessionId);
            return gameViewModel;
        }

        public async Task<GameViewModel> BotsTurn(int sessionId)
        {
            var gameViewModel = await GetGameViewModel();

            foreach (var bot in gameViewModel.Bots)
            {
                await BotTurn(bot.Id, sessionId);
            }
            await DealerTurn(gameViewModel.Dealer.Id, sessionId);
            await UpdateGameResult();
            await CloseGameSession(sessionId);
            return gameViewModel;
        }

        public async Task<bool> BotTurn(int botId, int sessionId)
        {
            int handValue = await _playerService.GetHandValue(botId, sessionId);
            if (handValue > ValueHelper.MinBotHandValue)
            {
                return false;
            }
            await _deckService.DealCard(botId, sessionId);
            return await BotTurn(botId, sessionId);
        }

        public async Task<bool> DealerTurn(int dealerId, int sessionId)
        {
            int handValue = await _playerService.GetHandValue(dealerId, sessionId);
            if (handValue > ValueHelper.MinDealerHandValue)
            {
                return false;
            }
            await _deckService.DealCard(dealerId, sessionId);
            return await DealerTurn(dealerId, sessionId);
        }

        public async Task CloseGameSession(int sessionId)
        {
            await _sessionService.CloseSession(sessionId);
        }
    }
}