<%@ Page Title="" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true"
  CodeFile="configuration.aspx.cs" Inherits="housekeeping" %>

  <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  </asp:Content>
  <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="content-header">
      <h1>
        Configuration
      </h1>
      <ol class="breadcrumb">
        <li><a href="map.aspx"><i class="fa fa-home"></i> Home</a></li>

        <li class="active">Configuration</li>
      </ol>
      <br />
    </section>

    <section class="content">
      <div class="container-fluid">
        <div class="page-wrapper-box">

          <div class="row">
            <!-- left column -->
            <div class="col-md-6">
              <!-- general form elements -->
              <div class="box box-primary">

                <div class="box-body">

                  <div class="form-group">
                    <label for="exampleInputEmail1">Maximum Data Storage Period (In Days). Enter 0 for no
                      limit</label><span id="spConfig"></span>
                    <input type="text" class="form-control" id="txtMaxStoragePeriod"
                      placeholder="Enter Max Storage Period (In Days). Enter 0 for no limit" runat="server"
                      onkeypress="return validNumbers(event,this,spConfig,'Enter only numbers');" />
                  </div>


                </div><!-- /.box-body -->
                <div class="box-footer">
                  <button type="submit" class="btn btn-primary" onclick="javascript:fnSaveConfiguration();">Save
                    Configuration</button>
                </div>
              </div><!-- /.box -->



            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- Modal -->


    </div>
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
  </asp:Content>