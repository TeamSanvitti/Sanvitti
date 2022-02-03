using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Controls
{
    public partial class FuolfillmentOrderSummary : System.Web.UI.UserControl
    {
        //private int timeInterval = 30;
        private int companyID = 0;
        private string sku = string.Empty;
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
        //public int TimeInterval
        //{
        //    get
        //    {
        //        return timeInterval;
        //    }
        //    set
        //    {
        //        timeInterval = value;
        //    }
        //}
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
            string sortExpression = "SKU";
            string sortDirection = "ASC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;

            // int isAdmin = 1;
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    CompanyID = userInfo.CompanyGUID;
                    //isAdmin = 0;
                }

            }

            List<AssignedSKUSummary> assignedSKUsList = DashboardOperations.GetAssignedSKUSummary(CompanyID, FromDate, ToDate, SKU);

            if (assignedSKUsList.Count > 0)
            {
                lblCount.Text = "<strong>Total count:</strong> " + assignedSKUsList.Count.ToString();
                gvSKU.DataSource = assignedSKUsList;
                Session["SKU"] = assignedSKUsList;
                lblSKU.Text = string.Empty;
            }
            else
            {
                lblCount.Text = string.Empty;
                gvSKU.DataSource = null;
                Session["SKU"] = null;
                lblSKU.Text = "No records found";
            }
            gvSKU.DataBind();
        }
        private void ReloadData()
        {
            if (Session["SKU"] != null)
            {
                List<AssignedSKUSummary> assignedSKUsList = (List<AssignedSKUSummary>)Session["SKU"];
                gvSKU.DataSource = assignedSKUsList;
                gvSKU.DataBind();
                lblCount.Text = "<strong>Total count:</strong> " + assignedSKUsList.Count.ToString();

            }
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSKU.PageIndex = e.NewPageIndex;
            ReloadData();

        }
        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }
        public List<AssignedSKUSummary> Sort<TKey>(List<AssignedSKUSummary> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<AssignedSKUSummary>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<AssignedSKUSummary>();
            }
        }
        protected void gvSKU_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["SKU"] != null)
            {
                List<AssignedSKUSummary> assignedSKUsList = (List<AssignedSKUSummary>)Session["SKU"];

                if (assignedSKUsList != null && assignedSKUsList.Count > 0)
                {
                    var list = assignedSKUsList;
                    if (Sortdir == "ASC")
                    {
                        list = Sort<AssignedSKUSummary>(list, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        list = Sort<AssignedSKUSummary>(list, SortExp, SortDirection.Descending);
                    }
                    Session["SKU"] = list;
                    gvSKU.DataSource = list;
                    gvSKU.DataBind();
                }
            }
        }
    }
}