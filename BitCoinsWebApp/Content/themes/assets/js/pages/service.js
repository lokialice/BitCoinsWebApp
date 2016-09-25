jQuery(function($) {
    "use strict";

    var SLZ = window.SLZ || {};


    /*=======================================
    =            SAMPLE FUNCTION            =
    =======================================*/

    SLZ.mainFunction = function() {
        if($(window).width() > 768){
            $('.content-plan').hover(function(){
                $('.content-plan.plan').removeClass('active');
            },function(){
                $('.content-plan.plan').addClass('active');
            });
        }
        else {
            $('.content-plan.plan').removeClass('active');
        }
    };

    /*=====  End of SAMPLE FUNCTION  ======*/




    /*======================================
    =            INIT FUNCTIONS            =
    ======================================*/

    $(document).ready(function() {
        SLZ.mainFunction();
    });

    /*=====  End of INIT FUNCTIONS  ======*/


});