using Functions;
using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using Sistema.Assets.Functions;
using System;
using System.Collections.Generic;
using System.Web;

namespace Sistema.Models
{
    // Menu do sistema
    public class PartialsView_MenuView : Session
    {
        public List<Menus> menus = null;

        public PartialsView_MenuView()
        {
            this.menus = new MenusDB().ListarPorNivel(0);
        }
    }

    // Aplicativo
    public class PartialsView_AppView : Session
    {
        public string controller { get; set; }
        public string action { get; set; }
        public int id { get; set; }
        public int id2 { get; set; }
        public List<Select_List> list { get; set; }
        public Usuarios_Dashboard dashboard { get; set; }
        public string register { get; set; }
        public int idcodigoidioma { get; set; }

        public PartialsView_AppView(string controller, string action, int id, int id2)
        {
            this.controller = controller;
            this.action = action;
            this.id = id;
            this.id2 = id2;
            this.register = "";

            // Crias as opções para o controle da dashboard
            this.list = new List<Select_List>();
            this.list.Add(new Select_List() { ident = new Variable(value: ""), text = new Variable(value: Language.XmlLang(24, 2).Text) });
            this.list.Add(new Select_List() { ident = new Variable(value: "F"), text = new Variable(value: Language.XmlLang(242, 2).Text) });
            this.list.Add(new Select_List() { ident = new Variable(value: "L"), text = new Variable(value: Language.XmlLang(240, 2).Text) });

            // Pega o nome do menu para montar o breadcrumb
            this.idcodigoidioma = new MenusDB().BuscarActionController("Dashboard", controller);

            // Pega as configurações da dashboard para o usuario
            this.dashboard = new Usuarios_DashboardDB().Buscar(session_usuario, new AplicativosDB().BuscarActionController("Dashboard", controller).idaplicativo.value);

            // Valida a configuração da dashboard
            if (this.dashboard != null)
            {
                // Não exibir dashboard
                if (this.dashboard.fldashboard.value == 0)
                {
                    this.action = "Incluir";
                    this.id = 0;
                    this.register = Convert.ToString(this.dashboard.fltiporeg.value).Trim();

                    // Grava a pagina atual na session para utilizar caso seja feito refresh da página
                    HttpContext.Current.Session["current_page"] = controller + "|" + this.action + "|" + this.id + "|" + id2;
                }
            }
            else
            {
                this.dashboard = new Usuarios_Dashboard();
            }
        }
    }

    // Painel do chat
    public class PartialsView_ChatPanel : Session
    {
        public Usuarios_Sistema usuarios_sistema = null;

        // Configurações do usuário
        public PartialsView_ChatPanel()
        {
            usuarios_sistema = new Usuarios_SistemaDB().Buscar(session_usuario);
        }
    }

    // Lista de usuários do chat
    public class PartialsView_ChatPanelList : Session
    {
        public List<ChatUser> usuarios = null;
        public int flonline { get; set; }
        public int floffline { get; set; }

        public PartialsView_ChatPanelList(int online = 1, int offline = 0, string search = "")
        {
            flonline = online;
            floffline = offline;

            // Busca lista de usuários e mensagens
            usuarios = new ChatDB().ListarUsuarios(search);
        }
    }

    // Lista de mensagens do chat
    public class PartialsView_ChatPanelListMsgs : Session
    {
        public int id { get; set; }
        public List<ChatMsg> msgs { get; set; }
        //public ChatDest dest { get; set; }

        public PartialsView_ChatPanelListMsgs(int id = 0)
        {
            this.id = id;
            this.msgs = new ChatDB().ListarMensagens(id);
            //this.dest = new ChatDB().DadosDestinatario(id);
        }
    }
}
