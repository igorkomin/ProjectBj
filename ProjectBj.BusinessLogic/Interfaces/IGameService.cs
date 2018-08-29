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
        Task<GameViewModel> DealFirstCards();
        Task<GameViewModel> GetGameViewModel();
        Task<GameViewModel> Hit(int playerId, int sessionId);
        Task<GameViewModel> MakeBet(int playerId, int sessionId, int betValue);
        Task<GameViewModel> NewGame(string playerName, int botsNumber);
        Task<GameViewModel> LoadGame(string playerName);
        Task<GameViewModel> Stand(int playerId, int sessionId);
        Task<GameViewModel> UpdateGameResult(int playerId, int sessionId);
        Task<List<LogEntryViewModel>> GetLogs(int sessionId);
        Task<bool> DealerTurn(int dealerId, int sessionId);
        Task CloseGameSession(int sessionId);
        Task UpdateViewModel(GameViewModel gameViewModel);
    }
}
