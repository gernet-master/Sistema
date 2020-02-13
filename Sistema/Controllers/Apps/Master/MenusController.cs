using Functions;
using Sistema.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sistema.Controllers
{
    public class MenusController : Controller
    {
        // Página inicial do cadastro de menus
        [Autentication]
        [Route("Apps/Master/Menus")]
        public ActionResult Index()
        {
            return PartialView("~/Views/Apps/Master/Menus/Index.cshtml", new UsuariosView());
        }
    }
}