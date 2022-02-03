using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
using System.Reflection;

namespace avii
{
    public partial class SKUStockStatus : System.Web.UI.Page
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
                //if (Session["UserID"] == null)
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
                txtStockDate.Text = DateTime.Now.AddDays(-7).ToShortDateString();
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

            BindSKUStocks();
        }
        private void BindSKUStocks()
        {
            btnSummary.Visible = false;
            btnDownload.Visible = false;

            lblNote.Visible = false;
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
           
            List<StockStatus> stocks = StockOperation.GetStockStatus(companyID, stockDateFrom, stockDateTo, sku);
            if(stocks!= null && stocks.Count > 0)
            {
                gvStocks.DataSource = stocks;
                gvStocks.DataBind();
                Session["stocks"] = stocks;
                lblNote.Visible = true;
                btnSummary.Visible = true;
                btnDownload.Visible = true;

                lblCount.Text = "<strong>Total Count:</strong> " + stocks.Count;
            }
            else
            {
                gvStocks.DataSource = null;
                gvStocks.DataBind();
                Session["stocks"] = null;
                lblMsg.Text = "No record found";
            }

        }

        private void ShowSummary()
        {
            List<StockStatus> stocks = Session["stocks"] as List<StockStatus>;
            if(stocks != null && stocks.Count > 0)
            {
                var skuSummary = stocks.GroupBy(k => new { k.SKU }).Select(g => new { SKU = g.Key.SKU, ReceivedQty = g.Sum(s => s.StockReceived) }).ToList();

                if (skuSummary != null && skuSummary.Count > 0)
                {
                    rptSummary.DataSource = skuSummary;
                    rptSummary.DataBind();
                }
                else
                {
                    rptSummary.DataSource = null;
                    rptSummary.DataBind();
                }


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
            btnSummary.Visible = false;
            btnDownload.Visible = false;
            //lblMsg.Text = "";
            lblCount.Text = "";

            gvStocks.DataSource = null;
            gvStocks.DataBind();
            Session["stocks"] = null;
        }
        protected void gvStocks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStocks.PageIndex = e.NewPageIndex;
            if (Session["stocks"] != null)
            {
                List<StockStatus> soList = (List<StockStatus>)Session["stocks"];

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
        public List<StockStatus> Sort<TKey>(List<StockStatus> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<StockStatus>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<StockStatus>();
            }
        }
        protected void gvStocks_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["stocks"] != null)
            {
                List<StockStatus> stocks = (List<StockStatus>)Session["stocks"];

                if (stocks != null && stocks.Count > 0)
                {
                    var list = stocks;
                    if (Sortdir == "ASC")
                    {
                        list = Sort<StockStatus>(list, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        list = Sort<StockStatus>(list, SortExp, SortDirection.Descending);
                    }
                    Session["stocks"] = list;
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
            List<StockStatus> stocks = Session["stocks"] as List<StockStatus>;

            if (stocks != null && stocks.Count > 0)
            {
               
                    string string2CSV = stocks.ToCSV();

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=StockReceived.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(string2CSV);
                    Response.Flush();
                    Response.End();
                

            }
            //ServiceRequestDownload
        }

        protected void btnSummary_Click(object sender, EventArgs e)
        {
            ShowSummary();
            RegisterStartupScript("jsUnblockDialog", "unblockSummaryDialog();");
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

    }
}