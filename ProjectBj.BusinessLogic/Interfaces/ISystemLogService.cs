using ProjectBj.ViewModels.Logs;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface ISystemLogService
    {
        Task<GetFullLogView> GetFullLog();
    }
}
