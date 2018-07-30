using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.ViewModels.Game;
using ProjectBj.Service.Enums;
using ProjectBj.Service.Helpers;

namespace ProjectBj.Service.Providers
{
    public class GameProvider
    {
        DeckService _deckService;
        GameService _gameService;
        LogService _logService;
        PlayerService _playerService;
        SessionService _sessionService;
        string _playerName;
        int _botNumber;

        public GameProvider(string playerName, int botNumber)
        {
            _playerName = playerName;
            _botNumber = botNumber;
            _deckService = new DeckService();
            _gameService = new GameService();
            _logService = new LogService();
            _playerService = new PlayerService();
            _sessionService = new SessionService();
        }
        
        public async Task<GameViewModel> GetGameViewModel()
        {
            var player = await _playerService.GetPlayerViewModel(_playerName);
            var session = await _sessionService.GetSession(player.Id);
            var dealer = await _playerService.GetDealerViewModel();
            var bots = await _playerService.GetBotViewModelList(_botNumber, session);
            
            GameViewModel gameViewModel = new GameViewModel
            {
                Player = player,
                Dealer = dealer,
                Bots = bots,
                SessionId = session
            };

            gameViewModel.Player.Hand = await _playerService.GetCards(player.Id, session);
            gameViewModel.Dealer.Hand = await _playerService.GetCards(dealer.Id, session);

            foreach(var bot in gameViewModel.Bots)
            {
                bot.Hand = await _playerService.GetCards(bot.Id, session);
            }

            return gameViewModel;
        }

        public async Task<GameResults.Result> GetGameResult()
        {
            var gameViewModel = await GetGameViewModel();
            var playerId = gameViewModel.Player.Id;
            var dealerId = gameViewModel.Dealer.Id;
            var sessionId = gameViewModel.SessionId;
            var playerScore = await _playerService.GetHandValue(playerId, sessionId);
            var dealerScore = await _playerService.GetHandValue(dealerId, sessionId);
            var bet = gameViewModel.Player.Bet;
            var result = await _gameService.GetGameResult(playerId, playerScore, dealerScore, bet);
            return result;
        }

        public async Task<GameViewModel> NewGame()
        {
            var gameViewModel = await GetGameViewModel();
            
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