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
        },

        // Inicia contadores
        interval: function () {
            setInterval(TIMERS.clock, 1000);
        },

        // Relógio
        clock: function () {
            $('#clock').load('/Partials/Clock');
        },

    }

})(jQuery);

$(document).ready(function () {

    // TIMERS funções
    TIMERS.init();

});

