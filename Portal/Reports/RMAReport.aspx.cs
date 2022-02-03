using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using SV.Framework.Models.RMA;

namespace avii.RMA
{
    public partial class RMAReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

                    //lblCompany.Text = userInfo.CompanyName;
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
                ///btnPrint.Visible = false;
                btnDownload.Visible = false;
                if (Session["adm"] != null)
                {
                    BindCustomer();
                }
                else
                {
                    trCustomer.Visible = false;
                }
                BindRMAStatus();
                BindLineItemsRMAStatus();
                BindReceiveStatuses();
                BindTriageStatus();
            }
        }
        private void BindRMAStatus()
        {
            try
            {
                SV.Framework.RMA.RmaOperation rmaOperation = SV.Framework.RMA.RmaOperation.CreateInstance<SV.Framework.RMA.RmaOperation>();

                int companyID = 0;

                List<CustomerRMAStatus> customerRMAStatusList = rmaOperation.GetCustomerRMAStatusList(companyID, true);
                if (customerRMAStatusList != null && customerRMAStatusList.Count > 0)
                {
                    ddlRmaStaus.DataSource = customerRMAStatusList;
                    ddlRmaStaus.DataValueField = "StatusID";
                    ddlRmaStaus.DataTextField = "StatusDescription";

                    ddlRmaStaus.DataBind();
                    ddlRmaStaus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        private void BindLineItemsRMAStatus()
        {
            try
            {
                SV.Framework.RMA.RmaOperation rmaOperation = SV.Framework.RMA.RmaOperation.CreateInstance<SV.Framework.RMA.RmaOperation>();

                List<CustomerRMAStatus> customerRMAStatusList = rmaOperation.GetRmaDetailStatusList();
                if (customerRMAStatusList != null && customerRMAStatusList.Count > 0)
                {
                    ddlEsnStatus.DataSource = customerRMAStatusList;
                    ddlEsnStatus.DataValueField = "StatusID";
                    ddlEsnStatus.DataTextField = "StatusDescription";
                    ddlEsnStatus.DataBind();
                    ddlEsnStatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        private void BindTriageStatus()
        {
            try
            {
                List<avii.Classes.RmaTriageStatus> triagestatusList = new List<Classes.RmaTriageStatus>();// avii.Classes.CompanyOperations.GetReceiveRMAStatusList();
                if (Session["triagestatusList"] != null)
                {
                    triagestatusList = Session["triagestatusList"] as List<Classes.RmaTriageStatus>;
                }
                else
                {
                    triagestatusList = avii.Classes.CompanyOperations.GetTriageStatusList();
                }
                if (triagestatusList != null && triagestatusList.Count > 0)
                {
                    Session["triagestatusList"] = triagestatusList;
                    ddlTriage.DataSource = triagestatusList;
                    ddlTriage.DataValueField = "TriageStatusID";
                    ddlTriage.DataTextField = "TriageStatus";
                    ddlTriage.DataBind();
                    ddlTriage.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        private void BindReceiveStatuses()
        {
            try
            {
                List<avii.Classes.RmaReceiveStatus> statusList = new List<Classes.RmaReceiveStatus>();// avii.Classes.CompanyOperations.GetReceiveRMAStatusList();
                if (Session["receivestatusList"] != null)
                {
                    statusList = Session["receivestatusList"] as List<Classes.RmaReceiveStatus>;
                }
                else
                {
                    statusList = avii.Classes.CompanyOperations.GetReceiveRMAStatusList();
                }
                if (statusList != null && statusList.Count > 0)
                {
                    Session["receivestatusList"] = statusList;
                    ddlReceive.DataSource = statusList;
                    ddlReceive.DataValueField = "ReceiveStatusID";
                    ddlReceive.DataTextField = "ReceiveStatus";
                    ddlReceive.DataBind();
                    ddlReceive.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
                
        private void BindCustomer()
        {
            DataTable dt = avii.Classes.clsCompany.GetCompany(0, 0);
            ViewState["customer"] = dt;
            dpCompany.DataSource = dt;
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }

        private void BindRmaReport()
        {
            SV.Framework.RMA.RmaReportOperation rmaReportOperation = SV.Framework.RMA.RmaReportOperation.CreateInstance<SV.Framework.RMA.RmaReportOperation>();

            bool IsValid = true;
            int companyID = 0, rmaStatusID = 0, esnStatusID = 0, triageStatusID = 0, receiveStatusID = 0;
            string fromDate, toDate;
            fromDate = toDate = string.Empty;
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            Session["rmaList"] = null;
            fromDate = txtFromDate.Text;
            toDate = txtToDate.Text;
            if(ddlEsnStatus.SelectedIndex > 0)
                esnStatusID = Convert.ToInt32(ddlEsnStatus.SelectedValue);
            if (ddlRmaStaus.SelectedIndex > 0)
                rmaStatusID = Convert.ToInt32(ddlRmaStaus.SelectedValue);
            if (ddlReceive.SelectedIndex > 0)
                receiveStatusID = Convert.ToInt32(ddlReceive.SelectedValue);
            if (ddlTriage.SelectedIndex > 0)
                triageStatusID = Convert.ToInt32(ddlTriage.SelectedValue);
            if (Session["adm"] != null)
            {
                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);

                if (companyID == 0 && receiveStatusID == 0 && triageStatusID == 0 && rmaStatusID == 0 && esnStatusID == 0 && string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
                {
                    IsValid = false;
                }
            }
            else
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;

                if (userInfo != null)
                {
                    companyID = userInfo.CompanyGUID;
                }
                if (receiveStatusID == 0 && triageStatusID == 0 && rmaStatusID == 0 && esnStatusID == 0 && string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
                {
                    IsValid = false;
                }
            }

            if (IsValid)
            {
                List<RmaInfo> rmaList = rmaReportOperation.GetRMAReport(companyID, fromDate, toDate, rmaStatusID, esnStatusID, triageStatusID, receiveStatusID);
                if (rmaList != null && rmaList.Count > 0)
                {
                    Session["rmaList"] = rmaList;
                    gvRMA.DataSource = rmaList;
                    gvRMA.DataBind();
                    
                    lblCount.Text = "<strong>Total ESN:</strong> " + Convert.ToString(rmaList.Count);
                }
                else
                {
                    //btnPrint.Visible = false;
                    btnDownload.Visible = false;
                    lblCount.Text = string.Empty;
                    gvRMA.DataSource = null;
                    gvRMA.DataBind();
                    lblMsg.Text = "No records found";
                }
            }
            else
            {
                lblMsg.Text = "Please select search criteria!";
            }
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                List<RmaInfo> rmaList = new List<RmaInfo>();
                int index = e.Row.RowIndex;
                if (Session["rmaList"] != null)
                {
                    rmaList = Session["rmaList"] as List<RmaInfo>;
                    if (rmaList != null && rmaList.Count > 0)
                    {
                        List<RMADetail> rmaDetails = rmaList[index].RMADetails;
                        if (rmaDetails != null && rmaDetails.Count > 0)
                        {
                            GridView gvRmaDetail = e.Row.FindControl("gvRmaDetail") as GridView;
                            gvRmaDetail.DataSource = rmaDetails;
                            gvRmaDetail.DataBind();
                        }
                    }
                }
                //int poid = Convert.ToInt32(gvReceive.DataKeys[e.Row.RowIndex].Value);


            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindRmaReport();
        }
        private void Reset()
        {
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            dpCompany.SelectedIndex = 0;
            ddlEsnStatus.SelectedIndex = 0;
            ddlRmaStaus.SelectedIndex = 0;
            ddlReceive.SelectedIndex = 0;
            ddlTriage.SelectedIndex = 0;
            gvRMA.DataSource = null;
            gvRMA.DataBind();

            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Reset();
        }
        protected void imgViewRMA_Command(object sender, CommandEventArgs e)
        {
            int rmaGUID = Convert.ToInt32(e.CommandArgument);
            Session["rmaGUID"] = rmaGUID;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('../RMA/RmaView.aspx')</script>", false);

            // Response.Redirect("~/RMA/RmaView.aspx", false);
        }
        protected void gvRMA_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvRMA_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
    }
}