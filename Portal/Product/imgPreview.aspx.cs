using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace avii.product
{
    public partial class imgPreview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request["url"];
            if (url != null && url != string.Empty)
            {
                imagePreview.ImageUrl = url;
                lblMsg.Visible = false;
            }
            else
            {
                imagePreview.Visible = false;
                lblMsg.Visible = true;
            }
        }
    }
}
