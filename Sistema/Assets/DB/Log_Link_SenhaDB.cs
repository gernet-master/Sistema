using Functions;
using Sistema.Assets.Entities;
using Sistema.Assets.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Sistema.Assets.DB
{
    public class Log_Link_SenhaDB : Session
    {
        // Gravar novo log de envio de link para alteração de senha
        public void Gravar(Log_Link_Senha rs)
        {
            try
            {
                string qry = "";
                qry += "INSERT INTO log_link_senha (idusuario, dtlink, txchave, flutilizado, dtutilizado) ";
                qry += "VALUES (" + rs.idusuario.value + ", '" + rs.dtlink.value + "', '" + rs.txchave.value + "', " + rs.flutilizado.value + ", null)";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        // Altera log
        public void Alterar(Log_Link_Senha variavel)
        {
            try
            {
                string qry = "";
                qry += "UPDATE log_link_senha ";
                qry += "SET flutilizado = " + variavel.flutilizado.value + ", dtutilizado = '" + variavel.dtutilizado.value + "' WHERE idlog = " + variavel.idlog.value;

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        // Valida se já foi enviado o link para recuperar senha
        public Boolean LinkSenha(int idusuario = 0)
        {
            try
            {
                Boolean ret = false;

                string qry = "";
                qry += "SELECT * from log_link_senha WHERE idusuario = " + idusuario + " AND flutilizado = 0 AND DATEDIFF(HOUR, dtlink, getdate()) < 24";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    ret = true;
                }
                reader.Close();
                session.Close();

                return ret;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        // Busca pelo código
        public Log_Link_Senha Buscar(string txchave = "")
        {
            try
            {
                Log_Link_Senha lls = null;

                string qry = "";
                qry += "SELECT idlog, idusuario, dtlink, txchave, flutilizado, ISNULL(dtutilizado, '1900-01-01') as dtutilizado ";
                qry += "FROM log_link_senha WHERE txchave = '" + txchave + "'";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);

                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    lls = new Log_Link_Senha()
                    {
                        idlog = new Variable(value: Convert.ToInt32(reader["idlog"])),
                        idusuario = new Variable(value: Convert.ToInt32(reader["idusuario"])),
                        dtlink = new Variable(value: Convert.ToDateTime(reader["dtlink"])),
                        txchave = new Variable(value: Convert.ToString(reader["txchave"])),
                        flutilizado = new Variable(value: Convert.ToInt32(reader["flutilizado"])),
                        dtutilizado = new Variable(value: Convert.ToDateTime(reader["dtutilizado"]))
                    };
                }
                reader.Close();
                session.Close();

                return lls;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}

