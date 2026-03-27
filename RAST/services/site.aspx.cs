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
using System.Web.SessionState;
using System.Text;

public partial class services_site : System.Web.UI.Page
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
                fnSaveSite();
                break;
            case 2:
                fnUpdateSite();
                break;
            case 3:
                fnGetSites();
                break;
            case 4:
                fnDeleteHub();
                break;
            case 5:
                fnUpdateHub();
                break;
            case 6:
                fnDeleteSite();
                break;
            case 7:
                fnUpdateFlorMap();
                break;
            case 8:
                fnRetriveHubInfo();
                break;
        }


    }



    private void fnRetriveHubInfo()
    {
        //int intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
        //int intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"].ToString().Trim()));
        int intSiteId = 0;

        if (Request.QueryString["siteid"].Length <= 5)
        {
            intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
            
            
        }
        else
        {
            intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
        }

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            Sites objSites = new Sites();
            objSites.ObjCon = conn;
            DataSet dsHubDet = objSites.GetHubDetailBySite(intSiteId+"");

            for (int intRowCtr = 0; intRowCtr < dsHubDet.Tables[0].Rows.Count; intRowCtr++)
            {
                string strHubId = dsHubDet.Tables[0].Rows[intRowCtr]["s_HubId"].ToString();
                string strHubPhoneNumber = dsHubDet.Tables[0].Rows[intRowCtr]["s_HubPhoneNumber"].ToString();
            }
            string strData = ConvertDataTabletoString(dsHubDet.Tables[0]);

            dsHubDet = null;
            Response.Write(strData);
            objSites = null;
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

    private void fnUpdateFlorMap()
    {
        //int intSiteId = Convert.ToInt32(Request.Form["siteid"]);
        //intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"].ToString().Trim()));

        int intSiteId;

        if (Request.Form["siteid"].Length <= 5)
        {
            intSiteId = Convert.ToInt32(Request.Form["siteid"]);

            
        }
        else
        {
            intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.Form["siteid"]));
        }

        
        string strFloorMap = Convert.ToString(Request.Form["floormap"]);
        string strLocation = Convert.ToString(Request.Form["location"]);

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            Sites objSite = new Sites();
            objSite.ObjCon = conn;

            objSite.SiteId = intSiteId;
            objSite.BuildingFloorMap = strFloorMap;
            objSite.Location = strLocation;
            objSite.UpdateFloorMap();

            objSite = null;
            conn.Close();

        }

    }

    private void fnUpdateHub()
    {
        //int intHubId = Convert.ToInt32(Request.QueryString["hubid"]);
        //int intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
        //int intHubId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["hubid"].ToString().Trim()));
        //int intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"].ToString().Trim()));

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

        string strHubId = Convert.ToString(Request.QueryString["shubid"]);
        string strHubPhoneNumber = Convert.ToString(Request.QueryString["phonenumber"]);

        //Ragu 21 May 2018
        string strSelectMobileProvider = Convert.ToString(Request.QueryString["mobileprovider"]);
        string strSelectTypeOfSim = Convert.ToString(Request.QueryString["selectTypeofsim"]);
        //string strtxtNOfSensor = Convert.ToString(Request.QueryString["phonenumber"]);
        string strtxtHubOnTime = Convert.ToString(Request.QueryString["txthubonTime"]);
        string strtxtHubOffTime = Convert.ToString(Request.QueryString["txthuboffTime"]);
        //Ragu 21 May 2018

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            Sites objSite = new Sites();
            objSite.ObjCon = conn;

            objSite.HubIdDel = intHubId;
            objSite.HubIdUpdate = strHubId;
            objSite.SiteId = intSiteId;
            objSite.PhoneNumberDel = strHubPhoneNumber;

        
    //Ragu 21 May 2018
            objSite.MobileProviderUpdate = strSelectMobileProvider;
            objSite.paymentTypeUpdate = strSelectTypeOfSim;
            objSite.hubOnTimeUpdate = strtxtHubOnTime;
            objSite.hubOffTimeUpdate = strtxtHubOffTime;
            int intReturnValue = objSite.UpdateHub();

            objSite = null;
            conn.Close();

        }

    }

    private void fnDeleteHub()
    {
        //int intHubId = Convert.ToInt32(Request.QueryString["hubid"]);

        //int intHubId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["hubid"].ToString().Trim()));
        int intHubId = 0;


        if (Request.QueryString["hubid"].Length <= 5)
        {
            intHubId = Convert.ToInt32(Request.QueryString["hubid"]);
            
        }
        else
        {
            intHubId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["hubid"]));
        }

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            Sites objSite = new Sites();
            objSite.ObjCon = conn;

            objSite.HubIdDel = intHubId;

            int intReturnValue = objSite.DeleteHub();

            objSite = null;
            conn.Close();

        }

    }
    private void fnSaveSite()
    {
        //int intSiteId = Convert.ToInt32(Request.Form["siteid"]);

        //int intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.Form["siteid"]));
        int intSiteId;
        if (Request.Form["siteid"].Length <= 5)
        {
            intSiteId = Convert.ToInt32(Request.Form["siteid"]);
            

        }
        else
        {
            intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.Form["siteid"]));
        }


        int intOrganizationId = Convert.ToInt32(Request.Form["organization"]);
        string strSitename = Convert.ToString(Request.Form["sitename"]);
        string strLatitude = Convert.ToString(Request.Form["lat"]);
        string strLongitude = Convert.ToString(Request.Form["lon"]);
        string strAddress = Convert.ToString(Request.Form["address"]);
        //int intNoOfSensors = Convert.ToInt32(Request.Form["nosensors"]);
        int intDeploymentTechnicianId = Convert.ToInt32(Request.Form["deploymenttechnician"]);
        int intAlertTechnicianId = Convert.ToInt32(Request.Form["alerttechnician"]);
        int intStatus = Convert.ToInt32(Request.Form["status"]);
        string strImageName = Convert.ToString(Session["s_FloorMaps"]);



        string strHubID = Convert.ToString(Request.Form["hubid"]);
        string strHubPhoneNumber = Convert.ToString(Request.Form["hubphonenumber"]);

        string strHubOnTime = Convert.ToString(Request.Form["hubontime"]);
        string strHubOffTime = Convert.ToString(Request.Form["hubofftime"]);
        string strMobileProvider = Convert.ToString(Request.Form["mobileprovider"]);
        string strPaymentType = Convert.ToString(Request.Form["paymenttype"]);


        string[] arrHubId = strHubID.Split(',');
        string[] arrHubPhoneNumber = strHubPhoneNumber.Split(',');

        string[] arrHubOnTime = strHubOnTime.Split(',');
        string[] arrHubOffTime = strHubOffTime.Split(',');
        string[] arrMobileProvider = strMobileProvider.Split(',');
        string[] arrPaymentType = strPaymentType.Split(',');



        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            Sites objSite = new Sites();
            objSite.ObjCon = conn;
            objSite.OrganizationId = intOrganizationId;
            objSite.SiteId = intSiteId;
            objSite.SiteName = strSitename;
            objSite.Latitude = strLatitude;
            objSite.Longitude = strLongitude;
            objSite.Address = strAddress;
            //objSite.NoSensors = intNoOfSensors;
            objSite.DeploymentTechnicianId = intDeploymentTechnicianId;
            objSite.AlertTechnicianId = intAlertTechnicianId;
            objSite.Status = intStatus;
            objSite.HubId = arrHubId;
            objSite.BuildingFloorMap = strImageName;
            objSite.HubPhoneNumber = arrHubPhoneNumber;

            objSite.hubOnTime = arrHubOnTime;
            objSite.hubOffTime = arrHubOffTime;
            objSite.MobileProvider = arrMobileProvider;
            objSite.paymentType = arrPaymentType;

            int intReturnValue = objSite.Add();
            
            objSite = null;
            conn.Close();
            Response.Write("{\"status\": \"success\"}");

        }

       


    }

    private void fnUpdateSite()
    {

        int intSiteId;
        if (Request.Form["siteid"].Length <= 5)
        {
            intSiteId = Convert.ToInt32(Request.Form["siteid"]);
            
        }
        else
        {
            intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.Form["siteid"]));
        }

        //int intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.Form["siteid"]));



        //int intOrganizationId = Convert.ToInt32(objpwd.DecryptData(Request.Form["organization"]));

        int intOrganizationId;
        if (Request.Form["organization"].Length <= 5)
        {
            intOrganizationId = Convert.ToInt32(Request.Form["organization"]);
            
        }
        else
        {
            intOrganizationId = Convert.ToInt32(objpwd.DecryptData(Request.Form["organization"]));
        }

        string strSitename = Convert.ToString(Request.Form["sitename"]);
        string strLatitude = Convert.ToString(Request.Form["lat"]);
        string strLongitude = Convert.ToString(Request.Form["lon"]);
        string strAddress = Convert.ToString(Request.Form["address"]);
        int intNoOfSensors = Convert.ToInt32(Request.Form["nosensors"]);
        int intDeploymentTechnicianId = Convert.ToInt32(Request.Form["deploymenttechnician"]);
        int intAlertTechnicianId = Convert.ToInt32(Request.Form["alerttechnician"]);
        int intStatus = Convert.ToInt32(Request.Form["status"]);
        string strImageName = Convert.ToString(Request.Form["imagename"]);

        string strHubID = Convert.ToString(Request.Form["hubid"]);
        string strHubPhoneNumber = Convert.ToString(Request.Form["hubphonenumber"]);

        string strHubOnTime = Convert.ToString(Request.Form["hubontime"]);
        string strHubOffTime = Convert.ToString(Request.Form["hubofftime"]);
        string strMobileProvider = Convert.ToString(Request.Form["mobileprovider"]);
        string strPaymentType = Convert.ToString(Request.Form["paymenttype"]);

        string[] arrHubId = strHubID.Split(',');
        string[] arrHubPhoneNumber = strHubPhoneNumber.Split(',');

        string[] arrHubOnTime = strHubOnTime.Split(',');
        string[] arrHubOffTime = strHubOffTime.Split(',');
        string[] arrMobileProvider = strMobileProvider.Split(',');
        string[] arrPaymentType = strPaymentType.Split(',');




        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            Sites objSite = new Sites();
            objSite.ObjCon = conn;
            objSite.OrganizationId = intOrganizationId;
            objSite.SiteId = intSiteId;
            objSite.SiteName = strSitename;
            objSite.Latitude = strLatitude;
            objSite.Longitude = strLongitude;
            objSite.Address = strAddress;
            objSite.BuildingFloorMap = "nomap.png";
            objSite.NoSensors = intNoOfSensors;
            objSite.DeploymentTechnicianId = intDeploymentTechnicianId;
            objSite.AlertTechnicianId = intAlertTechnicianId;
            objSite.Status = intStatus;
            objSite.HubId = arrHubId;
            objSite.BuildingFloorMap = strImageName;
            objSite.HubPhoneNumber = arrHubPhoneNumber;


            objSite.hubOnTime = arrHubOnTime;
            objSite.hubOffTime = arrHubOffTime;
            objSite.MobileProvider = arrMobileProvider;
            objSite.paymentType = arrPaymentType;

            int intReturnValue = objSite.Update();

            objSite = null;
            conn.Close();

            Response.Write("{\"status\": \"success\"}");

        }

    }

    private void fnGetSites()
    {
        string strReturnContent = "";
        StringBuilder strReturnContentBuilder = new StringBuilder();
        int intUserId = Convert.ToInt16(Session["i_UloginId"]);
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            Sites objSites = new Sites();
            objSites.ObjCon = conn;
            DataSet dsStations = objSites.ReadDataset(intUserId,0, 0);

            for (int intRowCtr = 0; intRowCtr < dsStations.Tables[0].Rows.Count; intRowCtr++)
            {
                string strSiteId = objpwd.EncryptData(dsStations.Tables[0].Rows[intRowCtr]["i_SiteId"].ToString());
                //string strSiteId = dsStations.Tables[0].Rows[intRowCtr]["i_SiteId"].ToString();
                string strSiteName = dsStations.Tables[0].Rows[intRowCtr]["s_SiteName"].ToString();
                string strLatitude = dsStations.Tables[0].Rows[intRowCtr]["s_Latitude"].ToString();
                string strLongitude = dsStations.Tables[0].Rows[intRowCtr]["s_Longitude"].ToString();
                string strSiteStatus = dsStations.Tables[0].Rows[intRowCtr]["b_AlarmStatus"].ToString();
                string strAlertCount = dsStations.Tables[0].Rows[intRowCtr]["i_AlertCount"].ToString();
                if (dsStations.Tables[0].Rows[intRowCtr]["b_Status"].ToString() == "1")
                {
                    strReturnContentBuilder.Append(strSiteId + "#" + strSiteName + "#" + strLatitude + "#" + strLongitude + "#" + strSiteStatus + "#" + strAlertCount + ",");
                }
                
            }
            strReturnContent = strReturnContentBuilder.ToString();

            dsStations = null;
            objSites = null;
            conn.Close();

        }

        Response.Write(strReturnContent);

    }

    private void fnDeleteSite()
    {
        //int intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
        //int intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"].ToString().Trim()));

        int intSiteId = 0;

        if (Request.QueryString["siteid"].Length <= 5)
        {
            intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
            
        }
        else
        {
            intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
        }


        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            Sites objSites = new Sites();
            conn.Open();
            objSites.ObjCon = conn;
            objSites.SiteId = intSiteId;
            objSites.Delete();
            conn.Close();
            objSites = null;
        }
    }

}