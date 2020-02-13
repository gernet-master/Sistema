using Functions;
using Sistema.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sistema.Controllers
{
    public class TabelasController : Controller
    {
        // Página inicial do cadastro de tabelas
        [Autentication]
        [Route("Apps/Master/Tabelas")]
        public ActionResult Index()
        {
            return PartialView("~/Views/Apps/Master/Tabelas/Index.cshtml", new UsuariosView());
        }
    }
}