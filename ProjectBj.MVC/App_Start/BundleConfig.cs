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
                        "~/Scripts/bootstrap.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/blackjack").Include(
                        "~/Scripts/bj/slider.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                        "~/Scripts/bj/login.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/game").Include(
                        "~/Scripts/bj/game.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/jquery-ui.css",
                        "~/Content/bj-styles/style.css",
                        "~/Content/bj-styles/slider.css",
                        "~/Content/bj-styles/cards.css"
                         ));
        }
    }
}