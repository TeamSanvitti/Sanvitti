using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Controls
{
    public partial class ViewRMA : System.Web.UI.UserControl
    {
        private int rmaGUID = 0;
        public int RmaGUID
        {
            get
            {
                return rmaGUID;
            }
            set
            {
                rmaGUID = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindRMA();
        }
        public void BindRMA()
        {
            lblMsgDetail.Text = string.Empty;
            gvRMADetail.DataSource = null;
            gvRMADetail.DataBind();

            if (RmaGUID > 0)
            {
                List<avii.Classes.RMA> objRmaList = null;
                if (Session["result"] != null)
                {
                    objRmaList = Session["result"] as List<avii.Classes.RMA>;

                    if (objRmaList != null)
                    {
                        var rmaInfoList = (from item in objRmaList where item.RmaGUID.Equals(RmaGUID) select item).ToList();
                        if (rmaInfoList != null && rmaInfoList.Count > 0)
                        {
                            lblRMA.Text = rmaInfoList[0].RmaNumber;
                            lblStatus.Text = rmaInfoList[0].Status;
                            lblComment.Text = rmaInfoList[0].Comment;
                            lblAVComment.Text = rmaInfoList[0].AVComments;
                            lblCompanyName.Text = rmaInfoList[0].RMAUserCompany.CompanyName;
                            lblRMADate.Text = Convert.ToDateTime(rmaInfoList[0].RmaDate).ToShortDateString();



                            List<avii.Classes.RMADetail> rmaDetailList = null;// avii.Classes.RMAUtility.GetRMADetailNew(RmaGUID, -1, string.Empty);
                            if (rmaDetailList != null)
                            {



                                gvRMADetail.DataSource = rmaDetailList;
                                gvRMADetail.DataBind();
                            }
                            else
                            {
                                lblMsgDetail.Text = "No records found";
                                gvRMADetail.DataSource = null;
                                gvRMADetail.DataBind();
                            }
                        }

                    }
                }


            }
        }


        //protected void gvRMADetail_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Label lblreason = (Label)e.Row.FindControl("lblreason");
        //        HiddenField hdnReason = (HiddenField)e.Row.FindControl("hdnReason");
        //        try
        //        {
        //            if (hdnReason.Value != null && hdnReason.Value != string.Empty && hdnReason.Value != "0")
        //            {
        //                //Hashtable reasonHashtable = avii.Classes.RMAUtility.getReasonHashtable();

        //                //lblreason.Text = reasonHashtable[hdnReason.Value].ToString();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //lblMsg.Text = ex.Message.ToString();
        //        }
        //    }
        //}
        protected void imgDeleteRMADetail_Commnad(object sender, CommandEventArgs e)
        {
            int rmadetguid = Convert.ToInt32(e.CommandArgument);
            //int rmaGUID = 0;
            //List<avii.Classes.RMA> objRmaList = null;
            //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            int userID = Convert.ToInt32(Session["UserID"]);
            try
            {
                avii.Classes.RMAUtility.delete_RMA_RMADETAIL(0, rmadetguid, userID);
                //BindRMASearch(false);
                if (ViewState["rmaGUID"] != null)
                    RmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);

                BindRMA();
                //List<avii.Classes.RMADetail> rmaDetailList = (List<avii.Classes.RMADetail>)Session["rmadetaillist"];

                //var EsnList = (from item in rmaDetailList where item.rmaGUID.Equals(rmaGUID) select item).ToList();
                //if (EsnList.Count > 0)
                //{
                //    rptRmaItem.DataSource = EsnList;
                //    rptRmaItem.DataBind();
                //    lblMsgDetail.Text = "Deleted successfully";
                //}`   
                //elsea
                //    lblMsgDetail.Text = "No RMA found";

                //}
                
                
                //Commented on 20 Feb, 2013 as we don't need this
                //int itemIndex = -1;
                //if (Session["itemindex"] != null)
                //    itemIndex = Convert.ToInt32(Session["itemindex"]);
                //List<avii.Classes.RMADetail> rmaDetailList = new List<avii.Classes.RMADetail>();
                //objRmaList = Session["result"] as List<avii.Classes.RMA>;
                //if (objRmaList != null)
                //{
                //    var rmaInfoList = (from item in objRmaList where item.RmaGUID.Equals(rmaGUID) select item).ToList();
                //    if (rmaInfoList != null && rmaInfoList.Count > 0)
                //    {
                //        rmaDetailList = rmaInfoList[0].RmaDetails;
                //        var rmaDetailInfoList = (from item2 in rmaDetailList where item2.rmaGUID.Equals(rmaGUID) select item2).ToList();
                //        //RMADetail d = rmaDetailList.Where(d => d.rmaDetGUID == rmadetguid).Single();
                //        var onlyMatch = rmaDetailInfoList.Single(s => s.rmaDetGUID == rmadetguid);
                //        rmaDetailInfoList.Remove(onlyMatch);
                //        gvRMADetail.DataSource = rmaDetailInfoList;
                //        gvRMADetail.DataBind();
                //        lblRMA.Text = rmaInfoList[0].RmaNumber;
                //        lblStatus.Text = rmaInfoList[0].Status;
                //        lblComment.Text = rmaInfoList[0].Comment;
                //        lblAVComment.Text = rmaInfoList[0].AVComments;
                //        lblCompanyName.Text = rmaInfoList[0].RMAUserCompany.CompanyName;
                //        lblRMADate.Text = Convert.ToDateTime(rmaInfoList[0].RmaDate).ToShortDateString();

                //        if (itemIndex > -1)
                //            objRmaList[itemIndex].RmaDetails = rmaDetailInfoList;
                //        Session["result"] = objRmaList;
                //        lblMsgDetail.Text = "Deleted successfully";

                //        //mdlPopup.Show();
                //    }
                //}

                //gvRMADetail.DataSource = rmaInfoList[0].RmaDetails;
                //gvRMADetail.DataBind();
                //BindRMA(rmaGUID, false);

            }
            catch (Exception ex)
            {
                lblMsgDetail.Text = ex.Message.ToString();
            }

        }
        
    }
}