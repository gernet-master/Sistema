/*
Descrição: Validação dos dados de acesso ao sistema
Data: 01/01/2020 - v.1.0
*/

using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using System;

namespace Functions
{
    public class Login
    {
        // Valida usuário e senha
        public static string LoginCheck(string user = "", string password = "")
        {

            // Verifica se o usuário está dentro do padrão
            if ((user == "") || (user.Length > 20))
            {                
                return "0|1";
            }

            // Verifica se a senha está dentro do padrão
            if ((password == "") || (password.Length > 20))
            {
                return "0|2";
            }

            // Codifica a senha
            password = Crypt.CreateHash(password);

            // Valida os dados
            Usuarios usuario = new UsuariosDB().Login(user, password);

            // Dados incorretos
            if (usuario == null)
            {
                return "0|3";
            }

            // Dados corretos
            else
            {
                // Verifica se usuário está ativo
                if (!Convert.ToBoolean(usuario.flativo))
                {
                    return "0|4";
                }

                // Verifica se o usuário é forçado a alterar a senha
                if (Convert.ToBoolean(usuario.flalterasenha))
                {
                    return "0|5";
                }

                // Armazena em variáveis de sessão
                System.Web.HttpContext.Current.Session["idusuario"] = usuario.idusuario;
                System.Web.HttpContext.Current.Session["idperfil"] = usuario.idperfil;
                System.Web.HttpContext.Current.Session["txnome"] = usuario.txnome;

                return "1";
            }

        }

    }
}