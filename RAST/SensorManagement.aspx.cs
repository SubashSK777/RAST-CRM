using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using RAST.DAL;
using System.Data;
using System.IO;
public partial class sites_ui : System.Web.UI.Page
{
    public int lastSensorNumber = 0;
    EncryptionHelper objpwd = new EncryptionHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        int intSiteId = 0;
        int intOrganizationId = 0;

        if (Session["i_UloginId"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        if (Request.QueryString["id"] != null)
        {
            if (Request.QueryString["id"].ToString().Trim().Length > 0)
            {
                try
                {
                    if(Request.QueryString["id"].ToString().Equals("0"))
                    {
                        intSiteId = 0;
                    }
                    else
                    {
                        

                        if (Request.QueryString["id"].Length <= 5)
                        {
                            intSiteId = Convert.ToInt32(Request.QueryString["id"].ToString().Trim());
                        }
                        else
                        {
                            intSiteId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["id"].ToString().Trim()));
                        }

                    }

                    if (Request.QueryString["organizationid"].Length <= 5)
                    {
                        intOrganizationId = Convert.ToInt32(Request.QueryString["organizationid"].ToString().Trim());
                    }
                    else
                    {
                        intOrganizationId = Convert.ToInt32(objpwd.DecryptData(Request.QueryString["organizationid"].ToString().Trim()));
                       

                    }

                    hidOrgId.Value = intOrganizationId.ToString();

                    int intFloorMapId = 0;
                    using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
                    {
                        conn.Open();
                        Sites objSite = new Sites();
                        objSite.ObjCon = conn;
                        objSite.SiteId = intSiteId;

                        objSite.OrganizationId = intOrganizationId; // Ragu 13 Sep 2019


                        DataSet dsFloorMapImage = objSite.GetBuildingMapForStation();


                        if (dsFloorMapImage.Tables[0].Rows.Count > 0)
                        {
                            List<ListItem> itemsDT = new List<ListItem>();

                            for (int intRowCtr = 0; intRowCtr < dsFloorMapImage.Tables[0].Rows.Count; intRowCtr++)
                            {
                                itemsDT.Add(new ListItem(dsFloorMapImage.Tables[0].Rows[intRowCtr][1].ToString(), dsFloorMapImage.Tables[0].Rows[intRowCtr][0].ToString()));
                            }

                            cmbFloorMapImage.Items.AddRange(itemsDT.ToArray());
                            //intFloorMapId = Convert.ToInt32(objpwd.DecryptData(dsFloorMapImage.Tables[0].Rows[0][0].ToString()));
                            intFloorMapId = Convert.ToInt32(dsFloorMapImage.Tables[0].Rows[0][0].ToString());
                            txtFloorMapImageName.Value = Convert.ToString(dsFloorMapImage.Tables[0].Rows[0][2].ToString());
                            imgBuildingPlan.Src = "/plans/" + dsFloorMapImage.Tables[0].Rows[0][1].ToString();
                            //+ "\" onclick=\"javascript: getMouseXY(event)\" />";

                        }
                        dsFloorMapImage = null;
                    }


                    
                    using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
                    {
                        conn.Open();
                        Sensors objSensors = new Sensors();
                        objSensors.ObjCon = conn;
                        objSensors.SiteId = intSiteId;

                        DataSet dsSensors = objSensors.fnGetSensorsForFloorMap(intFloorMapId,intSiteId);

                        if (dsSensors.Tables[0].Rows.Count > 0)
                        {
                            List<ListItem> itemsDT = new List<ListItem>();

                            for (int intRowCtr = 0; intRowCtr < dsSensors.Tables[0].Rows.Count; intRowCtr++)
                            {
                                itemsDT.Add(new ListItem(dsSensors.Tables[0].Rows[intRowCtr][1].ToString(), dsSensors.Tables[0].Rows[intRowCtr][0].ToString()));
                            }

                            cmbHubId.Items.AddRange(itemsDT.ToArray());
                        }


                        
                        for (int intRowCtr = 0; intRowCtr < dsSensors.Tables[1].Rows.Count; intRowCtr++)
                        {
                            Image img = new Image();
                            
                            img.ID = Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][0]);
                            string strImageUrl = "";
                            string strStatus = Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][7]);
                            if(strStatus.Trim() == "1")
                            {
                                strStatus = "Active";
                            }
                            else
                            {
                                strStatus = "Deactive";
                            }


                            //Ragu 30 Jul 2019
                           // int curSensorNumber = Convert.ToInt32(dsSensors.Tables[1].Rows[intRowCtr][2].ToString().Substring(0,2));  // Get First 2 digit
                            //if (lastSensorNumber< curSensorNumber)
                            //{
                             //   lastSensorNumber= curSensorNumber;
                            //}
                            //-------

                            //Ragu 30 Apr 2018
                            cmbSensorType.Text=Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][8]);
                            cmbHubId.Text = Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][1]);
                            //--------

                           
                            strImageUrl = "/openlayers/img/0.gif";
                            img.ToolTip = "Hub Id:" + Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][1]) + "\nSensor Id:" + Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][2]) + "\nMinimum Threshold Level:" + Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][3]) + "\nMaximum Threshold Level:" + Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][4]) + "\nStatus:" + strStatus;

                           
                            img.ImageUrl = strImageUrl;
                            img.Style["position"] = "absolute";
                            img.Style["left"] = Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][5]) + "px";
                            img.Style["top"] = Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][6]) + "px";
          
                            img.Attributes.Add("onclick", "fnGetSensorData(" + Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr]["i_SensorId"]) + ")");


                            
                          //  image_panel.Controls.Add(img);

                            /*
                            Label lblContent = new Label();
                            lblContent.Style["position"] = "absolute";
                            lblContent.Style["left"] = Convert.ToString((Convert.ToInt32(dsSensors.Tables[1].Rows[intRowCtr][5])) + 2) + "px";
                            lblContent.Style["top"] = Convert.ToString((Convert.ToInt32(dsSensors.Tables[1].Rows[intRowCtr][6])) + 2) + "px"; 
                            lblContent.Text = "20";
                            lblContent.Font.Bold = true;
                            lblContent.Font.Size = 8;
                            image_panel.Controls.Add(lblContent);
                            */


                        }
                        dsSensors = null;
                        objSensors = null;
                        conn.Close();

                    }
                }

                catch (Exception exp)
                {

                }

            }
        }


            /*
            int intSiteId = 0;

            if (Request.QueryString["id"] != null)
            {
                if (Request.QueryString["id"].ToString().Trim().Length > 0)
                {
                    try
                    {
                        intSiteId = Convert.ToInt32(Request.QueryString["id"].ToString().Trim());


                        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/plans/"));
                        FileInfo[] files = dirInfo.GetFiles("Site_" + intSiteId.ToString() + ".*");

                        if (files.Length > 0)
                        {
                            foreach (FileInfo fl in files)
                            {
                                buildingPlan.Src = "/plans/" + fl.Name.ToString();
                            }
                        }
                        else
                        {
                            buildingPlan.Src = "/plans/noimage.png";
                        }






                    }
                    catch (Exception exp)
                    {
                        intSiteId = 0;
                    }


                }
            }

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
            {
                conn.Open();
                Sensors objSensors = new Sensors();
                objSensors.ObjCon = conn;
                objSensors.SiteId = intSiteId;

                DataSet dsSensors = objSensors.ReadElementsForUI(intSiteId);


                if (dsSensors.Tables[0].Rows.Count > 0)
                {
                    List<ListItem> itemsDT = new List<ListItem>();

                    for (int intRowCtr = 0; intRowCtr < dsSensors.Tables[0].Rows.Count; intRowCtr++)
                    {
                        itemsDT.Add(new ListItem(dsSensors.Tables[0].Rows[intRowCtr][1].ToString(), dsSensors.Tables[0].Rows[intRowCtr][0].ToString()));
                    }

                    cmbHubId.Items.AddRange(itemsDT.ToArray());
                }


                //load the sensor information





                for (int intRowCtr = 0; intRowCtr < dsSensors.Tables[1].Rows.Count; intRowCtr++)
                {
                    HtmlTableRow tblRow = new HtmlTableRow();
                    HtmlTableCell tbclSlNo = new HtmlTableCell();
                    HtmlTableCell tbclHubId = new HtmlTableCell();
                    HtmlTableCell tbclSensorId = new HtmlTableCell();
                    HtmlTableCell tbclSensorType = new HtmlTableCell();
                    HtmlTableCell tbclUnit = new HtmlTableCell();
                    HtmlTableCell tbclMinThresholdId = new HtmlTableCell();
                    HtmlTableCell tbclMaxThresholdId = new HtmlTableCell();
                     HtmlTableCell tbclStatus = new HtmlTableCell();

                    HtmlTableCell tbclDelete = new HtmlTableCell();

                    Label lblSlNo = new Label();
                    Label lblHubId = new Label();
                    Label lblSensorId = new Label();
                    Label lblSensorType = new Label();
                    Label lblUnit = new Label();
                    Label lblMinThresholdId = new Label();
                    Label lblMaxThresholdId = new Label();
                     Label lblStatus = new Label();
                    Label lblDelete = new Label();

                    lblSlNo.Text = Convert.ToString(intRowCtr + 1);
                    lblHubId.Text = Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][1]);

                    lblSensorId.Text = Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][2]);


                    lblMinThresholdId.Text = Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][3]);
                    lblMaxThresholdId.Text = Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][4]);
                    lblSensorType.Text = Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][8]);
                    lblUnit.Text = Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][9]);
                   lblStatus.Text = Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][7]);
                    lblDelete.Text = "<i  style='cursor:pointer' class='fa fa-edit' onclick='fnEditRowSensors(" + Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][0]) + ")'></i>&nbsp;<i  style='cursor:pointer' class='fa fa-minus-square' onclick='fnDeleteRowSensors(" + Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][0]) + ")'></i>";

                    tbclSlNo.Controls.Add(lblSlNo);
                    tbclHubId.Controls.Add(lblHubId);
                    tbclSensorId.Controls.Add(lblSensorId);
                    tbclSensorType.Controls.Add(lblSensorType);

                    tbclMinThresholdId.Controls.Add(lblMinThresholdId);
                    tbclMaxThresholdId.Controls.Add(lblMaxThresholdId);
                    tbclUnit.Controls.Add(lblUnit);
                    tbclStatus.Controls.Add(lblStatus);
                    tbclDelete.Controls.Add(lblDelete);


                    tblRow.Cells.Add(tbclSlNo);
                    tblRow.Cells.Add(tbclHubId);
                    tblRow.Cells.Add(tbclSensorId);
                    tblRow.Cells.Add(tbclSensorType);

                    tblRow.Cells.Add(tbclMinThresholdId);
                    tblRow.Cells.Add(tbclMaxThresholdId);
                    tblRow.Cells.Add(tbclUnit);
                    tblRow.Cells.Add(tbclStatus);
                    tblRow.Cells.Add(tbclDelete);


                    tblSensors.Rows.Add(tblRow);

                    tblRow = null;
                    tbclSlNo = null;
                    tbclHubId = null;
                    tbclSensorId = null;
                    tbclSensorType = null;
                    tbclUnit = null;
                    tbclStatus = null;
                    tbclMinThresholdId = null;
                    tbclMaxThresholdId = null;

                    Image img = new Image();
                    img.ID = Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][0]);
                    string strImageUrl = "";

                    strImageUrl = "/openlayers/img/0.gif";
                    img.ToolTip = "Hub Id:" + Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][1]) + "\nSensor Id:" + Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][2]) + "\nSensorType:" + Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][8]) + "\nMinimum Threshold Level:" + Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][3]) + "\nMaximum Threshold Level:" + Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][4]) + "\nUnit:" + Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][9] + "\nStatus:" + Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][7]));


                    img.ImageUrl = strImageUrl;
                    img.Style["position"] = "absolute";
                    img.Style["left"] = Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][5]) + "px";
                    img.Style["top"] = Convert.ToString(dsSensors.Tables[1].Rows[intRowCtr][6]) + "px";



                    image_panel.Controls.Add(img);


                }



                dsSensors = null;
                objSensors = null;
                conn.Close();




            }
            **/
                        }
                    }