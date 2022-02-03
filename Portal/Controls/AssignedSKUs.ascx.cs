using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using avii.Classes;
namespace avii.Controls
{
    public partial class AssignedSKUs : System.Web.UI.UserControl
    {
        private int timeInterval = 30;
        private int companyID = 0;
        private string sku = string.Empty;
        public string SKU
        {
            get
            {
                return sku;
            }
            set
            {
                sku = value;
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
                ReloadData();
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
            List<AssignedSKUsSummary> assignedSKUsList = DashboardOperations.GetAssignedSKUsSummary(CompanyID, TimeInterval, SKU);
            
            if (assignedSKUsList.Count > 0)
            {
                gvSKU.DataSource = assignedSKUsList;
                Session["SKUs"] = assignedSKUsList;
                lblSKU.Text = string.Empty;
            }
            else
            {
                gvSKU.DataSource = null;
                Session["SKUs"] = null;
                lblSKU.Text = "No records found";
            }
            gvSKU.DataBind();
        }
        private void ReloadData()
        {
            if (Session["SKUs"] != null)
            {
                List<AssignedSKUsSummary> assignedSKUsList = (List<AssignedSKUsSummary>)Session["SKUs"];
                gvSKU.DataSource = assignedSKUsList;
                gvSKU.DataBind();
                
            }
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSKU.PageIndex = e.NewPageIndex;
            ReloadData();
            
        }
    }
}