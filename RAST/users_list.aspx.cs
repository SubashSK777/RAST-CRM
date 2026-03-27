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
using System.Text;

public partial class users_list : System.Web.UI.Page
{
    String strConnection = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
    EncryptionHelper objpwd = new EncryptionHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        int intUserId = Convert.ToInt16(Session["i_UloginId"]);
        int intRoleId = 0;
        using (MySqlConnection conn = new MySqlConnection(strConnection))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("p_GetUserRole", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("m_UserId", intUserId);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hidRoleId.Value = ds.Tables[0].Rows[0]["i_RoleId"].ToString();
                intRoleId = Convert.ToInt16(ds.Tables[0].Rows[0]["i_RoleId"].ToString());
            }
        }
        using (MySqlConnection conn = new MySqlConnection(strConnection))
        {
            conn.Open();
            Users objUser = new Users();
            objUser.conn = conn;
            try
            {
                DataSet dsData = objUser.ReadDataset(intUserId,0, 0);
                StringBuilder strReturnUser = new StringBuilder();

                string strTableData;
                strReturnUser.Append("<table id=\"tblUsersList\" class=\"table table-bordered table-striped\">\n");
                strReturnUser.Append("<thead><tr><th>Name</th><th>Organization</th><th>Email Address</th><th>Phone Number</th><th>Role</th> <th>Status</th></tr></thead>");
                strReturnUser.Append(" <tbody>");

                for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
                {
                    strReturnUser.Append("<tr>");
                    if (intRoleId > 2)
                        strReturnUser.Append("<td>" + dsData.Tables[0].Rows[intRowCtr][2].ToString() +"</td>");
                    else
                        strReturnUser.Append("<td>" + "<a href='users_ui.aspx?type=e&id=" + objpwd.EncryptData(Convert.ToString(dsData.Tables[0].Rows[intRowCtr][0])) + "'>" + dsData.Tables[0].Rows[intRowCtr][2].ToString() + "</a></td>");
                    strReturnUser.Append("<td>" + dsData.Tables[0].Rows[intRowCtr][1].ToString() + "</td>");
                    strReturnUser.Append("<td>" + dsData.Tables[0].Rows[intRowCtr][3].ToString() + "</td>");
                    strReturnUser.Append("<td>" + dsData.Tables[0].Rows[intRowCtr][4].ToString() + "</td>");
                    strReturnUser.Append("<td>" + dsData.Tables[0].Rows[intRowCtr][5].ToString() + "</td>");

                    if (Convert.ToString(dsData.Tables[0].Rows[intRowCtr][6]) == "1")
                    {
                        strReturnUser.Append("<td>Active</td>");
                    }
                    else
                    {
                        strReturnUser.Append("<td>Deactive</td>");
                    }

                    strReturnUser.Append("</tr>");
                }

                strReturnUser.Append(" </tbody></table>");

                dsData = null;
                objUser = null;
                strTableData = strReturnUser.ToString();
                spTable.InnerHtml = strTableData;
            }
            catch(Exception ex)
            {

            }
        }
    }
}