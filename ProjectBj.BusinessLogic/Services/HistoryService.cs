using ProjectBj.BusinessLogic.Mappers;
using ProjectBj.BusinessLogic.Managers.Interfaces;
using ProjectBj.BusinessLogic.Services.Interfaces;
using ProjectBj.Entities;
using ProjectBj.ViewModels.History;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryManager _historyProvider;

        public HistoryService(IHistoryManager historyProvider)
        {
            _historyProvider = historyProvider;
        }
        
        public async Task<GetFullHistoryHistoryView> GetFullHistory()
        {
            IEnumerable<History> history = await _historyProvider.GetAll();
            GetFullHistoryHistoryView historyViewModels = HistoryViewMapper.GetFullHistoryView(history);
            return historyViewModels;
        }
    }
}
