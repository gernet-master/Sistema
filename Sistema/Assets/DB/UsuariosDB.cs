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
        public int Save(Usuarios rs)
        {
            try
            {
                int idusuario = 0;
                Connection session = new Connection();
                Query query = session.CreateQuery(@"
                    INSERT INTO Usuarios (txnome, txusuario, txsenha, txemail, idperfil, flativo, flmaster, flalterasenha, txfoto) output INSERTED.idusuario 
                    VALUES (" + rs.idgernet.value + ", '" + rs.txnome.value + "', '" + rs.txusuario.value + "', '" + rs.txsenha.value + "', '" + rs.txemail.value + "', " + rs.idperfil.value + ", " + 
                        rs.flativo.value + ", " + rs.flmaster.value + ", " + rs.flalterasenha.value + ", '" + rs.txfoto.value + "')");
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
        public void Edit(Usuarios variavel)
        {
            try
            {
                Connection session = new Connection();
                Query query = session.CreateQuery("UPDATE Banners SET idsite = @idsite, txfoto = @txfoto, txlink = @txlink, nrordem = @nrordem, flativo = @flativo, dtinicio = @dtinicio, dtfim = @dtfim WHERE idbanner = @idbanner");
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        // Valida dados de acesso
        public Usuarios Login(string user, string password)
        {
            try
            {
                Usuarios us = null;

                Connection session = new Connection();
                Query query = session.CreateQuery(@"SELECT * FROM usuarios WHERE txusuario = '" + user + "' AND txsenha = '" + password + "' AND idgernet = " + session_gernet);
                IDataReader reader = query.ExecuteQuery();

                if (reader.Read())
                {
                    us = new Usuarios()
                    {
                        idusuario = new Variable(value: Convert.ToInt32(reader["idusuario"])),
                        txnome = new Variable(value: Convert.ToString(reader["txnome"])),
                        idperfil = new Variable(value: Convert.ToInt32(reader["idperfil"])),
                        flativo = new Variable(value: Convert.ToInt32(reader["flativo"])),
                        flmaster = new Variable(value: Convert.ToInt32(reader["flmaster"])),
                        flalterasenha = new Variable(value: Convert.ToInt32(reader["flalterasenha"]))
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
        public Boolean ValidacaoUsuarioCliente(int idusuario)
        {
            try
            {
                Boolean ret = false;

                Connection session = new Connection();
                Query query = session.CreateQuery(@"SELECT * FROM usuarios WHERE idusuario = " + idusuario + " AND idgernet = " + session_gernet);
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

        // Lista usuários
        public List<Usuarios> List()
        {
            try
            {
                List<Usuarios> us = new List<Usuarios>();

                Connection session = new Connection();                
                Query query = session.CreateQuery(@"SELECT * FROM usuarios");
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

    }
}