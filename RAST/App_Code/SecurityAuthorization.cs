using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

/// <summary>
/// Central Security Authority for Role-Based Access Control (RBAC)
/// Phase-1: Infrastructure and Mapping only. No enforcement yet.
/// </summary>
public static class SecurityAuthorization
{
    // Role Definitions for clarity
    public const int ROLE_SUPERADMIN = 1;
    public const int ROLE_ADMIN = 2;
    public const int ROLE_OPERATOR = 3;
    public const int ROLE_DEPLOYMENT_TECH = 4;
    public const int ROLE_ALERT_TECH = 5;
    public const int ROLE_ANALYST = 6;

    // The Central Permission Dictionary
    // Key: Role ID
    // Value: HashSet of allowed lowercase page names
    private static readonly Dictionary<int, HashSet<string>> RoleAccessMap = new Dictionary<int, HashSet<string>>();

    static SecurityAuthorization()
    {
        InitializePermissions();
    }

    private static void InitializePermissions()
    {
        // ==========================================
        // ROLE 3: OPERATOR
        // ==========================================
        var operatorPages = new HashSet<string>
        {
            "dashboard.aspx",
            "map.aspx",
            "alerts_log.aspx",
            "sensormanagement.aspx",
            "sensor_config.aspx",
            "sensoralertreport.aspx",
            "sitealertreport.aspx",
            "batterystatusclient.aspx",
            "deploylog.aspx",
            "triggerstatus.aspx",
            "hubstatus.aspx",
            "profile.aspx"
        };
        RoleAccessMap[ROLE_OPERATOR] = operatorPages;

        // ==========================================
        // ROLE 4: DEPLOYMENT TECHNICIAN
        // ==========================================
        var deployTechPages = new HashSet<string>
        {
            "dashboard.aspx",
            "map.aspx",
            "deploylog.aspx",
            "sites_list.aspx",
            "sites_ui.aspx",
            "organization_list.aspx",
            "sensor_config.aspx",
            "sensorreposition.aspx",
            "sensormanagement.aspx",
            "notriggergateway.aspx",
            "hubstatus.aspx"
        };
        RoleAccessMap[ROLE_DEPLOYMENT_TECH] = deployTechPages;

        // ==========================================
        // ROLE 5: ALERT TECHNICIAN
        // ==========================================
        var alertTechPages = new HashSet<string>
        {
            "dashboard.aspx",
            "map.aspx",
            "alerts_log.aspx",
            "alertsms.aspx",
            "sms_log.aspx",
            "sitealertreport.aspx",
            "organizationalertcount.aspx",
            "batterystatus.aspx",
            "batterystatusclient.aspx",
            "triggerstatus.aspx",
            "hubstatus.aspx"
        };
        RoleAccessMap[ROLE_ALERT_TECH] = alertTechPages;

        // ==========================================
        // ROLE 6: ANALYST
        // ==========================================
        var analystPages = new HashSet<string>
        {
            "dashboard.aspx",
            "map.aspx",
            "sitealertreport.aspx",
            "organizationalertcount.aspx",
            "sensoralertreport.aspx",
            "batterystatusclient.aspx",
            "deploylog.aspx",
            "alerts_log.aspx"
        };
        RoleAccessMap[ROLE_ANALYST] = analystPages;
    }

    /// <summary>
    /// Checks if a specific role is allowed to access a backend page.
    /// Usage: SecurityAuthorization.IsRoleAllowed("users_list.aspx", 3);
    /// </summary>
    public static bool IsRoleAllowed(string pageName, int roleId)
    {
        // 1. Sanitize Inputs
        if (string.IsNullOrEmpty(pageName)) return false; // Safety
        
        // Normalize page name to lowercase for consistent matching
        string normalizedPage = pageName.ToLower().Trim();

        // 2. Universal Access (Super Admin & Admin)
        if (roleId == ROLE_SUPERADMIN || roleId == ROLE_ADMIN)
        {
            return true; // They can see everything
        }

        // 3. Check Dictionary
        if (RoleAccessMap.ContainsKey(roleId))
        {
            // O(1) Lookup
            if (RoleAccessMap[roleId].Contains(normalizedPage))
            {
                return true;
            }
        }

        // 4. Default Deny
        // If role not found, or page not in list, DENY.
        return false;
    }

    /// <summary>
    /// Safely extracts the clean page name from the HttpContext.
    /// Handles query strings, paths, and casing.
    /// Example: "/help/Users_List.aspx?id=5" -> "users_list.aspx"
    /// </summary>
    public static string GetRequestedPageName(HttpContext context)
    {
        if (context == null || context.Request == null) return string.Empty;

        // Get the absolute path (e.g., /RAST/users_list.aspx)
        string absolutePath = context.Request.Url.AbsolutePath;

        // Extract filename only (users_list.aspx)
        string fileName = Path.GetFileName(absolutePath);

        // Normalize
        return fileName.ToLower();
    }
}
