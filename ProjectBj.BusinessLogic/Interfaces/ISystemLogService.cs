using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.ViewModels.Logs;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface ISystemLogService
    {
        Task<List<SystemLogViewModel>> GetSystemLogs();
    }
}
