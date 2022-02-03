using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace avii.Reports
{
    public partial class BillingReport : System.Web.UI.Page
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
                ddlPoType.SelectedIndex = 1;
                //BindCustomer();
               // txtDateFrom.Text = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToShortDateString()).ToString("MM/dd/yyyy");
               // txtToDate.Text = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("MM/dd/yyyy");
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
            BindReport();
        }
        private void BindReport()
        {
            SV.Framework.Fulfillment.FulfillmentReportOperation fulfillmentReportOperation = SV.Framework.Fulfillment.FulfillmentReportOperation.CreateInstance<SV.Framework.Fulfillment.FulfillmentReportOperation>();

            //btnSummary.Visible = false;
            //btnDownload.Visible = false;
            btnDownload.Visible = false;
            //lblNote.Visible = false;
            lblMsg.Text = "";
            lblCount.Text = "";
            int companyID = 0;
            string dateFrom, dateTo, ShipFromDate, ShipToDate, FulfillmentNumber, FulfillmentType, contactName;
            dateFrom = txtDateFrom.Text.Trim();
            dateTo = txtToDate.Text.Trim();
            string sortExpression = "FulfillmentDate";
            string sortDirection = "DESC";
            ShipFromDate = txtShipFromDate.Text.Trim();
            ShipToDate = txtShipDateTo.Text.Trim();
            FulfillmentNumber = txtFulfillmentNo.Text.Trim();
            FulfillmentType = ddlPoType.SelectedValue;
            contactName = txtContactName.Text.Trim();
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

            List<FulfillmentBilling> poBillings = fulfillmentReportOperation.GetFulfillmentBillingReport(companyID, dateFrom, dateTo, ShipFromDate, ShipToDate, FulfillmentNumber, FulfillmentType, contactName);
            if (poBillings != null && poBillings.Count > 0)
            {
                gvPO.DataSource = poBillings;
                gvPO.DataBind();
                Session["poBillings"] = poBillings;
                
                btnDownload.Visible = true;
                // var MinDate = (from d in poBillings select d.StockDate).Min();

                //Retrieve Maximum Date
                // var MaxDate = (from d in poBillings select d.StockDate).Max();
                //lblCount.Text = "<strong>Date Range:</strong> " + MinDate.ToString("MM-dd-yyyy") + " - " + MaxDate.ToString("MM-dd-yyyy") + " &nbsp;&nbsp;&nbsp; <strong>Total records:</strong> " + stockSummary.Count + " &nbsp;&nbsp;&nbsp; <strong>Total SKU(s): </strong> " + skuCount;
                lblCount.Text = "<strong>Total records:</strong> " + poBillings.Count;
            }
            else
            {
                gvPO.DataSource = null;
                gvPO.DataBind();
                Session["poBillings"] = null;
                lblMsg.Text = "No record found";
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Session["adm"] != null)
            {
                dpCompany.SelectedIndex = 0;
                
            }
            ddlPoType.SelectedIndex = 0;
            txtContactName.Text = string.Empty;//DateTime.Now.ToShortDateString();
            txtDateFrom.Text = string.Empty;//DateTime.Now.ToShortDateString();
            txtToDate.Text = string.Empty;//DateTime.Now.ToShortDateString();
            txtShipDateTo.Text = string.Empty;//DateTime.Now.ToShortDateString();
            txtShipFromDate.Text = string.Empty;//DateTime.Now.ToShortDateString();
            txtFulfillmentNo.Text = string.Empty;//DateTime.Now.ToShortDateString();
         //   txtSKU.Text = string.Empty;
            lblMsg.Text = string.Empty;
            btnDownload.Visible = false;
            //lblMsg.Text = "";
            lblCount.Text = "";
            gvPO.DataSource = null;
            gvPO.DataBind();
            Session["poBillings"] = null;
        }

        protected void gvPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPO.PageIndex = e.NewPageIndex;
            if (Session["poBillings"] != null)
            {
                List<FulfillmentBilling> poBillings = (List<FulfillmentBilling>)Session["poBillings"];

                gvPO.DataSource = poBillings;
                gvPO.DataBind();
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
        public List<FulfillmentBilling> Sort<TKey>(List<FulfillmentBilling> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<FulfillmentBilling>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<FulfillmentBilling>();
            }
        }
        protected void gvPO_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["poBillings"] != null)
            {
                List<FulfillmentBilling> stocks = (List<FulfillmentBilling>)Session["poBillings"];

                if (stocks != null && stocks.Count > 0)
                {
                    var list = stocks;
                    if (Sortdir == "ASC")
                    {
                        list = Sort<FulfillmentBilling>(list, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        list = Sort<FulfillmentBilling>(list, SortExp, SortDirection.Descending);
                    }
                    Session["poBillings"] = list;
                    gvPO.DataSource = list;
                    gvPO.DataBind();
                }
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadCSV();
        }
        private void DownloadCSV()
        {
            List<FulfillmentBilling> stocks = Session["poBillings"] as List<FulfillmentBilling>;
            List<FulfillmentBillingCSV> billList = new List<FulfillmentBillingCSV>();
            FulfillmentBillingCSV model = null;
            if (stocks != null && stocks.Count > 0)
            {
                foreach (FulfillmentBilling item in stocks)
                {
                    model = new FulfillmentBillingCSV();
                    model.BatchNumber = item.BatchNumber;
                    model.ContactName = item.ContactName;
                    model.ContainerID = !string.IsNullOrEmpty(item.ContainerID) ? "#" +item.ContainerID : "";
                    model.ICC_ID = !string.IsNullOrEmpty(item.ICC_ID) ? "#" + item.ICC_ID : ""; ;
                    model.FulfillmentDate = item.FulfillmentDate.ToString("MM-dd-yyyy");
                    model.FulfillmentNumber = item.FulfillmentNumber;
                    model.FulfillmentType = item.FulfillmentType;
                    model.ESN = "#" + item.ESN;
                    model.Price = item.Price;
                    model.ShipDate = item.ShipDate.ToString("MM-dd-yyyy");
                    model.ShipmentType = item.ShipmentType;
                    model.ShipPackage = item.ShipPackage;
                    model.ShipVia = item.ShipVia;
                    model.Weight = item.Weight;
                    model.TrackingNumber = "#" + item.TrackingNumber;
                    model.ShipmentType = item.ShipmentType;



                    billList.Add(model);
                }

                if (billList != null && billList.Count > 0)
                {
                    string string2CSV = billList.ToCSV();

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=BillingReport.csv");
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