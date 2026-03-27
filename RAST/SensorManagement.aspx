<%@ Page Title="" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true" CodeFile="SensorManagement.aspx.cs" Inherits="sites_ui"%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript" src="scripts/scripts.js"></script>
   <script type="text/javascript" src="/js/jQuery-1.8.3.js"></script>
      <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <!--<script src="https://rawgit.com/schmich/instascan-builds/master/instascan.min.js"></script>-->
    <script src="/QRScanner/instascan.min.js"></script>
    <link href="/plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" /> 
 

      <!-- <link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css"/>-->
    <style>
        .imgBuildingPlan{
            display:block;
     
            width:auto;
            height:auto;
        /* Ragu 07 May 19*/
            position:relative;
                overflow-y:scroll;
                overflow-x:scroll; /*visible*/

            /* Ragu 07 May 19*/
        }
    </style>
    

    <script type="text/javascript">
        // WRITE THE VALIDATION SCRIPT IN THE HEAD TAG.
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57)){

                //return false;
                alert("Please enter only numbers:");
            }
                
            
        else
        {
                return true;
            }
            
        }
</script>

    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"  onmousemove="capmouse(event)">
    <!--<div class="row"> Ragu 09 May 2019 -->
        <section class="content-header">
            <h1>
                Sensor Management
            </h1>
            <!--<ol class="breadcrumb">
                <li><a href="map.aspx"<i class="fa fa-home"</i>Home</a></li>
                <li class="active">Sensor Management</li>
            </ol><br />-->
        </section>

          <section class="content">
        <div class="container-fluid">
          <div class="page-wrapper-box">
<!-- </div> // Ragu 09 May 2019
     <div class="row">
         <div class="col-md-12">-->
              <!-- general form elements -->
              <div class="box box-primary">
             
             <div class="box-body">
                 <div class="row">
                  <div class="col-md-3">
                 <div class="form-group">
                       <label for="exampleInputPassword1">Floor Map Image</label>
           
                     <select id="cmbFloorMapImage" runat="server" class="form-control" onchange="javascript:fnViewFloorMap()"> </select>        


                 </div>
                      </div>
                     <div class="col-md-3">
                   <div class="form-group">
                       <label for="exampleInputPassword1">Location</label>
            
                               <input type="text"   id="txtFloorMapImageName"  class="form-control" runat="server"/> 
                    <span id="spError"></span>
                     </div>
                         </div>
                       <div class="col-md-2">
                   <div class="form-group">
                       <br />
                       <button type="button" class="btn btn-primary" onclick="javascript:fnSaveLocation()">Save Location</button>
                       </div>
                           </div>

                     <div class="col-md-2">
                   <div class="form-group">
                       <br />
                       <button type="button" class="btn btn-primary" onclick="javascript:fncloseSensorMgmt()">Close</button>
                       </div>
                           </div>

                     <div class="col-md-2">
                   <div class="form-group">
                       <br />
                       <label><input type="checkbox" id="chkQrScan" onclick="javascript:qrScanClose()" checked />QR Scan</label>
                       </div>
                           </div>

                     <!-- Ragu 16 May 2019 -->
                      <div class="row">
         <div class="col-md-12" style="overflow-x:auto;"> 
         <div id='image_panel' runat="server"  class="imgBuildingPlan">  
         <img src="" id="imgBuildingPlan" runat="server" onclick="javascript: getMouseXY(event);qrScanLocal1();"/>  <!-- class="imgBuildingPlan" -->
             </div>
        </div>
        </div>

        <input type="hidden"  id="hidLastHubId" value="" visible="false"/>
        <input type="hidden"  id="hidLastSensorId" value="" visible="false"/>
<!-- Ragu 16 May 2019 -->

                    
                     </div>

                 

                     </div>



                 </div>
         <!--         </div>
         </div>// Ragu 09 May 2019 -->
         
        

   
   
<div id="modalSensorSettings" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">

           <div class="col-sm-11 text-left"><h4 class="modal-title">Sensor Settings</h4></div>
            <div class="col-sm-1 text-right"><button type="button" class="close" data-dismiss="modal">&times;</button></div>

      </div>
      <div class="modal-body">

          <div align="center"><span id="spdelMsg"></span></div>

   <div class="row">
        <div class="col-md-6">
              <!-- general form elements -->
              <div class="box box-primary">
             
             <div class="box-body">
                 
                 <div class="form-group">
                      <label for="exampleInputPassword1">Hub Id</label><span id="spSiteName"></span>
           
                             <asp:DropDownList  class="form-control" ID="cmbHubId" onchange="javascript:lastChangeHub()" runat="server">
                                                    </asp:DropDownList></div>

                      <div class="form-group">		
                          <label for="exampleInputName">Sensor QR Id</label><span id="spSensorQRId"></span>		
                      <input type="text" class="form-control" id="txtQRScanId" placeholder="Enter QR Scan""/>		
                     		
                    </div>

                     <div class="form-group">		
                      <video id="preview" style="position:fixed;z-index:10;display:none;width: 570px;height: 320px;top: 100px;left: 100px;"></video>		
                    </div>		
                    

               
                   <div class="form-group">
                      <label for="exampleInputName">SensorName</label><span id="spSensorName"></span>
                      <input type="text" class="form-control" id="txtSensorName" onchange="javascript:changeSensorNumber()" placeholder="Enter Sensor Name"/>

                  </div>

                     <div class="form-group">
                    <label for="exampleInputMinimum">Sensor Location</label><span id="spSensorLocation"></span>
                  <input type="text"   class="form-control" id="txtSensorLocation"   placeholder="Enter Sensor Location" />
                </div>


                 
      

                

                       <!--
               <div class="form-group"> 
                      <label for="exampleInputEmail1">Range</label><span id="spRange"></span>
                       <input type="text"   id="Min" aria-labelledby='lblRange'  onChange="javascript:fnRangeChange()"   onkeypress="javascript: return isNumber(event)" runat="server" /> &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp
                        <input type="text"  id="Max" aria-labelledby='lblRange' onChange="javascript:fnRangeChange()"   onkeypress="javascript: return isNumber(event)" runat="server" />
                        </div>

                 <div class="form-group">
                      <label for="exampleInputUnit">Unit</label><span id="spUnit"> </span>
                      <input type="text" class="form-control" id="txtUnit" placeholder="Enter the Unit"    disabled="disabled"/>
                                                      
                  </div>
                     -->
                
                
</div></div></div>


          <div class="col-md-6">
        <div class="box box-primary">
            <div class="box-body">
                

                 <div class="form-group">
                 <label for="exampleSenorType">Sensor Type</label><span id="spSensorType" ></span>         
                         
     
                                <asp:DropDownList  class="form-control" ID="cmbSensorType"  runat="server">
                                                   
                                                   
                                    <asp:ListItem Text="[--Select--]" Value="RE*">RE*</asp:ListItem>
                                    <asp:ListItem Value="SRS-ST">SRS-ST</asp:ListItem>
                                                    <asp:ListItem Value="SRS-GB">SRS-GB</asp:ListItem>
                                                    <asp:ListItem Value="SRS-CG">SRS-CG</asp:ListItem>
                                                    <asp:ListItem Value="SMT">SMT</asp:ListItem>
                                                    <asp:ListItem Value="SRS-Bait Station">SRS-Bait Station</asp:ListItem>
                                                    <asp:ListItem Value="SRS-Trap">SRS-Trap</asp:ListItem>
                                                    <asp:ListItem Value="Trap without Sensor">Trap without Sensor</asp:ListItem>
                                                    <asp:ListItem Value="Hunting Camera">Hunting Camera</asp:ListItem>
                                                    <asp:ListItem Value="Bait Station">Bait Station</asp:ListItem>
                                                    
                                </asp:DropDownList>

                  </div> 


                 <div class="form-group">
                      <label for="exampleInputStatus">Status</label><span id="spStatus"></span>
                      <asp:DropDownList class="form-control" ID="ddStatus" runat="server">
                           <asp:ListItem Text="[--Select--]" Value="1">Active</asp:ListItem>
                           <asp:ListItem Value="0">Deactive</asp:ListItem>
                                   
                      </asp:DropDownList>
                  </div>
               

                <%-- <div class="form-group" visible="false">--%>
                   <%-- <asp:TextBox ID="txtMin" runat="server">Minimum Threshold Value</asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMin"
        ErrorMessage="Enter a valid number" ValidationExpression="^\d$"></asp:RegularExpressionValidator>--%>

                   <%-- <label for="exampleInputMinimum"  visible="false">MinimumThreshold Value</label>--%><span id="spMin"></span>
                  <input type="text"   class="form-control" id="txtMin" value="0"  onkeypress="javascript: return isNumber(event)" style="display:none;"/>

                <%-- </div>--%>

                 <div class="form-group">
                    <label for="exampleInputMaximum">Threshold Value</label><span id="spMax"></span>
                    <input type="text"   class="form-control" id="txtMax" value="2" onkeypress="javascript: return isNumber(event)" />
                    
                      <input type="hidden"  id="txtX" value="" visible="false"/>
                          <input type="hidden"  id="txtY" value="" visible="false"/>

                           <input type="hidden"  id="hidSensorId" value="" visible="false"/>
                        
                   
                </div>
          
                <br />

              
                       <div class="form-group">
                    

     </div></div></div>
   
       





      </div>
       </div>

      <div style="text-align:left" class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
           <button type="button" class="btn btn-success" onclick="javascript:fnAddSensorDetails();">Save Sensor</button>
           <button type="button" class="btn btn-danger" onclick="javascript:fnDeleteSensor()" id="btnDelete">Delete Sensor</button>
          <button type="button" class="btn btn-warning" onclick="javascript:fnCloseQR()" id="btnCloseQR">Close QR</button>
      </div>
    </div>

  </div>
</div>
            </div>
            </div>
          </div>
               </section>


       <input type="hidden"  id="hidSensorName" value="" visible="false"/>
    
 <script src="../../plugins/jquery/jquery.min.js"></script>
    <!--   Bootstrap 3.3.2 JS -->
   <script src="../../plugins/bootstrap/js/bootstrap.bundle.min.js"></script>

     <!-- DATA TABES SCRIPT -->
    <script src="../../plugins/datatables/jquery.dataTables.min.js"></script>
<script src="../../plugins/datatables-bs4/js/dataTables.bootstrap4.min.js"></script>
<script src="../../plugins/datatables-responsive/js/dataTables.responsive.min.js"></script>
<script src="../../plugins/datatables-responsive/js/responsive.bootstrap4.min.js"></script>

    <!-- SlimScroll -->
    <script src="/plugins/slimScroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <!-- FastClick -->
    <script src='/plugins/fastclick/fastclick.min.js'></script>
<!-- AdminLTE App 
    <script src="../../dist/js/adminlte.min.js"></script>-->

    
    <script src="/scripts/scripts.js"></script>

  
    <!--  <script src="/QRScanner/instascan.min.js"></script>-->
    

    <script>
        fnViewFloorMap();
        
    </script>

    <script>

        scanner = new Instascan.Scanner({ video: document.getElementById('preview') });

        function qrScanLocal1() {
            if ($('#preview').css('display') != 'none') {
                $('#preview').hide('none');
                scanner.stop();
                return;
            }

            //Ragu 19 Dec 19
            pickUpLastHubSelection();
            //Ragu 19 Dec 19
            lastUpdSensorNumber();
            lastChangeHub();
            
            var chkQr = document.getElementById('chkQrScan');
            if (chkQr.checked == false)
            {
                return;
            }
           
            document.getElementById('preview').style.display = 'block'
            
            scanner.addListener('scan', function (content) {
                //alert(content);
                //return content;
                document.getElementById('txtQRScanId').value = content;
                document.getElementById('preview').style.display = 'none';

                
                //QR Validation Check

                //

                //Last Record Update
               
                
                //---
                scanner.stop();
                return;
            });
            Instascan.Camera.getCameras().then(function (cameras) {
                if (cameras.length == 1)
                {
                    scanner.start(cameras[0]);
                }
                else if (cameras.length > 1) {
                    scanner.start(cameras[cameras.length-1]);
                }
                else {
                    console.error('No cameras found.');
                }
            }).catch(function (e) {
                //console.error(e);
            });
        }

        function pickUpLastHubSelection() {

            $('#cmbHubId').val(document.getElementById('hidLastHubId').value);
            //alert("pickUpLastHubSelection:" + document.getElementById('hidLastHubId').value);
            
        }

        function changeSensorNumber()
        {
            var ddlHubId = $("[id*=cmbHubId]");
            var selHub = ddlHubId.find("option:selected").text();
            document.getElementById('hidLastHubId').value = selHub;

            //Ragu 19 Dec 19
            document.getElementById('hidLastSensorId').value = document.getElementById('txtSensorName').value;
            var tmp = twoDigit(document.getElementById('txtSensorName').value);
            document.getElementById('txtSensorLocation').value = selHub.substr(selHub.length - 4) + "-" + tmp;
        }

        function twoDigit(n) {
            return n > 9 ? "" + n : "0" + n;
        }

        function fncloseSensorMgmt()
        {
            window.history.back();
        }

        function lastChangeHub()
        {
            var ddlHubId = $("[id*=cmbHubId]");
            var selHub = ddlHubId.find("option:selected").text();
            document.getElementById('hidLastHubId').value = selHub;

            //Ragu 19 Dec 19
            var tmp = twoDigit(parseInt(document.getElementById('hidLastSensorId').value));
            document.getElementById('txtSensorName').value = tmp;
            document.getElementById('txtSensorLocation').value = selHub.substr(selHub.length - 4) + "-" + tmp;
            
        }

        function updSensorNumber()
        {
            //document.getElementById('hidLastSensorId').value = document.getElementById('txtSensorName').value;
        }

        function fnCloseQR()
        {
            if ($('#preview').css('display') != 'none') {
                $('#preview').hide('none');
                scanner.stop();
                return;
            }
        }
    
        
        function lastUpdSensorNumber() {

           var ddlHubId = $("[id*=cmbHubId]");
           var selHub = ddlHubId.find("option:selected").text();

           var tmp = twoDigit(parseInt(document.getElementById('hidLastSensorId').value));
           document.getElementById('txtSensorName').value = tmp;
           document.getElementById('txtSensorLocation').value = selHub.substr(selHub.length - 4) + "-" + tmp;
           document.getElementById('hidLastSensorId').value = parseInt(tmp) + 1;

        }

        function lastUpdSensorNumber_minusOne() {
            var tmp = twoDigit(parseInt(document.getElementById('hidLastSensorId').value));
            document.getElementById('hidLastSensorId').value = parseInt(tmp) - 1;
        }
    </script>

    <input type="hidden"  id="hidOrgId" runat="server"/>
    <input type="hidden"  id="strcmbFloorMapImage" runat="server"/>
   
</asp:Content>


    