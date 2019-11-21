using Functions;
using Sistema.Assets.Entities;
using Sistema.Assets.Functions;
using System;

namespace Sistema.Assets.DB
{
    public class Log_SenhaDB : Session
    {
        // Gravar novo log de alteração de senha
        public void Gravar(Log_Senha rs)
        {
            try
            {
                string qry = "";
                qry += "INSERT INTO log_senha (idusuario, dtalteracao) ";
                qry += "VALUES (" + rs.idusuario.value + ", '" + rs.dtalteracao.value + "')";

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

