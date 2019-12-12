using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using Sistema.Assets.Functions;
using System.Collections.Generic;

namespace Sistema.Models
{
    // Breadcrumb
    public class PartialsView_Subheader
    {
        public string app { get; set; }
        public int id { get; set; }
        public int id2 { get; set; }

        public PartialsView_Subheader(string app, int id, int id2)
        {
            this.app = app;
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
}