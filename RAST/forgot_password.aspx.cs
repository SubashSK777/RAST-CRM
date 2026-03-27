using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Net.Mail;
using System.Net;

public partial class forgot_password : System.Web.UI.Page
{
    string strConnection = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text.Trim();
        if (string.IsNullOrEmpty(email))
        {
            lblMessage.Text = "Please enter your email.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }

        try
        {
            using (MySqlConnection conn = new MySqlConnection(strConnection))
            {
                conn.Open();
                // Check if user exists
                // Use i_UserId and s_Email based on typical schema seen in authentication.aspx.cs
                string queryUser = "SELECT i_UserId FROM Users WHERE s_Email = @Email";
                MySqlCommand cmdUser = new MySqlCommand(queryUser, conn);
                cmdUser.Parameters.AddWithValue("@Email", email);
                object result = cmdUser.ExecuteScalar();

                if (result != null)
                {
                    int userId = Convert.ToInt32(result);
                    string token = Guid.NewGuid().ToString();
                    DateTime expiry = DateTime.Now.AddMinutes(30);

                    // Insert token
                    string insertToken = "INSERT INTO password_reset_tokens (user_id, token, expires_at, used) VALUES (@UserId, @Token, @Expiry, 0)";
                    MySqlCommand cmdToken = new MySqlCommand(insertToken, conn);
                    cmdToken.Parameters.AddWithValue("@UserId", userId);
                    cmdToken.Parameters.AddWithValue("@Token", token);
                    cmdToken.Parameters.AddWithValue("@Expiry", expiry);
                    cmdToken.ExecuteNonQuery();

                    // Send Email
                    SendResetEmail(email, token);
                }

                // Always show success message for security
                lblMessage.Text = "If an account exists with this email, a reset link has been sent.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
        }
        catch (Exception ex)
        {
            // Log error in production, show generic here
            lblMessage.Text = "An error occurred. Please try again later. " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void SendResetEmail(string toEmail, string token)
    {
        string strSmtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
        string strFromMail = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
        string strPwdMail = ConfigurationManager.AppSettings["SMTPUserPassword"].ToString();
        string strPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpserver = new SmtpClient(strSmtpHost);
            mail.From = new MailAddress(strFromMail);
            mail.To.Add(toEmail);
            mail.Subject = "Password Reset Requested";
            mail.IsBodyHtml = true;
            
            string resetLink = Request.Url.GetLeftPart(UriPartial.Authority) + "/reset_password.aspx?token=" + token;
            
            mail.Body = "You have requested to reset your password. Click the link below to reset it:<br/><br/>" +
                        "<a href='" + resetLink + "'>" + resetLink + "</a><br/><br/>" +
                        "This link expires in 30 minutes.<br/><br/>" +
                        "If you did not request this, please ignore this email.";

            smtpserver.Port = Convert.ToInt32(strPort);
            smtpserver.Credentials = new NetworkCredential(strFromMail, strPwdMail);
            smtpserver.EnableSsl = true;
            smtpserver.Send(mail);
        }
        catch (Exception ex)
        {
            // Log email failure
            // throw ex; // Or handle silently
        }
    }
}
