using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using System.Collections.Generic;

namespace Sistema.Models
{
    public class UsuariosView
    {
        public List<Usuarios> usuarios { get; set; }

        public UsuariosView()
        {
            this.usuarios = new UsuariosDB().List();
        }
    }
}