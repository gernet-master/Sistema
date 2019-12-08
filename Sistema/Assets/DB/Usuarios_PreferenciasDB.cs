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
    public class Usuarios_PreferenciasDB : Session
    {
        // Gravar as preferencias do usuário
        public void Gravar(Usuarios_Preferencias rs)
        {
            try
            {
                string qry = "";
                qry += "INSERT INTO Usuarios_Preferencias (idusuario, idunidade, txidioma) ";
                qry += "VALUES (" + rs.idusuario.value + ", " + rs.idunidade.value + ", '" + rs.txidioma.value + "')";

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

        // Pega as preferencias do usuário
        public Usuarios_Preferencias PreferenciasUsuario(int idusuario)
        {
            try
            {
                Usuarios_Preferencias up = null;

                Connection session = new Connection();
                Query query = session.CreateQuery(@"SELECT * FROM Usuarios_Preferencias WHERE idusuario = " + idusuario);
                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    up = new Usuarios_Preferencias()
                    {
                        idusuario = new Variable(value: Convert.ToInt32(reader["idusuario"])),
                        idunidade = new Variable(value: Convert.ToInt32(reader["idunidade"])),
                        txidioma = new Variable(value: Convert.ToString(reader["txidioma"]))
                    };
                }
                reader.Close();
                session.Close();

                return up;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}

