using Functions;
using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using Sistema.Models;
using System;
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

        // Pesquisa Rápida
        [Autentication]
        public ActionResult PesquisaRapida(Widgets widget, string texto = "")
        {
            return PartialView(new WidgetsView_PesquisaRapida(widget, texto));
        }
    }
}