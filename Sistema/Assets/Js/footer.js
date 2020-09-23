/* -------------------------------------------------------------------------------------------------------
' Funções do botões do rodapé do layout
' ------------------------------------------------------------------------------------------------------- */

'use strict';

var FOOTER = {};

// Variaveis para controle de calculadora
var calc_temp = '&nbsp;';
var calc_currentEntry = 0;
var calc_currentTemp = '';
var calc_valTemp = 0;
var calc_operTemp = '';
var calc_clearField = false;

(function ($) {

    FOOTER = {

        // Init
        init: function () {

            // Calculadora
            FOOTER.calculator();

            // Impede a dropdown de fechar
            $(document).on('click', '#footer_calc a, .calc-temp, .calc-result, #footer_calc, #footer_cep', function (e) {
                e.stopPropagation();
            });
            
        },

        // Busca o cep ao pressionar tecla enter
        enterSearchCep: function (e) {
            if (e.keyCode == 13) {
                FOOTER.searchCep();
            }
        },

        // CEP
        searchCep: function () {

            // Valor do cep
            var v = $.trim($('#search_cep').val());

            // Consulta e retorna o resultado
            if (v.length > 0) {
                if (v.length == 9) {
                    $('#cep_result').html(UTILS.xmlLang(169, 2).Text);
                    $.getJSON('https://viacep.com.br/ws/' + v + '/json', function (result) {
                        if (('erro' in result)) {
                            $('#cep_result').html(UTILS.xmlLang(180, 2).Text);
                        } else {
                            $('#cep_result').html(result.logradouro + '<br>' + result.bairro + '<br>' + result.localidade + ' - ' + result.uf);
                        }
                    });
                }
                else {
                    $('#cep_result').html(UTILS.xmlLang(182, 2).Text);
                }
            } else {
                $('#cep_result').html(UTILS.xmlLang(181, 2).Text);
            }
        },

        // Calculadora
        calculator: function () {

            // Tecla pressionada
            $(document).on('click', '#footer_calc a', function (e) {

                // Pega a tecla pressionada
                var k = $(this).attr('data-calc');

                // Validação para impedir erro
                if (calc_currentEntry === undefined) { calc_currentEntry = ''; }

                // Ação da tecla

                // Números
                if (UTILS.isNumber(k)) {
                    if (calc_currentEntry.toString() == '0') {
                        calc_currentEntry = k;
                    } else {
                        if ((calc_currentEntry.toString() != '0') && (!calc_clearField)) {
                            calc_currentEntry += k;
                        } else {
                            calc_currentEntry = k;
                            calc_clearField = false;
                        }
                    }
                }

                // Ponto
                if ((k == '.') && (calc_currentEntry.toString().indexOf('.') <= 0)) {
                    calc_currentEntry += '.';
                }

                // Limpa tudo
                if (k == 'C') {
                    calc_currentEntry = 0;
                    calc_currentTemp = '';
                    calc_valTemp = 0;
                    calc_clearField = false;
                    calc_operTemp = '';
                }

                // Limpa atual
                if (k == 'CE') {
                    calc_currentEntry = 0;
                }

                // Limpar
                if (k == '<') {
                    calc_currentEntry = calc_currentEntry.toString().substring(0, calc_currentEntry.length - 1);
                    if (calc_currentEntry == '') { calc_currentEntry = 0 }
                }

                // Inverter sinal
                if (k == '+/-') {
                    if (calc_currentEntry != 0) {
                        calc_currentEntry *= -1;
                    }
                }

                // Raiz quadrada
                if (k == 'R') {
                    calc_currentTemp += '&radic;' + calc_currentEntry;
                    calc_currentEntry = Math.sqrt(calc_currentEntry);
                }

                // Potência
                if (k == 'P') {
                    calc_currentTemp += '(' + calc_currentEntry + ')&sup2;';
                    calc_currentEntry = calc_currentEntry * calc_currentEntry;
                }

                // 1/x
                if (k == '1/x') {
                    calc_currentTemp += '1/(' + calc_currentEntry + ')';
                    calc_currentEntry = 1 / calc_currentEntry;
                }

                // Operações
                if (FOOTER.isOperator(k)) {
                    calc_currentTemp += ' ' + calc_currentEntry + ' ' + k;
                    if (calc_valTemp == 0) {
                        calc_valTemp = calc_currentEntry;
                    } else {
                        calc_valTemp = FOOTER.operate(calc_valTemp, calc_currentEntry, calc_operTemp);
                        calc_currentEntry = calc_valTemp;
                    }
                    calc_operTemp = k;
                    calc_clearField = true;
                }

                // Resultado
                if (k == '=') {
                    calc_valTemp = FOOTER.operate(calc_valTemp, calc_currentEntry, calc_operTemp);
                    calc_currentEntry = calc_valTemp;
                    calc_currentTemp = '';
                    calc_valTemp = 0;
                    calc_clearField = false;
                    calc_operTemp = '';
                }

                // Percentagem
                if (k == '%') {
                    if (calc_operTemp == '') {
                        calc_currentEntry = 0;
                    } else {
                        calc_currentEntry = (calc_valTemp * calc_currentEntry) / 100;
                    }
                }

                // Atualiza o visor da calculadora
                FOOTER.updateCalculatorScreen(calc_currentEntry, calc_currentTemp);
            })
        },

        // Atualiza o visor da calculadora
        updateCalculatorScreen: function (dv, tv) {
            if (dv === undefined) { dv = ''; }
            if (tv === undefined) { tv = ''; }
            if (tv == '') { tv = calc_temp; }
            $('.calc-result').html(dv);
            $('.calc-temp').html(tv);
        },

        // Verifica se é uma operação válida
        isOperator: function (value) {
            return value == '/' || value == '*' || value == '+' || value == '-';
        },

        // Execução da operação
        operate: function (a, b, operation) {
            a = parseFloat(a);
            b = parseFloat(b);
            if (operation == '+') return a + b;
            if (operation == '-') return a - b;
            if (operation == '*') return a * b;
            if (operation == '/') return a / b;
        }
    }

})(jQuery);

$(document).ready(function () {

    // Inicia funções
    FOOTER.init();

});