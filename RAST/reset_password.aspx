<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reset_password.aspx.cs" Inherits="reset_password" %>

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>RodentEye - Reset Password</title>
        <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
        <!-- Font Awesome Icons -->
        <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet"
            type="text/css" />
        <!-- Theme style -->
        <link href="dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
        <!-- iCheck -->
        <link href="plugins/iCheck/square/blue.css" rel="stylesheet" type="text/css" />

        <style>
            .center-screen {
                position: fixed;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                width: 360px;
            }
        </style>
    </head>

    <body class="hold-transition login-page">
        <div class="center-screen">
            <div class="card card-info">
                <div class="login-logo">
                    <a href="#"><b>RodentEye</b>&#8482</a>
                </div>
                <div class="card-header">
                    <h3 class="card-title">Reset Password</h3>
                </div>

                <form id="form1" runat="server">
                    <div class="card-body">
                        <p class="login-box-msg">Enter your new password.</p>

                        <asp:Panel ID="pnlReset" runat="server">
                            <div class="form-group">
                                <label>New Password</label>
                                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control"
                                    TextMode="Password" placeholder="New Password"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Confirm Password</label>
                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control"
                                    TextMode="Password" placeholder="Confirm Password"></asp:TextBox>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <asp:Button ID="btnReset" runat="server" Text="Reset Password"
                                        CssClass="btn btn-primary btn-block" OnClick="btnReset_Click" />
                                </div>
                            </div>
                        </asp:Panel>

                        <br />
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </div>

                    <div class="card-footer">
                        <a href="default.aspx">Back to Login</a>
                    </div>
                </form>
            </div>
        </div>

        <!-- jQuery -->
        <script src="../../plugins/jquery/jquery.min.js"></script>
        <!-- Bootstrap -->
        <script src="/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
    </body>

    </html>