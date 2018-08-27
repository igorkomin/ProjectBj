using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.BusinessLogic.Providers;
using ProjectBj.BusinessLogic.Services;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.DataAccess.Repositories;

namespace ProjectBj.BusinessLogic.Utility
{
    public class AutofacTypeRegistry
    {
        public static ContainerBuilder RegisterTypes(ContainerBuilder builder, string connectionString)
        {
            builder.RegisterType<CardRepository>().As<ICardRepository>()
                .WithParameter("connectionString", connectionString);
            builder.RegisterType<GameSessionRepository>().As<IGameSessionRepository>()
                .WithParameter("connectionString", connectionString);
            builder.RegisterType<LogRepository>().As<ILogRepository>()
                .WithParameter("connectionString", connectionString);
            builder.RegisterType<PlayerRepository>().As<IPlayerRepository>()
                .WithParameter("connectionString", connectionString);

            builder.RegisterType<DeckProvider>().As<IDeckProvider>();
            builder.RegisterType<GameProvider>().As<IGameProvider>();
            builder.RegisterType<LogProvider>().As<ILogProvider>();
            builder.RegisterType<PlayerProvider>().As<IPlayerProvider>();
            builder.RegisterType<SessionProvider>().As<ISessionProvider>();
            builder.RegisterType<GameService>().As<IGameService>();

            return builder;
        }
    }
}
