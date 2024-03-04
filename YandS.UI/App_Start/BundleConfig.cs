using System.Web;
using System.Web.Optimization;

namespace YandS.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.FileSetOrderList.Clear();

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            "~/Scripts/jquery.validate.min.js",
            "~/Scripts/jquery.validate.unobtrusive.min.js",
            "~/Scripts/jquery.validate.CustomValidator.js",
            "~/Scripts/jquery.validate.GBDateFormat.js"
            ));


            bundles.Add(new StyleBundle("~/AllPageTheme").Include(
                "~/Content/plugins/fontawesome-free/css/all.min.css",
                "~/Content/plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css",
                "~/Content/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
                "~/Content/plugins/overlayScrollbars/css/OverlayScrollbars.min.css",
                "~/Content/plugins/daterangepicker/daterangepicker.css",
                "~/Content/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
                "~/Content/plugins/select2/css/select2.min.css",
                "~/Content/plugins/select2-bootstrap4-theme/select2-bootstrap4.min.css",
                "~/Content/plugins/summernote/summernote-bs4.css",
                "~/Content/plugins/datatables-bs4/css/dataTables.bootstrap4.css",
                "~/Content/plugins/datatables-responsive/css/responsive.bootstrap4.min.css",
                "~/Content/plugins/datatables-buttons/css/buttons.bootstrap4.min.css",
                "~/Content/plugins/datatables-select/css/select.bootstrap4.min.css",
                "~/Content/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css",
                "~/Content/plugins/toastr/toastr.min.css",
                "~/Content/plugins/bootstrapDatetimePicker/css/bootstrap-datepicker.min.css",
                "~/Content/Bootstrap-Validator/dist/css/bootstrapValidator.css",
                "~/Content/dist/css/adminlte.min.css",
                "~/scripts/plugin/fancybox/jquery.fancybox-1.3.4.min.css",
                "~/Content/MySite_Ver_16.css",
                "~/Content/MySite_Ver_17.css"));

            bundles.Add(new StyleBundle("~/ChartTheme").Include(
                "~/Content/plugins/jqvmap/jqvmap.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/JqueryScript").Include("~/Content/plugins/jquery/jquery.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/BootstrapScript").Include("~/Content/plugins/bootstrap/js/bootstrap.bundle.min.js"));
            bundles.Add(new ScriptBundle("~/CommonFunctions").Include("~/Content/CommonFunction.js"));

            bundles.Add(new ScriptBundle("~/AllPageScripts").Include(
                        "~/Content/dist/js/popper.min.js",
                        "~/Content/plugins/moment/moment.min.js",
                        "~/Content/plugins/moment/moment-business-days.js",
                        "~/Content/plugins/daterangepicker/daterangepicker.js",
                        "~/Content/plugins/select2/js/select2.full.min.js",
                        "~/Content/plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js",
                        "~/Content/plugins/summernote/summernote-bs4.min.js",
                        "~/Content/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js",
                        "~/Content/plugins/datatables/jquery.dataTables.js",
                        "~/Content/plugins/datatables-bs4/js/dataTables.bootstrap4.js",
                        "~/Content/plugins/datatables-responsive/js/dataTables.responsive.min.js",
                        "~/Content/plugins/jszip/jszip.min.js",
                        "~/Content/plugins/pdfmake/pdfmake.min.js",
                        "~/Content/plugins/pdfmake/vfs_fonts.js",
                        "~/Content/plugins/datatables-buttons/js/dataTables.buttons.min.js",
                        "~/Content/plugins/datatables-buttons/js/buttons.bootstrap4.min.js",
                        "~/Content/plugins/datatables-buttons/js/buttons.html5.min.js",
                        "~/Content/plugins/datatables-buttons/js/buttons.print.min.js",
                        "~/Content/plugins/datatables-select/js/dataTables.select.min.js",
                        "~/Content/plugins/datatables-select/js/select.bootstrap4.min.js",
                        "~/Content/plugins/datatables-sorting/natural.js",
                        "~/Content/plugins/datatables/datetime.js",
                        "~/Content/plugins/datatables/sum().js",
                        "~/Content/plugins/sweetalert2/sweetalert2.min.js",
                        "~/Content/plugins/toastr/toastr.min.js",
                        "~/Content/plugins/bootstrapDatetimePicker/js/bootstrap-datepicker.min.js",
                        "~/Content/plugins/bs-custom-file-input/bs-custom-file-input.min.js",
                        "~/Content/Bootstrap-Validator/dist/js/bootstrapValidator.js",
                        "~/Content/dist/js/adminlte.js",
                        "~/Scripts/fancybox/jquery.browsershim.js",
                        "~/Scripts/fancybox/jquery.easing-1.3.pack.js",
                        "~/Scripts/fancybox/jquery.mousewheel-3.0.4.pack.js",
                        "~/Scripts/fancybox/jquery.fancybox-1.3.4.js",
                        "~/Scripts/CommonForAll/jquery-blockUI.js",
                        "~/Scripts/CommonForAll/jquery-jtemplates.js",
                        "~/Content/CommonScript.js",
                        "~/Content/scrolls.js"
                        ));

            bundles.Add(new ScriptBundle("~/ChartScripts").Include(
                        "~/Content/plugins/chart.js/Chart.min.js",
                        "~/Content/plugins/sparklines/sparkline.js",
                        "~/Content/plugins/jqvmap/jquery.vmap.min.js",
                        "~/Content/plugins/jqvmap/maps/jquery.vmap.usa.js",
                        "~/Content/plugins/jquery-knob/jquery.knob.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/autoNumeric").Include("~/Scripts/autoNumeric/autoNumeric.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Content/jquery-ui/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/jqueryui/css").Include(
                "~/Content/jquery-ui/themes/ui-lightness/theme.css",
                "~/Content/jquery-ui/themes/ui-lightness/jquery-ui.min.css"
                ));

            //BundleTable.EnableOptimizations = true;

        }
    }
}
