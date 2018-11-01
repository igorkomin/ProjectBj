using ProjectBj.BusinessLogic.Managers.Interfaces;
using ProjectBj.DataAccess.Repositories.Interfaces;
using ProjectBj.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Managers
{
    public class HistoryManager : IHistoryManager
    {
        private readonly IHistoryRepository _historyRepository;

        public HistoryManager(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public async Task Create(long playerId, string message, long sessionId)
        {
            var entry = new History
            {
                PlayerId = playerId,
                SessionId = sessionId,
                Event = message,
            };
            await _historyRepository.Insert(entry);
        }

        public async Task Create(List<History> historyEntries)
        {
            await _historyRepository.Insert(historyEntries);
        }

        public async Task<IEnumerable<History>> GetAll()
        {
            IEnumerable<History> fullHistory = await _historyRepository.GetAll();
            return fullHistory;
        }
    }
}
