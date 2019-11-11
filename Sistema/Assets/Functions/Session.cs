using Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema.Assets.Functions
{
    public class Session
    {
        public int session_gernet = Convert.ToInt32(Utils.Null(Convert.ToString(HttpContext.Current.Session["idgernet"]), "0"));

    }

}
