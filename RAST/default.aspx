<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>





    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>RODENT Eye Login</title>
        <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
        <!-- Bootstrap 3.3.2 
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />-->'

        <!-- Font Awesome Icons -->
        <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet"
            type="text/css" />
        <!-- Theme style -->
        <link href="dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
        <!-- iCheck -->
        <link href="plugins/iCheck/square/blue.css" rel="stylesheet" type="text/css" />

        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->

        <script src="scripts/scripts.js"></script>

    </head>


    <style>
        .center-screen {
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
        }
    </style>

    <body>




        <div class="center-screen">


            <div class="card card-info">

                <div class="login-logo">
                    <a href="../../index2.html"><b>RodentEye</b>&#8482</a>
                </div><!-- /.login-logo -->
                <div class="card-header">
                    <h3 class="card-title">Login</h3>
                </div>

                <div id="divMsg"></div>
                <form id="login">
                    <div class="card-body">

                        <div class="form-group row">

                            <label for="inputEmail3" class="col-sm-12 col-form-label">Email</label>
                            <div class="col-sm-12">
                                <span id="spEmail"></span>
                                <input type="email" class="form-control" id="txtEmail" name="txtEmail"
                                    placeholder="Email" />
                            </div>
                        </div>

                        <div class="form-group row">
                            <label for="inputPassword3" class="col-sm-12 col-form-label">Password</label>
                            <div class="col-sm-12">
                                <span id="spPassword"></span>
                                <input type="password" class="form-control" id="txtPassword" name="txtPassword"
                                    placeholder="Password" />
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-sm-12">
                                <span id="spCaptchaText"></span>

                            </div>

                        </div>

                        <!-- Old Captch
              <div class="form-group row">
                    <label for="inputCaptchaText1" class="col-sm-12 col-form-label">Enter Code :</label>
                    <div class="col-sm-12">
                         <span id="spCaptchaText" ></span>
                        
                            <input type="text" id="txtCode" class="form-control" name="txtCode"/>
                        
                    </div>
                  </div>

              <div class="form-group row">
                    <label for="inputCaptcha1" class="col-sm-12 col-form-label">Captcha</label>
                    <div class="col-sm-12">
                         <span id="spCaptcha"></span>
                        <asp:Placeholder runat="server" id="ph"/>
                        <form id="frm1" runat="server">
                        
                     <asp:Image ImageUrl="ghCaptcha.ashx" runat="server" ID="imgCaptcha" />
                    <asp:HiddenField id="hidCaptcha" runat="server" />
                    </form>
                    </div>
                  </div> -->


                        <div class="form-group row">
                            <div class="col-sm-12">
                                <div class="form-check">
                                    <input type="checkbox" class="form-check-input" id="chkRemember" />
                                    <label class="form-check-label" for="exampleCheck2">Remember me</label>
                                </div>
                            </div>
                        </div>

                        </br>

                        <div class="form-group row">
                            <div class="col-sm-12">
                                <script src="https://www.google.com/recaptcha/api.js" async defer></script>
                                <div class="g-recaptcha" id="grecaptcha"
                                    data-sitekey=<%=ConfigurationManager.AppSettings["recaptchaSiteKey"]%>></div>
                            </div>
                        </div>


                    </div>




                    <div class="form-group row">
                        <div class="offset-sm-3 col-sm-9">
                            <!--<button type="submit" class="btn btn-info" onclick="javascript:fnAuthenticateLogin()">Sign in</button>-->


                            <!--  <button type="submit" class="btn btn-info" onclick="javascript:fnAuthenticateLoginNew()">Sign in</button>-->

                            <!--<button type="submit" class="btn btn-info" onclick="javascript:fnAuthenticateLoginGoogle()">Sign in</button>-->
                            <button type="button" class="btn btn-info">Cancel</button>
                            <button type="submit" id="submit" class="btn btn-info">Sign In</button>
                        </div>
                    </div>
                    <!-- /.card-footer -->
                </form>


                <div class="card-footer">
                    <a href="forgot_password.aspx">Forgot Password?</a></br>
                </div>

            </div><!-- /.login-box-body -->
            <br />



            <div align="center"><span id="spRecoveryMsg"></span></div>


            <div class="login-box" id="idpr">
                <div align="right"> <a href="#" onclick="javascript:$('#idpr').hide();">close</a> </div>
                <div class="login-box-body"><b>PASSWORD RECOVERY</b>
                    <p>To retrive your password, enter the email address you use to login into SNDA</p>


                    <div class="form-group has-feedback">
                        <span id="spRecEmail"></span>
                        <input type="text" class="form-control" placeholder="Enter valid email address" id="txtRecEmail"
                            name="txtEmail" />

                    </div>

                    <div align="center">
                        <button type="submit" class="btn btn-primary btn-block btn-flat"
                            onclick="javascript:fnSubmitRecovery()">Submit</button>

                    </div><!-- /.col -->


                </div>


                <div style="text-align:center"> &copy; Pestech Holding (S) Pte Ltd </div>
            </div><!-- /.login-box -->


        </div>



        <!-- jQuery 2.1.3 
    <script src="../../plugins/jQuery/jQuery-2.1.3.min.js"></script>-->
        <script src="../../plugins/jquery/jquery.min.js"></script>
        <!-- Bootstrap 3.3.2 JS
    <script src="../../bootstrap/js/bootstrap.min.js" type="text/javascript"></script> -->
        <script src="/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
        <!-- iCheck -->
        <script src="../../plugins/iCheck/icheck.min.js" type="text/javascript"></script>
        <script type="text/javascript" src="scripts/scripts.js"></script>
        <!-- AdminLTE App 
    <script src="../../dist/js/adminlte.min.js"></script>-->


        <script>
            $(function () {
                $('input').iCheck({
                    checkboxClass: 'icheckbox_square-blue',
                    radioClass: 'iradio_square-blue',
                    increaseArea: '20%' // optional
                });
            });


        </script>
    </body>

    </html>
    <script>

        $(document).ready(function () {
            $('#idpr').hide();
            document.getElementById('spRecoveryMsg').innerHTML = "";

            var email = getCookie("Login");
            var password = getCookie("Password");

            if (email != "") {
                document.getElementById("txtEmail").value = email;
                document.getElementById("chkRemember").checked = true;
            }

            if (password != "") {
                document.getElementById("txtPassword").value = password;
            }

        });



        function fnSubmitRecovery() {

            if (CheckEmpty(document.getElementById("txtRecEmail"), document.getElementById("spRecEmail"), "  Error - Email address cannot be empty") == false) {
                return false;
            }
            if (checkEmail(document.getElementById("txtRecEmail").value) == false) {
                document.getElementById('spRecEmail').innerHTML = "<font color='red'> Error - Invalid Email address</font>";
                return false;
            }

            document.getElementById("spRecEmail").innerHTML = "<font color='blue'>Processing. Please wait....</font>";

            var response = grecaptcha.getResponse();


            $.ajax({
                type: 'GET',
                url: '/services/authentication.aspx?id=1&email=' + document.getElementById("txtRecEmail").value + '&captacharespone=' + response,
                success: function (data) {
                    if (data == 1) {
                        $('#idpr').hide();
                        document.getElementById('spRecoveryMsg').innerHTML = "<font color='blue'> We'hve sent password to your " + document.getElementById("txtRecEmail").value + " email address.</font>"
                    }
                    else {
                        grecaptcha.reset();
                        if (data == 2) {
                            $('#idpr').hide();
                            document.getElementById('spRecoveryMsg').innerHTML = "<font color='blue'>Invalid Captcha</font>"
                        }
                        else if (data == 3) {
                            $('#idpr').hide();
                            document.getElementById('spRecoveryMsg').innerHTML = "<font color='red'> Invalid Captcha</font>"
                        }
                        else
                            document.getElementById('spRecEmail').innerHTML = "<font color='red'>-- No account found with this email address.</font>"
                    }
                }
            });

        }

        function fnForgotPassword() {
            $('#idpr').show();
            document.getElementById('spRecoveryMsg').innerHTML = "";
            document.getElementById('spRecEmail').innerHTML = "";
            document.getElementById("txtRecEmail").value = "";
            document.getElementById("spCaptchaText").value = "";
        }

        $('#login').on('submit', (function (e) {
            e.preventDefault();
            fnAuthenticateLoginNew();
        }));

        //$(function () {
        //if (localStorage.chkbx && localStorage.chkbx != '') {
        //    $('#remember').attr('checked', 'checked');
        //    $('#email').val(localStorage.usrname);
        //    $('#password').val(localStorage.pass);
        //}
        //else {
        //    $('#remember').removeAttr('checked');
        //    $('#email').val('');
        //    $('#password').val('');
        //}

        //$('#remember').click(function () {
        //    if ($('#remember').is(':checked')) {
        //        localStorage.usrname = $('#email').val();
        //        localStorage.pass = $('#password').val();
        //        localStorage.chkbx = $('#remember').val();
        //    }
        //    else {
        //        localStorage.usrname = '';
        //        localStorage.pass = '';
        //        localStorage.chkbx = '';
        //    }
        //});
        //});

        function fnAuthenticateLoginNew() {

            //var sessionvar = '@Request.RequestContext.HttpContext.Session["Captcha"]'.value;

            var login = document.getElementById("txtEmail").value;
            var password = document.getElementById("txtPassword").value;

            //var sessionvar = document.getElementById("hidCaptcha").value;
            //var vartxtCode = document.getElementById("txtCode").value;
            //document.getElementById('spCaptchaText').innerHTML = "<font color='red'> Error - Invalid Captcha" + sessionvar +"</font>";
            //alert(sessionvar);
            //if (sessionvar!=document.getElementById("txtCode").value) {
            //    document.getElementById('spCaptchaText').innerHTML = "<font color='red'> Error - Invalid Captcha</font>";
            //    return false;
            //}



            if (CheckEmpty(document.getElementById("txtEmail"), document.getElementById("spEmail"), "  Error - Email address cannot be empty") == false) {
                return false;
            }
            if (checkEmail(document.getElementById("txtEmail").value) == false) {
                document.getElementById('spEmail').innerHTML = "<font color='red'> Error - Invalid Email address</font>";
                return false;
            }
            if (CheckEmpty(document.getElementById("txtPassword"), document.getElementById("spPassword"), "  Error - Password cannot be empty") == false) {
                return false;
            }

            var chkStatus = 0;
            if ($('#chkRemember').is(':checked')) {
                chkStatus = 1;
            }

            if (checkEmail(document.getElementById("txtEmail").value) == false) {
                document.getElementById('spRecEmail').innerHTML = "<font color='red'> Error - Invalid Email address</font>";
                return false;
            }


            document.getElementById("spRecEmail").innerHTML = "<font color='blue'>Processing. Please wait....</font>";

            var response = grecaptcha.getResponse();

            $.ajax({
                type: 'POST',
                url: '/services/authentication.aspx',
                data: {
                    id: 0,
                    login: document.getElementById("txtEmail").value,
                    password: document.getElementById("txtPassword").value,
                    chkbox: 0,
                    captacharespone: response
                },
                success: function (data) {
                    if (data == 1) {
                        location.href = "Map.aspx";
                    }
                    else {
                        grecaptcha.reset();

                        if (data == 4) {
                            document.getElementById('spCaptchaText').innerHTML = "<font color='red'> Invalid access to the Site</font>";
                        }
                        else if (data == 3) {
                            document.getElementById('spCaptchaText').innerHTML = "<font color='red'> Error - Invalid Captcha</font>";
                        }
                        else if (data == 2) {
                            document.getElementById('spCaptchaText').innerHTML = "<font color='red'><b>Invalid Password</b></font>";
                        }
                        else if (data == 5) {
                            document.getElementById('spCaptchaText').innerHTML = "<font color='red'><b>Account Locked. Contact Administrator.</b></font>";
                        }
                        else if (data == 0) {
                            document.getElementById('spEmail').innerHTML = "<font color='red'><b>INVALID EMAIL ID</b></font>";
                        }
                        else {
                            document.getElementById('spEmail').innerHTML = "<font color='red'><b>Login Invalid</b></font>";
                        }
                    }
                }
            });
        }


    </script>