using Functions;
using System.Web.Mvc;

namespace Sistema.Controllers
{
    public class ErrorController : Controller
    {
        // Controle de erros do sistema
        public ActionResult Index()
        { 
            return PartialView();
        }

        // Página de url de aplicativo inválido
        [Autentication]
        [Route("Error/InvalidUrl")]
        public ActionResult InvalidUrl()
        {
            return PartialView();
        }
    }
}