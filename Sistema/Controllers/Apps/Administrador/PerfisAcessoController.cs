using Functions;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sistema.Controllers
{
    public class PerfisAcessoController : Controller
    {
        // Página inicial do cadastro de perfil de acesso
        [Autentication]
        [Route("Apps/Administrador/PerfisAcesso")]
        public ActionResult Index()
        {
            return PartialView("~/Views/Apps/Administrador/PerfisAcesso/Index.cshtml");
        }
    }
}