using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Questions;
using System.IO;
using DataBase;
using System.Data;
namespace Web
{
    [System.Web.Script.Services.ScriptService]
    public partial class Master_Page : System.Web.UI.MasterPage
    {
        public DBclass DB = new DBclass();
        private int oldValue = 0;
        public readonly string[] Tables = new string[] { "questions", "Slider", "Smiley", "Stars" };
        public Question q;
        public Panel QuestionPanel
        {
            private set
            {
                questionPanel = value;
            }
            get
            {
                return questionPanel;
            }
        }
        public TextBox QuestionTextbox
        {
            set
            {
                questionTextbox = value;
            }
            get
            {
                return questionTextbox;
            }
        }
        public TextBox StartTextbox
        {
            set
            {
                startTextbox = value;
            }
            get
            {
                return startTextbox;
            }
        }
        public TextBox EndTextbox
        {
            set
            {
                endTextbox = value;
            }
            get
            {
                return endTextbox;
            }
        }
        public TextBox Start_captionTextbox
        {
            set
            {
                start_captionTextbox = value;
            }
            get
            {
                return start_captionTextbox;
            }
        }
        public TextBox End_captionTextbox
        {
            set
            {
                end_captionTextbox = value;
            }
            get
            {
                return end_captionTextbox;
            }
        }
        /// <summary>
        /// check if text is contain a number or not 
        /// </summary>
        /// <param name="text"></param>
        public void Check_number(string text)
        {
            if (text.Any(char.IsDigit))
            {
                throw new ContainNumberException();
            }
        }
        /// <summary>
        /// check if text is contain any punctuation marks
        /// </summary>
        /// <param name="text"></param>
        public void Check_punctuation(string text)
        {
            if (text.Any(char.IsPunctuation))
                throw new PunctuationException();
        }
        /// <summary>
        /// Determines whether the specified box is empty.
        /// </summary>
        /// <param name="box">The box.</param>
        /// <returns>
        ///   <c>true</c> if the specified box is empty; otherwise, <c>false</c>.
        /// </returns>
        protected bool isEmpty(TextBox box)//this method to check if text box is contain default value or not 
        {

            if (ReferenceEquals(box, QuestionTextbox))
            {
                if (QuestionTextbox.Text == "")
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
        public void Set_Placeholders()
        {
            QuestionTextbox.Attributes.Add("placeholder", q.Question_text);
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
        /// <summary>
        /// Change values if they are correct 
        /// </summary>
        /// <param name="Values"></param>
        /// <returns></returns>
        protected bool Change_Values(List<string> Values)//check if values are changed or not 
        {
        
            try
            {
                q.Question_order = Int32.Parse(OrderTextbox.Text);
                if (QuestionTextbox.Text == "")
                {
                    throw new EmptyException();
                }
                else
                {
                    try
                    {
                        Check_punctuation(questionTextbox.Text);
                        Check_number(questionTextbox.Text);
                        q.Question_text = QuestionTextbox.Text;//validate user input 
                    }
                    catch (PunctuationException ex)
                    {
                        Alert(ex.Message, ex);
                        return false;
                    }
                    catch (ContainNumberException ex)
                    {
                        Alert(ex.Message, ex, false);
                        return false;
                    }


                }
            }
            catch (EmptyException ex)
            {
                Alert(ex.Message, ex);
                return false;
            }



            if (q.Question_type == "Slider")
            {
                try
                {
                    if (!isEmpty(StartTextbox))
                    {
                        Check_punctuation(StartTextbox.Text);
                        Values[0] = StartTextbox.Text;//validate user input 
                    }
                }
                catch (FormatException ex)
                {
                    Alert("Start value should be integer number", ex);
                    return false;
                }
                catch (PunctuationException ex)
                {
                    Alert(ex.Message, ex);
                    return false;
                }

                try
                {
                    if (!isEmpty(EndTextbox))
                    {
                        Check_punctuation(EndTextbox.Text);
                        Values[1] = EndTextbox.Text;
                    }
                }
                catch (FormatException ex)
                {
                    Alert("End value should be integer number", ex);
                    return false;
                }
                catch (PunctuationException ex)
                {
                    Alert(ex.Message, ex);
                    return false;
                }


                try
                {
                    if (!isEmpty(Start_captionTextbox))
                    {
                        Check_punctuation(Start_captionTextbox.Text);
                        Values[2] = Start_captionTextbox.Text;
                    }
                }
                catch (FormatException ex)
                {
                    Alert("Start caption should be integer number", ex);
                    return false;
                }
                catch (PunctuationException ex)
                {
                    Alert(ex.Message, ex);
                    return false;
                }


                try
                {
                    if (!isEmpty(End_captionTextbox))
                    {
                        Check_punctuation(End_captionTextbox.Text);
                        Values[3] = End_captionTextbox.Text;
                    }
                }
                catch (FormatException ex)
                {
                    Alert("End caption should be integer number", ex);
                    return false;
                }
                catch (PunctuationException ex)
                {
                    Alert(ex.Message, ex);
                    return false;
                }

                try
                {
                    q.Set_values(Values);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Alert(ex.Message, ex, false);
                    return false;
                }

            }
            else if (q.Question_type == "Smiley")
            {
                try
                {
                    if (!isEmpty(StartTextbox))
                    {
                        Check_punctuation(StartTextbox.Text);
                        Values[0] = StartTextbox.Text;
                        q.Set_values(Values);
                    }
                }
                catch (FormatException ex)
                {
                    Alert("Number of Smiles should be integer number", ex);
                    return false;
                }
                catch (PunctuationException ex)
                {
                    Alert(ex.Message, ex);
                    return false;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Alert(ex.Message, ex, false);
                    return false;
                }

            }
            else if (q.Question_type == "Stars")
            {
                try
                {
                    if (!isEmpty(StartTextbox))
                    {
                        Check_punctuation(StartTextbox.Text);
                        Values[0] = StartTextbox.Text;
                        q.Set_values(Values);
                    }
                }
                catch (FormatException ex)
                {

                    Alert("Number of Stars should be integer", ex);
                    return false;
                }
                catch (PunctuationException ex)
                {
                    Alert(ex.Message, ex);
                    return false;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Alert(ex.Message, ex, false);
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// check if entered values are correct and within thier ranges 
        /// </summary>
        /// <param name="Values"></param>
        /// <returns></returns>
        public bool Check(List<string> Values)
        {
            if (!Change_Values(Values))//check if values changed (or not) correctly or not 
            {
                return false;
            }
            try
            {
                return q.Validate();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Alert(ex.Message, ex, false);
                return false;
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
            //string Error_file = string.Format(@path + @"\Error.txt");
            if (validation == false)
                Response.Write("<script> alert('Values are not valid\\nCheck Error.txt file') </script>");
            else
                Response.Write("<script > alert('" + Message + "') ;</script>");
            using (StreamWriter stream = new StreamWriter(@"C:\Users\a.barakat\source\repos\Task1-Web\Error.txt", true))//save errors in Error.txt file
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
        }
        ///// <summary>
        ///// Handles the ValueChanged event of the QuestionOrderUpDown control, call Prev_Number_UpDown or Next_Number_UpDown depends on up or down button that pressed.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        //private void QuestionOrderUpDown_ValueChanged(object sender, EventArgs e)
        //{
        //    if (Int32.Parse(OrderTextbox.Text) > oldValue)
        //    {
        //        Next_Number_UpDown(OrderTextbox);
        //        oldValue = Int32.Parse(OrderTextbox.Text);
        //    }
        //    else
        //    {
        //        Prev_Number_UpDown(OrderTextbox);
        //        oldValue = Int32.Parse(OrderTextbox.Text);
        //    }

        //}
     
        
    }
}