jQuery(function($) {
    "use strict";

    var SLZ = window.SLZ || {};


    /*=======================================
    =            SAMPLE FUNCTION            =
    =======================================*/

    SLZ.mainFunction = function() {
        $(".wrapper-project-logo").slick({
            infinite: true,
            slidesToShow: 6,
            slidesToScroll: 1,
            speed: 1000,
            arrows: false,
            dots: false,
            autoplay: true,
            autoplaySpeed: 2000,
            responsive: [
            {
                breakpoint: 601,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 481,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1
                }
            }]
        });

        $(".testimonial-slider").slick({
            arrows: false,
            dots: true,
            infinite: false,
            fade: true,
            adaptiveHeight: true
        });

        $('.grid .grid-item').directionalHover({
            speed: 200
        });

        $('.wrapper-gallery .pic').directionalHover({
            speed: 200
        });
    };

    SLZ.initIsotope = function() {
        var g = $('.grid').isotope({
            layoutMode: 'packery',
            itemSelector: '.grid-item',
            percentPosition: true,
            packery: {
                columnWidth: '.grid-sizer',
            }
            // getSortData: {
            //     size: '[data-size]'
            // }
        });

        $('section.portfolio ul.nav-tabs').on('click', 'li > a:not(.default)', function() {
            var filterValue = $(this).attr('data-filter');
            g.isotope({
                filter: filterValue,
                sortBy: 'random',
                // sortAscending: false
            });
        });

        $('section.portfolio ul.nav-tabs > li > a.default').on('click', function() {
            var filterValue = $(this).attr('data-filter');
            g.isotope({
                filter: filterValue,
                sortBy: 'original-order',
                // sortAscending: true
            });
        });
    };

    /*=====  End of SAMPLE FUNCTION  ======*/




    /*======================================
    =            INIT FUNCTIONS            =
    ======================================*/

    $(document).ready(function() {
        SLZ.mainFunction();
    });

    $(window).on("load", function() {
        SLZ.initIsotope();
    });

    /*=====  End of INIT FUNCTIONS  ======*/


});
