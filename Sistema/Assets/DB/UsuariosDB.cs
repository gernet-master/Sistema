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
    public class UsuariosDB : Session
    {
        // Gravar novo usuário
        public int Gravar(Usuarios rs)
        {
            try
            {
                int idusuario = 0;

                string qry = "";
                qry += "INSERT INTO Usuarios (idgernet, txnome, txusuario, txsenha, txemail, idperfil, flativo, flmaster, flalterasenha, txfoto) output INSERTED.idusuario ";
                qry += "VALUES (" + rs.idgernet.value + ", '" + rs.txnome.value + "', '" + rs.txusuario.value + "', '" + rs.txsenha.value + "', '" + rs.txemail.value + "', " + rs.idperfil.value + ", ";
                qry += rs.flativo.value + ", " + rs.flmaster.value + ", " + rs.flalterasenha.value + ", '" + rs.txfoto.value + "')";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                idusuario = query.ExecuteScalar();
                session.Close();
                return idusuario;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        // Altera usuário
        public void Alterar(Usuarios variavel)
        {
            try
            {
                string qry = "";
                qry += "UPDATE Usuarios ";
                qry += "SET txnome = '" + variavel.txnome.value + "', txusuario = '" + variavel.txusuario.value + "', txsenha = '" + variavel.txsenha.value + "', ";
                qry += "txemail = '" + variavel.txemail.value + "', idperfil = " + variavel.idperfil.value + ", flativo = " + variavel.flativo.value + ", ";
                qry += "flalterasenha = " + variavel.flalterasenha.value + ", txfoto = '" + variavel.txfoto.value + "' ";
                qry += "WHERE idusuario = " + variavel.idusuario.value + " AND idgernet = " + session_gernet;

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

        // Valida dados de acesso
        public Usuarios Login(string txusuario = "", string password = "", int idusuario = 0)
        {
            try
            {
                Usuarios us = null;

                string qry = "";
                qry += "SELECT * FROM usuarios WHERE txsenha = '" + password + "' AND idgernet = " + session_gernet + " ";

                if (idusuario > 0)
                {
                    qry += " AND idusuario = " + idusuario + " ";
                }
                else
                {
                    qry += " AND txusuario = '" + txusuario + "' ";
                }
                
                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                if (reader.Read())
                {
                    us = new Usuarios()
                    {
                        idusuario = new Variable(value: Convert.ToInt32(reader["idusuario"])),
                        idgernet = new Variable(value: Convert.ToInt32(reader["idgernet"])),
                        txnome = new Variable(value: Convert.ToString(reader["txnome"])),
                        txusuario = new Variable(value: Convert.ToString(reader["txusuario"])),
                        idperfil = new Variable(value: Convert.ToInt32(reader["idperfil"])),
                        txsenha = new Variable(value: Convert.ToString(reader["txsenha"])),
                        txemail = new Variable(value: Convert.ToString(reader["txemail"])),
                        flativo = new Variable(value: Convert.ToInt32(reader["flativo"])),
                        flmaster = new Variable(value: Convert.ToInt32(reader["flmaster"])),
                        flalterasenha = new Variable(value: Convert.ToInt32(reader["flalterasenha"])),
                        txfoto = new Variable(value: Convert.ToString(reader["txfoto"])),
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

        // Valida se o usuário pertence ao cliente
        public Boolean ValidaUsuarioCliente(int idusuario = 0)
        {
            try
            {
                Boolean ret = false;

                string qry = "";
                qry += "SELECT * FROM usuarios WHERE idgernet = " + session_gernet + " AND idusuario = " + idusuario;

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    ret = true;
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

        // Valida se o email pertence ao usuário
        public int ValidaUsuarioEmail(string txusuario = "", string txemail = "")
        {
            try
            {
                int ret = 0;

                string qry = "";
                qry += "SELECT * FROM usuarios WHERE idgernet = " + session_gernet + " AND txusuario = '" + txusuario + "' AND txemail = '" + txemail + "'";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    ret = Convert.ToInt32(reader["idusuario"]);
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

        // Pega o id de acordo com o usuário
        public int Id(string txusuario = "")
        {
            try
            {
                int ret = 0;

                string qry = "";
                qry += "SELECT idusuario FROM usuarios WHERE idgernet = " + session_gernet + " AND txusuario = '" + txusuario + "' ";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    ret = Convert.ToInt32(reader["idusuario"]);
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

        // Lista usuários
        public List<Usuarios> Listar()
        {
            try
            {
                List<Usuarios> us = new List<Usuarios>();

                string qry = "";
                qry += "SELECT * FROM usuarios WHERE idgernet = " + session_gernet;

                Connection session = new Connection();                
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    us.Add(new Usuarios()
                    {
                        idusuario = new Variable(value: Convert.ToInt32(reader["idusuario"])),
                        idgernet = new Variable(value: Convert.ToInt32(reader["idgernet"])),
                        txnome = new Variable(value: Convert.ToString(reader["txnome"])),
                        txusuario = new Variable(value: Convert.ToString(reader["txusuario"])),
                        txsenha = new Variable(value: Convert.ToString(reader["txsenha"])),
                        txemail = new Variable(value: Convert.ToString(reader["txemail"])),
                        idperfil = new Variable(value: Convert.ToInt32(reader["idperfil"])),
                        flativo = new Variable(value: Convert.ToInt32(reader["flativo"])),
                        flmaster = new Variable(value: Convert.ToInt32(reader["flmaster"])),
                        flalterasenha = new Variable(value: Convert.ToInt32(reader["flalterasenha"])),
                        txfoto = new Variable(value: Convert.ToString(reader["txfoto"]))
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

        // Busca pelo código
        public Usuarios Buscar(int idusuario = 0)
        {
            try
            {
                Usuarios u = null;

                string qry = "";
                qry += "SELECT * FROM usuarios WHERE idusuario = " + idusuario;

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);

                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    u = new Usuarios()
                    {
                        idusuario = new Variable(value: Convert.ToInt32(reader["idusuario"])),
                        idgernet = new Variable(value: Convert.ToInt32(reader["idgernet"])),
                        txnome = new Variable(value: Convert.ToString(reader["txnome"])),
                        txusuario = new Variable(value: Convert.ToString(reader["txusuario"])),
                        txsenha = new Variable(value: Convert.ToString(reader["txsenha"])),
                        txemail = new Variable(value: Convert.ToString(reader["txemail"])),
                        idperfil = new Variable(value: Convert.ToInt32(reader["idperfil"])),
                        flativo = new Variable(value: Convert.ToInt32(reader["flativo"])),
                        flmaster = new Variable(value: Convert.ToInt32(reader["flmaster"])),
                        flalterasenha = new Variable(value: Convert.ToInt32(reader["flalterasenha"])),
                        txfoto = new Variable(value: Convert.ToString(reader["txfoto"]))
                    };
                }
                reader.Close();
                session.Close();

                return u;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

    }
}