using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;
using ProjectBj.BusinessLogic;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.BusinessLogic.Providers;
using ProjectBj.BusinessLogic.Services;
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
