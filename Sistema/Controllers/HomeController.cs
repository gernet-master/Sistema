/*
Descrição: Controlador incial do sistema
Data: 01/01/2021 - v.1.0
*/

using System.Web.Mvc;
using Functions;

namespace Sistema.Controllers
{
    public class HomeController : Controller
    {
        // Dashboard principal do sistema
        [Autentication]
        public ActionResult Index()
        {
            return View();
        }

        // Dashboard principal do sistema
        [Autentication]
        public ActionResult Dashboard()
        {
            return PartialView();
        }
    }
}