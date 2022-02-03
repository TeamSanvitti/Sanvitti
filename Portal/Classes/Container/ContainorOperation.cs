using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class ContainorOperation
    {

        Image PostalCodeimage;
        Image SSCCImage;
        public string CreatePDFforNotes(ContainerModel ContainerInfo)
        {

            

            
            if(! string.IsNullOrEmpty(ContainerInfo.PostalCode))
            {
                PostalCodeimage = GenerateBarCode(ContainerInfo.PostalCode);
            }

            if (!string.IsNullOrEmpty(ContainerInfo.ContainerNumber))
            {
                SSCCImage = GenerateBarCode(ContainerInfo.ContainerNumber);
            }
            string FilePath = string.Empty;
            string filepath = "";
            DateTime fileCreationDatetime = DateTime.Now;
            String timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            filepath = "/Upload/pdf/" + timeStamp;

            FilePath = HttpContext.Current.Server.MapPath("~/Upload/pdf/" + timeStamp);
            filepath = "/Upload/pdf/" + timeStamp;
            if (!System.IO.Directory.Exists(FilePath))
                System.IO.Directory.CreateDirectory(FilePath);
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

            string filepaths = FilePath + "/" + fileName;
            string path = filepath + "/" + fileName;
            string pdfPath = filepaths;

            PdfPTable table;
            PdfPCell cell = null;

            using (FileStream msReport = new FileStream(pdfPath, FileMode.OpenOrCreate))
            {
                //step 1
                Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 50f, 70f);
                try
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, msReport);


                    //open the stream 
                    pdfDoc.Open();

                    PdfPTable tablespace = new PdfPTable(1);
                    tablespace.TotalWidth = 500f;
                    tablespace.LockedWidth = true;
                    tablespace.SetWidths(new float[] { 0.2f });


                    Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                    tablespace.AddCell(PhraseCell(new Phrase("\n", FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));



                    PdfPCell cell1 = null;
                    cell1 = new PdfPCell();


                    table = new PdfPTable(2);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    float[] widths = new float[] { 287.6f, 287.6f };
                    table.SetWidths(widths);
                    addCellwithborderNew(table, "FROM: ", 0.5f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    addCellwithborderNew(table, "TO: ", 0.5f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);

                    table = new PdfPTable(2);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                     widths = new float[] { 287.6f, 287.6f };
                    table.SetWidths(widths);
                    addCellwithborderNormal(table, ContainerInfo.CompanyName, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    addCellwithborderNormal(table, ContainerInfo.CustomerName, 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);

                    table = new PdfPTable(2);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 287.6f, 287.6f };
                    table.SetWidths(widths);
                    if (string.IsNullOrEmpty(ContainerInfo.AddressLine2))
                        addCellwithborderNormal(table, ContainerInfo.AddressLine1, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    else
                        addCellwithborderNormal(table, ContainerInfo.AddressLine1 + ", " + ContainerInfo.AddressLine2, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);



                    table = new PdfPTable(2);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 287.6f, 287.6f };
                    table.SetWidths(widths);
                     if (string.IsNullOrEmpty(ContainerInfo.ShippingAddressLine2))
                        addCellwithborderNormal(table, ContainerInfo.ShippingAddressLine1, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    else
                        addCellwithborderNormal(table, ContainerInfo.ShippingAddressLine1 + ", " + ContainerInfo.ShippingAddressLine2, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);

                    table = new PdfPTable(2);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 287.6f, 287.6f };
                    table.SetWidths(widths);

                    addCellwithborderNormal(table, ContainerInfo.City + " " + ContainerInfo.State + " " + ContainerInfo.ZipCode, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    addCellwithborderNormal(table, ContainerInfo.ShippingCity + " " + ContainerInfo.ShippingState + " " + ContainerInfo.ShippingZipCode, 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);

                    table = new PdfPTable(2);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 287.6f, 287.6f };
                    table.SetWidths(widths);
                    addCellwithborderNormal(table, ContainerInfo.Country, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    addCellwithborderNormal(table, ContainerInfo.ShippingCountry, 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);


                    table = new PdfPTable(2);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 287.6f, 287.6f };
                    table.SetWidths(widths);
                    addCellwithborderNew(table, "SHIP TO POSTAL CODE: ", 0.5f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    addCellwithborderNew(table, "CARRIER: ", 0.5f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);

                    table = new PdfPTable(2);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 287.6f, 287.6f };
                    table.SetWidths(widths);
                    addCellwithborderNew(table, ContainerInfo.PostalCode, 0f, 0f, 0f, 0.5f, BaseColor.WHITE, BaseColor.BLACK);
                    addCellwithborderNew(table, "", 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);

                    table = new PdfPTable(2);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    widths = new float[] { 287.6f, 287.6f };
                    table.SetWidths(widths);
                    if (PostalCodeimage != null)
                    {

                        cell1 = ImageCellNew(PostalCodeimage, 150f, PdfPCell.ALIGN_LEFT);
                        cell1.BorderWidthRight = 0.5f;
                        table.AddCell(cell1);

                    }

                    addCellwithborderNormal(table, ContainerInfo.Carrier, 0f, 0f, 0.5f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);

                    
                    table = new PdfPTable(1);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    addCellwithborderNormal(table, "PO#: "+ContainerInfo.PoNumber, 0.5f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);

                    table = new PdfPTable(1);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    addCellwithborderNormal(table, "DPCI: " + ContainerInfo.PoNumber, 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);
                    table = new PdfPTable(1);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    addCellwithborderNormal(table, "Casepack: " + ContainerInfo.Casepack, 0f, 0.5f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);
                    pdfDoc.Add(tablespace);
                    pdfDoc.Add(tablespace);
                    pdfDoc.Add(tablespace);
                    pdfDoc.Add(tablespace);
                    pdfDoc.Add(tablespace);
                    pdfDoc.Add(tablespace);
                    pdfDoc.Add(tablespace);
                    pdfDoc.Add(tablespace);

                    table = new PdfPTable(1);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    addCellwithborderNew(table, "SSCC-18", 0.5f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);
                    pdfDoc.Add(tablespace);
                    pdfDoc.Add(tablespace);
                    table = new PdfPTable(1);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    table.DefaultCell.Border = Rectangle.NO_BORDER;

                    addCellwithborderNormal(table, ContainerInfo.ContainerNumber, 0f, 0f, 0f, 0f, BaseColor.WHITE, BaseColor.BLACK);
                    pdfDoc.Add(table);
                    pdfDoc.Add(tablespace);

                    table = new PdfPTable(1);
                    table.TotalWidth = 575f;
                    table.LockedWidth = true;
                    //table.SetWidths(new float[] { 150f, 60f, 90f });
                    PdfPCell cell2 = null;
                    cell2 = new PdfPCell();

                    if (SSCCImage != null)
                    {

                        cell2 = ImageCellNew(SSCCImage, 150f, PdfPCell.ALIGN_LEFT);
                        table.AddCell(cell2);

                    }
                    pdfDoc.Add(table);


                    pdfDoc.Close();
                    return path;
                   


                }
                catch (Exception ex)
                {
                    //handle exception
                    return "-1";
                }

            }


        }

        private const float BARCODE_HEIGHT = 12f;
        private const float BARCODE_WIDTH = 180f;
        private const float PDF_PAGE_WIDTH_PERCENT = 100f;
        private const float TEXT_FONT_SIZE = 6f;
        private const float LABEL_WIDTH_SIZE = 216;
        private const float LABEL_HEIGHT_SIZE = 140;//128

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
        
        private static void addMultiImageCell(PdfPTable table, string path, string path1, float scale, int rwspan, float top, float bottom, float left, float right)
        {
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path));

            image.ScaleAbsolute(15f, 15f);
            image.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
            PdfPCell cell = new PdfPCell();
            image.Border = 0;
            iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path1));
            cell.HorizontalAlignment = 2;
            cell.AddElement(image);
            cell.AddElement(image1);
            cell.FixedHeight = 60f;
            image.Border = 0;

            cell.BorderWidthLeft = left;
            cell.BorderWidthRight = right;
            cell.BorderWidthTop = top;
            cell.BorderWidthBottom = bottom;
            cell.Rowspan = rwspan;
            table.AddCell(cell);
        }
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
            Font times = new Font(bfTimes, 12, Font.NORMAL, BaseColor.BLACK);

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
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, false);
            Font times = new Font(bfTimes, fontsize, Font.NORMAL, textcolor);
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
            cell.VerticalAlignment = Rectangle.ALIGN_TOP;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 0f;
            cell.PaddingLeft = 5f;
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

    public class ContainerModel
    {
        public String PostalCode { get; set; }
        public String Carrier { get; set; }
        public String PoNumber { get; set; }
        public String DPCI { get; set; }
        public String Casepack { get; set; }
        public String ContainerNumber { get; set; }
        public String Text { get; set; }
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

    }
}


