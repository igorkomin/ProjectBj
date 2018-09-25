using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Logger;
using System;
using System.Collections.Generic;
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

        public async Task Create(string playerName, string message, long sessionId)
        {
            var entry = new History
            {
                PlayerName = playerName,
                SessionId = sessionId,
                Event = message,
                CreationDate = DateTime.Now
            };
            Log.Info(StringHelper.CreatingHistoryEntryMessage);
            await _historyRepository.Create(entry);
        }

        public async Task<IEnumerable<History>> Get(long sessionId)
        {
            IEnumerable<History> sessionHistory = await _historyRepository.GetBySessionId(sessionId);
            return sessionHistory;
        }

        public async Task<IEnumerable<History>> GetAll()
        {
            IEnumerable<History> fullHistory = await _historyRepository.GetAll();
            return fullHistory;
        }
    }
}
