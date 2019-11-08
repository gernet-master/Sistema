/*
Descrição: Funções para controle da tela de login
Data: 01/01/ 2020 - v.1.0
*/

'use strict';

// Variáveis
var LOGIN = {};
var login_div = "";

(function ($) {

    LOGIN = {

        // Inicialização do script
        init: function () {
            login_div = $('#kt_login');
            LOGIN.formSwitch();
            LOGIN.submitButton();
            LOGIN.recoverButton();
            LOGIN.enterPress();
        },

        // Alterna entre o formulário de login e o de recuperar senha
        formSwitch: function () {

            // Recuperar senha
            $('#kt_login_forgot').click(function (e) {
                e.preventDefault();
                login_div.removeClass('kt-login--signin');
                login_div.removeClass('kt-login--signup');
                login_div.addClass('kt-login--forgot');
                KTUtil.animateClass(login_div.find('.kt-login__forgot')[0], 'flipInX animated');
            });

            // Login
            $('#kt_login_forgot_cancel').click(function(e) {
                e.preventDefault();
                login_div.removeClass('kt-login--forgot');
                login_div.removeClass('kt-login--signup');
                login_div.addClass('kt-login--signin');
                KTUtil.animateClass(login_div.find('.kt-login__signin')[0], 'flipInX animated');
            });

        },

        // Ação ao clicar no botão de login
        submitButton: function () {
            $('#kt_login_submit').click(function() {
                LOGIN.submitLogin();
            });
        },

        // Ação ao clicar no botão de recuperar senha
        recoverButton: function () {
            $('#kt_login_forgot_submit').click(function() {
                LOGIN.submirRecover();
            });
        },

        // Envia o formulário de login
        submitLogin: function () {
            $('#kt-form-login').validationEngine('attach', { boxField: '' });
            if ($('#kt-form-login').validationEngine('validate')) {

                // Envia o formulário para gravar
                $.post('Login/Validation', $('#kt-form-login').serializeArray(), function (data) {
                    alert(data)        
                });
            }
        },

        // Envia o formulário para recuperar senha
        submirRecover: function () {
            $('#kt-form-recover').validationEngine('attach', { boxField: '' });
            if ($('#kt-form-recover').validationEngine('validate')) {
                alert(2);
            }
        },

        // Cria ação para submeter ao clicar no enter
        enterPress: function () {

            login_div.keypress(function (e) {
                if (login_div.hasClass('kt-login--signin')) {
                    if (e.which === 13) {
                        LOGIN.submitLogin();
                        return false;
                    }
                }

                if (login_div.hasClass('kt-login--forgot')) {
                    if (e.which === 13) {
                        LOGIN.submirRecover();
                        return false;
                    }
                }
            });            

        },

    }

})(jQuery);

$(document).ready(function () {

    // LOGIN inicialização
    LOGIN.init();

    // Move focus para primeiro campo
    $('#kt-form-login #user').focus();

});

