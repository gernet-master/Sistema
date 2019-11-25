using Sistema.Assets.DB;
using Sistema.Assets.Entities;
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

    // Lista de usuários do chat
    public class PartialsView_ChatList
    {
        public List<ChatUser> usuarios = null;

        public PartialsView_ChatList()
        {
            usuarios = new ChatDB().ListarUsuarios();
        }
    }
}