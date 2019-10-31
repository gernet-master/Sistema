using Functions;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sistema.Controllers
{
    public class UsuariosController : Controller
    {
        // Página inicial do cadastro de usuários
        [Autentication]
        [Route("Administrador/Usuarios")]
        public ActionResult Index()
        {
            return View("~/Views/Administrador/Usuarios/Index.cshtml");
        }
    }
}