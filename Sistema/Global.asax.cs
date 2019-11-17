using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sistema
{
    public class MvcApplication : System.Web.HttpApplication
    {
        // Aplicação iniciada
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Variaveis de aplicação
            Application["contusr"] = 0;
            Application["sessions"] = "";
        }

        // Aplicação encerrada
        protected void Application_End()
        {
            Application.Clear();
        }

        // Sessão iniciada
        protected void Session_Start(object sender, EventArgs e)
        {
            Session["gernet"] = 1;
        }

        // Sessão encerrada
        protected void Session_End(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
        }
    }
}
