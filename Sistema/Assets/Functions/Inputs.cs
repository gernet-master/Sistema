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
        public string mask { get; set; }
        public string placeholder { get; set; }
        public string keyUp { get; set; }
        public string blur { get; set; }
        public bool addButton { get; set; }
        public string buttonTooltip { get; set; }
        public string buttonClick { get; set; }
        public string buttonClass { get; set; }
        public string buttonValue { get; set; }

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
            this.mask = "";
            this.placeholder = "";
            this.keyUp = "";
            this.blur = "";
            this.addButton = false;
            this.buttonTooltip = "";
            this.buttonClick = "";
            this.buttonClass = "";
            this.buttonValue = "";
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
                    if (Utils.Null(this.label, "") != "") { inp += "<label for='" + this.id + "' class='" + (this.required ? "required" : "") + "'>" + this.label + "</label>"; }

                    // Se possuir botão, cria o input-group
                    if (this.addButton) { inp += "<div class='input-group'>"; }

                    // Cria input
                    inp += "<input type='text' ";

                    // Classe
                    if (Utils.Null(this.className, "") != "") { inp += " class='" + this.className + " ' "; }

                    // ID
                    if (Utils.Null(this.id, "") != "") { inp += " id='" + this.id + "' name='" + this.id + "' "; }

                    // Validação
                    if (Utils.Null(this.validation, "") != "") { inp += " validate='" + this.validation + "' "; }

                    // MaxLenght
                    if (this.maxLenght > 0) { inp += " maxlength='" + this.maxLenght + "' "; }

                    // Mascara
                    if (Utils.Null(this.mask, "") != "") { inp += " mask='" + this.mask + "' "; }

                    // Placeholder
                    if (Utils.Null(this.placeholder, "") != "") { inp += " placeholder='" + this.placeholder + "' "; }

                    // KeyUp
                    if (Utils.Null(this.keyUp, "") != "") { inp += " onKeyUp='" + this.keyUp + "' "; }

                    // Blur
                    if (Utils.Null(this.blur, "") != "") { inp += " onBlur='" + this.blur + "' "; }

                    // ReadOnly
                    if (this.readOnly) { inp += " readonly "; }

                    // Valor
                    inp += " value='" + this.inputValue + "' ";

                    // Finaliza input
                    inp += " />";

                    // Se possuir botão, finaliza o input-group
                    if (this.addButton) {
                        inp += "<div class='input-group-append'>";
                        inp += "<button onClick='" + buttonClick + "' class='" + buttonClass + "' type='button' data-toggle='kt-tooltip' title='" + buttonTooltip + "' data-placement='right'>" + buttonValue + "</button>";
                        inp += "</div>";
                        inp += "</div>";
                    }

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