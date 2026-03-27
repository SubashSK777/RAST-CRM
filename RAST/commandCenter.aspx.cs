/*********************************************************************************************************************
Module Name: Dashboard 
Module Description: This page provides the option of visualization the data in graphical and tabular format
The entire work of data visualization is performed via JavaScript
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


public partial class commandCenter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (!Page.IsPostBack)
        {
            int intUserId = Convert.ToInt16(Session["i_UloginId"]);
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
            {
                conn.Open();
                Sites objSites = new Sites();
                objSites.ObjCon = conn;
                DataSet dsData = objSites.ReadDataset(intUserId,0, 0);

                //Ragu 21 Jun 2017
                dsData.Tables[0].DefaultView.RowFilter = "b_Status = '1'";

                cmbSiteName.DataSource = dsData.Tables[0];
                cmbSiteName.DataValueField = "i_SiteId";
                cmbSiteName.DataTextField = "s_SiteName";
                cmbSiteName.DataBind();

                cmbSiteName.Items.Insert(0,new ListItem("-- Select Site --", "0"));

                Sites objSites1 = new Sites();
                objSites1.ObjCon = conn;
                DataSet dsDataCommand = objSites1.ReadCommandDataset();

                //Ragu 21 Jun 2017
                cmbCommand.DataSource = dsDataCommand.Tables[0];
                cmbCommand.DataValueField = "Code";
                cmbCommand.DataTextField = "Desc";
                cmbCommand.DataBind();

                cmbCommand.Items.Insert(0, new ListItem("-- Select Command --", "0"));


                //Ragu 21 Jun 2017

                cmbSiteName.SelectedIndex = 2;

                int intRequestId = Convert.ToInt32(Request.QueryString["id"]);

                dsData = null;
                objSites = null;

                if (intRequestId >= 1)
                {
                    cmbSiteName.SelectedIndex = cmbSiteName.Items.IndexOf(cmbSiteName.Items.FindByValue(intRequestId.ToString()));

                    Sites objSitesLoc = new Sites();
                    objSitesLoc.ObjCon = conn;

                    DataSet dsHubDet = objSitesLoc.GetHubDetailBySite(intRequestId+"");
                    cmbHubName.DataSource = dsHubDet.Tables[0];
                    cmbHubName.DataValueField = "s_HubPhoneNumber";
                    cmbHubName.DataTextField = "s_HubId" +"-"+"s_HubPhoneNumber";
                    cmbHubName.DataBind();

                    cmbHubName.Items.Insert(0, new ListItem("-- Select Hub --", "0"));

                    cmbHubName.SelectedIndex = 1;

                    dsHubDet = null;
                    objSitesLoc = null;

                   
                   
                    
                    //var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                    //nameValues.Set("floormapid","42");

                }
                else
                {
                    cmbSiteName.SelectedIndex = 0;
                }
                //-------------------- Ragu 21 Jun 2017

            }

        }

    }
    
}