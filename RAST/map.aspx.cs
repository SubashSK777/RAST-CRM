/*********************************************************************************************************************
Module Name: Map Page
Module Description: This page is responsible for displaying the Singapore Map along with the list of sites configured
**********************************************************************************************************************
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;

public partial class map : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
    }
}