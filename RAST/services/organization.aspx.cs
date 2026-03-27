using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using RAST.DAL;

public partial class services_organization : System.Web.UI.Page
{
    string strMysqlConnection = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString();
    EncryptionHelper objpwd = new EncryptionHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        int retval;
        if (Request.QueryString["id"].Equals("0"))
        {
            fnSaveOrganization();
        }
        else
        {
            int intOrganizationId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["id"]));



            if (Request.QueryString["orgname"].Equals("delete"))
            {
                retval = fnDeleteOrganization();
                Response.Write(@retval);
                //if (retval == 0)
                //    Response.Write(@"0");  // for user validation
                //else if (retval == 1)
                //    Response.Write(@"1"); // on success, org deleted
                //else
                //    Response.Write(@"2");  // for site validation
            }
            else if (Request.QueryString["orgname"].Equals("addHub"))
            {
                fnAddHub();
            }
            else if (Request.QueryString["orgname"].Equals("viewHub"))
            {
                fnViewActivateHub();
            }
            else if (Request.QueryString["orgname"].Equals("addSensor"))
            {
                fnAddSensor();
            }
            else if (Request.QueryString["orgname"].Equals("viewSensor"))
            {
                fnViewActivateSensor();
            }
            else if (Request.QueryString["orgname"].Equals("getSensor"))
            {
                fnGetActivateSensor();
            }
            else
                fnUpdateOrganization();
        }
    }


    private void fnAddSensor()
    {
        int intOrganizationId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["id"]));
        string s_sensorqrnumber = Convert.ToString(Request.QueryString["sensorqrnumber"]);
        string s_sensoractive = Convert.ToString(Request.QueryString["sensoractive"]);

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString_license"].ConnectionString))
        {

            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_AddSensorLicenseData";
                cmd.Parameters.AddWithValue("m_OrganizationId", intOrganizationId);
                cmd.Parameters.AddWithValue("m_sensorqrnumber", s_sensorqrnumber);
                cmd.Parameters.AddWithValue("m_Active", s_sensoractive);
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {

            }


            conn.Close();


        }
    }
    

    private void fnGetActivateSensor()
    {
        int intOrganizationId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["id"]));

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString_license"].ConnectionString))
        {

            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetSensorLicenseData";
                cmd.Parameters.AddWithValue("m_OrganizationId", intOrganizationId);

                DataSet dsData = new DataSet();
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                sda.Fill(dsData);

                string strTableData = "";// "<table id=\"tblData\" class=\"table table-bordered table-striped\">\n";

                for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
                {
                    strTableData = strTableData + dsData.Tables[0].Rows[intRowCtr][2].ToString() + ",";
                }

                dsData = null;
                sda = null;

                Response.Write(strTableData);

            }
            catch (Exception ex)
            {

            }


            conn.Close();


        }
    }

    private void fnViewActivateSensor()
    {
        int intOrganizationId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["id"]));

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString_license"].ConnectionString))
        {

            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetSensorLicenseData";
                cmd.Parameters.AddWithValue("m_OrganizationId", intOrganizationId);

                DataSet dsData = new DataSet();
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                sda.Fill(dsData);

                string strTableData = "";// "<table id=\"tblData\" class=\"table table-bordered table-striped\">\n";
                strTableData = strTableData + "<thead><tr><th>Hub detail</th><th>Creation Date</th><th>Status</th></tr></thead>";
                strTableData = strTableData + " <tbody>";


                for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
                {
                    strTableData = strTableData + "<tr>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr][2].ToString() + "</td>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr][6].ToString() + "</td>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr][5].ToString() + "</td>";

                    strTableData = strTableData + "</tr>";
                }

                dsData = null;
                sda = null;

                strTableData = strTableData + " </tbody>";//</table>";

                Response.Write(strTableData);

            }
            catch (Exception ex)
            {

            }


            conn.Close();


        }
    }
    private void fnAddHub()
    {
        int intOrganizationId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["id"]));
        string s_hubserialnumber = Convert.ToString(Request.QueryString["hubserialnumber"]);
        string s_hubactive = Convert.ToString(Request.QueryString["hubactive"]);

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString_license"].ConnectionString))
        {

            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_AddHubLicenseData";
                cmd.Parameters.AddWithValue("m_OrganizationId", intOrganizationId);
                cmd.Parameters.AddWithValue("m_hubSerialNumber", s_hubserialnumber);
                cmd.Parameters.AddWithValue("m_Active", s_hubactive);
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {

            }


            conn.Close();


        }
    }



    private void fnViewActivateHub()
    {
        int intOrganizationId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["id"]));
        
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString_license"].ConnectionString))
        {

            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetHubLicenseData";
                cmd.Parameters.AddWithValue("m_OrganizationId", intOrganizationId);
                
                DataSet dsData = new DataSet();
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                sda.Fill(dsData);

                string strTableData = "";// "<table id=\"tblData\" class=\"table table-bordered table-striped\">\n";
                strTableData = strTableData + "<thead><tr><th>Hub detail</th><th>Creation Date</th><th>Status</th></tr></thead>";
                strTableData = strTableData + " <tbody>";


                for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
                {
                    strTableData = strTableData + "<tr>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr][2].ToString() + "</td>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr][6].ToString() + "</td>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr][5].ToString() + "</td>";
                    
                    strTableData = strTableData + "</tr>";
                }

                dsData = null;
                sda = null;

                strTableData = strTableData + " </tbody>";//</table>";

                Response.Write(strTableData);

            }
            catch (Exception ex)
            {

            }


            conn.Close();


        }
    }

    private void fnSaveOrganization_old()
    {
        
        string strOrganizationName = Convert.ToString(Request.QueryString["orgname"]);
        string strContactPerson = Convert.ToString(Request.QueryString["contactperson"]);
        string strEmail = Convert.ToString(Request.QueryString["email"]);
        string strPhone = Convert.ToString(Request.QueryString["phone"]);
        string strAddress = Convert.ToString(Request.QueryString["address"]);
        int intStatus = Convert.ToInt32(Request.QueryString["status"]);
        string strOrgCode = Convert.ToString(Request.QueryString["orgcode"]);

        string strLogo = Convert.ToString(Request.QueryString["logo"]);

       
        using (MySqlConnection conn = new MySqlConnection(strMysqlConnection))
        {
            conn.Open();
            Organization objOrganizaton = new Organization();
            objOrganizaton.ObjCon = conn;
            objOrganizaton.OrganizationName = strOrganizationName.Trim();
            objOrganizaton.ContactPerson = strContactPerson.Trim();
            objOrganizaton.Phone = strPhone.Trim();
            objOrganizaton.Email = strEmail.Trim();
            objOrganizaton.Address = strAddress.Trim();
            objOrganizaton.OrgCode = strOrgCode.Trim();
            objOrganizaton.Status = intStatus;
            objOrganizaton.Logo = strLogo;

            int intReturnValue = objOrganizaton.Add();

            objOrganizaton = null;
            conn.Close();

        }

        

    }

    private void fnSaveOrganization()
    {
        
        string strOrganizationName = Convert.ToString(Request.QueryString["orgname"]);
        string strContactPerson = Convert.ToString(Request.QueryString["contactperson"]);
        string strEmail = Convert.ToString(Request.QueryString["email"]);
        string strPhone = Convert.ToString(Request.QueryString["phone"]);
        string strAddress = Convert.ToString(Request.QueryString["address"]);
        int intStatus = Convert.ToInt32(Request.QueryString["status"]);
        string strOrgCode = Convert.ToString(Request.QueryString["orgcode"]);

        string strLogo = Convert.ToString(Request.QueryString["logo"]);


        using (MySqlConnection conn = new MySqlConnection(strMysqlConnection))
        {
            conn.Open();
            Organization objOrganizaton = new Organization();
            objOrganizaton.ObjCon = conn;
            objOrganizaton.OrganizationName = strOrganizationName.Trim();
            objOrganizaton.ContactPerson = strContactPerson.Trim();
            objOrganizaton.Phone = strPhone.Trim();
            objOrganizaton.Email = strEmail.Trim();
            objOrganizaton.Address = strAddress.Trim();
            objOrganizaton.OrgCode = strOrgCode.Trim();
            objOrganizaton.Status = intStatus;
            objOrganizaton.Logo = strLogo;

            int intReturnValue = objOrganizaton.Add();

            objOrganizaton = null;
            conn.Close();

        }



    }

    private void fnUpdateOrganization()
    {
        int intOrganizationId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["id"]));
        string strOrganizationName = Convert.ToString(Request.QueryString["orgname"]);
        string strContactPerson = Convert.ToString(Request.QueryString["contactperson"]);
        string strEmail = Convert.ToString(Request.QueryString["email"]);
        string strPhone = Convert.ToString(Request.QueryString["phone"]);
        string strAddress = Convert.ToString(Request.QueryString["address"]);      
        int intStatus = Convert.ToInt32(Request.QueryString["status"]);
        string strOrgCode = Convert.ToString(Request.QueryString["orgcode"]);

        string strLogo = Convert.ToString(Request.QueryString["logo"]);


        using (MySqlConnection conn = new MySqlConnection(strMysqlConnection))
        {
            conn.Open();
            Organization objOrganizaton = new Organization();
            objOrganizaton.ObjCon = conn;
            objOrganizaton.OrganizationId = intOrganizationId;
            objOrganizaton.OrganizationName = strOrganizationName.Trim();
            objOrganizaton.ContactPerson = strContactPerson.Trim();
            objOrganizaton.Phone = strPhone.Trim();
            objOrganizaton.Email = strEmail.Trim();
            objOrganizaton.Address = strAddress.Trim();
            objOrganizaton.OrgCode = strOrgCode.Trim();
            objOrganizaton.Status = intStatus;
            objOrganizaton.Logo = strLogo;

            int intReturnValue = objOrganizaton.Update();

            objOrganizaton = null;
            conn.Close();

        }

    }

    private int fnDeleteOrganization()
    {
        int userid = Convert.ToInt16(objpwd.DecryptData(Request.QueryString["id"]));
        using (MySqlConnection MySQLCon = new MySqlConnection(strMysqlConnection))
        {
            int intretVal = 0;
            Organization objOrg = new Organization();
            MySQLCon.Open();
            objOrg.ObjCon = MySQLCon;
            objOrg.OrganizationId = userid;
            DataSet dsOrgSite = objOrg.ValidateOrganizationSite();
            if (dsOrgSite.Tables[0].Rows.Count == 0)
            {
                DataSet dsOrgUser = objOrg.ValidateOrganizationUser();
                if (dsOrgUser.Tables[0].Rows.Count == 0)
                {
                    intretVal = objOrg.Delete();
                }
                else
                    return 0;                
            }
            else
                return 2;
            objOrg = null;
            MySQLCon.Close();
        }
        return 1;
    }
}