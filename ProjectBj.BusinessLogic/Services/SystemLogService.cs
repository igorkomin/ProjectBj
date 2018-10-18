using AutoMapper;
using ProjectBj.BusinessLogic.Services.Interfaces;
using ProjectBj.DataAccess.Repositories.Interfaces;
using ProjectBj.Entities;
using ProjectBj.ViewModels.Logs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Services
{
    public class SystemLogService : ISystemLogService
    {
        private readonly ISystemLogRepository _systemLogRepository;

        public SystemLogService(ISystemLogRepository systemLogRepository)
        {
            _systemLogRepository = systemLogRepository;
        }

        public async Task<GetFullLogView> GetFullLog()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<SystemLog, EntryGetFullLogViewItem>());
            GetFullLogView view = new GetFullLogView();
            IEnumerable<SystemLog> systemLogs = await _systemLogRepository.GetAll();
            var viewItems = new List<EntryGetFullLogViewItem>();
            foreach (var item in systemLogs)
            {
                EntryGetFullLogViewItem logView = Mapper.Map<EntryGetFullLogViewItem>(item);
                viewItems.Add(logView);
            }

            view.Entries = viewItems;

            return view;
        }
    }
}
