using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using avii.Classes;
namespace avii.Controls
{
    public partial class RMAStatus : System.Web.UI.UserControl
    {
        private int timeInterval = 30;
        private int companyID = 0;
        private string fromDate = "";
        private string toDate = "";
        public string ToDate
        {
            get
            {
                return toDate;
            }
            set
            {
                toDate = value;
            }
        }
        public string FromDate
        {
            get
            {
                return fromDate;
            }
            set
            {
                fromDate = value;
            }
        }
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

        private void PopulateData()
        {
            

            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    CompanyID = userInfo.CompanyGUID;

                }

            }
            List<RMAStatusSummary> rmaStatusSummaryList = DashboardOperations.GetRMAStatusSummary(CompanyID, FromDate, ToDate);
            
            
            if (rmaStatusSummaryList.Count > 0)
            {
                Session["RMAStatusSummary"] = rmaStatusSummaryList;
                rptRMA.DataSource = rmaStatusSummaryList;
                lblRMA.Text = string.Empty;
            }
            else
            {
                Session["RMAStatusSummary"] = null;
                rptRMA.DataSource = null;
                lblRMA.Text = "No records found";
            }

            rptRMA.DataBind();
        }
        public void ReloadData()
        {


            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    CompanyID = userInfo.CompanyGUID;

                }

            }
            List<RMAStatusSummary> rmaStatusSummaryList = null;
            if (Session["RMAStatusSummary"] == null)
                rmaStatusSummaryList = DashboardOperations.GetRMAStatusSummary(CompanyID, FromDate, ToDate);
            else
                rmaStatusSummaryList = (List<RMAStatusSummary>)Session["RMAStatusSummary"];

            Session["RMAStatusSummary"] = rmaStatusSummaryList;

            if (rmaStatusSummaryList.Count > 0)
            {
                rptRMA.DataSource = rmaStatusSummaryList;
                lblRMA.Text = string.Empty;
            }
            else
            {
                rptRMA.DataSource = null;
                lblRMA.Text = "No records found";
            }

            rptRMA.DataBind();
        }
    }
}