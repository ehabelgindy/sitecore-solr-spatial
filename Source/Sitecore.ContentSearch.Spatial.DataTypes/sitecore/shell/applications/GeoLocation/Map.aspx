<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Map.aspx.cs" Inherits="Sitecore.ContentSearch.Spatial.DataTypes.sitecore.shell.applications.GeoLocation.Map" %>
<!DOCTYPE html>
<html>
  <head>
    <title>Map</title>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no">
    <meta charset="utf-8">
    <style>
      html, body, #map-canvas {
        height: 100%;
        margin: 0px;
        padding: 0px
      }
    </style>
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp"></script>
    <script>
        var map;
        var marker;
        var lat = <%= !string.IsNullOrEmpty(Request.Params["lat"])?Request.Params["lat"]:"null"%>;
        var lon = <%= !string.IsNullOrEmpty(Request.Params["lon"])?Request.Params["lon"]:"null"%>;
        if (lat != null && lon != null) {
            var latlng = new google.maps.LatLng(lat, lon);
            initMapWithCoordinates(latlng);
        } else {
            initMap();
        }


        function initMap() {
            function initializeNoLatLon() {
                var mapOptions = {
                    zoom: 4,
                    center: { lat: 51.481581, lng: 0}
                };

                map = new google.maps.Map(document.getElementById('map-canvas'),
                    mapOptions);

                var marker; 

                google.maps.event.addListener(map, 'click', function(event) {
                    if (marker == null) {
                        marker = new google.maps.Marker({
                            position: event.latLng,
                            map: map,
                            title: 'Your place is here!'
                        });
                    } else {
                        marker.setPosition(event.latLng);
                    }
                    parent.document.getElementById('<%=Request.Params["ctrlid"]%>').value = event.latLng.lat().toString() + ',' + event.latLng.lng().toString();
                });
            }

            google.maps.event.addDomListener(window, 'load', initializeNoLatLon); 
        }
        function initMapWithCoordinates(latlon) {


            function initialize() {
                var mapOptions = {
                    zoom: 12,
                    center: latlng
                };

                map = new google.maps.Map(document.getElementById('map-canvas'),
                    mapOptions);

                marker = new google.maps.Marker({
                    position: latlng,
                    map: map,
                    title: 'Your place is here!'
                });

                google.maps.event.addListener(map, 'click', function(event) {
                    marker.setPosition(event.latLng);
                    parent.document.getElementById('<%=Request.Params["ctrlid"]%>').value = event.latLng.lat().toString() + ',' + event.latLng.lng().toString();
                });
            }

            google.maps.event.addDomListener(window, 'load', initialize);
        }
    </script>
  </head>
  <body>
    <form id="form1" runat="server">
    <div id="map-canvas" style="width:500px;height:380px;></div>
    </form>
  </body>
</html>

