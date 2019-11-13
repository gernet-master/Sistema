using Functions;
using System;
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
            int remember = Utils.Numbers(form["password"]);

            // Valida dados
            string r = Login.LoginCheck(user, password, remember);

            // Retorno
            return Json(r);
        }

        // Formulário para alterar senha
        public ActionResult Password()
        {
            return PartialView();
        }

        // Validação dos dados para alterar a senha
        [HttpPost]
        public JsonResult ChangePassword(FormCollection form)
        {
            // Recebe as variáveis
            string password_atual = Utils.ClearText(form["password_atual"], 20);
            string password_novo = Utils.ClearText(form["password_novo"], 20);

            // Valida dados
            string r = Login.ChangePassword(password_atual, password_novo);

            // Retorno
            return Json(r);
        }

    }
}