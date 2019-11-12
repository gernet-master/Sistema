using Sistema.Assets.DB;
using Sistema.Assets.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema.Assets.Entities
{

    // Prefêrencias do sistema definidas pelo usuário
    public class Usuarios_Preferencias : Audit
    {

        // Variáveis
        public Variable idusuario = new Variable(config: "26|0");
        public Variable idunidade = new Variable(config: "51|1|unidades,idunidade,txunidade");

        // Inicial
        public Usuarios_Preferencias()
        {
            this.idusuario.value = 0;
            this.idunidade.value = 0;
        }

        // Gravar
        public void Save()
        {
            new Usuarios_PreferenciasDB().Save(this);
        }
    }
}