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
using System.Threading;
namespace Web
{
    public partial class Add_dialog : Base
    {


        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.DataSource = DB.question_table().Clone();
                GridView1.DataBind();
            }
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
                    SaveButton.Visible = true;
                    ViewState["q"] = q;
                    Question_types.Items[0].Selected = true;//set check property of Slider radio button to true

                    break;
                case 1://if Smiley radio button 
                    EndTextbox.Visible = false;
                    End_captionTextbox.Visible = false;
                    Start_captionTextbox.Visible = false;

                    StartTextbox.Visible = true;//make groupbox that contain above controls visible

                    q = new Smiley();//create smiley object
                    q.Question_order = DB.Max_order() + 1;//sign qustion_order property to next_order field
                    SaveButton.Visible = true;
                    ViewState["q"] = q;
                    Question_types.Items[1].Selected = true;//set check property of Smiley radio button to true

                    break;
                case 2://if Stars radio button 
                    EndTextbox.Visible = false;
                    End_captionTextbox.Visible = false;
                    Start_captionTextbox.Visible = false;

                    StartTextbox.Visible = true;//make groupbox that contain above controls visible
                    q = new Stars();//create star object
                    q.Question_order = DB.Max_order() + 1;//sign qustion_order property to next_order field
                    SaveButton.Visible = true;
                    ViewState["q"] = q;

                    Question_types.Items[2].Selected = true;//set check property of Stars radio button to true


                    break;
                default:
                    Alert("No Selection !");
                    break;
            }
        }

        protected override bool isEmpty(TextBox box)//this method to check if text box is contain default value or not 
        {
            q = (Question)ViewState["q"];
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

        protected override void Save_Click(object sender, EventArgs e)//click event hanlder for save button
        {
            q = (Question)ViewState["q"];

            if (Check(q.Current_values()))//call method check in Base class that check question's values if they are correct or not 
            {

                int Groupbox_index = Question_types.SelectedIndex;//return index of selected control in GroupBox
                if (Groupbox_index == 0 || Groupbox_index == 1 || Groupbox_index == 2)//if no control selected in GroupBox
                {
                    try
                    {
                        DB.Insert(Groupbox_index, Tables, q);//call Insert method in DBclass to insert the new question into database
                        DataTable temp = DB.question_table().Clone();
                        DataRow row = temp.NewRow();

                        row[0] = questionTextbox.Text;
                        row[1] = q.Question_order.ToString();
                        row[2] = Tables[Groupbox_index + 1];
                        temp.Rows.Add(row);


                        GridView1.DataSource = temp;
                        GridView1.DataBind();
                        Alert("Done!!");
                    }
                    catch (Exception ex)
                    {
                        Alert(ex.Message, ex);
                    }
                }
                else
                    Alert("Please select question type ");//if no question type is selected 
            }
            else
            {
                q.Reset_values();
            }
        }


        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (GridView1.Rows.Count == 1)
            {
                SaveButton.Visible = false;
                SaveButton.Enabled = false;
            }
        }

    }
}