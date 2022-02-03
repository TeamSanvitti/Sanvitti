using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace avii.Controls
{
    public partial class ctlItemSummary : System.Web.UI.UserControl
    {
        private bool adminForm = false;
        private string itemCode;
        private DateTime startDate = DateTime.MinValue;
        private DateTime endDate = DateTime.MinValue;
        private int companyID = 0;
        private int userID = 0;

        public bool Administrator
        {
            get
            {
                return adminForm;
            }
            set
            {
                adminForm = value;
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

        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
            }
        }

        public int UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }

        public string ItemCode
        {
            get
            {
                return itemCode;
            }
            set
            {
                itemCode = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateData();
            }
        }

        public void LoadData()
        {
            PopulateData();
        }

        private void PopulateData()
        {
            grdSummary.DataSource = null;
            grdSummary.DataBind();
            avii.Classes.PhoneSummary summ = new avii.Classes.PhoneSummary();
            summ.StartDate = startDate;
            summ.EndDate = endDate;
            summ.ItemCode = itemCode;
            summ.CompanyID = companyID;

            if (chkHistory.Checked)
            {
                summ.AddHistory = true;
            }

            if (adminForm == false)
            {
                summ.UserID = userID;
            }

            System.Collections.Generic.List<avii.Classes.InventorySummary> inventoryList = avii.Classes.PurchaseOrder.GetPhoneSummary(summ);
            if (inventoryList != null && inventoryList.Count > 0)
            {
                chkHistory.Visible = true;
                grdSummary.DataSource = inventoryList;
                grdSummary.DataBind();
            }
            else
            {
                chkHistory.Visible = false;
                lblMsg.Text = "No Inventory assigned, please contact Aerovocie Administrator";
            }
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void chkHistory_CheckedChanged(object sender, EventArgs e)
        {
            PopulateData();
        }
    }
}