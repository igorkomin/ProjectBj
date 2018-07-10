using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DAL.EF;
using ProjectBj.DAL.Interfaces;
using System.Data.Entity;

namespace ProjectBj.DAL.Repositories
{
    public class PlayerRepository : IRepository<Player>
    {
        private BjContext _db;

        public PlayerRepository(BjContext context)
        {
            _db = context;
        }

        public void Create(Player player)
        {
            _db.Players.Add(player);
            _db.SaveChanges();
        }

        public void Attach(Player player)
        {
            _db.Players.Attach(player);
        }

        public void Detach(Player player)
        {
            _db.Entry(player).State = EntityState.Detached;
        }

        public void Delete(int id)
        {
            Player player = _db.Players.Find(id);
            if (player != null)
            {
                _db.Players.Remove(player);
            }
            _db.SaveChanges();
        }

        public ICollection<Player> Find(Func<Player, bool> predicate)
        {
            List<Player> foundPlayers = _db.Players.Include(x => x.Cards).Where(predicate).ToList();
            return foundPlayers;
        }

        public Player Get(int id)
        {
            Player player = _db.Players.Include(x => x.Cards).Where(x => x.Id == id).SingleOrDefault();
            return player;
        }

        public ICollection<Player> GetAll()
        {
            List<Player> allPlayers = _db.Players.Include(x => x.Cards).ToList();
            return allPlayers;
        }

        public void Update(Player player)
        {
            _db.Entry(player).State = EntityState.Modified;
        }
    }
}
