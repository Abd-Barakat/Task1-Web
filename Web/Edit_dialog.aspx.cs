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
        public int index;//to save index of selected row in gridview         
        /// <summary>
        /// Handles the Click event of the Save control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void Save_Click(object sender, EventArgs e)//click event hanlder for save button
        {
            q = (Question)ViewState["q"];//load question from view state (required due to HTTP stateless)
            if (Check(q.Current_values()))//call method check in Base class that check question's values if they are correct or not 
            {
                try
                {
                    DB.Update(q);//update database with new question 
                    GridView1_Update(q);//update gridview with the new question
                    Alert("Done!!");
                    SaveButton.Visible = false;//hide Save button
                    ScriptManager.RegisterClientScriptBlock(this,GetType(),"KEY", "Show_Close();",true);//call Show_Close() method written in javascript

                }
                catch (Exception ex)
                {
                    Alert(ex.Message, ex);//show alert and write error exception into Error.txt file
                }
            }
            else
            {
                q.Reset_values();//if current values are not correct
            }
        }
        /// <summary>
        /// Determines whether the specified box is empty.
        /// </summary>
        /// <param name="box">The box.</param>
        /// <returns>
        ///   <c>true</c> if the specified box is empty; otherwise, <c>false</c>.
        /// </returns>
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
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)//for first time only
            {
                    index = Int32.Parse(Session["Index"].ToString());//save index of selected row in gridview in Form1
                    SaveButton.Visible = true;//show save button
                    SaveButton.Focus();//focus on save button

                    DataRow[] data = new DataRow[2];//temp data rows to hold question row from question table and a row from Slider or Smiley or Stars
                    data = DB.extract_row(index);//return the specified two rows 

                    DataTable temp_question_table = data[0].Table.Clone();//copy headers only 
                    temp_question_table.Rows.Add(data[0].ItemArray);//copy first row's items in the table 

                    string question_type = data[0].ItemArray[2].ToString();//hole question type from first row 

                    GridView1.DataSource = temp_question_table;
                    GridView1.DataBind();//bind temp_table to gridview

                    Show_controls(question_type);//call Show_controls that take question type 

                    Retrive_Data(data, question_type);//call Retrive_Data that take the tow rows and question type 

                    Set_Placeholders();//call Set_Placeholders
            }
        }
        /// <summary>
        /// Shows the controls related to question type.
        /// </summary>
        /// <param name="QuestionType">Type of the question.</param>
        private void Show_controls(string QuestionType)
        {
            if (QuestionType == "Slider")
            {
                EndTextbox.Visible = true;
                Start_captionTextbox.Visible = true;
                End_captionTextbox.Visible = true;
            }
        }
        /// <summary>
        /// create object from question class to hold data stored in the bassed rows 
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="QuestionType">Type of the question.</param>
        private void Retrive_Data(DataRow[] rows, string QuestionType)
        {
            string question_text = rows[0].ItemArray[0].ToString();//hold question text
            int question_order = (int)rows[0].ItemArray[1];//hold question order
            switch (QuestionType)
            {
                case "Slider":
                    int Start = (int)rows[1].ItemArray[1];//hold start value from second row 
                    int End = (int)rows[1].ItemArray[2];//hold end value from second row 
                    int Start_Caption = (int)rows[1].ItemArray[3];//hold start_caption from second row 
                    int End_Caption = (int)rows[1].ItemArray[4];//hold end_caption from second row 

                    q = new Slider(question_text, question_order, Start, End, Start_Caption, End_Caption);//create slider object 
                    break;
                case "Smiley":
                    int Faces = (int)rows[1].ItemArray[1];//hold faces number from second row
                    q = new Smiley(question_text, question_order, Faces);//create smiley object 
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