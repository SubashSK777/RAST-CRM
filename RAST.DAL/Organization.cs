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
    public class Organization : IDALObject
    {
        private int m_OrganizationId;
        private string m_OrganizationName;
        private string m_ContactPerson;
        private string m_Email;
        private string m_Phone;
        private string m_Address;
        private int m_AdminstratorId;
        private string m_Logo;
        private int m_Status;
        private int m_ReturnValue;
        private int m_Edit;
        private string m_OrgCode;


        private MySqlConnection m_ObjCon;
        
        
        public MySqlConnection ObjCon
        {
            get { return m_ObjCon; }
            set { m_ObjCon = value; }
        }

        public int OrganizationId
        {
            get { return m_OrganizationId; }
            set { m_OrganizationId = value; }
        }

        public string OrgCode
        {
            get { return m_OrgCode; }
            set { m_OrgCode = value; }
        }

        public string OrganizationName
        {
            get { return m_OrganizationName; }
            set { m_OrganizationName = value; }
        }

        public string ContactPerson
        {
            get { return m_ContactPerson; }
            set { m_ContactPerson = value; }
        }


        public string Email
        {
            get { return m_Email; }
            set { m_Email = value; }
        }

        public string Phone
        {
            get { return m_Phone; }
            set { m_Phone = value; }
        }

        public string Address
        {
            get { return m_Address; }
            set { m_Address = value; }
        }

        public int AdminstratorId
        {
            get { return m_AdminstratorId; }
            set { m_AdminstratorId = value; }
        }

        public string Logo
        {
            get { return m_Logo; }
            set { m_Logo = value; }
        }

        
        public int Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        public int ReturnValue
        {
            get { return m_ReturnValue; }
            set { m_ReturnValue = value; }
        }


        public int Edit
        {
            get { return m_Edit; }
            set { m_Edit = value; }
        }


        public DataSet ReadElementsForUI(int intId)
        {
            DataSet dsData = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = m_ObjCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetDataForOrganizationsUI";
                cmd.Parameters.AddWithValue("m_OrganizationId", m_OrganizationId);
                cmd.Parameters.AddWithValue("m_Edit", m_Edit);
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

        public DataSet ReadDataset(int intUserId,int intCurrentPage, int intPageSize)
        {
            DataSet dsData = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = m_ObjCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetOrganizations";
                cmd.Parameters.AddWithValue("m_UserId", intUserId);
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
            cmd.CommandText = "p_AddOrganization";
            cmd.Parameters.AddWithValue("m_OrganizationName", m_OrganizationName);
            cmd.Parameters.AddWithValue("m_ContactPerson", m_ContactPerson);
            cmd.Parameters.AddWithValue("m_Email", m_Email);
            cmd.Parameters.AddWithValue("m_Phone", m_Phone);
            cmd.Parameters.AddWithValue("m_Logo", m_Logo);
            cmd.Parameters.AddWithValue("m_Address", m_Address);
            cmd.Parameters.AddWithValue("m_Status", m_Status);
            cmd.Parameters.AddWithValue("m_OrgCode", m_OrgCode);

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



            return 0;
        }
        public int Update() {

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_UpdateOrganization";
            cmd.Parameters.AddWithValue("m_OrganizationId", m_OrganizationId);
            cmd.Parameters.AddWithValue("m_OrganizationName", m_OrganizationName);
            cmd.Parameters.AddWithValue("m_ContactPerson", m_ContactPerson);
            cmd.Parameters.AddWithValue("m_Email", m_Email);
            cmd.Parameters.AddWithValue("m_Phone", m_Phone);
            cmd.Parameters.AddWithValue("m_Logo", m_Logo);
            cmd.Parameters.AddWithValue("m_Address", m_Address);
            cmd.Parameters.AddWithValue("m_Status", m_Status);
            cmd.Parameters.AddWithValue("m_OrgCode", m_OrgCode);

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



            return 0;
        
        }

        public DataSet ValidateOrganizationSite()
        {
            DataSet ds = new DataSet();
            try
            {

                MySqlCommand cmd = new MySqlCommand("p_CheckOrganizationSite", m_ObjCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("m_OrganizationId", m_OrganizationId);
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

        public DataSet ValidateOrganizationUser()
        {
            DataSet ds = new DataSet();
            try
            {

                MySqlCommand cmd = new MySqlCommand("p_CheckOrganizationUser", m_ObjCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("m_OrganizationId", m_OrganizationId);
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

       
        public int Delete() {
            try
            {
                MySqlCommand cmdDel = new MySqlCommand();
                cmdDel.Connection = m_ObjCon;
                cmdDel.CommandType = System.Data.CommandType.StoredProcedure;
                cmdDel.CommandText = "p_DeleteOrganization";
                cmdDel.Parameters.AddWithValue("m_OrganizationId", m_OrganizationId);
                cmdDel.ExecuteNonQuery();

                cmdDel = null;
                m_ObjCon.Close();
                m_ObjCon = null;
            }
            catch (Exception ex)
            {

            }

            return 1;
            
        }

        public int Enable() { return 0; }
        public int Disable() { return 0; }
        
    }
}
