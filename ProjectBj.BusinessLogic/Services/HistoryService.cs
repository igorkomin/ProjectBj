using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.ViewModels.History;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Services
{
    public class HistoryService
    {
        private readonly IHistoryProvider _historyProvider;

        public HistoryService(IHistoryProvider historyProvider)
        {
            _historyProvider = historyProvider;
        }

        public async Task<List<HistoryViewModel>> GetSessionHistory(int sessionId)
        {
            var history = await _historyProvider.GetHistory(sessionId);
            var historyViewModels = ModelViewModelConverter.GetHistory(history);
            return historyViewModels;
        }

        public async Task<List<HistoryViewModel>> GetFullHistory()
        {
            var history = await _historyProvider.GetFullHistory();
            var historyViewModels = ModelViewModelConverter.GetHistory(history);
            return historyViewModels;
        }
    }
}
