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

        public async Task CreateLogEntry(string message, int sessionId)
        {
            LogEntry entry = new LogEntry { SessionId = sessionId, Message = message, Time = DateTime.Now };
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
