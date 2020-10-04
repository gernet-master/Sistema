using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using Sistema.Assets.Functions;
using System.Collections.Generic;
using System.Web;

namespace Sistema.Models
{
    public class AplicativosView
    {
        public int id { get; set; }
        public int id2 { get; set; }
        public Aplicativos aplicativo { get; set; }

        public AplicativosView(int id = 0, int id2 = 0)
        {
            // Identifcadores
            this.id = id;
            this.id2 = id2;

            // Busca registro principal
            this.aplicativo = new AplicativosDB().Buscar(id);

            // Grava o nome do identifcador principal
            HttpContext.Current.Session["ident_name"] = aplicativo.txaplicativo.value;
            HttpContext.Current.Session["ident_code"] = aplicativo.idaplicativo.value;
        }
    }
}