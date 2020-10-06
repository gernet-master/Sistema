/* -------------------------------------------------------------------------------------------------------
' Funções do botões do rodapé do layout
' ------------------------------------------------------------------------------------------------------- */

'use strict';

var FORMS = {};

(function ($) {

    FORMS = {

        // Init
        init: function () {
            FORMS.mask();
        },

        // Inicia mascaras de campos
        mask: function () {
            $(document).find('input[mask]').each(function () {
                var m = $(this).attr('mask').split('|');

                // Validação especifica para tratar telefones
                if (m[0] == '(00) 00000-0000') {
                    var maskBehavior = function (val) {
                        return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
                    },
                        options = {
                            onKeyPress: function (val, e, field, options) {
                                field.mask(maskBehavior.apply({}, arguments), options);
                            }
                        };
                    $(this).mask(maskBehavior, options);
                } else {
                    var rev = false;
                    if (m[1] !== undefined) { rev = m[1]; }
                    $(this).mask(m[0], { reverse: rev });
                }
            });
        },

        // Valida campos de formulário
        validate: function (id) {

            // Retorno
            var error_form = false;
            var error_validate = false;

            // Remove todos os erros existentes e a classe de erro
            $('#' + id).find('div.form-error-msg').remove();
            $('#' + id + ' :input').removeClass('form-error');

            // Pega todos os inputs do form
            var fields = $('#' + id + ' :input'); 

            // Faz loop nos inputs
            for (var f = 0; f < fields.length; f++) {

                // Cria o objeto
                var obj = $(fields[f]);

                // Pega o atributo validação
                var validate = obj.attr('validate');

                // Se possuir validação, pega todos os parametros
                if ((validate != '') && (validate !== undefined)) {

                    // Separa os parametros
                    var param = validate.split(',');

                    // Loop nos parametros
                    for (var p = 0; p < param.length; p++) {

                        // Chama função de validação
                        error_validate = FORMS.validateParam(obj, param[p]);
                        
                        // Se possuir erro, para de processar demais parametros
                        if (error_validate) {
                            error_form = true;
                            break;
                        }
                    }
                }
            }        
            
            return error_form;
        },

        // Valida os parametros de validação dos campos de formulário
        validateParam: function (obj, param) {

            // Retorno
            var ret = false;
            var pattern = "";

            // Separa o tipo e o valor
            var tipo = param.split('=');

            // Pega o tipo de paramentro
            switch (tipo[0].toLowerCase()) {

                // Campo obrigatorio
                case 'required':

                    if ($(obj).val() == '') {
                        FORMS.insertValidation(obj, UTILS.xmlLang(219, 2).Text);
                        ret = true;
                    } 
                    break;

                // Tamanho mínimo 
                case 'minsize':
                    if ($(obj).val().length < UTILS.isUndefined(tipo[1], 0)) {
                        FORMS.insertValidation(obj, UTILS.xmlLang(220, 2).Text + ' ' + tipo[1] + ' ' + UTILS.xmlLang(222, 0).Text);
                        ret = true;
                    }
                    break;

                // Tamanho máximo
                case 'maxsize':
                    if ($(obj).val().length > UTILS.isUndefined(tipo[1], 0)) {
                        FORMS.insertValidation(obj, UTILS.xmlLang(221, 2).Text + ' ' + tipo[1] + ' ' + UTILS.xmlLang(222, 0).Text);
                        ret = true;
                    }
                    break;

                // E-mail
                case "email":
                    pattern = /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i;
                    if (!pattern.test($(obj).val())) {
                        FORMS.insertValidation(obj, UTILS.xmlLang(223, 2).Text);
                        ret = true;
                    }
                    break;

                // Campos iguais
                case "equals":
                    if ($(obj).val() !== $('#' + UTILS.isUndefined(tipo[1], 'xxx')).val()) {
                        FORMS.insertValidation(obj, UTILS.xmlLang(224, 2).Text);
                        ret = true;
                    }
                    break;

                // Somente números
                case "numbers":
                    pattern = /^\d+$/;
                    if (!pattern.test($(obj).val())) {
                        FORMS.insertValidation(obj, UTILS.xmlLang(225, 2).Text);
                        ret = true;
                    }
                    break;

                default:
                    break;
            }

            return ret;
        },

        // Insere o aviso de erro no campo
        insertValidation: function (obj, error) {

            // Mensagem
            var html = '';
            html = '<div class="form-error-msg" id="form-erro-id-' + $(obj).attr('id') + '" ';

            // Verifica se tem um input group
            if ($(obj).prev().attr('class') == 'input-group-prepend') {
                html += ' style="margin-left:' + ($(obj).prev().width() + ($(obj).hasClass('kt-margin-l-40') ? 40 : 0)) + 'px !important; margin-top:2px !important;" ';      
            }

            html += '><i class="fa fa-times-circle" aria-hidden="true"></i>' + error + '</div>';            

            // Adiciona a classe para deixar a border inferior do objeto vermelha
            $(obj).addClass('form-error');

            // Verifica se tem um botao após o input e  nsere a mensagem abaixo do objeto
            if ($(obj).next().attr('class') == 'input-group-append') {
                $(obj).next().after(html);
            } else {
                $(obj).after(html);
            }


            // Remove a mensagem de erro se for digitado alguma coisa em campo input text ou password
            if ($(obj).is('input:text, input:password')) {
                $(obj).keyup(function () {
                    $('#form-erro-id-' + $(obj).attr('id')).remove();
                    $('#' + $(obj).attr('id')).removeClass('form-error');
                });
            }

            // Remove a mensagem de erro se for selecionado algum item
            if ($(obj).is('select')) {
                $(obj).change(function () {
                    $('#form-erro-id-' + $(obj).attr('id')).remove();
                    $('#' + $(obj).attr('id')).removeClass('form-error');
                })
            }
        },

        // Remove valores dos campos
        removeValues: function (f) {
            var arr = f.split(",");
            for (var i = 0; i < arr.length; i++) {
                $('#' + arr[i]).val('');
            }
        },

        // Abre tela de localização para campos
        fieldLocation: function (a, f, i) {

            $('.kt_content_app_gernet').block({ baseZ: 3, message: null, overlayCSS: { backgroundColor: '#ececec', opacity: 0.3, cursor: 'default' } });
            $('#kt_buttons_app_gernet').block({ baseZ: 3, message: null, overlayCSS: { backgroundColor: '#ececec', opacity: 0.3, cursor: 'default' } });

            // Cria campos de formulário
            var data = [];
            data.push({ name: 'widget_temp_name', value: 'inner_app_find' });
            data.push({ name: 'widget_temp_controller', value: a });
            data.push({ name: 'widget_temp_action', value: 'ListarWidget' });
            data.push({ name: 'widget_temp_page', value: 1 });
            data.push({ name: 'widget_temp_registers', value: 10 });
            data.push({ name: 'widget_temp_order', value: '' });
            data.push({ name: 'widget_temp_direction', value: 'asc' });
            data.push({ name: 'widget_temp_input_find', value: 1 });
            data.push({ name: 'widget_temp_input_fields', value: f });
            data.push({ name: 'widget_temp_input_return', value: i });

            // Carrega página
            $('#panel_search').load('/' + a + '/ListarWidget/', data, function () {
                $("#panel_search").slideToggle("slow");
            });
        },

        // Carrega valores nos campos
        loadRegister: function (v, f) {

            // Fecha a tela
            APP.closeSearch();

            // Pega os valores
            var values = v.split('||');
            var fields = f.split(',');

            // Carrega os valores
            for (var p = 0; p < values.length; p++) {
                $('#' + fields[p]).val(values[p]);

                // Remove a mensagem de erro se existir
                $('#form-erro-id-' + fields[p]).remove();
                $('#' + fields[p]).removeClass('form-error');
            }
        },

        // Controle de caixas de switch
        switchAll: function (o, i) {

            // Verifica se botão já foi clicado
            if ($(o).hasClass('bg-dark_green')) {
                $(o).removeClass('bg-dark_green');
                $('#' + i).attr('disabled', false);
            } else {
                $(o).addClass('bg-dark_green');
                $('#' + i).attr('disabled', true);
            }
        },
    }

})(jQuery);

$(document).ready(function () {

    // Inicia funções
    FORMS.init();

});
