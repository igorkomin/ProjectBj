using ProjectBj.ViewModels.Logs;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Services.Interfaces
{
    public interface ISystemLogService
    {
        Task<GetFullLogView> GetFullLog();
    }
}
