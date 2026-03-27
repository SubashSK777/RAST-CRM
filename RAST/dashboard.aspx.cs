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
using System.Data.SqlClient;



public partial class dashboard : System.Web.UI.Page
{
    EncryptionHelper objpwd = new EncryptionHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["i_UloginId"] == null)
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
                DataSet dsData = objSites.ReadDataset(intUserId, 0, 0);

                //Ragu 21 Jun 2017
                dsData.Tables[0].DefaultView.RowFilter = "b_Status = '1'";

                cmbSiteName.DataSource = dsData.Tables[0];
                cmbSiteName.DataValueField = "i_SiteId";
                cmbSiteName.DataTextField = "s_SiteName";
                cmbSiteName.DataBind();

                cmbSiteName.Items.Insert(0, new ListItem("-- Select Site --", "0"));
                

                //Ragu 21 Jun 2017

                //cmbSiteName.SelectedIndex = 0;
                int intRequestId = 0;
                if (Request.QueryString["id"]!=null){
                    if (Request.QueryString["id"].Length <= 5)
                    {
                        intRequestId = Convert.ToInt32(Request.QueryString["id"]);
                        
                    }
                    else
                    {
                        intRequestId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["id"]));
                        cmbSiteName.SelectedIndex = cmbSiteName.Items.IndexOf(cmbSiteName.Items.FindByValue(intRequestId.ToString()));
                        
                    }

                    //intRequestId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["id"]));
                }

                dsData = null;
                objSites = null;

                


                if (intRequestId >= 1)
                {
                    //int t= cmbSiteName.Items.IndexOf(cmbSiteName.Items.FindByValue(intRequestId.ToString()));
                    
                    cmbSiteName.SelectedIndex = cmbSiteName.Items.IndexOf(cmbSiteName.Items.FindByValue(intRequestId.ToString()));
                    
                    Sites objSitesLoc = new Sites();
                    objSitesLoc.ObjCon = conn;

                    DataSet dsFloorMapImage = objSitesLoc.GetBuildingMapForStation_fromMap(intRequestId+"");
                    cmbLocation.DataSource = dsFloorMapImage.Tables[0];
                    cmbLocation.DataValueField = "i_SiteImageId";
                    cmbLocation.DataTextField = "s_FloorMapImageName";
                    cmbLocation.DataBind();

                    cmbLocation.Items.Insert(0, new ListItem("-- Select Location --", "0"));

                    cmbLocation.SelectedIndex = 1;

                    dsFloorMapImage = null;
                    objSitesLoc = null;




                    //var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                    //nameValues.Set("floormapid","42");

                }
                else
                {
                    cmbSiteName.SelectedIndex = 0;
                }

                //strtxtSiteId.Value = intRequestId+"";

                //Ragu 11 Oct 2018 ---- Disable Time Selection for normal user

                String roleName = "";
                int intRoleId=2;

                using (MySqlConnection conn1 = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("p_GetUserRole", conn1);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("m_UserId", intUserId);
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        roleName = ds.Tables[0].Rows[0]["s_Role"].ToString();
                        intRoleId = Convert.ToInt32(ds.Tables[0].Rows[0]["i_RoleId"]);
                    }
                    if(intRoleId==1)
                    {
                        timerangepathdiv.Visible = true;
                        AnalysisDivButton.Visible = true;
                        //Div_Path1.Visible = true;
                    }
                    else
                    {
                        timerangepathdiv.Visible = true;
                        AnalysisDivButton.Visible = true;
                        //Div_Path1.Visible = false;
                    }
                }

                /*
                1	SuperAdmin
                2	Administrator
                3	Operator
                4	Deployment Technician
                5	Alert Technician
                6   Analyst
                */

                //-------------
                //-------------------- Ragu 21 Jun 2017

            }

        }
        

    }
    
}