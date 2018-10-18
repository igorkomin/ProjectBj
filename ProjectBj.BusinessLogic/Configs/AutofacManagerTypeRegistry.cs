using Autofac;
using ProjectBj.BusinessLogic.Managers;
using ProjectBj.BusinessLogic.Managers.Interfaces;

namespace ProjectBj.BusinessLogic.Configs
{
    public static class AutofacManagerTypeRegistry
    {
        public static ContainerBuilder RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<GameManager>().As<IGameManager>();
            builder.RegisterType<GameResultManager>().As<IGameResultManager>();
            builder.RegisterType<GameViewManager>().As<IGameViewManager>();
            return builder;
        }
    }
}
