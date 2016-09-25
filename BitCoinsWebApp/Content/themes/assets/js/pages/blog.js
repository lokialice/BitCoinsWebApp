jQuery(function($) {
    "use strict";

    var SLZ = window.SLZ || {};


    /*=======================================
    =            SAMPLE FUNCTION            =
    =======================================*/

    SLZ.mainFunction = function() {
        if($("#player2").length > 0){
            $("#player2").mediaelementplayer({
                audioWidth: '100%',
                audioHeight: 50
            });
        }
        if($("#player3").length > 0){
            $("#player3").mediaelementplayer({
                audioWidth: '100%',
                audioHeight: 50
            });
        }

        // js for calendar
        $('.input-daterange, .archive-datepicker').datepicker({
            format: 'mm/dd/yy',
            maxViewMode: 0
        });

        $('.gallery-widget .thumb').directionalHover({
            speed: 200
        });
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
