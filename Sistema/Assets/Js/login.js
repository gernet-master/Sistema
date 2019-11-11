/*
Descri��o: Fun��es para controle da tela de login
Data: 01/01/ 2020 - v.1.0
*/

'use strict';

// Vari�veis
var LOGIN = {};
var login_div = "";

(function ($) {

    LOGIN = {

        // Inicializa��o do script
        init: function () {
            login_div = $('#kt_login');
            LOGIN.formSwitch();
            LOGIN.submitButton();
            LOGIN.recoverButton();
            LOGIN.enterPress();
        },

        // Alterna entre o formul�rio de login e o de recuperar senha
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

        // A��o ao clicar no bot�o de login
        submitButton: function () {
            $('#kt_login_submit').click(function() {
                LOGIN.submitLogin();
            });
        },

        // A��o ao clicar no bot�o de recuperar senha
        recoverButton: function () {
            $('#kt_login_forgot_submit').click(function() {
                LOGIN.submirRecover();
            });
        },

        // Envia o formul�rio de login
        submitLogin: function () {
            $('#kt-form-login').validationEngine('attach', { boxField: '' });
            if ($('#kt-form-login').validationEngine('validate')) {

                // Envia o formul�rio para gravar
                $.post('Login/Validation', $('#kt-form-login').serializeArray(), function (data) {

                    // Retorno
                    var arr = data.split("|");
                    
                    // Se retornou 0, exibe alerta de erro																										
                    if (arr[0] == 0) {
                        swal.fire({
                            title: arr[1],
                            type: 'error'
                        });
                    }

                    // Se retornou 1, redireciona para p�gina de altera��o de senha
                    if (arr[0] == 1) {
                        $(location).attr('href', 'ChangePassword');
                    }

                    // Se retornou 2, carrega p�gina inicial
                    if (arr[0] == 2) {
                        $(location).attr('href', 'Home');
                    }

                    // Se retornou 3, usu�rio j� conectado																										
                    if (arr[0] == 3) {
                        swal({
                            title: arr[1],
                            text: xmlLang("si365", 2).Text + '?',
                            type: 'question',
                            showConfirmButton: true,
                            showCancelButton: true,
                            confirmButtonText: xmlLang('sc13', 2).Text
                        }).then(function () {

                            // Encerra a conx�o atual
                            $.post('assets/includes/', { inc: '/assets/includes/login', action: 'session' }).done(function (data) {

                                // Chama fun��o de valida��o de login novamente
                                LOGIN.validateForm();

                            });

                        }).catch(swal.noop);
                    }

                });
            }
        },

        // Envia o formul�rio para recuperar senha
        submirRecover: function () {
            $('#kt-form-recover').validationEngine('attach', { boxField: '' });
            if ($('#kt-form-recover').validationEngine('validate')) {
                alert(2);
            }
        },

        // Cria a��o para submeter ao clicar no enter
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

    // LOGIN inicializa��o
    LOGIN.init();

    // Move focus para primeiro campo
    $('#kt-form-login #user').focus();

});

