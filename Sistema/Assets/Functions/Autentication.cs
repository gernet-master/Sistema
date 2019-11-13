/*
Descrição: Verifica se o usuário está autenticado no sistema
Data: 01/01/2020 - v.1.0
*/

using Sistema.Assets.DB;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Functions
{
    public class Autentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            // Verifica se as variáveis de sessão existem
            int idusuario = Convert.ToInt32(HttpContext.Current.Session["usuario"]);
            int idgernet = Convert.ToInt32(HttpContext.Current.Session["gernet"]);

            // Valida dados
            if ((idusuario == 0) || (idgernet == 0))
            {
                // Redireciona para a tela de login.
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
            else
            {
                // Valida na base de dados
                Boolean usuario = new UsuariosDB().ValidaUsuarioCliente(idusuario);

                if (usuario)
                {
                    // Autentica
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    // Redireciona para a tela de login.
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
                }
            }
        }
    }
}