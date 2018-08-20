﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface ILogService
    {
        Task CreateLogEntry(string message, int sessionId);
        Task<LogEntry> GetLogEntry(int id);
        Task<List<LogEntry>> GetLogs();
        Task DeleteLogEntry(LogEntry entry);
    }
}