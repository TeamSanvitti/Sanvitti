using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Reports
{
    public partial class RmaSummary : System.Web.UI.Page
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
                BindCustomer();
                string companyAccountNumber = string.Empty;
                if (Session["adm"] == null)
                {
                    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    if (userInfo != null)
                    {
                        companyAccountNumber = userInfo.CompanyAccountNumber;


                        ReloadRMAs(companyAccountNumber, 30);
                    }

                }
                
                //if (Request["pos"] != null && Request["t"] != null && Request["cid"] != null)
                //{
                //    Session["postatus"] = Request["pos"].ToString();
                //    Session["days"] = Request["t"].ToString();
                //    Session["cid"] = Request["cid"].ToString();

                //    Response.Redirect("~/POQueryNew.aspx");
                //}
            }
        }
        private void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyAccountNumber";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string companyAccountNumber = string.Empty;
            int timeInterval = 0;
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

                    lblCompany.Text = userInfo.CompanyName;
                    dpCompany.Visible = false;

                }

            }
            else
                lblCompany.Text = string.Empty;

            if (dpCompany.SelectedIndex > 0)
                companyAccountNumber = dpCompany.SelectedValue;
            //if (ddlDuration.SelectedIndex > 0)
            timeInterval = Convert.ToInt32(ddlDuration.SelectedValue);

            
            

            ReloadRMAs(companyAccountNumber, timeInterval);
        }
        private void ReloadRMAs(string companyAccountNumber, int timeInterval)
        {
            if (timeInterval == 1)
                lblDuration.Text = DateTime.Now.ToShortDateString();
            else
            {
                lblDuration.Text = "From: " + DateTime.Now.AddDays(-timeInterval).ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
            }
            PopulateData(companyAccountNumber, timeInterval);


            //Control tmp1 = LoadControl("~/controls/RmaReasonSummary.ascx");
            //avii.Controls.RmaReasonSummary ctlRMAStatus = tmp1 as avii.Controls.RmaReasonSummary;
            //pnlRMA.Controls.Clear();
            //if (tmp1 != null)
            //{
            //    ctlRMAStatus.TimeInterval = timeInterval;
            //    ctlRMAStatus.CompanyAccountNumber = companyAccountNumber;

            //    ctlRMAStatus.PopulateData();
            //}
            //pnlRMA.Controls.Add(ctlRMAStatus);


        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton lnkDOA = (LinkButton)e.Row.FindControl("lnkDOA");
                lnkDOA.OnClientClick = "openDialogAndBlock('Product RMA Statuses', '" + lnkDOA.ClientID + "')";
                
                LinkButton lnkAI = (LinkButton)e.Row.FindControl("lnkAI");
                lnkAI.OnClientClick = "openDialogAndBlock('Product RMA Statuses', '" + lnkAI.ClientID + "')";
                
                LinkButton lnkSI = (LinkButton)e.Row.FindControl("lnkSI");
                lnkSI.OnClientClick = "openDialogAndBlock('Product RMA Statuses', '" + lnkSI.ClientID + "')";
                
                LinkButton lnkPI = (LinkButton)e.Row.FindControl("lnkPI");
                lnkPI.OnClientClick = "openDialogAndBlock('Product RMA Statuses', '" + lnkPI.ClientID + "')";

                
                LinkButton lnkOthers = (LinkButton)e.Row.FindControl("lnkOthers");
                lnkOthers.OnClientClick = "openDialogAndBlock('Product RMA Statuses', '" + lnkOthers.ClientID + "')";
                
                LinkButton lnkMP = (LinkButton)e.Row.FindControl("lnkMP");
                lnkMP.OnClientClick = "openDialogAndBlock('Product RMA Statuses', '" + lnkMP.ClientID + "')";
                
                LinkButton lnkRTS = (LinkButton)e.Row.FindControl("lnkRTS");
                lnkRTS.OnClientClick = "openDialogAndBlock('Product RMA Statuses', '" + lnkRTS.ClientID + "')";
                
                LinkButton lnkBR = (LinkButton)e.Row.FindControl("lnkBR");
                lnkBR.OnClientClick = "openDialogAndBlock('Product RMA Statuses', '" + lnkBR.ClientID + "')";
                
                LinkButton lnkPA = (LinkButton)e.Row.FindControl("lnkPA");
                lnkPA.OnClientClick = "openDialogAndBlock('Product RMA Statuses', '" + lnkPA.ClientID + "')";
                
                LinkButton lnkLD = (LinkButton)e.Row.FindControl("lnkLD");
                lnkLD.OnClientClick = "openDialogAndBlock('Product RMA Statuses', '" + lnkLD.ClientID + "')";
                
                LinkButton lnkDC = (LinkButton)e.Row.FindControl("lnkDC");
                lnkDC.OnClientClick = "openDialogAndBlock('Product RMA Statuses', '" + lnkDC.ClientID + "')";
                
                LinkButton lnkACI = (LinkButton)e.Row.FindControl("lnkACI");
                lnkACI.OnClientClick = "openDialogAndBlock('Product RMA Statuses', '" + lnkACI.ClientID + "')";
                
                LinkButton lnkCVI = (LinkButton)e.Row.FindControl("lnkCVI");
                lnkCVI.OnClientClick = "openDialogAndBlock('Product RMA Statuses', '" + lnkCVI.ClientID + "')";
                
                LinkButton lnkSoftware = (LinkButton)e.Row.FindControl("lnkSoftware");
                lnkSoftware.OnClientClick = "openDialogAndBlock('Product RMA Statuses', '" + lnkSoftware.ClientID + "')";
                
                LinkButton lnkLP = (LinkButton)e.Row.FindControl("lnkLP");
                lnkLP.OnClientClick = "openDialogAndBlock('Product RMA Statuses', '" + lnkLP.ClientID + "')";
                
                LinkButton lnkSE = (LinkButton)e.Row.FindControl("lnkSE");
                lnkSE.OnClientClick = "openDialogAndBlock('Product RMA Statuses', '" + lnkSE.ClientID + "')";

                LinkButton lnkHI = (LinkButton)e.Row.FindControl("lnkHI");
                lnkHI.OnClientClick = "openDialogAndBlock('Product RMA Statuses', '" + lnkHI.ClientID + "')";


            }
        }
        protected void lnkDOA_OnCommand(object sender, CommandEventArgs e)
        {
            string productInfo = e.CommandArgument.ToString();
            string[] arr = productInfo.Split(',');
            string productName = string.Empty;
            
            int reasonID = 1;
            
            if (arr != null && arr.Length > 0)
                reasonID = Convert.ToInt32(arr[0]);
            
            if (arr != null && arr.Length > 1)
                productName = arr[1];

            BindStatusPopup(reasonID, productName);

            
        }
        private void BindStatusPopup(int reasonID, string productName)
        {
            int timeInterval = 0;
            string companyName = string.Empty;
            timeInterval = Convert.ToInt32(ddlDuration.SelectedValue);
            string companyAccountNumber = string.Empty;
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    companyAccountNumber = userInfo.CompanyAccountNumber;
                    companyName = userInfo.CompanyName;
                }

            }
            else
            {
                if (dpCompany.SelectedIndex > 0)
                {
                    companyAccountNumber = dpCompany.SelectedValue;
                    companyName = dpCompany.SelectedItem.Text;
                }
            }
            if (!string.IsNullOrEmpty(productName))
            {

                Control tmp1 = LoadControl("../controls/ctlRmaStatusesSummary.ascx");
                avii.Controls.ctlRmaStatusesSummary ctlRMAReason = tmp1 as avii.Controls.ctlRmaStatusesSummary;
                ctlRMAReason.CompanyName = companyName;
                //ctlRMAReason.TimeInterval = timeInterval;
                pnlRMAView.Controls.Clear();
                ctlRMAReason.BindRmaStatus(reasonID, productName, companyAccountNumber, timeInterval);

                pnlRMAView.Controls.Add(ctlRMAReason);

                RegisterStartupScript("jsUnblockDialog", "unblockDialog();");



            }
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRMA.PageIndex = e.NewPageIndex;
            ReloadData();

        }
        public void PopulateData(string companyAccountNumber, int timeInterval)
        {
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    //CompanyAccountNumber = userInfo.CompanyAccountNumber;

                }

            }
            if (string.IsNullOrEmpty(companyAccountNumber) && timeInterval == 0)
            {
                lblCount.Text = string.Empty;
                gvRMA.DataSource = null;
                lblPO.Text = string.Empty;
                Session["rmareason"] = null;
            }
            else
            {
                List<avii.Classes.ProductRmaReason> rmaSummaryList = avii.Classes.ReportOperations.GetProductRMAReasonSummary(companyAccountNumber, timeInterval, 0);

                Session["rmareason"] = rmaSummaryList;
                if (rmaSummaryList.Count > 0)
                {
                    gvRMA.DataSource = rmaSummaryList;
                    lblCount.Text = "<strong>Total products:</strong> " + Convert.ToString(rmaSummaryList.Count-1);
                    lblPO.Text = string.Empty;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    gvRMA.DataSource = null;
                    lblPO.Text = "No records found";
                }
            }
            gvRMA.DataBind();
        }
        public void ReloadData()
        {
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    //CompanyID = userInfo.CompanyGUID;

                }

            }
            List<avii.Classes.ProductRmaReason> rmaSummaryList = null;
            if (Session["rmareason"] != null)
            {
                //  rmaSummaryList = ReportOperations.GetProductRMAReasonSummary(CompanyAccountNumber, TimeInterval);
                //else
                rmaSummaryList = (List<avii.Classes.ProductRmaReason>)Session["rmareason"];


                Session["rmareason"] = rmaSummaryList;
                if (rmaSummaryList.Count > 0)
                {
                    lblCount.Text = "<strong>Total products:</strong> " + Convert.ToString(rmaSummaryList.Count - 1);
                    gvRMA.DataSource = rmaSummaryList;
                    lblPO.Text = string.Empty;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    gvRMA.DataSource = null;
                    lblPO.Text = "No records found";
                }
            }
            else
            {
                lblCount.Text = string.Empty;
                gvRMA.DataSource = null;
                lblPO.Text = string.Empty; ;
            }
            gvRMA.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Session["adm"] != null)
                dpCompany.SelectedIndex = 0;

            lblDuration.Text = string.Empty;
            ddlDuration.SelectedIndex = 0;
            //dpCompany.SelectedIndex = 0;
            lblMsg.Text = string.Empty;
            ReloadRMAs(null, 0);
        }
    }
}