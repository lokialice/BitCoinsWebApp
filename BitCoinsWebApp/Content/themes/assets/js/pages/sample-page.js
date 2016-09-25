jQuery(function($) {
    "use strict";

    var SLZ = window.SLZ || {};


    /*=======================================
    =            SAMPLE FUNCTION            =
    =======================================*/

    SLZ.sampleFunction = function() {
        $(selector).on('click', function() {

        });
    };

    /*=====  End of SAMPLE FUNCTION  ======*/




    /*======================================
    =            INIT FUNCTIONS            =
    ======================================*/

    $(document).ready(function() {
        SLZ.sampleFunction();
    });

    /*=====  End of INIT FUNCTIONS  ======*/


});
