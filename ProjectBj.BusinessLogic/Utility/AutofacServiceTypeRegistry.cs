﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.DataAccess.Utility;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.BusinessLogic.Providers;
using ProjectBj.BusinessLogic.Services;

namespace ProjectBj.BusinessLogic.Utility
{
    public class AutofacServiceTypeRegistry
    {
        public static ContainerBuilder RegisterTypes(ContainerBuilder builder, string connectionString)
        {
            builder.RegisterType<GameService>().As<IGameService>();

            AutofacProviderTypeRegistry.RegisterTypes(builder, connectionString);

            return builder;
        }
    }
}
