/*
Descrição: Funções para controle de timers
Data: 01/01/ 2020 - v.1.0
*/

'use strict';

var TIMERS = {};
var userDisconnectInterval;
var contUserDisconnect;

(function ($) {

    TIMERS = {

        // Inicialização do script
        init: function () {
            TIMERS.clock();
            TIMERS.interval();
            TIMERS.chat();
        },

        // Inicia contadores
        interval: function () {
            setInterval(TIMERS.clock, 1000);
            setInterval(TIMERS.chat, 10000);
        },

        // Relógio
        clock: function () {
            $('#clock').load('/Partials/Clock');
        },

        // Atualiza chat e checa mensagens - 10 segundos
        chat: function () {

            // Carrega lista de usuários
            CHAT.listUsers();
        }

    }

})(jQuery);

$(document).ready(function () {

    // TIMERS funções
    TIMERS.init();

});

