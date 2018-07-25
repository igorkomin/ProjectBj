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
        Player CreateOne(Player player);
        Player Get(int id);
        ICollection<Player> CreateMany(ICollection<Player> players);
        ICollection<Player> FindPlayers(string name);
        ICollection<Player> GetAllPlayers();
        ICollection<Card> GetCards(Player player);
        void Delete(int id);
        void Update(Player player);
        void AddCard(Player player, Card card);
        void DeleteCards(Player player);
        void DeletePlayersByName(string name);
    }
}