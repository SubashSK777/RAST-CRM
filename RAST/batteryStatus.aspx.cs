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

public partial class battteryStaus : System.Web.UI.Page
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
            MySqlCommand cmd = new MySqlCommand("p_GetBatteryStatus", conn);
            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("m_UserId", intUserId);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet dsData = new DataSet();
            sda.Fill(dsData);

            StringBuilder strTableData = new StringBuilder();

            strTableData.Append("<table id=\"tblBatteyStatus\" class=\"table table-bordered table-striped\">\n");
            strTableData.Append("<thead><tr><th>Battery Status</th><th>Battery % SMS</th><th>Battery % Trigger</th><th>Real Count</th><th>Filter Count</th><th>Battery Change Date</th><th>Site Name</th><th>Floor Plan Name</th><th>HUB Number</th><th>Sensor Location</th><th>Active</th><th>Sensor Log Details</th><th>Last Week Count</th><th>Update Battery Date</th></tr></thead>");
            strTableData.Append(" <tbody>");
            if (dsData.Tables[0].Rows.Count > 0)
            {
                    
                int batMaxTriggerOriginal= Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["BatteryMaxTrigger"].ToString());
                for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
                {
                    int batMaxTrigger;
                    //Find Wifi /SMS
                    String tPhone = Convert.ToString(dsData.Tables[0].Rows[intRowCtr]["s_HubPhoneNumber"].ToString());
                    if (tPhone.StartsWith("XR"))
                    {
                        batMaxTrigger = batMaxTriggerOriginal*2;
                    }
                    else
                    {
                        batMaxTrigger = batMaxTriggerOriginal;
                    }

                    int intSMSCount = 0;
                    int intTriggCount = 0;
                    intSMSCount = Convert.ToInt32(dsData.Tables[0].Rows[intRowCtr]["actualCount"].ToString());
                    intTriggCount = Convert.ToInt32(dsData.Tables[0].Rows[intRowCtr]["triggerCount"].ToString());


                    //Ragu Battery StandbyTime Update
                    DateTime installation_date = Convert.ToDateTime(dsData.Tables[0].Rows[intRowCtr]["dt_batChangeDate"].ToString());
                    DateTime todayDate = DateTime.Now;
                    double diff = (todayDate - installation_date).Days;

                    double t_batPer = 0;
                    t_batPer = batMaxTrigger - intTriggCount;
                    //t_batPer = (t_batPer * 100) / batMaxTrigger;
                    t_batPer = Math.Round((t_batPer / batMaxTrigger) * 100,0);

                    t_batPer = Math.Round(t_batPer - (diff * 0.25),0);
                    

                    if (t_batPer <= 0)
                        t_batPer = 0;


                    //SMS Log Batter Percentage
                    double t_batPerSms = 0;
                    t_batPerSms = batMaxTrigger - intSMSCount;
                    t_batPerSms = Math.Round((t_batPerSms / batMaxTrigger) * 100,0);


                    t_batPerSms = Math.Round(t_batPerSms - (diff * 0.25),0);
                    
                    if (t_batPerSms <= 0)
                        t_batPerSms = 0;




                    strTableData.Append("<tr>");

                if (t_batPerSms <= 0)
                    {
                        strTableData.Append("<td>" + "<b><font color=RED>ALIVE</font></b>" + "</td>");
                    }
                    else if((t_batPerSms >= 1) && (t_batPerSms <= 30))
                    {
                        strTableData.Append("<td>" + "<b><font color=RED>LIVE</font></b>" + "</td>");
                    }
                    else if ((t_batPerSms >= 31) && (t_batPerSms <= 50))
                    {
                        strTableData.Append("<td>" + "<b><font color=BLUE>LIVE</font></b>" + "</td>");
                    }
                    else
                    {
                        strTableData.Append("<td>" + "<b><font color = GREEN >" + dsData.Tables[0].Rows[intRowCtr]["s_batStaus"].ToString() + "</font></b></td>");
                    }

                    if ((t_batPerSms >= 0) && (t_batPerSms <= 30))
                    {
                        strTableData.Append("<td>" + "<b><font color=RED>" + t_batPerSms.ToString() + "</font></b>" + "</td>");
                        strTableData.Append("<td>" + "<b><font color=RED>" + t_batPer.ToString() + "</font></b>" + "</td>");
                            
                        //strTableData.Append("<td>" + "<b><font color=RED>" + intSMSCount  + "</font></b> % </td>");
                    }
                    else if ((t_batPerSms >= 31) && (t_batPerSms <= 50))
                    {
                        strTableData.Append("<td>" + "<b><font color=BLUE>" + t_batPerSms.ToString() + "</font></b>" + "</td>");
                        strTableData.Append("<td>" + "<b><font color=BLUE>" + t_batPer.ToString() + "</font></b>" + "</td>");
                        
                        //strTableData.Append("<td>" + "<b><font color=RED>" + intSMSCount + "</font></b> % </td>");
                    }
                    else
                    {
                        strTableData.Append("<td>" + "<b><font color=GREEN>" + t_batPerSms.ToString() + "</font></b>" + "</td>");
                        strTableData.Append("<td>" + "<b><font color=GREEN>" + t_batPer.ToString() + "</font></b>" + "</td>");
                        
                        //strTableData.Append("<td>" + "<b><font color=RED>" + intSMSCount + "</font></b> % </td>");
                    }


                    strTableData.Append("<td>" + intSMSCount + "</td>");
                    strTableData.Append("<td>" + intTriggCount + "</td>");

                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["dt_batChangeDate"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_SiteName"].ToString() + "</a></td>");
                    //strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_Address"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_FloorMapImageName"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_HubPhoneNumber"].ToString() + "</td>");
                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["s_SensorLocation"].ToString() + "</td>");
                    string tmp1 = dsData.Tables[0].Rows[intRowCtr]["active"].ToString();
                    if (tmp1.Equals("True"))
                    {
                        strTableData.Append("<td><input type=\"checkbox\" id=\"chk_" + dsData.Tables[0].Rows[intRowCtr]["i_SensorId"].ToString() + "\" class=\"checkbox\" value=\"" + dsData.Tables[0].Rows[intRowCtr]["active"].ToString() + " \" onchange=\"updSensorStatus(" + dsData.Tables[0].Rows[intRowCtr]["i_SensorId"].ToString() + ")\" checked></td>");
                    }
                    else
                    { 
                        strTableData.Append("<td><input type=\"checkbox\" id=\"chk_" + dsData.Tables[0].Rows[intRowCtr]["i_SensorId"].ToString() + "\" class=\"checkbox\" value=\"" + dsData.Tables[0].Rows[intRowCtr]["active"].ToString() + "\" onchange=\"updSensorStatus(" + dsData.Tables[0].Rows[intRowCtr]["i_SensorId"].ToString() + ")\"></td>");
                    }
                    strTableData.Append("<td><input type=\"text\" id=\"txt_" +dsData.Tables[0].Rows[intRowCtr]["i_SensorId"].ToString() + "\" class=\"text\" value=\""+ dsData.Tables[0].Rows[intRowCtr]["SensorLog"].ToString() + "\" onchange=\"updSensorLog(" + dsData.Tables[0].Rows[intRowCtr]["i_SensorId"].ToString() + ")\"></td>");

                    strTableData.Append("<td>" + dsData.Tables[0].Rows[intRowCtr]["sevenDaysCount"].ToString() + "</td>");

                    //strTableData.Append("<td><input type=\"button\" class=\"btn\" value=\"" + dsData.Tables[0].Rows[intRowCtr]["i_SensorId"].ToString() + "\" onclick=\"updBat(" + dsData.Tables[0].Rows[intRowCtr]["i_SensorId"].ToString() + ")\"></td>");
                    strTableData.Append("<td><input type=\"button\" class=\"btn btn-success btn-sm m-1\" value=\"Update Battery\" onclick=\"updBat(" + dsData.Tables[0].Rows[intRowCtr]["i_SensorId"].ToString() + ")\"></td>");

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