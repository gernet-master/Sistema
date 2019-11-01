using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema.Assets.Functions
{
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

    public class Audit
    {
        // Recebe os objetos e compara as alterações para gravar no banco de dados
        public void FormatString(string text = "", int format = 0)
        {

        }
    }
}
