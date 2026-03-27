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
    public class Users : IDALObject
    {
        private int m_UserId;
        private string m_Name;
        private string m_Email;
        private string m_Password;
        private string m_Phone;
        private int m_OrganizationId;
        private int m_RoleId;
        private bool m_Status;
        private int m_ReturnValue;

        private MySqlConnection myconn = new MySqlConnection();

        public MySqlConnection conn
        {
            get { return myconn;}
            set { myconn = value; }
        }

        public int UserId
        {
            get { return m_UserId; }
            set { m_UserId = value; }
        }


        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public string Email
        {
            get { return m_Email; }
            set { m_Email = value; }
        }

        public string Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }

        public string Phone
        {
            get { return m_Phone; }
            set { m_Phone = value; }
        }

        public int OrganizationId
        {
            get { return m_OrganizationId; }
            set { m_OrganizationId = value; }
        }

        public int RoleId
        {
            get { return m_RoleId; }
            set { m_RoleId = value; }
        }

        
        public bool Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        public DataSet ReadElementsForUI(int selUserId)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand("p_GetUserUIData", myconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("m_UserId", UserId);
                cmd.Parameters.AddWithValue("m_SelUserId", selUserId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
                cmd = null;
                da = null;
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet ReadDataset(int intUserId,int intCurrentPage, int intPageSize)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand("p_GetUsers",myconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("m_UserId", intUserId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
           
                da.Fill(ds);
                cmd = null;
                da = null;
            }
            catch(Exception ex)
            {

            }
            return ds;
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
       
            cmd.Connection = myconn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_AddUser";
         
            cmd.Parameters.AddWithValue("m_Name",m_Name);
            cmd.Parameters.AddWithValue("m_Email", m_Email);
            cmd.Parameters.AddWithValue("m_Password",m_Password);
            cmd.Parameters.AddWithValue("m_PhoneNumber", m_Phone);
            cmd.Parameters.AddWithValue("m_OrganizationId", m_OrganizationId);
            cmd.Parameters.AddWithValue("m_RoleId", m_RoleId);
            cmd.Parameters.AddWithValue("m_Status", m_Status);
            
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    m_ReturnValue = reader.GetInt32(reader.GetOrdinal("intStatus"));
                }
            }
            cmd = null;

            myconn.Close();
            myconn = null;
            return m_ReturnValue;
        }
        public int Update()
        {

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = myconn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_UpdateUser";
            cmd.Parameters.AddWithValue("m_UserId", m_UserId);
            cmd.Parameters.AddWithValue("m_Name", m_Name);
            cmd.Parameters.AddWithValue("m_Email", m_Email);
            cmd.Parameters.AddWithValue("m_Password", m_Password);
            cmd.Parameters.AddWithValue("m_PhoneNumber", m_Phone);
            cmd.Parameters.AddWithValue("m_OrganizationId", m_OrganizationId);
            cmd.Parameters.AddWithValue("m_RoleId", m_RoleId);
            cmd.Parameters.AddWithValue("m_Status", m_Status);

            cmd.ExecuteNonQuery();
           
            cmd = null;

            myconn.Close();
            myconn = null;

            return m_ReturnValue;
        }

        public DataSet CheckSiteUSer()
        {
            DataSet ds = new DataSet();
            try
            {

                MySqlCommand cmd = new MySqlCommand("p_CheckSiteUser", myconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("m_UserId", m_UserId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
                cmd = null;
                da = null;
            }


            catch (Exception ex)
            {

            }

            return ds;

        }


        public int Delete()
        {
            int dbreturn = 0;
            try
            {
                MySqlCommand cmdDel = new MySqlCommand();
                cmdDel.Connection = myconn;
                cmdDel.CommandType = System.Data.CommandType.StoredProcedure;
                cmdDel.CommandText = "p_DeleteUser";
                cmdDel.Parameters.AddWithValue("m_UserId", m_UserId);
                dbreturn = cmdDel.ExecuteNonQuery();

                cmdDel = null;
                myconn.Close();
                myconn = null;
            }
            catch (Exception ex)
            {

            }

            return dbreturn;
        }

      
        public int Enable() { return 0; }
        public int Disable() { return 0; }
        
    }
}
