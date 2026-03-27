using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Net;
using System.Text;
using System.IO;
using RAST.DAL;
using System.Net.Mail;
using System.Data.SqlClient;

public partial class services_receive_sensor_data : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            //fnCreateAlert("06", 194, 2, "6584995991");

            /*  live Portion*/

            //fnSendSMS("6584995991", "TEST Ragu");

            string strMobileNumber = Convert.ToString(Request.Form["Mobile"]);
            string strMessageType = Convert.ToString(Request.Form["Type"]);
            string strMessage = Convert.ToString(Request.Form["Message"]);
            DateTime dtTimeStamp = Convert.ToDateTime(Request.Form["Timestamp"]);
            /*Live End */

            /* Test Portion
            string strMobileNumber = "XR1B0284B000006";
            string strMessageType = "A";
            string strMessage = "Zone 00";
            DateTime dtTimeStamp = Convert.ToDateTime("2019-05-14 17:59:22"); */

            /*Test Portion  */

            /*fnSendSMS("6584995991", "TEST CP Chicken Farm Ragu");
            return;*/
            /*Test Portion Send Sms*/

            int intReturnValue = 0;
            int intReturnValue1 = 0;
            string strSensorType = "";
            string strDeletionDetail = "";
            int intSRSAlert = 0;

            if ((strMobileNumber.Length == 0)||(strMessageType.Length == 0)||(strMessage.Length == 0))
            {
                Response.Write("ERROR");
                return;
            }

            string strSMSMessage = strMessage;
            string strAPIConfig = ConfigurationManager.AppSettings["SMSPrefix"].ToString();
            
            
            strMessage = strMessage.Trim().ToLower();
            strMessage = strMessage.Replace(strAPIConfig, "");
            strMessage = strMessage.Replace("zone", "");
            strMessage = strMessage.Trim();

            if (strMessage.Contains("check"))
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
                {
                    conn.Open();
                    Array arrMessage = strMessage.Split(' ');
                    if (arrMessage.Length == 3)
                    {
                        int intSiteId = Convert.ToInt32(arrMessage.GetValue(1));
                        string strHubId = Convert.ToString(arrMessage.GetValue(2));
                        string strHubPhoneNumber = "";

                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = "p_CheckHub";
                        cmd.Parameters.AddWithValue("m_HubId", strHubId.Trim());
                        cmd.Parameters.AddWithValue("m_SiteId", intSiteId);


                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                strHubPhoneNumber = reader.GetString(reader.GetOrdinal("s_HubPhoneNumber"));
                            }
                        }
                        
                        cmd = null;

                        if (strHubPhoneNumber != "")
                        {
                            bool blnStatus = fnSendSMS(strHubPhoneNumber, strHubId);
                            string strHubStatus = "";
                            if(blnStatus == true)
                            {
                                strHubStatus = "Hub working correctly";
                                blnStatus = fnSendSMS(strMobileNumber, strHubStatus);
                            }
                            else
                            {
                                strHubStatus = "Hub not working";
                                blnStatus = fnSendSMS(strMobileNumber, strHubStatus);
                            }


                            MySqlCommand cmdSMS = new MySqlCommand();
                            cmdSMS.Connection = conn;
                            cmdSMS.CommandType = System.Data.CommandType.StoredProcedure;
                            cmdSMS.CommandText = "p_SaveSMS";
                            cmdSMS.Parameters.AddWithValue("m_dtTimeStamp", DateTime.Now);
                            cmdSMS.Parameters.AddWithValue("m_strPhoneNumber", strMobileNumber);
                            cmdSMS.Parameters.AddWithValue("m_strMessageType", strMessageType );
                            cmdSMS.Parameters.AddWithValue("m_strMessage", "Checking Hub Status. Message: " + strMessage + ". Result:" + strHubStatus);
                            cmdSMS.ExecuteNonQuery();
                            Response.Write("OK");
                        }
                        else
                        {
                            bool blnStatus = fnSendSMS(strMobileNumber, "Hub does not exist");
                            Response.Write("OK");
                        }
                    }
                    else
                    {
                        Response.Write("ERROR");
                    }
                }
            }
            else if (strMessage.Contains("start") || strMessage.Contains("stop"))
            {
                //Response.Write(strMessage + "<br/>");
                //Code for commissioning and decommissioning the the Sensor
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
                {
                    conn.Open();
                    try
                    {
                        Array arrMessage = strMessage.Split(' ');
                        if (arrMessage.Length == 2)
                        {
                            string strKeyword = Convert.ToString(arrMessage.GetValue(0));
                            int intSiteId = Convert.ToInt32(arrMessage.GetValue(1));
                            string strType = "";

                            if (strMessage.Contains("start"))
                            {
                                //Response.Write("starting<br/>" + intSiteId.ToString() + "<br/>");
                                strType = "Site Commission";
                                MySqlCommand cmd = new MySqlCommand();
                                cmd.Connection = conn;
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.CommandText = "p_CommissionSite";
                                cmd.Parameters.AddWithValue("m_Status","1");
                                cmd.Parameters.AddWithValue("m_SiteId", intSiteId);
                                cmd.ExecuteNonQuery();
                                cmd = null;

                                bool blnStatus = fnSendSMS(strMobileNumber, "Site Id '" + intSiteId.ToString() + "' Commissioned successfully");
                            }
                            else if (strMessage.Contains("stop"))
                            {
                                strType = "Site Decommission";
                                MySqlCommand cmd = new MySqlCommand();
                                cmd.Connection = conn;
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.CommandText = "p_CommissionSite";
                                cmd.Parameters.AddWithValue("m_Status", "0");
                                cmd.Parameters.AddWithValue("m_SiteId", intSiteId);
                                cmd.ExecuteNonQuery();
                                cmd = null;

                                bool blnStatus = fnSendSMS(strMobileNumber, "Site Id '" + intSiteId.ToString() + "' Decommissioned successfully");
                
                            }
                            MySqlCommand cmdSMS = new MySqlCommand();
                            cmdSMS.Connection = conn;
                            cmdSMS.CommandType = System.Data.CommandType.StoredProcedure;
                            cmdSMS.CommandText = "p_SaveSMS";
                            cmdSMS.Parameters.AddWithValue("m_dtTimeStamp", DateTime.Now);
                            cmdSMS.Parameters.AddWithValue("m_strPhoneNumber", strMobileNumber);
                            cmdSMS.Parameters.AddWithValue("m_strMessageType", strType);
                            cmdSMS.Parameters.AddWithValue("m_strMessage", strSMSMessage);
                            cmdSMS.ExecuteNonQuery();

                        }
                         Response.Write("OK");
                    }
                    catch(Exception exp)
                    {
                        Response.Write("ERROR" + exp.ToString());
                    }

                    conn.Close();
                   
                }

            }
            else
            {
                //------ Ragu Error log -----//
                

                //---------------------------

                if (strSMSMessage.Contains("AC power"))
                {
                        fnPrepareSMS_ACPower(strMobileNumber.Trim(), strSMSMessage.Trim(), dtTimeStamp);
                }


                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
                {
                    int intHubId = 0;
                    string strSensorId = Convert.ToString(strMessage);
                    conn.Open();

                    //----------------------------- Wifi Gateway Data Remover ---------------//
                    string gwType = "GSM";
                    int skipStatus = 0;
                    int tHubId = 0;
                    string t_SensorType = "";
                    int t_sensorId = 0;

                    bool isNumeric = true;
                    foreach (char c in strMobileNumber.Trim())
                    {
                        if (!Char.IsNumber(c))
                        {
                            isNumeric = false;
                            break;
                        }
                    }

                    if (isNumeric==false)
                        gwType = "DATA";
                    else
                        gwType = "GSM";
                    
                    DataSet dsChecker1 = new DataSet();
                    MySqlCommand cmdChecker1 = new MySqlCommand();
                    cmdChecker1.Connection = conn;
                    cmdChecker1.CommandText = "SELECT s_hubOnTime as m_hubOnTime, s_hubOffTime as m_hubOffTime, s_MobileProvider as m_MobileProvider, i_HubId  from hub where s_HubPhoneNumber = '" + strMobileNumber.Trim() + "'";
                    MySqlDataAdapter daChecker1 = new MySqlDataAdapter(cmdChecker1);
                    daChecker1.Fill(dsChecker1);

                    DateTime dtHubOnTime=new DateTime();
                    DateTime dtHubOffTime = new DateTime();
                    DateTime t_TriggerTime = new DateTime();
                    string tMobileProvider;

                    if (dsChecker1.Tables[0].Rows.Count != 0)
                    {
                        dtHubOnTime = Convert.ToDateTime(dsChecker1.Tables[0].Rows[0]["m_hubOnTime"]);
                        dtHubOffTime = Convert.ToDateTime(dsChecker1.Tables[0].Rows[0]["m_hubOffTime"]);
                        tMobileProvider = Convert.ToString(dsChecker1.Tables[0].Rows[0]["m_MobileProvider"]);
                        tHubId = Convert.ToInt32(dsChecker1.Tables[0].Rows[0]["i_HubId"]);

                        if(dtTimeStamp < dtHubOnTime)
                        {
                            dtHubOnTime = dtHubOnTime.AddDays(-1);
                        }
                        else if (dtHubOffTime <= dtHubOnTime)
                        {
                            dtHubOffTime = dtHubOffTime.AddDays(1);
                        }

                        DataSet dsChecker2 = new DataSet();
                        MySqlCommand cmdChecker2 = new MySqlCommand();
                        cmdChecker2.Connection = conn;
                        cmdChecker2.CommandText = "SELECT i_SensorId, s_SensorType from sensor where i_HubId = '" + tHubId + "' and s_SensorId= '" + strMessage + "'";
                        MySqlDataAdapter daChecker2 = new MySqlDataAdapter(cmdChecker2);
                        daChecker2.Fill(dsChecker2);
                        if (dsChecker2.Tables[0].Rows.Count != 0)
                        {
                            t_SensorType = Convert.ToString(dsChecker2.Tables[0].Rows[0]["s_SensorType"]);
                            t_sensorId = Convert.ToInt32(dsChecker2.Tables[0].Rows[0]["i_SensorId"]);
                        }
                        else
                        {
                            skipStatus = 0;
                        }
                        t_TriggerTime = DateTime.Parse(dtTimeStamp.ToString());

                    }
                    else
                    {
                        skipStatus = 0;
                    }

                    if (gwType.Equals("DATA"))  // Wifi -  3G gateway
                    {
                        if (t_SensorType.Equals("RE*"))  // Rodent Monitoring
                        //if (t_SensorType.Length>=1)  // Rodent Monitoring
                        {
                            skipStatus = 0;
                            int t_off,t_on,t_tri;
                            t_off = Convert.ToInt32(dtHubOffTime.ToString("HHmm"));
                            t_on = Convert.ToInt32(dtHubOnTime.ToString("HHmm"));
                            t_tri = Convert.ToInt32(t_TriggerTime.ToString("HHmm"));

                            if(t_on> t_tri)
                            {
                                if(t_off<= t_tri)
                                {
                                    skipStatus = 1;
                                }
                            }
                        }
                        else // Wifi with Trap / SRS
                        {
                            skipStatus = 0;
                        }
                    }
                    else
                    {
                        skipStatus = 0;
                    }


                    if (skipStatus == 1)
                    {
                        MySqlCommand cmdupd = new MySqlCommand();
                        cmdupd.Connection = conn;
                        cmdupd.CommandText = "update sensor set actualCount = actualCount + 1 where i_SensorId = '" + t_sensorId + "'";
                        cmdupd.ExecuteNonQuery();
                    }
                    ///--------------------------------

                    if (skipStatus == 0)
                    {

                        int applyAutoRemoval = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["applyAutoRemoval"].ToString());

                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        if (applyAutoRemoval == 1)
                        {
                            cmd.CommandText = "p_AddSensorData_New";
                        }
                        else
                        {
                            cmd.CommandText = "p_AddSensorData";
                        }
                        cmd.Parameters.AddWithValue("m_MobileNumber", strMobileNumber.Trim());
                        cmd.Parameters.AddWithValue("m_MessageType", strMessageType.Trim());
                        cmd.Parameters.AddWithValue("m_Message", strSMSMessage.Trim());
                        cmd.Parameters.AddWithValue("m_TimeStamp", dtTimeStamp);
                        cmd.Parameters.AddWithValue("m_SensorId", strSensorId.Trim());

                        //Test by Ragu
                        //fnSendSMS("6584995991", strSMSMessage);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                intReturnValue = reader.GetInt32(reader.GetOrdinal("intStatus"));
                                intHubId = reader.GetInt32(reader.GetOrdinal("intHubId"));
                                strSensorType = reader.GetString(reader.GetOrdinal("strSensorType"));  // Ragu 01 May 2018
                                intReturnValue1 = reader.GetInt32(reader.GetOrdinal("intStatus1"));
                                intSRSAlert = reader.GetInt32(reader.GetOrdinal("SRSAlert"));
                                if (applyAutoRemoval == 1)
                                {
                                    strDeletionDetail = reader.GetString(reader.GetOrdinal("strDeletionDetail"));  // Ragu 13 May 2019
                                }
                                else
                                {
                                    //strDeletionDetail = reader.GetString(reader.GetOrdinal("strDeletionDetail"));
                                }
                            }
                        }
                        cmd = null;

                        if (intReturnValue > 0)
                        {
                            fnPrepareSMS(strSensorId, intHubId, intReturnValue, strMobileNumber.Trim());
                        }

                        if (intReturnValue1 == 6)
                        {
                            fnPrepareSMS(strSensorId, intHubId, intReturnValue1, strMobileNumber.Trim());
                        }

                        if(strDeletionDetail.Length>1)
                        {
                            fnSendEmail(strSensorId, strDeletionDetail, "Support", "support@pestech.com.sg");
                        }
                        conn.Close();


                        //Alert Sent for SRS and SRT

                        if (strSensorType.Equals("SRS-Bait Station") || strSensorType.Equals("SRS-Trap") || strSensorType.Equals("SRS-ST") || strSensorType.Equals("SRS-GB") || strSensorType.Equals("SRS-CG"))
                        {
                            if (intSRSAlert == 1)
                            {
                                fnCreateAlert(strSensorId, intHubId, intReturnValue, strMobileNumber.Trim());
                                //fnCreateAlert("3210", 194, 2, 6584995991);
                            }
                        }

                        //Create Appoiintment
                        if (strSensorType.Equals("RE*"))
                        {

                        }
                        else if (strSensorType.Equals("SRS-Bait Station"))
                        {
                            
                        }
                        else if (strSensorType.Equals("Trap without Sensor"))
                        {

                        }
                        else if (strSensorType.Equals("SRS-Trap") || strSensorType.Equals("SRS-ST") || strSensorType.Equals("SRS-GB") || strSensorType.Equals("SRS-CG") || strSensorType.Equals("SMT"))
                        {
                            if (intSRSAlert == 1)
                                fnCreateAppointment(strSensorId, intHubId, intReturnValue, strMobileNumber.Trim());
                        }

                        
                        Response.Write("OK");
                    }
                    else
                    {
                        Response.Write("OK : Trigger Skipped - Due to Schedule");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("ERROR. Adding Sensor Values" + ex.ToString());
        }
    }

 
        private void fnPrepareSMS(string strSensorId, int intHubId, int intSMSType, string HubPhoneNumber )
        {

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            string strPhoneNumber = "";
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetSiteTechncian";
            cmd.Parameters.AddWithValue("m_HubId", intHubId);
            cmd.Parameters.AddWithValue("m_SensorId", strSensorId);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            
            if(ds.Tables[0].Rows.Count > 0)
            {
                string strLimit = "";
                string strLimitText = "";
                if(intSMSType == 1)
                {
                    strLimit = Convert.ToString(ds.Tables[1].Rows[0]["i_MinThresholdLevel"]);
                    strLimitText = "Above Min";

                    // Ragu 03 Aug - Removoe Min Threshold
                    cmd = null;
                    conn.Close();
                    da = null;
                    ds = null;
                    return;
                }
                if (intSMSType == 2)
                {
                    strLimit = Convert.ToString(ds.Tables[1].Rows[0]["i_MaxThresholdLevel"]);
                    strLimitText = "Above Max";
                }

                
                /*string strSMSMessage = "Site Alarm" + Environment.NewLine;
                strSMSMessage = strSMSMessage + Convert.ToString(ds.Tables[1].Rows[0]["s_SiteName"]) + " - " + Convert.ToString(ds.Tables[1].Rows[0]["s_SensorId"]) + Environment.NewLine;
                strSMSMessage = strSMSMessage + strLimitText + Environment.NewLine + "Current Value " + Convert.ToString(ds.Tables[2].Rows[0]["i_SensorDataCount"]) + Environment.NewLine;
                strSMSMessage = strSMSMessage + "Threshold Value "  + strLimit + Environment.NewLine;
                strSMSMessage = strSMSMessage + "DateTime " + Convert.ToString(ds.Tables[2].Rows[0]["dt_MaxDateTime"]);*/


                string strSMSMessage = "RodentEye System Alert" + Environment.NewLine;
                strSMSMessage = strSMSMessage + Convert.ToString(ds.Tables[1].Rows[0]["s_SiteName"]) + " - " + Convert.ToString(ds.Tables[1].Rows[0]["s_SensorLocation"]) + Environment.NewLine;
                //strSMSMessage = strSMSMessage + strLimitText + Environment.NewLine + Environment.NewLine;  // + "Current Value " + Convert.ToString(ds.Tables[2].Rows[0]["i_SensorDataCount"])
                strSMSMessage = strSMSMessage + "Threshold Value "  + strLimit + Environment.NewLine;
                strSMSMessage = strSMSMessage + "DateTime " + Convert.ToString(ds.Tables[2].Rows[0]["dt_MaxDateTime"]);

                strPhoneNumber = Convert.ToString(ds.Tables[0].Rows[0][0]);
                string strUserName = Convert.ToString(ds.Tables[0].Rows[0][1]);
                string strEmail = Convert.ToString(ds.Tables[0].Rows[0][2]);


                if (intSMSType == 4)
                {
                    strSMSMessage = "RodentEye === HUB DisArm ===" + Environment.NewLine;
                    strSMSMessage = strSMSMessage + "Unusal Trigger 50 Times at 24 Hrs " + Environment.NewLine;
                    strSMSMessage = strSMSMessage + "Site Name : " + Convert.ToString(ds.Tables[1].Rows[0]["s_SiteName"]) + " - Sensor Location : " + Convert.ToString(ds.Tables[1].Rows[0]["s_SensorLocation"]) + Environment.NewLine;
                    strSMSMessage = strSMSMessage + "HUB Phone Number : " + HubPhoneNumber + Environment.NewLine;
                    strSMSMessage = strSMSMessage + "Date Time " + Convert.ToString(ds.Tables[2].Rows[0]["dt_MaxDateTime"]);

                    String t_secretCode = System.Web.Configuration.WebConfigurationManager.AppSettings["SecretCode"].ToString();

                    bool blnStatus1 = fnSendSMS("6597291320", strSMSMessage);
                    blnStatus1 = fnSendSMS("6584995991", strSMSMessage);
                    //blnStatus1 = fnSendSMS("6583987391", strSMSMessage);
                    blnStatus1 = fnSendSMS("6582672062", strSMSMessage);
                    //blnStatus1 = fnSendSMS("6592305656", strSMSMessage);

                    blnStatus1 = fnSendSMS(HubPhoneNumber, t_secretCode + "0#");
                    //blnStatus1 = fnSendSMS(HubPhoneNumber, t_secretCode + "0#");
                    //blnStatus1 = fnSendSMS(HubPhoneNumber, t_secretCode + "0#");
                    //blnStatus1 = fnSendSMS(HubPhoneNumber, t_secretCode + "0#");
                    //blnStatus1 = fnSendSMS(HubPhoneNumber, t_secretCode + "0#");
                    //blnStatus1 = fnSendSMS(HubPhoneNumber, t_secretCode + "0#");
                    //blnStatus1 = fnSendSMS(HubPhoneNumber, t_secretCode + "0#");
                    //blnStatus1 = fnSendSMS(HubPhoneNumber, t_secretCode + "0#");
                    //blnStatus1 = fnSendSMS(HubPhoneNumber, t_secretCode + "0#");

                    MySqlCommand cmd1 = new MySqlCommand();
                    cmd1.Connection = conn;
                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd1.CommandText = "p_AddAlertSMS";
                    cmd1.Parameters.AddWithValue("m_alertMessage", "RodentEye === HUB DisArm ===");
                    cmd1.Parameters.AddWithValue("m_alertStatus", "Pending");
                    cmd1.Parameters.AddWithValue("m_handleBy", "");
                    cmd1.Parameters.AddWithValue("m_dtDateTime", Convert.ToDateTime(ds.Tables[2].Rows[0]["dt_MaxDateTime"]));
                    cmd1.Parameters.AddWithValue("m_siteName", Convert.ToString(ds.Tables[1].Rows[0]["s_SiteName"]));
                    cmd1.Parameters.AddWithValue("m_floorName", HubPhoneNumber + "-" + Convert.ToString(ds.Tables[1].Rows[0]["s_SensorLocation"]));
                    cmd1.Parameters.AddWithValue("m_intSensorId", 0);
                    cmd1.ExecuteNonQuery();
                    cmd1 = null;

                    cmd = null;
                    conn.Close();
                    da = null;
                    ds = null;

                    fnSendEmail(strSensorId, strSMSMessage, "Support", "support@pestech.com.sg");

                    return;
                }


                if (intSMSType == 5)
                {
                    strSMSMessage = "RodentEye - 2 Trigger with in 1 Hour" + Environment.NewLine;
                    strSMSMessage = strSMSMessage + "Site Name : " + Convert.ToString(ds.Tables[1].Rows[0]["s_SiteName"]) + " - Sensor Location : " + Convert.ToString(ds.Tables[1].Rows[0]["s_SensorLocation"]) + Environment.NewLine;
                    strSMSMessage = strSMSMessage + "HUB Phone Number : " + HubPhoneNumber + Environment.NewLine;
                    strSMSMessage = strSMSMessage + "Date Time " + Convert.ToString(ds.Tables[2].Rows[0]["dt_MaxDateTime"]);

                    MySqlCommand cmd1 = new MySqlCommand();
                    cmd1.Connection = conn;
                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd1.CommandText = "p_AddAlertSMS";
                    cmd1.Parameters.AddWithValue("m_alertMessage", "RodentEye - 2 Trigger with in 1 Hour");
                    cmd1.Parameters.AddWithValue("m_alertStatus", "Pending");
                    cmd1.Parameters.AddWithValue("m_handleBy", "");
                    cmd1.Parameters.AddWithValue("m_dtDateTime", Convert.ToDateTime(ds.Tables[2].Rows[0]["dt_MaxDateTime"]));
                    cmd1.Parameters.AddWithValue("m_siteName", Convert.ToString(ds.Tables[1].Rows[0]["s_SiteName"]));
                    cmd1.Parameters.AddWithValue("m_floorName", HubPhoneNumber + "-" + Convert.ToString(ds.Tables[1].Rows[0]["s_SensorLocation"]));
                    cmd1.Parameters.AddWithValue("m_intSensorId", 0);
                    cmd1.ExecuteNonQuery();
                    cmd1 = null;

                    /*---- Hide Avoid 8 Trigger----*/
                    /*
                    bool blnStatus1 = fnSendSMS("6597291320", strSMSMessage);
                    blnStatus1 = fnSendSMS("6584995991", strSMSMessage);
                    //blnStatus1 = fnSendSMS("6583987391", strSMSMessage);
                    blnStatus1 = fnSendSMS("6582672062", strSMSMessage);
                    blnStatus1 = fnSendSMS("6592305656", strSMSMessage);
                    */

                    cmd = null;
                    conn.Close();
                    da = null;
                    ds = null;
                    return;
                }

                if (intSMSType == 3)
                {
                    strSMSMessage = "RodentEye - High Trigger Alert - 20 Triggers in one Day" + Environment.NewLine;
                    strSMSMessage = strSMSMessage + Convert.ToString(ds.Tables[1].Rows[0]["s_SiteName"]) + " - " + Convert.ToString(ds.Tables[1].Rows[0]["s_SensorLocation"]) + Environment.NewLine;
                    strSMSMessage = strSMSMessage + "Threshold Value " + strLimit + Environment.NewLine;
                    strSMSMessage = strSMSMessage + "DateTime " + Convert.ToString(ds.Tables[2].Rows[0]["dt_MaxDateTime"]);

                    /*bool blnStatus1 = fnSendSMS("6597291320", strSMSMessage);
                    blnStatus1 = fnSendSMS("6584995991", strSMSMessage);
                    //blnStatus1 = fnSendSMS("6583987391", strSMSMessage);
                    blnStatus1 = fnSendSMS("6582672062", strSMSMessage);
                    blnStatus1 = fnSendSMS("6592305656", strSMSMessage);*/

                    MySqlCommand cmd1 = new MySqlCommand();
                    cmd1.Connection = conn;
                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd1.CommandText = "p_AddAlertSMS";
                    cmd1.Parameters.AddWithValue("m_alertMessage", "RodentEye - High Trigger Alert - 20 Triggers in one Day");
                    cmd1.Parameters.AddWithValue("m_alertStatus", "Pending");
                    cmd1.Parameters.AddWithValue("m_handleBy", "");
                    cmd1.Parameters.AddWithValue("m_dtDateTime", Convert.ToDateTime(ds.Tables[2].Rows[0]["dt_MaxDateTime"]));
                    cmd1.Parameters.AddWithValue("m_siteName", Convert.ToString(ds.Tables[1].Rows[0]["s_SiteName"]));
                    cmd1.Parameters.AddWithValue("m_floorName", HubPhoneNumber + "-" + Convert.ToString(ds.Tables[1].Rows[0]["s_SensorLocation"]));
                    cmd1.Parameters.AddWithValue("m_intSensorId", 0);
                    cmd1.ExecuteNonQuery();
                    cmd1 = null;

                    cmd = null;
                    conn.Close();
                    da = null;
                    ds = null;
                    return;
                }


                if (intSMSType == 6)
                {
                    strSMSMessage = "3 Hours Continous trigger" + Environment.NewLine;
                    strSMSMessage = strSMSMessage + "Site Name : " + Convert.ToString(ds.Tables[1].Rows[0]["s_SiteName"]) + " - Sensor Location : " + Convert.ToString(ds.Tables[1].Rows[0]["s_SensorLocation"]) + Environment.NewLine;
                    strSMSMessage = strSMSMessage + "HUB Phone Number : " + HubPhoneNumber + Environment.NewLine;
                    strSMSMessage = strSMSMessage + "Date Time " + Convert.ToString(ds.Tables[2].Rows[0]["dt_MaxDateTime"]);

                    String t_secretCode = System.Web.Configuration.WebConfigurationManager.AppSettings["SecretCode"].ToString();

                    bool blnStatus1 = fnSendSMS("6597291320", strSMSMessage);
                    blnStatus1 = fnSendSMS("6584995991", strSMSMessage);
                    //blnStatus1 = fnSendSMS("6583987391", strSMSMessage);
                    blnStatus1 = fnSendSMS("6582672062", strSMSMessage);
                    //blnStatus1 = fnSendSMS("6592305656", strSMSMessage);
                    
                    MySqlCommand cmd1 = new MySqlCommand();
                    cmd1.Connection = conn;
                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd1.CommandText = "p_AddAlertSMS";
                    cmd1.Parameters.AddWithValue("m_alertMessage", "3 Hours Continous trigger");
                    cmd1.Parameters.AddWithValue("m_alertStatus", "Pending");
                    cmd1.Parameters.AddWithValue("m_handleBy", "");
                    cmd1.Parameters.AddWithValue("m_dtDateTime", Convert.ToDateTime(ds.Tables[2].Rows[0]["dt_MaxDateTime"]));
                    cmd1.Parameters.AddWithValue("m_siteName", Convert.ToString(ds.Tables[1].Rows[0]["s_SiteName"]));
                    cmd1.Parameters.AddWithValue("m_floorName", HubPhoneNumber + "-" + Convert.ToString(ds.Tables[1].Rows[0]["s_SensorLocation"]));
                    cmd1.Parameters.AddWithValue("m_intSensorId", 0);
                    cmd1.ExecuteNonQuery();
                    cmd1 = null;

                    cmd = null;
                    conn.Close();
                    da = null;
                    ds = null;
                    return;
                }



                bool blnStatus = fnSendSMS(strPhoneNumber, strSMSMessage);

                string strResult = "";
                if(blnStatus == true)
                {
                    strResult = "SMS delivery successful";
                }
                else
                {
                    strResult = "SMS delivery failed";
                }

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_SaveSMS";
                cmd.Parameters.AddWithValue("m_dtTimeStamp", Convert.ToDateTime(ds.Tables[2].Rows[0]["dt_MaxDateTime"]));
                cmd.Parameters.AddWithValue("m_strPhoneNumber", strPhoneNumber);
                cmd.Parameters.AddWithValue("m_strMessageType", strLimitText);
                cmd.Parameters.AddWithValue("m_strMessage", strSMSMessage + ". SMS Response:" + strResult);
                cmd.ExecuteNonQuery();

                fnSendEmail(strSensorId, strSMSMessage, strUserName, strEmail);

                //Deloyment Techinician Send SMS and Email
                
                string strPhoneNumber_Dep = Convert.ToString(ds.Tables[3].Rows[0][0]);
                string strUserName_Dep = Convert.ToString(ds.Tables[3].Rows[0][1]);
                string strEmail_Dep = Convert.ToString(ds.Tables[3].Rows[0][2]);

                if(!strPhoneNumber_Dep.Equals("0"))
                {
                    if (strPhoneNumber_Dep.Length >= 10)
                    {
                        blnStatus = fnSendSMS(strPhoneNumber_Dep, strSMSMessage);
                        
                    }
                }
                fnSendEmail(strSensorId, strSMSMessage, strUserName_Dep, strEmail_Dep);
            }
                    
            cmd = null;
            conn.Close();
            da = null;
            ds = null; 
        }
    }

    private void fnPrepareSMS_ACPower(string strMobileNumber, string strMessgae, DateTime dtDateTime)
    {

        if (strMobileNumber.Substring(0, 2).Equals("60"))  // Avoid Send SMS to MAS
        {
            return;
        }
        if (strMobileNumber.Substring(0, 2).Equals("85")) // Avoid Send SMS to HK
        {
            return;
        }
        if (strMobileNumber.Length < 10) // Avoid Send SMS less than 10 Digit
        {
            return;
        }


        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {

            conn.Open();
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_CheckSiteDet";
            cmd.Parameters.AddWithValue("m_mobileNumber", strMobileNumber);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string strSMSMessage = "Power Status : " + strMessgae + Environment.NewLine;
                strSMSMessage = strSMSMessage + "Site Name : " + Convert.ToString(ds.Tables[0].Rows[0]["s_SiteName"]) + Environment.NewLine;
                //strSMSMessage = strSMSMessage + "Site Address : " + Convert.ToString(ds.Tables[0].Rows[0]["s_Address"]) + Environment.NewLine;
                strSMSMessage = strSMSMessage + "Hub Detail : " + Convert.ToString(ds.Tables[0].Rows[0]["s_HubId"]) + Environment.NewLine;
                strSMSMessage = strSMSMessage + "Hub Number : " + Convert.ToString(ds.Tables[0].Rows[0]["s_HubPhoneNumber"]) + Environment.NewLine;
                strSMSMessage = strSMSMessage + "DateTime " + dtDateTime;

                MySqlCommand cmd1 = new MySqlCommand();
                cmd1.Connection = conn;
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                cmd1.CommandText = "p_AddAlertSMS";
                cmd1.Parameters.AddWithValue("m_alertMessage", strMessgae);
                cmd1.Parameters.AddWithValue("m_alertStatus", "Pending");
                cmd1.Parameters.AddWithValue("m_handleBy", "");
                cmd1.Parameters.AddWithValue("m_dtDateTime", dtDateTime);
                cmd1.Parameters.AddWithValue("m_siteName", Convert.ToString(ds.Tables[0].Rows[0]["s_SiteName"]));
                cmd1.Parameters.AddWithValue("m_floorName", Convert.ToString(ds.Tables[0].Rows[0]["s_HubId"]));
                cmd1.Parameters.AddWithValue("m_intSensorId", 0);
                cmd1.ExecuteNonQuery();
                cmd1 = null;

                //if (strMessgae.Contains("AC power Shutdown"))
                if (strMessgae.Contains("AC power"))
                { 
                    //AC Power Off Details
                    bool blnStatus1 = fnSendSMS("6597291320", strSMSMessage);
                    blnStatus1 = fnSendSMS("6584995991", strSMSMessage);
                    //blnStatus1 = fnSendSMS("6583987391", strSMSMessage);
                    blnStatus1 = fnSendSMS("6582672062", strSMSMessage);
                    //blnStatus1 = fnSendSMS("6592305656", strSMSMessage);
                }
            }

            
            cmd = null;
            conn.Close();
            da = null;
            ds = null;
        }
    }


    private void fnPrepareSMS_SRSSRTALert(string strMobileNumber, string strMessgae, DateTime dtDateTime)
    {
    }

    /*    //Commzgate Method
    private bool fnSendSMS(string strPhoneNumber,string strSMSMessage)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.commzgate.net/gateway/SendMsg");
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        string postData = "ID=56420002&Password=smsgate1014&Mobile=" + strPhoneNumber + "&Type=A&Message=" + Server.UrlEncode(strSMSMessage);
        byte[] bytes = Encoding.UTF8.GetBytes(postData);
        request.ContentLength = bytes.Length;

        Stream requestStream = request.GetRequestStream();
        requestStream.Write(bytes, 0, bytes.Length);

        WebResponse response = request.GetResponse();
        Stream stream = response.GetResponseStream();
        StreamReader reader = new StreamReader(stream);

        var result = reader.ReadToEnd();

        stream.Dispose();
        reader.Dispose();

        if (Convert.ToString(result).Contains("01010"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }*/

    private bool fnSendSMS(string strPhoneNumber, string strSMSMessage)
    {

        if (strPhoneNumber.Substring(0, 2).Equals("60")) // Avoid Send SMS to MAS
        {
            return false;
        }

        if (strPhoneNumber.Substring(0, 2).Equals("85")) // Avoid Send SMS to HK
        {
            return false;
        }

        if (strPhoneNumber.Length < 10)  // Avoid Send SMS if Less Digit
        {
            return false;
        }

        //Remove Site Alert
        string t_noAlertSite = Convert.ToString(System.Web.Configuration.WebConfigurationManager.AppSettings["NoAlertSite"].ToString());

        string[] t_noAlertSiteName = t_noAlertSite.Split(';');

        foreach (var t_sitName in t_noAlertSiteName)
        {
            if(strSMSMessage.Contains(t_sitName))
            {
                return false;
            }
        }
        //---------------
        /*
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.1.110/source/send_sms.php");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Credentials = new NetworkCredential("admin", "admin");
                string postData = "username=root&pwd=foxbox&from=RodentEyeGateWay&nphone=" + strPhoneNumber + "&outCh=GSM7&testo=" + strSMSMessage;
                byte[] bytes = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = bytes.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);

                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                var result = reader.ReadToEnd();

                stream.Dispose();
                reader.Dispose();

                return true;

                //-----------
                */

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.1.41:9710/http/send-message");
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.Credentials = new NetworkCredential("admin", "admin1234");
        string postData = "username=admin&password=admin1234&to=" + strPhoneNumber + "&message-type=sms.automatic&message=" + strSMSMessage;
        byte[] bytes = Encoding.UTF8.GetBytes(postData);
        request.ContentLength = bytes.Length;

        Stream requestStream = request.GetRequestStream();
        requestStream.Write(bytes, 0, bytes.Length);

        WebResponse response = request.GetResponse();
        Stream stream = response.GetResponseStream();
        StreamReader reader = new StreamReader(stream);

        var result = reader.ReadToEnd();

        stream.Dispose();
        reader.Dispose();

        return true;



    }

    

    private void fnCreateAlert(string strSensorId, int intHubId, int intSMSType, string HubPhoneNumber)
    {
        //Find the Data
        String t_locationDetail = "";
        try
        {
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
            {
                conn.Open();
                DataSet ds = new DataSet();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetSiteTechncian";
                cmd.Parameters.AddWithValue("m_HubId", intHubId);
                cmd.Parameters.AddWithValue("m_SensorId", strSensorId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    t_locationDetail = "Rodent Caught Alert Following Location : \n Site Detail : ";
                    t_locationDetail = t_locationDetail + Convert.ToString(ds.Tables[1].Rows[0]["s_SiteName"]) + Environment.NewLine + "Hub Number :";
                    t_locationDetail = t_locationDetail + Convert.ToString(ds.Tables[1].Rows[0]["s_HubId"]) + Environment.NewLine + "Sensor Location :";
                    t_locationDetail = t_locationDetail + Convert.ToString(ds.Tables[1].Rows[0]["s_SensorLocation"]) + Environment.NewLine + "Date Time :";
                    t_locationDetail = t_locationDetail + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                }

                //Send Rodent Alert Alert Tech
                string strPhoneNumber_Alert = Convert.ToString(ds.Tables[0].Rows[0][0]);
                if (!strPhoneNumber_Alert.Equals("0"))
                {
                    if (strPhoneNumber_Alert.Length >= 10)
                    {
                        fnSendSMS(strPhoneNumber_Alert, t_locationDetail);
                    }
                }

                //Send Rodent Alert Deployment
                string strPhoneNumber_Dep = Convert.ToString(ds.Tables[3].Rows[0][0]);
                
                if (!strPhoneNumber_Dep.Equals("0"))
                {
                    if (strPhoneNumber_Dep.Length >= 10)
                    {
                        fnSendSMS(strPhoneNumber_Dep, t_locationDetail);
                    }
                }


                //Send Rodent Alert to Internal User
                string t_trapAlertNo = Convert.ToString(System.Web.Configuration.WebConfigurationManager.AppSettings["smsTrapAlert"].ToString());

                string[] t_trapAlertNoArr = t_trapAlertNo.Split(';');

                foreach (var t_phone in t_trapAlertNoArr)
                {
                    fnSendSMS(t_phone, t_locationDetail);
                }

                cmd.Dispose();
                conn.Dispose();
            }
            
            //--------------
        }
        catch (Exception ex)
        {
            Response.Write("Error Sending Rodent Caught Alert" + ex.ToString());
        }
    }
    private void fnCreateAppointment(string strSensorId, int intHubId, int intSMSType, string HubPhoneNumber)
    {
        string connetionString = null;
        //Find the Data
        int t_rcNoContract = 0, t_contratPest = 0, t_RcNoCust = 0, t_RcNoJobLog = 0, t_RcNoEmp = 0;
        String t_locationDetail = "", t_ItemType = "";
        String t_svcEmail = "", t_svcAdd = "", t_contTech = "", t_contactPerson = "", t_mobile = "";
        String t_company = "";
        String t_Scheduler = "", t_techEmail = "", t_schEmail = "", t_techMobile = "";

        SqlConnection cnn;
        try
        {
            //link to Ragu Prog

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
            {
                conn.Open();
                DataSet ds = new DataSet();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetSiteTechncian";
                cmd.Parameters.AddWithValue("m_HubId", intHubId);
                cmd.Parameters.AddWithValue("m_SensorId", strSensorId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);

                DataSet ds1 = new DataSet();
                MySqlCommand cmd1 = new MySqlCommand();
                cmd1.Connection = conn;
                cmd1.CommandText = "select i_EHalosLink, i_EHalosConnStr from hub where i_HubId=" + intHubId;
                MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                da1.Fill(ds1);

                
                t_rcNoContract = Convert.ToInt32(ds1.Tables[0].Rows[0]["i_EHalosLink"]);
                connetionString = Convert.ToString(ds1.Tables[0].Rows[0]["i_EHalosConnStr"]);

                t_locationDetail = "Rodent Caught Alert Following Location : \n Site Detail : " + t_locationDetail + Convert.ToString(ds.Tables[1].Rows[0]["s_SiteName"]) + " - " + Convert.ToString(ds.Tables[1].Rows[0]["s_SensorLocation"]) + Environment.NewLine;

                cmd.Dispose();
                cmd1.Dispose();
                conn.Dispose();
            }

            
            

            //Set Value
            //t_rcNoContract = 23637;
            //connetionString = "Data Source=118.201.57.142,1433;Network Library = DBMSSOCN; Initial Catalog = PestCtrlData;User ID = sa; Password = Tong!@34";

            cnn = new SqlConnection(connetionString);
            cnn.Open();

            string sqlCont = "SELECT RcNoCust,svcEmail,serviceAdd, contractTech, accountIC, mobileNo, contractPest FROM tbl_contract WHERE (RcNoContract='" + t_rcNoContract + "')";
            SqlCommand commandContract = new SqlCommand(sqlCont, cnn);
            SqlDataReader dataReaderContract = commandContract.ExecuteReader();
            while (dataReaderContract.Read())
            {
                t_RcNoCust = Convert.ToInt32(dataReaderContract.GetValue(0));
                t_svcEmail = Convert.ToString(dataReaderContract.GetValue(1));
                t_svcAdd = Convert.ToString(dataReaderContract.GetValue(2));
                t_contTech = Convert.ToString(dataReaderContract.GetValue(3));
                t_contactPerson = Convert.ToString(dataReaderContract.GetValue(4));
                t_mobile = Convert.ToString(dataReaderContract.GetValue(5));
                t_ItemType = Convert.ToString(dataReaderContract.GetValue(6));
                string[] res = t_ItemType.Split(',');
                for (int i = 0; i < res.Length; i++)
                {
                    if(res[i].Contains("ROD"))
                    {
                        t_ItemType = res[i].Trim();
                    }
                    
                }
            }
            dataReaderContract.Close();
            commandContract.Dispose();

           
            string sqlTech = "SELECT RcNoEmp,t2,t3,mobileNo FROM tbl_emp WHERE(Na= '" + t_contTech + "')";
            SqlCommand commandTech = new SqlCommand(sqlTech, cnn);
            SqlDataReader dataReaderTech = commandTech.ExecuteReader();
            while (dataReaderTech.Read())
            {
                t_RcNoEmp = Convert.ToInt32(dataReaderTech.GetValue(0));
                t_Scheduler = Convert.ToString(dataReaderTech.GetValue(1));
                t_techEmail = Convert.ToString(dataReaderTech.GetValue(2));
                t_techMobile = Convert.ToString(dataReaderTech.GetValue(3));
            }
            dataReaderTech.Close();
            commandTech.Dispose();

            //Scheduler Info


            string sqlSch = "SELECT t3 FROM tbl_emp WHERE (Na='" + t_Scheduler + "')";
            SqlCommand commandSch = new SqlCommand(sqlSch, cnn);
            SqlDataReader dataReaderSch = commandSch.ExecuteReader();
            while (dataReaderSch.Read())
            {
                t_schEmail = Convert.ToString(dataReaderSch.GetValue(0));
            }
            dataReaderSch.Close();
            commandSch.Dispose();


            string sqlCust = "SELECT company FROM tbl_customerProfile WHERE (RcNoCust='" + t_RcNoCust + "')";
            SqlCommand commandCust = new SqlCommand(sqlCust, cnn);
            SqlDataReader dataReaderCust = commandCust.ExecuteReader();
            while (dataReaderCust.Read())
            {
                t_company = Convert.ToString(dataReaderCust.GetValue(0));
            }
            dataReaderCust.Close();
            commandCust.Dispose();

            //Adding into JobLog
            string sqlJobLog = "insert into tbl_jobLog (RcNoCust,custOfficerName,custOfficerID, RcNoContract, ReportType, purpose, JSDate, jobLogStatus, logType, technician,serviceDate,pestJob,serviceAdd,t1,t2,n2,UserID,PestValue,TotalJob,JobComplete, TimeIn, TimeOut, HourTaken, Scheduled, concatTech, ReportNoAuto) values (@RcNoCust,@custOfficerName,@custOfficerID,@RcNoContract,@ReportType,@purpose,@JSDate,@jobLogStatus,@logType,@technician,@serviceDate,@pestJob,@serviceAdd,@t1,@t2,@n2,@UserID,@PestValue,@TotalJob,@JobComplete,@TimeIn,@TimeOut,@HourTaken,@Scheduled,@concatTech,@ReportNoAuto)";
            SqlCommand commandJobLog = new SqlCommand(sqlJobLog, cnn);

            commandJobLog.Parameters.AddWithValue("@RcNoCust", t_RcNoCust);
            commandJobLog.Parameters.AddWithValue("@custOfficerName", "Auto Trigger");
            commandJobLog.Parameters.AddWithValue("@custOfficerID", 0);
            commandJobLog.Parameters.AddWithValue("@RcNoContract", t_rcNoContract);
            commandJobLog.Parameters.AddWithValue("@ReportType", "Contract");
            commandJobLog.Parameters.AddWithValue("@purpose", "Follow Up");
            commandJobLog.Parameters.AddWithValue("@JSDate", DateTime.Now);
            commandJobLog.Parameters.AddWithValue("@jobLogStatus", "Current");
            commandJobLog.Parameters.AddWithValue("@logType", "Contract");
            commandJobLog.Parameters.AddWithValue("@technician", t_contTech);
            commandJobLog.Parameters.AddWithValue("@serviceDate", DateTime.Now.ToString("MM/dd/yyyy"));
            commandJobLog.Parameters.AddWithValue("@pestJob", t_ItemType);
            commandJobLog.Parameters.AddWithValue("@serviceAdd", t_svcAdd);
            commandJobLog.Parameters.AddWithValue("@t1", t_contactPerson);
            commandJobLog.Parameters.AddWithValue("@t2", t_mobile);
            commandJobLog.Parameters.AddWithValue("@n2", "-1");
            commandJobLog.Parameters.AddWithValue("@UserID", "Auto Trigger");
            commandJobLog.Parameters.AddWithValue("@PestValue", 0);
            commandJobLog.Parameters.AddWithValue("@TotalJob", 0);
            commandJobLog.Parameters.AddWithValue("@JobComplete", "0");
            commandJobLog.Parameters.AddWithValue("@TimeIn", DateTime.Now.AddHours(2).ToString("HH:mm"));
            commandJobLog.Parameters.AddWithValue("@TimeOut", DateTime.Now.AddHours(2.5).ToString("HH:mm"));
            commandJobLog.Parameters.AddWithValue("@HourTaken", "00:30");
            commandJobLog.Parameters.AddWithValue("@Scheduled", "Call & Fix");
            commandJobLog.Parameters.AddWithValue("@concatTech", t_contTech);
            commandJobLog.Parameters.AddWithValue("@ReportNoAuto", "1");

            int result = commandJobLog.ExecuteNonQuery();
            commandJobLog.Dispose();

            //Find JobLog

            string sqlfindJL = "SELECT Max(RcNoJobLog) as MaxRcNoJobLog FROM tbl_jobLog WHERE (RcNoContract='" + t_rcNoContract + "' AND logType='Contract')";
            SqlCommand commandfindJL = new SqlCommand(sqlfindJL, cnn);
            SqlDataReader dataReaderfindJL = commandfindJL.ExecuteReader();
            while (dataReaderfindJL.Read())
            {
                t_RcNoJobLog = Convert.ToInt32(dataReaderfindJL.GetValue(0));
            }
            dataReaderfindJL.Close();
            commandfindJL.Dispose();


            //Job Log Pest

            //Adding into JobLog
            string sqlJobLogPest = "insert into tbl_jobLogPest (RcNoJobLog, Pest, purpose, treatment, degree, value, rcnocjw, origin) values (@RcNoJobLog,@Pest, @purpose, @treatment, @degree, @value, @rcnocjw, @origin)";
            SqlCommand commandJobLogPest = new SqlCommand(sqlJobLogPest, cnn);

            commandJobLogPest.Parameters.AddWithValue("@RcNoJobLog", t_RcNoJobLog);
            commandJobLogPest.Parameters.AddWithValue("@Pest", t_ItemType);
            commandJobLogPest.Parameters.AddWithValue("@purpose", "Follow Up");
            commandJobLogPest.Parameters.AddWithValue("@treatment", "Inactive");
            commandJobLogPest.Parameters.AddWithValue("@degree", "0");
            commandJobLogPest.Parameters.AddWithValue("@value", "0");
            commandJobLogPest.Parameters.AddWithValue("@rcnocjw", t_rcNoContract);
            commandJobLogPest.Parameters.AddWithValue("@origin", "C - Pest");

            int result1 = commandJobLogPest.ExecuteNonQuery();
            commandJobLogPest.Dispose();

            //Tech Prodt
            string sqlTechProd = "insert into tbl_TechProdt (RcNoJobLog, techID, techName, prodValue, earnDate) values (@RcNoJobLog, @techID, @techName, @prodValue, @earnDate)";
            SqlCommand commandTechProd = new SqlCommand(sqlTechProd, cnn);
            commandTechProd.Parameters.AddWithValue("@RcNoJobLog", t_RcNoJobLog);
            commandTechProd.Parameters.AddWithValue("@techID", t_RcNoEmp);
            commandTechProd.Parameters.AddWithValue("@techName", t_contTech);
            commandTechProd.Parameters.AddWithValue("@prodValue", 0);
            commandTechProd.Parameters.AddWithValue("@earnDate", DateTime.Now.ToString("MM/dd/yyyy"));

            int result2 = commandTechProd.ExecuteNonQuery();
            commandTechProd.Dispose();

            t_locationDetail = t_locationDetail + "-" + t_company + "-" + "Service Address :" + t_svcAdd;
            fnSendSMS(t_techMobile, t_locationDetail);
        }
        catch (Exception ex)
        {
            Response.Write("Error Create Appointment" + ex.ToString());
        }
    }
    
    private void fnSendEmail(string strSensorId,string strSMSMessage,string strUserName, string strEmail)
    {
        string strSMTPServer = Convert.ToString(ConfigurationManager.AppSettings["SMTPHost"]);
        string strSMTPPort = Convert.ToString(ConfigurationManager.AppSettings["SMTPPort"]);
        string strSMTPUserName = Convert.ToString(ConfigurationManager.AppSettings["SMTPUserName"]);
        string strSMTPPassword = Convert.ToString(ConfigurationManager.AppSettings["SMTPUserPassword"]);
        string strFromEmailAddress = Convert.ToString(ConfigurationManager.AppSettings["FromEmailAddress"]);
        
        

        string strMessage = "Dear " + strUserName + ",\n\n";
        strMessage = strMessage + strSMSMessage + ",\n";
        strMessage = strMessage + "\n";
        strMessage = strMessage + "Regards,\nRodentEye Support Team";

        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient(strSMTPServer);

        mail.From = new MailAddress(strFromEmailAddress);
        mail.To.Add(strEmail);
        mail.Subject = "RodentEye - Alert - " + strSensorId;
        mail.Body = strSMSMessage;

        SmtpServer.Port = Convert.ToInt32(strSMTPPort);
        SmtpServer.Credentials = new System.Net.NetworkCredential(strSMTPUserName, strSMTPPassword);
        SmtpServer.EnableSsl = true;

        SmtpServer.Send(mail);
    }

}