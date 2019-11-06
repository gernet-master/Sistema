using Functions;
using Sistema.Models;
using System;
using System.Globalization;
using System.Web.Mvc;

namespace Sistema.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return PartialView();
        }

        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public JsonResult acessojson(string login, string senha)
        //{
        //    string retorno = "";

        //    TimelineUsuariosDB db = new TimelineUsuariosDB();
        //    TimelineUsuarios usuario = db.ValidaSenha(login, GerarHashMd5(senha));

        //    if (usuario == null)
        //    {
        //        retorno = "Dados incorretos";

        //        retorno = "{\"retorno\": \"" + retorno + "\"}";
        //    }
        //    else
        //    {

        //        //salva o cookies com o codigo do acesso_cookies
        //        HttpCookie cookie = Request.Cookies["CenbrapTimeline"];
        //        if (cookie != null)
        //            cookie.Value = Convert.ToString(usuario.idusuario);   // update cookie value
        //        else
        //        {
        //            cookie = new HttpCookie("CenbrapTimeline");
        //            cookie.Value = Convert.ToString(usuario.idusuario);
        //        }

        //        if (Request.Url.Host != "localhost")
        //        {
        //            cookie.Domain = ".cenbrap.com.br";
        //        }

        //        //cookie.Domain = ".cenbrap.com.br";
        //        Response.Cookies.Add(cookie);

        //        retorno = "OK";

        //        retorno = "{\"retorno\": \"" + retorno + "\"}";

        //    }

        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        //public static string GerarHashMd5(string input)
        //{
        //    MD5 md5Hash = MD5.Create();
        //    byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        //    StringBuilder sBuilder = new StringBuilder();
        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        sBuilder.Append(data[i].ToString("x2"));
        //    }
        //    return sBuilder.ToString();
        //}

    }
}