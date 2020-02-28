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
    public class TabelasDB : Session
    {
        // Gravar nova tabela
        public int Gravar(Tabelas rs)
        {
            try
            {
                int idtabela = 0;

                string qry = "";
                qry += "INSERT INSERT INTO Tabelas (txtabela, flauditoria, idcodigoidioma) output INSERTED.idtabela ";
                qry += "VALUES ('" + rs.txtabela.value + "', " + rs.flauditoria.value + ", " + rs.idcodigoidioma.value + ")";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                idtabela = query.ExecuteScalar();
                session.Close();
                return idtabela;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        // Altera tabela
        public void Alterar(Tabelas rs)
        {
            try
            {
                string qry = "";
                qry += "UPDATE Tabelas ";
                qry += "SET txtabela = '" + rs.txtabela.value + "', flauditoria = " + rs.flauditoria.value + ", idcodigoidioma = " + rs.idcodigoidioma.value + " ";
                qry += "WHERE idtabela = " + rs.idtabela.value;

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

        // Lista tabelas
        public List<Tabelas> Listar()
        {
            try
            {
                List<Tabelas> tabs = new List<Tabelas>();

                string qry = "";
                qry += "SELECT * FROM Tabelas";

                Connection session = new Connection();                
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    tabs.Add(new Tabelas()
                    {
                        idtabela = new Variable(value: Convert.ToInt32(reader["idtabela"])),
                        txtabela = new Variable(value: Convert.ToString(reader["txtabela"])),
                        flauditoria = new Variable(value: Convert.ToBoolean(reader["flauditoria"])),
                        idcodigoidioma = new Variable(value: Convert.ToInt32(reader["idcodigoidioma"]))
                    });
                }
                reader.Close();
                session.Close();

                return tabs;
            }
            catch (Exception error)
            {
                throw error;
            }
        }        

        // Busca pelo código
        public Tabelas Buscar(int idtabela = 0)
        {
            try
            {
                Tabelas tabs = null;

                string qry = "";
                qry += "SELECT * FROM Tabelas WHERE idtabela = " + idtabela;

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);

                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    tabs = new Tabelas()
                    {
                        idtabela = new Variable(value: Convert.ToInt32(reader["idtabela"])),
                        txtabela = new Variable(value: Convert.ToString(reader["txtabela"])),
                        flauditoria = new Variable(value: Convert.ToBoolean(reader["flauditoria"])),
                        idcodigoidioma = new Variable(value: Convert.ToInt32(reader["idcodigoidioma"]))
                    };
                }
                reader.Close();
                session.Close();

                return tabs;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        // Lista tabelas não cadastradas
        public List<Select_List> ListarNaoCadastradas()
        {
            try
            {
                List<Select_List> tabs = new List<Select_List>();

                string qry = "";
                qry += "SELECT name AS texto FROM SYS.TABLES WHERE name <> 'sysdiagrams' AND name NOT IN (SELECT txtabela FROM Tabelas) ORDER BY name";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    tabs.Add(new Select_List()
                    {
                        ident = new Variable(value: Convert.ToString(reader["texto"])),
                        text = new Variable(value: Convert.ToString(reader["texto"]))
                    });
                }
                reader.Close();
                session.Close();

                return tabs;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        // Listagem
        public (List<Tabelas>, WidgetsListagem) Listagem(int pagina = 1, int registros = 10, string order = "txtabela")
        {
            try
            {
                List<Tabelas> listagem = new List<Tabelas>();
                WidgetsListagem controle = new WidgetsListagem();

                string qry = "";
                qry += "SELECT COUNT (*) OVER () AS ROW_COUNT, idtabela, txtabela, flauditoria, idcodigoidioma ";
                qry += "FROM Tabelas ";
                qry += "ORDER BY " + order + " ";
                qry += "OFFSET " + ((pagina - 1) * registros) + " ROWS FETCH NEXT " + registros + " ROWS ONLY";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                if (reader.Read())
                {
                    controle.qtde = Convert.ToInt32(reader["ROW_COUNT"]);
                    controle.colunas = "25,25,25,25";
                    controle.exibe = "1,1,1,1";

                    while (reader.Read())
                    {
                        listagem.Add(new Tabelas()
                        {
                            idtabela = new Variable(value: Convert.ToInt32(reader["idtabela"])),
                            txtabela = new Variable(value: Convert.ToString(reader["txtabela"])),
                            flauditoria = new Variable(value: Convert.ToBoolean(reader["flauditoria"])),
                            idcodigoidioma = new Variable(value: Convert.ToInt32(reader["idcodigoidioma"]))
                        });
                    }
                }
                reader.Close();
                session.Close();

                return (listagem, controle);
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}