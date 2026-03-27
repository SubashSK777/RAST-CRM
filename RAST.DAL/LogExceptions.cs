using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace RAST.DAL
{
    public class LogExceptions
    {
        private MySqlConnection myconn = new MySqlConnection();

        public MySqlConnection conn
        {
            get { return myconn; }
            set { myconn = value; }
        }

        public void SaveLogExceptions(Exception ex, string strexepurl)
        {
          
            MySqlCommand com = new MySqlCommand("p_InsertLogExceptions", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@m_ExceptionMsg", ex.Message.ToString());
            com.Parameters.AddWithValue("@m_ExceptionType", ex.GetType().Name.ToString());
            com.Parameters.AddWithValue("@m_ExceptionUrl", strexepurl);
            com.Parameters.AddWithValue("@m_LogDate", DateTime.Now);
            com.ExecuteNonQuery();   
        }
    }
}
