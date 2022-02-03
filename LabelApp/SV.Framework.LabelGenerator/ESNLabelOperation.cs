using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Http;
using System.Drawing;


namespace SV.Framework.Common.LabelGenerator
{
    
    public class Model
    {
        public string SKU { get; set; }
        public string IMEI { get; set; }
        public string ICCID { get; set; }
        public string UPC { get; set; }
        public Model(string sku, string imei, string iccid, string upc)
        {
            SKU = sku;
            IMEI = imei;
            ICCID = iccid;
            UPC = upc;
        }
    }
    public class ESNLabelOperation
    {
       
        private const float BARCODE_HEIGHT = 12f;
        private const float BARCODE_WIDTH = 180f;
        private const float PDF_PAGE_WIDTH_PERCENT = 100f;
        private const float TEXT_FONT_SIZE = 6f;
        private const float LABEL_WIDTH_SIZE = 216;
        private const float LABEL_HEIGHT_SIZE = 140;//128

        public MemoryStream ExportToPDF(IList<Model> models)
        {
            if (models != null && models.Count > 0)
                return LabelsPdf(models);
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
                System.Drawing.Image img = code39.CreateDrawingImage(Color.Black, System.Drawing.Color.White);
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

        private MemoryStream LabelsPdf(IList<Model> models)
        {
            int bottomMargin = 0;
            int topMargin = 10;
            float LABELHEIGHTSIZE = LABEL_HEIGHT_SIZE;
            //foreach (var model in models)
            //{
            //    if (string.IsNullOrWhiteSpace(model.ICCID))
            //    {
            //        LABELHEIGHTSIZE = 102; //96
            //        topMargin = 5;
            //        bottomMargin = 15;
            //    }
                
            //}

            const int pageMargin = 3;
            const int pageRows = 1;
            const int pageCols = 1;
            string ESN = string.Empty;
            iTextSharp.text.Image tempImage;
            var doc = new Document();
            var memoryStream = new MemoryStream();

            var pdfWriter = PdfWriter.GetInstance(doc, memoryStream);
            doc.Open();

            var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, TEXT_FONT_SIZE);

            foreach (var model in models)
            {
                if (string.IsNullOrWhiteSpace(model.ICCID))
                {
                    LABELHEIGHTSIZE = 102; //96
                    topMargin = 5;
                    bottomMargin = 15;
                }
                else
                {
                    LABELHEIGHTSIZE = 140; //96
                    topMargin = 10;
                    bottomMargin = 0;
                }
                iTextSharp.text.Rectangle envelope = new iTextSharp.text.Rectangle(LABEL_WIDTH_SIZE, LABELHEIGHTSIZE);
                doc.SetPageSize(envelope);
                doc.SetMargins(pageMargin, pageMargin, 0, 0);

                doc.NewPage();
                PdfPTable table = new PdfPTable(pageCols);
                table.WidthPercentage = PDF_PAGE_WIDTH_PERCENT;
                table.DefaultCell.Border = 2;

                #region Label Construction
                tempImage = null;

                PdfPCell cell = new PdfPCell { PaddingLeft = 8, PaddingTop = topMargin, PaddingBottom = bottomMargin, PaddingRight = 1 };

                cell.Border = 0;
                cell.FixedHeight = (doc.PageSize.Height - (pageMargin * 2)) / pageRows;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;

                var contents = new Paragraph();

                tempImage = GenerateBarCode((model.SKU ?? ""));
                if (tempImage != null)
                {
                    contents = new Paragraph();
                    contents.Add(new Chunk(($"SKU: {(model.SKU ?? "")}")));
                    contents.Alignment = Element.ALIGN_LEFT;
                    contents.Font = new iTextSharp.text.Font(baseFont, TEXT_FONT_SIZE);
                    cell.AddElement(contents);
                    cell.AddElement(tempImage);
                    tempImage = null;
                }

                
              //  contents.Add(new Chunk(($"SKU: {(model.SKU ?? "")}"), boldFont));
              //  cell.AddElement(contents);
              //  cell.AddElement(tempImage);

               
                tempImage = GenerateBarCode((model.IMEI ?? ""));
                if (tempImage != null)
                {
                    if (string.IsNullOrEmpty(model.ICCID))
                        contents = new Paragraph(($"ICCID: {(model.IMEI ?? "")}"));
                    else
                        contents = new Paragraph(($"IMEI: {(model.IMEI ?? "")}"));

                    contents.Alignment = Element.ALIGN_LEFT;
                    contents.Font = new iTextSharp.text.Font(baseFont, TEXT_FONT_SIZE);
                    cell.AddElement(contents);
                    cell.AddElement(tempImage);
                    tempImage = null;
                }


                tempImage = GenerateBarCode((model.ICCID ?? ""));
                if (tempImage != null)
                {
                    contents = new Paragraph(($"ICCID: {(model.ICCID ?? "")}"));
                    //contents.Add(new Chunk(($"ICCID: {(model.ICCID ?? "")}")));
                    contents.Alignment = Element.ALIGN_LEFT;

                    contents.Font = new iTextSharp.text.Font(baseFont, TEXT_FONT_SIZE);
                    cell.AddElement(contents);
                    cell.AddElement(tempImage);
                    tempImage = null;
                }

                tempImage = GenerateBarCode((model.UPC ?? ""));
                if (tempImage != null)
                {
                    contents = new Paragraph(($"UPC: {(model.UPC ?? "")}"));
                    contents.Alignment = Element.ALIGN_LEFT;
                    contents.Font = new iTextSharp.text.Font(baseFont, TEXT_FONT_SIZE);

                    //contents.Add(new Chunk(($"UPC: {(model.UPC ?? "")}"), new iTextSharp.text.Font(baseFont, TEXT_FONT_SIZE)));
                    cell.AddElement(contents);
                    cell.AddElement(tempImage);
                    tempImage = null;
                }

                table.AddCell(cell);

                #endregion


                table.CompleteRow();
                doc.Add(table);

            }


            // Close PDF document and send
            pdfWriter.CloseStream = false;
            doc.Close();

            memoryStream.Position = 0;

            return memoryStream;
        }

        public string RetriveLabelFromMemory(MemoryStream stream, string fileName, string directory)
        {
            FileInfo fileInfo = new FileInfo(Path.Combine(directory, fileName));

            if (fileInfo != null)
            {
                using (FileStream sourceStream = new FileStream(fileInfo.FullName, FileMode.Create))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.WriteTo(sourceStream);

                    stream.Flush();
                    stream.Close();
                }
                return fileInfo.FullName;

            }
            else
                return null;

        }

    }

}

