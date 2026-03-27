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

public partial class HubStaus : System.Web.UI.Page
{
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
            MySqlCommand cmd = new MySqlCommand("p_GetHubStatus", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("m_UserId", intUserId);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet dsData = new DataSet();
            sda.Fill(dsData);
            string strTableData = "<table id=\"tblHubStatus\" class=\"table table-bordered table-striped\">\n";
            strTableData = strTableData + "<thead><tr><th>No. Of. Hub. Trigger</th><th>Site Name</th><th>Site Address</th><th>Floor Plan Name</th><th>Hub ID</th><th>HUB Number</th></tr></thead>";
            strTableData = strTableData + " <tbody>";
            if (dsData.Tables[0].Rows.Count > 0)
            {
                for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
                {
                    strTableData = strTableData + "<td  style=visibility: hidden>" + intRowCtr + "</td>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr]["CountOfi_SensorId"].ToString() + "</td>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr]["s_SiteName"].ToString() + "</td>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr]["s_Address"].ToString() + "</td>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr]["s_FloorMapImageName"].ToString() + "</a></td>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr]["s_HubId"].ToString() + "</td>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr]["s_HubPhoneNumber"].ToString() + "</td>";
                    strTableData = strTableData + "</tr>";
                }
            }
            strTableData = strTableData + " </tbody></table>";
           
            dsData = null;
            
            spTable.InnerHtml = strTableData;


        }
    }
}