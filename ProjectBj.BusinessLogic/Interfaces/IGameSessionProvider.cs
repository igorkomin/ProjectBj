using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IGameSessionProvider
    {
        Task<GameSession> GetNew();
        Task<GameSession> GetById(int id);
        Task<GameSession> GetByPlayerId(int playerId);
        Task Close(int sessionId);
    }
}