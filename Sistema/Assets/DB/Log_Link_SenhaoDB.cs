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
    public class Log_Link_SenhaoDB : Session
    {
        // Gravar novo log de envio de link para alteração de senha
        public void Gravar(Log_Link_Senha rs)
        {
            try
            {
                string qry = "";
                qry += "INSERT INTO Log_Link_Senha (idusuario, dtlink, txchave, flutilizado) ";
                qry += "VALUES (" + rs.idusuario.value + ", '" + rs.dtlink.value + "', '" + rs.txchave.value + "', " + rs.flutilizado.value + ")";

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
    }
}

