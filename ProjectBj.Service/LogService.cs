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
            try
            {
                _logRepository.CreateEntry(entry);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static LogEntry GetLogEntry(int id)
        {
            LogEntry entry;
            try
            {
                entry = _logRepository.GetEntry(id);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return entry;
        }

        public static List<LogEntry> GetLogs()
        {
            List<LogEntry> entries;
            try
            {
                entries = _logRepository.GetAllLogs().ToList();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return entries;
        }

        public static void DeleteLogEntry(LogEntry entry)
        {
            try
            {
                _logRepository.DeleteEntry(entry.Id);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
