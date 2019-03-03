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
        public int order;
        protected bool Visablilty = false;
        protected override void Save_Click(object sender, EventArgs e)//click event hanlder for save button
        {
            q = (Question)ViewState["q"];
            if (Check(q.Current_values()))//call method check in Base class that check question's values if they are correct or not 
            {
                try
                {
                    DB.Update(q);
                    GridView1_Update(q);
                    Alert("Done!!");
                    SaveButton.Visible = false;
                    Response.Write("<script> document.getElementById(\"CloseButton\").style.display='';</script>");
                    
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
                if (questionTextbox.Text == "")
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
                order = Int32.Parse(Session["Order"].ToString());

                DataRow[] data = new DataRow[2];
                data = DB.extract_row(order);

                DataTable temp_question_table = data[0].Table.Clone();
                temp_question_table.Rows.Add(data[0].ItemArray);

                string question_type = data[0].ItemArray[2].ToString();

                GridView1.DataSource = temp_question_table;
                GridView1.DataBind();

                Show_controls(question_type);

                Retrive_Data(data, question_type);

                Set_Placeholders();
            }
        }
        private void Show_controls(string QuestionType)
        {
            if (QuestionType == "Slider")
            {
                EndTextbox.Visible = true;
                Start_captionTextbox.Visible = true;
                End_captionTextbox.Visible = true;
            }
        }
        private void Retrive_Data(DataRow[] rows, string QuestionType)//load saved data of selected question from database 
        {
            string question_text = rows[0].ItemArray[0].ToString();
            int question_order = (int)rows[0].ItemArray[1];
            switch (QuestionType)
            {
                case "Slider":
                    int Start = (int)rows[1].ItemArray[1];
                    int End = (int)rows[1].ItemArray[2];
                    int Start_Caption = (int)rows[1].ItemArray[3];
                    int End_Caption = (int)rows[1].ItemArray[4];

                    q = new Slider(question_text, question_order, Start, End, Start_Caption, End_Caption);
                    break;
                case "Smiley":
                    int Faces = (int)rows[1].ItemArray[1];
                    q = new Smiley(question_text, question_order, Faces);
                    break;
                case "Stars":
                    int Stars = (int)rows[1].ItemArray[1];
                    q = new Stars(question_text, question_order, Stars);
                    break;
            }
            ViewState["q"] = q;
        }
        private void Set_Placeholders()
        {
            switch (q.Question_type)
            {
                case "Slider":
                    StartTextbox.Attributes.Add("placeholder", string.Format("Start = {0}", q.Current_values().ElementAt(0).ToString()));
                    EndTextbox.Attributes.Add("placeholder", string.Format("End = {0}", q.Current_values().ElementAt(1).ToString()));
                    Start_captionTextbox.Attributes.Add("placeholder", string.Format("Start Caption = {0}", q.Current_values().ElementAt(2).ToString()));
                    End_captionTextbox.Attributes.Add("placeholder", string.Format("End Caption = {0}", q.Current_values().ElementAt(3).ToString()));
                    break;
                case "Smiley":
                    StartTextbox.Attributes.Add("placeholder", string.Format("Faces = {0}", q.Current_values().ElementAt(0).ToString()));
                    break;
                case "Stars":
                    StartTextbox.Attributes.Add("placeholder", string.Format("Stars = {0}", q.Current_values().ElementAt(0).ToString()));
                    break;
            }
        }
        private void GridView1_Update(Question q)
        {
            GridView1.Rows[0].Cells[0].Text = q.Question_text;
            GridView1.Rows[0].Cells[1].Text = q.Question_order.ToString();
            GridView1.Rows[0].Cells[2].Text = q.Question_type;
            Clear_Textboxes();
            Set_Placeholders();
        }
        private void Clear_Textboxes()
        {
            StartTextbox.Text = "";
            EndTextbox.Text = "";
            Start_captionTextbox.Text = "";
            End_captionTextbox.Text = "";
        }

       
    }
}