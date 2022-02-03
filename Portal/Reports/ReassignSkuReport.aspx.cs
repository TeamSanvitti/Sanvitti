using System;
using System.Data;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace avii.Reports
{
    public partial class ReassignSkuReport : System.Web.UI.Page
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
                        //companyID = userInfo.CompanyGUID;
                    }

                }
                else
                {
                    BindCustomer();
                    lblCompany.Text = string.Empty;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindReassignESN();
        }
        private void BindReassignESN()
        {
            SV.Framework.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.Inventory.InventoryReportOperation>();

            int companyID = 0;
            string esn, sku;
            DateTime fromDate, toDate;
            fromDate = toDate = DateTime.MinValue;
            esn = sku = string.Empty;
            lblMsg.Text = string.Empty;
            //if(ddlDuration != null && ddlDuration.se
            //duration = Convert.ToInt32(ddlDuration.SelectedValue);
            //DateTime dt = new DateTime(;

            esn = txtESN.Text.Trim();
            sku = txtSKU.Text.Trim();

            if (txtFromDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtFromDate.Text, out dt))
                    fromDate = dt;
                else
                    throw new Exception("Date From does not have correct format(MM/DD/YYYY)");
            }
            if (txtToDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtToDate.Text, out dt))
                    toDate = dt;
                else
                    throw new Exception("Date To does not have correct format(MM/DD/YYYY)");
            }

            //if (fromDate != DateTime.MinValue && toDate == DateTime.MinValue)
            //    lblDate.Text = "<b>Fulfillment Date Range:</b> " + fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + " &nbsp; &nbsp; " + "<b>RMA Date Range:</b> " + fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + "<b> &nbsp; &nbsp; Unused ESN Date Range:</b> " + fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString();
            //else
            //    if (fromDate != DateTime.MinValue && toDate != DateTime.MinValue)
            //        lblDate.Text = "<b>Fulfillment Date Range:</b> " + fromDate.ToShortDateString() + " - " + toDate.ToShortDateString() + " &nbsp; &nbsp; " + "<b>RMA Date Range:</b> " + fromDate.ToShortDateString() + " - " + toDate.ToShortDateString() + "<b> &nbsp; &nbsp; Unused ESN Date Range:</b> " + fromDate.ToShortDateString() + " - " + toDate.ToShortDateString();
            //    else
            //        if (fromDate == DateTime.MinValue && toDate == DateTime.MinValue)
            //            lblDate.Text = "<b>Fulfillment Date Range:</b> " + DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + " &nbsp; &nbsp; " + "<b>RMA Date Range:</b> " + DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + "<b> &nbsp; &nbsp; Unused ESN Date Range:</b> " + DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString();

            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {

                    companyID = userInfo.CompanyGUID;
                }
            }
            else
            {
                if (dpCompany.SelectedIndex > 0)
                {
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);

                }
            }

            List<ReassignSKU> reassignSKUList = inventoryReportOperation.GetReassignSkuReport(companyID, fromDate, toDate, esn, sku);
            if (reassignSKUList != null && reassignSKUList.Count > 0)
            {
                btnDownload.Visible = true;
                gvESN.DataSource = reassignSKUList;
                gvESN.DataBind();
                lblCount.Text = "Total count: " + reassignSKUList.Count;
                Session["ReassignSKU"] = reassignSKUList;
            }
            else
            {
                btnDownload.Visible = false;
                lblCount.Text = string.Empty;
                lblMsg.Text = "No record found";
                gvESN.DataSource = null;
                gvESN.DataBind();
            }
                


        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            gvESN.DataSource = null;
            gvESN.DataBind();
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            //ddlDuration.SelectedIndex = 0;
            //lblDate.Text = string.Empty;
            btnDownload.Visible = false;

        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvESN.PageIndex = e.NewPageIndex;
            if (Session["ReassignSKU"] != null)
            {
                List<ReassignSKU> skuEsnList = (List<ReassignSKU>)Session["ReassignSKU"];

                gvESN.DataSource = skuEsnList;
                gvESN.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            DownloadReport();
        }
        private void DownloadReport()
        {

            string downLoadPath = string.Empty;
            downLoadPath = System.Configuration.ConfigurationManager.AppSettings["FullfillmentFilesStoreage"].ToString();
            if (Session["ReassignSKU"] != null)
            {
                try
                {
                    List<ReassignSKU> esnList = (Session["ReassignSKU"]) as List<ReassignSKU>;

                    if (esnList != null && esnList.Count > 0)
                    {
                        string path = Server.MapPath(downLoadPath).ToString();
                        string fileName = "ReassignSKU" + Session.SessionID + ".csv";
                        bool found = true;
                        //System.IO.FileInfo file = null;
                        //file = new System.IO.FileInfo(path + fileName);
                        //if (file.Exists)
                        //{
                        //    file.Delete();
                        //}

                        //System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        //sb.Append("ESN,OldSKUCategory,OldSKU,NewSKUCategory,NewESN,ChangeDate,CreatedBy,FulfillmentNumber,RmaNumber\r\n");

                        //foreach (ReassignSKU esnObj in esnList)
                        //{
                        //    sb.Append(esnObj.ESN + ","
                        //         + esnObj.OldSKUCategory + ","
                        //                + esnObj.OldSKU + ","
                        //                 + esnObj.NewSKUCategory + ","
                        //                + esnObj.NewSKU + ","
                        //                + esnObj.ChangeDate.ToShortDateString() + ","
                        //                + esnObj.CustomerName + ","
                        //                + esnObj.FulfillmentNumber + ","
                        //                + esnObj.RMANumber 
                        //                + "\r\n");

                        //    found = true;
                        //}
                        string string2CSV = esnList.ToCSV();
                        //try
                        //{
                        //    using (StreamWriter sw = new StreamWriter(file.FullName))
                        //    {
                        //        sw.WriteLine(sb.ToString());
                        //        sw.Flush();
                        //        sw.Close();
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    lblMsg.Text = ex.Message;
                        //}

                        if (found)
                        {
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                            Response.Charset = "";
                            Response.ContentType = "application/text";
                            Response.Output.Write(string2CSV);
                            Response.Flush();
                            Response.End();
                            //Response.Clear();
                            //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                            //Response.ContentType = "application/octet-stream";
                            //Response.WriteFile(file.FullName);
                            //Response.End();
                        }
                    }
                    else
                    { lblMsg.Text = "No records found"; }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                }

            }
            else
                lblMsg.Text = "Session expired!";
        }

        
    }
}