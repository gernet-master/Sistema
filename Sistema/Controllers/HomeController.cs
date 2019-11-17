using Functions;
using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using Sistema.Models;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using System;

namespace Sistema.Controllers
{
    public class HomeController : Controller
    {
        // Dashboard principal do sistema
        [Autentication]
        public ActionResult Index()
        { 
            return View();
        }

        // Gera nova senha para o usuário
        public ActionResult Password(string code = "")
        {
            return View();
        }

        // Sai do sistema
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