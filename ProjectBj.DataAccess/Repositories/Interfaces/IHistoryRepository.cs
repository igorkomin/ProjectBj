using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Repositories.Interfaces
{
    public interface IHistoryRepository
    {
        Task<History> Insert(History entry);
        Task<IEnumerable<History>> GetAll();
        Task<IEnumerable<History>> GetBySessionId(long sessionId);
    }
}