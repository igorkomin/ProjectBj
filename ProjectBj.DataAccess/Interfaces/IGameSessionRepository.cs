using ProjectBj.Entities;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Interfaces
{
    public interface IGameSessionRepository
    {
        Task<GameSession> GetById(int id);
        Task<GameSession> Create(GameSession session);
        Task<GameSession> GetUnfinishedSession(int playerId);
        Task Update(GameSession session);
    }
}