using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Managers.Interfaces
{
    public interface IHistoryManager
    {
        Task Create(List<History> entry);
        Task Create(long playerId, string message, long sessionId);
        Task<IEnumerable<History>> GetAll();
    }
}
