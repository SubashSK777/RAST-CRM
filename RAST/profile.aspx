<%@ Page Title="" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true"
  CodeFile="profile.aspx.cs" Inherits="profile" %>


  <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="scripts/scripts.js"></script>

    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Update Profile
      </h1>
      <ol class="breadcrumb">
        <li><a href="map.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Update Profile</li>
      </ol>
      <br />
    </section>
    <section class="content">
      <div class="container-fluid">
        <div class="page-wrapper-box">
          <div class="row">


            <div class="col-xs-12">

              <div class="box">

                <div class="box-body" align="">

                  <span id="spTable" runat="server"></span>


                  <label for="exampleInputEmail1">Change Password</label><span id="spPassword"></span>
                  <input type="password" style="width:300px" class="form-control" id="txtPassword" runat="server"
                    placeholder="Enter Password" />



                  <label for="exampleInputEmail1">Phone Number</label><span id="spPhone"></span>
                  <input type="text" style="width:300px" class="form-control" id="txtPhone" runat="server"
                    placeholder="Enter Phone Number" />

                  </br>

                  <button class="btn btn-primary" id="btnAddUser" onclick="javascript:fnUpdateProfile();">Save</button>

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

    <!-- SlimScroll -->
    <script src="/plugins/slimScroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <!-- FastClick -->
    <script src='/plugins/fastclick/fastclick.min.js'></script>
    <!-- AdminLTE App -->

    <script src="/scripts/scripts.js"></script>
    <!-- page script -->
    <form>

      <asp:HiddenField id="hidUserDetails" runat="server" />
    </form>

  </asp:Content>