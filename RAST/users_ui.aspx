<%@ Page Title="" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true"
  CodeFile="users_ui.aspx.cs" Inherits="users_ui" %>


  <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" src="scripts/scripts.js"></script>

    <section class="content-header">
      <h1>
        Add / Edit Users
      </h1>
      <!--<ol class="breadcrumb">
            <li><a href="map.aspx"><i class="fa fa-home"></i> Home</a></li>
              <li><a href="users_list.aspx">List of Users</a></li>
            <li  class="active">User Management</li>
             </ol>
            <br />-->
    </section>
    <div align="center"><span id="spdelMsg"></span></div>

    <section class="content">
      <div class="container-fluid">

        <div class="row">
          <!-- left column -->
          <div class="col-md-6">
            <!-- general form elements -->
            <div class="box box-primary">

              <div class="box-body">
                <div class="form-group">
                  <label for="exampleInputEmail1">Name</label><span id="spName"></span>
                  <input type="text" class="form-control" id="txtName" runat="server" placeholder="Enter User Name" />
                </div>

                <div class="form-group">
                  <label for="exampleInputEmail1">Email Address</label><span id="spEmail"></span>
                  <input type="email" class="form-control" id="txtEmail" runat="server"
                    placeholder="Enter Email Address" />
                </div>
                <div class="form-group">
                  <label for="exampleInputEmail1">Password</label><span id="spPassword"></span>
                  <input type="password" class="form-control" id="txtPassword" runat="server"
                    placeholder="Enter Password" />
                </div>
                <div class="form-group">
                  <label for="exampleInputEmail1">Confirm Password</label><span id="spConfirmPassword"></span>
                  <input type="password" class="form-control" id="txtConfirmPassword" runat="server"
                    placeholder="Enter password again" />
                </div>
                <div class="form-group">
                  <label for="exampleInputEmail1">Phone Number</label><span id="spPhone"></span>
                  <input type="text" class="form-control" id="txtPhone" runat="server"
                    placeholder="Enter Phone Number" />
                </div>

              </div><!-- /.box-body -->
            </div><!-- /.box -->

          </div>
          <div class="col-md-6">
            <!-- general form elements -->
            <div class="box box-primary">
              <form>
                <div class="box-body">
                  <div class="form-group">
                    <label for="exampleInputPassword1">Organization</label><span id="spOrgLocation"></span>

                    <asp:DropDownList class="form-control" ID="ddOrgLocation" runat="server">
                    </asp:DropDownList>
                  </div>
                </div>
                <div class="box-body">
                  <div class="form-group">
                    <label for="exampleInputPassword1">Role</label><span id="spRole"></span>

                    <asp:DropDownList class="form-control" ID="ddRole" runat="server">
                    </asp:DropDownList>
                  </div>
                </div>
                <div class="box-body">
                  <div class="form-group">
                    <label for="exampleInputPassword1">Status</label><span id="spStatus"></span>


                    <asp:DropDownList class="form-control" ID="ddStatus" runat="server">

                      <asp:ListItem Text="[--Select--]" Value="1">Active</asp:ListItem>
                      <asp:ListItem Value="0">Deactive</asp:ListItem>

                    </asp:DropDownList>
                  </div>
                </div>
                <asp:HiddenField id="hidUserId" runat="server" />
              </form>
              <div class="box-footer">
                <button type="submit" class="btn btn-primary" onclick="javascript:fnAddUpdateUser();">Save</button>
                <button type="submit" class="btn btn-danger"
                  onclick="javascript:fnDeleteUser();">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Delete&nbsp;&nbsp;&nbsp;&nbsp;</button>
              </div>
            </div>

          </div><!-- /.box -->
        </div>
      </div>
    </section>
    <!-- Modal -->

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