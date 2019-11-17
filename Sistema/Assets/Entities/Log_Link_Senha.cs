/*
Descrição: Classe do log de envio de link para alteração de senha
Data: 01/01/2020 - v.1.0
*/

using Functions;
using Sistema.Assets.DB;
using Sistema.Assets.Functions;
using System;

namespace Sistema.Assets.Entities
{
    // Log de envio de link para alteração de senha
    public class Log_Link_Senha : Audit
    {

        // Variáveis
        public Variable idlog = new Variable(config: "0|0");
        public Variable idusuario = new Variable(config: "0|0");
        public Variable dtlink = new Variable(config: "0|0");
        public Variable txchave = new Variable(config: "0|0");
        public Variable flutilizado = new Variable(config: "0|0");

        // Inicial
        public Log_Link_Senha()
        {
            this.idlog.value = 0;
            this.idusuario.value = 0;
            this.dtlink.value = DateTime.Now;
            this.txchave.value = "";
            this.flutilizado.value = 0; 
        }

        // Gravar
        public void Gravar()
        {
            new Log_Link_SenhaoDB().Gravar(this);
        }

    }
}