/*
Descrição: Classe de tabelas do sistema
Data: 01/01/2020 - v.1.0
*/

using Sistema.Assets.DB;
using Sistema.Assets.Functions;

namespace Sistema.Assets.Entities
{

    // Tabelas do sistema
    public class Tabelas : Audit
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
            this.flauditoria.value = false;
            this.idcodigoidioma.value = null;
        }

        // Gravar
        public int Gravar()
        {
            return new TabelasDB().Gravar(this);
        }

        // Alterar
        public void Alterar()
        {
            new TabelasDB().Alterar(this);
        }
    }
}