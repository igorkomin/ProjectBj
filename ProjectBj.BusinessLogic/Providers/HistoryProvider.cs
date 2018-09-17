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
    public class HistoryProvider : IHistoryProvider
    {
        private readonly IHistoryRepository _historyRepository;

        public HistoryProvider(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public async Task Create(string playerName, string message, int sessionId)
        {
            History entry = new History
            {
                PlayerName = playerName,
                SessionId = sessionId,
                Event = message,
                Time = DateTime.Now
            };
            Log.Info(StringHelper.CreatingHistoryEntryMessage);
            await _historyRepository.Create(entry);
        }

        public async Task<List<History>> Get(int sessionId)
        {
            var logs = await _historyRepository.GetBySessionId(sessionId);
            return logs.ToList();
        }

        public async Task<List<History>> GetFullHistory()
        {
            var logs = await _historyRepository.GetAll();
            return logs.ToList();
        }
    }
}
