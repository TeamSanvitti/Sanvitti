using System;

namespace avii
{
    public partial class CompanyDisplay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnClose.Attributes.Add("onclick", "window.close();");

        }
    }
}