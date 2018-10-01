using Autofac;
using ProjectBj.DataAccess.Repositories;
using ProjectBj.DataAccess.Repositories.Interfaces;

namespace ProjectBj.DataAccess.Configs
{
    public static class AutofacDataAccessTypeRegistry
    {
        public static ContainerBuilder RegisterTypes(ContainerBuilder builder, string connectionString)
        {
            builder.RegisterGeneric(typeof(RepositoryBase<>))
                .As(typeof(IRepositoryBase<>));
            builder.RegisterType<CardRepository>().As<ICardRepository>()
                .WithParameter("connectionString", connectionString);
            builder.RegisterType<GameSessionRepository>().As<IGameSessionRepository>()
                .WithParameter("connectionString", connectionString);
            builder.RegisterType<HistoryRepository>().As<IHistoryRepository>()
                .WithParameter("connectionString", connectionString);
            builder.RegisterType<PlayerRepository>().As<IPlayerRepository>()
                .WithParameter("connectionString", connectionString);
            builder.RegisterType<SystemLogRepository>().As<ISystemLogRepository>()
                .WithParameter("connectionString", connectionString);

            return builder;
        }
    }
}
