/*********************************************************************************************************************
Module Name: Configuration 
Module Description: This page provides the configuration option to define the maximum storage period for the data
**********************************************************************************************************************
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using RAST.DAL;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

public partial class housekeeping : System.Web.UI.Page
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
            RAST.DAL.Configuration objConfiguration = new RAST.DAL.Configuration();
            objConfiguration.ObjCon = conn;
            DataSet dsData = objConfiguration.ReadDataset(intUserId, 0, 0);

            if(dsData.Tables[0].Rows.Count > 0)
            {
                txtMaxStoragePeriod.Value = Convert.ToString(dsData.Tables[0].Rows[0][0]);

            }
            else
            {
                txtMaxStoragePeriod.Value = "0";
            }

          

            dsData = null;
            conn.Close();
           
        }
   
    }
}