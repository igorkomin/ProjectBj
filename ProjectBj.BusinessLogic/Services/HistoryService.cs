using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;
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

        public async Task<List<HistoryViewModel>> GetSessionHistory(int sessionId)
        {
            var history = await _historyProvider.Get(sessionId);
            var historyViewModels = MapHelper.GetHistoryInfo(history);
            return historyViewModels;
        }

        public async Task<List<HistoryViewModel>> GetFullHistory()
        {
            var history = await _historyProvider.GetAll();
            var historyViewModels = MapHelper.GetHistoryInfo(history);
            return historyViewModels;
        }
    }
}
