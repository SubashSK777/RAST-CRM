<%@ Page Title="" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="dashboard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!-- <link href="/plugins/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" type="text/css" />
     <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
      <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>-->
    <link href="/plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" /> 
    
    
     <script src="/dist/js/Chart.js"></script>
     
    <style>


        
        /* For Video popup*/

            #fade {
            display: none;
            position: fixed;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: black;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: .80;
            filter: alpha(opacity=80);
            }

            #light {
            display: none;
            position: absolute;
            top: 50%;
            left: 50%;
            max-width: 600px;
            max-height: 360px;
            margin-left: -300px;
            margin-top: -180px;
            border: 2px solid #FFF;
            background: #FFF;
            z-index: 1002;
            overflow: visible;
            }

            #boxclose {
            float: right;
            cursor: pointer;
            color: #fff;
            border: 1px solid #AEAEAE;
            border-radius: 3px;
            background: #222222;
            font-size: 31px;
            font-weight: bold;
            display: inline-block;
            line-height: 0px;
            padding: 11px 3px;
            position: absolute;
            right: 2px;
            top: 2px;
            z-index: 1002;
            opacity: 0.9;
            }

            .boxclose:before {
            content: "×";
            }

            #fade:hover ~ #boxclose {
            display:none;
            }


             /* For Video popup*/

        .imgBuildingPlan {
            display: block;
                width: auto;
                height: 100%;
                position:relative;
                overflow-y:scroll;
                overflow-x:scroll; /*visible*/
        }

        div.newarrow{
        height:4px;
        background-color:black;
        background: #0000ff;
        -webkit-transform-origin: top left;
        -moz-transform-origin: top left;
        -o-transform-origin: top left;
        -ms-transform-origin: top left;
        transform-origin: top left;
        position:absolute;
        animation: blinker 1s step-start infinite;
        }

         

        div.newarrow:before{
         margin-top:-8px;
          margin-left:0px;
            width:0;
            height:0;
            content:"";
            display:inline-block;
            border-top:10px solid transparent;
            border-left:15px solid #0000ff;
            border-bottom:10px solid transparent;
            position:absolute;
            right:0;
            -webkit-transform-origin: top left;
            -moz-transform-origin: top left;
            -o-transform-origin: top left;
            -ms-transform-origin: top left;
            transform-origin: top left;
            animation: blinker 1s step-start infinite;
        }

       
        div.line{
            margin: 0px;
            padding: 0px;
            -webkit-transform-origin: top left;
            -moz-transform-origin: top left;
            -o-transform-origin: top left;
            -ms-transform-origin: top left;
            transform-origin: top left;
               
            height: 4px; /* Line width of 3 */
            background: #0000ff; /* Black fill */
            /*position:relative;*/
            
            }

        div.arrow{
            margin: 0px;
            padding: 0px;
            transform-origin: top left;
            height: 4px; /* Line width of 3*/ 
            background: #0000ff; 
           /*  position:relative;*/
            -webkit-transform-origin: top left;
            -moz-transform-origin: top left;
            -o-transform-origin: top left;
            -ms-transform-origin: top left;
            transform-origin: top left;
            
            /*position:static;
            animation: blinker 1s step-start infinite;*/
            }

        div.line:hover{
            background: #C30;
            box-shadow: 0 0 8px #C30;
            opacity: 1;
        }
        div.line.active{
            background: #666;
            box-shadow: 0 0 8px #666;
            opacity: 1;
        }

        /* Style the tab */
        .tab {
          overflow: hidden;
          border: 1px solid #ccc;
          background-color: #f1f1f1;
        }

        /* Style the buttons that are used to open the tab content */
        .tab button {
          background-color: inherit;
          float: left;
          border: none;
          outline: none;
          cursor: pointer;
          padding: 14px 16px;
          transition: 0.3s;
        }

        /* Change background color of buttons on hover */
        .tab button:hover {
          background-color: #ddd;
        }

        /* Create an active/current tablink class */
        .tab button.active {
          background-color: #ccc;
        }

        /* Style the tab content */
        .tabcontent {
          display: none;
          padding: 6px 12px;
          border: 1px solid #ccc;
          border-top: none;
        }

      .legend { list-style: none; }
        .legend li { float: left; margin-right: 10px; }
        .legend span { border: 1px solid #ccc; float: left; width: 40px; height: 5px; margin:10px; }
        /* your colors */
        .legend .red { background-color: #ff0000; }
        .legend .green { background-color: #00ff00; }
        .legend .blue { background-color: #0000ff; }
        .legend .pink { background-color: #ff00ff; }
        .legend .lightblue { background-color: #00ffff; }
        .legend .orange { background-color: #ffa500; }
        .legend .teal { background-color: #008080; }
        .legend .brown { background-color: #a52a2a; }
        .legend .lavender { background-color: #e6e6fa; }
        .legend .gray { background-color: #808080; }
        

        .modal.in .modal-dialog 
        { 
         -moz-transform: none;
         -ms-transform: none;
         -o-transform: none;
         -ms-transform: none;
         transform: none;
        }

        
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
 
      <section class="content-header">
          <div class="modal-header">
           <div class="col-sm-7 text-left">
          <h1>
           <i class=\"nav-icon fas fa-tachometer-alt\"></i> Dashboard
          </h1>
          <!-- <ol class="breadcrumb">
            <li><a href="map.aspx"><i class="fa fa-home"></i> Home</a></li>
              <li><a href="dashboard.aspx">Dashboard</a></li>
             </ol> -->

           </div>
            <div class="col-sm-5 text-right">
                <button type="button" class="btn btn-danger" onclick="javascript:fnDownloadDashboard();" id="btnDownloadDashboard">Download Data (csv)</button>&nbsp;&nbsp;<button type="button" class="btn btn-primary" onclick="javascript:fnViewPathWay();">&nbsp;&nbsp;Path Way&nbsp;&nbsp;</button></div>
            </div>
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
                           <label for="exampleInputPassword1">Location Name</label>
                           </div>

                     
                        <div class="col-md-3">
                           <label>Select Period :</label> 
                           </div>

                    </div>

                     
     <div class="row">
         <div class="col-sm-2">
            <div class="form-group">
                <select  class="form-control" id="cmbSiteName" runat="server" onchange="fnLoadDashboardSelSite($(this).val())"> </select>
            </div>
        </div>
         <div class="col-sm-2">
                  <div class="form-group">
                      <select  class="form-control"  id="cmbLocation" runat="server" onchange="fnLoadDashboardSelSite_location($(this).val())">                               
                            </select>
                    </div>
     </div>
         <div class="col-sm-3">
                   <div class="form-group">
                         
                        <div class="input-group">
                            <tr>
                                <td>
                                    <button class="btn btn-default pull-right" id="daterange-btn">
                                    <i class="fa fa-calendar"></i> <span id="spDateRange"> Select Date </span>
                                    <i class="fa fa-caret-down"></i>
                                    </button>
                                </td>
                               
                                

                            </tr>
                        </div>
                       
                     </div>
     </div>
        <!-- <div class="col-sm-1">
                  <div class="form-group">
                     
                      
 
                     </div>
         </div> -->
         <div class="col-sm-4">
                  <div class="form-group">
                        <table><tr>
                       <td><button type="button" class="btn btn-primary" onclick="javascript:fnViewDashboard();">Reload</button></td><td>&nbsp;&nbsp;</td>
                       <td><input type="checkbox" id="chkViewSensorOnly" onchange="javascript: fnViewDashboard();">&nbsp;&nbsp;View Sensor Only</input></td><td>&nbsp;&nbsp;</td>
                      <td><input type="checkbox" id="chkViewSensor" onchange="javascript: fnViewDashboard();">&nbsp;&nbsp;Show Sensor ID</input></td><td>&nbsp;&nbsp;</td>
                      <td><input type="checkbox" id="chkViewRodent" onchange="javascript: fnViewDashboard();">&nbsp;&nbsp;Rodent Caught</input></td></tr></table>
                      
                    </div>
            </div>
         
         <div class="col-sm-1" id="timerangepathdiv" runat="server">
                   <div class="form-group">
                        <table><tr>
                       <td><select id="timerange-btn" name="timerange-btn" onchange="javascript:fnViewHourlyData();" onclick="javascript:fnHourCheckData();">
                            <option value="0">------ All Time ------</option>
                        </select></td><td>&nbsp;&nbsp;&nbsp;</td></tr></table>
                   </div>
        </div>
      </div> 
 
         <!-- Info boxes -->
   <div class="row">
        <div class="col-md-9" style="overflow-x:auto;"> 
            <div class="row">
                <div id="image_panel" runat="server" class="imgBuildingPlan">          
                    <img src="" id="imgBuildingPlan" runat="server" style="visibility:hidden"/>     
                </div>
            </div>
           
           <div class="row" id="AnalysisDivButton" runat="server">
                <div class="col-md-2">
                    <button type="button" class="btn btn-primary" onclick="javascript:fnViewAnalysis(0);" id="btnViewAnalysis0" style="height:100%"> Daily Activity  -  All Level </button>
                </div>
                <div class="col-md-2">
                    <button type="button" class="btn btn-primary" onclick="javascript:fnViewAnalysis(1);" id="btnViewAnalysis1" style="height:100%">Daily Activity - Group By Level</button>
                </div>
                <div class="col-md-2">
                    <button type="button" class="btn btn-primary" onclick="javascript:fnViewAnalysis(2);" id="btnViewAnalysis2" style="height:100%">Weekly Activity - All Level</button>
                </div>                  
                <div class="col-md-2">
                    <button type="button" class="btn btn-primary" onclick="javascript:fnViewAnalysis(3);" id="btnViewAnalysis3" style="height:100%">Weekly Activity - Group By Level</button>
                </div>
                <div class="col-md-2">
                    <button type="button" class="btn btn-primary" onclick="javascript:fnViewAnalysis(4);" id="btnViewAnalysis4" style="height:100%">Monthly Activity - All Level</button>
                </div>
                <div class="col-md-2">
                    <button type="button" class="btn btn-primary" onclick="javascript:fnViewAnalysis(5);" id="btnViewAnalysis5" style="height:100%">Monthly Activity - Group By Level</button>
                </div>
                
            </div>
       </div>


       <div class="col-md-3" style="overflow-x:auto;"> 
           
           <div class="col">
                <div class="card card-secondary">
                    <div class="card-header">
                            <h3 class="card-title">Site Monitoring</h3>
                        </div>
                       
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <table cellpadding="5">
                                    <tr><td><lable>Total Triggers Count</lable></td><td><lable id="lblType_Flashes" style="font-weight:bold;font-size: large;"></lable></td></tr>
                                    <tr><td><lable>Total Rodent Caught</lable></td><td><lable id="lblType_Catches" style="font-weight:bold;font-size: large;"></lable></td></tr>
                                    <tr><td><lable>Live Status Update</lable></td><td><lable id="lblType_LastUpdate" style="font-weight:bold;font-size: large;"></lable></td></tr></table>
                                </div>
                            </div>
                        </div>
                    </div>
               </div>
           <div class="col">
                <div class="card card-secondary">
                    <div class="card-header">
                            <h3 class="card-title">Battery Status</h3>
                        </div>
                           
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    
                                    <canvas id="pieChart_BatStatus" height="75" width="150" class="chartjs-render-monitor" style="display: block; width: 150px; height: 75px;"></canvas>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>
           <div class="col">
                    <div class="card card-secondary">
                        <div class="card-header">
                                <h3 class="card-title">Legends & Sensor Count</h3>
                            </div>
                       
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                            <table cellpadding="5">
                                             <tr>
                                                <td><lable id="lblType_RE" style="font-weight:bold;font-size: large;"></lable></td><td><img src="/openlayers/img/0.gif" width="27" height="18" alt="Square - RE Monitor"/></td><td><lable>RE Surveillance (RE*)</lable></td>
                                            </tr>
                                            <tr>
                                                <td><lable id="lblType_GB" style="font-weight:bold;font-size: large;"></lable></td><td><img src="/openlayers/img/0_Cir.png" width="27" height="18" alt="SRS-GB"/></td><td><lable>Smart Rodent Stations Glue Board (SRS-GB)</lable></td>
                                            </tr>
                                            <tr>
                                                <td><lable id="lblType_ST" style="font-weight:bold;font-size: large;"></lable></td><td><img src="/openlayers/img/0_Cir_Orange.png" width="27" height="18" alt="SRS Snap Trap"/></td><td><lable>Smart Rodent Stations Snap Trap (SRS-ST)</lable></td>
                                            </tr>
                                            <tr>
                                                <td><lable id="lblType_CG" style="font-weight:bold;font-size: large;"></lable></td><td><img src="/openlayers/img/0_Cir_Black.png"width="27" height="18"  alt="SRS Cage"/></td><td><lable>Smart Rodent Stations Cage (SRS-CG)</lable></td>
                                            </tr>
                                            <tr>
                                                <td><lable id="lblType_SMT" style="font-weight:bold;font-size: large;"></lable></td><td><img src="/openlayers/img/0_tri.png" width="27" height="18" alt="SMT Multi Rodent Trap"/></td><td><lable>Smart Multi Rodent Trap (SMT)</lable></td>
                                            </tr>
                                            <tr>
                                                <td><lable id="lblNoSensor" style="font-weight:bold;font-size: large;"></lable></td><td><img src="/openlayers/img/04_Poly_Blue.png" width="27" height="18"  alt="Trap without Sensor"/></td><td><lable>Trap without Sensor </lable></td>
                                            </tr>
                                            <tr>
                                                <td><lable id="lblHuntCamera" style="font-weight:bold;font-size: large;"></lable></td><td><img src="/openlayers/img/04_Camera.png" width="27" height="18"  alt="Hunting Camera"/></td><td><lable>Hunting Camera </lable></td>
                                            </tr>
                                            <tr>
                                                <td><lable id="lblBaitStation" style="font-weight:bold;font-size: large;"></lable></td><td><img src="/openlayers/img/0_Bait.png" width="27" height="18" alt="Bait Station"/></td><td><lable>Bait Station </lable></td>
                                            </tr>
                                                <tr>
                                                <td><lable id="lblBait" style="font-weight:bold;font-size: large;"></lable></td><td><img src="/openlayers/img/0_Bait.jpg" width="27" height="18" alt="SRS-Bait Station"/></td><td><lable>SRS - Bait Station </lable></td>
                                            </tr>

                                            </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                   </div>
              
        </div>
    </div>
 
   

    
                 <!--  Ragu Changes 09 Sep 20 <div class="row" id="Div1" runat="server">
                <div class="col-md-2">
                    <button type="button" class="btn btn-primary" onclick="javascript:fnViewRodentAnalysis(0);" id="btnViewRodentAnalysis0">Daily-All Level-Rodent Caught</button>
                </div>
                <div class="col-md-2">
                    <button type="button" class="btn btn-primary" onclick="javascript:fnViewRodentAnalysis(1);" id="btnViewRodentAnalysis1">Daily-Individual-Rodent Caught</button>
                </div>
                <div class="col-md-2">
                    <button type="button" class="btn btn-primary" onclick="javascript:fnViewRodentAnalysis(2);" id="btnViewRodentAnalysis2">Weekly-All Level-Rodent Caught</button>
                </div>                  
                <div class="col-md-2">
                    <button type="button" class="btn btn-primary" onclick="javascript:fnViewRodentAnalysis(3);" id="btnViewRodentAnalysis3">Weekly-Individual-Rodent Caught</button>
                </div>
                <div class="col-md-2">
                    <button type="button" class="btn btn-primary" onclick="javascript:fnViewRodentAnalysis(4);" id="btnViewRodentAnalysis4">Monthly-All Level-Rodent Caught</button>
                </div>
                <div class="col-md-2">
                    <button type="button" class="btn btn-primary" onclick="javascript:fnViewRodentAnalysis(5);" id="btnViewRodentAnalysis5">Monthly-Individual-Rodent Caught</button>
                </div>
                
            </div>
           </div>-->
   


       
      
<!-- Modal -->


            

                              
<div class="modal fade" id="modalDashboard" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  style="overflow-x:auto;">
    <div class="modal-dialog" role="document">
        <!-- Modal content-->
        <div class="modal-content"">
            <div class="modal-header">
                <div class="col-sm-11 text-left"><h4 class="modal-title">Dashboard</h4></div>
                <div class="col-sm-1 text-right"><button type="button" class="close" data-dismiss="modal">&times;</button></div>
                
                
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
                    <button id="btnRodentCaught" class="btn btn-primary" onclick="javascript:fnRodentCaught();" style="visibility:hidden">Rodent Caught</button>
                    <button id="btnBaitConsumed" class="btn btn-primary" onclick="javascript:fnBaitConsumed();" style="visibility:hidden">Bait Consumed</button>
            </div>                           
        </div>
    </div>

</div>

    <h4 align="center"><span id="spStationSensorName"></span></h4>

    <div id="tab_chart"></div>

    <div id="tab_chart1"></div>

    </div>

    <div id="light">
        <br />
        <a class="boxclose" id="boxclose" onclick="lightbox_close();"></a>
        <video id="VisaChipCardVideo" width="600" controls></video>
            
    </div>

    <div id="fade" onclick="lightbox_close();"></div>

        <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
    </div>




<div class="modal fade" id="modDashboardData" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  style="overflow-x:auto;z-index: 100;">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">

           <div class="col-sm-11 text-left"> <h4 class="modal-title"><span id="myModalLabel">Data View</span></h4></div>
        <div class="col-sm-1 text-right"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>

       
      </div><br />
     

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

<div class="modal fade" id="modRodentCaughtData" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  style="overflow-x:auto;z-index: 100;">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">

             <div class="col-sm-11 text-left"><h4 class="modal-title"><span id="myModalLabel_RodentCaught">Rodent Caught Information</span></h4></div>
        <div class="col-sm-1 text-right"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>
      
      </div>
        <div>
                   
                           <table>
                            <tr>
                            <th style="width: 5%;"><span></span></th>
                            <th style="width: 10%;"><span>&nbsp;&nbsp;&nbsp;Date Time : </span></th>
                            <td style="width: 25%;"><input type="datetime-local" id="rodcaughtTimeStamp" onload="javascript:getTimeStamp();" placeholder="dd/MM/yyy hh:mm AM/PM"/></td>

                               

                            <!--<td style="width: 25%;"><input type="date" id="rodcaught_date" onload="javascript:getdt1();"/><input type="time" id="rodcaught_Time" onload="javascript:gettime1();"/>
                            <input type="hidden" id="rodcaughtTimeStamp"/></td>
                                -->
                            <th style="width: 10%;"><span>No of Caught : </span></th>
                            <td style="width: 10%;"><select name="noofrodcaught" id="noofrodcaught">
                              <option value="1">1</option>
                              <option value="2">2</option>
                              <option value="3">3</option>
                              <option value="4">4</option>
                              <option value="5">5</option>
                              <option value="6">6</option>
                              <option value="7">7</option>
                              <option value="8">8</option>
                              <option value="9">9</option>
                              <option value="10">10</option>
                              <option value="11">11</option>
                              <option value="12">12</option>
                              <option value="13">13</option>
                              <option value="14">14</option>
                              <option value="15">15</option>
                            </select></td>

                           <th style="width: 10%;"><span>Rodent Type : </span> </th>
                            
                            <td style="width: 20%;"><select name="rodcaughtrema" id="rodcaughtrema">
                              <option value="Black Rat (Roof Rat)">Black Rat (Roof Rat)</option>
                              <option value="Brown Rat (Norway Rat)">Brown Rat (Norway Rat)</option>
                              <option value="House Mouse">House Mouse</option>
                              <option value="Others">Others</option>
                            </select></td>
                      
                                <td style="width: 2%;"></td>
                        <td style="width: 5%;"><button id="btnSubmitRodentCaught" class="btn btn-primary" onclick="javascript:btnRodentSubmit();">Submit</button></td>
                            <td style="width: 3%;"></td>    
                            </tr>
                     </table>
                            <br /><br />
        

        <div class="modal-body">
               <table class="table table-striped table-bordered table-hover order-column" id="tblData_rodentCaught"></table>
         </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
          </div>

        </div>
  </div>
 </div>
</div>

<div class="modal fade" id="modBaitConsumedData" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  style="overflow-x:auto;z-index: 100;" >
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
          <div class="col-sm-11 text-left"> <h4 class="modal-title"><span id="myModalLabel_BaitConsumedData">Bait Consumed Information</span></h4></div>
        <div class="col-sm-1 text-right"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>

        
       
      </div>
        <div>
                   
                           <table>
                            <tr>
                            <th style="width: 5%;"><span></span></th>
                            <th style="width: 10%;"><span>&nbsp;&nbsp;&nbsp;Date Time : </span></th>
                            <td style="width: 25%;"><input type="datetime-local" id="consumedTimeStamp" onload="javascript:getTimeStamp();" placeholder="dd/MM/yyy hh:mm AM/PM"/></td>

                               

                            <!--<td style="width: 25%;"><input type="date" id="consumed_date" onload="javascript:getdt1();"/><input type="time" id="consumedTimeStamp" onload="javascript:gettime1();"/>
                            <input type="hidden" id="rodcaughtTimeStamp"/></td>
                                -->
                            <th style="width: 10%;"><span>% of Consume</span></th>
                            <td style="width: 10%;"><select name="noofconsumed" id="noofconsumed">
                              <option value="0">0</option>
                              <option value="10">10</option>
                              <option value="20">20</option>
                              <option value="30">30</option>
                              <option value="40">40</option>
                              <option value="50">50</option>
                              <option value="60">60</option>
                              <option value="70">70</option>
                              <option value="80">80</option>
                              <option value="90">90</option>
                              <option value="100">100</option>
                              
                            </select></td>

                           <th style="width: 10%;"><span>Remarks </span> </th>
                            
                            <td style="width: 20%;"><input type="text" name="consumedrema" id="consumedrema">
                              
                            </input></td>
                      
                                <td style="width: 2%;"></td>
                        <td style="width: 5%;"><button id="btnSubmitBaitConsumed" class="btn btn-primary" onclick="javascript:btnBaitConsumedSubmit();">Submit</button></td>
                            <td style="width: 3%;"></td>    
                            </tr>
                     </table>
                            <br /><br />
        

        <div class="modal-body">
               <table class="table table-striped table-bordered table-hover order-column" id="tblData_baitConsumed"></table>
         </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
          </div>

        </div>
  </div>
 </div>
</div>  

    </div>

  </div>

<!--  RAGU 18 Dec 2018 --> 
<!-- Modal -->
    
<!--  RAGU 18 Dec 2018 --> 
<!-- Modal -->

<!--  RAGU 18 Dec 2018 --> 
<!-- Modal -->

<div class="modal fade" id="modalDashboard_ActivityAnalysis" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  style="overflow-x:auto;">
  <div class="modal-dialog" role="document">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
         <div class="col-sm-11 text-left"> <h4 class="modal-title"><span>Activity Analysis</span></h4></div>
        <div class="col-sm-1 text-right"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>

      </div>
      <div class="modal-body">
          
          
           <div align="center">
       
            <span id="spChartHead">N/A</span>
                             
            </div> 

             <div id="tab_chartAnalysis">
       
             </div>      

    
          
           <div align="center">
    <span id="spLegend"></span>
                             
         </div> 

	</div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
          
      </div>
    </div>

  </div>
</div>
</div>
</div>
</div>

</section>


<!-- Ragu 18 Dec 2018 --->

  <input type="hidden" id="hidSensorId" />
 <input type="hidden" id="hidStartDate"/>
     <input type="hidden" id="hidEndDate"/>

     <input type="hidden" id="hidDashboardData" />
    <input type="hidden" id="hidDashboardDataForChatAnalysis" />
    <input type="hidden" id="hidDashboardDataForFloorGroup" />
    <input type="hidden" id="hidDashboardDataForFirstTirgger" />
    <input type="hidden" id="hidDashboardDataPathWay" />
    <input type="hidden" id="hidDashboardUserRights"/>
    <input type="hidden" id="BatteryMaxTrigger"/>
    
     <input type="hidden" id="strcmbLocation" />

    


 <script src="../../plugins/jquery/jquery.min.js"></script>
    <!--   Bootstrap 3.3.2 JS -->
   <script src="../../plugins/bootstrap/js/bootstrap.bundle.min.js"></script>

     <!-- DATA TABES SCRIPT -->
    <script src="../../plugins/datatables/jquery.dataTables.min.js"></script>
<script src="../../plugins/datatables-bs4/js/dataTables.bootstrap4.min.js"></script>
<script src="../../plugins/datatables-responsive/js/dataTables.responsive.min.js"></script>
<script src="../../plugins/datatables-responsive/js/responsive.bootstrap4.min.js"></script>

    
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
<!-- AdminLTE App 
    <script src="../../dist/js/adminlte.min.js"></script>-->


    <script src="/plugins/chartnew.js/ChartNew.js" type="text/javascript"></script>
    
    


      <script src="/scripts/scripts.js"></script>
    <script src="/scripts/.js"></script>

    
   
    <script type="text/javascript">

    window.document.onkeydown = function (e) {
            //alert("hoge");
            if (!e) {
            e = event;
            }
            if (e.keyCode == 27) {
            lightbox_close();
            }
        };

    function lightbox_open(url) {
        var lightBoxVideo = document.getElementById("VisaChipCardVideo");
        lightBoxVideo.src =url;
        lightBoxVideo.play();

        window.scrollTo(0, 0);
        document.getElementById("light").style.display = "block";
        document.getElementById("fade").style.display = "block";
        lightBoxVideo.play();
    }

    function lightbox_close() {
        var lightBoxVideo = document.getElementById("VisaChipCardVideo");
        document.getElementById("light").style.display = "none";
        document.getElementById("fade").style.display = "none";
        lightBoxVideo.pause();
    }

    var dtEndDateMoment = new Date();
    dtEndDateMoment.setHours(23);
    dtEndDateMoment.setMinutes(59);
    fnGetDashboardDataForSite();

        //Ragu 17 Sep 2020
        var tmpSiteId = getParameterByName("id"); 
        //tmpSiteId = document.getElementById("strtxtSiteId");
        //Alert(tmpSiteId);

        //if (tmpSiteId >=1) {
            fnLoadDashboardSelSite(tmpSiteId);
        //}

        $('#daterange-btn').daterangepicker(
       {
                ranges: {

               //     'Today': [moment().subtract(1, 'days').format("YYYY-MM-DD HH:mm"), moment()],
               'Last 2 Days': [moment().subtract(24, 'hours').startOf('day'), moment()],
               'Last 7 Days': [moment().subtract(6, 'days').startOf('day'), moment()],
               'Last 30 Days': [moment().subtract(29, 'days').startOf('day'), moment()],
               'Last 2 Months': [moment().subtract(2, 'month').startOf('day'), moment()],
               'Last 3 Months': [moment().subtract(3, 'month').startOf('day'), moment()],
               'Last 6 Months': [moment().subtract(6, 'month').startOf('day'), moment()],
               'Last 12 Months': [moment().subtract(12, 'month').startOf('day'), moment()]
           },
           //startDate: moment().subtract(1, 'days').format("YYYY-MM-DD 00:00"),
                startDate: moment().subtract(24, 'hours').startOf('day'),
           //endDate: moment().format("YYYY-MM-DD 23:59"),  // Ragu 07 Mar 2017
           //startDate: moment().startOf('day'),
           endDate: moment(),
                //startDate: moment().subtract(29, 'days'),
                //endDate: moment().format("YYYY-MM-DD HH:mm"),
       },

        function (start, end) {

            var dtStartDate = start.format("YYYY-MM-DD 00:00");
            //var dtEndDate = moment().format("YYYY-MM-DD 23:59");
            var dtEndDate = end.format("YYYY-MM-DD 23:59");

       //var dtStartDate = moment().subtract(1, 'days').format("YYYY-MM-DD 00:00");   
       //var dtEndDate = moment().format("YYYY-MM-DD 23:59");

      //var dtEndDate = end.format("YYYY-MM-DD 23:59");  // Ragu 07 Mar 2017

        //document.getElementById("hidStartDate").value = dtStartDate;
        //document.getElementById("hidEndDate").value = dtEndDate;
       
       document.getElementById("spDateRange").innerHTML = " " + dtStartDate + '  to  ' + dtEndDate + "";
       $('#reportrange span').html(dtStartDate + ' - ' + dtEndDate);



       document.getElementById("hidStartDate").value = dtStartDate;
       document.getElementById("hidEndDate").value = dtEndDate;

           

       //Ragu 13 Sep 2018
        populateTime(dtStartDate, dtEndDate);
        
       }
       );

       
        function fnLoadDashboard()
        {
            var strSiteId = getParameterByName("id");
            //strSiteId = document.getElementById("strtxtSiteId");
            //Alert(strSiteId);
            if (strSiteId != "") {
                fnGetDashboardDataForSite(strSiteId);
            }
            //document.getElementById("BatteryMaxTrigger").value = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["BatteryMaxTrigger"].ToString());
            
        }

       
        fnLoadDashboard();
        getTimeStamp();

       

        function fnViewDashboard()
        {
            fnGetDashboardDataForSite(0);
            getTimeStamp();
        }

        function fnLoadDashboardSelSite(id1) {
            fnGetLocations(0);
            //document.getElementById("cmbLocation").SelectedIndex = 1;
            fnGetDashboardDataForSite(id1);
        }

        function fnLoadDashboardSelSite_location(id2)
        {
            /*intSiteId = document.getElementById("ContentPlaceHolder1_cmbSiteName").value
            if (intSiteId == 0) {
                 var strSiteId = getParameterByName("id");
                if (strSiteId >= 1) {
                    fnLoadDashboardSelSite(strSiteId);
                }
            }*/
           
            getTimeStamp();
            fnViewDashboard();
        }
        
        function autoRefreshDashClick()
        {
            for (i = 0; i <= 20000; i++)
            {
                fnViewDashboard();
                wait(5000);
                //alert("Refresed")
            }
        }
        function wait(ms) {
            var start = new Date().getTime();
            var end = start;
            while (end < start + ms) {
                end = new Date().getTime();
            }
        }

        function populateTime(tStartDate, tEndDate)
        {

        
            var select = document.getElementById("timerange-btn");
            if (select == null)
            {
                return;
            }

            while (select.options.length > 0) {
                select.remove(0);
            }  

            
            //var opt = document.createElement('option');
            //opt.value = "All";
            //opt.innerHTML = "All";
            //var loop = new Date(dateToYMD(tStartDate));
            //while (loop <= new Date(dateToYMD(tEndDate)))
            var difference = dateDiff(dateToYMD(tStartDate), dateToYMD(tEndDate));

            var dtTemp = new Date(dateToYMD(tStartDate));
            var fmtDate = dtTemp.getFullYear() + "/" + digitFix(dtTemp.getMonth()+1) + "/" + digitFix(dtTemp.getDate());

            for (var i = 0; i <= difference; i++)
            {
                for (var j = 0; j <= 23; j++) {
                    //startTime += 86400000
                    
                   
                    var opt = document.createElement('option');
                    if (i==0 && j==0)
                    {
                        opt.value = "0";
                        opt.innerHTML = "------ All Time ------";
                        select.appendChild(opt);
                    }
                    opt.value = fmtDate + " " + digitFix(j) + ":00";
                    opt.innerHTML = fmtDate + " " + digitFix(j) + ":00";
                    select.appendChild(opt);
                    
                    //dtTemp.setDate(dtTemp.getDate() + 1);
                }
                dtTemp.setDate(dtTemp.getDate() + 1);
                fmtDate = dtTemp.getFullYear() + "/" + digitFix(dtTemp.getMonth()+1) + "/" + digitFix(dtTemp.getDate());
            }
            //Ragu 17 Sep 2020

            fnViewDashboard();
        }

        function dateDiff(s1, s2) {
            var d1 = new Date(s1);
            var d2 = new Date(s2);
            var t2 = d2.getTime();
            var t1 = d1.getTime();

            return parseInt((t2 - t1) / (24 * 3600 * 1000));
        }

        function dateToYMD(date) {
            var d = date.slice(8,10);
            var m = date.slice(5, 7);
            var y = date.slice(0, 4);
            return '' + y + '-' +  m + '-' +  d;
            //return '' + y + '-' + (m <= 9 ? '0' + m : m) + '-' + (d <= 9 ? '0' + d : d);
        }
        function digitFix(temp)
        {
            //If date is not passed, get current date
            if (parseInt(temp) <= 9)
                return "0" + temp;
            else
                return temp;
        }

       
            function fnViewHourlyData()
            {

            var tmp = document.getElementById("timerange-btn").value;

            if (tmp != 0) {
                var dt = new Date(tmp);
                dt.setHours(dt.getHours() + 1);

                var tmp1 = dt.getFullYear() + "/" + digitFix(dt.getMonth() + 1) + "/" + digitFix(dt.getDate()) + " " + digitFix(dt.getHours()) + ":00";

                var dt0 = new Date(tmp);
                var tmp0 = dt0.getFullYear() + "/" + digitFix(dt0.getMonth() + 1) + "/" + digitFix(dt0.getDate()) + " " + digitFix(dt0.getHours()) + ":00";


                var id1 = getParameterByName("id");
                //id1 = document.getElementById("strtxtSiteId");
                //Alert(strSiteId);
                if (id1 <= 0) {
                    id1 = 0;
                }

                fnGetDashboardDataForSite_Hourly(id1, tmp, tmp1);


                

                var path, path1;
                var e = document.getElementById("ContentPlaceHolder1_cmbLocation");
                var intFloorMapId = e.options[e.selectedIndex].id;
                
                
                // Ragu Hid
                /*
                //getDataForPath(intFloorMapId, tmp, tmp1);

                $.ajax({
                    type: 'GET',
                    url: '/services/dashboard.aspx?id=9&floormapid=' + intFloorMapId + ' &startdate=' + tmp0 + ' &enddate=' + tmp1,
                    success: function (data) {
                        var sensor_data = data;
                        var pos = 0;
                        var x1 = "", y1 = "", x2="", y2="";
                        for (var i = 0; i < data.length; i++) {
                            tmp = data.charAt(i);
                            if (tmp == ",")
                            {
                                pos++; //X and Y
                            }

                            if (tmp == ".") {
                                pos++;
                                if (pos == 4) {
                                    //Draw Line
                                    //window.alert(x1 + " " + y1 + " " + x2 + " " + y2);
                                    drawArrow(parseInt(x1), parseInt(y1), parseInt(x2), parseInt(y2));
                                    pos = 0;
                                }
                                x1 = ""; y1 = ""; x2 = ""; y2 = "";
                            }
                            if ((tmp != ".") && (tmp != ","))
                            {
                                if (pos == 0) {
                                    x1 = x1 + tmp;
                                }
                                if (pos == 1) {
                                    y1 = y1 + tmp;
                                }
                                if (pos == 2) {
                                    x2 = x2 + tmp;
                                }
                                if (pos == 3) {
                                    y2 = y2 + tmp;
                                }
                            }
                        }
                    }
                });*/ 
                
            }
            else
            {
                fnGetDashboardDataForSite(id1);
            }
        }       
        
    
        function fnHourCheckData()
        {
            if (document.getElementById("timerange-btn").length <= 1) {
                populateTime(moment().subtract(1, 'days').format('YYYY/MM/DD 00:00'), moment().format('YYYY/MM/DD 23:59'));
            }
        }

        function dateToYMD1(date) {
            var d = date.getDate();
            var m = date.getMonth() + 1; //Month from 0 to 11
            var y = date.getFullYear();
            return '' + y + '-' + (m <= 9 ? '0' + m : m) + '-' + (d <= 9 ? '0' + d : d) + " 00:00";
        }

        function dateToYMD2(date) {
            var d = date.getDate();
            var m = date.getMonth() + 1; //Month from 0 to 11
            var y = date.getFullYear();
            return '' + y + '-' + (m <= 9 ? '0' + m : m) + '-' + (d <= 9 ? '0' + d : d) + " 23:59";
        }

        function getTimeStamp()
        {
            var today = new Date();
            var text = ('0' + today.getDate()).slice(-2) + '/' + ('0' + (today.getMonth() + 1)).slice(-2) + '/' + today.getFullYear() + " " + today.getHours() + ":00:00";
            document.getElementById("rodcaughtTimeStamp").value = text;
        }
        
        

        function fnViewPathWay() {
            if (document.getElementById("timerange-btn").length <= 1) {
                populateTime(moment().subtract(1, 'days').format('YYYY/MM/DD 00:00'), moment().format('YYYY/MM/DD 23:59'));
            }
            var tmp = document.getElementById("timerange-btn").value;
            if (tmp.length != 0) {
                var dt = new Date(tmp);
                dt.setHours(dt.getHours() + 1);

                var tmp1 = dt.getFullYear() + "/" + digitFix(dt.getMonth() + 1) + "/" + digitFix(dt.getDate()) + " " + digitFix(dt.getHours()) + ":00";

                var dt0 = new Date(tmp);
                var tmp0 = dt0.getFullYear() + "/" + digitFix(dt0.getMonth() + 1) + "/" + digitFix(dt0.getDate()) + " " + digitFix(dt0.getHours()) + ":00";

                var id1 = document.getElementById("ContentPlaceHolder1_cmbSiteName").value;

                var e = document.getElementById("ContentPlaceHolder1_cmbLocation");
                var intFloorMapId = e.options[e.selectedIndex].id;

                fnCallViewPathHttp(id1, tmp, tmp1, intFloorMapId);
            }
        }

        function btnRodentSubmit()
        {
            updRodentCaughtInfo();
        }

        function btnBaitConsumedSubmit()
        {
            updBaitConsumedInfo();
        }
       
        function fnViewAnalysis(selection)
        {
        if (document.getElementById("timerange-btn").length <= 1) {
            populateTime(moment().subtract(1, 'days').format('YYYY/MM/DD 00:00'), moment().format('YYYY/MM/DD 23:59'));
        }
        var tmp = document.getElementById("timerange-btn").value;
        if (tmp.length != 0)
        {
            var dt = new Date(tmp);
            dt.setHours(dt.getHours() + 1);

            var tmp1 = dt.getFullYear() + "/" + digitFix(dt.getMonth() + 1) + "/" + digitFix(dt.getDate()) + " " + digitFix(dt.getHours()) + ":00";

            var dt0 = new Date(tmp);
            var tmp0 = dt0.getFullYear() + "/" + digitFix(dt0.getMonth() + 1) + "/" + digitFix(dt0.getDate()) + " " + digitFix(dt0.getHours()) + ":00";

            var id1 = document.getElementById("ContentPlaceHolder1_cmbSiteName").value;
                
            var e = document.getElementById("ContentPlaceHolder1_cmbLocation");
            var intFloorMapId = e.options[e.selectedIndex].id;

            if (selection == 0) {
                fnGetAnalysisData0(id1, tmp, tmp1, intFloorMapId);
            }
            else if (selection == 1) {
                fnGetAnalysisData1(id1, tmp, tmp1, intFloorMapId);
            }
            else if (selection == 2) {
                fnGetAnalysisData2(id1, tmp, tmp1, intFloorMapId);
            }
            else if (selection == 3) {
                fnGetAnalysisData3(id1, tmp, tmp1, intFloorMapId);
            }
            else if (selection == 4) {
                fnGetAnalysisData4(id1, tmp, tmp1, intFloorMapId);
            }
            else if (selection == 5) {
                fnGetAnalysisData5(id1, tmp, tmp1, intFloorMapId);
            }
            else if (selection == 6) {
                fnGetAnalysisData6(id1, tmp, tmp1, intFloorMapId);
            }
        }
        }

        function fnViewRodentAnalysis(selection) {
            if (document.getElementById("timerange-btn").length <= 1) {
                populateTime(moment().subtract(1, 'days').format('YYYY/MM/DD 00:00'), moment().format('YYYY/MM/DD 23:59'));
            }
            var tmp = document.getElementById("timerange-btn").value;
            if (tmp.length != 0) {
                var dt = new Date(tmp);
                dt.setHours(dt.getHours() + 1);

                var tmp1 = dt.getFullYear() + "/" + digitFix(dt.getMonth() + 1) + "/" + digitFix(dt.getDate()) + " " + digitFix(dt.getHours()) + ":00";

                var dt0 = new Date(tmp);
                var tmp0 = dt0.getFullYear() + "/" + digitFix(dt0.getMonth() + 1) + "/" + digitFix(dt0.getDate()) + " " + digitFix(dt0.getHours()) + ":00";

                var id1 = document.getElementById("ContentPlaceHolder1_cmbSiteName").value;

                var e = document.getElementById("ContentPlaceHolder1_cmbLocation");
                var intFloorMapId = e.options[e.selectedIndex].id;

                if (selection == 0) {
                    fnGetRodentAnalysisData0(id1, tmp, tmp1, intFloorMapId);
                }
                else if (selection == 1) {
                    fnGetRodentAnalysisData1(id1, tmp, tmp1, intFloorMapId);
                }
                else if (selection == 2) {
                    fnGetRodentAnalysisData2(id1, tmp, tmp1, intFloorMapId);
                }
                else if (selection == 3) {
                    fnGetRodentAnalysisData3(id1, tmp, tmp1, intFloorMapId);
                }
                else if (selection == 4) {
                    fnGetRodentAnalysisData4(id1, tmp, tmp1, intFloorMapId);
                }
                else if (selection == 5) {
                    fnGetRodentAnalysisData5(id1, tmp, tmp1, intFloorMapId);
                }
                else if (selection == 6) {
                    fnGetRodentAnalysisData6(id1, tmp, tmp1, intFloorMapId);
                }
            }
        }
   
       
      
    </script>
  
  
    <span></span>
 
  
  
</asp:Content>

