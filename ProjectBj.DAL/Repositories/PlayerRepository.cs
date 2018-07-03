﻿using System;
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
            this.db = context;
        }

        public void Create(Player player)
        {
            db.Players.Add(player);
        }

        public void Delete(int id)
        {
            Player player = db.Players.Find(id);
            if (player != null)
                db.Players.Remove(player); 
        }

        public IEnumerable<Player> Find(Func<Player, bool> predicate)
        {
            return db.Players.Include(x => x.Cards).Where(predicate).ToList();
        }

        public Player Get(int id)
        {
            return db.Players.Find(id);
        }

        public IEnumerable<Player> GetAll()
        {
            return db.Players.Include(x => x.Cards);
        }

        public void Update(Player player)
        {
            db.Entry(player).State = EntityState.Modified;
        }
    }
}
