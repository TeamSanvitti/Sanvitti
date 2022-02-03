using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace avii.Reports
{
    public partial class PurchaseOrderTrackingReport : System.Web.UI.Page
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
                    //BindShipViaCode();

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

            BindPO();
        }
        public void BindPO()
        {
            int companyID = 0;
            string trackingNumber, shipViaCode, fulfillmentNumber;
            trackingNumber = shipViaCode = fulfillmentNumber = null;

            DateTime fromDate, toDate;
            lblMsg.Text = string.Empty;

            string sortExpression = "PO_ID";
            string sortDirection = "DESC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;


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

                //trackingNumber = txtTrackingNumber.Text.Trim().Length > 0 ? txtTrackingNumber.Text.Trim() : null;
                fulfillmentNumber = txtPoNum.Text.Trim().Length > 0 ? txtPoNum.Text.Trim() : null;
                //shipViaCode = ddlShipVia.SelectedIndex > 0 ? ddlShipVia.SelectedValue : null;

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
                     && string.IsNullOrEmpty(fulfillmentNumber))
                {
                    lblMsg.Text = "Please select the search criteria";
                }
                else
                    PopulateData(companyID, fromDate, toDate, fulfillmentNumber);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void imgPrint_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int lineNumber = Convert.ToInt32(e.CommandArgument);
                ViewState["linenumber"] = lineNumber;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp1", "<script language='javascript'>Refresh();</script>", false);

                //    string shipMethod = string.Empty;
                //    string labelBase64 = ShippingLabelOperation.GetLabelBase64(lineNumber, out shipMethod);
                //    if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), shipMethod))
                //    {
                //        if (!string.IsNullOrWhiteSpace(labelBase64))
                //        {
                //            // imgLabel.ImageUrl = "data:image;base64," + labelBase64;
                //            // RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");
                //            byte[] imageBytes = Convert.FromBase64String(labelBase64);
                //            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);

                //            image.ScaleToFit(320, 520);

                //            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                //            {
                //                Document document = new Document();
                //                iTextSharp.text.Rectangle envelope = new iTextSharp.text.Rectangle(320, 500);
                //                document.SetPageSize(envelope);
                //                document.SetMargins(0, 0, 0, 0);

                //                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                //                document.Open();

                //                document.Add(image);
                //                document.Close();
                //                var bytes = memoryStream.ToArray();
                //                // memoryStream.Close();

                //                string fileType = ".pdf";
                //                string filename = DateTime.Now.Ticks + fileType;
                //                Response.Clear();
                //                //Response.ContentType = "application/pdf";
                //                Response.ContentType = "application/octet-stream";
                //                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                //                Response.Buffer = true;
                //                Response.Clear();
                //                // var bytes = memSt.ToArray();
                //                Response.OutputStream.Write(bytes, 0, bytes.Length);
                //                Response.OutputStream.Flush();
                //            }
                //        }
                //    }
                //    else
                //    {
                //       //// RegisterStartupScript("jsUnblockDialog", "closeLabelDialog();");
                //        if (!string.IsNullOrWhiteSpace(labelBase64))
                //        {
                //            // ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenPDF('" + labelBase64 + "')</script>", false);

                //            string fileType = ".pdf";
                //            string filename = DateTime.Now.Ticks + fileType;
                //            Response.Clear();
                //            //Response.ContentType = "application/pdf";
                //            Response.ContentType = "application/octet-stream";
                //            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                //            Response.Buffer = true;
                //            Response.Clear();
                //            var bytes = Convert.FromBase64String(labelBase64);
                //            Response.OutputStream.Write(bytes, 0, bytes.Length);
                //            Response.OutputStream.Flush();

                //            //string fileType = ".pdf";
                //            //string filename = DateTime.Now.Ticks + fileType;
                //            //Response.Clear();
                //            ////Response.ContentType = "application/pdf";
                //            //Response.ContentType = "application/octet-stream";
                //            //Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                //            //Response.Buffer = true;
                //            //Response.Clear();
                //            //var bytes = Convert.FromBase64String(labelBase64);
                //            //Response.OutputStream.Write(bytes, 0, bytes.Length);
                //            //Response.OutputStream.Flush();
                //        }
                //    }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void btnhdPrintlabel_Click(object sender, EventArgs e)
        {
            if (ViewState["linenumber"] != null)
            {
                PrintLabel(Convert.ToInt32(ViewState["linenumber"]));
            }
        }
        private void PrintLabel(int lineNumber)
        {
            try
            {
                SV.Framework.Fulfillment.ShippingLabelOperation shippingLabelOperation = SV.Framework.Fulfillment.ShippingLabelOperation.CreateInstance<SV.Framework.Fulfillment.ShippingLabelOperation>();

                //int lineNumber = Convert.ToInt32(e.CommandArgument);
                float width = 320, height = 520, envHeight = 500;
                string shipMethod = string.Empty, ShipPackage = string.Empty;
                string labelBase64 = shippingLabelOperation.GetLabelBase64(lineNumber, out shipMethod, out ShipPackage);
                if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), shipMethod))
                {
                    if (!string.IsNullOrWhiteSpace(labelBase64))
                    {
                        if (shipMethod.ToLower() == "first" && ShipPackage.ToString().ToLower() == "letter")
                        {
                            width = 500;
                            height = 320;
                            envHeight = 320;
                        }
                        // imgLabel.ImageUrl = "data:image;base64," + labelBase64;
                        // RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");
                        byte[] imageBytes = Convert.FromBase64String(labelBase64);
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);

                        image.ScaleToFit(width, height);

                        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                        {
                            Document document = new Document();
                            iTextSharp.text.Rectangle envelope = new iTextSharp.text.Rectangle(width, envHeight);
                            document.SetPageSize(envelope);
                            document.SetMargins(0, 0, 0, 0);

                            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                            document.Open();

                            document.Add(image);
                            document.Close();
                            var bytes = memoryStream.ToArray();
                            // memoryStream.Close();

                            string fileType = ".pdf";
                            string filename = DateTime.Now.Ticks + fileType;
                            Response.Clear();
                            //Response.ContentType = "application/pdf";
                            Response.ContentType = "application/octet-stream";
                            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                            Response.Buffer = true;
                            Response.Clear();
                            // var bytes = memSt.ToArray();
                            Response.OutputStream.Write(bytes, 0, bytes.Length);
                            Response.OutputStream.Flush();
                        }
                    }
                }
                else
                {
                    //// RegisterStartupScript("jsUnblockDialog", "closeLabelDialog();");
                    if (!string.IsNullOrWhiteSpace(labelBase64))
                    {
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenPDF('" + labelBase64 + "')</script>", false);

                        string fileType = ".pdf";
                        string filename = DateTime.Now.Ticks + fileType;
                        Response.Clear();
                        //Response.ContentType = "application/pdf";
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                        Response.Buffer = true;
                        Response.Clear();
                        var bytes = Convert.FromBase64String(labelBase64);
                        Response.OutputStream.Write(bytes, 0, bytes.Length);
                        Response.OutputStream.Flush();

                        //string fileType = ".pdf";
                        //string filename = DateTime.Now.Ticks + fileType;
                        //Response.Clear();
                        ////Response.ContentType = "application/pdf";
                        //Response.ContentType = "application/octet-stream";
                        //Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                        //Response.Buffer = true;
                        //Response.Clear();
                        //var bytes = Convert.FromBase64String(labelBase64);
                        //Response.OutputStream.Write(bytes, 0, bytes.Length);
                        //Response.OutputStream.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
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
                    lnkESN.OnClientClick = "openDialogAndBlock('Line Items', '" + lnkESN.ClientID + "')";
                }
                ImageButton imgPrint = (ImageButton)e.Row.FindControl("imgPrint");

                if (Session["userInfo"] != null)
                {
                    avii.Classes.UserInfo objUserInfo = Session["userInfo"] as avii.Classes.UserInfo;

                    if (objUserInfo.CompanyGUID == 470)
                    {
                        imgPrint.Visible = false;
                        lnkESN.Visible = false;
                        e.Row.Cells[10].Visible = false;
                    }
                }
              //  imgPrint.OnClientClick = "openLabelDialogAndBlock('Shipping Label', '" + imgPrint.ClientID + "')";



            }

        }
        protected void imgViewESN_Click(object sender, CommandEventArgs e)
        {
            lblEsnMsg.Text = string.Empty;
            try
            {
                string trackings = e.CommandArgument.ToString();
                string[] arr = trackings.Split(',');
                if (arr.Length > 1)
                {
                    int poid = Convert.ToInt32(arr[0]);
                    string trackingNumber = arr[1];
                    lblTracking.Text = trackingNumber;
                    BindESN(poid, trackingNumber);

                    RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
                }
            }
            catch (Exception ex)
            {
                lblEsnMsg.Text = ex.Message;
            }


        }
        public string ContainerID { get; set; }
        protected void lnkGenerateLabel_Click(object sender, CommandEventArgs e)
        {
            lblEsnMsg.Text = string.Empty;
            SV.Framework.Fulfillment.PurchaseOrder purchaseOrder = SV.Framework.Fulfillment.PurchaseOrder.CreateInstance<SV.Framework.Fulfillment.PurchaseOrder>();

            try
            {
                int po_id = Convert.ToInt32(e.CommandArgument);
                if (po_id > 0)
                {
                   
                    List<SV.Framework.Common.LabelGenerator.Model> models = new List<SV.Framework.Common.LabelGenerator.Model>();
                    List<BasePurchaseOrderItem> esnList = purchaseOrder.GetPurchaseOrderItemList(po_id);

                    //List<avii.Classes.BasePurchaseOrderItem> esnList = Session["poa"] as List<avii.Classes.BasePurchaseOrderItem>;
                    SV.Framework.Common.LabelGenerator.ESNLabelOperation slabel = new SV.Framework.Common.LabelGenerator.ESNLabelOperation();
                    if (esnList != null && esnList.Count > 0)
                    {
                        foreach (BasePurchaseOrderItem poDetail in esnList)
                        {
                            if (!string.IsNullOrWhiteSpace(poDetail.ESN))
                                models.Add(new SV.Framework.Common.LabelGenerator.Model(poDetail.ItemCode, poDetail.ESN, poDetail.LTEICCID, poDetail.UPC));
                        }

                        var memSt = slabel.ExportToPDF(models);
                        if (memSt != null)
                        {
                            string fileType = ".pdf";
                            string filename = DateTime.Now.Ticks + fileType;
                            Response.Clear();
                            //Response.ContentType = "application/pdf";
                            Response.ContentType = "application/octet-stream";
                            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                            Response.Buffer = true;
                            Response.Clear();
                            var bytes = memSt.ToArray();
                            Response.OutputStream.Write(bytes, 0, bytes.Length);
                            Response.OutputStream.Flush();

                            //string imagePath = "~/pdffiles/";
                            //string fileType = ".pdf";
                            //string fileDirctory = HttpContext.Current.Server.MapPath(imagePath);
                            //string filename = DateTime.Now.Ticks + fileType;
                            //slabel.RetriveLabelFromMemory(memSt, filename, fileDirctory);


                            //string baseurl = System.Configuration.ConfigurationManager.AppSettings["url"].ToString();
                            //string filePath = baseurl + "/pdffiles/" + filename;
                            //lblPOA.Text = "Generated successfully";
                            ////ScriptManager.RegisterStartupScript(this, this.GetType(), "new window", "javascript:window.open(\""+ filePath + "\", \"_newtab\");", true);
                            //lnk_Print.HRef = filePath;
                            // lnk_Print.Visible = true;
                        }
                        else
                            lblEsnMsg.Text = "Technical error!";
                    }
                }
                else
                    lblEsnMsg.Text = "Session expired!";
            }
            catch (Exception ex)
            {
                lblEsnMsg.Text = ex.Message;
            }


        }
        private void BindESN(int poid, string trackingNumber)
        {
            lblPO.Text = string.Empty;
            List <PurchaseOrderShipmentDB> shipments = new List<PurchaseOrderShipmentDB>();
            PurchaseOrderShipmentDB shipment = new PurchaseOrderShipmentDB();
            if (HttpContext.Current.Session["shipmentDB"] != null)
            {
                List<PurchaseOrderShipmentDB> poList = HttpContext.Current.Session["shipmentDB"] as List<PurchaseOrderShipmentDB>;
                var poInfoList = (from item in poList where item.TrackingNumber.Equals(trackingNumber) select item).ToList();
                if (poInfoList != null && poInfoList.Count > 0)
                {
                    ContainerID = poInfoList[0].ContainerID;
                    lblPO.Text = poInfoList[0].PO_NUM;
                    rptESN.DataSource = poInfoList;
                    rptESN.DataBind();
                }
                else
                {
                    lblEsnMsg.Text = "No record found!";
                    rptESN.DataSource = null;
                    rptESN.DataBind();
                }
            }
            else
            {
                rptESN.DataSource = null;
                rptESN.DataBind();
                lblEsnMsg.Text = "Session expired!";
            }
        }

        private void DownloadTrackingNumberInfo()
        {

            {
                string downLoadPath = ConfigurationManager.AppSettings["FullfillmentFilesStoreage"].ToString();
                string path = Server.MapPath(downLoadPath).ToString();
                string fileName = Session.SessionID + ".xls";

                bool found = false;
                System.IO.FileInfo file = null;
                file = new System.IO.FileInfo(path + fileName);
                if (file.Exists)
                {
                    file.Delete();
                }

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                this.EnableViewState = false;




                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                
                
                using (StringWriter sw = new StringWriter())
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a table to contain the grid
                    Table table = new Table();

                    //  Gridline to box the cells
                    table.GridLines = System.Web.UI.WebControls.GridLines.Both;

                    string[] columns = { "FulfillmentNumber", "FulfillmentDate", "Ship_Via", "TrackingNumber", "ShipDate", "ShipmentType", "AcknowledgmentSent", "ESN", "ICC_ID", "BatchNumber", "ShipPackage", "Weight", "Price", "ContainerID", "ContactName", "FulfillmentType" };


                    TableRow tRow = new TableRow();
                    TableCell tCell;
                    foreach (string name in columns)
                    {
                        tCell = new TableCell();
                        tCell.Text = name;
                        tRow.Cells.Add(tCell);
                    }
                    table.Rows.Add(tRow);

                    List<PurchaseOrderShipmentDB> shipments = Session["shipmentDB"] as List<PurchaseOrderShipmentDB>;
                    if (shipments != null && shipments.Count > 0)
                    {
                        foreach (PurchaseOrderShipmentDB item in shipments)
                        {
                            try
                            {
                                tRow = new TableRow();
                                //tCell = new TableCell();
                                //tCell.Text = "H";
                                //tRow.Cells.Add(tCell);



                                tCell = new TableCell();
                                tCell.Text = item.PO_NUM;
                                tRow.Cells.Add(tCell);


                                tCell = new TableCell();
                                tCell.Text = item.PO_Date;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = item.ShipMethod;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                //string ss = ",\"0\"";
                                //tCell.Text = "=Text(" + item.TrackingNumber + ",\"0\")";//// !string.IsNullOrEmpty(item.TrackingNumber) ? "#" + item.TrackingNumber : item.TrackingNumber;
                                //tCell.Text = "=Text('" + item.TrackingNumber + @"',""0"")";
                                tCell.Text =  !string.IsNullOrEmpty(item.TrackingNumber) ? "#" + item.TrackingNumber : item.TrackingNumber;

                                //tCell.Text = "'" +item.TrackingNumber + "'";
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = item.ShipDate;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = item.ShipmentType;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = item.AcknowledgmentSent;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                //tCell.Attributes()
                                tCell.Text = !string.IsNullOrEmpty(item.ESN) ? "#" + item.ESN : item.ESN;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = !string.IsNullOrEmpty(item.ICC_ID) ? "#" + item.ICC_ID : item.ICC_ID;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = item.BatchNumber;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = item.ShipPackage;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = item.ShipWeight.ToString();
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = item.ShipPrice.ToString();
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = !string.IsNullOrEmpty(item.ContainerID) ? "#" + item.ContainerID : item.ContainerID; //item.ContainerID;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = item.ContactName;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = item.PoType;
                                tRow.Cells.Add(tCell);

                                table.Rows.Add(tRow);

                            }
                            catch (Exception ex)
                            {
                                System.Threading.Thread.Sleep(5000);
                                Response.Write(ex.Message);
                            }
                        }
                    }
                  
                    //  Htmlwriter into the table
                    table.RenderControl(htw);

                    //  Htmlwriter into the response
                    //  HttpContext.Current.Response.Write(sw.ToString());
                    // HttpContext.Current.Response.End();
                    Response.Charset = "";
                    // Response.ContentType = "application/text";
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();

                }



            }
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (Session["shipmentDB"] != null)
            {
                DownloadTrackingNumberInfo();
                //List<PurchaseOrderShipmentCSV> shipmentCSVs = new List<PurchaseOrderShipmentCSV>();
                //PurchaseOrderShipmentCSV shipment = null;
                //List<PurchaseOrderShipmentDB> shipments = Session["shipmentDB"] as List<PurchaseOrderShipmentDB>;
                //if (shipments != null && shipments.Count > 0)
                //{
                //    foreach(PurchaseOrderShipmentDB item in shipments)
                //    {
                //        shipment = new PurchaseOrderShipmentCSV();
                //        shipment.AcknowledgmentSent = item.AcknowledgmentSent;
                //        shipment.BatchNumber = item.BatchNumber;
                //        shipment.ESN = !string.IsNullOrEmpty(item.ESN) ? "=" + item.ESN : item.ESN; //item.ESN;
                //        shipment.FulfillmentDate = item.PO_Date;
                //        shipment.FulfillmentNumber = item.PO_NUM;
                //      //  shipment.FulfillmentStatus = item.PO_Status;
                //        shipment.ICC_ID = !string.IsNullOrEmpty(item.ICC_ID) ? "=" + item.ICC_ID : item.ICC_ID; //item.ICC_ID;
                //       // shipment.Line_no = item.Line_no;
                //       // shipment.Qty = item.Qty;
                //        shipment.ShipDate = item.ShipDate;
                //        shipment.ShipmentType = item.ShipmentType;
                //        shipment.Ship_Via = item.Ship_Via;
                //      //  shipment.SKU = item.SKU;
                //        shipment.TrackingNumber = !string.IsNullOrEmpty(item.TrackingNumber) ? "="+ item.TrackingNumber: item.TrackingNumber;
                //        shipment.FulfillmentType = item.PoType;
                //        shipment.ContactName = item.ContactName;
                //        shipment.Price = item.ShipPrice;
                //        shipment.ShipPackage = item.ShipPackage;
                //        shipment.Weight = item.ShipWeight;
                //        shipment.ContainerID = item.ContainerID;
                //        shipmentCSVs.Add(shipment);
                //    }
                //    string string2CSV = shipmentCSVs.ToCSV();

                //    Response.Clear();
                //    Response.Buffer = true;
                //    Response.AddHeader("content-disposition", "attachment;filename=ShipmentExport.csv");
                //    Response.Charset = "";
                //    Response.ContentType = "application/text";
                //    Response.Output.Write(string2CSV);
                //    Response.Flush();
                //    Response.End();
                //}
            }
        }

        private void PopulateData(int companyID, DateTime fromDate, DateTime toDate, string fulfillmentNumber)
        {
            SV.Framework.Fulfillment.FulfillmentReportOperation fulfillmentReportOperation = SV.Framework.Fulfillment.FulfillmentReportOperation.CreateInstance<SV.Framework.Fulfillment.FulfillmentReportOperation>();

            decimal totalShipPrice = 0;
           // PurchaseOrderShipmentTracking service = new PurchaseOrderShipmentTracking();
            List<PurchaseOrderShipmentDB> shipmentDB = default;
            List<PurchaseOrderShipmentDB> poList = fulfillmentReportOperation.GetPurchaseOrderShipmentReport(fulfillmentNumber, fromDate, toDate, companyID, out totalShipPrice, out shipmentDB);
            HttpContext.Current.Session["shipmentDB"] = shipmentDB;

            if (poList != null && poList.Count > 0)
            {
                btnDownload.Visible = true;
                lnkShipSummary.Visible = true;
                lnkSKUSumary.Visible = true;
                Session["poList"] = poList;
                gvPO.DataSource = poList;
                lblCount.Text = "<strong>Total Shipment:</strong> " + Convert.ToString(poList.Count) + ", &nbsp;&nbsp; &nbsp;&nbsp; <strong>Total Shipment Cost:</strong> $" + Convert.ToString(totalShipPrice);
                
                lblMsg.Text = string.Empty;

            }
            else
            {
                lnkSKUSumary.Visible = false;
                lnkShipSummary.Visible = false;
                btnDownload.Visible = false;
                Session["poList"] = null;
                lblCount.Text = string.Empty;
                //gridView_PageIndexChanging.DataSource = null;
                lblMsg.Text = "No records found";
            }

            gvPO.DataBind();
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //int poid = Convert.ToInt32(gvPO.DataKeys[row.RowIndex].Value);
                int poid = Convert.ToInt32(gvPO.DataKeys[e.Row.RowIndex].Value);
                GridView gvTracking = e.Row.FindControl("gvTracking") as GridView;
                BindTracking(poid, gvTracking);
            }
        }
        protected void Show_Hide_Shipment(object sender, EventArgs e)
        {
            ImageButton imgShowHide = (sender as ImageButton);
            GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
            if (imgShowHide.CommandArgument == "Show")
            {
                row.FindControl("pnlTracking").Visible = true;
                imgShowHide.CommandArgument = "Hide";
                imgShowHide.ImageUrl = "~/images/minus.png";
                int poid = Convert.ToInt32(gvPO.DataKeys[row.RowIndex].Value);
                GridView gvTracking = row.FindControl("gvTracking") as GridView;
                BindTracking(poid, gvTracking);
            }
            else
            {
                row.FindControl("pnlTracking").Visible = false;
                imgShowHide.CommandArgument = "Show";
                imgShowHide.ImageUrl = "~/images/plus.png";
            }
        }
        private void BindTracking(int poid, GridView gvTracking)
        {
            List<PurchaseOrderShipmentDB> shipments = new List<PurchaseOrderShipmentDB>();
            PurchaseOrderShipmentDB shipment = new PurchaseOrderShipmentDB();
            if (HttpContext.Current.Session["shipmentDB"] != null)
            {
                List<PurchaseOrderShipmentDB> poList = HttpContext.Current.Session["shipmentDB"] as List<PurchaseOrderShipmentDB>;
                var poInfoList = (from item in poList where item.PO_ID.Equals(poid) select item).ToList();
                if (poInfoList != null && poInfoList.Count > 0)
                {
                    var res = poInfoList.GroupBy(e => new { e.PO_NUM, e.TrackingNumber, e.ShipDate, e.Ship_Via, e.PO_ID, e.ShipmentType, e.LineNumber, e.AcknowledgmentSent, e.ShipPackage, e.ShipPrice,e.ShipWeight,e.ShipMethod });
                    foreach (var val in res)
                    {
                        shipment = new PurchaseOrderShipmentDB()
                        {
                            TrackingNumber = val.Key.TrackingNumber,
                            ShipDate = val.Key.ShipDate,
                            PO_ID = val.Key.PO_ID,
                            ShipmentType = val.Key.ShipmentType,
                            LineNumber = val.Key.LineNumber,
                            ShipWeight = val.Key.ShipWeight,
                            ShipPrice = val.Key.ShipPrice,
                            ShipPackage = val.Key.ShipPackage,
                            Ship_Via = val.Key.Ship_Via,
                            AcknowledgmentSent = val.Key.AcknowledgmentSent,
                            ShipMethod = val.Key.ShipMethod

                        };

                        shipments.Add(shipment);
                       
                    }
                    gvTracking.DataSource = shipments;
                    gvTracking.DataBind();
                }
                else
                {
                    gvTracking.DataSource = null;
                    gvTracking.DataBind();
                }
            }
            else
            {
                gvTracking.DataSource = null;
                gvTracking.DataBind();
                lblMsg.Text = "Session expired!";
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lnkSKUSumary.Visible = false;
            lnkShipSummary.Visible = false;

            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            gvPO.DataSource = null;
            gvPO.DataBind();
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            btnDownload.Visible = false;
          //  txtTrackingNumber.Text = string.Empty;
            txtPoNum.Text = string.Empty;
          //  ddlShipVia.SelectedIndex = 0;
            dpCompany.SelectedIndex = 0;

        }

        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPO.PageIndex = e.NewPageIndex;
            if (Session["poList"] != null)
            {
                List<PurchaseOrderShipmentDB> poList = (List<PurchaseOrderShipmentDB>)Session["poList"];

                gvPO.DataSource = poList;
                gvPO.DataBind();
            }
            else
                BindPO();

        }

        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }
        public List<PurchaseOrderShipmentDB> Sort<TKey>(List<PurchaseOrderShipmentDB> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<PurchaseOrderShipmentDB>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<PurchaseOrderShipmentDB>();
            }
        }
        protected void gvPO_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["poList"] != null)
            {
                List<PurchaseOrderShipmentDB> poList = (List<PurchaseOrderShipmentDB>)Session["poList"];

                if (poList != null && poList.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        poList = Sort<PurchaseOrderShipmentDB>(poList, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        poList = Sort<PurchaseOrderShipmentDB>(poList, SortExp, SortDirection.Descending);
                    }
                    Session["poList"] = poList;
                    gvPO.DataSource = poList;
                    gvPO.DataBind();
                }
            }
        }

        private void BindSKUSummary()
        {
            List<PurchaseOrderShipmentDB> shipments = HttpContext.Current.Session["shipmentDB"] as List<PurchaseOrderShipmentDB>;
            PurchaseOrderShipmentDB shipment = new PurchaseOrderShipmentDB();
            List<SKUSummary> skuSummary = shipments.GroupBy(k => new  { k.SKU, k.CategoryName, k.ItemName }).Select(g => new SKUSummary { SKU = g.Key.SKU, CategoryName = g.Key.CategoryName, ProductName = g.Key.ItemName, Count = g.Count() }).ToList();
            if(skuSummary != null && skuSummary.Count > 0)
            {
                Session["SKUSummary"] = skuSummary;
                rptSKU.DataSource = skuSummary;
                rptSKU.DataBind();
            }
            else
            {
                rptSKU.DataSource = null;
                rptSKU.DataBind();
            }
            
        }
        private void BindShipMethodSummary()
        {
            List<PurchaseOrderShipmentDB> shipments = HttpContext.Current.Session["shipmentDB"] as List<PurchaseOrderShipmentDB>;
            PurchaseOrderShipmentDB shipment = new PurchaseOrderShipmentDB();
            var pos = shipments.GroupBy(k => new { k.PO_NUM, k.ShipMethod, k.ShipPackage, k.ShipPrice }).Select(g => new { PO_NUM = g.Key.PO_NUM, ShipMethod = g.Key.ShipMethod, ShipPackage = g.Key.ShipPackage, ShipPrice = g.Key.ShipPrice }).ToList();
            List<ShipMethodSummary> shipMethodSummary = pos.GroupBy(k => new { k.ShipMethod, k.ShipPackage }).Select(g => new ShipMethodSummary { ShipMethod = g.Key.ShipMethod, ShipPackage = g.Key.ShipPackage, Cost = g.Sum(s => s.ShipPrice) }).ToList();
            if (shipMethodSummary != null && shipMethodSummary.Count > 0)
            {
                Session["ShipMethodSummary"] = shipMethodSummary;

                rptShip.DataSource = shipMethodSummary;
                rptShip.DataBind();
            }
            else
            {
                rptShip.DataSource = null;
                rptShip.DataBind();
            }

        }

        protected void btnSKU_Click(object sender, EventArgs e)
        {
            BindSKUSummary();
            RegisterStartupScript("jsUnblockDialog", "unblockSKUDialog();");
        }

        protected void btnShipMethods_Click(object sender, EventArgs e)
        {
            BindShipMethodSummary();
            RegisterStartupScript("jsUnblockDialog", "unblockShipDialog();");
        }

        protected void btnShipDownload_Click(object sender, EventArgs e)
        {
            if (Session["ShipMethodSummary"]!= null)
            {
                List<ShipMethodSummary> shipMethodSummary = Session["ShipMethodSummary"] as List<ShipMethodSummary>;
                if(shipMethodSummary != null && shipMethodSummary.Count > 0 )
                {
                    string string2CSV = shipMethodSummary.ToCSV();

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=ShipMethodSummary.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(string2CSV);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void btnSKUDownload_Click(object sender, EventArgs e)
        {
            if (Session["SKUSummary"] != null)
            {
                List<SKUSummary> skuSummary = Session["SKUSummary"] as List<SKUSummary>;
                if (skuSummary != null && skuSummary.Count > 0)
                {
                    string string2CSV = skuSummary.ToCSV();

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=SKUSummary.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(string2CSV);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        decimal TOTALCOST = 0;
        protected void rptShip_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblCost = ((Label)e.Item.FindControl("lblCost"));
                TOTALCOST = TOTALCOST + Convert.ToDecimal(lblCost.Text);
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)e.Item.FindControl("lblTotalCost")).Text = TOTALCOST.ToString("0.00"); /* your value */;
            }
        }
    }
}