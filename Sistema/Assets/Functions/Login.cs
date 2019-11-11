/*
Descrição: Validação dos dados de acesso ao sistema
Data: 01/01/2020 - v.1.0
*/

using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using System;
using System.Web;

namespace Functions
{
    public class Login
    {
        // Valida usuário e senha
        public static string LoginCheck(string user = "", string password = "", int remember = 0)
        {

            // Verifica se o usuário está dentro do padrão
            if ((user == "") || (user.Length > 20))
            {                
                return "0|" + Language.XmlLang(46, 2).Text;
            }

            // Verifica se a senha está dentro do padrão
            if ((password == "") || (password.Length > 20))
            {
                return "0|" + Language.XmlLang(46, 2).Text;
            }

            // Codifica a senha
            password = Crypt.CreateHash(password);

            // Valida os dados
            Usuarios usuario = new UsuariosDB().Login(user, password);

            // Dados incorretos
            if (usuario == null)
            {
                return "0|" + Language.XmlLang(46, 2).Text;
            }

            // Dados corretos
            else
            {
                // Verifica se usuário está ativo
                if (!Convert.ToBoolean(usuario.flativo.value))
                {
                    return "0|" + Language.XmlLang(47, 2).Text;
                }

                // Verifica se o usuário é forçado a alterar a senha
                if (Convert.ToBoolean(usuario.flalterasenha.value))
                {
                    return "1|";
                }

                // Armazena em variáveis de sessão
                HttpContext.Current.Session["idusuario"] = usuario.idusuario.value;
                HttpContext.Current.Session["idperfil"] = usuario.idperfil.value;
                HttpContext.Current.Session["txnome"] = usuario.txnome.value;

                // Verifica se deve criar cookie
                if (remember == 1)
                {
                    HttpCookie cookie = new HttpCookie("USERIDENT")("sdfsdsd");
                }

                return "2|";
            }

        }

    }
}