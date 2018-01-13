
var imgIndex = 0;
showDivs(imgIndex);

$('.gallery-left-arrow').click(function(){plusDivs(-1)});
$('.gallery-right-arrow').click(function(){plusDivs(1)});

function plusDivs(n) {
    showDivs(imgIndex += n);
}

function showDivs(n) {
  var images = document.getElementsByClassName("gallery-image");
  if (n > images.length) {imgIndex = 1}
  if (n < 1) {imgIndex = images.length}

  for (var i = 0; i < images.length; i++) {
     images[i].classList.add('hidden');
  }
  images[imgIndex-1].classList.remove('hidden');
}
