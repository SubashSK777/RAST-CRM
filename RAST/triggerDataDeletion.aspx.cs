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
using System.Globalization;


public partial class triggerDataDeletion : System.Web.UI.Page
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
            MySqlCommand cmd = new MySqlCommand("p_GetSensorInfo", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("m_UserId", intUserId);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet dsData = new DataSet();
            sda.Fill(dsData);

            StringBuilder strTableData = new StringBuilder();

            strTableData.Append("<table id=\"tbltriggerDataDeletion\" class=\"table table-bordered table-striped\">\n");
            strTableData.Append("<thead><tr><th>Site Name</th><th>Floor Plan Name</th><th>HUB Number</th><th>Sensor Location</th><th>Date Selection</th><th>Time Selection</th><th>Delete ?</th></tr></thead>");
            strTableData.Append(" <tbody>");
            string curDate = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            string curTime = DateTime.Now.ToString("HH");

            if (dsData.Tables[0].Rows.Count > 0)
            {
                    
                for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
                {
                   
                    strTableData.Append("<tr>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_SiteName"].ToString() + "</a></td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_FloorMapImageName"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_HubPhoneNumber"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_SensorLocation"].ToString() + "</td>");

                    strTableData.Append("<td><input type=\"text\" id=\"txtDate_" + dsData.Tables[0].Rows[intRowCtr]["i_SensorId"].ToString() + "\" class=\"text\" value=\""+ curDate + "\"></td>");
                    strTableData.Append("<td><input type=\"text\" id=\"txtTime_" + dsData.Tables[0].Rows[intRowCtr]["i_SensorId"].ToString() + "\" class=\"text\" value=\"" + curTime + "\"></td>");

                    strTableData.Append("<td><input type=\"button\" class=\"btn\" value=\"Delete Data\" onclick=\"delSensorData(" + dsData.Tables[0].Rows[intRowCtr]["i_SensorId"].ToString() + ")\"></td>");

                    strTableData.Append("</tr>");
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