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

public partial class deployLog : System.Web.UI.Page
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
            MySqlCommand cmd = new MySqlCommand("p_GetDeployLog", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("m_UserId", intUserId);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet dsData = new DataSet();
            sda.Fill(dsData);
            StringBuilder strTableData = new StringBuilder();

            strTableData.Append("<table id=\"tblGetDeployLog\" class=\"table table-bordered table-striped\">\n");
            strTableData.Append("<thead><tr><th>Sensor Name</th><th>Hub Name</th><th>trigger Time</th></thead>");
            strTableData.Append(" <tbody>");
            if (dsData.Tables[0].Rows.Count > 0)
            {
                    
                for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
                {
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_SensorId"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_MobileNumber"].ToString() + "</a></td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["dt_TimeStamp"].ToString() + "</td>");
                    
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