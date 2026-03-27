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

public partial class services_configuration : System.Web.UI.Page
{
    string strConnection = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        int intRequestId = Convert.ToInt32(Request.QueryString["id"]);

        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        switch (intRequestId)
        {
            case 1:
                fnResetCounter();
                break;
            case 2:
                fnSaveConfiguration();
                break;
        }


    }

    private void fnResetCounter()
    {
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            RAST.DAL.Configuration objConfiguration = new RAST.DAL.Configuration();
            objConfiguration.ObjCon = conn;
            objConfiguration.ResetCounter();

            conn.Close();
        
        }
    }

    private void fnSaveConfiguration()
    {
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            int intMaxDays = Convert.ToInt32(Request.QueryString["period"]);
            conn.Open();
            RAST.DAL.Configuration objConfiguration = new RAST.DAL.Configuration();
            objConfiguration.ObjCon = conn;
            objConfiguration.MaxDataStorage = intMaxDays;
            objConfiguration.SaveConfiguration();

            conn.Close();

        }
    }
}