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

public partial class services_receive_command_data : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string strMobileNumber = Convert.ToString(Request.Form["Mobile"]);
            string strMessageType = Convert.ToString(Request.Form["Type"]);
            string strMessage = Convert.ToString(Request.Form["Message"]);
            
            //fnSendEmail(strMacNumber,strCpuID, strBatStatus, strSensorData);


	    MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString);
            conn.Open();
            try
            {
		        MySqlCommand cmdMsg = new MySqlCommand();
                cmdMsg.Connection = conn;
                cmdMsg.CommandType = System.Data.CommandType.StoredProcedure;
                cmdMsg.CommandText = "p_SaveCommand";

                cmdMsg.Parameters.AddWithValue("m_TimeStamp", DateTime.Now);
                cmdMsg.Parameters.AddWithValue("m_MobileNumber", strMobileNumber);
                cmdMsg.Parameters.AddWithValue("m_MessageType", strMessageType);
                cmdMsg.Parameters.AddWithValue("m_Message", strMessage);
                cmdMsg.ExecuteNonQuery();

                String t_secretCode = System.Web.Configuration.WebConfigurationManager.AppSettings["SecretCode"].ToString();

            
                bool blnStatus1 = fnSendSMS(strMobileNumber, t_secretCode + strMessage + "#");
                
                Response.Write("OK");
			
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
        /*HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.1.110/source/send_sms.php");
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

        return true;*/


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