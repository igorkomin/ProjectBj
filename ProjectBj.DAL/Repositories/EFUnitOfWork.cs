using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DAL.EF;
using ProjectBj.DAL.Interfaces;

namespace ProjectBj.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private BjContext db;
        private PlayerRepository playerRepository;
        private CardRepository cardRepository;

        public EFUnitOfWork()
        {
            db = new BjContext();
        }

        public IRepository<Player> Players
        {
            get
            {
                if (playerRepository == null)
                    playerRepository = new PlayerRepository(db);
                return playerRepository;
            }
        }

        public IRepository<Card> Cards
        {
            get
            {
                if (cardRepository == null)
                    cardRepository = new CardRepository(db);
                return cardRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    db.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
