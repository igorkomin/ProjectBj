using Autofac;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.BusinessLogic.Services;

namespace ProjectBj.BusinessLogic.Configs
{
    public static class AutofacServiceTypeRegistry
    {
        public static ContainerBuilder RegisterTypes(ContainerBuilder builder, string connectionString)
        {
            builder.RegisterType<GameService>().As<IGameService>();
            builder.RegisterType<SystemLogService>().As<ISystemLogService>();

            AutofacProviderTypeRegistry.RegisterTypes(builder, connectionString);

            return builder;
        }
    }
}
