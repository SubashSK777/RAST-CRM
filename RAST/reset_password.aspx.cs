using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using RAST.Utilities; // Assuming Encryption class is here or global

public partial class reset_password : System.Web.UI.Page
{
    string strConnection = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString();
    string token = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        token = Request.QueryString["token"];
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(token))
            {
                pnlReset.Visible = false;
                lblMessage.Text = "Invalid token.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (!ValidateToken(token))
            {
                pnlReset.Visible = false;
                lblMessage.Text = "Invalid or expired token.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    private bool ValidateToken(string t)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(strConnection))
            {
                conn.Open();
                string query = "SELECT expires_at, used FROM password_reset_tokens WHERE token = @Token";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Token", t);
                
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DateTime expiry = Convert.ToDateTime(reader["expires_at"]);
                        int used = Convert.ToInt32(reader["used"]);

                        if (used == 0 && expiry > DateTime.Now)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log error
        }
        return false;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        string newPwd = txtNewPassword.Text;
        string confirmPwd = txtConfirmPassword.Text;

        if (string.IsNullOrEmpty(newPwd) || string.IsNullOrEmpty(confirmPwd))
        {
            lblMessage.Text = "Passwords cannot be empty.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }

        if (newPwd != confirmPwd)
        {
            lblMessage.Text = "Passwords do not match.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }

        // Hash Password
        Encryption objEncryption = new Encryption();
        string hashedPwd = objEncryption.EncryptData(newPwd);

        try
        {
            using (MySqlConnection conn = new MySqlConnection(strConnection))
            {
                conn.Open();
                
                // Get User ID from Token
                string queryUser = "SELECT user_id FROM password_reset_tokens WHERE token = @Token";
                MySqlCommand cmdUser = new MySqlCommand(queryUser, conn);
                cmdUser.Parameters.AddWithValue("@Token", token);
                object result = cmdUser.ExecuteScalar();

                if (result != null)
                {
                    int userId = Convert.ToInt32(result);

                    // Update Password
                    string updatePwd = "UPDATE Users SET s_Password = @Pwd WHERE i_UserId = @Uid";
                    MySqlCommand cmdUpdate = new MySqlCommand(updatePwd, conn);
                    cmdUpdate.Parameters.AddWithValue("@Pwd", hashedPwd);
                    cmdUpdate.Parameters.AddWithValue("@Uid", userId);
                    cmdUpdate.ExecuteNonQuery();

                    // Mark Token Used
                    string updateToken = "UPDATE password_reset_tokens SET used = 1 WHERE token = @Token";
                    MySqlCommand cmdToken = new MySqlCommand(updateToken, conn);
                    cmdToken.Parameters.AddWithValue("@Token", token);
                    cmdToken.ExecuteNonQuery();

                    pnlReset.Visible = false;
                    lblMessage.Text = "Password reset successfully. You can now login.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = "Invalid request.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error resetting password: " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
}
