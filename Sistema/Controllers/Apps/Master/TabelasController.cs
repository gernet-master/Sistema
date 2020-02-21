using Functions;
using Sistema.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sistema.Controllers
{
    public class TabelasController : Controller
    {
        // Dashboard do cadastro de tabelas
        [Autentication]
        [Route("Apps/Master/Tabelas")]
        public ActionResult Index()
        {
            return PartialView("~/Views/Apps/Master/Tabelas/Index.cshtml");
        }

        // Página do cadastro de tabelas
        [Autentication]
        [Route("Apps/Master/Tabelas/Incluir")]
        public ActionResult Incluir()
        {
            return PartialView("~/Views/Apps/Master/Tabelas/Incluir.cshtml", new TabelasView(0, 0));
        }
    }
}