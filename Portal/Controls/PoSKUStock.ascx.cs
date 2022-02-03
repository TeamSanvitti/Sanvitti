using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
namespace avii.Controls
{
    public partial class PoSKUStock : System.Web.UI.UserControl
    {
        private int companyID = 0;
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

        public void PopulateData()
        {
            string sortExpression = "SKU";
            string sortDirection = "ASC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;

            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    CompanyID = userInfo.CompanyGUID;
                }
            }
            List<POSKUStock> stockList = DashboardOperations.GetSKUStock(CompanyID);

            Session["skustock"] = stockList;
            if (stockList.Count > 0)
            {
                gvSKUs.DataSource = stockList;
                lblSkuMsg.Text = string.Empty;
            }
            else
            {
                gvSKUs.DataSource = stockList;
                lblSkuMsg.Text = "No records found";
            }

            gvSKUs.DataBind();
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
            List<POSKUStock> stockList = null;
            if (Session["skustock"] == null)
                stockList = DashboardOperations.GetSKUStock(CompanyID);
            else
                stockList = (List<POSKUStock>)Session["skustock"];

            Session["skustock"] = stockList;
            if (stockList.Count > 0)
            {
                gvSKUs.DataSource = stockList;
                lblSkuMsg.Text = string.Empty;
            }
            else
            {
                lblSKUCount.Text = string.Empty;
                gvSKUs.DataSource = stockList;
                Session["skustock"] = null;
                lblSkuMsg.Text = "No records found";
            }
            gvSKUs.DataBind();
        }
        protected void gvSKUs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSKUs.PageIndex = e.NewPageIndex;
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
        public List<POSKUStock> Sort<TKey>(List<POSKUStock> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<POSKUStock>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<POSKUStock>();
            }
        }
        protected void gvSKUs_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["skustock"] != null)
            {
                List<POSKUStock> stocks = (List<POSKUStock>)Session["skustock"];

                if (stocks != null && stocks.Count > 0)
                {
                    var list = stocks;
                    if (Sortdir == "ASC")
                    {
                        list = Sort<POSKUStock>(list, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        list = Sort<POSKUStock>(list, SortExp, SortDirection.Descending);
                    }
                    Session["skustock"] = list;
                    gvSKUs.DataSource = list;
                    gvSKUs.DataBind();
                }
            }
        }
    }
}