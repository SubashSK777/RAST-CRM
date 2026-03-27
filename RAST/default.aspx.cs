/*********************************************************************************************************************
Module Name: Default Page
Module Description: This page is loaded by default with the option to login for the user
**********************************************************************************************************************
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.IO;
using System.Web.Services;
using Newtonsoft.Json;

public partial class _default : System.Web.UI.Page
{
    EncryptionHelper objpwd = new EncryptionHelper();

    protected static string siteKey = "6LcoULUoAAAAAK88CLyuBWNZkJ-ta_CVZ3qYbetx";
    protected static string secretKey = "6LcoULUoAAAAAEccdLE0Ve_BfeCJ9D6XjTj_LPsG";
    

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            UpdateCaptchaText();
        }
        
        
    }
    #region " [ Button Event ] "
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        UpdateCaptchaText();
    }

    
  
    
    
    
    #endregion
    
    #region " [ Private Function ] "
    private void UpdateCaptchaText()
    {
        //txtCode.Text = string.Empty;
        
        Random randNum = new Random();

        //Store the captcha text in session to validate
        Session["Captcha"] = randNum.Next(10000, 99999).ToString();
        imgCaptcha.ImageUrl = "~/ghCaptcha.ashx?" + objpwd.EncryptData(Session["Captcha"].ToString()); 
        hidCaptcha.Value = objpwd.EncryptData(Session["Captcha"].ToString());
    }
    #endregion


    public bool VerifyCaptcha(string response)
    {
        string apiUrl = "https://www.google.com/recaptcha/api/siteverify";
        //string data = $"secret={secretKey}&response={response}";
        string data = "secret=" + secretKey + "&response=" + response;

        using (var client = new WebClient())
        {
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            string responseString = client.UploadString(apiUrl, data);

            // Parse the JSON response
            dynamic jsonData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);
            
            return jsonData.success;
        }
    }
}