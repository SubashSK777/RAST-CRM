using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Web.Script;
using System.Drawing;

namespace RAST.DAL
{
    public class Dashboard
    {
        private int m_SiteId;
        private int m_SensorId;
        private DateTime m_StartDateTime;
        private DateTime m_EndDateTime;
        private DateTime m_LastDataReceived;
        private int m_SensorValue;
        private int m_MinThresholdLevel;
        private int m_MaxThresholdLevel;
        private int m_XPos;
        private int m_YPos;
        private string m_FloorMapImage;
        private int m_FloorId;
        private DateTime i_SensorMin;

        public int FloorId
        {
            get { return m_FloorId; }
            set { m_FloorId = value; }
        }
        private MySqlConnection m_ObjCon;


        public MySqlConnection ObjCon
        {
            get { return m_ObjCon; }
            set { m_ObjCon = value; }
        }
        public int SiteId
        {
            set { m_SiteId = value; }
        }

        public int SensorId
        {
            set { m_SensorId = value; }
        }
        public DateTime StartDateTime
        {
            set { m_StartDateTime = value; }
        }

        public DateTime EndDateTime
        {
            set { m_EndDateTime = value; }
        }

        public int SensorValue
        {
            get { return m_SensorValue; }
        }
        public int MinThresholdLevel
        {
            get { return m_MinThresholdLevel; }
        }
        public int MaxThresholdLevel
        {
            get { return m_MaxThresholdLevel; }
        }
        public int XPos
        {
            get { return m_XPos; }
        }
        public int YPos
        {
            get { return m_YPos; }
        }

        public DateTime SensorMinDate
        {
            set { i_SensorMin = value; }
        }

        

        public string GetPathDataForDashboard1()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetPathDataForDashboard";
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            cmd.Parameters.AddWithValue("floormap", m_FloorId);
            cmd.Parameters.AddWithValue("m_StartDate", m_StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("m_EndDate", m_EndDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            try
            {
                DataSet dsData = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dsData);

                int varFloorID, varSensorID, var_X, var_Y, varFloorID1, varSensorID1, var_X1, var_Y1;
                DateTime vartriggerTime;
                int flag = 0;
                if (dsData.Tables[0].Rows.Count > 0)
                {

                    //int batMaxTrigger = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["BatteryMaxTrigger"].ToString());
                    int nearestDistanceX = 200;
                    int nearestDistanceY = 100;
                    for (int intRowCtr = 0; intRowCtr < dsData.Tables[0].Rows.Count; intRowCtr++)
                    {
                        varFloorID = Convert.ToInt16(dsData.Tables[0].Rows[intRowCtr][0].ToString());
                        varSensorID = Convert.ToInt16(dsData.Tables[0].Rows[intRowCtr][1].ToString());
                        vartriggerTime = Convert.ToDateTime(dsData.Tables[0].Rows[intRowCtr][2].ToString());
                        var_X = Convert.ToInt16(dsData.Tables[0].Rows[intRowCtr][3].ToString());
                        var_Y = Convert.ToInt16(dsData.Tables[0].Rows[intRowCtr][4].ToString());
                        for (int intcolCtr = intRowCtr + 1; intcolCtr < dsData.Tables[0].Rows.Count; intcolCtr++)
                        {
                            flag = 0;
                            varSensorID1 = Convert.ToInt16(dsData.Tables[0].Rows[intcolCtr][1].ToString());
                            var_X1 = Convert.ToInt16(dsData.Tables[0].Rows[intcolCtr][3].ToString());
                            var_Y1 = Convert.ToInt16(dsData.Tables[0].Rows[intcolCtr][4].ToString());
                            if (var_X > var_X1)
                            {
                                if ((var_X - var_X1) <= nearestDistanceX)
                                {
                                    flag = 1;
                                }
                            }
                            if (var_X1 > var_X)
                            {
                                if ((var_X1 - var_X) <= nearestDistanceX)
                                {
                                    flag = 1;
                                }
                            }

                            if (flag == 1)
                            {

                                if (var_Y > var_Y1)
                                {
                                    if ((var_Y - var_Y1) <= nearestDistanceY)
                                    {
                                        flag = 2;
                                    }
                                }
                                if (var_Y1 > var_Y)
                                {
                                    if ((var_Y1 - var_Y) <= nearestDistanceY)
                                    {
                                        flag = 2;
                                    }
                                }
                            }
                            if (flag == 2)
                            {
                                strReturnValue = strReturnValue + var_X + "," + var_Y + "," + var_X1 + "," + var_Y1 + "," + vartriggerTime + ".";
                                intcolCtr = dsData.Tables[0].Rows.Count; // For Terminate the Path. 1 to 1 only
                            }
                        }

                    }
                }


                dsData = null;
            }
            catch (Exception ex)
            {

            }
            return strReturnValue;

        }

        public string GetPathDataForDashboard()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetPathDataForDashboard";
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            cmd.Parameters.AddWithValue("floormap", m_FloorId);
            cmd.Parameters.AddWithValue("m_StartDate", m_StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("m_EndDate", m_EndDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            try
            {
                DataSet dsData = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dsData);

                strReturnValue = ConvertDataTabletoString(dsData.Tables[0]);
                dsData = null;
            }
            catch (Exception ex)
            {

            }
            return strReturnValue;

        }

        public string GetSensorDataForDashboard()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetSensorDataForDashboard";
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            cmd.Parameters.AddWithValue("floormap", m_FloorId);
            cmd.Parameters.AddWithValue("m_StartDate", m_StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("m_EndDate", m_EndDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
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

            return strReturnValue;

        }

        public string GetAllSensorDataForDashboard()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetAllSensorDataForDashboard";
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            cmd.Parameters.AddWithValue("m_floormap", m_FloorId);
            cmd.Parameters.AddWithValue("m_StartDate", m_StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("m_EndDate", m_EndDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
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

            return strReturnValue;

        }


        public string GetBuildingPlanForSite()
        {

            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetBuildingPlanForSite";
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            try
            {

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        strReturnValue = reader.GetString(reader.GetOrdinal("s_BuildingFloorMap"));
                    }
                }
            }
            catch (Exception exp)
            {

            }
            cmd = null;

            return strReturnValue;

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

        public string GetDataForChart()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetDataForChart";
            cmd.Parameters.AddWithValue("m_SensorId", m_SensorId);
            cmd.Parameters.AddWithValue("m_StartDate", m_StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("m_EndDate", m_EndDateTime.ToString("yyyy-MM-dd HH:mm:ss"));


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

            return strReturnValue;
        }

        public string GetDataForChart_LocationDet()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetDataLocDet";
            cmd.Parameters.AddWithValue("m_SensorId", m_SensorId);

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

            return strReturnValue;
        }


        public string GetFloorMapImage(int intFloorMapId)
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select s_FloorMapImage from site_images where i_SiteImageId = " + intFloorMapId.ToString();


            try
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        strReturnValue = reader.GetString(reader.GetOrdinal("s_FloorMapImage"));
                    }
                }
            }
            catch (Exception exp)
            {

            }
            cmd = null;
            return strReturnValue;
        }


        public string GetTriggerDataBySite_Day()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetTriggerDataBySite_Day";
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            //cmd.Parameters.AddWithValue("m_floormap", m_FloorId);
            cmd.Parameters.AddWithValue("m_StartDate", m_StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("m_EndDate", m_EndDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
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

            return strReturnValue;

        }

        public string GetTriggerDataByFloor_Day()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetTriggerDataByFloor_Day";
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            //cmd.Parameters.AddWithValue("m_floormap", m_FloorId);
            cmd.Parameters.AddWithValue("m_StartDate", m_StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("m_EndDate", m_EndDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
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

            return strReturnValue;

        }


        public string GetTriggerDataBySite_Week()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetTriggerDataBySite_Week";
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            //cmd.Parameters.AddWithValue("m_floormap", m_FloorId);
            cmd.Parameters.AddWithValue("m_StartDate", m_StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("m_EndDate", m_EndDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
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

            return strReturnValue;

        }

        public string GetTriggerDataByFloor_Week()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetTriggerDataByFloor_Week";
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            cmd.Parameters.AddWithValue("m_floormap", m_FloorId);
            cmd.Parameters.AddWithValue("m_StartDate", m_StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("m_EndDate", m_EndDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
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

            return strReturnValue;

        }

        public string GetTriggerDataBySite_Month()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetTriggerDataBySite_Month";
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            //cmd.Parameters.AddWithValue("m_floormap", m_FloorId);
            cmd.Parameters.AddWithValue("m_StartDate", m_StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("m_EndDate", m_EndDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
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

            return strReturnValue;

        }

        public string GetTriggerDataByFloor_Month()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetTriggerDataByFloor_Month";
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            //cmd.Parameters.AddWithValue("m_floormap", m_FloorId);
            cmd.Parameters.AddWithValue("m_StartDate", m_StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("m_EndDate", m_EndDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
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

            return strReturnValue;

        }


        public string GetRodentTriggerDataBySite_Day()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetRodentTriggerDataBySite_Day";
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            //cmd.Parameters.AddWithValue("m_floormap", m_FloorId);
            cmd.Parameters.AddWithValue("m_StartDate", m_StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("m_EndDate", m_EndDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
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

            return strReturnValue;

        }

        public string GetRodentTriggerDataByFloor_Day()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetRodentTriggerDataByFloor_Day";
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            //cmd.Parameters.AddWithValue("m_floormap", m_FloorId);
            cmd.Parameters.AddWithValue("m_StartDate", m_StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("m_EndDate", m_EndDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
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

            return strReturnValue;

        }


        public string GetRodentTriggerDataBySite_Week()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetRodentTriggerDataBySite_Week";
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            //cmd.Parameters.AddWithValue("m_floormap", m_FloorId);
            cmd.Parameters.AddWithValue("m_StartDate", m_StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("m_EndDate", m_EndDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
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

            return strReturnValue;

        }

        public string GetRodentTriggerDataByFloor_Week()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetRodentTriggerDataByFloor_Week";
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            cmd.Parameters.AddWithValue("m_floormap", m_FloorId);
            cmd.Parameters.AddWithValue("m_StartDate", m_StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("m_EndDate", m_EndDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
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

            return strReturnValue;

        }

        public string GetRodentTriggerDataBySite_Month()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetRodentTriggerDataBySite_Month";
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            //cmd.Parameters.AddWithValue("m_floormap", m_FloorId);
            cmd.Parameters.AddWithValue("m_StartDate", m_StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("m_EndDate", m_EndDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
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

            return strReturnValue;

        }

        public string GetRodentTriggerDataByFloor_Month()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetRodentTriggerDataByFloor_Month";
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            //cmd.Parameters.AddWithValue("m_floormap", m_FloorId);
            cmd.Parameters.AddWithValue("m_StartDate", m_StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("m_EndDate", m_EndDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
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

            return strReturnValue;

        }

        public string GetFloorPlanGroupData()
        {
            string strReturnValue = "";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_GetFloorPlan";
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
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

            return strReturnValue;

        }
    }
}

