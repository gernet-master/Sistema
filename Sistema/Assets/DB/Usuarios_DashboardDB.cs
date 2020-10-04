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
    public class Usuarios_DashboardDB : Session
    {
        // Gravar as preferencias do usuário
        public void Gravar(Usuarios_Dashboard rs)
        {
            try
            {
                string qry = "";
                qry += "INSERT INTO Usuarios_Dashboard (idusuario, idaplicativo, fldashboard, fltiporeg) ";
                qry += "VALUES (" + rs.idusuario.value + ", " + rs.idaplicativo.value + ", " + rs.fldashboard.value + ", '" + rs.fltiporeg.value + "')";

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

        // Alterar
        public void Alterar(Usuarios_Dashboard rs)
        {
            try
            {
                string qry = "";
                qry += "UPDATE Usuarios_Dashboard ";
                qry += "SET fldashboard = " + rs.fldashboard.value + ", fltiporeg = '" + rs.fltiporeg.value + "' ";
                qry += "WHERE idusuario = " + rs.idusuario.value + " AND idaplicativo = " + rs.idaplicativo.value;

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

        // Pega as configurações da dashboard  do usuário
        public Usuarios_Dashboard Buscar(int idusuario, int idaplicativo)
        {
            try
            {
                Usuarios_Dashboard config = null;

                Connection session = new Connection();
                Query query = session.CreateQuery(@"SELECT * FROM Usuarios_Dashboard WHERE idusuario = " + idusuario + " AND idaplicativo = " + idaplicativo);
                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    config = new Usuarios_Dashboard()
                    {
                        idusuario = new Variable(value: Convert.ToInt32(reader["idusuario"])),
                        idaplicativo = new Variable(value: Convert.ToInt32(reader["idaplicativo"])),
                        fldashboard = new Variable(value: Convert.ToInt32(reader["fldashboard"])),
                        fltiporeg = new Variable(value: Convert.ToString(reader["fltiporeg"]))
                    };
                }
                reader.Close();
                session.Close();

                return config;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}

