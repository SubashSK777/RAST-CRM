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
    public class Configuration
    {
        private MySqlConnection m_ObjCon;
        private int m_MaxDataStorage;
        private int m_SensorCounter;

        public MySqlConnection ObjCon
        {
            get { return m_ObjCon; }
            set { m_ObjCon = value; }
        }

        public int MaxDataStorage
        {
            get { return m_MaxDataStorage; }
            set { m_MaxDataStorage = value; }
        }

        public int SensorCounter
        {
            get { return m_SensorCounter; }
            set { m_SensorCounter = value; }
        }

        public DataSet ReadDataset(int intUserId, int intCurrentPage, int intPageSize)
        {
            DataSet dsData = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = m_ObjCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetConfiguration";
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
    
        public void SaveConfiguration()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_SaveConfiguration";
            cmd.Parameters.AddWithValue("m_MaxDataStorage", m_MaxDataStorage);

            cmd.ExecuteNonQuery();
     
            cmd = null;
            m_ObjCon.Close();
            m_ObjCon = null;

        }

        public void ResetCounter()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_ResetSensorCounter";
            cmd.ExecuteNonQuery();
            cmd = null;
            m_ObjCon.Close();
            m_ObjCon = null;
        }
    }
}
