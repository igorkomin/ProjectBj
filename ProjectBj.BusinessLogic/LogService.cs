using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DataAccess.Repositories;
using ProjectBj.BusinessLogic.Interfaces;

namespace ProjectBj.BusinessLogic
{
    public class LogService : ILogService
    {
        private LogRepository _logRepository;

        public LogService()
        {
            _logRepository = new LogRepository();
        }

        public async Task CreateLogEntry(string message, int sessionId)
        {
            LogEntry entry = new LogEntry
            {
                SessionId = sessionId,
                Message = message,
                Time = DateTime.Now };
            try
            {
                await _logRepository.CreateEntry(entry);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<LogEntry> GetLogEntry(int id)
        {
            LogEntry entry;
            try
            {
                entry = await _logRepository.GetEntry(id);
                return entry;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<List<LogEntry>> GetLogs()
        {
            try
            {
                var entries = await _logRepository.GetAllLogs();
                return entries.ToList();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task DeleteLogEntry(LogEntry entry)
        {
            try
            {
                await _logRepository.DeleteEntry(entry);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
