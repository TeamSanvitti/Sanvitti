using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Controls
{
    public partial class FulfillmentStatuses : System.Web.UI.UserControl
    {
        private int timeInterval = 30;
        private int companyID = 0;
        public int TimeInterval
        {
            get
            {
                return timeInterval;
            }
            set
            {
                timeInterval = value;
            }
        }
        public int CompanyID
        {
            get
            {
                return companyID;
            }
            set
            {
                companyID = value;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                PopulateData();
            }
            else
            {
                ReloadData();
            }
        }
        public void LoadData()
        {
            PopulateData();
        }

        public void PopulateData()
        {
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    CompanyID = userInfo.CompanyGUID;

                }

            }
            if (TimeInterval == 0)
            {
                lblCount.Text = string.Empty;
                gvPO.DataSource = null;
                lblPO.Text = string.Empty;
                Session["POStatus"] = null;
            }
            else
            {
                List<FulfillmentStatusReport> poStatusSummaryList = ReportOperations.GetCustomerFulfillmentStatusReport(CompanyID, TimeInterval, 0);

                Session["POStatus"] = poStatusSummaryList;
                if (poStatusSummaryList.Count > 0)
                {
                    gvPO.DataSource = poStatusSummaryList;
                    lblCount.Text = "<strong>Total Customers:</strong> " + Convert.ToString(poStatusSummaryList.Count - 1);
                    lblPO.Text = string.Empty;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    gvPO.DataSource = null;
                    lblPO.Text = "No records found";
                }
            }
            gvPO.DataBind();
        }
        public void ReloadData()
        {
            //if (Session["adm"] == null)
            //{
            //    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            //    if (userInfo != null)
            //    {
            //        CompanyID = userInfo.CompanyGUID;

            //    }

            //}
            List<FulfillmentStatusReport> poStatusSummaryList = null;
            if (Session["POStatus"] != null)
            {
                //    poStatusSummaryList = ReportOperations.GetCustomerFulfillmentStatusReport(CompanyID, TimeInterval);
                //else
                poStatusSummaryList = (List<FulfillmentStatusReport>)Session["POStatus"];


                Session["POStatus"] = poStatusSummaryList;
                if (poStatusSummaryList.Count > 0)
                {
                    lblCount.Text = "<strong>Total Customers:</strong> " + Convert.ToString(poStatusSummaryList.Count - 1);
                    gvPO.DataSource = poStatusSummaryList;
                    lblPO.Text = string.Empty;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    gvPO.DataSource = null;
                    lblPO.Text = "No records found";
                }
            }
            else
            {
                lblCount.Text = string.Empty;
                gvPO.DataSource = null;
                lblPO.Text = string.Empty;
         
            }
            gvPO.DataBind();
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPO.PageIndex = e.NewPageIndex;
            ReloadData();

        }
    }
}