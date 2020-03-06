/*
Descrição: Classe dos frames vinculados ao usuário
Data: 01/01/2021 - v.1.0
*/

using Sistema.Assets.Functions;

namespace Sistema.Assets.Entities
{

    // Aplicativos vinculados aos frames do usuário
    public class Usuarios_Frames : Audit
    {

        // Variáveis
        public Variable idusuario = new Variable(config: "0|0");
        public Variable idunidade = new Variable(config: "0|0");
        public Variable idframe = new Variable(config: "0|0");
        public Variable txaplicativo = new Variable(config: "0|0");
        public Variable txaplicativomain = new Variable(config: "0|0");
        public Variable txtitulo = new Variable(config: "0|0");
        public Variable txcanvas = new Variable(config: "0|0");

        // Inicial
        public Usuarios_Frames()
        {
            this.idusuario.value = 0;
            this.idunidade.value = 0;
            this.idframe.value = 0;
            this.txaplicativo.value = "";
            this.txaplicativomain.value = "";
            this.txtitulo.value = "";
            this.txcanvas.value = "";
        }

    }
}