/* -------------------------------------------------------------------------------------------------------
' Funções do botões do rodapé do layout
' ------------------------------------------------------------------------------------------------------- */

'use strict';

var APP = {};

(function ($) {

    APP = {

        // Init
        init: function () {
            FORMS.init();
        },

        // Exibe identificador dos registros
        showIdent: function () {

            // Pega identificadores
            var id = $('#app_temp_id').val();
            var id2 = $('#app_temp_id2').val();

            Swal.fire({
                icon: 'info',
                title: 'ID: ' + id + '<br>ID2: ' + id2
            });
        },

        // Paginação nos registros
        regChange: function (a) {

            // Pega identificadores
            var id = $('#app_temp_id').val();
            var id2 = $('#app_temp_id2').val();
            var controller = $('#app_temp_controller').val();
            var action = $('#app_temp_action').val();

            switch (a) {

                // Ação de paginação
                case 'F': case 'P': case 'N': case 'L':

                    $.post(controller + "/Paginar", { id: id, id2: id2, action: a }, function (data) {
                        UTILS.menu(controller, action, data, 0);
                    });

                    break;

                // Ação não encontrada
                default:
                    Swal.fire({
                        icon: 'error',
                        title: UTILS.xmlLang(77, 1).Text,
                        text: UTILS.xmlLang(203, 2).Text
                    });
                    break;
            }
        },

        // Ações dos botões
        action: function (a) {

            // Pega identificadores
            var id = $('#app_temp_id').val();
            var id2 = $('#app_temp_id2').val();
            var controller = $('#app_temp_controller').val();
            var action = $('#app_temp_action').val();

            switch (a) {

                // Novo registro
                case 'N':
                    UTILS.menu(controller, action, 0, 0);
                    break;

                // Excluir
                case 'D':

                    // Verifica se o id foi informado e se é numérico
                    if ((id != '') && ($.isNumeric(id))) {

                        // Abre tela para confirmar senha de exclusão
                        Swal.fire({
                            icon: 'warning',
                            title: UTILS.xmlLang(204, 1).Text,
                            text: UTILS.xmlLang(205, 2).Text,
                            input: 'password',
                            showCancelButton: true,
                            confirmButtonText: UTILS.xmlLang(206, 2).Text,
                            cancelButtonText: UTILS.xmlLang(152, 2).Text,
                            showLoaderOnConfirm: false,
                            preConfirm: (password) => {

                                // Verifica se a senha foi informada
                                if (password == '') {
                                    Swal.showValidationMessage(UTILS.xmlLang(207, 2).Text);
                                } else {

                                    // Envia o formulário para validação de senha
                                    return fetch('/Login/ConfirmDelete?password=' + password).then(response => {
                                        if (!response.ok) {
                                            Swal.showValidationMessage(UTILS.xmlLang(208, 2).Text);
                                        }

                                        return response.json();
                                    }).catch(error => {
                                        Swal.showValidationMessage(UTILS.xmlLang(208, 2).Text);
                                    });
                                }
                            },
                            allowOutsideClick: () => !Swal.isLoading()
                        }).then((result) => {
                            if (result.isConfirmed) {

                                // Se retornou 0, login inválido, exibe alerta de erro
                                if (result.value.login == 0) {
                                    Swal.fire({
                                        icon: 'error',
                                        title: UTILS.xmlLang(77, 1).Text,
                                        text: UTILS.xmlLang(71, 2).Text
                                    });
                                } else {

                                    // Abre alerta de processando
                                    Swal.fire({
                                        title: UTILS.xmlLang(209, 2).Text,
                                        icon: 'info',
                                        showConfirmButton: false
                                    });

                                    // Envia o formulário para exclusão
                                    $.post(controller + "/Excluir", { id: id, id2: id2 }, function (response) {

                                        // Se retornou 0, exibe alerta de erro																									
                                        if (response.success == 0) {
                                            Swal.fire({
                                                icon: 'error',
                                                title: UTILS.xmlLang(77, 1).Text,
                                                text: response.msg
                                            });
                                        }

                                        // Se retornou 1, exibe alerta de sucesso
                                        if (response.success == 1) {
                                            let timerInterval;
                                            Swal.fire({
                                                title: UTILS.xmlLang(211, 2).Text,
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
                                                }

                                                // Ao clicar em OK, recarrega iframe com o último registro de acordo com identificador principal
                                            }).then((result) => {
                                                $.post(controller + "/Paginar", { id: id, id2: id2, action: 'L' }, function (data) {
                                                    UTILS.menu(controller, action, data, 0);
                                                });
                                            });
                                        }

                                        // Se ocorreu erro durante a validação de senha, exibe alerta				
                                    }).fail(function () {
                                        Swal.fire({
                                            title: UTILS.xmlLang(208, 2).Text,
                                            icon: 'error'
                                        });
                                    });
                                }
                            }
                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: UTILS.xmlLang(77, 1).Text,
                            text: UTILS.xmlLang(201, 2).Text
                        });
                    }
                    break;

                // Localizar
                case 'F':

                    $('.kt_content_app_gernet').block({ baseZ: 3, message: null, overlayCSS: { backgroundColor: '#ececec', opacity: 0.3, cursor: 'default' } });
                    $('#kt_buttons_app_gernet').block({ baseZ: 3, message: null, overlayCSS: { backgroundColor: '#ececec', opacity: 0.3, cursor: 'default' } });

                    // Cria campos de formulário
                    var data = [];
                    data.push({ name: 'widget_temp_name', value: 'inner_app_find' });
                    data.push({ name: 'widget_temp_controller', value: $('#app_temp_controller').val() });
                    data.push({ name: 'widget_temp_action', value: 'ListarWidget' });
                    data.push({ name: 'widget_temp_page', value: 1 });
                    data.push({ name: 'widget_temp_registers', value: 10 });
                    data.push({ name: 'widget_temp_order', value: '' });
                    data.push({ name: 'widget_temp_direction', value: 'asc' });

                    // Carrega página
                    $('#panel_search').load('/' + $('#app_temp_controller').val() + '/ListarWidget/', data, function () {
                        $("#panel_search").slideToggle("slow");
                    });
                    
                    break;

                // Gravar
                case 'S':

                    // Valida os campos do formulário
                    if (!FORMS.validate('kt_form_app_gernet')) {

                        // Processando
                        UTILS.processing();

                        // Envia o formulário
                        data = $('#kt_form_app_gernet, #app_form_control').serializeArray();
                        $.post(controller + '/Gravar', data, function (response) {

                            // Se retornou 0, exibe alerta de erro																									
                            if (response.success == 0) {
                                Swal.fire({
                                    icon: 'error',
                                    title: UTILS.xmlLang(77, 1).Text,
                                    text: response.msg
                                });
                            }

                            // Se retornou 1, exibe alerta de sucesso
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
                                    }

                                    // Ao clicar em OK, recarrega iframe com o último registro de acordo com identificador principal
                                }).then((result) => {
                                        UTILS.menu(controller, action, response.ident, response.ident2);
                                });
                            }

                        });

                    }

                    break;

                // Cancelar
                case 'C':
                    $.post(controller + '/Paginar', { id: id, id2: id2, action: 'L' }, function (data) {
                        UTILS.menu(controller, action, data, 0);
                    });
                    break;

                // Imprimir
                case 'P':
                    alert(1);
                    break;

                // Ação não encontrada
                default:
                    Swal.fire({
                        icon: 'error',
                        title: UTILS.xmlLang(77, 1).Text,
                        text: UTILS.xmlLang(203, 2).Text
                    });
                    break;
            }
        },

        // Fecha a tela de localização
        closeSearch: function () {
            $("#panel_search").slideToggle("slow");
            $('.kt_content_app_gernet').unblock();
            $('#kt_buttons_app_gernet').unblock();
        }
    };

})(jQuery);

$(document).ready(function () {

    // Inicia funções
    APP.init();

});
