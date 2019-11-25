using Functions;
using Sistema.Assets.Entities;
using Sistema.Assets.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace Sistema.Assets.DB
{
    public class ChatDB : Session
    {
        // Gravar nova mensagem de chat
        public void Gravar(Chat rs)
        {
            try
            {
                string qry = "";
                qry += "INSERT INTO chat (idremetente, iddestinatario, txmensagem, dtmensagem, dtlido) ";
                qry += "VALUES (" + rs.idremetente.value + ", " + rs.iddestinatario.value + ", '" + rs.txmensagem.value + ", '" + rs.dtmensagem.value + ", null, null)";

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

        // Lista usuários
        public List<ChatUser> ListarUsuarios()
        {
            try
            {
                List<ChatUser> us = new List<ChatUser>();

                string qry = "";
                qry += "SELECT *, ISNULL(t.data, '1900-01-01') as data_ultima FROM (";
                qry += "    SELECT u.idusuario, u.txnome, u.txfoto, ISNULL(us.idsession,0) AS idsession, ";
                qry += "        (SELECT COUNT(*) AS total FROM chat WHERE idremetente = u.idusuario AND iddestinatario = u.idusuario AND dtlido is null) AS txmensagem, ";
                qry += "        (SELECT COUNT(*) AS total FROM chat WHERE idremetente = u.idusuario AND iddestinatario = u.idusuario AND dtrecebido is null) AS recebidas, ";
                qry += "        (SELECT top 1 txmensagem FROM chat WHERE idremetente = u.idusuario AND iddestinatario = u.idusuario order by dtmensagem desc) as ultima, ";
                qry += "        (SELECT top 1 dtmensagem FROM chat WHERE idremetente = u.idusuario AND iddestinatario = u.idusuario order by dtmensagem desc) as data ";
                qry += "    FROM usuarios u ";
                qry += "	LEFT JOIN usuarios_sistema us ON us.idusuario = u.idusuario ";
                qry += "    WHERE u.flativo = 1 AND u.idusuario <> " + session_usuario + " AND u.idgernet = " + session_gernet + " ";
                qry += ") AS t ";
                qry += "ORDER BY CASE WHEN t.txmensagem > 0 THEN CONVERT(VARCHAR(20), t.data, 113) END DESC, CASE WHEN t.txmensagem = 0 THEN t.txnome END ASC";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    us.Add(new ChatUser()
                    {
                        idusuario = Convert.ToInt32(reader["idusuario"]),
                        txnome = Convert.ToString(reader["txnome"]),
                        txmensagem = Convert.ToString(reader["txmensagem"]),
                        recebidas = Convert.ToInt32(reader["recebidas"]),
                        ultima = Convert.ToString(reader["ultima"]),
                        data_ultima = Convert.ToDateTime(reader["data_ultima"]),
                        txfoto = Convert.ToString(reader["txfoto"]),
                        idsession = Convert.ToString(reader["idsession"])
                    });
                }
                reader.Close();
                session.Close();

                return us;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

    }
}
