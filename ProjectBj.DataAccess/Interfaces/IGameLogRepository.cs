using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.DataAccess.Interfaces
{
    public interface IGameLogRepository
    {
        Task CreateEntry(LogEntry entry);
        Task<ICollection<LogEntry>> GetAllLogs();
        Task<ICollection<LogEntry>> GetSessionLogs(int sessionId);
    }
}