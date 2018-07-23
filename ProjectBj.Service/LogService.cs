using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DAL.Repositories;

namespace ProjectBj.Service
{
    public static class LogService
    {
        private static LogRepository _logRepository;

        static LogService()
        {
            _logRepository = new LogRepository();
        }

        public static void PushLogEntry(LogEntry entry)
        {
            _logRepository.CreateEntry(entry);
        }

        public static LogEntry PullLogEntry(int id)
        {
            LogEntry entry = _logRepository.GetEntry(id);
            return entry;
        }

        public static List<LogEntry> PullLogs()
        {
            List<LogEntry> entries = _logRepository.GetAllLogs().ToList();
            return entries;
        }

        public static void DeleteLogEntry(LogEntry entry)
        {
            _logRepository.DeleteEntry(entry.Id);
        }
    }
}
