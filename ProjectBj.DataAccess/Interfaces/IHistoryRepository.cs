using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Interfaces
{
    public interface IHistoryRepository
    {
        Task Create(History entry);
        Task<ICollection<History>> GetAll();
        Task<ICollection<History>> GetBySessionId(int sessionId);
    }
}