/*
Descrição: Classe das configurações de dashboard por usuário
Data: 01/01/2021 - v.1.0
*/

using Sistema.Assets.DB;
using Functions;

namespace Sistema.Assets.Entities
{

    // Prefêrencias do sistema definidas pelo usuário
    public class Usuarios_Dashboard : Audit
    {

        // Variáveis
        public Variable idusuario = new Variable();
        public Variable idaplicativo = new Variable();
        public Variable fldashboard = new Variable();
        public Variable fltiporeg = new Variable();

        // Inicial
        public Usuarios_Dashboard()
        {
            this.idusuario.value = 0;
            this.idaplicativo.value = 0;
            this.fldashboard.value = 1;
            this.fltiporeg.value = "";
        }

        // Gravar
        public void Gravar()
        {
            new Usuarios_DashboardDB().Gravar(this);
        }

        // Alterar
        public void Alterar()
        {
            new Usuarios_DashboardDB().Alterar(this);
        }
    }
}