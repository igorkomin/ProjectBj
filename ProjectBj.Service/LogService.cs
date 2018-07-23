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

        public static void CreateLogEntry(string message)
        {
            LogEntry entry = new LogEntry { Message = message, Time = DateTime.Now };
            _logRepository.CreateEntry(entry);
        }

        public static LogEntry GetLogEntry(int id)
        {
            LogEntry entry = _logRepository.GetEntry(id);
            return entry;
        }

        public static List<LogEntry> GetLogs()
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
