using ProjectBj.ViewModels.History;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Services.Interfaces
{
    public interface IHistoryService
    {
        Task<GetFullHistoryHistoryView> GetFullHistory();
    }
}
