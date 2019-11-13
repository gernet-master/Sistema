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
                Connection session = new Connection();
                Query query = session.CreateQuery(@"
                    INSERT INTO Log_Acesso (idusuario, dtlog, tplog, txip)  
                    VALUES (" + rs.idusuario.value + ", '" + rs.dtlog.value + "', '" + rs.tplog.value + "', '" + rs.txip.value + "')");
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

