using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Http;

public partial class upload_logo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EncryptionHelper objpwd = new EncryptionHelper();
        int intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["id"]));
        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        int intUserId = Convert.ToInt16(Session["i_UloginId"]);

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
                    var filePath = HttpContext.Current.Server.MapPath("~/logo/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                }
            }
            //result = Request.CreateResponse(HttpStatusCode.Created);
        }
        else
        {
            //result = Request.CreateResponse(HttpStatusCode.BadRequest);
        }



    }
}