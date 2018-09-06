using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.DataAccess.Interfaces;
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

        public async Task<List<SystemLogViewModel>> GetSystemLogs()
        {
            var systemLogs = await _systemLogRepository.GetAllLogs();
            var systemLogViewModels = new List<SystemLogViewModel>();
            foreach (var log in systemLogs)
            {
                SystemLogViewModel systemLogViewModel = new SystemLogViewModel
                {
                    Id = log.Id,
                    CallSite = log.CallSite,
                    Exception = log.Exception,
                    Https = log.Https,
                    Level = log.Level,
                    Logged = log.Logged,
                    Logger = log.Logger,
                    MachineName = log.MachineName,
                    Message = log.Message,
                    Port = log.Port,
                    Properties = log.Properties,
                    RemoteAddress = log.RemoteAddress,
                    ServerAddress = log.ServerAddress,
                    ServerName = log.ServerName,
                    SiteName = log.SiteName,
                    Url = log.Url,
                    UserName = log.UserName
                };
                systemLogViewModels.Add(systemLogViewModel);
            }
            return systemLogViewModels;
        }
    }
}
