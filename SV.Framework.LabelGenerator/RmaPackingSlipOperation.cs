using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Http;

namespace SV.Framework.LabelGenerator
{
    public class RmaPackingSlipOperation
    {
        public MemoryStream ExportToPDF(RmaInfo rmaInfo)
        {
            if (rmaInfo != null && !string.IsNullOrWhiteSpace(rmaInfo.RMANumber))
                return CreateRMAPackingSlipPDF(rmaInfo);
            else
                return null;
        }
        private MemoryStream CreateRMAPackingSlipPDF(RmaInfo rmaInfo)
        {
            //server folder path which is stored your PDF documents  

            //  string path = Server.MapPath("~");
            //string logo = path + "/img/Aarons-Logo.jpg";
            var memoryStream = new MemoryStream();

            //string path = Server.MapPath("~/PDFFiles");
            string logo = rmaInfo.CompanyLogo; //path + "/PDFFiles/" + txtRmaNum.Text + ".pdf";
            List<RmaDetailModel> rmaDetail = rmaInfo.RmaDetails; //Session["rmadetailslist"] as List<avii.Classes.RMADetail>;

            Document document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
                //FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
               // PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);

                //PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
                using (PdfWriter writer = PdfWriter.GetInstance(document, memoryStream))
                {
                    iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(logo);
                    pic.ScaleAbsolute(235, 113);
                    pic.ScaleToFit(235f, 113f);
                    document.Open();

                    //document.Add(pic);

                    PdfPTable logoTable = new PdfPTable(2);
                    logoTable.TotalWidth = 555f;
                    logoTable.HorizontalAlignment = 0;
                    //fix the absolute width of the table
                    logoTable.LockedWidth = true;
                    logoTable.SpacingAfter = 4f;
                    PdfPCell logoCell = new PdfPCell(pic);
                    logoCell.Border = 0;
                    logoTable.AddCell(logoCell);


                    ///document.Add(logoTable);


                    PdfPTable barTable = new PdfPTable(1);
                    barTable.TotalWidth = 270f;
                    barTable.HorizontalAlignment = 2;
                    //fix the absolute width of the table
                    barTable.LockedWidth = true;
                    barTable.SpacingAfter = 4f;

                    PdfContentByte cb = writer.DirectContent;

                    Barcode39 bc39 = new Barcode39();
                    //bc39.Code = "123400088";
                    bc39.Code = rmaInfo.RMANumber;
                    //bc39.BarHeight = 28;
                    // comment next line to show barcode text
                    bc39.Font = null;
                    bc39.TextAlignment = Element.ALIGN_RIGHT;

                    iTextSharp.text.Image img = bc39.CreateImageWithBarcode(cb, null, null);

                    PdfPTable barcodeTable = new PdfPTable(1);
                    barcodeTable.TotalWidth = 241f;
                    barcodeTable.HorizontalAlignment = 2;
                    //fix the absolute width of the table
                    barcodeTable.LockedWidth = true;
                    //barcodeTable.SpacingBefore = 5f;
                    barcodeTable.SpacingAfter = 4f;
                    PdfPCell barcodeCell = new PdfPCell(img);
                    barcodeCell.Border = 0;
                    barcodeTable.AddCell(barcodeCell);
                    //document.Add(barcodeTable);

                    PdfPCell barCell = new PdfPCell(barcodeTable);
                barCell.PaddingTop = 10f;
                //barCell.PaddingRight = 20f;

                barCell.Border = 0;
                
                    PdfPTable pakingTable = new PdfPTable(1);
                    pakingTable.TotalWidth = 241f;
                    pakingTable.HorizontalAlignment = 2;
                    //fix the absolute width of the table
                    pakingTable.LockedWidth = true;
                    pakingTable.SpacingAfter = 4f;
                    PdfPCell pkCell = new PdfPCell(new Phrase("PACKING SLIP", new Font(Font.FontFamily.HELVETICA, 28f, Font.BOLD)));
                    pkCell.Border = 0;
                    pakingTable.AddCell(pkCell);
                    //document.Add(pakingTable);

                    PdfPCell packCell = new PdfPCell(pakingTable);
                    packCell.Border = 0;
                    barTable.AddCell(packCell);


                    PdfPTable table = new PdfPTable(2);
                    table.TotalWidth = 240f;
                    table.HorizontalAlignment = 2;
                    //fix the absolute width of the table
                    table.LockedWidth = true;

                    //relative col widths in proportions - 1/3 and 2/3
                    float[] widths = new float[] { 2f, 3f };
                    table.SetWidths(widths);
                    //table.HorizontalAlignment = 0;
                    //leave a gap before and after the table
                    //table.SpacingBefore = 0f;
                    //table.SpacingAfter = 0f;

                    PdfPCell cell = new PdfPCell(new Phrase("Packing Slip", new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD)));
                    //cell.Colspan = 2;
                    //table.HeaderRows = 
                    //cell.Phrase = Phrase.GetInstance("Packing Slip");
                    //cell.Border = 1;
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    //cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    //table.AddCell(cell);
                    PdfPCell cell2 = new PdfPCell(new Phrase("Document Date", new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD)));
                    //cell2.Phrase = Phrase.GetInstance("Document Date");
                    //cell2.Border = 1;
                    cell2.BackgroundColor = BaseColor.LIGHT_GRAY;
                    //cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    //table.AddCell(cell2);


                    table.AddCell(cell);
                    table.AddCell(rmaInfo.RMANumber);
                    //table.AddCell("Col 3 Row 1");
                    table.AddCell(cell2);
                    table.AddCell(rmaInfo.RmaDate);
                    //table.AddCell("Col 3 Row 2");
                    //document.Add(table);


                    PdfPCell packinkCell = new PdfPCell(table);
                    packinkCell.Border = 0;
                    barTable.AddCell(packinkCell);


                barTable.AddCell(barCell);

                PdfPCell logoCell2 = new PdfPCell(barTable);
                    logoCell2.Border = 0;
                    logoTable.AddCell(logoCell2);


                    document.Add(logoTable);

                    //document.Add(barTable);



                    PdfPTable shipTable = new PdfPTable(1);
                    //shipTable.b
                    shipTable.TotalWidth = 350f;
                    shipTable.HorizontalAlignment = 2;
                    //fix the absolute width of the table
                    shipTable.LockedWidth = true;
                    shipTable.SpacingAfter = 5f;
                    PdfPCell shipCell = new PdfPCell(new Phrase("Ship To:", new Font(Font.FontFamily.HELVETICA, 18f, Font.BOLD)));
                    shipCell.Border = 0;
                    shipTable.AddCell(shipCell);
                    document.Add(shipTable);

                    PdfPTable table3 = new PdfPTable(1);
                    table3.TotalWidth = 350f;
                    //table3.TotalHeight = 100f;
                    table3.HorizontalAlignment = 2;
                    table3.LockedWidth = true;
                    table3.SpacingAfter = 10f;

                    string text = rmaInfo.CompanyName + Environment.NewLine + "RMA Department - RMA#: " + rmaInfo.RMANumber + Environment.NewLine + rmaInfo.ShippingAddressLine1 + Environment.NewLine + rmaInfo.ShippingCity + ", " + rmaInfo.ShippingState + " " + rmaInfo.ShippingZipCode + " " + rmaInfo.ShippingCountry;// ;// "San Diego, CA 92078";


                    PdfPCell cell33 = new PdfPCell(new Phrase(text, new Font(Font.FontFamily.HELVETICA, 12f, Font.NORMAL)));
                    cell33.PaddingBottom = 5f;

                    cell33.PaddingTop = 5f;
                    table3.AddCell(cell33);

                    document.Add(table3);


                    PdfPTable table2 = new PdfPTable(7);
                    table2.TotalWidth = 555f;
                    table2.HorizontalAlignment = 0;
                    //fix the absolute width of the table
                    table2.LockedWidth = true;

                    //relative col widths in proportions - 1/3 and 2/3
                    float[] widths2 = new float[] { 3f, 2f, 2f, 2f, 2f, 3f, 1f };
                    table2.SetWidths(widths2);
                    //table.HorizontalAlignment = 0;
                    //leave a gap before and after the table
                    table2.SpacingBefore = 0f;
                    table2.SpacingAfter = 10f;


                    PdfPCell cell01 = new PdfPCell(new Phrase("Date/Time Printed", new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD)));
                    cell01.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table2.AddCell(cell01);
                    PdfPCell cell02 = new PdfPCell(new Phrase("Store ID", new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD)));
                    cell02.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table2.AddCell(cell02);
                    PdfPCell cell03 = new PdfPCell(new Phrase("Store Name", new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD)));
                    cell03.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table2.AddCell(cell03);
                    PdfPCell cell04 = new PdfPCell(new Phrase("Contact", new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD)));
                    cell04.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table2.AddCell(cell04);
                    PdfPCell cell05 = new PdfPCell(new Phrase("Phone #", new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD)));
                    cell05.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table2.AddCell(cell05);
                    PdfPCell cell06 = new PdfPCell(new Phrase("Email", new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD)));
                    cell06.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table2.AddCell(cell06);
                    PdfPCell cell07 = new PdfPCell(new Phrase("Page", new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD)));
                    cell07.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table2.AddCell(cell07);

                    table2.AddCell(rmaInfo.RmaDate);
                    //if (ddlStoreID.SelectedIndex > 0)
                    //{
                    //    string storeInfo = ddlStoreID.SelectedItem.Text;
                    //    string[] str = storeInfo.Split('-');


                    //    table2.AddCell(ddlStoreID.SelectedValue);
                    //    table2.AddCell(str[1]);
                    //}
                    //else
                    //{
                    //    table2.AddCell(string.Empty);
                    //    table2.AddCell(string.Empty);
                    //}
                    table2.AddCell(rmaInfo.StoreID);
                    table2.AddCell(rmaInfo.StoreName);
                    table2.AddCell(rmaInfo.ContactName);
                    table2.AddCell(rmaInfo.PhoneNumber);
                    table2.AddCell(rmaInfo.Email);
                    table2.AddCell("1");
                    PdfPTable nested = new PdfPTable(6);
                    float[] widths3 = new float[] { 1f, 3f, 4f, 2f, 2f, 2f };
                    nested.SetWidths(widths3);

                    PdfPCell nestedcell1 = new PdfPCell(new Phrase("Shipped", new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD)));
                    nestedcell1.BackgroundColor = BaseColor.LIGHT_GRAY;
                    PdfPCell nestedcell2 = new PdfPCell(new Phrase("SKU#", new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD)));
                    nestedcell2.BackgroundColor = BaseColor.LIGHT_GRAY;
                    PdfPCell nestedcell3 = new PdfPCell(new Phrase("Description", new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD)));
                    nestedcell3.BackgroundColor = BaseColor.LIGHT_GRAY;
                    PdfPCell nestedcell4 = new PdfPCell(new Phrase("Type", new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD)));
                    nestedcell4.BackgroundColor = BaseColor.LIGHT_GRAY;
                    PdfPCell nestedcell5 = new PdfPCell(new Phrase("Desposition", new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD)));
                    nestedcell5.BackgroundColor = BaseColor.LIGHT_GRAY;
                    PdfPCell nestedcell6 = new PdfPCell(new Phrase("Reason", new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD)));
                    nestedcell6.BackgroundColor = BaseColor.LIGHT_GRAY;

                    nested.AddCell(nestedcell1);
                    nested.AddCell(nestedcell2);
                    nested.AddCell(nestedcell3);
                    nested.AddCell(nestedcell4);
                    nested.AddCell(nestedcell5);
                    nested.AddCell(nestedcell6);

                    foreach (RmaDetailModel rmaDet in rmaDetail)
                    {
                        if (!string.IsNullOrEmpty(rmaDet.ESN))
                        {
                          //  Warranty warranty = (Warranty)rmaDet.Warranty;
                           // Disposition disposition = (Disposition)rmaDet.Disposition;
                            //RMAReason reason = (RMAReason)rmaDet.Reason;

                            nested.AddCell("1");
                            nested.AddCell(rmaDet.ItemCode == string.Empty ? "Not Found" : rmaDet.ItemCode);
                            //nested.AddCell(rmaDet.ItemCode);
                            //nested.AddCell("TestSKU");

                            nested.AddCell(rmaDet.ItemDescription + Environment.NewLine + "   ESN# " + rmaDet.ESN);
                            //nested.AddCell(rmaDet.ESN);
                            nested.AddCell(Environment.NewLine + rmaDet.Warranty);
                            nested.AddCell(Environment.NewLine + rmaDet.Disposition);
                            nested.AddCell(Environment.NewLine + rmaDet.RMAReason);
                        }
                    }


                    PdfPCell nestedHousing = new PdfPCell(nested);
                    nestedHousing.Colspan = 7;
                    table2.AddCell(nestedHousing);
                    document.Add(table2);


                    Paragraph para3 = new Paragraph("Comment:", new Font(Font.FontFamily.HELVETICA, 14f, Font.BOLD));
                    //para1.Font = iTextSharp.text.Font.BOLD;
                    para3.Alignment = Element.ALIGN_LEFT;
                    //para1.SpacingBefore = 400f;
                    document.Add(para3);

                    Paragraph para4 = new Paragraph(rmaInfo.Comments);
                    //para1.Font = iTextSharp.text.Font.BOLD;
                    para4.Alignment = Element.ALIGN_LEFT;
                    //para1.SpacingBefore = 400f;
                    document.Add(para4);


                writer.CloseStream = false;
                document.Close();


            }


            memoryStream.Position = 0;



            return memoryStream;


        }

    }
    public class RmaInfo
    {
        public string Comments { get; set; }
        public string PackingSlip { get; set; }
        public string DocumentDate { get; set; }
        public int Page { get; set; }
        public string WhoPrinted { get; set; }
        public string DateTimePrinted { get; set; }
        public string CompanyLogo { get; set; }
        public string Email { get; set; }
        public string RMANumber { get; set; }
        public string ContactName { get; set; }
        public string RmaDate { get; set; }
        public string StoreID { get; set; }
        public string StoreName { get; set; }
        public string PhoneNumber { get; set; }

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

        public List<RmaDetailModel> RmaDetails { get; set; }
    }
    public class RmaDetailModel
    {

        public string ESN { get; set; }
        public string Warranty { get; set; }
        public string Disposition { get; set; }
        public string RMAReason { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }

        //public string ShippingCountry { get; set; }
    }

}
