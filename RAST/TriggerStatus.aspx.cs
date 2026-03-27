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

public partial class TriggerStatus : System.Web.UI.Page
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
            MySqlCommand cmd = new MySqlCommand("p_GetTriggerStatus", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("m_UserId", intUserId);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet dsData = new DataSet();
            sda.Fill(dsData);


            StringBuilder strTableData = new StringBuilder();

            strTableData.Append("<table id=\"tblTriggerStatus\" class=\"table table-bordered table-striped\">\n");
            strTableData.Append("<thead><tr><th>No. Of. Trigger</th><th>No. Of. SMS</th><th>Site Name</th><th>Site Address</th><th>Floor Plan Name</th><th>Hub ID</th><th>HUB Number</th><th>Sensor ID</th><th>Sensor Location</th></tr></thead>");
            strTableData.Append(" <tbody>");
            if (dsData.Tables[0].Rows.Count > 0)
            {
                for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
                {
                    MySqlCommand cmd1 = new MySqlCommand("p_GetSmsCount", conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("m_MobileNumber", dsData.Tables[0].Rows[intRowCtr]["s_HubPhoneNumber"].ToString());
                    cmd1.Parameters.AddWithValue("m_Message", "Zone " + dsData.Tables[0].Rows[intRowCtr]["s_SensorId"].ToString());
                    MySqlDataAdapter sda1 = new MySqlDataAdapter(cmd1);
                    DataSet dsData1 = new DataSet();
                    sda1.Fill(dsData1);

                    /*strTableData.Append("<td  style=visibility: hidden>" + intRowCtr + "</td>");*/
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["CountOfi_SensorId"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData1.Tables[0].Rows[0]["NoOfSMS"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_SiteName"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_Address"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_FloorMapImageName"].ToString() + "</a></td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_HubId"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_HubPhoneNumber"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_SensorId"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_SensorLocation"].ToString() + "</td>");

                    

                    strTableData.Append("</tr>");
                    dsData1 = null;
                }
            }
            strTableData.Append(" </tbody></table>");
            String strTableData1;
            strTableData1 = strTableData.ToString();
            dsData = null;

            spTable.InnerHtml = strTableData1;
        }
    }
    
}