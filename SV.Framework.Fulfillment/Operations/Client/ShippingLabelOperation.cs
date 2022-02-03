using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.Fulfillment
{
    public class ShippingLabelOperation : BaseCreateInstance
    {        
        

        //public static LabelResponse GetEsnsForLabelPrint(LabelRequest labelRequest)
        //{
        //    LabelResponse serviceResponse = new LabelResponse();
        //    serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
        //    serviceResponse.LabelList = null;
        //    try
        //    {
        //        if (labelRequest != null)
        //        {
        //            int userId = 0;
        //            int companyID = 0;
        //            if (PurchaseOrder.AuthenticateRequestNew(labelRequest.Authentication, out userId, out companyID))
        //            {
        //                List<ShippingLabel> labelList = GetLabelList(userId, companyID);

        //                if (labelList != null && labelList.Count > 0)
        //                {
        //                    serviceResponse.ErrorCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
        //                    serviceResponse.LabelList = labelList;
        //                }
        //                else
        //                    serviceResponse.ErrorCode = ResponseErrorCode.NoRecordsFound.ToString();
        //            }
        //            else
        //            {
        //                serviceResponse.Comment = "Cannot authenticate user";
        //                serviceResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        serviceResponse.LabelList = null;
        //        serviceResponse.Comment = ex.Message;
        //        serviceResponse.ErrorCode = ResponseErrorCode.InternalError.ToString();
        //    }
        //    return serviceResponse;
        //}
        public  List<ShippingLabel> GetLabelList(int userId, int companyID)
        {
            SV.Framework.DAL.Fulfillment.ShippingLabelOperation shippingLabelOperation = SV.Framework.DAL.Fulfillment.ShippingLabelOperation.CreateInstance<DAL.Fulfillment.ShippingLabelOperation>();

            List<ShippingLabel> labelList = shippingLabelOperation.GetLabelList(userId, companyID);

            return labelList;
        }

        public string GetLabelBase64(int lineNumber, out string shipMethod, out string ShipPackage)
        {
            SV.Framework.DAL.Fulfillment.ShippingLabelOperation shippingLabelOperation = SV.Framework.DAL.Fulfillment.ShippingLabelOperation.CreateInstance<DAL.Fulfillment.ShippingLabelOperation>();

            string labelBase64 = shippingLabelOperation.GetLabelBase64(lineNumber, out shipMethod, out ShipPackage);
            
            return labelBase64;
        }

        //public static List<CustomValues> GetCustomdeclarations(int poLabelID)
        //{
        //    List<CustomValues> CustomValueList = new List<CustomValues>();

        //    DBConnect db = new DBConnect();
        //    string[] arrSpFieldSeq;
        //    DataTable dt = new DataTable();
        //    Hashtable objCompHash = new Hashtable();

        //    try
        //    {
        //        objCompHash.Add("@POLabelID", poLabelID);

        //        arrSpFieldSeq = new string[] { "@POLabelID" };
        //        dt = db.GetTableRecords(objCompHash, "av_TrackingCustomDeclaration_Select", arrSpFieldSeq);
        //        CustomValueList = PopulateCustomValues(dt);

        //    }
        //    catch (Exception objExp)
        //    {
        //        throw new Exception(objExp.Message.ToString());
        //    }
        //    finally
        //    {
        //        db = null;
        //    }

        //    return CustomValueList;
        //}
        public List<CustomValues> GetCustomdeclarations(int POID)
        {
            SV.Framework.DAL.Fulfillment.ShippingLabelOperation shippingLabelOperation = SV.Framework.DAL.Fulfillment.ShippingLabelOperation.CreateInstance<DAL.Fulfillment.ShippingLabelOperation>();

            List<CustomValues> CustomValueList = shippingLabelOperation.GetCustomdeclarations(POID);

            return CustomValueList;
        }


        //public static ShippingLabelResponse ShippingLabelUpdate(ShippingLabelRequest request)
        //{

        //    ShippingLabelResponse serviceResponse = new ShippingLabelResponse();
        //    ShippingLabelInfo shippingLabelInfo = new ShippingLabelInfo();
        //    shippingLabelInfo = request.ShippingLabelDetail;
        //    serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
        //    Exception exc = null;


        //    try
        //    {

        //        if (request != null)
        //        {

        //            int userId = PurchaseOrder.AuthenticateRequest(request.Authentication, out exc);
        //            if (userId > 0)
        //            {
        //                // serviceResponse = ShippingLabelUpdateDB(shippingLabelInfo, userId);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        serviceResponse.Comment = ex.Message;
        //        serviceResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
        //    }
        //    return serviceResponse;


        //}
        public ShippingLabelResponse ShippingLabelUpdate(ShippingLabelInfo request, int userId, List<FulfillmentShippingLineItem> listitems, string ShipDate, string Package, string ShipVia, decimal Weight, string Comments, decimal FinalPostage, bool IsManualTracking)
        {
            SV.Framework.DAL.Fulfillment.ShippingLabelOperation shippingLabelOperation = SV.Framework.DAL.Fulfillment.ShippingLabelOperation.CreateInstance<DAL.Fulfillment.ShippingLabelOperation>();

            ShippingLabelResponse serviceResponse = shippingLabelOperation.ShippingLabelUpdateDB(request, userId, listitems, ShipDate, Package, ShipVia, Weight, Comments, FinalPostage, IsManualTracking);

            return serviceResponse;
        }

        public ShippingLabelResponse ShippingLabelUpdateNew(ShippingLabelInfo request, int userId, List<FulfillmentShippingLineItem> listitems, string ShipDate, string Package, string ShipVia, decimal Weight, string Comments, decimal FinalPostage, bool IsManualTracking, int poid, List<CustomValues> customValues)
        {
            SV.Framework.DAL.Fulfillment.ShippingLabelOperation shippingLabelOperation = SV.Framework.DAL.Fulfillment.ShippingLabelOperation.CreateInstance<DAL.Fulfillment.ShippingLabelOperation>();

            ShippingLabelResponse serviceResponse = shippingLabelOperation.ShippingLabelUpdateDBNew(request, userId, listitems, ShipDate, Package, ShipVia, Weight, Comments, FinalPostage, IsManualTracking, poid, customValues);
            
            return serviceResponse;
        }        

        //public static void ExportToPDF(List<ShippingLabel> _List)
        //{
        //    using (Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f))
        //    {
        //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
        //        pdfDoc.Open();

        //        foreach (var item in _List)
        //        {
        //            pdfDoc.NewPage();
        //            //pdfDoc.PageSize = true; 
        //            Paragraph SKU = new Paragraph("SKU: " + Convert.ToString(item.SKU));
        //            Paragraph IMEI = new Paragraph("IMEI: " + Convert.ToString(item.IMEI));
        //            Paragraph ICCID = new Paragraph("ICCID: " + Convert.ToString(item.ICCID));
        //            Paragraph UPC = new Paragraph("UPC: " + Convert.ToString(item.UPC));

        //            pdfDoc.Add(ImageCell(HttpContext.Current.Server.MapPath(GenerateBarCode(Convert.ToString(item.SKU)))));
        //            pdfDoc.Add(SKU);
        //            pdfDoc.Add(ImageCell(HttpContext.Current.Server.MapPath(GenerateBarCode(Convert.ToString(item.IMEI)))));
        //            pdfDoc.Add(IMEI);
        //            pdfDoc.Add(ImageCell(HttpContext.Current.Server.MapPath(GenerateBarCode(Convert.ToString(item.ICCID)))));
        //            pdfDoc.Add(ICCID);
        //            pdfDoc.Add(ImageCell(HttpContext.Current.Server.MapPath(GenerateBarCode(Convert.ToString(item.UPC)))));
        //            pdfDoc.Add(UPC);
        //        }

        //        pdfDoc.Close();
        //        HttpContext.Current.Response.ContentType = "application/pdf";
        //        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + DateTime.Now.Ticks + "-barcode.pdf");
        //        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        HttpContext.Current.Response.Write(pdfDoc);
        //        HttpContext.Current.Response.End();

        //    }
        //}
        //public static string GenerateBarCode(string code)
        //{
        //    Barcode39 code39 = new Barcode39();
        //    code39.Code = code;
        //    code39.StartStopText = true;
        //    code39.GenerateChecksum = false;
        //    code39.Extended = true;

        //    System.Drawing.Image img = code39.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);

        //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
        //    img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        //    byte[] imageBytes = ms.ToArray();
        //    string base64String = Convert.ToBase64String(imageBytes);

        //    return ConvertToImage(Convert.ToBase64String(imageBytes), ".jpg");

        //}
        //public static string ConvertToImage(string base64String, string fileType)
        //{
        //    if (string.IsNullOrEmpty(base64String) || base64String == "undefined")
        //        return string.Empty;

        //    var bytes = Convert.FromBase64String(base64String.Replace("data:image/jpeg;base64,", ""));
        //    string imagePath = "~/pdffiles/";
        //    string fileDirctory = HttpContext.Current.Server.MapPath(imagePath);
        //    string filename = DateTime.Now.Ticks + fileType;
        //    string file = Path.Combine(fileDirctory, filename);
        //    if (bytes.Length > 0)
        //    {
        //        using (var stream = new FileStream(file, FileMode.Create))
        //        {
        //            stream.Write(bytes, 0, bytes.Length);
        //            stream.Flush();
        //        }
        //    }
        //    string filePath = "/pdffiles/" + filename;
        //    return filePath;
        //}
        //public static iTextSharp.text.Image ImageCell(string filePath)
        //{
        //    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(filePath);
        //    //jpg.ScaleToFit(140f, 120f);
        //    jpg.SpacingBefore = 10f;
        //    jpg.SpacingAfter = 1f;
        //    jpg.Alignment = Element.ALIGN_LEFT;
        //    return jpg;
        //}

        //public void DeleteImages(List<ShippingLabel> _List)
        //{
        //    foreach (var item in _List)
        //    {
        //        File.Delete(HttpContext.Current.Server.MapPath(GenerateBarCode(Convert.ToString(item.SKU))));
        //        File.Delete(HttpContext.Current.Server.MapPath(GenerateBarCode(Convert.ToString(item.IMEI))));
        //        File.Delete(HttpContext.Current.Server.MapPath(GenerateBarCode(Convert.ToString(item.ICCID))));
        //        File.Delete(HttpContext.Current.Server.MapPath(GenerateBarCode(Convert.ToString(item.UPC))));
        //    }
        //}
    }

    
}
