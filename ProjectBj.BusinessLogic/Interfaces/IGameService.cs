using ProjectBj.ViewModels.Game;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IGameService
    {
        Task<GameViewModel> GetNewGame(string playerName, int botsNumber, int bet);
        Task<GameViewModel> GetUnfinishedGame(string playerName);
        Task<GameViewModel> MakeHitDecision(int playerId, int sessionId);
        Task<GameViewModel> MakeStandDecision(int playerId, int sessionId);
        Task<GameViewModel> MakeDoubleDownDecision(int playerId, int sessionId);
        Task<GameViewModel> MakeSurrenderDecision(int playerId, int sessionId);
        Task<List<LogEntryViewModel>> GetSessionLogs(int sessionId);
        Task<List<LogEntryViewModel>> GetAllLogs();
    }
}
