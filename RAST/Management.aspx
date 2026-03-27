<%@ Page Title="" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true"
  CodeFile="Management.aspx.cs" Inherits="Management" %>

  <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css">
    <style>
      .imgBuildingPlan {
        display: block;
        max-width: 281px;
        width: auto;
        height: auto;
      }
    </style>

    <script type="text/javascript" src="scripts/scripts.js"></script>
    <script type="text/javascript" src="/js/jQuery-1.8.3.js"></script>
    <script type="text/javascript">
      function fnRangeChange() {
        document.getElementById("ContentPlaceHolder1_Min").value = "";
        document.getElementById("ContentPlaceHolder1_Max").value = "";
        var range = $("#ContentPlaceHolder1_cmbRange option:selected").text();
        var rspl = range.split("\(")[1].split("-");
        var rs = rspl[1].split("m");

        document.getElementById("ContentPlaceHolder1_Min").value = rspl[0];
        document.getElementById("ContentPlaceHolder1_Max").value = rs[0];
      }
    </script>

  </asp:Content>
  <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="content-header">
      <h1>
        Sensor Configuration Page
      </h1>
      <ol class="breadcrumb">
        <li><a href="map.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li><a href="sites_list.aspx">List of Sites</a></li>

        <li class="active">Sensor Management Page</li>
      </ol>
      <br />
    </section>


    <section class="content">
      <div class="container-fluid">
    <div class="row">
      <!-- left column -->
      <div class="col-md-3">
        <!-- general form elements -->
        <div class="box box-primary">
          <div id='image_panel' runat="server">
            <img src='plans/plan.jpg' id='buildingPlan' onclick="javascript:getMouseXY(event)" runat="server"
              class="imgBuildingPlan" />


          </div>
        </div><!-- /.box -->



      </div>

      <td valign="top">
        <div align="center"><span id="spdelMsg"></span></div>
        <div class="row">
          <!-- left column -->
          <div class="col-md-4">
            <!-- general form elements -->
            <div class="box box-primary">
              <form>
                <div class="box-body">
                  <div class="form-group">
                    <label for="exampleInputPassword1">SiteName</label><span id="spSiteName"></span>

                    <asp:DropDownList class="form-control" ID="ddSiteLocation" runat="server">
                    </asp:DropDownList>
                  </div>
                </div>
                <div class="form-group">
                  <label for="exampleInputEmail1">Sensor Name</label><span id="spSensorName"></span>
                  <input type="text" class="form-control" id="txtSensorName" placeholder="Enter sensor name"
                    runat="server" />
                </div>

                <div class="form-group">
                  <label for="exampleSenorType">Sensor Type</label><span id="spSensorType" onclick="function()"></span>


                  <asp:DropDownList class="form-control" ID="cmbRange" runat="server"
                    onChange="javascript:fnRangeChange()">

                    <asp:ListItem Text="[--Select--]" Value="0">[-select-]</asp:ListItem>
                    <asp:ListItem Value="1">Analog(0-20mA)</asp:ListItem>
                    <asp:ListItem Value="2">Analog(4-20mA)</asp:ListItem>
                    <asp:ListItem Value="3">Value</asp:ListItem>
                  </asp:DropDownList>
                </div>
                <div class="form-group">

                  <label for="exampleInputEmail1">Range</label><span id="spRange"></span>
                  <input type='text' id='Min' aria-labelledby='lblRange' runat="server" />&nbsp &nbsp &nbsp&nbsp
                  &nbsp&nbsp
                  <input type='text' id='Max' aria-labelledby='lblRange' runat="server" />

                </div>
                <div class="form-group">
                  <label for="exampleInputEmail1">Unit</label><span id="spUnit"></span>
                  <input type="text" class="form-control" id="txtUnit" placeholder="Enter the unit" runat="server" />
                </div>
                <div class="form-group">
                  <label for="exampleInputPassword1">Status</label><span id="spStatus"></span>


                  <asp:DropDownList class="form-control" ID="ddStatus" runat="server">

                    <asp:ListItem Text="[--Select--]" Value="1">Active</asp:ListItem>
                    <asp:ListItem Value="0">Deactive</asp:ListItem>

                  </asp:DropDownList>
                </div>
                </div< /div>
              </form>
            </div>
          </div>
          <div class="col-md-4">
            <!-- general form elements -->
            <div class="box box-primary">
              <%-- <div id="wrapper">
                <div id="down">
                  --%> <div class="box-body">
                    <div class="form-group">

                    </div>
                    <div class="form-group">
                      <label for="exampleInputEmail1">Minimum Threshold Value</label><span id="spMin"></span>
                      <input type="text" class="form-control" id="txtMin" runat="server" />
                    </div>
                    <div class="form-group">
                      <label for="exampleInputEmail1">Maximum Threshold value</label><span id="spMax"></span>
                      <input type="text" class="form-control" id="txtMax" runat="server" />
                    </div>
                    <div class="box-footer">
                      <a href="data:application/octet-stream,field1%2Cfield2%0Afoo%2Cbar%0Agoo%2Cgai%0A">
                        <button id="btnDownload" class="btn btn-primary" runat="server">Download</button>

                        <button id="btnCSV" class="btn btn-primary" runat="server">CSV</button></a>

                    </div>

                  </div>
                </div>
            </div>
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