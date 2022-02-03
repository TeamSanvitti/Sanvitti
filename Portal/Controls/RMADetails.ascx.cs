using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Controls
{
    public partial class RMADetails : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BindRMA(int rmaGUID, bool rmaQuery)
        {
            List<SV.Framework.Models.RMA.RMA> objRmaList = default;
            if (rmaQuery)
            {
                objRmaList = Session["result"] as List<SV.Framework.Models.RMA.RMA>;

                if (objRmaList != null)
                {
                    var rmaInfoList = (from item in objRmaList where item.RmaGUID.Equals(rmaGUID) select item).ToList();
                    if (rmaInfoList != null && rmaInfoList.Count > 0)
                    {
                        lblRMA.Text = rmaInfoList[0].RmaNumber;
                        lblStatus.Text = rmaInfoList[0].Status;
                        lblComment.Text = rmaInfoList[0].Comment;
                        lblAVComment.Text = rmaInfoList[0].AVComments;
                        lblCompanyName.Text = rmaInfoList[0].RMAUserCompany.CompanyName;
                        lblRMADate.Text = Convert.ToDateTime(rmaInfoList[0].RmaDate).ToShortDateString();



                        gvRMA.DataSource = rmaInfoList[0].RmaDetails;
                        gvRMA.DataBind();
                    }

                }
            }
            else
            {
                SV.Framework.Models.RMA.RMA objRma = null;

               // objRma = avii.Classes.RMAUtility.GetNewRMAInfo(rmaGUID, string.Empty, string.Empty, -1, -1, string.Empty);
                if (objRma != null)
                {
                    lblRMA.Text = objRma.RmaNumber;
                    lblStatus.Text = objRma.Status;
                    lblComment.Text = objRma.Comment;
                    lblAVComment.Text = objRma.AVComments;
                    lblCompanyName.Text = objRma.RMAUserCompany.CompanyName;
                    lblRMADate.Text = Convert.ToDateTime(objRma.RmaDate).ToShortDateString();



                    gvRMA.DataSource = objRma.RmaDetails;
                    gvRMA.DataBind();
                }
            }

            
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblreason = (Label)e.Row.FindControl("lblreason");
                HiddenField hdnReason = (HiddenField)e.Row.FindControl("hdnReason");
                try
                {
                    if (hdnReason.Value != null && hdnReason.Value != string.Empty && hdnReason.Value != "0")
                    {
                        Hashtable reasonHashtable = avii.Classes.RMAUtility.getReasonHashtable();

                        lblreason.Text = reasonHashtable[hdnReason.Value].ToString();
                    }
                }
                catch (Exception ex)
                {
                    //lblMsg.Text = ex.Message.ToString();
                }
            }
        }

    }
}