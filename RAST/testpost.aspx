<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testpost.aspx.cs" Inherits="testpost" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/dist/js/jquery.min.js"></script>
   
  
    <script>

        function fnSendData() {
            var strPhoneNumber = document.getElementById("txtPhone").value;
            var strMessage = document.getElementById("txtMessage").value;
            var dtDate = document.getElementById("txtDate").value;

            $.post("http://localhost:55442/Services/receive_sensor_data.aspx",
                  { Mobile: strPhoneNumber, Type: 'A', Message: strMessage, Timestamp: dtDate },
                  function (data, textStatus, jqXHR) {
                      document.getElementById("spResult").innerHTML = data;

                  });
        }
     
    </script>

</head>
<body>
    <table border="0">
        <tr><td>Sender Phone Number</td><td><input type="text" id="txtPhone" /></td></tr>
        <tr><td>SMS Message</td><td><input type="text" id="txtMessage" value="tum1014 Zone 99" /></td></tr>
        <tr><td>SMS Message Date Time</td><td><input type="text" id="txtDate" runat="server" disabled="disabled"/></td></tr>
        <tr><td colspan="2"><input type="button" id="btnClick" value="Send Data" onclick="fnSendData()" /></td></tr>
        <tr><td colspan="2"><span id="spResult"></span></td></tr>
    </table>
</body>
</html>
