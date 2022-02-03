using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.RMA;

namespace avii.RMA
{
    public partial class RmaView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["rmaGUID"] != null)
                {
                    int rmaGUID = Convert.ToInt32(Session["rmaGUID"]);
                    ViewState["rmaGUID"] = rmaGUID;
                    Session["rmaGUID"] = null;
                    GetRMA(rmaGUID);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>closeWindow()</script>", false);

                }
            }
        }

        private void GetRMA(int rmaGUID)
        {
            try
            {
                SV.Framework.RMA.RmaOperation rmaOperation = SV.Framework.RMA.RmaOperation.CreateInstance<SV.Framework.RMA.RmaOperation>();


                lblMsg.Text = string.Empty;
                List<RmaTracking> RmaTrackings = new List<RmaTracking>();
                RmaInfo rmaInfo = rmaOperation.GetRMA(rmaGUID);
                if (rmaInfo != null && !string.IsNullOrWhiteSpace(rmaInfo.RmaNumber))
                {
                    lblDocs.Text = rmaInfo.RMADocuments;
                    lblRmaNumber.Text = rmaInfo.RmaNumber;
                    lblAddress.Text = rmaInfo.CustomerAddress1;
                    lblCity.Text = rmaInfo.CustomerCity;
                    lblCompanyName.Text = rmaInfo.CustomerName;
                    lblContactName.Text = rmaInfo.CustomerContactName;
                    lblContactNumber.Text = rmaInfo.CustomerContactNumber;
                    lblCustomerRmaNo.Text = rmaInfo.CustomerRMANumber;
                    lblRmaDate.Text = rmaInfo.RmaDate;
                    lblRmaStatus.Text = rmaInfo.RmaStatus;
                    lblState.Text = rmaInfo.CustomerState;
                    lblStoreID.Text = rmaInfo.StoreID;
                    lblZip.Text = rmaInfo.CustomerZipCode;
                    lblTriageStatus.Text = rmaInfo.TriageStatus;
                    lblReceiveStatus.Text = rmaInfo.ReceiveStatus;
                    lblLabel.Text = "";
                    lblDetail.Text = "";
                    lblReceive.Text = "";
                    lblCComments.Text = "";
                    lblIComments.Text = "";
                    //lblLabel.Text = "";
                    if (rmaInfo.RmaTrackings != null && rmaInfo.RmaTrackings.Count > 0)
                    {
                        RmaTrackings = rmaInfo.RmaTrackings;
                        rptTracking.DataSource = RmaTrackings;
                        rptTracking.DataBind();
                    }
                    else
                    {
                        rptTracking.DataSource = RmaTrackings;
                        rptTracking.DataBind();
                        lblLabel.Text = "No record found";
                    }
                    if (rmaInfo.RMADetails != null && rmaInfo.RMADetails.Count > 0)
                    {
                        //RmaTrackings = rmaInfo.RmaTrackings;
                        rptRmaDetail.DataSource = rmaInfo.RMADetails;
                        rptRmaDetail.DataBind();
                    }
                    else
                    {
                        rptRmaDetail.DataSource = null;
                        rptRmaDetail.DataBind();
                        lblDetail.Text = "No record found";
                    }
                    if (rmaInfo.ReceiveList != null && rmaInfo.ReceiveList.Count > 0)
                    {
                        //RmaTrackings = rmaInfo.RmaTrackings;
                        Session["rmareceive"]  = rmaInfo.ReceiveList;
                        gvReceive.DataSource = rmaInfo.ReceiveList;
                        gvReceive.DataBind();
                    }
                    else
                    {
                        gvReceive.DataSource = null;
                        gvReceive.DataBind();
                        lblReceive.Text = "No record found";
                    }
                    if (rmaInfo.CustomerComments != null && rmaInfo.CustomerComments.Count > 0)
                    {
                        //RmaTrackings = rmaInfo.RmaTrackings;
                        rptCustComments.DataSource = rmaInfo.CustomerComments;
                        rptCustComments.DataBind();
                    }
                    else
                    {
                        rptCustComments.DataSource = null;
                        rptCustComments.DataBind();
                        lblCComments.Text = "No record found";
                    }
                    if (rmaInfo.InternalComments != null && rmaInfo.InternalComments.Count > 0)
                    {
                        //RmaTrackings = rmaInfo.RmaTrackings;
                        rptInternalComments.DataSource = rmaInfo.InternalComments;
                        rptInternalComments.DataBind();
                    }
                    else
                    {
                        rptInternalComments.DataSource = null;
                        rptInternalComments.DataBind();
                        lblIComments.Text = "No record found";
                    }


                }
            }
            catch(Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void btnPackingSlip_Click(object sender, EventArgs e)
        {
            int rmaGUID = 0;
            if (ViewState["rmaGUID"] != null)
            {
                rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
                CreateRmaPackageSlipPDF(rmaGUID);
            }
        }

        protected void btnPrintLabel_Click(object sender, EventArgs e)
        {
            int rmaGUID = 0;
            if (ViewState["rmaGUID"] != null)
            {
                rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
                PrintLabel(rmaGUID);
            }
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                List<RMAReceive> receiveList = new List<RMAReceive>(); 
                int index = e.Row.RowIndex;
                if(Session["rmareceive"] != null)
                {
                    receiveList = Session["rmareceive"] as List<RMAReceive>;
                    if(receiveList != null && receiveList.Count > 0)
                    {
                        List<ReceiveDetail> receiveDetails = receiveList[index].ReceiveDetails;
                        if(receiveDetails != null && receiveDetails.Count > 0)
                        {
                            GridView gvReceiveDetail = e.Row.FindControl("gvReceiveDetail") as GridView;
                            gvReceiveDetail.DataSource = receiveDetails;
                            gvReceiveDetail.DataBind();
                        }
                    }
                }
                //int poid = Convert.ToInt32(gvReceive.DataKeys[e.Row.RowIndex].Value);
               
               
            }
        }

        private void CreateRmaPackageSlipPDF(int rmaGUID)
        {
            if (rmaGUID > 0)
            {
                SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

                SV.Framework.LabelGenerator.RmaInfo rmaInfo = new SV.Framework.LabelGenerator.RmaInfo();
                SV.Framework.LabelGenerator.RmaDetailModel rmaDetailModel = null;
                List<SV.Framework.LabelGenerator.RmaDetailModel> rmaDetails = null;
                SV.Framework.LabelGenerator.RmaPackingSlipOperation operation = new SV.Framework.LabelGenerator.RmaPackingSlipOperation();
                string shipFromContactName = ConfigurationSettings.AppSettings["ShipFromContactName"].ToString();
                string shipFromAddress = ConfigurationSettings.AppSettings["ShipFromAddress"].ToString();
                string shipFromCity = ConfigurationSettings.AppSettings["ShipFromCity"].ToString();
                string shipFromState = ConfigurationSettings.AppSettings["ShipFromState"].ToString();
                string shipFromZip = ConfigurationSettings.AppSettings["ShipFromZip"].ToString();
                string shipFromCountry = ConfigurationSettings.AppSettings["ShipFromCountry"].ToString();
                string shipFromAttn = ConfigurationSettings.AppSettings["ShipFromAttn"].ToString();
                string shipFromPhone = ConfigurationSettings.AppSettings["ShipFromPhone"].ToString();


                List<SV.Framework.Models.RMA.RMA> objRmaList = null;
                if (Session["result"] != null)
                {
                    objRmaList = Session["result"] as List<SV.Framework.Models.RMA.RMA>;

                    if (objRmaList != null)
                    {
                        var rmaInfoList = (from item in objRmaList where item.RmaGUID.Equals(rmaGUID) select item).ToList();
                        if (rmaInfoList != null && rmaInfoList.Count > 0)
                        {
                            rmaInfo.RMANumber = rmaInfoList[0].RmaNumber;
                            rmaInfo.Comments = rmaInfoList[0].Comment;
                            //lblAVComment.Text = rmaInfoList[0].AVComments;
                            rmaInfo.ContactName = rmaInfoList[0].RmaContactName;
                            rmaInfo.PhoneNumber = rmaInfoList[0].Phone;
                            rmaInfo.Email = rmaInfoList[0].Email;
                            rmaInfo.CompanyLogo = Server.MapPath("~/img/fplogo2.png"); ;
                            rmaInfo.RmaDate = rmaInfoList[0].RmaDate.ToShortDateString();
                            rmaInfo.PackingSlip = rmaInfoList[0].RmaNumber;
                            rmaInfo.StoreID = rmaInfoList[0].StoreID;
                            rmaInfo.StoreName = string.Empty;//rmaInfoList[0].StoreID;



                            rmaInfo.CompanyName = rmaInfoList[0].RMAUserCompany.CompanyName;// shipFromContactName;
                            rmaInfo.ShippingAddressLine1 = shipFromAddress;
                            rmaInfo.ShippingCity = shipFromCity;
                            rmaInfo.ShippingState = shipFromState;
                            rmaInfo.ShippingZipCode = shipFromZip;
                            rmaInfo.ShippingCountry = shipFromCountry;
                            //rmaInfo.s = shipFromContactName;


                        }

                    }
                }

                rmaDetails = new List<SV.Framework.LabelGenerator.RmaDetailModel>();

                DataTable objDataTable = (DataTable)HttpContext.Current.Session["rmadetail"];
                if (objDataTable != null)
                {
                    DataRow[] rows = objDataTable.Select(string.Format("rmaGUID='{0}' ", rmaGUID));
                    DataRow searchedRow = null;
                    if (rows.Length > 0)
                    {
                        foreach (DataRow dataRow in rows)
                        {
                            rmaDetailModel = new SV.Framework.LabelGenerator.RmaDetailModel();
                            int warrantyInt = Convert.ToInt32(avii.Classes.clsGeneral.getColumnData(dataRow, "Warranty", 0, false));
                            int dispositionId = Convert.ToInt32(avii.Classes.clsGeneral.getColumnData(dataRow, "Disposition", 0, false));
                            int ReasonId = Convert.ToInt32(avii.Classes.clsGeneral.getColumnData(dataRow, "Reason", 0, false));

                            Warranty warranty = (Warranty)warrantyInt;

                            Disposition disposition = (Disposition)dispositionId;
                            RMAReason reason = (RMAReason)ReasonId;


                            rmaDetailModel.Disposition = disposition.ToString();
                            rmaDetailModel.Warranty = warranty.ToString();
                            rmaDetailModel.RMAReason = reason.ToString();



                            rmaDetailModel.ItemDescription = avii.Classes.clsGeneral.getColumnData(dataRow, "ItemDescription", string.Empty, false) as string;

                            rmaDetailModel.ItemCode = avii.Classes.clsGeneral.getColumnData(dataRow, "itemcode", string.Empty, false) as string;

                            rmaDetailModel.ESN = avii.Classes.clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;




                            rmaDetails.Add(rmaDetailModel);
                        }
                        rmaInfo.RmaDetails = rmaDetails;
                        var memSt = operation.ExportToPDF(rmaInfo);
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

                            string packingSlip = "pck" + rmaGUID.ToString();
                            
                            rmaUtility.RmaPackingSlipInsertUpdate(rmaGUID, packingSlip);
                        }
                        else
                            lblMsg.Text = "Technical error!";


                    }
                }
                else
                {

                }

            }


        }
        protected void imgPrint_Command(object sender, CommandEventArgs e)
        {
            int TrackingId = Convert.ToInt32(e.CommandArgument);
            ViewState["TrackingId"] = TrackingId;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp1", "<script language='javascript'>Refresh();</script>", false);

        }
        protected void btnhdnPrintlabel_Click(object sender, EventArgs e)
        {
            if (ViewState["TrackingId"] != null)
            {
                PrintLabel(Convert.ToInt32(ViewState["TrackingId"]));
            }
        }

        private void PrintLabel(int trackingId)
        {
            try
            {
                SV.Framework.RMA.RMATrackingOperation rmaTrackingOperation = SV.Framework.RMA.RMATrackingOperation.CreateInstance<SV.Framework.RMA.RMATrackingOperation>();

                float width = 320, height = 520, envHeight = 500;
                bool IsManualTracking;

                string shipMethod = string.Empty, ShipPackage = string.Empty;
                string labelBase64 = rmaTrackingOperation.GetLabelBase64(trackingId, out shipMethod, out ShipPackage, out IsManualTracking);
                if (!IsManualTracking)
                {
                    lblMsg.Text = "Cannot print manually created label!";
                }
                else
                {
                    if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), shipMethod))
                    {
                        if (!string.IsNullOrWhiteSpace(labelBase64))
                        {
                            //  imgLabel.ImageUrl = "data:image;base64," + labelBase64;
                            //RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");

                            byte[] imageBytes = Convert.FromBase64String(labelBase64);
                            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);
                            if (shipMethod.ToLower() == "first" && ShipPackage.ToString().ToLower() == "letter")
                            {
                                width = 500;
                                height = 320;
                                envHeight = 320;
                            }
                            //image.ScaleToFit(320, 520);
                            image.ScaleToFit(width, height);
                            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                            {
                                Document document = new Document();
                                iTextSharp.text.Rectangle envelope = new iTextSharp.text.Rectangle(320, 500);
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
                        // RegisterStartupScript("jsUnblockDialog", "closeLabelDialog();");
                        if (!string.IsNullOrWhiteSpace(labelBase64))
                        {
                            //var script = "OpenPDF('" + labelBase64 + "')"; //"window.open('" + pdfByteArray + "', '_blank');";
                            //ScriptManager.RegisterClientScriptBlock(Parent.Page, typeof(Page), "pdf", script, true);
                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenPDF('" + labelBase64 + "')</script>", false);
                            // imgLabel.ImageUrl = "data:application/pdf;base64," + labelBase64;
                            //  RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");
                            //data: application/pdf; base64,
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
                            // Response.End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }


        //private void PrintLabel(int rmaGUID)
        //{
        //    try
        //    {
        //        float width = 320, height = 520, envHeight = 500;

        //        string shipMethod = string.Empty, ShipPackage = string.Empty;
        //        string labelBase64 = avii.Classes.RMATrackingOperation.GetLabelBase64(rmaGUID, out shipMethod, out ShipPackage);
        //        if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), shipMethod))
        //        {
        //            if (!string.IsNullOrWhiteSpace(labelBase64))
        //            {
        //                //  imgLabel.ImageUrl = "data:image;base64," + labelBase64;
        //                //RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");

        //                byte[] imageBytes = Convert.FromBase64String(labelBase64);
        //                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);
        //                if (shipMethod.ToLower() == "first" && ShipPackage.ToString().ToLower() == "letter")
        //                {
        //                    width = 500;
        //                    height = 320;
        //                    envHeight = 320;
        //                }
        //                //image.ScaleToFit(320, 520);
        //                image.ScaleToFit(width, height);
        //                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
        //                {
        //                    Document document = new Document();
        //                    iTextSharp.text.Rectangle envelope = new iTextSharp.text.Rectangle(320, 500);
        //                    document.SetPageSize(envelope);
        //                    document.SetMargins(0, 0, 0, 0);

        //                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
        //                    document.Open();

        //                    document.Add(image);
        //                    document.Close();
        //                    var bytes = memoryStream.ToArray();
        //                    // memoryStream.Close();

        //                    string fileType = ".pdf";
        //                    string filename = DateTime.Now.Ticks + fileType;
        //                    Response.Clear();
        //                    //Response.ContentType = "application/pdf";
        //                    Response.ContentType = "application/octet-stream";
        //                    Response.AddHeader("content-disposition", "attachment;filename=" + filename);
        //                    Response.Buffer = true;
        //                    Response.Clear();
        //                    // var bytes = memSt.ToArray();
        //                    Response.OutputStream.Write(bytes, 0, bytes.Length);
        //                    Response.OutputStream.Flush();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            // RegisterStartupScript("jsUnblockDialog", "closeLabelDialog();");
        //            if (!string.IsNullOrWhiteSpace(labelBase64))
        //            {
        //                //var script = "OpenPDF('" + labelBase64 + "')"; //"window.open('" + pdfByteArray + "', '_blank');";
        //                //ScriptManager.RegisterClientScriptBlock(Parent.Page, typeof(Page), "pdf", script, true);
        //                // ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenPDF('" + labelBase64 + "')</script>", false);
        //                // imgLabel.ImageUrl = "data:application/pdf;base64," + labelBase64;
        //                //  RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");
        //                //data: application/pdf; base64,
        //                string fileType = ".pdf";
        //                string filename = DateTime.Now.Ticks + fileType;
        //                Response.Clear();
        //                //Response.ContentType = "application/pdf";
        //                Response.ContentType = "application/octet-stream";
        //                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
        //                Response.Buffer = true;
        //                Response.Clear();
        //                var bytes = Convert.FromBase64String(labelBase64);
        //                Response.OutputStream.Write(bytes, 0, bytes.Length);
        //                Response.OutputStream.Flush();
        //                // Response.End();

        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Text = ex.Message;
        //    }
        //}

        protected void imgRmaDet_Command(object sender, CommandEventArgs e)
        {
            int rmaDetGUID = Convert.ToInt32(e.CommandArgument);
            if(rmaDetGUID > 0)
            {
                int userID = 0;
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    //ViewState["companyID"] = userInfo.CompanyGUID;
                    userID = userInfo.UserGUID;
                }
                try
                {
                   avii.Classes.RMAUtility.delete_RMA_RMADETAIL(0, rmaDetGUID, userID);
                    int rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
                    ViewState["rmaGUID"] = rmaGUID;
                    GetRMA(rmaGUID);
                    lblMsg.Text = "Deleted successfully";
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message.ToString();
                }
            }
        }
    }
}