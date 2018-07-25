using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.DAL.Interfaces
{
    interface ILogRepository
    {
        LogEntry GetEntry(int id);
        ICollection<LogEntry> GetAllLogs();
        void CreateEntry(LogEntry entry);
        void DeleteEntry(int id);
    }
}