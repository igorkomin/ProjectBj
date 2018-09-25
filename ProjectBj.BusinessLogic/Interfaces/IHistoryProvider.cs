using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IHistoryProvider
    {
        Task Create(string player, string message, long sessionId);
        Task<IEnumerable<History>> Get(long sessionId);
        Task<IEnumerable<History>> GetAll();
    }
}
