using ProjectBj.ViewModels.Game;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<GameViewModel> DoubleDown(int playerId, int sessionId);
        Task<GameViewModel> Surrender(int playerId, int sessionId);
        Task<GameViewModel> UpdateGameResult(int playerId, int sessionId);
        Task DealCard(int playerId, int sessionId);
        Task DealFirstTwoCards(List<int> playerIds, int sessionId);
        Task<List<LogEntryViewModel>> GetLogs(int sessionId);
        Task<List<LogEntryViewModel>> GetAllLogs();
        Task<bool> DealerTurn(int dealerId, int sessionId);
        Task CloseGameSession(int sessionId);
        Task UpdateViewModel(GameViewModel gameViewModel);
    }
}
