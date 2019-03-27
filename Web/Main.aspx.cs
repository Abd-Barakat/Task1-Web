using DataBase;
using Questions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
namespace Web
{
    public partial class Form1 : System.Web.UI.Page
    {
        private DBclass DataBase = new DBclass();
        private List<Question> questions;
        public int Row_index ;//to javascript variable (index)
        public int Row_count ;//to javascript variable (Row_Count)
        private string path ;


        /// <summary>
        /// event handler when page loaded ,this handler for gridview only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DatabaseListBox_Load(object sender, EventArgs e)
        {
            Load_Database();
            Row_count = DatabaseListBox.Rows;//hold # of rows in gridview 
        }
        /// <summary>
        /// Handles the Click event of the DeleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            DataBase.Delete(Row_index);//call delete method from database
            DatabaseListBox_Load(null, null);//call gridview load event handler to update gridview 
            Row_index = -1;
            DatabaseListBox.SelectedIndex = Row_index;
        }
        /// <summary>
        /// Handles the Click event of the Edit_Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Edit_Button_Click(object sender, EventArgs e)
        {
            if (DatabaseListBox.GetSelectedIndices().Length == 1)
            {
                Session["IsEdit"] = true;
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "Edit", "Edit_dialog();", true);
            }
            else if (DatabaseListBox.GetSelectedIndices().Length == 0)
            {
                Alert("No question selected");
            }
            else
            {
                Alert("Please select one question to edit");
            }
            Load_Database();//update datalist box view with new data when add dialog close
        }
        /// <summary>
        /// return id for selected question from database.
        /// </summary>
        /// <returns>
        /// Id for selected question
        /// </returns>
        private int Selected_question_id()//this method return question order of selected question in data grid view 
        {
            try
            {
                int current_row_index = DatabaseListBox.SelectedIndex;//return current row index from data grid view
                return (int)DataBase.Question_table().Rows[current_row_index].ItemArray[3];//return question order from selected question
            }
            catch (Exception ex)
            {
                Alert(ex.Message, ex);
                return -1;
            }
        }
        /// <summary>
        /// Uploads list box with new questions from database.
        /// </summary>
        private void Load_Database()//this method for update List box with data from database 
        {
            if (!IsPostBack)
            {
                Create_Objects();
            }
            try
            {
                DatabaseListBox.Items.Clear();//clear list box from questions
                foreach (Question question in questions)
                {
                    DatabaseListBox.Items.Add(question.Question_text);
                }
            }
            catch (Exception ex)
            {
                Alert(ex.Message, ex);
            }

        }
        /// <summary>
        /// </summary>
        /// return id for selected question from database.
        /// <returns>
        /// id for selection question
        /// </returns>
        private int Selected_question_id(int index)//this method return question order of selected question in data grid view 
        {
            try
            {

                return (int)DataBase.Question_table().Rows[index].ItemArray[3];//return question order from selected question
            }
            catch (Exception ex)
            {
                Alert(ex.Message, ex);
                return -1;
            }
        }
        /// <summary>
        /// Print Errors in Error.txt file and alert with a specific message.
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="ex"></param>
        /// <param name="validation"></param>
        public void Alert(string Message, Exception ex, bool validation = true)
        {
            path = HttpRuntime.AppDomainAppPath;
            string Error_file = string.Format(@path + @"Error.txt");
            if (validation == false)
                Response.Write("<script> alert('Values are not valid\\nCheck Error.txt file') </script>");
            else
                Response.Write("<script > alert('" + Message + "') ;</script>");
            using (StreamWriter stream = new StreamWriter(@Error_file, true))//save errors in Error.txt file
            {
                stream.WriteLine("-------------------------------------------------------------------\n");
                stream.WriteLine("Date :" + DateTime.Now.ToLocalTime());
                while (ex != null)
                {
                    stream.WriteLine("Message :\n" + Message);
                    stream.WriteLine("Stack trace :\n" + ex.StackTrace);
                    ex = ex.InnerException;
                }
            }
        }
        /// <summary>
        /// Print Errors in Error.txt file and alert with a specific message.
        /// </summary>
        /// <param name="Message"></param>
        public void Alert(string Message)
        {
            path = HttpRuntime.AppDomainAppPath;
            Response.Write("<script> alert('" + Message + "') </script>");
            string Error_file = string.Format(@path + @"Error.txt");
            using (StreamWriter stream = new StreamWriter(@Error_file, true))//save errors in Error.txt file
            {
                stream.WriteLine("-------------------------------------------------------------------\n");
                stream.WriteLine("Date :" + DateTime.Now.ToLocalTime());
                stream.WriteLine("Message :\n" + Message);
            }
        }
        /// <summary>
        /// this method return question type from database.
        /// </summary>
        /// <returns></returns>
        private string Qustion_type(int Index)//method return question type as string 
        {
            return DataBase.Question_table().Rows[Index].ItemArray[2].ToString();
        }
        /// <summary>
        /// pair question types with indexes.
        /// </summary>
        /// <returns></returns>
        private int DataTable_index()//return a number that represent question type (used to determine which data table to be retrived)
        {
            switch (Qustion_type(DatabaseListBox.SelectedIndex))
            {
                case "Slider":
                    return 1;
                case "Smiley":
                    return 2;
                case "Stars":
                    return 3;
            }
            return -1;
        }
        /// <summary>
        /// Event handler for Add button click that show Question attribute popup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddButton_Click(object sender, EventArgs e)
        {
            Session["IsEdit"] = false;
            QuestionAttributesFrame.Disabled = false;
            ModalPopupExtender1.Enabled = true;
            ModalPopupExtender1.Show();
        }
        /// <summary>
        /// create Question objects to manipulate them.
        /// </summary>
        private void Create_Objects()
        {
            try
            {
                DataTable[] Temp_Tables = DataBase.Load();
                foreach (DataRow Question_row in Temp_Tables[0].Rows)
                {
                    string Question_text = Question_row.ItemArray[0].ToString();
                    int Question_order = (int)Question_row.ItemArray[1];
                    string Question_type = Question_row.ItemArray[2].ToString();
                    int Id = (int)Question_row.ItemArray[3];
                    switch (Question_type)
                    {
                        case "Slider":
                            DataRow Slider_row = Temp_Tables[1].Select(string.Format("Convert(Question_Id,'System.String') LIKE '%{0}%'", Id)).First();
                            int Start = (int)Slider_row.ItemArray[1];
                            int End = (int)Slider_row.ItemArray[2];
                            string Start_Caption = Slider_row.ItemArray[3].ToString();
                            string End_Caption = Slider_row.ItemArray[4].ToString();
                            Question Slider_question = new Slider(Question_text, Question_order, Id, Start, End, Start_Caption, End_Caption);
                            questions.Add(Slider_question);
                            break;
                        case "Smiley":
                            DataRow Smiley_row = Temp_Tables[2].Select(string.Format("Convert(Question_Id, 'System.String') LIKE '%{0}%'", Id)).First();
                            int Smiles = (int)Smiley_row.ItemArray[1];
                            Question Smiley_question = new Smiley(Question_text, Question_order, Id, Smiles);
                            questions.Add(Smiley_question);
                            break;
                        case "Stars":
                            DataRow Stars_row = Temp_Tables[3].Select(string.Format("Convert(Question_Id, 'System.String') LIKE '%{0}%'", Id)).First();
                            int Stars = (int)Stars_row.ItemArray[1];
                            Question Stars_question = new Stars(Question_text, Question_order, Id, Stars);
                            questions.Add(Stars_question);
                            break;
                    }
                    ViewState["questions"] = questions;
                }
            }
            catch(Exception ex)
            {
                Alert(ex.Message, ex);
            }
        }
    
        /// <summary>
        /// Initialize variables when page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                questions = new List<Question>();
                Row_index = -1;
                Row_count = 0;
            }
            else
            {
                questions = (List<Question>)ViewState["questions"];
            }
        }

        protected void DatabaseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Row_index = DatabaseListBox.SelectedIndex;//used for delete and edit dialog
            if (Row_index != -1)
            {
                Session["question"] = questions[Row_index];
            }
        }

     
    }
}
