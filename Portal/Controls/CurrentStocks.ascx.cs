using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
using System.Drawing;

namespace avii.Controls
{
    public partial class CurrentStocks : System.Web.UI.UserControl
    {
        private bool isKitted = false;
        public bool IsKitted
        {
            get
            {
                return isKitted;
            }
            set
            {
                isKitted = value;
            }
        }
        private bool isDisable = false;
        public bool IsDisable
        {
            get
            {
                return isDisable;
            }
            set
            {
                isDisable = value;
            }
        }
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
        private string sku = "";
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
            List<CurrentStock> stockList = StockOperation.GetCurrentStock(CompanyID, SKU, IsDisable, IsKitted);

            Session["stockList"] = stockList;
            if (stockList.Count > 0)
            {
                gvcSKU.DataSource = stockList;
                lblStock.Text = string.Empty;
            }
            else
            {
                gvcSKU.DataSource = null;
                lblStock.Text = "No records found";
            }
            gvcSKU.DataBind();
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
            List<CurrentStock> stockList = null;
            if (Session["stockList"] == null)
                stockList = StockOperation.GetCurrentStock(CompanyID, SKU, IsDisable, IsKitted);
            else
                stockList = (List<CurrentStock>)Session["stockList"];

            Session["stockList"] = stockList;
            if (stockList.Count > 0)
            {
                gvcSKU.DataSource = stockList;
                lblStock.Text = string.Empty;
            }
            else
            {
                lblSKUCount.Text = string.Empty;
                gvcSKU.DataSource = null;
                Session["stockList"] = null;
                lblStock.Text = "No records found";
            }
            gvcSKU.DataBind();
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvcSKU.PageIndex = e.NewPageIndex;
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
        public List<CurrentStock> Sort<TKey>(List<CurrentStock> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<CurrentStock>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<CurrentStock>();
            }
        }
        protected void gvcSKU_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["stockList"] != null)
            {
                List<CurrentStock> stocks = (List<CurrentStock>)Session["stockList"];

                if (stocks != null && stocks.Count > 0)
                {
                    var list = stocks;
                    if (Sortdir == "ASC")
                    {
                        list = Sort<CurrentStock>(list, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        list = Sort<CurrentStock>(list, SortExp, SortDirection.Descending);
                    }
                    Session["stockList"] = stocks;
                    gvcSKU.DataSource = list;
                    gvcSKU.DataBind();
                }
            }
        }

        protected void gvcSKU_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           // e.Row.Attributes.Add("style", "cursor:help;");
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Alternate)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.CssStyle.Value = "background-color: Gainsboro; color: Black";
                    HiddenField hdIsDisable = e.Row.FindControl("hdIsDisable") as HiddenField;
                    if (hdIsDisable != null && Convert.ToBoolean(hdIsDisable.Value))
                    {
                        e.Row.Attributes.CssStyle.Value = "background-color: #808080; forecolor: White";

                    }

                    // e.Row.CssClass.Add("onmouseover", "this.style.backgroundColor='orange'");
                    // e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E56E94'");
                    // e.Row.BackColor = Color.FromName("#E56E94");
                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.CssStyle.Value = "background-color: White; color: Black";

                    HiddenField hdIsDisable = e.Row.FindControl("hdIsDisable") as HiddenField;
                    if (hdIsDisable != null && Convert.ToBoolean(hdIsDisable.Value))
                    {
                        e.Row.Attributes.CssStyle.Value = "background-color: #808080; forecolor: White";

                    }
                    //e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='orange'");
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='gray'");
                    //e.Row.BackColor = Color.FromName("gray");
                }
            }
        }
    }
}