﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ProjectBj.Service;
using ProjectBj.Service.Interfaces;

namespace ProjectBj.Web.Utility
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DeckService>().As<IDeckService>();
            builder.RegisterType<GameService>().As<IGameService>();
            builder.RegisterType<LogService>().As<ILogService>();
            builder.RegisterType<PlayerService>().As<IPlayerService>();
            builder.RegisterType<SessionService>().As<ISessionService>();
            var container = builder.Build();
        }
    }
}