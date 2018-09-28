using System.Web.Optimization;

namespace ProjectBj.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/dist/runtime.js",
                        "~/dist/polyfills.js",
                        "~/dist/styles.js",
                        "~/dist/vendor.js",
                        "~/dist/main.js"
                        ));
        }
    }
}