using System.Web;
using System.Web.Optimization;

namespace CrowdFunding
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-animate.js",
                        "~/Scripts/angular-route.js",
                        "~/Scripts/angular-sanitize.js",
                        "~/Scripts/ui-bootstrap-tpls-{version}.js",
                        "~/Scripts/angular-form-ui.js",
                        
                        "~/Scripts/spin.js", //### Non-Angular files!!!
                        "~/Scripts/toastr.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/ajaxlogin").Include(
                "~/app/ajaxlogin.js"));

            bundles.Add(new ScriptBundle("~/bundles/breeze").Include(
                        "~/Scripts/breeze.js",
                        "~/Scripts/breeze.angular.js",
                        //"~/Scripts/breeze.savequeuing.js",  //@@@ not in use, but see how to use it (in original todo app). It is a good idea!
                        "~/Scripts/breeze.directives.validation.js",
                        "~/Scripts/breeze.saveErrorExtensions.js"));

            bundles.Add(new ScriptBundle("~/bundles/angelikoo").Include(
                "~/app/_main.js", // must be first                
                "~/app/_config.js",
                "~/app/config.exceptionHandler.js",
                //<!-- common Modules -->
                "~/app/_common.js",
                "~/app/logger.js",
                "~/app/spinner.js",
                //<!-- app -->
                "~/app/entityManagerFactory.js",
                "~/app/dataContext.js",
                "~/app/HomePageCtrl.js",
                "~/app/CompaniesPageCtrl.js",
                "~/app/CompanyPageCtrl.js",
                "~/app/_shell.js",
                "~/app/model.js"
                ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                //"~/Content/themes/base/jquery-ui.css",
                //"~/Content/ie10mobile.css",
                "~/Content/bootstrap.css",
                //"~/Content/font-awesome.css",
                "~/Content/toastr.css",
                //"~/Content/customtheme.css",
                //"~/Content/bootstrap-responsive.css",
                "~/Content/styles.css"
                //"~/Content/breeze.directives.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Site.css",
                "~/Content/TodoList.css",
                "~/Content/angular-form-ui.css"
                ));

            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            "~/Content/themes/base/jquery.ui.selectable.css",
            //            "~/Content/themes/base/jquery.ui.accordion.css",
            //            "~/Content/themes/base/jquery.ui.autocomplete.css",
            //            "~/Content/themes/base/jquery.ui.button.css",
            //            "~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            "~/Content/themes/base/jquery.ui.tabs.css",
            //            "~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}