﻿using System.Web;
using System.Web.Optimization;

namespace Sistema
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
                 
            bundles.Add(new ScriptBundle("~/system_scripts").Include(
                "~/Assets/js/global.js",
                "~/Assets/plugins/general/jquery/dist/jquery-3.5.1.min.js",
                "~/Assets/plugins/general/popper.js/dist/js/popper.min.js",
                "~/Assets/plugins/general/bootstrap/dist/js/bootstrap.min.js",
                "~/Assets/plugins/general/js-cookie/src/js.cookie.js",
                //"~/Assets/plugins/general/moment/min/moment.min.js",
                "~/Assets/plugins/general/tooltip.js/dist/umd/tooltip.min.js",
                "~/Assets/plugins/general/perfect-scrollbar/dist/perfect-scrollbar.js",
                "~/Assets/plugins/general/sticky-js/dist/sticky.min.js",
                //"~/Assets/plugins/general/wnumb/wNumb.js",
                //"~/Assets/plugins/general/jquery-form/dist/jquery.form.min.js",
                "~/Assets/plugins/general/block-ui/jquery.blockUI.js",
                //"~/Assets/plugins/general/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js",
                //"~/Assets/plugins/general/js/global/integration/plugins/bootstrap-datepicker.init.js",
                //"~/Assets/plugins/general/bootstrap-datetime-picker/js/bootstrap-datetimepicker.min.js",
                //"~/Assets/plugins/general/bootstrap-timepicker/js/bootstrap-timepicker.min.js",
                //"~/Assets/plugins/general/js/global/integration/plugins/bootstrap-timepicker.init.js",
                //"~/Assets/plugins/general/bootstrap-daterangepicker/daterangepicker.js",
                //"~/Assets/plugins/general/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.js",
                //"~/Assets/plugins/general/bootstrap-maxlength/src/bootstrap-maxlength.js",
                //"~/Assets/plugins/general/plugins/bootstrap-multiselectsplitter/bootstrap-multiselectsplitter.min.js",
                //"~/Assets/plugins/general/bootstrap-select/dist/js/bootstrap-select.js",
                //"~/Assets/plugins/general/bootstrap-switch/dist/js/bootstrap-switch.js",
                //"~/Assets/plugins/general/js/global/integration/plugins/bootstrap-switch.init.js",
                //"~/Assets/plugins/general/select2/dist/js/select2.full.js",
                //"~/Assets/plugins/general/ion-rangeslider/js/ion.rangeSlider.js",
                //"~/Assets/plugins/general/typeahead.js/dist/typeahead.bundle.js",
                //"~/Assets/plugins/general/handlebars/dist/handlebars.js",
                //"~/Assets/plugins/general/inputmask/dist/jquery.inputmask.bundle.js",
                //"~/Assets/plugins/general/inputmask/dist/inputmask/inputmask.date.extensions.js",
                //"~/Assets/plugins/general/inputmask/dist/inputmask/inputmask.numeric.extensions.js",
                //"~/Assets/plugins/general/nouislider/distribute/nouislider.js",
                //"~/Assets/plugins/general/owl.carousel/dist/owl.carousel.js",
                //"~/Assets/plugins/general/autosize/dist/autosize.js",
                //"~/Assets/plugins/general/clipboard/dist/clipboard.min.js",
                //"~/Assets/plugins/general/dropzone/dist/dropzone.js",
                //"~/Assets/plugins/general/js/global/integration/plugins/dropzone.init.js",
                //"~/Assets/plugins/general/quill/dist/quill.js",
                //"~/Assets/plugins/general/yaireo/tagify/dist/tagify.polyfills.min.js",
                //"~/Assets/plugins/general/yaireo/tagify/dist/tagify.min.js",
                //"~/Assets/plugins/general/summernote/dist/summernote.js",
                //"~/Assets/plugins/general/markdown/lib/markdown.js",
                //"~/Assets/plugins/general/bootstrap-markdown/js/bootstrap-markdown.js",
                //"~/Assets/plugins/general/js/global/integration/plugins/bootstrap-markdown.init.js",
                //"~/Assets/plugins/general/bootstrap-notify/bootstrap-notify.min.js",
                //"~/Assets/plugins/general/js/global/integration/plugins/bootstrap-notify.init.js",
                //"~/Assets/plugins/general/jquery-validation/dist/jquery.validate.js",
                //"~/Assets/plugins/general/jquery-validation/dist/additional-methods.js",
                //"~/Assets/plugins/general/js/global/integration/plugins/jquery-validation.init.js",
                "~/Assets/plugins/general/toastr/build/toastr.min.js",
                //"~/Assets/plugins/general/dual-listbox/dist/dual-listbox.js",
                //"~/Assets/plugins/general/raphael/raphael.js",
                //"~/Assets/plugins/general/morris.js/morris.js",
                //"~/Assets/plugins/general/chart.js/dist/Chart.bundle.js",
                //"~/Assets/plugins/general/plugins/bootstrap-session-timeout/dist/bootstrap-session-timeout.min.js",
                //"~/Assets/plugins/general/plugins/jquery-idletimer/idle-timer.min.js",
                //"~/Assets/plugins/general/waypoints/lib/jquery.waypoints.js",
                //"~/Assets/plugins/general/counterup/jquery.counterup.js",
                //"~/Assets/plugins/general/es6-promise-polyfill/promise.min.js",
                "~/Assets/plugins/general/sweetalert2/dist/sweetalert2.min.js",
                //"~/Assets/plugins/general/js/global/integration/plugins/sweetalert2.init.js",
                //"~/Assets/plugins/general/jquery.repeater/src/lib.js",
                //"~/Assets/plugins/general/jquery.repeater/src/jquery.input.js",
                //"~/Assets/plugins/general/jquery.repeater/src/repeater.js",
                //"~/Assets/plugins/general/dompurify/dist/purify.js",
                "~/Assets/js/scripts.bundle.js",
                "~/Assets/plugins/custom/plugins/jquery-ui/jquery-ui.min.js",
                //"~/Assets/plugins/custom/fullcalendar/core/main.js",
                //"~/Assets/plugins/custom/fullcalendar/daygrid/main.js",
                //"~/Assets/plugins/custom/fullcalendar/google-calendar/main.js",
                //"~/Assets/plugins/custom/fullcalendar/interaction/main.js",
                //"~/Assets/plugins/custom/fullcalendar/list/main.js",
                //"~/Assets/plugins/custom/fullcalendar/timegrid/main.js",
                //"~/Assets/plugins/custom/flot/dist/es5/jquery.flot.js",
                //"~/Assets/plugins/custom/flot/source/jquery.flot.resize.js",
                //"~/Assets/plugins/custom/flot/source/jquery.flot.categories.js",
                //"~/Assets/plugins/custom/flot/source/jquery.flot.pie.js",
                //"~/Assets/plugins/custom/flot/source/jquery.flot.stack.js",
                //"~/Assets/plugins/custom/flot/source/jquery.flot.crosshair.js",
                //"~/Assets/plugins/custom/flot/source/jquery.flot.axislabels.js",
                //"~/Assets/plugins/custom/datatables.net/js/jquery.dataTables.js",
                //"~/Assets/plugins/custom/datatables.net-bs4/js/dataTables.bootstrap4.js",
                //"~/Assets/plugins/custom/js/global/integration/plugins/datatables.init.js",
                //"~/Assets/plugins/custom/datatables.net-autofill/js/dataTables.autoFill.min.js",
                //"~/Assets/plugins/custom/datatables.net-autofill-bs4/js/autoFill.bootstrap4.min.js",
                //"~/Assets/plugins/custom/jszip/dist/jszip.min.js",
                //"~/Assets/plugins/custom/pdfmake/build/pdfmake.min.js",
                //"~/Assets/plugins/custom/pdfmake/build/vfs_fonts.js",
                //"~/Assets/plugins/custom/datatables.net-buttons/js/dataTables.buttons.min.js",
                //"~/Assets/plugins/custom/datatables.net-buttons-bs4/js/buttons.bootstrap4.min.js",
                //"~/Assets/plugins/custom/datatables.net-buttons/js/buttons.colVis.js",
                //"~/Assets/plugins/custom/datatables.net-buttons/js/buttons.flash.js",
                //"~/Assets/plugins/custom/datatables.net-buttons/js/buttons.html5.js",
                //"~/Assets/plugins/custom/datatables.net-buttons/js/buttons.print.js",
                //"~/Assets/plugins/custom/datatables.net-colreorder/js/dataTables.colReorder.min.js",
                //"~/Assets/plugins/custom/datatables.net-fixedcolumns/js/dataTables.fixedColumns.min.js",
                //"~/Assets/plugins/custom/datatables.net-fixedheader/js/dataTables.fixedHeader.min.js",
                //"~/Assets/plugins/custom/datatables.net-keytable/js/dataTables.keyTable.min.js",
                //"~/Assets/plugins/custom/datatables.net-responsive/js/dataTables.responsive.min.js",
                //"~/Assets/plugins/custom/datatables.net-responsive-bs4/js/responsive.bootstrap4.min.js",
                //"~/Assets/plugins/custom/datatables.net-rowgroup/js/dataTables.rowGroup.min.js",
                //"~/Assets/plugins/custom/datatables.net-rowreorder/js/dataTables.rowReorder.min.js",
                //"~/Assets/plugins/custom/datatables.net-scroller/js/dataTables.scroller.min.js",
                //"~/Assets/plugins/custom/datatables.net-select/js/dataTables.select.min.js",
                //"~/Assets/plugins/custom/jstree/dist/jstree.js",
                //"~/Assets/plugins/custom/uppy/dist/uppy.min.js",
                //"~/Assets/plugins/custom/tinymce/tinymce.min.js",
                //"~/Assets/plugins/custom/tinymce/themes/silver/theme.js",
                //"~/Assets/plugins/custom/tinymce/themes/mobile/theme.js",
                "~/Assets/plugins/custom/mask/jquery.mask.min.js",
                "~/Assets/plugins/custom/highlight/highlight.js",
                "~/Assets/plugins/custom/viacep/jquery-viacep.min.js",
                "~/Assets/js/utils.js",
                "~/Assets/js/forms.js",
                "~/Assets/js/chat.js",
                "~/Assets/plugins/custom/gridstack/gridstack.js",
                "~/Assets/plugins/custom/gridstack/gridstack.jQueryUI.js",
                "~/Assets/js/widgets.js",
                "~/Assets/js/list.js"
            ));

            bundles.Add(new ScriptBundle("~/system_page").Include(
                "~/Assets/js/page.js"));

            bundles.Add(new ScriptBundle("~/system_timer").Include(
                "~/Assets/js/timer.js"));

            bundles.Add(new ScriptBundle("~/system_login").Include(
                "~/Assets/js/login.js"));

            bundles.Add(new ScriptBundle("~/system_header").Include(
               "~/Assets/js/header.js"));

            bundles.Add(new ScriptBundle("~/system_footer").Include(
               "~/Assets/js/footer.js"));

            bundles.Add(new ScriptBundle("~/system_apps").Include(
               "~/Assets/js/app.js"));

            bundles.Add(new ScriptBundle("~/system_list").Include(
               "~/Assets/js/list.js"));

            bundles.Add(new StyleBundle("~/system_css").Include(
                "~/Assets/plugins/general/perfect-scrollbar/css/perfect-scrollbar.css",
                //"~/Assets/plugins/general/tether/dist/css/tether.css",
                //"~/Assets/plugins/general/bootstrap-datepicker/dist/css/bootstrap-datepicker3.css",
                //"~/Assets/plugins/general/bootstrap-datetime-picker/css/bootstrap-datetimepicker.css",
                //"~/Assets/plugins/general/bootstrap-timepicker/css/bootstrap-timepicker.css",
                //"~/Assets/plugins/general/bootstrap-daterangepicker/daterangepicker.css",
                //"~/Assets/plugins/general/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.css",
                //"~/Assets/plugins/general/bootstrap-select/dist/css/bootstrap-select.css",
                //"~/Assets/plugins/general/bootstrap-switch/dist/css/bootstrap3/bootstrap-switch.css",
                //"~/Assets/plugins/general/select2/dist/css/select2.css",
                //"~/Assets/plugins/general/ion-rangeslider/css/ion.rangeSlider.css",
                //"~/Assets/plugins/general/nouislider/distribute/nouislider.css",
                //"~/Assets/plugins/general/owl.carousel/dist/Assets/owl.carousel.css",
                //"~/Assets/plugins/general/owl.carousel/dist/Assets/owl.theme.default.css",
                //"~/Assets/plugins/general/dropzone/dist/dropzone.css",
                //"~/Assets/plugins/general/quill/dist/quill.snow.css",
                //"~/Assets/plugins/general/yaireo/tagify/dist/tagify.css",
                //"~/Assets/plugins/general/summernote/dist/summernote.css",
                //"~/Assets/plugins/general/bootstrap-markdown/css/bootstrap-markdown.min.css",
                "~/Assets/plugins/general/animate.css/animate.css",
                "~/Assets/plugins/general/toastr/build/toastr.css",
                //"~/Assets/plugins/general/dual-listbox/dist/dual-listbox.css",
                //"~/Assets/plugins/general/morris.js/morris.css",
                "~/Assets/plugins/general/sweetalert2/dist/sweetalert2.css",
                //"~/Assets/plugins/general/socicon/css/socicon.css",
                "~/Assets/plugins/general/plugins/line-awesome/css/line-awesome.css",
                "~/Assets/plugins/general/plugins/flaticon/flaticon.css",
                "~/Assets/plugins/general/plugins/flaticon2/flaticon.css",
                "~/Assets/plugins/general/fontawesome/fontawesome-free/css/all.min.css",
                "~/Assets/css/style.bundle.css",
                "~/Assets/plugins/custom/plugins/jquery-ui/jquery-ui.min.css",
                //"~/Assets/plugins/custom/fullcalendar/core/main.css",
                //"~/Assets/plugins/custom/fullcalendar/daygrid/main.css",
                //"~/Assets/plugins/custom/fullcalendar/list/main.css",
                //"~/Assets/plugins/custom/fullcalendar/timegrid/main.css",
                //"~/Assets/plugins/custom/datatables.net-bs4/css/dataTables.bootstrap4.css",
                //"~/Assets/plugins/custom/datatables.net-buttons-bs4/css/buttons.bootstrap4.min.css",
                //"~/Assets/plugins/custom/datatables.net-autofill-bs4/css/autoFill.bootstrap4.min.css",
                //"~/Assets/plugins/custom/datatables.net-colreorder-bs4/css/colReorder.bootstrap4.min.css",
                //"~/Assets/plugins/custom/datatables.net-fixedcolumns-bs4/css/fixedColumns.bootstrap4.min.css",
                //"~/Assets/plugins/custom/datatables.net-fixedheader-bs4/css/fixedHeader.bootstrap4.min.css",
                //"~/Assets/plugins/custom/datatables.net-keytable-bs4/css/keyTable.bootstrap4.min.css",
                //"~/Assets/plugins/custom/datatables.net-responsive-bs4/css/responsive.bootstrap4.min.css",
                //"~/Assets/plugins/custom/datatables.net-rowgroup-bs4/css/rowGroup.bootstrap4.min.css",
                //"~/Assets/plugins/custom/datatables.net-rowreorder-bs4/css/rowReorder.bootstrap4.min.css",
                //"~/Assets/plugins/custom/datatables.net-scroller-bs4/css/scroller.bootstrap4.min.css",
                //"~/Assets/plugins/custom/datatables.net-select-bs4/css/select.bootstrap4.min.css",
                //"~/Assets/plugins/custom/jstree/dist/themes/default/style.css",
                //"~/Assets/plugins/custom/jqvmap/dist/jqvmap.css",
                //"~/Assets/plugins/custom/uppy/dist/uppy.min.css",
                "~/Assets/css/skins/header/base/light.css",
                "~/Assets/css/skins/header/menu/light.css",
                "~/Assets/css/skins/brand/dark.css",
                "~/Assets/css/skins/aside/dark.css",
                "~/Assets/plugins/custom/gridstack/gridstack.css",
                "~/Assets/css/custom.css",
                "~/Assets/css/grid.css"
            ));

            bundles.Add(new StyleBundle("~/login_css").Include(
                "~/Assets/css/login/login.css"
            ));
        }
    }
}
