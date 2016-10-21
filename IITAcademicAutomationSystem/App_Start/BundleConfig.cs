using System.Web;
using System.Web.Optimization;

namespace IITAcademicAutomationSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // Bootstrap js
            bundles.Add(new ScriptBundle("~/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            // Datatable js
            bundles.Add(new ScriptBundle("~/datatable").Include(
                      "~/Content/plugins/datatables/jquery.dataTables.js",
                      "~/Content/plugins/datatables/dataTables.bootstrap.js"));

            // Admin LTE js
            bundles.Add(new ScriptBundle("~/admin").Include(
                      "~/Content/plugins/slimScroll/jquery.slimscroll.min.js",
                      "~/Content/plugins/fastclick/fastclick.min.js",
                      "~/Content/dist/js/app.js"));

            // Foolproof js
            bundles.Add(new ScriptBundle("~/foolproof").Include(
                      "~/Client Scripts/MvcFoolproofValidation.min.js",
                      "~/Client Scripts/MvcFoolproofJQueryValidation.min.js",
                      "~/Client Scripts/mvcfoolproof.unobtrusive.min.js"));

            // Bootstrap css
            bundles.Add(new StyleBundle("~/bootstrapcss").Include(
                      "~/Content/bootstrap/css/bootstrap.css",
                      "~/Content/font-awesome/css/font-awesome.min.css",
                      "~/Content/ionicons/css/ionicons.min.css"));

            // Datatable css
            bundles.Add(new StyleBundle("~/datatablecss").Include(
                      "~/Content/plugins/datatables/dataTables.bootstrap.css"));

            // Admin LTE css
            bundles.Add(new StyleBundle("~/admincss").Include(
                      "~/Content/dist/css/AdminLTE.css",
                      "~/Content/dist/css/skins/skin-blue.css"));

            // App css
            bundles.Add(new StyleBundle("~/css").Include(
                      "~/Content/Site.css",
                      "~/Content/One.css"));
        }
    }
}
