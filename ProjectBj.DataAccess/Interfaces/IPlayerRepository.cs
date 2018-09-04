using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Interfaces
{
    public interface IPlayerRepository
    {
        Task<Player> Create(Player player);
        Task<Player> GetById(int id);
        Task<ICollection<Player>> Create(ICollection<Player> players);
        Task<ICollection<Player>> FindPlayers(string name);
        Task<ICollection<Player>> GetSessionBots(int sessionId);
        Task Update(Player player);
        Task AddCard(Player player, int cardId, int sessionId);
        Task DeleteNonHumanPlayers(int sessionId);
    }
}