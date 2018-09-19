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
            var entry = new History
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
            ICollection<History> sessionHistory = await _historyRepository.GetBySessionId(sessionId);
            return sessionHistory.ToList();
        }

        public async Task<List<History>> GetAll()
        {
            ICollection<History> fullHistory = await _historyRepository.GetAll();
            return fullHistory.ToList();
        }
    }
}
