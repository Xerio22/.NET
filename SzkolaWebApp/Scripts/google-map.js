function initMap() {
    var uluru = {lat: 52.171040, lng: 20.438797};

    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 17,
        mapTypeId: 'satellite',
        center: uluru
    });

    var marker = new google.maps.Marker({
        position: uluru,
        map: map
    });
}
