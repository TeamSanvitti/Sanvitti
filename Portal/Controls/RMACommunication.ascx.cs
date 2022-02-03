using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.RMA;

namespace avii.Controls
{
    public partial class RMACommunication : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindRMAComments(0);
        }


        public void BindRMAComments(int rmaguid)
        {
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            bool exclude = false;
            lblMsg.Text = string.Empty;
            //int rmaguid = 0;
            if (Request.QueryString["rmaGUID"] != null)
                int.TryParse(Request.QueryString["rmaGUID"].ToString(), out rmaguid);
            if (Request.QueryString["ex"] != null)
                exclude = true;

            lblMsg.Text = string.Empty;
            //int rmaguid = 0;
            if (Request.QueryString["rmaGUID"] != null)
                int.TryParse(Request.QueryString["rmaGUID"].ToString(), out rmaguid);
            if (rmaguid > 0)
            {
                List<RMAComments> rmaCommentsList = rmaUtility.GetRMACommentList(rmaguid, exclude);
                if (rmaCommentsList != null && rmaCommentsList.Count > 0)
                {
                    rptComments.DataSource = rmaCommentsList;
                    rptComments.DataBind();


                }
                else
                    lblMsg.Text = "No record found";

            }
        }
    }
}