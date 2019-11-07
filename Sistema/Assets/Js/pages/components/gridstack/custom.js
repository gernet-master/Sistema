$(function () {

    // Inicia grid
    var grid = $('.grid-stack').gridstack();

    // Função minimizar/maximizar
    $(".gernet-widget-toogle").click(function () {

        // Minimizar
        if ($(this).closest('.kt-portlet').hasClass('kt-portlet--collapse')) {

            // Seta o min height para 1
            $(".grid-stack").data("gridstack").minHeight($(this).closest('.grid-stack-item'), 1);

            // Remove a opção de redimensionar
            $(".grid-stack").data("gridstack").resizable($(this).closest('.grid-stack-item'), false);

            // Reduz o widget para 1 de altura
            $(".grid-stack").data("gridstack").resize($(this).closest('.grid-stack-item'), null, 1);

            // Altera o texto do botão
            $(this).find('span').html('Maximizar');

        // Maximizar
        } else {

            // Seta o min height para o tamanho original
            $(".grid-stack").data("gridstack").minHeight($(this).closest('.grid-stack-item'), parseInt($(this).closest('.grid-stack-item').attr('data-gs-temp-height')));

            // Insere a opção de redimensionar
            $(".grid-stack").data("gridstack").resizable($(this).closest('.grid-stack-item'), true);

            // Retorna o widget para o tamanho original
            $(".grid-stack").data("gridstack").resize($(this).closest('.grid-stack-item'), null, parseInt($(this).closest('.grid-stack-item').attr('data-gs-original-height')));

            // Altera o texto do botão
            $(this).find('span').html('Minimizar');

        }
    });

    // Seta o campo tamanho original ao redimensionar widget
    $('.grid-stack').on('gsresizestop', function (event, elem) {
        $(elem).attr('data-gs-original-height',  $(elem).attr('data-gs-height'));
    });

    // Grava a nova configuração da grid sempre que houver ação
    $('.grid-stack').on('change', function (event, items) {
        for (var i = 0; i < items.length; i++) {
            var widget = items[i].id;
            var height = items[i].height;
            var width = items[i].width;
            var top = items[i].y;
            var left = items[i].x;
        }
    });        

});



