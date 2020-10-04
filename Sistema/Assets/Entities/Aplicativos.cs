/*
Descrição: Classe de aplicativos do sistema
Data: 01/01/2021 - v.1.0
*/

using Functions;
using Sistema.Assets.DB;

namespace Sistema.Assets.Entities
{
    // Aplicativos do sistema
    public class Aplicativos
    {
        // Variáveis
        public Variable idaplicativo = new Variable(config: "229|0");
        public Variable txaplicativo = new Variable(config: "230|1");
        public Variable txaction = new Variable(config: "231|1");
        public Variable txcontroller = new Variable(config: "232|1");
        public Variable idtabela = new Variable(config: "144|1|tabelas,idtabela,txtabela");
        public Variable txtabela = new Variable();

        // Inicial
        public Aplicativos()
        {
            this.idaplicativo.value = 0;
            this.txaplicativo.value = "";
            this.txaction.value = "";
            this.txcontroller.value = "";
            this.idtabela.value = 0;
            this.txtabela.value = "";
        }

        // Gravar
        public int Gravar()
        {
            return new AplicativosDB().Gravar(this);
        }

        // Alterar
        public void Alterar(Aplicativos temp)
        {
            new AplicativosDB().Alterar(this, temp);
        }

        // Excluir
        public void Excluir()
        {
            new AplicativosDB().Excluir(this);
        }

        // Função para retornar o campo correto
        public Variable GetField(string field = "")
        {
            dynamic r = null;
            switch (field)
            {
                case "idaplicativo": r = this.idaplicativo; break;
                case "txaplicativo": r = this.txaplicativo; break;
                case "txaction": r = this.txaction; break;
                case "txcontroller": r = this.txcontroller; break;
                case "idtabela": r = this.idtabela; break;
                case "txtabela": r = this.txtabela; break;
            }
            return r;
        }
    }
}