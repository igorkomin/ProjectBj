using ProjectBj.ViewModels.History;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IHistoryService
    {
        Task<IEnumerable<GameHistoryView>> GetSessionHistory(long sessionId);
        Task<IEnumerable<FullHistoryView>> GetFullHistory();
    }
}
