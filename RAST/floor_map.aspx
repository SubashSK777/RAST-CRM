<%@ Page Language="C#" AutoEventWireup="true" CodeFile="floor_map.aspx.cs" Inherits="floor_map" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Building Floor Map</title>
    <link href="/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/dist/css/AdminLTE.min.css" rel="stylesheet" />
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
    <form id="frmFloorPlan" method="post" runat="server" enctype="multipart/form-data">
   <div class="row">
       <div class="col-md-12">
              <!-- general form elements -->
              <div class="box box-primary">
             
                       <div class="box-body">
                            <div class="form-group">
    <input id="flFloorPlan" type="file" runat="server" />
                                <br />
       <asp:Button runat="server"  ID="btnUpload" OnClick="btnUploadClick" Text="Upload" class="btn btn-primary"/>
                                </div>
                           
                         <div class="form-group">
  
       <img id="imgFloorPlan" name="imgFloorPlan" runat="server" visible="false" />
                                </div>
                      

                   <div class="form-group">
  
       <span id="spMsg" runat="server" />


                                </div>

                    <div class="form-group">
  
           <button type="submit" class="btn btn-info" onclick="javascript:window.close();">Close Window</button> 
                  


                                </div>
                        </div>
                       </div>
                  </div>
           </div>

    </form>
</body>
</html>
