using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Questions;
using DataBase;
using System.IO;
namespace Web
{
    public partial class Master_Page : System.Web.UI.MasterPage
    {

        public readonly string[] Tables = new string[] { "questions", "Slider", "Smiley", "Stars" };
        public DBclass DB = new DBclass();
        public Question q;
        
        public GridView GridView1
        {
            set
            {
                gridView1 = value;
            }
            get
            {
                return (GridView)this.FindControl("gridView1");
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

        public void Check_number(string text)
        {
            if (text.Any(char.IsDigit))
            {
                throw new ContainNumberException();
            }
        }
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
        protected bool Change_Values(List<int> Values)//check if values are changed or not 
        {

            try
            {
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
                        Values[0] = Int32.Parse(StartTextbox.Text);//validate user input 
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
                        Values[1] = Int32.Parse(EndTextbox.Text);
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
                        Values[2] = Int32.Parse(Start_captionTextbox.Text);
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
                        Values[3] = Int32.Parse(End_captionTextbox.Text);
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
                        Values[0] = Int32.Parse(StartTextbox.Text);
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
                        Values[0] = Int32.Parse(StartTextbox.Text);
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

        public bool Check(List<int> Values)//this function check if entered values are correct and within thier ranges 
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

        public void Alert(string Message, Exception ex, bool validation = true)
        {
            if (validation == false)
                Response.Write("<script> alert('Values are not valid\\nCheck Error.txt file') </script>");
            else
                Response.Write("<script > alert('" + Message + "') ;</script>");
            using (StreamWriter stream = new StreamWriter(@"C: \Users\a.barakat\source\repos\Task1-Web\Error.txt", true))//save errors in Error.txt file
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
        public void Alert(string Message)
        {
            Response.Write("<script> alert('" + Message + "') </script>");
        }

    }
}