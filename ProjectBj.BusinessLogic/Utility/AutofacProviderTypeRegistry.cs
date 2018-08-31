using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.DataAccess.Utility;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.BusinessLogic.Providers;

namespace ProjectBj.BusinessLogic.Utility
{
    public static class AutofacProviderTypeRegistry
    {
        public static ContainerBuilder RegisterTypes(ContainerBuilder builder, string connectionString)
        {
            builder.RegisterType<DeckProvider>().As<IDeckProvider>();
            builder.RegisterType<LogProvider>().As<ILogProvider>();
            builder.RegisterType<PlayerProvider>().As<IPlayerProvider>();
            builder.RegisterType<SessionProvider>().As<ISessionProvider>();

            builder = AutofacDataAccessTypeRegistry.RegisterTypes(builder, connectionString);
            return builder;
        }
    }
}
