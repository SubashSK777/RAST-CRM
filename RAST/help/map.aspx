<%@ Page Language="C#" AutoEventWireup="true" CodeFile="map.aspx.cs" Inherits="help_map" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  
</head>
<body>
    <p><strong>GIS View</strong></p>
    <p>This window will display the list of all sites configured in the application. Each site is denoted by a marker with color representations.</p>

    <p>Sites with green color <img src="/openlayers/img/0.gif" /> displays the Sites with data below the threshold limits. Sites with yellow color <img src="/openlayers/img/1.gif" /> displays the Sites with data above the Lower Threshold Limit. Sites with red color <img src="/openlayers/img/2.gif" /> displays the Sites with data above the Upper Threshold Limit. </p>

    <p>Click on a Site to view the site details. This will open a popup with the current status of the Site and the Latitude and Longitude details. Click on the View Dashboard link to view the dashboard for the selected site</p>

    <p>Double click to zoom into the map. Use mouse scroll to zoom out from the map</p>
</body>
</html>
