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
using System.Net.Mail;
using System.Net;

public partial class services_receive_wifi_data : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string strMacNumber = Convert.ToString(Request.Form["MacNumber"]);
            string strCpuID = Convert.ToString(Request.Form["CpuID"]);
	    string strBatStatus = Convert.ToString(Request.Form["BatStatus"]);	
            string strSensorData = Convert.ToString(Request.Form["SensorData"]);
	    
	fnSendEmail(strMacNumber,strCpuID, strBatStatus, strSensorData);


	    MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString_test"].ConnectionString);
            conn.Open();
            try
            {
		MySqlCommand cmdMsg = new MySqlCommand();
                cmdMsg.Connection = conn;
                cmdMsg.CommandType = System.Data.CommandType.StoredProcedure;
                cmdMsg.CommandText = "p_SaveMsg";
                            
                cmdMsg.Parameters.AddWithValue("m_macAdd", strMacNumber);
                cmdMsg.Parameters.AddWithValue("m_cpuAdd", strCpuID);
                cmdMsg.Parameters.AddWithValue("m_battStatus", strBatStatus);
		cmdMsg.Parameters.AddWithValue("m_sensorData", strSensorData);
                cmdMsg.ExecuteNonQuery();
		  
                Response.Write("OK");
		fnSendEmail(strMacNumber,strCpuID, strBatStatus, strSensorData);
			
            }
            catch(Exception exp)
            {
                 Response.Write("ERROR Upload" + exp.ToString());
            }
	    conn.Close();
   	}
        catch (Exception ex)
        {
            Response.Write("ERROR. Getting Value" + ex.ToString());
        }
    }

    private void fnSendEmail(string strMacNumber,string strCpuID,string strBatStatus, string strSensorData)
    {
        string strSMTPServer = "smtp.gmail.com";
        string strSMTPPort = "587";
        string strSMTPUserName = "support@pestechholding.com";
        string strSMTPPassword = "supp4321";
        string strFromEmailAddress ="support@pestechholding.com";
        

        string strMessage = "Mac Number : " + strMacNumber + "\n";
        strMessage = strMessage + "CPU ID : " + strCpuID + "\n";
        strMessage = strMessage + "Battery Status : " + strBatStatus + "\n";
        strMessage = strMessage + "Sensor Data : " + strSensorData + "\n";

        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient(strSMTPServer);

        mail.From = new MailAddress(strFromEmailAddress);
        mail.To.Add("mike@pestechholding.com");
	mail.To.Add("ragu@pestechholding.com");
        mail.Subject = "Wifi System Msg - " + strMacNumber;
        mail.Body = strMessage;

        SmtpServer.Port = Convert.ToInt32(strSMTPPort);
        SmtpServer.Credentials = new System.Net.NetworkCredential(strSMTPUserName, strSMTPPassword);
        SmtpServer.EnableSsl = true;

        SmtpServer.Send(mail);

    }

}