using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T: class
    {
        Task<T> Insert(T item);
        Task<T> GetById(long id);
        Task Update(T item);
        Task Delete(T item);
    }
}
