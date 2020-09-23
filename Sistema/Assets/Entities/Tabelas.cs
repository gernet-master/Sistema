/*
Descrição: Classe de tabelas do sistema
Data: 01/01/2021 - v.1.0
*/

using Functions;
using Sistema.Assets.DB;

namespace Sistema.Assets.Entities
{
    // Tabelas do sistema
    public class Tabelas
    {
        // Variáveis
        public Variable idtabela = new Variable(config: "143|0");
        public Variable txtabela = new Variable(config: "144|1");
        public Variable flauditoria = new Variable(config: "141|1");
        public Variable idcodigoidioma = new Variable(config: "145|1");

        // Inicial
        public Tabelas()
        {
            this.idtabela.value = 0;
            this.txtabela.value = "";
            this.flauditoria.value = 0;
            this.idcodigoidioma.value = null;
        }

        // Gravar
        public int Gravar()
        {
            return new TabelasDB().Gravar(this);
        }

        // Alterar
        public void Alterar(Tabelas temp)
        {
            new TabelasDB().Alterar(this, temp);
        }

        // Excluir
        public void Excluir()
        {
            new TabelasDB().Excluir(this);
        }

        // Função para retornar o campo correto
        public Variable GetField(string field = "")
        {
            dynamic r = null;
            switch (field)
            {
                case "idtabela": r = this.idtabela; break;
                case "txtabela": r = this.txtabela; break;
                case "flauditoria": r = this.flauditoria; break;
                case "idcodigoidioma": r = this.idcodigoidioma; break;
            }
            return r;
        }
    }
}