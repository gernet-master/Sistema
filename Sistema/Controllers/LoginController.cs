using Functions;
using Sistema.Models;
using System;
using System.Globalization;
using System.Web.Mvc;

namespace Sistema.Controllers
{
    public class LoginController : Controller
    {
        // Tela de login
        public ActionResult Index()
        {
            return PartialView();
        }

        // Validação dos dados de acesso
        [HttpPost]
        public JsonResult Validation(FormCollection form)
        {
            // Recebe as variáveis
            string user = Utils.ClearText(form["user"], 20);
            string password = Utils.ClearText(form["password"], 20);

            // Valida dados
            string r = Login.LoginCheck(user, password);

            // Retorno
            return Json(r);
        }

    }
}