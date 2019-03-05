using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
namespace Web.secure
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
           if ( FormsAuthentication.Authenticate(Login1.UserName.ToString(), Login1.Password.ToString()))
            {
                FormsAuthentication.RedirectFromLoginPage(Login1.UserName, false);
            }
           else
            {
                
            }
        }
    }
}