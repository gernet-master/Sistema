/*
Descrição: Classe do log de alteração de senha
Data: 01/01/2021 - v.1.0
*/

using Functions;
using Sistema.Assets.DB;
using System;

namespace Sistema.Assets.Entities
{
    // Log de alteração de senha
    public class Retorno
    {
        // Variáveis
        public int success { get; set; }
        public string msg { get; set; }
        public int ident { get; set; }
        public int ident2 { get; set; }

        // Inicial
        public Retorno()
        {
            this.success = 0;
            this.msg = "";
            this.ident = 0;
            this.ident2 = 0;
        }

    }
}