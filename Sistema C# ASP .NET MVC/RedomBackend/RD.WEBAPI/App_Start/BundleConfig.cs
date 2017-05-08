using System.Web;
using System.Web.Optimization;

namespace RD.WEBAPI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/unobstrusive").Include(
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/jquery.mask.min.js",
                "~/Scripts/demo.js",
                "~/Scripts/bootstrap-datetimepicker.min.js"
                ));

        

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/moment.min.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/fastclick.min.js",
                      "~/Scripts/app.js",
                      "~/Scripts/jquery.slimscroll.min.js",
                      "~/Scripts/jquery.dataTables.min.js",
                      "~/Scripts/dataTables.bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include( 
                      "~/Content/bootstrap.css",
                      "~/Content/AdminLTE.css",
                      "~/Content/_all-skins.min.css",
                      "~/Content/jquery-jvectormap-1.2.2.css",
                      "~/Content/site.css",
                      "~/Content/dataTables.bootstrap.css",
                      //"~/Content/jquery.dataTables.min.css",
                      "~/Content/bootstrap-datetimepicker.min.css"));
        }
    }
}
