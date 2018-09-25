using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IGameSessionProvider
    {
        Task<GameSession> GetNew();
        Task<GameSession> GetById(long id);
        Task<GameSession> GetByPlayerId(long playerId);
        Task Close(long sessionId);
    }
}