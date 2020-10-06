/*
Descrição: Controlador para cadastro de módulos do sistema
Data: 01/01/2021 - v.1.0
*/

using Functions;
using Sistema.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sistema.Controllers
{
    public class ModulosSistemaController : Controller
    {
        // Dashboard
        [Autentication]
        public ActionResult Dashboard()
        {
            return PartialView("~/Views/Apps/Master/Modulos/Dashboard.cshtml");
        }
    }
}