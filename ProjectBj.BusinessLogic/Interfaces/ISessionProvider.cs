using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface ISessionProvider
    {
        Task<SessionViewModel> CreateSession();
        Task<SessionViewModel> GetSessionByPlayerId(int playerId);
        Task<GameSession> GetSessionById(int id);
        Task CloseSession(int sessionId);
    }
}