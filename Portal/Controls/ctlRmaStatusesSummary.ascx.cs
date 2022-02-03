using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Controls
{
    public partial class ctlRmaStatusesSummary : System.Web.UI.UserControl
    {
        private int reasonID = 0;
        private string productName = string.Empty;
        private int timeInterval = 30;
        private string companyAccountNumber = string.Empty;
        private string companyName = string.Empty;
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
        public string CompanyAccountNumber
        {
            get
            {
                return companyAccountNumber;
            }
            set
            {
                companyAccountNumber = value;
            }
        }
        public int ReasonID
        {
            get
            {
                return reasonID;
            }
            set
            {
                reasonID = value;
            }
        }
        public string ProductName
        {
            get
            {
                return productName;
            }
            set
            {
                productName = value;
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
                if (!string.IsNullOrEmpty(ProductName))
                {
                    BindRmaStatus(ReasonID, ProductName, CompanyAccountNumber, TimeInterval);
                }
            }
        }
        public void BindRmaStatus()
        {
            List<avii.Classes.RmaStatus> rmaSummaryList = avii.Classes.ReportOperations.GetRmaStatusesSummary(ReasonID, ProductName, CompanyAccountNumber, TimeInterval);
            if (rmaSummaryList != null && rmaSummaryList.Count > 0)
            {
                rptRmaStatus.DataSource = rmaSummaryList;
                rptRmaStatus.DataBind();
            }
            else
            {
                rptRmaStatus.DataSource = null;
                rptRmaStatus.DataBind();
            }

        }
        public void BindRmaStatus(int reasonID, string productsName, string companyAccountNumber, int timeIntervals)
        {
            lblRma.Text = string.Empty;
            if (!string.IsNullOrEmpty(CompanyName))
            {
                lblCustName.Text = CompanyName;
                lblCust.Visible = true;
            }
            else
            {
                lblCustName.Text = string.Empty;
                lblCust.Visible = false;

            }
            lblProduct.Text = productsName;
            if (timeIntervals == 1)
                lblDate.Text = DateTime.Now.ToShortDateString();
            else
            {
                lblDate.Text = "From: " + DateTime.Now.AddDays(-timeIntervals).ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
            }
                
            List<avii.Classes.RmaStatus> rmaSummaryList = avii.Classes.ReportOperations.GetRmaStatusesSummary(reasonID, productsName, companyAccountNumber, timeIntervals);
            if (rmaSummaryList != null && rmaSummaryList.Count > 0)
            {
                rptRmaStatus.DataSource = rmaSummaryList;
                rptRmaStatus.DataBind();
                

                
            }
            else
            {
                rptRmaStatus.DataSource = null;
                rptRmaStatus.DataBind();
                lblRma.Text = "No records found";
            }

        }
    }
}