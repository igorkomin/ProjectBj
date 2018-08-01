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
using ProjectBj.Service.Utility;

namespace ProjectBj.Service.Providers
{
    public class GameProvider
    {
        IDeckService _deckService;
        IGameService _gameService;
        ILogService _logService;
        IPlayerService _playerService;
        ISessionService _sessionService;
        string _playerName;
        int _botNumber;

        public GameProvider(string playerName, int botNumber)
        {
            _playerName = playerName;
            _botNumber = botNumber;
            _deckService = ContainerProvider.Container.Resolve<IDeckService>();
            _gameService = ContainerProvider.Container.Resolve<IGameService>();
            _logService = ContainerProvider.Container.Resolve<ILogService>();
            _playerService = ContainerProvider.Container.Resolve<IPlayerService>();
            _sessionService = ContainerProvider.Container.Resolve<ISessionService>();
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

            var playerHand = await _playerService.GetCards(player.Id, session);
            var dealerHand = await _playerService.GetCards(dealer.Id, session);

            gameViewModel.Player.Hand = await _deckService.GetCardViewModels(playerHand);
            gameViewModel.Dealer.Hand = await _deckService.GetCardViewModels(dealerHand);

            foreach (var bot in bots)
            {
                var botHand = await _playerService.GetCards(bot.Id, session);
                bot.Hand = await _deckService.GetCardViewModels(botHand);
            }
        }

        public async Task<GameViewModel> MakeBet(int playerId, int betValue)
        {
            var gameViewModel = await GetGameViewModel();
            gameViewModel.Player.Bet = betValue;
            return gameViewModel;
        }

        public async Task SetGameResult()
        {
            var gameViewModel = await GetGameViewModel();

            var sessionId = gameViewModel.SessionId;
            var playerId = gameViewModel.Player.Id;
            var playerScore = await _playerService.GetHandValue(gameViewModel.Player.Id, sessionId);
            var dealerScore = await _playerService.GetHandValue(gameViewModel.Dealer.Id, sessionId);
            var bet = gameViewModel.Player.Bet;

            var result = await _gameService.GetGameResult(playerId, playerScore, dealerScore, bet);
            gameViewModel.Player.GameResult = (int)result;

            foreach(var bot in gameViewModel.Bots)
            {
                var botScore = await _playerService.GetHandValue(bot.Id, sessionId);
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

        public async Task<GameViewModel> BotsTurn(int sessionId)
        {
            var gameViewModel = await GetGameViewModel();

            foreach (var bot in gameViewModel.Bots)
            {
                await BotTurn(bot.Id, sessionId);
            }

            await DealerTurn(gameViewModel.Dealer.Id, sessionId);   

            // TODO: Get game result
            // TODO: Close session

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
    }
}