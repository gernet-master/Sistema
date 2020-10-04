/* -------------------------------------------------------------------------------------------------------
' Funções para controle de páginas
' ------------------------------------------------------------------------------------------------------- */

'use strict';

var PAGE = {};

(function ($) {

    PAGE = {

        // Inicialização do script
        init: function () {

            // Ações
            PAGE.redirect();
            PAGE.controlDropDown();
        },

        // Controla as posições do elementos de acordo com a resolução da tela
        windowResize: function () {

            // Verifica a resolução de tela e a posição dos elementos
            if ($(document).width() < 1024) {
                if ($('#kt_content_app').find('.page-element-responsive').first().attr('pos') == 1) {
                    $('#kt_content_app').html($('#kt_content_app').find('.page-element-responsive').get().reverse());
                }
            }
        },

        // Redireciona
        redirect: function () {

            // Pega a página
            var page = $('#current_page').val();

            // Se não possui página atual, redireciona para dashboard principal
            if (page == '') {
                UTILS.menu('Home', 'Dashboard');
            }

            // Redireciona para página
            else {

                // Pega os valores
                var p = page.split('|');

                // Redireciona
                UTILS.menu(p[0], p[1], p[2], p[3]);
            }

        },

        // Controle as dropdown para que não fechem automaticamente com um clique interno, nem fiquem exibindo a tooltip
        controlDropDown: function () {

            // Impede de fechar
            $(document).on('click', '.gernet-history', function (e) {
                e.stopPropagation();
            });

            // Esconde tooltip
            $(document).on('mouseover', '.gernet-history', function () {
                $(this).closest('label').tooltip('hide');
            });
        },

        // Controle de opções da dashboard
        controlAppDashboard: function (c) {
            if (c) {
                $('#dropdown_dashboard_control').addClass('hide');
            } else {
                $('#dropdown_dashboard_control').removeClass('hide');
            }
        },

        // Grava as configurações de dashboard
        controlAppDashboardSave: function () {

            // Variaveis
            var action = 'Dashboard';
            var controller = $('#app_temp_controller').val();
            var dashboard = ($('#config_app_dashboard').is(":checked") ? 1 : 0);
            var register = $('#config_app_register').val();

            $.post('Partials/SaveConfigDashboard', { action: action, controller: controller, dashboard: dashboard, register: register }, function (response) {

                // Se retornou 0, exibe alerta de erro																									
                if (response.success == 0) {
                    Swal.fire({
                        icon: 'error',
                        title: UTILS.xmlLang(77, 1).Text,
                        text: response.msg
                    });
                }

                // Se retornou 1, exibe alerta de sucesso
                if (response.success == 1) {
                    let timerInterval;
                    Swal.fire({
                        title: response.msg,
                        icon: 'success',
                        timer: 5000,
                        showConfirmButton: true,
                        confirmButtonText: UTILS.xmlLang(93, 2).Text + ' (5)',
                        timerProgressBar: true,
                        allowOutsideClick: false,
                        onBeforeOpen: () => {
                            timerInterval = setInterval(() => {
                                Swal.getConfirmButton().textContent = UTILS.xmlLang(93, 2).Text + ' (' + (Math.ceil(Swal.getTimerLeft() / 1000)) + ')';
                            }, 1000);
                        },
                        onClose: () => {
                            clearInterval(timerInterval);
                        }
                    });
                }

            });

        }
        
    };

})(jQuery);

$(document).ready(function () {

    // Inicia funções
    PAGE.init();

    // Controle posição dos elementos
    $(window).resize(PAGE.windowResize);

});
