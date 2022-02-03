using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Http;
using System.Drawing;

namespace avii.Classes
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
        public MemoryStream ExportToPDF(IList<Model> models)
        {
            if (models != null && models.Count > 0)
                return LabelsPdf(models);
            else
                return null;
        }

        private string GenerateBarCode(string code)
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                Barcode39 code39 = new Barcode39();
                code39.Code = code;
                code39.StartStopText = true;
                code39.GenerateChecksum = false;
                code39.Extended = true;
                code39.Baseline = 12f;
                code39.BarHeight = 15f;

                code39.TextAlignment = Element.ALIGN_CENTER;
                System.Drawing.Image img = code39.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imageBytes = ms.ToArray();
                //return imageBytes;
                return Convert.ToBase64String(imageBytes);
            }
            else
            {
                return null;
            }
        }

        private iTextSharp.text.Image ImageCell(string imageBytes)
        {
            if (!string.IsNullOrWhiteSpace(imageBytes))
            {
                System.Drawing.Image image;
                byte[] bytes = Convert.FromBase64String(imageBytes);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    using (System.Drawing.Image tmpImage = System.Drawing.Image.FromStream(ms))
                    {
                        image = new Bitmap(tmpImage);
                    }
                }
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);
                jpg.ScaleToFit(300f, 12f);
                //jpg.SpacingBefore = 2f;            
                jpg.Alignment = Element.ALIGN_LEFT;
                return jpg;
            }
            else
                return null;
        }

        private MemoryStream LabelsPdf(IList<Model> models)
        {
            const int pageMargin = 2;
            const int pageRows = 2;
            const int pageCols = 1;
            iTextSharp.text.Image tempImage;
            var doc = new Document();
            doc.SetMargins(pageMargin, pageMargin, pageMargin, pageMargin);
            var memoryStream = new MemoryStream();

            var pdfWriter = PdfWriter.GetInstance(doc, memoryStream);
            doc.Open();


            var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8f);

            foreach (var model in models)

            {
                doc.NewPage();
                PdfPTable table = new PdfPTable(pageCols);
                table.WidthPercentage = 100f;
                table.DefaultCell.Border = 0;

                #region Label Construction
                tempImage = null;

                PdfPCell cell = new PdfPCell { PaddingLeft = 10, PaddingTop = 2, PaddingBottom = 1, PaddingRight = 1 };

                cell.Border = 0;
                cell.FixedHeight = (doc.PageSize.Height - (pageMargin * 2)) / pageRows;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;

                var contents = new Paragraph();
                contents.Alignment = Element.ALIGN_LEFT;
                contents.Add(new Chunk(($"SKU: {(model.SKU ?? "")}"), boldFont));
                cell.AddElement(contents);
                tempImage = ImageCell(GenerateBarCode((model.SKU ?? "")));
                if (tempImage != null) cell.AddElement(tempImage);
                tempImage = null;

                contents = new Paragraph(($"IMEI: {(model.IMEI ?? "")}")); contents.Alignment = Element.ALIGN_LEFT;
                contents.Font = new iTextSharp.text.Font(baseFont, 6f);
                cell.AddElement(contents);
                tempImage = ImageCell(GenerateBarCode((model.IMEI ?? "")));
                if (tempImage != null) cell.AddElement(tempImage);
                tempImage = null;

                contents = new Paragraph();
                contents.Add(new Chunk(($"ICCID: {(model.ICCID ?? "")}")));
                contents.Font = new iTextSharp.text.Font(baseFont, 6f);
                cell.AddElement(contents);
                tempImage = ImageCell(GenerateBarCode((model.ICCID ?? "")));
                if (tempImage != null) cell.AddElement(tempImage);
                tempImage = null;


                contents = new Paragraph();
                contents.Alignment = Element.ALIGN_LEFT;
                contents.Add(new Chunk(($"UPC: {(model.UPC ?? "")}"), new iTextSharp.text.Font(baseFont, 6f)));
                cell.AddElement(contents);
                tempImage = ImageCell(GenerateBarCode((model.UPC ?? "")));
                if (tempImage != null) cell.AddElement(tempImage);


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