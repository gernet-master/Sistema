/*
Descrição: Retorna o código do cliente
Data: 01/01/2020 - v.1.0
*/

using Functions;
using System;
using System.Web;

namespace Sistema.Assets.Functions
{
    public class Session
    {
        // Pega o ódigo do cliente da sessão
        public int session_gernet = Convert.ToInt32(Utils.Null(Convert.ToString(HttpContext.Current.Session["gernet"]), "0"));
    }

}
