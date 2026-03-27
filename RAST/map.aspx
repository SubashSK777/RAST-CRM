<%@ Page Title="" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true" CodeFile="map.aspx.cs" Inherits="map" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="/openlayers/jquery.min.js"></script>
    <script src="/openlayers/OpenLayers.js"></script>

    <style>
        #popup_FrameDecorationDiv_0 img, #popup_FrameDecorationDiv_1 img,
        #popup_FrameDecorationDiv_2 img, #popup_FrameDecorationDiv_3 img,
        #popup_FrameDecorationDiv_4 img{
        display: none;
        }

        .olPopupCloseBox {
        background: url("/openlayers/img/gmap_ui.png") no-repeat;
        background-position: 0px -334px;
        cursor: pointer;
        top: 10px !important;
        right: 10px !important;
        }
        .olFramedCloudPopupContent {
        padding: 10px;
        overflow: auto;
        font-family: Verdana, Tahoma, "DejaVu Sans", sans-serif;
        font-size: 12px;
        top: 5px !important;
        right: 5px;
        bottom: 20px;
        left: 5px !important;
        width: auto !important;
        height: auto !important;
        background: white;
        border-radius: 2px;
        border: 1px solid rgba(0, 0, 0, 0.298039) !important;
        background-repeat: no-repeat;
        background-position: bottom center;
        box-shadow: 0px 1px 4px -1px rgba(0, 0, 0, 0.298039);
        line-height: 1.5em;
        }
        .olFramedCloudPopupContent .title{
        font-size: 14px;
        margin-top: 0;
        margin-bottom: 0;
        font-weight: bold;
        }
        .olFramedCloudPopupContent .spacer{
        margin-left: 40px;
        }
        #popup_FrameDecorationDiv_2{
        width: 100% !important;
        background: url('/openlayers/img/tail.png') no-repeat center center;
        z-index: 10;
        height: 21px !important;
        bottom: 0px !important;
        }

  

    </style>



</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <section class="content-header">
          <h1>
            GIS View
            
          </h1>
          <!-- <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
            <li class="active">GIS View</li>
          </ol>-->
        </section>

        <!-- Main content -->
            <section class="content">
        <div class="container-fluid">
          <!-- Info boxes -->
          <div class="row">

               <div id="map" style="height:400px"></div>

     </div><!-- /.row -->
         </div> 
        </section><!-- /.content -->

    <script type="text/javascript">
        
        function fnclose() {
            $(".olPopup").hide();
        }


        $(document).ready(function () {

            document.getElementById("map").style.height = (screen.height - 150) + "px";

            var swidth = screen.width;
            var mapdiv = document.getElementById("map");

            mapdiv.style.width = (swidth - 10) + "px";

            map = new OpenLayers.Map("map");
            map.addLayer(new OpenLayers.Layer.OSM());
            map.removeControl(map.getControlsByClass('OpenLayers.Control.PanZoom')[0]);
            map.addControl(map.getControlsByClass('OpenLayers.Control.Navigation')[0]);

            //Ragu 21 Aug 2017

            

            //var lonLat = new OpenLayers.LonLat(103.813, 1.347)
            //             .transform(new OpenLayers.Projection("EPSG:4326"),
            //                map.getProjectionObject());

            var t_long;
            var t_lat;

            t_long = '<%=ConfigurationManager.AppSettings["LongVal"].ToString() %>';
            t_lat = '<%=ConfigurationManager.AppSettings["LatVal"].ToString() %>';
            var lonLat = new OpenLayers.LonLat(t_long, t_lat).transform(new OpenLayers.Projection("EPSG:4326"),map.getProjectionObject());

            map.setCenter(lonLat, 12);


            var xmlhttp = new XMLHttpRequest();
            var params = "";

            xmlhttp.open("GET", "/services/site.aspx?id=3&" + params, false);
 
            xmlhttp.send();

            var strReturnValue = xmlhttp.responseText;
            var strResult = strReturnValue.split(",");
           
            var markerName = "/openlayers/img/0.gif";

            var strStationId = "";
            for (var intCtr = 0; intCtr < strResult.length - 1; intCtr++) {

                var strStationDetails = strResult[intCtr];

                var strStationResult = strStationDetails.split("#");

                strStationId = strStationResult[0];
                var strSiteName = strStationResult[1];
                var strLatitude = strStationResult[2];
                var strLongitude = strStationResult[3];
                var strSiteStatus = strStationResult[4];
                var strAlertCount = strStationResult[5];
                if (strSiteStatus == "") {
                    markerName = "/openlayers/img/0.gif";
                }
                if (strSiteStatus == "0") {
                    markerName = "/openlayers/img/0.gif";
                }
                if (strSiteStatus == "1") {
                    //markerName = "/openlayers/img/1.gif";
                    markerName = "/openlayers/img/0.gif";   // Ragu 2 Aug 2017
                }
                if (strSiteStatus == "2") {
                    markerName = "/openlayers/img/2.gif";
                }
                if (strSiteStatus == "3") {
                    markerName = "/openlayers/img/3.gif";
                }

                var lonLat1 = new OpenLayers.LonLat(strLongitude, strLatitude)
                .transform(new OpenLayers.Projection("EPSG:4326"),
                 map.getProjectionObject());

                var markers = new OpenLayers.Layer.Markers("Markers");
                map.addLayer(markers);

                var size = new OpenLayers.Size(13, 13);
                var offset = new OpenLayers.Pixel(-(size.w / 2), -size.h);
                var icon = new OpenLayers.Icon(markerName, size, offset);
                markers.addMarker(new OpenLayers.Marker(lonLat1, icon));
                markers.addMarker(new OpenLayers.Marker(lonLat1, icon.clone()));

                var lonLat0 = new OpenLayers.LonLat(strLongitude, strLatitude)
                  .transform(new OpenLayers.Projection("EPSG:4326"),
                   map.getProjectionObject());

                addpopup(lonLat0, strSiteName, strStationId, strLatitude, strLongitude, strSiteStatus,10);
            }

            
            function addpopup(lonLat0, sitename, siteid, strlat, strlon, status,count) {
             
                var intSiteId = siteid;
                if (status == "0")
                {
                    status = "Live";
                }
                if (status == "1") {
                    status = "Minimum Threshold Alarm";
                }
                if (status == "2") {
                    status = "Maximum Threshold Alarm";
                }
                var content = "<table width='300px'><tr><td valign='top' style='font-size:14px;'>" + "<strong>" + sitename + "</strong></br><font style='font-family: Calibri, Geneva,sans-serif;font-size:13px';>Site Details<br/>" + "<strong>Status:</strong>" + status + "</br><strong>Latitude:</strong>" + strlat + "<br/><strong>Longitude:</strong>" + strlon + "</br></font></td></tr><tr><td>&nbsp;</td></tr><tr><td><a href='dashboard.aspx?id=" + intSiteId + "'>View Dashboard</a></td></tr></table>";
         
                var onclick = function (evt) {
                    popup = new OpenLayers.Popup.FramedCloud("markerpopup",
                                                lonLat0,
                                                null,
                                                content,
                                                 new OpenLayers.Icon(
                                                '',
                                                new OpenLayers.Size(0, 0),
                                                new OpenLayers.Pixel(0, 0)
                                                ),
                                                true, null);
                                popup.minSize = new OpenLayers.Size(200, 40);
                                popup.maxSize = new OpenLayers.Size(350, 300);
                                popup.autoSize = true;
                                popup.offset = 7;
                                map.addPopup(popup);
                };

               // markers.events.register('mouseover', markers, function (evt) { });
              markers.events.register('mouseover', markers, onclick);
             //markers.events.register('mouseout', markers, function (evt) { popup.hide(); });


            }

            function popupClear() {
                //alert('number of popups '+map.popups.length);
                while (map.popups.length) {
                    map.removePopup(map.popups[0]);
                }
            }              
         });

    </script>

   
    <script src="/plugins/jQuery/jQuery-2.1.3.min.js"></script>
    <!-- Bootstrap 3.3.2 JS -->
    <script src="/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
     <!-- SlimScroll 1.3.0 -->
    <script src="/plugins/slimScroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <!-- iCheck 1.0.1 -->
    <script src="/plugins/iCheck/icheck.min.js" type="text/javascript"></script>
    <!-- FastClick -->
    <script src='/plugins/fastclick/fastclick.min.js'></script>
   <!-- AdminLTE App 
    <script src="../../dist/js/adminlte.min.js"></script>-->

    
       <script src="/scripts/scripts.js"></script>
   
</asp:Content>

