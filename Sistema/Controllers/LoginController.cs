using Functions;
using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using System;
using System.Web.Mvc;
using System.Web.Routing;

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
        [Route("Validation")]
        [HttpPost]
        public JsonResult Validation(FormCollection form)
        {
            // Recebe as variáveis
            string user = Utils.ClearText(form["user"], 20);
            string password = Utils.ClearText(form["password"], 20);
            int remember = Utils.Numbers(Utils.Null(form["password"], "0"));

            // Valida dados
            string r = Login.LoginCheck(user, password, remember);

            // Retorno
            return Json(r);
        }

        // Formulário para alterar senha
        [Route("Password")]
        public ActionResult Password()
        {
            // Se não possuir usuário na sessão, redireciona para login
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return PartialView();
        }

        // Validação dos dados para alterar a senha
        [Route("ChangePassword")]
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

        // Validação dos dados para recuperar senha
        [Route("RecoverPassword")]
        [HttpPost]
        public JsonResult RecoverPassword(FormCollection form)
        {
            // Recebe as variáveis
            string user = Utils.ClearText(form["user_r"], 20);
            string email = Utils.ClearText(form["email"], 20);

            // Valida dados
            string r = Login.RecoverPassword(user, email);

            // Retorno
            return Json(r);
        }

        // Usuário acessou de outro local
        [Route("Disconnnect")]
        public ActionResult Disconnnect()
        {

            // Grava log de acesso
            Log_Acesso log = new Log_Acesso();
            log.idusuario.value = Convert.ToInt32(Session["usuario"]);
            log.tplog.value = "D";
            log.Gravar();

            // Remove as variáveis de sessão
            Session.Clear();
            Session.Abandon();

            // Redireciona para página de login
            return PartialView();
        }

        // Reseta a senha do usuaário
        [Route("ResetPassword/{code}")]
        public ActionResult Reset(string code = "")
        {
            
            return PartialView();
        }

        // Sai do sistema
        [Route("Logout")]
        public ActionResult Logout()
        {
            // Remove da aplicação
            HttpContext.Application.Lock();
            HttpContext.Application["contusr"] = Convert.ToInt32(HttpContext.Application["contusr"]) - 1;
            HttpContext.Application["sessions"] = Convert.ToString(HttpContext.Application["sessions"]).Replace(Session.SessionID, "");
            HttpContext.Application.UnLock();

            // Se possuir usuário na sessão, executa ações de logout
            if (Session["usuario"] != null)
            {

                // Remove o sessionid
                Usuarios_Sistema usuarios_sistema = new Usuarios_SistemaDB().Buscar(Convert.ToInt32(Session["usuario"]));
                usuarios_sistema.idsession.value = "";
                usuarios_sistema.Alterar();

                // Exclui os frames cadastrados do usuário
                new Usuarios_FramesDB().Excluir(Convert.ToInt32(Session["usuario"]));

                // Grava log de acesso
                Log_Acesso log = new Log_Acesso();
                log.idusuario.value = Convert.ToInt32(Session["usuario"]);
                log.tplog.value = "S";
                log.Gravar();
            }

            // Remove as variáveis de sessão
            Session.Clear();
            Session.Abandon();

            // Redireciona para página de login
            return RedirectToAction("Index");
        }

    }
}