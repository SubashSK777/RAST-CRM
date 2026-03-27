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

public partial class noTriggerGateway : System.Web.UI.Page
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
            MySqlCommand cmd = new MySqlCommand("p_GetNoTriggerGateway_New", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("m_UserId", intUserId);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet dsData = new DataSet();
            sda.Fill(dsData);

            StringBuilder strTableData = new StringBuilder();

            strTableData.Append("<table id=\"tblNoTriggerGateway\" class=\"table table-bordered table-striped\">\n");
            strTableData.Append("<thead><tr><th>No Response X Days</th><th>Last Trigger Date</th><th>Site Name</th><th>Service Location</th><th>HUB ID</th><th>HUB Number</th><th>Send Status Check</th></tr></thead>");
            strTableData.Append(" <tbody>");

            String t_secretCode = System.Web.Configuration.WebConfigurationManager.AppSettings["SecretCode"].ToString();

            if (dsData.Tables[0].Rows.Count > 0)
            {
                    
                for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
                {
                    String tPhone = Convert.ToString(dsData.Tables[0].Rows[intRowCtr]["s_HubPhoneNumber"].ToString());
                    if (tPhone.StartsWith("XR"))
                    {
                        tPhone = "0";
                    }
                    

                    //Ragu Battery StandbyTime Update
                    DateTime installation_date = Convert.ToDateTime(dsData.Tables[0].Rows[intRowCtr]["hubLastTrigger"].ToString());
                    DateTime todayDate = DateTime.Now;
                    double diff = (todayDate - installation_date).Days;

                 
                    strTableData.Append("<tr>");

                    strTableData.Append("<td>" + diff + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["hubLastTrigger"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_SiteName"].ToString() + "</a></td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_Address"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_HubId"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_HubPhoneNumber"].ToString() + "</td>");
                    
                    //strTableData.Append("<td><input type=\"button\" class=\"btn\" value=\"" + dsData.Tables[0].Rows[intRowCtr]["i_SensorId"].ToString() + "\" onclick=\"updBat(" + dsData.Tables[0].Rows[intRowCtr]["i_SensorId"].ToString() + ")\"></td>");
                    strTableData.Append("<td><input type=\"button\" class=\"btn btn-success btn-sm m-1\" value=\"Send Status Check\" onclick=\"sendStatusCode('" + tPhone +"','"+ t_secretCode+"7#')\"></td>");

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