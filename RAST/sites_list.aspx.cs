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
using System.Text;
public partial class sites_list : System.Web.UI.Page
{
    EncryptionHelper objpwd = new EncryptionHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        Session["s_FloorMaps"] = "";
        int intUserId = Convert.ToInt16(Session["i_UloginId"]);

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            Sites objSites = new Sites();
            objSites.ObjCon = conn;
            DataSet dsData = objSites.ReadDataset(intUserId,0, 0);
            string strTableData;
            StringBuilder siteDet = new StringBuilder();

            siteDet.Append("<table id=\"tblSiteList\" class=\"table table-bordered table-striped\">\n");
            siteDet.Append("<thead><tr><th>Organization</th><th>Site Name</th><th>No of Sensors on Site</th><th>Deployment Technician</th><th>Alert Technician</th><th>Status</th></tr></thead>");
            siteDet.Append("<tbody>");

            for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
            {
                siteDet.Append("<tr>");
                siteDet.Append("<td>" + dsData.Tables[0].Rows[intRowCtr][0].ToString() + "</td>");
                siteDet.Append("<td>" + "<a href='sites_ui.aspx?type=e&id=" + objpwd.EncryptData(Convert.ToString(dsData.Tables[0].Rows[intRowCtr][6])) + "'>" + dsData.Tables[0].Rows[intRowCtr][1].ToString() + "</a></td>");
                siteDet.Append("<td>" + dsData.Tables[0].Rows[intRowCtr][2].ToString() + "</td>");
                siteDet.Append("<td>" + dsData.Tables[0].Rows[intRowCtr][3].ToString() + "</td>");
                siteDet.Append("<td>" + dsData.Tables[0].Rows[intRowCtr][4].ToString() + "</td>");

                if (Convert.ToString(dsData.Tables[0].Rows[intRowCtr][9]) == "1")
                {
                    siteDet.Append("<td>Active</td>");
                }
                else
                {
                    siteDet.Append("<td>Deactive</td>");
                }
                siteDet.Append("</tr>");
            }

            siteDet.Append("</tbody></table>");

            strTableData = siteDet.ToString();
            dsData = null;
            objSites = null;


            spTable.InnerHtml = strTableData;

        }
    }
}