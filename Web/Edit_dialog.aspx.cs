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
    public partial class Edit_dialog : System.Web.UI.Page
    {
        Master_Page master;

        public int id;//to save index of selected row in gridview         

        /// <summary>
        /// Handles the Load event of the Page control that fill controls with stored data of selected question
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
            master = (Master_Page)Page.Master;
            if (!IsPostBack)//for first time only
            {
                id = Int32.Parse(Session["Id"].ToString());//save index of selected row in gridview in Form1
                SaveButton.Visible = true;//show save button
                SaveButton.Focus();//focus on save button

                DataRow[] data = new DataRow[2];//temp data rows to hold question row from question table and a row from Slider or Smiley or Stars

                data = master.DB.extract_row(id);//return the specified two rows 
                
                string question_type = data[0].ItemArray[2].ToString();//hole question type from first row 


                Show_controls(question_type);//call Show_controls that take question type 

                Retrive_Data(data, question_type);//call Retrive_Data that take the tow rows and question type 

                master.Set_Placeholders();//call Set_Placeholders
            }
        }
        /// <summary>
        /// Shows the controls related to question type.
        /// </summary>
        /// <param name="QuestionType">Type of the question.</param>
        private void Show_controls(string QuestionType)
        {
            master.StartTextbox.Visible = true;
            if (QuestionType == "Slider")
            {
                master.EndTextbox.Visible = true;
                master.Start_captionTextbox.Visible = true;
                master.End_captionTextbox.Visible = true;
            }
        }
        /// <summary>
        /// create object from question class to hold data stored in the passed rows 
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="QuestionType">Type of the question.</param>
        private void Retrive_Data(DataRow[] rows, string QuestionType)
        {
            int id =(int)(Session["Next_Id"]);
            string question_text = rows[0].ItemArray[0].ToString();//hold question text
            int question_order = (int)rows[0].ItemArray[1];//hold question order
            switch (QuestionType)
            {
                case "Slider":
                    int Start = (int)rows[1].ItemArray[1];//hold start value from second row 
                    int End = (int)rows[1].ItemArray[2];//hold end value from second row 
                    string Start_Caption = rows[1].ItemArray[3].ToString() ;//hold start_caption from second row 
                    string End_Caption = rows[1].ItemArray[4].ToString();//hold end_caption from second row 

                    master.q = new Slider(question_text, question_order,id, Start, End, Start_Caption, End_Caption);//create slider object 
                    break;
                case "Smiley":
                    int Faces = (int)rows[1].ItemArray[1];//hold faces number from second row
                    master.q = new Smiley(question_text, question_order, id,Faces);//create smiley object 
                    break;
                case "Stars":
                    int Stars = (int)rows[1].ItemArray[1];//hold stars number from second row 
                    master.q = new Stars(question_text, question_order, id,Stars);//create star object 
                    break;
            }
            ViewState["q"] = master.q;//save question to use next time 
        }

      
        /// <summary>
        /// Disable all controls after saving 
        /// </summary>
        /// <param name="q"></param>
        private void Disable_controls(Question q)
        {
            SaveButton.Enabled = false;
            master.QuestionTextbox.Enabled = false;
            master.StartTextbox.Enabled = false;
            if (q.Question_type == "Slider")
            {
                master.EndTextbox.Enabled = false;
                master.Start_captionTextbox.Enabled = false;
                master.End_captionTextbox.Enabled = false;
            }

        }
        /// <summary>
        /// Handles the Click event of the Save control that update question in the database
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            master.q = (Question)ViewState["q"];//load question from view state (required due to HTTP stateless)
            if (master.Check(master.q.Current_values()))//call method check in Base class that check question's values if they are correct or not 
            {
                try
                {
                    master.DB.Update(master.q);//update database with new question 
                    master.Set_Placeholders();//change hints with new values
                    Disable_controls(master.q);//disable controls to prevent modification after editing 
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Key", "RefreshParent();", true);
                    master.Alert("Done!!");
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "KEY", "setTimeout(function(){Close();},2000);", true);
                }
                catch (Exception ex)
                {
                    master.Alert(ex.Message, ex);//show alert and write error exception into Error.txt file
                }
            }
            else
            {
                master.q.Set_values(master.q.Default_values());//if current values are not correct
            }

        }

    }
}