using ProjectBj.Entities;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IGameSessionProvider
    {
        Task<GameSession> GetNew();
        Task<GameSession> GetByPlayerId(long playerId);
        Task Close(long sessionId);
    }
}