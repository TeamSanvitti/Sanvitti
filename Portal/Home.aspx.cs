using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii
{
    public partial class Home : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                string fromDate = DateTime.Now.AddMonths(-1).ToShortDateString();
                string toDate = DateTime.Now.ToShortDateString();
                txtDateFrom.Text = fromDate;
                txtDateTo.Text = toDate;
                if (Request["pos"] != null && Request["t"] != null && Request["cid"] != null)
                {
                    Session["postatus"] = Request["pos"].ToString();
                    Session["days"] = Request["t"].ToString();
                    Session["cid"] = Request["cid"].ToString();
                    if (Request["type"] != null)
                        Session["type"] = Request["type"].ToString();


                    if (Session["adm"] == null)
                        Response.Redirect("~/POQueryNew.aspx");
                    else
                        Response.Redirect("~/POQueryNew.aspx");
                }
                if (Request["rma"] != null && Request["t"] != null && Request["cid"] != null)
                {
                    Session["rmastatus"] = Request["rma"].ToString();
                    Session["days"] = Request["t"].ToString();
                    Session["cid"] = Request["cid"].ToString();
                    Response.Redirect("~/RMA/NewRMAQuery.aspx");
                }
                if (Request["sku"] != null)
                {
                    Session["sku"] = Request["sku"].ToString();
                    Response.Redirect("~/SKUPOStatusDetails.aspx");
                }
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
                
            }
        }       
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //int timeInterval = Convert.ToInt32(ddlDuration.SelectedValue);
            string fromDate = string.Empty, todate = string.Empty;
            int companyID = 0;
            string sku = string.Empty;
            if (Session["adm"] != null)
            {
                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);

            }
            fromDate = txtDateFrom.Text.Trim().Length > 0 ? txtDateFrom.Text.Trim() : string.Empty;
            todate = txtDateTo.Text.Trim().Length > 0 ? txtDateTo.Text.Trim() : string.Empty;

            //sku = txtSearch.Text.Trim().Length > 0 ? txtSearch.Text.Trim() : string.Empty;
            //if (!string.IsNullOrEmpty(sku))
            //ReloadAssignedSKUs(companyID, fromDate, todate, sku);
            //else
            //{
            //Label lblMsg = SKUAssigned1.FindControl("lblSKU") as Label;
            //lblMsg.Text = "SKU can not be empty";
            //}

            ReloadData(1);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);

        }
        
        
        private void ReloadData(int flag)
        {
            //load PO from session
            Control tmp1 = LoadControl("./controls/POStatus.ascx");
            avii.Controls.POStatus ctlPOStatus = tmp1 as avii.Controls.POStatus;
            pnlPO.Controls.Clear();
            ctlPOStatus.ReloadData();
            //load RMA from session
            Control tmp = LoadControl("./controls/RMAStatus.ascx");
            avii.Controls.RMAStatus ctlRMAStatus = tmp as avii.Controls.RMAStatus;
            //pnlRMA.Controls.Clear();
            ctlRMAStatus.ReloadData();
            pnlPO.Controls.Add(ctlPOStatus);
            //pnlRMA.Controls.Add(ctlRMAStatus);


            //load PO sku stock from session
            Control poSKUStock = LoadControl("./controls/PoSKUStock.ascx");
            avii.Controls.PoSKUStock ctlPoSKUStock = poSKUStock as avii.Controls.PoSKUStock;
            pnlPoSkuStock.Controls.Clear();
            ctlPoSKUStock.ReloadData();
            pnlPoSkuStock.Controls.Add(ctlPoSKUStock);


            

        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ReloadControls();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }
        private void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        protected void ddlDuration_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadControls();
        }
        //private void ReloadAssignedSKUs(int companyID, string fromDate, string toDate, string sku)
        //{
        //    Control tmp2 = LoadControl("./controls/FulfillmentOrderSummary.ascx");
        //    avii.Controls.FuolfillmentOrderSummary ctlAssignedSKU = tmp2 as avii.Controls.FuolfillmentOrderSummary;
        //    pnlSKU.Controls.Clear();
        //    if (tmp2 != null)
        //    {
        //        ctlAssignedSKU.FromDate = fromDate;
        //        ctlAssignedSKU.ToDate = toDate;
        //        ctlAssignedSKU.CompanyID = companyID;
        //        ctlAssignedSKU.SKU = sku;

        //        ctlAssignedSKU.LoadData();
        //    }
        //    pnlSKU.Controls.Add(ctlAssignedSKU);


        //}
        private void ReloadPOStatus(int companyID, string fromDate, string toDate)
        {
            Control tmp1 = LoadControl("./controls/POStatus.ascx");
            avii.Controls.POStatus ctlPOStatus = tmp1 as avii.Controls.POStatus;
            pnlPO.Controls.Clear();
            if (tmp1 != null)
            {
                ctlPOStatus.FromDate = fromDate;
                ctlPOStatus.ToDate = toDate;
                ctlPOStatus.CompanyID = companyID;

                ctlPOStatus.LoadData();
            }
            pnlPO.Controls.Add(ctlPOStatus);

        }
       
        
        private void ReloadPOSKUStock(int companyID)
        {
            Control tmpStock = LoadControl("./controls/PoSKUStock.ascx");


            avii.Controls.PoSKUStock ctlSKUStocks = tmpStock as avii.Controls.PoSKUStock;
            pnlPoSkuStock.Controls.Clear();

            if (tmpStock != null)
            {
                ctlSKUStocks.CompanyID = companyID;
                ctlSKUStocks.PopulateData();
            }

            pnlPoSkuStock.Controls.Add(ctlSKUStocks);
        }
        private void ReloadPoStockInDemand(int companyID, string sku, string fromDate, string toDate)
        {
            Control tmpStockInDemand = LoadControl("./controls/PoStockInDemand.ascx");
            avii.Controls.PoStockInDemand ctlStockInDemand = tmpStockInDemand as avii.Controls.PoStockInDemand;
            pnlSID.Controls.Clear();

            if (tmpStockInDemand != null)
            {
                ctlStockInDemand.CompanyID = companyID;
                ctlStockInDemand.SKU = sku;
                ctlStockInDemand.FromDate = fromDate;
                ctlStockInDemand.ToDate = toDate;
                ctlStockInDemand.PopulateData();
            }
            pnlSID.Controls.Add(ctlStockInDemand);
        }

        private void ReloadControls()
        {
            string fromDate = txtDateFrom.Text.Trim();
            string toDate = txtDateTo.Text.Trim();
            bool IsDisable = false;
            bool IsKitted = false;

            int companyID = 0;
            string sku = string.Empty;

            if (Session["adm"] != null)
            {
                if (dpCompany.SelectedIndex > 0)
                {
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                    Session["cid"] = companyID;
                }

            }

            ReloadPOSKUStock(companyID);
            ReloadPoStockInDemand(companyID, sku, fromDate, toDate);
            //ReloadStock(companyID, sku, IsDisable, IsKitted);
            //ReloadSOR(companyID, sku, "");
            ReloadPOStatus(companyID, fromDate, toDate);
            //ReloadRMAStatus(companyID, fromDate, toDate);
            
        }
        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadControls();
        }

        
        protected void btnSIDSearch_Click(object sender, EventArgs e)
        {
            int companyID = 0;
            string sku, fromDate, toDate;
            sku = string.Empty;
            fromDate = toDate = null;

            if (Session["adm"] != null)
            {
                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);

            }
            if (txtDateFrom.Text.Trim().Length > 0)
                fromDate = txtDateFrom.Text.Trim();
            if (txtDateTo.Text.Trim().Length > 0)
                toDate = txtDateTo.Text.Trim();

            sku = txtSIDSKU.Text.Trim();
            ReloadPoStockInDemand(companyID, sku, fromDate, toDate);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }

        protected void lnkDashboard_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("~/dashboard.aspx");
        }
    }
}