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
    public class Gernet_ControleDB : Session
    {
        // Pega as configurações do Cliente
        public Gernet_Controle Buscar()
        {
            try
            {
                Gernet_Controle gc = null;

                string qry = "";
                qry += "SELECT * FROM gernet_controle WHERE idgernet = " + session_gernet;

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);

                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    gc = new Gernet_Controle()
                    {
                        idgernet = new Variable(value: Convert.ToInt32(reader["idgernet"])),
                        txcliente = new Variable(value: Convert.ToString(reader["txcliente"])),
                        txlink = new Variable(value: Convert.ToString(reader["txlink"])),
                        qtunidades = new Variable(value: Convert.ToInt32(reader["qtunidades"]))
                    };
                }
                reader.Close();
                session.Close();

                return gc;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}

