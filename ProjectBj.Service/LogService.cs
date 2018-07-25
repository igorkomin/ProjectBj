using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DAL.Repositories;
using ProjectBj.Service.Interfaces;

namespace ProjectBj.Service
{
    public class LogService : ILogService
    {
        private LogRepository _logRepository;

        public LogService()
        {
            _logRepository = new LogRepository();
        }

        public void CreateLogEntry(string message, int sessionId)
        {
            LogEntry entry = new LogEntry { SessionId = sessionId, Message = message, Time = DateTime.Now };
            try
            {
                _logRepository.CreateEntry(entry);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public LogEntry GetLogEntry(int id)
        {
            LogEntry entry;
            try
            {
                entry = _logRepository.GetEntry(id);
                return entry;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<LogEntry> GetLogs()
        {
            List<LogEntry> entries;
            try
            {
                entries = _logRepository.GetAllLogs().ToList();
                return entries;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void DeleteLogEntry(LogEntry entry)
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
