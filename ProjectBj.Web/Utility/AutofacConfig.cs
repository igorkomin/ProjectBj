using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using ProjectBj.BusinessLogic;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.BusinessLogic.Providers;
using System.Web.Compilation;

namespace ProjectBj.Web.Utility
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            
            builder.RegisterType<DeckProvider>().As<IDeckProvider>();
            builder.RegisterType<GameProvider>().As<IGameProvider>();
            builder.RegisterType<LogProvider>().As<ILogProvider>();
            builder.RegisterType<PlayerProvider>().As<IPlayerProvider>();
            builder.RegisterType<SessionProvider>().As<ISessionProvider>();
            builder.RegisterType<GameProvider>().As<IGameService>();
            
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
