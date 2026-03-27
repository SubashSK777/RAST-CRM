<%@ Page Title="" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true"
  CodeFile="deployLog.aspx.cs" Inherits="deployLog" %>

  <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
  </asp:Content>
  <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <!-- Content Header (Page header) -->
    <%-- <script type="text/javascript">

      </script>--%>
      <section class="content-header">
        <h1>
          Deployment Log
        </h1>
        <ol class="breadcrumb">
          <li><a href="deployLog.aspx"><i class="fa fa-home"></i> Home</a></li>
          <li class="active">Deploy Log</li>
        </ol>
        <br />
      </section>
      <section class="content">
        <div class="container-fluid">
          <div class="page-wrapper-box">

            <div class="row">


              <div class="col-xs-12">

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

      <!-- jQuery 2.1.3 -->
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
      <!-- AdminLTE App 
    <script src="../../dist/js/adminlte.min.js"></script>-->


      <script src="/scripts/scripts.js"></script>
      <!-- page script -->
      <form>
        <script type="text/javascript">
          $(function () {
            $("#tblGetDeployLog").dataTable();
            var roleId = $("#ContentPlaceHolder1_hidRoleId").val();
            if (roleId != 1) {
              //document.getElementById("btnAddOrg").style.visibility = "hidden";
            }
          });



        </script>

        <asp:HiddenField id="hidRoleId" runat="server" />
      </form>
  </asp:Content>