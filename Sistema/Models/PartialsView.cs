using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using Sistema.Assets.Functions;
using System.Collections.Generic;

namespace Sistema.Models
{
    // Aplicativo
    public class PartialsView_AppView
    {
        public string controller { get; set; }
        public string action { get; set; }
        public int id { get; set; }
        public int id2 { get; set; }

        public PartialsView_AppView(string controller, string action, int id, int id2)
        {
            this.controller = controller;
            this.action = action;
            this.id = id;
            this.id2 = id2;
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
