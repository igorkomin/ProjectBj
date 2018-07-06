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
        private BjContext _db;
        private PlayerRepository _playerRepository;
        private CardRepository _cardRepository;
        private bool _disposed = false;

        public EFUnitOfWork()
        {
            _db = new BjContext();
        }

        public IRepository<Player> Players
        {
            get
            {
                if (_playerRepository == null)
                {
                    _playerRepository = new PlayerRepository(_db);
                }
                return _playerRepository;
            }
        }

        public IRepository<Card> Cards
        {
            get
            {
                if (_cardRepository == null)
                {
                    _cardRepository = new CardRepository(_db);
                }
                return _cardRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }


        public virtual void Dispose(bool disposing)
        {
            if(!this._disposed)
            {
                if(disposing)
                {
                    _db.Dispose();
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
