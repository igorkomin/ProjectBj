using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.DataAccess.Interfaces
{
    interface ILogRepository
    {
        Task<LogEntry> GetEntry(int id);
        Task<ICollection<LogEntry>> GetAllLogs();
        Task CreateEntry(LogEntry entry);
        Task DeleteEntry(int id);
    }
}