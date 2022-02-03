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
    public partial class POStatus : System.Web.UI.UserControl
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
            List<POStatusSummary> poStatusSummaryList = DashboardOperations.GetPOStatusSummary(CompanyID, FromDate, ToDate);

            Session["POStatusSummary"] = poStatusSummaryList;
            if (poStatusSummaryList.Count > 0)
            {
                rptPO.DataSource = poStatusSummaryList;
                lblPO.Text = string.Empty;
            }
            else
            {
                rptPO.DataSource = null;
                lblPO.Text = "No records found";
            }
            rptPO.DataBind();
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
            List<POStatusSummary> poStatusSummaryList = null;
            if (Session["POStatusSummary"] == null)
                poStatusSummaryList = DashboardOperations.GetPOStatusSummary(CompanyID, FromDate, ToDate);
            else
                poStatusSummaryList = (List<POStatusSummary>)Session["POStatusSummary"];


            Session["POStatusSummary"] = poStatusSummaryList;
            if (poStatusSummaryList.Count > 0)
            {
                rptPO.DataSource = poStatusSummaryList;
                lblPO.Text = string.Empty;
            }
            else
            {
                rptPO.DataSource = null;
                lblPO.Text = "No records found";
            }
            rptPO.DataBind();
        }
    }
}