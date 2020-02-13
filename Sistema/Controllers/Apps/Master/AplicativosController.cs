using Functions;
using Sistema.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sistema.Controllers
{
    public class AplicativosController : Controller
    {
        // Página inicial do cadastro de aplicativos
        [Autentication]
        [Route("Apps/Master/Aplicativos")]
        public ActionResult Index()
        {
            return PartialView("~/Views/Apps/Master/Aplicativos/Index.cshtml", new UsuariosView());
        }
    }
}