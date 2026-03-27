/*********************************************************************************************************************
Module Name: Logout 
Module Description: This page is responsible for clearing all the session information and logging off the user
**********************************************************************************************************************
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class logoff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["i_UloginId"] = null;
        Session["email"] = null;
        Session["i_RoleId"] = null;
        Session.RemoveAll();
        Session.Abandon();

        Response.Redirect("default.aspx");
        
    }
}