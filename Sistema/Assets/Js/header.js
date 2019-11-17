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
                    title: UTILS.xmlLang(48, 2).Text,
                    type: 'question',
                    showCancelButton: true,
                    confirmButtonText: UTILS.xmlLang(49, 2).Text,
                    cancelButtonText: UTILS.xmlLang(50, 2).Text
                }).then(function (result) {
                    if (result.value) {
                        $(location).attr('href', '/Home/Logout');
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