/*
Descrição: Rotinas para gravar a auditoria do sistema
Data: 01/01/2021 - v.1.0
*/

using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using Sistema.Assets.Functions;
using System;
using System.Reflection;
using System.Web;

namespace Functions
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
        // Chama a função de auditoria correta
        public static void Check(string table, string action, int ident, dynamic obj1, dynamic obj2 = null)
        {
            // Verifica se a tabela foi marcada para possuir auditoria
            Tabelas tabela = new TabelasDB().Buscar(table);            

            // Possui auditoria
            if (tabela.flauditoria.value == 1)
            {
                // Inclusão
                if (action == "I") { Audit.Insert(tabela.idtabela.value, ident, obj1); }

                // Exclusão
                if (action == "D") { Audit.Delete(tabela.idtabela.value, ident, obj1); }

                // Atiualização
                if (action == "U") { Audit.Update(tabela.idtabela.value, ident, obj1, obj2); }
            }
        }

        // Grava auditoria de inclusao
        public static void Insert(int table, int ident, dynamic obj)
        {
            Auditoria aud = new Auditoria();
            aud.idusuario = Convert.ToInt32(Utils.Null(Convert.ToString(HttpContext.Current.Session["usuario"]), "0"));
            aud.txlog = Audit.AuditMsg(obj, null);
            aud.txoperacao = "I";
            aud.txip = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();
            aud.idtabela = table;
            aud.ididentificador = ident;
            aud.Gravar();
        }

        // Grava auditoria de exclusao
        public static void Delete(int table, int ident, dynamic obj)
        {
            Auditoria aud = new Auditoria();
            aud.idusuario = Convert.ToInt32(Utils.Null(Convert.ToString(HttpContext.Current.Session["usuario"]), "0"));
            aud.txlog = Audit.AuditMsg(obj, null);
            aud.txoperacao = "D";
            aud.txip = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();
            aud.idtabela = table;
            aud.ididentificador = ident;
            aud.Gravar();
        }

        // Grava auditoria de atualização
        public static void Update(int table, int ident, dynamic obj, dynamic obj2)
        {
            Auditoria aud = new Auditoria();
            aud.idusuario = Convert.ToInt32(Utils.Null(Convert.ToString(HttpContext.Current.Session["usuario"]), "0"));
            aud.txlog = Audit.AuditMsg(obj, obj2);
            aud.txoperacao = "U";
            aud.txip = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();
            aud.idtabela = table;
            aud.ididentificador = ident;
            aud.Gravar();
        }

        // Monta a string de auditoria
        private static string AuditMsg(dynamic obj, dynamic obj2)
        {
            string msg = "";

            // Incluir ou excluir
            if (obj2 == null)
            {

                foreach (FieldInfo myField in obj.GetType().GetFields())
                {
                    // Pega o campo
                    Variable val = obj.GetField(myField.Name);

                    // Somente faz auditoria nos campos com configuração
                    if (val.config != "")
                    {                        
                        msg += Language.XmlLang(Convert.ToInt32(val.config.Split('|')[0]), 1).Text + ": " + val.value + "<br>";
                    }
                }
                
            }

            // Alterar
            else
            {
                foreach (FieldInfo myField in obj.GetType().GetFields())
                {
                    // Pega os campos
                    Variable val = obj.GetField(myField.Name);
                    Variable val2 = obj2.GetField(myField.Name);

                    // Somente faz auditoria nos campos com configuração
                    if (val.config != "")
                    {
                        // Verifica se houve alteração
                        if (val.value != val2.value)
                        {
                            msg += Language.XmlLang(Convert.ToInt32(val2.config.Split('|')[0]), 1).Text + ": " + val2.value + " => " + val.value + "<br>";
                        }
                    }
                }
            }

            return msg;
        }
    }
}
