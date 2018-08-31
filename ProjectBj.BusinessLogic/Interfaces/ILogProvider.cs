using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface ILogProvider
    {
        Task CreateLogEntry(string player, string message, int sessionId);
        Task<List<LogEntry>> GetLogs(int sessionId);
    }
}
