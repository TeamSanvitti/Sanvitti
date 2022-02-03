using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.IO;
//using iTextSharp.text.pdf;
//using iTextSharp.text;

namespace SV.Framework.Fulfillment
{
    public class FulfillmentLabelOperation
    {
        public static MemoryStream LabelsPdf(IList<FulfillmentLabel> poList)
        {
            float width = 320, height = 520, envHeight = 500;
            string shipMethod = string.Empty, ShipPackage = string.Empty;

            byte[] bytes = new byte[64];
            //  byte[] bytes1 = new byte[64];
            byte[] bytes2 = new byte[64];
            Array.Clear(bytes, 0, bytes.Length);
            //   Array.Clear(bytes1, 0, bytes1.Length);
            Array.Clear(bytes2, 0, bytes2.Length);
//            string shipMethod = string.Empty;
            string labelBase64 = "";
            var memoryStream = new MemoryStream();

            Document document = new Document();
            iTextSharp.text.Rectangle envelope = new iTextSharp.text.Rectangle(320, 500);
            document.SetPageSize(envelope);
            document.SetMargins(0, 0, 0, 0);

            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            PdfContentByte cb = writer.DirectContent;
            PdfImportedPage page;

            PdfReader reader;
            //int i = 1;
            foreach (FulfillmentLabel po in poList)
            {
                shipMethod = po.ShipMethod;
                ShipPackage = po.ShipPackage;
                labelBase64 = po.ShippingLabelImage;
                if (shipMethod.ToLower() == "first" && ShipPackage.ToString().ToLower() == "letter")
                {
                    width = 500;
                    height = 320;
                    envHeight = 320;
                   
                }
                if("firstclassmailinternational" == shipMethod.ToLower())
                {
                    width = 500;
                    height = 320;
                    envHeight = 320;
                }
                envelope = new iTextSharp.text.Rectangle(width, envHeight);
                document.SetPageSize(envelope);

                if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), shipMethod))
                {
                    if (!string.IsNullOrWhiteSpace(labelBase64))
                    {

                        byte[] imageBytes = Convert.FromBase64String(labelBase64);
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);

                        image.ScaleToFit(width, height);
                        {
                            document.NewPage();


                            document.Add(image);
                            // bytes = memoryStream.ToArray();
                            // memoryStream.Close();


                        }
                    }
                }
                else
                {
                    // RegisterStartupScript("jsUnblockDialog", "closeLabelDialog();");
                    if (!string.IsNullOrWhiteSpace(labelBase64))
                    {
                        bytes2 = Convert.FromBase64String(labelBase64);
                        reader = new PdfReader(bytes2);
                        //  int pages = reader.NumberOfPages;
                        document.NewPage();
                        page = writer.GetImportedPage(reader, 1);
                        cb.AddTemplate(page, 0, 0);

                        //i = i + 1;
                        // document.Add(bytes2);
                        // Response.End();

                    }
                }


            }
            writer.CloseStream = false;
            document.Close();
            memoryStream.Position = 0;

            return memoryStream;

        }
        public static List<FulfillmentLabel> GetFulfillments(string fromDate, string toDate, string poNum, int statusId, string ShipVia, string poType, int companyID)
        {
            DBConnect db = new DBConnect();
            List<FulfillmentLabel> poList = null;
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@CompanyID", companyID);
                objParams.Add("@Po_Num", poNum);
                objParams.Add("@From_Date", string.IsNullOrWhiteSpace(fromDate) ? null : fromDate);
                objParams.Add("@To_Date", string.IsNullOrWhiteSpace(toDate) ? null : toDate);
                objParams.Add("@StatusID", statusId);
                objParams.Add("@ShipVia", ShipVia);
                objParams.Add("@POType", poType);

                arrSpFieldSeq =
                new string[] { "@CompanyID", "@Po_Num", "@From_Date", "@To_Date", "@StatusID", "@ShipVia", "@POType" };


                dataTable = db.GetTableRecords(objParams, "Av_PurchaseOrder_Batch_Select", arrSpFieldSeq);
                poList = PopulatePOs(dataTable);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return poList;
        }

        public static List<FulfillmentLabel> GetLabels(string PoNums, int companyID)
        {
            DBConnect db = new DBConnect();
            List<FulfillmentLabel> poList = null;
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@CompanyID", companyID);
                objParams.Add("@Po_Nums", PoNums);
                
                arrSpFieldSeq =
                new string[] { "@CompanyID", "@Po_Nums" };


                dataTable = db.GetTableRecords(objParams, "Av_PurchaseOrder_BatchLabel_Select", arrSpFieldSeq);
                poList = PopulatePOLabels(dataTable);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return poList;
        }


        private static List<FulfillmentLabel> PopulatePOLabels(DataTable dataTable)
        {
            List<FulfillmentLabel> poList = new List<FulfillmentLabel>();
            FulfillmentLabel fulfillmentLabel = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    fulfillmentLabel = new FulfillmentLabel();
                    fulfillmentLabel.ShipMethod = clsGeneral.getColumnData(dataRow, "ShipByCode", string.Empty, false) as string;
                    fulfillmentLabel.ShippingLabelImage = clsGeneral.getColumnData(dataRow, "ShippingLabel", string.Empty, false) as string;
                    fulfillmentLabel.ShipPackage = clsGeneral.getColumnData(dataRow, "ShipPackage", string.Empty, false) as string;
                    poList.Add(fulfillmentLabel);


                }
            }
            return poList;
        }

        private static List<FulfillmentLabel> PopulatePOs(DataTable dataTable)
        {
            List<FulfillmentLabel> poList = new List<FulfillmentLabel>();
            FulfillmentLabel fulfillmentLabel = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    fulfillmentLabel = new FulfillmentLabel();
                    fulfillmentLabel.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                    fulfillmentLabel.FulfillmentDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.MinValue, false)).ToShortDateString();
                    fulfillmentLabel.ContactName = clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, false) as string;
                    fulfillmentLabel.ContactPhone = clsGeneral.getColumnData(dataRow, "Contact_Phone", string.Empty, false) as string;
                    fulfillmentLabel.ShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.MinValue, false)).ToShortDateString();
                    fulfillmentLabel.ShipMethod = clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false) as string;
                   // fulfillmentLabel.ShippingLabelImage = clsGeneral.getColumnData(dataRow, "ShippingLabel", string.Empty, false) as string;
                    fulfillmentLabel.Status = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                    fulfillmentLabel.StoreID = clsGeneral.getColumnData(dataRow, "Store_ID", string.Empty, false) as string;
                    fulfillmentLabel.StreetAddress1 = clsGeneral.getColumnData(dataRow, "ShipTo_Address", string.Empty, false) as string;
                    fulfillmentLabel.StreetAddress2 = clsGeneral.getColumnData(dataRow, "ShipTo_Address2", string.Empty, false) as string;
                    fulfillmentLabel.ShipToCity = clsGeneral.getColumnData(dataRow, "ShipTo_City", string.Empty, false) as string;
                    fulfillmentLabel.ShipToState = clsGeneral.getColumnData(dataRow, "ShipTo_State", string.Empty, false) as string;
                    fulfillmentLabel.ShipToZip = clsGeneral.getColumnData(dataRow, "ShipTo_Zip", string.Empty, false) as string;
                    fulfillmentLabel.POID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, false));

                    fulfillmentLabel.ShippingWeight = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "ShippingWeight", 0, false));

                    fulfillmentLabel.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                    fulfillmentLabel.ShipPackage = clsGeneral.getColumnData(dataRow, "ShipPackage", string.Empty, false) as string;
                    fulfillmentLabel.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;


                    //fulfillmentLabel.LogID = Convert.ToInt64(clsGeneral.getColumnData(dataRow, "LogID", 0, false));
                    //fulfillmentLabel. = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "ExceptionOccured", false, false));
                    //log.TimeDifference = Convert.ToInt64(clsGeneral.getColumnData(dataRow, "TimeDifference", 0, false));
                    poList.Add(fulfillmentLabel);

                }
            }
            return poList;
        }

    }
}