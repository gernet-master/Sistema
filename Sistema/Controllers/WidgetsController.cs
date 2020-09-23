using Functions;
using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using Sistema.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace Sistema.Controllers
{
    public class WidgetsController : Controller
    {
        // Opções
        [Autentication]
        public ActionResult Options(string id = "")
        {
            return PartialView(new WidgetsView_Options(id));
        }

        // Novo Registro
        [Autentication]
        public ActionResult New(Widgets widget, string texto = "")
        {
            return PartialView(new WidgetsView_New(widget, texto));
        }

        // Listagem
        [Autentication]
        public ActionResult List(Widgets widget, string texto, dynamic result)
        {           
            return PartialView(new WidgetsView_List(widget, texto, result));
        }
    }
}