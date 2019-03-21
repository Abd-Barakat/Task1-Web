using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DataBase;
namespace Web
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class NumericUpDown : System.Web.Services.WebService
    {
        private DBclass DataBase;
        private List<int> Reserved_orders;
        /// <summary>
        /// increment question order with exclude reserved orders that already exist in the database.
        /// </summary>
        /// <param name="QuestionOrderUpDown">The question order up down.</param>
        [WebMethod (enableSession:true)]
        public int Up(int current, string tag)
        {
            DataBase = (DBclass)Session["DataBase"];
            current++;
            if (Reserved_orders == null)
            {
                Reserved_orders = DataBase.Orders();
            }
            while (current == -1 || Reserved_orders.Contains(current))
            {
                current++;
            }
            return current;
        }

        /// <summary>
        /// decrement question order with exclude reserved orders that already exist in the database.
        /// </summary>
        /// <param name="QuestionOrderUpDown">The question order up down.</param>
        [WebMethod (enableSession:true) ] 
        public int Down(int current, string tag)
        {
            DataBase = (DBclass)Session["DataBase"];
            current--;
            if (Reserved_orders == null)
            {
                Reserved_orders = DataBase.Orders();
            }
            while (Reserved_orders.Contains(current) && current >= 0)
            {
                current--;
            }
            if (current == -1)
            {
                current = Up(current, tag);
            }
            return current;
        }

    }
}
