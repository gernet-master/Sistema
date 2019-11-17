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

            // Define o formul�rio
            if ($('#kt_login').length > 0) {
                login_div = $('#kt_login');
            }

            if ($('#kt_password').length > 0) {
                login_div = $('#kt_password');
            }

            // A��es
            LOGIN.formSwitch();
            LOGIN.submitButton();
            LOGIN.recoverButton();
            LOGIN.changePassword();
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
            $('#kt_login_forgot_cancel').click(function (e) {
                e.preventDefault();
                login_div.removeClass('kt-login--forgot');
                login_div.removeClass('kt-login--signup');
                login_div.addClass('kt-login--signin');
                KTUtil.animateClass(login_div.find('.kt-login__signin')[0], 'flipInX animated');
            });

        },

        // A��o ao clicar no bot�o de login
        submitButton: function () {
            $('#kt_login_submit').click(function () {
                LOGIN.submitLogin();
            });
        },

        // A��o ao clicar no bot�o de recuperar senha
        recoverButton: function () {
            $('#kt_login_forgot_submit').click(function () {
                LOGIN.submirRecover();
            });
        },

        // A��o ao clicar no bot�o de alterar senha
        changePassword: function () {
            $('#kt_password_submit').click(function () {
                LOGIN.submirChangePassword();
            });
        },

        // Envia o formul�rio de login
        submitLogin: function () {

            $('#kt-form-login').validationEngine('attach', { boxField: '' });
            if ($('#kt-form-login').validationEngine('validate')) {

                // Processando
                swal.fire({
                    title: UTILS.xmlLang(68, 2).Text,
                    type: 'warning',
                    showConfirmButton: false,
                    allowOutsideClick: false
                });

                // Envia o formul�rio
                $.post('/Login/Validation', $('#kt-form-login').serializeArray(), function (data) {

                    // Retorno
                    var arr = data.split("|");

                    // Se retornou 0, exibe alerta de erro																										
                    if (arr[0] == 0) {
                        swal.fire({
                            title: arr[1],
                            type: 'error',
                            showConfirmButton: true,
                            allowOutsideClick: false
                        });
                    }

                    // Se retornou 1, redireciona para p�gina de altera��o de senha
                    if (arr[0] == 1) {
                        $(location).attr('href', '/Login/Password');
                    }

                    // Se retornou 2, carrega p�gina inicial
                    if (arr[0] == 2) {
                        $(location).attr('href', 'Home');
                    }

                    // Se retornou 3, usu�rio j� conectado																										
                    if (arr[0] == 3) {
                        swal.fire({
                            title: UTILS.xmlLang(75, 2).Text,
                            text: UTILS.xmlLang(76, 2).Text + '?',
                            type: 'question',
                            showConfirmButton: true,
                            showCancelButton: true,
                            confirmButtonText: UTILS.xmlLang(49, 2).Text,
                            cancelButtonText: UTILS.xmlLang(50, 2).Text,
                            allowOutsideClick: false
                        }).then(function (result) {
                            if (result.value) {

                                // Encerra a conx�o atual
                                $.post('/Home/Logout').done(function (data) {

                                    // Chama fun��o de valida��o de login novamente
                                    LOGIN.submitLogin();

                                });
                            }
                        });

                    }

                });
            }
        },

        // Envia o formul�rio para recuperar senha
        submirRecover: function () {
            $('#kt-form-recover').validationEngine('attach', { boxField: '' });
            if ($('#kt-form-recover').validationEngine('validate')) {

                // Envia o formul�rio
                $.post('/Login/RecoverPassword', $('#kt-form-recover').serializeArray(), function (data) {

                    // Retorno
                    var arr = data.split("|");

                    // Se retornou 0, exibe alerta de erro																										
                    if (arr[0] == 0) {
                        swal.fire({
                            title: arr[1],
                            type: 'error'
                        });
                    }

                    // Se retornou 1, carrega p�gina inicial
                    if (arr[0] == 1) {
                        $(location).attr('href', '/Home');
                    }

                });
            }
        },

        // Envia o formul�rio para alterar a senha
        submirChangePassword: function () {
            $('#kt-form-password').validationEngine('attach', { boxField: '' });
            if ($('#kt-form-password').validationEngine('validate')) {

                // Envia o formul�rio
                $.post('/Login/ChangePassword', $('#kt-form-password').serializeArray(), function (data) {

                    // Retorno
                    var arr = data.split("|");

                    // Se retornou 0, exibe alerta de erro																										
                    if (arr[0] == 0) {
                        swal.fire({
                            title: arr[1],
                            type: 'error'
                        });
                    }

                    // Se retornou 1, carrega p�gina inicial
                    if (arr[0] == 1) {
                        $(location).attr('href', '/Home');
                    }

                });
            }
        },

        // Cria a��o para submeter ao clicar no enter
        enterPress: function () {

            login_div.keypress(function (e) {

                // Login
                if (login_div.hasClass('kt-login--signin')) {
                    if (e.which === 13) {
                        LOGIN.submitLogin();
                        return false;
                    }
                }

                // Esqueceu a senha
                if (login_div.hasClass('kt-login--forgot')) {
                    if (e.which === 13) {
                        LOGIN.submirRecover();
                        return false;
                    }
                }

                // Alterar a senha
                if (login_div.hasClass('kt-login--password')) {
                    if (e.which === 13) {
                        LOGIN.submirChangePassword();
                        return false;
                    }
                }
            });

        }

    };

})(jQuery);

$(document).ready(function () {

    // LOGIN inicializa��o
    LOGIN.init();

    // Move focus para primeiro campo
    $('#kt-form-login #user').focus();

});

