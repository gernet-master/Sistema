/* -------------------------------------------------------------------------------------------------------
' Funções dos botões do topo do sistema
' ------------------------------------------------------------------------------------------------------- */

'use strict';

var HEADER = {};

(function ($) {

    HEADER = {

        // Init
        init: function () {
            HEADER.logout();
        },

        // Sair do sistema
        logout: function () {
            $('.kt-header__topbar-item #gernet-logout').click(function () {

                swal.fire({
                    title: 'deseja realmente sair do sistema',
                    type: 'question',
                    showCancelButton: true,
                    confirmButtonText: 'sim',
                    cancelButtonText: 'não'
                }).then(function (result) {
                    if (result.value) {
                        $(location).attr('href', 'Home/Logout');
                    }
                });
            });
        }
    }

})(jQuery);

$(document).ready(function () {

    // Inicia funções
    HEADER.init();

});