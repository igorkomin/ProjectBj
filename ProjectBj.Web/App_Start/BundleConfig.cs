using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ProjectBj.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bj/slider.js",
                        "~/Scripts/bj/slider-bots.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.3.1.js",
                        "~/Scripts/jquery-ui.js",
                        "~/Scripts/jquery.cookie.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                        "~/Scripts/bj/login.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                        "~/Scripts/bj/main.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/style.css",
                        "~/Content/jquery-ui.css",
                        "~/Content/slider.css"
                        ));
        }
    }
}