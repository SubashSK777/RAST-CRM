<%@ WebHandler Language="C#" Class="ghCaptcha" %>

using System;
using System.Web;
using System.IO;
using System.Web.SessionState;
using System.Drawing;
using System.Drawing.Imaging;

public class ghCaptcha : IHttpHandler, IReadOnlySessionState
{
    public void ProcessRequest(HttpContext context)
    {
        MemoryStream memStream = new MemoryStream();
        string phrase = Convert.ToString(context.Session["Captcha"]);

        //Generate an image from the text stored in session
        Bitmap CaptchaImg = new Bitmap(180, 60);
        Graphics Graphic = Graphics.FromImage(CaptchaImg);
        Graphic.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

        //Set height and width of captcha image
        Graphic.FillRectangle(new SolidBrush(Color.Blue), 0, 0, 180, 60);
        Graphic.DrawString(phrase, new Font("Calibri", 30), new SolidBrush(Color.White), 15, 15);
        CaptchaImg.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        byte[] imgBytes = memStream.GetBuffer();

        Graphic.Dispose();
        CaptchaImg.Dispose();
        memStream.Close();

        //write image
        context.Response.ContentType = "image/jpeg";
        context.Response.BinaryWrite(imgBytes);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
