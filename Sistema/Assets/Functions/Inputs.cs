/*
Descrição: Funções para montar os inputs
Data: 01/01/2021 - v.1.0
*/

using System;
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
        public string optionsFields { get; set; }
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
        public string change { get; set; }
        public string id2 { get; set; }
        public string inputValue2 { get; set; }
        public string findClick { get; set; }
        public string removeClick { get; set; }
        public bool showAll { get; set; }
        public bool inputCheckedAll { get; set; }
        public string defaultValue { get; set; }
        public bool noClear { get; set; }

        private void Init()
        {
            this.inputType = "";
            this.label = "";
            this.id = "";
            this.required = false;
            this.validation = "";
            this.options = null;
            this.optionsFields = "";
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
            this.change = "";
            this.inputValue2 = "";
            this.id2 = "";
            this.findClick = "";
            this.removeClick = "";
            this.showAll = false;
            this.inputCheckedAll = false;
            this.defaultValue = "";
            this.noClear = false;

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

                    // No clear
                    if (this.noClear) { inp += " noclear='true' "; }

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

                // Textarea
                case "textarea":

                    inp += "<div class='form-group'>";

                    // Label
                    if (Utils.Null(this.label, "") != "") { inp += "<label for='" + this.id + "' class='" + (this.required ? "required" : "") + "'>" + this.label + "</label>"; }

                    // Cria input
                    inp += "<textarea ";

                    // Classe
                    if (Utils.Null(this.className, "") != "") { inp += " class='" + this.className + " ' "; }

                    // ID
                    if (Utils.Null(this.id, "") != "") { inp += " id='" + this.id + "' name='" + this.id + "' "; }

                    // Validação
                    if (Utils.Null(this.validation, "") != "") { inp += " validate='" + this.validation + "' "; }

                    // MaxLenght
                    if (this.maxLenght > 0) { inp += " maxlength='" + this.maxLenght + "' "; }

                    // Placeholder
                    if (Utils.Null(this.placeholder, "") != "") { inp += " placeholder='" + this.placeholder + "' "; }

                    // ReadOnly
                    if (this.readOnly) { inp += " readonly "; }

                    // No clear
                    if (this.noClear) { inp += " noclear='true' "; }

                    inp += ">";

                    // Valor
                    inp += this.inputValue;

                    // Finaliza input
                    inp += " </textarea>";

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
                    if (Utils.Null(this.className, "") != "") { inp += " class='" + this.className + " ' "; }

                    // ID
                    if (Utils.Null(this.id, "") != "") { inp += " id='" + this.id + "' name='" + this.id + "' "; }

                    // Validação
                    if (Utils.Null(this.validation, "") != "") { inp += " validate='" + this.validation + "' "; }

                    // Finaliza input
                    inp += ">";

                    // Primeira opção
                    if (this.firstOption != "") { inp += "<option value=''>-- " + this.firstOption + " --</option>"; }

                    // Pega os campos para utilizar como identificador e valor
                    var vals = optionsFields.Split(',');

                    // Opções
                    if (this.options != null)
                    {
                        if (this.options.Count > 0)
                        {
                            foreach (var o in this.options)
                            {
                                Variable value = o.GetField(vals[0].Trim());
                                Variable text = o.GetField(vals[1].Trim());

                                inp += "<option value='" + value.value + "' ";

                                // Marca o item se possuir valor
                                if (Utils.Null(this.inputValue, "") != "")
                                {
                                    if (Convert.ToString(this.inputValue) == Convert.ToString(value.value))
                                    {
                                        inp += " selected ";
                                    }
                                }

                                inp += ">" + text.value + "</option>";
                            }
                        }
                    }

                    // No clear
                    if (this.noClear) { inp += " noclear='true' "; }

                    // Finaliza input
                    inp += "</select>";

                    inp += "</div>";

                    break;

                // Localização
                case "find":

                    inp += "<div class='form-group'>";

                    // Label
                    if (this.label != "") { inp += "<label for='" + this.id + "' class='" + (this.required ? "required" : "") + "'>" + this.label + "</label>"; }

                    // Campo escondido para armazenar o identificador
                    inp += "<input type='hidden' class='hidden' id='" + this.id2 + "' name='" + this.id2 + "' value='" + this.inputValue2 + "'>";

                        // Input
                        inp += "<div class='input-group pointer'>";

                            inp += "<div onClick=\"" + this.findClick + "\" class='input-group-prepend' data-toggle='kt-tooltip' title='" + Language.XmlLang(149, 2).Text + "' data-placement='bottom'>";
                            inp += "<span class='input-group-text'><i class='fas fa-search'></i></span></div>";

                            inp += "<input onClick=\"" + this.findClick + "\" type='text' readonly ";

                            // Classe
                            if (Utils.Null(this.className, "") != "") { inp += " class='" + this.className + " ' "; }

                            // ID
                            if (Utils.Null(this.id, "") != "") { inp += " id='" + this.id + "' name='" + this.id + "' "; }

                            // Validação
                            if (Utils.Null(this.validation, "") != "") { inp += " validate='" + this.validation + "' "; }

                            // Valor
                            inp += " value='" + this.inputValue + "' ";

                            inp += ">";

                            inp += "<div onClick=\"" + this.removeClick + "\" class='input-group-append' data-toggle='kt-tooltip' title='" + Language.XmlLang(161, 2).Text + "' data-placement='bottom'>";
                            inp += "<span class='input-group-text'><i class='fas fa-times'></i></span></div>";

                        inp += "</div>";

                    inp += "</div>";

                    break;

                // Switch
                case "switch":

                    inp += "<div class='form-group'>";

                    // Label
                    if (this.label != "") { inp += "<label for='" + this.id + "' class='" + (this.required ? "required" : "") + "'>" + this.label + "</label>"; }

                    inp += "<div class='kt-checkbox-list'><span class='kt-switch kt-switch--outline kt-switch--icon kt-switch--success kt-switch--lg'><label>";

                    // Cria input
                    inp += "<input class='control-switch' type='checkbox' ";

                    // Valor padrão para reset de formulário
                    inp += " defaultValue='" + Utils.Null(this.defaultValue, "1") + "' ";

                    // ID
                    if (Utils.Null(this.id, "") != "") { inp += " id='" + this.id + "' name='" + this.id + "' "; }

                    // Validação
                    if (Utils.Null(this.validation, "") != "") { inp += " validate='" + this.validation + "' "; }

                    // Checked
                    if (this.inputChecked) { inp += " checked "; }

                    // OnChange
                    if (Utils.Null(this.change, "") != "") { inp += " onChange='" + this.change + "' "; }

                    // Se possuir o botão todos e estiver com a opção checkedAll, desabilita o input
                    if ((this.showAll) && (this.inputCheckedAll))
                    {
                        inp += " disabled ";
                    }

                    // Finaliza input
                    inp += "value='1' />";

                    // Verifica se deve mostrar o botão todos
                    if (this.showAll)
                    {
                        inp += "<button onClick=\"FORMS.switchAll(this, '" + this.id + "')\" type='button' class='swicth-all-button ";

                        // Checked All
                        if (this.inputCheckedAll) { inp += " bg-dark_green "; }

                        inp += "'>" + Language.XmlLang(251, 2).Text + "</button>";
                    }

                    inp += "<span></span></label></span></div></div>";

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