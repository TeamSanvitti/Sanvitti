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
    public partial class AssignedEsnList : System.Web.UI.UserControl
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

            //if (chkHistory.Checked)
            //{
            //    summ.AddHistory = true;
            //}

            if (adminForm == false)
            {
                summ.UserID = userID;
            }

            DataTable dataTable = avii.Classes.PurchaseOrder.GetAssignedEsnList(summ);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                //chkHistory.Visible = true;
                //Session["esnsummary"] = dataTable;
                grdSummary.DataSource = dataTable;
                grdSummary.DataBind();
            }
            else
            {
                lblMsg.Text = "No record exists";//"No Inventory assigned, please contact Aerovocie Administrator";
            }
        }
        protected void grdSummary_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdSummary.EditIndex = e.NewEditIndex;
        }
        protected void grdSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSummary.PageIndex = e.NewPageIndex;
            if (Session["esnsummary"] != null)
            {
                DataTable dataTable = (DataTable)Session["esnsummary"];
                grdSummary.DataSource = dataTable;
                grdSummary.DataBind();
            }
            else
                PopulateData();

        }

    }
}