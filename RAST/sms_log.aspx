<%@ Page Title="" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true"
  CodeFile="sms_log.aspx.cs" Inherits="sms_log" %>

  <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
  </asp:Content>
  <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        SMS Logs
      </h1>
      <!--<ol class="breadcrumb">
            <li><a href="map.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li  class="active">SMS Logs</li>
             </ol>
            <br />-->
    </section>
    <section class="content">
      <div class="container-fluid">
        <div class="page-wrapper-box">
          <div class="row">


            <div class="col-xs-12">

              <div class="box">

                <div class="box-body" id="spTable" runat="server">

                </div><!-- /.box-body -->
              </div><!-- /.box -->
            </div><!-- /.col -->
          </div><!-- /.row -->
        </div>
      </div>
    </section><!-- /.content -->


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
    <script type="text/javascript">
      $(document).ready(function () {
        $("#tblSMSLogs").dataTable();
      });
    </script>
  </asp:Content>