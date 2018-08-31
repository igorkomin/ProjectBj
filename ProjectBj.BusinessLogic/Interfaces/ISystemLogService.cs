using ProjectBj.ViewModels.Logs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface ISystemLogService
    {
        Task<List<SystemLogViewModel>> GetSystemLogs();
    }
}
