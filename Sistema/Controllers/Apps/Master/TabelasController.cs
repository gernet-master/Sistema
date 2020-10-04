using Functions;
using Sistema.Assets.DB;
using Sistema.Assets.Entities;
using Sistema.Models;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sistema.Controllers
{
    public class TabelasController : Controller
    {
        // Dashboard do cadastro de tabelas
        [Autentication]
        public ActionResult Dashboard()
        {
            return PartialView("~/Views/Apps/Master/Tabelas/Dashboard.cshtml");
        }

        // Formulário do cadastro de tabelas
        [Autentication]
        public ActionResult Incluir(int id = 0, int id2 = 0, string register = "")
        {
            // Validação para seleção de registro inicial caso esteja configurado para não exibir a dashboard
            if (register != "")
            {
                id = new TabelasDB().Paginar(id, register);
            }

            return PartialView("~/Views/Apps/Master/Tabelas/Incluir.cshtml", new TabelasView(id, id2));
        }

        // Excluir registro
        [Autentication]
        public JsonResult Excluir(int id = 0, int id2 = 0)
        {
            Retorno result = new Retorno();
            
            // Verifica se foi passado id
            if (id > 0)
            {
                Tabelas tabela = new TabelasDB().Buscar(id);

                // Verifica se encontrou o registro
                if (tabela.idtabela.value > 0)
                {
                    // Exclusão
                    tabela.Excluir();
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
            return PartialView("~/Views/Apps/Master/Tabelas/Filtro.cshtml");
        }

        // Gravar registro
        [Autentication]
        [HttpPost]
        public JsonResult Gravar(FormCollection form)
        {
            Retorno result = new Retorno();

            // Recupera variáveis
            int ident = Convert.ToInt32(Utils.Null(Convert.ToString(Utils.Numbers(form["app_temp_id"])), "0"));
            string tabela = Utils.ClearText(form["frm_tabela"], 200);
            int auditoria = Convert.ToInt32(Utils.Null(Convert.ToString(Utils.Null(Utils.Null(form["frm_auditoria"]),"0")), "0")); 
            int codigo_idioma = Convert.ToInt32(Utils.Null(form["frm_codigo_idioma"], "0"));

            // Verifica permissões

            // Valida dados obrigatórios
            if (tabela == "") {
                result.msg = Language.XmlLang(138, 2).Text + " " + Language.XmlLang(234, 0).Text;
                return Json(result);
            }

            // Busca registro para gravar
            Tabelas reg = new TabelasDB().Buscar(ident);

            // Carrega valores
            reg.txtabela.value = tabela;
            reg.idcodigoidioma.value = codigo_idioma;
            reg.flauditoria.value = auditoria;

            // Gravar/Alterar
            if (reg.idtabela.value == 0)
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
                reg.Alterar(new TabelasDB().Buscar(ident));
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
            dynamic result = new TabelasDB().ListarWidget(form);
            return PartialView("~/Views/Widgets/List.cshtml", new WidgetsView_List(new Widgets().Create(form), "", result));
        }

        [Autentication]
        public int Paginar(int id = 0, int id2 = 0, string action = "")
        {
            return new TabelasDB().Paginar(id, action);
        }
    }
}