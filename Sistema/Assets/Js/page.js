/* -------------------------------------------------------------------------------------------------------
' Funções para controle de páginas
' ------------------------------------------------------------------------------------------------------- */

'use strict';

var PAGE = {};

(function ($) {

    PAGE = {

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

        }
        
    };

})(jQuery);

$(document).ready(function () {

    // Inicia funções
    PAGE.redirect();

});
