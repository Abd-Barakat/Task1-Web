using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Questions;
using System.Data;
using DataBase;
namespace Web
{
    public partial class Form1 : System.Web.UI.Page
    {
        private DBclass db = new DBclass();
        public int Row_index = -1;//to javascript variable (index)
        public int Row_count = 0;//to javascript variable (Row_Count)
        /// <summary>
        /// event handler when page loaded ,this handler for gridview only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_Load(object sender, EventArgs e)
        {
            Row_index = GridView1.SelectedIndex;//used for delete  and edit dialog
            Session["Index"] = Row_index.ToString();//used in edit dialog
            GridView1.DataSource = db.load();//return question table and assign it to gridview 
            GridView1.DataBind();//connect between gridview and data source
            Row_count = GridView1.Rows.Count;//hold # of rows in gridview 
        }
        /// <summary>
        /// Handles the Click event of the DeleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            db.Delete(Row_index);//call delete method from database
            GridView1_Load(null, null);//call gridview load event handler to update gridview 
            Row_index = -1;//used for delete and edit dialog
        }
        /// <summary>
        /// event handler for selected index changed of gridview 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Row_index = GridView1.SelectedIndex;//used for delete and edit dialog
            Session["Index"] = Row_index.ToString();//used in edit dialog
        }


    }
}
