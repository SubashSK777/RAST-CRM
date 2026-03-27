using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using MySql.Data.MySqlClient;
using System.Configuration;
using RAST.DAL;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Mail;

public partial class services_sensor : System.Web.UI.Page
{
    EncryptionHelper objpwd = new EncryptionHelper();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        int intRequestId = Convert.ToInt32(Request.QueryString["id"]);

        switch (intRequestId)
        {
            case 1:
                fnSaveSensor();
                break;
            case 2:
                fnDeleteSensor();
                break;
            case 3:
                fnGetSensor();
                break;
            case 4:
                fnGetSensorForFloorMap();
                break;
            case 5:
                fnUpdBatteryChangeDate();  // Ragu 24 Jan 2018 Battery Update Event
                break;
            case 6:
                fnSendSMS();
                break;
            case 7:
                fnAlertSMS();
                break;
            case 8:
                fnUpdSesnorLog();
                break;
            case 9:
                fnAlertSMS1();
                break;
            case 10:
                fnAlertSMS2();
                break;
            case 11:
                fnUpdSesnorActive();
                break;
            case 12:
                fnUpdSensorXPos();
                break;
            case 13:
                fnUpdSensorYPos();
                break;
            case 14:
                fnDelSensorData();
                break;
            case 15:
                fnFindHub();
                break;
            case 16:
                fnFindSensor();
                break;
            

          
        }
    }

 

    private void fnFindHub()
    {
        int intOrganizationId;
        if (Request.QueryString["organizationid"].Length <= 5)
        {
            intOrganizationId = Convert.ToInt32(Request.QueryString["organizationid"]);

            
        }
        else
        {
            intOrganizationId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["organizationid"]));
        }

        
        string strhubSerialNumber = Convert.ToString(Request.QueryString["hubserialnumber"]);

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString_license"].ConnectionString))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select count(*) as RecCount from hubdetail WHERE i_OrganizationId = '" + intOrganizationId + "' and  hubSerialNumber = '" + strhubSerialNumber + "'";
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            String tmp = ds.Tables[0].Rows[0]["RecCount"].ToString();
            Response.Write(tmp);
        }

    }

    private void fnFindSensor()
    {
        //int intOrganizationId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["organizationid"]));
        int intOrganizationId;
        if (Request.QueryString["organizationid"].Length <= 5)
        {
            intOrganizationId = Convert.ToInt32(Request.QueryString["organizationid"]);

            
        }
        else
        {
            intOrganizationId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["organizationid"]));
        }

        string strsensorQRNumber = Convert.ToString(Request.QueryString["sensorqrnumber"]);

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString_license"].ConnectionString))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select count(*) as RecCount from sensordetail WHERE i_OrganizationId = '" + intOrganizationId + "' and  sensorQRNumber = '" + strsensorQRNumber + "'";
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            String tmp = ds.Tables[0].Rows[0]["RecCount"].ToString();
            Response.Write(tmp);
        }

    }




    private void fnAlertSMS1()
    {
        int intidalertSms = Convert.ToInt32(Request.QueryString["idalertSms"]);
        string strHandleBy = Convert.ToString(Request.QueryString["handleBy"]);
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE alertsms SET handleBy = '" + strHandleBy + "' WHERE idalertSms = '" + intidalertSms + "'";
            int numRowsUpdated = cmd.ExecuteNonQuery();
            conn.Close();
        }

    }

    private void fnAlertSMS2()
    {
        int intidalertSms = Convert.ToInt32(Request.QueryString["idalertSms"]);
        string stralertStatus = Convert.ToString(Request.QueryString["alertStatus"]);
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE alertsms SET alertStatus = '" + stralertStatus + "' WHERE idalertSms = '" + intidalertSms + "'";
            int numRowsUpdated = cmd.ExecuteNonQuery();
            conn.Close();
        }

    }

    

    private void fnUpdSesnorActive()   // Ragu 17 Apr 19 - Sesnor Active
    {
        int intSensorId=0;
        if (Request.QueryString["sensorid"].Length <= 5)
        {
            intSensorId = Convert.ToInt32(Request.QueryString["sensorid"]);
            
        }
        else
        {
            intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));
        }
        int intActive = 0;
        if (Convert.ToBoolean(Request.QueryString["active"])==true)
            intActive = 0;
        else
            intActive = 1;

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE sensor SET active= '" + intActive + "' WHERE i_SensorId = " + intSensorId;
            int numRowsUpdated = cmd.ExecuteNonQuery();
            conn.Close();
        }
    }

    private void fnUpdSesnorLog()   // Ragu 24 Jan 2018 - Update Battery Change Date - Reset Counter
    {

        //int intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));

        int intSensorId = 0;
        if (Request.QueryString["sensorid"].Length <= 5)
        {
            intSensorId = Convert.ToInt32(Request.QueryString["sensorid"]);

            
        }
        else
        {
            intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));
        }


        string strSensorLog = Convert.ToString(Request.QueryString["SensorLog"]);
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE sensor SET SensorLog= '" + strSensorLog + "' WHERE i_SensorId = " + intSensorId;
            int numRowsUpdated = cmd.ExecuteNonQuery();
            conn.Close();
        }
    }

    private void fnUpdSensorXPos()   // Ragu 29 Apr 2019
    {
        //int intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));

        int intSensorId = 0;
        if (Request.QueryString["sensorid"].Length <= 5)
        {
            intSensorId = Convert.ToInt32(Request.QueryString["sensorid"]);

            
        }
        else
        {
            intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));
        }


        int intXpos = Convert.ToInt32(Request.QueryString["xpos"]);
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE sensor SET i_X= '" + intXpos + "' WHERE i_SensorId = " + intSensorId;
            int numRowsUpdated = cmd.ExecuteNonQuery();
            conn.Close();
        }
    }

    private void fnUpdSensorYPos()   // Ragu 29 Apr 2019
    {
        //int intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));

        int intSensorId = 0;
        if (Request.QueryString["sensorid"].Length <= 5)
        {
            intSensorId = Convert.ToInt32(Request.QueryString["sensorid"]);
            
        }
        else
        {
            intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));
        }

        int intYpos = Convert.ToInt32(Request.QueryString["ypos"]);
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE sensor SET i_Y= '" + intYpos + "' WHERE i_SensorId = " + intSensorId;
            int numRowsUpdated = cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
    private void fnAlertSMS()
    {
        int intidalertSms = Convert.ToInt32(Request.QueryString["idalertSms"]);
        string strHandleBy = Convert.ToString(Request.QueryString["handleBy"]);
        string stralertStatus = Convert.ToString(Request.QueryString["alertStatus"]);
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE alertsms SET alertStatus = " + stralertStatus + " , handleBy = " + strHandleBy + "WHERE idalertSms = " + intidalertSms;
            int numRowsUpdated = cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
    private void fnSaveSensor()
    {
        //int intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["intsensorid"]));
        int intSensorId = 0;
        if (Request.QueryString["sensorid"].Length <= 5)
        {
            intSensorId = Convert.ToInt32(Request.QueryString["sensorid"]);
            
        }
        else
        {
            intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));
        }


        //int intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));

        int intSiteId = 0;
        if (Request.QueryString["siteid"].Length <= 5)
        {
            intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
            
        }
        else
        {
            intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
        }

        int intHubId = 0;
       

        if (Request.QueryString["hubid"].Length <= 5)
        {
            intHubId = Convert.ToInt32(Request.QueryString["hubid"]);

            
        }
        else
        {
            intHubId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["hubid"]));
        }

        //int intHubId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["hubid"]));
        string strSensorId = Convert.ToString(Request.QueryString["sensorname"]);
        string strsensortype = Convert.ToString(Request.QueryString["sensortype"]);
        int intXPix = Convert.ToInt32(Request.QueryString["min"]);
        int intYPix = Convert.ToInt32(Request.QueryString["max"]);
        int intMinThreshold = Convert.ToInt32(Request.QueryString["th_x"].ToString().Replace("px", ""));
        int intMaxThreshold = Convert.ToInt32(Request.QueryString["th_y"].ToString().Replace("px", ""));
        string strunit = Convert.ToString(Request.QueryString["unit"]);
        int intstatus = Convert.ToInt16(Request.QueryString["status"]);

        //int intX = Convert.ToInt32(Request.QueryString["XPix"].ToString().Replace("px", ""));
        //int intY = Convert.ToInt32(Request.QueryString["YPix"].ToString().Replace("px", ""));
        string t_x = Request.QueryString["XPix"].ToString().Replace("px", "");
        string t_y = Request.QueryString["YPix"].ToString().Replace("px", "");

        int intX = Convert.ToInt32(Convert.ToDouble(t_x));
        int intY = Convert.ToInt32(Convert.ToDouble(t_y));


        //int intFloorId = Convert.ToInt32(Request.QueryString["floorid"]);
        int intFloorId=0;

        if (Request.QueryString["floorid"].Length <= 5)
        {
            intFloorId = Convert.ToInt32(Request.QueryString["floorid"]);
            
        }
        else
        {
            intFloorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["floorid"]));
        }

        string strSensorLocation = Convert.ToString(Request.QueryString["sensorlocation"]);
        string strQRScanId = Convert.ToString(Request.QueryString["qrCode"]);
        





        bool bstatus;
        if (intstatus == 1)
            bstatus = true;
        else
            bstatus = false;
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            Sensors objSensor = new Sensors();
            objSensor.ObjCon = conn;
            objSensor.Sensor = intSensorId;
            objSensor.SiteId = intSiteId;
            objSensor.HubId = intHubId;
            objSensor.SensorId = strSensorId;
            objSensor.MinThresholdLevel = intMinThreshold;
            objSensor.MaxThresholdLevel = intMaxThreshold;
            objSensor.X = intX;
            objSensor.Y = intY;
            objSensor.Status = bstatus;
            objSensor.unit = strunit;
            objSensor.SensorType = strsensortype;
            objSensor.XRange = intXPix;
            objSensor.YRange = intYPix;
            objSensor.SensorLocation = strSensorLocation;
            objSensor.qrCode = strQRScanId;
            objSensor.FloorId = intFloorId;
            int intReturnValue = objSensor.Add();
            Response.Write(intReturnValue.ToString());

            objSensor = null;
            conn.Close();

        }




    }

    private void fnDeleteSensor()
    {

        //int intSensorId = Convert.ToInt32(Request.QueryString["sensorid"]);

        int intSensorId = 0;
        if (Request.QueryString["sensorid"].Length <= 5)
        {
            intSensorId = Convert.ToInt32(Request.QueryString["sensorid"]);
           
        }
        else
        {
            intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));
        }


        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            Sensors objSensor = new Sensors();
            objSensor.ObjCon = conn;
            objSensor.Sensor = intSensorId;
            int intReturnValue = objSensor.Delete();

            objSensor = null;
            conn.Close();

        }
    }
    public string ConvertDataTabletoString(System.Data.DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (System.Data.DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (System.Data.DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }
    private void fnGetSensor()
    {
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {

            //int intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));
            int intSensorId = 0;
            if (Request.QueryString["sensorid"].Length <= 5)
            {
                intSensorId = Convert.ToInt32(Request.QueryString["sensorid"]);

                
            }
            else
            {
                intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));
            }

            conn.Open();
            Sensors objSensor = new Sensors();
            objSensor.ObjCon = conn;
            objSensor.Sensor = intSensorId;
            DataSet dsData = objSensor.GetSensorData();
            objSensor = null;
            conn.Close();

            Response.Write(ConvertDataTabletoString(dsData.Tables[0]));

        }

    }

    private void fnGetSensorForFloorMap()
    {
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            int intFloorMapId;
            if (Request.QueryString["floormapid"]=="")  
            {
                intFloorMapId = 0;
            }
            else
            {
                //intFloorMapId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["floormapid"]));
                if (Request.QueryString["floormapid"].Length <= 5)
                {
                    intFloorMapId = Convert.ToInt32(Request.QueryString["floormapid"]);
                    
                }
                else
                {
                    intFloorMapId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["floormapid"]));
                }
            }

            int intSiteId;// = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));

            if (Request.QueryString["siteid"].Length <= 5)
            {
                intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
               
            }
            else
            {
                intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            }


            conn.Open();
            Sensors objSensor = new Sensors();
            objSensor.ObjCon = conn;
            objSensor.SiteId = intSiteId;
            objSensor.FloorId = intFloorMapId;
            DataSet dsData = objSensor.fnGetSensorsForFloorMap(intFloorMapId, intSiteId);
            objSensor = null;
            conn.Close();

            Response.Write(ConvertDataTabletoString(dsData.Tables[1]));

        }
    }


    private void fnUpdBatteryChangeDate()   // Ragu 24 Jan 2018 - Update Battery Change Date - Reset Counter
    {
        //int intSensorId = Convert.ToInt32(Request.QueryString["sensorid"]);
        int intSensorId = 0;
        if (Request.QueryString["sensorid"].Length <= 5)
        {
            intSensorId = Convert.ToInt32(Request.QueryString["sensorid"]);
            
        }
        else
        {
            intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));
        }

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE sensor SET dtSensorUpdTime = now(), actualCount=0, triggerCount=0 WHERE i_SensorId = " + intSensorId;
            int numRowsUpdated = cmd.ExecuteNonQuery();
            conn.Close();
        }
    }

    private void fnDelSensorData()   // Ragu 30 APr 2019
    {
        //int intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));
        int intSensorId = 0;
        if (Request.QueryString["sensorid"].Length <= 5)
        {
            intSensorId = Convert.ToInt32(Request.QueryString["sensorid"]);

            
        }
        else
        {
            intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));
        }


        string dt_Date = Convert.ToString(Request.QueryString["delDate"]);
        string dt_Time = Convert.ToString(Request.QueryString["delTime"]);
        string dt_From = dt_Date + " " + dt_Time + ":00:00";
        string dt_To = dt_Date + " " + dt_Time + ":59:59";

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "delete from sensor_data WHERE i_SensorId = '" + intSensorId + "' and (dt_DateTime >= '" + dt_From + "' and dt_DateTime <= '" + dt_To +"')";
            int numRowsUpdated = cmd.ExecuteNonQuery();
            conn.Close();

            string t_msg = numRowsUpdated + " Records Deleted Between " + dt_From + " to " + dt_To;
            fnSendEmail_DelSensorData(intSensorId+"", t_msg);
        }
    }
    

    private bool fnSendSMS() // Ragu 18 Dec 2018 - Send Status Code to HUB
    {

        string txtPhoneNumber = Convert.ToString(Request.QueryString["strPhoneNumber"]);
        string txtMessage = Convert.ToString(Request.QueryString["strSMSMessage"]);
        txtMessage = txtMessage + "#";
        if (txtPhoneNumber.Substring(0, 2).Equals("60")) // Avoid Send SMS to MAS
        {
            return false;
        }

        if (txtPhoneNumber.Substring(0, 2).Equals("85")) // Avoid Send SMS to HK
        {
            return false;
        }

        if (txtPhoneNumber.Length < 10)  // Avoid Send SMS if Less Digit
        {
            return false;
        }
        /*HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.1.110/source/send_sms.php");
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.Credentials = new NetworkCredential("admin", "admin");
        string postData = "username=root&pwd=foxbox&from=RodentEyeGateWay&nphone=" + txtPhoneNumber + "&outCh=GSM7&testo=" + txtMessage;
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

        return true;*/

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.1.41:9710/http/send-message");
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.Credentials = new NetworkCredential("admin", "admin1234");
        string postData = "username=admin&password=admin1234&to=" + txtPhoneNumber + "&message-type=sms.automatic&message=" + txtMessage;
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
    private void fnSendEmail_DelSensorData(string strSensorId, string strSMSMessage)
    {
        string strSMTPServer = Convert.ToString(ConfigurationManager.AppSettings["SMTPHost"]);
        string strSMTPPort = Convert.ToString(ConfigurationManager.AppSettings["SMTPPort"]);
        string strSMTPUserName = Convert.ToString(ConfigurationManager.AppSettings["SMTPUserName"]);
        string strSMTPPassword = Convert.ToString(ConfigurationManager.AppSettings["SMTPUserPassword"]);
        string strFromEmailAddress = Convert.ToString(ConfigurationManager.AppSettings["FromEmailAddress"]);



        string strMessage = "Sensor Data Deleted : Sensor Number " + strSensorId;
        strMessage = strMessage + "\n"+ strSMSMessage;

        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient(strSMTPServer);

        mail.From = new MailAddress(strFromEmailAddress);
        mail.To.Add("support@pestech.com.sg");
        mail.Subject = "RodentEye - Deletion Alert - " + strSensorId;
        mail.Body = strSMSMessage;

        SmtpServer.Port = Convert.ToInt32(strSMTPPort);
        SmtpServer.Credentials = new System.Net.NetworkCredential(strSMTPUserName, strSMTPPassword);
        SmtpServer.EnableSsl = true;

        SmtpServer.Send(mail);
    }
}