﻿using ProjectBj.BusinessLogic.Interfaces;
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

        public async Task<List<GetFullLogView>> GetFull()
        {
            IEnumerable<SystemLog> systemLogs = await _systemLogRepository.GetAll();
            var systemLogViews = new List<GetFullLogView>();
            foreach (var log in systemLogs)
            {
                var LogView = new GetFullLogView
                {
                    Id = log.Id,
                    CallSite = log.CallSite,
                    Exception = log.Exception,
                    Https = log.Https,
                    Level = log.Level,
                    CreationDate = log.CreationDate,
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
                systemLogViews.Add(LogView);
            }
            return systemLogViews;
        }
    }
}
