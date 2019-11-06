$(function () {
    var grid = $('.grid-stack').gridstack();

    $(".gernet-widget-toogle").click(function () {
        if ($(this).closest('.kt-portlet').hasClass('kt-portlet--collapse')) {
      //      $(this).attr('data-original-title', 'Reduzir');
     //       $('.tooltip-inner').html('Reduzir');
      //      $(this).closest('.kt-portlet__head').css('border-bottom', '1px solid #ebedf2');
  //          $(this).closest('.grid-stack-item').animate({ height: ((($(this).closest('.grid-stack-item').attr('data-gs-original-height') - 1) * 80) + 60) }, 400, function () { $(this).closest('.grid-stack-item-content').css('overflow', 'auto'); });
//            $(this).closest('.grid-stack-item').attr('data-gs-height', $(this).closest('.grid-stack-item').attr('data-gs-original-height'));
        } else {
     //       $(this).attr('data-original-title', 'Expandir');
     //       $('.tooltip-inner').html('Expandir');
     //       $(this).closest('.kt-portlet__head').css('border-bottom', 'unset');
      //      $(this).closest('.grid-stack-item-content').css('overflow', 'hidden');
          //  $(this).closest('.grid-stack-item').animate({ height: 60 }, 400);
          //  $(this).closest('.grid-stack-item').attr('data-gs-height', 1);

            
        }
       // $(this).closest('.grid-stack').css('height', 60)
    })

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




