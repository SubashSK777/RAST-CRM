<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="help_dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
</head>
<body>
<p><strong>Dashboard View</strong></p>
<p>This screen will assist you in viewing the graphical and tabular view of the Sites</p>

<p>Select the Site Name from the drop down list. The building map for the selected site will be displayed on the left side and the configured sensors will be displayed on the building map.  </p>

 <p>Sensors with green color <img src="/openlayers/img/0.gif" /> contain data below the threshold limits. Sensors with yellow color <img src="/openlayers/img/1.gif" /> contain data above the Lower Threshold Limit. Sites with red color <img src="/openlayers/img/2.gif" /> contain data above the Upper Threshold Limit. </p>

<p>Click on the Sensor icon to view the graphical view for that sensor for the selected day. The Last Data Received at (Date & Time), Latest Sensor Value, Minimum and Maximum Threshold configured for that sensor will be displayed </p>

<p>You can toggle between the graphical (chart) view and the tabular view by clicking on the Chart and Data Tab options</p>

<p>Click on the Export to Excel button to export the Chart and Data into an Excel file</p>

<p>You can view the Chart and Data for the selected Sensor for a different period by clicking on the Date Range Picker and either selecting the available period or by selecting a custom date and time</p>

<p>To view data for another sensor, click on the Sensor icon on the Building Map</p>

<p>To view data for another site, select the Site Name from the drop down list</p>
</body>
</html>
