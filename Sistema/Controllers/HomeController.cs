using Functions;
using Sistema.Models;
using System.Web.Mvc;

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
    }
}