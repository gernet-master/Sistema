/*
Descrição: Controlador da página de login
Data: 01/01/2021 - v.1.0
*/

using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using System;
using System.Web.Mvc;
using System.Web.Routing;
using Functions;

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
            string user = Utils.RemoveDiacritics(form["user"]);
            string password = Utils.RemoveDiacritics(form["password"]);
            int remember = Utils.Numbers(Utils.Null(form["password"], "0"));

            // Valida dados
            Retorno result = Login.LoginCheck(user, password, remember);

            // Retorno
            return Json(result);
        }

        // Confirma a exclusão de registros
        [Route("Login/ConfirmDelete")]
        public JsonResult ConfirmDelete(string password = "")
        {
            var resultado = new { login = 0 };

            // Verifica se possui usuário na sessão
            if (Session["usuario"] != null)
            {
                // Codifica a senha
                password = Crypt.CreateHash(password);

                // Valida os dados
                Usuarios usuario = new UsuariosDB().Login("", password, Convert.ToInt32(Session["usuario"]));

                // Usuário validado
                if (usuario != null)
                {
                    resultado = new { login = 1 };
                }
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
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
            string password_atual = Utils.RemoveDiacritics(form["password_atual"]);
            string password_novo = Utils.RemoveDiacritics(form["password_novo"]);

            // Valida dados
            Retorno result = Login.ChangePassword(password_atual, password_novo);

            // Retorno
            return Json(result);
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
            Retorno result = Login.RecoverPassword(user, email);

            // Retorno
            return Json(result);
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
            // Verifica se o código é válido
            Log_Link_Senha log_link_senha = new Log_Link_SenhaDB().Buscar(code);

            if (log_link_senha == null)
            {
                ViewBag.error = 1;
            }
            else
            {
                // Verifica se o link já foi utilizado
                if (log_link_senha.flutilizado.value == 1)
                {
                    ViewBag.error = 2;
                }
                else
                {
                    // Minutos entre a data atual e a data do link
                    var minutes = System.Math.Abs((Convert.ToDateTime(log_link_senha.dtlink.value) - DateTime.Now).TotalMinutes);

                    // Verifica o período de validade do link
                    if (minutes >= 1440)
                    {
                        ViewBag.error = 3;
                    }
                    else
                    {
                        // Busca o usuário
                        Usuarios usuarios = new UsuariosDB().Buscar(log_link_senha.idusuario.value);

                        // Verifica se o usuário existe
                        if (usuarios == null)
                        {
                            ViewBag.error = 4;
                        }
                        else
                        {
                            ViewBag.error = 0;

                            // Gera nova senha
                            ViewBag.password = Utils.RandomString(8);

                            // Altera a senha do usuário
                            usuarios.txsenha.value = Crypt.CreateHash(ViewBag.password);
                            usuarios.Alterar();

                            // Grava no log de alteração de senha do usuário
                            Log_Senha log_senha = new Log_Senha();
                            log_senha.idusuario.value = log_link_senha.idusuario.value;
                            log_senha.Gravar();

                            // Grava que o link foi utilizado
                            log_link_senha.flutilizado.value = 1;
                            log_link_senha.dtutilizado.value = DateTime.Now;
                            log_link_senha.Alterar();
                        }
                        
                    }
                }
            }

            return PartialView();
        }

        // Sair do sistema
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