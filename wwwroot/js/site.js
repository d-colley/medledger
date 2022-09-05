// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//for Jamaica map - need to illustrate the health regions and health centers

jQuery(function ($) {
    // Asynchronously Load the map API 
    var script = document.createElement('script');
    script.src = "https://maps.googleapis.com/maps/api/js?sensor=false&callback=initialize";
    document.body.appendChild(script);
});
function initialize() {
    var map;
    var bounds = new google.maps.LatLngBounds();
    var mapOptions = {
        mapTypeId: 'roadmap'
    };
    // Display a map on the page
    map = new google.maps.Map(document.getElementById("map_tuts"), mapOptions);
    map.setTilt(45);
    // Multiple Markers
    var markers = [
        ['UHWI', 18.0114, -76.7445],
        ['Princess Margaret', 17.880941, -76.390469],
        ['Spanish Town Public Hospital', 17.99263805011246, -76.9482042902198],
        ['Port Maria Hospital', 18.358539626076517, -76.89515794603373],
        ['Black River', 18.02681553791359, -77.85909259021909],
    ];
    // Info Window Content
    var infoWindowContent = [
        ['<div class="info_content">' +
            '<h3>UHWI</h3>' +
            '<p></p>' + '</div>'],
        ['<div class="info_content">' +
            '<h3>Princess Margaret</h3>' +
            '<p></p>' + '</div>'],
        ['<div class="info_content">' +
            '<h3>Spanish Town Public Hospital</h3>' +
            '<p></p>' + '</div>'],
        ['<div class="info_content">' +
            '<h3>Port Maria Hospital</h3>' +
            '<p></p>' + '</div>'],
        ['<div class="info_content">' +
            '<h3>Black River</h3>' +
            '<p>Lorem Ipsum  Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum</p>' + '</div>'],
    ];
    // Display multiple markers on a map
    var infoWindow = new google.maps.InfoWindow(), marker, i;
    // Loop through our array of markers & place each one on the map  
    for (i = 0; i < markers.length; i++) {
        var position = new google.maps.LatLng(markers[i][1], markers[i][2]);
        bounds.extend(position);
        marker = new google.maps.Marker({
            position: position,
            map: map,
            title: markers[i][0]
        });
        // Each marker to have an info window    
        google.maps.event.addListener(marker, 'click', (function (marker, i) {
            return function () {
                infoWindow.setContent(infoWindowContent[i][0]);
                infoWindow.open(map, marker);
            }
        })(marker, i));
        // Automatically center the map fitting all markers on the screen
        map.fitBounds(bounds);
    }
    // Override our map zoom level once our fitBounds function runs (Make sure it only runs once)
    var boundsListener = google.maps.event.addListener((map), 'bounds_changed', function (event) {
        this.setZoom(8);
        google.maps.event.removeListener(boundsListener);
    });
}