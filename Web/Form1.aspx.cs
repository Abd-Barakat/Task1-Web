using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Questions;
using DataBase;
using System.Data;
namespace Web
{
    public partial class Form1 : System.Web.UI.Page
    {
        private DBclass db = new DBclass();
        protected void Page_Init(object sender, EventArgs e)
        {
            GridView1.DataSource = db.load();
            GridView1.DataBind();
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            Response.Write("<script>");
            Response.Write("window.open('Add_dialog.aspx','_blank',false)");
            Response.Write("</script>");
        }
       
    }
}
