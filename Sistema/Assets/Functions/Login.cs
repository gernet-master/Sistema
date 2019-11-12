/*
Descrição: Validação dos dados de acesso ao sistema
Data: 01/01/2020 - v.1.0
*/

using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
                // Pega a data de bloqueio se existir
                Usuarios_Sistema usuarios_sistema = new Usuarios_SistemaDB().GetUserControl(usuario.idusuario.value);
                if (usuarios_sistema != null)
                {
                    if (usuarios_sistema != null)
                    {
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        var seconds = System.Math.Abs((Convert.ToDateTime(usuarios_sistema.txbloqueado.value) - DateTime.Now).TotalSeconds);
                    }
                }

                // Verifica se usuário está ativo
                if (!Convert.ToBoolean(usuario.flativo.value))
                {
                    return "0|" + Language.XmlLang(47, 2).Text;
                }

                // Verifica se o usuário é forçado a alterar a senha
                if (Convert.ToBoolean(usuario.flalterasenha.value))
                {
                    // Redireciona para tela de alteração
                    return "1|";
                }

                // Insere a sessão do usuário na aplicação se não existir
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application["sessions"] = HttpContext.Current.Application["sessions"] + HttpContext.Current.Session.SessionID + ",";
                HttpContext.Current.Application.UnLock();

                // Armazena em variáveis de sessão
                HttpContext.Current.Session["idusuario"] = usuario.idusuario.value;
                HttpContext.Current.Session["idperfil"] = usuario.idperfil.value;
                HttpContext.Current.Session["txnome"] = usuario.txnome.value;
                HttpContext.Current.Session["flmaster"] = usuario.flmaster.value;

                // Verifica se deve gravar cookie
                if (remember == 1)
                {
                    // Pega o cookie
                    HttpCookie cookie = HttpContext.Current.Request.Cookies["sistema_gernet"];

                    // Se já existir, atualiza valor
                    if (cookie != null)
                    {
                        cookie.Value = usuario.idusuario.value + "|" + usuario.idperfil.value; 
                    }

                    // Cria novo cookie
                    else
                    {
                        cookie = new HttpCookie("sistema_gernet");
                        cookie.Value = usuario.idusuario.value + "|" + usuario.idperfil.value;
                    }

                    // Define o domínio do cookie
                    cookie.Domain = "localhost";

                    // Define a data de expiração
                    cookie.Expires = DateTime.Now.AddYears(3);                    

                    // Grava cookie
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }

                // Remove cookie
                else
                {
                    // Pega o cookie
                    HttpCookie cookie = HttpContext.Current.Request.Cookies["sistema_gernet"];

                    // Se já existir, atualiza valor e seta data de expiração
                    if (cookie != null)
                    {
                        cookie.Value = "0|0";
                        cookie.Expires = DateTime.Now.AddDays(-1);

                        // Grava cookie
                        HttpContext.Current.Response.Cookies.Add(cookie);
                    }
                }

                // Verifica se não existe e cria o controle de usuário
                if (usuarios_sistema == null)
                {
                    usuarios_sistema = new Usuarios_Sistema();
                    usuarios_sistema.idusuario.value = usuario.idusuario.value;
                    usuarios_sistema.Save();
                }

                // Se usuário estava marcado para derrubar, verifica tempo e zera variáveis
                else
                {
                    if ((usuarios_sistema.flderrubar.value == 0) && (System.Math.Abs((DateTime.Now - Convert.ToDateTime(usuarios_sistema.txderrubar)).TotalSeconds) < 0))
                    {
                        usuarios_sistema.flderrubar.value = 0;
                        usuarios_sistema.txderrubar.value = null;
                    }
                }

                // Pega preferencias do usuário
                Usuarios_Preferencias usuarios_preferencias = new Usuarios_PreferenciasDB().GetUserPreferences(usuario.idusuario.value);

                // Se não existir unidade padrão, pega o primeiro cadastrado
                int unidade = 0;
                if ((usuarios_preferencias == null) || (Convert.ToInt32(usuarios_preferencias.idunidade) == 0))
                {
                    List<Usuarios_Unidades> usuarios_unidades = new Usuarios_UnidadesDB().List(usuario.idusuario.value);
                    if (usuarios_unidades != null)
                    {
                        unidade = usuarios_unidades.FirstOrDefault().idunidade.value;
                    }
                }

                // Atualiza valores de controle
                usuarios_sistema.idunidade.value = unidade;
                usuarios_sistema.idsession.value = HttpContext.Current.Session.SessionID;
                usuarios_sistema.qtacessos.value = usuarios_sistema.qtacessos.value++;
                usuarios_sistema.txip.value = Utils.GetIPAddress();
                usuarios_sistema.txrefresh.value = DateTime.Now;
                usuarios_sistema.Edit();

                // Grava o código do escritório
                HttpContext.Current.Session["IdUnidade"] = unidade;

                // Exclui os frames cadastrados do usuário
                new Usuarios_FramesDB().Delete(usuario.idusuario.value);

                // Grava log de acesso
                Log_Acesso log = new Log_Acesso();
                log.idusuario.value = usuario.idusuario.value;
                log.tplog.value = "E";
                log.Save();

                // Redireciona para home
                return "2|";
            }

        }

    }
}