using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using System.Collections.Generic;

namespace Sistema.Models
{
    public class TabelasView
    {
        public int id { get; set; }
        public int id2 { get; set; }
        public Tabelas tabela { get; set; }
        public List<Select_List> tabelaN { get; set; }

        public TabelasView(int id = 0, int id2 = 0)
        {
            // Identifcadores
            this.id = id;
            this.id2 = id2;

            // Busca registro principal
            this.tabela = new TabelasDB().Buscar(id);

            // Lista todas as tabelas que ainda não foram cadastradas
            this.tabelaN = new TabelasDB().ListarNaoCadastradas();
        }
    }
}