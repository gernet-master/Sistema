using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using Sistema.Assets.Functions;
using System.Collections.Generic;
using System.Web;

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

            // Grava o nome e identificador do registro
            HttpContext.Current.Session["ident_name"] = tabela.txtabela.value;
            HttpContext.Current.Session["ident_code"] = tabela.idtabela.value;

            // Lista todas as tabelas que ainda não foram cadastradas
            this.tabelaN = new TabelasDB().ListarNaoCadastradas();
        }
    }
}