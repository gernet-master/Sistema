using Functions;
using Sistema.Models;
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
            return PartialView("~/Views/Administrador/Usuarios/Index.cshtml", new UsuariosView());
        }
    }
}