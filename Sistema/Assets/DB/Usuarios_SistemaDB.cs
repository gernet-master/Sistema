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
    public class Usuarios_SistemaDB : Session
    {
        // Gravar novo controle de usuário
        public void Gravar(Usuarios_Sistema rs)
        {
            try
            {
                string qry = "";
                qry += "INSERT INTO Usuarios_Sistema (idusuario, idunidade, flderrubar, idsession, qtacessos, txip, txrefresh, txderrubar, txaplicativo, ";
                qry += "txaplicativomain, txbloqueado, flstatuschat, flprivacidade, flmsgconfig) ";
                qry += "VALUES (" + rs.idusuario.value + ", " + rs.idunidade.value + ", '" + rs.flderrubar.value + "', '" + rs.idsession.value + "', " + rs.qtacessos.value + ", '" + rs.txip.value + "', ";
                qry += "'" + rs.txrefresh.value + "', '" + rs.txderrubar.value + "', '" + rs.txaplicativo.value + "', '" + rs.txaplicativomain.value + "', '" + rs.txbloqueado.value + "', ";
                qry += rs.flstatuschat.value + ", " + rs.flprivacidade.value + ", " + rs.flmsgconfig.value + ")";

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

        // Altera controle de usuário
        public void Alterar(Usuarios_Sistema rs)
        {
            try
            {
                string qry = "";
                qry += "UPDATE Usuarios_Sistema ";
                qry += "SET idunidade = " + rs.idunidade.value + ", flderrubar = " + rs.flderrubar.value + ", idsession = '" + rs.idsession.value + "', ";
                qry += "qtacessos = " + rs.qtacessos.value + ", txip = '" + rs.txip.value + "', txrefresh = '" + rs.txrefresh.value + "', txderrubar = '" + rs.txderrubar.value + "', ";
                qry += "txaplicativo = '" + rs.txaplicativo.value + "', txaplicativomain = '" + rs.txaplicativomain.value + "', txbloqueado = '" + rs.txbloqueado.value + "', ";
                qry += "flstatuschat = " + rs.flstatuschat.value + ", flprivacidade = " + rs.flprivacidade.value  + ", flmsgconfig = " + rs.flmsgconfig.value + " ";
                qry += "WHERE idusuario = " + rs.idusuario.value;

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

        // Altera o status do chat
        public void AlterarStatusChat(int flstatuschat = 1)
        {
            try
            {
                string qry = "";
                qry += "UPDATE Usuarios_Sistema ";
                qry += "SET flstatuschat = " + flstatuschat + " ";
                qry += "WHERE idusuario = " + session_usuario;

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

        // Altera a privacidade do chat
        public void AlterarPrivacidadeChat(int flprivacidade = 1)
        {
            try
            {
                string qry = "";
                qry += "UPDATE Usuarios_Sistema ";
                qry += "SET flprivacidade = " + flprivacidade + " ";
                qry += "WHERE idusuario = " + session_usuario;

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

        // Altera a configuração de msensagem do chat
        public void AlterarConfigMsgChat(int flmsgconfig = 0)
        {
            try
            {
                string qry = "";
                qry += "UPDATE Usuarios_Sistema ";
                qry += "SET flmsgconfig = " + flmsgconfig + " ";
                qry += "WHERE idusuario = " + session_usuario;

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

        // Pega os dados de controle do usuário
        public Usuarios_Sistema Buscar(int idusuario = 0)
        {
            try
            {
                Usuarios_Sistema us = null;

                string qry = "";
                qry += "SELECT * FROM Usuarios_Sistema WHERE idusuario = " + idusuario;

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    us = new Usuarios_Sistema()
                    {
                        idusuario = new Variable(value: Convert.ToInt32(reader["idusuario"])),
                        idunidade = new Variable(value: Convert.ToInt32(reader["idunidade"])),
                        flderrubar = new Variable(value: Convert.ToInt32(reader["flderrubar"])),
                        idsession = new Variable(value: Convert.ToString(reader["idsession"])),
                        qtacessos = new Variable(value: Convert.ToInt32(reader["qtacessos"])),
                        txip = new Variable(value: Convert.ToString(reader["txip"])),
                        txrefresh = new Variable(value: Convert.ToDateTime(reader["txrefresh"])),
                        txderrubar = new Variable(value: Convert.ToDateTime(reader["txderrubar"])),
                        txaplicativo = new Variable(value: Convert.ToString(reader["txaplicativo"])),
                        txaplicativomain = new Variable(value: Convert.ToString(reader["txaplicativomain"])),
                        txbloqueado = new Variable(value: Convert.ToDateTime(reader["txbloqueado"])),
                        flstatuschat = new Variable(value: Convert.ToInt32(reader["flstatuschat"])),
                        flprivacidade = new Variable(value: Convert.ToInt32(reader["flprivacidade"])),
                        flmsgconfig = new Variable(value: Convert.ToInt32(reader["flmsgconfig"]))
                    };
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

        // Pega o sessionid de acordo com o usuário
        public string SessionId(int idusuario = 0)
        {
            try
            {
                string ret = "";

                string qry = "";
                qry += "SELECT idsession FROM usuarios_sistema WHERE idusuario = " + idusuario;

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    ret = Convert.ToString(reader["idsession"]);
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
    }
}

