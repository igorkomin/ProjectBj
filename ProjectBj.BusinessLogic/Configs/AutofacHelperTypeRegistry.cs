using Autofac;
using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Helpers.Interfaces;
using ProjectBj.DataAccess.Configs;

namespace ProjectBj.BusinessLogic.Configs
{
    public static class AutofacHelperTypeRegistry
    {
        public static ContainerBuilder RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<GameHelper>().As<IGameHelper>();
            builder.RegisterType<GameResultHelper>().As<IGameResultHelper>();
            builder.RegisterType<GameViewHelper>().As<IGameViewHelper>();
            return builder;
        }
    }
}
