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
    public class AlertLog : IDALObject
    {
        private MySqlConnection m_ObjCon;
        private DateTime m_dtStartDate;
        private DateTime m_dtEndDate;

        public MySqlConnection ObjCon
        {
            get { return m_ObjCon; }
            set { m_ObjCon = value; }
        }


        public DateTime StartDate
        {
            get { return m_dtStartDate; }
            set { m_dtStartDate = value; }
        }

        public DateTime EndDate
        {
            get { return m_dtEndDate; }
            set { m_dtEndDate = value; }
        }

        public DataSet ReadDataset(int intUserId, int intCurrentPage, int intPageSize)
        {
            DataSet dsData = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = m_ObjCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetAlertLog";
                cmd.Parameters.AddWithValue("m_dtStartDate", m_dtStartDate);
                cmd.Parameters.AddWithValue("m_dtEndDate", m_dtEndDate);

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

        public DataSet ReadElementsForUI(int intId)
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

            return 0;
        }
        public int Update()
        {
            return 0;
        }
        public int Delete() { return 0; }
        public int Enable() { return 0; }
        public int Disable() { return 0; }
    }
}
