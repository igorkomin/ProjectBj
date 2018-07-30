using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.DAL.Interfaces
{
    interface IPlayerRepository
    {
        Task<Player> CreateOne(Player player);
        Task<Player> Get(int id);
        Task<ICollection<Player>> CreateMany(ICollection<Player> players);
        Task<ICollection<Player>> FindPlayers(string name);
        Task<ICollection<Player>> GetAllPlayers();
        Task<ICollection<Card>> GetCards(Player player, int sessionId);
        Task Delete(int id);
        Task Update(Player player);
        Task AddCard(Player player, Card card, int sessionId);
        Task DeleteCards(Player player);
        Task DeletePlayersByName(string name);
    }
}