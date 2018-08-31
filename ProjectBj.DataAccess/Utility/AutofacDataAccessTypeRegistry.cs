using Autofac;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.DataAccess.Repositories;

namespace ProjectBj.DataAccess.Utility
{
    public static class AutofacDataAccessTypeRegistry
    {
        public static ContainerBuilder RegisterTypes(ContainerBuilder builder, string connectionString)
        {
            builder.RegisterType<CardRepository>().As<ICardRepository>()
                .WithParameter("connectionString", connectionString);
            builder.RegisterType<GameSessionRepository>().As<IGameSessionRepository>()
                .WithParameter("connectionString", connectionString);
            builder.RegisterType<GameLogRepository>().As<IGameLogRepository>()
                .WithParameter("connectionString", connectionString);
            builder.RegisterType<PlayerRepository>().As<IPlayerRepository>()
                .WithParameter("connectionString", connectionString);
            builder.RegisterType<SystemLogRepository>().As<ISystemLogRepository>()
                .WithParameter("connectionString", connectionString);

            return builder;
        }
    }
}
