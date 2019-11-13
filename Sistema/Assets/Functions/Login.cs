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
            Usuarios usuario = new UsuariosDB().Login(user, password, 0);

            // Dados incorretos
            if (usuario == null)
            {
                return "0|" + Language.XmlLang(46, 2).Text;
            }

            // Dados corretos
            else
            {
                // Pega a data de bloqueio se existir
                Usuarios_Sistema usuarios_sistema = new Usuarios_SistemaDB().ControleUsuario(usuario.idusuario.value);
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
                    // Armazena em variáveis de sessão
                    HttpContext.Current.Session["usuario"] = usuario.idusuario.value;
                    HttpContext.Current.Session["nome"] = usuario.txnome.value;
                    HttpContext.Current.Session["cookie"] = remember;

                    // Redireciona para tela de alteração
                    return "1|";
                }

                // Executa as rotinas de login
                RotinaLogin(usuario, usuarios_sistema, remember);

                // Redireciona para home
                return "2|";
            }

        }

        // Altera a senha do usuário
        public static string ChangePassword(string password_atual = "", string password_novo = "")
        {
            // Verifica se a senha está dentro do padrão
            if ((password_atual == "") || (password_atual.Length > 20))
            {
                return "0|" + Language.XmlLang(46, 2).Text;
            }

            // Verifica se a nova senha está dentro do padrão
            if ((password_novo == "") || (password_novo.Length > 20))
            {
                return "0|" + Language.XmlLang(46, 2).Text;
            }

            // Verifica se a nova senha é igual a antiga
            if (password_atual == password_novo)
            {
                return "0|" + Language.XmlLang(67, 2).Text;
            }

            // Codifica a senha
            password_atual = Crypt.CreateHash(password_atual);
            password_novo = Crypt.CreateHash(password_novo);

            // Valida os dados
            Usuarios usuario = new UsuariosDB().Login("", password_atual, Convert.ToInt32(HttpContext.Current.Session["usuario"]));

            // Pega os dados de controle do usuário
            Usuarios_Sistema usuarios_sistema = new Usuarios_SistemaDB().ControleUsuario(usuario.idusuario.value);

            // Dados incorretos
            if (usuario == null)
            {
                return "0|" + Language.XmlLang(46, 2).Text;
            }

            // Dados corretos
            else
            {
                usuario.flalterasenha.value = 0;
                usuario.txsenha.value = password_novo;
                usuario.Alterar();
            }

            // Executa as rotinas de login
            RotinaLogin(usuario, usuarios_sistema, Convert.ToInt32(HttpContext.Current.Session["cookie"]));

            // Redireciona para home
            return "1|";
        }

        // Rotinas de login
        public static void RotinaLogin(Usuarios usuario, Usuarios_Sistema usuarios_sistema, int remember)
        {
            // Pega preferencias do usuário
            Usuarios_Preferencias usuarios_preferencias = new Usuarios_PreferenciasDB().PreferenciasUsuario(usuario.idusuario.value);

            // Insere a sessão do usuário na aplicação se não existir
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["sessions"] = HttpContext.Current.Application["sessions"] + HttpContext.Current.Session.SessionID + ",";
            HttpContext.Current.Application.UnLock();

            // Armazena em variáveis de sessão
            HttpContext.Current.Session["usuario"] = usuario.idusuario.value;
            HttpContext.Current.Session["perfil"] = usuario.idperfil.value;
            HttpContext.Current.Session["nome"] = usuario.txnome.value;
            HttpContext.Current.Session["master"] = usuario.flmaster.value;
            HttpContext.Current.Session["language"] = usuarios_preferencias.txidioma.value;

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
                usuarios_sistema.Gravar();
            }

            // Se usuário estava marcado para derrubar, verifica tempo e zera variáveis
            else
            {
                if ((usuarios_sistema.flderrubar.value == 0) && (System.Math.Abs((DateTime.Now - Convert.ToDateTime(usuarios_sistema.txderrubar.value)).TotalSeconds) < 0))
                {
                    usuarios_sistema.flderrubar.value = 0;
                    usuarios_sistema.txderrubar.value = null;
                }
            }

            // Se não existir unidade padrão, pega o primeiro cadastrado
            int unidade = 0;
            if ((usuarios_preferencias == null) || (Convert.ToInt32(usuarios_preferencias.idunidade.value) == 0))
            {
                List<Usuarios_Unidades> usuarios_unidades = new Usuarios_UnidadesDB().Listar(usuario.idusuario.value);
                if (usuarios_unidades != null)
                {
                    unidade = usuarios_unidades.FirstOrDefault().idunidade.value;
                }
            }

            // Atualiza valores de controle
            usuarios_sistema.idunidade.value = unidade;
            usuarios_sistema.idsession.value = HttpContext.Current.Session.SessionID;
            usuarios_sistema.qtacessos.value = usuarios_sistema.qtacessos.value + 1;
            usuarios_sistema.txip.value = Utils.GetIPAddress();
            usuarios_sistema.txrefresh.value = DateTime.Now;
            usuarios_sistema.Alterar();

            // Grava o código do escritório
            HttpContext.Current.Session["unidade"] = unidade;

            // Exclui os frames cadastrados do usuário
            new Usuarios_FramesDB().Excluir(usuario.idusuario.value);

            // Grava log de acesso
            Log_Acesso log = new Log_Acesso();
            log.idusuario.value = usuario.idusuario.value;
            log.tplog.value = "E";
            log.Gravar();
        }
    }
}