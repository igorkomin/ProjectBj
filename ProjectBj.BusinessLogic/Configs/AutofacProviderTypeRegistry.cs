using Autofac;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.BusinessLogic.Providers;
using ProjectBj.DataAccess.Configs;

namespace ProjectBj.BusinessLogic.Configs
{
    public static class AutofacProviderTypeRegistry
    {
        public static ContainerBuilder RegisterTypes(ContainerBuilder builder, string connectionString)
        {
            builder.RegisterType<CardProvider>().As<ICardProvider>();
            builder.RegisterType<LogProvider>().As<ILogProvider>();
            builder.RegisterType<PlayerProvider>().As<IPlayerProvider>();
            builder.RegisterType<SessionProvider>().As<ISessionProvider>();

            builder = AutofacDataAccessTypeRegistry.RegisterTypes(builder, connectionString);
            return builder;
        }
    }
}
