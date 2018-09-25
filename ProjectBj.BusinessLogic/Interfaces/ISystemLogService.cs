﻿using ProjectBj.ViewModels.Log;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface ISystemLogService
    {
        Task<List<FullLogView>> GetSystemLogs();
    }
}
