using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.DataAccess.Interfaces
{
    public interface IPlayerRepository
    {
        Task<Player> CreateOne(Player player);
        Task<Player> GetById(int id);
        Task<ICollection<Player>> CreateMany(ICollection<Player> players);
        Task<ICollection<Player>> FindPlayers(string name);
        Task<ICollection<Player>> GetAllPlayers();
        Task<ICollection<Player>> GetSessionBots(int sessionId);
        Task<ICollection<Card>> GetCards(Player player, int sessionId);
        Task Delete(Player player);
        Task Update(Player player);
        Task AddCard(Player player, int cardId, int sessionId);
        Task DeleteCards(Player player);
        Task DeleteNonHumanPlayers(string dealerName);
    }
}