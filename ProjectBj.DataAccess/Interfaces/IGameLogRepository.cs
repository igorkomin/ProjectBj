using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.DataAccess.Interfaces
{
    public interface IGameLogRepository
    {
        Task CreateEntry(LogEntry entry);
        Task<ICollection<LogEntry>> GetAllLogs();
        Task<ICollection<LogEntry>> GetLogsBySessionId(int sessionId);
    }
}