using System.Web.Optimization;

namespace ProjectBj.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/style.css",
                        "~/Content/jquery-ui.css",
                        "~/Content/slider.css"
                        ));
        }
    }
}