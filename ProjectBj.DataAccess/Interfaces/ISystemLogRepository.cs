﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.DataAccess.Interfaces
{
    public interface ISystemLogRepository
    {
        Task<IEnumerable<SystemLog>> GetAllLogs();
    }
}
