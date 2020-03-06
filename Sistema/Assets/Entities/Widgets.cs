/*
Descrição: Classe de controle de widgets
Data: 01/01/2021 - v.1.0
*/

using Sistema.Assets.DB;
using Sistema.Assets.Functions;

namespace Sistema.Assets.Entities
{

    // Widgets
    public class Widgets
    {

        // Variáveis
        public int xpos { get; set; }
        public int ypos { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int min_width { get; set; }
        public int min_height { get; set; }
        public bool resize { get; set; }
        public string app { get; set; }

        // Inicial
        public Widgets()
        {
            this.xpos = 0;
            this.ypos = 0;
            this.width = 0;
            this.height = 0;
            this.min_width = 0;
            this.min_height = 0;
            this.resize = false;
            this.app = "";
        }

        public Widgets(int xpos, int ypos, int width, int height, int min_width, int min_height, bool resize, string app)
        {
            this.xpos = xpos;
            this.ypos = ypos;
            this.width = width;
            this.height = height;
            this.min_width = min_width;
            this.min_height = min_height;
            this.resize = resize;
            this.app = app;
        }
    }

    // Listagem
    public class WidgetsListagem
    {
        // Variáveis
        public int qtde { get; set; }
        public string colunas { get; set; }
        public string exibe { get; set; }

        // Inicial
        public WidgetsListagem()
        {
            this.qtde = 0;
            this.colunas = "";
            this.exibe = "";
        }

        public WidgetsListagem(int qtde, string colunas, string exibe)
        {
            this.qtde = qtde;
            this.colunas = colunas;
            this.exibe = exibe;
        }
    }
}