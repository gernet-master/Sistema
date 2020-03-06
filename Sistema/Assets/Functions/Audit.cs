/*
Descrição: Rotinas para gravar a auditoria do sistema
Data: 01/01/2021 - v.1.0
*/

using System;

namespace Sistema.Assets.Functions
{
    // Armazena o valor e a configuração
    public class Variable
    {
        public dynamic value { get; set; }
        public string config { get; set; }

        public Variable(dynamic value = null, string config = "")
        {
            this.value = value;
            this.config = config;
        }
    }

    // Auditoria
    public class Audit
    {
        // Recebe os objetos e compara as alterações para gravar no banco de dados
        public void FormatString(string text = "", int format = 0)
        {

        }
    }
}
