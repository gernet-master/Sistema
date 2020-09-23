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
            } else {
                if ($('#kt_password').length > 0) {
                    login_div = $('#kt_password');
                }
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

            if (!FORMS.validate('kt-form-login')) {

                // Processando
                UTILS.processing();

                // Envia o formul�rio
                $.post('/Validation', $('#kt-form-login').serializeArray(), function (response) {

                    // Se retornou 0, exibe alerta de erro	
                    if (response.success == 0) {
                        Swal.fire({
                            icon: 'error',
                            title: response.msg,
                            showConfirmButton: true,
                            allowOutsideClick: false
                        });
                    }

                    // Se retornou 1, redireciona para p�gina de altera��o de senha
                    if (response.success == 1) {
                        $(location).attr('href', '/Password');
                    }

                    // Se retornou 2, carrega p�gina inicial
                    if (response.success == 2) {
                        $(location).attr('href', 'Home');
                    }

                    // Se retornou 3, usu�rio j� conectado																										
                    if (response.success == 3) {
                        Swal.fire({
                            title: UTILS.xmlLang(75, 2).Text,
                            text: UTILS.xmlLang(76, 2).Text + '?',
                            icon: 'question',
                            showConfirmButton: true,
                            showCancelButton: true,
                            confirmButtonText: UTILS.xmlLang(49, 2).Text,
                            cancelButtonText: UTILS.xmlLang(50, 2).Text,
                            allowOutsideClick: false
                        }).then(function (result) {
                            if (result.value) {

                                // Encerra a conx�o atual
                                $.post('/Logout').done(function (data) {

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

            if (!FORMS.validate('kt-form-recover')) {

                // Processando
                UTILS.processing();

                // Envia o formul�rio
                $.post('/RecoverPassword', $('#kt-form-recover').serializeArray(), function (response) {

                    // Se retornou 0, exibe alerta de erro																									
                    if (response.success == 0) {
                        Swal.fire({
                            icon: 'error',
                            title: response.msg
                        });
                    }

                    // Se retornou 1, carrega p�gina inicial
                    if (response.success == 1) {
                        let timerInterval;
                        Swal.fire({
                            title: response.msg,
                            icon: 'success',
                            timer: 5000,
                            showConfirmButton: true,
                            confirmButtonText: UTILS.xmlLang(93, 2).Text + ' (5)',
                            timerProgressBar: true,
                            allowOutsideClick: false,
                            onBeforeOpen: () => {
                                timerInterval = setInterval(() => {
                                    Swal.getConfirmButton().textContent = UTILS.xmlLang(93, 2).Text + ' (' + (Math.ceil(Swal.getTimerLeft() / 1000)) + ')';
                                }, 1000);
                            },
                            onClose: () => {
                                clearInterval(timerInterval);
                                $(location).attr('href', '/Home');
                            }
                        }).then((result) => {
                            if (result.dismiss === Swal.DismissReason.timer) {
                                $(location).attr('href', '/Home');
                            }
                        });
                    }

                });
            }
        },

        // Envia o formul�rio para alterar a senha
        submirChangePassword: function () {

            if (!FORMS.validate('kt-form-password')) {

                // Processando
                UTILS.processing();

                // Envia o formul�rio
                $.post('/ChangePassword', $('#kt-form-password').serializeArray(), function (response) {

                    // Retorno
                    if (response.success == 0) {
                        Swal.fire({
                            icon: 'error',
                            title: response.msg
                        });
                    }

                    // Se retornou 1, carrega p�gina inicial
                    if (response.success == 1) {
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

