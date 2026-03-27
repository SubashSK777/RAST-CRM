using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

public partial class services_alertcount_report : System.Web.UI.Page
{
    string strConnection = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        String id = Request.QueryString["id"].ToString();
        if (id == "1")
            fnGetSensorAlertCount();
        else if (id == "2")
            fnGetSiteAlertCount();
        else if (id == "3")
            fnGetOrganizationAlertCount();
        else if (id == "4")
            fnGetSensorDataTable();
        else if (id == "5")
            fnGetSiteDataTable();
        else if (id == "6")
            fnGetOrgDataTable();

    }

	private void fnGetSensorAlertCount()
    {
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetSensorAlertCount";
            cmd.Parameters.AddWithValue("siteid", Convert.ToInt32(Request.QueryString["sid"]));
            cmd.Parameters.AddWithValue("dtstart", Convert.ToDateTime(Request.QueryString["startdate"]).ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("dtend", Convert.ToDateTime(Request.QueryString["enddate"]).ToString("yyyy-MM-dd HH:mm:ss"));

            try
            {
                DataSet dsData = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dsData);

                strReturnValue = ConvertDataTabletoString(dsData.Tables[0]);

                da = null;
                dsData = null;


            }
            catch (Exception exp)
            {

            }
            cmd = null;

            Response.Write(strReturnValue);

        }
    }
   

    private void fnGetSiteAlertCount()
    {
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {

            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetSiteAlertCount";
            cmd.Parameters.AddWithValue("orgid", Convert.ToInt32(Request.QueryString["orgid"]));
            cmd.Parameters.AddWithValue("dtstart", Convert.ToDateTime(Request.QueryString["startdate"]).ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("dtend", Convert.ToDateTime(Request.QueryString["enddate"]).ToString("yyyy-MM-dd HH:mm:ss"));

            try
            {
                DataSet dsData = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dsData);

                strReturnValue = ConvertDataTabletoString(dsData.Tables[0]);

                da = null;
                dsData = null;


            }
            catch (Exception exp)
            {

            }
            cmd = null;

            Response.Write(strReturnValue);

        }
    }
   
    private void fnGetOrganizationAlertCount()
    {
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetOrganizationAlertCount";
            cmd.Parameters.AddWithValue("dtstart", Convert.ToDateTime(Request.QueryString["startdate"]).ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("dtend", Convert.ToDateTime(Request.QueryString["enddate"]).ToString("yyyy-MM-dd HH:mm:ss"));

            try
            {
                DataSet dsData = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dsData);

                strReturnValue = ConvertDataTabletoString(dsData.Tables[0]);

                da = null;
                dsData = null;


            }
            catch (Exception exp)
            {

            }
            cmd = null;

            Response.Write(strReturnValue);

        }
    }
    
    private void  fnGetSensorDataTable()

    {
       
        DateTime dtStartDate = Convert.ToDateTime(Request.QueryString["startdate"]);
        DateTime dtEndDate = Convert.ToDateTime(Request.QueryString["enddate"]);
        string strRetFileName = "nodata";

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {

            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetSensorAlertCount";
                cmd.Parameters.AddWithValue("siteid", Convert.ToInt32(Request.QueryString["sid"]));
                cmd.Parameters.AddWithValue("dtstart", Convert.ToDateTime(Request.QueryString["startdate"]).ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("dtend", Convert.ToDateTime(Request.QueryString["enddate"]).ToString("yyyy-MM-dd HH:mm:ss"));

                DataSet dsData = new DataSet();
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                sda.Fill(dsData);

                string strTableData = "<table id=\"tblData\" class=\"table table-bordered table-striped\">\n";
                strTableData = strTableData + "<thead><tr><th>Sensor Name</th><th>Alert Count</th></tr></thead>";
                strTableData = strTableData + " <tbody>";


                for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
                {
                    strTableData = strTableData + "<tr>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr][0].ToString() + "</td>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr][1].ToString() + "</td>";
                    strTableData = strTableData + "</tr>";
                }

                dsData = null;
                sda = null;

                strTableData = strTableData + " </tbody></table>";

                Response.Write(strTableData);

            }
            catch (Exception ex)
            {

            }


            conn.Close();


        }
    }

    private void fnGetSiteDataTable()
    {

        DateTime dtStartDate = Convert.ToDateTime(Request.QueryString["startdate"]);
        DateTime dtEndDate = Convert.ToDateTime(Request.QueryString["enddate"]);
        string strRetFileName = "nodata";

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {

            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetSiteAlertCount";
                cmd.Parameters.AddWithValue("orgid", Convert.ToInt32(Request.QueryString["orgid"]));
                cmd.Parameters.AddWithValue("dtstart", dtStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("dtend", dtEndDate.ToString("yyyy-MM-dd HH:mm:ss"));
                DataSet dsData = new DataSet();
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                sda.Fill(dsData);

                string strTableData = "<table id=\"tblData\" class=\"table table-bordered table-striped\">\n";
                strTableData = strTableData + "<thead><tr><th>Site Name</th><th>Alert Count</th></tr></thead>";
                strTableData = strTableData + " <tbody>";


                for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
                {
                    strTableData = strTableData + "<tr>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr][0].ToString() + "</td>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr][1].ToString() + "</td>";
                    strTableData = strTableData + "</tr>";
                }

                dsData = null;
                sda = null;

                strTableData = strTableData + " </tbody></table>";

                Response.Write(strTableData);

            }
            catch (Exception ex)
            {

            }


            conn.Close();


        }
    }

    private void fnGetOrgDataTable()
    {

        DateTime dtStartDate = Convert.ToDateTime(Request.QueryString["startdate"]);
        DateTime dtEndDate = Convert.ToDateTime(Request.QueryString["enddate"]);
        string strRetFileName = "nodata";

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {

            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetOrganizationAlertCount";

                cmd.Parameters.AddWithValue("dtstart", dtStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("dtend", dtEndDate.ToString("yyyy-MM-dd HH:mm:ss"));
                DataSet dsData = new DataSet();
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                sda.Fill(dsData);

                string strTableData = "<table id=\"tblData\" class=\"table table-bordered table-striped\">\n";
                strTableData = strTableData + "<thead><tr><th>Organization Name</th><th>Alert Count</th></tr></thead>";
                strTableData = strTableData + " <tbody>";


                for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
                {
                    strTableData = strTableData + "<tr>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr][0].ToString() + "</td>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr][1].ToString() + "</td>";
                    strTableData = strTableData + "</tr>";
                }

                dsData = null;
                sda = null;

                strTableData = strTableData + " </tbody></table>";

                Response.Write(strTableData);

            }
            catch (Exception ex)
            {

            }


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
}