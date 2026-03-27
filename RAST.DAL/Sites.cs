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
    
    public class Sites : IDALObject
    {
        private int m_SiteId;
        private int m_OrganizationId;
        private string m_SiteName;
        private string m_Latitude;
        private string m_Longitude;
        private string m_Address;
        private string m_BuildingFloorMap;
        private int m_NoSensors;
        private int m_DeploymentTechnicianId;
        private int m_AlertTechnicianId;
        private string[] m_HubId;
        private string[] m_HubPhoneNumber;
        private int m_Status;
        private int m_ReturnValue;
        private int m_Edit;
        private int m_HubIdDel;
        private string m_HubIdUpdate;
        private string m_PhoneNumberDel;
        private string m_Location;
        private string[] m_MobileProvider;
        private string[] m_paymentType;
        private string[] m_hubOnTime;
        private string[] m_hubOffTime;

        private string m_MobileProviderUpdate;
        private string m_paymentTypeUpdate;
        private string m_hubOnTimeUpdate;
        private string m_hubOffTimeUpdate;


        private MySqlConnection m_ObjCon;
        EncryptionHelper objpwd = new EncryptionHelper();
        public MySqlConnection ObjCon
        {
            get { return m_ObjCon; }
            set { m_ObjCon = value; }
        }


        public string MobileProviderUpdate
        {
            get { return m_MobileProviderUpdate; }
            set { m_MobileProviderUpdate = value; }
        }


        public string paymentTypeUpdate
        {
            get { return m_paymentTypeUpdate; }
            set { m_paymentTypeUpdate = value; }
        }


        public string hubOnTimeUpdate
        {
            get { return m_hubOnTimeUpdate; }
            set { m_hubOnTimeUpdate = value; }
        }

        public string hubOffTimeUpdate
        {
            get { return m_hubOffTimeUpdate; }
            set { m_hubOffTimeUpdate = value; }
        }

        public int HubIdDel
        {
            get { return m_HubIdDel; }
            set { m_HubIdDel = value; }
        }

        public string PhoneNumberDel
        {
            get { return m_PhoneNumberDel; }
            set { m_PhoneNumberDel = value; }
        }

        public string[] MobileProvider
        {
            get { return m_MobileProvider; }
            set { m_MobileProvider = value; }
        }

        public string[] paymentType
        {
            get { return m_paymentType; }
            set { m_paymentType = value; }
        }

        public string[] hubOnTime
        {
            get { return m_hubOnTime; }
            set { m_hubOnTime = value; }
        }

        public string[] hubOffTime
        {
            get { return m_hubOffTime; }
            set { m_hubOffTime = value; }
        }

        public string HubIdUpdate
        {
            get { return m_HubIdUpdate; }
            set { m_HubIdUpdate = value; }
        }
        public int SiteId
        {
            get { return m_SiteId; }
            set { m_SiteId = value; }
        }

        public int OrganizationId
        {
            get { return m_OrganizationId; }
            set { m_OrganizationId = value; }
        }
        public string SiteName
        {
            get { return m_SiteName; }
            set { m_SiteName = value; }
        }

        public string Latitude
        {
            get { return m_Latitude; }
            set { m_Latitude = value; }
        }

        public string Longitude
        {
            get { return m_Longitude; }
            set { m_Longitude = value; }
        }

        public string Address
        {
            get { return m_Address; }
            set { m_Address = value; }
        }

        public string BuildingFloorMap
        {
            get { return m_BuildingFloorMap; }
            set { m_BuildingFloorMap = value; }
        }

        public string Location
        {
            get { return m_Location; }
            set { m_Location = value; }
        }

        public int NoSensors
        {
            get { return m_NoSensors; }
            set { m_NoSensors = value; }
        }

        public int DeploymentTechnicianId
        {
            get { return m_DeploymentTechnicianId; }
            set { m_DeploymentTechnicianId = value; }
        }
        public int AlertTechnicianId
        {
            get { return m_AlertTechnicianId; }
            set { m_AlertTechnicianId = value; }
        }

        public string[] HubId
        {
            get { return m_HubId; }
            set { m_HubId = value; }
        }
        public string[] HubPhoneNumber
        {
            get { return m_HubPhoneNumber; }
            set { m_HubPhoneNumber = value; }
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
                cmd.CommandText = "p_GetDataForSiteUI";
                cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
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
                cmd.CommandText = "p_GetSites";
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


        public DataSet ReadCommandDataset()
        {
            DataSet dsData = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = m_ObjCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetCommand";
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


        public DataSet GetBuildingMapForStation_fromMap(String tmp_SiteId)
        {

            if (tmp_SiteId.Length < 5)
            {

            }
            else
            {
                tmp_SiteId = objpwd.DecryptData(tmp_SiteId);
            }

            DataSet dsData = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = m_ObjCon;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT i_SiteImageId,s_FloorMapImage, s_FloorMapImageName  from site_images where i_SiteId = " + tmp_SiteId;
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



        public DataSet GetHubDetailBySite(string tmp_SiteId)
        {
            if (tmp_SiteId.Length < 5)
            {
                
            }
            else
            {
                tmp_SiteId = objpwd.DecryptData(tmp_SiteId);
            }

            DataSet dsData = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = m_ObjCon;
                cmd.CommandType = System.Data.CommandType.Text;
                //cmd.CommandText = "SELECT i_HubId,s_HubId,s_HubPhoneNumber  from hub where i_SiteId = " + tmp_SiteId;  
                //Ragu 21 May 2018
                cmd.CommandText = "SELECT i_HubId,s_HubId,s_HubPhoneNumber,s_MobileProvider, s_paymentType,s_hubOnTime, s_hubOffTime  from hub where i_SiteId = " + tmp_SiteId;
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
            cmd.CommandText = "p_AddSite";
            cmd.Parameters.AddWithValue("m_OrganizationId", m_OrganizationId);
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            cmd.Parameters.AddWithValue("m_SiteName", m_SiteName);
            cmd.Parameters.AddWithValue("m_Latitude", m_Latitude);
            cmd.Parameters.AddWithValue("m_Longitude", m_Longitude);
            cmd.Parameters.AddWithValue("m_Address", m_Address);
            cmd.Parameters.AddWithValue("m_DeploymentTechnicianId", m_DeploymentTechnicianId);
            cmd.Parameters.AddWithValue("m_AlertTechnicianId", m_AlertTechnicianId);
            cmd.Parameters.AddWithValue("m_Status", m_Status);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    m_ReturnValue = reader.GetInt32(reader.GetOrdinal("intStatus"));
                }
            }
            cmd = null;

            

            for (int intCtr = 0; intCtr < m_HubId.Length - 1; intCtr++)
            {
                try
                {
                    MySqlCommand cmdHub = new MySqlCommand();
                    cmdHub.Connection = m_ObjCon;
                    cmdHub.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdHub.CommandText = "p_AddHub";
                    cmdHub.Parameters.AddWithValue("m_SiteId", m_SiteId);
                    cmdHub.Parameters.AddWithValue("m_HubId", Convert.ToString(m_HubId.GetValue(intCtr)));
                    cmdHub.Parameters.AddWithValue("m_HubPhoneNumber", Convert.ToString(m_HubPhoneNumber.GetValue(intCtr)));

                    //Ragu 21 May 2018
                    cmdHub.Parameters.AddWithValue("m_MobileProvider", Convert.ToString(m_MobileProvider.GetValue(intCtr)));
                    cmdHub.Parameters.AddWithValue("m_paymentType", Convert.ToString(m_paymentType.GetValue(intCtr)));
                    cmdHub.Parameters.AddWithValue("m_hubOnTime", Convert.ToString(m_hubOnTime.GetValue(intCtr)));
                    cmdHub.Parameters.AddWithValue("m_hubOffTime", Convert.ToString(m_hubOffTime.GetValue(intCtr)));

                    cmdHub.ExecuteNonQuery();
                    
                    cmdHub = null;
                }
                catch (Exception ex)
                {

                }
            }

            Array arrFloorMap = m_BuildingFloorMap.Split(',');
            for (int intCtr = 0; intCtr < arrFloorMap.Length - 1; intCtr++)
            {
                try
                {
                    MySqlCommand cmdFloorMap = new MySqlCommand();
                    cmdFloorMap.Connection = m_ObjCon;
                    cmdFloorMap.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdFloorMap.CommandText = "p_AddBuildingFloorMap";
                    cmdFloorMap.Parameters.AddWithValue("m_SiteId", m_SiteId);
                    cmdFloorMap.Parameters.AddWithValue("m_FloorMap", Convert.ToString(arrFloorMap.GetValue(intCtr)));
                    cmdFloorMap.ExecuteNonQuery();

                    cmdFloorMap = null;
                }
                catch (Exception ex)
                {

                }
            }



            m_ObjCon.Close();
            m_ObjCon = null;

            return m_ReturnValue;
        }
        public int Update() {

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = m_ObjCon;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "p_UpdateSite";
            cmd.Parameters.AddWithValue("m_OrganizationId", m_OrganizationId);
            cmd.Parameters.AddWithValue("m_SiteId", m_SiteId);
            cmd.Parameters.AddWithValue("m_SiteName", m_SiteName);
            cmd.Parameters.AddWithValue("m_Latitude", m_Latitude);
            cmd.Parameters.AddWithValue("m_Longitude", m_Longitude);
            cmd.Parameters.AddWithValue("m_Address", m_Address);
            cmd.Parameters.AddWithValue("m_DeploymentTechnicianId", m_DeploymentTechnicianId);
            cmd.Parameters.AddWithValue("m_AlertTechnicianId", m_AlertTechnicianId);
            cmd.Parameters.AddWithValue("m_Status", m_Status);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    m_ReturnValue = reader.GetInt32(reader.GetOrdinal("intStatus"));
                }
            }
            cmd = null;


            
            for (int intCtr = 1; intCtr < m_HubId.Length - 1; intCtr++)
            {
                try
                {
                    MySqlCommand cmdHub = new MySqlCommand();
                    cmdHub.Connection = m_ObjCon;
                    cmdHub.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdHub.CommandText = "p_UpdateHub";
                    cmdHub.Parameters.AddWithValue("m_SiteId", m_SiteId);
                    cmdHub.Parameters.AddWithValue("m_HubId", Convert.ToString(m_HubId.GetValue(intCtr)));
                    cmdHub.Parameters.AddWithValue("m_HubPhoneNumber", Convert.ToString(m_HubPhoneNumber.GetValue(intCtr)));

                    //Ragu 21 May 2018
                    cmdHub.Parameters.AddWithValue("m_MobileProvider", Convert.ToString(m_MobileProvider.GetValue(intCtr)));
                    cmdHub.Parameters.AddWithValue("m_paymentType", Convert.ToString(m_paymentType.GetValue(intCtr)));
                    cmdHub.Parameters.AddWithValue("m_hubOnTime", Convert.ToString(m_hubOnTime.GetValue(intCtr)));
                    cmdHub.Parameters.AddWithValue("m_hubOffTime", Convert.ToString(m_hubOffTime.GetValue(intCtr)));

                    cmdHub.ExecuteNonQuery();
                    cmdHub = null;
                }
                catch (Exception ex)
                {

                }
            }
            
            m_ObjCon.Close();
            m_ObjCon = null;

            return m_ReturnValue;
        }

        public int Delete() {

            try
            {
                MySqlCommand cmdHub = new MySqlCommand();
                cmdHub.Connection = m_ObjCon;
                cmdHub.CommandType = System.Data.CommandType.StoredProcedure;
                cmdHub.CommandText = "p_DeleteSiteDetails";
                cmdHub.Parameters.AddWithValue("m_SiteId", m_SiteId);
                cmdHub.ExecuteNonQuery();

                cmdHub = null;
            }
            catch (Exception ex)
            {

            }

            return 1;
        }
        public int Enable() { return 0; }
        public int Disable() { return 0; }
        
        public int DeleteHub()
        {
            try
            {
                MySqlCommand cmdHub = new MySqlCommand();
                cmdHub.Connection = m_ObjCon;
                cmdHub.CommandType = System.Data.CommandType.StoredProcedure;
                cmdHub.CommandText = "p_DeleteHub";
                cmdHub.Parameters.AddWithValue("m_HubId", m_HubIdDel);
                cmdHub.ExecuteNonQuery();

                cmdHub = null;
            }
            catch (Exception ex)
            {

            }

            return 1;
        }

        public int UpdateHub()
        {
            try
            {
                MySqlCommand cmdHub = new MySqlCommand();
                cmdHub.Connection = m_ObjCon;
                cmdHub.CommandType = System.Data.CommandType.StoredProcedure;
                cmdHub.CommandText = "p_UpdateHub_Individual";
                cmdHub.Parameters.AddWithValue("m_SiteId", m_SiteId);
                cmdHub.Parameters.AddWithValue("m_HubId", m_HubIdDel);
                cmdHub.Parameters.AddWithValue("m_HubIdUpdate", m_HubIdUpdate);
                cmdHub.Parameters.AddWithValue("m_HubPhoneNumber", m_PhoneNumberDel);

                //Ragu 21 May 2018
                cmdHub.Parameters.AddWithValue("m_MobileProvider", m_MobileProviderUpdate);
                cmdHub.Parameters.AddWithValue("m_paymentType", m_paymentTypeUpdate);
                cmdHub.Parameters.AddWithValue("m_hubOnTime", m_hubOnTimeUpdate);
                cmdHub.Parameters.AddWithValue("m_hubOffTime", m_hubOffTimeUpdate);

                cmdHub.ExecuteNonQuery();

                cmdHub = null;
            }
            catch (Exception ex)
            {

            }

            return 1;
        }

        public DataSet GetBuildingMapForStation()
        {
            DataSet dsData = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = m_ObjCon;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT i_SiteImageId,s_FloorMapImage, s_FloorMapImageName  from site_images where i_SiteId = " + m_SiteId.ToString();
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

        public void UpdateFloorMap()
        {
            try
            {
                MySqlCommand cmdLocation = new MySqlCommand();
                cmdLocation.Connection = m_ObjCon;
                cmdLocation.CommandType = System.Data.CommandType.Text;
                cmdLocation.CommandText = "update site_images set s_FloorMapImageName = '" + m_Location + "' WHERE i_SiteId = " + m_SiteId.ToString() + " AND i_SiteImageId = " + m_BuildingFloorMap;
                cmdLocation.ExecuteNonQuery();

                cmdLocation = null;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
