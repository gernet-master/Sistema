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

            // Define o formulário
            if ($('#kt_login').length > 0) {
                login_div = $('#kt_login');
            }

            if ($('#kt_password').length > 0) {
                login_div = $('#kt_password');
            }

            // Ações
            LOGIN.formSwitch();
            LOGIN.submitButton();
            LOGIN.recoverButton();
            LOGIN.changePassword();
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

        // Ação ao clicar no botão de alterar senha
        changePassword: function () {
            $('#kt_password_submit').click(function () {
                LOGIN.submirChangePassword();
            });
        },

        // Envia o formulário de login
        submitLogin: function () {

            $('#kt-form-login').validationEngine('attach', { boxField: '' });
            if ($('#kt-form-login').validationEngine('validate')) {

                // Processando
                const Swal = swal.fire({
                    title: UTILS.xmlLang(68, 2).Text,
                    type: 'warning'
                });

                // Envia o formulário para gravar
                $.post('/Login/Validation', $('#kt-form-login').serializeArray(), function (data) {

                    // Retorno
                    var arr = data.split("|");
                    
                    // Se retornou 0, exibe alerta de erro																										
                    if (arr[0] == 0) {
                        Swal.update({
                            title: arr[1],
                            type: 'error'
                        });
                    }

                    // Se retornou 1, redireciona para página de alteração de senha
                    if (arr[0] == 1) {
                        $(location).attr('href', 'Login/Password');
                    }

                    // Se retornou 2, carrega página inicial
                    if (arr[0] == 2) {
                        $(location).attr('href', 'Home');
                    }

                    // Se retornou 3, usuário já conectado																										
                    //if (arr[0] == 3) {
                    //    swal({
                    //        title: arr[1],
                    //        text: xmlLang("si365", 2).Text + '?',
                    //        type: 'question',
                    //        showConfirmButton: true,
                    //        showCancelButton: true,
                    //        confirmButtonText: xmlLang('sc13', 2).Text
                    //    }).then(function () {

                    //        // Encerra a conxão atual
                    //        $.post('assets/includes/', { inc: '/assets/includes/login', action: 'session' }).done(function (data) {

                    //            // Chama função de validação de login novamente
                    //            LOGIN.validateForm();

                    //        });

                    //    }).catch(swal.noop);
                    //}

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

        // Envia o formulário para alterar a senha
        submirChangePassword: function () {
            $('#kt-form-password').validationEngine('attach', { boxField: '' });
            if ($('#kt-form-password').validationEngine('validate')) {

                // Envia o formulário para gravar
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

                    // Se retornou 1, carrega página inicial
                    if (arr[0] == 1) {
                        $(location).attr('href', '/Home');
                    }

                });
            }
        },

        // Cria ação para submeter ao clicar no enter
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

        },

    }

})(jQuery);

$(document).ready(function () {

    // LOGIN inicialização
    LOGIN.init();

    // Move focus para primeiro campo
    $('#kt-form-login #user').focus();

});

