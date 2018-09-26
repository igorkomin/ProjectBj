using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        Task<Player> Insert(Player player);
        Task<Player> GetById(long id);
        Task<IEnumerable<Player>> Insert(IEnumerable<Player> players);
        Task<IEnumerable<Player>> Find(string name);
        Task<IEnumerable<Player>> GetSessionBots(long sessionId);
        Task Update(Player player);
        Task AddCardToPlayerHand(Player player, long cardId, long sessionId);
        Task DeleteBotsFromSession(long sessionId);
    }
}