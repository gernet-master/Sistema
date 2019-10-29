using Functions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema.Controllers
{
    public class PartialsController : Controller
    {
        public ActionResult HeaderMobile()
        {
            return PartialView();
        }

        public ActionResult Menu()
        {
            return PartialView();
        }

        public ActionResult Header()
        {
            return PartialView();
        }

        public ActionResult SubHeader()
        {
            return PartialView();
        }

        public ActionResult Footer()
        {
            return PartialView();
        }

        public ActionResult QuickPanel()
        {
            return PartialView();
        }

        public ActionResult ScrollTop()
        {
            return PartialView();
        }

        public ActionResult Search()
        {
            return PartialView();
        }

        public ActionResult Notifications()
        {
            return PartialView();
        }

        public ActionResult QuickActions()
        {
            return PartialView();
        }

        public ActionResult QuickPanelInfo()
        {
            return PartialView();
        }

        public ActionResult Languages()
        {
            return PartialView();
        }

        public ActionResult UserPanel()
        {
            return PartialView();
        }

        // Retorna a hora atual do servidor
        public string Clock()
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            int day = DateTime.Now.Day;
            int year = DateTime.Now.Year;
            string month = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
            string weekday = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
            string date = weekday + ", " + day + " " + Language.XmlLang(5, 0).Text + " " + month + " " + Language.XmlLang(5, 0).Text + " " + year + "<br>" + DateTime.Now.ToString("HH:mm:ss");
            return date;
        }

    }
}