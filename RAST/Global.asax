<%@ Application Language="C#" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.SessionState" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="MySql.Data.MySqlClient" %>
<%@ Import Namespace="System.IO" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
    }

    void Application_AcquireRequestState(object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;

        // 1. Basic Safety Checks: Ensure Context and Session are available
        if (context == null || context.Session == null) return;

        // =========================================================================
        // SECURITY LAYER 1: AUTHENTICATION CHECK (User Logged In)
        // =========================================================================
        // Check if the User is actually logged in (Phase-1 keys must exist)
        if (context.Session["i_UloginId"] == null || context.Session["SessionKey"] == null) return;

        // 3. Exclusions: Do not run DB check for these paths
        string path = context.Request.Url.AbsolutePath.ToLower();
        
        // Explicit Page Exclusions (Skip Auth Check entirely for these - let native ASP.NET or other layers handle)
        if (path.EndsWith("/default.aspx") || 
            path.Contains("/services/") || 
            path.Contains("/help/") ||
            path.EndsWith("/logoff.aspx") ||
            path.EndsWith("/ghcaptcha.ashx")) 
        {
            return;
        }

        // Static File Exclusions (Extensions)
        string ext = Path.GetExtension(path);
        if (ext == ".css" || ext == ".js" || ext == ".png" || ext == ".jpg" || 
            ext == ".jpeg" || ext == ".gif" || ext == ".ico" || ext == ".woff" || 
            ext == ".ttf" || ext == ".svg" || ext == ".map")
        {
            return;
        }

        // =========================================================================
        // SECURITY LAYER 2: SINGLE SESSION ENFORCEMENT
        // =========================================================================
        try
        {
            string currentSessionId = context.Session["SessionKey"].ToString();
            string userId = context.Session["i_UloginId"].ToString();
            string dbSessionId = "";

            string connStr = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT SessionId FROM user_sessions WHERE UserId = @uid", conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        dbSessionId = result.ToString();
                    }
                }
            }

            // If DB has a session ID, and it does not match our current one, we are stale.
            if (string.IsNullOrEmpty(dbSessionId) || currentSessionId != dbSessionId)
            {
                // Force Logout Logic
                context.Session.RemoveAll();
                context.Session.Abandon();
                
                // Silent Redirect
                context.Response.Redirect("~/default.aspx", true);
            }
        }
        catch (System.Threading.ThreadAbortException)
        {
             // Redirect happened.
        }
        catch (Exception)
        {
            // In case of DB error, fail safely mostly to allow subsequent logic to possibly run or user to continue
        }

        // =========================================================================
        // SECURITY LAYER 3: ROLE-BASED AUTHORIZATION ENFORCEMENT (PHASE-2)
        // =========================================================================
        try
        {
             // A. Get Page Name safely
             string pageName = SecurityAuthorization.GetRequestedPageName(context);

             // B. Whitelist Pages that are Safe/Public for any Authenticated User.
             // IMPORTANT: Must include Login/Logout to prevent infinite loops if logic fails elsewhere.
             if (pageName == "dashboard.aspx" || 
                 pageName == "map.aspx" || 
                 pageName == "profile.aspx" || 
                 pageName == "default.aspx" ||
                 pageName == "logoff.aspx" ||
                 string.IsNullOrEmpty(pageName))
             {
                 return; // Allow access
             }

             // Double Check for services/help exclusions in case extraction failed or path was weird
             if (path.Contains("/services/") || path.Contains("/help/"))
             {
                 return;
             }

             // C. Get User Role
             int roleId = 0;
             if (context.Session["i_RoleId"] != null)
             {
                 int.TryParse(context.Session["i_RoleId"].ToString(), out roleId);
             }

             // D. Check Permission
             bool isAllowed = SecurityAuthorization.IsRoleAllowed(pageName, roleId);

             // E. Enforce
             if (!isAllowed)
             {
                 // REDIRECT to Dashboard (Safe Landing)
                 context.Response.Redirect("~/dashboard.aspx", true);
             }
        }
        catch (System.Threading.ThreadAbortException)
        {
            // Expected during Redirect. Do nothing.
        }
        catch (Exception)
        {
            // Log error if needed, but fail safe.
        }
    }

</script>
