using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface ILogProvider
    {
        Task CreateLogEntry(string player, string message, int sessionId);
        Task<List<LogEntry>> GetSessionLogs(int sessionId);
        Task<List<LogEntry>> GetAllLogs();
    }
}
