/*
Descrição: SQL para menus
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
    public class MenusDB : Session
    {
        // Gravar novo menu
        public int Gravar(Menus rs)
        {
            try
            {
                int idmenu = 0;

                string qry = "";
                qry += "INSERT INTO Menus (idmenupai, idaplicativo, idcodigoidioma, txicone, nrordem, flativo, flmaster) output INSERTED.idmenu ";
                qry += "VALUES (" + rs.idmenupai.value + ", " + rs.idaplicativo.value + ", " + rs.idcodigoidioma.value + ", '" + rs.txicone.value + "', ";
                qry += rs.nrordem.value + ", " + rs.flativo.value + ", " + rs.flmaster.value + ")";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);

                // Retorna o identificador
                idmenu = query.ExecuteScalar();

                // Atualizar registro para registro de auditoria
                rs.idmenu.value = idmenu;

                session.Close();

                // Auditoria
                Audit.Check("Menus", "I", idmenu, rs);
                
                return idmenu;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        // Alterar menu
        public void Alterar(Menus rs, Menus temp)
        {
            try
            {
                string qry = "";
                qry += "UPDATE Menus ";
                qry += "SET idmenupai = " + rs.idmenupai.value + ", idaplicativo = " + rs.idaplicativo.value + ", idcodigoidioma = " + rs.idcodigoidioma.value + ", ";
                qry += "txicone = '" + rs.txicone.value + "', nrordem = " + rs.nrordem.value + ", flativo = " + rs.flativo.value + ", flmaster = " + rs.flmaster.value + " ";
                qry += "WHERE idmenu = " + rs.idmenu.value;

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                query.ExecuteUpdate();

                // Auditoria
                Audit.Check("Menus", "U", rs.idmenu.value, rs, temp);

                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        // Excluir menu
        public void Excluir(Menus rs)
        {
            try
            {
                string qry = "";
                qry += "DELETE FROM Menus ";
                qry += "WHERE idmenu = " + rs.idmenu.value;

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                query.ExecuteUpdate();

                // Auditoria
                Audit.Check("Menus", "D", rs.idmenu.value, rs);

                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        // Listar Menus
        public List<Menus> Listar()
        {
            try
            {
                List<Menus> menus = new List<Menus>();

                string qry = "";
                qry += "SELECT * FROM Menus ORDER BY nrordem";

                Connection session = new Connection();                
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    menus.Add(new Menus()
                    {
                        idmenu = new Variable(value: Convert.ToInt32(reader["idmenu"])),
                        idmenupai = new Variable(value: Convert.ToInt32(reader["idmenupai"])),
                        idcodigoidioma = new Variable(value: Convert.ToInt32(reader["idcodigoidioma"])),
                        idaplicativo = new Variable(value: Convert.ToInt32(reader["idaplicativo"])),
                        txicone = new Variable(value: Convert.ToString(reader["txicone"])),
                        nrordem = new Variable(value: Convert.ToInt32(reader["nrordem"])),
                        flativo = new Variable(value: Convert.ToInt32(reader["flativo"])),
                        flmaster = new Variable(value: Convert.ToInt32(reader["flmaster"]))
                    });
                }
                reader.Close();
                session.Close();

                return menus;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        // Listar Menus por nivel
        public List<Menus> ListarPorNivel(int idmenupai)
        {
            try
            {
                List<Menus> menus = new List<Menus>();

                string qry = "";
                qry += "SELECT * FROM Menus WHERE idmenupai = " + idmenupai + " AND flativo = 1 ORDER BY nrordem";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                while (reader.Read())
                {
                    menus.Add(new Menus()
                    {
                        idmenu = new Variable(value: Convert.ToInt32(reader["idmenu"])),
                        idmenupai = new Variable(value: Convert.ToInt32(reader["idmenupai"])),
                        idcodigoidioma = new Variable(value: Convert.ToInt32(reader["idcodigoidioma"])),
                        idaplicativo = new Variable(value: Convert.ToInt32(reader["idaplicativo"])),
                        txicone = new Variable(value: Convert.ToString(reader["txicone"])),
                        nrordem = new Variable(value: Convert.ToInt32(reader["nrordem"])),
                        flativo = new Variable(value: Convert.ToInt32(reader["flativo"])),
                        flmaster = new Variable(value: Convert.ToInt32(reader["flmaster"])),
                        menus_filhos = new Variable(value: new MenusDB().ListarPorNivel(Convert.ToInt32(reader["idmenu"]))),
                        aplicativo = new Variable(value: (Convert.ToInt32(reader["idaplicativo"]) == 0 ? null : new AplicativosDB().Buscar(Convert.ToInt32(reader["idaplicativo"]))))
                    });
                }
                reader.Close();
                session.Close();

                return menus;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        // Busca pelo action e controller do aplicativo para retornar o nome do menu
        public int BuscarActionController(string action = "", string controller = "")
        {
            try
            {
                int idioma = 0;

                string qry = "";
                qry += "SELECT idcodigoidioma FROM Menus ";
                qry += "WHERE idaplicativo = (SELECT idaplicativo FROM Aplicativos WHERE txaction = '" + action + "' AND txcontroller = '" + controller + "')";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);

                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    idioma = Convert.ToInt32(reader["idcodigoidioma"]);
                    
                }
                reader.Close();
                session.Close();

                return idioma;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        // Busca pelo código
        public Menus Buscar(int idmenu = 0)
        {
            try
            {
                Menus menus = new Menus();

                string qry = "";
                qry += "SELECT m.*, ISNULL(mp.idcodigoidioma, 0) as idcodigoidioma_pai, a.txaplicativo ";
                qry += "FROM Menus m ";
                qry += "LEFT JOIN menus mp ON mp.idmenu = m.idmenupai ";
                qry += "LEFT JOIN aplicativos a ON a.idaplicativo = m.idaplicativo ";
                qry += "WHERE m.idmenu = " + idmenu;

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);

                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    menus.idmenu = new Variable(value: Convert.ToInt32(reader["idmenu"]));
                    menus.idmenupai = new Variable(value: Convert.ToInt32(reader["idmenupai"]));
                    menus.idcodigoidioma = new Variable(value: Convert.ToInt32(reader["idcodigoidioma"]));
                    menus.idaplicativo = new Variable(value: Convert.ToInt32(reader["idaplicativo"]));
                    menus.txicone = new Variable(value: Convert.ToString(reader["txicone"]));
                    menus.nrordem = new Variable(value: Convert.ToInt32(reader["nrordem"]));
                    menus.flativo = new Variable(value: Convert.ToInt32(reader["flativo"]));
                    menus.flmaster = new Variable(value: Convert.ToInt32(reader["flmaster"]));
                    menus.menupai = new Variable(value: Convert.ToInt32(reader["idcodigoidioma_pai"]));
                    menus.aplicativo = new Variable(value: Convert.ToString(reader["txaplicativo"]));
                    menus.menu_nome = new Variable(value: Language.XmlLang(Convert.ToInt32(Utils.Null(Convert.ToString(reader["idcodigoidioma"]), "0")), 2).Text);
                    menus.menupai_nome = new Variable(value: Language.XmlLang(Convert.ToInt32(Utils.Null(Convert.ToString(reader["idcodigoidioma_pai"]), "0")), 2).Text);
                }
                reader.Close();
                session.Close();

                return menus;
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
                if (action == "F") { qry += "SELECT TOP 1 idmenu FROM Menus ORDER BY idmenu ASC"; }
                if (action == "P") { qry += "SELECT TOP 1 idmenu FROM Menus WHERE idmenu < " + id + " ORDER BY idmenu DESC"; }
                if (action == "N") { qry += "SELECT TOP 1 idmenu FROM Menus WHERE idmenu > " + id + " ORDER BY idmenu ASC"; }
                if ((action == "L") || (action == "")) { qry += "SELECT TOP 1 idmenu FROM Menus ORDER BY idmenu DESC"; }

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);

                IDataReader reader = query.ExecuteQuery();
                if (reader.Read())
                {
                    ret = Convert.ToInt32(reader["idmenu"]);
                    
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
        public (List<Menus>, WidgetsListConfig) ListarWidget(FormCollection form = null)
        {
            try
            {
                List<Menus> list = new List<Menus>();
                WidgetsListConfig control = new WidgetsListConfig();
                string filter = "";

                // Parametros de pesquisa
                int page = 1;
                int registers = 10;
                string order = "m.idmenu";
                string direction = "asc";

                // Filtro de resultados
                if (form != null)
                {

                    // Ativo
                    if (form.AllKeys.Contains("filter_flativo"))
                    {
                        filter += "AND m.flativo = " + Convert.ToInt32(Utils.Null(form["filter_flativo"], "0")) + " ";
                    }

                    // Master
                    if (form.AllKeys.Contains("filter_flmaster"))
                    {
                        filter += "AND m.flmaster = " + Convert.ToInt32(Utils.Null(form["filter_flmaster"], "0")) + " ";
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
                control.columns = new int[8] { 0, 20, 20, 20, 10, 10, 10, 10 };
                control.show = new int[8] { 0, 1, 1, 1, 1, 1, 1, 1 };
                control.headers = new string[8] { "28", "139", "245", "230", "247", "246", "33", "34" };
                control.orderfields = new string[8] { "m.idmenu", "m.idcodigoidioma", "m.idmenupai", "a.txaplicativo", "", "m.nrordem", "m.flativo", "m.flmaster" };
                control.fields = new string[8] { "idmenu", "idcodigoidioma", "menupai", "aplicativo", "txicone", "nrordem", "flativo", "flmaster" };
                control.formatFields = new string[8] { "master", "language", "language", "", "", "", "boolean", "boolean" };

                // Query
                string qry = "";
                qry += "SELECT COUNT (*) OVER () AS ROW_COUNT, m.idmenu, m.idcodigoidioma, m.idmenupai, m.idaplicativo, m.txicone, m.nrordem, ";
                qry += "m.flativo, m.flmaster, ISNULL(mp.idcodigoidioma, 0) as idcodigoidioma_pai, a.txaplicativo ";
                qry += "FROM Menus m ";
                qry += "LEFT JOIN menus mp ON mp.idmenu = m.idmenupai ";
                qry += "LEFT JOIN aplicativos a ON a.idaplicativo = m.idaplicativo ";
                qry += "WHERE 1=1 " + filter;
                qry += "ORDER BY " + Utils.Null(order, "m.idmenu") + " " + direction + " ";
                qry += "OFFSET " + ((page - 1) * registers) + " ROWS FETCH NEXT " + registers + " ROWS ONLY";

                Connection session = new Connection();
                Query query = session.CreateQuery(qry);
                IDataReader reader = query.ExecuteQuery();

                if (reader.Read())
                {
                    control.count = Convert.ToInt32(reader["ROW_COUNT"]);

                    list.Add(new Menus()
                    {
                        idmenu = new Variable(value: Convert.ToInt32(reader["idmenu"])),
                        idmenupai = new Variable(value: Convert.ToInt32(reader["idmenupai"])),
                        idcodigoidioma = new Variable(value: Convert.ToInt32(reader["idcodigoidioma"])),
                        idaplicativo = new Variable(value: Convert.ToInt32(reader["idaplicativo"])),
                        txicone = new Variable(value: Convert.ToString(reader["txicone"])),
                        nrordem = new Variable(value: Convert.ToInt32(reader["nrordem"])),
                        flativo = new Variable(value: Convert.ToInt32(reader["flativo"])),
                        flmaster = new Variable(value: Convert.ToInt32(reader["flmaster"])),
                        menupai = new Variable(value: Convert.ToInt32(reader["idcodigoidioma_pai"])),
                        aplicativo = new Variable(value: Convert.ToString(reader["txaplicativo"])),
                        menu_nome = new Variable(value: Language.XmlLang(Convert.ToInt32(Utils.Null(Convert.ToString(reader["idcodigoidioma"]), "0")), 2).Text),
                        menupai_nome = new Variable(value: Language.XmlLang(Convert.ToInt32(Utils.Null(Convert.ToString(reader["idcodigoidioma_pai"]), "0")), 2).Text)
                    });

                    while (reader.Read())
                    {
                        list.Add(new Menus()
                        {
                            idmenu = new Variable(value: Convert.ToInt32(reader["idmenu"])),
                            idmenupai = new Variable(value: Convert.ToInt32(reader["idmenupai"])),
                            idcodigoidioma = new Variable(value: Convert.ToInt32(reader["idcodigoidioma"])),
                            idaplicativo = new Variable(value: Convert.ToInt32(reader["idaplicativo"])),
                            txicone = new Variable(value: Convert.ToString(reader["txicone"])),
                            nrordem = new Variable(value: Convert.ToInt32(reader["nrordem"])),
                            flativo = new Variable(value: Convert.ToInt32(reader["flativo"])),
                            flmaster = new Variable(value: Convert.ToInt32(reader["flmaster"])),
                            menupai = new Variable(value: Convert.ToInt32(reader["idcodigoidioma_pai"])),
                            aplicativo = new Variable(value: Convert.ToString(reader["txaplicativo"])),
                            menu_nome = new Variable(value: Language.XmlLang(Convert.ToInt32(Utils.Null(Convert.ToString(reader["idcodigoidioma"]), "0")), 2).Text),
                            menupai_nome = new Variable(value: Language.XmlLang(Convert.ToInt32(Utils.Null(Convert.ToString(reader["idcodigoidioma_pai"]), "0")), 2).Text)
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