using System.Web;
using System.Web.Optimization;

namespace OCC.UI.Webhost
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.browser.js",
                        "~/Scripts/jquery.validate *"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapScript").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrapCSS").Include(
                      "~/Content/bootstrap.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/Layout.css",
                      "~/Content/css/styles.css"));
        }
    }
}