using ProjectBj.Entities;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Managers.Interfaces
{
    public interface IGameSessionManager
    {
        Task<GameSession> GetNew();
        Task<GameSession> GetByPlayerId(long playerId);
        Task Close(long sessionId);
    }
}