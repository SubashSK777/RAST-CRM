using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

public partial class Test : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
       // BindGrid();
    }
   //public void BindGrid()
   // {
   //     try
   //     {
   //         using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
   //         {
   //             conn.Open();
   //             MySqlCommand cmd = new MySqlCommand("select * from alert_log", conn);
   //             MySqlDataAdapter da = new MySqlDataAdapter(cmd);
   //             cmd.ExecuteNonQuery();
   //             DataSet ds = new DataSet();
   //             da.Fill(ds);
   //             dt = ds.Tables[0];
   //             GridView1.DataSource = dt;
   //             GridView1.DataBind();
   //             conn.Close();
   //         }
   //     }
   //    catch(Exception ex)
   //     {

   //     }


    protected void Buttton1_Click(object sender,EventArgs e)
    {
         using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString))
         {
             conn.Open();
             MySqlCommand cmd = new MySqlCommand();
             cmd.CommandText = "select *  from alert_log";
             cmd.Connection = conn;
             MySqlDataReader dr = cmd.ExecuteReader();
             GridView1.DataSource = dr;
             GridView1.DataBind();
         }
    }
   
}
