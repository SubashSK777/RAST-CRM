using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using RAST.DAL;
using RAST.Utilities;
using System.Data;
using System.Net.Mail;
using System.Net;

public partial class services_authentication : System.Web.UI.Page
{
    string strConnection = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString();

    //protected static string siteKey = "6LcoULUoAAAAAK88CLyuBWNZkJ-ta_CVZ3qYbetx";
    //protected static string secretKey = "6LcoULUoAAAAAEccdLE0Ve_BfeCJ9D6XjTj_LPsG";

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            string id = Request["id"].ToString();
            if (id == "1")
                fnPasswordRecovery();
            else if (id == "0")
                //fnAuthenticateLoginNew();
                fnAuthenticateLoginGoogle();
        }
        catch (Exception ex)
        {
            Response.Write("Error: " + ex.Message);
        }
    }

    public bool VerifyCaptcha(string response)
    {
        string apiUrl = "https://www.google.com/recaptcha/api/siteverify";
        //string data = $"secret={secretKey}&response={response}";
        string data = "secret=" + ConfigurationManager.AppSettings["recaptchaServerKey"].ToString() + "&response=" + response;

        using (var client = new WebClient())
        {
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            string responseString = client.UploadString(apiUrl, data);

            // Parse the JSON response
            dynamic jsonData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

            return jsonData.success;
        }
    }

    private void fnPasswordRecovery()
    {

        string strEmail = Request.QueryString["email"].ToString();
        string strCaptcha = Request.QueryString["captacharespone"].ToString();

        if (strCaptcha == null)
        {
            Response.Write(@"2");
            //conn.Close();
            return;
        }
        //string strKeyCaptcha = Request.QueryString["keycaptach"].ToString();
        //if (VerifyCaptcha(Request.Form["g-recaptcha-response"]))

        if (VerifyCaptcha(strCaptcha))
        {
            // reCAPTCHA passed, process the form

        }
        else
        {
            Response.Write(@"3");
            //conn.Close();
            return;
        }

        
        //string strEmail = Convert.ToString(Request.QueryString["email"]);
        string strSmtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
        string strFromMail = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
        string strPwdMail = ConfigurationManager.AppSettings["SMTPUserPassword"].ToString();
        string strPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();
        
        using(MySqlConnection conn = new MySqlConnection(strConnection))
        {
            conn.Open();
            DataSet ds = new DataSet();
            String result;
            MySqlCommand cmd = new MySqlCommand("p_GetRecoveryPassword", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("m_EmailId", strEmail);
            object recPassword = cmd.ExecuteScalar();
            Encryption objpwd = new Encryption();
            if (recPassword != null)
            {                
                result = recPassword.ToString();
                string dycryptPwd = objpwd.DecryptData(result);
                MailMessage mail = new MailMessage();
                SmtpClient smtpserver = new SmtpClient(strSmtpHost);
                mail.From = new MailAddress(strFromMail);  
                mail.To.Add(strEmail);
                mail.Subject = "RodentEye Password Recovery";
                mail.IsBodyHtml = true;
                mail.Body = "We've sent this message as you have requested password to access your RodentEye account.<br><br>To login use the below passoword <br><b>Password: " + dycryptPwd+
                    "</b><br><br>Regards,<br>Techspaneng Team";

                smtpserver.Port = Convert.ToInt16(strPort);
                smtpserver.Credentials = new System.Net.NetworkCredential(strFromMail, strPwdMail);
                smtpserver.EnableSsl = true;
                smtpserver.Send(mail);     
                
                Response.Write(@"1");
            }
            else
            {
                Response.Write(@"0");
            }           
          
        }

        
    }

    private void fnAuthenticate()
    {
        string strEmail = Convert.ToString(Request.QueryString["login"]);
        string strPassword = Convert.ToString(Request.QueryString["password"]);
        string strChkbox = Convert.ToString(Request.QueryString["chkbox"]);
      
        Encryption objEncryption = new Encryption();

        using (MySqlConnection conn = new MySqlConnection(strConnection))
        {
            conn.Open();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand("p_AuthenticateUser",conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("m_EmailId", strEmail);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            sda.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (objEncryption.EncryptData(strPassword) == ds.Tables[0].Rows[0]["s_Password"].ToString())
                {
                    if (strChkbox.Equals("1"))
                    {
                        Response.Cookies["Login"].Value = strEmail;
                        Response.Cookies["Password"].Value = strPassword;
                        Response.Cookies["Login"].Expires = DateTime.MaxValue;
                        Response.Cookies["Password"].Expires = DateTime.MaxValue;
                    }
                    else
                    {
                        Response.Cookies["Login"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                    }

                    Session["i_UloginId"] = ds.Tables[0].Rows[0]["i_UserId"].ToString();
                    Session["s_Email"] = strEmail;
                    Session["i_RoleId"] = ds.Tables[0].Rows[0]["i_RoleId"].ToString(); ;
                    Response.Write(@"1");  // Valid User
                  
                }
                else
                {
                    Response.Write(@"2");   // Invalid Password                   
                }

            }
            else
            {
                Response.Write(@"0");  // Invalid Email Id                
            }                         
            
            conn.Close();

        }

        objEncryption = null;  

    }

   
    private void fnAuthenticateLoginNew()
    {
        string strEmail = Request.QueryString["login"].ToString();
        string strPassword = Request.QueryString["password"].ToString();
        string strChkbox = Request.QueryString["chkbox"].ToString();
        string strCaptcha = Request.QueryString["strcaptcha"].ToString();
        string strKeyCaptcha = Request.QueryString["keycaptach"].ToString();

        EncryptionHelper objpwd = new EncryptionHelper();
        Encryption objEncryption = new Encryption();

        using (MySqlConnection conn = new MySqlConnection(strConnection))
        {
            conn.Open();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand("p_AuthenticateUser", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("m_EmailId", strEmail);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            sda.Fill(ds);

            if ((strCaptcha.ToString()).Length < 7)
            {
                Response.Write(@"4");   // Invalid Captcha 
            }
            else if(objpwd.DecryptData(strCaptcha.ToString())!= strKeyCaptcha)
            {
                Response.Write(@"3");   // Invalid Captcha   
            }
            else if (ds.Tables[0].Rows.Count > 0)
            {
                if (objEncryption.EncryptData(strPassword) == ds.Tables[0].Rows[0]["s_Password"].ToString())
                {
                    if (strChkbox.Equals("1"))
                    {
                        Response.Cookies["Login"].Value = strEmail;
                        Response.Cookies["Password"].Value = strPassword;
                        Response.Cookies["Login"].Expires = DateTime.MaxValue;
                        Response.Cookies["Password"].Expires = DateTime.MaxValue;
                    }
                    else
                    {
                        Response.Cookies["Login"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                    }

                    Session["i_UloginId"] = ds.Tables[0].Rows[0]["i_UserId"].ToString();
                    Session["s_Email"] = strEmail;
                    Session["i_RoleId"] = ds.Tables[0].Rows[0]["i_RoleId"].ToString(); ;

                    // --- Single Session Logic Start ---
                    string sessionKey = Guid.NewGuid().ToString();
                    Session["SessionKey"] = sessionKey;
                    string userId = ds.Tables[0].Rows[0]["i_UserId"].ToString();

                    using (MySqlCommand cmdSession = new MySqlCommand())
                    {
                        cmdSession.Connection = conn;
                        // Delete existing session
                        cmdSession.CommandText = "DELETE FROM user_sessions WHERE UserId = @uid";
                        cmdSession.Parameters.AddWithValue("@uid", userId);
                        cmdSession.ExecuteNonQuery();

                        // Insert new session
                        cmdSession.CommandText = "INSERT INTO user_sessions (UserId, SessionId) VALUES (@uid, @sid)";
                        cmdSession.Parameters.AddWithValue("@sid", sessionKey);
                        cmdSession.ExecuteNonQuery();
                    }
                    // --- Single Session Logic End ---

                    Response.Write(@"1");  // Valid User

                }
                else
                {
                    Response.Write(@"2");   // Invalid Password                   
                }

            }
            else
            {
                Response.Write(@"0");  // Invalid Email Id                
            }

            conn.Close();

        }

        objEncryption = null;

    }


    private void fnAuthenticateLoginGoogle()
    {
        string strEmail = Request["login"].ToString();
        string strPassword = Request["password"].ToString();
        string strChkbox = Request["chkbox"].ToString();
        string strCaptcha = Request["captacharespone"].ToString();
        //string strKeyCaptcha = Request["keycaptach"].ToString();

        EncryptionHelper objpwd = new EncryptionHelper();
        Encryption objEncryption = new Encryption();




        //if (VerifyCaptcha(Request.Form["g-recaptcha-response"]))

        if (VerifyCaptcha(strCaptcha))
        {
            // reCAPTCHA passed, process the form
            
        }
        else
        {
            Response.Write(@"3");
            //conn.Close();
            return;
        }


        using (MySqlConnection conn = new MySqlConnection(strConnection))
        {
            conn.Open();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand("p_AuthenticateUser", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("m_EmailId", strEmail);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            sda.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string sUserId = ds.Tables[0].Rows[0]["i_UserId"].ToString();
                int iUserId = Convert.ToInt32(sUserId);
                
                // --- SECURITY CHECK START ---
                int failedAttempts = 0;
                bool isLocked = false;
                
                // Check user_security status
                using (MySqlCommand cmdSec = new MySqlCommand("SELECT failed_attempts, is_locked FROM user_security WHERE user_id = @uid", conn))
                {
                    cmdSec.Parameters.AddWithValue("@uid", sUserId);
                    using (MySqlDataReader rdr = cmdSec.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            failedAttempts = rdr["failed_attempts"] != DBNull.Value ? Convert.ToInt32(rdr["failed_attempts"]) : 0;
                            isLocked = rdr["is_locked"] != DBNull.Value ? Convert.ToBoolean(rdr["is_locked"]) : false;
                        }
                        else
                        {
                            // Create default row if missing
                            rdr.Close();
                            using (MySqlCommand cmdIns = new MySqlCommand("INSERT INTO user_security (user_id, failed_attempts, is_locked, locked_at) VALUES (@uid, 0, 0, NULL)", conn))
                            {
                                cmdIns.Parameters.AddWithValue("@uid", sUserId);
                                cmdIns.ExecuteNonQuery();
                            }
                        }
                    }
                }

                if (isLocked)
                {
                    Response.Write(@"5"); // Account Locked
                    return;
                }
                // --- SECURITY CHECK END ---

                if (objEncryption.EncryptData(strPassword) == ds.Tables[0].Rows[0]["s_Password"].ToString())
                {
                    // --- SUCCESS: RESET LOCKS ---
                    using (MySqlCommand cmdReset = new MySqlCommand("UPDATE user_security SET failed_attempts = 0, is_locked = 0, locked_at = NULL WHERE user_id = @uid", conn))
                    {
                        cmdReset.Parameters.AddWithValue("@uid", sUserId);
                        cmdReset.ExecuteNonQuery();
                    }
                    // -----------------------------

                    if (strChkbox.Equals("1"))
                    {
                        Response.Cookies["Login"].Value = strEmail;
                        Response.Cookies["Password"].Value = strPassword;
                        Response.Cookies["Login"].Expires = DateTime.MaxValue;
                        Response.Cookies["Password"].Expires = DateTime.MaxValue;
                    }
                    else
                    {
                        Response.Cookies["Login"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                    }

                    Session["i_UloginId"] = ds.Tables[0].Rows[0]["i_UserId"].ToString();
                    Session["s_Email"] = strEmail;
                    Session["i_RoleId"] = ds.Tables[0].Rows[0]["i_RoleId"].ToString(); ;

                    // --- Single Session Logic Start ---
                    string sessionKey = Guid.NewGuid().ToString();
                    Session["SessionKey"] = sessionKey;
                    string userId = ds.Tables[0].Rows[0]["i_UserId"].ToString();

                    using (MySqlCommand cmdSession = new MySqlCommand())
                    {
                        cmdSession.Connection = conn;
                        // Delete existing session
                        cmdSession.CommandText = "DELETE FROM user_sessions WHERE UserId = @uid";
                        cmdSession.Parameters.AddWithValue("@uid", userId);
                        cmdSession.ExecuteNonQuery();

                        // Insert new session
                        cmdSession.CommandText = "INSERT INTO user_sessions (UserId, SessionId) VALUES (@uid, @sid)";
                        cmdSession.Parameters.AddWithValue("@sid", sessionKey);
                        cmdSession.ExecuteNonQuery();
                    }
                    // --- Single Session Logic End ---

                    Response.Write(@"1");  // Valid User

                }
                else
                {
                    // --- FAILURE: INCREMENT ATTEMPTS ---
                    failedAttempts++;
                    bool lockNow = failedAttempts >= 3;
                    
                    string updateSql = "UPDATE user_security SET failed_attempts = @fails";
                    if (lockNow) updateSql += ", is_locked = 1, locked_at = NOW()";
                    updateSql += " WHERE user_id = @uid";

                    using (MySqlCommand cmdFail = new MySqlCommand(updateSql, conn))
                    {
                        cmdFail.Parameters.AddWithValue("@fails", failedAttempts);
                        cmdFail.Parameters.AddWithValue("@uid", sUserId);
                        cmdFail.ExecuteNonQuery();
                    }

                    if (lockNow)
                        Response.Write(@"5"); // Locked NOW
                    else
                        Response.Write(@"2"); // Invalid Password                   
                }

            }
            else
            {
                Response.Write(@"0");  // Invalid Email Id                
            }

            conn.Close();

        }

        objEncryption = null;

    }

 
}