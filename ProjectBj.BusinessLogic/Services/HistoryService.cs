using ProjectBj.BusinessLogic.Helpers.ViewMapHelpers;
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

        public async Task<IEnumerable<GameHistoryView>> GetSessionHistory(long sessionId)
        {
            IEnumerable<History> history = await _historyProvider.Get(sessionId);
            IEnumerable<GameHistoryView> historyViewModels = HistoryMapHelper.GetGameHistoryView(history);
            return historyViewModels;
        }

        public async Task<IEnumerable<FullHistoryView>> GetFullHistory()
        {
            IEnumerable<History> history = await _historyProvider.GetAll();
            IEnumerable<FullHistoryView> historyViewModels = HistoryMapHelper.GetFullHistoryView(history);
            return historyViewModels;
        }
    }
}
