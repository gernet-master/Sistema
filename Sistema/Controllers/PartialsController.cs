/*
Descrição: Controlador de includes internos do sistema
Data: 01/01/2021 - v.1.0
*/

using Sistema.Assets.DB;
using Sistema.Models;
using System;
using System.Globalization;
using System.Web.Mvc;
using Functions;

namespace Sistema.Controllers
{
    public class PartialsController : Controller
    {
        // Cabeçalho para mobile
        [Autentication]
        public ActionResult HeaderMobile()
        {
            return PartialView();
        }

        // Cabeçalho
        [Autentication]
        public ActionResult Header()
        {
            return PartialView();
        }

        // Menu
        [Autentication]
        public ActionResult Menu()
        {
            return PartialView();
        }

        // Aplicativo
        [Autentication]
        public ActionResult App(string controller = "", string action = "", int id = 0, int id2 = 0)
        {
            // Verifica se foi passado controller e action
            if ((controller != "") && (action != ""))
            {
                // Grava a pagina atual na session para utilizar caso seja feito refresh da página
                Session["current_page"] = controller + "|" + action + "|" + id + "|" + id2;

                // Redireciona para app
                return PartialView(new PartialsView_AppView(controller, action, id, id2));

            }
            // Redireciona para dashboard principal
            else
            {
                return PartialView(new PartialsView_AppView("Home", "Dashboard", id, id2));
            }
        }

        // Rodapé
        [Autentication]
        public ActionResult Footer()
        {
            return PartialView();
        }

        // Calculadora
        [Autentication]
        public ActionResult Calculator()
        {
            return PartialView();
        }

        // ***************** CEP *****************

        // Pesquisa CEP
        [Autentication]
        public ActionResult Cep()
        {
            return PartialView();
        }

        // ***************** CHAT *****************

        // Icone
        [Autentication]
        public ActionResult Chat()
        {
            return PartialView();
        }

        // Painel do chat
        [Autentication]
        public ActionResult ChatPanel()
        {
            return PartialView(new PartialsView_ChatPanel());
        }

        // Tela de mensagem do chat
        [Autentication]
        public ActionResult ChatMsg()
        {
            return PartialView();
        }

        // Lista de usuários do chat
        [Autentication]
        public ActionResult ChatPanelList(int online = 1, int offline = 0, string search = "")
        {
            return PartialView(new PartialsView_ChatPanelList(online, offline, search));
        }

        // Grava o novo status de chat do usuário
        [Autentication]
        public void ChatPanelChangeStatus(int status = 0)
        {
            new Usuarios_SistemaDB().AlterarStatusChat(status);
        }

        // Grava a nova configuração de privacidade
        [Autentication]
        public void ChatPanelChangePrivacy(int privacy = 0)
        {
            new Usuarios_SistemaDB().AlterarPrivacidadeChat(privacy);
        }

        // Grava a nova configuração de mensagem
        [Autentication]
        public void ChatPanelChatMsg(int config = 0)
        {
            new Usuarios_SistemaDB().AlterarConfigMsgChat(config);
        }

        // Lista as mensagens
        [Autentication]
        public ActionResult ChatPanelListMsgs(int id = 0)
        {
            return PartialView(new PartialsView_ChatPanelListMsgs(id));
        }

        // ***************** E-MAIL *****************

        [Autentication]
        public ActionResult Email()
        {
            return PartialView();
        }

        [Autentication]
        public ActionResult Whatsapp()
        {
            return PartialView();
        }

        [Autentication]
        public ActionResult ScrollTop()
        {
            return PartialView();
        }

        [Autentication]
        public ActionResult Modal()
        {
            return PartialView();
        }

        [Autentication]
        public ActionResult Search()
        {
            return PartialView();
        }

        [Autentication]
        public ActionResult Notifications()
        {
            return PartialView();
        }

        [Autentication]
        public ActionResult QuickActions()
        {
            return PartialView();
        }        

        // Lista de idiomas do sistema
        [Autentication]
        public ActionResult Languages()
        {
            return PartialView();
        }

        // Menu de ações do usuário
        [Autentication]
        public ActionResult UserPanel()
        {
            return PartialView();
        }

        // Retorna a hora atual do servidor
        [Autentication]
        public string Clock()
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            int day = DateTime.Now.Day;
            int year = DateTime.Now.Year;
            string month = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
            string weekday = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
            string date = weekday + ", " + day + " " + Language.XmlLang(5, 0).Text + " " + month + " " + Language.XmlLang(5, 0).Text + " " + year + "<br>" + DateTime.Now.ToString("HH:mm:ss");
            return date;
        }

    }
}