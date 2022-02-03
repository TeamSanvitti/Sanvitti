using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
using System.Text;
using System.IO;

namespace avii.Reports
{
    public partial class FulfillmentTrackingReport : System.Web.UI.Page
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
                btnDownload.Visible = false;
                if (Session["adm"] == null)
                {
                    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    if (userInfo != null)
                    {
                        //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

                        lblCompany.Text = userInfo.CompanyName;
                        dpCompany.Visible = false;
                

                    }
                }
                else
                {
                    BindCustomer();
                    BindShipViaCode();

                }
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
        private void BindShipViaCode()
        {

            ddlShipVia.DataSource = avii.Classes.PurchaseOrder.GetShipByList();
            ddlShipVia.DataTextField = "ShipByText";
            ddlShipVia.DataValueField = "ShipByCode";
            ddlShipVia.DataBind();
            ListItem li = new ListItem("", "");

            ddlShipVia.Items.Insert(0, li);

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int companyID = 0;
            string trackingNumber, shipViaCode, fulfillmentNumber;
            trackingNumber = shipViaCode = fulfillmentNumber = null;

            DateTime fromDate, toDate;
            lblMsg.Text = string.Empty;

            try
            {
                fromDate = toDate = Convert.ToDateTime("1/1/0001");
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

                trackingNumber = txtTrackingNumber.Text.Trim().Length > 0 ? txtTrackingNumber.Text.Trim() : null;
                fulfillmentNumber = txtPoNum.Text.Trim().Length > 0 ? txtPoNum.Text.Trim() : null;
                shipViaCode = ddlShipVia.SelectedIndex > 0 ? ddlShipVia.SelectedValue : null;

                if (txtFromDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtFromDate.Text, out dt))
                        fromDate = dt;
                    else
                        throw new Exception("Ship Date From does not have correct format(MM/DD/YYYY)");
                }
                if (txtToDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtToDate.Text, out dt))
                        toDate = dt;
                    else
                        throw new Exception("Ship Date To does not have correct format(MM/DD/YYYY)");
                }
                if (companyID == 0 && txtFromDate.Text.Trim().Length == 0 && txtToDate.Text.Trim().Length == 0
                    && string.IsNullOrEmpty(shipViaCode) && string.IsNullOrEmpty(fulfillmentNumber) && string.IsNullOrEmpty(trackingNumber))
                {
                    lblMsg.Text = "Please select the search criteria";
                }
                else
                    PopulateData(companyID, fromDate, toDate, fulfillmentNumber, trackingNumber, shipViaCode);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (Session["trackingList"] != null)
            {
                List<FulfillmentTracking> trackingList = (Session["trackingList"]) as List<FulfillmentTracking>;
                string downLoadPath = string.Empty;
                downLoadPath = System.Configuration.ConfigurationManager.AppSettings["FullfillmentFilesStoreage"].ToString();

                try
                {


                    if (trackingList != null && trackingList.Count > 0)
                    {
                        string path = Server.MapPath(downLoadPath).ToString();
                        string fileName = "tracking" + Session.SessionID + ".csv";
                        bool found = false;
                        System.IO.FileInfo file = null;
                        file = new System.IO.FileInfo(path + fileName);
                        if (file.Exists)
                        {
                            file.Delete();
                        }

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        sb.Append("ShipDate,TrackingNumber,FulfillmentNumber,FulfillmentDate,ShipViaCode,ShipmentType,EsnCount\r\n");

                        foreach (avii.Classes.FulfillmentTracking trackingObj in trackingList)
                        {
                            sb.Append(trackingObj.ShipDate.ToShortDateString() + ","
                                        + trackingObj.TrackingNumber + ","
                                        + trackingObj.FulfillmentNumber + ","
                                        + trackingObj.FulfillmentDate.ToShortDateString() + ","
                                        + trackingObj.ShipByCode + ","
                                        + trackingObj.ShipmentType + ","
                                        + trackingObj.EsnCount.ToString() + ","
                                        + "\r\n");

                            found = true;
                        }

                        try
                        {
                            using (StreamWriter sw = new StreamWriter(file.FullName))
                            {
                                sw.WriteLine(sb.ToString());
                                sw.Flush();
                                sw.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMsg.Text = ex.Message;
                        }

                        if (found)
                        {
                            Response.Clear();
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                            Response.ContentType = "application/octet-stream";
                            Response.WriteFile(file.FullName);
                            Response.End();
                        }
                    }
                    else
                    { 
                        lblMsg.Text = "No records found"; 
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                }

            }
            else
                lblMsg.Text = "Session expire!";
        }

        private void PopulateData(int companyID, DateTime fromDate, DateTime toDate, string fulfillmentNumber, string trackingNumber, string shipViaCode)
        {

            List<FulfillmentTracking> trackingList = ReportOperations.GetCustomerFulfillmentTrackingReport(companyID, fromDate, toDate, fulfillmentNumber, trackingNumber, shipViaCode);

            
            if (trackingList != null && trackingList.Count > 0)
            {
                btnDownload.Visible = true;
                Session["trackingList"] = trackingList;
                gvTracking.DataSource = trackingList;
                lblCount.Text = "<strong>Total Shipment:</strong> " + Convert.ToString(trackingList.Count);
                lblMsg.Text = string.Empty;

            }
            else
            {
                btnDownload.Visible = false;
                Session["trackingList"] = null;
                lblCount.Text = string.Empty;
                gvTracking.DataSource = null;
                lblMsg.Text = "No records found";
            }

            gvTracking.DataBind();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            gvTracking.DataSource = null;
            gvTracking.DataBind();
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            btnDownload.Visible = false;
            txtTrackingNumber.Text = string.Empty;
            txtPoNum.Text = string.Empty;
            ddlShipVia.SelectedIndex = 0;
            dpCompany.SelectedIndex = 0;
            
        }
        protected void gvTracking_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lblAE = e.Row.FindControl("lblAE") as Label;
                //if(lblAE != null)
                //    availableBalance += Convert.ToInt32(lblAE.Text);

                LinkButton lnkESN = e.Row.FindControl("lnkESN") as LinkButton;
                if (lnkESN != null)
                {
                    lnkESN.OnClientClick = "openDialogAndBlock('ESN List', '" + lnkESN.ClientID + "')";
                   
                }
                

            }
            
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }
        protected void imgViewESN_Click(object sender, CommandEventArgs e)
        {
            try
            {
                Control tmp2 = LoadControl("~/controls/poESN.ascx");
                avii.Controls.poESN ctlViewESN = tmp2 as avii.Controls.poESN;
                pnlESN.Controls.Clear();
                string trackings = e.CommandArgument.ToString();
                string[] arr = trackings.Split(',');
                if (arr.Length > 1)
                {
                    int poid = Convert.ToInt32(arr[0]);
                    string trackingNumber = arr[1];
                    lblTracking.Text = trackingNumber;
                    if (tmp2 != null)
                    {

                        ctlViewESN.BindESN(poid, trackingNumber);
                    }
                    pnlESN.Controls.Add(ctlViewESN);

                    RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
                }
            }
            catch (Exception ex)
            {
                lblEsnMsg.Text = ex.Message;
            }


        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTracking.PageIndex = e.NewPageIndex;
            if (Session["trackingList"] != null)
            {
                List<FulfillmentTracking> trackingList = (List<FulfillmentTracking>)Session["trackingList"];

                gvTracking.DataSource = trackingList;
                gvTracking.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }


    }
}