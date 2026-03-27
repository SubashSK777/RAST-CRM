/*********************************************************************************************************************
Module Name: Floor Map Upload Module 
Module Description: This page is responsible for uploading the Floor Map from the client computers to the server 
**********************************************************************************************************************
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class floor_map : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        int intUserId = Convert.ToInt16(Session["i_UloginId"]);
        if (Request.QueryString["id"] != null)
        {
            if (Request.QueryString["id"].ToString().Trim().Length > 0)
            {
                string strSiteId = Convert.ToString(Request.QueryString["id"]);

                DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/plans/"));
                FileInfo[] files = dirInfo.GetFiles("Site_" + strSiteId + ".*");

                if (files.Length > 0)
                {
                    foreach(FileInfo fl in files)
                    {
                        imgFloorPlan.Src = "/plans/" + fl.Name.ToString();
                        imgFloorPlan.Visible = true;
                    }
                }
                else
                {
                    imgFloorPlan.Src = "/plans/noimage.png";
                    imgFloorPlan.Visible = true;
                }
               

                

            }
        }


    }



    protected void btnUploadClick(object sender, EventArgs e)
    {

        if (Request.Files["flFloorPlan"] != null)
        {

            string strSaveFileName = "";
            HttpPostedFile MyFile = Request.Files["flFloorPlan"];

            string[] validFileTypes = {"gif", "png", "jpg", "jpeg" };
            string ext = System.IO.Path.GetExtension(MyFile.FileName);

            strSaveFileName = "Site_" + Convert.ToString(Request.QueryString["id"]) + ext;

            bool isValidFile = false;

            for (int i = 0; i < validFileTypes.Length; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break;
                }
            }

            if (isValidFile)
            {
                //Setting location to upload files
                string TargetLocation = Server.MapPath("~/plans/");
                try
                {
                    if (MyFile.ContentLength > 0)
                    {

                        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/plans/"));
                        FileInfo[] files = dirInfo.GetFiles("Site_" + Convert.ToString(Request.QueryString["id"]) + ".*");

                        if (files.Length > 0)
                        {
                            foreach (FileInfo fl in files)
                            {
                                fl.Delete();
                            }
                        }


                        //Determining file name. You can format it as you wish.
                        string FileName = MyFile.FileName;
                        //Determining file size.
                        int FileSize = MyFile.ContentLength;
                        //Creating a byte array corresponding to file size.
                        byte[] FileByteArray = new byte[FileSize];
                        //Posted file is being pushed into byte array.
                        MyFile.InputStream.Read(FileByteArray, 0, FileSize);
                        //Uploading properly formatted file to server.
                        MyFile.SaveAs(TargetLocation + strSaveFileName);




                        imgFloorPlan.Visible = true;
                        spMsg.InnerHtml = "<font color='blue'><strong>File has been uploaded successfully</strong></font>";
                        imgFloorPlan.Src = "/plans/" + strSaveFileName;
                    }
                    else
                    {
                        spMsg.InnerHtml = "<font color='red'><strong>Select a valid file for upload</strong></font>";
                    }
                }
                catch (Exception Exp)
                {
                    spMsg.InnerHtml = "<font color='red'><strong>Select a valid file for upload</strong></font>";
                }
            }
            else
            {
                spMsg.InnerHtml = "<font color='red'><strong>Only gif, jpg, jpeg and png files can be uploaded</strong></font>";
            }
        }
        else
        {
            spMsg.InnerHtml = "<font color='red'><strong>Select a valid file for upload</strong></font>";
        }

    }
}
