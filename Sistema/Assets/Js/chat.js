﻿/* -------------------------------------------------------------------------------------------------------
Descrição: Funções do chat
Data: 01/01/2020 - v.1.0
' ------------------------------------------------------------------------------------------------------- */

'use strict';

var CHAT = {};
var chat_running = false;

(function ($) {

    CHAT = {

        // Inicialização do script
        init: function () {

            // Definição de variaveis
            chat_running = false;

            // Ações
            CHAT.catchStatusList();
            CHAT.search();            
            CHAT.statusButton();
            CHAT.highlight();
            CHAT.changeStatus();
            CHAT.changePrivacy();
            CHAT.changeMsg();
        },

        // Lista os usuários do chat
        listUsers: function () {

            if (!chat_running) {

                chat_running = true;

                // Verifica se a listagem de usuários online/offline está aberta
                var offline = 0;
                var online = 1;

                if ($('#kt_quick_panel #chat-div-online').length > 0) {
                    if ($('#kt_quick_panel #chat-div-online').hasClass('kt-hidden')) { online = 0; } else { online = 1; }
                }

                if ($('#kt_quick_panel #chat-div-offline').length > 0) {
                    if ($('#kt_quick_panel #chat-div-offline').hasClass('kt-hidden')) { offline = 0; } else { offline = 1; }
                }

                // Pega filtro
                var search = $('#kt_quick_panel #chat-search').val();

                // Carrega lista
                $('#kt_quick_panel #chat-list-users').load('/Partials/ChatPanelList/', { online: online, offline: offline, search: search }, function () {
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
                CHAT.listUsers();                  
            });
        },

        // Abre a tela de configurações para mudar o status
        statusButton: function () {
            $('#kt_quick_panel #kt_quick_panel_status_btn').click(function () {
                $('#kt_quick_panel .kt-quick-panel__nav li').eq(1).find('a').click();
            });            
        },

        // Destaca o texto procurado
        highlight: function () {

            // Texto pesquisado
            var search_string = $('#kt_quick_panel #chat-search').val();

            // Marca highlight
            if (search_string.length > 1) {
                $('#kt_quick_panel .kt-notification-v2__item-title').each(function () { $(this).highlight(search_string, { className: 'text-danger font-weight-bold' }); });
            }
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
                $.post('/Partials/ChatPanelChangeStatus/', { status: status });
            });
        },

        // Altera a privacidade
        changePrivacy: function() {
            $('#kt_quick_panel #kt_quick_panel_tab_settings #flprivacy').click(function () {

                // Grava nova configuração de privacidade
                $.post('/Partials/ChatPanelChangePrivacy/', { privacy: ($(this).is(':checked') ? 1 : 0) });
            });
        },

        // Altera a configuração de mensagem
        changeMsg: function () {
            $('#kt_quick_panel #kt_quick_panel_tab_settings #flmsgconfig').click(function () {

                // Grava nova configuração de mensagem
                $.post('/Partials/ChatPanelChatMsg/', { config: ($(this).is(':checked') ? 1 : 0) });
            });
        }

    };

})(jQuery);




