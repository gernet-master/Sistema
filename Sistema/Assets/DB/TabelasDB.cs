using Functions;
using Sistema.Assets.Entities;
using Sistema.Assets.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                qry += "INSERT INTO Tabelas (txtabela, flauditoria, idcodigoidioma) output INSERTED.idtabela ";
                qry += "VALUES ('" + rs.txtabela.value + "', " + rs.flauditoria.value + ", " + rs.idcodigoidioma.value + ")";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);

                // Retorna o identificador
                idtabela = query.ExecuteScalar();

                // Atualizar registro para registro de auditoria
                rs.idtabela.value = idtabela;

                session.Close();

                // Auditoria
                Audit.Check("Tabelas", "I", idtabela, rs);
                
                return idtabela;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        // Alterar tabela
        public void Alterar(Tabelas rs, Tabelas temp)
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

                // Auditoria
                Audit.Check("Tabelas", "U", rs.idtabela.value, rs, temp);

                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        // Excluir tabela
        public void Excluir(Tabelas rs)
        {
            try
            {
                string qry = "";
                qry += "DELETE FROM Tabelas ";
                qry += "WHERE idtabela = " + rs.idtabela.value;

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                query.ExecuteUpdate();

                // Auditoria
                Audit.Check("Tabelas", "D", rs.idtabela.value, rs);

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
                        flauditoria = new Variable(value: Convert.ToInt32(reader["flauditoria"])),
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
                Tabelas tabs = new Tabelas();

                string qry = "";
                qry += "SELECT * FROM Tabelas WHERE idtabela = " + idtabela;

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);

                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    tabs.idtabela.value = Convert.ToInt32(reader["idtabela"]);
                    tabs.txtabela.value = Convert.ToString(reader["txtabela"]);
                    tabs.flauditoria.value = Convert.ToInt32(reader["flauditoria"]);
                    tabs.idcodigoidioma.value = Convert.ToInt32(reader["idcodigoidioma"]);
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

        // Busca pelo titulo
        public Tabelas Buscar(string txtabela = "")
        {
            try
            {
                Tabelas tabs = new Tabelas();

                string qry = "";
                qry += "SELECT * FROM Tabelas WHERE txtabela = '" + txtabela + "'";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);

                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    tabs.idtabela.value = Convert.ToInt32(reader["idtabela"]);
                    tabs.txtabela.value = Convert.ToString(reader["txtabela"]);
                    tabs.flauditoria.value = Convert.ToInt32(reader["flauditoria"]);
                    tabs.idcodigoidioma.value = Convert.ToInt32(reader["idcodigoidioma"]);
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

        // Paginar registros
        public int Paginar(int id = 0, string action = "")
        {
            try
            {
                int ret = id;

                string qry = "";
                if (action == "F") { qry += "SELECT TOP 1 idtabela FROM tabelas ORDER BY idtabela ASC"; }
                if (action == "P") { qry += "SELECT TOP 1 idtabela FROM tabelas WHERE idtabela < " + id + " ORDER BY idtabela DESC"; }
                if (action == "N") { qry += "SELECT TOP 1 idtabela FROM tabelas WHERE idtabela > " + id + " ORDER BY idtabela ASC"; }
                if ((action == "L") || (action == "")) { qry += "SELECT TOP 1 idtabela FROM tabelas ORDER BY idtabela DESC"; }

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);

                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    ret = Convert.ToInt32(reader["idtabela"]);
                    
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
        public (List<Tabelas>, WidgetsListConfig) ListarWidget(FormCollection form = null)
        {
            try
            {
                List<Tabelas> list = new List<Tabelas>();
                WidgetsListConfig control = new WidgetsListConfig();
                string filter = "";

                // Parametros de pesquisa
                int page = 1;
                int registers = 10;
                string order = "txtabela";
                string direction = "asc";

                // Filtro de resultados
                if (form != null)
                {
                    if (form.AllKeys.Contains("filter_txtabela"))
                    {
                        string filter_txtabela = Utils.ClearText(form["filter_txtabela"], 50);
                        if (filter_txtabela.Length > 0) { filter += "AND txtabela LIKE '%" + filter_txtabela.Replace(" ", "%") + "%' "; }
                    }

                    if (form.AllKeys.Contains("widget_temp_page")) { page = Convert.ToInt32(form["widget_temp_page"]); }
                    if (form.AllKeys.Contains("widget_temp_registers")) { registers = Convert.ToInt32(form["widget_temp_registers"]); }
                    if (form.AllKeys.Contains("widget_temp_order")) { order = form["widget_temp_order"]; } 
                    if (form.AllKeys.Contains("widget_temp_direction")) { direction = form["widget_temp_direction"]; }
                }

                // Controle de widget
                control.count = 0;
                control.registers = registers;
                control.page = page;
                control.order = order;
                control.direction = direction;
                control.columns = new int[4] { 0, 34, 33, 34 };
                control.show = new int[4] { 0, 1, 1, 1 };
                control.headers = new string[4] { "199", "144", "141", "145" };
                control.fields = new string[4] { "idtabela", "txtabela", "flauditoria", "idcodigoidioma" };
                control.formatFields = new string[4] { "master", "", "boolean", "language" };

                // Query
                string qry = "";
                qry += "SELECT COUNT (*) OVER () AS ROW_COUNT, idtabela, txtabela, flauditoria, idcodigoidioma ";
                qry += "FROM Tabelas ";
                qry += "WHERE 1=1 " + filter;
                qry += "ORDER BY " + Utils.Null(order, "txtabela") + " " + direction + " ";
                qry += "OFFSET " + ((page - 1) * registers) + " ROWS FETCH NEXT " + registers + " ROWS ONLY";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                if (reader.Read())
                {
                    control.count = Convert.ToInt32(reader["ROW_COUNT"]);

                    list.Add(new Tabelas()
                    {
                        idtabela = new Variable(value: Convert.ToInt32(reader["idtabela"])),
                        txtabela = new Variable(value: Convert.ToString(reader["txtabela"])),
                        flauditoria = new Variable(value: Convert.ToBoolean(reader["flauditoria"])),
                        idcodigoidioma = new Variable(value: Convert.ToInt32(reader["idcodigoidioma"]))
                    });

                    while (reader.Read())
                    {
                        list.Add(new Tabelas()
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

                return (list, control);
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}