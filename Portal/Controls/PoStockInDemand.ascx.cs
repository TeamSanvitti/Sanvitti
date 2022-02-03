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
    public partial class PoStockInDemand : System.Web.UI.UserControl
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
            List<StockInDemand> stockList = StockInDemandOperation.GetStockInDemandList(CompanyID, SKU, FromDate, ToDate);

            Session["skuindemand"] = stockList;
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
            List<StockInDemand> stockList = null;
            if (Session["skuindemand"] == null)
                stockList = StockInDemandOperation.GetStockInDemandList(CompanyID, SKU, FromDate, ToDate);
            else
                stockList = (List<StockInDemand>)Session["skuindemand"];

            Session["skuindemand"] = stockList;
            if (stockList.Count > 0)
            {
                gvSKUs.DataSource = stockList;
                lblSkuMsg.Text = string.Empty;
            }
            else
            {
                lblSKUCount.Text = string.Empty;
                gvSKUs.DataSource = stockList;
                Session["skuindemand"] = null;
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
        public List<StockInDemand> Sort<TKey>(List<StockInDemand> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<StockInDemand>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<StockInDemand>();
            }
        }
        protected void gvSKUs_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["skuindemand"] != null)
            {
                List<StockInDemand> stocks = (List<StockInDemand>)Session["skuindemand"];

                if (stocks != null && stocks.Count > 0)
                {
                    var list = stocks;
                    if (Sortdir == "ASC")
                    {
                        list = Sort<StockInDemand>(list, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        list = Sort<StockInDemand>(list, SortExp, SortDirection.Descending);
                    }
                    Session["skuindemand"] = list;
                    gvSKUs.DataSource = list;
                    gvSKUs.DataBind();
                }
            }
        }

        protected void lnkPO_Command(object sender, CommandEventArgs e)
        {
            string sku = Convert.ToString(e.CommandArgument);
            if(!string.IsNullOrWhiteSpace(sku))
            {
                Session["sku"] = sku;
                //Response.Redirect("~/PoQueryNew.aspx");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('../PoQueryNew.aspx')</script>", false);


            }
        }
        protected void lnkESN_Command(object sender, CommandEventArgs e)
        {
            string sku = Convert.ToString(e.CommandArgument);
            string[] array = sku.Split(',');
            if (array.Length > 1)
            {
                if (!string.IsNullOrWhiteSpace(array[0]))
                {
                    Session["skuid"] = array[0];
                    Session["sidcompnyid"] = array[1];
                    // Response.Redirect("~/PoQueryNew.aspx");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('../Reports/CustomerEsnRepositoryDetail.aspx')</script>", false);
                }
            }
        }
    }
}