//using avii.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.RMA;
//using SV.Framework.Models.Common;

namespace avii.RMA
{
    public partial class RmaStatusSummary : System.Web.UI.Page
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
                //pnlPoSku.Visible = false;
                BindCustomer();
                
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
            BindSummary();



        }
        private void BindSummary()
        {
            SV.Framework.RMA.RmaReportOperation rmaReportOperation = SV.Framework.RMA.RmaReportOperation.CreateInstance<SV.Framework.RMA.RmaReportOperation>();

            int companyID = 0, userID = 0;
            string fromDate = string.Empty, toDate = string.Empty, summaryBy;
            divRMA.InnerText = string.Empty;
            divRMA.InnerHtml = string.Empty;
            lblMsg.Text = string.Empty;
            fromDate = txtDateFrom.Text.Trim();
            toDate = txtDateTo.Text.Trim();
            summaryBy = ddlSummaryBy.SelectedValue;

            if (Session["adm"]==null)
            {
                userID = Convert.ToInt32(Session["UserID"]);
            }
            else
            {
                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
            }

            int i = 1;
            string fontweight = "normal";
            string backgroundcolor = "";

            if (summaryBy == "RMAStatus")
            {
                List<CustomerRmaStatus> rmaList = rmaReportOperation.GetCustomerRmaStatusSummary(companyID, fromDate, toDate, userID, summaryBy);
                if (rmaList != null && rmaList.Count > 0)
                {
                    lblCount.Text = "<strong>Total Customers:</strong> " + Convert.ToString(rmaList.Count - 1);

                    string rmaHTML = "<table width='100%' border='1' bordercolor='#839abf'><tr><td class='buttonlabel' >S.No.</td><td class='buttonlabel' >Customer Name</td><td class='buttonlabel' >Pending</td><td class='buttonlabel' >Completed</td><td class='buttonlabel' >Cancelled</td><td class='buttonlabel' >RMA Total</td></tr>";
                    string rmaHTMLBody = string.Empty;
                    string sno = string.Empty;

                    string CustomerName = string.Empty;
                    string Pending = string.Empty;
                    string Completed = string.Empty;
                    string Cancelled = string.Empty;

                    foreach (CustomerRmaStatus item in rmaList)
                    {
                        fontweight = item.CustomerName == "zzTotal" ? "bold" : "normal";
                        backgroundcolor = item.CustomerName == "zzTotal" ? "yellow" : "";
                        sno = item.CustomerName == "zzTotal" ? "" : i.ToString();
                        CustomerName = item.CustomerName == "zzTotal" ? "RMA Total" : item.CustomerName;

                        Pending = item.Pending.ToString() == "0" ? "" : item.Pending.ToString();
                        Completed = item.Completed.ToString() == "0" ? "" : item.Completed.ToString();
                        Cancelled = item.Cancelled.ToString() == "0" ? "" : item.Cancelled.ToString();

                        rmaHTMLBody = rmaHTMLBody + "<tr><td class='copy10grey' >" + sno + "</td><td class='copy10grey' ><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + CustomerName + "</div></td><td class='copy10grey' align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + Pending + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + Completed + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + Cancelled + "</div></td><td class='copy10grey' align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:yellow;height:20px'><b>" + item.Total + "</b></div></td></tr>";

                        i = i + 1;

                    }
                    rmaHTML = rmaHTML + rmaHTMLBody + "</table>";

                    divRMA.InnerText = rmaHTML;
                    divRMA.InnerHtml = rmaHTML;
                }
                else
                {

                    // Session["POStatus"] = null;
                    lblCount.Text = string.Empty;
                    // gvPO.DataSource = null;
                    lblMsg.Text = "No records found";
                }
            }
            else if (summaryBy == "LineItemStatus")
            {
                List<CustomerRmaEsnStatus> rmaList = rmaReportOperation.GetCustomerRmaESNStatusSummary(companyID, fromDate, toDate, userID, summaryBy);
                if (rmaList != null && rmaList.Count > 0)
                {
                    lblCount.Text = "<strong>Total Customers:</strong> " + Convert.ToString(rmaList.Count - 1);

                    string rmaHTML = "<table width='100%' border='1' bordercolor='#839abf'><tr><td class='buttonlabel' >S.No.</td><td class='buttonlabel' >Customer Name</td><td class='buttonlabel' >Pending</td><td class='buttonlabel' >Completed</td><td class='buttonlabel' >External ESN</td><td class='buttonlabel' >RTS (Return To Stock)</td><td class='buttonlabel' >Sent to Supplier</td><td class='buttonlabel' >Cancelled</td><td class='buttonlabel' >RMA Total</td></tr>";
                    string rmaHTMLBody = string.Empty;
                    string sno = string.Empty;
                    string SentToSupplier = string.Empty;
                    string ExternalESN = string.Empty;
                    string ReturnToStock = string.Empty;

                    string CustomerName = string.Empty;
                    string Pending = string.Empty;
                    string Completed = string.Empty;
                    string Cancelled = string.Empty;

                    foreach (CustomerRmaEsnStatus item in rmaList)
                    {
                        fontweight = item.CustomerName == "zzTotal" ? "bold" : "normal";
                        backgroundcolor = item.CustomerName == "zzTotal" ? "yellow" : "";
                        sno = item.CustomerName == "zzTotal" ? "" : i.ToString();
                        CustomerName = item.CustomerName == "zzTotal" ? "RMA Total" : item.CustomerName;

                        Pending = item.Pending.ToString() == "0" ? "" : item.Pending.ToString();
                        Completed = item.Completed.ToString() == "0" ? "" : item.Completed.ToString();
                        Cancelled = item.Cancelled.ToString() == "0" ? "" : item.Cancelled.ToString();
                        SentToSupplier = item.SentToSupplier.ToString() == "0" ? "" : item.SentToSupplier.ToString();
                        ExternalESN = item.ExternalESN.ToString() == "0" ? "" : item.ExternalESN.ToString();
                        ReturnToStock = item.ReturnToStock.ToString() == "0" ? "" : item.ReturnToStock.ToString();

                        rmaHTMLBody = rmaHTMLBody + "<tr><td class='copy10grey' >" + sno + "</td><td class='copy10grey' ><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + CustomerName + "</div></td><td class='copy10grey' align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + Pending + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + Completed + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + ExternalESN + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + ReturnToStock + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + SentToSupplier + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + Cancelled + "</div></td><td class='copy10grey' align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:yellow;height:20px'><b>" + item.Total + "</b></div></td></tr>";

                        i = i + 1;

                    }
                    rmaHTML = rmaHTML + rmaHTMLBody + "</table>";

                    divRMA.InnerText = rmaHTML;
                    divRMA.InnerHtml = rmaHTML;
                }
                else
                {

                    // Session["POStatus"] = null;
                    lblCount.Text = string.Empty;
                    // gvPO.DataSource = null;
                    lblMsg.Text = "No records found";
                }
            }
            else if (summaryBy == "Triage")
            {
                List<CustomerRmaTriageStatus> rmaList = rmaReportOperation.GetCustomerRmaTriageStatusSummary(companyID, fromDate, toDate, userID, summaryBy);
                if (rmaList != null && rmaList.Count > 0)
                {
                    lblCount.Text = "<strong>Total Customers:</strong> " + Convert.ToString(rmaList.Count - 1);

                    string rmaHTML = "<table width='100%' border='1' bordercolor='#839abf'><tr><td class='buttonlabel' >S.No.</td><td class='buttonlabel' >Customer Name</td><td class='buttonlabel' >Pending</td><td class='buttonlabel' >In-Process</td><td class='buttonlabel' >Complete</td><td class='buttonlabel' >Not Required</td><td class='buttonlabel' >RMA Total</td></tr>";
                    string rmaHTMLBody = string.Empty;
                    string sno = string.Empty;
                    string InProcess = string.Empty;
                    string NotRequired = string.Empty;
                    //string ReturnToStock = string.Empty;

                    string CustomerName = string.Empty;
                    string Pending = string.Empty;
                    string Complete = string.Empty;
                    //string Cancelled = string.Empty;

                    foreach (CustomerRmaTriageStatus item in rmaList)
                    {
                        fontweight = item.CustomerName == "zzTotal" ? "bold" : "normal";
                        backgroundcolor = item.CustomerName == "zzTotal" ? "yellow" : "";
                        sno = item.CustomerName == "zzTotal" ? "" : i.ToString();
                        CustomerName = item.CustomerName == "zzTotal" ? "RMA Total" : item.CustomerName;

                        Pending = item.Pending.ToString() == "0" ? "" : item.Pending.ToString();
                        InProcess = item.InProcess.ToString() == "0" ? "" : item.InProcess.ToString();
                        Complete = item.Complete.ToString() == "0" ? "" : item.Complete.ToString();
                        NotRequired = item.NotRequired.ToString() == "0" ? "" : item.NotRequired.ToString();
                        //ExternalESN = item.ExternalESN.ToString() == "0" ? "" : item.ExternalESN.ToString();
                        //ReturnToStock = item.ReturnToStock.ToString() == "0" ? "" : item.ReturnToStock.ToString();

                        rmaHTMLBody = rmaHTMLBody + "<tr><td class='copy10grey' >" + sno + "</td><td class='copy10grey' ><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + CustomerName + "</div></td><td class='copy10grey' align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + Pending + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + InProcess + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + Complete + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + NotRequired + "</div></td><td class='copy10grey' align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:yellow;height:20px'><b>" + item.Total + "</b></div></td></tr>";

                        i = i + 1;

                    }
                    rmaHTML = rmaHTML + rmaHTMLBody + "</table>";

                    divRMA.InnerText = rmaHTML;
                    divRMA.InnerHtml = rmaHTML;
                }
                else
                {

                    // Session["POStatus"] = null;
                    lblCount.Text = string.Empty;
                    // gvPO.DataSource = null;
                    lblMsg.Text = "No records found";
                }
            }

            else if (summaryBy == "Reason")
            {
                List<CustomerRmaReason> rmaList = rmaReportOperation.GetCustomerRmaReasonSummary(companyID, fromDate, toDate, userID, summaryBy);
                if (rmaList != null && rmaList.Count > 0)
                {
                    lblCount.Text = "<strong>Total Customers:</strong> " + Convert.ToString(rmaList.Count - 1);

                    string rmaHTML = "<table width='100%' border='1' bordercolor='#839abf'><tr><td class='buttonlabel' >S.No.</td><td class='buttonlabel' >Customer Name</td><td class='buttonlabel' >Activation/Coverage</td><td class='buttonlabel' >Battery/Power</td><td class='buttonlabel' >Hardware Issues</td><td class='buttonlabel' >Liquid Damage</td><td class='buttonlabel' >Missing Parts</td><td class='buttonlabel' >Others</td><td class='buttonlabel' >Physical Abuse</td><td class='buttonlabel' >Software</td><td class='buttonlabel' >RMA Total</td></tr>";
                    string rmaHTMLBody = string.Empty;
                    string sno = string.Empty;
                    string CustomerName = string.Empty;

                    string ActivationCoverage = string.Empty;
                    string BatteryPower = string.Empty;
                    //string BuyerRemorse = string.Empty;
                    //string CoverageIssues = string.Empty;
                    //string DOA = string.Empty;
                    //string DropCalls = string.Empty;
                    string HardwareIssues = string.Empty;
                    string LiquidDamage = string.Empty;
                   // string LoanerProgram = string.Empty;
                    string MissingParts = string.Empty;
                    string Others = string.Empty;
                    string PhysicalAbuse = string.Empty;
                    //string PowerIssues = string.Empty;
                    //string ReturnToStock = string.Empty;
                    //string ScreenIssues = string.Empty;
                    //string ShippingError = string.Empty;
                    string Software = string.Empty;
                    
                    foreach (CustomerRmaReason item in rmaList)
                    {
                        fontweight = item.CustomerName == "zzTotal" ? "bold" : "normal";
                        backgroundcolor = item.CustomerName == "zzTotal" ? "yellow" : "";
                        sno = item.CustomerName == "zzTotal" ? "" : i.ToString();
                        CustomerName = item.CustomerName == "zzTotal" ? "RMA Total" : item.CustomerName;

                        ActivationCoverage = item.ActivationCoverage.ToString() == "0" ? "" : item.ActivationCoverage.ToString();
                        //AudioIssues = item.AudioIssues.ToString() == "0" ? "" : item.AudioIssues.ToString();
                        BatteryPower = item.BatteryPower.ToString() == "0" ? "" : item.BatteryPower.ToString();
                        //CoverageIssues = item.CoverageIssues.ToString() == "0" ? "" : item.CoverageIssues.ToString();
                        //DOA = item.DOA.ToString() == "0" ? "" : item.DOA.ToString();
                        //DropCalls = item.DropCalls.ToString() == "0" ? "" : item.DropCalls.ToString();
                        HardwareIssues = item.HardwareIssues.ToString() == "0" ? "" : item.HardwareIssues.ToString();
                        LiquidDamage = item.LiquidDamage.ToString() == "0" ? "" : item.LiquidDamage.ToString();
                        //LoanerProgram = item.LoanerProgram.ToString() == "0" ? "" : item.LoanerProgram.ToString();
                        MissingParts = item.MissingParts.ToString() == "0" ? "" : item.MissingParts.ToString();
                        Others = item.Others.ToString() == "0" ? "" : item.Others.ToString();
                        PhysicalAbuse = item.PhysicalAbuse.ToString() == "0" ? "" : item.PhysicalAbuse.ToString();
                        //PowerIssues = item.PowerIssues.ToString() == "0" ? "" : item.PowerIssues.ToString();
                        //ReturnToStock = item.ReturnToStock.ToString() == "0" ? "" : item.ReturnToStock.ToString();
                        //ScreenIssues = item.ScreenIssues.ToString() == "0" ? "" : item.ScreenIssues.ToString();
                        //ShippingError = item.ShippingError.ToString() == "0" ? "" : item.ShippingError.ToString();
                        Software = item.Software.ToString() == "0" ? "" : item.Software.ToString();
                        
                        rmaHTMLBody = rmaHTMLBody + "<tr><td class='copy10grey' >" + sno + "</td><td class='copy10grey' ><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + CustomerName + "</div></td><td class='copy10grey' align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + ActivationCoverage + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + BatteryPower + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + HardwareIssues + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + LiquidDamage + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + MissingParts + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + Others + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + PhysicalAbuse + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + Software + "</div></td><td class='copy10grey' align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:yellow;height:20px'><b>" + item.Total + "</b></div></td></tr>";

                        i = i + 1;

                    }
                    rmaHTML = rmaHTML + rmaHTMLBody + "</table>";

                    divRMA.InnerText = rmaHTML;
                    divRMA.InnerHtml = rmaHTML;
                }
                else
                {

                    // Session["POStatus"] = null;
                    lblCount.Text = string.Empty;
                    // gvPO.DataSource = null;
                    lblMsg.Text = "No records found";
                }
            }
            else if (summaryBy == "Disposition")
            {
                List<CustomerRmaDisposition> rmaList = rmaReportOperation.GetCustomerRmaDispositionSummary(companyID, fromDate, toDate, userID, summaryBy);
                if (rmaList != null && rmaList.Count > 0)
                {
                    lblCount.Text = "<strong>Total Customers:</strong> " + Convert.ToString(rmaList.Count - 1);

                    string rmaHTML = "<table width='100%' border='1' bordercolor='#839abf'><tr><td class='buttonlabel' >S.No.</td><td class='buttonlabel' >Customer Name</td><td class='buttonlabel' >Credit</td><td class='buttonlabel' >Discarded</td><td class='buttonlabel' >Repair</td><td class='buttonlabel' >Replaced</td><td class='buttonlabel' >RMA Total</td></tr>";
                    string rmaHTMLBody = string.Empty;
                    string sno = string.Empty;
                    string Credit = string.Empty;
                    string Discarded = string.Empty;
                    //string ReturnToStock = string.Empty;

                    string CustomerName = string.Empty;
                    string Repair = string.Empty;
                    string Replaced = string.Empty;
                    //string Cancelled = string.Empty;

                    foreach (CustomerRmaDisposition item in rmaList)
                    {
                        fontweight = item.CustomerName == "zzTotal" ? "bold" : "normal";
                        backgroundcolor = item.CustomerName == "zzTotal" ? "yellow" : "";
                        sno = item.CustomerName == "zzTotal" ? "" : i.ToString();
                        CustomerName = item.CustomerName == "zzTotal" ? "RMA Total" : item.CustomerName;

                        Credit = item.Credit.ToString() == "0" ? "" : item.Credit.ToString();
                        Discarded = item.Discarded.ToString() == "0" ? "" : item.Discarded.ToString();
                        Repair = item.Repair.ToString() == "0" ? "" : item.Repair.ToString();
                        Replaced = item.Replaced.ToString() == "0" ? "" : item.Replaced.ToString();
                        //ExternalESN = item.ExternalESN.ToString() == "0" ? "" : item.ExternalESN.ToString();
                        //ReturnToStock = item.ReturnToStock.ToString() == "0" ? "" : item.ReturnToStock.ToString();

                        rmaHTMLBody = rmaHTMLBody + "<tr><td class='copy10grey' >" + sno + "</td><td class='copy10grey' ><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + CustomerName + "</div></td><td class='copy10grey' align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + Credit + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + Discarded + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + Repair + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + Replaced + "</div></td><td class='copy10grey' align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:yellow;height:20px'><b>" + item.Total + "</b></div></td></tr>";

                        i = i + 1;

                    }
                    rmaHTML = rmaHTML + rmaHTMLBody + "</table>";

                    divRMA.InnerText = rmaHTML;
                    divRMA.InnerHtml = rmaHTML;
                }
                else
                {

                    // Session["POStatus"] = null;
                    lblCount.Text = string.Empty;
                    // gvPO.DataSource = null;
                    lblMsg.Text = "No records found";
                }
            }
            else if (summaryBy == "ShipmentPaidBy")
            {
                List<CustomerRmaShipmentPaidBy> rmaList = rmaReportOperation.GetCustomerRmaShipmentPaidBySummary(companyID, fromDate, toDate, userID, summaryBy);
                if (rmaList != null && rmaList.Count > 0)
                {
                    lblCount.Text = "<strong>Total Customers:</strong> " + Convert.ToString(rmaList.Count - 1);

                    string rmaHTML = "<table width='100%' border='1' bordercolor='#839abf'><tr><td width='2%' class='buttonlabel' >S.No.</td><td class='buttonlabel' >Customer Name</td><td class='buttonlabel' >Customer</td><td class='buttonlabel' >Internal</td><td class='buttonlabel' >RMA Total</td></tr>";
                    string rmaHTMLBody = string.Empty;
                    string sno = string.Empty;
                    string Customer = string.Empty;
                    string Internal = string.Empty;
                   
                    string CustomerName = string.Empty;

                    foreach (CustomerRmaShipmentPaidBy item in rmaList)
                    {
                        fontweight = item.CustomerName == "zzTotal" ? "bold" : "normal";
                        backgroundcolor = item.CustomerName == "zzTotal" ? "yellow" : "";
                        sno = item.CustomerName == "zzTotal" ? "" : i.ToString();
                        CustomerName = item.CustomerName == "zzTotal" ? "RMA Total" : item.CustomerName;

                        Customer = item.Customer.ToString() == "0" ? "" : item.Customer.ToString();
                        Internal = item.Internal.ToString() == "0" ? "" : item.Internal.ToString();
                        
                        rmaHTMLBody = rmaHTMLBody + "<tr><td class='copy10grey' >" + sno + "</td><td class='copy10grey' ><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + CustomerName + "</div></td><td class='copy10grey' align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + Customer + "</div></td><td class='copy10grey'  align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:" + backgroundcolor + "; height:20px'>" + Internal + "</div></td><td class='copy10grey' align='right'><div style='width:100%;font-weight:" + fontweight + "; background-color:yellow;height:20px'><b>" + item.Total + "</b></div></td></tr>";

                        i = i + 1;

                    }
                    rmaHTML = rmaHTML + rmaHTMLBody + "</table>";

                    divRMA.InnerText = rmaHTML;
                    divRMA.InnerHtml = rmaHTML;
                }
                else
                {
                    // Session["POStatus"] = null;
                    lblCount.Text = string.Empty;
                    // gvPO.DataSource = null;
                    lblMsg.Text = "No records found";
                }
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divRMA.InnerText = string.Empty;
            divRMA.InnerHtml = string.Empty;
            lblCompany.Text = string.Empty;
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            txtDateFrom.Text = string.Empty;
            txtDateTo.Text = string.Empty;
            dpCompany.SelectedIndex = 0;
            ddlSummaryBy.SelectedIndex = 0;
        }
    }
}