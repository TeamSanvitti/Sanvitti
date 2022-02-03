using System;
using System.Data;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Reports
{
    public partial class ProductWiseSKUsWithEsnDetail : System.Web.UI.Page
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
                //btnDownload.Visible = false;

                BindProucts();
                    
            }
        }
        private void BindProucts()
        {
            InventoryList inventoryList = new InventoryList();
            inventoryList.CurrentInventory = avii.Classes.PurchaseOrder.GetInventory(0, 0).CurrentInventory;
            if (inventoryList != null && inventoryList.CurrentInventory != null && inventoryList.CurrentInventory.Count > 0)
            {
                dpItems.Items.Clear();
                foreach (avii.Classes.clsInventory invItem in avii.Classes.PurchaseOrder.GetInventory(0).CurrentInventory)
                {
                    dpItems.Items.Add(new ListItem(invItem.ItemCode, invItem.ItemID.ToString()));
                }

                dpItems.Visible = true;
            }
            else
            {
                lblMsg.Text = "Inventory items are missing, please contact administrator";
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindReassignESN();
        }
        private void BindReassignESN()
        {
            int itemGUID = 0;
            string esn, sku;
            DateTime fromDate, toDate;
            fromDate = toDate = DateTime.MinValue;
            esn = sku = string.Empty;
            lblMsg.Text = string.Empty;
            
            itemGUID = Convert.ToInt32(dpItems.SelectedValue);

            if (itemGUID > 0)
            {
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

                

                List<avii.Classes.ProductSKUsInfo> skuList = avii.Classes.ReportOperations.GetProductWiseSKUsEsnSummary(itemGUID, fromDate, toDate);
                if (skuList != null && skuList.Count > 0)
                {
                    //btnDownload.Visible = true;
                    gvESN.DataSource = skuList;
                    gvESN.DataBind();
                    lblCount.Text = "Total count: " + skuList.Count;
                    Session["ProductSKUsInfo"] = skuList;
                }
                else
                {
                    //btnDownload.Visible = false;
                    lblCount.Text = string.Empty;
                    lblMsg.Text = "No record found";
                    gvESN.DataSource = null;
                    gvESN.DataBind();
                }

            }
            else
            {
                lblMsg.Text = "Please select a product";
                lblCount.Text = string.Empty;
                
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
            //btnDownload.Visible = false;

        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvESN.PageIndex = e.NewPageIndex;
            if (Session["ProductSKUsInfo"] != null)
            {
                List<ProductSKUsInfo> skuEsnList = (List<ProductSKUsInfo>)Session["ProductSKUsInfo"];

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
            if (Session["ProductSKUsInfo"] != null)
            {
                try
                {
                    List<ReassignSKU> esnList = (Session["ReassignSKU"]) as List<ReassignSKU>;

                    if (esnList != null && esnList.Count > 0)
                    {
                        string path = Server.MapPath(downLoadPath).ToString();
                        string fileName = "ReassignSKU" + Session.SessionID + ".csv";
                        bool found = false;
                        System.IO.FileInfo file = null;
                        file = new System.IO.FileInfo(path + fileName);
                        if (file.Exists)
                        {
                            file.Delete();
                        }

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        sb.Append("ESN,OldSKU,NewESN,ChangeDate,CreatedBy,FulfillmentNumber,RmaNumber\r\n");

                        foreach (avii.Classes.ReassignSKU esnObj in esnList)
                        {
                            sb.Append(esnObj.ESN + ","
                                        + esnObj.OldSKU + ","
                                        + esnObj.NewSKU + ","
                                        + esnObj.ChangeDate.ToShortDateString() + ","
                                        + esnObj.CustomerName + ","
                                        + esnObj.FulfillmentNumber + ","
                                        + esnObj.RMANumber
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