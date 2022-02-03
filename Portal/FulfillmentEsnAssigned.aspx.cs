using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using SV.Framework.Models.Common;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Fulfillment;

namespace avii
{
    public partial class FulfillmentEsnAssigned : System.Web.UI.Page
    {
        PurchaseOrder purchaseOrderOperation = PurchaseOrder.CreateInstance<PurchaseOrder>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int poid = 0;
                if (Session["poid"] != null)
                {
                    poid = Convert.ToInt32(Session["poid"]);
                    ViewState["poid"] = poid;
                    BindProvisioning(poid);
                }
            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            PurchaseOrder purchaseOrderOperation = PurchaseOrder.CreateInstance<PurchaseOrder>();
            lblPOA.Text = string.Empty;
            int po_id = 0;
            if (ViewState["poid"] != null)
            {
                po_id = Convert.ToInt32(ViewState["poid"]);

                List<SV.Framework.Common.LabelGenerator.Model> models = new List<SV.Framework.Common.LabelGenerator.Model>();
                List<BasePurchaseOrderItem> esnList = purchaseOrderOperation.GetPurchaseOrderItemList(po_id);

                //List<BasePurchaseOrderItem> esnList = Session["poa"] as List<BasePurchaseOrderItem>;
                SV.Framework.Common.LabelGenerator.ESNLabelOperation slabel = new SV.Framework.Common.LabelGenerator.ESNLabelOperation();
                if (esnList != null && esnList.Count > 0)
                {
                    foreach (BasePurchaseOrderItem poDetail in esnList)
                    {
                        if (!string.IsNullOrWhiteSpace(poDetail.ESN))
                            models.Add(new SV.Framework.Common.LabelGenerator.Model(poDetail.ItemCode, poDetail.ESN, poDetail.LTEICCID, poDetail.UPC));
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

                        //string imagePath = "~/pdffiles/";
                        //string fileType = ".pdf";
                        //string fileDirctory = HttpContext.Current.Server.MapPath(imagePath);
                        //string filename = DateTime.Now.Ticks + fileType;
                        //slabel.RetriveLabelFromMemory(memSt, filename, fileDirctory);


                        //string baseurl = System.Configuration.ConfigurationManager.AppSettings["url"].ToString();
                        //string filePath = baseurl + "/pdffiles/" + filename;
                        //lblPOA.Text = "Generated successfully";
                        ////ScriptManager.RegisterStartupScript(this, this.GetType(), "new window", "javascript:window.open(\""+ filePath + "\", \"_newtab\");", true);
                        //lnk_Print.HRef = filePath;
                        // lnk_Print.Visible = true;
                    }
                    else
                        lblPOA.Text = "Technical error!";
                }
            }
            else
                lblPOA.Text = "Session expired!";
        }
        public string ContainerID { get; set; }
        private void BindProvisioning(int po_id)
        {
            PurchaseOrder purchaseOrderOperation = PurchaseOrder.CreateInstance<PurchaseOrder>();
            lblPOA.Text = string.Empty;

            PurchaseOrders purchaseOrders = Session["POS"] as PurchaseOrders;
            List<BasePurchaseOrder> purchaseOrderList = purchaseOrders.PurchaseOrderList;

            var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(po_id) select item).ToList();
            if (poInfoList.Count > 0)
            {

                lblAssignPO.Text = poInfoList[0].PurchaseOrderNumber;
            }
            // int poRecordCount = 0;
            //  int poErrorLogNumber = 0;
            //btnESNDelete.Visible = false;
            btnPrint.Visible = false;
            
            btnDownload.Visible = false;
            try
            {
                List<POEsn>  esnList = new List<POEsn>();
                //List<FulfillemtSKU> GetPurchaseSKUList
                List<FulfillmentSKUs> poProvisioningLineItems = purchaseOrderOperation.GetPurchaseSKUList(po_id, out esnList);
                if (esnList != null && esnList.Count > 0)
                {
                    Session["poesnlist"] = esnList;
                    ContainerID = esnList[0].ContainerID;
                    rptSKUESN.DataSource = esnList;
                    rptSKUESN.DataBind();
                }
                if (poProvisioningLineItems != null && poProvisioningLineItems.Count > 0)
                {
                    foreach (FulfillmentSKUs item in poProvisioningLineItems)
                    {
                        if (item.IsDelete)
                        {
                            btnPrint.Visible = true;
                            btnDownload.Visible = true;
                            //btnESNDelete.Visible = true;
                            //if (Session["adm"] == null)
                            //    btnESNDelete.Visible = false;

                            if (ViewState["IsReadOnly"] != null)
                            {
                                btnPrint.Visible = false;
                               // btnESNDelete.Visible = false;

                            }

                        }

                    }
                    Session["poa"] = poProvisioningLineItems;
                    gvPOA.DataSource = poProvisioningLineItems;
                    gvPOA.DataBind();
                }
                else
                {
                    gvPOA.DataSource = null;
                    gvPOA.DataBind();
                    lblPOA.Text = "No records exists!";
                    Session["poa"] = null;
                }
            }
            catch (Exception ex)
            {
                lblPOA.Text = ex.Message;
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (Session["poesnlist"] != null)
            {
                List<POEsn> esnList = Session["poesnlist"] as List<POEsn>;
                if (esnList != null && esnList.Count > 0)
                {
                    List<POEsnCsv> EsnList = new List<POEsnCsv>();
                    POEsnCsv pOEsnCsv = null;
                    foreach (POEsn item in esnList)
                    {
                        pOEsnCsv = new POEsnCsv();
                        pOEsnCsv.BoxID = item.BoxID;
                        pOEsnCsv.ContainerID = item.ContainerID;
                        pOEsnCsv.Dec = item.Dec;
                        pOEsnCsv.ESN = item.ESN;
                        pOEsnCsv.Hex = item.Hex;
                        pOEsnCsv.PalletID = item.PalletID;
                        pOEsnCsv.SerialNumber = item.SerialNumber;
                        EsnList.Add(pOEsnCsv);
                    }
                    if (EsnList != null && EsnList.Count > 0)
                    {
                        string string2CSV = esnList.ToCSV();

                        Response.Clear();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition", "attachment;filename=assignedESN.csv");
                        Response.Charset = "";
                        Response.ContentType = "application/text";
                        Response.Output.Write(string2CSV);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }
    }
}