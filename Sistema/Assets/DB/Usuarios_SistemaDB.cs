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
        public void Save(Usuarios_Sistema rs)
        {
            try
            {
                string qry = "";
                qry += "INSERT INTO Usuarios_Sistema (idusuario, idunidade, flderrubar, idsession, qtacessos, txip, txrefresh, txderrubar, txaplicativo, txaplicativomain, txbloqueado) ";
                qry += "VALUES (" + rs.idusuario.value + ", " + rs.idunidade.value + ", '" + rs.flderrubar.value + "', '" + rs.idsession.value + "', " + rs.qtacessos.value + ", '" + rs.txip.value + "', ";
                qry += "'" + rs.txrefresh.value + "', '" + rs.txderrubar.value + "', '" + rs.txaplicativo.value + "', '" + rs.txaplicativomain.value + "', '" + rs.txbloqueado.value + "')";

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
        public void Edit(Usuarios_Sistema variavel)
        {
            try
            {
                string qry = "";
                qry += "UPDATE Usuarios_Sistema ";
                qry += "SET idunidade = " + variavel.idunidade.value + ", flderrubar = " + variavel.flderrubar.value + ", idsession = '" + variavel.idsession.value + "', ";
                qry += "qtacessos = " + variavel.qtacessos.value + ", txip = '" + variavel.txip.value + "', txrefresh = '" + variavel.txrefresh.value + "', txderrubar = '" + variavel.txderrubar.value + "', ";
                qry += "txaplicativo = '" + variavel.txaplicativo.value + "', txaplicativomain = '" + variavel.txaplicativomain.value + "', txbloqueado = '" + variavel.txbloqueado.value + "' ";
                qry += "WHERE idusuario = " + variavel.idusuario.value;

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
        public Usuarios_Sistema GetUserControl(int idusuario)
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
                        txbloqueado = new Variable(value: Convert.ToDateTime(reader["txbloqueado"]))
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

    }
}

