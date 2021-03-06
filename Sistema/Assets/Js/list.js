﻿/*
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

            // Se campo estiver vazio, não permite ordenar
            if (f == '') {

                Swal.fire({
                    icon: 'warning',
                    text: UTILS.xmlLang(250, 2).Text
                });

            } else {

                // Altera o campo de ordenação
                $('#widget_temp_order').val(f);

                // Altera a direção de ordenação
                $('#widget_temp_direction').val(d);

                // Recarrega resultados
                LIST.refreshList();
            }
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

            // Copia o html do dropdown clonado para o original
            $('#hideShow_' + n).html($('#temp_portlet_dropdown').find('div.dropdown-menu').html());
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

        // Limpa o filtro de pesquisa
        clearFilter: function (n) {

            // Limpa todos os campos text, select e textarea
            $('#widget_filter_' + n + ' :input:not(:checkbox):not(:button)[noclear!=true]').val('');

            // Limpa os campos checkbox
            $('#widget_filter_' + n + ' :input:checkbox').each(function () {

                // Pega o valor padrão
                var def = $(this).attr('defaultValue');

                // Seta o padrão
                switch (def) {

                    // Não selecionado
                    case "0":
                        $(this).prop('disabled', false);
                        $(this).prop('checked', false);
                        if ($(this).next('.swicth-all-button').length > 0) { $(this).next('.swicth-all-button').removeClass('bg-dark_green'); }
                        break;

                    // Selecionado
                    case "1":
                        $(this).prop('disabled', false);
                        $(this).prop('checked', true);
                        if ($(this).next('.swicth-all-button').length > 0) { $(this).next('.swicth-all-button').removeClass('bg-dark_green'); }
                        break;

                    // Todos selecionado
                    case "A":
                        $(this).prop('disabled', true);
                        $(this).prop('checked', true);
                        if ($(this).next('.swicth-all-button').length > 0) { $(this).next('.swicth-all-button').addClass('bg-dark_green'); }
                        break;

                    default:
                        break;
                }
            });
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

                // Serializa os inputs
                form = form.concat($("#widget_filter_" + n).serializeArray());

                // Procura todos os switchs 
                $('#widget_filter_' + n + ' .control-switch').each(function () {

                    // Verifica se este switch possui a opção todos
                    if ($(this).next('.swicth-all-button').length == 1) {

                        // Verifica se o botão todos possui a classe 
                        if (!$(this).next('.swicth-all-button').hasClass('bg-dark_green')) {

                            // Se o input não estiver marcado, adiciona ao array o item com valor 0
                            if (!$(this).is(':checked')) {
                                form.push({ name: $(this).attr('id'), value: 0 });
                            }
                        }

                        // Não possui
                    } else {

                        // Se o input não estiver marcado, adiciona ao array o item com valor 0
                        if (!$(this).is(':checked')) {
                            form.push({ name: $(this).attr('id'), value: 0 });
                        }
                    }
                })

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

