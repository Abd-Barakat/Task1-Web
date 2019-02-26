using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Questions;
using DataBase;
using System.IO;
using System.Data;
namespace Web
{
    public partial class Add_dialog : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
        }
        protected void relese(Question q)
        {
            if (q != null)//to avoid null refrence exception
            {
                q = null;
            }
        }
        protected void Question_types_SelectedIndexChanged(object sender, EventArgs e)
        {
            relese(q);//call method release that release  q object if refere to another object 
            switch (Question_types.SelectedIndex)
            {
                case 0://if slider radio button 

                    EndTextbox.Visible = true;
                    End_captionTextbox.Visible = true;
                    Start_captionTextbox.Visible = true;
                    StartTextbox.Visible = true;

                    q = new Slider();//create slider object
                    q.Question_order = DB.Max_order() + 1;//sign qustion_order property to next_order field
                    Question_types.Items[0].Selected = true;//set check property of Slider radio button to true

                    Make_boxes_Empty();
                    break;
                case 1://if Smiley radio button 
                    EndTextbox.Visible = false;
                    End_captionTextbox.Visible = false;
                    Start_captionTextbox.Visible = false;

                    StartTextbox.Visible = true;//make groupbox that contain above controls visible

                    q = new Smiley();//create smiley object
                    q.Question_order = DB.Max_order() + 1;//sign qustion_order property to next_order field
                    Question_types.Items[1].Selected = true;//set check property of Smiley radio button to true

                    Make_boxes_Empty();
                    break;
                case 2://if Stars radio button 
                    EndTextbox.Visible = false;
                    End_captionTextbox.Visible = false;
                    Start_captionTextbox.Visible = false;

                    StartTextbox.Visible = true;//make groupbox that contain above controls visible

                    q = new Stars();//create star object
                    q.Question_order = DB.Max_order() + 1;//sign qustion_order property to next_order field
                    Question_types.Items[2].Selected = true;//set check property of Stars radio button to true

                    Make_boxes_Empty();

                    break;
            }
             Response.Write("<script> alert('q=" + q.Default_values().ElementAt(0) + "') </script>");
            Make_Empty(questionTextbox);
        }
        protected override void Reset() //to reset default values of smiley ,slider and star questions in case invalid input entered
        {
            q.Reset_values();//call reset values method in Questions class
            Make_boxes_Empty();//call method that clear all textboxes
        }
        protected override bool isEmpty(TextBox box)//this method to check if text box is contain default value or not 
        {
            if (ReferenceEquals(box, questionTextbox))
            {
                if (questionTextbox.Text == "Write a question here ...")
                    return true;
                else
                    return false;
            }
            if (q.Question_type == "Slider")
            {

                if (ReferenceEquals(box, StartTextbox))
                {
                    if (StartTextbox.Text == string.Format("Start ={0}", q.Default_values().ElementAt(0)))
                        return true;
                    else
                        return false;

                }
                else if (ReferenceEquals(box, EndTextbox))
                {
                    if (EndTextbox.Text == string.Format("End ={0}", q.Default_values().ElementAt(1)))
                        return true;
                    else
                        return false;
                }
                else if (ReferenceEquals(box, Start_captionTextbox))
                {
                    if (Start_captionTextbox.Text == string.Format("Start Caption ={0}", q.Default_values().ElementAt(2)))
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (End_captionTextbox.Text == string.Format("End Caption ={0}", q.Default_values().ElementAt(3)))
                        return true;
                    else
                        return false;
                }
            }
            else if (q.Question_type == "Smiles")
            {

                if (StartTextbox.Text == string.Format("Smiles = {0}", q.Default_values().ElementAt(0)))
                    return true;
                else
                    return false;
            }
            else if (q.Question_type == "Stars")
            {

                if (StartTextbox.Text == string.Format("Stars = {0}", q.Default_values().ElementAt(0)))
                    return true;
                else
                    return false;
            }
            else
                return false;

        }
        protected override void Make_Empty(TextBox box)
        {
            if (ReferenceEquals(box, questionTextbox))
            {
                questionTextbox.ForeColor = System.Drawing.Color.Gray;
                questionTextbox.Text = "Write a question here ...";
            }
            if (q.Question_type == "Slider")
            {
                if (ReferenceEquals(box, StartTextbox))
                {
                    StartTextbox.ForeColor = System.Drawing.Color.Gray;
                    StartTextbox.Text = string.Format("Start ={0}", q.Default_values().ElementAt(0));

                }
                else if (ReferenceEquals(box, EndTextbox))
                {
                    EndTextbox.ForeColor = System.Drawing.Color.Gray;
                    EndTextbox.Text = string.Format("End ={0}", q.Default_values().ElementAt(1));
                }
                else if (ReferenceEquals(box, Start_captionTextbox))
                {
                    Start_captionTextbox.ForeColor = System.Drawing.Color.Gray;
                    Start_captionTextbox.Text = string.Format("Start Caption ={0}", q.Default_values().ElementAt(2));

                }
                else if (ReferenceEquals(box, End_captionTextbox))
                {
                    End_captionTextbox.ForeColor = System.Drawing.Color.Gray;
                    End_captionTextbox.Text = string.Format("End Caption ={0}", q.Default_values().ElementAt(3));

                }
            }
            if (q.Question_type == "Smiley")
            {
                if (ReferenceEquals(box, StartTextbox))
                {
                    StartTextbox.ForeColor = System.Drawing.Color.Gray;
                    StartTextbox.Text = string.Format("Smiles = {0}", q.Default_values().ElementAt(0));
                }
            }
            if (q.Question_type == "Stars")
            {
                if (ReferenceEquals(box, StartTextbox))
                {
                    StartTextbox.ForeColor = System.Drawing.Color.Gray;
                    StartTextbox.Text = string.Format("Stars = {0}", q.Default_values().ElementAt(0));
                }
            }
        }
        protected override void Save_Click(object sender, EventArgs e)//click event hanlder for save button
        {
            if (check(q.Current_values()))//call method check in Base class that check question's values if they are correct or not 
            {
                if (!questionTextbox.Text.Any(char.IsDigit) && !isEmpty(questionTextbox))//check question textbox if contain invalid inputs or default text
                {
                    int Groupbox_index = Question_types.SelectedIndex;//return index of selected control in GroupBox

                    if (Groupbox_index != -1)//if no control selected in GroupBox
                    {

                        try
                        {
                            DataTable temp = new DataTable();
                            
                            GridView1.Rows[0].Cells[0].Text = questionTextbox.Text;//fill data grid view with question text
                            GridView1.Rows[0].Cells[1].Text = q.Question_order.ToString();//fill data grid view with question order
                            GridView1.Rows[0].Cells[2].Text = Tables[Groupbox_index + 1];//fill data grid view with question type
                            DB.Insert(Groupbox_index, Tables, q);//call Insert method in DBclass to insert the new question into database
                            Response.Write("<script> alert('Done!!');window.close(); </script>");
                        }
                        catch (Exception ex)
                        {
                            alert(ex.Message);
                            using (StreamWriter stream = new StreamWriter(@"C: \Users\a.barakat\source\repos\Task1\Error.txt", true))//save errors in Error.txt file
                            {
                                stream.WriteLine("-------------------------------------------------------------------\n");
                                stream.WriteLine("Date :" + DateTime.Now.ToLocalTime());
                                while (ex != null)
                                {

                                    stream.WriteLine("Message :\n" + ex.Message);
                                    stream.WriteLine("Stack trace :\n" + ex.StackTrace);

                                    ex = ex.InnerException;
                                }
                            }
                          //  FORM.Visible = false;//hide add_dialog form 
                        }

                    }
                    else
                        alert("Please select question type ");//if no question type is selected 


                }
                else if (isEmpty(questionTextbox) || questionTextbox.Text == "")//if question textbox is empty or contain default value
                {
                    questionTextbox.Text = "";
                    alert("Please Write a question  ");
                }
                else if (questionTextbox.Text.Any(char.IsDigit))//if question textbox  contain a number in it
                {
                    questionTextbox.Text = "";
                    alert("Please Write a question without numbers   ");
                }

            }

        }


    }
}