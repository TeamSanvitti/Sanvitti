using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace avii.Reports
{
    public partial class StockInDemands : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    url = System.Configuration.ConfigurationSettings.AppSettings["LogonPage"].ToString();

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
            if(!IsPostBack)
            {
                BindCompany();
                if (Session["adm"] == null)
                {
                    if (Session["userInfo"] != null)
                    {
                       avii.Classes. UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                        if (userInfo != null)
                        {
                            dpCompany.SelectedValue = userInfo.CompanyGUID.ToString();
                            dpCompany.Enabled = false;
                        }
                    }
                }
                string fromDate = DateTime.Now.AddMonths(-1).ToShortDateString();
                string toDate = DateTime.Now.ToShortDateString();
                txtFromDate.Text = fromDate;
                txtToDate.Text = toDate;

            }
        }

        private void BindCompany()
        {
            //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            try
            {
                dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
                dpCompany.DataValueField = "companyID";
                dpCompany.DataTextField = "companyName";
                dpCompany.DataBind();
                //ListItem item = new ListItem("", "0");
                //ddlCompany.Items.Insert(0, item);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindStockInDemand();
        }
        protected void lnkPO_Command(object sender, CommandEventArgs e)
        {
            string sku = Convert.ToString(e.CommandArgument);
            if (!string.IsNullOrWhiteSpace(sku))
            {
                Session["sku"] = sku;
                // Response.Redirect("~/PoQueryNew.aspx");
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
        private void BindStockInDemand()
        {
            SV.Framework.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.Inventory.InventoryReportOperation>();
            int companyID = 0;
            bool validForm = false;
            string fromDate, toDate, SKU;
            fromDate = toDate = null;
            SKU = string.Empty;
            btnDownload.Visible = false;
            try
            {
                SKU = txtSKU.Text.Trim();
                if (txtFromDate.Text.Trim().Length > 0)
                    fromDate = txtFromDate.Text.Trim();
                if (txtToDate.Text.Trim().Length > 0)
                    toDate = txtToDate.Text.Trim();     
                
                lblMsg.Text = string.Empty;
                lblCount.Text = string.Empty;

                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);

                string sortExpression = "SKU";
                string sortDirection = "ASC";
                ViewState["SortDirection"] = sortDirection;
                ViewState["SortExpression"] = sortExpression;


                if (companyID == 0 && string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(SKU))
                {
                    validForm = false;
                }
                else
                    validForm = true;

                if (validForm)
                {
                    List<StockInDemand> StockInDemands = inventoryReportOperation.GetStockInDemandList(companyID, SKU, fromDate, toDate);
                    if (StockInDemands != null && StockInDemands.Count > 0)
                    {
                        gvStock.DataSource = StockInDemands;
                        gvStock.DataBind();
                        Session["StockInDemand"] = StockInDemands;
                        btnDownload.Visible = true;
                        lblCount.Text = "<strong>Total Records:</strong> " + StockInDemands.Count;
                    }
                    else
                    {
                        lblMsg.Text = "No record exists";
                        Session["StockInDemand"] = null;
                        gvStock.DataSource = null;
                        gvStock.DataBind();
                    }
                }
                else
                {
                    lblMsg.Text = "Please select the search criteria";
                    Session["StockInDemand"] = null;
                    gvStock.DataSource = null;
                    gvStock.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Clear()
        {
            btnDownload.Visible = false;
            dpCompany.SelectedIndex = 0;
            txtFromDate.Text = string.Empty;
            txtSKU.Text = string.Empty;
            txtToDate.Text = string.Empty;
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            gvStock.DataSource = null;
            gvStock.DataBind();

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void DownloadCSV()
        {
            List<StockInDemand> StockInDemands = Session["StockInDemand"] as List<StockInDemand>;
            List<StockInDemandCSV> StockInDemandCSVs = null;
            StockInDemandCSV stockInDemand = null;
            if (StockInDemands != null && StockInDemands.Count > 0)
            {
                StockInDemandCSVs = new List<StockInDemandCSV>();
                foreach(StockInDemand row in StockInDemands)
                {
                    stockInDemand = new StockInDemandCSV();
                    stockInDemand.CategoryName = row.CategoryName;
                    stockInDemand.CurrentStock = row.CurrentStock;
                    stockInDemand.OrderCount = row.OrderCount;
                    stockInDemand.ProductName = row.ProductName;
                    stockInDemand.RequiredQunatity = row.RequiredQunatity;
                    stockInDemand.SKU = row.SKU;
                    StockInDemandCSVs.Add(stockInDemand);
                }
                string string2CSV = StockInDemandCSVs.ToCSV();

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=sockindemand.csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(string2CSV);
                Response.Flush();
                Response.End();

            }
            //ServiceRequestDownload
        }
        protected void gvStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStock.PageIndex = e.NewPageIndex;


            if (Session["StockInDemand"] != null)
            {
                List<StockInDemand> StockInDemands = (List<StockInDemand>)Session["StockInDemand"];

                gvStock.DataSource = StockInDemands;
                gvStock.DataBind();
            }
            else
            {
                BindStockInDemand();
            }

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


        protected void gvStock_Sorting(object sender, GridViewSortEventArgs e)
        {

            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["StockInDemand"] != null)
            {
                List<StockInDemand> StockInDemands = (List<StockInDemand>)Session["StockInDemand"];

                if (StockInDemands != null && StockInDemands.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        StockInDemands = Sort<StockInDemand>(StockInDemands, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        StockInDemands = Sort<StockInDemand>(StockInDemands, SortExp, SortDirection.Descending);
                    }
                    Session["StockInDemand"] = StockInDemands;
                    gvStock.DataSource = StockInDemands;
                    gvStock.DataBind();
                }
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadCSV();
        }
    }
}