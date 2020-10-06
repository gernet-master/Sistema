/*
Descrição: Classe de menus do sistema
Data: 01/01/2021 - v.1.0
*/

using Functions;
using Sistema.Assets.DB;

namespace Sistema.Assets.Entities
{
    // Menus do sistema
    public class Menus
    {
        // Variáveis
        public Variable idmenu = new Variable(config: "244|0");
        public Variable idmenupai = new Variable(config: "245|1");
        public Variable idcodigoidioma = new Variable(config: "145|1");
        public Variable idaplicativo = new Variable(config: "229|1|aplicativos,idaplicativo,txaplicativo");
        public Variable txicone = new Variable(config: "247|1");
        public Variable nrordem = new Variable(config: "246|1");
        public Variable flativo = new Variable(config: "33|1");
        public Variable flmaster = new Variable(config: "34|1");
        public Variable menupai = new Variable();
        public Variable menu_nome = new Variable();
        public Variable menupai_nome = new Variable();
        public Variable aplicativo = new Variable();
        public Variable menus_filhos = new Variable();

        // Inicial
        public Menus()
        {
            this.idmenu.value = 0;
            this.idmenupai.value = null;
            this.idcodigoidioma.value = null;
            this.idaplicativo.value = null;
            this.txicone.value = "";
            this.nrordem.value = null;
            this.flativo.value = 1;
            this.flmaster.value = 0;
        }

        // Gravar
        public int Gravar()
        {
            return new MenusDB().Gravar(this);
        }

        // Alterar
        public void Alterar(Menus temp)
        {
            new MenusDB().Alterar(this, temp);
        }

        // Excluir
        public void Excluir()
        {
            new MenusDB().Excluir(this);
        }

        // Função para retornar o campo correto
        public Variable GetField(string field = "")
        {
            dynamic r = null;
            switch (field)
            {
                case "idmenu": r = this.idmenu; break;
                case "idmenupai": r = this.idmenupai; break;
                case "idcodigoidioma": r = this.idcodigoidioma; break;
                case "idaplicativo": r = this.idaplicativo; break;
                case "txicone": r = this.txicone; break;
                case "nrordem": r = this.nrordem; break;
                case "flativo": r = this.flativo; break;
                case "flmaster": r = this.flmaster; break;
                case "menupai": r = this.menupai; break;
                case "menu_nome": r = this.menu_nome; break;
                case "menupai_nome": r = this.menupai_nome; break;
                case "aplicativo": r = this.aplicativo; break;
            }
            return r;
        }
    }
}