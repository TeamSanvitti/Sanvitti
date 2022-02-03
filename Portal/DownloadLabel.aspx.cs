using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;

using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace avii
{
    public partial class DownloadLabel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            { download(); }
        }
        private void download()
        {
            if (Request["tr"] != null)
            {
                string trackingNumber = Convert.ToString(Request["tr"]);

                List<SV.Framework.Common.LabelGenerator.Model> models = new List<SV.Framework.Common.LabelGenerator.Model>();

                List<PurchaseOrderShipmentDB> poList = HttpContext.Current.Session["shipmentDB"] as List<PurchaseOrderShipmentDB>;
                if (poList != null)
                {//List<avii.Classes.BasePurchaseOrderItem> esnList = Session["poa"] as List<avii.Classes.BasePurchaseOrderItem>;
                    SV.Framework.Common.LabelGenerator.ESNLabelOperation slabel = new SV.Framework.Common.LabelGenerator.ESNLabelOperation();
                    List<PurchaseOrderShipmentDB> esnList = (from item in poList where item.TrackingNumber.Equals(trackingNumber) select item).ToList();

                    if (esnList != null && esnList.Count > 0)
                    {
                        foreach (PurchaseOrderShipmentDB poDetail in esnList)
                        {
                            if (!string.IsNullOrWhiteSpace(poDetail.ESN))
                                models.Add(new SV.Framework.Common.LabelGenerator.Model(poDetail.SKU, poDetail.ESN, poDetail.ICC_ID, poDetail.UPC));
                        }

                        var memSt = slabel.ExportToPDF(models);
                        if (memSt != null)
                        {
                            string fileType = ".pdf";
                            string filename = DateTime.Now.Ticks + fileType;
                            Response.Clear();
                            //Response.ContentType = "application/pdf";
                            Response.ContentType = "application/octet-stream";
                            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                            Response.Buffer = true;
                            Response.Clear();
                            var bytes = memSt.ToArray();
                            Response.OutputStream.Write(bytes, 0, bytes.Length);
                            Response.OutputStream.Flush();


                        }
                        else
                            Response.Write("Technical error!");
                    }
                }
            }
        }
    }
}