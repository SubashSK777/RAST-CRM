using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using RAST.DAL;
using RAST.Utilities;
using System.Data;

public partial class services_user : System.Web.UI.Page
{
    string strMysqlConnection = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString();
    EncryptionHelper objpwd = new EncryptionHelper();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        int intUserId = 0;
        string exepurl = Request.Url.ToString();
        if (Request.QueryString["id"].Equals("0"))
        {
            intUserId = 0;
        }
        else
        {
            intUserId = Convert.ToInt16(objpwd.DecryptData(Request.QueryString["id"]).Trim());
        }
        int retVal;
        try
        {
            if (intUserId == 0)
            {
                Save();
            }
            else
            {
                if (Request.QueryString["uname"].Equals("delete"))
                {
                    retVal = Delete();
                    Response.Write(@retVal);
                }
                else
                    Update();
            }
        }
        catch(Exception ex)
        {
            LogExceptions objlog = new LogExceptions();
            using (MySqlConnection MySQLCon = new MySqlConnection(strMysqlConnection))
            {
                MySQLCon.Open();
                objlog.conn = MySQLCon;
                objlog.SaveLogExceptions(ex, exepurl);
            }
        }

    }

    protected void Save()
    {
        Encryption objEncryption = new Encryption();
 
        string name = Convert.ToString(Request.QueryString["uname"]);
        string email = Convert.ToString(Request.QueryString["email"]);
        string password = Convert.ToString(Request.QueryString["password"]);
        string phone = Convert.ToString(Request.QueryString["phone"]);
        int organizationId = Convert.ToInt16(Request.QueryString["organization"]);
        int roleId = Convert.ToInt16(Request.QueryString["role"]);
        bool status = Convert.ToBoolean(Convert.ToInt16(Request.QueryString["bstatus"]));

        using(MySqlConnection MySQLCon = new MySqlConnection(strMysqlConnection))
        {
            Users objUser = new Users();
            MySQLCon.Open();
            objUser.conn = MySQLCon;
            objUser.Name = name;
            objUser.Email = email;
            objUser.Password = objEncryption.EncryptData(password);
            objUser.Phone = phone;
            objUser.OrganizationId = organizationId;
            objUser.RoleId = roleId;
            objUser.Status = status;

            int intretVal = objUser.Add();

            objUser = null;
            MySQLCon.Close();
        }

        objEncryption = null;


    }

    protected void Update()
    {
        Encryption objEncryption = new Encryption();
 
        int userid = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["id"]));
        string name = Convert.ToString(Request.QueryString["uname"]);
        string email = Convert.ToString(Request.QueryString["email"]);
        string password = Convert.ToString(Request.QueryString["password"]);
        string phone = Convert.ToString(Request.QueryString["phone"]);
        int organizationId = Convert.ToInt16(Request.QueryString["organization"]);
        int roleId = Convert.ToInt16(Request.QueryString["role"]);
        bool status = Convert.ToBoolean(Convert.ToInt16(Request.QueryString["bstatus"]));

        using (MySqlConnection MySQLCon = new MySqlConnection(strMysqlConnection))
        {
            Users objUser = new Users();
            MySQLCon.Open();
            objUser.conn = MySQLCon;
            objUser.UserId = userid;
            objUser.Name = name;
            objUser.Email = email;
            if (password.Trim().Length == 0)
            {
                objUser.Password = "";
            }
            else
            {
                objUser.Password = objEncryption.EncryptData(password);
            }
            objUser.Phone = phone;
            objUser.OrganizationId = organizationId;
            objUser.RoleId = roleId;
            objUser.Status = status;

            int intretVal = objUser.Update();

            objUser = null;
            MySQLCon.Close();
        }
        objEncryption = null;

    }

    protected int Delete()
    {
        int userid = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["id"]));
        int intretVal = 0;
        using (MySqlConnection MySQLCon = new MySqlConnection(strMysqlConnection))
        {
           
            Users objUser = new Users();
            MySQLCon.Open();
            objUser.conn = MySQLCon;
            objUser.UserId = userid;
            DataSet dsSiteUser = objUser.CheckSiteUSer();
            if(dsSiteUser.Tables[0].Rows.Count == 0)
                intretVal = objUser.Delete();            
            objUser = null;
            MySQLCon.Close();
        }
        return intretVal;
    }
   
}