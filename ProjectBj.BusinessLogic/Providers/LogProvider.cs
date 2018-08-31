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
        private readonly IGameLogRepository _logRepository;

        public LogProvider(IGameLogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task CreateLogEntry(string player, string message, int sessionId)
        {
            LogEntry entry = new LogEntry
            {
                Player = player,
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

        public async Task<List<LogEntry>> GetLogs(int sessionId)
        {
            try
            {
                Log.Info(StringHelper.GetGettingSessionLogMessage(sessionId));
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
