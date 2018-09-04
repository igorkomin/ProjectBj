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
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            var connectionString = WebConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            AutofacServiceTypeRegistry.RegisterTypes(builder, connectionString);
            
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
