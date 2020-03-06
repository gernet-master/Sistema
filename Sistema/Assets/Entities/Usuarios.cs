/*
Descrição: Classe de usuários
Data: 01/01/2021 - v.1.0
*/

using Sistema.Assets.DB;
using Sistema.Assets.Functions;

namespace Sistema.Assets.Entities
{

    // Usuários do sistema
    public class Usuarios : Audit
    {

        // Variáveis
        public Variable idusuario = new Variable(config: "26|0");
        public Variable idgernet = new Variable(config: "27|0");
        public Variable txnome = new Variable(config: "28|1");
        public Variable txusuario = new Variable(config: "29|1");
        public Variable txsenha = new Variable(config: "30|0");
        public Variable txemail = new Variable(config: "31|1");
        public Variable idperfil = new Variable(config: "32|1|perfis_acesso,idperfil,txperfil");
        public Variable flativo = new Variable(config: "33|1");
        public Variable flmaster = new Variable(config: "34|0");
        public Variable flalterasenha = new Variable(config: "35|1");
        public Variable txfoto = new Variable(config: "15|0");

        // Inicial
        public Usuarios()
        {
            this.idusuario.value = 0;
            this.idgernet.value = 0;
            this.txnome.value = "";
            this.txusuario.value = "";
            this.txsenha.value = "";
            this.txemail.value = "";
            this.idperfil.value = 0;
            this.flativo.value = 0;
            this.flmaster.value = 0;
            this.flalterasenha.value = 0;
            this.txfoto.value = "";
        }

        // Gravar
        public int Gravar()
        {
            return new UsuariosDB().Gravar(this);
        }

        // Alterar
        public void Alterar()
        {
            new UsuariosDB().Alterar(this);
        }
    }
}