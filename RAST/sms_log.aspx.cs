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
using System.Data.SqlClient;
public partial class sms_log : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        
        int intUserId = Convert.ToInt16(Session["i_UloginId"]);
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            Smslog objSMSLog = new Smslog();
            objSMSLog.ObjCon = conn;
            objSMSLog.StartDate = DateTime.Now.AddMonths(-1);
            objSMSLog.EndDate = DateTime.Now;
            DataSet dsData = objSMSLog.ReadDataset(intUserId, 0, 0);

            StringBuilder strTableData = new StringBuilder();
            strTableData.Append("<table id=\"tblSMSLogs\" class=\"table table-bordered table-striped\">\n");
            //string strTableData = "<table id=\"tblSMSLogs\" class=\"table table-bordered table-striped\">\n";
            strTableData.Append("<thead><tr><th>Date Time</th><th>Number</th><th>Message Type</th><th>Message</th></tr></thead>");
            strTableData.Append(" <tbody>");

            for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
            {
                strTableData.Append("<tr>");
                strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr][0].ToString() + "</td>");
                strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr][1].ToString() + "</td>");
                strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr][2].ToString() + "</td>");
                strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr][3].ToString() + "</td>");
                strTableData.Append("</tr>");
            }

            strTableData.Append(" </tbody></table>");
            objSMSLog = null;
            dsData = null;
            String strTableData1 = strTableData.ToString() ;

            spTable.InnerHtml = strTableData1;



        }
    }
}