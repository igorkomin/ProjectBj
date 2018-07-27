using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.ViewModels.Game;

namespace ProjectBj.Service.Providers
{
    public class GameProvider
    {
        DeckService _deckService;
        GameService _gameService;
        LogService _logService;
        PlayerService _playerService;
        SessionService _sessionService;

        public GameProvider()
        {
            _deckService = new DeckService();
            _gameService = new GameService();
            _logService = new LogService();
            _playerService = new PlayerService();
            _sessionService = new SessionService();
        }
        
        public async Task<GameViewModel> PrepareGameViewModel(string playerName, int botNumber)
        {
            GameViewModel gameViewModel = new GameViewModel();

            gameViewModel.Player = await _playerService.PreparePlayerViewModel(playerName);
            gameViewModel.Dealer = await _playerService.PrepareDealerViewModel();
            gameViewModel.Bots = await _playerService.PrepareBotViewModelList(botNumber);

            return gameViewModel;
        }


    }
}
