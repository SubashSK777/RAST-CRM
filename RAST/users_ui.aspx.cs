using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using RAST.DAL;
using System.Data;

public partial class users_ui : System.Web.UI.Page
{
    int intUserId = 0;
    EncryptionHelper objpwd = new EncryptionHelper();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        hidUserId.Value = Convert.ToString(Session["i_UloginId"]);

        if (Request.QueryString["id"] != null)
        {
            if (Request.QueryString["id"].ToString().Trim().Length > 0)
            {
                try
                {
                    intUserId = Convert.ToInt32(objpwd.EncryptData(Request.QueryString["id"].ToString().Trim()));

                }
                catch (Exception exp)
                {
                    intUserId = 0;
                }

            }
        }
        try
        {
            if (!IsPostBack)
                UserDataBind();
        }
        catch (Exception ex)
        {

        }

    }

    protected void UserDataBind()
    {
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            Users objUser = new Users();
            objUser.conn = conn;

            int selectedUserId = 0;
            if (Request.QueryString["id"].Equals("0"))
            {
                selectedUserId = 0;
            }
            else
            {
                selectedUserId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["id"].ToString().Trim()));
            }

            
            objUser.UserId = Convert.ToInt16(Session["i_UloginId"]);
            DataSet ds = objUser.ReadElementsForUI(selectedUserId);
            DataTable tblUser = ds.Tables[0];
            DataTable tblOrganization = ds.Tables[1];
            DataTable tblRole = ds.Tables[2];

            ddOrgLocation.DataSource = tblOrganization;
            ddOrgLocation.DataValueField = tblOrganization.Columns["i_OrganizationId"].ToString();
            ddOrgLocation.DataTextField = tblOrganization.Columns["s_OrganizationName"].ToString();
            ddOrgLocation.DataBind();

            ddRole.DataSource = tblRole;
            ddRole.DataValueField = tblRole.Columns["i_RoleId"].ToString();
            ddRole.DataTextField = tblRole.Columns["s_Role"].ToString();
            ddRole.DataBind();

            if (Convert.ToInt32(objpwd.DecryptData(Request.QueryString["id"].ToString().Trim())) > 0)
            {
                txtName.Value = tblUser.Rows[0]["s_Name"].ToString();
                txtEmail.Value = tblUser.Rows[0]["s_Email"].ToString();
                txtPhone.Value = tblUser.Rows[0]["s_PhoneNumber"].ToString();

                ddOrgLocation.SelectedValue = Convert.ToString(tblUser.Rows[0]["i_OrganizationId"]);
                ddRole.SelectedValue = Convert.ToString(Convert.ToInt16(tblUser.Rows[0]["i_RoleId"]));
                ddStatus.SelectedValue = Convert.ToString(Convert.ToInt16(tblUser.Rows[0]["b_Status"]));
            }

        }
    }


}