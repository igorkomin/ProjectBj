using ProjectBj.ViewModels.History;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IHistoryService
    {
        Task<IEnumerable<GetGameHistoryHistoryView>> GetGameHistory(long sessionId);
        Task<IEnumerable<GetFullHistoryHistoryView>> GetFullHistory();
    }
}
