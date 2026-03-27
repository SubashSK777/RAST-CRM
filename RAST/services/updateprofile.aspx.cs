using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RAST.DAL;
using MySql.Data.MySqlClient;
using System.Configuration;

public partial class services_Profiler : System.Web.UI.Page
{
    string strMysqlConnection = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        string exepurl = Request.Url.ToString();
        int returnVal;
        try
        {
           returnVal = Save();
           Response.Write(@returnVal);
        }
        catch (Exception ex)
        {
           
        }

    }

    protected int Save()
    {
        int intRetVal = 0;
        Encryption objEncryption = new Encryption();
        int intUserId = Convert.ToInt32(Request.QueryString["id"]);    
        string strpassword = Convert.ToString(Request.QueryString["pwd"]);
        string phone = Convert.ToString(Request.QueryString["phone"]);
      
        using (MySqlConnection MySQLCon = new MySqlConnection(strMysqlConnection))
        {           
            MySQLCon.Open();
            string enPassword = objEncryption.EncryptData(strpassword);
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = MySQLCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_UpdateProfile";
            cmd.Parameters.AddWithValue("m_UserId", intUserId);
            cmd.Parameters.AddWithValue("m_Password", enPassword);
            cmd.Parameters.AddWithValue("m_PhoneNumber", phone);           

            cmd.ExecuteNonQuery();
            intRetVal = 1;
            cmd = null; 
            MySQLCon.Close();
        }


        return intRetVal;
    }
}