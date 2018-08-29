using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.DataAccess.Repositories;
using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.Logger;

namespace ProjectBj.BusinessLogic.Providers
{
    public class LogProvider : ILogProvider
    {
        private ILogRepository _logRepository;

        public LogProvider(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task CreateLogEntry(string message, int sessionId)
        {
            LogEntry entry = new LogEntry
            {
                SessionId = sessionId,
                Message = message,
                Time = DateTime.Now
            };
            try
            {
                Log.Info(StringHelper.CreatingLogEntry);
                await _logRepository.CreateEntry(entry);
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                throw exception;
            }
        }

        public async Task<LogEntry> GetLogEntry(int id)
        {
            LogEntry entry;
            try
            {
                Log.Info(StringHelper.GettingLogEntry);
                entry = await _logRepository.GetEntryById(id);
                return entry;
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                throw exception;
            }
        }

        public async Task<List<LogEntry>> GetLogs()
        {
            try
            {
                Log.Info(StringHelper.GettingAllLogs);
                var entries = await _logRepository.GetAllLogs();
                return entries.ToList();
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                throw exception;
            }
        }

        public async Task<List<LogEntry>> GetLogs(int sessionId)
        {
            try
            {
                Log.Info(StringHelper.GettingSessionLog(sessionId));
                var entries = await _logRepository.GetSessionLogs(sessionId);
                return entries.ToList();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
