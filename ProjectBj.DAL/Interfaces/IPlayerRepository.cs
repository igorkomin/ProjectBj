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
        ICollection<Card> GetCards(Player player);
        Task Delete(int id);
        Task Update(Player player);
        void AddCard(Player player, Card card);
        void DeleteCards(Player player);
        void DeletePlayersByName(string name);
    }
}