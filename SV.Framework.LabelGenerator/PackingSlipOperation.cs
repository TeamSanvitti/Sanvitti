using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Http;
using System.Threading.Tasks;
//using System.Drawing;


namespace SV.Framework.LabelGenerator
{
    public class PackingSlipOperation
    {

        private const float BARCODE_HEIGHT = 14f;
        private const float BARCODE_WIDTH = 180f;
        // private const float PDF_PAGE_WIDTH_PERCENT = 100f;
        // private const float TEXT_FONT_SIZE = 7f;
        // private const float LABEL_WIDTH_SIZE = 216;
        // private const float LABEL_HEIGHT_SIZE = 140;//128

        public MemoryStream ExportToPDF(PurchaseOrderInfo purchaseOrderInfo)
        {
            if (purchaseOrderInfo != null && !string.IsNullOrWhiteSpace(purchaseOrderInfo.PurchaseOrderNumber))
                return PackingSlipPdf(purchaseOrderInfo);
            else
                return null;
        }
        
        private iTextSharp.text.Image GenerateBarCode(string code)
        {
            iTextSharp.text.Image image = null;
            if (!string.IsNullOrWhiteSpace(code))
            {
                Barcode128 code39 = new Barcode128();
                code39.Code = code;
                //code39.X = 2;
                code39.CodeType = Barcode.CODE128;
                code39.TextAlignment = 1;
                code39.StartStopText = true;
                code39.GenerateChecksum = true;
                code39.Extended = true;
                code39.BarHeight = BARCODE_HEIGHT;

                code39.TextAlignment = Element.ALIGN_CENTER;
                System.Drawing.Image img = code39.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                //System.Drawing.Image img = code39.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);

                if (img != null)
                {
                    image = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //image = iTextSharp.text.Image.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);

                    image.ScaleToFit(BARCODE_WIDTH, BARCODE_HEIGHT);
                    image.Alignment = Element.ALIGN_LEFT;
                }
            }

            return image;
        }

        private MemoryStream PackingSlipPdf(PurchaseOrderInfo purchaseOrderInfo)
        {
            var memoryStream = new MemoryStream();
            List<ProductModel> productslist = new List<ProductModel>();
            productslist = purchaseOrderInfo.ProductsList;

            iTextSharp.text.Image barcodeimage;
            //if (!string.IsNullOrEmpty(purchaseOrderInfo.SalesOrder))
            //{
            //    barcodeimage = GenerateBarCode(purchaseOrderInfo.SalesOrder);
            //}

            Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 50f, 40f);
            PdfPTable table;
            PdfPCell cell = null;
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);
            pdfDoc.Open();


            PdfPTable tablespace = new PdfPTable(1);
            tablespace.TotalWidth = 575f;
            tablespace.LockedWidth = true;
            tablespace.SetWidths(new float[] { 0.2f });

            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));



            table = new PdfPTable(3);
            table.TotalWidth = 575f;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 150f, 60f, 90f });
            PdfPCell cellnew = null;
            cellnew = new PdfPCell();
             cellnew.Colspan = 3;

            string imageurl = purchaseOrderInfo.CompanyLogo;
            if (purchaseOrderInfo.CompanyLogo != "")
            {

                cellnew = ImageCell(imageurl, 120f, PdfPCell.ALIGN_CENTER);
                //     addCellwithborderNormal(table, "STS MEDIA, INC DBA FREEDOMPOP", 0.5f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                //     addCellwithborderNormal(table, "12031 SHERMAN WAY", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                //     addCellwithborderNormal(table, "NORTH HOLYWOOD CA 91605", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);


            }
            else
            {
                cellnew = new PdfPCell();
                cellnew.BorderWidth = 0;
                cellnew.BorderColor = BaseColor.WHITE;
            }
            cellnew.Rowspan = 5;


            table.AddCell(cellnew);





            cellnew = new PdfPCell(new Phrase());
            addCellwithborderNew(table, "Packing Slip ", 0.5f, 0f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
            addCellwithborderNormal(table, purchaseOrderInfo.PackingSlip, 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNew(table, "Document Date ", 0.5f, 0f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
            addCellwithborderNormal(table, purchaseOrderInfo.DocumentDate, 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNew(table, "Page ", 0.5f, 0f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
            addCellwithborderNormal(table, purchaseOrderInfo.Page.ToString(), 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNew(table, "Who Printed ", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
            addCellwithborderNormal(table, purchaseOrderInfo.WhoPrinted, 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNew(table, "Date/Time Printed ", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
            addCellwithborderNormal(table, purchaseOrderInfo.DateTimePrinted, 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

          //  addCellwithborderNew(table, "FreedomPop", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
           // addCellwithborderNormal(table, "North Hollywood CA", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);


            PdfPCell cell1 = null;
            cell1 = new PdfPCell();
            cell1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell1.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            cell1.PaddingRight = 16f;

            if (!string.IsNullOrWhiteSpace(purchaseOrderInfo.PurchaseOrderNumber))
            {
                barcodeimage = GenerateBarCode(purchaseOrderInfo.SalesOrder);
                //table = new PdfPTable(3);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //float[] widths12 = new float[] { 250.6f, 30.6f, 280.6f };
                //table.SetWidths(widths12);
                //addCellwithborderNormal(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                //addCellwithborderNormal(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                cell1 = ImageCellNew(barcodeimage, 150f, PdfPCell.ALIGN_LEFT);
               // table.AddCell(cell1);
                // pdfDoc.Add(table);
            }
            //addCellwithborderNormalTop(table, "STS MEDIA, INC DBA FREEDOMPOP", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            //addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            //addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            //addCellwithborderNormal2(table, "12031 SHERMAN WAY", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            //addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            //addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);

            //addCellwithborderNormal2(table, "NORTH HOLYWOOD CA 91605", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            //table.AddCell(cell1);
            //addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);


            addCellwithborderNormalTop(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);

            addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            table.AddCell(cell1);
            addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);

            pdfDoc.Add(table);

            //pdfDoc.Add(tablespace);

            //pdfDoc.Add(tablespace);


            table = new PdfPTable(3);
            table.TotalWidth = 575f;
            table.LockedWidth = true;
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            float[] widths = new float[] { 250.6f, 30.6f, 280.6f };
            table.SetWidths(widths);

            addCellwithborderNew(table, "Ship From:", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNew(table, "Ship To:", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            pdfDoc.Add(table);

            table = new PdfPTable(3);
            table.TotalWidth = 575f;
            table.LockedWidth = true;
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            widths = new float[] { 250.6f, 30.6f, 280.6f };
            table.SetWidths(widths);

            addCellwithborderNormalTop(table, purchaseOrderInfo.CompanyName, 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table, "", 0f, 0f, 0.5f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormalTop(table, purchaseOrderInfo.CustomerName, 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            pdfDoc.Add(table);

            table = new PdfPTable(3);
            table.TotalWidth = 575f;
            table.LockedWidth = true;
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            float[] widths1 = new float[] { 250.6f, 30.6f, 280.6f };
            table.SetWidths(widths1);

            if (string.IsNullOrEmpty(purchaseOrderInfo.AddressLine2))
                addCellwithborderNormal2(table, purchaseOrderInfo.AddressLine1, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            else
                addCellwithborderNormal2(table, purchaseOrderInfo.AddressLine1 + ", " + purchaseOrderInfo.AddressLine2, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

            addCellwithborderNormal(table, "", 0f, 0f, 0.5f, 0f, BaseColor.WHITE, BaseColor.BLACK);

            if (string.IsNullOrEmpty(purchaseOrderInfo.ShippingAddressLine2))
                addCellwithborderNormal2(table, purchaseOrderInfo.ShippingAddressLine1, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            else
                addCellwithborderNormal2(table, purchaseOrderInfo.ShippingAddressLine1 + ", " + purchaseOrderInfo.ShippingAddressLine2, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

            pdfDoc.Add(table);

            table = new PdfPTable(3);
            table.TotalWidth = 575f;
            table.LockedWidth = true;
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            widths1 = new float[] { 250.6f, 30.6f, 280.6f };
            table.SetWidths(widths1);

            addCellwithborderNormal2(table, purchaseOrderInfo.City + " " + purchaseOrderInfo.State + " " + purchaseOrderInfo.ZipCode, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table, "", 0f, 0f, 0.5f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal2(table, purchaseOrderInfo.ShippingCity + " " + purchaseOrderInfo.ShippingState + " " + purchaseOrderInfo.ShippingZipCode, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            pdfDoc.Add(table);
            table = new PdfPTable(3);
            table.TotalWidth = 575f;
            table.LockedWidth = true;
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            widths1 = new float[] { 250.6f, 30.6f, 280.6f };
            table.SetWidths(widths1);

            addCellwithborderNormalBottom(table, purchaseOrderInfo.Country, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table, "", 0f, 0f, 0.5f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormalBottom(table, purchaseOrderInfo.ShippingCountry, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            pdfDoc.Add(table);

            table = new PdfPTable(3);
            table.TotalWidth = 575f;
            table.LockedWidth = true;
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            widths1 = new float[] { 250.6f, 30.6f, 280.6f };
            table.SetWidths(widths1);

            addCellwithborderNormal(table, "", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table, "", 0f, 0f, 0.5f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table, "", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            pdfDoc.Add(table);


            pdfDoc.Add(tablespace);
            table = new PdfPTable(1);
            table.TotalWidth = 575f;
            table.LockedWidth = true;
            table.AddCell(PhraseCell(new Phrase("* Item Shipped Directly from Vendor", FontFactory.GetFont("Normal", 11, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
            pdfDoc.Add(table);

            PdfPTable table1 = new PdfPTable(6);
            table1.TotalWidth = 575f;
            table1.LockedWidth = true;
            table1.DefaultCell.Border = Rectangle.NO_BORDER;
            widths1 = new float[] { 101.3f, 84.3f, 83.3f, 94.3f, 102.3f, 85.3f };
            table1.SetWidths(widths1);
            addCellwithborderNew(table1, "Purchase Order No.", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
            addCellwithborderNew(table1, "Customer ID", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
            addCellwithborderNew(table1, "Sales Order", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
            addCellwithborderNew(table1, "Shipping Method", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
            addCellwithborderNew(table1, "Req Shipping Date", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
            addCellwithborderNew(table1, "Master No.", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
            pdfDoc.Add(table1);
            table1 = new PdfPTable(6);
            table1.TotalWidth = 575f;
            table1.LockedWidth = true;
            table1.DefaultCell.Border = Rectangle.NO_BORDER;
            widths = new float[] { 101.3f, 84.3f, 83.3f, 94.3f, 102.3f, 85.3f };
            table1.SetWidths(widths);

            addCellwithborderNormal(table1, purchaseOrderInfo.PurchaseOrderNumber, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table1, purchaseOrderInfo.CustomerId, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table1, purchaseOrderInfo.SalesOrder, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table1, purchaseOrderInfo.ShippingMethod, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table1, purchaseOrderInfo.ReqShippingDate, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table1, purchaseOrderInfo.MasterNumber, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
            pdfDoc.Add(table1);

            pdfDoc.Add(tablespace);

            table1 = new PdfPTable(6);
            table1.TotalWidth = 575f;
            table1.LockedWidth = true;
            table1.DefaultCell.Border = Rectangle.NO_BORDER;
            float[] widths2 = new float[] { 56.3f, 68.3f, 50.3f, 115.3f, 115.3f, 70.3f };
            table1.SetWidths(widths2);
            addCellwithborderNew(table1, "Units Order", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
            addCellwithborderNew(table1, "Units Shipped", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
            addCellwithborderNew(table1, "Units B/O", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
            addCellwithborderNew(table1, "Item Number", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
            addCellwithborderNew(table1, "Description", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
            addCellwithborderNew(table1, "QTY Shipped", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
            pdfDoc.Add(table1);

            if (productslist != null && productslist.Count > 0)
            {

                for (int i = 0; i < productslist.Count; i++)
                {
                    table1 = new PdfPTable(6);
                    table1.TotalWidth = 575f;
                    table1.LockedWidth = true;
                    table1.DefaultCell.Border = Rectangle.NO_BORDER;
                    float[] widthsNew = new float[] { 56.3f, 68.3f, 50.3f, 115.3f, 115.3f, 70.3f };
                    table1.SetWidths(widthsNew);

                    addCellwithborderRight(table1, Convert.ToString(productslist[i].UnitsOrdered), 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    addCellwithborderRight(table1, Convert.ToString(productslist[i].UnitsShipped), 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    addCellwithborderRight(table1, Convert.ToString(productslist[i].UnitsBO), 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    addCellwithborderNormal(table1, Convert.ToString(productslist[i].ItemNumber), 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    addCellwithborderNormal(table1, Convert.ToString(productslist[i].Description), 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    addCellwithborderRight(table1, Convert.ToString(productslist[i].QtyShipped), 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table1);
                }
            }
            table1 = new PdfPTable(6);
            table1.TotalWidth = 575f;
            table1.LockedWidth = true;
            table1.DefaultCell.Border = Rectangle.NO_BORDER;
            float[] widthsNew1 = new float[] { 63.3f, 68.3f, 63.3f, 81.3f, 106.3f, 93.3f };
            table1.SetWidths(widthsNew1);

            addCellwithborderNormal(table1, "", 0f, 0.5f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table1, "", 0f, 0.5f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table1, "", 0f, 0.5f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table1, "", 0f, 0.5f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table1, "", 0f, 0.5f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table1, "", 0f, 0.5f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            pdfDoc.Add(table1);

            //pdfDoc.Add(tablespace);
            //pdfDoc.Add(tablespace);
            //pdfDoc.Add(tablespace);
            //pdfDoc.Add(tablespace);
            //pdfDoc.Add(tablespace);
            //pdfDoc.Add(tablespace);
            //pdfDoc.Add(tablespace); 
            //pdfDoc.Add(tablespace);
            //pdfDoc.Add(tablespace);
            //pdfDoc.Add(tablespace);
            //pdfDoc.Add(tablespace);
            //pdfDoc.Add(tablespace);

            table1 = new PdfPTable(6);
            table1.TotalWidth = 575f;
            table1.LockedWidth = true;
            table1.DefaultCell.Border = Rectangle.NO_BORDER;
            widthsNew1 = new float[] { 80.3f, 48.3f, 63.3f, 81.3f, 106.3f, 93.3f };
            table1.SetWidths(widthsNew1);

            addCellwithborderNormal(table1, purchaseOrderInfo.CompanyName, 0.5f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table1, "", 0.5f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table1, "", 0.5f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table1, "", 0.5f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderNormal(table1, "", 0.5f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            addCellwithborderRight(table1, "(855) 703-5785", 0.5f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
            // pdfDoc.Add(table1);

            table1.WriteSelectedRows(0, -1, 0, pdfDoc.Bottom, pdfWriter.DirectContent);

            // Close PDF document and send
            pdfWriter.CloseStream = false;
            pdfDoc.Close();

            memoryStream.Position = 0;



            return memoryStream;
        }
        public MemoryStream ExportToPDFNew(List<PurchaseOrderInfo> poList)
        {
            if (poList != null && poList.Count > 0)
                return PackingSlipPdfNew(poList);
            else
                return null;
        }
        private MemoryStream PackingSlipPdfNew(List<PurchaseOrderInfo> poList)
        {
            //PurchaseOrderInfo purchaseOrderInfo = null;
            var memoryStream = new MemoryStream();
            List<ProductModel> productslist = null;
            
            iTextSharp.text.Image barcodeimage;
            //if (!string.IsNullOrEmpty(purchaseOrderInfo.SalesOrder))
            //{
            //    barcodeimage = GenerateBarCode(purchaseOrderInfo.SalesOrder);
            //}

            Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 50f, 40f);
            PdfPTable table;
            PdfPCell cell = null;
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);
            pdfDoc.Open();

            //Parallel.ForEach(poList, purchaseOrderInfo =>

            foreach (PurchaseOrderInfo purchaseOrderInfo in poList)
            {
                pdfDoc.NewPage();
                productslist = new List<ProductModel>();
                productslist = purchaseOrderInfo.ProductsList;

                PdfPTable tablespace = new PdfPTable(1);
                tablespace.TotalWidth = 575f;
                tablespace.LockedWidth = true;
                tablespace.SetWidths(new float[] { 0.2f });

                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));



                table = new PdfPTable(3);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 150f, 60f, 90f });
                PdfPCell cellnew = null;
                cellnew = new PdfPCell();
                cellnew.Colspan = 3;

                string imageurl = purchaseOrderInfo.CompanyLogo;
                if (purchaseOrderInfo.CompanyLogo != "")
                {

                    cellnew = ImageCell(imageurl, 120f, PdfPCell.ALIGN_CENTER);
                    //     addCellwithborderNormal(table, "STS MEDIA, INC DBA FREEDOMPOP", 0.5f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                    //     addCellwithborderNormal(table, "12031 SHERMAN WAY", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                    //     addCellwithborderNormal(table, "NORTH HOLYWOOD CA 91605", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);


                }
                else
                {
                    cellnew = new PdfPCell();
                    cellnew.BorderWidth = 0;
                    cellnew.BorderColor = BaseColor.WHITE;
                }
                cellnew.Rowspan = 5;


                table.AddCell(cellnew);





                cellnew = new PdfPCell(new Phrase());
                addCellwithborderNew(table, "Packing Slip ", 0.5f, 0f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
                addCellwithborderNormal(table, purchaseOrderInfo.PackingSlip, 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNew(table, "Document Date ", 0.5f, 0f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
                addCellwithborderNormal(table, purchaseOrderInfo.DocumentDate, 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNew(table, "Page ", 0.5f, 0f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
                addCellwithborderNormal(table, purchaseOrderInfo.Page.ToString(), 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNew(table, "Who Printed ", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
                addCellwithborderNormal(table, purchaseOrderInfo.WhoPrinted, 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNew(table, "Date/Time Printed ", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
                addCellwithborderNormal(table, purchaseOrderInfo.DateTimePrinted, 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

                //  addCellwithborderNew(table, "FreedomPop", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
                // addCellwithborderNormal(table, "North Hollywood CA", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);


                PdfPCell cell1 = null;
                cell1 = new PdfPCell();
                cell1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                cell1.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                cell1.PaddingRight = 16f;

                if (!string.IsNullOrWhiteSpace(purchaseOrderInfo.PurchaseOrderNumber))
                {
                    barcodeimage = GenerateBarCode(purchaseOrderInfo.SalesOrder);
                    //table = new PdfPTable(3);
                    //table.TotalWidth = 575f;
                    //table.LockedWidth = true;
                    //float[] widths12 = new float[] { 250.6f, 30.6f, 280.6f };
                    //table.SetWidths(widths12);
                    //addCellwithborderNormal(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                    //addCellwithborderNormal(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                    cell1 = ImageCellNew(barcodeimage, 150f, PdfPCell.ALIGN_LEFT);
                    // table.AddCell(cell1);
                    // pdfDoc.Add(table);
                }
                //addCellwithborderNormalTop(table, "STS MEDIA, INC DBA FREEDOMPOP", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                //addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                //addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                //addCellwithborderNormal2(table, "12031 SHERMAN WAY", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                //addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                //addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);

                //addCellwithborderNormal2(table, "NORTH HOLYWOOD CA 91605", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                //table.AddCell(cell1);
                //addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);


                addCellwithborderNormalTop(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);

                addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                table.AddCell(cell1);
                addCellwithborderNormal2(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);

                pdfDoc.Add(table);

                //pdfDoc.Add(tablespace);

                //pdfDoc.Add(tablespace);


                table = new PdfPTable(3);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                float[] widths = new float[] { 250.6f, 30.6f, 280.6f };
                table.SetWidths(widths);

                addCellwithborderNew(table, "Ship From:", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNew(table, "Ship To:", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250.6f, 30.6f, 280.6f };
                table.SetWidths(widths);

                addCellwithborderNormalTop(table, purchaseOrderInfo.CompanyName, 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table, "", 0f, 0f, 0.5f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormalTop(table, purchaseOrderInfo.CustomerName, 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                float[] widths1 = new float[] { 250.6f, 30.6f, 280.6f };
                table.SetWidths(widths1);

                if (string.IsNullOrEmpty(purchaseOrderInfo.AddressLine2))
                    addCellwithborderNormal2(table, purchaseOrderInfo.AddressLine1, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                else
                    addCellwithborderNormal2(table, purchaseOrderInfo.AddressLine1 + ", " + purchaseOrderInfo.AddressLine2, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

                addCellwithborderNormal(table, "", 0f, 0f, 0.5f, 0f, BaseColor.WHITE, BaseColor.BLACK);

                if (string.IsNullOrEmpty(purchaseOrderInfo.ShippingAddressLine2))
                    addCellwithborderNormal2(table, purchaseOrderInfo.ShippingAddressLine1, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                else
                    addCellwithborderNormal2(table, purchaseOrderInfo.ShippingAddressLine1 + ", " + purchaseOrderInfo.ShippingAddressLine2, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths1 = new float[] { 250.6f, 30.6f, 280.6f };
                table.SetWidths(widths1);

                addCellwithborderNormal2(table, purchaseOrderInfo.City + " " + purchaseOrderInfo.State + " " + purchaseOrderInfo.ZipCode, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table, "", 0f, 0f, 0.5f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal2(table, purchaseOrderInfo.ShippingCity + " " + purchaseOrderInfo.ShippingState + " " + purchaseOrderInfo.ShippingZipCode, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                table = new PdfPTable(3);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths1 = new float[] { 250.6f, 30.6f, 280.6f };
                table.SetWidths(widths1);

                addCellwithborderNormalBottom(table, purchaseOrderInfo.Country, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table, "", 0f, 0f, 0.5f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormalBottom(table, purchaseOrderInfo.ShippingCountry, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths1 = new float[] { 250.6f, 30.6f, 280.6f };
                table.SetWidths(widths1);

                addCellwithborderNormal(table, "", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table, "", 0f, 0f, 0.5f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table, "", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);


                pdfDoc.Add(tablespace);
                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.AddCell(PhraseCell(new Phrase("* Item Shipped Directly from Vendor", FontFactory.GetFont("Normal", 11, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                pdfDoc.Add(table);

                PdfPTable table1 = new PdfPTable(6);
                table1.TotalWidth = 575f;
                table1.LockedWidth = true;
                table1.DefaultCell.Border = Rectangle.NO_BORDER;
                widths1 = new float[] { 101.3f, 84.3f, 83.3f, 94.3f, 102.3f, 85.3f };
                table1.SetWidths(widths1);
                addCellwithborderNew(table1, "Purchase Order No.", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
                addCellwithborderNew(table1, "Customer ID", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
                addCellwithborderNew(table1, "Sales Order", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
                addCellwithborderNew(table1, "Shipping Method", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
                addCellwithborderNew(table1, "Req Shipping Date", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
                addCellwithborderNew(table1, "Master No.", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
                pdfDoc.Add(table1);
                table1 = new PdfPTable(6);
                table1.TotalWidth = 575f;
                table1.LockedWidth = true;
                table1.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 101.3f, 84.3f, 83.3f, 94.3f, 102.3f, 85.3f };
                table1.SetWidths(widths);

                addCellwithborderNormal(table1, purchaseOrderInfo.PurchaseOrderNumber, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table1, purchaseOrderInfo.CustomerId, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table1, purchaseOrderInfo.SalesOrder, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table1, purchaseOrderInfo.ShippingMethod, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table1, purchaseOrderInfo.ReqShippingDate, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table1, purchaseOrderInfo.MasterNumber, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table1);

                pdfDoc.Add(tablespace);

                table1 = new PdfPTable(6);
                table1.TotalWidth = 575f;
                table1.LockedWidth = true;
                table1.DefaultCell.Border = Rectangle.NO_BORDER;
                float[] widths2 = new float[] { 56.3f, 68.3f, 50.3f, 115.3f, 115.3f, 70.3f };
                table1.SetWidths(widths2);
                addCellwithborderNew(table1, "Units Order", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
                addCellwithborderNew(table1, "Units Shipped", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
                addCellwithborderNew(table1, "Units B/O", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
                addCellwithborderNew(table1, "Item Number", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
                addCellwithborderNew(table1, "Description", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
                addCellwithborderNew(table1, "QTY Shipped", 0.5f, 0.5f, 0.5f, 0.5f, BaseColor.LIGHT_GRAY, BaseColor.BLACK);
                pdfDoc.Add(table1);

                if (productslist != null && productslist.Count > 0)
                {

                    for (int i = 0; i < productslist.Count; i++)
                    {
                        table1 = new PdfPTable(6);
                        table1.TotalWidth = 575f;
                        table1.LockedWidth = true;
                        table1.DefaultCell.Border = Rectangle.NO_BORDER;
                        float[] widthsNew = new float[] { 56.3f, 68.3f, 50.3f, 115.3f, 115.3f, 70.3f };
                        table1.SetWidths(widthsNew);

                        addCellwithborderRight(table1, Convert.ToString(productslist[i].UnitsOrdered), 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                        addCellwithborderRight(table1, Convert.ToString(productslist[i].UnitsShipped), 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                        addCellwithborderRight(table1, Convert.ToString(productslist[i].UnitsBO), 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                        addCellwithborderNormal(table1, Convert.ToString(productslist[i].ItemNumber), 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                        addCellwithborderNormal(table1, Convert.ToString(productslist[i].Description), 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                        addCellwithborderRight(table1, Convert.ToString(productslist[i].QtyShipped), 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                        pdfDoc.Add(table1);
                    }
                }
                table1 = new PdfPTable(6);
                table1.TotalWidth = 575f;
                table1.LockedWidth = true;
                table1.DefaultCell.Border = Rectangle.NO_BORDER;
                float[] widthsNew1 = new float[] { 63.3f, 68.3f, 63.3f, 81.3f, 106.3f, 93.3f };
                table1.SetWidths(widthsNew1);

                addCellwithborderNormal(table1, "", 0f, 0.5f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table1, "", 0f, 0.5f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table1, "", 0f, 0.5f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table1, "", 0f, 0.5f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table1, "", 0f, 0.5f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table1, "", 0f, 0.5f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table1);

                //pdfDoc.Add(tablespace);
                //pdfDoc.Add(tablespace);
                //pdfDoc.Add(tablespace);
                //pdfDoc.Add(tablespace);
                //pdfDoc.Add(tablespace);
                //pdfDoc.Add(tablespace);
                //pdfDoc.Add(tablespace); 
                //pdfDoc.Add(tablespace);
                //pdfDoc.Add(tablespace);
                //pdfDoc.Add(tablespace);
                //pdfDoc.Add(tablespace);
                //pdfDoc.Add(tablespace);

                table1 = new PdfPTable(6);
                table1.TotalWidth = 575f;
                table1.LockedWidth = true;
                table1.DefaultCell.Border = Rectangle.NO_BORDER;
                widthsNew1 = new float[] { 80.3f, 48.3f, 63.3f, 81.3f, 106.3f, 93.3f };
                table1.SetWidths(widthsNew1);

                addCellwithborderNormal(table1, purchaseOrderInfo.CompanyName, 0.5f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table1, "", 0.5f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table1, "", 0.5f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table1, "", 0.5f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal(table1, "", 0.5f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderRight(table1, "(855) 703-5785", 0.5f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                // pdfDoc.Add(table1);

                table1.WriteSelectedRows(0, -1, 0, pdfDoc.Bottom, pdfWriter.DirectContent);
            }
            // Close PDF document and send
            pdfWriter.CloseStream = false;
            pdfDoc.Close();

            memoryStream.Position = 0;



            return memoryStream;
        }

        private static void addCellwithborderNormal2(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            Font times = new Font(bfTimes, 12, Font.NORMAL, forecolor);

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = top;
            cell.BorderWidthBottom = bottom;
            cell.BorderWidthRight = right;
            cell.BorderWidthLeft = left;
            cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = 4;
            cell.PaddingTop = 1;
            cell.PaddingBottom = 1;
            cell.PaddingRight = 4;
            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }
        private static void addCellwithborderNormalTop(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            Font times = new Font(bfTimes, 12, Font.NORMAL, forecolor);

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = top;
            cell.BorderWidthBottom = bottom;
            cell.BorderWidthRight = right;
            cell.BorderWidthLeft = left;
            cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = 4;
            cell.PaddingTop = 4;
            cell.PaddingBottom = 1;
            cell.PaddingRight = 4;
            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }
        private static void addCellwithborderNormalBottom(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            Font times = new Font(bfTimes, 12, Font.NORMAL, forecolor);

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = top;
            cell.BorderWidthBottom = bottom;
            cell.BorderWidthRight = right;
            cell.BorderWidthLeft = left;
            cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = 4;
            cell.PaddingTop = 1;
            cell.PaddingBottom = 4;
            cell.PaddingRight = 4;
            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }

        private static void addCellwithborderNormal(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            Font times = new Font(bfTimes, 12, Font.NORMAL, forecolor);

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = top;
            cell.BorderWidthBottom = bottom;
            cell.BorderWidthRight = right;
            cell.BorderWidthLeft = left;
            cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = 4;
            cell.PaddingTop = 4;
            cell.PaddingBottom = 4;
            cell.PaddingRight = 4;
            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }
        private static void addCellwithborderRight(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            Font times = new Font(bfTimes, 12, Font.NORMAL, forecolor);

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = top;
            cell.BorderWidthBottom = bottom;
            cell.BorderWidthRight = right;
            cell.BorderWidthLeft = left;
            cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = 4;
            cell.PaddingTop = 4;
            cell.PaddingBottom = 4;
            cell.PaddingRight = 4;
            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }

        private static PdfPCell ImageCell(string path, float scale, int align)
        {
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.FixedHeight = 30f;
            cell.BorderColor = BaseColor.WHITE;
            cell.BorderWidthBottom = 1;
            cell.BorderColorBottom = BaseColor.BLACK;

            cell.VerticalAlignment = Rectangle.ALIGN_TOP;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 0f;
            cell.PaddingTop = 0f;
            return cell;
        }
        private static PdfPCell ImageCellNew(Image img, float scale, int align)
        {
            iTextSharp.text.Image image = img;
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.FixedHeight = 40f;
            cell.BorderColor = BaseColor.WHITE;
            cell.VerticalAlignment = Rectangle.ALIGN_TOP;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 0f;
            cell.PaddingTop = 6f;
            return cell;
        }

        private static void addCellwithborderNopadding(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            Font times = new Font(bfTimes, 12, Font.BOLD, forecolor);

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.BorderColor = BaseColor.BLACK;
            cell.BorderWidthTop = top;
            cell.BorderWidthBottom = bottom;
            cell.BorderWidthRight = right;
            cell.BorderWidthLeft = left;
            //cell.PaddingTop = -2;
            cell.PaddingLeft = 4;
            cell.PaddingTop = 4;
            cell.PaddingBottom = 4;
            cell.PaddingRight = 4;

            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;

            table.SpacingAfter = 1;
            table.AddCell(cell);
        }
        private static void addCellwithborderNew(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            Font times = new Font(bfTimes, 12, Font.BOLD, forecolor);

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.BorderColor = BaseColor.BLACK;
            cell.BorderWidthTop = top;
            cell.BorderWidthBottom = bottom;
            cell.BorderWidthRight = right;
            cell.BorderWidthLeft = left;
            cell.PaddingLeft = 4;
            cell.PaddingTop = 4;
            cell.PaddingBottom = 4;
            cell.PaddingRight = 4;

            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;

            table.SpacingAfter = 1;
            table.AddCell(cell);
        }

        private static PdfPCell PhraseCell(Phrase phrase, int align)
        {
            PdfPCell cell = new PdfPCell(phrase);
            cell.BorderColor = BaseColor.WHITE;
            cell.VerticalAlignment = Rectangle.ALIGN_TOP;// PdfCell.ALIGN_TOP;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 1f;
            cell.PaddingTop = 0f;
            return cell;
        }
    }
    public class PurchaseOrderInfo
    {
        public string PackingSlip { get; set; }
        public string DocumentDate { get; set; }
        public int Page { get; set; }
        public string WhoPrinted { get; set; }
        public string DateTimePrinted { get; set; }
        public string CompanyLogo { get; set; }
        // public string BarCodeImage { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string SalesOrder { get; set; }
        public string CustomerId { get; set; }
        public string ShippingMethod { get; set; }
        public string ReqShippingDate { get; set; }
        public string MasterNumber { get; set; }
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public string CustomerName { get; set; }
        public string ShippingAddressLine1 { get; set; }
        public string ShippingAddressLine2 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZipCode { get; set; }
        public string ShippingCountry { get; set; }

        public List<ProductModel> ProductsList { get; set; }
    }
    public class ProductModel
    {
        public int UnitsOrdered { get; set; }
        public int UnitsShipped { get; set; }
        public int UnitsBO { get; set; }
        public string ItemNumber { get; set; }
        public string Description { get; set; }
        public int QtyShipped { get; set; }

    }
}
