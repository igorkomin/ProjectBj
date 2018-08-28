using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.DataAccess.Interfaces
{
    public interface ILogRepository
    {
        Task<LogEntry> GetEntryById(int id);
        Task<ICollection<LogEntry>> GetAllLogs();
        Task<ICollection<LogEntry>> GetSessionLogs(int sessionId);
        Task CreateEntry(LogEntry entry);
        Task DeleteEntry(LogEntry entry);
    }
}