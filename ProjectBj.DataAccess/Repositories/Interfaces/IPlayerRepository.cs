using ProjectBj.Entities;
using ProjectBj.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories.Interfaces
{
    public interface IPlayerRepository : IBaseRepository<Player>
    {
        Task<IEnumerable<Player>> GetByType(PlayerType playerType);
        Task<IEnumerable<Player>> Find(string name);
        Task<IEnumerable<Player>> GetSessionBots(long sessionId);
        Task AddCardsToPlayerHand(Player player, IEnumerable<long> cardIds, long sessionId);
        Task DeleteBotsFromSession(long sessionId);
    }
}