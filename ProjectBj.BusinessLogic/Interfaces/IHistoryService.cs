using ProjectBj.ViewModels.History;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IHistoryService
    {
        Task<GetGameHistoryHistoryView> GetGameHistory(long sessionId);
        Task<GetFullHistoryHistoryView> GetFullHistory();
    }
}
