﻿using Autofac;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.BusinessLogic.Services;

namespace ProjectBj.BusinessLogic.Configs
{
    public static class AutofacServiceTypeRegistry
    {
        public static ContainerBuilder RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<GameService>().As<IGameService>();
            builder.RegisterType<SystemLogService>().As<ISystemLogService>();

            AutofacProviderTypeRegistry.RegisterTypes(builder);

            return builder;
        }
    }
}
