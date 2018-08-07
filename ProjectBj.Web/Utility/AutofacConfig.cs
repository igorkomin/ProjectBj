using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using ProjectBj.Service;
using ProjectBj.Service.Interfaces;

namespace ProjectBj.Web.Utility
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<DeckService>().As<IDeckService>();
            builder.RegisterType<GameService>().As<IGameService>();
            builder.RegisterType<LogService>().As<ILogService>();
            builder.RegisterType<PlayerService>().As<IPlayerService>();
            builder.RegisterType<SessionService>().As<ISessionService>();
            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
