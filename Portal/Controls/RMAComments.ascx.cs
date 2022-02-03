using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
namespace Sanvitti1.Controls.Aarons
{
    public partial class RMAComment : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
                BindRMAComments(0, false);
        }


        public void BindRMAComments(int rmaguid, bool exclude)
        {
            lblMsg.Text = string.Empty;
            //int rmaguid = 0;
            if (Request.QueryString["rmaGUID"] != null)
                int.TryParse(Request.QueryString["rmaGUID"].ToString(), out rmaguid);

            if (rmaguid > 0)
            {
                List<avii.Classes.RMAComments> rmaCommentsList = avii.Classes.RMAUtility.GetRMACommentList(rmaguid, exclude);
                if (rmaCommentsList != null && rmaCommentsList.Count > 0)
                {
                    rptComments.DataSource = rmaCommentsList;
                    rptComments.DataBind();


                }
                else
                    lblMsg.Text = "No record found";

            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            int rmaGUID = 0;
            if (Request.QueryString["rmaGUID"] != null)
                int.TryParse(Request.QueryString["rmaGUID"].ToString(), out rmaGUID);

            List<RMAComments> rmaCommentList = new List<RMAComments>();
            RMAComments rmaComment = null;
            int commentID = 0;
            foreach (RepeaterItem item in rptComments.Items)
            {
                rmaComment = new RMAComments();
                HiddenField hdnCommentID = (HiddenField)item.FindControl("hdnCommentID");

                CheckBox chkEx = (CheckBox)item.FindControl("chkEx");
                if (chkEx != null && chkEx.Checked && hdnCommentID != null)
                {
                    int.TryParse(hdnCommentID.Value, out commentID);
                    rmaComment.CommentID = commentID;
                    rmaCommentList.Add(rmaComment);
                }
            }

            RMAUtility.ExcludeInclude_RMAComments(rmaCommentList, rmaGUID);

            lblMsg.Text = "Updated successfully";


        }

    }
}