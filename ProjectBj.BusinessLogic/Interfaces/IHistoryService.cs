using ProjectBj.ViewModels.History;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IHistoryService
    {
        Task<List<GameHistoryView>> GetSessionHistory(int sessionId);
        Task<List<FullHistoryView>> GetFullHistory();
    }
}
