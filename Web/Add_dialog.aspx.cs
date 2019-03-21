using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Questions;
using System.IO;
using System.Data;
using System.Threading;
namespace Web
{
    public partial class Add_dialog : System.Web.UI.Page
    {
        Master_Page master ;
        /// <summary>
        /// Handles the Init event of the Page control that show headers of gridview
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            Master_Page master = (Master_Page)Page.Master;
            ViewState["Master"] = master;
        }

        /// <summary>
        /// Releases the specified q.
        /// </summary>
        /// <param name="q">The q.</param>
        protected void Release(Question q)
        {
            if (q != null)
            {
                q = null;
            }
        }
       
        /// <summary>
        /// Shows the controls depending on question type
        /// </summary>
        /// <param name="QuestionType">Type of the question.</param>
        private void Show_controls (int QuestionType)
        {
            master.Set_Placeholders();
            master.QuestionPanel.Visible = true;
            master.StartTextbox.Visible = true;
            switch (QuestionType)
            {
                case 0:
                    master.StartTextbox.ToolTip = "Start";
                    master.EndTextbox.Visible = true;
                    master.End_captionTextbox.Visible = true;
                    master.Start_captionTextbox.Visible = true;
                    break;
                case 1:
                    master.StartTextbox.ToolTip = "Smiles";
                    master.EndTextbox.Visible = false;
                    master.End_captionTextbox.Visible = false;
                    master.Start_captionTextbox.Visible = false;
                    break;
                case 2:
                    master.StartTextbox.ToolTip = "Stars";
                    master.EndTextbox.Visible = false;
                    master.End_captionTextbox.Visible = false;
                    master.Start_captionTextbox.Visible = false;
                    break;
            }
        }
       
        /// <summary>
        /// Handles the Click event of the Save control that save current question in database
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected  void Save_Click(object sender, EventArgs e)//click event hanlder for save button
        {
            master =(Master_Page) ViewState["Master"];
            master.q = (Question)ViewState["q"];
            if (master.Check(master.q.Current_values()))//call method check in Base class that check question's values if they are correct or not 
            {
                int Groupbox_index = QuestionType.SelectedIndex;//return index of selected control in GroupBox
                if (Groupbox_index == 0 || Groupbox_index == 1 || Groupbox_index == 2)//if no control selected in GroupBox
                {
                    try
                    {
                        master.DB.Insert(master.q);//call Insert method in DBclass to insert the new question into database
                        Hide_controls(master.q);
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Key", "RefreshParent();", true);
                        master.Alert("Done!!");
                        ScriptManager.RegisterClientScriptBlock(this, GetType(),"KEY", "setTimeout(function(){Close();},2000);", true);
                    }
                    catch (Exception ex)
                    {
                        master.Alert(ex.Message, ex);
                    }
                }
                else
                    master.Alert("Please select question type ");//if no question type is selected 
            }
            else
            {
                master.q.Set_values(master.q.Default_values());
            }
        }
        /// <summary>
        /// Hides the controls depending on question type
        /// </summary>
        /// <param name="q">The q.</param>
        private void Hide_controls(Question q)
        {
            QuestionType.Enabled = false;
            SaveButton.Visible = false;
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
        /// Handles the SelectedIndexChanged event of the Question_types control that create question object depending on selection of radio buttons
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void QuestionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            master =(Master_Page) ViewState["Master"];
            int id = (int)Session["Next_Id"];
            Release(master.q);//call method release that release  q object if refere to another object 
            switch (QuestionType.SelectedIndex)
            {
                case 0://if slider radio button 
                    master.q = new Slider(id);//create slider object
                    ViewState["q"] = master.q;
                    Show_controls(0);
                    break;
                case 1://if Smiley radio button 
                    master.q = new Smiley(id);//create smiley object
                    ViewState["q"] = master.q;
                    Show_controls(1);
                    break;
                case 2://if Stars radio button 
                    master.q = new Stars(id);//create star object
                    ViewState["q"] = master.q;
                    Show_controls(2);
                    break;
                default:
                    master.Alert("No Selection !");
                    break;
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Page.Dispose();
        }
    }
}