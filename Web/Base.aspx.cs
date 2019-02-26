using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Questions;
using DataBase;
namespace Web
{
    public abstract partial class Base : System.Web.UI.Page
    {
        protected readonly string[] Tables = new string[] { "questions", "Slider", "Smiley", "Stars" };
        public Question q;

        protected DBclass DB = new DBclass();


        protected abstract void Make_Empty(TextBox box);//this function to fill each  textboxes in the dialog with default values 
        protected abstract bool isEmpty(TextBox box);//this function check the passed textbox if contain default value or it's empty 
        protected abstract void Save_Click(object sender, EventArgs e);//event handler for click event on save button 
        protected abstract void Reset();//reset values and then call Make_Empty method to print them in textboxes
       
        public void Make_boxes_Empty()//call Make_Empty method to all textboxes
        {
            if (q.Question_type == "Slider")
            {
                Make_Empty(StartTextbox);
                Make_Empty(EndTextbox);
                Make_Empty(Start_captionTextbox);
                Make_Empty(End_captionTextbox);
            }
            if (q.Question_type == "Smiley")
            {
                Make_Empty(StartTextbox);
            }
            if (q.Question_type == "Stars")
            {
                Make_Empty(StartTextbox);
            }
        }

        protected void TextChanged(object sender, EventArgs e)//event handler to change color or each text in textboxes in dialog and make them Empty 
        {
            if (ReferenceEquals(sender, questionTextbox))
            {
                if (isEmpty(questionTextbox))
                {
                    questionTextbox.Text = "";
                    questionTextbox.ForeColor = System.Drawing.Color.Black;
                }
            }
            else if (ReferenceEquals(sender, StartTextbox))
            {
                if (isEmpty(StartTextbox))
                {
                    StartTextbox.Text = "";
                    StartTextbox.ForeColor = System.Drawing.Color.Black;
                }

            }
            else if (ReferenceEquals(sender, EndTextbox))
            {
                if (isEmpty(EndTextbox))
                {
                    EndTextbox.Text = "";

                    EndTextbox.ForeColor = System.Drawing.Color.Black;
                }

            }
            else if (ReferenceEquals(sender, Start_captionTextbox))
            {
                if (isEmpty(Start_captionTextbox))
                {
                    Start_captionTextbox.Text = "";
                    Start_captionTextbox.ForeColor = System.Drawing.Color.Black;
                }

            }
            else if (ReferenceEquals(sender, End_captionTextbox))
            {
                if (isEmpty(End_captionTextbox))
                {
                    End_captionTextbox.Text = "";
                    End_captionTextbox.ForeColor = System.Drawing.Color.Black;
                }

            }


        }

        protected bool Values_Changes(List<int> Values)//check if values are changed or not 
        {

            if (questionTextbox.Text == "")
            {
                Make_Empty(questionTextbox);

            }
            else
            {
                try
                {
                    if (!isEmpty(questionTextbox))
                    {
                        q.Question_text = questionTextbox.Text;//validate user input 
                        if (q.Question_text == "")
                        {
                            Alert("Questions  shouldn't  contain any punctuation  mark");
                            return false;
                        }
                    }
                    q.Question_text = questionTextbox.Text;//validate user input 

                }
                catch (FormatException)
                {
                    Alert("Questions  shouldn't  contain number");
                    return false;
                }
            }

            if (q.Question_type == "Slider")
            {
                {
                    if (StartTextbox.Text == "")
                    {
                        Make_Empty(StartTextbox);
                    }
                    else
                    {
                        try
                        {
                            if (!isEmpty(StartTextbox))
                            {

                                Values[0] = Int32.Parse(StartTextbox.Text);//validate user input 
                            }

                        }
                        catch (FormatException)
                        {
                            Alert("Start value should be integer number");
                            return false;
                        }
                    }
                    if (EndTextbox.Text == "")
                    {
                        Make_Empty(EndTextbox);
                    }
                    else
                    {
                        try
                        {
                            if (!isEmpty(EndTextbox))
                                Values[1] = Int32.Parse(EndTextbox.Text);

                        }
                        catch (FormatException)
                        {
                            Alert("End value should be integer number");
                            return false;
                        }
                    }
                    if (Start_captionTextbox.Text == "")
                    {
                        Make_Empty(Start_captionTextbox);
                    }
                    else
                    {
                        try
                        {
                            if (!isEmpty(Start_captionTextbox))
                                Values[2] = Int32.Parse(Start_captionTextbox.Text);

                        }
                        catch (FormatException)
                        {
                            Alert("Start caption should be integer number");
                            return false;
                        }
                    }
                    if (EndTextbox.Text == "")
                    {

                        Make_Empty(EndTextbox);
                    }
                    else
                    {
                        try
                        {
                            if (!isEmpty(EndTextbox))
                                Values[3] = Int32.Parse(EndTextbox.Text);
                        }
                        catch (FormatException)
                        {
                            Alert("End caption should be integer number"); 
                            return false;
                        }
                    }
                }
                q.Set_values(Values);
            }
            else if (q.Question_type == "Smiley")
            {

                if (StartTextbox.Text == "")
                {
                    Make_Empty(StartTextbox);
                }

                else
                {
                    try
                    {
                        if (!isEmpty(StartTextbox))
                            Values[0] = Int32.Parse(StartTextbox.Text);
                    }
                    catch (FormatException)
                    {
                        Alert("Number of Smiles should be integer number");
                        return false;
                    }
                }
                q.Set_values(Values);

            }
            else if (q.Question_type == "Stars")
            {

                if (StartTextbox.Text == "")
                {
                    Make_Empty(StartTextbox);
                }
                else
                {
                    try
                    {
                        if (!isEmpty(StartTextbox))
                            Values[0] = Int32.Parse(StartTextbox.Text);

                    }
                    catch (FormatException)
                    {
                        Alert("Number of Stars should be integer");
                        return false;
                    }
                }
                q.Set_values(Values);

            }

            return true;
        }

        public bool Check(List<int> Values)//this function check if entered values are correct and within thier ranges 
        {
            if (!Values_Changes(Values))//if no values entered then no need to check 
            {
                return false;
            }
            return q.Validate();

        }

        protected void Alert(string Message)
        {
            Response.Write("<script> alert('"+Message +"') </script>");
        }
    }
}