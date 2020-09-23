/*
Descrição: Classe do log de acesso ao sistema
Data: 01/01/2021 - v.1.0
*/

using Functions;
using Sistema.Assets.DB;
using System;

namespace Sistema.Assets.Entities
{
    // Log de acesso ao sistema
    public class Log_Acesso : Audit
    {

        // Variáveis
        public Variable idlog = new Variable(config: "0|0");
        public Variable idusuario = new Variable(config: "0|0");
        public Variable dtlog = new Variable(config: "0|0");
        public Variable tplog = new Variable(config: "0|0");
        public Variable txip = new Variable(config: "0|0");

        // Inicial
        public Log_Acesso()
        {
            this.idlog.value = 0;
            this.idusuario.value = 0;
            this.dtlog.value = DateTime.Now;
            this.tplog.value = "";
            this.txip.value = Utils.GetIPAddress(); 
        }

        // Gravar
        public void Gravar()
        {
            new Log_AcessoDB().Gravar(this);
        }

    }
}