using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Interfaces
{
    public interface IPlayerRepository
    {
        Task<Player> Insert(Player player);
        Task<Player> GetById(int id);
        Task<ICollection<Player>> Insert(ICollection<Player> players);
        Task<ICollection<Player>> Find(string name);
        Task<ICollection<Player>> GetSessionBots(int sessionId);
        Task Update(Player player);
        Task AddCardToPlayerHand(Player player, int cardId, int sessionId);
        Task DeleteBotsFromSession(int sessionId);
    }
}