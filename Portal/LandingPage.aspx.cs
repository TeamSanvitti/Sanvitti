using System;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace avii
{
    public partial class LandingPage : System.Web.UI.Page
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
                //LoadUserLog();
                if (Request["pos"] != null && Request["t"] != null && Request["cid"] != null)
                {
                    Session["postatus"] = Request["pos"].ToString();
                    Session["days"] = Request["t"].ToString();
                    Session["cid"] = Request["cid"].ToString();
                    if (Request["type"] != null)
                        Session["type"] = Request["type"].ToString();

                    if (Session["adm"] == null)
                        Response.Redirect("~/FulfillmentSearch", false);
                    else
                        Response.Redirect("~/FulfillmentSearch", false);
                }
                if (Request["rma"] != null && Request["t"] != null && Request["cid"] != null)
                {
                    Session["rmastatus"] = Request["rma"].ToString();
                    Session["days"] = Request["t"].ToString();
                    Session["cid"] = Request["cid"].ToString();
                    Response.Redirect("~/RMA/RMAQueryNew.aspx");
                }
                if (Request["sku"] != null)
                {
                    Session["sku"] = Request["sku"].ToString();
                    Response.Redirect("~/SKUPOStatusDetails.aspx");
                }
                if (Session["adm"] == null)
                {
                   // pnlCust.Visible = false;
                    //btnRefresh.Visible = true;
                }
                else
                {
                  //  BindCustomer();
                    //pnlCust.Visible = true;
                    //btnRefresh.Visible = false;
                }
               // LoadShipBy();
            }
        }

        //protected void gvShipby_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvShipby.PageIndex = e.NewPageIndex;
        //    ReloadShipBy();

        //}
        //private void ReloadShipBy()
        //{
        //    if (Session["shipbysummary"] != null)
        //    {
        //        List<avii.Classes.ShipBySummary> shipByList = (List<avii.Classes.ShipBySummary>)Session["shipbysummary"];
        //        gvShipby.DataSource = shipByList;
        //        gvShipby.DataBind();

        //    }
        //}
        //private void LoadShipBy()
        //{

        //    lblShipBy.Text = string.Empty;
        //    int timeInterval = Convert.ToInt32(ddlDuration.SelectedValue);
        //    int companyID = 0;

        //    if (Session["adm"] != null)
        //    {
        //        if (dpCompany.SelectedIndex > 0)
        //            companyID = Convert.ToInt32(dpCompany.SelectedValue);

        //    }
        //    else
        //    {
        //        avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
        //        if (userInfo != null)
        //        {
        //            companyID = userInfo.CompanyGUID;

        //        }
        //    }
        //    List<avii.Classes.ShipBySummary> shipByList = avii.Classes.DashboardOperations.GetShipBySummary(companyID, string.Empty,string.Empty);
        //    if (shipByList != null && shipByList.Count > 0)
        //    {
        //        gvShipby.DataSource = shipByList;
        //        Session["shipbysummary"] = shipByList;
        //    }
        //    else
        //    {
        //        gvShipby.DataSource = null;
        //        Session["shipbysummary"] = null;
        //        lblShipBy.Text = "No records found";
        //    }
        //    gvShipby.DataBind();
        //}
        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    int timeInterval = Convert.ToInt32(ddlDuration.SelectedValue);
        //    int companyID = 0;
        //    string sku = string.Empty;
        //    if (Session["adm"] != null)
        //    {
        //        if (dpCompany.SelectedIndex > 0)
        //            companyID = Convert.ToInt32(dpCompany.SelectedValue);

        //    }
        //    sku = txtSearch.Text.Trim().Length > 0 ? txtSearch.Text.Trim() : string.Empty;
        //    //if (!string.IsNullOrEmpty(sku))
        //    ReloadAssignedSKUs(companyID, timeInterval, sku);
        //    //else
        //    //{
        //        //Label lblMsg = SKUAssigned1.FindControl("lblSKU") as Label;
        //        //lblMsg.Text = "SKU can not be empty";
        //    //}

        //    ReloadData();           
        //}
        //private void LoadUserLog()
        //{
        //    //load PO from session
        //    Control tmp1 = LoadControl("./controls/UserLog.ascx");
        //    avii.Controls.UserLog ctlUserLog = tmp1 as avii.Controls.UserLog;
        //    pnlUser.Controls.Clear();
        //    ctlUserLog.PopulateUserLogSummary();
        //    pnlUser.Controls.Add(ctlUserLog);

        //}
        //private void ReloadData()
        //{
        //    //load PO from session
        //    Control tmp1 = LoadControl("./controls/POStatus.ascx");
        //    avii.Controls.POStatus ctlPOStatus = tmp1 as avii.Controls.POStatus;
        //    pnlPO.Controls.Clear();
        //    ctlPOStatus.ReloadData();
        //    //load RMA from session
        //    Control tmp = LoadControl("./controls/RMAStatus.ascx");
        //    avii.Controls.RMAStatus ctlRMAStatus = tmp as avii.Controls.RMAStatus;
        //    pnlRMA.Controls.Clear();
        //    ctlRMAStatus.ReloadData();
        //    pnlPO.Controls.Add(ctlPOStatus);
        //    pnlRMA.Controls.Add(ctlRMAStatus);
        //}
        //protected void btnRefresh_Click(object sender, EventArgs e)
        //{
        //    txtSearch.Text = string.Empty;
        //    ReloadControls();
        //    LoadShipBy();
        //}
        //private void BindCustomer()
        //{
        //    dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
        //    dpCompany.DataValueField = "CompanyID";
        //    dpCompany.DataTextField = "CompanyName";
        //    dpCompany.DataBind();

        //}
        //protected void ddlDuration_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ReloadControls();
        //}
        //private void ReloadAssignedSKUs(int companyID, int timeInterval, string sku)
        //{
        //    Control tmp2 = LoadControl("./controls/AssignedSKUs.ascx");
        //    avii.Controls.AssignedSKUs ctlAssignedSKU = tmp2 as avii.Controls.AssignedSKUs;
        //    pnlSKU.Controls.Clear();
        //    if (tmp2 != null)
        //    {
        //        ctlAssignedSKU.TimeInterval = timeInterval;
        //        ctlAssignedSKU.CompanyID = companyID;
        //        ctlAssignedSKU.SKU = sku;

        //        ctlAssignedSKU.LoadData();
        //    }
        //    pnlSKU.Controls.Add(ctlAssignedSKU);


        //}
        //private void ReloadPOStatus(int companyID, int timeInterval)
        //{
        //    Control tmp1 = LoadControl("./controls/POStatus.ascx");
        //    avii.Controls.POStatus ctlPOStatus = tmp1 as avii.Controls.POStatus;
        //    pnlPO.Controls.Clear();
        //    if (tmp1 != null)
        //    {
                

        //        ctlPOStatus.FromDate = string.Empty;
        //        ctlPOStatus.ToDate = string.Empty;
        //        ctlPOStatus.CompanyID = companyID;

        //        ctlPOStatus.LoadData();
        //    }
        //    pnlPO.Controls.Add(ctlPOStatus);
            
        //}
        //private void ReloadRMAStatus(int companyID, int timeInterval)
        //{
        //    Control tmp = LoadControl("./controls/RMAStatus.ascx");


        //    avii.Controls.RMAStatus ctlRMAStatus = tmp as avii.Controls.RMAStatus;
        //    pnlRMA.Controls.Clear();

        //    if (tmp != null)
        //    {
        //        ctlRMAStatus.FromDate = string.Empty;
        //        ctlRMAStatus.ToDate = string.Empty;
        //       // ctlRMAStatus.TimeInterval = timeInterval;
        //        ctlRMAStatus.CompanyID = companyID;

        //        ctlRMAStatus.LoadData();
        //    }

        //    pnlRMA.Controls.Add(ctlRMAStatus);
        //}
        //private void ReloadControls()
        //{
        //    int timeInterval = Convert.ToInt32(ddlDuration.SelectedValue);
        //    int companyID  = 0;
        //    if (Session["adm"] != null)
        //    {
        //        if (dpCompany.SelectedIndex > 0)
        //            companyID = Convert.ToInt32(dpCompany.SelectedValue);
                
        //    }



        //    ReloadPOStatus(companyID, timeInterval);
        //    ReloadRMAStatus(companyID, timeInterval);
        //    ReloadAssignedSKUs(companyID, timeInterval, string.Empty);
        //    LoadUserLog();
            
        //}
        //protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ReloadControls();
        //}

    }
}