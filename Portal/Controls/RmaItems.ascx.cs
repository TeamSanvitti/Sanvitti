using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Controls
{
    public partial class RmaItems : System.Web.UI.UserControl
    {
        private string rmaNumber;
        public string RmaNumber
        {
            get
            {
                return rmaNumber;
            }
            set
            {
                rmaNumber = value;
            }
        }
        private bool rmaTemp;
        public bool RmaTemp
        {
            get
            {
                return rmaTemp;
            }
            set
            {
                rmaTemp = value;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BindRMA(string rmaNumber, bool rmaTemp)
        {
            lblMsgDetail.Text = string.Empty;
            lblCount.Text = string.Empty;
            if (!string.IsNullOrEmpty(rmaNumber))
            {
                List<avii.Classes.RMA> objRmaList = null;
                if (Session["rmalist"] != null)
                {
                    objRmaList = Session["rmalist"] as List<avii.Classes.RMA>;

                    if (objRmaList != null)
                    {
                        var rmaInfoList = objRmaList;// rmaTemp ==true ? (from item in objRmaList where item.RmaNumber.Equals(rmaNumber) select item).ToList() : (from item in objRmaList where item.TempRmaNumber.Equals(rmaNumber) select item).ToList();
                        if (rmaInfoList != null && rmaInfoList.Count > 0)
                        {
                            lblRMA.Text = rmaInfoList[0].RmaNumber;
                            lblStatus.Text = rmaInfoList[0].Status;
                            lblComment.Text = rmaInfoList[0].Comment;
                            lblAVComment.Text = rmaInfoList[0].AVComments;
                            lblCompanyName.Text = rmaInfoList[0].RMAUserCompany.CompanyName;
                            lblRMADate.Text = Convert.ToDateTime(rmaInfoList[0].RmaDate).ToShortDateString();



                            List<avii.Classes.RMADetail> rmaDetailList = null; // rmaInfoList[0].RmaDetails; //avii.Classes.RMAUtility.GetRMADetailNew(rmaGUID, -1, string.Empty);
                            if (rmaTemp)
                                rmaDetailList = rmaInfoList[0].RmaDetails;
                            else
                            {
                              //  rmaDetailList = avii.Classes.RmaOperations.GetRMADetail(0, -1, string.Empty, rmaNumber);
                            }
                            if (rmaDetailList != null)
                            {



                                gvRMADetail.DataSource = rmaDetailList;
                                gvRMADetail.DataBind();
                                lblCount.Text = "ESN Count: " + rmaDetailList.Count;
                            }
                            else
                            {
                                lblMsgDetail.Text = "No records found";
                                gvRMADetail.DataSource = rmaDetailList;
                                gvRMADetail.DataBind();
                            }
                        }

                    }
                }
            }
            
        }

    }
}