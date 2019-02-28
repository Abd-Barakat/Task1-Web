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
    public partial class Edit_dialog : Base
    {
        private DataRow [] question = new DataRow[2];
        int order;
        protected override void Save_Click(object sender, EventArgs e)//click event hanlder for save button
        {
            q = (Question)ViewState["q"];
            if (Check(q.Current_values()))//call method check in Base class that check question's values if they are correct or not 
            {
                try
                {
                    DB.Update(q);
                    Alert("Done!!");
                }
                catch (Exception ex)
                {
                    Alert(ex.Message, ex);
                }
            }
            else
            {
                q.Reset_values();
            }
        }
        protected override bool isEmpty(TextBox box)//this method to check if text box is contain default value or not 
        {
            q = (Question)ViewState["q"];
            if (ReferenceEquals(box, questionTextbox))
            {
                if (questionTextbox.Text == "Write a question here ...")
                    return true;
                else
                    return false;
            }
            if (q.Question_type == "Slider")
            {

                if (ReferenceEquals(box, StartTextbox))
                {
                    if (StartTextbox.Text == "")
                        return true;
                    else
                        return false;

                }
                else if (ReferenceEquals(box, EndTextbox))
                {
                    if (EndTextbox.Text == "")
                        return true;
                    else
                        return false;
                }
                else if (ReferenceEquals(box, Start_captionTextbox))
                {
                    if (Start_captionTextbox.Text == "")
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (End_captionTextbox.Text == "")
                        return true;
                    else
                        return false;
                }
            }
            else if (q.Question_type == "Smiley")
            {

                if (StartTextbox.Text == "")
                    return true;
                else
                    return false;
            }
            else if (q.Question_type == "Stars")
            {

                if (StartTextbox.Text == "")
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.DataSource = DB.question_table().Clone();
               // int order = Int32.Parse(Request.QueryString["order"]);
                question= DB.extract_row(order);
                GridView1.Rows[0].Cells[0].Text = question[0].ItemArray[0].ToString();
                GridView1.Rows[0].Cells[1].Text = question[0].ItemArray[1].ToString();
                GridView1.Rows[0].Cells[2].Text = question[0].ItemArray[2].ToString();
                GridView1.DataBind();
            }
        }
    }
}