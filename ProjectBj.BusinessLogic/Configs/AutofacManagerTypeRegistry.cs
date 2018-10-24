using Autofac;
using ProjectBj.BusinessLogic.Managers;
using ProjectBj.BusinessLogic.Managers.Interfaces;

namespace ProjectBj.BusinessLogic.Configs
{
    public static class AutofacManagerTypeRegistry
    {
        public static ContainerBuilder RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<CardManager>().As<ICardManager>();
            builder.RegisterType<HistoryManager>().As<IHistoryManager>();
            builder.RegisterType<PlayerManager>().As<IPlayerManager>();
            builder.RegisterType<GameSessionManager>().As<IGameSessionManager>();
            builder.RegisterType<GameManager>().As<IGameManager>();
            builder.RegisterType<GameResultManager>().As<IGameResultManager>();
            builder.RegisterType<GameViewManager>().As<IGameViewManager>();
            return builder;
        }
    }
}
