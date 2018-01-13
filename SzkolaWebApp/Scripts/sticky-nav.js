var $nav = $('#nav');
var scrolledForSticky = false;
var scrolledForNormal = false;
var lastState = 0; /* normal */
var $desiredPlace = $('.nav-content-wrapper');

$(window).scroll(function() {
    if (window.innerWidth >= 505) {
       scrolledForSticky = $(this).scrollTop() > 271.03125;
       scrolledForNormal = !scrolledForSticky;

       if(scrolledForSticky && lastState != 1){
           changeToStickyNav();
           lastState = 1;
           console.log('changing to sticky');
       }
       else if(scrolledForNormal && lastState != 0) {
           changeToNormalNav();
           lastState = 0;
           console.log('back to normal state');
       }
   }
});


function changeToStickyNav() {
    makeNavSticky();
    addReturnToTopButton();
}

function makeNavSticky() {
    $nav.addClass('nav-sticky');
}

function addReturnToTopButton() {
    $desiredPlace.prepend(
        '<a class="nav-sticky-scroll-top-button">' +
            '<i class="material-icons scroll-top-ico">keyboard_arrow_up</i>' +
        '</a>'
    );

    $('.nav-sticky-scroll-top-button').click(function() {
        $('html').animate({scrollTop: 0}, 800);
    });
}

function changeToNormalNav() {
    makeNavNormal();
    removeReturnToTopButton();
}

function makeNavNormal() {
    $(".nav-content-wrapper .nav-sticky-scroll-top-button").remove();
}

function removeReturnToTopButton() {
    $nav.removeClass('nav-sticky');
}
