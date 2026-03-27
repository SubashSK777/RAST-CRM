using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;



namespace RAST.DAL
{
    public class Sensors : IDALObject
    {
        private int m_Sensor;
        private int m_SiteId;
        private int m_HubId;
        private string m_SensorId;
        private int m_MinThresholdLevel;
        private int m_MaxThresholdLevel;
        private int m_X;
        private int m_Y;
        private bool m_Status;
        private MySqlConnection m_ObjCon;
        private int m_ReturnValue;
        private string m_unit;
        private string m_SensorType;

        private int m_XRange;
        private int m_YRange;
        private int m_FloorId;
        private string m_sensorlocation;
        private string m_qrCode;





        public MySqlConnection ObjCon
        {
            get { return m_ObjCon; }
            set { m_ObjCon = value; }
        }

        public int Sensor
        {
            get { return m_Sensor; }
            set { m_Sensor = value; }
        }

        public int SiteId
        {
            get { return m_SiteId; }
            set { m_SiteId = value; }
        }

       

        public int HubId
        {
            get { return m_HubId; }
            set { m_HubId = value; }
        }

        public string SensorId
        {
            get { return m_SensorId; }
            set { m_SensorId = value; }
        }

        public int MinThresholdLevel
        {
            get { return m_MinThresholdLevel; }
            set { m_MinThresholdLevel = value; }
        }

        public int MaxThresholdLevel
        {
            get { return m_MaxThresholdLevel; }
            set { m_MaxThresholdLevel = value; }
        }

        public string unit
        {
            get { return m_unit; }
            set { m_unit = value; }
        }

        public string SensorType
        {
            get { return m_SensorType; }
            set { m_SensorType = value; }
        }

        public string SensorLocation
        {
            get { return m_sensorlocation; }
            set { m_sensorlocation = value; }
        }

        public string qrCode
        {
            get { return m_qrCode; }
            set { m_qrCode = value; }
        }

        public int X
        {
            get { return m_X; }
            set { m_X = value; }
        }

        public int Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }
        public int XRange
        {
            get { return m_XRange; }
            set { m_XRange = value; }
        }
        public int YRange
        {
            get { return m_YRange; }
            set { m_YRange = value; }
        }

        public bool Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }


        public int ReturnValue
        {
            get { return m_ReturnValue; }
            set { m_ReturnValue = value; }
        }

        public int FloorId
        {
            get { return m_FloorId; }
            set { m_FloorId = value; }
        }

        

        public DataSet ReadElementsForUI(int intId)
        {
            DataSet dsData = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = m_ObjCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetDataForSensors";
                cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dsData);

                da = null;
                cmd = null;

            }
            catch (MySqlException ex)
            {

            }

            m_ObjCon.Close();
            m_ObjCon = null;




            return dsData;
        }

        

        public DataSet ReadDataset(int intUserId, int intCurrentPage, int intPageSize)
        {
            return new DataSet();
        }
        public IDataReader ReadDataReader(int intCurrentPage, int intPageSize)
        {
            IDataReader drSQL = null;
            return drSQL;
        }
        public DataSet ReadUIData(int intId)
        {
            return new DataSet();
        }
        public int Add()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_AddSensor";
            cmd.Parameters.AddWithValue("m_Sensor", m_Sensor);
            cmd.Parameters.AddWithValue("m_HubId", m_HubId);
            cmd.Parameters.AddWithValue("m_SensorId", m_SensorId);
            cmd.Parameters.AddWithValue("m_MinThresholdLevel", m_MinThresholdLevel);
            cmd.Parameters.AddWithValue("m_MaxThresholdLevel", m_MaxThresholdLevel);
            cmd.Parameters.AddWithValue("m_X", m_X);
            cmd.Parameters.AddWithValue("m_Y", m_Y);
            cmd.Parameters.AddWithValue("m_Status", m_Status);
            cmd.Parameters.AddWithValue("m_sensortype", m_SensorType);
            cmd.Parameters.AddWithValue("m_unit", m_unit);

            cmd.Parameters.AddWithValue("m_XRange", m_XRange);
            cmd.Parameters.AddWithValue("m_YRange", m_YRange);
            cmd.Parameters.AddWithValue("m_FloorId", m_FloorId);
            cmd.Parameters.AddWithValue("m_sensorlocation", m_sensorlocation);
            cmd.Parameters.AddWithValue("m_qrCode", m_qrCode);
            try
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        m_ReturnValue = reader.GetInt32(reader.GetOrdinal("intStatus"));
                    }
                }
            }
            catch (Exception exp)
            {

            }
            cmd = null;

            m_ObjCon.Close();
            m_ObjCon = null;

            return m_ReturnValue;

        }

        public int Update() { return 0; }
        public int Delete()
        {


            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_DeleteSensor";
            cmd.Parameters.AddWithValue("m_Sensor", m_Sensor);
            try
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        m_ReturnValue = reader.GetInt32(reader.GetOrdinal("intStatus"));
                    }
                }
            }
            catch (Exception exp)
            {

            }
            cmd = null;

            m_ObjCon.Close();
            m_ObjCon = null;

            return m_ReturnValue;



        }
        public int Enable() { return 0; }
        public int Disable() { return 0; }

        public DataSet GetSensorData()
        {
            DataSet dsData = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = m_ObjCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetSensorDetails";
                cmd.Parameters.AddWithValue("m_SensorId", m_Sensor);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dsData);

                da = null;
                cmd = null;

            }
            catch (MySqlException ex)
            {

            }

            m_ObjCon.Close();
            m_ObjCon = null;

            return dsData;
            //return ConvertDataTabletoString(dsData.Tables[0]);

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

        public DataSet fnGetSensorsForFloorMap(int intFloorMapId, int intSiteId)
        {
            DataSet dsData = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = m_ObjCon;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select i_HubId, s_HubId FROM hub WHERE i_SiteId = " + intSiteId.ToString() + ";" + "SELECT i_SensorId, s_HubId, s_SensorId, i_MinThresholdLevel, i_MaxThresholdLevel, i_X, i_Y, b_Status, s_SensorType, s_Unit, i_MinRange, i_MaxRange FROM hub h, sensor s WHERE h.i_hubid = s.i_hubid AND h.i_siteid = " + intSiteId.ToString() + " AND i_SiteImageId = " + intFloorMapId.ToString() + "; ";


                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dsData);

                da = null;
                cmd = null;

            }
            catch (MySqlException ex)
            {

            }

            m_ObjCon.Close();
            m_ObjCon = null;




            return dsData;

            

        }

    }
}
