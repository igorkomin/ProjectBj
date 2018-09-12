using ProjectBj.Entities;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Interfaces
{
    public interface IGameSessionRepository
    {
        Task<GameSession> GetById(int id);
        Task<GameSession> Insert(GameSession session);
        Task<GameSession> GetFirstOpenSession(int playerId);
        Task Update(GameSession session);
    }
}