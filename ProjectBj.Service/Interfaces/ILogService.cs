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
        Task<LogEntry> GetLogEntry(int id);
        Task<List<LogEntry>> GetLogs();
        void DeleteLogEntry(LogEntry entry);
    }
}
