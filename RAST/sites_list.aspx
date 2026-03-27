<%@ Page Title="" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true" CodeFile="sites_list.aspx.cs" Inherits="sites_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <link href="/plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Content Header (Page header) -->
        <section class="content-header">
          <h1>
           List of Sites
          </h1>
          <!--<ol class="breadcrumb">
            <li><a href="map.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li  class="active">List of Sites</li>
             </ol>
            <br />-->
        </section>
  
    <section class="content">
        <div class="container-fluid">
          <div class="page-wrapper-box">

     <div class="row">
      
        
            <div class="col-12">
             
             <div class="card">
               
                <div class="card-body">

<table style="border:0px;width:100%">
<tr>    
 
    <td style="text-align:right">
          <button class="btn btn-primary" onclick="javascript:location.href='sites_ui.aspx';">Add New Site</button> 
  
</td></tr>
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

    

    
    <!-- SlimScroll -->
    <script src="/plugins/slimScroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <!-- FastClick -->
    <script src='/plugins/fastclick/fastclick.min.js'></script>
    <script src="/scripts/scripts.js"></script>
    <!-- page script -->

    <!-- page script -->
    <script type="text/javascript">
        $(function () {
            $('#tblSiteList').dataTable({
              "paging": true,
          "lengthChange": true,
          "searching": true,
          "ordering": true,
          "info": true,
          "autoWidth": true,
          "responsive": true,
            });
        });
    </script>
</asp:Content>

