<%@ Page Title="" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true" CodeFile="organization_ui.aspx.cs" Inherits="organization_ui" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <!--<script src="https://rawgit.com/schmich/instascan-builds/master/instascan.min.js"></script>-->
    <script src="/QRScanner/instascan.min.js"></script>
 <link href="/plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" /> 
     <!--<link href="/bootstrap/css/bootstrap.min.css" rel="stylesheet" />-->
     <link rel="stylesheet" href="/css/jquery.fileupload.css"/>
    

   
</asp:Content>

<asp:Content ID="conOrganization" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     

     <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-body">
            <img id="imgLogo" runat="server"/>
           
            
        </div>
    </div>
  </div>
</div>

      <section class="content-header">
          <h1>
          Organization Management
          </h1>
          <!--<ol class="breadcrumb">
            <li><a href="map.aspx"><i class="fa fa-home"></i> Home</a></li>
              <li><a href="Organization_list.aspx">List of Organizations</a></li>
            <li  class="active">Organization Management</li>
             </ol>-->
            
        </section>

     <section class="content">
        <div class="container-fluid">

        <div align="center"><span id="spdelMsg"></span></div>
        
            <div class="row">

         
      <!-- left column -->
            <div class="col-md-6">


              <!-- general form elements -->
              <div class="box box-primary">
             
                  <div class="box-body">
                    
                      <div class="form-group">
                      <label for="exampleInputEmail1">Organization Name</label><span id="spOrganizationName"></span>
                      <input type="text" class="form-control" id="txtOrganizationName" placeholder="Enter Organization Name" runat="server"/>
                    </div>
                
                    <div class="form-group">
                      <label for="exampleInputEmail1">Contact Person</label><span id="spContactPerson"></span>
                      <input type="text" class="form-control" id="txtContactPerson" placeholder="Enter Contact Person Name" runat="server"/>
                    </div>
                     <div class="form-group">
                      <label for="exampleInputEmail1">Email Address</label><span id="spEmailAddress"></span>
                      <input type="email" class="form-control" id="txtEmail" placeholder="Enter Email Address" runat="server"/>
                    </div>
                    
                      <div class="form-group">
                      <label for="exampleInputEmail1">Phone Number</label><span id="spPhoneNumber"></span>
                      <input type="text" class="form-control" id="txtPhoneNumber" placeholder="Enter Phone Number" runat="server"/>
                    </div>
                    
                    <div class="form-group">
                        <label for="exampleInputFile">Logo</label><span id="spLogo"></span> <br />
                        <span class="btn btn-success fileinput-button">
                            <i class="glyphicon glyphicon-plus"></i>
                            <span>Select file...</span>
                            <!-- The file input field used as target for the file upload widget -->
                            <input id="fileupload" type="file" name="files[]" />
                            </span>
                            <button class="btn btn-primary"  data-toggle="modal" data-target="#myModal"><i class="glyphicon glyphicon-search"></i>&nbsp;View Logo</button> 
                            <div class="form-group"><label for="exampleInputFile"></label><div id="progress" class="progress"><div class="progress-bar progress-bar-success"></div> </div></div>
                            <!-- The container for the uploaded files -->
                            <div id="files" class="files"></div>
                    </div>

                 </div>
             </div>
            </div>



        <div class="col-md-6">
              <!-- general form elements -->
              <div class="box box-primary">
                
             
                  <div class="box-body">
                       <div class="form-group">
                      <label for="exampleInputPassword1">Organization Address</label><span id="spAddress"></span>
                     <textarea class="form-control" rows="8" placeholder="Enter ..." id="txtAddress" runat="server"></textarea>
                    </div>
                  
                      <div class="form-group">
                      <label for="exampleInputEmail1">Organization Code</label><span id="spOrgCode"></span>
                      <input type="email" class="form-control" id="txtOrgCode" placeholder="Enter Organization Code" runat="server"/>
                    </div>

                    
                     <div class="form-group">
                      <label for="exampleInputPassword1">Status</label><span id="spStatus"></span>
                     <select class="form-control" id="cmbStatus" runat="server">
                         <option value="1">Active</option>
                         <option value="2">Deactive</option>
                     </select>
                    </div>
                   
                  </div><!-- /.box-body -->
                  <div class="box-footer">
                    <button type="submit" class="btn btn-primary"  onclick="javascript:fnAddUpdateOrganization();">Save</button>
                      <button type="submit" class="btn btn-danger"  onclick="javascript:fnDeleteOrganization();">Delete</button>
                       
                  </div>





 <!--Ragu 20 Aug 2019 -->

                

               
                  
<div class="modal fade" id="modHubData" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  style="overflow-x:auto;">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
         <div class="col-sm-11 text-left"><h4 class="modal-title"><span id="myModalLabel_HubData">Hub detail Information</span></h4></div>
        <div class="col-sm-1 text-right"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>
        
      </div>
        <div><br />
                   
                           <table>
                            <tr>
                            <th style="width: 5%;"><span></span></th>
                            <th style="width: 10%;"><span>&nbsp;&nbsp;&nbsp;Hub detail</span></th>
                            <td style="width: 25%;"><input type="text" id="hubSerialNumber"/></td>
                            <td style="width: 10%;"><img src="img/qrScan1.jpg" name="qrbutton" id="qrbutton" onclick="javascript:qrScanHub();" /></td>


                               
                           <th style="width: 10%;"><span>Active  </span> </th>
                            
                            <td style="width: 20%;"><select name="hubactive" id="hubactive">
                              <option value="Active">Active</option>
                              <option value="In Active">In Active</option>
                            </select></td>
                      
                                <td style="width: 2%;"></td>
                        <td style="width: 5%;"><button id="btnHubSubmit" class="btn btn-primary" onclick="javascript:fnHubSubmit();">Submit</button></td>
                            <td style="width: 3%;"></td>    
                            </tr>
                     </table>
                            <br />
        

        <div class="modal-body">
               <table class="table table-striped table-bordered table-hover order-column" id="tblData_hub"></table>
         </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
          </div>

            <video id="preview" style="position:fixed;z-index:10;display:none;width: 570px;height: 320px;top: 100px;left: 100px;"></video>

        </div>
  </div>
 </div>
</div>



<!-- Ragu Sensor Widows -->
<div class="modal fade" id="modSensorData" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  style="overflow-x:auto;">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
          <div class="col-sm-11 text-left"><h4 class="modal-title"><span id="myModalLabel_SensorData">Bulk QR Creation Add / Print </span></h4></div>
        <div class="col-sm-1 text-right"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>
       </div>
        <div>

                         <div class="box-body">

                             
                               <div class="form-group" align="center">
                              <label style="text-align:center" id="lblOrgCode"></label>
                            </div>
                  
                              <div class="form-group">
                                <table>
                                <tr>
                              <td><label for="exampleInputEmail1">Generate QR From : </label></td>
                              <td><input type="text" class="form-control" id="txtQRStart" placeholder="Enter Start Number "/></td>
                                <td><span> </span></td>
                               <td><label for="exampleInputEmail1">Generate QR To : </label></td>
                              <td><input type="text" class="form-control" id="txtQREnd" placeholder="Enter End Number "/></td>
                              </tr></table>
                            </div>

                             

                             
                             <div class="form-group">
                                 <table>
                                <tr style="text-align:center">
                              <td style="text-align:center"><button id="btnGenerate" class="btn btn-primary" onclick="javascript:fnBulkSensorSubmit();">Generate QR</button></td>
                                     <td style="text-align:center"><span> </span></td>
                              <td style="text-align:center"> <button id="btnPrintGenerateQR" class="btn btn-primary" onclick="javascript:fnBulkQRPrint();">Print QR (Range) </button></td>
                              </tr></table>
                             </div>

                        </div>
                            
                   
                   
                    <!--<div class="box-body">

                             <div class="form-group"  align="center">
                              <label style="text-align:center" id="lbldisp3">Add Sensor one by One</label>
                            </div>

                           <table>
                            <tr>
                            <th style="width: 5%;"><span></span></th>
                            <th style="width: 10%;"><span>&nbsp;&nbsp;&nbsp;Sesnor detail</span></th>
                            <td style="width: 25%;"><input type="text" id="sensorQRNumber"/></td>
                            <td style="width: 10%;"><img src="img/qrScan1.jpg" name="qrbutton" id="qrbutton" onclick="javascript: qrScanSensor();" /></td>


                               
                           <th style="width: 10%;"><span>Active  </span> </th>
                            
                            <td style="width: 20%;"><select name="hubactive" id="sensoractive">
                              <option value="Active">Active</option>
                              <option value="In Active">In Active</option>
                            </select></td>
                      
                                <td style="width: 2%;"></td>
                        <td style="width: 5%;"><button id="btnSensorSubmit" class="btn btn-primary" onclick="javascript:fnSensorSubmit();">Submit & Print</button></td>
                            <td style="width: 5%;"><button id="btnPrint" class="btn btn-primary" onclick="javascript:fnQRPrint();">Print All</button></td>
                       
                            <td style="width: 3%;"></td>    
                            </tr>
                     </table>
                     </div>-->
                            
                       
        
           

                 <div class="box-body">
                               <div class="form-group" align="center">
                              <label style="text-align:center" id="lbldisp4">View Sensor QR List</label>
                            </div>
                    </div>

        <div class="modal-body">
               <table class="table table-striped table-bordered table-hover order-column" id="tblData_Sensor"></table>
         </div>
          

            <video id="preview_sensor" style="position:fixed;z-index:10;display:none;width: 570px;height: 320px;top: 100px;left: 100px;"></video>

            <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
          </div>

        </div>
  </div>
 </div>
</div>

                  <!--- QR Print Area ---->


 <div class="modal fade" id="modQRPrint" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  style="overflow-x:auto;">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title"><span id="myQRPrint">QR Print</span></h4>
      </div>
        <div>
       
            <div id="qrcode_div" style="width:109px; height:109px; margin-top: 5px; page-break-after:always;"></div>
             

          <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
          </div>

 
        </div>
  </div>
 </div>
</div>




                   <input type="hidden"  id="hidSensorQRlist" value="" visible="false"/>
              </div><!-- /.box -->
         </div>

<!-- Modal -->
                  

            </div>

 </div>
         <br/>
         <div style="text-align: center">
                <button id="btnSubmitHub" class="btn btn-primary" onclick="javascript:btnViewHubTable();">Add / View Hub</button><button id="btnSubmitSensor" class="btn btn-primary" onclick="javascript:btnViewSensorTable();">Add / View Sensor</button>
        </div>

   
 </section>
         

    
    <!-- jQuery -->
<script src="../../plugins/jquery/jquery.min.js"></script>
<!-- Bootstrap 4 -->
<script src="../../plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
<!-- DataTables -->
<script src="../../plugins/datatables/jquery.dataTables.min.js"></script>
<script src="../../plugins/datatables-bs4/js/dataTables.bootstrap4.min.js"></script>
<script src="../../plugins/datatables-responsive/js/dataTables.responsive.min.js"></script>
<script src="../../plugins/datatables-responsive/js/responsive.bootstrap4.min.js"></script>
<!-- AdminLTE App 
    <script src="../../dist/js/adminlte.min.js"></script>-->

    
<!-- AdminLTE for demo purposes -->
<script src="../../dist/js/demo.js"></script>

    
    <!-- SlimScroll -->
    <script src="/plugins/slimScroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <!-- FastClick -->
    <script src='/plugins/fastclick/fastclick.min.js'></script>
    <script src="/scripts/scripts.js"></script>
    <!-- page script -->

      <script src="/scripts/scripts.js"></script>
<!-- The jQuery UI widget factory, can be omitted if jQuery UI is already included -->
<script src="js/vendor/jquery.ui.widget.js"></script>
<!-- The Iframe Transport is required for browsers without support for XHR file uploads -->
<script src="js/jquery.iframe-transport.js"></script>
<!-- The basic File Upload plugin -->
<script src="js/jquery.fileupload.js"></script>

<script src="jsPDF/jspdf.debug.js"></script>  <!-- PDF Creater-->
<script src="jsPDF/jspdf.min.js"></script>


<script type="text/javascript">
    
    /*jslint unparam: true */
    /*global window, $ */

    $(function () { 
        'use strict';
        // Change this to the location of your server-side upload handler:

        //var intSiteId = "1";//document.getElementById("ContentPlaceHolder1_txtSiteId").value;
        var intSiteId = document.getElementById("ContentPlaceHolder1_txtSiteId").value;


        var url = 'upload_logo.aspx?id=' + intSiteId;
        $('#fileupload').fileupload({
            url: url,
            dataType: 'json',
            add: function (e, data) {
                var goUpload = true;
                var uploadFile = data.files[0];
                if (!(/\.(gif|jpg|jpeg|png)$/i).test(uploadFile.name)) {
                    document.getElementById("slLogo").innerHTML = "&nbsp;<font color='red'>You must select an image file only</font>";
                    goUpload = false;
                }
                if (uploadFile.size > 2000000) { // 2mb
                    document.getElementById("slLogo").innerHTML = "&nbsp;<font color='red'>Please upload a smaller image, max size is 2 MB</font>";

                    goUpload = false;
                }
                if (goUpload == true) {
                    document.getElementById("ContentPlaceHolder1_hidUploadFileName").value = uploadFile.name;
                    data.submit();
                }
            },
            done: function (e, data) {
                $.each(data.result.files, function (index, file) {
                    $('<p/>').text(file.name).appendTo('#files');


                });
            },
            progressall: function (e, data) {
                var progress = parseInt(data.loaded / data.total * 100, 10);
                $('#progress .progress-bar').css(
                    'width',
                    progress + '%'
                );
            }
        }).prop('disabled', !$.support.fileInput)
            .parent().addClass($.support.fileInput ? undefined : 'disabled');
    });

   $(function() {
       $("#tblData_Sensor").dataTable();
        var roleId1 = $("#ContentPlaceHolder1_hidRoleId1").val();
        if (roleId1 != 1) {
            //document.getElementById("btnAddOrg").style.visibility = "hidden";
        }
   });

    $(function() {
        $("#tblData_hub").dataTable();
        var roleId2 = $("#ContentPlaceHolder1_hidRoleId2").val();
        if (roleId2 != 1) {
            //document.getElementById("btnAddOrg").style.visibility = "hidden";
        }
    });

    function fnHubSubmit() {
        updHubActiveInfo();

       
    }

    function fnSensorSubmit() {
        updSensorActiveInfo();
       

    }

    function fnBulkSensorSubmit() {
        var tmpOrgCode = document.getElementById('ContentPlaceHolder1_txtOrgCode').value;
        updBulkSensorActiveInfo(tmpOrgCode);
    }

    function fnBulkQRPrint() {
        var tmpOrgCode = document.getElementById('ContentPlaceHolder1_txtOrgCode').value;
        printBulkSensorActiveInfo(tmpOrgCode);
    }

    function btnViewHubTable()
    {

        showHubActiveInfo();
        $('#modHubData').modal('show');
    }

    function btnViewSensorTable() {
        
        var lbl = "Organization Code : ";
        var tmpOrgCode = document.getElementById('ContentPlaceHolder1_txtOrgCode').value;
        //document.getElementById('ContentPlaceHolder1_hidQRPrefix').value = tmpOrgCode;

        lbl = lbl + tmpOrgCode;
        document.getElementById('lblOrgCode').innerHTML = lbl;
        
        showSensorActiveInfo();
        $('#modSensorData').modal('show');

    }

    function fnQRPrint()
    {
        //getSensorActiveInfoList();
        var qrList = getSensorActiveInfoList();
        document.getElementById('hidSensorQRlist').value = qrList;
        var qrListArr = new Array();
        qrListArr = qrList.split(",")

        
        
        for (i = 0; i <= qrListArr.length - 1; i++) {
            //qrcodevar.makeCode(qrListArr[i]);
            new QRCode(document.getElementById("qrcode_div"), qrListArr[i]);
            ClientSidePrint("qrcode_div");

        }

        /*var docPdf = new jsPDF({ orientation: 'landscape', unit: 'mm', format: [290, 290] });
        source = $('#qrcode_div')[0];
        specialElementHandlers = {
            '#bypassme': function (element, renderer) {
                return true
            }
        };
        margins = {
            top: 10,
            bottom: 10,
            left: 10,
            width: 290
        };
        docPdf.fromHTML(
            source, // HTML string or DOM elem ref.
            margins.left, // x coord
            margins.top, { // y coord
                'width': margins.width, // max width of content on PDF
                'elementHandlers': specialElementHandlers
            },

            function (dispose) {
                docPdf.save('qrScan1.pdf');
            }, margins
        );

        /*var divContents = document.getElementById("qrcode_div").innerHTML;
        var a = window.open("", "", "height=290, width=290");
        a.document.write("<html>");
        a.document.write("<body>");
        a.document.write(divContents);
        a.document.write("</body></html>");
        a.document.close();
        a.print();*/

        $('#modQRPrint').modal('show');

        //DoPrint();  // Default Brother Print --No working

        
       


        
       
        /* var docPdf = new jsPDF({orientation: 'landscape',unit: 'mm',format: [290, 290]});
        var docPdf = new jsPDF();
        docPdf.fromHTML($('qrcode'), function (dispose) {
            docPdf.save('qrScan1.pdf');
        });
        $('#modQRPrint').modal('show');
        */
        
        
        //window.loaction.href = "qrPrint.htm?data=" + document.getElementById(hidSensorQRlist).value;
    }
    scanner = new Instascan.Scanner({ video: document.getElementById('preview') });
    //let scanner = new Instascan.Scanner({ video: document.getElementById('preview') });
    function qrScanHub() {
        if ($('#preview').css('display') != 'none') {
            $('#preview').hide('none');
            scanner.stop();
            return;
        }

        document.getElementById('preview').style.display = 'block';

        scanner.addListener('scan', function (content) {
            document.getElementById('hubSerialNumber').value = content;
            document.getElementById('preview').style.display = 'none';
            scanner.stop();
            return;
        });
        Instascan.Camera.getCameras().then(function (cameras) {
            if (cameras.length == 1) {
                scanner.start(cameras[0]);
            }
            else if (cameras.length > 1) {
                scanner.start(cameras[cameras.length - 1]);
            } else {
                console.error('No cameras found.');
            }
        }).catch(function (e) {
            //console.error(e);
        });
    }


    scanner1 = new Instascan.Scanner({ video: document.getElementById('preview_sensor') });
    function qrScanSensor() {
        if ($('#preview_sensor').css('display') != 'none') {
            $('#preview_sensor').hide('none');
            scanner1.stop();
            return;
        }

        document.getElementById('preview_sensor').style.display = 'block';

        scanner1.addListener('scan', function (content) {
            document.getElementById('sensorQRNumber').value = content;
            document.getElementById('preview_sensor').style.display = 'none';
            scanner1.stop();
            return;
        });
        Instascan.Camera.getCameras().then(function (cameras) {
            if (cameras.length == 1) {
                scanner.start(cameras[0]);
            }
            else if (cameras.length > 1) {
                scanner.start(cameras[cameras.length - 1]);
            } else {
                console.error('No cameras found.');
            }
        }).catch(function (e) {
            console.error(e);
        });
    }


    function DoPrint(strExport) {
      
        /*try {
            const theForm = document.getElementById("myForm");
            const strPath = DATA_FOLDER + "SensorQRCode1.lbx";
            const objDoc = bpac.IDocument;
            const ret = await objDoc.Open(strPath);
            if (ret == true) {
                const objSesnorQR = await objDoc.GetObject("SensorQRCode");
                objSesnorQR.Text = "";

                if (strExport == "") {
                    objDoc.StartPrint("", 0);
                    objDoc.PrintOut(1, 0);
                    objDoc.EndPrint();
                }
                else {
                    const image = await objDoc.GetImageData(4, 0, 100);
                    const img = document.getElementById("previewArea");
                    img.src = image;
                }

                objDoc.Close();
            }
        }
        catch (e) {
            console.log(e);
        }*/
    }

    function ClientSidePrint(idDiv) {
        var w = 110;
        var h = 110;
        var l = (window.screen.availWidth - w) / 2;
        var t = (window.screen.availHeight - h) / 2;

        var sOption = "toolbar=no,location=no,directories=no,menubar=no,scrollbars=yes,width=" + w + ",height=" + h + ",left=" + l + ",top=" + t;
        // Get the HTML content of the div
        var sDivText = window.document.getElementById(idDiv).innerHTML;
        // Open a new window
        var objWindow = window.open("", "Print", sOption);
        // Write the div element to the window
        objWindow.document.write(sDivText);
        objWindow.document.close();
        // Print the window            
        objWindow.print();
        // Close the window
        objWindow.close();
    } 



</script>

        <input type="hidden" id="hidUploadFileName" runat="server" />
      

</asp:Content>

