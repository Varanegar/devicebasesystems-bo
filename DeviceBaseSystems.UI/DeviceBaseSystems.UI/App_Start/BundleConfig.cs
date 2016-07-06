using System.Web;
using System.Web.Optimization;

namespace DeviceBaseSystems.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery-{version}.js",
                        "~/Scripts/jquery/jquery.cookie.js",
                        "~/Scripts/jquery/jquery.blockUI.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/bootstrap.js",
                      "~/Scripts/bootstrap/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/util").Include(
                      "~/Scripts/util/persianDatepicker.min.js",
                      "~/Scripts/util/toastr.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                      "~/Scripts/kendo/kendo.all.min.js",
                      "~/Scripts/kendo/kendo.messages.fa-IR.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                     "~/Scripts/knockout/knockout-3.4.0.js",
                     "~/Scripts/knockout/knockout.mapping-latest.js"));

            bundles.Add(new StyleBundle("~/Content/kendocss").Include(
              "~/Content/kendo/kendo.common-material.min.css",
              "~/Content/kendo/kendo.rtl.min.css",
              "~/Content/kendo/kendo.material.min.css"
            ));
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap-rtl.css",
                //"~/Content/css/style_responsive.css",
                "~/Content/site.css"));
        
        }
    }
}
