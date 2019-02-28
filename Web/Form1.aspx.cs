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
        public int Row_index=-1;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void GridView1_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = db.load();
            GridView1.DataBind();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Row_index = GridView1.SelectedIndex;
        }
    }
}
