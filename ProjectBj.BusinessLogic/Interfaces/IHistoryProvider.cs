using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IHistoryProvider
    {
        Task Create(string player, string message, int sessionId);
        Task<List<History>> Get(int sessionId);
        Task<List<History>> GetAll();
    }
}
