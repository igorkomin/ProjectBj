using ProjectBj.Entities;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories.Interfaces
{
    public interface IGameSessionRepository : IBaseRepository<GameSession>
    {
        Task<GameSession> GetFirstOpen(long playerId);
    }
}