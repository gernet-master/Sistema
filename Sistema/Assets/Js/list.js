/*
Descrição: Funções para controle de widgets
Data: 01/01/ 2020 - v.1.0
*/

'use strict';

var LIST = {};

(function ($) {

    LIST = {

        // Alterar página
        pageChange: function (p) {

            // Altera a página
            $('#widget_temp_page').val(p);

            // Recarrega resultados
            LIST.refreshList();
        },

        // Alterar registros por página
        registersChange: function (r) {

            // Recoloca na página 1
            $('#widget_temp_page').val(1);

            // Altera os registros
            $('#widget_temp_registers').val(r);

            // Recarrega resultados
            LIST.refreshList();
        },

        // Alterar ordenação
        orderChange: function (f, d) {

            // Altera o campo de ordenação
            $('#widget_temp_order').val(f);

            // Altera a direção de ordenação
            $('#widget_temp_direction').val(d);

            // Recarrega resultados
            LIST.refreshList();
        },

        // Exibir ou esconder campos
        showHideColumn: function (o, n, f) {

            // Exibe
            if ($(o).find('i').hasClass('ft-red')) {
                $('#widget_portlet_' + n).find("[id='column_" + f + "']").removeClass('hide');
                $(o).find('i').removeClass('ft-red').removeClass('fa-eye-slash').addClass('fa-eye').addClass('ft-green');

                // Esconde
            } else {
                $('#widget_portlet_' + n).find("[id='column_" + f + "']").addClass('hide');
                $(o).find('i').removeClass('ft-green').removeClass('fa-eye').addClass('fa-eye-slash').addClass('ft-red');
            }
        },

        // Exibir ou esconder o filtro
        showHideFilter: function (n) {
            // Exibe
            if ($('#widget_filter_' + n).hasClass('hide')) {
                $('#widget_filter_' + n).removeClass('hide');
                // Esconde
            } else {
                $('#widget_filter_' + n).addClass('hide');
            }
        },

        // Limpa o filtro de resultado
        clearFilter: function (n) {
            $('#widget_filter_' + n + ' :input:not(:checkbox):not(:button)[noclear!=true]').val('');
        },

        // Atualiza o resultado
        refreshList: function () {

            // Nome do widget
            var n = $('#widget_temp_name').val();

            // Controller do widget
            var a = $('#widget_temp_controller').val() + '/' + $('#widget_temp_action').val();

            // Se o widget estiver minimizado, remove a dropdown e maximiza ele
            if ($('#widget_portlet_' + n).css('display') == "none") {
                WIDGETS.hideDropDown();
                $('#widget_name_' + n).find('.gernet-widget-toogle').click();
            }

            // Esconde qualquer tooltip que esteja aberta
            $(".tooltip").tooltip("hide");

            // Pega todos os campos do formulário de controle
            var form = $("#widget_form_" + n).serializeArray();

            // Verifica se o formulário de filtro está visivel e serializa os inputs
            if (!$('#widget_filter_' + n).hasClass('hide')) {
                form = form.concat($("#widget_filter_" + n).serializeArray());
            }

            // Atualiza o widget
            $('#widget_result_' + n).html("<center><br>" + UTILS.xmlLang(169, 2).Text + "</center>");
            $("#widget_result_" + n).load(a + " #widget_portlet_" + n, form, function () {

                // Reinicia os componentes no widget
                KTApp.initComponents();
            });
        },

        // Carrega o registro selecionado
        loadRegister: function (i) {

            // Verifica se foi passado identificador
            if (i == '') {
                Swal.fire({
                    icon: 'error',
                    title: UTILS.xmlLang(77, 1).Text,
                    text: UTILS.xmlLang(200, 2).Text
                });     
            } else {

                // Verifica se é número
                if ($.isNumeric(i)) {
                    UTILS.menu($('#widget_temp_controller').val(), 'Incluir', i, 0);
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: UTILS.xmlLang(77, 1).Text,
                        text: UTILS.xmlLang(201, 2).Text
                    });   
                }
            }
        }

    };

})(jQuery);

