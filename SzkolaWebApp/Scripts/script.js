function textAreaAdjust(ta) {
    ta.style.height = "8em"
    ta.style.height = (0 + ta.scrollHeight) + 'px'
}

$('.add-article-header').click(function () {
    $(".add-article-body").slideToggle();
});

$(function () {
    var path = window.location.pathname;

    if (path.startsWith("/Home/Edit") || path.startsWith("/Home/InsertPhotosToArticle")) {
        $('.add-article-body').slideToggle();
    }
});