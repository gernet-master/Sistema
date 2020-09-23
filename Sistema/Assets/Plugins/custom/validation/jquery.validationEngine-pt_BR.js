(function($){
    $.fn.validationEngineLanguage = function(){};
    $.validationEngineLanguage = {
        newLang: function(){
            $.validationEngineLanguage.allRules = {
                "required": {
                    "regex": "none",
                    "alertText": "Obrigatório",
                    "alertTextCheckboxMultiple": "Selecione uma opção",
                    "alertTextCheckboxe": "obrigatório",
                    "alertTextDateRange": "Ambas as datas do intervalo são obrigatórias"
                },
				"requiredIf": {
					"regex": "none",
                    "alertText": "obrigatório",
				},
                "requiredInFunction": { 
                    "func": function(field, rules, i, options){
                        return (field.val() == "test") ? true : false;
                    },
                    "alertText": "Field must equal test"
                },
                "dateRange": {
                    "regex": "none",
                    "alertText": "Intervalo de datas inválido"
                },
                "dateTimeRange": {
                    "regex": "none",
                    "alertText": "Intervalo de data e hora inválido"
                },
                "minSize": {
                    "regex": "none",
                    "alertText": "mínimo de",
                    "alertText2": "caractere(s)"
                },
                "maxSize": {
                    "regex": "none",
                    "alertText": "máximo de",
                    "alertText2": "caractere(s)"
                },
				"groupRequired": {
                    "regex": "none",
                    "alertText": "Você deve preencher um dos seguintes campos"
                },
				"checkboxRequired": {
                    "regex": "none",
                    "alertText": "você deve selecionar pelo menos 1 opção"
                },
                "min": {
                    "regex": "none",
                    "alertText": "Valor mínimo é " 
                },
                "max": {
                    "regex": "none",
                    "alertText": "Valor máximo é "
                },
                "past": {
                    "regex": "none",
                    "alertText": "Data anterior a "
                },
                "future": {
                    "regex": "none",
                    "alertText": "Data posterior a "
                },	
                "maxCheckbox": {
                    "regex": "none",
                    "alertText": "Máximo de ",
                    "alertText2": " opções permitidas" 
                },
                "minCheckbox": {
                    "regex": "none",
                    "alertText": "Favor selecionar ",
                    "alertText2": " opção(ões)" 
                },
                "equals": {
                    "regex": "none",
                    "alertText": "os campos devem ser iguais"
                },
                "creditCard": {
                    "regex": "none",
                    "alertText": "Número de cartão de crédito inválido"
                },
				"time": {
                    "regex": /^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$/,
                    "alertText": "a senha não pode ter espaços"
                },
                "phone": {
                    "regex": /^([\+][0-9]{1,3}[ \.\-])?([\(]{1}[0-9]{2,6}[\)])?([0-9 \.\-\/]{3,20})((x|ext|extension)[ ]?[0-9]{1,4})?$/,
                    "alertText": "Número de telefone inválido"
                },
                "email": {
                    "regex": /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i,
                    "alertText": "e-mail inválido"
                },
                "integer": {
                    "regex": /^[\-\+]?\d+$/,
                    "alertText": "Número inteiro inválido"
                },
                "number": {
                    "regex": /^[\-\+]?((([0-9]{1,3})([,][0-9]{3})*)|([0-9]+))?([\.]([0-9]+))?$/,
                    "alertText": "Número decimal inválido"
                },
                "date": {
                    "regex": /^\d{4}[\/\-](0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])$/,
                    "alertText": "data inválida, deve ser no formato dd/mm/aaaa"
                },
                "ipv4": {
                    "regex": /^((([01]?[0-9]{1,2})|(2[0-4][0-9])|(25[0-5]))[.]){3}(([0-1]?[0-9]{1,2})|(2[0-4][0-9])|(25[0-5]))$/,
                    "alertText": "endereço ip inválido"
                },
                "url": {
                    "regex": /[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/,
                    "alertText": "url inválida"
                },
                "onlyNumberSp": {
                    "regex": /^[0-9\ ]+$/,
                    "alertText": "Apenas números"
                },
                "onlyLetterSp": {
                    "regex": /^[a-zA-Z\ \']+$/,
                    "alertText": "Apenas letras"
                },
                "onlyLetterNumber": {
                    "regex": /^[0-9a-zA-Z]+$/,
                    "alertText": "Somente letras e números"
                },
                "real": {
                	// Brazilian (Real - R$) money format
                	"regex": /^([1-9]{1}[\d]{0,2}(\.[\d]{3})*(\,[\d]{0,2})?|[1-9]{1}[\d]{0,}(\,[\d]{0,2})?|0(\,[\d]{0,2})?|(\,[\d]{1,2})?)$/,
                    "alertText": "Número decimal inválido"
                },
				"captcha": {
					"regex": "none",
					"alertText": "Código de verificação deve possuir 8 caracteres",
					"alertText2": "Código de verificação incorreto",
				},
				"existValue": {
					"regex": "none",
					"alertText": "Já existe outro cadastro com esta descrição"
				},
				"notEquals": {
                    "regex": "none",
                    "alertText": "Os campos não podem ser iguais"
                },
				"cpf": {
                    "regex": "none",
                    "alertText": "cpf inválido"
                },
				"cnpj": {
                    "regex": "none",
                    "alertText": "cnpj inválido"
                },
            };
            
        }
    };

    $.validationEngineLanguage.newLang();
    
})(jQuery);

function valida_cpf_cnpj ( valor ) {
    var valida = verifica_cpf_cnpj( valor );
    valor = valor.toString();
    valor = valor.replace(/[^0-9]/g, '');
    if ( valida === 'CPF' ) {
        return valida_cpf( valor );
    } 
    else if ( valida === 'CNPJ' ) {
        return valida_cnpj( valor );
    } 
    else {
        return false;
    }    
}

function verifica_cpf_cnpj ( valor ) {
    valor = valor.toString();
    valor = valor.replace(/[^0-9]/g, '');
    if ( valor.length === 11 ) {
        return 'CPF';
    } 
    else if ( valor.length === 14 ) {
        return 'CNPJ';
    } 
    else {
        return false;
    }
} 

function valida_cpf( valor ) {
    valor = valor.toString();
    valor = valor.replace(/[^0-9]/g, '');
    var digitos = valor.substr(0, 9);
    var novo_cpf = calc_digitos_posicoes( digitos, 10, 0 );
    var novo_cpf = calc_digitos_posicoes( novo_cpf, 11, 0 );
    if ( novo_cpf === valor ) {		
		if ((valor != 00000000000) && (valor != 11111111111) && (valor != 22222222222) && (valor != 33333333333) && (valor != 44444444444) && (valor != 55555555555) && (valor != 66666666666) && (valor != 77777777777) && (valor != 88888888888) && (valor != 99999999999)) {
    	    return true;
		} else {
    	    return false;			
		}
    } else {
        return false;
    }
} 

function valida_cnpj ( valor ) {
    valor = valor.toString();
    valor = valor.replace(/[^0-9]/g, '');
    var cnpj_original = valor;
    var primeiros_numeros_cnpj = valor.substr( 0, 12 );
    var primeiro_calculo = calc_digitos_posicoes( primeiros_numeros_cnpj, 5, 0 );
    var segundo_calculo = calc_digitos_posicoes( primeiro_calculo, 6, 0 );
    var cnpj = segundo_calculo;
    if ( cnpj === cnpj_original ) {
		if ((valor != 00000000000000) && (valor != 11111111111111) && (valor != 22222222222222) && (valor != 33333333333333) && (valor != 44444444444444) && (valor != 55555555555555) && (valor != 66666666666666) && (valor != 77777777777777) && (valor != 88888888888888) && (valor != 99999999999999)) {
    	    return true;
		} else {
    	    return false;			
		}
    }
    return false;
}

function calc_digitos_posicoes( digitos, posicoes, soma_digitos) {
    digitos = digitos.toString();
    for ( var i = 0; i < digitos.length; i++  ) {
        soma_digitos = soma_digitos + ( digitos[i] * posicoes );
        posicoes--;
        if ( posicoes < 2 ) {
            posicoes = 9;
        }
    }
    soma_digitos = soma_digitos % 11;
    if ( soma_digitos < 2 ) {
        soma_digitos = 0;
    } else {
        soma_digitos = 11 - soma_digitos;
    }
    var cpf = digitos + soma_digitos;
    return cpf;    
}