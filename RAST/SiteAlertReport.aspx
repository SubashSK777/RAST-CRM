<%@ Page Title="" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="SiteAlertReport.aspx.cs" Inherits="SiteAlertReport" %>


    <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link href="/plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
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



        <div class="modal fade" id="modDashboardData" tabindex="-1" role="dialog" style="width: 100%;overflow-x:auto;"
            aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span
                                aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title"><span id="myModalLabel">Data View</span></h4>
                    </div>
                    <div class="modal-body">
                        <span id="spTable"><img src="/img/loading.gif" /></span>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>




        <section class="content-header">
            <h1>
                Site Alert Count Report
            </h1>
            <!--<ol class="breadcrumb">
            <li><a href="map.aspx"><i class="fa fa-home"></i> Home</a></li>
              <li><a>Site Alert Count Report</a></li>
             </ol>
            <br />-->
        </section>


        <section class="content">
            <div class="container-fluid">
                <div class="page-wrapper-box">
                    <div class="row">
                        <div class="col-md-12">
                            <!-- general form elements -->
                            <div class="box box-primary">

                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label>Select Organization :</label>
                                                <select id="cmbOrganizationName" runat="server" class="form-control">
                                                </select>


                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Select Period :</label>
                                                <div class="input-group">
                                                    <button class="btn btn-default pull-right" id="daterange-btn">
                                                        <i class="fa fa-calendar"></i> <span id="spDateRange"> Select
                                                            Date </span>
                                                        <i class="fa fa-caret-down"></i>
                                                    </button>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group pull-left">
                                                <br />
                                                <button id="btnDataReport" class="btn btn-primary"
                                                    onclick="javascript:fnViewReport();">View Report</button>


                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group pull-right">
                                                <br />
                                                <button id="btnData" class="btn btn-primary"
                                                    onclick="javascript:fnViewChartData();">View Data</button>


                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>



                            <div>
                                <div class="row">
                                    <div class="col-md-12" id="tab_chart" style="overflow-x:auto;">
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>



        <input type="hidden" id="hidStartDate" />
        <input type="hidden" id="hidEndDate" />

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
        <!-- AdminLTE App 
    <script src="../../dist/js/adminlte.min.js"></script>-->



        <script src="/scripts/scripts.js"></script>

        <script type="text/javascript">

            var dtEndDateMoment = new Date();
            dtEndDateMoment.setHours(23);
            dtEndDateMoment.setMinutes(59);

            $('#daterange-btn').daterangepicker(
                {
                    ranges: {
                        //     'Today': [moment().startOf('day'), moment()],
                        'Last 2 Days': [moment().subtract(24, 'hours').startOf('day'), moment()],
                        'Last 7 Days': [moment().subtract(6, 'days').startOf('day'), moment()],
                        'Last 30 Days': [moment().subtract(29, 'days').startOf('day'), moment()],
                        'Last 2 Months': [moment().subtract(2, 'month').startOf('day'), moment()]
                    },
                    startDate: moment().subtract(29, 'days'),
                    endDate: moment().format("YYYY-MM-DD HH:mm"),
                },
                function (start, end) {
                    var dtStartDate = start.format("YYYY-MM-DD HH:mm");
                    var dtEndDate = moment().format("YYYY-MM-DD HH:mm");

                    document.getElementById("spDateRange").innerHTML = " " + dtStartDate + '  to  ' + dtEndDate + "";
                    $('#reportrange span').html(dtStartDate + ' - ' + dtEndDate);

                    var intSensorId = 50;//document.getElementById("hidSensorId").value;

                    document.getElementById("hidStartDate").value = dtStartDate;
                    document.getElementById("hidEndDate").value = dtEndDate;





                }
            );
            function fnViewReport() {
                var dtStartDate = document.getElementById("hidStartDate").value;
                var dtEndDate = document.getElementById("hidEndDate").value;
                var intOrganizationId = document.getElementById("ContentPlaceHolder1_cmbOrganizationName").value;

                fnGetSiteAlertCount(intOrganizationId, dtStartDate, dtEndDate);
            }

            function fnGetSiteAlertCount(intOrganizationId, dtStartDate, dtEndDate) {

                if ((document.getElementById("hidStartDate").value != "") && (document.getElementById("hidEndDate").value != "")) {
                    dtStartDate = document.getElementById("hidStartDate").value;
                    dtEndDate = document.getElementById("hidEndDate").value;
                }

                //arrSensorDetails = strSensorId.split("_");
                //document.getElementById("spMinimumThreshold").innerHTML = arrSensorDetails[1];
                //document.getElementById("spMaximumThreshold").innerHTML = arrSensorDetails[2];
                //document.getElementById("spSensorId").innerHTML = arrSensorDetails[0];

                // document.getElementById("hidSensorId").value = arrSensorDetails[0];
                document.getElementById("hidStartDate").value = dtStartDate;
                document.getElementById("hidEndDate").value = dtEndDate;

                document.getElementById("spDateRange").innerHTML = " " + dtStartDate + '  to  ' + dtEndDate + "";

                $('#tab_chart').show();
                document.getElementById("tab_chart").innerHTML = "";

                $(document).ready(function () {
                    $.ajax({
                        type: 'GET',
                        url: '/services/alertcount_report.aspx?id=2&startdate=' + dtStartDate + '&enddate=' + dtEndDate + "&orgid=" + intOrganizationId,
                        success: function (data) {

                            var dv_chart_data = JSON.parse(data);
                            var arrXAxis = new Array();
                            var arrValue = new Array();
                            // var arrMin = new Array();
                            //  var arrMax = new Array();

                            if (dv_chart_data.length > 0) {

                                var canvas = document.createElement('canvas');
                                canvas.id = "canvas_chart";
                                //canvas.width = screen.availWidth - 50;
                                canvas.width = 1500;
                                canvas.height = 400;
                                document.getElementById("tab_chart").appendChild(canvas);

                                for (var i = 0; i < dv_chart_data.length; i++) {


                                    arrXAxis.push(dv_chart_data[i].Site_Name);
                                    arrValue.push(dv_chart_data[i].i_Value);
                                    // arrMin.push(dv_chart_data[i].sensor_min);
                                    //arrMax.push(dv_chart_data[i].sensor_max);

                                }

                                var intLatestValue = arrValue[dv_chart_data.length - 1];
                                var dtLastAvailableData = arrXAxis[dv_chart_data.length - 1];

                                //document.getElementById("spLastDateReceived").innerHTML = dtLastAvailableData;
                                //document.getElementById("spSensorId").innerHTML = arrSensorDetails[0];
                                //document.getElementById("spSensorValue").innerHTML = intLatestValue;
                                //document.getElementById("btnExport").style.visibility = "visible";
                                document.getElementById("btnData").style.visibility = "visible";

                                var barChartData = {
                                    labels: arrXAxis,

                                    datasets: [
                                        {
                                            type: "bar",
                                            fillColor: "#FF7F50",
                                            strokeColor: "#3c8dbc",
                                            highlightFill: "#5da2ca",
                                            highlightStroke: "#5da2ca",
                                            data: arrValue,
                                        }

                                    ]
                                }

                                var ctx = document.getElementById("canvas_chart").getContext("2d");
                                var myLineBarChart = new Chart(ctx).Overlay(barChartData);

                                document.getElementById("spTable").innerHTML = "";

                                fnShowChartData();
                            }
                            else {

                                document.getElementById("tab_chart").innerHTML = "No data available";
                                document.getElementById("spTable").innerHTML = "No data available";

                            }
                        }
                    });
                });

            }

            function fnShowChartData() {
                var dtStartDate = document.getElementById("hidStartDate").value;
                var dtEndDate = document.getElementById("hidEndDate").value;
                var intOrganizationId = document.getElementById("ContentPlaceHolder1_cmbOrganizationName").value;


                $.ajax({
                    type: 'GET',
                    async: false,
                    url: '/services/alertcount_report.aspx?id=5&startdate=' + dtStartDate + '&enddate=' + dtEndDate + "&orgid=" + intOrganizationId,
                    success: function (data) {
                        document.getElementById("spTable").innerHTML = data;

                        $(function () {
                            $("#tblData").dataTable();

                        });

                    }
                });
            }


        </script>

    </asp:Content>