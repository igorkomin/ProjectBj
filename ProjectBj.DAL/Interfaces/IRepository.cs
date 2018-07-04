using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.DAL.Interfaces
{
    public interface IRepository <T> where T : class
    {
        T Get(int id);
        ICollection<T> GetAll();
        ICollection<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Attach(T item);
        void Detach(T item);
        void Update(T item);
        void Delete(int id);
    }
}
