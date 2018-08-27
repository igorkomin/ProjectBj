using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using ProjectBj.BusinessLogic.Utility;
using System.Web.Configuration;

namespace ProjectBj.Web.Utility
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            var connectionString = WebConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            AutofacTypeRegistry.RegisterTypes(builder, connectionString);
            
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
