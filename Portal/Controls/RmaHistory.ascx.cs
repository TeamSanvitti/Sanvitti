using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Controls
{
    public partial class RmaHistory : System.Web.UI.UserControl
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
        //public UpdatePanelUpdateMode UpdateMode
        //{
        //    get { return this.upnHistory.UpdateMode; }
        //    set { this.upnHistory.UpdateMode = value; }
        //}

        //public void Update()
        //{
        //    this.upnHistory.Update();
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRmaHistory();
            }
        }
        public void BindRmaHistory()
        {
            //int RmaGUID = 0;
            if (Request["rmaGUID"] != null)
                RmaGUID = Convert.ToInt32(Request["rmaGUID"]);
            //else

                lblHistory.Text = string.Empty;

            List<avii.Classes.RMA> rmaList = new List<Classes.RMA>();// avii.Classes.RMAUtility.GetRMAStatusReport(RmaGUID);
                Session["rmaList"] = rmaList;
                if (rmaList.Count > 0)
                {
                    rptRma.DataSource = rmaList;
                    rptRma.DataBind();
                    lblRMANum.Text = rmaList[0].RmaNumber;
                    //mdlPopup2.Show();
                }
                else
                {
                    lblHistory.Text = "No RMA history exists for this RMA";
                    rptRma.DataSource = null;
                    rptRma.DataBind();
                    lblRMANum.Text = string.Empty;
                    // mdlPopup2.Show();

                }
            
        }
    }
}