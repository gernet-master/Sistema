using Functions;
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

        // Dashboard principal do sistema
        [Autentication]
        public ActionResult Dashboard()
        {
            return PartialView();
        }
    }
}