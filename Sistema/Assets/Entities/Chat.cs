/*
Descrição: Classe do chat
Data: 01/01/2020 - v.1.0
*/

using Functions;
using Sistema.Assets.DB;
using Sistema.Assets.Functions;
using System;

namespace Sistema.Assets.Entities
{
    // Mensagens do chat
    public class ChatMsg : Audit
    {

        // Variáveis
        public Variable idmensagem = new Variable(config: "123|0");
        public Variable idremetente = new Variable(config: "117|1|usuarios,idusuario,txusuario");
        public Variable iddestinatario = new Variable(config: "118|1|usuarios,idusuario,txusuario");
        public Variable txmensagem = new Variable(config: "119|1");
        public Variable dtmensagem = new Variable(config: "120|1");
        public Variable dtrecebido = new Variable(config: "121|1");
        public Variable dtlido = new Variable(config: "122|1");

        // Inicial
        public ChatMsg()
        {
            this.idmensagem.value = 0;
            this.idremetente.value = 0;
            this.iddestinatario.value = 0;
            this.txmensagem.value = "";
            this.dtmensagem.value = null;
            this.dtrecebido.value = null;
            this.dtlido.value = null;
        }

        // Gravar
        public void Gravar()
        {
            new ChatDB().Gravar(this);
        }

    }

    // Usuários do chat
    public class ChatUser : Audit
    {
        public int idusuario { get; set; }
        public string txnome { get; set; }
        public int qtnaolidas { get; set; }
        public string txfoto { get; set; }
        public string idsession { get; set; }
        public int flstatuschat { get; set; }
        public ChatMsg mensagem { get; set; }
    }
}