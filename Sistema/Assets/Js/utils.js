/* -------------------------------------------------------------------------------------------------------
Descrição: Funções do sistema
Data: 01/01/2020 - v.1.0
' ------------------------------------------------------------------------------------------------------- */

'use strict';

var UTILS = {};

(function ($) {

    UTILS = {

        // Recupera o texto do arquivo de idioma
        xmlLang: function (i, f) {
            var txt = '';
            var key = '';
            var aviso = $.ajax({
                url: '/assets/languages/pt-br.xml',
                type: 'GET',
                dataType: 'xml',
                async: false,
                success: function (xml) {
                    txt = UTILS.formatString($(xml).find("string[id='" + i + "']").attr('text'), f);
                    key = $(xml).find("string[id='" + i + "']").attr('accesskey');
                    if (txt === undefined) { txt = ''; }
                    if (key === undefined) { key = ''; }
                }
            });
            return { Text: txt, AccessKey: key };
        },

        // Formata texto
        formatString: function(s, f) {
            if (s !== undefined) {
                if (f == 0) { return s.toLowerCase(); }
                if (f == 1) { return s.toUpperCase(); }
                if (f == 2) { return s.substr(0, 1).toUpperCase() + s.substr(1); }
                if (f == 3) { return s; }
            } else {
                return "";
            }
        },

        // Exibe alerta de processando
        processing: function () {
            swal.fire({
                title: UTILS.xmlLang(68, 2).Text,
                icon: 'warning',
                showConfirmButton: false,
                allowOutsideClick: false
            });
        }
    };

})(jQuery);
