/* -------------------------------------------------------------------------------------------------------
Descrição: Funções do sistema
Data: 01/01/2021 - v.1.0
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

        // Verifica se existe o objeto
        isUndefined: function (o, v) {
            if (typeof o === 'undefined') {
                return v;
            } else {
                return o;
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
        },

        // Limpa o console de erros do navegador
        clear: function () {
            console.API;
            if (typeof console._commandLineAPI !== 'undefined') {
                console.API = console._commandLineAPI; 
            } else if (typeof console._inspectorCommandLineAPI !== 'undefined') {
                console.API = console._inspectorCommandLineAPI; 
            } else if (typeof console.clear !== 'undefined') {
                console.API = console;
            }
            console.API.clear();
        },

        // Carrega o link do menu
        menu: function (controller, action, id, id2) {
            if ((controller != '') && (action != '')) {

                // Bloqueia o aplicativo
                $('#kt_content_app').block({
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#17a2b8',
                        '-webkit-border-radius': '10px',
                        '-moz-border-radius': '10px',
                        opacity: .5,
                        color: '#fff'
                    },
                    overlayCSS: {
                        backgroundColor: 'background: rgba(0, 0, 0, 0.1)',
                        opacity: 0.6,
                        cursor: 'wait'
                    }, 
                    message: UTILS.xmlLang(68, 2).Text
                });              

                // Carrega e desbloqueia
                $('#kt_content_app').load("/Partials/App/", { controller: controller, action: action, id: id, id2: id2 } , function (response, status, xhr) {

                    // Esconde qualquer tooltip que esteja aberta
                    $(".tooltip").tooltip("hide");

                    // Controle posição dos elementos
                    PAGE.windowResize();

                    // Verifica se o arquivo existe
                    if (status == "error") {
                     //   $('#kt_content_app').load('/Error/InvalidUrl');
                    //    UTILS.clear();
                    } else {
                        $('#kt_content_app').unblock();
                        KTApp.initComponents();
                    }
                });
            }
        },

        // Valida se é numerico
        isNumber: function (value) {
            return !isNaN(value);
        }

    };

})(jQuery);
