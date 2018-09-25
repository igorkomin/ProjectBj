using Autofac;
using Autofac.Integration.WebApi;
using ProjectBj.BusinessLogic.Configs;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Http;

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
