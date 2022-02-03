using System;

namespace avii
{
    public partial class LinkToMobilair : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string url = "~/index.aspx";
                if (Request.UrlReferrer != null)
                    ViewState["url"] = Request.UrlReferrer.ToString();
                else
                    ViewState["url"] = url;

            

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            if (ViewState["url"] != null)
                Response.Redirect(ViewState["url"].ToString());
        }
    }
}