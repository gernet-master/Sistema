/*
Descrição: Classe de usuários
Data: 01/01/2021 - v.1.0
*/

using Sistema.Assets.DB;
using Functions;
using System;

namespace Sistema.Assets.Entities
{

    // Usuários do sistema
    public class Auditoria : Audit
    {

        // Variáveis
        public int idauditoria { get; set; }
        public DateTime dtauditoria { get; set; }
        public int idusuario { get; set; }
        public string txlog { get; set; }
        public string txoperacao { get; set; }
        public string txip { get; set; }
        public int idtabela { get; set; }
        public int ididentificador { get; set; }

        // Inicial
        public Auditoria()
        {
            this.idauditoria = 0;
            this.dtauditoria = DateTime.Now;
            this.idusuario = 0;
            this.txlog = "";
            this.txoperacao = "";
            this.txip = "";
            this.idtabela = 0;
            this.ididentificador = 0;
        }

        // Gravar
        public void Gravar()
        {
            new AuditoriaDB().Gravar(this);
        }
    }
}