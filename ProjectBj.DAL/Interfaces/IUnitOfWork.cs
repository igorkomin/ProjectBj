using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.DAL.Interfaces
{
    interface IUnitOfWork : IDisposable
    {
        IRepository<Player> Players { get; }
        IRepository<Card> Cards { get; }
        void Save();
    }
}
