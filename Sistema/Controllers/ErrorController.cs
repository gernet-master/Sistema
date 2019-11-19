using Functions;
using System.Web.Mvc;

namespace Sistema.Controllers
{
    public class ErrorController : Controller
    {
        // Controle de erros do sistema
        public ActionResult Index(int error = 0)
        { 
            return PartialView();
        }      
    }
}