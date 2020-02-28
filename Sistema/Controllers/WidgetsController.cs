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
        public ActionResult Opcoes(string id = "")
        {
            return PartialView(new WidgetsView_Opcoes(id));
        }

        // Novo Registro
        [Autentication]
        public ActionResult NovoRegistro(Widgets widget, string texto = "")
        {
            return PartialView(new WidgetsView_NovoRegistro(widget, texto));
        }

        // Listagem
        [Autentication]
        public ActionResult Listagem(Widgets widget, string texto, dynamic result)
        {
            return PartialView(new WidgetsView_Listagem(widget, texto, result));
        }
    }
}