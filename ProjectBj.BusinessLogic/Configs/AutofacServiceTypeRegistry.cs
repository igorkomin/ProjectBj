using Autofac;
using ProjectBj.BusinessLogic.Services;
using ProjectBj.BusinessLogic.Services.Interfaces;

namespace ProjectBj.BusinessLogic.Configs
{
    public static class AutofacServiceTypeRegistry
    {
        public static ContainerBuilder RegisterTypes(ContainerBuilder builder, string connectionString)
        {
            builder.RegisterType<GameService>().As<IGameService>();
            builder.RegisterType<HistoryService>().As<IHistoryService>();
            builder.RegisterType<SystemLogService>().As<ISystemLogService>();

            AutofacManagerTypeRegistry.RegisterTypes(builder);
            AutofacProviderTypeRegistry.RegisterTypes(builder, connectionString);

            return builder;
        }
    }
}
