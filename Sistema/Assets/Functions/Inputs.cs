/*
Descrição: Funções para montar os inputs
Data: 01/01/2021 - v.1.0
*/

using System.Collections.Generic;

namespace Functions
{
    // Inputs
    public class Inputs
    {
        // Variáveis
        public string inputType { get; set; }
        public string label { get; set; }
        public string id { get; set; }
        public bool required { get; set; }
        public string validation { get; set; }
        public dynamic options { get; set; }
        public string firstOption { get; set; }
        public string className { get; set; }
        public string inputValue { get; set; }
        public bool readOnly { get; set; }
        public int maxLenght { get; set; }
        public bool inputChecked { get; set; }

        private void Init()
        {
            this.inputType = "";
            this.label = "";
            this.id = "";
            this.required = false;
            this.validation = "";
            this.options = null;
            this.firstOption = "";
            this.className = "";
            this.inputValue = "";
            this.readOnly = false;
            this.maxLenght = 0;
            this.inputChecked = false;
        }

        // Desenha o input
        public string CreateInput()
        {
            // Declaração de variaveis
            string inp = "";

            // Monta input de acordo com o tipo
            switch (this.inputType)
            {
                // Texto
                case "text":

                    inp += "<div class='form-group'>";

                    // Label
                    if (this.label != "") { inp += "<label for='" + this.id + "' class='" + (this.required ? "required" : "") + "'>" + this.label + "</label>"; }

                    // Cria input
                    inp += "<input type='text' ";

                    // Classe
                    inp += " class='" + this.className + " " + (this.validation != "" ? " validate[" + this.validation + "]" : "") + "' ";

                    // ID
                    inp += " id='" + this.id + "' name='" + this.id + "' ";

                    // MaxLenght
                    if (this.maxLenght > 0) { inp += " maxlength='" + this.maxLenght + "' "; }

                    // Valor
                    inp += " value='" + this.inputValue + "' ";

                    // Finaliza input
                    inp += " />";

                    inp += "</div>";

                    break;

                // Select
                case "select":

                    inp += "<div class='form-group'>";

                    // Label
                    if (this.label != "") { inp += "<label for='" + this.id + "' class='" + (this.required ? "required" : "") + "'>" + this.label + "</label>"; }

                    // Cria input
                    inp += "<select ";

                    // Classe
                    inp += " class='" + this.className + " " + (this.validation != "" ? " validate[" + this.validation + "]" : "") + "' ";

                    // ID
                    inp += " id='" + this.id + "' name='" + this.id + "' ";

                    // Finaliza input
                    inp += ">";

                    // Primeira opção
                    if (this.firstOption != "") { inp += "<option value=''>-- " + this.firstOption + " --</option>"; }

                    // Opções
                    if (this.options.Count > 0)
                    {
                        foreach (var o in this.options)
                        {
                            inp += "<option value='" + o.ident.value + "'>" + o.text.value + "</option>";
                        }
                    }

                    // Finaliza input
                    inp += "</select>";

                    inp += "</div>";

                    break;

                // Switch
                case "switch":

                    inp += "<div class='form-group'>";

                    // Label
                    if (this.label != "") { inp += "<label for='" + this.id + "' class='" + (this.required ? "required" : "") + "'>" + this.label + "</label>"; }

                    inp += "<div class='kt-checkbox-list'><span class='kt-switch kt-switch--outline kt-switch--icon kt-switch--success kt-switch--lg'><label>";

                    // Cria input
                    inp += "<input type='checkbox' ";

                    // ID
                    inp += " id='" + this.id + "' name='" + this.id + "' ";

                    // Checked
                    if (this.inputChecked) { inp += " checked "; }

                    // Finaliza input
                    inp += "value='1' />";

                    inp += "<span></span></label ></span></div></div>";

                    break;

                // Inválido
                default:

                    inp += "<label class='ft-red'>" + Language.XmlLang(142, 2).Text + "</label>";

                    break;
            }

            // Reseta a classe
            Init();

            // Retorna o objeto
            return inp;
        }

    }
}