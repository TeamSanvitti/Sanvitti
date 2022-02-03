using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Controls
{
    public partial class States : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindStates();
        }
        private void BindStates()
        {
            DataTable dataTable = avii.Classes.clsCompany.GetState(0);

            rptState.DataSource = dataTable;
            rptState.DataBind();
        }
    }
}