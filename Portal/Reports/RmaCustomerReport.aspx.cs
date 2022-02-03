using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Reports
{
    public partial class RmaCustomerReport : System.Web.UI.Page
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
                int timeInterval = 30;
                if (timeInterval == 1)
                    lblDuration.Text = DateTime.Now.ToShortDateString();
                else
                {
                    lblDuration.Text = "From: " + DateTime.Now.AddDays(-timeInterval).ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
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
            DataTable dt = avii.Classes.clsCompany.GetCompany(0, 0);
            ViewState["customer"] = dt;
            dpCompany.DataSource = dt;
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
        }
        private void PopulateData(int companyID, int timeInterval)
        {
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
                gvRMA.DataSource = null;
                lblRMA.Text = string.Empty;
                Session["rmastatus"] = null;
            }
            else
            {
                List<CompanyRmaStatuses> rmaStatusList = ReportOperations.GetCustomerRmaStatusReport(companyID, timeInterval, 0);

                Session["rmastatus"] = rmaStatusList;
                if (rmaStatusList != null && rmaStatusList.Count > 0)
                {
                    gvRMA.DataSource = rmaStatusList;
                    lblCount.Text = "<strong>Total Customers:</strong> " + Convert.ToString(rmaStatusList.Count - 1);
                    lblRMA.Text = string.Empty;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    gvRMA.DataSource = null;
                    lblRMA.Text = "No records found";
                }
            }
            gvRMA.DataBind();
        }
        private void ReloadData()
        {
            List<CompanyRmaStatuses> rmaStatusList = null;
            if (Session["rmastatus"] != null)
            {
                rmaStatusList = (List<CompanyRmaStatuses>)Session["rmastatus"];


                Session["rmastatus"] = rmaStatusList;
                if (rmaStatusList != null && rmaStatusList.Count > 0)
                {
                    gvRMA.DataSource = rmaStatusList;
                    lblCount.Text = "<strong>Total Customers:</strong> " + Convert.ToString(rmaStatusList.Count - 1);
                    lblRMA.Text = string.Empty;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    gvRMA.DataSource = null;
                    lblRMA.Text = "No records found";
                }
            }
            else
            {
                lblCount.Text = string.Empty;
                gvRMA.DataSource = null;
                lblRMA.Text = string.Empty;
                lblMsg.Text = "Session expire!";
            }
            gvRMA.DataBind();
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRMA.PageIndex = e.NewPageIndex;
            ReloadData();

        }

        private void ReloadPOStatus(int companyID, int timeInterval)
        {
            //Control tmp1 = LoadControl("~/controls/CustomerRmaStatus.ascx");
            //avii.Controls.CustomerRmaStatus ctlRMAStatus = tmp1 as avii.Controls.CustomerRmaStatus;
            //pnlRMA.Controls.Clear();
            //if (tmp1 != null)
            //{
            //    ctlRMAStatus.TimeInterval = timeInterval;
            //    ctlRMAStatus.CompanyID = companyID;

            //    ctlRMAStatus.PopulateData();
            //}
            //pnlRMA.Controls.Add(ctlRMAStatus);

        }
        protected void lnkCustomer_OnCommand(object sender, CommandEventArgs e)
        {
            int timeInterval = 0;
            int companyID = 0;
            
            string companyName = e.CommandArgument.ToString();
            timeInterval = Convert.ToInt32(ddlDuration.SelectedValue);
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

                    companyID = userInfo.CompanyGUID;
                }

            }
            else
            {

                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                else
                {
                    if (ViewState["customer"] != null)
                    {
                        DataTable dt = (ViewState["customer"]) as DataTable;
                        var result = from r in dt.AsEnumerable()
                                     where r.Field<string>("CompanyName") == companyName
                                     select r;
                        DataTable dtResult = result.CopyToDataTable();
                        companyID = Convert.ToInt32(dtResult.Rows[0]["companyID"]);
                    }
                }
            }
            if (companyID > 0)
            {
                ReloadRmaEsnDetail(companyID, timeInterval);
            }

        }
        
        protected void lnkRMA_OnCommand(object sender, CommandEventArgs e)
        {
            int timeInterval = 0;
            int companyID = 0;
            string companyName = string.Empty;
            string company = e.CommandArgument.ToString();
            string[] arr = company.Split('|');

            int statusID = Convert.ToInt32(arr[1]);
            companyName = arr[0];

            timeInterval = Convert.ToInt32(ddlDuration.SelectedValue);
            
            
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

                    companyID = userInfo.CompanyGUID;
                }

            }
            else
            {
                lblCompany.Text = string.Empty;

                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                else
                { 
                    if(ViewState["customer"]!=null)
                    {
                        DataTable dt = (ViewState["customer"]) as DataTable;
                        var result = from r in dt.AsEnumerable()
                                     where r.Field<string>("CompanyName") == companyName  select r;
                        DataTable dtResult = result.CopyToDataTable();
                        companyID = Convert.ToInt32(dtResult.Rows[0]["companyID"]);
                        //DataRow[] result = dt.Select("CompanyName == " + companyName);
                        //foreach (DataRow row in result)
                        //{
                            
                        //}

                  }
                }
            }
            if (companyID > 0)
            {
                ReloadRmaESN(statusID, companyID, timeInterval);
            }

        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        protected void gvRMA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
                return;


            LinkButton lnkCustomer = e.Row.FindControl("lnkCustomer") as LinkButton;
            if (lnkCustomer != null)
                lnkCustomer.OnClientClick = "openEsnDetailDialogAndBlock('Rma ESN Listing', '" + lnkCustomer.ClientID + "')";


            LinkButton lnkRma1 = e.Row.FindControl("lnkRma1") as LinkButton;
            if (lnkRma1 != null)
                lnkRma1.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma1.ClientID + "')";

            LinkButton lnkRma2 = e.Row.FindControl("lnkRma2") as LinkButton;
            if (lnkRma2 != null)
                lnkRma2.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma2.ClientID + "')";

            LinkButton lnkRma3 = e.Row.FindControl("lnkRma3") as LinkButton;
            if (lnkRma3 != null)
                lnkRma3.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma3.ClientID + "')";
            
            LinkButton lnkRma4 = e.Row.FindControl("lnkRma4") as LinkButton;
            if (lnkRma4 != null)
                lnkRma4.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma4.ClientID + "')";

            LinkButton  lnkRma5 = e.Row.FindControl("lnkRma5") as LinkButton;
            if (lnkRma5 != null)
                lnkRma5.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma5.ClientID + "')";

            LinkButton lnkRma6 = e.Row.FindControl("lnkRma6") as LinkButton;
            if (lnkRma6 != null)
                lnkRma6.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma6.ClientID + "')";

            LinkButton lnkRma7 = e.Row.FindControl("lnkRma7") as LinkButton;
            if (lnkRma7 != null)
                lnkRma7.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma7.ClientID + "')";
            
            LinkButton lnkRma8 = e.Row.FindControl("lnkRma8") as LinkButton;
            if (lnkRma8 != null)
                lnkRma8.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma8.ClientID + "')";

            LinkButton lnkRma9 = e.Row.FindControl("lnkRma9") as LinkButton;
            if (lnkRma9 != null)
                lnkRma9.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma9.ClientID + "')";

            LinkButton lnkRma10 = e.Row.FindControl("lnkRma10") as LinkButton;
            if (lnkRma10 != null)
                lnkRma10.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma10.ClientID + "')";

            LinkButton lnkRma11 = e.Row.FindControl("lnkRma11") as LinkButton;
            if (lnkRma11 != null)
                lnkRma11.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma11.ClientID + "')";
            LinkButton lnkRma12 = e.Row.FindControl("lnkRma12") as LinkButton;
            if (lnkRma12 != null)
                lnkRma12.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma12.ClientID + "')";

            LinkButton lnkRma13 = e.Row.FindControl("lnkRma13") as LinkButton;
            if (lnkRma13 != null)
                lnkRma13.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma13.ClientID + "')";

            LinkButton lnkRma14 = e.Row.FindControl("lnkRma14") as LinkButton;
            if (lnkRma14 != null)
                lnkRma14.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma14.ClientID + "')";

            LinkButton lnkRma15 = e.Row.FindControl("lnkRma15") as LinkButton;
            if (lnkRma15 != null)
                lnkRma15.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma15.ClientID + "')";
            
            LinkButton lnkRma16 = e.Row.FindControl("lnkRma16") as LinkButton;
            if (lnkRma16 != null)
                lnkRma16.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma16.ClientID + "')";
            
            LinkButton lnkRma17 = e.Row.FindControl("lnkRma17") as LinkButton;
            if (lnkRma17 != null)
                lnkRma17.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma17.ClientID + "')";

            LinkButton lnkRma18 = e.Row.FindControl("lnkRma18") as LinkButton;
            if (lnkRma18 != null)
                lnkRma18.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma18.ClientID + "')";

            LinkButton lnkRma19 = e.Row.FindControl("lnkRma19") as LinkButton;
            if (lnkRma19 != null)
                lnkRma19.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma19.ClientID + "')";
            LinkButton lnkRma20 = e.Row.FindControl("lnkRma20") as LinkButton;
            if (lnkRma20 != null)
                lnkRma20.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma20.ClientID + "')";

            LinkButton  lnkRma21 = e.Row.FindControl("lnkRma21") as LinkButton;
            if (lnkRma21 != null)
                lnkRma21.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma21.ClientID + "')";

            LinkButton lnkRma22 = e.Row.FindControl("lnkRma22") as LinkButton;
            if (lnkRma22 != null)
                lnkRma22.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma22.ClientID + "')";

            LinkButton lnkRma23 = e.Row.FindControl("lnkRma23") as LinkButton;
            if (lnkRma23 != null)
                lnkRma23.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma23.ClientID + "')";
            
            LinkButton lnkRma24 = e.Row.FindControl("lnkRma24") as LinkButton;
            if (lnkRma24 != null)
                lnkRma24.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma24.ClientID + "')";
            
            LinkButton lnkRma25 = e.Row.FindControl("lnkRma25") as LinkButton;
            if (lnkRma25 != null)
                lnkRma25.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma25.ClientID + "')";

            LinkButton lnkRma26 = e.Row.FindControl("lnkRma26") as LinkButton;
            if (lnkRma26 != null)
                lnkRma26.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma26.ClientID + "')";

            LinkButton lnkRma27 = e.Row.FindControl("lnkRma27") as LinkButton;
            if (lnkRma27 != null)
                lnkRma27.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma27.ClientID + "')";
            LinkButton lnkRma28 = e.Row.FindControl("lnkRma28") as LinkButton;
            if (lnkRma28 != null)
                lnkRma28.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma28.ClientID + "')";

            LinkButton lnkRma29 = e.Row.FindControl("lnkRma29") as LinkButton;
            if (lnkRma29 != null)
                lnkRma29.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma29.ClientID + "')";

            LinkButton lnkRma30 = e.Row.FindControl("lnkRma30") as LinkButton;
            if (lnkRma30 != null)
                lnkRma30.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma30.ClientID + "')";

            LinkButton lnkRma31 = e.Row.FindControl("lnkRma31") as LinkButton;
            if (lnkRma31 != null)
                lnkRma31.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma31.ClientID + "')";
            
            LinkButton lnkRma32 = e.Row.FindControl("lnkRma32") as LinkButton;
            if (lnkRma32 != null)
                lnkRma32.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma32.ClientID + "')";
            
            LinkButton lnkRma33 = e.Row.FindControl("lnkRma33") as LinkButton;
            if (lnkRma33 != null)
                lnkRma33.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma33.ClientID + "')";

            LinkButton lnkRma34 = e.Row.FindControl("lnkRma34") as LinkButton;
            if (lnkRma34 != null)
                lnkRma34.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma34.ClientID + "')";

            LinkButton lnkRma35 = e.Row.FindControl("lnkRma35") as LinkButton;
            if (lnkRma35 != null)
                lnkRma35.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma35.ClientID + "')";
            LinkButton lnkRma36 = e.Row.FindControl("lnkRma36") as LinkButton;
            if (lnkRma36 != null)
                lnkRma36.OnClientClick = "openDialogAndBlock('Rma ESN', '" + lnkRma36.ClientID + "')";

            

        }
        private void ReloadRmaESN(int statusID, int companyID, int timeInterval)
        {
            Control tmp1 = LoadControl("~/controls/RmaESNs.ascx");
            avii.Controls.RmaESNs ctlRmaEsn = tmp1 as avii.Controls.RmaESNs;
            pnlESN.Controls.Clear();
            if (tmp1 != null)
            {
                ctlRmaEsn.PopulateData(statusID, companyID, timeInterval);
            }
            pnlESN.Controls.Add(ctlRmaEsn);

            RegisterStartupScript("jsUnblockDialog", "unblockDialog();");

        }
        private void ReloadRmaEsnDetail(int companyID, int timeInterval)
        {
            Control tmp1 = LoadControl("~/controls/RmaEsnDetail.ascx");
            avii.Controls.RmaEsnDetail ctlRmaEsnDetail = tmp1 as avii.Controls.RmaEsnDetail;
            pnlEsnDetail.Controls.Clear();
            if (tmp1 != null)
            {
                ctlRmaEsnDetail.PopulateData(companyID, timeInterval, 0, 0);
            }
            pnlEsnDetail.Controls.Add(ctlRmaEsnDetail);

            RegisterStartupScript("jsUnblockDialog", "unblockEsnDetailDialog();");

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Session["adm"] != null)
                dpCompany.SelectedIndex = 0;

            ddlDuration.SelectedIndex = 0;
            
            lblMsg.Text = string.Empty;
            lblDuration.Text = string.Empty;
            ReloadPOStatus(0, 0);


        }

    }
}