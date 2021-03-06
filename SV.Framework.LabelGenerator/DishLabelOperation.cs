using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
//using KeepAutomation.Barcode;
//using KeepAutomation.Barcode.Bean;

using TarCode.Barcode.Control;

namespace SV.Framework.LabelGenerator
{
    public class DishLabelOperation
    {
        Image POCodeimage;
        Image FOCodeimage;
        Image PalletCodeimage;
        Image CartonCodeimage;
        Image SKUCodeimage;
        Image PostalCodeimage;
        Image SSCCImage;
        Image UPCImage;
        Image CartonQtyImage;
        Image IMEIImage;
        Image MEIDImage;
        Image HEXImage;
        Image ICCIDImage;
        Image SerialNumImage;
        private const float BARCODE_HEIGHT = 20f;
        private const float SSCC_BARCODE_HEIGHT = 40f;
        private const float BARCODE_WIDTH = 250f;
        private const float PDF_PAGE_WIDTH_PERCENT = 100f;
        private const float TEXT_FONT_SIZE = 6f;
        private const float LABEL_WIDTH_SIZE = 216;
        private const float LABEL_HEIGHT_SIZE = 140;//128


        public MemoryStream ExportPalletToPDF(IList<PalletModel> pallets)
        {
            if (pallets != null && pallets.Count > 0)
                return PalletLabelPdf(pallets);
            else
                return null;
        }
        public MemoryStream ExportPalletToPDFNew(IList<PalletModel> pallets)
        {
            //H3LabelOperation h3LabelOperation = new H3LabelOperation();
            if (pallets != null && pallets.Count > 0)
                return PalletLabelPdfTarCode(pallets);
            else
                return null;
        }
        public MemoryStream ExportMasterCartonToPDF(IList<MasterCartonInfo> cartons)
        {
            if (cartons != null && cartons.Count > 0)
                return MasterCartonLabelPdf(cartons);
            else
                return null;
        }
        public MemoryStream ExportMasterCartonToPDFNew(IList<MasterCartonInfo> cartons)
        {
           // H3LabelOperation h3LabelOperation = new H3LabelOperation();

            if (cartons != null && cartons.Count > 0)
                return MasterCartonLabelPdfTarCode(cartons);
            else
                return null;
        }
        public MemoryStream PalletLabelPdf(IList<PalletModel> pallets)
        {
            var memoryStream = new MemoryStream();
            // ContainerModel ContainerInfo = new ContainerModel();
            Document pdfDoc = new Document(PageSize.A4, 25f, 25f, 20f, 20f);

            PdfPTable table;
            PdfPCell cell = null;

            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);
            //open the stream 
            pdfDoc.Open();
            string PalletID = string.Empty;
            foreach (var palletInfo in pallets)
            {
                pdfDoc.NewPage();

                PdfPTable tablespace = new PdfPTable(1);
                tablespace.TotalWidth = 575f;
                tablespace.LockedWidth = true;
                tablespace.SetWidths(new float[] { 0.2f });
                PalletID = palletInfo.PalletID.Replace("(", "").Replace(")", "");
                if (!string.IsNullOrEmpty(palletInfo.PoNumber))
                {
                    POCodeimage = GenerateBarCode(palletInfo.PoNumber);
                }
                if (!string.IsNullOrEmpty(palletInfo.FO))
                {
                    FOCodeimage = GenerateBarCode(palletInfo.FO);
                }
                if (!string.IsNullOrEmpty(palletInfo.SKU))
                {
                    SKUCodeimage = GenerateSSCCBarCode(palletInfo.SKU);
                }

                if (!string.IsNullOrEmpty(palletInfo.PalletID))
                {
                    PalletCodeimage = GenerateBarCode(PalletID);
                }

                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));


                PdfPCell cell1 = null;
                cell1 = new PdfPCell();


                table = new PdfPTable(2);

                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                float[] widths = new float[] { 250f, 250f };
                table.SetWidths(widths);
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithPaddingLeftSize(table, "Ship To: ", 0.5f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);
                addCellwithPaddingLeft50(table, "Ship From: ", 0.5f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.SetWidths(widths);
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithPaddingLeftSize(table, palletInfo.CustomerName, 0f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);
                addCellwithPaddingLeft50(table, "Lan Global Inc.", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                widths = new float[] { 250f, 250f };
                table.SetWidths(widths);

                if (string.IsNullOrEmpty(palletInfo.ShippingAddressLine2))
                    addCellwithPaddingLeftSize(table, palletInfo.ShippingAddressLine1, 0f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);
                else
                    addCellwithPaddingLeftSize(table, palletInfo.ShippingAddressLine1 + ", " + palletInfo.ShippingAddressLine2, 0f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);

                if (string.IsNullOrEmpty(palletInfo.AddressLine2))
                    addCellwithPaddingLeft50(table, palletInfo.AddressLine1, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);
                else
                    addCellwithPaddingLeft50(table, palletInfo.AddressLine1 + ", " + palletInfo.AddressLine2, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);


                
                pdfDoc.Add(table);




                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                table.SetWidths(widths);

                addCellwithPaddingLeftSize(table, palletInfo.ShippingCity + " " + palletInfo.ShippingState + " " + palletInfo.ShippingZipCode, 0f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);

                addCellwithPaddingLeft50(table, palletInfo.City + " " + palletInfo.State + " " + palletInfo.ZipCode, 0f, 0f, 0.5f, 0f, BaseColor.WHITE, BaseColor.BLACK, 14);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.SetWidths(widths);
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal1(table, palletInfo.ShippingCountry, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal1(table, palletInfo.Country, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

                pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);



                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                addCellwithborderNormalCenterSize(table, "SKU: "+ palletInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 250f });
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell2 = null;
                cell2 = new PdfPCell();

                if (SKUCodeimage != null)
                {

                    cell2 = ImageCellNew11(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT);
                    cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell2);

                }
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                addCellwithborderNormalCenterSize(table, "PO: " + palletInfo.PoNumber, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 24);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 250f });
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell3 = null;
                cell3 = new PdfPCell();

                if (POCodeimage != null)
                {

                    cell3 = ImageCellNew11(POCodeimage, 220f, PdfPCell.ALIGN_LEFT);
                    cell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell3);

                }
                pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                

                if(!string.IsNullOrEmpty(palletInfo.FO))
                {
                    table = new PdfPTable(1);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.HorizontalAlignment = Element.ALIGN_CENTER;

                    addCellwithborderNormalCenterSize(table, "FO: " + palletInfo.FO, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 24);
                    pdfDoc.Add(table);

                    table = new PdfPTable(1);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.SetWidths(new float[] { 250f });
                    table.HorizontalAlignment = Element.ALIGN_CENTER;
                    PdfPCell cell13 = null;
                    cell13 = new PdfPCell();

                    if (FOCodeimage != null)
                    {

                        cell13 = ImageCellNew11(FOCodeimage, 220f, PdfPCell.ALIGN_LEFT);
                        cell13.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        table.AddCell(cell13);

                    }
                    pdfDoc.Add(table);


                    table = new PdfPTable(1);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.HorizontalAlignment = Element.ALIGN_CENTER;
                    addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);
                    table = new PdfPTable(1);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.HorizontalAlignment = Element.ALIGN_CENTER;
                    addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);

                }

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalCenterSizePaddingBottom(table, "Item Count: " + palletInfo.ItemCount, 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14, 4);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalCenterSizePaddingBottom(table, "Total Carton: " + palletInfo.CartonCount, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14, 4);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalCenterSizePaddingBottom(table, "Ship Date: " + palletInfo.ShipDate, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14, 4);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalCenterSizePaddingBottom(table, "Pallet: " + palletInfo.SNo + " of " + palletInfo.TotalPallet, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14,4);
                pdfDoc.Add(table);

                
                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                //table = new PdfPTable(1);
                //table.TotalWidth = 300f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);
                ////table = new PdfPTable(1);
                //table.TotalWidth = 300f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 250f });
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell4 = null;
                cell4 = new PdfPCell();

                if (PalletCodeimage != null)
                {

                    cell4 = ImageCellNew11(PalletCodeimage, 220f, PdfPCell.ALIGN_LEFT);
                    cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell4);

                }
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                string formatedPalletID = palletInfo.PalletID.Insert(4, " ");
                addCellwithborderNormalCenterSizePaddingBottom(table, formatedPalletID, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14, 1);
                pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithPaddingLeft(table, "Country of Origin: " + palletInfo.Comments, 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal1(table, "\n", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
            }

            // Close PDF document and send

            pdfWriter.CloseStream = false;
            pdfDoc.Close();

            memoryStream.Position = 0;
            return memoryStream;
        }

        public MemoryStream PalletLabelPdfNew(IList<PalletModel> pallets)
        {
            var memoryStream = new MemoryStream();
            // ContainerModel ContainerInfo = new ContainerModel();
            Document pdfDoc = new Document(PageSize.A4, 25f, 25f, 20f, 20f);

            PdfPTable table;
            PdfPCell cell = null;

            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);
            //open the stream 
            pdfDoc.Open();
            string PalletID = string.Empty;
            foreach (var palletInfo in pallets)
            {
                pdfDoc.NewPage();

                PdfPTable tablespace = new PdfPTable(1);
                tablespace.TotalWidth = 575f;
                tablespace.LockedWidth = true;
                tablespace.SetWidths(new float[] { 0.2f });
                PalletID = palletInfo.PalletID.Replace("(", "").Replace(")", "");
                if (!string.IsNullOrEmpty(palletInfo.PoNumber))
                {
                    POCodeimage = GenerateBarCode(palletInfo.PoNumber);
                }
                if (!string.IsNullOrEmpty(palletInfo.FO))
                {
                    FOCodeimage = GenerateBarCode(palletInfo.FO);
                }
                if (!string.IsNullOrEmpty(palletInfo.SKU))
                {
                    SKUCodeimage = GenerateSSCCBarCode(palletInfo.SKU);
                }

                if (!string.IsNullOrEmpty(palletInfo.PalletID))
                {
                    PalletCodeimage = GenerateBarCode(PalletID);
                }

                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));


                PdfPCell cell1 = null;
                cell1 = new PdfPCell();


                table = new PdfPTable(2);

                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                float[] widths = new float[] { 250f, 250f };
                table.SetWidths(widths);
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithPaddingLeftSize(table, "Ship To: ", 0.5f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);
                addCellwithPaddingLeft50(table, "Ship From: ", 0.5f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.SetWidths(widths);
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithPaddingLeftSize(table, palletInfo.CustomerName, 0f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);
                addCellwithPaddingLeft50(table, "Lan Global Inc.", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                widths = new float[] { 250f, 250f };
                table.SetWidths(widths);

                if (string.IsNullOrEmpty(palletInfo.ShippingAddressLine2))
                    addCellwithPaddingLeftSize(table, palletInfo.ShippingAddressLine1, 0f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);
                else
                    addCellwithPaddingLeftSize(table, palletInfo.ShippingAddressLine1 + ", " + palletInfo.ShippingAddressLine2, 0f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);

                if (string.IsNullOrEmpty(palletInfo.AddressLine2))
                    addCellwithPaddingLeft50(table, palletInfo.AddressLine1, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);
                else
                    addCellwithPaddingLeft50(table, palletInfo.AddressLine1 + ", " + palletInfo.AddressLine2, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);



                pdfDoc.Add(table);




                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                table.SetWidths(widths);

                addCellwithPaddingLeftSize(table, palletInfo.ShippingCity + " " + palletInfo.ShippingState + " " + palletInfo.ShippingZipCode, 0f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);

                addCellwithPaddingLeft50(table, palletInfo.City + " " + palletInfo.State + " " + palletInfo.ZipCode, 0f, 0f, 0.5f, 0f, BaseColor.WHITE, BaseColor.BLACK, 14);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.SetWidths(widths);
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal1(table, palletInfo.ShippingCountry, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal1(table, palletInfo.Country, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

                pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);



                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                addCellwithborderNormalCenterSize(table, "SKU: " + palletInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 250f });
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell2 = null;
                cell2 = new PdfPCell();

                if (SKUCodeimage != null)
                {

                    cell2 = ImageCellNew11(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT);
                    cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell2);

                }
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                addCellwithborderNormalCenterSize(table, "PO: " + palletInfo.PoNumber, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 24);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 250f });
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell3 = null;
                cell3 = new PdfPCell();

                if (POCodeimage != null)
                {

                    cell3 = ImageCellNew11(POCodeimage, 220f, PdfPCell.ALIGN_LEFT);
                    cell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell3);

                }
                pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);



                if (!string.IsNullOrEmpty(palletInfo.FO))
                {
                    table = new PdfPTable(1);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.HorizontalAlignment = Element.ALIGN_CENTER;

                    addCellwithborderNormalCenterSize(table, "FO: " + palletInfo.FO, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 24);
                    pdfDoc.Add(table);

                    table = new PdfPTable(1);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.SetWidths(new float[] { 250f });
                    table.HorizontalAlignment = Element.ALIGN_CENTER;
                    PdfPCell cell13 = null;
                    cell13 = new PdfPCell();

                    if (FOCodeimage != null)
                    {

                        cell13 = ImageCellNew11(FOCodeimage, 220f, PdfPCell.ALIGN_LEFT);
                        cell13.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        table.AddCell(cell13);

                    }
                    pdfDoc.Add(table);


                    table = new PdfPTable(1);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.HorizontalAlignment = Element.ALIGN_CENTER;
                    addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);
                    table = new PdfPTable(1);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.HorizontalAlignment = Element.ALIGN_CENTER;
                    addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);

                }

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalCenterSizePaddingBottom(table, "Item Count: " + palletInfo.ItemCount, 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14, 4);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalCenterSizePaddingBottom(table, "Total Carton: " + palletInfo.CartonCount, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14, 4);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalCenterSizePaddingBottom(table, "Ship Date: " + palletInfo.ShipDate, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14, 4);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalCenterSizePaddingBottom(table, "Pallet: " + palletInfo.SNo + " of " + palletInfo.TotalPallet, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14, 4);
                pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                //table = new PdfPTable(1);
                //table.TotalWidth = 300f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);
                ////table = new PdfPTable(1);
                //table.TotalWidth = 300f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 250f });
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell4 = null;
                cell4 = new PdfPCell();

                if (PalletCodeimage != null)
                {

                    cell4 = ImageCellNew11(PalletCodeimage, 220f, PdfPCell.ALIGN_LEFT);
                    cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell4);

                }
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                string formatedPalletID = palletInfo.PalletID.Insert(4, " ");
                addCellwithborderNormalCenterSizePaddingBottom(table, formatedPalletID, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 14, 1);
                pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithPaddingLeft(table, "Country of Origin: " + palletInfo.Comments, 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal1(table, "\n", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
            }

            // Close PDF document and send

            pdfWriter.CloseStream = false;
            pdfDoc.Close();

            memoryStream.Position = 0;
            return memoryStream;
        }

        public MemoryStream PalletLabelPdfTarCode(IList<PalletModel> pallets)
        {
            var memoryStream = new MemoryStream();
            float skuWarcodeWidth = 12.9f;
            // ContainerModel ContainerInfo = new ContainerModel();
            Document pdfDoc = new Document(PageSize.A4, 25f, 25f, 20f, 20f);

            PdfPTable table;
            PdfPCell cell = null;

            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);
            //open the stream 
            pdfDoc.Open();
            string PalletID = string.Empty;
            foreach (var palletInfo in pallets)
            {
                pdfDoc.NewPage();

                PdfPTable tablespace = new PdfPTable(1);
                tablespace.TotalWidth = 575f;
                tablespace.LockedWidth = true;
                tablespace.SetWidths(new float[] { 0.2f });
                PalletID = palletInfo.PalletID.Replace("(", "").Replace(")", "");
                if (!string.IsNullOrEmpty(palletInfo.PoNumber))
                {
                    POCodeimage = GenerateBarCodeTarCode(palletInfo.PoNumber, 12f, 1.359f, 4f, 3f);
                }
                if (!string.IsNullOrEmpty(palletInfo.FO))
                {
                    FOCodeimage = GenerateBarCodeTarCode(palletInfo.FO, 13f, 1.359f, 4f, 3f);
                }
                if (!string.IsNullOrEmpty(palletInfo.SKU))
                {
                    if (palletInfo.SKU.Length == 10)
                        skuWarcodeWidth = 12.9f;
                    else if (palletInfo.SKU.Length == 11)
                        skuWarcodeWidth = 13f;
                    else if (palletInfo.SKU.Length == 12)
                        skuWarcodeWidth = 13f;
                    else if (palletInfo.SKU.Length == 13)
                        skuWarcodeWidth = 13.1f;
                    else if (palletInfo.SKU.Length == 14)
                        skuWarcodeWidth = 13.2f;
                    else if (palletInfo.SKU.Length == 15)
                        skuWarcodeWidth = 13.3f;
                    else if (palletInfo.SKU.Length > 15)
                        skuWarcodeWidth = 14.1f;
                    SKUCodeimage = GenerateBarCodeTarCode(palletInfo.SKU, skuWarcodeWidth, 1.359f, 4f, 3f);
                }

                if (!string.IsNullOrEmpty(palletInfo.PalletID))
                {
                    PalletCodeimage = GenerateBarCodeTarCode(PalletID, 17f, 1.359f, 4f, 3f);
                }

                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));


                PdfPCell cell1 = null;
                cell1 = new PdfPCell();


                //table = new PdfPTable(2);
                table = new PdfPTable(3);

                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                //float[] widths = new float[] { 250f, 250f };
                float[] widths = new float[] { 200f, 120f, 200f };
                table.SetWidths(widths);
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                addCellwithPaddingLeftSize(table, "Ship To: ", 0.5f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 16);
                addCellwithPaddingLeftSize(table, " ", 0.5f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 16);
                addCellwithPaddingLeft50(table, "Ship From: ", 0.5f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 16, 10f);
                //pdfDoc.Add(table);

                //table = new PdfPTable(2);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 250f, 250f };
                //table.SetWidths(widths);

                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithPaddingLeftSize(table, palletInfo.CustomerName, 0f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 16);
                addCellwithPaddingLeftSizeRowSpan(table, "SO", 0f, 0f, 15f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 60, 3);
                addCellwithPaddingLeft50(table, "Lan Global Inc.", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 16, 10f);
                //pdfDoc.Add(table);

                //table = new PdfPTable(2);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //widths = new float[] { 250f, 250f };
                //table.SetWidths(widths);

                if (string.IsNullOrEmpty(palletInfo.ShippingAddressLine2))
                    addCellwithPaddingLeftSize(table, palletInfo.ShippingAddressLine1, 0f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 16);
                else
                    addCellwithPaddingLeftSize(table, palletInfo.ShippingAddressLine1 + ", " + palletInfo.ShippingAddressLine2, 0f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 16);

                if (string.IsNullOrEmpty(palletInfo.AddressLine2))
                    addCellwithPaddingLeft50(table, palletInfo.AddressLine1, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK,  16, 10f);
                else
                    addCellwithPaddingLeft50(table, palletInfo.AddressLine1 + ", " + palletInfo.AddressLine2, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 16, 10f);



                //pdfDoc.Add(table);




                //table = new PdfPTable(2);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 250f, 250f };
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //table.SetWidths(widths);

                addCellwithPaddingLeftSize(table, palletInfo.ShippingCity + " " + palletInfo.ShippingState + " " + palletInfo.ShippingZipCode, 0f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 16);

                addCellwithPaddingLeft50(table, palletInfo.City + " " + palletInfo.State + " " + palletInfo.ZipCode, 0f, 0f, 0.5f, 0f, BaseColor.WHITE, BaseColor.BLACK, 16, 10f);
                //pdfDoc.Add(table);

                //table = new PdfPTable(2);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 250f, 250f };
                //table.SetWidths(widths);
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal1(table, palletInfo.ShippingCountry, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal1(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal1(table, palletInfo.Country, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

                pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);



                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                addCellwithborderNormalCenterSize(table, "SKU: " + palletInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 28);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 575f });
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell2 = null;
                cell2 = new PdfPCell();

                if (SKUCodeimage != null)
                {

                    cell2 = ImageCellNew11(SKUCodeimage, 320f, PdfPCell.ALIGN_LEFT);
                    cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell2);

                }
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                addCellwithborderNormalCenterSize(table, "PO: " + palletInfo.PoNumber, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 36);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 575f });
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell3 = null;
                cell3 = new PdfPCell();

                if (POCodeimage != null)
                {

                    cell3 = ImageCellNew11(POCodeimage, 320f, PdfPCell.ALIGN_LEFT);
                    cell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell3);

                }
                pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);



                if (!string.IsNullOrEmpty(palletInfo.FO))
                {
                    table = new PdfPTable(1);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.HorizontalAlignment = Element.ALIGN_CENTER;

                    addCellwithborderNormalCenterSize(table, "FO: " + palletInfo.FO, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 36);
                    pdfDoc.Add(table);

                    table = new PdfPTable(1);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.SetWidths(new float[] { 575f });
                    table.HorizontalAlignment = Element.ALIGN_CENTER;
                    PdfPCell cell13 = null;
                    cell13 = new PdfPCell();

                    if (FOCodeimage != null)
                    {

                        cell13 = ImageCellNew11(FOCodeimage, 320f, PdfPCell.ALIGN_LEFT);
                        cell13.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        table.AddCell(cell13);

                    }
                    pdfDoc.Add(table);


                    table = new PdfPTable(1);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.HorizontalAlignment = Element.ALIGN_CENTER;
                    addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);
                    table = new PdfPTable(1);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.HorizontalAlignment = Element.ALIGN_CENTER;
                    addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);

                }

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalPaddingLeftSizePaddingBottom(table, "Item Count: " + palletInfo.ItemCount, 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 18, 4, 200f);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalPaddingLeftSizePaddingBottom(table, "Total Carton: " + palletInfo.CartonCount, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 18, 4, 200f);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalPaddingLeftSizePaddingBottom(table, "Ship Date: " + palletInfo.ShipDate, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 18, 4, 200f);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalPaddingLeftSizePaddingBottom(table, "Pallet: " + palletInfo.SNo + " of " + palletInfo.TotalPallet, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 18, 4, 200f);
                pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);

                //table = new PdfPTable(1);
                //table.TotalWidth = 300f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);
                ////table = new PdfPTable(1);
                //table.TotalWidth = 300f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 575f });
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell4 = null;
                cell4 = new PdfPCell();

                if (PalletCodeimage != null)
                {

                    cell4 = ImageCellNew11(PalletCodeimage, 350f, PdfPCell.ALIGN_LEFT);
                    cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell4);

                }
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                string formatedPalletID = palletInfo.PalletID.Insert(4, " ");
                addCellwithborderNormalCenterSizePaddingBottom(table, formatedPalletID, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 24, 1);
                pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithPaddingLeft(table, "Country of Origin: " + palletInfo.Comments, 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal1(table, "\n", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
               // pdfDoc.Add(table);
            }

            // Close PDF document and send

            pdfWriter.CloseStream = false;
            pdfDoc.Close();

            memoryStream.Position = 0;
            return memoryStream;
        }

        public MemoryStream MasterCartonLabelPdf(IList<MasterCartonInfo> cartons)
        {
            var memoryStream = new MemoryStream();
            // ContainerModel ContainerInfo = new ContainerModel();

            Document pdfDoc = new Document(PageSize.A4, 25f, 25f, 20f, 20f);

            PdfPTable table;
            PdfPCell cell = null;

            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);



            //open the stream 
            pdfDoc.Open();
            string ContainerID = string.Empty;
            foreach (var cartonInfo in cartons)
            {
                pdfDoc.NewPage();

                PdfPTable tablespace = new PdfPTable(1);
                tablespace.TotalWidth = 575f;
                tablespace.LockedWidth = true;
                tablespace.SetWidths(new float[] { 0.2f });

                if (!string.IsNullOrEmpty(cartonInfo.UPC))
                {
                    UPCImage = GenerateBarCodeUPCA(cartonInfo.UPC, 350f, 48f);
                }
                if (!string.IsNullOrEmpty(cartonInfo.SKU))
                {
                    SKUCodeimage = GenerateSSCCBarCode(cartonInfo.SKU);
                }

                if (!string.IsNullOrEmpty(cartonInfo.CartonQty))
                {
                    CartonQtyImage = GenerateBarCode(cartonInfo.CartonQty);
                }
                ContainerID = cartonInfo.ContainerID.Replace("(", "").Replace(")", "");

                if (!string.IsNullOrEmpty(ContainerID))
                {
                    CartonCodeimage = GenerateMasterBarCode(ContainerID, 20, 350);
                }



                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));


                PdfPCell cell1 = null;
                cell1 = new PdfPCell();


                //table = new PdfPTable(2);

                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //float[] widths = new float[] { 250f, 250f };
                //table.SetWidths(widths);
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //addCellwithPaddingLeft(table, "Ship To: ", 0.5f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //addCellwithPaddingLeft50(table, "Ship From: ", 0.5f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);

                //table = new PdfPTable(2);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 250f, 250f };
                //table.SetWidths(widths);
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //addCellwithPaddingLeft(table, palletInfo.CustomerName, 0f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //addCellwithPaddingLeft50(table, "Lan Global Inc.", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);

                //table = new PdfPTable(2);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //widths = new float[] { 250f, 250f };
                //table.SetWidths(widths);

                //if (string.IsNullOrEmpty(palletInfo.ShippingAddressLine2))
                //    addCellwithPaddingLeft(table, palletInfo.ShippingAddressLine1, 0f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //else
                //    addCellwithPaddingLeft(table, palletInfo.ShippingAddressLine1 + ", " + palletInfo.ShippingAddressLine2, 0f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

                //if (string.IsNullOrEmpty(palletInfo.AddressLine2))
                //    addCellwithPaddingLeft50(table, palletInfo.AddressLine1, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //else
                //    addCellwithPaddingLeft50(table, palletInfo.AddressLine1 + ", " + palletInfo.AddressLine2, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);



                //pdfDoc.Add(table);




                //table = new PdfPTable(2);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 250f, 250f };
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //table.SetWidths(widths);

                //addCellwithPaddingLeft(table, palletInfo.ShippingCity + " " + palletInfo.ShippingState + " " + palletInfo.ShippingZipCode, 0f, 0f, 25f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

                //addCellwithPaddingLeft50(table, palletInfo.City + " " + palletInfo.State + " " + palletInfo.ZipCode, 0f, 0f, 0.5f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);

                //table = new PdfPTable(2);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 250f, 250f };
                //table.SetWidths(widths);
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //addCellwithborderNormal1(table, palletInfo.ShippingCountry, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //addCellwithborderNormal1(table, palletInfo.Country, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

                //pdfDoc.Add(table);


                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);



                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                addCellwithborderNormalCenter(table, cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 250f });
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell2 = null;
                cell2 = new PdfPCell();

                if (SKUCodeimage != null)
                {

                    cell2 = ImageCellNew11(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT);
                    cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell2);

                }
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                addCellwithborderNormalCenterSizePaddingBottom(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 16, 10f);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderBold(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1f, 1f, 7, 7f);
                pdfDoc.Add(table);


                //
                float[] widths = new float[] { 350f, 150f };


                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 350f, 150f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell11 = null;
                cell11 = new PdfPCell();

                if (UPCImage != null)
                {
                    cell11 = ImageCellNew22(UPCImage, 350f, PdfPCell.ALIGN_LEFT, 25f, 42f);
                    //cell11 = ImageCellNewLeft(UPCImage, 220f, PdfPCell.ALIGN_LEFT);
                    cell11.BorderWidthRight = 0.5f;
                    cell11.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell11);

                }

                addCellwithborderPaddingSize(table, "Carton Qty: " + cartonInfo.CartonQty, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 40f, 18f, 0, 12);
                pdfDoc.Add(table);



                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 350f, 150f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                addCellwithborderPaddingSize(table, "SW Version: " + cartonInfo.SWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 20f, 13f, 0, 12);

                PdfPCell cell12 = null;
                cell12 = new PdfPCell();

                if (CartonQtyImage != null)
                {

                    cell12 = ImageCellNewLeft(CartonQtyImage, 420f, PdfPCell.ALIGN_LEFT, 28f, 48f, 0);
                    cell12.BorderWidthRight = 0.5f;
                    cell12.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell12);

                }

                pdfDoc.Add(table);

                //table = new PdfPTable(2);
                //table.TotalWidth = 575;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 350f, 150f };
                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(widths);
                //addCellwithPaddingLeft(table, "SW Version: "+cartonInfo.SWVersion, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //addCellwithborderNew(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 350f, 150f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                addCellwithborderPaddingSize(table, "HW Version: " + cartonInfo.HWVersion, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 20f, 5, 5, 12);
                addCellwithborderPaddingSize(table, "Date: " + cartonInfo.ShipDate, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 30f, 5, 5, 12);

                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderBold(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1f, 1f, 12, 18f);
                pdfDoc.Add(table);


                PdfPCell cell3 = null;
                PdfPCell cell13 = null;

                foreach (var item in cartonInfo.CartonItems)
                {
                    if (!string.IsNullOrEmpty(item.HEX))
                    {
                        IMEIImage = GenerateBarCode(item.HEX);
                    }
                    if (!string.IsNullOrEmpty(item.MEID))
                    {
                        MEIDImage = GenerateBarCode(item.MEID);
                    }

                    table = new PdfPTable(2);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 250f, 250f };
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    addCellwithPaddingLeftPaddingBottomSize(table, "DEC: " + item.MEID, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 30f, 12, 24f);
                    addCellwithPaddingLeftPaddingBottomSize(table, "HEX: " + item.HEX, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 95f, 12, 24f);

                    pdfDoc.Add(table);


                    table = new PdfPTable(2);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 250f, 250f };
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    //PdfPCell cell11 = null;
                    cell13 = new PdfPCell();

                    if (MEIDImage != null)
                    {

                        cell13 = ImageCellNewLeft(MEIDImage, 220f, PdfPCell.ALIGN_LEFT, 28f, 25f, 3f);
                        cell13.BorderWidthRight = 0.5f;
                        cell13.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell13);

                    }
                    cell3 = new PdfPCell();

                    if (IMEIImage != null)
                    {

                        cell3 = ImageCellNewLeft(IMEIImage, 220f, PdfPCell.ALIGN_LEFT, 28f, 90f, 3f);
                        cell3.BorderWidthRight = 0.5f;
                        cell3.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell3);

                    }

                    pdfDoc.Add(table);

                }




                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);



                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 250f });
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell4 = null;
                cell4 = new PdfPCell();

                if (CartonCodeimage != null)
                {

                    cell4 = ImageCellNew11(CartonCodeimage, 220f, PdfPCell.ALIGN_LEFT);
                    cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell4);

                }
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                string labelContainerID = cartonInfo.ContainerID.Insert(4, " ");

                addCellwithborderNormalCenterSize(table, labelContainerID, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 16);
                pdfDoc.Add(table);


                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //addCellwithborderNormal(table, "\n", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithPaddingLeft(table, "Country of Origin: " + cartonInfo.Comments, 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal1(table, "\n", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
            }

            // Close PDF document and send

            pdfWriter.CloseStream = false;
            pdfDoc.Close();

            memoryStream.Position = 0;
            return memoryStream;
        }

        public MemoryStream MasterCartonLabelPdfNew(IList<MasterCartonInfo> cartons)
        {
            var memoryStream = new MemoryStream();
            // ContainerModel ContainerInfo = new ContainerModel();

            Document pdfDoc = new Document(PageSize.A4, 25f, 25f, 20f, 20f);

            PdfPTable table;
            PdfPCell cell = null;

            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);

            //open the stream 
            pdfDoc.Open();
            string ContainerID = string.Empty;
            foreach (var cartonInfo in cartons)
            {
                pdfDoc.NewPage();

                PdfPTable tablespace = new PdfPTable(1);
                tablespace.TotalWidth = 575f;
                tablespace.LockedWidth = true;
                tablespace.SetWidths(new float[] { 0.2f });

                if (!string.IsNullOrEmpty(cartonInfo.UPC))
                {
                    UPCImage = GenerateBarCodeUPCATarCode(cartonInfo.UPC, 350f, 48f);
                }
                if (!string.IsNullOrEmpty(cartonInfo.SKU))
                {
                    SKUCodeimage = GenerateSSCCBarCode(cartonInfo.SKU);
                }

                if (!string.IsNullOrEmpty(cartonInfo.CartonQty))
                {
                    CartonQtyImage = GenerateBarCode(cartonInfo.CartonQty);
                }
                ContainerID = cartonInfo.ContainerID.Replace("(", "").Replace(")", "");

                if (!string.IsNullOrEmpty(ContainerID))
                {
                    CartonCodeimage = GenerateMasterBarCode(ContainerID, 20, 350);
                }

                

                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));


                PdfPCell cell1 = null;
                cell1 = new PdfPCell();






                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);

                float[] widths = new float[] { 350f, 150f };


                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;

                //addCellwithborderNormalCenter(table,  cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);
                //
                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                //PdfPCell cell011 = null;
                //cell11 = new PdfPCell();


                addCellwithborderPaddingSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 40f, 18f, 0, 12);

                addCellwithborderPaddingSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 40f, 18f, 0, 12);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 250f, 250f });
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell2 = null;
                cell2 = new PdfPCell();
                addCellwithborderPaddingSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 40f, 18f, 2, 16);

                if (SKUCodeimage != null)
                {

                    cell2 = ImageCellNew110(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f);
                    cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell2);

                }
                pdfDoc.Add(table);

                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;

                //addCellwithborderNormalCenterSizePaddingBottom(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 16, 10f);
                //pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderBold(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1f, 3f, 7, 7f);
                pdfDoc.Add(table);


                //
                
                
                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 350f, 150f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell11 = null;
                cell11 = new PdfPCell();

                if (UPCImage != null)
                {
                    cell11 = ImageCellNew22(UPCImage, 350f, PdfPCell.ALIGN_LEFT, 20f, 48f);
                    //cell11 = ImageCellNewLeft(UPCImage, 220f, PdfPCell.ALIGN_LEFT);
                    cell11.BorderWidthRight = 0.5f;
                    cell11.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell11);

                }

                addCellwithborderPaddingSize(table, "Carton Qty: "+cartonInfo.CartonQty, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 40f, 18f, 0, 12);
                pdfDoc.Add(table);


                
                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 350f, 150f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                addCellwithborderPaddingSize(table, "SW Version: " + cartonInfo.SWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 20f, 13f, 0, 12);
                
                PdfPCell cell12 = null;
                cell12 = new PdfPCell();

                if (CartonQtyImage != null)
                {

                    cell12 = ImageCellNewLeft(CartonQtyImage, 420f, PdfPCell.ALIGN_LEFT, 28f, 48f, 0);
                    cell12.BorderWidthRight = 0.5f;
                    cell12.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell12);

                }

                pdfDoc.Add(table);

                //table = new PdfPTable(2);
                //table.TotalWidth = 575;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 350f, 150f };
                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(widths);
                //addCellwithPaddingLeft(table, "SW Version: "+cartonInfo.SWVersion, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //addCellwithborderNew(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 350f, 150f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                addCellwithborderPaddingSize(table, "HW Version: " + cartonInfo.HWVersion, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 20f, 5, 5, 12);
                addCellwithborderPaddingSize(table, "Date: " + cartonInfo.ShipDate, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 30f, 5, 5, 12);

                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderBold(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1f, 1f, 12, 18f);
                pdfDoc.Add(table);

                PdfPCell cell3 = null;
                PdfPCell cell13 = null;

                foreach (var item in cartonInfo.CartonItems)
                {
                    if (!string.IsNullOrEmpty(item.HEX))
                    {
                        IMEIImage = GenerateBarCode(item.HEX);
                    }
                    if (!string.IsNullOrEmpty(item.MEID))
                    {
                        MEIDImage = GenerateBarCode(item.MEID);
                    }

                    table = new PdfPTable(2);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 250f, 250f };
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    addCellwithPaddingLeftPaddingBottomSize(table, "DEC: " + item.MEID, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK,0f, 25f, 12, 26f);
                    addCellwithPaddingLeftPaddingBottomSize(table, "HEX: " + item.HEX, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 0f, 90f, 12, 26f);

                    pdfDoc.Add(table);


                    table = new PdfPTable(2);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 250f, 250f };
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    //PdfPCell cell11 = null;
                    cell13 = new PdfPCell();

                    if (MEIDImage != null)
                    {

                        cell13 = ImageCellNewLeft(MEIDImage, 220f, PdfPCell.ALIGN_LEFT, 28f, 25f, 1f);
                        cell13.BorderWidthRight = 0.5f;
                        cell13.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell13);

                    }
                    cell3 = new PdfPCell();

                    if (IMEIImage != null)
                    {

                        cell3 = ImageCellNewLeft(IMEIImage, 220f, PdfPCell.ALIGN_LEFT, 28f, 90f, 1f);
                        cell3.BorderWidthRight = 0.5f;
                        cell3.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell3);

                    }

                    pdfDoc.Add(table);

                }




                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);



                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 250f });
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell4 = null;
                cell4 = new PdfPCell();

                if (CartonCodeimage != null)
                {

                    cell4 = ImageCellNew11(CartonCodeimage, 220f, PdfPCell.ALIGN_LEFT);
                    cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell4);

                }
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                string labelContainerID =  cartonInfo.ContainerID.Insert(4," ");

                addCellwithborderNormalCenterSize(table, labelContainerID, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 16);
                pdfDoc.Add(table);


                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //addCellwithborderNormal(table, "\n", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithPaddingLeft(table, "Country of Origin: " + cartonInfo.Comments, 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal1(table, "\n", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
            }

            // Close PDF document and send

            pdfWriter.CloseStream = false;
            pdfDoc.Close();

            memoryStream.Position = 0;
            return memoryStream;
        }
        public MemoryStream MasterCartonLabelPdfTarCode(IList<MasterCartonInfo> cartons)
        {
            var memoryStream = new MemoryStream();
            // ContainerModel ContainerInfo = new ContainerModel();
            float skuWarcodeWidth = 8.2f;
            Document pdfDoc = new Document(PageSize.A4, 25f, 25f, 20f, 20f);

            PdfPTable table;
            PdfPCell cell = null;

            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);

            //open the stream 
            pdfDoc.Open();
            string ContainerID = string.Empty;
            foreach (var cartonInfo in cartons)
            {
                pdfDoc.NewPage();

                PdfPTable tablespace = new PdfPTable(1);
                tablespace.TotalWidth = 575f;
                tablespace.LockedWidth = true;
                tablespace.SetWidths(new float[] { 0.2f });

                if (!string.IsNullOrEmpty(cartonInfo.UPC))
                {
                    UPCImage = GenerateBarCodeMasterUPCATarCode(cartonInfo.UPC, 220f, 52f);
                }
                if (!string.IsNullOrEmpty(cartonInfo.SKU))
                {
                    if (cartonInfo.SKU.Length == 10)
                        skuWarcodeWidth = 8.5f;
                    else if (cartonInfo.SKU.Length == 11)
                        skuWarcodeWidth = 8.5f;
                    else if (cartonInfo.SKU.Length == 12)
                        skuWarcodeWidth = 8.5f;
                    else if (cartonInfo.SKU.Length == 13)
                        skuWarcodeWidth = 8.6f;
                    else if (cartonInfo.SKU.Length == 14)
                        skuWarcodeWidth = 8.7f;
                    else if (cartonInfo.SKU.Length == 15)
                        skuWarcodeWidth = 8.8f;
                    else if (cartonInfo.SKU.Length > 15)
                        skuWarcodeWidth = 9.3f;
                    SKUCodeimage = GenerateBarCodeTarCode(cartonInfo.SKU, skuWarcodeWidth, 1.359f, 4f, 3f);
                    //SKUCodeimage = GenerateSSCCBarCode(cartonInfo.SKU);
                }

                if (!string.IsNullOrEmpty(cartonInfo.CartonQty))
                {
                    //CartonQtyImage = GenerateBarCode(cartonInfo.CartonQty);
                    CartonQtyImage = GenerateBarCodeTarCode(cartonInfo.CartonQty, 6f, 1.35f, 2f, 3f);

                }
                ContainerID = cartonInfo.ContainerID.Replace("(", "").Replace(")", "");

                if (!string.IsNullOrEmpty(ContainerID))
                {
                    CartonCodeimage = GenerateBarCodeTarCodeCARTON(ContainerID, 17f, 1.359f, 4f, 4f);

                  //  CartonCodeimage = GenerateMasterBarCode(ContainerID, 20, 350);
                }



                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 4, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));


                PdfPCell cell1 = null;
                cell1 = new PdfPCell();
                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);

                float[] widths = new float[] { 350f, 150f };


                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;

                //addCellwithborderNormalCenter(table,  cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);
                //
                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                table.SetWidths(widths);
                //PdfPCell cell011 = null;
                //cell11 = new PdfPCell();


                addCellwithborderPaddingSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 40f, 18f, 0, 16);

                addCellwithborderPaddingSize(table,  cartonInfo.SKU, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 60f, 18f, 2f, 20);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 260f, 260f });
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell2 = null;
                cell2 = new PdfPCell();
                addCellwithborderPaddingSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 5f, 18f, 2, 20);

                if (SKUCodeimage != null)
                {

                    cell2 = ImageCellNew110(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f);
                    cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell2);

                }
                pdfDoc.Add(table);

                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;

                //addCellwithborderNormalCenterSizePaddingBottom(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 16, 10f);
                //pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderBold(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1f, 3f, 7, 7f);
                pdfDoc.Add(table);


                //


                //table = new PdfPTable(2);
                table = new PdfPTable(3);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 350f, 150f };
                widths = new float[] { 250f, 115f, 135f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell11 = null;
                cell11 = new PdfPCell();

                if (UPCImage != null)
                {
                    cell11 = ImageCellNew22(UPCImage, 220f, PdfPCell.ALIGN_LEFT, 0f, 52f);
                    //cell11 = ImageCellNewLeft(UPCImage, 220f, PdfPCell.ALIGN_LEFT);
                    cell11.BorderWidthRight = 0.5f;
                    cell11.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell11);

                }
              //  if (cartonInfo.ProductType.ToLower() == "h3")
                //    addCellwithPaddingLeftSizeRowSpan(table, "SN", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 60, 3);
                //else
                    addCellwithPaddingLeftSizeRowSpan(table, "SO", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 60, 3);

                addCellwithborderPaddingSize(table, "Carton Qty: " + cartonInfo.CartonQty, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 15f, 18f, 0, 14);
                //pdfDoc.Add(table);



                //table = new PdfPTable(2);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 350f, 150f };
                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(widths);

                addCellwithborderPaddingSize(table, "SW Version: " + cartonInfo.SWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 0f, 13f, 0, 14);

                PdfPCell cell12 = null;
                cell12 = new PdfPCell();

                if (CartonQtyImage != null)
                {

                    cell12 = ImageCellNewLeft(CartonQtyImage, 220f, PdfPCell.ALIGN_LEFT, 28f, 0f, 0);
                    cell12.BorderWidthRight = 0.5f;
                    cell12.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell12);

                }

                //pdfDoc.Add(table);

                

                //table = new PdfPTable(2);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 350f, 150f };
                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(widths);
                addCellwithborderPaddingSize(table, "HW Version: " + cartonInfo.HWVersion, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 0f, 5, 5, 14);
                addCellwithborderPaddingSize(table, "Date: " + cartonInfo.ShipDate, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 5f, 5, 5, 14);

                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderBold(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1f, 1f, 12, 18f);
                pdfDoc.Add(table);

                PdfPCell cell3 = null;
                PdfPCell cell13 = null;

                foreach (var item in cartonInfo.CartonItems)
                {
                    
                    if (cartonInfo.ProductType.ToLower() == "h3")
                    {
                        if (!string.IsNullOrEmpty(item.IMEI))
                        {
                            IMEIImage = GenerateBarCodeTarCode(item.IMEI, 10f, 1.359f, 4f, 3f);

                            //IMEIImage = GenerateBarCode(item.HEX);
                        }

                        table = new PdfPTable(3);
                        table.TotalWidth = 575f;
                        table.LockedWidth = true;
                        table.DefaultCell.Border = Rectangle.NO_BORDER;
                        widths = new float[] { 120f, 260f, 120f };
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.SetWidths(widths);
                        addCellwithPaddingLeftPaddingBottomSize(table, "" , 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 2f, 25f, 14, 26f);

                        addCellwithPaddingLeftPaddingBottomSize(table, "IMEI: " + item.IMEI, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 2f, 75f, 14, 26f);
                        addCellwithPaddingLeftPaddingBottomSize(table, "" , 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 2f, 25f, 14, 26f);

                        pdfDoc.Add(table);

                        table = new PdfPTable(3);
                        table.TotalWidth = 575f;
                        table.LockedWidth = true;
                        table.DefaultCell.Border = Rectangle.NO_BORDER;
                        widths = new float[] { 100f, 260f, 100f };
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.SetWidths(widths);
                        //PdfPCell cell11 = null;
                        addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 2f, 25f, 14, 26f);

                        //cell13 = new PdfPCell();

                        //if (MEIDImage != null)
                        //{
                        //    cell13 = ImageCellNewLeft(MEIDImage, 220f, PdfPCell.ALIGN_LEFT, 28f, 10f, 2f);
                        //    cell13.BorderWidthRight = 0.5f;
                        //    cell13.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        //    table.AddCell(cell13);

                        //}
                        cell3 = new PdfPCell();

                        if (IMEIImage != null)
                        {

                            cell3 = ImageCellNewLeft(IMEIImage, 220f, PdfPCell.ALIGN_LEFT, 28f, 75f, 2f);
                            cell3.BorderWidthRight = 0.5f;
                            cell3.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            table.AddCell(cell3);

                        }
                        addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 2f, 25f, 14, 26f);

                        pdfDoc.Add(table);

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item.HEX))
                        {
                            IMEIImage = GenerateBarCodeTarCode(item.HEX, 10f, 1.359f, 4f, 3f);

                            //IMEIImage = GenerateBarCode(item.HEX);
                        }
                        if (!string.IsNullOrEmpty(item.MEID))
                        {
                            MEIDImage = GenerateBarCodeTarCode(item.MEID, 12f, 1.359f, 4f, 3f);

                            // MEIDImage = GenerateBarCode(item.MEID);
                        }
                        table = new PdfPTable(2);
                        table.TotalWidth = 575f;
                        table.LockedWidth = true;
                        table.DefaultCell.Border = Rectangle.NO_BORDER;
                        widths = new float[] { 250f, 250f };
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.SetWidths(widths);
                        addCellwithPaddingLeftPaddingBottomSize(table, "DEC: " + item.MEID, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 2f, 25f, 14, 26f);
                        addCellwithPaddingLeftPaddingBottomSize(table, "HEX: " + item.HEX, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 2f, 90f, 14, 26f);

                        pdfDoc.Add(table);

                        table = new PdfPTable(2);
                        table.TotalWidth = 575f;
                        table.LockedWidth = true;
                        table.DefaultCell.Border = Rectangle.NO_BORDER;
                        widths = new float[] { 260f, 260f };
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.SetWidths(widths);
                        //PdfPCell cell11 = null;
                        cell13 = new PdfPCell();

                        if (MEIDImage != null)
                        {
                            cell13 = ImageCellNewLeft(MEIDImage, 220f, PdfPCell.ALIGN_LEFT, 28f, 10f, 2f);
                            cell13.BorderWidthRight = 0.5f;
                            cell13.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            table.AddCell(cell13);

                        }
                        cell3 = new PdfPCell();

                        if (IMEIImage != null)
                        {

                            cell3 = ImageCellNewLeft(IMEIImage, 220f, PdfPCell.ALIGN_LEFT, 28f, 75f, 2f);
                            cell3.BorderWidthRight = 0.5f;
                            cell3.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            table.AddCell(cell3);

                        }

                        pdfDoc.Add(table);
                    }



                }




                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);



                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 550f });
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell4 = null;
                cell4 = new PdfPCell();

                if (CartonCodeimage != null)
                {

                    cell4 = ImageCellNew11(CartonCodeimage, 220f, PdfPCell.ALIGN_LEFT);
                    cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell4);

                }
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                string labelContainerID = cartonInfo.ContainerID.Insert(4, " ");

                addCellwithborderNormalCenterSize(table, labelContainerID, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 20);
                pdfDoc.Add(table);


                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //addCellwithborderNormal(table, "\n", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithPaddingLeft(table, "Country of Origin: " + cartonInfo.Comments, 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal1(table, "\n", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
               // pdfDoc.Add(table);
            }

            // Close PDF document and send

            pdfWriter.CloseStream = false;
            pdfDoc.Close();

            memoryStream.Position = 0;
            return memoryStream;
        }

        public MemoryStream MasterCartonLabelPdfTarCodeNew(IList<MasterCartonInfo> cartons)
        {
            var memoryStream = new MemoryStream();
            // ContainerModel ContainerInfo = new ContainerModel();

            Document pdfDoc = new Document(PageSize.A4, 25f, 25f, 20f, 20f);

            PdfPTable table;
            PdfPCell cell = null;

            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);

            //open the stream 
            pdfDoc.Open();
            string ContainerID = string.Empty;
            foreach (var cartonInfo in cartons)
            {
                pdfDoc.NewPage();

                PdfPTable tablespace = new PdfPTable(1);
                tablespace.TotalWidth = 575f;
                tablespace.LockedWidth = true;
                tablespace.SetWidths(new float[] { 0.2f });

                if (!string.IsNullOrEmpty(cartonInfo.UPC))
                {
                    UPCImage = GenerateBarCodeMasterUPCATarCode(cartonInfo.UPC, 220f, 52f);
                }
                if (!string.IsNullOrEmpty(cartonInfo.SKU))
                {
                    SKUCodeimage = GenerateBarCodeTarCode(cartonInfo.SKU, 8.5f, 1.359f, 4f, 3f);
                    //SKUCodeimage = GenerateSSCCBarCode(cartonInfo.SKU);
                }

                if (!string.IsNullOrEmpty(cartonInfo.CartonQty))
                {
                    //CartonQtyImage = GenerateBarCode(cartonInfo.CartonQty);
                    CartonQtyImage = GenerateBarCodeTarCode(cartonInfo.CartonQty, 6f, 1.35f, 2f, 3f);

                }
                ContainerID = cartonInfo.ContainerID.Replace("(", "").Replace(")", "");

                if (!string.IsNullOrEmpty(ContainerID))
                {
                    CartonCodeimage = GenerateBarCodeTarCodeCARTON(ContainerID, 17f, 1.359f, 4f, 4f);

                    //  CartonCodeimage = GenerateMasterBarCode(ContainerID, 20, 350);
                }



                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 4, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));


                PdfPCell cell1 = null;
                cell1 = new PdfPCell();






                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);

                float[] widths = new float[] { 350f, 150f };


                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;

                //addCellwithborderNormalCenter(table,  cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);
                //
                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                table.SetWidths(widths);
                //PdfPCell cell011 = null;
                //cell11 = new PdfPCell();


                addCellwithborderPaddingSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 40f, 18f, 0, 16);

                addCellwithborderPaddingSize(table, cartonInfo.SKU, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 60f, 18f, 2f, 20);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 260f, 260f });
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell2 = null;
                cell2 = new PdfPCell();
                addCellwithborderPaddingSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 5f, 18f, 2, 20);

                if (SKUCodeimage != null)
                {

                    cell2 = ImageCellNew110(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f);
                    cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell2);

                }
                pdfDoc.Add(table);

                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;

                //addCellwithborderNormalCenterSizePaddingBottom(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 16, 10f);
                //pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderBold(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1f, 3f, 7, 7f);
                pdfDoc.Add(table);


                //


                //table = new PdfPTable(2);
                table = new PdfPTable(3);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 350f, 150f };
                widths = new float[] { 250f, 115f, 135f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell11 = null;
                cell11 = new PdfPCell();

                if (UPCImage != null)
                {
                    cell11 = ImageCellNew22(UPCImage, 220f, PdfPCell.ALIGN_LEFT, 0f, 52f);
                    //cell11 = ImageCellNewLeft(UPCImage, 220f, PdfPCell.ALIGN_LEFT);
                    cell11.BorderWidthRight = 0.5f;
                    cell11.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell11);

                }
                addCellwithPaddingLeftSizeRowSpan(table, "SO", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 60, 3);

                addCellwithborderPaddingSize(table, "Carton Qty: " + cartonInfo.CartonQty, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 15f, 18f, 0, 14);
                //pdfDoc.Add(table);



                //table = new PdfPTable(2);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 350f, 150f };
                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(widths);

                addCellwithborderPaddingSize(table, "SW Version: " + cartonInfo.SWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 0f, 13f, 0, 14);

                PdfPCell cell12 = null;
                cell12 = new PdfPCell();

                if (CartonQtyImage != null)
                {

                    cell12 = ImageCellNewLeft(CartonQtyImage, 220f, PdfPCell.ALIGN_LEFT, 28f, 0f, 0);
                    cell12.BorderWidthRight = 0.5f;
                    cell12.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell12);

                }

                //pdfDoc.Add(table);



                //table = new PdfPTable(2);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 350f, 150f };
                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(widths);
                addCellwithborderPaddingSize(table, "HW Version: " + cartonInfo.HWVersion, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 0f, 5, 5, 14);
                addCellwithborderPaddingSize(table, "Date: " + cartonInfo.ShipDate, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 5f, 5, 5, 14);

                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderBold(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1f, 1f, 12, 18f);
                pdfDoc.Add(table);

                PdfPCell cell3 = null;
                PdfPCell cell13 = null;

                foreach (var item in cartonInfo.CartonItems)
                {
                    if (!string.IsNullOrEmpty(item.HEX))
                    {
                        IMEIImage = GenerateBarCodeTarCode(item.HEX, 10f, 1.359f, 4f, 3f);

                        //IMEIImage = GenerateBarCode(item.HEX);
                    }
                    if (!string.IsNullOrEmpty(item.MEID))
                    {
                        MEIDImage = GenerateBarCodeTarCode(item.MEID, 12f, 1.359f, 4f, 3f);

                        // MEIDImage = GenerateBarCode(item.MEID);
                    }

                    table = new PdfPTable(2);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 250f, 250f };
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    addCellwithPaddingLeftPaddingBottomSize(table, "DEC: " + item.MEID, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 2f, 25f, 14, 26f);
                    addCellwithPaddingLeftPaddingBottomSize(table, "HEX: " + item.HEX, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 2f, 90f, 14, 26f);

                    pdfDoc.Add(table);


                    table = new PdfPTable(2);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 260f, 260f };
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    //PdfPCell cell11 = null;
                    cell13 = new PdfPCell();

                    if (MEIDImage != null)
                    {
                        cell13 = ImageCellNewLeft(MEIDImage, 220f, PdfPCell.ALIGN_LEFT, 28f, 10f, 2f);
                        cell13.BorderWidthRight = 0.5f;
                        cell13.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell13);

                    }
                    cell3 = new PdfPCell();

                    if (IMEIImage != null)
                    {

                        cell3 = ImageCellNewLeft(IMEIImage, 220f, PdfPCell.ALIGN_LEFT, 28f, 75f, 2f);
                        cell3.BorderWidthRight = 0.5f;
                        cell3.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell3);

                    }

                    pdfDoc.Add(table);

                }




                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);



                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 550f });
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell4 = null;
                cell4 = new PdfPCell();

                if (CartonCodeimage != null)
                {

                    cell4 = ImageCellNew11(CartonCodeimage, 220f, PdfPCell.ALIGN_LEFT);
                    cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell4);

                }
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                string labelContainerID = cartonInfo.ContainerID.Insert(4, " ");

                addCellwithborderNormalCenterSize(table, labelContainerID, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 20);
                pdfDoc.Add(table);


                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //addCellwithborderNormal(table, "\n", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithPaddingLeft(table, "Country of Origin: " + cartonInfo.Comments, 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal1(table, "\n", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                // pdfDoc.Add(table);
            }

            // Close PDF document and send

            pdfWriter.CloseStream = false;
            pdfDoc.Close();

            memoryStream.Position = 0;
            return memoryStream;
        }

        public MemoryStream POSKITLabelPdf(IList<PosKitInfo> posKitS)
        {
            int bottomMargin = 0;
            int topMargin = 10;
            float LABELHEIGHTSIZE = 280;
            const int pageMargin = 3;
            const int pageRows = 1;
            const int pageCols = 1;

            var memoryStream = new MemoryStream();
            // ContainerModel ContainerInfo = new ContainerModel();

            Document pdfDoc = new Document();
            iTextSharp.text.Rectangle envelope = new iTextSharp.text.Rectangle(500, LABELHEIGHTSIZE);
            pdfDoc.SetPageSize(envelope);
            pdfDoc.SetMargins(pageMargin, pageMargin, 0, 0);

            PdfPTable table;
            PdfPCell cell = null;

            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);


            //open the stream 
            pdfDoc.Open();
            string ContainerID = string.Empty;
            foreach (var cartonInfo in posKitS)
            {
                pdfDoc.NewPage();

                PdfPTable tablespace = new PdfPTable(1);
                tablespace.TotalWidth = 500f;
                tablespace.LockedWidth = true;
                tablespace.SetWidths(new float[] { 0.2f });

                if (!string.IsNullOrEmpty(cartonInfo.UPC))
                {
                    UPCImage = GenerateBarCodeUPCA(cartonInfo.UPC, 350f, 48f);
                }
                if (!string.IsNullOrEmpty(cartonInfo.MEID))
                {
                    MEIDImage = GenerateBarCode(cartonInfo.MEID);
                }
                if (!string.IsNullOrEmpty(cartonInfo.ICCID))
                {
                    ICCIDImage = GenerateBarCode(cartonInfo.ICCID);
                }
                
                //if (!string.IsNullOrEmpty(cartonInfo.SerialNum))
                //{
                //    SerialNumImage = GenerateBarCode(cartonInfo.SerialNum);
                //}
                if (!string.IsNullOrEmpty(cartonInfo.HEX))
                {
                    HEXImage = GenerateBarCode(cartonInfo.HEX);
                }

                if (!string.IsNullOrEmpty(cartonInfo.SKU))
                {
                    SKUCodeimage = GenerateMasterBarCode(cartonInfo.SKU, 30, 220);
                }



                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));


                PdfPCell cell1 = null;
                cell1 = new PdfPCell();




                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 3, 0, 0, 0, 3);
                pdfDoc.Add(table);


                //
                float[] widths = new float[] { 250f, 250f };

                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                addCellwithPaddingLeftPaddingBottomSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 12f);
                addCellwithPaddingLeftPaddingBottomSize(table, cartonInfo.CompanyName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell11 = null;
                cell11 = new PdfPCell();

                if (SKUCodeimage != null)
                {

                    cell11 = ImageCellNew2(SKUCodeimage, 200f, PdfPCell.ALIGN_LEFT, 20f);
                    cell11.BorderWidthRight = 0.5f;
                    cell11.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell11);

                }

                addCellwithPaddingLeft(table,  "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell12 = null;
                cell12 = new PdfPCell();

                if (UPCImage != null)
                {
                    cell12 = ImageCellNew22(UPCImage, 350f, PdfPCell.ALIGN_LEFT, 20f, 42f);
                    cell12.BorderWidthRight = 0.5f;
                    cell12.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell12);
                }

                addCellwithPaddingLeft(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);


                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                //addCellwithPaddingLeft(table, cartonInfo.UPC, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                addCellwithPaddingLeftSize(table, "HEX: " + cartonInfo.HEX, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
               // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);


                //table = new PdfPTable(2);
                //table.TotalWidth = 500f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 250f, 250f };
                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(widths);

                //addCellwithPaddingLeft(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //addCellwithPaddingLeft(table, "MEID: " + cartonInfo.MEID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);


                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                addCellwithPaddingTopPaddingLeft(table, "CONTENTS INCLUDE:", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 0, 10);
               
                PdfPCell cell16 = null;
                cell16 = new PdfPCell();
                if (HEXImage != null)
                {

                    cell16 = ImageCellNewLeft(HEXImage, 220f, PdfPCell.ALIGN_LEFT, 27f, 20f, 3f);
                    cell16.BorderWidthRight = 0.5f;
                    cell16.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell16);

                }
                //if (SerialNumImage != null)
                //{

                //    cell16 = ImageCellNewLeft(SerialNumImage, 220f, PdfPCell.ALIGN_LEFT);
                //    cell16.BorderWidthRight = 0.5f;
                //    cell16.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                //    table.AddCell(cell16);

                //}

                pdfDoc.Add(table);

                //table = new PdfPTable(2);
                //table.TotalWidth = 500f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 250f, 250f };
                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(widths);

                //addCellwithPaddingLeft(table, "CONTENTS INCLUDE:", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //addCellwithPaddingLeft(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);

                //PdfPCell cell13 = null;
                //cell13 = new PdfPCell();

                //if (MEIDImage != null)
                //{
                //    cell13 = ImageCellNewLeft(MEIDImage, 220f, PdfPCell.ALIGN_LEFT);
                //    cell13.BorderWidthRight = 0.5f;
                //    cell13.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                //    table.AddCell(cell13);

                //}

                //pdfDoc.Add(table);
                PdfPCell cell15 = null;
                cell15 = new PdfPCell();

                int I = 0;
                foreach (var item in cartonInfo.KitBoxList)
                {
                    I = I + 1;
                    table = new PdfPTable(2);
                    table.TotalWidth = 500f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 250f, 250f };
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);


                    if (I == 2)
                    {
                        addCellwithPaddingLeftPaddingBottomSize(table, item.DisplayName, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 0, 10f);
                        if (!string.IsNullOrEmpty(cartonInfo.ICCID))
                            addCellwithPaddingLeftPaddingBottomSize(table, "ICCID: " + cartonInfo.ICCID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 0, 10f);
                        else
                            addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 0, 10f);
                    }
                    else
                    if (I == 3)
                    {
                        addCellwithPaddingLeftPaddingBottomSize(table, item.DisplayName, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK,0, 10f);

                        if (ICCIDImage != null)
                        {
                            cell15 = ImageCellNewBottomPaddingLeft(ICCIDImage, 220f, PdfPCell.ALIGN_LEFT, 0f);
                            cell15.BorderWidthRight = 0.5f;
                            cell15.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            table.AddCell(cell15);
                        }
                        else
                           addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 42, 27f);
                    }
                    else
                    {
                        addCellwithPaddingLeftSize(table, item.DisplayName, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                        addCellwithborderNewSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f); 
                    }

                    pdfDoc.Add(table);
                }

                if (ICCIDImage == null)
                {
                    table = new PdfPTable(1);
                    table.TotalWidth = 500f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.HorizontalAlignment = Element.ALIGN_CENTER;
                    addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 32, 0, 0, 0, 22);
                    pdfDoc.Add(table);

                }
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                addCellwithPaddingTopPaddingLeftSize(table, "SW Version: " +cartonInfo.SWVersion, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 0, 10f);
                addCellwithPaddingTopPaddingLeftSize(table, "" , 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 0, 10f);
                
                
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                addCellwithPaddingLeftSize(table, "HW Version: " + cartonInfo.HWVersion, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);

                addCellwithPaddingLeftSize(table, "DEC: "+ cartonInfo.MEID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                addCellwithPaddingLeftSize(table, "DATE: " + cartonInfo.ShipDate, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                PdfPCell cell161 =  new PdfPCell();

                if (MEIDImage != null)
                {
                    cell161 = ImageCellNewBottomPaddingLeft(MEIDImage, 220f, PdfPCell.ALIGN_LEFT, 0f);
                    cell161.BorderWidthRight = 0.5f;
                    cell161.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell161);
                }
                //addCellwithborderNew(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                if (cartonInfo.KitBoxList.Count > 0)
                {
                    table = new PdfPTable(2);
                    table.TotalWidth = 500f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 250f, 250f };
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    addCellwithborderNormalHeightSize(table, "Handset: " + cartonInfo.KitBoxList[0].OriginCountry, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 30, 1, 0, 20, 10);
                    addCellwithborderNew(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);

                    table = new PdfPTable(2);
                    table.TotalWidth = 500f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 250f, 250f };
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    addCellwithPaddingLeftSize(table, "Accessory: " + cartonInfo.KitBoxList[0].OriginCountry, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10);
                    addCellwithborderNew(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);
                }

                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 2, 0, 0, 0, 2);
                pdfDoc.Add(table);

            }

            // Close PDF document and send

            pdfWriter.CloseStream = false;
            pdfDoc.Close();

            memoryStream.Position = 0;
            return memoryStream;
        }
        public MemoryStream POSKITLabelPdfTarCode(IList<PosKitInfo> posKitS)
        {
            int bottomMargin = 0;
            int topMargin = 10;
            float LABELHEIGHTSIZE = 280;
            const int pageMargin = 3;
            const int pageRows = 1;
            const int pageCols = 1;
            float skuWarcodeWidth = 12.7f;
            var memoryStream = new MemoryStream();
            // ContainerModel ContainerInfo = new ContainerModel();

            Document pdfDoc = new Document();
            iTextSharp.text.Rectangle envelope = new iTextSharp.text.Rectangle(570, LABELHEIGHTSIZE);
            pdfDoc.SetPageSize(envelope);
            pdfDoc.SetMargins(pageMargin, pageMargin, 0, 0);

            PdfPTable table;
            PdfPCell cell = null;
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);
            //open the stream 
            pdfDoc.Open();
            string ContainerID = string.Empty;
            foreach (var cartonInfo in posKitS)
            {
                pdfDoc.NewPage();

                PdfPTable tablespace = new PdfPTable(1);
                tablespace.TotalWidth = 550f;
                tablespace.LockedWidth = true;
                tablespace.SetWidths(new float[] { 0.2f });

                if (!string.IsNullOrEmpty(cartonInfo.UPC))
                {
                    UPCImage = GenerateBarCodeUPCATarCode(cartonInfo.UPC, 220f, 52f);
                }
                if (!string.IsNullOrEmpty(cartonInfo.ESN))
                {
                    //IMEIImage = GenerateBarCode(cartonInfo.ESN);
                    IMEIImage = GenerateBarCodeTarCode(cartonInfo.ESN, 9.3f, 1.359f, 3f, 2f);

                }
                if (!string.IsNullOrEmpty(cartonInfo.MEID))
                {
                    //MEIDImage = GenerateBarCode(cartonInfo.MEID);
                    MEIDImage = GenerateBarCodeTarCode(cartonInfo.MEID, 9.3f, 1.359f, 3f, 2f);

                }
                if (!string.IsNullOrEmpty(cartonInfo.ICCID))
                {
                    ICCIDImage = GenerateBarCode(cartonInfo.ICCID);
                }
                if (!string.IsNullOrEmpty(cartonInfo.SerialNum))
                {
                   // SerialNumImage = GenerateMasterBarCode(cartonInfo.SerialNum, 30, 160);
                    SerialNumImage = GenerateBarCodeTarCode(cartonInfo.SerialNum, 14f, 1.359f, 3f, 2f);

                }
                if (!string.IsNullOrEmpty(cartonInfo.HEX))
                {
                    HEXImage = GenerateBarCodeTarCode(cartonInfo.HEX, 8.3f, 1.359f, 3f, 2f);

                    //HEXImage = GenerateBarCode(cartonInfo.HEX);
                }
                if (!string.IsNullOrEmpty(cartonInfo.SKU))
                {
                    if (cartonInfo.SKU.Length == 10)
                        skuWarcodeWidth = 12.8f;
                    else if (cartonInfo.SKU.Length == 11)
                        skuWarcodeWidth = 13f;
                    else if (cartonInfo.SKU.Length == 12)
                        skuWarcodeWidth = 13.09f;
                    else if (cartonInfo.SKU.Length == 13)
                        skuWarcodeWidth = 13.2f;
                    else if (cartonInfo.SKU.Length == 14)
                        skuWarcodeWidth = 13.5f;
                    else if (cartonInfo.SKU.Length == 15)
                        skuWarcodeWidth = 13.7f;
                    else if (cartonInfo.SKU.Length > 15)
                        skuWarcodeWidth = 14f;

                    SKUCodeimage = GenerateBarCodeTarCode(cartonInfo.SKU, skuWarcodeWidth, 1.359f, 3f, 3f);
                    //SKUCodeimage = GenerateBarCodeTarCode(cartonInfo.SKU, 13.09f, 1.359f, 3f, 3f);
                    //SKUCodeimage = GenerateMasterBarCode(cartonInfo.SKU, 30, 220);
                }


                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 4, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));


                PdfPCell cell1 = null;
                cell1 = new PdfPCell();

                table = new PdfPTable(1);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 3, 0, 0, 0, 3);
                //pdfDoc.Add(table);



                //
                float[] widths = new float[] { 170f, 174, 170f };

                table = new PdfPTable(3);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 30f, 290f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                //addCellwithPaddingLeftPaddingBottomSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 15f);
                addCellwithPaddingLeftBottomTopRightSizeBolD(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 1, 1, 1, 18f, 24f);
                addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);
                addCellwithPaddingLeftBottomTopRightSize(table, "Serial Number - Scan for Activation", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 40f, 1f, 8f, 1f, 12f, 24f);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 30f, 290f };

                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell11 = null;
                cell11 = new PdfPCell();

                if (SKUCodeimage != null)
                {
                    if (cartonInfo.ProductType.ToLower() == "h3")
                        cell11 = ImageCellNew22(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f, 29f); // ImageCellNew110(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f); // ImageCellNew2(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 0f);
                    else
                        cell11 = ImageCellNew22(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f, 28f); // ImageCellNew110(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f); // ImageCellNew2(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 0f);

                    //cell11 = ImageCellNew22(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f, 36f); // ImageCellNew110(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f); // ImageCellNew2(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 0f);
                    cell11.BorderWidthRight = 0.5f;
                    cell11.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell11);
                }
                addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 10f);

                // addCellwithPaddingLeftSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //addCellwithPaddingLeftBottomTopRightSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 20f, 1f, 15f, 1f, 12f, 36f);

                PdfPCell cell103 = null;
                cell103 = new PdfPCell();

                if (SerialNumImage != null)
                {
                    cell103 = ImageCellNew22(SerialNumImage, 220f, PdfPCell.ALIGN_LEFT, 25f, 28f);//ImageCellNew110(SerialNumImage, 220f, PdfPCell.ALIGN_LEFT, 1f); //ImageCellNew02(SerialNumImage, 200f, PdfPCell.ALIGN_LEFT, 0f, 3f);
                    cell103.BorderWidthRight = 0.5f;
                    cell103.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell103);

                }
                //addCellwithPaddingLeft(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 30f, 290f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                //addCellwithPaddingLeftPaddingBottomSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 15f);
                addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);

                addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);
                addCellwithPaddingLeftBottomTopRightSize(table, cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 40f, 1f, 1f, 1f, 14f, 16f);

                //addCellwithPaddingLeftPaddingBottomSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 16f);

                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 230f, 340f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                //addCellwithPaddingLeftPaddingBottomSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 15f);

                //addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);
                if (cartonInfo.ProductType.ToLower() == "h3")
                    addCellwithPaddingLeftBottomTopRightSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 4f, 1f, 12f, 16f);
                else
                    addCellwithPaddingLeftBottomTopRightSize(table, "HEX: " + cartonInfo.HEX, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 4f, 1f, 12f, 16f);

                addCellwithPaddingLeftBottomTopRightSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK,10f, 0f,4f, 1f, 12f, 16f);
                //addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);


                pdfDoc.Add(table);




                table = new PdfPTable(2);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 230f, 340 };

                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                if (cartonInfo.ProductType.ToLower() == "h3")
                {
                    //PdfPCell cell0221 = null;
                    //cell0221 = new PdfPCell();
                    //if (IMEIImage != null)
                    //{
                    //    cell0221 = ImageCellNew022(IMEIImage, 180f, PdfPCell.ALIGN_LEFT, 0f, 24f);
                    //    //cell0221 = ImageCellNewLeft(IMEIImage, 180f, PdfPCell.ALIGN_LEFT, 24f, 0f, 3f);
                    //    cell0221.BorderWidthRight = 0.5f;
                    //    cell0221.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    //    table.AddCell(cell0221);

                    //}
                    addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                }
                else
                {
                    PdfPCell cell211 = null;
                    cell211 = new PdfPCell();

                    if (HEXImage != null)
                    {

                        cell211 = ImageCellNew022(HEXImage, 170f, PdfPCell.ALIGN_LEFT, 0f, 24f);
                        cell211.BorderWidthRight = 0.5f;
                        cell211.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell211);

                    }
                }
                PdfPCell cell12 = null;
                cell12 = new PdfPCell();

                //addCellwithPaddingLeftSize(table, "HEX: " + cartonInfo.HEX, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);


                if (UPCImage != null)
                {
                    cell12 = ImageCellNew22(UPCImage, 220f, PdfPCell.ALIGN_LEFT, 1f, 52f);
                    cell12.BorderWidthRight = 0.5f;
                    cell12.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell12);
                }

                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;

                widths = new float[] { 230f, 180f, 160f };
                //widths = new float[] { 230f, 340f };

                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                // addCellwithPaddingLeftBottomTopRightSize(table, "HEX: " + cartonInfo.HEX, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 26f, 1f, 10f, 36f);

                addCellwithPaddingLeftBottomTopRightSize(table, "IMEI: " + cartonInfo.ESN, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 1f, 1f, 12f, 28f);
                addCellwithPaddingLeftBottomTopRightSize(table, "SW Version: " + cartonInfo.SWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 1f, 1f,12f, 28f);
                addCellwithPaddingLeftSizeRowSpan(table, "SO", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 60, 3);


                //addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                
                //pdfDoc.Add(table);

                //table = new PdfPTable(2);
                //table.TotalWidth = 570f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 230f, 340f };

                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(widths);

                PdfPCell cell221 = null;
                cell221 = new PdfPCell();
                if (IMEIImage != null)
                {

                    cell221 = ImageCellNewLeft(IMEIImage, 180f, PdfPCell.ALIGN_LEFT, 36f, 0f, 3f);
                    cell221.BorderWidthRight = 0.5f;
                    cell221.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell221);

                }
                //addCellwithPaddingLeft(table, cartonInfo.UPC, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithPaddingLeftBottomTopRightSize(table, "HW Version: " + cartonInfo.HWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 1f, 10f, 1f, 12f, 32f);

                // addCellwithPaddingLeftSize(table, "HW Version: " + cartonInfo.HWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 12f);
                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

                //pdfDoc.Add(table);

                //table = new PdfPTable(2);
                //table.TotalWidth = 570f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 230f, 340f };

                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(widths);


                if (cartonInfo.ProductType.ToLower() == "h3")
                    addCellwithPaddingLeftBottomTopRightSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 3f, 1f, 12f, 28f);
                else
                    addCellwithPaddingLeftBottomTopRightSize(table, "DEC: " + cartonInfo.MEID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 3f, 1f, 12f, 28f);
                //addCellwithPaddingLeftSize(table, "DEC: " + cartonInfo.MEID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //addCellwithPaddingLeftSize(table, "DEC: " + cartonInfo.MEID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                addCellwithPaddingLeftBottomTopRightSize(table, "Date: " + cartonInfo.ShipDate, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 1f, 1f, 12f, 28f);
              //  addCellwithPaddingLeftSize(table, "Date: " + cartonInfo.ShipDate, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 11f);
               // addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 12f);
                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                if (cartonInfo.ProductType.ToLower() == "h3")
                { }
                else
                {
                    table = new PdfPTable(2);
                    table.TotalWidth = 570f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 230f, 340f };

                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    PdfPCell cell331 = null;
                    cell331 = new PdfPCell();
                    if (MEIDImage != null)
                    {

                        cell331 = ImageCellNewLeft(MEIDImage, 180f, PdfPCell.ALIGN_LEFT, 36f, 0f, 3f);
                        cell331.BorderWidthRight = 0.5f;
                        cell331.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell331);

                    }
                    addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 12f);
                    //addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 12f);
                    pdfDoc.Add(table);
                }
                //if (ICCIDImage != null)
                //{
                //    table = new PdfPTable(3);
                //    table.TotalWidth = 524f;
                //    table.LockedWidth = true;
                //    table.DefaultCell.Border = Rectangle.NO_BORDER;
                //    widths = new float[] { 170f, 174f, 170f };
                //    table.HorizontalAlignment = Element.ALIGN_LEFT;
                //    table.SetWidths(widths);
                //    PdfPCell cell431 = null;
                //    cell431 = new PdfPCell();
                //    if (ICCIDImage != null)
                //    {

                //        cell431 = ImageCellNewLeft(ICCIDImage, 180f, PdfPCell.ALIGN_LEFT, 27f, 10f, 3f);
                //        cell431.BorderWidthRight = 0.5f;
                //        cell431.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                //        table.AddCell(cell431);

                //    }
                //    addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //    addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //    pdfDoc.Add(table);
                //}


                if (cartonInfo.KitBoxList.Count > 0)
                {
                    table = new PdfPTable(1);
                    table.TotalWidth = 570f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 570f };
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    addCellwithborderNormalHeightSize(table, "Phone " + cartonInfo.KitBoxList[0].OriginCountry + ", Accessories " + cartonInfo.KitBoxList[0].OriginCountry, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 30, 1, 5f, 10, 11f);
                    addCellwithborderNew(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);


                }

                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string contentsInclude = "";

                //System.IO stringb  contentsInclude = "";
                foreach (var item in cartonInfo.KitBoxList)
                {
                    //table = new PdfPTable(2);
                    //table.TotalWidth = 500f;
                    //table.LockedWidth = true;
                    //table.DefaultCell.Border = Rectangle.NO_BORDER;
                    //widths = new float[] { 250f, 250f };
                    //table.HorizontalAlignment = Element.ALIGN_LEFT;
                    //table.SetWidths(widths);

                    //sb.Append(item.DisplayName + ",");

                    if (string.IsNullOrEmpty(contentsInclude))
                        contentsInclude = item.DisplayName;
                    else
                        contentsInclude = contentsInclude + "," + item.DisplayName;
                }

                //string contentsInclude = sb.ToString();
                //if (contentsInclude.Length > 0)
                //    contentsInclude = contentsInclude.Substring(0, contentsInclude.Length-1);

                table = new PdfPTable(1);

                table.TotalWidth = 524f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 500f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                addCellwithPaddingTopPaddingLeft(table, "Contents include: " + contentsInclude, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 0, 11f);



                pdfDoc.Add(table);



                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 2, 0, 0, 0, 2);
                //pdfDoc.Add(table);

            }

            // Close PDF document and send

            pdfWriter.CloseStream = false;
            pdfDoc.Close();

            memoryStream.Position = 0;
            return memoryStream;
        }

        public MemoryStream POSKITLabelPdfTarCode2(IList<PosKitInfo> posKitS)
        {
            int bottomMargin = 0;
            int topMargin = 10;
            float LABELHEIGHTSIZE = 280;
            float skuWarcodeWidth = 12.7f;
            const int pageMargin = 3;
            const int pageRows = 1;
            const int pageCols = 1;

            var memoryStream = new MemoryStream();
            // ContainerModel ContainerInfo = new ContainerModel();

            Document pdfDoc = new Document();
            iTextSharp.text.Rectangle envelope = new iTextSharp.text.Rectangle(570, LABELHEIGHTSIZE);
            pdfDoc.SetPageSize(envelope);
            pdfDoc.SetMargins(pageMargin, pageMargin, 0, 0);

            PdfPTable table;
            PdfPCell cell = null;
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);
            //open the stream 
            pdfDoc.Open();
            string ContainerID = string.Empty;
            foreach (var cartonInfo in posKitS)
            {
                pdfDoc.NewPage();

                PdfPTable tablespace = new PdfPTable(1);
                tablespace.TotalWidth = 550f;
                tablespace.LockedWidth = true;
                tablespace.SetWidths(new float[] { 0.2f });

                if (!string.IsNullOrEmpty(cartonInfo.UPC))
                {
                    UPCImage = GenerateBarCodeUPCATarCode(cartonInfo.UPC, 220f, 52f);
                }
                if (!string.IsNullOrEmpty(cartonInfo.ESN))
                {
                    //IMEIImage = GenerateBarCode(cartonInfo.ESN);
                    IMEIImage = GenerateBarCodeTarCode(cartonInfo.ESN, 9.3f, 1.359f, 3f, 2f);

                }
                if (!string.IsNullOrEmpty(cartonInfo.MEID))
                {
                    //MEIDImage = GenerateBarCode(cartonInfo.MEID);
                    MEIDImage = GenerateBarCodeTarCode(cartonInfo.MEID, 9.3f, 1.359f, 3f, 2f);

                }
                if (!string.IsNullOrEmpty(cartonInfo.ICCID))
                {
                    ICCIDImage = GenerateBarCode(cartonInfo.ICCID);
                }
                if (!string.IsNullOrEmpty(cartonInfo.SerialNum))
                {
                    // SerialNumImage = GenerateMasterBarCode(cartonInfo.SerialNum, 30, 160);
                    SerialNumImage = GenerateBarCodeTarCode(cartonInfo.SerialNum, 14f, 1.359f, 3f, 2f);

                }
                if (!string.IsNullOrEmpty(cartonInfo.HEX))
                {
                    HEXImage = GenerateBarCodeTarCode(cartonInfo.HEX, 8.3f, 1.359f, 3f, 2f);

                    //HEXImage = GenerateBarCode(cartonInfo.HEX);
                }
                if (!string.IsNullOrEmpty(cartonInfo.SKU))
                {
                    if (cartonInfo.SKU.Length == 10)
                        skuWarcodeWidth = 12.8f;                    
                    else if (cartonInfo.SKU.Length == 11)
                        skuWarcodeWidth = 13f;
                    else if (cartonInfo.SKU.Length == 12)
                        skuWarcodeWidth = 13.09f;
                    else if (cartonInfo.SKU.Length == 13)
                        skuWarcodeWidth = 13.2f;
                    else if (cartonInfo.SKU.Length == 14)
                        skuWarcodeWidth = 13.5f;
                    else if (cartonInfo.SKU.Length == 15)
                        skuWarcodeWidth = 13.7f;
                    else if (cartonInfo.SKU.Length > 15)
                        skuWarcodeWidth = 14f;

                    SKUCodeimage = GenerateBarCodeTarCode(cartonInfo.SKU, skuWarcodeWidth, 1.359f, 3f, 3f);
                    //SKUCodeimage = GenerateMasterBarCode(cartonInfo.SKU, 30, 220);
                }


                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 4, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));


                PdfPCell cell1 = null;
                cell1 = new PdfPCell();

                table = new PdfPTable(1);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 3, 0, 0, 0, 3);
                //pdfDoc.Add(table);



                //
                float[] widths = new float[] { 170f, 174, 170f };

                table = new PdfPTable(3);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 30f, 290f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                //addCellwithPaddingLeftPaddingBottomSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 15f);
                addCellwithPaddingLeftBottomTopRightSizeBolD(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 1, 1, 1, 18f, 24f);
                addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);
                addCellwithPaddingLeftBottomTopRightSize(table, "Serial Number - Scan for Activation", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 40f, 1f, 8f, 1f, 12f, 24f);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 30f, 290f };

                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell11 = null;
                cell11 = new PdfPCell();

                if (SKUCodeimage != null)
                {
                    cell11 = ImageCellNew22(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f, 28f); // ImageCellNew110(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f); // ImageCellNew2(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 0f);
                    cell11.BorderWidthRight = 0.5f;
                    cell11.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell11);
                }
                addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 10f);

                // addCellwithPaddingLeftSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //addCellwithPaddingLeftBottomTopRightSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 20f, 1f, 15f, 1f, 12f, 36f);

                PdfPCell cell103 = null;
                cell103 = new PdfPCell();

                if (SerialNumImage != null)
                {
                    cell103 = ImageCellNew22(SerialNumImage, 220f, PdfPCell.ALIGN_LEFT, 25f, 28f);//ImageCellNew110(SerialNumImage, 220f, PdfPCell.ALIGN_LEFT, 1f); //ImageCellNew02(SerialNumImage, 200f, PdfPCell.ALIGN_LEFT, 0f, 3f);
                    cell103.BorderWidthRight = 0.5f;
                    cell103.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell103);

                }
                //addCellwithPaddingLeft(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 30f, 290f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                //addCellwithPaddingLeftPaddingBottomSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 15f);
                addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);

                addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);
                addCellwithPaddingLeftBottomTopRightSize(table, cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 40f, 1f, 1f, 1f, 14f, 16f);

                //addCellwithPaddingLeftPaddingBottomSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 16f);

                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 230f, 340f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                //addCellwithPaddingLeftPaddingBottomSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 15f);

                //addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);
                //if (cartonInfo.ProductType.ToLower() == "h3")
                //    addCellwithPaddingLeftBottomTopRightSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 4f, 1f, 12f, 16f);
                if (cartonInfo.ProductType.ToLower() == "h3")
                    addCellwithPaddingLeftBottomTopRightSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 4f, 1f, 12f, 16f);
                else
                    addCellwithPaddingLeftBottomTopRightSize(table, "HEX: " + cartonInfo.HEX, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 4f, 1f, 12f, 16f);

                addCellwithPaddingLeftBottomTopRightSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 4f, 1f, 12f, 16f);
                //addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);


                pdfDoc.Add(table);




                table = new PdfPTable(2);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 230f, 340 };

                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                
                if (cartonInfo.ProductType.ToLower() == "h3")
                {
                    //PdfPCell cell0221 = null;
                    //cell0221 = new PdfPCell();
                    //if (IMEIImage != null)
                    //{
                    //    cell0221 = ImageCellNew022(IMEIImage, 180f, PdfPCell.ALIGN_LEFT, 0f, 24f);
                    //    //cell0221 = ImageCellNewLeft(IMEIImage, 180f, PdfPCell.ALIGN_LEFT, 24f, 0f, 3f);
                    //    cell0221.BorderWidthRight = 0.5f;
                    //    cell0221.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    //    table.AddCell(cell0221);

                    //}
                    addCellwithPaddingLeftSize(table, "" , 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                }
                else
                {
                    PdfPCell cell211 = null;
                    cell211 = new PdfPCell();

                    if (HEXImage != null)
                    {

                        cell211 = ImageCellNew022(HEXImage, 170f, PdfPCell.ALIGN_LEFT, 0f, 24f);
                        cell211.BorderWidthRight = 0.5f;
                        cell211.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell211);

                    }
                }
                PdfPCell cell12 = null;
                cell12 = new PdfPCell();

                //addCellwithPaddingLeftSize(table, "HEX: " + cartonInfo.HEX, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);


                if (UPCImage != null)
                {
                    cell12 = ImageCellNew22(UPCImage, 220f, PdfPCell.ALIGN_LEFT, 1f, 52f);
                    cell12.BorderWidthRight = 0.5f;
                    cell12.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell12);
                }

                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;

                widths = new float[] { 230f, 180f, 160f };
                //widths = new float[] { 230f, 340f };

                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                // addCellwithPaddingLeftBottomTopRightSize(table, "HEX: " + cartonInfo.HEX, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 26f, 1f, 10f, 36f);
                //if (cartonInfo.ProductType.ToLower() == "h3")
                //    addCellwithPaddingLeftBottomTopRightSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 1f, 1f, 12f, 28f);
                //else
                    addCellwithPaddingLeftBottomTopRightSize(table, "IMEI: " + cartonInfo.ESN, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 1f, 1f, 12f, 28f);
                addCellwithPaddingLeftBottomTopRightSize(table, "SW Version: " + cartonInfo.SWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 1f, 1f, 12f, 28f);
                //if (cartonInfo.ProductType.ToLower() == "h3")
                //    addCellwithPaddingLeftSizeRowSpan(table, "SN", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 60, 3);
                //else
                    addCellwithPaddingLeftSizeRowSpan(table, "SO", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 60, 3);


                //addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

                //pdfDoc.Add(table);

                //table = new PdfPTable(2);
                //table.TotalWidth = 570f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 230f, 340f };

                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(widths);
                //if (cartonInfo.ProductType.ToLower() == "h3")
                //    addCellwithPaddingLeftBottomTopRightSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 1f, 1f, 12f, 28f);
                //else
                {
                    PdfPCell cell221 = null;
                    cell221 = new PdfPCell();
                    if (IMEIImage != null)
                    {

                        cell221 = ImageCellNewLeft(IMEIImage, 180f, PdfPCell.ALIGN_LEFT, 36f, 0f, 3f);
                        cell221.BorderWidthRight = 0.5f;
                        cell221.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell221);

                    }
                }
                //addCellwithPaddingLeft(table, cartonInfo.UPC, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithPaddingLeftBottomTopRightSize(table, "HW Version: " + cartonInfo.HWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 1f, 10f, 1f, 12f, 32f);

                // addCellwithPaddingLeftSize(table, "HW Version: " + cartonInfo.HWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 12f);
                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

                //pdfDoc.Add(table);

                //table = new PdfPTable(2);
                //table.TotalWidth = 570f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 230f, 340f };

                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(widths);
                if (cartonInfo.ProductType.ToLower() == "h3")
                    addCellwithPaddingLeftBottomTopRightSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 3f, 1f, 12f, 28f);
                else
                    addCellwithPaddingLeftBottomTopRightSize(table, "DEC: " + cartonInfo.MEID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 3f, 1f, 12f, 28f);

                //addCellwithPaddingLeftSize(table, "DEC: " + cartonInfo.MEID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //addCellwithPaddingLeftSize(table, "DEC: " + cartonInfo.MEID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                addCellwithPaddingLeftBottomTopRightSize(table, "Date: " + cartonInfo.ShipDate, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 1f, 1f, 12f, 28f);
                //  addCellwithPaddingLeftSize(table, "Date: " + cartonInfo.ShipDate, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 11f);
                // addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 12f);
                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                if (cartonInfo.ProductType.ToLower() == "h3")
                { }
                else
                {
                    table = new PdfPTable(2);
                    table.TotalWidth = 570f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 230f, 340f };

                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    PdfPCell cell331 = null;
                    cell331 = new PdfPCell();
                    if (MEIDImage != null)
                    {

                        cell331 = ImageCellNewLeft(MEIDImage, 180f, PdfPCell.ALIGN_LEFT, 36f, 0f, 3f);
                        cell331.BorderWidthRight = 0.5f;
                        cell331.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell331);

                    }
                    addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 12f);
                    //addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 12f);
                    pdfDoc.Add(table);
                }

                //if (ICCIDImage != null)
                //{
                //    table = new PdfPTable(3);
                //    table.TotalWidth = 524f;
                //    table.LockedWidth = true;
                //    table.DefaultCell.Border = Rectangle.NO_BORDER;
                //    widths = new float[] { 170f, 174f, 170f };
                //    table.HorizontalAlignment = Element.ALIGN_LEFT;
                //    table.SetWidths(widths);
                //    PdfPCell cell431 = null;
                //    cell431 = new PdfPCell();
                //    if (ICCIDImage != null)
                //    {

                //        cell431 = ImageCellNewLeft(ICCIDImage, 180f, PdfPCell.ALIGN_LEFT, 27f, 10f, 3f);
                //        cell431.BorderWidthRight = 0.5f;
                //        cell431.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                //        table.AddCell(cell431);

                //    }
                //    addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //    addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //    pdfDoc.Add(table);
                //}


                if (cartonInfo.KitBoxList.Count > 0)
                {
                    table = new PdfPTable(1);
                    table.TotalWidth = 570f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 570f };
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    addCellwithborderNormalHeightSize(table, "Phone " + cartonInfo.KitBoxList[0].OriginCountry + ", Accessories " + cartonInfo.KitBoxList[0].OriginCountry, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 30, 1, 5f, 10, 11f);
                    addCellwithborderNew(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);


                }

                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string contentsInclude = "";

                //System.IO stringb  contentsInclude = "";
                foreach (var item in cartonInfo.KitBoxList)
                {
                    //table = new PdfPTable(2);
                    //table.TotalWidth = 500f;
                    //table.LockedWidth = true;
                    //table.DefaultCell.Border = Rectangle.NO_BORDER;
                    //widths = new float[] { 250f, 250f };
                    //table.HorizontalAlignment = Element.ALIGN_LEFT;
                    //table.SetWidths(widths);

                    //sb.Append(item.DisplayName + ",");

                    if (string.IsNullOrEmpty(contentsInclude))
                        contentsInclude = item.DisplayName;
                    else
                        contentsInclude = contentsInclude + "," + item.DisplayName;
                }

                //string contentsInclude = sb.ToString();
                //if (contentsInclude.Length > 0)
                //    contentsInclude = contentsInclude.Substring(0, contentsInclude.Length-1);

                table = new PdfPTable(1);

                table.TotalWidth = 524f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 500f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                addCellwithPaddingTopPaddingLeft(table, "Contents include: " + contentsInclude, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 0, 11f);



                pdfDoc.Add(table);



                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 2, 0, 0, 0, 2);
                //pdfDoc.Add(table);

            }

            // Close PDF document and send

            pdfWriter.CloseStream = false;
            pdfDoc.Close();

            memoryStream.Position = 0;
            return memoryStream;
        }
        public MemoryStream POSKITLabelPdfTarCodeNew2(IList<PosKitInfo> posKitS)
        {
            int bottomMargin = 0;
            int topMargin = 10;
            float LABELHEIGHTSIZE = 280;
            float skuWarcodeWidth = 12.7f;
            const int pageMargin = 3;
            const int pageRows = 1;
            const int pageCols = 1;

            var memoryStream = new MemoryStream();
            // ContainerModel ContainerInfo = new ContainerModel();

            Document pdfDoc = new Document();
            iTextSharp.text.Rectangle envelope = new iTextSharp.text.Rectangle(570, LABELHEIGHTSIZE);
            pdfDoc.SetPageSize(envelope);
            pdfDoc.SetMargins(pageMargin, pageMargin, 0, 0);

            PdfPTable table;
            PdfPCell cell = null;
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);
            //open the stream 
            pdfDoc.Open();
            string ContainerID = string.Empty;
            foreach (var cartonInfo in posKitS)
            {
                pdfDoc.NewPage();

                PdfPTable tablespace = new PdfPTable(1);
                tablespace.TotalWidth = 550f;
                tablespace.LockedWidth = true;
                tablespace.SetWidths(new float[] { 0.2f });

                if (!string.IsNullOrEmpty(cartonInfo.UPC))
                {
                    UPCImage = GenerateBarCodeUPCATarCode(cartonInfo.UPC, 220f, 52f);
                }
                if (!string.IsNullOrEmpty(cartonInfo.ESN))
                {
                    //IMEIImage = GenerateBarCode(cartonInfo.ESN);
                    IMEIImage = GenerateBarCodeTarCode(cartonInfo.ESN, 9.3f, 1.359f, 3f, 2f);

                }
                if (!string.IsNullOrEmpty(cartonInfo.MEID))
                {
                    //MEIDImage = GenerateBarCode(cartonInfo.MEID);
                    MEIDImage = GenerateBarCodeTarCode(cartonInfo.MEID, 9.3f, 1.359f, 3f, 2f);

                }
                if (!string.IsNullOrEmpty(cartonInfo.ICCID))
                {
                    ICCIDImage = GenerateBarCode(cartonInfo.ICCID);
                }
                if (!string.IsNullOrEmpty(cartonInfo.SerialNum))
                {
                    // SerialNumImage = GenerateMasterBarCode(cartonInfo.SerialNum, 30, 160);
                    SerialNumImage = GenerateBarCodeTarCode(cartonInfo.SerialNum, 14f, 1.359f, 3f, 2f);

                }
                if (!string.IsNullOrEmpty(cartonInfo.HEX))
                {
                    HEXImage = GenerateBarCodeTarCode(cartonInfo.HEX, 8.3f, 1.359f, 3f, 2f);

                    //HEXImage = GenerateBarCode(cartonInfo.HEX);
                }
                if (!string.IsNullOrEmpty(cartonInfo.SKU))
                {
                    if (cartonInfo.SKU.Length == 10)
                        skuWarcodeWidth = 12.8f;
                    else if (cartonInfo.SKU.Length == 11)
                        skuWarcodeWidth = 13f;
                    else if (cartonInfo.SKU.Length == 12)
                        skuWarcodeWidth = 13.09f;
                    else if (cartonInfo.SKU.Length == 13)
                        skuWarcodeWidth = 13.2f;
                    else if (cartonInfo.SKU.Length == 14)
                        skuWarcodeWidth = 13.5f;
                    else if (cartonInfo.SKU.Length == 15)
                        skuWarcodeWidth = 13.7f;
                    else if (cartonInfo.SKU.Length > 15)
                        skuWarcodeWidth = 14f;

                    SKUCodeimage = GenerateBarCodeTarCode(cartonInfo.SKU, skuWarcodeWidth, 1.359f, 3f, 3f);
                    //SKUCodeimage = GenerateMasterBarCode(cartonInfo.SKU, 30, 220);
                }


                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 4, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));


                PdfPCell cell1 = null;
                cell1 = new PdfPCell();

                table = new PdfPTable(1);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 3, 0, 0, 0, 3);
                //pdfDoc.Add(table);



                //
                float[] widths = new float[] { 170f, 174, 170f };

                table = new PdfPTable(3);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 30f, 290f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                //addCellwithPaddingLeftPaddingBottomSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 15f);
                addCellwithPaddingLeftBottomTopRightSizeBolD(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 1, 1, 1, 18f, 24f);
                addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);
                addCellwithPaddingLeftBottomTopRightSize(table, "Serial Number - Scan for Activation", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 40f, 1f, 8f, 1f, 12f, 24f);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 30f, 290f };

                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell11 = null;
                cell11 = new PdfPCell();

                if (SKUCodeimage != null)
                {
                    if (cartonInfo.ProductType.ToLower() == "h3")
                        cell11 = ImageCellNew22(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f, 29f); // ImageCellNew110(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f); // ImageCellNew2(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 0f);
                    else
                        cell11 = ImageCellNew22(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f, 28f); // ImageCellNew110(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f); // ImageCellNew2(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 0f);
                    cell11.BorderWidthRight = 0.5f;
                    cell11.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell11);
                }
                addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 10f);

                // addCellwithPaddingLeftSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //addCellwithPaddingLeftBottomTopRightSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 20f, 1f, 15f, 1f, 12f, 36f);

                PdfPCell cell103 = null;
                cell103 = new PdfPCell();

                if (SerialNumImage != null)
                {
                    cell103 = ImageCellNew22(SerialNumImage, 220f, PdfPCell.ALIGN_LEFT, 25f, 28f);//ImageCellNew110(SerialNumImage, 220f, PdfPCell.ALIGN_LEFT, 1f); //ImageCellNew02(SerialNumImage, 200f, PdfPCell.ALIGN_LEFT, 0f, 3f);
                    cell103.BorderWidthRight = 0.5f;
                    cell103.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell103);

                }
                //addCellwithPaddingLeft(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 30f, 290f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                //addCellwithPaddingLeftPaddingBottomSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 15f);
                addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);

                addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);
                addCellwithPaddingLeftBottomTopRightSize(table, cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 40f, 1f, 1f, 1f, 14f, 16f);

                //addCellwithPaddingLeftPaddingBottomSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 16f);

                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 230f, 340f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                //addCellwithPaddingLeftPaddingBottomSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 15f);

                //addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);
                //if (cartonInfo.ProductType.ToLower() == "h3")
                //    addCellwithPaddingLeftBottomTopRightSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 4f, 1f, 12f, 16f);
                if (cartonInfo.ProductType.ToLower() == "h3")
                    addCellwithPaddingLeftBottomTopRightSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 4f, 1f, 12f, 16f);
                else
                    addCellwithPaddingLeftBottomTopRightSize(table, "HEX: " + cartonInfo.HEX, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 4f, 1f, 12f, 16f);

                addCellwithPaddingLeftBottomTopRightSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 4f, 1f, 12f, 16f);
                //addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);

                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 230f, 340 };

                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                if (cartonInfo.ProductType.ToLower() == "h3")
                {
                    //PdfPCell cell0221 = null;
                    //cell0221 = new PdfPCell();
                    //if (IMEIImage != null)
                    //{
                    //    cell0221 = ImageCellNew022(IMEIImage, 180f, PdfPCell.ALIGN_LEFT, 0f, 24f);
                    //    //cell0221 = ImageCellNewLeft(IMEIImage, 180f, PdfPCell.ALIGN_LEFT, 24f, 0f, 3f);
                    //    cell0221.BorderWidthRight = 0.5f;
                    //    cell0221.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    //    table.AddCell(cell0221);

                    //}
                    addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                }
                else
                {
                    PdfPCell cell211 = null;
                    cell211 = new PdfPCell();

                    if (HEXImage != null)
                    {

                        cell211 = ImageCellNew022(HEXImage, 170f, PdfPCell.ALIGN_LEFT, 0f, 24f);
                        cell211.BorderWidthRight = 0.5f;
                        cell211.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell211);

                    }
                }
                PdfPCell cell12 = null;
                cell12 = new PdfPCell();

                //addCellwithPaddingLeftSize(table, "HEX: " + cartonInfo.HEX, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);


                if (UPCImage != null)
                {
                    cell12 = ImageCellNew22(UPCImage, 220f, PdfPCell.ALIGN_LEFT, 1f, 52f);
                    cell12.BorderWidthRight = 0.5f;
                    cell12.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell12);
                }

                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;

                widths = new float[] { 230f, 180f, 160f };
                //widths = new float[] { 230f, 340f };

                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                // addCellwithPaddingLeftBottomTopRightSize(table, "HEX: " + cartonInfo.HEX, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 26f, 1f, 10f, 36f);
                //if (cartonInfo.ProductType.ToLower() == "h3")
                //    addCellwithPaddingLeftBottomTopRightSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 1f, 1f, 12f, 28f);
                //else
                addCellwithPaddingLeftBottomTopRightSize(table, "IMEI: " + cartonInfo.ESN, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 1f, 1f, 12f, 28f);
                addCellwithPaddingLeftBottomTopRightSize(table, "SW Version: " + cartonInfo.SWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 1f, 1f, 12f, 28f);
                //if (cartonInfo.ProductType.ToLower() == "h3")
                //    addCellwithPaddingLeftSizeRowSpan(table, "SN", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 60, 3);
                //else
                addCellwithPaddingLeftSizeRowSpan(table, "SO", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 60, 3);


                //addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

                //pdfDoc.Add(table);

                //table = new PdfPTable(2);
                //table.TotalWidth = 570f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 230f, 340f };

                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(widths);
                //if (cartonInfo.ProductType.ToLower() == "h3")
                //    addCellwithPaddingLeftBottomTopRightSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 1f, 1f, 12f, 28f);
                //else
                {
                    PdfPCell cell221 = null;
                    cell221 = new PdfPCell();
                    if (IMEIImage != null)
                    {

                        cell221 = ImageCellNewLeft(IMEIImage, 180f, PdfPCell.ALIGN_LEFT, 36f, 0f, 3f);
                        cell221.BorderWidthRight = 0.5f;
                        cell221.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell221);

                    }
                }
                //addCellwithPaddingLeft(table, cartonInfo.UPC, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithPaddingLeftBottomTopRightSize(table, "HW Version: " + cartonInfo.HWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 1f, 10f, 1f, 12f, 32f);

                // addCellwithPaddingLeftSize(table, "HW Version: " + cartonInfo.HWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 12f);
                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

                //pdfDoc.Add(table);

                //table = new PdfPTable(2);
                //table.TotalWidth = 570f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //widths = new float[] { 230f, 340f };

                //table.HorizontalAlignment = Element.ALIGN_LEFT;
                //table.SetWidths(widths);
                if (cartonInfo.ProductType.ToLower() == "h3")
                    addCellwithPaddingLeftBottomTopRightSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 3f, 1f, 12f, 28f);
                else
                    addCellwithPaddingLeftBottomTopRightSize(table, "DEC: " + cartonInfo.MEID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 3f, 1f, 12f, 28f);

                //addCellwithPaddingLeftSize(table, "DEC: " + cartonInfo.MEID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //addCellwithPaddingLeftSize(table, "DEC: " + cartonInfo.MEID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                addCellwithPaddingLeftBottomTopRightSize(table, "Date: " + cartonInfo.ShipDate, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 1f, 1f, 12f, 28f);
                //  addCellwithPaddingLeftSize(table, "Date: " + cartonInfo.ShipDate, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 11f);
                // addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 12f);
                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                if (cartonInfo.ProductType.ToLower() == "h3")
                { }
                else
                {
                    table = new PdfPTable(2);
                    table.TotalWidth = 570f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 230f, 340f };

                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    PdfPCell cell331 = null;
                    cell331 = new PdfPCell();
                    if (MEIDImage != null)
                    {

                        cell331 = ImageCellNewLeft(MEIDImage, 180f, PdfPCell.ALIGN_LEFT, 36f, 0f, 3f);
                        cell331.BorderWidthRight = 0.5f;
                        cell331.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell331);

                    }
                    addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 12f);
                    //addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 12f);
                    pdfDoc.Add(table);
                }

                //if (ICCIDImage != null)
                //{
                //    table = new PdfPTable(3);
                //    table.TotalWidth = 524f;
                //    table.LockedWidth = true;
                //    table.DefaultCell.Border = Rectangle.NO_BORDER;
                //    widths = new float[] { 170f, 174f, 170f };
                //    table.HorizontalAlignment = Element.ALIGN_LEFT;
                //    table.SetWidths(widths);
                //    PdfPCell cell431 = null;
                //    cell431 = new PdfPCell();
                //    if (ICCIDImage != null)
                //    {

                //        cell431 = ImageCellNewLeft(ICCIDImage, 180f, PdfPCell.ALIGN_LEFT, 27f, 10f, 3f);
                //        cell431.BorderWidthRight = 0.5f;
                //        cell431.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                //        table.AddCell(cell431);

                //    }
                //    addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //    addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //    pdfDoc.Add(table);
                //}


                if (cartonInfo.KitBoxList.Count > 0)
                {
                    table = new PdfPTable(1);
                    table.TotalWidth = 570f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 570f };
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    addCellwithborderNormalHeightSize(table, "Phone " + cartonInfo.KitBoxList[0].OriginCountry + ", Accessories " + cartonInfo.KitBoxList[0].OriginCountry, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 30, 1, 5f, 10, 11f);
                    addCellwithborderNew(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);


                }

                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string contentsInclude = "";

                //System.IO stringb  contentsInclude = "";
                foreach (var item in cartonInfo.KitBoxList)
                {
                    //table = new PdfPTable(2);
                    //table.TotalWidth = 500f;
                    //table.LockedWidth = true;
                    //table.DefaultCell.Border = Rectangle.NO_BORDER;
                    //widths = new float[] { 250f, 250f };
                    //table.HorizontalAlignment = Element.ALIGN_LEFT;
                    //table.SetWidths(widths);

                    //sb.Append(item.DisplayName + ",");

                    if (string.IsNullOrEmpty(contentsInclude))
                        contentsInclude = item.DisplayName;
                    else
                        contentsInclude = contentsInclude + "," + item.DisplayName;
                }

                //string contentsInclude = sb.ToString();
                //if (contentsInclude.Length > 0)
                //    contentsInclude = contentsInclude.Substring(0, contentsInclude.Length-1);

                table = new PdfPTable(1);

                table.TotalWidth = 524f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 500f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                addCellwithPaddingTopPaddingLeft(table, "Contents include: " + contentsInclude, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 0, 11f);



                pdfDoc.Add(table);



                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 2, 0, 0, 0, 2);
                //pdfDoc.Add(table);

            }

            // Close PDF document and send

            pdfWriter.CloseStream = false;
            pdfDoc.Close();

            memoryStream.Position = 0;
            return memoryStream;
        }

        public MemoryStream POSKITLabelPdfTarCodeNEW(IList<PosKitInfo> posKitS)
        {
            int bottomMargin = 0;
            int topMargin = 10;
            float LABELHEIGHTSIZE = 280;
            const int pageMargin = 3;
            const int pageRows = 1;
            const int pageCols = 1;

            var memoryStream = new MemoryStream();
            // ContainerModel ContainerInfo = new ContainerModel();

            Document pdfDoc = new Document();
            iTextSharp.text.Rectangle envelope = new iTextSharp.text.Rectangle(570, LABELHEIGHTSIZE);
            pdfDoc.SetPageSize(envelope);
            pdfDoc.SetMargins(pageMargin, pageMargin, 0, 0);

            PdfPTable table;
            PdfPCell cell = null;
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);
            //open the stream 
            pdfDoc.Open();
            string ContainerID = string.Empty;
            foreach (var cartonInfo in posKitS)
            {
                pdfDoc.NewPage();

                PdfPTable tablespace = new PdfPTable(1);
                tablespace.TotalWidth = 550f;
                tablespace.LockedWidth = true;
                tablespace.SetWidths(new float[] { 0.2f });

                if (!string.IsNullOrEmpty(cartonInfo.UPC))
                {
                    UPCImage = GenerateBarCodeUPCATarCode(cartonInfo.UPC, 220f, 52f);
                }
                if (!string.IsNullOrEmpty(cartonInfo.ESN))
                {
                    //IMEIImage = GenerateBarCode(cartonInfo.ESN);
                    IMEIImage = GenerateBarCodeTarCode(cartonInfo.ESN, 9.3f, 1.359f, 3f, 2f);

                }
                if (!string.IsNullOrEmpty(cartonInfo.MEID))
                {
                    //MEIDImage = GenerateBarCode(cartonInfo.MEID);
                    MEIDImage = GenerateBarCodeTarCode(cartonInfo.MEID, 9.3f, 1.359f, 3f, 2f);

                }
                if (!string.IsNullOrEmpty(cartonInfo.ICCID))
                {
                    ICCIDImage = GenerateBarCode(cartonInfo.ICCID);
                }
                if (!string.IsNullOrEmpty(cartonInfo.SerialNum))
                {
                    // SerialNumImage = GenerateMasterBarCode(cartonInfo.SerialNum, 30, 160);
                    SerialNumImage = GenerateBarCodeTarCode(cartonInfo.SerialNum, 14f, 1.359f, 3f, 2f);

                }
                if (!string.IsNullOrEmpty(cartonInfo.HEX))
                {
                    HEXImage = GenerateBarCodeTarCode(cartonInfo.HEX, 8.3f, 1.359f, 3f, 2f);

                    //HEXImage = GenerateBarCode(cartonInfo.HEX);
                }
                if (!string.IsNullOrEmpty(cartonInfo.SKU))
                {
                    SKUCodeimage = GenerateBarCodeTarCode(cartonInfo.SKU, 16.09f, 1.359f, 3f, 3f);
                    //SKUCodeimage = GenerateMasterBarCode(cartonInfo.SKU, 30, 220);
                }


                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 4, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));


                PdfPCell cell1 = null;
                cell1 = new PdfPCell();

                table = new PdfPTable(1);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 3, 0, 0, 0, 3);
                //pdfDoc.Add(table);



                //
                float[] widths = new float[] { 170f, 174, 170f };

                table = new PdfPTable(3);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 279f, 1f, 290f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                //addCellwithPaddingLeftPaddingBottomSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 15f);
                addCellwithPaddingLeftBottomTopRightSizeBolD(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 1, 1, 1, 18f, 24f);
                addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);
                addCellwithPaddingLeftBottomTopRightSize(table, "Serial Number - Scan for Activation", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 40f, 1f, 8f, 1f, 12f, 24f);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 30f, 290f };

                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell11 = null;
                cell11 = new PdfPCell();

                if (SKUCodeimage != null)
                {
                    cell11 = ImageCellNew22(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f, 28f); // ImageCellNew110(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f); // ImageCellNew2(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 0f);
                    cell11.BorderWidthRight = 0.5f;
                    cell11.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell11);
                }
                addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 10f);

                // addCellwithPaddingLeftSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //addCellwithPaddingLeftBottomTopRightSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 20f, 1f, 15f, 1f, 12f, 36f);

                PdfPCell cell103 = null;
                cell103 = new PdfPCell();

                if (SerialNumImage != null)
                {
                    cell103 = ImageCellNew22(SerialNumImage, 220f, PdfPCell.ALIGN_LEFT, 25f, 28f);//ImageCellNew110(SerialNumImage, 220f, PdfPCell.ALIGN_LEFT, 1f); //ImageCellNew02(SerialNumImage, 200f, PdfPCell.ALIGN_LEFT, 0f, 3f);
                    cell103.BorderWidthRight = 0.5f;
                    cell103.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell103);

                }
                //addCellwithPaddingLeft(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 30f, 290f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                //addCellwithPaddingLeftPaddingBottomSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 15f);
                addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);

                addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);
                addCellwithPaddingLeftBottomTopRightSize(table, cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 40f, 1f, 1f, 1f, 14f, 16f);

                //addCellwithPaddingLeftPaddingBottomSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 16f);

                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 230f, 340f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                //addCellwithPaddingLeftPaddingBottomSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 15f);

                //addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);
                addCellwithPaddingLeftBottomTopRightSize(table, "HEX: " + cartonInfo.HEX, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 4f, 1f, 12f, 16f);

                addCellwithPaddingLeftBottomTopRightSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 4f, 1f, 12f, 16f);
                //addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);


                pdfDoc.Add(table);




                table = new PdfPTable(2);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 230f, 340 };

                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell211 = null;
                cell211 = new PdfPCell();

                if (HEXImage != null)
                {

                    cell211 = ImageCellNew022(HEXImage, 170f, PdfPCell.ALIGN_LEFT, 0f, 24f);
                    cell211.BorderWidthRight = 0.5f;
                    cell211.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell211);

                }

                PdfPCell cell12 = null;
                cell12 = new PdfPCell();

                //addCellwithPaddingLeftSize(table, "HEX: " + cartonInfo.HEX, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);


                if (UPCImage != null)
                {
                    cell12 = ImageCellNew22(UPCImage, 220f, PdfPCell.ALIGN_LEFT, 1f, 52f);
                    cell12.BorderWidthRight = 0.5f;
                    cell12.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell12);
                }

                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 230f, 340f };

                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                // addCellwithPaddingLeftBottomTopRightSize(table, "HEX: " + cartonInfo.HEX, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 26f, 1f, 10f, 36f);

                addCellwithPaddingLeftBottomTopRightSize(table, "IMEI: " + cartonInfo.ESN, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 1f, 1f, 12f, 28f);
                addCellwithPaddingLeftBottomTopRightSize(table, "SW Version: " + cartonInfo.SWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 1f, 1f, 12f, 28f);
                //addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 230f, 340f };

                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell221 = null;
                cell221 = new PdfPCell();
                if (IMEIImage != null)
                {

                    cell221 = ImageCellNewLeft(IMEIImage, 180f, PdfPCell.ALIGN_LEFT, 36f, 0f, 3f);
                    cell221.BorderWidthRight = 0.5f;
                    cell221.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell221);

                }
                //addCellwithPaddingLeft(table, cartonInfo.UPC, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithPaddingLeftBottomTopRightSize(table, "HW Version: " + cartonInfo.HWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 1f, 10f, 1f, 12f, 32f);

                // addCellwithPaddingLeftSize(table, "HW Version: " + cartonInfo.HWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 12f);
                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 230f, 340f };

                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                addCellwithPaddingLeftBottomTopRightSize(table, "DEC: " + cartonInfo.MEID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 3f, 1f, 12f, 28f);
                //addCellwithPaddingLeftSize(table, "DEC: " + cartonInfo.MEID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //addCellwithPaddingLeftSize(table, "DEC: " + cartonInfo.MEID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                addCellwithPaddingLeftBottomTopRightSize(table, "Date: " + cartonInfo.ShipDate, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 1f, 1f, 12f, 28f);
                //  addCellwithPaddingLeftSize(table, "Date: " + cartonInfo.ShipDate, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 11f);
                // addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 12f);
                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 570f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 230f, 340f };

                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell331 = null;
                cell331 = new PdfPCell();
                if (MEIDImage != null)
                {

                    cell331 = ImageCellNewLeft(MEIDImage, 180f, PdfPCell.ALIGN_LEFT, 36f, 0f, 3f);
                    cell331.BorderWidthRight = 0.5f;
                    cell331.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell331);

                }
                addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 12f);
                //addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 12f);
                pdfDoc.Add(table);

                //if (ICCIDImage != null)
                //{
                //    table = new PdfPTable(3);
                //    table.TotalWidth = 524f;
                //    table.LockedWidth = true;
                //    table.DefaultCell.Border = Rectangle.NO_BORDER;
                //    widths = new float[] { 170f, 174f, 170f };
                //    table.HorizontalAlignment = Element.ALIGN_LEFT;
                //    table.SetWidths(widths);
                //    PdfPCell cell431 = null;
                //    cell431 = new PdfPCell();
                //    if (ICCIDImage != null)
                //    {

                //        cell431 = ImageCellNewLeft(ICCIDImage, 180f, PdfPCell.ALIGN_LEFT, 27f, 10f, 3f);
                //        cell431.BorderWidthRight = 0.5f;
                //        cell431.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                //        table.AddCell(cell431);

                //    }
                //    addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //    addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //    pdfDoc.Add(table);
                //}


                if (cartonInfo.KitBoxList.Count > 0)
                {
                    table = new PdfPTable(1);
                    table.TotalWidth = 570f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 570f };
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    addCellwithborderNormalHeightSize(table, "Phone " + cartonInfo.KitBoxList[0].OriginCountry + ", Accessories " + cartonInfo.KitBoxList[0].OriginCountry, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 30, 1, 5f, 10, 11f);
                    addCellwithborderNew(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);


                }

                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string contentsInclude = "";

                //System.IO stringb  contentsInclude = "";
                foreach (var item in cartonInfo.KitBoxList)
                {
                    //table = new PdfPTable(2);
                    //table.TotalWidth = 500f;
                    //table.LockedWidth = true;
                    //table.DefaultCell.Border = Rectangle.NO_BORDER;
                    //widths = new float[] { 250f, 250f };
                    //table.HorizontalAlignment = Element.ALIGN_LEFT;
                    //table.SetWidths(widths);

                    //sb.Append(item.DisplayName + ",");

                    if (string.IsNullOrEmpty(contentsInclude))
                        contentsInclude = item.DisplayName;
                    else
                        contentsInclude = contentsInclude + "," + item.DisplayName;
                }

                //string contentsInclude = sb.ToString();
                //if (contentsInclude.Length > 0)
                //    contentsInclude = contentsInclude.Substring(0, contentsInclude.Length-1);

                table = new PdfPTable(1);

                table.TotalWidth = 524f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 500f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                addCellwithPaddingTopPaddingLeft(table, "Contents include: " + contentsInclude, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 0, 11f);



                pdfDoc.Add(table);



                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 2, 0, 0, 0, 2);
                //pdfDoc.Add(table);

            }

            // Close PDF document and send

            pdfWriter.CloseStream = false;
            pdfDoc.Close();

            memoryStream.Position = 0;
            return memoryStream;
        }

        public MemoryStream BOXLabelPdfTarCode(IList<CartonBoxID> boxIDs)
        {
            int bottomMargin = 0;
            int topMargin = 10;
            float LABELHEIGHTSIZE = 180;
            const int pageMargin = 3;
            const int pageRows = 1;
            const int pageCols = 1;

            var memoryStream = new MemoryStream();
            // ContainerModel ContainerInfo = new ContainerModel();

            Document pdfDoc = new Document();
            iTextSharp.text.Rectangle envelope = new iTextSharp.text.Rectangle(500, LABELHEIGHTSIZE);
            pdfDoc.SetPageSize(envelope);
            pdfDoc.SetMargins(pageMargin, pageMargin, 0, 0);

            PdfPTable table;
            PdfPCell cell = null;
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);
            //open the stream 
            pdfDoc.Open();
            string ContainerID = string.Empty;
            foreach (var cartonInfo in boxIDs)
            {
                pdfDoc.NewPage();

                PdfPTable tablespace = new PdfPTable(1);
                tablespace.TotalWidth = 500f;
                tablespace.LockedWidth = true;
                tablespace.SetWidths(new float[] { 0.2f });

                if (!string.IsNullOrEmpty(cartonInfo.BoxID))
                {
                    //IMEIImage = GenerateBarCode(cartonInfo.ESN);
                    IMEIImage = GenerateBarCodeTarCode(cartonInfo.BoxID, 6.5f, 1.359f, 4f, 3f);

                }
                
                if (!string.IsNullOrEmpty(cartonInfo.SKU))
                {
                    SKUCodeimage = GenerateBarCodeTarCode(cartonInfo.SKU, 13f, 1.359f, 3f, 2f);
                    //SKUCodeimage = GenerateMasterBarCode(cartonInfo.SKU, 30, 220);
                }


                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));


                PdfPCell cell1 = null;
                cell1 = new PdfPCell();

                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 3, 0, 0, 0, 3);
                pdfDoc.Add(table);

                //table = new PdfPTable(1);
                //table.TotalWidth = 500f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 3, 0, 0, 0, 3);
                //pdfDoc.Add(table);

                //table = new PdfPTable(1);
                //table.TotalWidth = 500f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 3, 0, 0, 0, 3);
                //pdfDoc.Add(table);


                //
                float[] widths = new float[] { 250f, 250f };

                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell11 = null;
                cell11 = new PdfPCell();

                if (SKUCodeimage != null)
                {
                    cell11 = ImageCellNew22(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f, 28f); // ImageCellNew110(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f); // ImageCellNew2(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 0f);
                    //cell11 = ImageCellNew22(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f, 28f); // ImageCellNew110(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f); // ImageCellNew2(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 0f);
                    cell11.BorderWidthRight = 0.5f;
                    cell11.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell11);
                }
                addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 10f);

                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                addCellwithPaddingLeftBottomTopRightSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 15f, 2f, 1f, 1f, 16f, 22f);
                PdfPCell cell101 = null;
                cell101 = new PdfPCell();

                if (IMEIImage != null)
                {
                    cell101 = ImageCellNew22(IMEIImage, 120f, PdfPCell.ALIGN_LEFT, 50f, 28f); // ImageCellNew110(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 1f); // ImageCellNew2(SKUCodeimage, 220f, PdfPCell.ALIGN_LEFT, 0f);
                    cell101.BorderWidthRight = 0.5f;
                    cell101.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell101);
                }
                else
                    addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 10f);

                //addCellwithPaddingLeftPaddingBottomSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 16f);

                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 500f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                addCellwithPaddingLeftBottomTopRightSize(table, cartonInfo.BoxID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 0f, 1f, 80f, 80f);

                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 3, 0, 0, 0, 3);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 3, 0, 0, 0, 3);
                pdfDoc.Add(table);

                


                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 250f, 250f };

                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                //"Apple_A1863_UL_64gb_Grey"
                addCellwithPaddingLeftBottomTopRightSizeBolD(table, cartonInfo.BoxDesc,  0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 1f, 1f, 1f, 17f, 48f);
                addCellwithPaddingLeftBottomTopRightSizeBolD(table, "PO #: " + cartonInfo.FulfillmentNumber, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 40f, 1f, 1f, 1f, 18f, 48f);
                //addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                
                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 2, 0, 0, 0, 2);
                pdfDoc.Add(table);

            }

            // Close PDF document and send

            pdfWriter.CloseStream = false;
            pdfDoc.Close();

            memoryStream.Position = 0;
            return memoryStream;
        }

        public MemoryStream POSKITLabelPdfNew(IList<PosKitInfo> posKitS)
        {
            int bottomMargin = 0;
            int topMargin = 10;
            float LABELHEIGHTSIZE = 280;
            const int pageMargin = 3;
            const int pageRows = 1;
            const int pageCols = 1;

            var memoryStream = new MemoryStream();
            // ContainerModel ContainerInfo = new ContainerModel();

            Document pdfDoc = new Document();
            iTextSharp.text.Rectangle envelope = new iTextSharp.text.Rectangle(524, LABELHEIGHTSIZE);
            pdfDoc.SetPageSize(envelope);
            pdfDoc.SetMargins(pageMargin, pageMargin, 0, 0);

            PdfPTable table;
            PdfPCell cell = null;
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);
            //open the stream 
            pdfDoc.Open();
            string ContainerID = string.Empty;
            foreach (var cartonInfo in posKitS)
            {
                pdfDoc.NewPage();

                PdfPTable tablespace = new PdfPTable(1);
                tablespace.TotalWidth = 524f;
                tablespace.LockedWidth = true;
                tablespace.SetWidths(new float[] { 0.2f });

                if (!string.IsNullOrEmpty(cartonInfo.UPC))
                {
                    UPCImage = GenerateBarCodeUPCATarCode(cartonInfo.UPC, 150f, 58f);
                }
                if (!string.IsNullOrEmpty(cartonInfo.ESN))
                {
                    IMEIImage = GenerateBarCode(cartonInfo.ESN);
                }
                if (!string.IsNullOrEmpty(cartonInfo.MEID))
                {
                    MEIDImage = GenerateBarCode(cartonInfo.MEID);
                }
                if (!string.IsNullOrEmpty(cartonInfo.ICCID))
                {
                    ICCIDImage = GenerateBarCode(cartonInfo.ICCID);
                }
                if (!string.IsNullOrEmpty(cartonInfo.SerialNum))
                {
                    SerialNumImage = GenerateMasterBarCode(cartonInfo.SerialNum, 30, 160);
                }
                if (!string.IsNullOrEmpty(cartonInfo.HEX))
                {
                    HEXImage = GenerateBarCode(cartonInfo.HEX);
                }
                if (!string.IsNullOrEmpty(cartonInfo.SKU))
                {
                    SKUCodeimage = GenerateMasterBarCode(cartonInfo.SKU, 30, 220);
                }

                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));


                PdfPCell cell1 = null;
                cell1 = new PdfPCell();

                table = new PdfPTable(1);
                table.TotalWidth = 524f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 3, 0, 0, 0, 3);
                pdfDoc.Add(table);


                //
                float[] widths = new float[] { 170f, 174, 170f };

                table = new PdfPTable(3);
                table.TotalWidth = 524f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 170f, 174f, 170f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                //addCellwithPaddingLeftPaddingBottomSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 15f);
                addCellwithPaddingLeftBottomTopRightSize(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 1, 1, 1, 15f, 24f);
                addCellwithPaddingLeftPaddingBottomSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 14f);
                addCellwithPaddingLeftBottomTopRightSize(table, "Serial Number - Scan for Activation", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 5f, 0f, 1f, 1f, 10f, 24f);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 524f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 170f, 174f, 170f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell11 = null;
                cell11 = new PdfPCell();

                if (SKUCodeimage != null)
                {
                    cell11 = ImageCellNew2(SKUCodeimage, 160f, PdfPCell.ALIGN_LEFT, 10f);
                    cell11.BorderWidthRight = 0.5f;
                    cell11.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell11);
                }
               // addCellwithPaddingLeftSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                addCellwithPaddingLeftBottomTopRightSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 1f, 15f, 0f, 11f, 36f);

                //addCellwithPaddingLeftPaddingBottomSize(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 20f);
                PdfPCell cell103 = null;
                cell103 = new PdfPCell();

                if (SerialNumImage != null)
                {
                    cell103 = ImageCellNew02(SerialNumImage, 150f, PdfPCell.ALIGN_LEFT, 0f, 3f);
                    cell103.BorderWidthRight = 0.5f;
                    cell103.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell103);

                }
                //addCellwithPaddingLeft(table, cartonInfo.ItemName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 524f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 170f, 180f, 170f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell12 = null;
                cell12 = new PdfPCell();

                addCellwithPaddingLeftBottomTopRightSize(table, "HEX: " + cartonInfo.HEX, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 26f,1f, 10f, 36f);
                //addCellwithPaddingLeftSize(table, "HEX: " + cartonInfo.HEX, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);


                if (UPCImage != null)
                {
                    cell12 = ImageCellNew22(UPCImage, 150f, PdfPCell.ALIGN_LEFT, 15f, 54f);
                    cell12.BorderWidthRight = 0.5f;
                    cell12.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell12);
                }

                addCellwithPaddingLeftBottomTopRightSize(table, cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 5f, 1f, 1f, 1f, 12f, 36f);

                //addCellwithPaddingLeft(table, cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

                pdfDoc.Add(table);


                table = new PdfPTable(3);
                table.TotalWidth = 524f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 170f, 174f, 170f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell211 = null;
                cell211 = new PdfPCell();
                if (HEXImage != null)
                {

                    cell211 = ImageCellNewLeft(HEXImage, 180f, PdfPCell.ALIGN_LEFT, 27f, 10f, 0f);
                    cell211.BorderWidthRight = 0.5f;
                    cell211.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell211);

                }
                //addCellwithPaddingLeft(table, cartonInfo.UPC, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                addCellwithPaddingLeftSize(table, "" , 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 524f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 170f, 174f, 170f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

               // addCellwithPaddingLeftBottomTopRightSize(table, "HEX: " + cartonInfo.HEX, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 0f, 26f, 1f, 10f, 36f);

                addCellwithPaddingLeftBottomTopRightSize(table, "IMEI: " + cartonInfo.ESN, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 1f, 1f, 1f, 10f, 36f);
                addCellwithPaddingLeftSize(table, "SW Version: " + cartonInfo.SWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                addCellwithPaddingLeftSize(table, "" , 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 524f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 170f, 174f, 170f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell221 = null;
                cell221 = new PdfPCell();
                if (IMEIImage != null)
                {

                    cell221 = ImageCellNewLeft(IMEIImage, 180f, PdfPCell.ALIGN_LEFT, 27f, 10f, 3f);
                    cell221.BorderWidthRight = 0.5f;
                    cell221.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell221);

                }
                //addCellwithPaddingLeft(table, cartonInfo.UPC, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithPaddingLeftBottomTopRightSize(table, "HW Version: " + cartonInfo.HWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 20f, 1f, 10f, 1f, 10f, 36f);

               // addCellwithPaddingLeftSize(table, "HW Version: " + cartonInfo.HWVersion, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 524f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 170f, 174f, 170f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                addCellwithPaddingLeftBottomTopRightSize(table, "DEC: " + cartonInfo.MEID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f, 1f, 1f, 1f, 10f, 36f);
                //addCellwithPaddingLeftSize(table, "DEC: " + cartonInfo.MEID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                //addCellwithPaddingLeftSize(table, "DEC: " + cartonInfo.MEID, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                addCellwithPaddingLeftSize(table, "Date: " + cartonInfo.ShipDate, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                // addCellwithPaddingLeft(table, "Serial Num: " + cartonInfo.SerialNum, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 524f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 170f, 174f, 170f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                PdfPCell cell331 = null;
                cell331 = new PdfPCell();
                if (MEIDImage != null)
                {

                    cell331 = ImageCellNewLeft(MEIDImage, 180f, PdfPCell.ALIGN_LEFT, 27f, 10f, 3f);
                    cell331.BorderWidthRight = 0.5f;
                    cell331.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table.AddCell(cell331);

                }
                addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                pdfDoc.Add(table);

                if (ICCIDImage != null)
                {
                    table = new PdfPTable(3);
                    table.TotalWidth = 524f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 170f, 174f, 170f };
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    PdfPCell cell431 = null;
                    cell431 = new PdfPCell();
                    if (ICCIDImage != null)
                    {

                        cell431 = ImageCellNewLeft(ICCIDImage, 180f, PdfPCell.ALIGN_LEFT, 27f, 10f, 3f);
                        cell431.BorderWidthRight = 0.5f;
                        cell431.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell431);

                    }
                    addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                    addCellwithPaddingLeftSize(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 10f);
                    pdfDoc.Add(table);
                }


                if (cartonInfo.KitBoxList.Count > 0)
                {
                    table = new PdfPTable(1);
                    table.TotalWidth = 524f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 500f };
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    addCellwithborderNormalHeightSize(table, "Phone " + cartonInfo.KitBoxList[0].OriginCountry + ", Accessories " + cartonInfo.KitBoxList[0].OriginCountry , 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 30, 1, 0, 10, 10);
                    addCellwithborderNew(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);

                  
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //System.IO stringb  contentsInclude = "";
                foreach (var item in cartonInfo.KitBoxList)
                {
                    //table = new PdfPTable(2);
                    //table.TotalWidth = 500f;
                    //table.LockedWidth = true;
                    //table.DefaultCell.Border = Rectangle.NO_BORDER;
                    //widths = new float[] { 250f, 250f };
                    //table.HorizontalAlignment = Element.ALIGN_LEFT;
                    //table.SetWidths(widths);

                    sb.Append(item.DisplayName + ",");

                   
                }

                string contentsInclude = sb.ToString();
                if (contentsInclude.Length > 0)
                    contentsInclude = contentsInclude.Substring(0, contentsInclude.Length-1);

                table = new PdfPTable(1);

                table.TotalWidth = 524f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 500f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                addCellwithPaddingTopPaddingLeft(table, "Contents include: " + contentsInclude, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 0, 11f);



                pdfDoc.Add(table);

              
              
                table = new PdfPTable(1);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormalHeightSize(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 2, 0, 0, 0, 2);
                pdfDoc.Add(table);

            }

            // Close PDF document and send

            pdfWriter.CloseStream = false;
            pdfDoc.Close();

            memoryStream.Position = 0;
            return memoryStream;
        }

        public MemoryStream PalletCartonMappingLabelPdf(IList<PalletCartonMap> cartons)
        {
            var memoryStream = new MemoryStream();
            // ContainerModel ContainerInfo = new ContainerModel();

            Document pdfDoc = new Document(PageSize.A4, 25f, 25f, 20f, 20f);

            PdfPTable table;
            PdfPCell cell = null;

            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);



            //open the stream 
            pdfDoc.Open();
            string ContainerID = string.Empty;
            string PalletID = string.Empty;
            foreach (var cartonInfo in cartons)
            {
                pdfDoc.NewPage();

                PdfPTable tablespace = new PdfPTable(1);
                tablespace.TotalWidth = 575f;
                tablespace.LockedWidth = true;
                tablespace.SetWidths(new float[] { 0.2f });


                PalletID = cartonInfo.PalletID.Replace("(", "").Replace(")", "");

                if (!string.IsNullOrEmpty(PalletID))
                {
                    PalletCodeimage = GenerateMasterBarCode(PalletID, 20, 350);
                }
                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));


                PdfPCell cell1 = null;
                cell1 = new PdfPCell();

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 250f });
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell2 = null;
                cell2 = new PdfPCell();

                if (PalletCodeimage != null)
                {

                    cell2 = ImageCellNew11(PalletCodeimage, 220f, PdfPCell.ALIGN_LEFT);
                    cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell2);

                }
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                string PalletID2 = cartonInfo.PalletID.Insert(4, " ");
                addCellwithborderNormalCenter(table, "Pallet: " + PalletID2, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);


                //table = new PdfPTable(1);
                //table.TotalWidth = 575f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //table.HorizontalAlignment = Element.ALIGN_CENTER;

                //addCellwithborderNormalCenter(table, "SKU: " + cartonInfo.SKU, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderBold(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1f, 1f, 12, 18f);

                pdfDoc.Add(table);

                


                //
                float[] widths = new float[] { 350f, 150f };           


                

                PdfPCell cell3 = null;
                PdfPCell cell13 = null;
                string Column1, Column2; 

                foreach (var item in cartonInfo.Cartons)
                {
                    if (!string.IsNullOrEmpty(item.Column1))
                    {
                        IMEIImage = GenerateBarCode(item.Column1);
                    }
                    if (!string.IsNullOrEmpty(item.Column2))
                    {
                        MEIDImage = GenerateBarCode(item.Column2);
                    }

                    table = new PdfPTable(2);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 250f, 250f };
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    Column1 = item.Column1.Insert(4, " ");
                    Column2 = item.Column2.Insert(4, " ");
                    addCellwithPaddingLeftPaddingBottomSize(table, "Carton: " + Column1 +"      " + item.BOXColumn1, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 10);
                    addCellwithPaddingLeftPaddingBottomSize(table, "Carton: " + Column2 + "      " + item.BOXColumn2, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK, 1, 10);

                    pdfDoc.Add(table);


                    table = new PdfPTable(2);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 250f, 250f };
                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.SetWidths(widths);
                    //PdfPCell cell11 = null;
                    cell3 = new PdfPCell();

                    if (IMEIImage != null)
                    {

                        cell3 = ImageCellNewLeft(IMEIImage, 220f, PdfPCell.ALIGN_LEFT);
                        cell3.BorderWidthRight = 0.5f;
                        cell3.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell3);

                    }

                    cell13 = new PdfPCell();

                    if (MEIDImage != null)
                    {

                        cell13 = ImageCellNewLeft(MEIDImage, 220f, PdfPCell.ALIGN_LEFT);
                        cell13.BorderWidthRight = 0.5f;
                        cell13.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell13);

                    }
                    

                    pdfDoc.Add(table);

                }




                table = new PdfPTable(1);
                table.TotalWidth = 575f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);



               
            }

            // Close PDF document and send

            pdfWriter.CloseStream = false;
            pdfDoc.Close();

            memoryStream.Position = 0;
            return memoryStream;
        }

        public MemoryStream ExportToPDF(IList<ContainerModel> models)
        {
            if (models != null && models.Count > 0)
                return ContainerSlipPdf(models);
            else
                return null;
        }
        public MemoryStream ContainerSlipPdf(IList<ContainerModel> containers)
        {
            var memoryStream = new MemoryStream();
            // ContainerModel ContainerInfo = new ContainerModel();



            Document pdfDoc = new Document(PageSize.A4.Rotate(), 15f, 10f, 20f, 20f);

            PdfPTable table;
            PdfPCell cell = null;

            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);


            //open the stream 
            pdfDoc.Open();

            foreach (var ContainerInfo in containers)
            {
                pdfDoc.NewPage();

                PdfPTable tablespace = new PdfPTable(1);
                tablespace.TotalWidth = 320f;
                tablespace.LockedWidth = true;
                tablespace.SetWidths(new float[] { 0.2f });
                if (!string.IsNullOrEmpty(ContainerInfo.PostalCode))
                {
                    PostalCodeimage = GenerateBarCode(ContainerInfo.PostalCode);
                }

                if (!string.IsNullOrEmpty(ContainerInfo.ContainerNumber))
                {
                    SSCCImage = GenerateSSCCBarCode(ContainerInfo.ContainerNumber);
                }

                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));


                PdfPCell cell1 = null;
                cell1 = new PdfPCell();


                table = new PdfPTable(2);

                table.TotalWidth = 320f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                float[] widths = new float[] { 150f, 150f };
                table.SetWidths(widths);
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                addCellwithborderNew(table, "FROM: ", 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNew(table, "TO: ", 0.5f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 320f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 150f, 150f };
                table.SetWidths(widths);
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                addCellwithborderNormal1(table, ContainerInfo.CompanyName, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal1(table, ContainerInfo.CustomerName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 320f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                widths = new float[] { 150f, 150f };
                table.SetWidths(widths);
                if (string.IsNullOrEmpty(ContainerInfo.AddressLine2))
                    addCellwithborderNormal1(table, ContainerInfo.AddressLine1, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                else
                    addCellwithborderNormal1(table, ContainerInfo.AddressLine1 + ", " + ContainerInfo.AddressLine2, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);


                if (string.IsNullOrEmpty(ContainerInfo.ShippingAddressLine2))
                    addCellwithborderNormal1(table, ContainerInfo.ShippingAddressLine1, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                else
                    addCellwithborderNormal1(table, ContainerInfo.ShippingAddressLine1 + ", " + ContainerInfo.ShippingAddressLine2, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);

                pdfDoc.Add(table);




                table = new PdfPTable(2);
                table.TotalWidth = 320f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 150f, 150f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);

                addCellwithborderNormal1(table, ContainerInfo.City + " " + ContainerInfo.State + " " + ContainerInfo.ZipCode, 0f, 0f, 0.5f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal1(table, ContainerInfo.ShippingCity + " " + ContainerInfo.ShippingState + " " + ContainerInfo.ShippingZipCode, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 320f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 150f, 150f };
                table.SetWidths(widths);
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                addCellwithborderNormal1(table, ContainerInfo.Country, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal1(table, ContainerInfo.ShippingCountry, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                table = new PdfPTable(2);
                table.TotalWidth = 320f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 150f, 150f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                addCellwithborderNormal1(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNormal1(table, "\n", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);


                table = new PdfPTable(2);
                table.TotalWidth = 320f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 150f, 150f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                addCellwithborderNew(table, "SHIP TO POSTAL CODE: ", 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNew(table, "CARRIER: ", 0.5f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 320f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 150f, 150f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                addCellwithborderNormalCenter(table, ContainerInfo.PostalCode, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                addCellwithborderNew(table, "", 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 320f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                widths = new float[] { 150f, 150f };
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(widths);
                if (PostalCodeimage != null)
                {

                    cell1 = ImageCellNew1(PostalCodeimage, 120f, PdfPCell.ALIGN_LEFT);
                    cell1.BorderWidthRight = 0.5f;
                    cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell1);

                }

                addCellwithborderNormalCenter(table, ContainerInfo.Carrier, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 320f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                addCellwithborderNormal(table, "PO#: " + ContainerInfo.PoNumber, 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 320f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                addCellwithborderNormal(table, "DPCI: " + ContainerInfo.DPCI, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 320f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                addCellwithborderNormal(table, "Casepack: " + ContainerInfo.Casepack, 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 320f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                table = new PdfPTable(1);
                table.TotalWidth = 320f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 320f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                addCellwithborderNew(table, "SSCC-18", 0.5f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);
                //table = new PdfPTable(1);
                //table.TotalWidth = 300f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);
                ////table = new PdfPTable(1);
                //table.TotalWidth = 300f;
                //table.LockedWidth = true;
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                //addCellwithborderNormal(table, "\n", 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                //pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 320f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_LEFT;

                addCellwithborderNormalCenter(table, ContainerInfo.ContainerNumber, 0f, 0f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);

                table = new PdfPTable(1);
                table.TotalWidth = 320f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 150f });
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                PdfPCell cell2 = null;
                cell2 = new PdfPCell();

                if (SSCCImage != null)
                {

                    cell2 = ImageCellNew11(SSCCImage, 220f, PdfPCell.ALIGN_LEFT);
                    cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell2);

                }
                pdfDoc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 320f;
                table.LockedWidth = true;
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                addCellwithborderNormal(table, "\n", 0f, 0.5f, 0.5f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                pdfDoc.Add(table);


            }

            // Close PDF document and send

            pdfWriter.CloseStream = false;
            pdfDoc.Close();

            memoryStream.Position = 0;
            return memoryStream;
        }

        private iTextSharp.text.Image GenerateBarCodeUPCA(string code, float BARCODEWIDTH, float BARCODEHEIGHT)
        {
            iTextSharp.text.Image image = null;
            if (!string.IsNullOrWhiteSpace(code))
            {
                BarcodeLib.Barcode b = new BarcodeLib.Barcode();
                b.IncludeLabel = true;
               // b.StandardizeLabel = false;
             //   b.Alignment = BarcodeLib.AlignmentPositions
            //    b.HoritontalResolution = "";
                //b.Height = 38;
                //b.Height = 250;
                System.Drawing.Image img = b.Encode(BarcodeLib.TYPE.UPCA, code, 490, 150);

                //BarcodeLib.Barcode.Linear upca = new BarcodeLib.Barcode.Linear();
                //upca.Type = BarcodeLib.Barcode.BarcodeType.UPCA;
                //upca.Data = code;

                //upca.BarColor = System.Drawing.Color.Black;
                //upca.BarWidth = 1;
                //upca.LeftMargin = 8;
                //upca.RightMargin = 8;
                //upca.BarHeight = 32;
                //// upca.BarWidth = 150;

                //// More UPC-A barcode settings here, like image format, font, data text style etc.
                //System.Drawing.Image img = upca.drawBarcode();// (System.Drawing.Color.Black, System.Drawing.Color.White);

                if (img != null)
                {
                    image = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //image = iTextSharp.text.Image.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);

                    image.ScaleToFit(BARCODEWIDTH, BARCODEHEIGHT);
                    image.Alignment = Element.ALIGN_LEFT;
                }
            }

            return image;
        }

        //private iTextSharp.text.Image GenerateBarCodeUPCANew(string code, float BARCODEWIDTH, float BARCODEHEIGHT)
        //{
        //    iTextSharp.text.Image image = null;
        //    if (!string.IsNullOrWhiteSpace(code))
        //    {
        //        cUPCA upca = new cUPCA();

        //        string newCode = code.Substring(0, 11) + upca.GetCheckSum(code).ToString();
        //        System.Drawing.Image img;
        //        img = upca.CreateBarCode(newCode, 1);


        //        //BarcodeLib.Barcode.Linear upca = new BarcodeLib.Barcode.Linear();
        //        //upca.Type = BarcodeLib.Barcode.BarcodeType.UPCA;
        //        //upca.Data = code;

        //        //upca.BarColor = System.Drawing.Color.Black;
        //        //upca.BarWidth = 1;
        //        //upca.LeftMargin = 8;
        //        //upca.RightMargin = 8;
        //        //upca.BarHeight = 32;
        //        //// upca.BarWidth = 150;

        //        //// More UPC-A barcode settings here, like image format, font, data text style etc.
        //        //System.Drawing.Image img = upca.drawBarcode();// (System.Drawing.Color.Black, System.Drawing.Color.White);

        //        if (img != null)
        //        {
        //            image = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);
        //            //image = iTextSharp.text.Image.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);

        //            image.ScaleToFit(BARCODEWIDTH, BARCODEHEIGHT);
        //            image.Alignment = Element.ALIGN_LEFT;
        //        }
        //    }

        //    return image;
        //}

        private iTextSharp.text.Image GenerateBarCodeMasterUPCATarCode(string code, float BARCODEWIDTH, float BARCODEHEIGHT)
        {
            iTextSharp.text.Image image = null;
            if (!string.IsNullOrWhiteSpace(code))
            {
                ///new code
                ///

                Linear upca2 = new Linear();
                upca2.BarcodeType = LinearBarcode.UPCA;

                upca2.Auto_Resize = true;
                upca2.Bar_Alignment = AlignmentHori.Left;
                upca2.Resolution = 300;
                upca2.UOM = UnitOfMeasure.Inch;
                upca2.Barcode_Width = 6.2f;
                upca2.Barcode_Height = 2f;
                upca2.Width_X = 1.6f;
                upca2.Height_Y = 16;
                //upca2.Barcode_Width = 7.5f;
                //upca2.Barcode_Height = 2f;
                //upca2.Width_X = 1.6f;
                //upca2.Height_Y = 16;

                upca2.Left_Margin = 0;
                //upca.Left_Margin = 0;
                upca2.Right_Margin = 0;
                upca2.Top_Margin = 0;
                upca2.Bottom_Margin = 0;
                //upca2.Barcode_Width = 270f;
                // upca2.Width_X = 170f;
                //upca2.Height_Y = 60;

                upca2.Back_Color = System.Drawing.Color.White;

                upca2.Fore_Color = System.Drawing.Color.Black;
                //upca2.Display_Text = true;
                upca2.Text_Color = System.Drawing.Color.Black;

                upca2.Text_Font = new System.Drawing.Font("Arial", 22f, System.Drawing.FontStyle.Bold);
                upca2.Text_Margin = 0;
                upca2.Valid_Data = code.Substring(0, 11);
                System.Drawing.Image img = upca2.draw();
                //   upca2.drawToFile("c://upc/upcanew.png");

                if (img != null)
                {
                    image = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //image = iTextSharp.text.Image.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);

                    image.ScaleToFit(BARCODEWIDTH, BARCODEHEIGHT);
                    image.Alignment = Element.ALIGN_LEFT;
                }
            }

            return image;
        }

        private iTextSharp.text.Image GenerateBarCodeUPCATarCode(string code, float BARCODEWIDTH, float BARCODEHEIGHT)
        {
            iTextSharp.text.Image image = null;
            if (!string.IsNullOrWhiteSpace(code))
            {
                ///new code
                ///

                Linear upca2 = new Linear();
                upca2.BarcodeType = LinearBarcode.UPCA;

                upca2.Auto_Resize = true;
                upca2.Bar_Alignment = AlignmentHori.Left;
                upca2.Resolution = 100;
                upca2.UOM = UnitOfMeasure.Inch;
                upca2.Barcode_Width = 7.5f;
                upca2.Barcode_Height = 2f;
                upca2.Width_X = 1.6f;
                upca2.Height_Y = 16;

                //upca.Resolution = 300;
                //upca2.Barcode_Height = 100;
                //upca2.Barcode_Width = 00;
                //upca2.Width_X = 6;
                //upca2.Height_Y = 300;
                //upca2.Text_Font = new System.Drawing.Font("Arial", 30, System.Drawing.FontStyle.Regular);
                //upca2.Text_Margin = 0.5f;


                ////upca2.Left_Margin = 0;
                //////upca.Left_Margin = 0;
                ////upca2.Right_Margin = 0;
                ////upca2.Top_Margin = 0;
                ////upca2.Bottom_Margin = 0;
                ////upca2.Barcode_Width = 270f;
                //// upca2.Width_X = 170f;
                ////upca2.Height_Y = 60;

                upca2.Back_Color = System.Drawing.Color.White;

                upca2.Fore_Color = System.Drawing.Color.Black;
                ///upca2.Display_Text = true;
                upca2.Text_Color = System.Drawing.Color.Black;

                upca2.Text_Font = new System.Drawing.Font("Arial", 22f, System.Drawing.FontStyle.Bold);
                upca2.Text_Margin = 0;
                upca2.Valid_Data = code.Substring(0, 11);
               System.Drawing.Image img = upca2.draw();

                //   upca2.drawToFile("c://upc/upcanew.png");

                //Linear linear = new Linear();
                //linear.BarcodeType = LinearBarcode.UPCA;
                //linear.Valid_Data = code; // Increase Barcode Image's Resolution from default 96 dpi to 600 dpi
                //                          //linear.Resolution = 300;
                //                          // linear.UOM = UnitOfMeasure.Pixel;   // Use more pixels for Barcode Modules to reducing the distortion effect during printing.
                //                          // 10 pixels with 600 dpi is around 1.6 pixels in 96 dpi.  //linear.Barcode_Width = 2;
                //linear.Resolution = 300;
                //linear.UOM = UnitOfMeasure.Pixel;   // Use more pixels for Barcode Modules to reducing the distortion effect during printing.
                //                                    // linear.Barcode_Width = 300;
                //                                    // linear.Barcode_Height = 100;                                // 10 pixels with 600 dpi is around 1.6 pixels in 96 dpi.  //linear.Barcode_Width = 2;
                //linear.Width_X = 9;
                //linear.Barcode_Height = 200;

                ////linear.Width_X = 30;
                //linear.Height_Y = 350;
                //linear.Auto_Resize = false;
                //linear.Left_Margin = 0;
                //linear.Right_Margin = 0;
                //linear.Top_Margin = 0;
                //linear.Bottom_Margin = 0;
                //linear.Text_Margin = 5.5f;
                //linear.Text_Font = new System.Drawing.Font("Arial", 65, System.Drawing.FontStyle.Regular);

                //linear.Valid_Data = code.Substring(0, 11);
                //System.Drawing.Image img = linear.draw();


                if (img != null)
                {
                    image = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //image = iTextSharp.text.Image.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);
                    image.ScaleToFit(BARCODEWIDTH, BARCODEHEIGHT);
                    image.Alignment = Element.ALIGN_LEFT;
                }
            }

            return image;
        }
        private iTextSharp.text.Image GenerateBarSKUCodeTarCode(string code, float Barcode_Width, float Barcode_Height, float Width_X, float Height_Y)
        {
            iTextSharp.text.Image image = null;
            if (!string.IsNullOrWhiteSpace(code))
            {
                Linear code128 = new Linear();
                code128.BarcodeType = LinearBarcode.Code128;
                code128.Auto_Resize = true;
                code128.Bar_Alignment = AlignmentHori.Left;
                code128.Resolution = 150;
                code128.UOM = UnitOfMeasure.Inch;
                code128.Barcode_Width = Barcode_Width;
                code128.Barcode_Height = Barcode_Height;
                code128.Width_X = Width_X;
                code128.Height_Y = Height_Y;

                code128.Left_Margin = 0;
                code128.Right_Margin = 0;
                code128.Top_Margin = 0;
                code128.Bottom_Margin = 0;

                code128.Back_Color = System.Drawing.Color.White;

                code128.Fore_Color = System.Drawing.Color.Black;
                code128.Display_Text = false;
                code128.Text_Color = System.Drawing.Color.Black;

                code128.Text_Font = new System.Drawing.Font("Arial", 18f, System.Drawing.FontStyle.Regular);
                //upca2.Text_Margin = 0.01f;
                code128.Valid_Data = code;
                System.Drawing.Image img = code128.draw();



                //Barcode128 code39 = new Barcode128();
                //code39.Code = code;
                ////code39.X = 2;
                //code39.CodeType = Barcode.CODE128;
                //code39.TextAlignment = 1;
                //code39.StartStopText = true;
                //code39.GenerateChecksum = true;
                //code39.Extended = true;
                //code39.BarHeight = BARCODE_HEIGHT;

                //code39.TextAlignment = Element.ALIGN_CENTER;
                //System.Drawing.Image img = code39.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                ////System.Drawing.Image img = code39.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);

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
        private iTextSharp.text.Image GenerateBarCodeTarCode(string code, float Barcode_Width, float Barcode_Height, float Width_X, float Height_Y)
        {
            iTextSharp.text.Image image = null;
            if (!string.IsNullOrWhiteSpace(code))
            {
                Linear code128 = new Linear();
                code128.BarcodeType = LinearBarcode.Code128;
                code128.Auto_Resize = true;
                code128.Bar_Alignment = AlignmentHori.Left;
                code128.Resolution = 100;
                code128.UOM = UnitOfMeasure.Inch;
                code128.Barcode_Width = Barcode_Width;
                code128.Barcode_Height = Barcode_Height;
                code128.Width_X = Width_X;
                code128.Height_Y = Height_Y;

                code128.Left_Margin = 0;
                code128.Right_Margin = 0;
                code128.Top_Margin = 0;
                code128.Bottom_Margin = 0;

                code128.Back_Color = System.Drawing.Color.White;

                code128.Fore_Color = System.Drawing.Color.Black;
                code128.Display_Text = false;
                code128.Text_Color = System.Drawing.Color.Black;

                code128.Text_Font = new System.Drawing.Font("Arial", 18f, System.Drawing.FontStyle.Regular);
                //upca2.Text_Margin = 0.01f;
                code128.Valid_Data = code;
                System.Drawing.Image img = code128.draw();

                if (img != null)
                {
                    image = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);
                    
                    image.ScaleToFit(BARCODE_WIDTH, BARCODE_HEIGHT);
                    image.Alignment = Element.ALIGN_LEFT;
                }
            }

            return image;
        }

        private iTextSharp.text.Image GenerateBarCodeTarCodeOld(string code, float Barcode_Width, float Barcode_Height, float Width_X, float Height_Y)
        {
            iTextSharp.text.Image image = null;
            if (!string.IsNullOrWhiteSpace(code))
            {
                Linear code128 = new Linear();
                code128.BarcodeType = LinearBarcode.Code128;
                code128.Auto_Resize = true;
                code128.Bar_Alignment = AlignmentHori.Left;
                code128.Resolution = 100;
                code128.UOM = UnitOfMeasure.Inch;
                code128.Barcode_Width = Barcode_Width;
                code128.Barcode_Height = Barcode_Height;
                code128.Width_X = Width_X;
                code128.Height_Y = Height_Y;

                code128.Left_Margin = 0;
                code128.Right_Margin = 0;
                code128.Top_Margin = 0;
                code128.Bottom_Margin = 0;
                
                code128.Back_Color = System.Drawing.Color.White;

                code128.Fore_Color = System.Drawing.Color.Black;
                code128.Display_Text = false;
                code128.Text_Color = System.Drawing.Color.Black;

                code128.Text_Font = new System.Drawing.Font("Arial", 18f, System.Drawing.FontStyle.Regular);
                //upca2.Text_Margin = 0.01f;
                code128.Valid_Data = code;
                System.Drawing.Image img = code128.draw();



                //Barcode128 code39 = new Barcode128();
                //code39.Code = code;
                ////code39.X = 2;
                //code39.CodeType = Barcode.CODE128;
                //code39.TextAlignment = 1;
                //code39.StartStopText = true;
                //code39.GenerateChecksum = true;
                //code39.Extended = true;
                //code39.BarHeight = BARCODE_HEIGHT;

                //code39.TextAlignment = Element.ALIGN_CENTER;
                //System.Drawing.Image img = code39.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                ////System.Drawing.Image img = code39.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);

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
        private iTextSharp.text.Image GenerateBarCodeTarCodeCARTON(string code, float Barcode_Width, float Barcode_Height, float Width_X, float Height_Y)
        {
            iTextSharp.text.Image image = null;
            if (!string.IsNullOrWhiteSpace(code))
            {
                Linear code128 = new Linear();
                code128.BarcodeType = LinearBarcode.Code128;
                code128.Auto_Resize = true;
                code128.Bar_Alignment = AlignmentHori.Left;
                code128.Resolution = 300;
                code128.UOM = UnitOfMeasure.Inch;
                code128.Barcode_Width = Barcode_Width;
                code128.Barcode_Height = Barcode_Height;
                code128.Width_X = Width_X;
                code128.Height_Y = Height_Y;

                code128.Left_Margin = 0;
                code128.Right_Margin = 0;
                code128.Top_Margin = 0;
                code128.Bottom_Margin = 0;

                code128.Back_Color = System.Drawing.Color.White;

                code128.Fore_Color = System.Drawing.Color.Black;
                code128.Display_Text = false;
                code128.Text_Color = System.Drawing.Color.Black;

                code128.Text_Font = new System.Drawing.Font("Arial", 22f, System.Drawing.FontStyle.Regular);
                //upca2.Text_Margin = 0.01f;
                code128.Valid_Data = code;
                System.Drawing.Image img = code128.draw();



                //Barcode128 code39 = new Barcode128();
                //code39.Code = code;
                ////code39.X = 2;
                //code39.CodeType = Barcode.CODE128;
                //code39.TextAlignment = 1;
                //code39.StartStopText = true;
                //code39.GenerateChecksum = true;
                //code39.Extended = true;
                //code39.BarHeight = BARCODE_HEIGHT;

                //code39.TextAlignment = Element.ALIGN_CENTER;
                //System.Drawing.Image img = code39.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                ////System.Drawing.Image img = code39.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);

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

        private iTextSharp.text.Image GenerateBarCode_TarCode(string code, float Barcode_Width, float Barcode_Height, float Width_X, float Height_Y)
        {
            iTextSharp.text.Image image = null;
            if (!string.IsNullOrWhiteSpace(code))
            {
                Linear code128 = new Linear();
                code128.BarcodeType = LinearBarcode.Code128;
                code128.Auto_Resize = false;
                code128.Bar_Alignment = AlignmentHori.Left;
                code128.Resolution = 300;
                code128.UOM = UnitOfMeasure.Inch;
                code128.Barcode_Width = Barcode_Width;
                code128.Barcode_Height = Barcode_Height;
                code128.Width_X = Width_X;
                code128.Height_Y = Height_Y;

                code128.Left_Margin = 0;
                code128.Right_Margin = 0;
                code128.Top_Margin = 0;
                code128.Bottom_Margin = 0;

                code128.Back_Color = System.Drawing.Color.White;

                code128.Fore_Color = System.Drawing.Color.Black;
                code128.Display_Text = false;
                code128.Text_Color = System.Drawing.Color.Black;

                code128.Text_Font = new System.Drawing.Font("Arial", 18f, System.Drawing.FontStyle.Regular);
                //upca2.Text_Margin = 0.01f;
                code128.Valid_Data = code;
                System.Drawing.Image img = code128.draw();



                //Barcode128 code39 = new Barcode128();
                //code39.Code = code;
                ////code39.X = 2;
                //code39.CodeType = Barcode.CODE128;
                //code39.TextAlignment = 1;
                //code39.StartStopText = true;
                //code39.GenerateChecksum = true;
                //code39.Extended = true;
                //code39.BarHeight = BARCODE_HEIGHT;

                //code39.TextAlignment = Element.ALIGN_CENTER;
                //System.Drawing.Image img = code39.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                ////System.Drawing.Image img = code39.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);

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

        private iTextSharp.text.Image GenerateBarCodeNEW(string code, float fontsize)
        {
            Font font = new Font(FontFactory.GetFont("Arial", fontsize, Font.NORMAL));

            iTextSharp.text.Image image = null;
            if (!string.IsNullOrWhiteSpace(code))
            {
                Barcode128 code128 = new Barcode128();
                code128.Code = code;
                //code39.X = 2;
                code128.CodeType = Barcode.CODE128;
                code128.TextAlignment = 1;
                code128.StartStopText = true;
                code128.GenerateChecksum = true;
                code128.Extended = true;
                code128.BarHeight = BARCODE_HEIGHT;
                code128.Size = 48f;
                //code128.Font = font;
                code128.TextAlignment = Element.ALIGN_CENTER;
                System.Drawing.Image img = code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
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

        private iTextSharp.text.Image GenerateBarCode(string code)
        {
            //Font = new Font(Font.FontFamily)
            iTextSharp.text.Image image = null;
            if (!string.IsNullOrWhiteSpace(code))
            {
                Barcode128 code128 = new Barcode128();
                code128.Code = code;
                //code39.X = 2;
                code128.CodeType = Barcode.CODE128;
                code128.TextAlignment = 1;
                code128.StartStopText = true;
                code128.GenerateChecksum = true;
                code128.Extended = true;
                code128.BarHeight = BARCODE_HEIGHT;
                //code128.Size = 48f;
                //code128.Font 
                code128.TextAlignment = Element.ALIGN_CENTER;
                System.Drawing.Image img = code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
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
        private iTextSharp.text.Image GenerateMasterBarCode(string code, float barcode_Height, float barcode_Width)
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
                code39.BarHeight = barcode_Height;

                code39.TextAlignment = Element.ALIGN_CENTER;
                System.Drawing.Image img = code39.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                //System.Drawing.Image img = code39.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);

                if (img != null)
                {
                    image = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //image = iTextSharp.text.Image.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);

                    image.ScaleToFit(barcode_Width, barcode_Height);
                    image.Alignment = Element.ALIGN_LEFT;
                }
            }

            return image;
        }
        private iTextSharp.text.Image GenerateSSCCBarCode(string code)
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
                code39.BarHeight = SSCC_BARCODE_HEIGHT;

                code39.TextAlignment = Element.ALIGN_CENTER;
                System.Drawing.Image img = code39.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                //System.Drawing.Image img = code39.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);

                if (img != null)
                {
                    image = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //image = iTextSharp.text.Image.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);

                    image.ScaleToFit(BARCODE_WIDTH, SSCC_BARCODE_HEIGHT);
                    image.Alignment = Element.ALIGN_LEFT;
                }
            }

            return image;
        }

        private static void addCellwithPaddingLeftSizeRowSpan(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor, float size, int Rowspan)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, size, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", size, Font.BOLD, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.Rowspan = Rowspan;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            // cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = 5;
            cell.PaddingTop = 1;
            cell.PaddingBottom = 1;
            cell.PaddingRight = 1;
            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }

        private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, BaseColor clor)
        {
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetColorStroke(clor);
            contentByte.MoveTo(x1, y1);
            contentByte.LineTo(x2, y2);
            contentByte.Stroke();
        }
        private static void addRow(PdfPTable table, string text, int rowspan)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, false);
            Font times = new Font(bfTimes, 9, Font.NORMAL, BaseColor.BLACK);
            PdfPTable t = new PdfPTable(7);
            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.FixedHeight = 25f;
            cell.Rowspan = rowspan;
            cell.HorizontalAlignment = 1;
            // table.DefaultCell.Height = 200f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(cell);

        }
        private static void addCellnoborder1(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor)
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
            cell.PaddingLeft = -1;
            table.DefaultCell.FixedHeight = 30f;

            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;

            table.SpacingAfter = 1;
            table.AddCell(cell);
        }

        //private static void addMultiImageCell(PdfPTable table, string path, string path1, float scale, int rwspan, float top, float bottom, float left, float right)
        //{
        //    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path));

        //    image.ScaleAbsolute(15f, 15f);
        //    image.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
        //    PdfPCell cell = new PdfPCell();
        //    image.Border = 0;
        //    iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path1));
        //    cell.HorizontalAlignment = 2;
        //    cell.AddElement(image);
        //    cell.AddElement(image1);
        //    cell.FixedHeight = 60f;
        //    image.Border = 0;

        //    cell.BorderWidthLeft = left;
        //    cell.BorderWidthRight = right;
        //    cell.BorderWidthTop = top;
        //    cell.BorderWidthBottom = bottom;
        //    cell.Rowspan = rwspan;
        //    table.AddCell(cell);
        //}
        private static void addCellnoborder2(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            Font times = new Font(bfTimes, 14, Font.BOLD, forecolor);

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 1;
            cell.BorderWidthTop = top;
            cell.BorderWidthBottom = bottom;
            cell.BorderWidthRight = right;
            cell.BorderWidthLeft = left;
            table.DefaultCell.FixedHeight = 30f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;

            table.SpacingAfter = 1;
            table.AddCell(cell);
        }


        private static void addCellnoborder(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            Font times = new Font(bfTimes, 12, Font.BOLD, forecolor);

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 1;
            cell.BorderWidthTop = top;
            cell.BorderWidthBottom = bottom;
            cell.BorderWidthRight = right;
            cell.BorderWidthLeft = left;
            table.DefaultCell.FixedHeight = 30f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;

            table.SpacingAfter = 1;
            table.AddCell(cell);
        }
        private static void addRowwithoutborder(PdfPTable table, string text, int rowspan, int fontsize, float height, float top, float bottom, float left, float right, BaseColor textcolor)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, false);
            Font times = new Font(bfTimes, fontsize, Font.NORMAL, textcolor);
            PdfPTable t = new PdfPTable(7);
            //  t.AddCell(new PdfPCell(new Phrase("RepGroup Logo")) { Rowspan = 9 });
            PdfPCell cell = new PdfPCell(new Phrase(text, times));

            cell.BorderWidthLeft = left;
            cell.BorderWidthRight = right;
            cell.BorderWidthTop = top;
            cell.BorderWidthBottom = bottom;
            cell.FixedHeight = height;
            cell.Rowspan = rowspan;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.AddCell(cell);

        }

        private static void addCell(PdfPTable table, string text, int rowspan)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            Font times = new Font(bfTimes, 12, Font.NORMAL, BaseColor.BLACK);


            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            // cell.Colspan = rowspan;
            cell.FixedHeight = 30f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            table.SpacingAfter = 20;
            table.AddCell(cell);
        }
        private static void addCellwithspan(PdfPTable table, string text, int colspan)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, 12, Font.NORMAL, BaseColor.BLACK);
            Font times = new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            // cell.Rowspan = rowspan;
            // cell.FixedHeight = 30f;
            cell.Colspan = colspan;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 20;
            table.AddCell(cell);
        }
        private static void addRow_span(PdfPTable table, string text, int rowspan, int fontsize, float height, float top, float bottom, float left, float right, BaseColor color, BaseColor backcolor, BaseColor textcolor)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
         //BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, false);
         //Font times = new Font(bfTimes, fontsize, Font.NORMAL, textcolor);
            Font times = new Font(FontFactory.GetFont("Arial", fontsize, Font.NORMAL));

            PdfPTable t = new PdfPTable(7);
            //  t.AddCell(new PdfPCell(new Phrase("RepGroup Logo")) { Rowspan = 9 });
            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = backcolor;
            cell.BorderColorLeft = color;
            cell.BorderWidthLeft = left;
            cell.BorderWidthRight = right;
            cell.BorderWidthTop = top;
            cell.BorderWidthBottom = bottom;
            cell.FixedHeight = height;
            cell.Rowspan = rowspan;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.AddCell(cell);

        }
        private static void addCellwithborderNormal1(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
          //  BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            // Font times = new Font(bfTimes, 12, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
           // cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = 3;
            cell.PaddingTop = 1;
            cell.PaddingBottom = 1;
            cell.PaddingRight = 1;
            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }
        private static void addCellwithborderPaddingSize(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, 
            BaseColor forecolor, float paddingLeft, float paddingTop, float paddingBottom, float size)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, size, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", size, Font.NORMAL, forecolor));
            

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            // cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = paddingLeft;
            cell.PaddingTop = paddingTop;
            cell.PaddingBottom = paddingBottom;
            cell.PaddingRight = 1;
            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }
        private static void addCellwithPaddingLeft50(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor,
            BaseColor forecolor, float size, float PaddingLeft)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, size, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", size, Font.NORMAL, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            // cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = PaddingLeft;
            cell.PaddingTop = 1;
            cell.PaddingBottom = 1;
            cell.PaddingRight = 1;
            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }

        private static void addCellwithPaddingLeft50(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor, float size)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, size, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", size, Font.NORMAL, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            // cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = 50;
            cell.PaddingTop = 1;
            cell.PaddingBottom = 1;
            cell.PaddingRight = 1;
            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }
        private static void addCellwithPaddingLeftSize(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor, float size)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, size, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", size, Font.NORMAL, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            // cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = 20;
            cell.PaddingTop = 1;
            cell.PaddingBottom = 1;
            cell.PaddingRight = 1;
            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }
        private static void addCellwithPaddingLeftBottomTopRightSize(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, 
            BaseColor forecolor, float PaddingLeft, float PaddingBottom, float PaddingTop, float PaddingRight, float size, float FixedHeight)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, size, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", size, Font.NORMAL, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            // cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = PaddingLeft;
            cell.PaddingTop = PaddingTop;
            cell.PaddingBottom = PaddingBottom;
            cell.PaddingRight = PaddingRight;
            table.DefaultCell.FixedHeight = FixedHeight;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }

        private static void addCellwithPaddingLeftBottomTopRightSizeBolD(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor,
            BaseColor forecolor, float PaddingLeft, float PaddingBottom, float PaddingTop, float PaddingRight, float size, float FixedHeight)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, size, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", size, Font.BOLD, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            // cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = PaddingLeft;
            cell.PaddingTop = PaddingTop;
            cell.PaddingBottom = PaddingBottom;
            cell.PaddingRight = PaddingRight;
            table.DefaultCell.FixedHeight = FixedHeight;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }

        private static void addCellwithPaddingLeftPaddingBottomSize(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor, float PaddingBottom, float size)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, size, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", size, Font.NORMAL, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            // cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = 20;
            cell.PaddingTop = 1;
            cell.PaddingBottom = PaddingBottom;
            cell.PaddingRight = 1;
            table.DefaultCell.FixedHeight = 24f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }
        private static void addCellwithPaddingLeftPaddingBottomSize(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, 
            BaseColor forecolor, float PaddingBottom, float PaddingLeft, float size, float FixedHeight)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, size, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", size, Font.NORMAL, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            // cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = PaddingLeft;
            cell.PaddingTop = 1;
            cell.PaddingBottom = PaddingBottom;
            cell.PaddingRight = 1;
            table.DefaultCell.FixedHeight = FixedHeight;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }

        private static void addCellwithPaddingLeft(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            // Font times = new Font(bfTimes, 12, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            // cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = 20;
            cell.PaddingTop = 1;
            cell.PaddingBottom = 1;
            cell.PaddingRight = 1;
            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }
        private static void addCellwithPaddingTopPaddingLeft(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor, float PaddingTop, float size)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, 10, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", size, Font.NORMAL, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            // cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = 10;
            cell.PaddingTop = PaddingTop;
            cell.PaddingBottom = 1;
            cell.PaddingRight = 1;
            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }
        private static void addCellwithPaddingTopPaddingLeftSize(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, 
            BaseColor forecolor, float PaddingTop, float size)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, size, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", size, Font.NORMAL, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            // cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = 20;
            cell.PaddingTop = PaddingTop;
            cell.PaddingBottom = 1;
            cell.PaddingRight = 1;
            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }
        private static void addCellwithborderNormalCenter(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            //BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, 12, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            //cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = 3;
            cell.PaddingTop = 1;
            cell.PaddingBottom = 1;
            cell.PaddingRight = 1;
            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }
        private static void addCellwithborderNormalCenterSizePaddingBottom(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor, float size, float PaddingBottom)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, size, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", size, Font.NORMAL, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            //cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = 3;
            cell.PaddingTop = 1;
            cell.PaddingBottom = PaddingBottom;
            cell.PaddingRight = 1;
            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }
        private static void addCellwithborderNormalPaddingLeftSizePaddingBottom(PdfPTable table, string text, float top, float bottom, float left, float right, 
            BaseColor bckcolor, BaseColor forecolor, float size, float PaddingBottom, float PaddingLeft)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, size, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", size, Font.NORMAL, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            //cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = PaddingLeft;
            cell.PaddingTop = 1;
            cell.PaddingBottom = PaddingBottom;
            cell.PaddingRight = 1;
            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }

        private static void addCellwithborderNormalCenterSize(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor, float size)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, size, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", size, Font.NORMAL, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            //cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = 3;
            cell.PaddingTop = 1;
            cell.PaddingBottom = 1;
            cell.PaddingRight = 1;
            table.DefaultCell.FixedHeight = 36f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }

        private static void addCellwithborderNormal(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, 12, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            //cell.BorderColor = BaseColor.BLACK;
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
        private static void addCellwithborderNormalHeightSize(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor, float FixedHeight, float bottomPadding, float topPadding, float leftPadding, float size)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, size, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", size, Font.NORMAL, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            //cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = leftPadding;
            cell.PaddingTop = topPadding;
            cell.PaddingBottom = bottomPadding;
            cell.PaddingRight = 4;
            table.DefaultCell.FixedHeight = FixedHeight;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }
        private static void addCellwithborderBold(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor,
            float PaddingTop, float PaddingBottom, float size, float FixedHeight)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            //BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, size, Font.BOLD, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", size, Font.BOLD, forecolor));

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            cell.Colspan = 0;
            cell.BorderWidthTop = 1;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderColor = BaseColor.BLACK;
            cell.PaddingLeft = 4;
            cell.PaddingTop = PaddingTop;
            cell.PaddingBottom = PaddingBottom;
            cell.PaddingRight = 4;
            table.DefaultCell.FixedHeight = FixedHeight;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            table.SpacingAfter = 1;
            table.AddCell(cell);
        }
        private static void addCellwithborderRight(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            //Font times = new Font(bfTimes, 12, Font.NORMAL, forecolor);
            Font times = new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL, forecolor));

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
            cell.FixedHeight = 40f;
            cell.BorderColor = BaseColor.WHITE;
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
            //cell.VerticalAlignment = Rectangle.ALIGN_TOP;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 0f;
            cell.PaddingLeft = 5f;
            cell.PaddingTop = 6f;

            cell.BorderColor = BaseColor.BLACK;
            cell.BorderWidthTop = 0f;
            cell.BorderWidthBottom = 0.5f;
            cell.BorderWidthRight = 0.5f;
            cell.BorderWidthLeft = 0.5f;
            return cell;
        }
        private static PdfPCell ImageCellNew10(Image img, float scale, int align)
        {
            iTextSharp.text.Image image = img;
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.FixedHeight = 40f;
            cell.BorderColor = BaseColor.WHITE;
            //cell.VerticalAlignment = Rectangle.ALIGN_TOP;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 0f;
            cell.PaddingLeft = 5f;
            cell.PaddingTop = 6f;

            //cell.BorderColor = BaseColor.BLACK;
            cell.BorderWidthTop = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderWidthRight = 0f;
            // cell.BorderWidthRight = 0.5f;
            cell.BorderWidthLeft = 0f;
            //cell.BorderWidthLeft = 0.5f;
            return cell;
        }
        private static PdfPCell ImageCellNew110(Image img, float scale, int align, float PaddingBottom)
        {
            iTextSharp.text.Image image = img;
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.FixedHeight = 48f;
            cell.BorderColor = BaseColor.WHITE;
            //cell.VerticalAlignment = Rectangle.ALIGN_TOP;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = PaddingBottom;
            cell.PaddingLeft = 5f;
            cell.PaddingTop = 3f;

            //cell.BorderColor = BaseColor.BLACK;
            cell.BorderWidthTop = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderWidthRight = 0f;
            // cell.BorderWidthRight = 0.5f;
            cell.BorderWidthLeft = 0f;
            //cell.BorderWidthLeft = 0.5f;
            return cell;
        }

        private static PdfPCell ImageCellNew11(Image img, float scale, int align)
        {
            iTextSharp.text.Image image = img;
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.FixedHeight = 48f;
            cell.BorderColor = BaseColor.WHITE;
            //cell.VerticalAlignment = Rectangle.ALIGN_TOP;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 0f;
            cell.PaddingLeft = 5f;
            cell.PaddingTop = 3f;

            //cell.BorderColor = BaseColor.BLACK;
            cell.BorderWidthTop = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderWidthRight = 0f;
           // cell.BorderWidthRight = 0.5f;
            cell.BorderWidthLeft = 0f;
            //cell.BorderWidthLeft = 0.5f;
            return cell;
        }
        private static PdfPCell ImageCellNew22(Image img, float scale, int align, float PADDINGLEFT, float FixedHeight)
        {
            iTextSharp.text.Image image = img;
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.FixedHeight = FixedHeight;
            cell.BorderColor = BaseColor.WHITE;
            //cell.VerticalAlignment = Rectangle.ALIGN_TOP;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 0;
            cell.PaddingLeft = PADDINGLEFT;
            cell.PaddingRight = 0;
            cell.PaddingTop = 2f;

            //cell.BorderColor = BaseColor.BLACK;
            cell.BorderWidthTop = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderWidthRight = 0f;
            // cell.BorderWidthRight = 0.5f;
            cell.BorderWidthLeft = 0f;
            //cell.BorderWidthLeft = 0.5f;
            return cell;
        }
        private static PdfPCell ImageCellNew022(Image img, float scale, int align, float PADDINGLEFT, float FixedHeight)
        {
            iTextSharp.text.Image image = img;
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.FixedHeight = FixedHeight;
            cell.BorderColor = BaseColor.WHITE;
            //cell.VerticalAlignment = Rectangle.ALIGN_TOP;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 7f;
            cell.PaddingLeft = PADDINGLEFT;
            cell.PaddingRight = 0;
            cell.PaddingTop = 7f;

            //cell.BorderColor = BaseColor.BLACK;
            cell.BorderWidthTop = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderWidthRight = 0f;
            // cell.BorderWidthRight = 0.5f;
            cell.BorderWidthLeft = 0f;
            //cell.BorderWidthLeft = 0.5f;
            return cell;
        }

        private static PdfPCell ImageCellNew02(Image img, float scale, int align, float PADDINGLEFT, float PADDINGRight)
        {
            iTextSharp.text.Image image = img;
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.FixedHeight = 32f;
            cell.BorderColor = BaseColor.WHITE;
            //cell.VerticalAlignment = Rectangle.ALIGN_TOP;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 4f;
            cell.PaddingLeft = PADDINGLEFT;
            cell.PaddingRight = PADDINGRight;
            cell.PaddingTop = 1f;

            //cell.BorderColor = BaseColor.BLACK;
            cell.BorderWidthTop = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderWidthRight = 0f;
            // cell.BorderWidthRight = 0.5f;
            cell.BorderWidthLeft = 0f;
            //cell.BorderWidthLeft = 0.5f;
            return cell;
        }

        private static PdfPCell ImageCellNew2(Image img, float scale, int align, float PADDINGLEFT)
        {
            iTextSharp.text.Image image = img;
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.FixedHeight = 32f;
            cell.BorderColor = BaseColor.WHITE;
            //cell.VerticalAlignment = Rectangle.ALIGN_TOP;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 4f;
            cell.PaddingLeft = PADDINGLEFT;
            cell.PaddingTop = 1f;

            //cell.BorderColor = BaseColor.BLACK;
            cell.BorderWidthTop = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderWidthRight = 0f;
            // cell.BorderWidthRight = 0.5f;
            cell.BorderWidthLeft = 0f;
            //cell.BorderWidthLeft = 0.5f;
            return cell;
        }
        private static PdfPCell ImageCellNewLeft(Image img, float scale, int align)
        {
            iTextSharp.text.Image image = img;
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.FixedHeight = 28f;
            cell.BorderColor = BaseColor.WHITE;
            //cell.VerticalAlignment = Rectangle.ALIGN_TOP;
          //  cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            //cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 0f;
            cell.PaddingLeft = 20f;
            cell.PaddingTop = 3f;

            //cell.BorderColor = BaseColor.BLACK;
            cell.BorderWidthTop = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderWidthRight = 0f;
            // cell.BorderWidthRight = 0.5f;
            cell.BorderWidthLeft = 0f;
            //cell.BorderWidthLeft = 0.5f;
            return cell;
        }
        private static PdfPCell ImageCellNewLeft(Image img, float scale, int align, float FixedHeight, float PaddingLeft, float PaddingTop)
        {
            iTextSharp.text.Image image = img;
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.FixedHeight = FixedHeight;
            cell.BorderColor = BaseColor.WHITE;
            //cell.VerticalAlignment = Rectangle.ALIGN_TOP;
            //  cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            //cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 0f;
            cell.PaddingLeft = PaddingLeft;
            cell.PaddingTop = PaddingTop;

            //cell.BorderColor = BaseColor.BLACK;
            cell.BorderWidthTop = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderWidthRight = 0f;
            // cell.BorderWidthRight = 0.5f;
            cell.BorderWidthLeft = 0f;
            //cell.BorderWidthLeft = 0.5f;
            return cell;
        }
        private static PdfPCell ImageCellNewBottomPaddingLeft(Image img, float scale, int align, float PaddingBottom)
        {
            iTextSharp.text.Image image = img;
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.FixedHeight = 28f;
            cell.BorderColor = BaseColor.WHITE;
            //cell.VerticalAlignment = Rectangle.ALIGN_TOP;
            //  cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            //cell.VerticalAlignment = PdfPCell.ALIGN_LEFT;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = PaddingBottom;
            cell.PaddingLeft = 20f;
            cell.PaddingTop = 3f;

            //cell.BorderColor = BaseColor.BLACK;
            cell.BorderWidthTop = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderWidthRight = 0f;
            // cell.BorderWidthRight = 0.5f;
            cell.BorderWidthLeft = 0f;
            //cell.BorderWidthLeft = 0.5f;
            return cell;
        }

        private static PdfPCell ImageCellNew1(Image img, float scale, int align)
        {
            iTextSharp.text.Image image = img;
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.FixedHeight = 40f;
            cell.BorderColor = BaseColor.WHITE;
            //cell.VerticalAlignment = Rectangle.ALIGN_TOP;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 3f;
            cell.PaddingLeft = 5f;
            cell.PaddingTop = 3f;

            cell.BorderColor = BaseColor.BLACK;
            cell.BorderWidthTop = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderWidthRight = 0.5f;
            cell.BorderWidthLeft = 0.5f;
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
        private static void addCellwithborderNewSize(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor, float size)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            Font times = new Font(bfTimes, size, Font.BOLD, forecolor);

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            //cell.BorderColor = BaseColor.BLACK;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
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
            //cell.BorderColor = BaseColor.BLACK;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
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
        private static void addCellwithborder(PdfPTable table, string text, float top, float bottom, float left, float right, BaseColor bckcolor, BaseColor forecolor)
        {//FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, false);
            Font times = new Font(bfTimes, 12, Font.NORMAL, forecolor);

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.BackgroundColor = bckcolor;
            //cell.BorderColor = BaseColor.BLACK;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthRight = 0;
            cell.BorderWidthLeft = 0;
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

    public class PalletModel
    {	
        public String FO { get; set; }
        public String PoNumber { get; set; }
        public String ShipDate { get; set; }
        public String PalletID { get; set; }
        public String SKU { get; set; }
        public String ItemCount { get; set; }
        public String CartonCount { get; set; }
        public String TotalPallet { get; set; }
        public String Comments { get; set; }
        public String SNo { get; set; }
        public String CompanyName { get; set; }
        public String AddressLine1 { get; set; }
        public String AddressLine2 { get; set; }
        public String State { get; set; }
        public String City { get; set; }
        public String ZipCode { get; set; }
        public String Country { get; set; }

        public String CustomerName { get; set; }
        public String ShippingAddressLine1 { get; set; }
        public String ShippingAddressLine2 { get; set; }
        public String ShippingCity { get; set; }
        public String ShippingState { get; set; }
        public String ShippingZipCode { get; set; }
        public String ShippingCountry { get; set; }
        public String ProductType { get; set; }
        public String OSType { get; set; }
        
    }
    public class CartonItem 
    {
        public String IMEI { get; set; }
        public string HEX { get; set; }
        public String MEID { get; set; }

    }

    public class MasterCartonInfo
    {
        public List<CartonItem> CartonItems { get; set; }
        public String SWVersion { get; set; }
        public String HWVersion { get; set; }
        public String ShipDate { get; set; }
        public String ContainerID { get; set; }
        public String SKU { get; set; }
        public String CartonQty { get; set; }
        public String Comments { get; set; }
        public String UPC { get; set; }
        public String ProductType { get; set; }
        public String OSType { get; set; }
        
    }

    public class CartonBoxID
    {
        public string FulfillmentNumber { get; set; }
        public string BoxID { get; set; }
        public string BoxDesc { get; set; }
        public string SKU { get; set; }

    }

}

