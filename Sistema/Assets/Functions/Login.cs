﻿/*
Descrição: Validação dos dados de acesso ao sistema
Data: 01/01/2021 - v.1.0
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
        public static Retorno LoginCheck(string user = "", string password = "", int remember = 0)
        {
            // Retorno
            Retorno result = new Retorno();

            // Verifica se o usuário está dentro do padrão
            if ((user == "") || (user.Length > 20))
            {
                result.success = 0;
                result.msg = Language.XmlLang(46, 2).Text;
                return result;
            }

            // Verifica se a senha está dentro do padrão
            if ((password == "") || (password.Length > 20))
            {
                result.success = 0;
                result.msg = Language.XmlLang(46, 2).Text;
                return result;
            }

            // Verifica se possui caracteres inválidos para senha
            if ((password.IndexOf(" ") >= 0) || (password.IndexOf("<") >= 0) || (password.IndexOf(">") >= 0) || (password.IndexOf("'") >= 0))
            {
                result.success = 0;
                result.msg = Language.XmlLang(46, 2).Text;
                return result;
            }

            // Codifica a senha
            password = Crypt.CreateHash(password);

            // Valida os dados
            Usuarios usuario = new UsuariosDB().Login(user, password, 0);

            // Dados incorretos
            if (usuario == null)
            {

                // Verifica se o usuário existe para o cliente no sistema
                int idusuario = new UsuariosDB().Id(user);
                if (idusuario > 0)
                {

                    // Total de vezes o usuário errou a senha nos últimos 5 minutos
                    int erro = new Log_AcessoDB().ErrouSenha(idusuario);

                    // Mais de 5 erros
                    if (erro > 5)
                    {
                        // Exibe mensagem de usuário bloqueado e a data para desbloqueio
                        result.success = 0;
                        result.msg = Language.XmlLang(69, 2).Text + "<br><br>" + Language.XmlLang(70, 2).Text + ": " + DateTime.Now.AddMinutes(10);
                        return result;
                    }

                    // 5º erro
                    else if (erro == 5)
                    {
                        // Grava log de acesso
                        Log_Acesso log = new Log_Acesso();
                        log.idusuario.value = idusuario;
                        log.tplog.value = "R";
                        log.Gravar();

                        // Pega os dados de controle do usuário
                        Usuarios_Sistema usuarios_sistema = new Usuarios_SistemaDB().Buscar(idusuario);

                        // Grava tempo de bloqueio
                        if (usuarios_sistema == null)
                        {
                            usuarios_sistema = new Usuarios_Sistema();
                            usuarios_sistema.idusuario.value = idusuario;
                            usuarios_sistema.txbloqueado.value = DateTime.Now;
                            usuarios_sistema.Gravar();
                        }
                        else
                        {
                            usuarios_sistema.txbloqueado.value = DateTime.Now;
                            usuarios_sistema.Alterar();
                        }

                        // Exibe mensagem de usuário bloqueado
                        result.success = 0;
                        result.msg = Language.XmlLang(71, 2).Text + ".<br><br>" + Language.XmlLang(72, 2).Text + " " + erro + " " + Language.XmlLang(5, 0).Text + " 5.<br><br>" + Language.XmlLang(73, 2).Text;
                        return result;
                    }

                    // 1º ao 4º erro
                    else
                    {
                        // Grava log de acesso
                        Log_Acesso log = new Log_Acesso();
                        log.idusuario.value = idusuario;
                        log.tplog.value = "R";
                        log.Gravar();

                        // Exibe aviso
                        result.success = 0;
                        result.msg = Language.XmlLang(71, 2).Text + ".<br><br>" + Language.XmlLang(72, 2).Text + " " + erro + " " + Language.XmlLang(5, 0).Text + " 5.<br><br>" + Language.XmlLang(74, 2).Text;
                        return result;
                    }                      

                }
            }

            // Dados corretos
            else
            {
                // Pega a data de bloqueio se existir
                Usuarios_Sistema usuarios_sistema = new Usuarios_SistemaDB().Buscar(usuario.idusuario.value);
                if (usuarios_sistema != null)
                {
                    if (usuarios_sistema != null)
                    {
                        // Segundos entre a data atual e a data de bloqueio
                        var seconds = System.Math.Abs((Convert.ToDateTime(usuarios_sistema.txbloqueado.value) - DateTime.Now).TotalSeconds);

                        // Tempo de bloqueio for menor que 10 minutos
                        if (seconds < 600) {

                            // Exibe mensagem de usuário bloqueado e a data para desbloqueio
                            result.success = 0;
                            result.msg = Language.XmlLang(69, 2).Text + "<br><br>" + Language.XmlLang(70, 2).Text + ": " + DateTime.Now.AddMinutes(10);
                            return result;
                        }
                    }
                }

                // Verifica se usuário está ativo
                if (!Convert.ToBoolean(usuario.flativo.value))
                {
                    result.success = 0;
                    result.msg = Language.XmlLang(47, 2).Text;
                    return result;
                }

                // Pega a última sessionid
                string session_id = new Usuarios_SistemaDB().SessionId(usuario.idusuario.value);

                // Verifica se o usuário já está logado
                if ((session_id != "") && (Convert.ToString(HttpContext.Current.Application["sessions"]).IndexOf(session_id) >= 0))
                {

                    // Armazena em variáveis de sessão
                    HttpContext.Current.Session["usuario"] = usuario.idusuario.value;

                    // Retorna mensagem de confirmação
                    result.success = 3;
                    return result;
                }

                // Verifica se o usuário é forçado a alterar a senha
                if (Convert.ToBoolean(usuario.flalterasenha.value))
                {
                    // Armazena em variáveis de sessão
                    HttpContext.Current.Session["usuario"] = usuario.idusuario.value;
                    HttpContext.Current.Session["nome"] = usuario.txnome.value;
                    HttpContext.Current.Session["cookie"] = remember;

                    // Redireciona para tela de alteração
                    result.success = 1;
                    return result;
                }                

                // Executa as rotinas de login
                RotinaLogin(usuario, usuarios_sistema, remember);

                // Redireciona para home
                result.success = 2;
                return result;
            }
            return result;
        }

        // Envia link para recuperação de senha
        public static Retorno RecoverPassword(string user = "", string email = "")
        {
            // Retorno
            Retorno result = new Retorno();

            // Verifica se o usuário está dentro do padrão
            if ((user == "") || (user.Length > 20))
            {
                result.success = 0;
                result.msg = Language.XmlLang(46, 2).Text;
                return result;
            }

            // Verifica se o email está dentro do padrão
            if ((email == "") || (email.Length > 100))
            {
                result.success = 0;
                result.msg = Language.XmlLang(46, 2).Text;
                return result;
            }

            // Valida os dados
            int idusuario = new UsuariosDB().ValidaUsuarioEmail(user, email);

            // Se retornou 0, dados inválidos
            if (idusuario == 0)
            {
                result.success = 0;
                result.msg = Language.XmlLang(46, 2).Text;
                return result;
            }
            else
            {
                // Verifica se já foi enviado link no período de 24h
                if (new Log_Link_SenhaDB().LinkSenha(idusuario))
                {
                    result.success = 0;
                    result.msg = Language.XmlLang(81, 2).Text;
                    return result;
                }
                else
                {
                    // Busca os dados do cliente
                    Gernet_Controle gernet_controle = new Gernet_ControleDB().Buscar();

                    if (gernet_controle == null)
                    {
                        result.success = 0;
                        result.msg = Language.XmlLang(84, 2).Text;
                        return result;
                    }
                    else
                    {
                        // Monta titulo de email
                        string title = Language.XmlLang(82, 1).Text + " " + Utils.FormatString(gernet_controle.txcliente.value, 1) + " - " + Language.XmlLang(83, 2).Text;

                        // Monta mensagem
                        string link_key = Crypt.CreateHash(DateTime.Now.ToLongDateString());
                        string link_password = "https://" + gernet_controle.txlink.value + "/resetPassword/" + link_key;
                        string message = "<strong>" + Language.XmlLang(85, 1).Text + "!</strong><Br><Br>";
                        message += Language.XmlLang(86, 2).Text + ".<Br><br>";
                        message += Language.XmlLang(87, 2).Text + ", <a href='" + link_password + "'>" + Language.XmlLang(88, 1).Text + "!</a> " + Language.XmlLang(89, 0).Text + ": " + link_password + "<Br>";
                        message += Language.XmlLang(90, 2).Text + ".<Br><Br>";
                        message += Language.XmlLang(91, 2).Text + ".";

                        // Envia e-mail
                        if (Mail.EnviaEmail(email, title, message))
                        {
                            // Grava o link para controle de utilização
                            Log_Link_Senha log_link_senha = new Log_Link_Senha();
                            log_link_senha.idusuario.value = idusuario;
                            log_link_senha.txchave.value = link_key;
                            log_link_senha.Gravar();

                            result.success = 1;
                            result.msg = Language.XmlLang(92, 2).Text;
                            return result;
                        }
                        else
                        {
                            result.success = 0;
                            result.msg = Language.XmlLang(84, 2).Text;
                            return result;
                        }
                        
                    }

                }
            }
        }

        // Altera a senha do usuário
        public static Retorno ChangePassword(string password_atual = "", string password_novo = "")
        {
            // Retorno
            Retorno result = new Retorno();

            // Verifica se a senha está dentro do padrão
            if ((password_atual.Length < 8) || (password_atual.Length > 20))
            {
                result.success = 0;
                result.msg = Language.XmlLang(46, 2).Text;
                return result;
            }

            // Verifica se a nova senha está dentro do padrão
            if ((password_novo.Length < 8) || (password_novo.Length > 20))
            {
                result.success = 0;
                result.msg = Language.XmlLang(46, 2).Text;
                return result;
            }

            // Verifica se a nova senha é igual a antiga
            if (password_atual == password_novo)
            {
                result.success = 0;
                result.msg = Language.XmlLang(67, 2).Text;
                return result;
            }

            // Verifica se possui caracteres inválidos para senha
            if ((password_novo.IndexOf(" ") >= 0) || (password_novo.IndexOf("<") >= 0) || (password_novo.IndexOf(">") >= 0) || (password_novo.IndexOf("'") >= 0))
            {
                result.success = 0;
                result.msg = Language.XmlLang(67, 2).Text;
                return result;
            }

            // Codifica a senha atual e nova senha
            password_atual = Crypt.CreateHash(password_atual);
            password_novo = Crypt.CreateHash(password_novo);

            // Valida os dados
            Usuarios usuario = new UsuariosDB().Login("", password_atual, Convert.ToInt32(HttpContext.Current.Session["usuario"]));

            // Dados incorretos
            if (usuario == null)
            {
                result.success = 0;
                result.msg = Language.XmlLang(46, 2).Text;
                return result;
            }

            // Dados corretos
            else
            {
                result.success = 1;
                usuario.flalterasenha.value = 0;
                usuario.txsenha.value = password_novo;
                usuario.Alterar();

                // Pega os dados de controle do usuário
                Usuarios_Sistema usuarios_sistema = new Usuarios_SistemaDB().Buscar(usuario.idusuario.value);

                // Executa as rotinas de login
                RotinaLogin(usuario, usuarios_sistema, Convert.ToInt32(HttpContext.Current.Session["cookie"]));

                // Redireciona para home
                return result;
            }            
        }

        // Rotinas de login
        public static void RotinaLogin(Usuarios usuario, Usuarios_Sistema usuarios_sistema, int remember)
        {
            // Pega preferencias do usuário
            Usuarios_Preferencias usuarios_preferencias = new Usuarios_PreferenciasDB().PreferenciasUsuario(usuario.idusuario.value);

            // Se não existir preferencias, cria com base na configuração do cliente
            if (usuarios_preferencias == null)
            {
                Gernet_Controle gernet_controle = new Gernet_ControleDB().Buscar();

                Usuarios_Preferencias cliente_preferencias = new Usuarios_Preferencias();
                cliente_preferencias.idusuario.value = usuario.idusuario.value;
                cliente_preferencias.idunidade.value = gernet_controle.idunidadeprincipal.value;
                cliente_preferencias.txidioma.value = gernet_controle.txidioma.value;
                cliente_preferencias.Gravar();

                usuarios_preferencias = cliente_preferencias;
            }

            // Insere a sessão do usuário na aplicação se não existir
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["contusr"] = Convert.ToInt32(HttpContext.Current.Application["contusr"]) + 1;
            HttpContext.Current.Application["sessions"] = Convert.ToString(HttpContext.Current.Application["sessions"]) + HttpContext.Current.Session.SessionID + ",";
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
                    cookie.SameSite = SameSiteMode.None;
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
            else
            {
                unidade = Convert.ToInt32(usuarios_preferencias.idunidade.value);
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

            // Grava log de acesso
            Log_Acesso log = new Log_Acesso();
            log.idusuario.value = usuario.idusuario.value;
            log.tplog.value = "E";
            log.Gravar();
        }
    }
}