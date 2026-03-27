<%@ Page Language="C#" AutoEventWireup="true" CodeFile="site_location.aspx.cs" Inherits="site_location" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Select Site Longitude and Latitude</title>
    <script src="/openlayers/jquery.min.js"></script>
    <script src="/openlayers/OpenLayers.js"></script>


    <script type = "text/javascript">
        function OnClose() {
            if (window.opener != null && !window.opener.closed) {
                window.opener.HideModalDiv();
            }
        }
        window.onunload = OnClose;
</script>
</head>
<body>

      <div id="map" style="height:400px"></div>

     <script type="text/javascript">

      

         $(document).ready(function () {

             var swidth = screen.width;
             var mapdiv = document.getElementById("map");

             mapdiv.style.width = "970px";

             map = new OpenLayers.Map("map");
             map.addLayer(new OpenLayers.Layer.OSM());
             var lonLat = new OpenLayers.LonLat(103.805069, 1.369751)
                          .transform(new OpenLayers.Projection("EPSG:4326"),
                             map.getProjectionObject());

             map.setCenter(lonLat, 11);



             var markerName = "/openlayers/img/0.gif";

             var lonLat1 = new OpenLayers.LonLat(103.805069, 1.369751)
                .transform(new OpenLayers.Projection("EPSG:4326"),
                  map.getProjectionObject());



             map.events.register("click", map, function (e) {
                 var toProjection = new OpenLayers.Projection("EPSG:4326");
                 var position = map.getLonLatFromPixel(e.xy).transform(map.getProjectionObject(), toProjection);


                 if (confirm("The selected co-ordinates are \nLongitude: " + position.lon.toFixed(3) + "\nLatitude" + position.lat.toFixed(3) + "\nDo you want to save the values?")) {

                     document.cookie = "lon=" + position.lon.toFixed(3);
                     document.cookie = "lat=" + position.lat.toFixed(3);

                     window.close();

                 }
                



             });
                



             document.getElementById("map").style.height = "530px";


         });

</script>
</body>
</html>
