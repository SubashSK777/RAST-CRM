using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using RAST.DAL;
using System.Data;
using System.IO;

public partial class SensorLogReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (!Page.IsPostBack)
        {
            int intUserId = Convert.ToInt16(Session["i_UloginId"]);
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
            {
                conn.Open();
                Sites objSites = new Sites();
                objSites.ObjCon = conn;
                DataSet dsData = objSites.ReadDataset(intUserId, 0, 0);

                dsData.Tables[0].DefaultView.RowFilter = "b_Status = '1'";

                cmbSiteName.DataSource = dsData.Tables[0];
                cmbSiteName.DataValueField = "i_SiteId";
                cmbSiteName.DataTextField = "s_SiteName";
                cmbSiteName.DataBind();

                cmbSiteName.Items.Insert(0, new ListItem("-- Select Site --", "0"));

                cmbSiteName.SelectedIndex = 0;

                dsData = null;
                objSites = null;
            }

        }
    }
}