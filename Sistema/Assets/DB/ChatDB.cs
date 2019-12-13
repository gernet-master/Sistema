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
        public void Gravar(ChatMsg rs)
        {
            try
            {
                string qry = "";
                qry += "INSERT INTO chat (idremetente, iddestinatario, txmensagem, dtmensagem, dtlido, flprivacidade) ";
                qry += "VALUES (" + rs.idremetente.value + ", " + rs.iddestinatario.value + ", '" + rs.txmensagem.value + ", '" + rs.dtmensagem.value + ", null, null, " + rs.flprivacidade.value + ")";

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
        public List<ChatUser> ListarUsuarios(string search = "")
        {
            try
            {
                List<ChatUser> us = new List<ChatUser>();

                string qry = "";
                qry += "SELECT * FROM ( ";
                qry += "SELECT u.idusuario, u.txnome, u.txfoto, ISNULL(us.idsession,0) AS idsession, ISNULL(us.flstatuschat, 2) as flstatuschat, ";
                qry += "    (SELECT COUNT(*) AS total FROM chat WHERE idremetente = u.idusuario AND iddestinatario = " + session_usuario + " AND dtlido is null) AS qtnaolidas, ";
                qry += "    (SELECT COUNT(*) AS total FROM chat WHERE idremetente = u.idusuario AND iddestinatario = " + session_usuario + " AND dtrecebido is null) AS qtnaorecebidas ";
                qry += "FROM usuarios u ";
                qry += "LEFT JOIN usuarios_sistema us ON us.idusuario = u.idusuario ";
                qry += "WHERE u.flativo = 1 AND u.idusuario <> " + session_usuario + " AND u.idgernet = " + session_gernet + " ";

                if (search.Length > 1)
                {
                    qry += "AND u.txnome LIKE '%" + search.Replace(" ", "%") + "%' ";
                }

                qry += ") AS t ";
                qry += "ORDER BY t.qtnaolidas DESC, t.txnome ASC";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    us.Add(new ChatUser()
                    {
                        idusuario = Convert.ToInt32(reader["idusuario"]),
                        txnome = Convert.ToString(reader["txnome"]),
                        qtnaolidas = Convert.ToInt32(reader["qtnaolidas"]),
                        qtnaorecebidas = Convert.ToInt32(reader["qtnaorecebidas"]),
                        txfoto = Convert.ToString(reader["txfoto"]),
                        idsession = Convert.ToString(reader["idsession"]),
                        flstatuschat = Convert.ToInt32(reader["flstatuschat"]),
                        mensagem = new ChatDB().BuscarUltima(Convert.ToInt32(reader["idusuario"]))
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

        // Busca a última mensagem do chat entre usuários
        public ChatMsg BuscarUltima(int idusuario = 0)
        {
            try
            {
                ChatMsg msg = new ChatMsg();

                string qry = "";
                qry += "SELECT TOP 1 * FROM chat WHERE (iddestinatario = " + idusuario + " AND idremetente = " + session_usuario + ") ";
                qry += "    or (iddestinatario = " + session_usuario + " AND idremetente = " + idusuario + ") ORDER BY dtmensagem DESC";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                if (reader.Read())
                {                    
                    msg.idmensagem.value = Convert.ToInt32(reader["idmensagem"]);
                    msg.idremetente.value = Convert.ToInt32(reader["idremetente"]);
                    msg.iddestinatario.value = Convert.ToInt32(reader["iddestinatario"]);
                    msg.txmensagem.value = Convert.ToString(reader["txmensagem"]);
                    msg.dtmensagem.value = (DateTime?)Convert.ToDateTime(reader["dtmensagem"]);
                    msg.dtrecebido.value = reader["dtrecebido"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["dtrecebido"]);
                    msg.dtlido.value = reader["dtlido"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["dtlido"]);
                    msg.flprivacidade.value = Convert.ToInt32(reader["flprivacidade"]);
                }

                reader.Close();
                session.Close();

                return msg;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        // Marca como lida todas as novas mensagens
        public void MarcaRecebido()
        {
            try
            {
                string qry = "";
                qry += "UPDATE chat SET dtrecebido = GETDATE() WHERE iddestinatario = " + session_usuario + " AND dtrecebido IS NULL";

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
