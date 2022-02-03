using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SV.Framework.Models.RMA;
using SV.Framework.Models.Fulfillment;

namespace avii.RMA
{
    public partial class RmaShipLabel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    url = ConfigurationSettings.AppSettings["LogonPage"].ToString();
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
                int rmaguid = 0;
                if (Session["rmaguid"] != null)
                {
                    rmaguid = Convert.ToInt32(Session["rmaguid"]);
                    ViewState["rmaguid"] = rmaguid;
                    Session["rmaguid"] = null;
                    BindShipBy();
                    RmaShipment(rmaguid);
                    GetRMATracking(rmaguid);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>closeWindow()</script>", false);

                    btnShip.Visible = false;
                }
            }
            

        }
        private void BindShipBy()
        {
            try
            {
               SV.Framework.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.Fulfillment.PurchaseOrder.CreateInstance<SV.Framework.Fulfillment.PurchaseOrder>();
                List<ShipBy> shipBy = purchaseOrderOperation.GetShipByList();//avii.Classes.PurchaseOrder.GetShipByList();

                ddlShipVia.DataSource = shipBy;
                ddlShipVia.DataTextField = "ShipCodeNText";
                ddlShipVia.DataValueField = "ShipByCode";
                ddlShipVia.DataBind();
            }
            catch (Exception ex)
            {
                lblShipItem.Text = ex.Message;
            }
        }

        private void RmaShipment(int rmaGUID)
        {
            try
            {
                lblShipItem.Text = string.Empty;
                chkTracking.Checked = false;
                btnShip.Visible = true;
                txtTrackingNumber.Text = string.Empty;
                txtShippingDate.Text = DateTime.Now.ToShortDateString();

                DateTime dateNow = DateTime.Now.Date;


                txtShipComments.Text = string.Empty;
                ddlShipVia.SelectedIndex = 0;
                ddlShape.Items.Clear();
                string[] itemNames = System.Enum.GetNames(typeof(SV.Framework.Common.LabelGenerator.ShipPackageShape));
                for (int i = 0; i <= itemNames.Length - 1; i++)
                {
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(itemNames[i], itemNames[i]);
                    ddlShape.Items.Add(item);
                }
                System.Web.UI.WebControls.ListItem item1 = new System.Web.UI.WebControls.ListItem("", "");
                ddlShape.Items.Insert(0, item1);

                List<SV.Framework.Models.RMA.RMA> objRmaList = null;
                if (Session["result"] != null)
                {
                    objRmaList = Session["result"] as List<SV.Framework.Models.RMA.RMA>;

                    if (objRmaList != null)
                    {
                        var rmaInfoList = (from item in objRmaList where item.RmaGUID.Equals(rmaGUID) select item).ToList();
                        if (rmaInfoList != null && rmaInfoList.Count > 0)
                        {
                            string day = string.Empty;
                            string month = string.Empty;
                            string year = string.Empty;

                            { //txtShippingDate.Text = dateNow.ToString("MM/dd/yyyy");
                                day = dateNow.Day.ToString();
                                month = dateNow.Month.ToString();
                                year = dateNow.Year.ToString();
                                txtShippingDate.Text = month + "/" + day + "/" + year;

                            }
                            lblRMA.Text = rmaInfoList[0].RmaNumber;
                            lblCustomerRMA.Text = rmaInfoList[0].CustomerRMANumber;
                            lblRmaDate.Text = rmaInfoList[0].RmaDate.ToShortDateString();
                            lblRmaStatus.Text = rmaInfoList[0].Status;

                            //string poShipBy = rmaInfoList[0].ShipToBy;
                            
                            //ddlShipVia.SelectedValue = poShipBy;

                            //if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), poShipBy))
                            //{
                            //    ddlShape.SelectedValue = SV.Framework.Common.LabelGenerator.ShipPackageShape.Letter.ToString();
                            //}
                            //else
                            //    ddlShape.SelectedValue = SV.Framework.Common.LabelGenerator.ShipPackageShape.package.ToString();


                           // if (rmaInfoList[0].RmaStatusID == 3)
                             //   btnShip.Visible = false;
                        }

                    }
                }
                
            }
            catch (Exception ex)
            {
                lblShipItem.Text = ex.Message;
            }
        }
        private void GetRMATracking(int rmaGUID)
        {
            trShip.Visible = false;
            SV.Framework.RMA.RmaOperation rmaOperation = SV.Framework.RMA.RmaOperation.CreateInstance<SV.Framework.RMA.RmaOperation>();

            try
            {
                lblShipItem.Text = string.Empty;
                List<RmaTracking> RmaTrackings = new List<RmaTracking>();
                RmaInfo rmaInfo = rmaOperation.GetRMA(rmaGUID);
                if (rmaInfo != null && !string.IsNullOrWhiteSpace(rmaInfo.RmaNumber))
                {
                    //lblLabel.Text = "";
                    if (rmaInfo.RmaTrackings != null && rmaInfo.RmaTrackings.Count > 0)
                    {
                        RmaTrackings = rmaInfo.RmaTrackings;
                        rptTracking.DataSource = RmaTrackings;
                        rptTracking.DataBind();
                        trShip.Visible = true;
                    }
                    
                    
                    


                }
            }
            catch (Exception ex)
            {
                lblShipItem.Text = ex.Message;
            }
        }

        protected void btnAddShip_Click(object sender, EventArgs e)
        {
            int returnResult = GenerateShipmentLabel();
            if (returnResult == 1)
            {
                int rmaGUID = Convert.ToInt32(ViewState["rmaguid"]);

                GetRMATracking(rmaGUID);
                btnShip.Visible = false;
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
                SV.Framework.RMA.RMATrackingOperation RMATrackingOperation = SV.Framework.RMA.RMATrackingOperation.CreateInstance<SV.Framework.RMA.RMATrackingOperation>();

                float width = 320, height = 520, envHeight = 500;
                bool IsManualTracking;
                string shipMethod = string.Empty, ShipPackage = string.Empty;
                string labelBase64 = RMATrackingOperation.GetLabelBase64(trackingId, out shipMethod, out ShipPackage, out IsManualTracking);
                if (!IsManualTracking)
                {
                    lblShipItem.Text = "Cannot print manually created label!";
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
                lblShipItem.Text = ex.Message;
            }
        }

        private int GenerateShipmentLabel()
        {
            SV.Framework.RMA.RMATrackingOperation RMATrackingOperation = SV.Framework.RMA.RMATrackingOperation.CreateInstance<SV.Framework.RMA.RMATrackingOperation>();

            string shipFromContactName = ConfigurationSettings.AppSettings["ShipFromContactName"].ToString();
            string shipFromAddress = ConfigurationSettings.AppSettings["ShipFromAddress"].ToString();
            string shipFromCity = ConfigurationSettings.AppSettings["ShipFromCity"].ToString();
            string shipFromState = ConfigurationSettings.AppSettings["ShipFromState"].ToString();
            string shipFromZip = ConfigurationSettings.AppSettings["ShipFromZip"].ToString();
            string shipFromCountry = ConfigurationSettings.AppSettings["ShipFromCountry"].ToString();
            string shipFromAttn = ConfigurationSettings.AppSettings["ShipFromAttn"].ToString();
            string shipFromPhone = ConfigurationSettings.AppSettings["ShipFromPhone"].ToString();
            int returnResult = 0;
            int rmaGUID = 0, userId = 0;
            double weight = 0;
            string Prepaid = string.Empty;
            Prepaid = ddlPrepaid.SelectedValue;
            double.TryParse(txtWeight.Text.Trim(), out weight);
            if (weight == 0)
                weight = 1;
            string ShipDate = txtShippingDate.Text;
            DateTime LabelPrintDateTime = DateTime.Today;
            //if (!string.IsNullOrEmpty(ShipDate))
              //  LabelPrintDateTime = Convert.ToDateTime(ShipDate);

            //lblESN.Text = string.Empty;
            //lblTracking.Text = string.Empty;
            SV.Framework.Common.LabelGenerator.EndiciaPrintLabel labelInfo = new SV.Framework.Common.LabelGenerator.EndiciaPrintLabel();

            SV.Framework.Common.LabelGenerator.ShippingLabelOperation shipAccess = new SV.Framework.Common.LabelGenerator.ShippingLabelOperation();
            SV.Framework.Common.LabelGenerator.ShipInfo shipToInfo = new SV.Framework.Common.LabelGenerator.ShipInfo();
            SV.Framework.Common.LabelGenerator.ShipInfo shipFromInfo = new SV.Framework.Common.LabelGenerator.ShipInfo();


            try
            {
                string shipmentType = "S";
                if (ViewState["rmaguid"] != null)
                {
                    rmaGUID = Convert.ToInt32(ViewState["rmaguid"]);

                    if (Session["UserID"] != null)
                    {
                        int.TryParse(Session["UserID"].ToString(), out userId);
                    }
                    //objRmaList = Session["result"] as List<SV.Framework.Models.RMA.RMA>;
                    List<SV.Framework.Models.RMA.RMA> rmaList = Session["result"] as List<SV.Framework.Models.RMA.RMA>;

                    if (rmaList != null && rmaList.Count > 0)
                    {

                        var rma = (from item in rmaList where item.RmaGUID.Equals(rmaGUID) select item).ToList();
                        if (rma != null && rma.Count > 0)
                        {
                            labelInfo.FulfillmentNumber = rma[0].RmaNumber;
                            //labelInfo.LabelPrintDateTime = DateTime.Today;
                            labelInfo.LabelPrintDateTime = LabelPrintDateTime;

                            //shipToInfo
                            shipToInfo.ShipToAddress = shipFromAddress;
                            shipToInfo.ShipToAddress2 = "";
                            shipToInfo.ContactName = shipFromContactName;
                            shipToInfo.ShipToCity = shipFromCity;
                            shipToInfo.ShipToState = shipFromState;
                            shipToInfo.ShipToZip = shipFromZip;
                            shipToInfo.ShipToAttn = shipFromContactName;
                            shipToInfo.ContactPhone = shipFromPhone;
                            shipToInfo.ShipToCountry = shipFromCountry;
                            labelInfo.ShipTo = shipToInfo;

                            //string[] array = rma[0].State.Split('-');
                            //ship From Info
                            shipFromInfo.ShipToAddress = rma[0].Address;
                            shipFromInfo.ShipToAddress2 = "";
                            shipFromInfo.ContactName = rma[0].RmaContactName;
                            shipFromInfo.ShipToCity = rma[0].City;
                            shipFromInfo.ShipToState = rma[0].State;
                            shipFromInfo.ShipToZip = rma[0].Zip;
                            shipFromInfo.ShipToAttn = rma[0].RmaContactName;
                            shipFromInfo.ShipToCountry = rma[0].ContactCountry;
                            shipFromInfo.ContactPhone = rma[0].Phone;

                            labelInfo.ShipFrom = shipFromInfo;
                            //
                            labelInfo.PackageWeight = new SV.Framework.Common.LabelGenerator.Weight { units = "ounces", value = weight };
                            labelInfo.PackageContent = "Description";

                            //labelInfo.ShippingMethod = SV.Framework.Common.LabelGenerator.ShipMethods.Priority;
                            // if (ddlShipVia.SelectedIndex > 0)
                            {
                                Enum.TryParse(ddlShipVia.SelectedValue, out SV.Framework.Common.LabelGenerator.ShipMethods shipMethods);
                                labelInfo.ShippingMethod = shipMethods;
                            }

                            labelInfo.ShippingType = SV.Framework.Common.LabelGenerator.ShipType.Ship;


                            if (ddlShape.SelectedIndex > 0)
                            {
                                Enum.TryParse(ddlShape.SelectedValue, out SV.Framework.Common.LabelGenerator.ShipPackageShape shipPackage);
                                labelInfo.PackageShape = shipPackage;
                            }
                            else
                                labelInfo.PackageShape = SV.Framework.Common.LabelGenerator.ShipPackageShape.Letter;

                            //string ShipDate = txtShippingDate.Text;
                            string Package = ddlShape.SelectedValue;
                            string ShipVia = ddlShipVia.SelectedValue;
                            double Weight = Convert.ToDouble(txtWeight.Text);
                            string Comments = txtShipComments.Text.Trim();
                            decimal FinalPostage = 0;
                            bool IsManualTracking = chkTracking.Checked;

                            RMATrackning request = new RMATrackning();
                            request.RMANumber = labelInfo.FulfillmentNumber;
                            request.ShipmentType = shipmentType;
                            request.ShipDate = ShipDate;
                            request.Package = Package;
                            request.ShipVia = ShipVia;
                            request.Weight = Weight;
                            request.Comments = Comments;
                            request.Prepaid = Prepaid;
                            request.IsManualTracking = IsManualTracking;
                            if (!IsManualTracking)
                            {
                                SV.Framework.Common.LabelGenerator.iPrintLabel response = shipAccess.PrintShippingLabel(labelInfo);

                                if (response != null && !string.IsNullOrWhiteSpace(response.TrackingNumber))
                                {
                                    request.ShippingLabelImage = response.ShippingLabelImage;
                                    request.TrackingNumber = response.TrackingNumber;
                                    request.FinalPostage = response.FinalPostage;

                                    //request.LineItems = listitems;
                                    txtTrackingNumber.Text = response.TrackingNumber;
                                    txtCost.Text = response.FinalPostage.ToString();
                                    ShippingLabelResponse setResponse = RMATrackingOperation.ShippingLabelUpdate(request, userId);
                                    lblShipItem.Text = "Label generated successfully";
                                    returnResult = 1;
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(response.PackageContent))
                                        lblShipItem.Text = response.PackageContent;
                                    else
                                        lblShipItem.Text = "Technical error please try again!";
                                }
                            }
                            else
                            {
                                request.ShippingLabelImage = null;
                                request.TrackingNumber = txtTrackingNumber.Text.Trim();
                                FinalPostage = Convert.ToDecimal(txtCost.Text);

                                request.FinalPostage = FinalPostage;


                                ShippingLabelResponse setResponse = RMATrackingOperation.ShippingLabelUpdate(request, userId);
                                lblShipItem.Text = "Submitted successfully";
                                returnResult = 1;
                            }

                        }


                    }

                }
            }
            catch (Exception ex)
            {
                lblShipItem.Text = ex.Message;
            }
            return returnResult;
        }

    }
}