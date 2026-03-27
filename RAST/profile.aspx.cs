using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using RAST.DAL;
using System.Data;

public partial class profile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        int intUserId = Convert.ToInt16(Session["i_UloginId"]);
        
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            Users objUser = new Users();
            objUser.conn = conn;
            if (intUserId == 0)
                intUserId = Convert.ToInt16(Session["i_UloginId"]);

            objUser.UserId = intUserId;           
            DataSet ds =  objUser.ReadElementsForUI(intUserId);
            DataTable tblUser = ds.Tables[0];
            txtPhone.Value = tblUser.Rows[0]["s_PhoneNumber"].ToString();

            hidUserDetails.Value = Convert.ToString(intUserId) + "$" + tblUser.Rows[0]["s_Name"].ToString() + "$" + tblUser.Rows[0]["s_Email"].ToString();
          
        }
    }
   
}