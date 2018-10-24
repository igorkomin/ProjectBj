using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Managers.Interfaces
{
    public interface IHistoryManager
    {
        Task Create(string player, string message, long sessionId);
        Task<IEnumerable<History>> Get(long sessionId);
        Task<IEnumerable<History>> GetAll();
    }
}
