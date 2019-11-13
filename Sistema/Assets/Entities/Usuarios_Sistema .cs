/*
Descrição: Classe do controle de usuário no sistema
Data: 01/01/2020 - v.1.0
*/

using Sistema.Assets.DB;
using Sistema.Assets.Functions;

namespace Sistema.Assets.Entities
{

    // Controle de usuários do sistema
    public class Usuarios_Sistema : Audit
    {

        // Variáveis
        public Variable idusuario = new Variable(config: "0|0");
        public Variable idunidade = new Variable(config: "0|0");
        public Variable flderrubar = new Variable(config: "0|0");
        public Variable idsession = new Variable(config: "0|0");
        public Variable qtacessos = new Variable(config: "0|0");
        public Variable txip = new Variable(config: "0|0");
        public Variable txrefresh = new Variable(config: "0|0");
        public Variable txderrubar = new Variable(config: "0|0");
        public Variable txaplicativo = new Variable(config: "0|0");
        public Variable txaplicativomain = new Variable(config: "0|0");
        public Variable txbloqueado = new Variable(config: "0|0");

        // Inicial
        public Usuarios_Sistema()
        {
            this.idusuario.value = 0;
            this.idunidade.value = 0;
            this.flderrubar.value = 0;
            this.idsession.value = "";
            this.qtacessos.value = 0;
            this.txip.value = "";
            this.txrefresh.value = null;
            this.txderrubar.value = null;
            this.txaplicativo.value = "";
            this.txaplicativomain.value = "";
            this.txbloqueado.value = null;
        }

        // Gravar
        public void Gravar()
        {
            new Usuarios_SistemaDB().Gravar(this);
        }

        // Alterar
        public void Alterar()
        {
            new Usuarios_SistemaDB().Alterar(this);
        }
    }
}