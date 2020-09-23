using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using Sistema.Assets.Functions;
using System.Collections.Generic;

namespace Sistema.Models
{
    // Opções
    public class WidgetsView_Options
    {
        public string id { get; set; }

        public WidgetsView_Options(string id)
        {
            this.id = id;
        }
    }

    // Novo Registro
    public class WidgetsView_New
    {
        public Widgets widget { get; set; }
        public string texto { get; set; }

        public WidgetsView_New(Widgets widget, string texto)
        {
            this.widget = widget;
            this.texto = texto;
        }
    }

    // Listagem
    public class WidgetsView_List
    {
        public Widgets widget { get; set; }
        public string texto { get; set; }
        public dynamic result { get; set; }

        public WidgetsView_List(Widgets widget, string texto, dynamic result)
        {
            this.widget = widget;
            this.texto = texto;
            this.result = result;
        }

        public WidgetsView_List(dynamic result)
        {
            this.result = result;
        }
    }
}
