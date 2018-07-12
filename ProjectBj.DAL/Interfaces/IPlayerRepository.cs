using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.DAL.Interfaces
{
    public interface IPlayerRepository
    {
        Player Get(int id);
        Player Create(Player player);
        ICollection<Player> GetAllPlayers();
        ICollection<Player> FindPlayers(string name);
        ICollection<Card> GetCards(Player player);
        void Update(Player player);
        void AddCard(Player player, Card card);
        void Delete(int id);
    }
}
