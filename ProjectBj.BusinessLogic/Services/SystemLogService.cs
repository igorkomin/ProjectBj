using AutoMapper;
using ProjectBj.BusinessLogic.Interfaces;
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

        public async Task<List<GetFullLogView>> GetFullLog()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<SystemLog, GetFullLogView>());
            IEnumerable<SystemLog> systemLogs = await _systemLogRepository.GetAll();
            var systemLogViews = new List<GetFullLogView>();
            foreach (var log in systemLogs)
            {
                GetFullLogView logView = Mapper.Map<GetFullLogView>(log);
                systemLogViews.Add(logView);
            }
            return systemLogViews;
        }
    }
}
