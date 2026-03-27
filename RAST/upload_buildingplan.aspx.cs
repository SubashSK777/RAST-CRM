using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Http;
using System.IO;

public partial class upload_buildingplan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        int intUserId = Convert.ToInt16(Session["i_UloginId"]);

        int intSiteId = Convert.ToInt32(Request.QueryString["id"]);

        HttpResponseMessage result = null;
        var httpRequest = HttpContext.Current.Request;
        if (httpRequest.Files.Count > 0)
        {
            foreach (string file in httpRequest.Files)
            {
                var postedFile = httpRequest.Files[file];

                string extension = System.IO.Path.GetExtension(postedFile.FileName);

                if ((string.Equals(extension, ".jpg", StringComparison.OrdinalIgnoreCase)) || (string.Equals(extension, ".jpeg", StringComparison.OrdinalIgnoreCase)) || (string.Equals(extension, ".png", StringComparison.OrdinalIgnoreCase)) || (string.Equals(extension, ".gif", StringComparison.OrdinalIgnoreCase)))
                {
                    string strFileName = Guid.NewGuid().ToString() + "_" + postedFile.FileName;
                    var filePath = HttpContext.Current.Server.MapPath("plans\\" + strFileName);
                    postedFile.SaveAs(filePath);

                    Session["s_FloorMaps"] = strFileName + "," + Session["s_FloorMaps"].ToString();
                    //Response.Write(@"1");
                    
                }
                else
                {
                    //result = Request.CreateResponse(HttpStatusCode.BadRequest);
                    //Page page = HttpContext.Current.Handler as Page;
                    //ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('System Accept JPG / JPEG / PNG format of file only');", true);
                    //Response.Write("<script>alert('System Accept JPG / JPEG / PNG format of file only!')</script>");
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "System Accept JPG/JPEG/PNG format of file only", true);
                    //Response.Write(@"0");
                    //Response.Write(@"0");

                }
         
            }
        }
        else
        {
            //result = Request.CreateResponse(HttpStatusCode.BadRequest);
            //Response.Write(@"0");
        }



    }
}