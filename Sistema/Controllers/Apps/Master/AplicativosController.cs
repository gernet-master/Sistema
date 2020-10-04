using Functions;
using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using Sistema.Models;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sistema.Controllers
{
    public class AplicativosController : Controller
    {
        // Dashboard do cadastro de aplicativos
        [Autentication]
        public ActionResult Dashboard()
        {
            return PartialView("~/Views/Apps/Master/Aplicativos/Dashboard.cshtml");
        }

        // Formulário do cadastro de aplicativos
        [Autentication]
        public ActionResult Incluir(int id = 0, int id2 = 0)
        {
            return PartialView("~/Views/Apps/Master/Aplicativos/Incluir.cshtml", new AplicativosView(id, id2));
        }

        // Excluir registro
        [Autentication]
        public JsonResult Excluir(int id = 0, int id2 = 0)
        {
            Retorno result = new Retorno();

            // Verifica se foi passado id
            if (id > 0)
            {
                Aplicativos aplicativo = new AplicativosDB().Buscar(id);

                // Verifica se encontrou o registro
                if (aplicativo.idaplicativo.value > 0)
                {
                    // Exclusão
                    aplicativo.Excluir();
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

        // Filtro do cadastro de tabelas
        [Autentication]
        public ActionResult Filtro()
        {
            return PartialView("~/Views/Apps/Master/Aplicativos/Filtro.cshtml");
        }

        // Gravar registro
        [Autentication]
        [HttpPost]
        public JsonResult Gravar(FormCollection form)
        {
            Retorno result = new Retorno();

            // Recupera variáveis
            int ident = Convert.ToInt32(Utils.Null(Convert.ToString(Utils.Numbers(form["app_temp_id"])), "0"));
            string aplicativo = Utils.ClearText(form["frm_aplicativo"], 100);
            string action = Utils.ClearText(form["frm_action"], 20);
            string controller = Utils.ClearText(form["frm_controller"], 20);
            int tabela = Convert.ToInt32(Utils.Null(form["frm_idtabela"], "0"));

            // Verifica permissões

            // Valida dados obrigatórios
            if (aplicativo == "")
            {
                result.msg = Language.XmlLang(230, 2).Text + " " + Language.XmlLang(234, 0).Text;
                return Json(result);
            }
            if (action == "")
            {
                result.msg = Language.XmlLang(231, 2).Text + " " + Language.XmlLang(234, 0).Text;
                return Json(result);
            }
            if (controller == "")
            {
                result.msg = Language.XmlLang(232, 2).Text + " " + Language.XmlLang(234, 0).Text;
                return Json(result);
            }

            // Busca registro para gravar
            Aplicativos reg = new AplicativosDB().Buscar(ident);

            // Carrega valores
            reg.txaplicativo.value = aplicativo;
            reg.txaction.value = action;
            reg.txcontroller.value = controller;
            reg.idtabela.value = tabela;

            // Gravar/Alterar
            if (reg.idaplicativo.value == 0)
            {
                // Gravar
                int i = reg.Gravar();
                result.success = 1;
                result.msg = Language.XmlLang(213, 2).Text;
                result.ident = i;
            }
            else
            {
                // Alterar
                reg.Alterar(new AplicativosDB().Buscar(ident));
                result.success = 1;
                result.msg = Language.XmlLang(214, 2).Text;
                result.ident = ident;
            }

            return Json(result);
        }

        // Página para listagem de registros de tabelas
        [Autentication]
        [HttpPost]
        public ActionResult ListarWidget(FormCollection form)
        {
            dynamic result = new AplicativosDB().ListarWidget(form);
            return PartialView("~/Views/Widgets/List.cshtml", new WidgetsView_List(new Widgets().Create(form), "", result));
        }

        [Autentication]
        public int Paginar(int id = 0, int id2 = 0, string action = "")
        {
            return new AplicativosDB().Paginar(id, action);
        }
    }
}