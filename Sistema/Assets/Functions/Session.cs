/*
Descrição: Retorna o código do cliente e do usuário
Data: 01/01/2020 - v.1.0
*/

using Functions;
using System;
using System.Web;

namespace Sistema.Assets.Functions
{
    public class Session
    {
        // Pega o código do cliente da sessão
        public int session_gernet = Convert.ToInt32(Utils.Null(Convert.ToString(HttpContext.Current.Session["gernet"]), "0"));

        // Pega o código do usuário da sessão
        public int session_usuario = Convert.ToInt32(Utils.Null(Convert.ToString(HttpContext.Current.Session["usuario"]), "0"));
    }

}
