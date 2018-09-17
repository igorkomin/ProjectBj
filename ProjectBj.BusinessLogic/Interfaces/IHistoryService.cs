using ProjectBj.ViewModels.History;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IHistoryService
    {
        Task<List<HistoryViewModel>> GetSessionHistory(int sessionId);
        Task<List<HistoryViewModel>> GetFullHistory();
    }
}
