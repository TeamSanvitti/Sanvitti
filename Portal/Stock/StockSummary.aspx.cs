using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace avii.Stock
{
    public partial class StockSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    url = ConfigurationSettings.AppSettings["LogonPage"].ToString();

                }
                catch
                {
                    url = "/avii/logon.aspx";
                }
                if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if (!IsPostBack)
            {
                if (Session["adm"] == null)
                {
                    pnlCust.Visible = false;
                    //btnRefresh.Visible = true;
                }
                else
                {
                    BindCustomer();
                    pnlCust.Visible = true;
                    //btnRefresh.Visible = false;
                }
                //BindCustomer();
                txtStockDate.Text =Convert.ToDateTime(DateTime.Now.AddDays(-1).ToShortDateString()).ToString("MM/dd/yyyy");
                txtToDate.Text = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM/dd/yyyy");
            }
        }
        private void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindStockSummary();
        }
        private void BindStockSummary()
        {
            SV.Framework.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.Inventory.InventoryReportOperation>();

            //btnSummary.Visible = false;
            //btnDownload.Visible = false;
            bool includeDisabledSKU = false;
            lblNote.Visible = false;
            btnDownload.Visible = false;
            //lblNote.Visible = false;
            lblMsg.Text = "";
            lblCount.Text = "";
            int companyID = 0;
            string stockDateFrom = txtStockDate.Text.Trim();
            string stockDateTo = txtToDate.Text.Trim();
            string sku = txtSKU.Text.Trim();
            string sortExpression = "SKU";
            string sortDirection = "ASC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;
            if (chkActive.Checked)
                includeDisabledSKU = chkActive.Checked;

            if (Session["adm"] != null)
            {
                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                else
                {
                    lblMsg.Text = "Customer is required!";
                    return;
                }
            }
            else
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    companyID = userInfo.CompanyGUID;
                }
            }

            List<StockCount> stockSummary = inventoryReportOperation.GetStockCountSummary(companyID, stockDateFrom, stockDateTo, sku, includeDisabledSKU);
            if (stockSummary != null && stockSummary.Count > 0)
            {
                gvStocks.DataSource = stockSummary;
                gvStocks.DataBind();
                Session["stockSummary"] = stockSummary;
                int skuCount = 0;
                var skulist = stockSummary.GroupBy(e => new { e.SKU }).ToList();
                if (skulist != null && skulist.Count > 0)
                    skuCount = skulist.Count;
                lblNote.Visible = true;
                
                btnDownload.Visible = true;
                var MinDate = (from d in stockSummary select d.StockDate).Min();

                //Retrieve Maximum Date
                var MaxDate = (from d in stockSummary select d.StockDate).Max();
                lblCount.Text = "<strong>Date Range:</strong> " + MinDate.ToString("MM-dd-yyyy") + " - " + MaxDate.ToString("MM-dd-yyyy") + " &nbsp;&nbsp;&nbsp; <strong>Total records:</strong> " + stockSummary.Count + " &nbsp;&nbsp;&nbsp; <strong>Total SKU(s): </strong> " + skuCount;
            }
            else
            {
                gvStocks.DataSource = null;
                gvStocks.DataBind();
                Session["stockSummary"] = null;
                lblMsg.Text = "No record found";
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Session["adm"] != null)
            {
                dpCompany.SelectedIndex = 0;
            }
            txtStockDate.Text = string.Empty;//DateTime.Now.ToShortDateString();
            txtToDate.Text = string.Empty;//DateTime.Now.ToShortDateString();
            txtSKU.Text = string.Empty;
            lblMsg.Text = string.Empty;
            lblNote.Visible = false;
          //  btnSummary.Visible = false;
            btnDownload.Visible = false;
            //lblMsg.Text = "";
            lblCount.Text = "";
            chkActive.Checked = false;
            gvStocks.DataSource = null;
            gvStocks.DataBind();
            Session["stockSummary"] = null;
        }

        protected void gvStocks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStocks.PageIndex = e.NewPageIndex;
            if (Session["stockSummary"] != null)
            {
                List<StockCount> soList = (List<StockCount>)Session["stockSummary"];

                gvStocks.DataSource = soList;
                gvStocks.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

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
        public List<SV.Framework.Models.Inventory.StockCount> Sort<TKey>(List<SV.Framework.Models.Inventory.StockCount> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<StockCount>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<StockCount>();
            }
        }
        protected void gvStocks_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["stockSummary"] != null)
            {
                List<StockCount> stocks = (List<StockCount>)Session["stockSummary"];

                if (stocks != null && stocks.Count > 0)
                {
                    var list = stocks;
                    if (Sortdir == "ASC")
                    {
                        list = Sort<StockCount>(list, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        list = Sort<StockCount>(list, SortExp, SortDirection.Descending);
                    }
                    Session["stockSummary"] = list;
                    gvStocks.DataSource = list;
                    gvStocks.DataBind();
                }
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadCSV();
        }
        private void DownloadCSV()
        {
            List<StockCount> stocks = Session["stockSummary"] as List<StockCount>;
            List<StockCountCSV> stockList = new List<StockCountCSV>();
            StockCountCSV model = null;
            if (stocks != null && stocks.Count > 0)
            {
                foreach(StockCount stock in stocks)
                {
                    model = new StockCountCSV();
                    model.CategoryName = stock.CategoryName;
                    model.SKU = stock.SKU;
                    model.ProductName = stock.ItemName;
                    model.ReceivedDate = stock.StockDate.ToString("MM-dd-yyyy");
                    model.OpeningBalance = stock.OpeningBalance;
                    model.StockReceived = stock.StockReceived;
                    model.StockAssigned = stock.StockAssigned;
                    model.StockReassignment = stock.StockReassignment;
                    model.ClosingBalance = stock.ClosingBalance;
                    stockList.Add(model);
                }

                if (stockList != null && stockList.Count > 0)
                {
                    string string2CSV = stockList.ToCSV();

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=StockReceived.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(string2CSV);
                    Response.Flush();
                    Response.End();

                }
            }
            //ServiceRequestDownload
        }
    }
}