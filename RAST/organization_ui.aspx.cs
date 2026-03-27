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


public partial class organization_ui : System.Web.UI.Page
{
    EncryptionHelper objpwd = new EncryptionHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        int intUserId = Convert.ToInt16(Session["i_UloginId"]);

        int intOrganizationId = 0;

        if (Request.QueryString["id"] != null)
        {
            if (Request.QueryString["id"].ToString().Equals("0"))
            {
                intOrganizationId = 0;
               
            }
            else
            {
                intOrganizationId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["id"].ToString().Trim()));

            }
         }
        
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            Organization objOrganization = new Organization();
            objOrganization.ObjCon = conn;

            if (Request.QueryString["type"] != null)
            {
                if (Request.QueryString["type"].ToString().Trim().Length > 0)
                {

                    string strEdit = Convert.ToString(Request.QueryString["type"].ToString().Trim());

                    if (strEdit.ToLower() == "e")
                    {
                        objOrganization.OrganizationId = intOrganizationId;
                        objOrganization.Edit = 1;


                    }
                }
            }

            DataSet dsOrganization = objOrganization.ReadElementsForUI(intOrganizationId);

            //if (dsOrganization.Tables[0].Rows.Count > 0)
            //{
            //    List<ListItem> itemsDT = new List<ListItem>();

            //    for (int intRowCtr = 0; intRowCtr < dsOrganization.Tables[0].Rows.Count; intRowCtr++)
            //    {
            //        itemsDT.Add(new ListItem(dsOrganization.Tables[0].Rows[intRowCtr][1].ToString(), dsOrganization.Tables[0].Rows[intRowCtr][0].ToString()));
            //    }

            //    cmbAdministrator.Items.AddRange(itemsDT.ToArray());
            //}

            if (objOrganization.Edit == 1)
            {
                if (dsOrganization.Tables[1].Rows.Count > 0)
                {
                    txtOrganizationName.Value = Convert.ToString(dsOrganization.Tables[1].Rows[0]["s_OrganizationName"]);
                    txtContactPerson.Value = Convert.ToString(dsOrganization.Tables[1].Rows[0]["s_ContactPerson"]);
                    txtEmail.Value = Convert.ToString(dsOrganization.Tables[1].Rows[0]["s_EmailAddress"]);
                    txtPhoneNumber.Value = Convert.ToString(dsOrganization.Tables[1].Rows[0]["s_PhoneNumber"]);
                    txtAddress.Value = Convert.ToString(dsOrganization.Tables[1].Rows[0]["s_Address"]);
                    cmbStatus.Value = Convert.ToString(dsOrganization.Tables[1].Rows[0]["b_Status"]);
                    txtOrgCode.Value = Convert.ToString(dsOrganization.Tables[1].Rows[0]["s_OrgCode"]);
                    hidUploadFileName.Value = Convert.ToString(dsOrganization.Tables[1].Rows[0]["s_Logo"]);
                    imgLogo.Src = "/logo/" + Convert.ToString(dsOrganization.Tables[1].Rows[0]["s_Logo"]);
                }
            }

            dsOrganization = null;
            objOrganization = null;
            conn.Close();
        }
    }
}