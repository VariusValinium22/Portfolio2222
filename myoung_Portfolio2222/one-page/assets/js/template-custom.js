$(function () {
    "use strict";
    //preloader
    $(window).preloader({
        delay:500
    });
//sticky header
    $(window).on("resize", function () {
        $(".navbar-collapse").css({maxHeight: $(window).height() - $(".navbar-header").height() + "px"});
    });

//sticky header on scroll
    $(".sticky-header").sticky({topSpacing: 0});
//shrink header trnsparent
    $(window).on("scroll", function () {
        if ($(document).scrollTop() > 50) {
            $('.transparent-nav').addClass('shrink');
        } else {
            $('.transparent-nav').removeClass('shrink');
        }
    });

    //animated scroll menu
    $(window).on("scroll", function () {
        var scroll = $(window).scrollTop();
        if (scroll > 0) {
            $('.navbar-transparent').addClass('shrink');
        }
        if (scroll <= 0) {
            $('.navbar-transparent').removeClass('shrink');
        }
    });

//smooth scroll
    smoothScroll.init({
        selector: '[data-scroll]', // Selector for links (must be a class, ID, data attribute, or element tag)
        speed: 1000, // Integer. How fast to complete the scroll in milliseconds
        easing: 'easeInOutCubic', // Easing pattern to use
        offset: 0, // Integer. How far to offset the scrolling anchor location in pixels
        callback: function (anchor, toggle) {} // Function to run after scrolling
    });



//Auto Close Responsive Navbar on Click
    function close_toggle() {
        if ($(window).width() <= 768) {
            $('.navbar-collapse a').on('click', function () {
                $('.navbar-collapse').collapse('hide');
            });
        } else {
            $('.navbar a').off('click');
        }
    }
    close_toggle();
    $(window).resize(close_toggle);

    //hero text fade flexslider
    $(window).on("load", function () {
        $('.flexslider').flexslider({
            controlNav: false,
            directionNav: false,
            slideshowSpeed: 4000
        });
    });

    //home slider
    $(window).on("load", function () {
        $('.main-slider').flexslider({
            controlNav: false,
            directionNav: true,
            slideshowSpeed: 4000,
            prevText: "<i class='ion-chevron-left'></i>",
            nextText: "<i class='ion-chevron-right'></i>"
        });
    });

    //testimonials
    $(window).on("load", function () {
        $('.testi-slider').flexslider({
            controlNav: false,
            directionNav: false,
            slideshowSpeed: 4000
        });
    });

    //back to top
    //Check to see if the window is top if not then display button
    $(window).on("scroll", function () {
        if ($(this).scrollTop() > 300) {
            $('.scrollToTop').fadeIn();
        } else {
            $('.scrollToTop').fadeOut();
        }
    });

    //Click event to scroll to top
    $('.scrollToTop').on("click", function () {
        $('html, body').animate({scrollTop: 0}, 800);
        return false;
    });
});

