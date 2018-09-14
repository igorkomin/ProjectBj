using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IHistoryProvider
    {
        Task CreateHistoryEntry(string player, string message, int sessionId);
        Task<List<History>> GetHistory(int sessionId);
        Task<List<History>> GetFullHistory();
    }
}
