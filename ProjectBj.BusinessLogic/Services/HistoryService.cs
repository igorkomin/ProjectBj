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

        public async Task<List<GameHistoryView>> GetSessionHistory(int sessionId)
        {
            List<History> history = await _historyProvider.Get(sessionId);
            List<GameHistoryView> historyViewModels = HistoryMapHelper.GetGameHistoryView(history);
            return historyViewModels;
        }

        public async Task<List<FullHistoryView>> GetFullHistory()
        {
            List<History> history = await _historyProvider.GetAll();
            List<FullHistoryView> historyViewModels = HistoryMapHelper.GetFullHistoryView(history);
            return historyViewModels;
        }
    }
}
