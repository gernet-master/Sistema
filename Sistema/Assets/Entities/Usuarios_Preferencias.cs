/*
Descrição: Classe das preferencias do usuário
Data: 01/01/2021 - v.1.0
*/

using Sistema.Assets.DB;
using Functions;

namespace Sistema.Assets.Entities
{

    // Prefêrencias do sistema definidas pelo usuário
    public class Usuarios_Preferencias : Audit
    {

        // Variáveis
        public Variable idusuario = new Variable(config: "26|0");
        public Variable idunidade = new Variable(config: "51|1|unidades,idunidade,txunidade");
        public Variable txidioma = new Variable(config: "52|1");

        // Inicial
        public Usuarios_Preferencias()
        {
            this.idusuario.value = 0;
            this.idunidade.value = 0;
            this.txidioma.value = "";
        }

        // Gravar
        public void Gravar()
        {
            new Usuarios_PreferenciasDB().Gravar(this);
        }
    }
}