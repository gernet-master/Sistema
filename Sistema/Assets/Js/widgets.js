/*
Descrição: Funções para controle de widgets
Data: 01/01/ 2020 - v.1.0
*/

'use strict';

var WIDGETS = {};
var grid;

(function ($) {

    WIDGETS = {

        // Inicialização do script
        init: function () {

            // Inicia grid
            grid = $('.grid-stack').gridstack();
        },

        // Minimizar/Maximizar
        toggle: function (p, o) {

            // Maximizar
            if ($('#portlet' + p).css('display') == 'none') {

                // Seta o min height para o tamanho original
                $(".grid-stack").data("gridstack").minHeight($('#portlet' + p).closest('.grid-stack-item'), parseInt($('#portlet' + p).closest('.grid-stack-item').attr('data-gs-temp-height')));

                // Insere a opção de redimensionar
                $(".grid-stack").data("gridstack").resizable($('#portlet' + p).closest('.grid-stack-item'), true);

                // Retorna o widget para o tamanho original
                $(".grid-stack").data("gridstack").resize($('#portlet' + p).closest('.grid-stack-item'), null, parseInt($('#portlet' + p).closest('.grid-stack-item').attr('data-gs-original-height')));

                // Altera o texto do botão
                $(o).find('span').html('Minimizar');

                // Altera o icone botão
                $(o).find('i').attr('class', 'kt-nav__link-icon la la-angle-down');

            // Minimizar
            } else {

                // Seta o min height para 1
                $(".grid-stack").data("gridstack").minHeight($('#portlet' + p).closest('.grid-stack-item'), 1);

                // Remove a opção de redimensionar
                $(".grid-stack").data("gridstack").resizable($('#portlet' + p).closest('.grid-stack-item'), false);

                // Reduz o widget para 1 de altura
                $(".grid-stack").data("gridstack").resize($('#portlet' + p).closest('.grid-stack-item'), null, 1);

                // Altera o texto do botão
                $(o).find('span').html('Maximizar');

                // Altera o icone botão
                $(o).find('i').attr('class', 'kt-nav__link-icon la la-angle-up');

            }

            // Ação
            $('#portlet' + p).toggle();

        },

        remove: function (p) {
            $(".grid-stack").data("gridstack").removeWidget($('#portlet' + p).closest('.grid-stack-item'));
            //$('#teste').find('div.dropdown-menu').html('');
            //$('#teste').dropdown('toggle');
        }

    };

})(jQuery);

$(document).ready(function () {

    // WIDGETS funções
    WIDGETS.init();

    // Seta o campo tamanho original ao redimensionar widget
    $('.grid-stack').on('gsresizestop', function (event, elem) {
        $(elem).attr('data-gs-original-height', $(elem).attr('data-gs-height'));
    });

    // Grava a nova configuração da grid sempre que houver ação
    $('.grid-stack').on('change', function (event, items) {
        for (var i = 0; i < items.length; i++) {
            var widget = items[i].id;
            var height = items[i].height;
            var width = items[i].width;
            var top = items[i].y;
            var left = items[i].x;
        }
    });

    // Controla o dropdown para exibir corretamente
    $('.gridstack-dropdown').on('show.bs.dropdown', function () {
        $('#temp_portlet_dropdown').find('div.dropdown-menu').html($(this).find('div.dropdown-menu').html());
        $('#temp_portlet_dropdown').dropdown('toggle');
        $(this).find('div.dropdown-menu').hide();
        var o = $(this).offset();
        $('#temp_portlet_dropdown').css('top', o.top + 40);
        $('#temp_portlet_dropdown').css('left', o.left + 33);
    })

    // Esconde o droppdown
    $('.gridstack-dropdown').on('hidden.bs.dropdown', function () {
        //    $('#teste').find('div.dropdown-menu').html('');
        //  $('#teste').dropdown('toggle');
    })

});