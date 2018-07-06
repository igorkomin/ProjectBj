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
        private BjContext db;

        public PlayerRepository(BjContext context)
        {
            db = context;
        }

        public void Create(Player player)
        {
            db.Players.Add(player);
            db.SaveChanges();
        }

        public void Attach(Player player)
        {
            db.Players.Attach(player);
        }

        public void Detach(Player player)
        {
            db.Entry<Player>(player).State = EntityState.Detached;
        }

        public void Delete(int id)
        {
            Player player = db.Players.Find(id);
            if (player != null)
                db.Players.Remove(player);
            db.SaveChanges();
        }

        public ICollection<Player> Find(Func<Player, bool> predicate)
        {
            List<Player> foundPlayers = db.Players.Include(x => x.Cards).Where(predicate).ToList();
            return foundPlayers;
        }

        public Player Get(int id)
        {
            Player player = db.Players.Include(x => x.Cards).Where(x => x.Id == id).SingleOrDefault();
            return player;
        }

        public ICollection<Player> GetAll()
        {
            List<Player> allPlayers = db.Players.Include(x => x.Cards).ToList();
            return allPlayers;
        }

        public void Update(Player player)
        {
            db.Entry(player).State = EntityState.Modified;
        }
    }
}
