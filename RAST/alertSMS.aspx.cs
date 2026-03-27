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

public partial class alertSMS : System.Web.UI.Page
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
            MySqlCommand cmd = new MySqlCommand("p_GetAlertSms", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("m_UserId", intUserId);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet dsData = new DataSet();
            sda.Fill(dsData);

            StringBuilder strTableData = new StringBuilder();

            strTableData.Append("<table id=\"tblAlertSMS\" class=\"table table-bordered table-striped\">\n");
            strTableData.Append("<thead><tr><th>Alert Detail</th><th>Site Name</th><th>Hub / Sensor Information</th><th>Alert Time</th><th>Alert Status log</th><th>Handle By</th></tr></thead>");
            strTableData.Append(" <tbody>");
            if (dsData.Tables[0].Rows.Count > 0)
            {
                    
                for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
                {
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["alertMessage"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["siteName"].ToString() + "</a></td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["floorName"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["dtDateTime"].ToString() + "</td>");
                    //strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["alertStatus"].ToString() + "</td>");
                    //strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["handleBy"].ToString() + "</td>");
                    strTableData.Append("<td><input type=\"text\" id=\"txtHandleBy_" + dsData.Tables[0].Rows[intRowCtr]["idalertSms"].ToString() + "\" class=\"text\" value=\"" + dsData.Tables[0].Rows[intRowCtr]["handleBy"].ToString() + "\" onchange=\"updAlertSMS1(" + dsData.Tables[0].Rows[intRowCtr]["idalertSms"].ToString() + ")\"></td>");
                    strTableData.Append("<td><input type=\"text\" id=\"txtalertStatus_" + dsData.Tables[0].Rows[intRowCtr]["idalertSms"].ToString() + "\" class=\"text\" value=\"" + dsData.Tables[0].Rows[intRowCtr]["alertStatus"].ToString() + "\" onchange=\"updAlertSMS2(" + dsData.Tables[0].Rows[intRowCtr]["idalertSms"].ToString() + ")\"></td>");


                    //strTableData.Append("<td><input type=\"button\" class=\"btn\" value=\"" + dsData.Tables[0].Rows[intRowCtr]["i_SensorId"].ToString() + "\" onclick=\"updBat(" + dsData.Tables[0].Rows[intRowCtr]["i_SensorId"].ToString() + ")\"></td>");
                    //strTableData.Append("<td><input type=\"button\" class=\"btn\" value=\"Update Staus\" onclick=\"updAlertSMS(" + dsData.Tables[0].Rows[intRowCtr]["idalertSms"].ToString() + ")\"></td>");

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