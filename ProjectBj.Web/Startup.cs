﻿using Microsoft.Owin;
using Owin;
using ProjectBj.Web.Configs;
using System.Web.Http;
using System.Web.Routing;

[assembly: OwinStartup(typeof(ProjectBj.Web.Startup))]

namespace ProjectBj.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = AutofacConfig.ConfigureContainer();
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }
}