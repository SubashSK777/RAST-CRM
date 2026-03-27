<%@ Page Title="" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true"
    CodeFile="sensor_config.aspx.cs" Inherits="sites_ui" %>


    <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script type="text/javascript" src="scripts/scripts.js"></script>
        <script type="text/javascript" src="/js/jQuery-1.8.3.js"></script>

        <link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css">
        <style>
            .imgBuildingPlan {
                display: block;
                max-width: 281px;
                width: auto;
                height: auto;
            }
        </style>

        <script type="text/javascript">
            function fnRangeChange() {
                //document.getElementById("ContentPlaceHolder1_Min").value = "";
                //document.getElementById("ContentPlaceHolder1_Max").value = "";
                var range = $("#ContentPlaceHolder1_cmbRange option:selected").text();
                var rspl = range.split("\(")[1].split("-");
                var rs = rspl[1].split("m");

                document.getElementById("ContentPlaceHolder1_Min").value = rspl[0];
                document.getElementById("ContentPlaceHolder1_Max").value = rs[0];

                if (range = "Analog(0-20mA)" || "Analog(4-20mA)") {
                    document.getElementById("cmbRange").value = "Analog(0-20mA)" || "Analog(4-20mA)";
                    document.getElementById("Analog(0-20mA)" || "Analog(4-20mA)").disabled = true;

                }
                else {
                    document.getElementById("Value").disabled = false;
                }

            }
        </script>
        <script type="text/javascript">
            // WRITE THE VALIDATION SCRIPT IN THE HEAD TAG.
            function isNumber(evt) {
                var iKeyCode = (evt.which) ? evt.which : evt.keyCode
                if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57)) {

                    //return false;
                    alert("Please enter only numbers:");
                }


                else {
                    return true;
                }

            }
        </script>

    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" onmousemove="capmouse(event)">
        <section class="content-header">
            <h1>
                Sensor Management
            </h1>
            <ol class="breadcrumb">
                <li><a href="map.aspx" <i class="fa fa-home" </i>Home</a></li>
                <li class="active">Sensor Management</li>
            </ol><br />
        </section>
        <section class="content">
            <div class="container-fluid">
                <div class="page-wrapper-box">
                    <div class="row">

                        <table class="table table-bordered table-striped" width="100%">

                            <tr>
                                <td valign="top" style="width:300px">
                                    <div class="col-md-12">
                                        <!-- general form elements -->
                                        <div class="box box-primary">

                                            <span id="ContentPlaceHolder1_spMessage">Sensor Configuration</span>
                                            <div id='image_panel' runat="server">
                                                <img src='plans/plan.jpg' id='buildingPlan'
                                                    onclick="javascript:getMouseXY(event)" runat="server"
                                                    class="imgBuildingPlan" />
                                            </div>
                                        </div>
                                    </div>


                                <td valign="top">
                                    <div align="center"><span id="spdelMsg"></span></div>


                                    <div class="col-md-6">
                                        <!-- general form elements -->
                                        <div class="box box-primary">

                                            <div class="box-body">
                                                <form>
                                                    <div class="form-group">
                                                        <label for="exampleInputPassword1">Hub Id</label><span
                                                            id="spSiteName"></span>

                                                        <asp:DropDownList class="form-control" ID="cmbHubId"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group">
                                                        <label for="exampleInputName">Sensor Name</label><span
                                                            id="spSensorName"></span>
                                                        <input type="text" class="form-control" id="txtSensorName"
                                                            placeholder="Enter Sensor Name" disabled="disabled" />

                                                    </div>

                                                    <div class="form-group">
                                                        <label for="exampleSenorType">Sensor Type</label><span
                                                            id="spSensorType"></span>


                                                        <asp:DropDownList class="form-control" ID="cmbRange"
                                                            runat="server" onChange="javascript:fnRangeChange()">

                                                            <asp:ListItem Text="[--Select--]" Value="0">[-select-]
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="Analog(0-20mA)">Analog(0-20mA)
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="Analog(4-20mA)">Analog(4-20mA)
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="Value">Value</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="exampleInputEmail1">Range</label><span
                                                            id="spRange"></span>
                                                        <input type="text" id="Min" aria-labelledby='lblRange'
                                                            onkeypress="javascript: return isNumber(event)"
                                                            runat="server" /> &nbsp
                                                        &nbsp &nbsp &nbsp &nbsp &nbsp
                                                        <input type="text" id="Max" aria-labelledby='lblRange'
                                                            onkeypress="javascript: return isNumber(event)"
                                                            runat="server" />
                                                    </div>

                                                    <div class="form-group">
                                                        <label for="exampleInputUnit">Unit</label><span id="spUnit">
                                                        </span>
                                                        <input type="text" class="form-control" id="txtUnit"
                                                            placeholder="Enter the Unit" disabled="disabled" />

                                                    </div>

                                                    <div class="form-group">
                                                        <label for="exampleInputStatus">Status</label><span
                                                            id="spStatus"></span>
                                                        <asp:DropDownList class="form-control" ID="ddStatus"
                                                            runat="server">
                                                            <asp:ListItem Text="[--Select--]" Value="1">Active
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="0">Deactive</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </div>
                                                </form>

                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-6">
                                        <div class="box box-primary">
                                            <div class="box-body">
                                                <div class="form-group">
                                                    <%-- <asp:TextBox ID="txtMin" runat="server">Minimum Threshold Value
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                            runat="server" ControlToValidate="txtMin"
                                                            ErrorMessage="Enter a valid number"
                                                            ValidationExpression="^\d$">
                                                        </asp:RegularExpressionValidator>--%>

                                                        <label for="exampleInputMinimum">MinimumThreshold
                                                            Value</label><span id="spMin"></span>
                                                        <input type="text" class="form-control" id="txtMin"
                                                            disabled="disabled"
                                                            onkeypress="javascript: return isNumber(event)" />
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputMaximum">MaximumThreshold Value</label><span
                                                        id="spMax"></span>
                                                    <input type="text" class="form-control" id="txtMax"
                                                        disabled="disabled"
                                                        onkeypress="javascript: return isNumber(event)" />

                                                    <input type="hidden" id="txtX" value="" visible="false" />
                                                    <input type="hidden" id="txtY" value="" visible="false" />

                                                    <input type="hidden" id="hidSensorId" value="" visible="false" />
                                                    <input type="hidden" id="hidSensorName" value="" visible="false" />
                                                </div>

                                                <br />

                                                <span id="spError"></span>
                                                <div class="form-group">
                                                    <div class="box-footer">
                                                        <button type="submit" class="btn btn-primary"
                                                            onclick="javascript:fnAddSensorDetails()"
                                                            id="btnAddSensor">Add
                                                            Sensor</button>

                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <!-- general form elements -->
                                        <div class="box box-primary">
                                            <div class="box-body">

                                                <div class="form-group">
                                                    <table id="tblSensors" class="table table-bordered table-striped"
                                                        runat="server">
                                                        <thead>
                                                            <tr>
                                                                <th style="width:20px;">#</th>
                                                                <th>Hub Id</th>
                                                                <th>Sensor Name</th>
                                                                <th>Sensor Type</th>
                                                                <th>Minimum Threshold Level</th>
                                                                <th>Maximum Threshold Level</th>
                                                                <th>Unit</th>
                                                                <th>Status</th>

                                                                <th style="width:50px;"></th>

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                        </tbody>
                                                    </table>



                                                </div>


                                                <div class="box-footer">
                                                    <button type="submit" class="btn btn-primary"
                                                        onclick="javascript:location.href='/sites_list.aspx'">Save</button>
                                                </div>


                                            </div><!-- /.box-body -->


                                        </div><!-- /.box -->



                                    </div>
                                </td>
                        </table>

                        <!-- Modal -->
                    </div>
                </div>
            </div>
        </section>

        <%--<script type="text/javascript">
            function Download(){
            window.open('/plans/plan.jpg','_self');
            }
            </script>
            --%>

            <%-- <script type="text/javascipt">
                setTimeout("startDownload()",5000); //starts download after 5 seconds
                </script>
                --%> <!-- jQuery 2.1.3 -->
                <script src="/plugins/jQuery/jQuery-2.1.3.min.js"></script>
                <!-- Bootstrap 3.3.2 JS -->
                <script src="/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
                <!-- DATA TABES SCRIPT -->
                <script src="/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
                <script src="/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
                <!-- SlimScroll -->
                <script src="/plugins/slimScroll/jquery.slimscroll.min.js" type="text/javascript"></script>
                <!-- FastClick -->
                <script src='/plugins/fastclick/fastclick.min.js'></script>
                <!-- AdminLTE App -->

                <script src="/scripts/scripts.js"></script>



    </asp:Content>