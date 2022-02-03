using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Fulfillment;

namespace avii.Reports
{
    public partial class FulfillmentsStatusReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

                    lblCompany.Text = userInfo.CompanyName;
                    dpCompany.Visible = false;

                }
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
                pnlPoSku.Visible = false;
                BindCustomer();
                int timeInterval = 30;
                if (timeInterval == 1)
                    lblDuration.Text = DateTime.Now.ToShortDateString();
                else
                {
                    lblDuration.Text = "From: " + DateTime.Now.AddDays(-timeInterval).ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                }
                if (Request["pos"] != null && Request["t"] != null && Request["cid"] != null)
                {
                    Session["postatus"] = Request["pos"].ToString();
                    Session["days"] = Request["t"].ToString();
                    Session["cid"] = Request["cid"].ToString();

                    Response.Redirect("~/POQueryNew.aspx");
                }
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
            int companyID = 0;
            int timeInterval = 0;
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

                    lblCompany.Text = userInfo.CompanyName;
                    dpCompany.Visible = false;
                    companyID = userInfo.CompanyGUID;
                }

            }
            else
            {
                lblCompany.Text = string.Empty;

                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
            }
            //if (ddlDuration.SelectedIndex > 0)
            timeInterval = Convert.ToInt32(ddlDuration.SelectedValue);
            if (timeInterval == 1)
                lblDuration.Text = DateTime.Now.ToShortDateString();
            else
            {
                lblDuration.Text = "From: " + DateTime.Now.AddDays(-timeInterval).ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
            }

            PopulateData(companyID, timeInterval);

            PopulatePoSKUs(timeInterval);
        }
        public void ReloadData()
        {
            //if (Session["adm"] == null)
            //{
            //    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            //    if (userInfo != null)
            //    {
            //        CompanyID = userInfo.CompanyGUID;

            //    }

            //}
            List<FulfillmentStatusReport> poStatusSummaryList = null;
            if (Session["POStatus"] != null)
            {
                //    poStatusSummaryList = ReportOperations.GetCustomerFulfillmentStatusReport(CompanyID, TimeInterval);
                //else
                poStatusSummaryList = (List<FulfillmentStatusReport>)Session["POStatus"];


                Session["POStatus"] = poStatusSummaryList;
                if (poStatusSummaryList.Count > 0)
                {
                    lblCount.Text = "<strong>Total Customers:</strong> " + Convert.ToString(poStatusSummaryList.Count - 1);
                    gvPO.DataSource = poStatusSummaryList;
                    lblMsg.Text = string.Empty;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    gvPO.DataSource = null;
                    lblMsg.Text = "No records found";
                }
            }
            else
            {
                lblCount.Text = string.Empty;
                gvPO.DataSource = null;
                lblMsg.Text = string.Empty;

            }
            gvPO.DataBind();
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPO.PageIndex = e.NewPageIndex;
            ReloadData();

        }

        protected void gvPOSKU_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPOSKU.PageIndex = e.NewPageIndex;
            if (Session["POskuStatus"] != null)
            {
                List<FulfillmentSKUStatus> poSkuStatusSummaryList = (Session["POskuStatus"]) as List<FulfillmentSKUStatus>;
                gvPOSKU.DataSource = poSkuStatusSummaryList;
                gvPOSKU.DataBind();
            }
            //ReloadData();

        }

        public void PopulateData(int companyID, int timeInterval)
        {
            SV.Framework.Fulfillment.FulfillmentReportOperation fulfillmentReportOperation = SV.Framework.Fulfillment.FulfillmentReportOperation.CreateInstance<SV.Framework.Fulfillment.FulfillmentReportOperation>();

            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    companyID = userInfo.CompanyGUID;

                }

            }

            if (timeInterval == 0)
            {
                lblCount.Text = string.Empty;
                gvPO.DataSource = null;
                lblMsg.Text = string.Empty;
                Session["POStatus"] = null;
            }
            else
            {
                List<FulfillmentStatusReport> poStatusSummaryList = fulfillmentReportOperation.GetCustomerFulfillmentStatusReport(companyID, timeInterval, 0);

                Session["POStatus"] = poStatusSummaryList;
                if (poStatusSummaryList != null && poStatusSummaryList.Count > 0)
                {
                    gvPO.DataSource = poStatusSummaryList;
                    lblCount.Text = "<strong>Total Customers:</strong> " + Convert.ToString(poStatusSummaryList.Count - 1);
                    lblMsg.Text = string.Empty;
                }
                else
                {
                    Session["POStatus"] = null;
                    lblCount.Text = string.Empty;
                    gvPO.DataSource = null;
                    lblMsg.Text = "No records found";
                }
            }
            gvPO.DataBind();
        }

        protected void gvPO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
                return;

            LinkButton lnkPos = e.Row.FindControl("lnkPos") as LinkButton;
            if (lnkPos != null)
                lnkPos.OnClientClick = "openDialogAndBlock('Storewise fulfillment statuses', '" + lnkPos.ClientID + "')";

            

        }
        protected void lnkPOStatus_OnCommand(object sender, CommandEventArgs e)
        {
            int timeInterval = 30;
            string companyName = e.CommandArgument.ToString();
            if (!string.IsNullOrEmpty(companyName))
            {
                //string[] arr = values.Split(',');
                //string poNum = arr[0];
                //string companyAccountNumber = arr[1];
                Control tmp1 = LoadControl("../controls/StoresFulfillmentStatus.ascx");
                avii.Controls.StoresFulfillmentStatus ctlStorePO = tmp1 as avii.Controls.StoresFulfillmentStatus;
                pnlStore.Controls.Clear();
                ctlStorePO.CompanyName = companyName;
                timeInterval = Convert.ToInt32(ddlDuration.SelectedValue);
                ctlStorePO.TimeInterval = timeInterval;
                ctlStorePO.PopulateData();

                pnlStore.Controls.Add(ctlStorePO);

                RegisterStartupScript("jsUnblockDialog", "unblockDialog();");



            }

        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        private void ReloadPOStatus(int companyID, int timeInterval)
        {
            //Control tmp1 = LoadControl("~/controls/FulfillmentStatus.ascx");
            //avii.Controls.FulfillmentStatuses ctlPOStatus = tmp1 as avii.Controls.FulfillmentStatuses;
            //pnlPO.Controls.Clear();
            //if (tmp1 != null)
            //{
            //    ctlPOStatus.TimeInterval = timeInterval;
            //    ctlPOStatus.CompanyID = companyID;

            //    ctlPOStatus.PopulateData();
            //}
            //pnlPO.Controls.Add(ctlPOStatus);

        }
        
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Session["adm"] != null)
                dpCompany.SelectedIndex = 0;

            lblDuration.Text = string.Empty;
            ddlDuration.SelectedIndex = 0;
            //dpCompany.SelectedIndex = 0;
            lblMsg.Text = string.Empty;
            gvPO.DataSource = null;
            gvPO.DataBind();
            lblCount.Text = string.Empty;
            //ReloadPOStatus(0, 0);
        }

        public void PopulatePoSKUs(int timeInterval)
        {
            SV.Framework.Fulfillment.FulfillmentReportOperation fulfillmentReportOperation = SV.Framework.Fulfillment.FulfillmentReportOperation.CreateInstance<SV.Framework.Fulfillment.FulfillmentReportOperation>();

            int companyID = 0;
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    companyID = userInfo.CompanyGUID;


                    pnlPoSku.Visible = true;
                    if (timeInterval == 0)
                    {
                        lblSKUCount.Text = string.Empty;
                        gvPO.DataSource = null;
                        lblPOsku.Text = string.Empty;
                        Session["POskuStatus"] = null;
                        pnlPoSku.Visible = false;
                    }
                    else
                    {
                        List<FulfillmentSKUStatus> poSkuStatusSummaryList = fulfillmentReportOperation.GetCustomerFulfillmentSKUStatusReport(companyID, timeInterval, 0);

                        Session["POskuStatus"] = poSkuStatusSummaryList;
                        if (poSkuStatusSummaryList != null && poSkuStatusSummaryList.Count > 0)
                        {
                            gvPOSKU.DataSource = poSkuStatusSummaryList;
                            lblSKUCount.Text = "<strong>Total SKUs:</strong> " + Convert.ToString(poSkuStatusSummaryList.Count - 1);
                            lblPOsku.Text = string.Empty;

                        }
                        else
                        {
                            lblSKUCount.Text = string.Empty;
                            gvPOSKU.DataSource = null;
                            Session["POskuStatus"] = null;
                            lblPOsku.Text = "No records found";
                        }
                    }
                    gvPOSKU.DataBind();
                }

            }
        }
    }
}