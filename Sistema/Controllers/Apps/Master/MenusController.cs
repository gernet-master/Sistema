/*
Descrição: Controlador para cadastro de menus
Data: 01/01/2021 - v.1.0
*/

using Functions;
using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using Sistema.Models;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sistema.Controllers
{
    public class MenusController : Controller
    {
        // Dashboard
        [Autentication]
        public ActionResult Dashboard()
        {
            return PartialView("~/Views/Apps/Master/Menus/Dashboard.cshtml");
        }

        // Cadastro
        [Autentication]
        public ActionResult Incluir(int id = 0, int id2 = 0, string register = "")
        {
            // Validação para seleção de registro inicial caso esteja configurado para não exibir a dashboard
            if (register != "")
            {
                id = new MenusDB().Paginar(id, register);
            }

            return PartialView("~/Views/Apps/Master/Menus/Incluir.cshtml", new MenusView(id, id2));
        }

        // Excluir
        [Autentication]
        public JsonResult Excluir(int id = 0, int id2 = 0)
        {
            Retorno result = new Retorno();

            // Verifica se foi passado id
            if (id > 0)
            {
                Menus menu = new MenusDB().Buscar(id);

                // Verifica se encontrou o registro
                if (menu.idmenu.value > 0)
                {
                    // Exclusão
                    menu.Excluir();
                    result.success = 1;
                }

                // Identificador não encontrado
                else
                {
                    result.msg = Language.XmlLang(210, 2).Text;
                }
            }

            // Identificador não informado
            else
            {
                result.msg = Language.XmlLang(200, 2).Text;
            }
            return Json(result);
        }

        // Filtro
        [Autentication]
        public ActionResult Filtro()
        {
            return PartialView("~/Views/Apps/Master/Menus/Filtro.cshtml");
        }

        // Gravar
        [Autentication]
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Gravar(FormCollection form)
        {
            Retorno result = new Retorno();

            // Recupera variáveis
            int ident = Convert.ToInt32(Utils.Null(Convert.ToString(Utils.Numbers(form["app_temp_id"])), "0"));
            int codigo_idioma = Convert.ToInt32(Utils.Null(form["frm_codigo_idioma"], "0"));
            int menu_pai = Convert.ToInt32(Utils.Null(form["frm_idmenu_pai"], "0"));
            int aplicativo = Convert.ToInt32(Utils.Null(form["frm_idaplicativo"], "0"));
            int ordem = Convert.ToInt32(Utils.Null(form["frm_ordem"], "0"));
            string icone = Convert.ToString(form["frm_icone"]).Trim();
            int ativo = Convert.ToInt32(Utils.Null(Convert.ToString(Utils.Null(Utils.Null(form["frm_ativo"]), "0")), "0"));
            int master = Convert.ToInt32(Utils.Null(Convert.ToString(Utils.Null(Utils.Null(form["frm_master"]), "0")), "0"));

            // Verifica permissões

            // Valida dados obrigatórios
            if (codigo_idioma == 0)
            {
                result.msg = Language.XmlLang(28, 2).Text + " " + Language.XmlLang(234, 0).Text;
                return Json(result);
            }  
            if (ordem == 0)
            {
                result.msg = Language.XmlLang(246, 2).Text + " " + Language.XmlLang(234, 0).Text;
                return Json(result);
            }

            // Busca registro para gravar
            Menus menu = new MenusDB().Buscar(ident);

            // Carrega valores
            menu.idmenupai.value = menu_pai;
            menu.idaplicativo.value = aplicativo;
            menu.idcodigoidioma.value = codigo_idioma;
            menu.nrordem.value = ordem;
            menu.txicone.value = icone;
            menu.flativo.value = ativo;
            menu.flmaster.value = master;

            // Gravar/Alterar
            if (menu.idmenu.value == 0)
            {
                // Gravar
                int i = menu.Gravar();
                result.success = 1;
                result.msg = Language.XmlLang(213, 2).Text;
                result.ident = i;
            }
            else
            {
                // Alterar
                menu.Alterar(new MenusDB().Buscar(ident));
                result.success = 1;
                result.msg = Language.XmlLang(214, 2).Text;
                result.ident = ident;
            }

            return Json(result);
        }

        // Listagem
        [Autentication]
        [HttpPost]
        public ActionResult ListarWidget(FormCollection form)
        {
            dynamic result = new MenusDB().ListarWidget(form);
            return PartialView("~/Views/Widgets/List.cshtml", new WidgetsView_List(new Widgets().Create(form), "", result));
        }

        // Paginação
        [Autentication]
        public int Paginar(int id = 0, int id2 = 0, string action = "")
        {
            return new MenusDB().Paginar(id, action);
        }
    }
}