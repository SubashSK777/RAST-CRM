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

public partial class SiteAlertReport : System.Web.UI.Page
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
                Organization objOrganization = new Organization();
                objOrganization.ObjCon = conn;
                DataSet dsOrganization = objOrganization.ReadDataset(intUserId, 0, 0);
                objOrganization = null;

                List<ListItem> itemsOrganization = new List<ListItem>();

                for (int intRowCtr = 0; intRowCtr < dsOrganization.Tables[0].Rows.Count; intRowCtr++)
                {
                    itemsOrganization.Add(new ListItem(dsOrganization.Tables[0].Rows[intRowCtr]["s_OrganizationName"].ToString(), dsOrganization.Tables[0].Rows[intRowCtr]["i_OrganizationId"].ToString()));
                }

                cmbOrganizationName.Items.AddRange(itemsOrganization.ToArray());
                conn.Close();
                dsOrganization = null;
            }
        }

    }
}