<%@ Page Title="" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="sites_ui.aspx.cs" Inherits="sites_ui" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
        <!--<script src="https://rawgit.com/schmich/instascan-builds/master/instascan.min.js"></script>-->
        <script src="/QRScanner/instascan.min.js"></script>

        <link rel="stylesheet" href="css/jquery.fileupload.css" />
        <!-- <link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css"/> -->
        <!-- Generic page styles -->
        <!-- CSS to style the file input field as button and adjust the Bootstrap progress bars -->




        <script type="text/javascript">
            function Phone() {
                var code = "txtPhoneNumber";
                //- Ragu 29/6/18
                // if (!document.getElementById(code).value.match(/^[0-9]+$/) && document.getElementById(code).value != "") {
                //document.getElementById(code).value = "";
                //document.getElementById(code).focus();
                //alert("Please enter phone no. in digits");  
                //return false;
                //}
            }


        </script>
        <script type="text/javascript">
            function LoadModalDiv() {
                var bcgDiv = document.getElementById("divBackground");
                bcgDiv.style.display = "block";
            }
            var popUpObj;
            function showModalPopUp(url) {
                var width = 1000;
                var height = 550;
                var left = (screen.width / 2) - (width / 2);
                var top = (screen.height / 2) - (height / 2);


                url = url + "?id=" + document.getElementById("ContentPlaceHolder1_txtSiteId").value;


                popUpObj = window.open(url,
                    "ModalPopUp",
                    "toolbar=no," +
                    "scrollbars=no," +
                    "location=no," +
                    "statusbar=no," +
                    "menubar=no," +
                    "resizable=0," +
                    "width=" + width + "," +
                    "height=" + height + "," +
                    "left = " + left + "," +
                    "top=" + top
                );
                popUpObj.focus();
                LoadModalDiv();
            }
            function HideModalDiv() {
                var bcgDiv = document.getElementById("divBackground");
                bcgDiv.style.display = "none";


                var lon = getCookie("lon");
                var lat = getCookie("lat");

                if (lon != "") {
                    document.getElementById("ContentPlaceHolder1_txtLongitude").value = lon;
                }

                if (lat != "") {
                    document.getElementById("ContentPlaceHolder1_txtLatitude").value = lat;
                }



                document.cookie = "lon=; expires=Thu, 01 Jan 1970 00:00:00 UTC";
                document.cookie = "lat=; expires=Thu, 01 Jan 1970 00:00:00 UTC";

            }
        </script>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-body">
                        <img id="imgBuildingPlan" runat="server" />


                    </div>
                </div>
            </div>
        </div>


        <section class="content-header">
            <h1>
                Add / Edit Site
            </h1>
            <!--<ol class="breadcrumb">
            <li><a href="map.aspx"><i class="fa fa-home"></i> Home</a></li>
              <li><a href="sites_list.aspx">List of Sites</a></li>
            <li  class="active">Site Management</li>
             </ol>
            <br />-->
        </section>
        <div id="divBackground"
            style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; left:0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8;display:none">
        </div>

        <section class="content">
            <div class="container-fluid">

                <div class="row">
                    <!-- left column -->
                    <div class="col-md-6">
                        <!-- general form elements -->
                        <div class="box box-primary">

                            <div class="box-body">

                                <div class="form-group">
                                    <label for="exampleInputEmail1">Organization</label><span
                                        id="spOrganization"></span>
                                    <select class="form-control" id="cmbOrganization" runat="server">

                                    </select>
                                </div>


                                <div class="form-group">
                                    <label for="exampleInputEmail1">Site Id</label>
                                    <input type="text" class="form-control" id="txtSiteId" placeholder="" disabled
                                        runat="server" />
                                </div>

                                <div class="form-group">
                                    <label for="exampleInputEmail1">Site Name</label><span id="spSiteNameError"></span>
                                    <input type="email" class="form-control" id="txtSiteName"
                                        placeholder="Enter Site Name" runat="server">
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1" id="lat">Site Latitude</label><span
                                        id="spSiteLatitude"></span>
                                    <a class="pull-right" href="#lat" onclick="showModalPopUp('site_location.aspx')">
                                        <i class="fa fa-globe "></i>
                                    </a>

                                    <input type="text" class="form-control" id="txtLatitude"
                                        placeholder="Selected automatically from Map" disabled="disabled"
                                        runat="server">
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Site Longitude</label><span
                                        id="spSiteLongitude"></span>
                                    <input type="text" class="form-control" id="txtLongitude"
                                        placeholder="Selected automatically from Map" disabled="disabled"
                                        runat="server">
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Site Address</label><span id="spSiteAddress"></span>
                                    <textarea class="form-control" rows="3" placeholder="Enter ..." id="txtSiteAddress"
                                        runat="server" maxlength="255"></textarea>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputFile">Building Floor Map</label><span
                                        id="spBuildingFloorMap"></span> <br />

                                    <span class="btn btn-success fileinput-button">
                                        <i class="glyphicon glyphicon-plus"></i>
                                        <span>Select file...</span>
                                        <!-- The file input field used as target for the file upload widget -->
                                        <input id="fileupload" type="file" name="files[]" multiple
                                            data-sequential-uploads="true" data-form-data='{"script": "true"}' />
                                    </span>

                                    <button class="btn btn-primary" data-toggle="modal" data-target="#myModal"><i
                                            class="glyphicon glyphicon-search"></i>&nbsp;View Building Map</button>


                                    <div class="form-group">
                                        <label for="exampleInputFile"></label>

                                        <div id="progress" class="progress">
                                            <div class="progress-bar progress-bar-success"></div>
                                        </div>
                                    </div>
                                    <!-- The container for the uploaded files -->
                                    <div id="files" class="files"></div>
                                </div>

                                <div class="form-group">
                                    <label for="exampleInputEmail1">Deployment Technician Name</label><span
                                        id="spDeploymentTechncianName"></span>
                                    <select class="form-control" id="cmbDeploymentTechnicianName" runat="server">

                                    </select>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Alert Technician Name</label><span
                                        id="spAlertTechnicianName"></span>
                                    <select class="form-control" id="cmbAlertTechnicianName" runat="server">


                                    </select>
                                </div>

                            </div><!-- /.box-body -->

                        </div><!-- /.box -->



                    </div>
                    <div class="col-md-6">
                        <!-- general form elements -->
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Hub Id</label> <span id="lblHubIdError"></span>

                                    <input type="text" class="form-control" id="txtHubId" placeholder="Enter Hub Id" />
                                </div>

                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-xs-11 col-sm-11 col-lg-11">
                                            <label for="exampleInputPassword1">Hub Phone Number</label><span
                                                id="lblHubPhoneNumberError"></span>
                                            <input type="text" class="form-control" id="txtPhoneNumber"
                                                placeholder="Enter Hub Phone Number" onkeypress="javascript:Phone()" />
                                        </div>
                                        <div class="col-xs-1 col-sm-1 col-lg-1">
                                            <label for="lblqrbutton"></label><span id="lblqrbutton"></span><br />
                                            <!--input type="button" name="qrbutton" onclick="javascript: window.open('/QRScanner/qrReader.htm', '1561103305702', 'width=500,height=500,toolbar=0,menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0')";) />
                                   <input type="button" style="background-position: center center; background-image: url('img/qrScan1.jpg'); background-repeat: no-repeat; background-size: cover; background-attachment: fixed; width: 35px; height: 35px; display: block; position: fixed; z-index: auto;" name="qrbutton" onclick="javascript: qrScanLocal();" /> -->
                                            <img src="img/qrScan1.jpg" name="qrbutton" id="qrbutton"
                                                onclick="javascript: qrScanLocal();" />
                                        </div>
                                    </div>
                                    <br />


                                    <video id="preview"
                                        style="position:fixed;z-index:10;display:none;width: 570px;height: 320px;top: 100px;left: 100px;"></video>



                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-xs-6 col-sm-6 col-lg-6">
                                                <label for="lblHubOnTime">Hub On Time</label> <span
                                                    id="lblHubOnTime"></span>
                                                <input type="text" class="form-control" id="txtHubOnTime"
                                                    value="00:01" />
                                            </div>
                                            <div class="col-xs-6 col-sm-6 col-lg-6">
                                                <label for="lblHubOffTime">Hub Off Time</label> <span
                                                    id="lblHubOffTime"></span>
                                                <input type="text" class="form-control" id="txtHubOffTime"
                                                    value="23:59" />

                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-xs-6 col-sm-6 col-lg-6">
                                                <label for="lblMobileProvider">Mobile Provider</label> <span
                                                    id="lblMobileProvider"></span>
                                                <select class="form-control" id="SelectMobileProvider">
                                                    <option value="Singtel">Singtel</option>
                                                    <option value="Starhub">Starhub</option>
                                                    <option value="M1">M1</option>
                                                    <option value="Republic">Republic</option>
                                                    <option value="Others">Others</option>
                                                </select>
                                            </div>

                                            <div class="col-xs-6 col-sm-6 col-lg-6">
                                                <label for="exampleInputPassword1">SIM Type</label> <span
                                                    id="lblSIMType"></span>
                                                <select class="form-control" id="SelectTypeOfSim">
                                                    <option value="M2M">M2M</option>
                                                    <option value="Prepaid">Prepaid</option>
                                                    <option value="Postpaid">Postpaid</option>
                                                    <option value="Others">Others</option>
                                                </select>
                                            </div>
                                        </div>
                                        <!--<div class="col-xs-4 col-sm-4 col-lg-4">
                                    <label for="exampleInputPassword1">No. Of Sensor</label> <span id="lblSIMType"></span>   
                                    <input type="text" class="form-control" id="txtNOfSensor" placeholder="Enter No. Of Sensor"/>
                                </div>-->

                                        <!-- Ragu 21 May 2018-->

                                    </div>


                                    <!-- Ragu 21 May 2018-->

                                    <br /><button type="submit" class="btn btn-primary" id="btnAddHubDetails"
                                        onclick="javascript:fnAddHubDetails()">Add Hub Details</button>

                                </div>
                                <div class="form-group">
                                    <table id="tblHubDetails" class="table table-bordered table-striped" runat="server">
                                        <thead>
                                            <tr>
                                                <th style="width:50px;">Sl No</th>
                                                <th>Hub Id</th>
                                                <th>Hub Number</th>
                                                <th>On Time</th>
                                                <th>Off Time</th>
                                                <th>Provider</th>
                                                <th>SIM Type</th>


                                                <th style="width:50px;"></th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                        </tbody>
                                    </table>

                                </div>


                                <br />

                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-xs-6 col-sm-6 col-lg-6">
                                            <label for="exampleInputPassword1">Contract Start Date</label>
                                            <input type="text" class="form-control" id="txtContractStart"
                                                placeholder="Enter Contract Start date" />
                                        </div>
                                        <div class="col-xs-6 col-sm-6 col-lg-6">
                                            <label for="exampleInputPassword1">Contract End Date</label>
                                            <input type="text" class="form-control" id="txtContractEnd"
                                                placeholder="Enter Contract End date" />

                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xs-6 col-sm-6 col-lg-6">
                                            <label for="exampleInputPassword1">Contract Value</label>
                                            <input type="text" class="form-control" id="txtContractValue"
                                                placeholder="Enter Contract Value" />
                                        </div>

                                        <div class="col-xs-6 col-sm-6 col-lg-6">
                                            <label for="exampleInputPassword1">Contract Term</label>
                                            <select class="form-control" id="SelectContractTerm" runat="server">
                                                <option value="0">Trail</option>
                                                <option value="1">Free</option>
                                                <option value="1">Contract</option>
                                                <option value="1">Job</option>
                                            </select>
                                        </div>
                                    </div>

                                    <div class="row">

                                        <div class="col-xs-6 col-sm-6 col-lg-6">
                                            <label for="exampleInputPassword1">Number of Repeater</label>
                                            <input type="text" class="form-control" id="txtNoOfRepeater"
                                                placeholder="Enter Number of Repeater" />
                                        </div>

                                        <div class="col-xs-6 col-sm-6 col-lg-6">
                                            <label for="exampleInputPassword1">Status</label>
                                            <select class="form-control" id="cmbStatus" runat="server">
                                                <option value="1">Active</option>
                                                <option value="0">Deactive</option>
                                            </select>
                                        </div>

                                    </div>
                                </div>

                                <input type="hidden" id="hidEdit" value="0" />
                                <input type="hidden" id="hidHubId" value="0" />
                            </div><!-- /.box-body -->

                            <div class="box-footer">
                                <!-- <button type="submit" class="btn btn-primary">Add / Update</button> -->
                                <button type="submit" class="btn btn-info"
                                    onclick="javascript:fnConfigureSensors();">Save Site & Configure Sensors</button>
                                <button type="submit" class="btn btn-danger"
                                    onclick="javascript:fnDeleteSite();">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Delete&nbsp;&nbsp;&nbsp;&nbsp;</button>
                            </div>

                        </div><!-- /.box -->

                    </div>

                </div>

                <!-- Modal -->


            </div>
        </section>
        <script src="/plugins/jQuery/jQuery-2.1.3.min.js"></script>
        <!-- Bootstrap 3.3.2 JS -->
        <script src="/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>

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



        <script src="/scripts/scripts.js"></script>
        <!-- The jQuery UI widget factory, can be omitted if jQuery UI is already included -->
        <script src="js/vendor/jquery.ui.widget.js"></script>
        <!-- The Iframe Transport is required for browsers without support for XHR file uploads -->
        <script src="js/jquery.iframe-transport.js"></script>
        <!-- The basic File Upload plugin -->
        <script src="js/jquery.fileupload.js"></script>





        <!-- <script src="/QRScanner/instascan.min.js"></script>-->

        <script>



            scanner = new Instascan.Scanner({ video: document.getElementById('preview') });

            function qrScanLocal() {
                /*if (navigator.userAgent.indexOf("Chrome") != -1)
                {
                    qrScanLocalChrome();
                }
                else
                {
                    qrScanLocalIE();
                }*/

                if ($('#preview').css('display') != 'none') {
                    $('#preview').hide('none');
                    scanner.stop();
                    return;
                }

                document.getElementById('preview').style.display = 'block'

                scanner.addListener('scan', function (content) {
                    document.getElementById('txtPhoneNumber').value = content;
                    document.getElementById('preview').style.display = 'none';
                    scanner.stop();
                    return;
                });
                Instascan.Camera.getCameras().then(function (cameras) {
                    if (cameras.length == 1) {
                        scanner.start(cameras[0]);
                    }
                    else if (cameras.length == 2) {
                        scanner.start(cameras[1]);
                    } else {
                        console.error('No cameras found.');
                    }
                }).catch(function (e) {
                    //console.error(e);
                });
            }

            function fnDeleteRowOld(intRowNo, intHubId) {


                var params = "hubid=" + intHubId;

                $.ajax({
                    type: 'GET',
                    url: '/services/site.aspx?id=4&=' + params,
                    success: function (data) {
                        if (data == 1) {
                            document.getElementById("hidEdit").value = "";
                            document.getElementById("hidHubId").value = "";
                            document.getElementById("txtHubId").value = "";
                            document.getElementById("txtPhoneNumber").value = "";
                            document.getElementById("txtHubOnTime").value = "00:01";
                            document.getElementById("txtHubOffTime").value = "23:59";

                            document.getElementById("SelectMobileProvider").value = "Singtel";
                            document.getElementById("SelectTypeOfSim").value = "M2M";


                            document.getElementById("btnAddHubDetails").innerHTML = "Add Hub Details";

                            xmlhttp.send();
                            var intReturnValue = xmlhttp.responseText;
                            document.getElementById("ContentPlaceHolder1_tblHubDetails").deleteRow(intRowNo);
                        }

                    }
                });
            }




            /*jslint unparam: true */
            /*global window, $ */
            $(function () {
                'use strict';
                // Change this to the location of your server-side upload handler:

                var intSiteId = document.getElementById("ContentPlaceHolder1_txtSiteId").value;


                var url = 'upload_buildingplan.aspx?id=' + intSiteId;
                $('#fileupload').fileupload({
                    url: url,
                    dataType: 'json',
                    add: function (e, data) {
                        var goUpload = true;
                        $.each(data.files, function (index, file) {
                            var uploadFile = data.files[0];
                            if (!(/\.(gif|jpg|jpeg|png)$/i).test(uploadFile.name)) {
                                document.getElementById("spBuildingFloorMap").innerHTML = "&nbsp;<font color='red'>You must select an image file only</font>";
                                goUpload = false;
                            }
                            if (uploadFile.size > 2000000) { // 2mb
                                document.getElementById("spBuildingFloorMap").innerHTML = "&nbsp;<font color='red'>Please upload a smaller image, max size is 2 MB</font>";

                                goUpload = false;
                            }
                        });
                        if (goUpload == true) {
                            //document.getElementById("ContentPlaceHolder1_hidUploadFileName").value = uploadFile.name;
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



        </script>
        <input type="hidden" id="hidUploadFileCount" runat="server" />
        <input type="hidden" id="hidUploadFileNames" runat="server" />
        <input type="hidden" id="hidQrScanValue" runat="server" />
        <input type="hidden" id="strtxtSiteId" runat="server" />
        <input type="hidden" id="strcmbOrganization" runat="server" />

    </asp:Content>