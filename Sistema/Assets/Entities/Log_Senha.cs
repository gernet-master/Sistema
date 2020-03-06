/*
Descrição: Classe do log de alteração de senha
Data: 01/01/2021 - v.1.0
*/

using Functions;
using Sistema.Assets.DB;
using Sistema.Assets.Functions;
using System;

namespace Sistema.Assets.Entities
{
    // Log de alteração de senha
    public class Log_Senha : Audit
    {

        // Variáveis
        public Variable idusuario = new Variable(config: "29|1|usuarios,idusuario,txusuario");
        public Variable dtalteracao = new Variable(config: "96|1");

        // Inicial
        public Log_Senha()
        {
            this.idusuario.value = 0;
            this.dtalteracao.value = DateTime.Now;
        }

        // Gravar
        public void Gravar()
        {
            new Log_SenhaDB().Gravar(this);
        }

    }
}