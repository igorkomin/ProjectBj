using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.Service.Interfaces
{
    interface ILogService
    {
        Task CreateLogEntry(string message, int sessionId);
        LogEntry GetLogEntry(int id);
        List<LogEntry> GetLogs();
        void DeleteLogEntry(LogEntry entry);
    }
}
