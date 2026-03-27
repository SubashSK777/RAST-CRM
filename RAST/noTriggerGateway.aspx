<%@ Page Title="" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true"
  CodeFile="noTriggerGateway.aspx.cs" Inherits="noTriggerGateway" %>

  <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
  </asp:Content>
  <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <!-- Content Header (Page header) -->
    <%-- <script type="text/javascript">

      </script>--%>
      <section class="content-header">
        <h1>
          No Trigger Gateway Status
        </h1>
        <!-- <ol class="breadcrumb">
            <li><a href="noTriggerGateway.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li  class="active">No Trigger Gateway Status</li>
             </ol> -->

      </section>
      <section class="content">
        <div class="container-fluid">
          <div class="page-wrapper-box">

            <div class="row">


              <div class="col-12">

                <div class="box">

                  <div class="box-body">

                    <table style="border:0px">
                      <tr>
                        <td style="width:95%"></td>
                        <td style="text-align:right">

                          <!-- <button class="btn btn-primary" id="btnAddUser"  onclick="javascript:location.href='organization_ui.aspx?type=e&id=0';" >Add New Organzation</button>-->
                        </td>
                      </tr>
                    </table>
                    <br />
                    <span id="spTable" runat="server"></span>

                  </div><!-- /.box-body -->
                </div><!-- /.box -->
              </div><!-- /.col -->
            </div><!-- /.row -->
          </div>
        </div>
      </section><!-- /.content -->


      <br />


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

      <script type="text/javascript">

        $(function () {
          $('#tblNoTriggerGateway').dataTable();
          var roleId = $("#ContentPlaceHolder1_hidRoleId").val();
          if (roleId != 1) {
            document.getElementById("btnAddOrg").style.visibility = "hidden";
          }
        });



        function sendStatusCode(m_hubNumber, m_StatusCode) {  // Ragu 24 Jan 2018 for Sensor Battery Update event
          //window.alert(m_sensorID);

          var txtHubNumber = m_hubNumber.toString();
          var txtSMSMessage = m_StatusCode.toString();
          var xmlhttp = new XMLHttpRequest();

          xmlhttp.open("GET", "/services/sensor.aspx?id=6&strPhoneNumber=" + txtHubNumber.toString() + "&strSMSMessage=" + txtSMSMessage, false);
          xmlhttp.send();

          var intReturnValue = xmlhttp.responseText;

          // document.location.reload();

        }

      </script>

      <asp:HiddenField id="hidRoleId" runat="server" />

  </asp:Content>