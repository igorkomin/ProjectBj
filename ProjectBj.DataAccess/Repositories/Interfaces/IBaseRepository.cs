using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T: BaseEntity
    {
        Task<T> Insert(T item);
        Task<T> GetById(long id);
        Task<IEnumerable<T>> GetAll();
        Task Update(T item);
        Task Delete(T item);
    }
}
