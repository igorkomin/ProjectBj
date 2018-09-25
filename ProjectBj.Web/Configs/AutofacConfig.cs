using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using ProjectBj.BusinessLogic.Configs;
using System.Web.Configuration;

namespace ProjectBj.Web.Configs
{
    public static class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            HttpConfiguration config = GlobalConfiguration.Configuration;
            string connectionString = WebConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            AutofacServiceTypeRegistry.RegisterTypes(builder, connectionString);

            IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            return container;
        }
    }
}
