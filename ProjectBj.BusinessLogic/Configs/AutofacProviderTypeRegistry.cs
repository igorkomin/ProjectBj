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
            builder.RegisterType<HistoryProvider>().As<IHistoryProvider>();
            builder.RegisterType<PlayerProvider>().As<IPlayerProvider>();
            builder.RegisterType<GameSessionProvider>().As<IGameSessionProvider>();

            AutofacDataAccessTypeRegistry.RegisterTypes(builder, connectionString);
            return builder;
        }
    }
}
