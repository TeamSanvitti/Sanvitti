using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Fulfillment
{
    public class ShippingLabelOperation : BaseCreateInstance
    {

        //public  LabelResponse GetEsnsForLabelPrint(LabelRequest labelRequest)
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
            List<ShippingLabel> labelList = new List<ShippingLabel>();

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@UserId", userId);
                    objCompHash.Add("@CompanyId", companyID);

                    arrSpFieldSeq = new string[] { "@UserId", "@CompanyId" };
                    dt = db.GetTableRecords(objCompHash, "av_ServiceOrderLabelSelect", arrSpFieldSeq);
                    labelList = PopulateLabelInfo(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //  throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                   // db = null;
                }
            }
            return labelList;
        }

        public  string GetLabelBase64(int lineNumber, out string shipMethod, out string ShipPackage)
        {
            string labelBase64 = default;//string.Empty;
            shipMethod = default;//string.Empty;
            ShipPackage = default;//string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@LineNumber", lineNumber);


                    arrSpFieldSeq = new string[] { "@LineNumber" };
                    dt = db.GetTableRecords(objCompHash, "av_PurchaseOrder_ShippingLabel_Base64", arrSpFieldSeq);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            labelBase64 = Convert.ToString(row["ShippingLabel"]);
                            shipMethod = Convert.ToString(row["ShipByCode"]);
                            ShipPackage = Convert.ToString(row["ShipPackage"]);
                        }
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //  throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                }
            }
            return labelBase64;
        }

        //public  List<CustomValues> GetCustomdeclarations(int poLabelID)
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
        public  List<CustomValues> GetCustomdeclarations(int POID)
        {
            List<CustomValues> CustomValueList = new List<CustomValues>();

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@POID", POID);

                    arrSpFieldSeq = new string[] { "@POID" };
                    dt = db.GetTableRecords(objCompHash, "av_TrackingCustomDeclaration_Select", arrSpFieldSeq);
                    CustomValueList = PopulateCustomValues(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //  throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                }
            }
            return CustomValueList;
        }


        //public  ShippingLabelResponse ShippingLabelUpdate(ShippingLabelRequest request)
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
        //public  ShippingLabelResponse ShippingLabelUpdate(ShippingLabelInfo request, int userId, List<FulfillmentShippingLineItem> listitems, string ShipDate, string Package, string ShipVia, decimal Weight, string Comments, decimal FinalPostage, bool IsManualTracking)
        //{

        //    ShippingLabelResponse serviceResponse = new ShippingLabelResponse();
        //    serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
        //    try
        //    {
        //        if (request != null)
        //        {
        //            //if (userId > 0)
        //            {
        //                serviceResponse = ShippingLabelUpdateDB(request, userId, listitems, ShipDate, Package, ShipVia, Weight, Comments, FinalPostage, IsManualTracking);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        serviceResponse.Comment = ex.Message;
        //        serviceResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
        //    }
        //    return serviceResponse;

        //}

        //public  ShippingLabelResponse ShippingLabelUpdateNew(ShippingLabelInfo request, int userId, List<FulfillmentShippingLineItem> listitems, string ShipDate, string Package, string ShipVia, decimal Weight, string Comments, decimal FinalPostage, bool IsManualTracking, int poid, List<CustomValues> customValues)
        //{

        //    ShippingLabelResponse serviceResponse = new ShippingLabelResponse();
        //    serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
        //    try
        //    {
        //        if (request != null)
        //        {
        //            //if (userId > 0)
        //            {
        //                serviceResponse = ShippingLabelUpdateDBNew(request, userId, listitems, ShipDate, Package, ShipVia, Weight, Comments, FinalPostage, IsManualTracking, poid, customValues);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        serviceResponse.Comment = ex.Message;
        //        serviceResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
        //    }
        //    return serviceResponse;

        //}


        public  ShippingLabelResponse ShippingLabelUpdateDB(ShippingLabelInfo shippingLabelInfo, int UserID, List<FulfillmentShippingLineItem> listitems, string ShipDate, string Package, string ShipVia, decimal Weight, string Comments, decimal FinalPostage, bool IsManualTracking)
        {
            string errorMessage = string.Empty;
            
            ShippingLabelResponse response = new ShippingLabelResponse();
            response.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            int returnValue = 0;
            using (DBConnect db = new DBConnect())
            {
                DataTable dt = LoadPOLineitems(listitems);
                string[] arrSpFieldSeq;
                string sCode = string.Empty;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@PO_Num", shippingLabelInfo.FulfillmentNumber);
                    objCompHash.Add("@UserId", UserID);
                    objCompHash.Add("@TrackingNumber", shippingLabelInfo.TrackingNumber);
                    objCompHash.Add("@ShipmentType", shippingLabelInfo.ShipmentType);
                    objCompHash.Add("@ShipVia", ShipVia);
                    objCompHash.Add("@ShippingLabel", shippingLabelInfo.ShippingLabelImage);
                    objCompHash.Add("@ShipDate", ShipDate);
                    objCompHash.Add("@Comments", Comments);
                    objCompHash.Add("@Weight", Weight);
                    objCompHash.Add("@ShipPackage", Package);
                    objCompHash.Add("@FinalPostage", FinalPostage);
                    objCompHash.Add("@IsManualTracking", IsManualTracking);
                    //objCompHash.Add("@POShippingLineItem", dt);

                    arrSpFieldSeq = new string[] { "@PO_Num", "@UserId", "@TrackingNumber", "@ShipmentType", "@ShipVia", "@ShippingLabel",
                    "@ShipDate","@Comments","@Weight","@ShipPackage","@FinalPostage", "@IsManualTracking"//,"@POShippingLineItem"
                };
                    returnValue = db.ExecCommand(objCompHash, "av_PurchaseOrder_ShippingLabel_Update", arrSpFieldSeq);
                    if (returnValue > 0)
                    {
                        errorMessage = ResponseErrorCode.UpdatedSuccessfully.ToString();
                        response.ErrorCode = ResponseErrorCode.UpdatedSuccessfully.ToString();
                    }
                    else if (returnValue == -1)
                    {
                        errorMessage = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                        response.ErrorCode = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                    }
                    else
                    {
                        errorMessage = ResponseErrorCode.DataNotUpdated.ToString();
                        response.ErrorCode = ResponseErrorCode.DataNotUpdated.ToString();
                    }
                }
                catch (Exception objExp)
                {
                    response.ErrorCode = ResponseErrorCode.InternalError.ToString();
                    errorMessage = objExp.Message.ToString();
                    Logger.LogMessage(objExp, this); //  
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
                response.Comment = errorMessage;
            }
            return response;
        }

        public  ShippingLabelResponse ShippingLabelUpdateDBNew(ShippingLabelInfo shippingLabelInfo, int UserID, List<FulfillmentShippingLineItem> listitems, string ShipDate, string Package, string ShipVia, decimal Weight, string Comments, decimal FinalPostage, bool IsManualTracking, int poid, List<CustomValues> customValues)
        {
            ShippingLabelResponse response = new ShippingLabelResponse();
            string errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                DataTable dt = LoadPOLineitems(listitems);

                response.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
                ShippingLabelsDetails request = new ShippingLabelsDetails();
                request.FulfillmentNumber = shippingLabelInfo.FulfillmentNumber;
                request.TrackingNumber = shippingLabelInfo.TrackingNumber;
                request.ShipmentType = shippingLabelInfo.ShipmentType;
                request.Comments = Comments;
                request.ShipVia = ShipVia;
                request.ShipDate = ShipDate;
                request.Package = Package;
                request.Weight = Weight;
                request.FinalPostage = FinalPostage;
                request.IsManualTracking = IsManualTracking;
                request.FulfillmentNumber = shippingLabelInfo.FulfillmentNumber;
                request.customValues = customValues;
                FulfillmentLogModel logModel = new FulfillmentLogModel();
                logModel.ActionName = "Generate Label";
                logModel.CreateUserID = UserID;
                logModel.StatusID = 1;
                logModel.PO_ID = poid;
                logModel.FulfillmentNumber = string.Empty;
                logModel.Comment = string.Empty;
                string poXML = BaseAerovoice.SerializeObject<ShippingLabelsDetails>(request);
                poXML = "<ShippingLabelsDetails>" + poXML.Substring(poXML.IndexOf("<FulfillmentNumber>"));

                string customValueXML = clsGeneral.SerializeObject(customValues);

                logModel.RequestData = poXML;

                int returnValue = 0;
                // DBConnect db = new DBConnect();
                string[] arrSpFieldSeq;
                string sCode = string.Empty;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@PO_Num", shippingLabelInfo.FulfillmentNumber);
                    objCompHash.Add("@UserId", UserID);
                    objCompHash.Add("@TrackingNumber", shippingLabelInfo.TrackingNumber);
                    objCompHash.Add("@ShipmentType", shippingLabelInfo.ShipmentType);
                    objCompHash.Add("@ShipVia", ShipVia);
                    objCompHash.Add("@ShippingLabel", shippingLabelInfo.ShippingLabelImage);
                    objCompHash.Add("@ShipDate", ShipDate);
                    objCompHash.Add("@Comments", Comments);
                    objCompHash.Add("@Weight", Weight);
                    objCompHash.Add("@ShipPackage", Package);
                    objCompHash.Add("@FinalPostage", FinalPostage);
                    objCompHash.Add("@IsManualTracking", IsManualTracking);
                    objCompHash.Add("@POID", poid);
                    objCompHash.Add("@CustomValueXML", customValueXML);
                    //objCompHash.Add("@POShippingLineItem", dt);

                    arrSpFieldSeq = new string[] { "@PO_Num", "@UserId", "@TrackingNumber", "@ShipmentType", "@ShipVia", "@ShippingLabel",
                    "@ShipDate","@Comments","@Weight","@ShipPackage","@FinalPostage", "@IsManualTracking","@POID", "@CustomValueXML"//,"@POShippingLineItem"
                };
                    returnValue = db.ExecCommand(objCompHash, "av_PurchaseOrder_ShippingLabel_Update", arrSpFieldSeq);
                    if (returnValue > 0)
                    {
                        errorMessage = ResponseErrorCode.UpdatedSuccessfully.ToString();
                        response.ErrorCode = ResponseErrorCode.UpdatedSuccessfully.ToString();
                    }
                    else if (returnValue == -1)
                    {
                        errorMessage = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                        response.ErrorCode = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                    }
                    else
                    {
                        errorMessage = ResponseErrorCode.DataNotUpdated.ToString();
                        response.ErrorCode = ResponseErrorCode.DataNotUpdated.ToString();
                    }
                }
                catch (Exception objExp)
                {
                    response.ErrorCode = ResponseErrorCode.InternalError.ToString();
                    errorMessage = objExp.Message.ToString();
                    logModel.Comment = response.ErrorCode;
                    Logger.LogMessage(objExp, this); //  
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                    string POreposponseXml = BaseAerovoice.SerializeObject<ShippingLabelResponse>(response);
                    POreposponseXml = "<ShippingLabelResponse>" + POreposponseXml.Substring(POreposponseXml.IndexOf("<ErrorCode>"));

                    logModel.ResponseData = POreposponseXml;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
                    SV.Framework.DAL.Fulfillment.LogOperations logOperations = new LogOperations();
                    logOperations.FulfillmentLogInsert(logModel);
                    //SV.Framework.DAL.Fulfillment.LogOperations.FulfillmentLogInsert(logModel);

                }
                response.Comment = errorMessage;
            }
            return response;
        }

        private  DataTable LoadPOLineitems(List<FulfillmentShippingLineItem> lineItems)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("POD_ID", typeof(System.Int32));
            dt.Columns.Add("Quantity", typeof(System.Int32));



            DataRow row;
            if (lineItems != null && lineItems.Count > 0)
            {
                foreach (FulfillmentShippingLineItem item in lineItems)
                {
                    row = dt.NewRow();
                    row["POD_ID"] = item.PODID;
                    row["Quantity"] = item.Quantity;


                    dt.Rows.Add(row);
                }
            }


            return dt;
        }



        private  List<CustomValues> PopulateCustomValues(DataTable dataTable)
        {
            List<CustomValues> CustomValueList = new List<CustomValues>();
            CustomValues shippingLabel = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    shippingLabel = new CustomValues();
                    shippingLabel.SKU = clsGeneral.getColumnData(dataRow, "Item_Code", string.Empty, false) as string;
                    shippingLabel.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    shippingLabel.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                    shippingLabel.CustomValue = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "CustomValue", 0, false));
                    shippingLabel.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));


                    CustomValueList.Add(shippingLabel);
                }
            }
            return CustomValueList;

        }

        private  List<ShippingLabel> PopulateLabelInfo(DataTable dataTable)
        {
            List<ShippingLabel> labelList = new List<ShippingLabel>();
            ShippingLabel shippingLabel = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    shippingLabel = new ShippingLabel();
                    shippingLabel.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    shippingLabel.IMEI = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    shippingLabel.ICCID = clsGeneral.getColumnData(dataRow, "ICC_ID", string.Empty, false) as string;
                    shippingLabel.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;

                    labelList.Add(shippingLabel);
                }
            }
            return labelList;

        }


        //public  void ExportToPDF(List<ShippingLabel> _List)
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
        //public  string GenerateBarCode(string code)
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
        //public  string ConvertToImage(string base64String, string fileType)
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
        //public  iTextSharp.text.Image ImageCell(string filePath)
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

    //public class ShippingLabel
    //{
    //    public string SKU { get; set; }
    //    public string IMEI { get; set; }
    //    public string ICCID { get; set; }
    //    public string UPC { get; set; }
    //}
    //public class ShippingLabelInfo
    //{
    //    public byte[] ShippingLabel { get; set; }
    //    public string ShippingLabelImage { get; set; }
    //    public string FulfillmentNumber { get; set; }
    //    public string TrackingNumber { get; set; }
    //    public string ShipmentType { get; set; }
    //    // public string ShipDate { get; set; }
    //    // public double Weight { get; set; }
    //    // public string Package { get; set; }
    //    //  public string ShipVia { get; set; }
    //    //  public string Comments { get; set; }
    //    //  public List<FulfillmentShippingLineItem> LineItems { get; set; }

    //}
    //public class ShippingLabelsDetails
    //{

    //    public string ShippingLabelImage { get; set; }
    //    public string FulfillmentNumber { get; set; }
    //    public string TrackingNumber { get; set; }
    //    public string ShipmentType { get; set; }
    //    public string ShipDate { get; set; }
    //    public decimal Weight { get; set; }
    //    public decimal FinalPostage { get; set; }
    //    public string Package { get; set; }
    //    public string ShipVia { get; set; }
    //    public string Comments { get; set; }
    //    public bool IsManualTracking { get; set; }
    //    //  public List<FulfillmentShippingLineItem> LineItems { get; set; }
    //    public List<CustomValues> customValues { get; set; }

    //}
    //public class ShippingLabelRequest
    //{

    //    private clsAuthentication _auth;
    //    private ShippingLabelInfo shippingLabelInfo;

    //    public ShippingLabelRequest()
    //    {

    //        _auth = new clsAuthentication();
    //        shippingLabelInfo = new ShippingLabelInfo();
    //    }


    //    public clsAuthentication Authentication
    //    {
    //        get
    //        {
    //            return _auth;
    //        }
    //        set
    //        {
    //            _auth = value;
    //        }
    //    }

    //    //[XmlElement(ElementName = "Authentication", IsNullable = true)]
    //    public ShippingLabelInfo ShippingLabelDetail
    //    {
    //        get
    //        {
    //            return shippingLabelInfo;
    //        }
    //        set
    //        {
    //            shippingLabelInfo = value;
    //        }
    //    }
    //}
    //public class LabelRequest
    //{

    //    private clsAuthentication _auth;

    //    public LabelRequest()
    //    {

    //        _auth = new clsAuthentication();
    //    }



    //    //[XmlElement(ElementName = "Authentication", IsNullable = true)]
    //    public clsAuthentication Authentication
    //    {
    //        get
    //        {
    //            return _auth;
    //        }
    //        set
    //        {
    //            _auth = value;
    //        }
    //    }
    //}
    //public class LabelResponse
    //{
    //    public string ErrorCode { get; set; }
    //    public string Comment { get; set; }
    //    public List<ShippingLabel> LabelList { get; set; }

    //}
    //public class ShippingLabelResponse
    //{
    //    public string ErrorCode { get; set; }
    //    public string Comment { get; set; }


    //}
}
