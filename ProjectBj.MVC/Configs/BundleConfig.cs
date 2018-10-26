using System.Web.Optimization;

namespace ProjectBj.MVC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.3.1.js",
                        "~/Scripts/jquery.cookie-1.4.1.min.js",
                        "~/Scripts/jquery-ui.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/style/bootstrap.js"
                        ));
            
            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                        "~/Scripts/login/login.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/game").Include(
                        "~/Scripts/login/game.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/jquery-ui.css",
                        "~/Content/style/style.css",
                        "~/Content/style/cards.css"
                         ));
        }
    }
}