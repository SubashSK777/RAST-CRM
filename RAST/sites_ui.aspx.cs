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

public partial class sites_ui : System.Web.UI.Page
{
    EncryptionHelper objpwd = new EncryptionHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        Session["s_FloorMaps"] = "";

        int intUserId = Convert.ToInt16(Session["i_UloginId"]);

        int intSiteId = 0;
        
        if (Request.QueryString["id"] != null)
        {
            if (Request.QueryString["id"].ToString().Equals("0"))
            {
                intSiteId = 0;

            }
            else
            {
                intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["id"].ToString().Trim()));

            }
        }

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            Sites objSites = new Sites();
            objSites.ObjCon = conn;
            objSites.SiteId = intSiteId;

            if (Request.QueryString["type"] != null)
            {
                if (Request.QueryString["type"].ToString().Trim().Length > 0)
                {

                    string strEdit = Convert.ToString(Request.QueryString["type"].ToString().Trim());

                    if (strEdit.ToLower() == "e")
                    {
                        objSites.Edit = 1;


                    }
                }
            }

            DataSet dsSites = objSites.ReadElementsForUI(intSiteId);

            txtSiteId.Value = Convert.ToString(dsSites.Tables[0].Rows[0][0]);
            strtxtSiteId.Value= objpwd.EncryptData(txtSiteId.Value);

            if (dsSites.Tables[1].Rows.Count > 0)
            {
                List<ListItem> itemsDT = new List<ListItem>();

                for(int intRowCtr = 0; intRowCtr < dsSites.Tables[1].Rows.Count; intRowCtr ++)
                {
                    itemsDT.Add(new ListItem(dsSites.Tables[1].Rows[intRowCtr][1].ToString(), dsSites.Tables[1].Rows[intRowCtr][0].ToString()));
                }

                cmbDeploymentTechnicianName.Items.AddRange(itemsDT.ToArray());
            }

            if (dsSites.Tables[2].Rows.Count > 0)
            {
                List<ListItem> itemsAT = new List<ListItem>();

                for (int intRowCtr = 0; intRowCtr < dsSites.Tables[2].Rows.Count; intRowCtr++)
                {
                    itemsAT.Add(new ListItem(dsSites.Tables[2].Rows[intRowCtr][1].ToString(), dsSites.Tables[2].Rows[intRowCtr][0].ToString()));
                }

                cmbAlertTechnicianName.Items.AddRange(itemsAT.ToArray());
            }

            if (dsSites.Tables[3].Rows.Count > 0)
            {
                List<ListItem> itemsOrganization = new List<ListItem>();

                for (int intRowCtr = 0; intRowCtr < dsSites.Tables[3].Rows.Count; intRowCtr++)
                {
                    itemsOrganization.Add(new ListItem(dsSites.Tables[3].Rows[intRowCtr][1].ToString(), dsSites.Tables[3].Rows[intRowCtr][0].ToString()));
                }

                cmbOrganization.Items.AddRange(itemsOrganization.ToArray());
            }


            if (objSites.Edit == 1)
            {
                if (dsSites.Tables[4].Rows.Count > 0)
                {
                    txtSiteId.Value = Convert.ToString(dsSites.Tables[4].Rows[0]["i_SiteId"]);
                    strtxtSiteId.Value = objpwd.EncryptData(txtSiteId.Value);
                    txtSiteName.Value = Convert.ToString(dsSites.Tables[4].Rows[0]["s_SiteName"]);
                    txtLatitude.Value = Convert.ToString(dsSites.Tables[4].Rows[0]["s_Latitude"]);
                    txtLongitude.Value = Convert.ToString(dsSites.Tables[4].Rows[0]["s_Longitude"]);
                    txtSiteAddress.Value = Convert.ToString(dsSites.Tables[4].Rows[0]["s_Address"]);
                   
                    cmbDeploymentTechnicianName.Value = Convert.ToString(dsSites.Tables[4].Rows[0]["i_DeploymentTechnicianId"]);
                    cmbAlertTechnicianName.Value = Convert.ToString(dsSites.Tables[4].Rows[0]["i_AlertTechnicianId"]);
                    cmbStatus.Value = Convert.ToString(dsSites.Tables[4].Rows[0]["b_Status"]);
                    cmbOrganization.Value = Convert.ToString(dsSites.Tables[4].Rows[0]["i_OrganizationId"]);
                    strcmbOrganization.Value = objpwd.EncryptData(cmbOrganization.Value);

                }

                if (Convert.ToInt32(dsSites.Tables[6].Rows.Count) > 0)
                {
                    hidUploadFileCount.Value = Convert.ToString(dsSites.Tables[6].Rows.Count);
                }
                else
                {
                    hidUploadFileCount.Value = "0";
                }

                if (dsSites.Tables[5].Rows.Count > 0)
                {

                    for (int intRowCtr = 0; intRowCtr < dsSites.Tables[5].Rows.Count; intRowCtr++)
                    {

                   

                        HtmlTableRow tblRow = new HtmlTableRow();
                        HtmlTableCell tbclSlNo = new HtmlTableCell();
                        HtmlTableCell tbclHubId = new HtmlTableCell();
                        HtmlTableCell tbclHubPhoneNumber = new HtmlTableCell();

                        HtmlTableCell tbclhubOnTime = new HtmlTableCell();
                        HtmlTableCell tbclhubOffTime = new HtmlTableCell();
                        HtmlTableCell tbclMobileProvider = new HtmlTableCell();
                        HtmlTableCell tbclpaymentType = new HtmlTableCell();

                        HtmlTableCell tbclDelete = new HtmlTableCell();
                        HtmlTableCell tbclHubIntId = new HtmlTableCell();

                        tbclSlNo.InnerText = Convert.ToString(intRowCtr + 1);
                        tbclHubId.InnerText = Convert.ToString(dsSites.Tables[5].Rows[intRowCtr]["s_HubId"]);
                        tbclHubPhoneNumber.InnerText = Convert.ToString(dsSites.Tables[5].Rows[intRowCtr]["s_HubPhoneNumber"]);
                        tbclhubOnTime.InnerText = Convert.ToString(dsSites.Tables[5].Rows[intRowCtr]["s_hubOnTime"]);
                        tbclhubOffTime.InnerText = Convert.ToString(dsSites.Tables[5].Rows[intRowCtr]["s_hubOffTime"]);
                        tbclMobileProvider.InnerText = Convert.ToString(dsSites.Tables[5].Rows[intRowCtr]["s_MobileProvider"]);
                        tbclpaymentType.InnerText = Convert.ToString(dsSites.Tables[5].Rows[intRowCtr]["s_paymentType"]);

                        
                        //tbclDelete.InnerHtml = "<i style='cursor:pointer' class='fa fa-edit' onclick='fnEditRowHub(" + objpwd.EncryptData(Convert.ToString(intRowCtr + 1)) + "," + objpwd.EncryptData(Convert.ToString(dsSites.Tables[5].Rows[intRowCtr]["i_HubId"])) + ")'></i>&nbsp;<i  style='cursor:pointer'  class='fa fa-minus-square' onclick='fnDeleteRow(" + objpwd.EncryptData(Convert.ToString(intRowCtr + 1)) + "," + objpwd.EncryptData(Convert.ToString(dsSites.Tables[5].Rows[intRowCtr]["i_HubId"])) + ")'></i>";
                        tbclDelete.InnerHtml = "<i style='cursor:pointer' class='fa fa-edit' onclick='fnEditRowHub(" + Convert.ToString(intRowCtr + 1) + "," + Convert.ToString(dsSites.Tables[5].Rows[intRowCtr]["i_HubId"]) + ")'></i>&nbsp;<i  style='cursor:pointer'  class='fa fa-minus-square' onclick='fnDeleteRow(" + Convert.ToString(intRowCtr + 1) + "," + Convert.ToString(dsSites.Tables[5].Rows[intRowCtr]["i_HubId"]) + ")'></i>";
                        //tbclDelete.InnerHtml = "<i style='cursor:pointer' class='fa fa-edit' onclick='fnEditRowHub(" + Convert.ToString(intRowCtr + 1) + "," + objpwd.EncryptData(Convert.ToString(dsSites.Tables[5].Rows[intRowCtr]["i_HubId"])) + ")'></i>&nbsp;<i  style='cursor:pointer'  class='fa fa-minus-square' onclick='fnDeleteRow(" + Convert.ToString(intRowCtr + 1) + "," + objpwd.EncryptData(Convert.ToString(dsSites.Tables[5].Rows[intRowCtr]["i_HubId"])) + ")'></i>";

                        tbclHubIntId.InnerText = Convert.ToString(dsSites.Tables[5].Rows[intRowCtr]["i_HubId"]);
                        tbclHubIntId.Visible = false;


                        tblRow.Cells.Add(tbclSlNo);
                        tblRow.Cells.Add(tbclHubId);
                        tblRow.Cells.Add(tbclHubPhoneNumber);
                        tblRow.Cells.Add(tbclhubOnTime);
                        tblRow.Cells.Add(tbclhubOffTime);
                        tblRow.Cells.Add(tbclMobileProvider);
                        tblRow.Cells.Add(tbclpaymentType);


                        tblRow.Cells.Add(tbclDelete);
                        tblRow.Cells.Add(tbclHubIntId);

                        tblHubDetails.Rows.Add(tblRow);
                    }

                }

            }
            else { hidUploadFileCount.Value = "0"; }

            dsSites = null;
            objSites = null;
            conn.Close();


        }
    }
}