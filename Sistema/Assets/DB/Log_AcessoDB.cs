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
    public class Log_AcessoDB : Session
    {
        // Gravar novo log de acesso
        public void Gravar(Log_Acesso rs)
        {
            try
            {
                string qry = "";
                qry += "INSERT INTO Log_Acesso (idusuario, dtlog, tplog, txip) ";
                qry += "VALUES (" + rs.idusuario.value + ", '" + rs.dtlog.value + "', '" + rs.tplog.value + "', '" + rs.txip.value + "')";

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

        // Total de vezes o usuário errou a senha nos últimos 5 minutos
        public int ErrouSenha(int idusuario = 0)
        {
            try
            {
                int ret = 0;

                string qry = "";
                qry += "SELECT (COUNT(*) + 1) AS cont FROM log_acesso WHERE idusuario = " + idusuario + " AND tplog = 'R' and DATEDIFF(MINUTE, dtlog, GETDATE()) <= 5";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    ret = Convert.ToInt32(reader["cont"]);
                }
                reader.Close();
                session.Close();

                return ret;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
    }
}

