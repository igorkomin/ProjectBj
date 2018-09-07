using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using ProjectBj.BusinessLogic.Configs;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace ProjectBj.MVC.Configs
{
    public static class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            AutofacServiceTypeRegistry.RegisterTypes(builder);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            return container;
        }
    }
}
