/*
Descrição: SQL para aplicativos
Data: 01/01/2021 - v.1.0
*/

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
    public class AplicativosDB : Session
    {
        // Gravar nova tabela
        public int Gravar(Aplicativos rs)
        {
            try
            {
                int idaplicativo = 0;

                string qry = "";
                qry += "INSERT INTO Aplicativos (txaplicativo, txaction, txcontroller, idtabela) output INSERTED.idtabela ";
                qry += "VALUES ('" + rs.txaplicativo.value + "', '" + rs.txaction.value + "', '" + rs.txcontroller.value + "', " + rs.idtabela.value + ")";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);

                // Retorna o identificador
                idaplicativo = query.ExecuteScalar();

                // Atualizar registro para registro de auditoria
                rs.idtabela.value = idaplicativo;

                session.Close();

                // Auditoria
                Audit.Check("Aplicativos", "I", idaplicativo, rs);
                
                return idaplicativo;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        // Alterar tabela
        public void Alterar(Aplicativos rs, Aplicativos temp)
        {
            try
            {
                string qry = "";
                qry += "UPDATE Aplicativos ";
                qry += "SET txaplicativo = '" + rs.txaplicativo.value + "', txaction = '" + rs.txaction.value + "', txcontroller = '" + rs.txcontroller.value + "', idtabela = " + rs.idtabela.value + " ";
                qry += "WHERE idaplicativo = " + rs.idaplicativo.value;

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                query.ExecuteUpdate();

                // Auditoria
                Audit.Check("Aplicativos", "U", rs.idaplicativo.value, rs, temp);

                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        // Excluir tabela
        public void Excluir(Aplicativos rs)
        {
            try
            {
                string qry = "";
                qry += "DELETE FROM Aplicativos ";
                qry += "WHERE idaplicativo = " + rs.idaplicativo.value;

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                query.ExecuteUpdate();

                // Auditoria
                Audit.Check("Aplicativos", "D", rs.idaplicativo.value, rs);

                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        // Lista Aplicativos
        public List<Aplicativos> Listar()
        {
            try
            {
                List<Aplicativos> apps = new List<Aplicativos>();

                string qry = "";
                qry += "SELECT * FROM Aplicativos";

                Connection session = new Connection();                
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    apps.Add(new Aplicativos()
                    {
                        idaplicativo = new Variable(value: Convert.ToInt32(reader["idaplicativo"])),
                        txaplicativo = new Variable(value: Convert.ToString(reader["txaplicativo"])),
                        txaction = new Variable(value: Convert.ToString(reader["txaction"])),
                        txcontroller = new Variable(value: Convert.ToString(reader["txcontroller"])),
                        idtabela = new Variable(value: Convert.ToInt32(reader["idtabela"]))
                    });
                }
                reader.Close();
                session.Close();

                return apps;
            }
            catch (Exception error)
            {
                throw error;
            }
        }        

        // Busca pelo código
        public Aplicativos Buscar(int idaplicativo = 0)
        {
            try
            {
                Aplicativos apps = new Aplicativos();

                string qry = "";
                qry += "SELECT a.*, t.txtabela FROM Aplicativos a LEFT JOIN Tabelas t ON t.idtabela = a.idtabela WHERE a.idaplicativo = " + idaplicativo;

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);

                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    apps.idaplicativo.value = Convert.ToInt32(reader["idaplicativo"]);
                    apps.txaplicativo.value = Convert.ToString(reader["txaplicativo"]);
                    apps.txaction.value = Convert.ToString(reader["txaction"]);
                    apps.txcontroller.value = Convert.ToString(reader["txcontroller"]);
                    apps.idtabela.value = Convert.ToInt32(reader["idtabela"]);
                    apps.txtabela.value = Convert.ToString(reader["txtabela"]);
                }
                reader.Close();
                session.Close();

                return apps;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        // Busca pelo action e controller
        public Aplicativos BuscarActionController(string action = "", string controller = "")
        {
            try
            {
                Aplicativos apps = new Aplicativos();

                string qry = "";
                qry += "SELECT * FROM Aplicativos WHERE txaction = '" + action + "' AND txcontroller = '" + controller + "'";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);

                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    apps.idaplicativo.value = Convert.ToInt32(reader["idaplicativo"]);
                    apps.txaplicativo.value = Convert.ToString(reader["txaplicativo"]);
                    apps.txaction.value = Convert.ToString(reader["txaction"]);
                    apps.txcontroller.value = Convert.ToString(reader["txcontroller"]);
                    apps.idtabela.value = Convert.ToInt32(reader["idtabela"]);
                }
                reader.Close();
                session.Close();

                return apps;
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
                if (action == "F") { qry += "SELECT TOP 1 idaplicativo FROM Aplicativos ORDER BY idaplicativo ASC"; }
                if (action == "P") { qry += "SELECT TOP 1 idaplicativo FROM Aplicativos WHERE idaplicativo < " + id + " ORDER BY idaplicativo DESC"; }
                if (action == "N") { qry += "SELECT TOP 1 idaplicativo FROM Aplicativos WHERE idaplicativo > " + id + " ORDER BY idaplicativo ASC"; }
                if ((action == "L") || (action == "")) { qry += "SELECT TOP 1 idaplicativo FROM Aplicativos ORDER BY idaplicativo DESC"; }

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);

                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    ret = Convert.ToInt32(reader["idaplicativo"]);
                    
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

        // Listagem
        public (List<Aplicativos>, WidgetsListConfig) ListarWidget(FormCollection form = null)
        {
            try
            {
                List<Aplicativos> list = new List<Aplicativos>();
                WidgetsListConfig control = new WidgetsListConfig();
                string filter = "";

                // Parametros de pesquisa
                int page = 1;
                int registers = 10;
                string order = "a.txaplicativo";
                string direction = "asc";

                // Filtro de resultados
                if (form != null)
                {
                    if (form.AllKeys.Contains("filter_txaplicativo"))
                    {
                        string filter_txaplicativo = Utils.ClearText(form["filter_txaplicativo"], 50);
                        if (filter_txaplicativo.Length > 0) { filter += "AND A.txaplicativo LIKE '%" + filter_txaplicativo.Replace(" ", "%") + "%' "; }
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
                control.columns = new int[6] { 0, 30, 25, 25, 0, 20 };
                control.show = new int[6] { 0, 1, 1, 1, 0, 1 };
                control.headers = new string[6] { "199", "230", "231", "232", "143", "144" };
                control.orderfields = new string[6] { "a.idaplicativo", "a.txaplicativo", "a.txaction", "a.txcontroller", "a.idtabela", "t.txtabela" };
                control.fields = new string[6] { "idaplicativo", "txaplicativo", "txaction", "txcontroller", "idtabela", "txtabela" };
                control.formatFields = new string[6] { "master", "", "", "", "", "" };

                // Query
                string qry = "";
                qry += "SELECT COUNT (*) OVER () AS ROW_COUNT, a.idaplicativo, a.txaplicativo, a.txaction, a.txcontroller, a.idtabela, t.txtabela ";
                qry += "FROM Aplicativos a ";
                qry += "LEFT JOIN Tabelas t ON t.idtabela = a.idtabela ";
                qry += "WHERE 1=1 " + filter;
                qry += "ORDER BY " + Utils.Null(order, "a.txaplicativo") + " " + direction + " ";
                qry += "OFFSET " + ((page - 1) * registers) + " ROWS FETCH NEXT " + registers + " ROWS ONLY";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                if (reader.Read())
                {
                    control.count = Convert.ToInt32(reader["ROW_COUNT"]);

                    list.Add(new Aplicativos()
                    {
                        idaplicativo = new Variable(value: Convert.ToInt32(reader["idaplicativo"])),
                        txaplicativo = new Variable(value: Convert.ToString(reader["txaplicativo"])),
                        txaction = new Variable(value: Convert.ToString(reader["txaction"])),
                        txcontroller = new Variable(value: Convert.ToString(reader["txcontroller"])),
                        idtabela = new Variable(value: Convert.ToInt32(reader["idtabela"])),
                        txtabela = new Variable(value: Convert.ToString(reader["txtabela"]))
                    });

                    while (reader.Read())
                    {
                        list.Add(new Aplicativos()
                        {
                            idaplicativo = new Variable(value: Convert.ToInt32(reader["idaplicativo"])),
                            txaplicativo = new Variable(value: Convert.ToString(reader["txaplicativo"])),
                            txaction = new Variable(value: Convert.ToString(reader["txaction"])),
                            txcontroller = new Variable(value: Convert.ToString(reader["txcontroller"])),
                            idtabela = new Variable(value: Convert.ToInt32(reader["idtabela"])),
                            txtabela = new Variable(value: Convert.ToString(reader["txtabela"]))
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