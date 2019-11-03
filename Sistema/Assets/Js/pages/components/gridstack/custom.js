$(function () {
    var options = {
        verticalMargin: 20,
    };
    $('.grid-stack').gridstack(options);
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