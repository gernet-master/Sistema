/*
Descrição: Classe do log de envio de link para alteração de senha
Data: 01/01/2021 - v.1.0
*/

using Functions;
using Sistema.Assets.DB;
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
        public Variable dtutilizado = new Variable(config: "0|0");

        // Inicial
        public Log_Link_Senha()
        {
            this.idlog.value = 0;
            this.idusuario.value = 0;
            this.dtlink.value = DateTime.Now;
            this.txchave.value = "";
            this.flutilizado.value = 0;
            this.dtutilizado = null;
        }

        // Gravar
        public void Gravar()
        {
            new Log_Link_SenhaDB().Gravar(this);
        }

        // Alterar
        public void Alterar()
        {
            new Log_Link_SenhaDB().Alterar(this);
        }

    }
}