function initMap() {
    var szkola = {lat: 52.171040, lng: 20.438797};

    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 17,
        mapTypeId: 'hybrid',
        center: szkola
    });

    var marker = new google.maps.Marker({
        position: szkola,
        map: map
    });
}
