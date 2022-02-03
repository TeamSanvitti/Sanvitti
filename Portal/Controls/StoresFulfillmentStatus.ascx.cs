using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Fulfillment;

namespace avii.Controls
{
    public partial class StoresFulfillmentStatus : System.Web.UI.UserControl
    {
        private string companyName = string.Empty;
        
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
        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                companyName = value;
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
            SV.Framework.Fulfillment.FulfillmentReportOperation fulfillmentReportOperation = SV.Framework.Fulfillment.FulfillmentReportOperation.CreateInstance<SV.Framework.Fulfillment.FulfillmentReportOperation>();

            lblCustomer.Text = CompanyName;
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
                gvPOs.DataSource = null;
                lblPOs.Text = string.Empty;
                Session["stPOStatus"] = null;
            }
            else
            {
                List<StoreFulfillmentStatus> poStatusSummaryList = fulfillmentReportOperation.GetCustomerStoreFulfillmentStatusReport(CompanyID, CompanyName, TimeInterval, 0);

                Session["stPOStatus"] = poStatusSummaryList;
                if (poStatusSummaryList.Count > 0)
                {
                    gvPOs.DataSource = poStatusSummaryList;
                    lblCount.Text = "<strong>Total Stores:</strong> " + Convert.ToString(poStatusSummaryList.Count - 1);
                    lblPOs.Text = string.Empty;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    gvPOs.DataSource = null;
                    lblPOs.Text = "No records found";
                }
            }
            gvPOs.DataBind();
        }
        public void ReloadData()
        {
            lblCustomer.Text = CompanyName;
            //if (Session["adm"] == null)
            //{
            //    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            //    if (userInfo != null)
            //    {
            //        CompanyID = userInfo.CompanyGUID;

            //    }

            //}
            List<StoreFulfillmentStatus> poStatusSummaryList = null;
            if (Session["stPOStatus"] != null)
            {
                //    poStatusSummaryList = ReportOperations.GetCustomerFulfillmentStatusReport(CompanyID, TimeInterval);
                //else
                poStatusSummaryList = (List<StoreFulfillmentStatus>)Session["stPOStatus"];


                Session["stPOStatus"] = poStatusSummaryList;
                if (poStatusSummaryList.Count > 0)
                {
                    lblCount.Text = "<strong>Total Stores:</strong> " + Convert.ToString(poStatusSummaryList.Count - 1);
                    gvPOs.DataSource = poStatusSummaryList;
                    lblPOs.Text = string.Empty;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    gvPOs.DataSource = null;
                    lblPOs.Text = "No records found";
                }
            }
            else
            {
                lblCount.Text = string.Empty;
                gvPOs.DataSource = null;
                lblPOs.Text = string.Empty;

            }
            gvPOs.DataBind();
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPOs.PageIndex = e.NewPageIndex;
            ReloadData();

        }

        
    }
}