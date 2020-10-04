/*
Descrição: Funções para controle de widgets
Data: 01/01/ 2020 - v.1.0
*/

'use strict';

var WIDGETS = {};
var grid;
var temp_portlet = "";

(function ($) {

    WIDGETS = {

        // Inicialização do script
        init: function () {

            // Ações
            WIDGETS.catchToolButton();
            WIDGETS.outsideClick();
            WIDGETS.resizeWidget();
            WIDGETS.onChange();
            WIDGETS.fixTableHeaders();
        },        

        // Pega o clique nos botões da barra para reposicionar o menu e abrir
        catchToolButton: function () {
            
            //// Abrir dropdown
            $(document).on('click', '.gridstack-dropdown', function () {

                // Checa se o clique foi em outro botão da toolbar
                if (temp_portlet != $(this).attr('data-id')) {
                    $('#temp_portlet_dropdown').find('div.dropdown-menu').removeClass('show');
                }

                if ($('#temp_portlet_dropdown').find('div.dropdown-menu').hasClass('show')) {

                    // Fecha o menu
                    WIDGETS.hideDropDown();

                } else {

                    // Clona a dropdown do widget
                    $('#temp_portlet_dropdown').find('div.dropdown-menu').html($(this).find('div.dropdown-menu').html());

                    // Abre o clone
                    $('#temp_portlet_dropdown').dropdown('toggle');

                    // Preenche a caixa de seleção de cor com a cor atual do widget
                    $('#temp_portlet_dropdown .gernet-widget-color').css('backgroundColor', $(this).find('div.dropdown-menu').closest('.grid-stack-item-content').css('backgroundColor'));

                    // Esconde a dropdown original
                    $(this).find('div.dropdown-menu').hide();

                    // Posiciona o clone dentro do widget
                    var o = $(this).offset();
                    $('#temp_portlet_dropdown').css('top', o.top + 40);
                    $('#temp_portlet_dropdown').css('left', o.left + 32);

                    // Define temp
                    temp_portlet = $(this).attr('data-id');
                }
            });

        },

        // Pega o click fora do dropdown para esconder o menu
        outsideClick: function () {
            $('body').on('click', function (e) {
                if ($(e.target).closest('.gridstack-dropdown').length == 0) {
                    WIDGETS.hideDropDown();
                }
            });
        },

        // Esconde a droppdown clonada
        hideDropDown: function () {
            $('#temp_portlet_dropdown').find('div.dropdown-menu').html('').removeAttr('style').removeAttr('x-placement').removeClass('show');
            $('#temp_portlet_dropdown').attr('style', 'position:absolute; z-index:999');
            temp_portlet = "";
        },

        // Seta o campo tamanho original ao redimensionar widget
        resizeWidget: function () {
            $('.grid-stack').on('gsresizestop', function (event, elem) {
                $(elem).attr('data-gs-original-height', $(elem).attr('data-gs-height'));
            });
        },

        // Remove o Widget
        remove: function (p) {
            swal.fire({
                title: UTILS.xmlLang(103, 2).Text,
                text: UTILS.xmlLang(104, 2).Text,
                icon: 'question',
                showCancelButton: true,
                confirmButtonText: UTILS.xmlLang(49, 2).Text,
                cancelButtonText: UTILS.xmlLang(50, 2).Text
            }).then(function (result) {
                if (result.value) {
                    WIDGETS.hideDropDown();
                    $(".grid-stack").data("gridstack").removeWidget($('#widget_portlet_' + p).closest('.grid-stack-item'));
                }
            });            
        },

        // Minimizar/Maximizar
        toggle: function (p, o) {

            // Maximizar
            if ($('#widget_portlet_' + p).css('display') == 'none') {

                // Seta o min height para o tamanho original
                $(".grid-stack").data("gridstack").minHeight($('#widget_portlet_' + p).closest('.grid-stack-item'), parseInt($('#widget_portlet_' + p).closest('.grid-stack-item').attr('data-gs-temp-height')));

                // Insere a opção de redimensionar
                $(".grid-stack").data("gridstack").resizable($('#widget_portlet_' + p).closest('.grid-stack-item'), true);

                // Retorna o widget para o tamanho original
                $(".grid-stack").data("gridstack").resize($('#widget_portlet_' + p).closest('.grid-stack-item'), null, parseInt($('#widget_portlet_' + p).closest('.grid-stack-item').attr('data-gs-original-height')));

                // Altera o texto do botão
                $(o).find('span').html('Minimizar');
                $('.grid-stack label[data-id="options_' + p + '"] .gernet-widget-toogle').find('span').html('Minimizar');

                // Altera o icone botão
                $(o).find('i').attr('class', 'kt-nav__link-icon la la-angle-down');
                $('.grid-stack label[data-id="options_' + p + '"] .gernet-widget-toogle').find('i').attr('class', 'kt-nav__link-icon la la-angle-down');

            // Minimizar
            } else {

                // Seta o min height para 1
                $(".grid-stack").data("gridstack").minHeight($('#widget_portlet_' + p).closest('.grid-stack-item'), 1);

                // Remove a opção de redimensionar
                $(".grid-stack").data("gridstack").resizable($('#widget_portlet_' + p).closest('.grid-stack-item'), false);

                // Reduz o widget para 1 de altura
                $(".grid-stack").data("gridstack").resize($('#widget_portlet_' + p).closest('.grid-stack-item'), null, 1);

                // Altera o texto do botão
                $(o).find('span').html('Maximizar');
                $('.grid-stack label[data-id="options_' + p + '"] .gernet-widget-toogle').find('span').html('Maximizar');

                // Altera o icone botão
                $(o).find('i').attr('class', 'kt-nav__link-icon la la-angle-up');
                $('.grid-stack label[data-id="options_' + p + '"] .gernet-widget-toogle').find('i').attr('class', 'kt-nav__link-icon la la-angle-up');

                // Se possuir filtro, esconde
                if ($('#widget_filter_' + p).length > 0) {
                    $('#widget_filter_' + p).addClass('hide');
                }

            }

            // Ação
            $('#widget_portlet_' + p).toggle();

        },

        // Acionado sempre que houver alteração de tamanho ou posição da grid
        onChange: function () {

            // Fecha a dropdown ao iniciar o reposicionamento
            $('.grid-stack').on('dragstart', function (event, ui) {
                WIDGETS.hideDropDown();
            });

            // Fecha a dropdown ao iniciar o redimensionamento
            $('.grid-stack').on('resizestart', function (event, ui) {
                WIDGETS.hideDropDown();
            });

            // Grava nova configuração da grid
            $('.grid-stack').on('change', function (event, items) {
                if (items !== undefined) {
                    for (var i = 0; i < items.length; i++) {
                        var widget = items[i].id;
                        var height = items[i].height;
                        var width = items[i].width;
                        var top = items[i].y;
                        var left = items[i].x;
                    }
                }
            }); 
        },

        // Abre paleta de cores
        color: function (p) {
            if ($('#temp_portlet_dropdown .color-picker').hasClass('kt-hidden')) {
                var arrColors = ['000000', '993300', '333300', '000080', '333399', '333333', '800000', 'FF6600', '808000', '008000', '008080', '0000FF', '666699', '808080', 'FF0000', 'FF9900', '99CC00', '339966', '33CCCC', '3366FF', '800080', '999999', 'FF00FF', 'FFCC00', 'FFFF00', '00FF00', '00FFFF', '00CCFF', '993366', 'C0C0C0', 'FF99CC', 'FFCC99', 'FFFF99', 'CCFFFF', '99CCFF', 'FFFFFF'];
                var temp = '';
                $.each(arrColors, function (index, value) {
                    temp += '<span style="background-color:#' + value + '" onClick="WIDGETS.colorChange(\'' + p + '\', \'#' + value + '\')"></span>';
                });
                $('#temp_portlet_dropdown .color-picker').removeClass('kt-hidden').html(temp);
            } else {
                $('#temp_portlet_dropdown .color-picker').addClass('kt-hidden').html('');
            }
        },


        // Altera a cor do widget
        colorChange: function (p, v) {
            $('#temp_portlet_dropdown .gernet-widget-color').css('backgroundColor', v);
            $('#widget_portlet_' + p).closest('.grid-stack-item-content').css('backgroundColor', v);
            $('#widget_portlet_' + p).closest('.kt-portlet').css('backgroundColor', v);
            $('#widget_portlet_' + p).closest('.kt-portlet').find('.kt-portlet__head-title').css("color", WIDGETS.getContrastYIQ(v));
            $('.grid-stack label[data-id="options_' + p + '"] .gernet-widget-color').css('backgroundColor', v);
        },

        // Pega o contraste da cor
        getContrastYIQ: function (hexcolor) {
            hexcolor = hexcolor.substring(1);
            var r = parseInt(hexcolor.substr(0, 2), 16);
            var g = parseInt(hexcolor.substr(2, 2), 16);
            var b = parseInt(hexcolor.substr(4, 2), 16);
            var yiq = ((r * 299) + (g * 587) + (b * 114)) / 1000;
            return (yiq >= 128) ? '#000000 !important' : '#FFFFFF !important';
        },

        // Fixa o cabeçalho de tabelas
        fixTableHeaders: function () {
            $('.grid-stack-item-content').scroll(function () {
                if ($(this).scrollTop() > 0) {

                    // Pega o nome do widget
                    var n = $(this).find('#widget_temp_name').val();

                    // Calcula a altura dos objetos anteriores ao cabeçalho da tabela
                    var h1 = $(this).find('#widget_header_' + n).height(); // Cabeçalho
                    var h2 = $(this).find('#widget_filter_' + n).height(); // Filtro
                    var h3 = $(this).find('#widget_table_buttons_' + n).height(); // Botões da listagem
                    var h4 = $(this).find('#widget_table_result_' + n + ' thead').height(); // Barra de cabeçalho da tabela
                    var h = parseInt(h1) + parseInt(h2) + parseInt(h3) + parseInt(h4);

                    // Se filtro estiver aberto, adiciona 6 para correção de posição
                    if (!$(this).find('#widget_filter_' + n).hasClass('hide')) {
                        h = h + 6;
                    }

                    // Pega o quanto foi alterado na barra de rolagem
                    var p = $(this).find('thead.fixed-header').offset().top;
                    var p2 = $(this).scrollTop();

                    // Pega o elemento clonado
                    var o = $(this).find('[id^=clone_header_]');

                    // Verifica se está no limite para fixar
                    if ((parseInt(p2) - parseInt(h2)) > p) {

                        // Se não existir cria
                        if (o.length == 0) {
                            
                            var clone = '';
                            clone = $('#clone');
                            clone = $(this).find('#widget_table_result_' + n).clone(true);
                            clone.find('tbody').remove();
                            clone.find('thead').removeClass('fixed-header');
                            clone.attr('id', 'clone_header_' + n);
                            clone.css({ position: 'relative', top: p2 - h });
                            $(this).find('#widget_table_result_' + n).parent('div').prepend(clone);

                        } else {
                            $(o).css({ top: p2 - h });
                        }
                    } else {
                        $(this).find('#clone_header_' + n).remove();
                    }
                }
            })
        }
    };

})(jQuery);

// Ações quando a página é carregada
$(document).ready(function () {

    // WIDGETS funções
    WIDGETS.init();

});
