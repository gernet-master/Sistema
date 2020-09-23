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
    public class AuditoriaDB : Session
    {

        // Altera usuário
        public void Gravar(Auditoria rs)
        {
            try
            {
                string qry = "";
                qry += "INSERT into Auditoria (dtauditoria, idusuario, txlog, txoperacao, txip, idtabela, ididentificador) ";
                qry += "VALUES ('" + rs.dtauditoria + "', " + rs.idusuario + ", '" + rs.txlog + "', '" + rs.txoperacao + "', '" + rs.txip + "', " + rs.idtabela + ", " + rs.ididentificador + ")";

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