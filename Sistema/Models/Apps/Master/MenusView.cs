using Functions;
using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using Sistema.Assets.Functions;
using System;
using System.Collections.Generic;
using System.Web;

namespace Sistema.Models
{
    public class MenusView
    {
        public int id { get; set; }
        public int id2 { get; set; }
        public Menus menu { get; set; }

        public MenusView(int id = 0, int id2 = 0)
        {
            // Identifcadores
            this.id = id;
            this.id2 = id2;

            // Busca registro principal
            this.menu = new MenusDB().Buscar(id);

            // Grava o nome do identifcador principal
            HttpContext.Current.Session["ident_name"] = Language.XmlLang(Convert.ToInt32(Utils.Null(Convert.ToString(menu.idcodigoidioma.value), "0")), 2).Text;
            HttpContext.Current.Session["ident_code"] = menu.idmenu.value;
        }
    }
}