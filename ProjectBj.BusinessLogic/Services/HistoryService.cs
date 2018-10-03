using ProjectBj.BusinessLogic.Mappers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.Entities;
using ProjectBj.ViewModels.History;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryProvider _historyProvider;

        public HistoryService(IHistoryProvider historyProvider)
        {
            _historyProvider = historyProvider;
        }

        public async Task<IEnumerable<GetGameHistoryHistoryView>> GetGameHistory(long sessionId)
        {
            IEnumerable<History> history = await _historyProvider.Get(sessionId);
            IEnumerable<GetGameHistoryHistoryView> historyViewModels = HistoryViewMapper.GetGameHistoryView(history);
            return historyViewModels;
        }

        public async Task<IEnumerable<GetFullHistoryHistoryView>> GetFullHistory()
        {
            IEnumerable<History> history = await _historyProvider.GetAll();
            IEnumerable<GetFullHistoryHistoryView> historyViewModels = HistoryViewMapper.GetFullHistoryView(history);
            return historyViewModels;
        }
    }
}
