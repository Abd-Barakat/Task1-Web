using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using DataBase;
using Questions;
using System.Net;
namespace Web
{
    public partial class QuestionAttributes : System.Web.UI.Page
    {
        private List<string> Slider_Defaults = new List<string>();
        private int Smiley_Default;
        private int Stars_Default;
        private int Next_ID;
        private string path = HttpRuntime.AppDomainAppPath;
        private Question q;
        private bool IsEdit = false;
        private int Q_order;
        private DataRow Type_row;//table will hold the selected question (text ,order ,type)
        private DataRow question_row;//table will hold values related to the selected question
        private List<int> Reserved_orders = new List<int>();
        private DBclass DB = new DBclass();
        private void Add_Initializer()
        {
           // SaveButton.Click += Save_new_question_Click;
            Slider_Defaults.Add("0");
            Slider_Defaults.Add("100");
            Slider_Defaults.Add("Not satisfied");
            Slider_Defaults.Add("Extremely statisfied");
            Smiley_Default = 3;
            Stars_Default = 5;
            ViewState["Slider_Defaults"] = Slider_Defaults;
            ViewState["Smiley_Default"] = Smiley_Default;
            ViewState["Stars_Default"] = Stars_Default;
        }
        private void Edit_Initializer()
        {
            int id = Int32.Parse(Session["Id"].ToString());
            DataRow[] rows = DB.Extract_row(id);
            SaveButton.Click += Save_Updates_Click;
            question_row = rows[0];
            IsEdit = true;
            Type_row = rows[1];
            Q_order = Int32.Parse(question_row.ItemArray[1].ToString());
            string question_type = question_row.ItemArray[2].ToString();
            Next_ID = Int32.Parse(question_row.ItemArray[3].ToString());
            switch (question_type)
            {
                case "Slider":
                    Slider_Defaults.Add(Type_row.ItemArray[1].ToString());
                    Slider_Defaults.Add(Type_row.ItemArray[2].ToString());
                    Slider_Defaults.Add(Type_row.ItemArray[3].ToString());
                    Slider_Defaults.Add(Type_row.ItemArray[4].ToString());

                    QuestionType_DropDownList.SelectedIndex = 0;
                    QuestionType_DropDownList.Enabled = false;
                    break;
                case "Smiley":
                    Smiley_Default = Int32.Parse(Type_row.ItemArray[1].ToString());
                    QuestionType_DropDownList.SelectedIndex = 1;
                    QuestionType_DropDownList.Enabled = false;
                    break;
                case "Stars":
                    Stars_Default = Int32.Parse(Type_row.ItemArray[1].ToString());

                    QuestionType_DropDownList.SelectedIndex = 2;
                    QuestionType_DropDownList.Enabled = false;
                    break;
            }
        }
        /// <summary>
        /// initialize Edit dialog's variables.
        /// </summary>
        /// <param name="rows"></param>
        public QuestionAttributes()
        {
            //bool.TryParse(Request.QueryString["IsEmpty"],out IsEdit);

        }
        /// <summary>
        /// Handles the Click event of the Save control that save updates to database.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Save_Updates_Click(object sender, EventArgs e)//event handler for save button that save entered values if they are not confilect database KEYS
        {
            q = (Question)ViewState["Question"];
            try
            {
                if (Change_Values())//call check method to check inserted values before update database 
                {
                    DB.Update(q);//upate database with new edited question
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Close", "window.close();", true);
                }
            }
            catch (Exception ex)//to catch eny problem that may occure
            {
                Alert(ex.Message, ex);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Close", "window.close();", true);
            }
        }
        /// <summary>
        /// Handles the Click event of the Save control that save new question to database.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Save_new_question_Click(object sender, EventArgs e)
        {
          //  Alert(QuestionOrder_UpDown.Text);
            q = (Question)ViewState["Question"];
            try
            {
                int Table_Index = QuestionType_DropDownList.SelectedIndex;
                if (Table_Index >= 0)//if no control selected in GroupBox
                {
                    if (Change_Values())//change question's properties if they are correct
                    {
                        DB.Insert(q);//call Insert method in DBclass to insert the new question into database
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Close", "window.close();", true);
                    }
                }
                else
                {
                    Alert("Please select question type ");
                }
            }
            catch (Exception ex)
            {
                Alert(ex.Message, ex);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Close", "window.close();", true);
            }
        }
        /// <summary>
        /// Determines whether the specified box is empty.
        /// </summary>
        /// <param name="box">The box.</param>
        /// <returns>
        ///   <c>true</c> if the specified box is empty; otherwise, <c>false</c>.
        /// </returns>
        private bool IsEmpty(TextBox box)
        {
            if (ReferenceEquals(box, question_box))
            {
                if (question_box.Text == "")
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, Shared_textbox))
            {
                switch (q.Question_type)
                {
                    case "Slider":
                        if (Shared_textbox.Text == string.Format("{0}", Slider_Defaults[0]) || Shared_textbox.Text == "")
                            return true;
                        else
                            return false;
                    case "Smiley":
                        if (Shared_textbox.Text == string.Format("{0}", Smiley_Default) || Shared_textbox.Text == "")
                            return true;
                        else
                            return false;
                    case "Stars":
                        if (Shared_textbox.Text == string.Format("{0}", Stars_Default) || Shared_textbox.Text == "")
                            return true;
                        else
                            return false;
                    default:
                        return false;
                }

            }
            else if (ReferenceEquals(box, End_textBox) || End_textBox.Text == "")
            {
                if (End_textBox.Text == string.Format("{0}", Slider_Defaults[1]))
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, Start_caption_textBox) || Start_caption_textBox.Text == "")
            {
                if (Start_caption_textBox.Text == string.Format("{0}", Slider_Defaults[2]))
                    return true;
                else
                    return false;
            }
            else if (ReferenceEquals(box, End_caption_textBox) || End_caption_textBox.Text == "")
            {
                if (End_caption_textBox.Text == string.Format("{0}", Slider_Defaults[3]))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        private bool Change_Values()//check if values are changed or not 
        {
            try
            {
                q.Question_order = Q_order;
                if (!IsEmpty(question_box))
                {
                    q.Question_text = question_box.Text;//validate user input 
                }
                else
                {
                    Alert("Please write a question");
                    return false;
                }
            }
            catch (FormatException ex)
            {
                Alert("Question should not contain a number", ex);
                return false;
            }
            if (q.Question_type == "Slider")
            {
                Slider slider = (Slider)q;
                try
                {
                    //if (!IsEmpty(Shared_textbox))
                    {
                        //if (Shared_textbox.Text.All(char.IsDigit))
                        //{
                        //    slider.Start = Int32.Parse(Shared_textbox.Text);
                        //}
                        //else
                        //{
                        //    throw new FormatException();
                        //}
                    }
                }
                catch (FormatException ex)
                {

                    Alert("Start value should be integer number", ex);
                    return false;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Alert("Start values should be between 0-100", ex);
                    return false;
                }
                try
                {
                    if (!IsEmpty(End_textBox))
                    {
                        if (End_textBox.Text.All(char.IsDigit))
                        {
                            slider.End = Int32.Parse(End_textBox.Text);

                        }
                        else
                        {
                            throw new FormatException();
                        }
                    }
                }
                catch (FormatException ex)
                {
                    Alert("End value should be integer number", ex);
                    return false;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Alert("End value should be between 0-100", ex);
                    return false;
                }
                try
                {

                    if (!IsEmpty(Start_caption_textBox))
                    {
                        slider.Start_Caption = Start_caption_textBox.Text;
                    }
                }
                catch (FormatException ex)
                {
                    Alert("Start Caption should be text only", ex);
                    return false;
                }
                try
                {

                    if (!IsEmpty(End_caption_textBox))
                    {
                        slider.End_Caption = End_caption_textBox.Text;
                    }
                }
                catch (FormatException ex)
                {
                    Alert("End Caption should be text only", ex);
                    return false;
                }
                try
                {
                    return slider.Validate();
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Alert("Start value should be lower than End value", ex);
                    return false;
                }
            }
            else if (q.Question_type == "Smiley")
            {
                Smiley smiley = (Smiley)q;
                try
                {

                    //if (!IsEmpty(Shared_textbox))
                    //{
                    //    if (Shared_textbox.Text.All(char.IsDigit))
                    //    {
                    //        smiley.Smiles = Int32.Parse(Shared_textbox.Text);
                    //    }
                    //    else
                    //    {
                    //        throw new FormatException();
                    //    }
                    //}
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Alert("Number of faces should be between 2-5", ex);
                    return false;
                }
                catch (FormatException ex)
                {
                    Alert("Number of Smiles should be integer number", ex);
                    return false;
                }
            }
            else if (q.Question_type == "Stars")
            {
                Stars stars = (Stars)q;
                try
                {


                    //if (!IsEmpty(Shared_textbox))
                    //{
                    //    if (Shared_textbox.Text.All(char.IsDigit))
                    //    {
                    //        stars.Star = Int32.Parse(Shared_textbox.Text);
                    //    }
                    //    else
                    //    {
                    //        throw new FormatException();
                    //    }
                    //}
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Alert("Number of stars  should be between 0-10", ex);
                    return false;
                }
                catch (FormatException ex)
                {
                    Alert("Number of Stars should be integer", ex);
                    return false;
                }
            }
            return true;
        }
        public void Set_Placeholders()
        {
            question_box.Attributes.Add("placeholder", q.Question_text);
            switch (q.Question_type)
            {
                case "Slider":
                    Slider slider = (Slider)q;
                    Shared_textbox.Attributes.Add("placeholder", string.Format("Start = {0}", slider.Start));
                    End_textBox.Attributes.Add("placeholder", string.Format("End = {0}", slider.End));
                    Start_caption_textBox.Attributes.Add("placeholder", string.Format("Start Caption = {0}", slider.Start_Caption));
                    End_caption_textBox.Attributes.Add("placeholder", string.Format("End Caption = {0}", slider.End_Caption));
                    break;
                case "Smiley":
                    Smiley smiley = (Smiley)q;
                    Shared_textbox.Attributes.Add("placeholder", string.Format("Faces = {0}", smiley.Smiles));
                    break;
                case "Stars":
                    Stars stars = (Stars)q;
                    Shared_textbox.Attributes.Add("placeholder", string.Format("Stars = {0}", stars.Star));
                    break;
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
            string Error_file = string.Format(@path + @"\Error.txt");
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
            Response.Write("<script> alert('" + Message + "') </script>");
            string Error_file = string.Format(@path + @"\Error.txt");
            using (StreamWriter stream = new StreamWriter(@Error_file, true))//save errors in Error.txt file
            {
                stream.WriteLine("-------------------------------------------------------------------\n");
                stream.WriteLine("Date :" + DateTime.Now.ToLocalTime());
                stream.WriteLine("Message :\n" + Message);
            }
        }
        /// <summary>
        /// Releses the specified question.
        /// </summary>
        /// <param name="q">The q.</param>
        private void Relese(Question q)
        {
            if (q != null)//to avoid null refrence exception
            {
                q = null;
            }
        }
        /// <summary>
        /// Event hander for question type selection
        /// Create question object depends on selection of question type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void QuestionType_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Slider_Defaults = (List<string>)ViewState["Slider_Defaults"];
            Smiley_Default = (int)ViewState["Smiley_Default"];
            Stars_Default = (int)ViewState["Stars_Default"];
            Next_ID = Int32.Parse(Session["Next_Id"].ToString());
            Relese(q);//call method release that release  q object if refere to another object 
           // Q_order = Int32.Parse(QuestionOrder_UpDown.Text);
            switch (QuestionType_DropDownList.SelectedIndex)
            {
                case 0:
                    Hide_controls();
                    break;
                case 1:
                    q = new Slider("", Q_order, Next_ID, Int32.Parse(Slider_Defaults[0]), Int32.Parse(Slider_Defaults[1]), Slider_Defaults[2], Slider_Defaults[3]);//create slider object
                    break;
                case 2:
                    q = new Smiley("", Q_order, Next_ID, Smiley_Default);//create smiley object
                    break;
                case 3:
                    q = new Stars("", Q_order, Next_ID, Stars_Default);//create star object               
                    break;
            }
            Show_controls();
            ViewState["Question"] = q;
        }
        private void Hide_controls()
        {
            Shared_textbox.Visible = false;
            End_textBox.Visible = false;
            Start_caption_textBox.Visible = false;
            End_caption_textBox.Visible = false;
        }
        /// <summary>
        /// Show related controls depending on question type.
        /// </summary>
        private void Show_controls()
        {
            Shared_textbox.Visible = true;
            int index = QuestionType_DropDownList.SelectedIndex;
            switch (index)
            {
                case 1:
                   
                    Shared_textbox.ToolTip = "Enter start value";
                    Slider_Defaults[0] = IsEdit == true ? Slider_Defaults[0] : "0";
                    Shared_textbox.Text = Slider_Defaults[0];//fill textbox with default value of start 
                    End_textBox.Text = Slider_Defaults[1];//fill textbox with default value of end 
                    Start_caption_textBox.Text = Slider_Defaults[2];//fill textbox with default value of start caption 
                    End_caption_textBox.Text = Slider_Defaults[3];//fill textbox with default value of end caption
                    End_textBox.Visible = true;
                    End_caption_textBox.Visible = true;
                    Start_caption_textBox.Visible = true;
                    break;
                case 2:
                    Shared_textbox.Attributes.Add("placeholder", "Enter number of smiles");
                    Smiley_Default = IsEdit == true ? Smiley_Default : 3;
                    Shared_textbox.Text = Smiley_Default.ToString();//fill textbox with default value of faces 
                    End_textBox.Visible = false;
                    End_caption_textBox.Visible = false;
                    Start_caption_textBox.Visible = false; break;
                case 3:
                    Shared_textbox.Attributes.Add("placeholder", "Enter number of stars");
                    Stars_Default = IsEdit == true ? Stars_Default : 5;
                    Shared_textbox.Text = Stars_Default.ToString();//fill textbox with default value of stars 
                    End_textBox.Visible = false;
                    End_caption_textBox.Visible = false;
                    Start_caption_textBox.Visible = false;

                    break;
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Close", "window.close();", true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IsEdit = (bool)Session["IsEdit"];
                if (IsEdit)
                {
                    Edit_Initializer();
                }
                else
                {
                    Add_Initializer();
                }
            }

        }

      
    }

}