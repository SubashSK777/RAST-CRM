<%@ Page Title="" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true" CodeFile="commandCenter.aspx.cs" Inherits="commandCenter" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <link href="/plugins/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" type="text/css" />    

    <script src="/dist/js/Chart.js"></script>
    <style>
        
        .imgBuildingPlan {
            display: block;
                width: auto;
                height: auto;
            
        }

      

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    
 <div class="modal fade" id="modDashboardData" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  style="overflow-x:auto;">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title"><span id="myModalLabel">Data View</span></h4>
      </div>
      <div class="modal-body">
               <table class="table table-striped table-bordered table-hover order-column" id="tblData">
                      
                                                         </table>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>

      <section class="content-header">
          <h1>
           Command Center
          </h1>
          <ol class="breadcrumb">
            <li><a href="map.aspx"><i class="fa fa-home"></i> Home</a></li>
              <li><a href="commandCenter.aspx">Command Center</a></li>
             </ol>
            <br />
        </section>


  
    <section class="content">
      <div class="container-fluid">
                 <div class="box box-primary">             
             <div class="box-body">

                 <div class="row">
                       <div class="col-md-2">
                           <label for="exampleInputPassword1">Site Name</label>
                           </div>

                        <div class="col-md-2">
                           <label for="exampleInputPassword1">Hub Name</label>
                           </div>

                     
                        <div class="col-md-3">
                           <label>Select Command</label> 
                           </div>

                    </div>

                     
     <div class="row">
                  <div class="col-md-2">
                  <div class="form-group">
                      <select  class="form-control" id="cmbSiteName" runat="server" onchange="fnLoadHub($(this).val())">                               
                            </select>
                    </div>
     </div>

                  <div class="col-md-2">
                  <div class="form-group">
                      <select  class="form-control" id="cmbHubName" runat="server">                               
                            </select>
                    </div>
     </div>
         <div class="col-md-3">
                   <div class="form-group">
                         
                    <div class="form-group">
                      <select  class="form-control" id="cmbCommand" runat="server">                               
                            </select>
                    </div>

                     </div>
     </div>
           <div class="col-md-2">
                  <div class="form-group">
                      <span id="spLoading">&nbsp;&nbsp;&nbsp;</span>
                       <label for="exampleInputPassword1">&nbsp;&nbsp;&nbsp;</label><button type="button" class="btn btn-primary" onclick="javascript:updCommand();">Send Command</button>
                     </div>
         </div>


         <div class="col-md-2">
                  <div class="form-group">
                      <span id="spLoading"></span>
                      <button type="button" class="btn btn-danger" onclick="javascript:refreshCommand();" id="btnDownloadDashboard">Manual Check</button>
                    </div>
          </div>


         </div>  
 
          <!-- Info boxes -->
                   <div class="row">
                       
                    </div>
 
   
            </div>
    
       </div>      
      </div>
    </section>
      
<!-- Modal -->

<div id="modalDashboard" class="modal fade" role="dialog" style="overflow-x:auto;z-index: 100;">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Command Center</h4>
      </div>
      <div class="modal-body">
          <div class="row">
               <div class="col-md-12">
              <!-- general form elements -->
              <div class="box box-primary">

                   <div class="box-body">
                      <!-- /.form group --> 
                        
             
             <div class="form-group">
                      <strong>Last Data Received at: </strong>&nbsp;<span id="spLastDateReceived">Select a Sensor from Building Map to view data</span>
                  <br />   
                 <strong>Location: </strong>&nbsp;<span id="spLocation">N/A</span>
                 <br />
                  <strong>Sensor Name: </strong>&nbsp;<span id="spSensorId">N/A</span>
                <br />
                                    <strong>Sensor Value: </strong>&nbsp;<span id="spSensorValue">N/A</span>
   
        <!--<strong>Minimum Threshold </strong>-->&nbsp;<span id="spMinimumThreshold" style="visibility:hidden">N/A</span>
        <strong>Threshold Level:</strong>&nbsp;<span id="spMaximumThreshold">N/A</span>
                    </div>
             
                       
                       <button id="btnExport" class="btn btn-danger" onclick="javascript:fnGetCSVForSensor();" style="visibility:hidden">Export to CSV</button>
                      <button id="btnData" class="btn btn-primary" onclick="javascript:fnViewChartData();" style="visibility:hidden">View Data</button>
                   

                  </div>                           
          </div>
</div>
          </div>
           <h4 align="center"><span id="spStationSensorName"></span></h4>

<div id="tab_chart">
       
     </div>

	</div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
          
      </div>
    </div>

  </div>
</div>

  <input type="hidden" id="hidSensorId" />
 <input type="hidden" id="hidStartDate"/>
     <input type="hidden" id="hidEndDate"/>

     <input type="hidden" id="hidDashboardData" />


    <script src="/plugins/jQuery/jQuery-2.1.3.min.js"></script>
    <!-- Bootstrap 3.3.2 JS -->
    <script src="/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>

     <!-- DATA TABES SCRIPT -->
    <script src="/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    
    <!-- InputMask -->
    <script src="/plugins/input-mask/jquery.inputmask.js" type="text/javascript"></script>
    <script src="/plugins/input-mask/jquery.inputmask.date.extensions.js" type="text/javascript"></script>
    <script src="/plugins/input-mask/jquery.inputmask.extensions.js" type="text/javascript"></script>
    <!-- date-range-picker -->
    <script src="/plugins/daterangepicker/daterangepicker.js" type="text/javascript"></script>
    <!-- bootstrap color picker -->
    <script src="/plugins/colorpicker/bootstrap-colorpicker.min.js" type="text/javascript"></script>
    <!-- bootstrap time picker -->
    <script src="/plugins/timepicker/bootstrap-timepicker.min.js" type="text/javascript"></script>
    <!-- SlimScroll 1.3.0 -->
    <script src="/plugins/slimScroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <!-- iCheck 1.0.1 -->
    <script src="/plugins/iCheck/icheck.min.js" type="text/javascript"></script>
    <!-- FastClick -->
    <script src='/plugins/fastclick/fastclick.min.js'></script>
    <!-- AdminLTE App -->



      <script src="/scripts/scripts.js"></script>
   
    <script type="text/javascript">

        function fnLoadHub(id1) {
            //fnGetHub(id1);
            var intSiteId = document.getElementById("ContentPlaceHolder1_cmbSiteName").value;
            $.ajax({
                type: 'GET',
                async: false,
                url: '/services/site.aspx?id=8&siteid=' + intSiteId,
                success: function (data) {
                    var location_data = JSON.parse(data);
                    document.getElementById("ContentPlaceHolder1_cmbHubName").options.length = 0;
                    var select = document.getElementById("ContentPlaceHolder1_cmbHubName");
                    var option0 = document.createElement("option");
                    option0.id = "0";
                    option0.value = "--Select Hub Name--";
                    option0.innerHTML = "--Select Hub Name--";
                    select.appendChild(option0);

                    for (var i = 0; i < location_data.length; i++) {

                        var option = document.createElement("option");
                        option.id = location_data[i].s_HubId;
                        option.value = location_data[i].s_HubPhoneNumber;
                        option.innerHTML = location_data[i].s_HubPhoneNumber;
                        select.appendChild(option);
                        
                    }
                }
            });

        }
        

        function updCommand() {  // Ragu 24 Jan 2018 for Sensor Battery Update event
            
            //window.alert(m_sensorID);
            var intSiteId = document.getElementById("ContentPlaceHolder1_cmbSiteName").value;
            var strHubName = document.getElementById("ContentPlaceHolder1_cmbHubName").value;
            var strCommandName = document.getElementById("ContentPlaceHolder1_cmbCommand").value;   

            //Ragu 22 Jun 2017
            var e = document.getElementById("ContentPlaceHolder1_cmbHubName");
            var strPhoneNumer = e.options[e.selectedIndex].id;

            var e1 = document.getElementById("ContentPlaceHolder1_cmbCommand");
            var strCommand = e1.options[e1.selectedIndex].id;

            if (strPhoneNumer < 0 || strPhoneNumer == null || strPhoneNumer == "") {
                e = document.getElementById("ContentPlaceHolder1_cmbHubName").value;
                strPhoneNumer = e;
            }

            if (strPhoneNumer == "0") {
                alert("Select the Hub");
                return false;
            }

            if (strCommandName < 0 || strCommandName == null || strCommandName == "") {
                e = document.getElementById("ContentPlaceHolder1_cmbCommand").value;
                strCommand = e;
            }


            if (strCommand == "0")
            {
                alert("Select the Command")
                return false;
            }
          


            var xmlhttp = new XMLHttpRequest();

            xmlhttp.open("GET", "/services/receive_command_data.aspx?Mobile=" + strHubName.toString() + "&Type=C&Message=" + strCommandName.toString(), false);
            xmlhttp.send();

            var intReturnValue = xmlhttp.responseText;

            // document.location.reload();

        }

        

    </script>
  
</asp:Content>

