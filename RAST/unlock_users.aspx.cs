using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

public partial class unlock_users : System.Web.UI.Page
{
    string strConnection = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        if (!IsPostBack)
        {
            // RBAC Check: Only SuperAdmin (1) or Administrator (2)
            string roleId = Convert.ToString(Session["i_RoleId"]);
            if (roleId != "1" && roleId != "2")
            {
                Response.Redirect("map.aspx"); // Access Denied
            }

            BindGrid();
        }
    }

    private void BindGrid()
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(strConnection))
            {
                conn.Open();
                string query = @"SELECT u.i_UserId, u.s_Name, u.s_Email, us.failed_attempts, us.locked_at 
                               FROM Users u 
                               INNER JOIN user_security us ON u.i_UserId = us.user_id 
                               WHERE us.is_locked = 1";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                gvLockedUsers.DataSource = dt;
                gvLockedUsers.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error loading data: " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void gvLockedUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "UnlockUser")
        {
            int userId = Convert.ToInt32(e.CommandArgument);
            UnlockUser(userId);
        }
    }

    private void UnlockUser(int userId)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(strConnection))
            {
                conn.Open();
                string query = "UPDATE user_security SET is_locked = 0, failed_attempts = 0, locked_at = NULL WHERE user_id = @uid";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@uid", userId);
                cmd.ExecuteNonQuery();

                lblMessage.Text = "User unlocked successfully at " + DateTime.Now.ToString("HH:mm:ss");
                lblMessage.ForeColor = System.Drawing.Color.Green;
                
                BindGrid(); // Refresh grid
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error unlocking user: " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
}
