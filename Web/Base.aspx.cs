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
    public abstract partial class Base : System.Web.UI.Page
    {
        protected readonly string[] Tables = new string[] { "questions", "Slider", "Smiley", "Stars" };
        public Question q;

        protected DBclass DB = new DBclass();


        protected abstract bool isEmpty(TextBox box);//this function check the passed textbox if contain default value or it's empty 
        protected abstract void Save_Click(object sender, EventArgs e);//event handler for click event on save button 


        public void Check_punctuation(string text)
        {
            if (text.Any(char.IsPunctuation))
                throw new PunctuationException();
        }

        protected bool Values_Changes(List<int> Values)//check if values are changed or not 
        {
            try
            {
                if (questionTextbox.Text == "")
                {
                    throw new EmptyException();
                }
                else
                {
                    try
                    {
                        q.Question_text = questionTextbox.Text;//validate user input 
                        Check_punctuation(q.Question_text);
                    }
                    catch (PunctuationException ex)
                    {
                        Alert(ex.Message, ex);
                        return false;
                    }
                    catch (FormatException ex)
                    {
                        Alert("Questions  shouldn't  contain number", ex);
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
                    Alert(ex.Message, ex);
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
                    Alert(ex.Message, ex);
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
                    Alert(ex.Message, ex);
                    return false;
                }
            }
            return true;
        }

        public bool Check(List<int> Values)//this function check if entered values are correct and within thier ranges 
        {
            if (!Values_Changes(Values))//check if values changed or not 
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

        protected void Alert(string Message, Exception ex, bool validation = true)
        {
            if (validation == false)
                Response.Write("<script> alert('Values are not valid\\nCheck Error.txt file') </script>");
            else
            Response.Write("<script > alert('" + ex.Message + "') ;</script>");
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
        protected void Alert(string Message)
        {
            Response.Write("<script> alert('" + Message + "') </script>");
        }

    }
}