using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Web.SessionState;
using System.Data;

namespace RAST.DAL
{
    public class Authentication
    {
        private string m_EmailId;
        private string m_Password;
        private int m_ReturnValue;
        private string m_Name;
        private MySqlConnection m_ObjCon;

        public string EmailId
        {
            get { return m_EmailId; }
            set { m_EmailId = value; }
        }
        public string Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }
        public MySqlConnection ObjCon
        {
            get { return m_ObjCon; }
            set { m_ObjCon = value; }
        }

        public int ReturnValue
        {
            get { return m_ReturnValue; }
            set { m_ReturnValue = value; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        
        public void fnAuthenticateUser()
        {
            try
            {
                m_ReturnValue = 1;
                //MySqlCommand cmd = new MySqlCommand();
                //cmd.Connection = m_ObjCon;
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.CommandText = "p_AuthenticateUser";
                //cmd.Parameters.AddWithValue("m_EmailId", m_EmailId);
                DataSet ds = new DataSet();
                //MySqlDataAdapter sda = new MySqlDataAdapter();
                //sda.Fill(ds);
               
                //using (var reader = cmd.ExecuteReader())
                //{
                //    if (reader.Read())
                //    {
                //        m_ReturnValue = reader.GetInt32(reader.GetOrdinal("intStatus"));
                //        m_Name = reader.GetString(reader.GetOrdinal("s_Name"));
                //    }
                //}

                //cmd = null;
               // return ds;
            }
            catch(MySqlException ex)
            {
                
            }


            /*
            m_ReturnValue = 0; // Valid User
            m_ReturnValue = 1; // Invalid Email Id
            m_ReturnValue = 2; // Invalid Password
            m_ReturnValue = 3; // User already logged in
             * **/

            m_ObjCon.Close();
            m_ObjCon = null;
        }

    }
}
