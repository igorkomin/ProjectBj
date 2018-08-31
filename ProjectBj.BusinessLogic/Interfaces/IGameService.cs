using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.ViewModels.Game;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IGameService
    {
        Task<GameViewModel> CreateGameViewModel();
        Task<GameViewModel> DealFirstCards();
        Task<GameViewModel> Hit(int playerId, int sessionId);
        Task<GameViewModel> NewGame(string playerName, int botsNumber, int bet);
        Task<GameViewModel> LoadGame(string playerName);
        Task<GameViewModel> Stand(int playerId, int sessionId);
        Task<GameViewModel> UpdateGameResult(int playerId, int sessionId);
        Task DealCard(int playerId, int sessionId);
        Task DealFirstTwoCards(List<int> playerIds, int sessionId);
        Task<List<LogEntryViewModel>> GetLogs(int sessionId);
        Task<bool> DealerTurn(int dealerId, int sessionId);
        Task CloseGameSession(int sessionId);
        Task UpdateViewModel(GameViewModel gameViewModel);
    }
}
