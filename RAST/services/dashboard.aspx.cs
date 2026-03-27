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
using System.IO;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using System.Drawing;

public partial class services_dashboard : System.Web.UI.Page
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
                GetSensorDataForDashboard();
                break;
            case 2:
                GetBuildingPlanForSite();
                break;
            case 3:
                GetDataForChart();
                break;
            case 4:
                string strExcelfile = GetSensorDataForExcel();
                Response.Write(@strExcelfile);
                break;
            case 5:
                GetDataTable();
                break;
            case 6:
                GetLocationsForSite();
                break;
            case 7:
                GetFloorMapForSite();
                break;
            case 8:
                GetAllSensorDataForDashboard();
                break;
            case 9:
                getDataForPath();
                break;
            case 10:
                getUserRole();
                break;
            case 11:
                GetDataForChartAnalysis(0);
                break;
            case 12:
                GetDataForChartAnalysis(1);
                break;
            case 13:
                GetDataForChartAnalysis(2);
                break;
            case 14:
                GetDataForChartAnalysis(3);
                break;
            case 15:
                GetDataForChartAnalysis(4);
                break;
            case 16:
                GetDataForChartAnalysis(5);
                break;
            case 20:
                GetPlanGroupData();
                break;
            case 21:
                GetFirstTrigger();
                break;
            case 22:
                GetPathWay();
                break;
            case 23:
                fnUpdRodentCaught();  // Ragu 05 Mar 19 Rodent Caught Info
                break;
            case 24:
                fnViewRodentCaught();  // Ragu 05 Mar 19 Rodent Caught Info
                break;
            case 25:
                GetRodentDataForChartAnalysis(0);
                break;
            case 26:
                GetRodentDataForChartAnalysis(1);
                break;
            case 27:
                GetRodentDataForChartAnalysis(2);
                break;
            case 28:
                GetRodentDataForChartAnalysis(3);
                break;
            case 29:
                GetRodentDataForChartAnalysis(4);
                break;
            case 30:
                GetRodentDataForChartAnalysis(5);
                break;
            case 31:
                fnUpdBaitConsumed();  // Ragu 05 Mar 19 Rodent Caught Info
                break;
            case 32:
                fnViewBaitConsumed();  // Ragu 05 Mar 19 Rodent Caught Info
                break;
            
        }
    }

    private void fnViewRodentCaught()
    {
        //int intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));

        int intSensorId = 0;
        if (Request.QueryString["sensorid"].Length <= 5)
        {
            intSensorId = Convert.ToInt32(Request.QueryString["sensorid"]);
        }
        else
        {
            intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));
        }

        DateTime dtStartDate = Convert.ToDateTime(Request.QueryString["startdate"]);
        DateTime dtEndDate = Convert.ToDateTime(Request.QueryString["enddate"]);

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {

            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetRodentInfo";
                cmd.Parameters.AddWithValue("m_SensorId", intSensorId);
                cmd.Parameters.AddWithValue("m_startdate", dtStartDate);
                cmd.Parameters.AddWithValue("m_enddate", dtEndDate);

                DataSet dsData = new DataSet();
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                sda.Fill(dsData);

                string strTableData = "";// "<table id=\"tblData\" class=\"table table-bordered table-striped\">\n";
                strTableData = strTableData + "<thead><tr><th>Date Time</th><th>Rodent Type</th><th>No. Of. Caught</th></tr></thead>";
                strTableData = strTableData + " <tbody>";
                

                for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
                {
                    strTableData = strTableData + "<tr>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr][2].ToString() + "</td>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr][3].ToString() + "</td>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr][4].ToString() + "</td>";
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

    private void fnUpdRodentCaught()
    {
        //int intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));

        int intSensorId = 0;
        if (Request.QueryString["sensorid"].Length <= 5)
        {
            intSensorId = Convert.ToInt32(Request.QueryString["sensorid"]);
        }
        else
        {
            intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));
        }


        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {

            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_AddRodentData";
                cmd.Parameters.AddWithValue("m_SensorId", intSensorId);
                cmd.Parameters.AddWithValue("m_dt_timeStamp", Convert.ToDateTime(Request.QueryString["rodcaughtTimeStamp"]));
                cmd.Parameters.AddWithValue("m_Remarks", Convert.ToString(Request.QueryString["rodcaughtrema"]));
                cmd.Parameters.AddWithValue("m_no_of_caught", Convert.ToString(Request.QueryString["noofrodcaught"]));

                cmd.ExecuteNonQuery();
               

            }
            catch (Exception ex)
            {

            }


            conn.Close();


        }
    }



    private void fnViewBaitConsumed()
    {
        //int intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));

        int intSensorId = 0;
        if (Request.QueryString["sensorid"].Length <= 5)
        {
            intSensorId = Convert.ToInt32(Request.QueryString["sensorid"]);
        }
        else
        {
            intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));
        }

        DateTime dtStartDate = Convert.ToDateTime(Request.QueryString["startdate"]);
        DateTime dtEndDate = Convert.ToDateTime(Request.QueryString["enddate"]);

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {

            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetConsumedInfo";
                cmd.Parameters.AddWithValue("m_SensorId", intSensorId);
                cmd.Parameters.AddWithValue("m_startdate", dtStartDate);
                cmd.Parameters.AddWithValue("m_enddate", dtEndDate);

                DataSet dsData = new DataSet();
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                sda.Fill(dsData);

                string strTableData = "";// "<table id=\"tblData\" class=\"table table-bordered table-striped\">\n";
                strTableData = strTableData + "<thead><tr><th>Date Time</th><th>Bait Cosumed %</th><th>Remarks</th></tr></thead>";
                strTableData = strTableData + " <tbody>";


                for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
                {
                    strTableData = strTableData + "<tr>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr][2].ToString() + "</td>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr][4].ToString() + "</td>";
                    strTableData = strTableData + "<td>" + dsData.Tables[0].Rows[intRowCtr][3].ToString() + "</td>";
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

    private void fnUpdBaitConsumed()
    {
        //int intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));

        int intSensorId = 0;
        if (Request.QueryString["sensorid"].Length <= 5)
        {
            intSensorId = Convert.ToInt32(Request.QueryString["sensorid"]);
        }
        else
        {
            intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));
        }


        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {

            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_AddConsumedData";
                cmd.Parameters.AddWithValue("m_SensorId", intSensorId);
                cmd.Parameters.AddWithValue("m_dt_timeStamp", Convert.ToDateTime(Request.QueryString["consumedTimeStamp"]));
                cmd.Parameters.AddWithValue("m_Remarks", Convert.ToString(Request.QueryString["consumedrema"]));
                cmd.Parameters.AddWithValue("m_no_of_consumed", Convert.ToString(Request.QueryString["noofconsumed"]));

                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {

            }


            conn.Close();


        }
    }

    private void GetPlanGroupData()
    {

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            //int intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            //int intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
            int intSiteId = 0;

            if (Request.QueryString["siteid"].Length <= 5)
            {
                intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
            }
            else
            {
                intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            }
            conn.Open();
            Dashboard objDashboard = new Dashboard();
            objDashboard.ObjCon = conn;
            objDashboard.SiteId = intSiteId;
            string strData = objDashboard.GetFloorPlanGroupData();

            objDashboard = null;
            conn.Close();

            Response.Write(strData);
        }
    }


    private void getUserRole()
    {


        string sqlCont = "SELECT i_RoleId FROM userroles where i_UserId='" + Convert.ToInt32(objpwd.DecryptData(Request.QueryString["userId"])) + "'";
        MySqlConnection sqlConnection1 = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());
        sqlConnection1.Open();
        MySqlCommand cmdUser = new MySqlCommand(sqlCont, sqlConnection1);
        MySqlDataReader dataUser = cmdUser.ExecuteReader();
        int retrievedValue = 0;
        while (dataUser.Read())
        {
            retrievedValue = (int)dataUser.GetValue(0);
        }
        dataUser.Close();
        cmdUser.Dispose();
        sqlConnection1.Close();
        Response.Write(retrievedValue);
    }

    private void GetFirstTrigger()
    {
        String retrievedValueDate=null;
        //string sqlCont = "SELECT dt_batChangeDate FROM site where i_SiteId='" + Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"])) + "'";
        string sqlCont = "SELECT dt_batChangeDate FROM site where i_SiteId='" + Convert.ToInt32(Request.QueryString["siteid"]) + "'";
        MySqlConnection sqlConnection1 = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());
        sqlConnection1.Open();
        MySqlCommand cmdUser = new MySqlCommand(sqlCont, sqlConnection1);
        MySqlDataReader dataUser = cmdUser.ExecuteReader();
        
        while (dataUser.Read())
        {
            retrievedValueDate = dataUser.GetValue(0).ToString(); ;
        }
        dataUser.Close();
        cmdUser.Dispose();
        sqlConnection1.Close();
        Response.Write(retrievedValueDate);
    }
    private void getDataForPath()
    {

        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            //int intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            //int intFloorMapId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["floormapid"]));

            int intSiteId;
            if (Request.QueryString["siteid"].Length <= 5)
            {
                intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
            }
            else
            {
                intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            }

            int intFloorMapId;
            if (Request.QueryString["floormapid"].Length <= 5)
            {
                intFloorMapId = Convert.ToInt32(Request.QueryString["floormapid"]);
            }
            else
            {
                intFloorMapId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["floormapid"]));
            }
            DateTime dtStartDate = Convert.ToDateTime(Request.QueryString["startdate"]);
            DateTime dtEndDate = Convert.ToDateTime(Request.QueryString["enddate"]);

            conn.Open();
            Dashboard objDashboard = new Dashboard();
            objDashboard.ObjCon = conn;
            objDashboard.SiteId = intSiteId;
            objDashboard.FloorId = intFloorMapId;
            objDashboard.StartDateTime = dtStartDate;
            objDashboard.EndDateTime = dtEndDate;
            string strData = objDashboard.GetPathDataForDashboard();

            objDashboard = null;
            conn.Close();

            Response.Write(strData);
        }
    }

    private void GetAllSensorDataForDashboard()
    {
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            //int intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            //int intFloorMapId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["floormapid"]));

            int intSiteId;
            if (Request.QueryString["siteid"].Length <= 5)
            {
                intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
            }
            else
            {
                intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            }

            int intFloorMapId;
            if (Request.QueryString["floormapid"].Length <= 5)
            {
                intFloorMapId = Convert.ToInt32(Request.QueryString["floormapid"]);
            }
            else
            {
                intFloorMapId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["floormapid"]));
            }

            DateTime dtStartDate = Convert.ToDateTime(Request.QueryString["startdate"]);
            DateTime dtEndDate = Convert.ToDateTime(Request.QueryString["enddate"]);

            conn.Open();
            Dashboard objDashboard = new Dashboard();
            objDashboard.ObjCon = conn;
            objDashboard.SiteId = intSiteId;
            objDashboard.FloorId = intFloorMapId;
            objDashboard.StartDateTime = dtStartDate;
            objDashboard.EndDateTime = dtEndDate;
            string strData = objDashboard.GetAllSensorDataForDashboard();

            objDashboard = null;
            conn.Close();

            Response.Write(strData);

        }
    }

    private void GetSensorDataForDashboard()
    {
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            int intSiteId = 0;
            int intFloorMapId = 0;
            
            if (Request.QueryString["siteid"].Length >= 5)
            {
                intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            }
            else
            {
                intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
            }
            if (Request.QueryString["floormapid"].Length >= 5)
            {
                intFloorMapId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["floormapid"]));
            }
            else
            {
                intFloorMapId = Convert.ToInt32(Request.QueryString["floormapid"]);

            }

            DateTime dtStartDate = Convert.ToDateTime(Request.QueryString["startdate"]);
            DateTime dtEndDate = Convert.ToDateTime(Request.QueryString["enddate"]);

            conn.Open();
            Dashboard objDashboard = new Dashboard();
            objDashboard.ObjCon = conn;
            objDashboard.SiteId = intSiteId;
            objDashboard.FloorId = intFloorMapId;
            objDashboard.StartDateTime = dtStartDate;
            objDashboard.EndDateTime = dtEndDate;
            string strData = objDashboard.GetSensorDataForDashboard();

            objDashboard = null;
            conn.Close();

            Response.Write(strData);

        }
    }

    private void GetSensorDataForDashboard_uuid()
    {
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {

            int intSiteId = 0;

            if (Request.QueryString["siteid"].Length <= 5)
            {
                intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
            }
            else
            {
                intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            }
            //int intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            //int intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
            //int intFloorMapId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["floormapid"]));
            //int intFloorMapId = Convert.ToInt32(Request.QueryString["floormapid"]);
            int intFloorMapId = 0;
            if (Request.QueryString["floormapid"].Length <= 5)
            {
                intFloorMapId = Convert.ToInt32(Request.QueryString["floormapid"]);
            }
            else
            {
                intFloorMapId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["floormapid"]));
            }

            DateTime dtStartDate = Convert.ToDateTime(Request.QueryString["startdate"]);
            DateTime dtEndDate = Convert.ToDateTime(Request.QueryString["enddate"]);

            conn.Open();
            Dashboard objDashboard = new Dashboard();
            objDashboard.ObjCon = conn;
            objDashboard.SiteId = intSiteId;
            objDashboard.FloorId = intFloorMapId;
            objDashboard.StartDateTime = dtStartDate;
            objDashboard.EndDateTime = dtEndDate;
            string strData = objDashboard.GetSensorDataForDashboard();

            objDashboard = null;
            conn.Close();

            Response.Write(strData);

        }
    }

    private void GetBuildingPlanForSite()
    {
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            //int intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            //string strFloorMap = Convert.ToString(objpwd.DecryptData(Request.QueryString["floormap"]));

            int intSiteId;
            if (Request.QueryString["siteid"].Length <= 5)
            {
                intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
            }
            else
            {
                intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            }

            int intFloorMapId;
            if (Request.QueryString["floormapid"].Length <= 5)
            {
                intFloorMapId = Convert.ToInt32(Request.QueryString["floormapid"]);
            }
            else
            {
                intFloorMapId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["floormapid"]));
            }

            conn.Open();
            Dashboard objDashboard = new Dashboard();
            objDashboard.ObjCon = conn;
            objDashboard.SiteId = intSiteId;

            string strData = objDashboard.GetBuildingPlanForSite();

            objDashboard = null;
            conn.Close();

            Response.Write(strData);

        }
    }

    private void GetPathWay()
    {
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            string strReturnValue=null;
            //int intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            DateTime dtStartDate = Convert.ToDateTime(Request.QueryString["startdate"]);
            DateTime dtEndDate = Convert.ToDateTime(Request.QueryString["enddate"]);
            //int intFloorMapId = Convert.ToInt32(Request.QueryString["intfloorid"]);

            int intSiteId;
            if (Request.QueryString["siteid"].Length <= 5)
            {
                intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
            }
            else
            {
                intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            }

            int intFloorMapId;
            if (Request.QueryString["floormapid"].Length <= 5)
            {
                intFloorMapId = Convert.ToInt32(Request.QueryString["floormapid"]);
            }
            else
            {
                intFloorMapId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["floormapid"]));
            }


            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetPathWayData";
            cmd.Parameters.AddWithValue("m_SiteId", intSiteId);
            cmd.Parameters.AddWithValue("floormap", intFloorMapId);
            cmd.Parameters.AddWithValue("m_StartDate", dtStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("m_EndDate", dtEndDate.ToString("yyyy-MM-dd HH:mm:ss"));
            try
            {
                DataSet dsData = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dsData);

                // JSON Creation
                StringBuilder pathWayValue = new StringBuilder();


                if (dsData.Tables[0].Rows.Count > 0)
                {
                    pathWayValue.Append("{");

                    pathWayValue.Append("\"SiteName\":");
                    pathWayValue.Append("\"");
                    pathWayValue.Append(dsData.Tables[0].Rows[0][0].ToString());
                    pathWayValue.Append("\",");

                    pathWayValue.Append("\"LocationName\":");
                    pathWayValue.Append("\"");
                    pathWayValue.Append(dsData.Tables[1].Rows[0][3].ToString());
                    pathWayValue.Append("\",");

                    pathWayValue.Append("\"FloorPathFileName\":");
                    pathWayValue.Append("\"");
                    //pathWayValue.Append("http://pestech.ddns.net:8087/harbourfront/lap/assets/heatmap/HARBOURFRONT/basemaps/");
                    //pathWayValue.Append("201010.png");
                    pathWayValue.Append("http://pestech.ddns.net/plans/");
                    pathWayValue.Append(dsData.Tables[1].Rows[0][2].ToString());
                    pathWayValue.Append("\",");

                    int imgWidth = 0;
                    int imgHeight = 0;

                    string filepath = "E:/Rast_Update/plans/" + dsData.Tables[1].Rows[0][2].ToString();
                    System.Drawing.Image objImage = System.Drawing.Image.FromFile(filepath);
                    imgWidth = objImage.Width;
                    imgHeight = objImage.Height;

                    pathWayValue.Append("\"FloorWidth\":");
                    //pathWayValue.Append("\"");
                    pathWayValue.Append(imgWidth);
                    pathWayValue.Append(",");

                    pathWayValue.Append("\"FloorHeight\":");
                    //pathWayValue.Append("\"");
                    pathWayValue.Append(imgHeight);
                    pathWayValue.Append(",");

                    pathWayValue.Append("\"dateFrom\":");
                    pathWayValue.Append("\"");
                    pathWayValue.Append(dtStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    pathWayValue.Append("\",");

                    pathWayValue.Append("\"dateTo\":");
                    pathWayValue.Append("\"");
                    pathWayValue.Append(dtEndDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    pathWayValue.Append("\",");

                    if (dsData.Tables[2].Rows.Count > 0)
                    {
                        //Sensor Info
                        pathWayValue.Append("\"SensorInfo\":");
                        pathWayValue.Append("[");
                        for (int i = 0; i < dsData.Tables[2].Rows.Count; i++)
                        {
                            if (i != 0)
                            {
                                pathWayValue.Append(",");
                            }
                            pathWayValue.Append("{");
                            pathWayValue.Append("\"sensor_id\":");
                            pathWayValue.Append(Convert.ToInt32(objpwd.DecryptData(dsData.Tables[2].Rows[i][0].ToString())));
                            pathWayValue.Append(",");

                            pathWayValue.Append("\"sensor_name\":");
                            pathWayValue.Append("\"");
                            pathWayValue.Append(dsData.Tables[2].Rows[i][2].ToString());
                            pathWayValue.Append("\",");

                            pathWayValue.Append("\"sensor_location\":");
                            pathWayValue.Append("\"");
                            pathWayValue.Append(dsData.Tables[2].Rows[i][13].ToString());
                            pathWayValue.Append("\",");

                            //pathWayValue.Append("\"sensor_id\":");
                            //pathWayValue.Append(Convert.ToInt32(dsData.Tables[2].Rows[i][0].ToString()));
                            //pathWayValue.Append(",");

                            //x = x + 25;
                            //y = y - 225;  

                            pathWayValue.Append("\"x_pos\":");
                            pathWayValue.Append(Convert.ToInt32(dsData.Tables[2].Rows[i][5].ToString())+25);
                            pathWayValue.Append(",");

                            pathWayValue.Append("\"y_pos\":");
                            pathWayValue.Append(Convert.ToInt32(dsData.Tables[2].Rows[i][6].ToString())-225);

                            pathWayValue.Append("}");
                        }
                        pathWayValue.Append("],");

                        //Trigger Info
                        pathWayValue.Append("\"TriggerData\":");
                        pathWayValue.Append("[");
                        for (int i = 0; i < dsData.Tables[3].Rows.Count; i++)
                        {
                            if (i != 0)
                            {
                                pathWayValue.Append(",");
                            }

                            pathWayValue.Append("{");
                            pathWayValue.Append("\"sensor_id\":");
                            pathWayValue.Append(Convert.ToInt32(objpwd.DecryptData(dsData.Tables[3].Rows[i][0].ToString())));
                            pathWayValue.Append(",");

                            pathWayValue.Append("\"datetime\":");
                            pathWayValue.Append("\"");
                            pathWayValue.Append(dsData.Tables[3].Rows[i][1].ToString());
                            //string dt = dsData.Tables[3].Rows[i][1].ToString();
                            //dt = dt.ToString("yyyy-MM-dd HH:mm:ss");
                            //pathWayValue.Append(dsData.Tables[3].Rows[i][1].ToString("yyyy-MM-dd HH:mm:ss"));
                            //pathWayValue.Append("14/01/2019 12:28:00");
                            pathWayValue.Append("\"");
                            pathWayValue.Append("}");
                        }
                        pathWayValue.Append("]}");
                        dsData = null;
                        strReturnValue = pathWayValue.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            Response.Write(strReturnValue);
        }
    }

    private void GetDataForChartAnalysis(int sel) //Ragu 19 Aug  2018
    {
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            //int intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            //int intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);

            int intSiteId = 0;

            if (Request.QueryString["siteid"].Length <= 5)
            {
                intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
            }
            else
            {
                intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            }

            DateTime dtStartDate = Convert.ToDateTime(Request.QueryString["startdate"]);
            DateTime dtEndDate = Convert.ToDateTime(Request.QueryString["enddate"]);
            //int intFloorMapId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["floormapid"]));
            //int intFloorMapId = Convert.ToInt32(Request.QueryString["floormapid"]);

            int intFloorMapId = 0;
            if (Request.QueryString["intFloorId"].Length <= 5)
            {
                intFloorMapId = Convert.ToInt32(Request.QueryString["intFloorId"]);
            }
            else
            {
                intFloorMapId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["intFloorId"]));
            }

            Dashboard objDashboard = new Dashboard();
            objDashboard.ObjCon = conn;
            objDashboard.SiteId = intSiteId;
            objDashboard.StartDateTime = dtStartDate;
            objDashboard.EndDateTime = dtEndDate;
            objDashboard.FloorId = intFloorMapId;

            string strData = null;
            if (sel == 0)
                strData = objDashboard.GetTriggerDataBySite_Day();
            else if (sel == 1)
                strData = objDashboard.GetTriggerDataByFloor_Day();
            else if (sel == 2)
                strData = objDashboard.GetTriggerDataBySite_Week();
            else if (sel == 3)
                strData = objDashboard.GetTriggerDataByFloor_Week();
            else if (sel == 4)
                strData = objDashboard.GetTriggerDataBySite_Month();
            else if (sel == 5)
                strData = objDashboard.GetTriggerDataByFloor_Month();

            objDashboard = null;
            conn.Close();

            Response.Write(strData);

        }
    }

    private void GetRodentDataForChartAnalysis(int sel) //Ragu 12 Nov  2019
    {
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            /*int intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            DateTime dtStartDate = Convert.ToDateTime(Request.QueryString["startdate"]);
            DateTime dtEndDate = Convert.ToDateTime(Request.QueryString["enddate"]);
            int intFloorMapId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["floormapid"]));*/

            int intSiteId = 0;
            int intFloorMapId = 0;
            if (Request.QueryString["siteid"].Length <= 5)
            {
                intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
            }
            else
            {
                intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            }

            if (Request.QueryString["floormapid"].Length <= 5)
            {
                intFloorMapId = Convert.ToInt32(Request.QueryString["floormapid"]);
            }
            else
            {
                intFloorMapId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["floormapid"]));
            }


            //int intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
            //int intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);
            DateTime dtStartDate = Convert.ToDateTime(Request.QueryString["startdate"]);
            DateTime dtEndDate = Convert.ToDateTime(Request.QueryString["enddate"]);
            //int intFloorMapId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["floormapid"]));
            //int intFloorMapId = Convert.ToInt32(Request.QueryString["floormapid"]);

            Dashboard objDashboard = new Dashboard();
            objDashboard.ObjCon = conn;
            objDashboard.SiteId = intSiteId;
            objDashboard.StartDateTime = dtStartDate;
            objDashboard.EndDateTime = dtEndDate;
            objDashboard.FloorId = intFloorMapId;

            string strData = null;
            if (sel == 0)
                strData = objDashboard.GetRodentTriggerDataBySite_Day();
            else if (sel == 1)
                strData = objDashboard.GetRodentTriggerDataByFloor_Day();
            else if (sel == 2)
                strData = objDashboard.GetRodentTriggerDataBySite_Week();
            else if (sel == 3)
                strData = objDashboard.GetRodentTriggerDataByFloor_Week();
            else if (sel == 4)
                strData = objDashboard.GetRodentTriggerDataBySite_Month();
            else if (sel == 5)
                strData = objDashboard.GetRodentTriggerDataByFloor_Month();

            objDashboard = null;
            conn.Close();

            Response.Write(strData);

        }
    }

    private void GetDataForChart()
    {
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            int intSensorId = 0;
            if (Request.QueryString["sensorid"].Length <= 5)
            {
                intSensorId = Convert.ToInt32(Request.QueryString["sensorid"]);
            }
            else
            {
                intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));
            }

            //int intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["SensorId"]));
            DateTime dtStartDate = Convert.ToDateTime(Request.QueryString["startdate"]);
            DateTime dtEndDate = Convert.ToDateTime(Request.QueryString["enddate"]);

            conn.Open();
            Dashboard objDashboard = new Dashboard();
            objDashboard.ObjCon = conn;
            objDashboard.SensorId = intSensorId;
            objDashboard.StartDateTime = dtStartDate;
            objDashboard.EndDateTime = dtEndDate;
            string strData = objDashboard.GetDataForChart();
            if (strData.Length <= 2)  //Ragu 12 jun 
            {
                strData = objDashboard.GetDataForChart_LocationDet();
            }

            objDashboard = null;
            conn.Close();

            Response.Write(strData);

        }
    }

    private void GetDataTable()
    {
        // int intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));

        int intSensorId = 0;
        if (Request.QueryString["sensorid"].Length <= 5)
        {
            intSensorId = Convert.ToInt32(Request.QueryString["sensorid"]);
        }
        else
        {
            intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));
        }


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
                cmd.CommandText = "p_GetDataForChart";
                cmd.Parameters.AddWithValue("m_SensorId", intSensorId);
                cmd.Parameters.AddWithValue("m_StartDate", dtStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("m_EndDate", dtEndDate.ToString("yyyy-MM-dd HH:mm:ss"));
                DataSet dsData = new DataSet();
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                sda.Fill(dsData);

                string strTableData = "";// "<table id=\"tblData\" class=\"table table-bordered table-striped\">\n";
                strTableData = strTableData + "<thead><tr><th>Date Time</th><th>Sensor Value</th></tr></thead>";
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

                strTableData = strTableData + " </tbody>";//</table>";

                Response.Write(strTableData);

            }
            catch (Exception ex)
            {

            }


            conn.Close();


        }
    }

    private string GetSensorDataForExcel()
    {
        string retFileName = "nodata";
        //int intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));

        int intSensorId = 0;
        if (Request.QueryString["sensorid"].Length <= 5)
        {
            intSensorId = Convert.ToInt32(Request.QueryString["sensorid"]);
        }
        else
        {
            intSensorId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["sensorid"]));
        }


        DateTime dtStartDate = Convert.ToDateTime(Request.QueryString["startdate"]);
        DateTime dtEndDate = Convert.ToDateTime(Request.QueryString["enddate"]);

        string strTemplatePath = Convert.ToString(ConfigurationManager.AppSettings["ExcelSensorTemplate"]);
        string strReportPath = Convert.ToString(ConfigurationManager.AppSettings["ExcelSensorReport"]);
        string date = DateTime.Now.ToString("yyyyMMdd-HHMMss");
        string strStationType = "S_" + intSensorId.ToString() + "_" + date + ".xlsx";

        if (File.Exists(strTemplatePath))
        {
            FileInfo templateFile = new FileInfo(strTemplatePath);
            FileInfo newFile = new FileInfo(strReportPath + strStationType);
            string strconnection = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
            try
            {
                MySqlConnection conn = new MySqlConnection(strconnection);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetDataForChart";
                cmd.Parameters.AddWithValue("m_SensorId", intSensorId);
                cmd.Parameters.AddWithValue("m_StartDate", dtStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("m_EndDate", dtEndDate.ToString("yyyy-MM-dd HH:mm:ss"));
                DataSet dsData = new DataSet();
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                sda.Fill(dsData);
                DataTable dtChartdata = dsData.Tables[0];

                using (ExcelPackage p = new ExcelPackage(newFile, templateFile))
                {
                    //Set up the headers
                    var ws = p.Workbook.Worksheets["SNDA"];

                    int intCurrentRow = 29;
                    int intColNo = 4;
                    string strColumn = "";

                    DataTable dtInterimData;
                    dtInterimData = dtChartdata.Clone();
                    dtInterimData = dtChartdata.Copy();

                    DateTime dtCurrentTime = DateTime.Now;
                    DateTime dtPreviousTime = DateTime.Now;

                    if (dtInterimData.Rows.Count > 0)
                    {
                        dtInterimData.Columns.Remove("dt_DateTime_Data");
                        dtInterimData.Columns.Remove("sensor_min");
                        dtInterimData.Columns.Remove("sensor_max");
                        ws.Cells["E29"].LoadFromDataTable(dtInterimData, true);
                        intCurrentRow = 30;
                        intColNo = 5;

                        ws.Cells["C29"].Value = "Date";
                        ws.Cells["D29"].Value = "Time";

                        for (int intRowCtr = 0; intRowCtr < dtChartdata.Rows.Count; intRowCtr++)
                        {

                            ws.Cells["B" + intCurrentRow.ToString()].Value = Convert.ToString(intRowCtr + 1);
                            ws.Cells["C" + intCurrentRow.ToString()].Value = Convert.ToDateTime(dtChartdata.Rows[intRowCtr][0]).ToString("dd-MM-yyyy");
                            ws.Cells["D" + intCurrentRow.ToString()].Value = Convert.ToDateTime(dtChartdata.Rows[intRowCtr][0]).ToString("HH:mm:ss");
                            ws.Cells["L" + intCurrentRow.ToString()].Value = Convert.ToDateTime(dtChartdata.Rows[intRowCtr][0]).ToString("dd-MM-yyyy HH:mm:ss");

                            intColNo = 5;

                            for (int intExcelColCtr = 1; intExcelColCtr < dtChartdata.Columns.Count - 4; intExcelColCtr++)
                            {
                                strColumn = fnGetCellAddress(intColNo);

                                if (dtChartdata.Rows[intRowCtr][intExcelColCtr] != DBNull.Value)
                                {
                                    if (dtChartdata.Rows[intRowCtr][intExcelColCtr].ToString().Trim() == "")
                                    {
                                        ws.Cells[strColumn + intCurrentRow.ToString()].Formula = "NA()";
                                    }
                                }
                                else
                                {
                                    ws.Cells[strColumn + intCurrentRow.ToString()].Formula = "NA()";
                                }
                                intColNo++;

                            }

                            intCurrentRow = intCurrentRow + 1;
                        }

                        ExcelChart chart = ((ExcelChart)ws.Drawings["Chart 1"]);

                        ws.Cells["B2"].Value = "Sensor Data";

                        intCurrentRow = 30;
                        intColNo = 5;

                        for (int intColCtr = 1; intColCtr < dtChartdata.Columns.Count - 2; intColCtr++)
                        {

                            strColumn = fnGetCellAddress(intColNo);
                            chart.Series[intColCtr - 1].Header = dtChartdata.Columns[intColCtr].ColumnName.ToString();
                            chart.Series[intColCtr - 1].XSeries = "'" + ws.Name + "'!" + ExcelRange.GetAddress(intCurrentRow, 12, intCurrentRow + dtChartdata.Rows.Count, 12);
                            chart.Series[intColCtr - 1].Series = "'" + ws.Name + "'!" + ExcelRange.GetAddress(intCurrentRow, intColNo, intCurrentRow + dtChartdata.Rows.Count, intColNo);
                            intColNo++;

                        }


                        ws.Cells["E30:" + fnGetCellAddress(intColNo + dtChartdata.Columns.Count - 1) + Convert.ToString(intCurrentRow + dtChartdata.Rows.Count)].Style.Numberformat.Format = "#,##0.00";

                        intColNo = 5;
                        string strRange = "B30:" + fnGetCellAddress(intColNo + dtChartdata.Columns.Count - 4) + Convert.ToString(intCurrentRow + dtChartdata.Rows.Count - 1);

                        if (intCurrentRow + dtChartdata.Rows.Count - 1 > 30)
                        {

                            ws.Cells[strRange].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            ws.Cells[strRange].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            ws.Cells[strRange].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            ws.Cells[strRange].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                        }


                        p.SaveAs(newFile);
                        retFileName = "../report/" + strStationType;
                        conn.Close();
                    }



                }
            }
            catch (Exception ex)
            {

            }
        }
        return retFileName;
    }

    protected string fnGetCellAddress(int intColno)
    {
        string[] arrStrAlph, arrAlphCount;
        string strAlph, strResult;
        int intCellVal, intCellMod;
        strResult = "";
        strAlph = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";

        arrStrAlph = strAlph.Split(',');

        if (intColno <= 26)
        {
            strResult = arrStrAlph[intColno - 1];
        }
        else if (intColno >= 26)
        {
            arrAlphCount = Convert.ToString(Convert.ToInt32(intColno) / 26).Split('.');
            intCellMod = intColno % 26;
            intCellVal = Convert.ToInt32(arrAlphCount[0]);
            if (intCellMod - 1 == -1)
            {
                intCellMod = 26;
                intCellVal = intCellVal - 1;
            }
            strResult = arrStrAlph[intCellVal - 1] + arrStrAlph[intCellMod - 1];
        }
        return (strResult);
    }

    public void GetLocationsForSite()
    {
        //int intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["siteid"]));
        //int intSiteId = Convert.ToInt32(Request.QueryString["siteid"]);

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
            Sites objSite = new Sites();
            objSite.ObjCon = conn;
            objSite.SiteId = intSiteId;

            DataSet dsFloorMapImage = objSite.GetBuildingMapForStation();
            string strData = ConvertDataTabletoString(dsFloorMapImage.Tables[0]);

            dsFloorMapImage = null;
            Response.Write(strData);
        }

    }

    public void GetFloorMapForSite()
    {
        //int intFloorMapId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["floormapid"]));
        //int intFloorMapId = Convert.ToInt32(Request.QueryString["floormapid"]);
        int intFloorMapId = 0;
        if (Request.QueryString["floormapid"].Length <= 5)
        {
            intFloorMapId = Convert.ToInt32(Request.QueryString["floormapid"]);
        }
        else
        {
            intFloorMapId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["floormapid"]));
        }

        string strFloorMapImage = "";
        using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
        {
            conn.Open();
            Dashboard objDashboard = new Dashboard();
            objDashboard.ObjCon = conn;
            strFloorMapImage = objDashboard.GetFloorMapImage(intFloorMapId);
            objDashboard = null;
            conn.Close();
        }
        Response.Write(strFloorMapImage);
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