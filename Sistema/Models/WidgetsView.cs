using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using Sistema.Assets.Functions;
using System.Collections.Generic;

namespace Sistema.Models
{
    // Opções
    public class WidgetsView_Opcoes
    {
        public string id { get; set; }

        public WidgetsView_Opcoes(string id)
        {
            this.id = id;
        }
    }

    // Novo Registro
    public class WidgetsView_NovoRegistro
    {
        public Widgets widget { get; set; }
        public string texto { get; set; }

        public WidgetsView_NovoRegistro(Widgets widget, string texto)
        {
            this.widget = widget;
            this.texto = texto;
        }
    }

    // Pesquisa Rápida
    public class WidgetsView_PesquisaRapida
    {
        public Widgets widget { get; set; }
        public string texto { get; set; }

        public WidgetsView_PesquisaRapida(Widgets widget, string texto)
        {
            this.widget = widget;
            this.texto = texto;
        }
    }
}
