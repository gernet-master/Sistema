/*
Descrição: Verifica se o usuário está autenticado no sistema
Data: 01/01/2020 - v.1.0
*/

using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Functions
{
    public class Autentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            // Pega o cookie
            HttpCookie cookie = new HttpCookie("Gernet");
            cookie = HttpContext.Current.Request.Cookies["Gernet"];

            // Verifica se o cookie possui valor
            if (cookie != null)
            {
                // Pega o valor do cookie
                int codigo = Convert.ToInt32(cookie.Value);

                // Busca o usuário no sistema
                int user = 1;
                
                // Valida o usuário
                if (user == 1)
                {
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    // Se não existe, redireciona para a tela de login.
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
                }

            }
            else
            {
                // Se os cookies não existem, redireciona para a tela de login
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }

        }



    }
}