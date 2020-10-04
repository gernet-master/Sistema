/*
Descrição: Funções para tela de cadastro de menus
Data: 01/01/ 2020 - v.1.0
*/

function loadTextLanguage(v) {
    if (v == '') {
        $('#texto_idioma').html('');
    } else {
        $('#texto_idioma').html(UTILS.xmlLang(v, 2).Text);
    }
}
