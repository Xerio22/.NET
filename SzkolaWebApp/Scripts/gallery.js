var imgIndex = 0;
[...document.getElementsByClassName('gallery-image')].forEach(elem => {
    var articleId = elem.name;
    showDivs(0, articleId);
});

$('.gallery-left-arrow').click(function (e) {
    var articleId = this.classList.item(1);
    plusDivs(-1, articleId);
});
$('.gallery-right-arrow').click(function () {
    var articleId = this.classList.item(1);
    plusDivs(1, articleId);
});

function plusDivs(n, articleId) {
    showDivs(imgIndex += n, articleId);
}

function showDivs(n, articleId) {
  var images = document.getElementsByName(articleId);
  if (n > images.length) {imgIndex = 1}
  if (n < 1) {imgIndex = images.length}

  for (var i = 0; i < images.length; i++) {
     images[i].classList.add('hidden');
  }
  images[imgIndex-1].classList.remove('hidden');
}
