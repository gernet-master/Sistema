/*
Descrição: Classe con a configuração do cliente
Data: 01/01/2021 - v.1.0
*/

using Functions;

namespace Sistema.Assets.Entities
{
    // Configuração do Cliente
    public class Gernet_Controle : Audit
    {
        // Variáveis
        public Variable idgernet = new Variable(config: "0|0");
        public Variable txlink = new Variable(config: "0|0");
        public Variable txcliente = new Variable(config: "0|0");
        public Variable qtunidades = new Variable(config: "0|0");
        public Variable idunidadeprincipal = new Variable(config: "0|0");
        public Variable txidioma = new Variable(config: "0|0");

        // Inicial
        public Gernet_Controle()
        {
            this.idgernet.value = 0;
            this.txlink.value = "";
            this.txcliente.value = "";
            this.qtunidades.value = 0;
            this.idunidadeprincipal.value = 0;
            this.txidioma.value = "pt-br";

        }
    }

    // Class utilizada para gerar listas
    public class Select_List
    {
        public Variable ident = new Variable(config: "0|0");
        public Variable text = new Variable(config: "0|0");

        // Inicial
        public Select_List()
        {
            this.ident.value = 0;
            this.text.value = "";
        }

        public Variable GetField(string field = "")
        {
            dynamic r = null;
            switch (field)
            {
                case "ident": r = this.ident; break;
                case "text": r = this.text; break;
            }
            return r;
        }
    }

}