using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

public partial class masters_MasterPage : System.Web.UI.MasterPage
{
    String strConnection = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        spVersion.InnerHtml = "Version " +Convert.ToString(ConfigurationManager.AppSettings["SWVersion"]) + " - " ;
        int intRoleId = 0;
        string loginEmail = Convert.ToString(Session["s_Email"]);
       // spUserName.InnerText = Convert.ToString(Session["name"]);
         // Get Current Page Name
         string sCurrentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

         // Allow public access to forgot/reset password pages
         bool isPublic = sCurrentPage.Equals("forgot_password.aspx", StringComparison.OrdinalIgnoreCase) || 
                         sCurrentPage.Equals("reset_password.aspx", StringComparison.OrdinalIgnoreCase);

         if (Session["i_UloginId"] == null && !isPublic)
         {
            Response.Redirect("Default.aspx");
         }
         String roleName = "";
        
         int intUserId = Convert.ToInt16(Session["i_UloginId"]);
         using (MySqlConnection conn = new MySqlConnection(strConnection))
         {
             conn.Open();
             MySqlCommand cmd = new MySqlCommand("p_GetUserRole", conn);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("m_UserId", intUserId);
             MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
             DataSet ds = new DataSet();
             sda.Fill(ds);
             if (ds.Tables[0].Rows.Count > 0)
             {
                 roleName = ds.Tables[0].Rows[0]["s_Role"].ToString();
                 intRoleId = Convert.ToInt32(ds.Tables[0].Rows[0]["i_RoleId"]);
             }
             String display = roleName + " | " + Convert.ToString(Session["s_Email"]);
             spUserName.InnerText = display;
         } 

/*
1	SuperAdmin
2	Administrator
3	Operator
4	Deployment Technician
5	Alert Technician
6   Analyst
*/

         string strTopMenu = "";

        strTopMenu = strTopMenu + "<div class=\"wrapper\"><nav class=\"main-header navbar navbar-expand navbar-white navbar-light\"><ul class=\"navbar-nav\"><li class=\"nav-item\"><a class=\"nav-link\" data-widget=\"pushmenu\" href=\"#\" role=\"button\"><i class=\"fas fa-bars\"></i></a></li><li class=\"nav-item d-none d-sm-inline-block\"><a href=\"map.aspx\" class=\"nav-link\">Home</a></li><li class=\"nav-item d-none d-sm-inline-block\"><a href=\"https://www.pestech.com.sg/contact/\" class=\"nav-link\">Contact</a></li></ul>";  // heading Menu

        // Search Menu
        strTopMenu = strTopMenu + "<form class=\"form-inline ml-3\"><div class=\"input-group input-group-sm\"><input class=\"form-control form-control-navbar\" type=\"search\" placeholder=\"Search\" aria-label=\"Search\">";
        strTopMenu = strTopMenu + "<div class=\"input-group-append\"><button class=\"btn btn-navbar\" type=\"submit\"><i class=\"fas fa-search\"></i></button></div></div></form><ul class=\"navbar-nav ml-auto\"></ul></nav>";

        //Group Button
        //strTopMenu = strTopMenu + "<ul class=\"navbar-nav\"><li class=\"nav-item\"><nav class=\"main-header navbar navbar-expand navbar-white navbar-light\"><a class=\"nav-link\" data-widget=\"pushmenu\" href=\"#\" role=\"button\"><i class=\"fas fa-bars\"></i></a></li></ul></nav>";

        //strTopMenu = strTopMenu + "<div class=\"collapse navbar-collapse pull-left\" id=\"navbar-collapse\">";
        //strTopMenu = strTopMenu + "<ul class=\"nav navbar-nav\">" ;

        //RodentEye Logo
        strTopMenu = strTopMenu + "<aside class=\"main-sidebar sidebar-dark-primary elevation-4\"><a href=\"#\" class=\"brand-link\"><span class=\"brand-text font-weight-light\"><img src=\"/img/logo.png\">&#8482</a></span></a>";
        //Sidebar Without User Image
        strTopMenu = strTopMenu + "<div class=\"sidebar\" id=\"navsidebar\"> <div class=\"user-panel mt-3 pb-3 mb-3 d-flex\"><div class=\"image\"></div><div class=\"info\"><a href=\"#\" class=\"d-block\">" + loginEmail + "</a></div></div>";
        //Slidebar With User Image
        //strTopMenu = strTopMenu + "<div class=\"sidebar\"> <div class=\"user-panel mt-3 pb-3 mb-3 d-flex\"><div class=\"image\"><img src=\"dist/img/user2-160x160.jpg\" class=\"img-circle elevation-2\" alt=\"User Image\"></div><div class=\"info\"><a href=\"#\" class=\"d-block\">" + loginEmail + "</a></div></div>";

        //SidebarSearch Form
        strTopMenu = strTopMenu + "<div class=\"form-inline\"><div class=\"input-group\" data-widget=\"sidebar-search\"><input class=\"form-control form-control-sidebar\" type=\"search\" placeholder=\"Search\" aria-label=\"Search\"><div class=\"input-group-append\"><button class=\"btn btn-sidebar\"><i class=\"fas fa-search fa-fw\"></i></button></div></div></div>";


        // Get Current Page Name
        string currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
        string activeClass = "active";
        string menuOpenClass = "menu-open";

        strTopMenu = strTopMenu + "<nav class=\"mt-2\"><ul class=\"nav nav-pills nav-sidebar flex-column\" data-widget=\"treeview\" role=\"menu\" data-accordion=\"false\">";

        // Home Link
        string homeActive = currentPage.Equals("map.aspx", StringComparison.OrdinalIgnoreCase) ? activeClass : "";
        strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"map.aspx\" class=\"nav-link " + homeActive + "\"><i class=\"nav-icon fas fa-home\"></i><p>Home</p></a></li>";

        // Dashboard Link
        string dashActive = currentPage.Equals("dashboard.aspx", StringComparison.OrdinalIgnoreCase) ? activeClass : "";
        strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"dashboard.aspx\" class=\"nav-link " + dashActive + "\"><i class=\"nav-icon fas fa-tachometer-alt\"></i><p>Dashboard</p></a></li>";

        // ==========================================
        // SECTION: ADMINISTRATIONS
        // Visible for: Role 1, 2, 3, 4
        // ==========================================
        if (intRoleId == 1 || intRoleId == 2 || intRoleId == 3 || intRoleId == 4)
        {
            // Determine if any child is active
            bool isAdminActive = (currentPage == "organization_list.aspx") ||
                                 (currentPage == "users_list.aspx") ||
                                 (currentPage == "sites_list.aspx") ||
                                 (currentPage == "configuration.aspx") ||
                                 (currentPage == "SensorManagement.aspx") ||
                                 (currentPage == "sensor_config.aspx") ||
                                 (currentPage == "profile.aspx");

            string adminMenuOpen = isAdminActive ? menuOpenClass : "";
            string adminNavLinkActive = isAdminActive ? activeClass : "";

            strTopMenu = strTopMenu + "<li class=\"nav-item has-treeview " + adminMenuOpen + "\">";
            strTopMenu = strTopMenu + "<a href =\"#\" class=\"nav-link " + adminNavLinkActive + "\"><i class=\"nav-icon fas fa-edit\"></i><p>Administrations<i class=\"fas fa-angle-left right\"></i><span class=\"badge badge-info right\"></span></p></a>";
            strTopMenu = strTopMenu + "<ul class=\"nav nav-treeview\">";
            
            // Org Mgmt: Role 1, 2, 4 (Corrected: Admin added)
            if (intRoleId == 1 || intRoleId == 2 || intRoleId == 4)
            {
                string sActive = currentPage == "organization_list.aspx" ? activeClass : "";
                strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"organization_list.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>Organization Management</p></a></li>";
            }
            
            // User Mgmt: Role 1, 2
            if (intRoleId == 1 || intRoleId == 2)
            {
                string sActive = currentPage == "users_list.aspx" ? activeClass : "";
                strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"users_list.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>User Management</p></a></li>";
            }
            
            // Site Mgmt: Role 1, 2, 4 (Corrected: Admin added)
            if (intRoleId == 1 || intRoleId == 2 || intRoleId == 4)
            {
                string sActive = currentPage == "sites_list.aspx" ? activeClass : "";
                strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"sites_list.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>Site Management</p></a></li>";
            }

            // Configuration: Role 1, 2 (Corrected: Admin added)
            if (intRoleId == 1 || intRoleId == 2)
            {
                string sActive = currentPage == "configuration.aspx" ? activeClass : "";
                strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"configuration.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>Configuration</p></a></li>";
            }

            // Sensor Mgmt: Role 1, 2, 3, 4 (Corrected: Admin added)
            if (intRoleId == 1 || intRoleId == 2 || intRoleId == 3 || intRoleId == 4)
            {
                string sActive = currentPage == "SensorManagement.aspx" ? activeClass : "";
                strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"SensorManagement.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>Sensor Management</p></a></li>";
                
                sActive = currentPage == "sensor_config.aspx" ? activeClass : "";
                strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"sensor_config.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>Sensor Config</p></a></li>";
            }

            // Profile: Role 1, 2, 3 (Corrected: Admin added)
            if (intRoleId == 1 || intRoleId == 2 || intRoleId == 3)
            {
                string sActive = currentPage == "profile.aspx" ? activeClass : "";
                strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"profile.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>Profile</p></a></li>";
            }
            
            // Unlock User Mgmt: Role 1, 2 ONLY
            if (intRoleId == 1 || intRoleId == 2)
            {
                string sActive = currentPage == "unlock_users.aspx" ? activeClass : "";
                strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"unlock_users.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>User Unlock Management</p></a></li>";
            }

            strTopMenu = strTopMenu + "</ul></li>";
        }

        // ==========================================
        // SECTION: REPORTS
        // Visible for: All Roles (1-6)
        // ==========================================
        
        // Determine if any report page is active
        bool isReportActive = (currentPage == "OrganizationAlertCount.aspx") ||
                             (currentPage == "SiteAlertReport.aspx") ||
                             (currentPage == "SensorAlertReport.aspx") ||
                             (currentPage == "batteryStatusClient.aspx") ||
                             (currentPage == "sms_log.aspx") ||
                             (currentPage == "deployLog.aspx") ||
                             (currentPage == "alerts_log.aspx");

        string reportMenuOpen = isReportActive ? menuOpenClass : "";
        string reportNavLinkActive = isReportActive ? activeClass : "";

        strTopMenu = strTopMenu + "<li class=\"nav-item has-treeview "+ reportMenuOpen +"\">";
        strTopMenu = strTopMenu + "<a href =\"#\" class=\"nav-link "+ reportNavLinkActive +"\"><i class=\"nav-icon fas fa-chart-pie\"></i><p>Reports<i class=\"fas fa-angle-left right\"></i><span class=\"badge badge-info right\"></span></p></a>";
        strTopMenu = strTopMenu + " <ul class=\"nav nav-treeview\">";
        
        // Org Alert: Role 1, 2, 5, 6 (Corrected: Admin added)
        if(intRoleId == 1 || intRoleId == 2 || intRoleId == 5 || intRoleId == 6)
        {
            string sActive = currentPage == "OrganizationAlertCount.aspx" ? activeClass : "";
            strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"OrganizationAlertCount.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>Organization Alert Count Report</p></a></li>";
        }

        // Site Alert: Role 1, 2, 3, 5, 6
        if (intRoleId == 1 || intRoleId == 2 || intRoleId == 3 || intRoleId == 5 || intRoleId == 6)
        {
            string sActive = currentPage == "SiteAlertReport.aspx" ? activeClass : "";
            strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"SiteAlertReport.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>Site Alert Count Report</p></a></li>";
        }

        // Sensor Alert: Role 1, 2, 3, 6 (Corrected: Admin added)
        if (intRoleId == 1 || intRoleId == 2 || intRoleId == 3 || intRoleId == 6)
        {
            string sActive = currentPage == "SensorAlertReport.aspx" ? activeClass : "";
            strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"SensorAlertReport.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>Sensor Alert Report</p></a></li>";
        }

        // Battery Status Client: Role 1, 2, 3, 5, 6
        if (intRoleId == 1 || intRoleId == 2 || intRoleId == 3 || intRoleId == 5 || intRoleId == 6)
        {
            string sActive = currentPage == "batteryStatusClient.aspx" ? activeClass : "";
            strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"batteryStatusClient.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>Battery Status</p></a></li>";
        }

        // SMS Logs: Role 1, 2, 5 (Corrected: Admin added)
        if (intRoleId == 1 || intRoleId == 2 || intRoleId == 5)
        {
            string sActive = currentPage == "sms_log.aspx" ? activeClass : "";
            strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"sms_log.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>SMS Logs</p></a></li>";
        }

        // Deployment Logs: Role 1, 2, 3, 4, 6 (Corrected: Admin added)
        if (intRoleId == 1 || intRoleId == 2 || intRoleId == 3 || intRoleId == 4 || intRoleId == 6)
        {
            string sActive = currentPage == "deployLog.aspx" ? activeClass : "";
            strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"deployLog.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>Deployment Logs</p></a></li>";
        }

        // Alerts Log: Role 1, 2, 3, 5, 6 (Corrected: Admin added)
        if (intRoleId == 1 || intRoleId == 2 || intRoleId == 3 || intRoleId == 5 || intRoleId == 6)
        {
            string sActive = currentPage == "alerts_log.aspx" ? activeClass : "";
            strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"alerts_log.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>Alerts Log</p></a></li>";
        }

        strTopMenu = strTopMenu + "</ul></li>";

        // ==========================================
        // SECTION: UTILITIES
        // Visible for: Role 1, 2, 3, 4, 5 (Corrected: Admin added)
        // ==========================================
        if (intRoleId == 1 || intRoleId == 2 || intRoleId == 3 || intRoleId == 4 || intRoleId == 5)
        {
            // Determine if any utility page is active
            bool isUtilActive = (currentPage == "batteryStatus.aspx") ||
                                (currentPage == "TriggerStatus.aspx") ||
                                (currentPage == "sensorRePosition.aspx") ||
                                (currentPage == "triggerDataDeletion.aspx") ||
                                (currentPage == "alertSMS.aspx") ||
                                (currentPage == "noTriggerGateway.aspx") ||
                                (currentPage == "HubStatus.aspx");

            string utilMenuOpen = isUtilActive ? menuOpenClass : "";
            string utilNavLinkActive = isUtilActive ? activeClass : "";

            strTopMenu = strTopMenu + "<li class=\"nav-item has-treeview " + utilMenuOpen + "\">";
            strTopMenu = strTopMenu + "<a href =\"#\" class=\"nav-link " + utilNavLinkActive + "\"><i class=\"nav-icon far fa-plus-square\"></i><p>Utilities<i class=\"fas fa-angle-left right\"></i><span class=\"badge badge-info right\"></span></p></a>";
            strTopMenu = strTopMenu + " <ul class=\"nav nav-treeview\">";
            
            // Battery Status: Role 1, 2, 5 (Corrected: Admin added)
            if (intRoleId == 1 || intRoleId == 2 || intRoleId == 5)
            {
                string sActive = currentPage == "batteryStatus.aspx" ? activeClass : "";
                strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"batteryStatus.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>Battery Status</p></a></li>";
            }

            // Trigger Status: Role 1, 2, 3, 5 (Corrected: Admin added)
            if (intRoleId == 1 || intRoleId == 2 || intRoleId == 3 || intRoleId == 5)
            {
                string sActive = currentPage == "TriggerStatus.aspx" ? activeClass : "";
                strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"TriggerStatus.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>Trigger Status</p></a></li>";
            }

            // Sensor Re-Position: Role 1, 2, 4 (Corrected: Admin added)
            if (intRoleId == 1 || intRoleId == 2 || intRoleId == 4)
            {
                string sActive = currentPage == "sensorRePosition.aspx" ? activeClass : "";
                strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"sensorRePosition.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>Sensor Re-Position</p></a></li>";
            }

            // Trigger Data Deletion: Role 1, 2 (Corrected: Admin added)
            if (intRoleId == 1 || intRoleId == 2)
            {
                string sActive = currentPage == "triggerDataDeletion.aspx" ? activeClass : "";
                strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"triggerDataDeletion.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>Trigger Data Deletion</p></a></li>";
            }

            // Alert Status: Role 1, 2, 5 (Corrected: Admin added)
            if (intRoleId == 1 || intRoleId == 2 || intRoleId == 5)
            {
                string sActive = currentPage == "alertSMS.aspx" ? activeClass : "";
                strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"alertSMS.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>Alert Status</p></a></li>";
            }

            // No Response Gateway: Role 1, 2, 4 (Corrected: Admin added)
            if (intRoleId == 1 || intRoleId == 2 || intRoleId == 4)
            {
                string sActive = currentPage == "noTriggerGateway.aspx" ? activeClass : "";
                strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"noTriggerGateway.aspx\" class=\"nav-link " + sActive + "\"><i class=\"far fa-circle nav-icon\"></i><p>No Response Gateway Status</p></a></li>";
            }

            // Hub Trigger Count: Role 1, 2, 3, 4, 5 (Corrected: Admin added)
            // (All in this section)
            string sActiveHub = currentPage == "HubStatus.aspx" ? activeClass : "";
            strTopMenu = strTopMenu + "<li class=\"nav-item\"><a href=\"HubStatus.aspx\" class=\"nav-link " + sActiveHub + "\"><i class=\"far fa-circle nav-icon\"></i><p>HUB wise Trigger Count Status</p></a></li>";
            
            strTopMenu = strTopMenu + "</ul></li>";
        }

        strTopMenu = strTopMenu + "<li class=\"nav-item has-treeview\">";
        strTopMenu = strTopMenu + "<a href=\"#\" onclick=\"fnHelp()\" class=\"nav-link\"><i class=\"nav-icon fas fa-copy\"></i><p>Help<i class=\"fas fa-angle-left right\"></i><span class=\"badge badge-info right\">1</span></p></a>";
        strTopMenu = strTopMenu + "<ul class=\"nav nav-treeview\"><li class=\"nav-item\"><a href=\"logoff.aspx\" class=\"nav-link\"><i class=\"far fa-circle nav-icon\"></i><p>Logout</p></a></li></ul></li></ul></nav></div></aside>";

        spTopMenu.InnerHtml = strTopMenu;
    }
}
