/* -------------------------------------------------------------------------------------------------------
Descrição: Funções do chat
Data: 01/01/2020 - v.1.0
' ------------------------------------------------------------------------------------------------------- */

'use strict';

var CHAT = {};

(function ($) {

    CHAT = {

        // Inicialização do script
        init: function () {

            // Ações
            CHAT.catchStatusList();
            CHAT.search();
            CHAT.changeStatus();
            CHAT.statusButton();
        },

        // Lista os usuários do chat
        listUsers: function () {

            // Verifica se a listagem de usuários oonline/offline está aberta
            var offline = 0;
            var online = 0;

            if ($('#kt_quick_panel #chat-div-online').length > 0) {
                if ($('#kt_quick_panel #chat-div-online').hasClass('kt-hidden')) { online = 0; } else { online = 1; }
            }

            if ($('#kt_quick_panel #chat-div-offline').length > 0) {
                if ($('#kt_quick_panel #chat-div-offline').hasClass('kt-hidden')) { offline = 0; } else { offline = 1; }
            }

            // Carrega lista
            $('#kt_quick_panel #chat-list-users').load('/Partials/ChatPanelList/', { online: online, offline: offline }, function () {
                CHAT.init();
            });
            }            
        },

        // Abre/Esconde lista de usuários
        catchStatusList: function () {

            // Online
            $('#kt_quick_panel #chat-button-online').click(function () {
                if ($('#kt_quick_panel #chat-div-online').hasClass('kt-hidden')) {
                    $('#kt_quick_panel #chat-div-online').removeClass('kt-hidden');
                } else {
                    $('#kt_quick_panel #chat-div-online').addClass('kt-hidden');
                }
            });

            // Offline
            $('#kt_quick_panel #chat-button-offline').click(function () {
                if ($('#kt_quick_panel #chat-div-offline').hasClass('kt-hidden')) {
                    $('#kt_quick_panel #chat-div-offline').removeClass('kt-hidden');
                } else {
                    $('#kt_quick_panel #chat-div-offline').addClass('kt-hidden');
                }
            });
        },

        // Filtra pelo usuário
        search: function () {
            $('#kt_quick_panel #chat-search').keyup(function () {

                  //          $('#kt_quick_panel .kt-notification-v2__item-title').each(function () { $(this).highlight(search_string, { className: 'text-danger font-weight-bold', accentInsensitive: true }); });


                // Texto pesquisado
                var search_string = $('#kt_quick_panel #chat-search').val();

                // Nenhum: remove highlight e exibe tudo
                if (search_string == '') {
                    $('#kt_quick_panel .kt-notification-v2__item-title').each(function () { $(this).closest('.kt-notification-v2').removeClass('kt-hidden'); });
                }

                // Filtra e marca highlight
                else
                {
                    $('#kt_quick_panel .kt-notification-v2__item-title').each(function () { $(this).closest('.kt-notification-v2').addClass('kt-hidden'); });
                }
            });
        },

        // Altera o status
        changeStatus: function () {
            $('#kt_quick_panel #kt_quick_panel_tab_settings #flstatuschat').click(function () {
                var status = $(this).val();
                switch (parseInt(status)) {
                    case 1: $('#kt_quick_panel #kt_quick_panel_status_btn').html('<i class="fa fa-circle btn-font-success"></i>&nbsp;' + UTILS.xmlLang(108, 2).Text); break;
                    case 2: $('#kt_quick_panel #kt_quick_panel_status_btn').html('<i class="fa fa-circle btn-font-danger"></i>&nbsp;' + UTILS.xmlLang(109, 2).Text); break;
                    case 3: $('#kt_quick_panel #kt_quick_panel_status_btn').html('<i class="fa fa-circle btn-font-brand"></i>&nbsp;' + UTILS.xmlLang(113, 2).Text); break;
                    case 4: $('#kt_quick_panel #kt_quick_panel_status_btn').html('<i class="fa fa-circle btn-font-warning"></i>&nbsp;' + UTILS.xmlLang(114, 2).Text); break;
                }

                // Grava novo status na configuração do usuário
                $.post('/Partials/ChatPanelListStatus/', { status: status });
            });
        },

        // Abre a tela de configurações para mudar o status
        statusButton: function () {
            $('#kt_quick_panel #kt_quick_panel_status_btn').click(function () {
                $('#kt_quick_panel .kt-quick-panel__nav li').eq(1).find('a').click();
            });            
        }

    };

})(jQuery);
