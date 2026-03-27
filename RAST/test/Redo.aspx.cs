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

public partial class services_Redo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string strMacNumber = Convert.ToString(Request.Form["MacNumber"]);
            //string strMacNumber = "2C_F7_F1_1_48_3F";

            var filePath = HttpContext.Current.Server.MapPath(".");
            filePath = filePath + "\\" + strMacNumber + ".txt";
            var filePath_target = HttpContext.Current.Server.MapPath(".");
            filePath_target = filePath_target + "\\" + "Done_" + strMacNumber + ".txt";

            //FileInfo theFile = new FileInfo("E:\\RAST_Test\\test\\" + strMacNumber + ".txt");
            FileInfo theFile = new FileInfo(filePath);
            FileInfo theFileTarget = new FileInfo(filePath_target);

            if (theFileTarget.Exists)
            {
                //theFile.CopyTo("E:\\RAST_Test\\test\\" + strMacNumber + ".txt");
                theFileTarget.CopyTo(filePath);
            }
            else
            {
                Response.Write("No File");
            }

        }
        catch (Exception ex)
        {
            Response.Write("ERROR. Getting Value" + ex.ToString());
        }
    }
}