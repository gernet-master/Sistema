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
    public class Usuarios_UnidadesDB : Session
    {
        // Gravar nova unidade para o usuário
        public void Gravar(Usuarios_Unidades rs)
        {
            try
            {
                Connection session = new Connection();
                Query query = session.CreateQuery(@"
                    INSERT INTO Usuarios_Unidades (idusuario, idunidade)  
                    VALUES (" + rs.idusuario.value + ", " + rs.idunidade.value + "')");
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        // Lista unidades vinculadas ao usuário
        public List<Usuarios_Unidades> Listar(int idusuario = 0)
        {
            try
            {
                List<Usuarios_Unidades> uu = new List<Usuarios_Unidades>();

                Connection session = new Connection();
                Query query = session.CreateQuery(@"SELECT * FROM Usuarios_Unidades WHERE idusuario = " + idusuario);
                IDataReader reader = query.ExecuteQuery();
                while (reader.Read())
                {
                    uu.Add(new Usuarios_Unidades()
                    {
                        idusuario = new Variable(value: Convert.ToInt32(reader["idusuario"])),
                        idunidade = new Variable(value: Convert.ToInt32(reader["idunidade"]))
                    });
                }
                reader.Close();
                session.Close();

                return uu;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}

