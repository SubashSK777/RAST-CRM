/*********************************************************************************************************************
Module Name: Organization List 
Module Description: This page lists down all the organizations to the users. 
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
using System.Text;

public partial class organization_list : System.Web.UI.Page
{
    EncryptionHelper objpwd = new EncryptionHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        String strConnection = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        int intUserId = Convert.ToInt16(Session["i_UloginId"]);
        int intRoleId = 0;
        using (MySqlConnection conn = new MySqlConnection(strConnection))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("p_GetUserRole", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("m_UserId", intUserId);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hidRoleId.Value = ds.Tables[0].Rows[0]["i_RoleId"].ToString();
                intRoleId = Convert.ToInt16(ds.Tables[0].Rows[0]["i_RoleId"].ToString());
            }
        }
        
        using (MySqlConnection conn = new MySqlConnection(strConnection))
        {
            conn.Open();
            Organization objOrganization = new Organization();
            objOrganization.ObjCon = conn;
            DataSet dsData = objOrganization.ReadDataset(intUserId,0, 0);
            string strTableData;
            StringBuilder orgDet = new StringBuilder();




            orgDet.Append("<table id=\"tblOrganization\" class=\"table table-bordered table-striped\">\n");
            orgDet.Append("<thead><tr><th>Organization Name</th><th>Contact Person</th><th>Email Address</th><th>Phone Number</th><th>Status</th></tr></thead>");
            orgDet.Append("<tbody>");

            for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
            {
                orgDet.Append("<tr>");
                if (intRoleId > 2)
                    orgDet.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_OrganizationName"].ToString() + "</a></td>");
                else
                    orgDet.Append("<td>" + "<a href='organization_ui.aspx?type=e&id=" + objpwd.EncryptData(Convert.ToString(dsData.Tables[0].Rows[intRowCtr]["i_OrganizationId"])) + "'>" + dsData.Tables[0].Rows[intRowCtr]["s_OrganizationName"].ToString() + "</a></td>");

                orgDet.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_ContactPerson"].ToString() + "</td>");
                orgDet.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_EmailAddress"].ToString() + "</td>");
                orgDet.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_PhoneNumber"].ToString() + "</td>");
               
                if (Convert.ToString(dsData.Tables[0].Rows[intRowCtr]["b_Status"]) == "1")
                {
                    orgDet.Append("<td>Active</td>");
                }
                else
                {
                    orgDet.Append("<td>Deactive</td>");
                }

                orgDet.Append("</tr>");
            }

            orgDet.Append("</tbody></table>");

            strTableData = orgDet.ToString();
            dsData = null;
            objOrganization = null;

            spTable.InnerHtml = strTableData;


        }
    }
}