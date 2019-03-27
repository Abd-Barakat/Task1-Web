using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using DataBase;
using Questions;
using System.Net;
using System.Web.Services;
namespace Web
{
    public partial class QuestionAttributes : System.Web.UI.Page
    {

        private string path = HttpRuntime.AppDomainAppPath;
        private Question question;
        private bool IsEdit = false;
        private int Q_order;
        private DataRow Type_row;//table will hold the selected question (text ,order ,type)
        private DataRow question_row;//table will hold values related to the selected question
        private List<int> Reserved_orders;
        private DBclass DataBase = new DBclass();
        private void Add_Initializer()
        {
            ViewState["OldOrder"] = -1;
            // Next_Order(Old_Order);
        }
        private void Edit_Initializer()
        {
            int id = Int32.Parse(Session["Id"].ToString());
            DataRow[] rows = DataBase.Extract_row(id);
            SaveButton.Click += Save_Updates_Click;
            question_row = rows[0];
            Type_row = rows[1];
            Q_order = Int32.Parse(question_row.ItemArray[1].ToString());
            string question_type = question_row.ItemArray[2].ToString();
            switch (question_type)
            {
                case "Slider":


                    QuestionType_DropDownList.SelectedIndex = 0;
                    QuestionType_DropDownList.Enabled = false;
                    break;
                case "Smiley":
                    QuestionType_DropDownList.SelectedIndex = 1;
                    QuestionType_DropDownList.Enabled = false;
                    break;
                case "Stars":

                    QuestionType_DropDownList.SelectedIndex = 2;
                    QuestionType_DropDownList.Enabled = false;
                    break;
            }
        }
        /// <summary>
        /// Handles the Click event of the Save control that save updates to database.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Save_Updates_Click(object sender, EventArgs e)//event handler for save button that save entered values if they are not confilect database KEYS
        {
            question = (Question)ViewState["Question"];
            try
            {
                if (Change_Values())//call check method to check inserted values before update database 
                {
                    DataBase.Update(question);//upate database with new edited question
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Close", "window.close();", true);
                }
            }
            catch (Exception ex)//to catch eny problem that may occure
            {
                Print_Errors(ex.Message, ex);
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
            if (Page.IsValid)
            {
                question = (Question)ViewState["Question"];
                try
                {
                    int Table_Index = QuestionType_DropDownList.SelectedIndex;
                    if (Table_Index >= 0)//if no control selected in GroupBox
                    {
                        if (Change_Values())//change question's properties if they are correct
                        {

                            DataBase.Insert(question);//call Insert method in DBclass to insert the new question into database
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Close", "window.close();", true);

                        }
                    }
                    else
                    {
                        Print_Errors("Please select question type ");
                    }
                }
                catch (Exception ex)
                {
                    Print_Errors(ex.Message, ex);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Close", "window.close();", true);
                }
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
            if (box.Text == "")
                return true;
            else
                return false;
        }
        private bool Change_Values()//check if values are changed or not 
        {
            try
            {
                if (!IsEmpty(question_box))
                {
                    question.Question_text = question_box.Text;//validate user input 
                }
                else
                {
                    Print_Errors("Question filed is required");
                    Question_Validator.Text = "Question filed is required";
                    Question_Validator.IsValid = false;
                    return false;
                }
            }
            catch (FormatException ex)
            {
                Print_Errors("Question should not contain a number", ex);
                Question_Validator.Text = "Question should not contain a number";
                Question_Validator.IsValid = false;
                return false;
            }
            try
            {
                if (!IsEmpty(QuestionOrder_Textbox))
                {
                    question.Question_order = Int32.Parse(QuestionOrder_Textbox.Text);
                }
                else
                {
                    Print_Errors("Order filed is required");
                    Order_Validator.Text = "Order filed is required";
                    Order_Validator.IsValid = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Print_Errors(ex.Message, ex);
            }
            if (question.Question_type == "Slider")
            {
                Slider slider = (Slider)question;
                try
                {
                    if (!IsEmpty(Shared_textbox))
                    {
                        if (Shared_textbox.Text.All(char.IsDigit))
                        {
                            slider.Start = Int32.Parse(Shared_textbox.Text);
                        }
                        else
                        {
                            throw new FormatException();
                        }
                    }
                    else
                    {
                        Print_Errors("Start Value filed is required");
                        Shared_Validator.Text = "Start Value filed is required";
                        Shared_Validator.IsValid = false;
                        return false;
                    }
                }
                catch (FormatException ex)
                {
                    Print_Errors("Start value should be integer number", ex);
                    Shared_Validator.Text = "Start value should be integer number";
                    Shared_Validator.IsValid = false;
                    return false;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Print_Errors("Start values should be between 0-100", ex);
                    Shared_Validator.Text = "Start values should be between 0-100";
                    Shared_Validator.IsValid = false;
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
                    else
                    {
                        Print_Errors("End Value filed is required");
                        End_Validator.Text = "End Value filed is required";
                        End_Validator.IsValid = false;
                        return false;
                    }
                }
                catch (FormatException ex)
                {
                    Print_Errors("End value should be integer number", ex);
                    End_Validator.Text = "End value should be integer number";
                    End_Validator.IsValid = false;
                    return false;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Print_Errors("End value should be between 0-100", ex);
                    End_Validator.Text = "End value should be between 0-100";
                    End_Validator.IsValid = false;
                    return false;
                }
                try
                {
                    if (!IsEmpty(Start_caption_textBox))
                    {
                        slider.Start_Caption = Start_caption_textBox.Text;
                    }
                    else
                    {
                        Print_Errors("Start Caption filed is required");
                        Start_Caption_Validator.Text = "Start Caption filed is required";
                        Start_Caption_Validator.IsValid = false;
                        return false;
                    }
                }
                catch (FormatException ex)
                {
                    Print_Errors("Start Caption should be text only", ex);

                    return false;
                }
                try
                {
                    if (!IsEmpty(End_caption_textBox))
                    {
                        slider.End_Caption = End_caption_textBox.Text;
                    }
                    else
                    {
                        Print_Errors("End Caption filed is required");
                        End_Caption_Validator.Text = "End Caption filed is required";
                        End_Caption_Validator.IsValid = false;
                        return false;
                    }
                }
                catch (FormatException ex)
                {
                    Print_Errors("End Caption should be text only", ex);
                    return false;
                }
                try
                {
                    return slider.Validate();
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Print_Errors("Start value should be lower than End value", ex);
                    End_Validator.Text = "*";
                    Shared_Validator.Text = "*";
                    Shared_textbox.BorderColor = System.Drawing.Color.Red;
                    End_textBox.BorderColor = System.Drawing.Color.Red;
                    End_Validator.IsValid = false;
                    Shared_Validator.IsValid = false;
                    return false;
                }
            }
            else if (question.Question_type == "Smiley")
            {
                Smiley smiley = (Smiley)question;
                try
                {
                    if (!IsEmpty(Shared_textbox))
                    {
                        if (Shared_textbox.Text.All(char.IsDigit))
                        {
                            smiley.Smiles = Int32.Parse(Shared_textbox.Text);
                        }
                        else
                        {
                            throw new FormatException();
                        }
                    }
                    else
                    {
                        Print_Errors("Smiles filed is required");
                        Shared_Validator.Text = "Smiles filed is required";
                        Shared_Validator.IsValid = false;
                        return false;
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Print_Errors("Number of Smiles should be between 2-5", ex);
                    Shared_Validator.Text = "Number of Smiles should be between 2-5";
                    Shared_Validator.IsValid = false;
                    return false;
                }
                catch (FormatException ex)
                {
                    Print_Errors("Number of Smiles should be positive integer", ex);
                    Shared_Validator.Text = "Number of Smiles should be positive integer";
                    Shared_Validator.IsValid = false;
                    return false;
                }
            }
            else if (question.Question_type == "Stars")
            {
                Stars stars = (Stars)question;
                try
                {
                    if (!IsEmpty(Shared_textbox))
                    {
                        if (Shared_textbox.Text.All(char.IsDigit))
                        {
                            stars.Star = Int32.Parse(Shared_textbox.Text);
                        }
                        else
                        {
                            throw new FormatException();
                        }
                    }
                    else
                    {
                        Print_Errors("Stars filed is required");
                        Shared_Validator.Text = "Stars filed is required";
                        Shared_Validator.IsValid = false;
                        return false;
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Print_Errors("Number of stars  should be between 0-10", ex);
                    Shared_Validator.Text = "Number of stars  should be between 0-10";
                    Shared_Validator.IsValid = false;
                    return false;
                }
                catch (FormatException ex)
                {
                    Print_Errors("Number of Stars should be positive integer", ex);
                    Shared_Validator.Text = "Number of Stars should be positive integer";
                    Shared_Validator.IsValid = false;
                    return false;
                }
            }
            return true;
        }
        public void Set_Placeholders()
        {
            question_box.Attributes.Add("placeholder", question.Question_text);
            switch (question.Question_type)
            {
                case "Slider":
                    Slider slider = (Slider)question;
                    Shared_textbox.Attributes.Add("placeholder", string.Format("Start = {0}", slider.Start));
                    End_textBox.Attributes.Add("placeholder", string.Format("End = {0}", slider.End));
                    Start_caption_textBox.Attributes.Add("placeholder", string.Format("Start Caption = {0}", slider.Start_Caption));
                    End_caption_textBox.Attributes.Add("placeholder", string.Format("End Caption = {0}", slider.End_Caption));
                    break;
                case "Smiley":
                    Smiley smiley = (Smiley)question;
                    Shared_textbox.Attributes.Add("placeholder", string.Format("Faces = {0}", smiley.Smiles));
                    break;
                case "Stars":
                    Stars stars = (Stars)question;
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
        public void Print_Errors(string Message, Exception ex, bool validation = true)
        {
            string Error_file = string.Format(@path + @"\Error.txt");
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
        public void Print_Errors(string Message)
        {
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
            Relese(question);//call method release that release  q object if refere to another object 
            switch (QuestionType_DropDownList.SelectedIndex)
            {
                case 0:
                    question = IsEdit ? question : new Slider("", 0);//create Slider object 
                    break;
                case 1:
                    question = IsEdit ? question : new Smiley("", 0);//create Smiley object 
                    break;
                case 2:
                    question = IsEdit ? question : new Stars("", 0);//create Stars object               
                    break;
            }
            Show_controls();
            ViewState["Question"] = question;
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
                case 0:
                    Slider slider = (Slider)question;
                    Shared_textbox.ToolTip = "Write start value";
                    Shared_textbox.Attributes.Add("placeholder", "Start Value");
                    Shared_textbox.Text = IsEdit ? slider.Start.ToString() : "";//fill textbox with default value of start 
                    End_textBox.Text = IsEdit ? slider.End.ToString() : "";//fill textbox with default value of end 
                    Start_caption_textBox.Text = IsEdit ? slider.Start_Caption : "";//fill textbox with default value of start caption 
                    End_caption_textBox.Text = IsEdit ? slider.End_Caption : "";//fill textbox with default value of end caption
                    End_Validator.Enabled = true;
                    Start_Caption_Validator.Enabled = true;
                    End_Caption_Validator.Enabled = true;

                    End_textBox.Visible = true;
                    End_caption_textBox.Visible = true;
                    Start_caption_textBox.Visible = true;
                    break;
                case 1:
                    Smiley smiley = (Smiley)question;
                    Shared_textbox.ToolTip = "Write Number of Smiles";
                    Shared_textbox.Attributes.Add("placeholder", "Number of smiles");
                    Shared_textbox.Text = IsEdit ? smiley.Smiles.ToString() : "";//fill textbox with default value of faces 

                    End_Validator.Enabled = false;
                    Start_Caption_Validator.Enabled = false;
                    End_Caption_Validator.Enabled = false;

                    End_textBox.Visible = false;
                    End_caption_textBox.Visible = false;
                    Start_caption_textBox.Visible = false; break;
                case 2:
                    Stars stars = (Stars)question;
                    Shared_textbox.ToolTip = "Write Number of Faces";
                    Shared_textbox.Attributes.Add("placeholder", "Number of stars");
                    Shared_textbox.Text = IsEdit ? stars.Star.ToString() : "";//fill textbox with default value of stars 
                    End_Validator.Enabled = false;
                    Start_Caption_Validator.Enabled = false;
                    End_Caption_Validator.Enabled = false;

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
                QuestionType_DropDownList.SelectedIndex = 0;
                QuestionType_DropDownList_SelectedIndexChanged(null, null);
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
            else
            {
                if (IsEdit)
                {
                    SaveButton.Click += Save_Updates_Click;
                }
                else
                {
                    SaveButton.Click += Save_new_question_Click;
                }


            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void QuestionOrder_Textbox_TextChanged(object sender, EventArgs e)
        {
            bool success = Int32.TryParse(QuestionOrder_Textbox.Text, out int Current_Order);
            if (success)
            {

                if (Current_Order > (int)ViewState["OldOrder"])
                {
                    Next_Order(Current_Order);
                    ViewState["OldOrder"] = Current_Order;
                }
                else
                {
                    Prev_Order(Current_Order);
                    ViewState["OldOrder"] = Current_Order;
                }

            }
        }
        /// <summary>
        /// Increment question order with exclude reserved orders that already exist in the database.
        /// </summary>
        /// <param name="Current_Order"></param>
        private void Next_Order(int Current_Order)
        {
            Reserved_orders = (List<int>)ViewState["Reserved_orders"];
            if (Reserved_orders == null)
            {
                Reserved_orders = DataBase.Orders();
                ViewState["Reserved_orders"] = Reserved_orders;
            }
            if (IsEdit)
            {
                Reserved_orders.Remove(question.Question_order);
            }
            while (Reserved_orders.Contains(Current_Order) || Current_Order < 0)
            {
                Current_Order++;
            }
            QuestionOrder_Textbox.Text = Current_Order.ToString();
            Q_order = Current_Order;
        }
        /// <summary>
        /// Decrement question order with exclude reserved orders that already exist in the database.
        /// </summary>
        /// <param name="Current_Order"></param>
        private void Prev_Order(int Current_Order)
        {
            Reserved_orders = (List<int>)ViewState["Reserved_orders"];
            if (Reserved_orders == null)
            {
                Reserved_orders = DataBase.Orders();
                ViewState["Reserved_orders"] = Reserved_orders;
            }
            if (IsEdit)
            {
                Reserved_orders.Remove(question.Question_order);
            }
            while (Reserved_orders.Contains(Current_Order) && Current_Order >= 0)
            {
                Current_Order--;
                if (Current_Order == -1)
                {
                    Next_Order(Current_Order);
                    return;
                }
            }
            QuestionOrder_Textbox.Text = Current_Order.ToString();
            Q_order = Current_Order;
        }

        protected void Order_Validator_Load(object sender, EventArgs e)
        {
            Order_Validator.Validate();
        }

        protected void ServerValidate(object source, ServerValidateEventArgs args)
        {
            CustomValidator validator = (CustomValidator)source;
            TextBox box = (TextBox)FindControl(validator.ControlToValidate);
            if (box.Text == "")
            {
                args.IsValid = false;
                validator.Text = "*";
                box.BorderColor = System.Drawing.Color.Red;
            }
            else
            {
                box.BorderColor = System.Drawing.Color.Black;
            }
        }

       
    }

}