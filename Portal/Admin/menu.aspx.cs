using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace avii.Admin
{
    public partial class menu : System.Web.UI.Page
    {
        public string menuitem;
        protected void Page_Load(object sender, EventArgs e)
        {
            avii.Classes.DBConnect db = new avii.Classes.DBConnect();
            DataTable menutable = db.GetTableRecords("menu","menutable");
            menuitem = menutable.Rows[0]["menu"].ToString();

        }
    }
}
