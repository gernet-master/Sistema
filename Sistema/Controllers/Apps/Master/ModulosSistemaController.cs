using Functions;
using Sistema.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sistema.Controllers
{
    public class ModulosSistemaController : Controller
    {
        // Página inicial do cadastro de módulos do sistema
        [Autentication]
        [Route("Apps/Master/ModulosSistema")]
        public ActionResult Index()
        {
            return PartialView("~/Views/Apps/Master/ModulosSistema/Index.cshtml", new UsuariosView());
        }
    }
}