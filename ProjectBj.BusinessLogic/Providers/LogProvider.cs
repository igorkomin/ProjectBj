using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Providers
{
    public class LogProvider : ILogProvider
    {
        private readonly IGameLogRepository _logRepository;

        public LogProvider(IGameLogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task CreateLogEntry(string playerName, string message, int sessionId)
        {
            LogEntry entry = new LogEntry
            {
                PlayerName = playerName,
                SessionId = sessionId,
                Message = message,
                Time = DateTime.Now
            };
            Log.Info(StringHelper.CreatingLogEntryMessage);
            await _logRepository.CreateEntry(entry);
        }

        public async Task<List<LogEntry>> GetSessionLogs(int sessionId)
        {
            var logs = await _logRepository.GetLogsBySessionId(sessionId);
            return logs.ToList();
        }

        public async Task<List<LogEntry>> GetAllLogs()
        {
            var logs = await _logRepository.GetAllLogs();
            return logs.ToList();
        }
    }
}
