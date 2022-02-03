//using avii.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Inventory;
using SV.Framework.Fulfillment;
namespace avii
{
    public partial class POKitting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    url = ConfigurationSettings.AppSettings["LogonPage"].ToString();
                }
                catch
                {
                    url = "/avii/logon.aspx";
                }
                if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if(!IsPostBack)
            {
                string fulfillmentNo = "", boxID = "", PalletID = "";
                if (Session["fulfillmentNo"] != null && Session["boxID"] != null && Session["PalletID"] != null)
                {
                    ViewState["fulfillmentNo"] = fulfillmentNo;
                    fulfillmentNo = Convert.ToString(Session["fulfillmentNo"]);
                    txtPONum.Text = fulfillmentNo;
                    PalletID = Convert.ToString(Session["PalletID"]);

                    GetPallets(PalletID);

                    boxID = Convert.ToString(Session["boxID"]);
                    
                    txtBoxNo.Text = boxID;
                    Session["fulfillmentNo"] = null;
                    Session["boxID"] = null;

                    BindPO();
                }
            }
        }

        private void BindPO()
        {
            KittingOperations kittingOperations = KittingOperations.CreateInstance<KittingOperations>();
            string ESN = txtESN.Text.Trim();
            string BoxID = txtBoxNo.Text.Trim();
            string poNumber = txtPONum.Text.Trim();
            string palletID = "";
            string PODATE = "";
            lblMsg.Text = "";
            lblPODate.Text = "";
            lblPONum.Text = "";
            lblQty.Text = "";
            lblStatus.Text = "";
            pnlPO.Visible = false;
            rptESN.DataSource = null;
            rptESN.DataBind();


            rptKit.DataSource = null;
            rptKit.DataBind();
            if (!string.IsNullOrEmpty(poNumber))
            {
                if (ddlPallet.SelectedIndex > 0)
                {
                    palletID = ddlPallet.SelectedValue;
                    if (string.IsNullOrEmpty(ESN) && string.IsNullOrEmpty(BoxID))
                    {
                        lblMsg.Text = "Please enter third search criteria";
                    }
                    else
                    {
                        // kittingInfo kittingInfo =
                        KittingInfo kittingInfo = kittingOperations.GetPurchaseOrderKittingInfo(poNumber, ESN, BoxID, palletID);
                        List<PurchaseOrderKitting> kittings = null;
                        List<KitRawSKU> rawSKUs = null;
                        if (kittingInfo != null && kittingInfo.PurchaseOrderKits != null && kittingInfo.PurchaseOrderKits.Count > 0)
                            kittings = kittingInfo.PurchaseOrderKits;

                        if (kittingInfo != null && kittingInfo.RawSKUs != null && kittingInfo.RawSKUs.Count > 0)
                            rawSKUs = kittingInfo.RawSKUs;

                        if (rawSKUs != null && rawSKUs.Count > 0)
                        {
                            rptKit.DataSource = rawSKUs;
                            rptKit.DataBind();
                        }
                        if (kittings != null && kittings.Count > 0)
                        {
                            ViewState["poid"] = kittings[0].POID;
                            ViewState["ContainerID"] = kittings[0].ContainerID;
                            ViewState["BoxNumber"] = kittings[0].BoxNumber;
                            rptESN.DataSource = kittings;
                            rptESN.DataBind();
                            Session["kittings"] = kittings;
                            pnlPO.Visible = true;
                            PODATE = kittings[0].PO_Date.ToString("MM/dd/yyyy");
                            lblPODate.Text = PODATE.Replace("-", "/");
                            lblPONum.Text = kittings[0].FulfillmentNumber;
                            lblQty.Text = kittings[0].QuantityCount.ToString();
                            lblStatus.Text = kittings[0].POStatus;
                            lblKittedSKU.Text = kittings[0].KittedSKU;

                            btnBox.Enabled = false;
                            btnCarton.Enabled = false;
                            btnPallet.Enabled = false;
                            btnPOSLabel.Enabled = false;

                            btnBox.CssClass = "buttongray";
                            btnCarton.CssClass = "buttongray";
                            btnPallet.CssClass = "buttongray";
                            btnPOSLabel.CssClass = "buttongray";

                            if (ViewState["fulfillmentNo"] != null)
                            {
                                btnSearch.Visible = false;
                                btnSubmit.Visible = false;
                                btnCancel.Visible = false;
                                chkVerify.Visible = false;
                                Cancel1.Text = "Close";
                            }
                            var esnError = (from item in kittings where (item.ErrorMessage != "") select item).ToList();
                            if (esnError != null && esnError.Count > 0)
                            {
                                btnSubmit.Visible = false;
                                chkVerify.Visible = false;
                                lblMsg.Text = esnError[0].ErrorMessage;

                                btnBox.CssClass = "button";
                                btnCarton.CssClass = "button";
                                btnPallet.CssClass = "button";
                                btnPOSLabel.CssClass = "button";
                                btnBox.Enabled = true;
                                btnCarton.Enabled = true;
                                btnPallet.Enabled = true;
                                btnPOSLabel.Enabled = true;
                            }

                        }
                        else
                        {
                            lblMsg.Text = "No record found";
                        }
                    }
                }
                else
                    lblMsg.Text = "Pallet is required!";
            }
            else
            {
                lblMsg.Text = "Fulfillment# is required!";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "stop loader", "StopProgress()", true);

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPO();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            pnlPO.Visible = false;
            txtBoxNo.Text = "";
            txtESN.Text = "";
            txtPONum.Text = "";
            ddlPallet.Items.Clear();
            pnlSearch.Visible = false;
            lblPallet.Visible = false;
            ddlPallet.Visible = false;

            //btnBox.Visible = false;
            //btnCarton.Visible = false;
            //btnPOSLabel.Visible = false;
            //rptESN.DataSource = null;
            //rptESN.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (chkVerify.Checked)
            {
                KittingOperations kittingOperations = KittingOperations.CreateInstance<KittingOperations>();
                string poAssignIDs = "";
                int userID = Convert.ToInt32(Session["UserID"]);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                DateTime currentUtcDateTime = DateTime.UtcNow;
                DateTime currentCSTDateTime = TimeZoneInfo.ConvertTime(currentUtcDateTime, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));
                string ESNs = "";
                List<PurchaseOrderKitting> kittings = Session["kittings"] as List<PurchaseOrderKitting>;
                foreach (PurchaseOrderKitting item in kittings)
                {                    
                    sb.Append(item.POAssignId + ",");
                }
                if (sb != null && sb.Length > 0)
                {
                    poAssignIDs = sb.ToString();
                    poAssignIDs = poAssignIDs.Substring(0, poAssignIDs.Length - 1);
                    //string[] esnList = ESNs.Split(',');

                    string returnMessage = kittingOperations.PurchaseOrderKittingUpdate(poAssignIDs, currentCSTDateTime, userID);
                    if (string.IsNullOrEmpty(returnMessage))
                    {
                        btnSubmit.Enabled = false;
                        lblMsg.Text = "Submitted successfully";
                        //if (poID > 0)
                        {
                            btnBox.Enabled = true;
                            btnCarton.Enabled = true;
                            btnPallet.Enabled = true;
                            btnBox.CssClass = "button";
                            btnCarton.CssClass = "button";
                            btnPallet.CssClass = "button";
                            btnPOSLabel.Enabled = true;
                            btnPOSLabel.CssClass = "button";
                        }
                    }
                    else
                    {
                        lblMsg.Text = returnMessage;
                    }
                }
            }
            else
            {
                lblMsg.Text = "Verification is required!";
            }

        }


        protected void Cancel1_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            pnlPO.Visible = false;
            txtBoxNo.Text = "";
            txtESN.Text = "";
            txtPONum.Text = "";
            ddlPallet.Items.Clear();
            pnlSearch.Visible = false;
            lblPallet.Visible = false;
            ddlPallet.Visible = false;


        }

        protected void btnCarton_Click(object sender, EventArgs e)
        {
            DishLabelOperations dishLabelOperations = DishLabelOperations.CreateInstance<DishLabelOperations>();

            int poID = 0;
            string ContainerID = "";

            poID = Convert.ToInt32(ViewState["poid"]);
            ContainerID = Convert.ToString(ViewState["ContainerID"]);
            List<SV.Framework.LabelGenerator.MasterCartonInfo> cartons = default; // new List<SV.Framework.LabelGenerator.MasterCartonInfo>();
            SV.Framework.LabelGenerator.MasterCartonInfo masterCartonInfo = default;
            List<SV.Framework.LabelGenerator.CartonItem> CartonItems = default;
            SV.Framework.LabelGenerator.CartonItem cartonItem = default;


            List<MasterCartonInfo> cartonsdb = dishLabelOperations.GetMasterCartonLabelByContainerID(ContainerID, poID);
            SV.Framework.LabelGenerator.DishLabelOperation dishLabelOperation = new SV.Framework.LabelGenerator.DishLabelOperation();
            SV.Framework.LabelGenerator.H3LabelOperation h3LabelOperation = new SV.Framework.LabelGenerator.H3LabelOperation();
            if (cartonsdb != null && cartonsdb.Count > 0)
            {
                cartons = new List<SV.Framework.LabelGenerator.MasterCartonInfo>();
                foreach (MasterCartonInfo item in cartonsdb)
                {
                    masterCartonInfo = new SV.Framework.LabelGenerator.MasterCartonInfo();
                    CartonItems = new List<SV.Framework.LabelGenerator.CartonItem>();
                    masterCartonInfo.CartonQty = item.CartonQty;
                    masterCartonInfo.Comments = item.Comments;
                    masterCartonInfo.ContainerID = item.ContainerID;
                    masterCartonInfo.HWVersion = item.HWVersion;
                    masterCartonInfo.OSType = item.OSType;
                    masterCartonInfo.ProductType = item.ProductType;
                    masterCartonInfo.ShipDate = item.ShipDate;
                    masterCartonInfo.SKU = item.SKU;
                    masterCartonInfo.SWVersion = item.SWVersion;
                    masterCartonInfo.UPC = item.UPC;
                    if (item.CartonItems != null && item.CartonItems.Count > 0)
                    {
                        foreach (CartonItem cartonItem1 in item.CartonItems)
                        {
                            cartonItem = new SV.Framework.LabelGenerator.CartonItem();
                            cartonItem.HEX = cartonItem1.HEX;
                            cartonItem.IMEI = cartonItem1.IMEI;
                            cartonItem.MEID = cartonItem1.MEID;
                            CartonItems.Add(cartonItem);
                        }
                    }
                    masterCartonInfo.CartonItems = CartonItems;
                    cartons.Add(masterCartonInfo);
                }
            }
            if (cartons != null && cartons.Count > 0)
            {
                string ProductType = cartons[0].ProductType;
                if (cartons[0].OSType.ToUpper() == "ANDROID")
                {
                    var memSt = h3LabelOperation.ExportMasterCartonToPDFNew(cartons);
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
                }
                else
                {
                    var memSt = dishLabelOperation.ExportMasterCartonToPDFNew(cartons);
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
                }
            }
            else
            {
                lblMsg.Text = "No record founs!";
            }
        }

        protected void btnBox_Click(object sender, EventArgs e)
        {
            ContainerOperation containerOperation = ContainerOperation.CreateInstance<ContainerOperation>();
            SV.Framework.LabelGenerator.DishLabelOperation dishLabelOperation = new SV.Framework.LabelGenerator.DishLabelOperation();

            string poNumber = txtPONum.Text.Trim();
            string BoxID = Convert.ToString(ViewState["BoxNumber"]); //txtBoxNo.Text.Trim();
            List<SV.Framework.LabelGenerator.CartonBoxID> cartonBoxIDS = default; // new List<SV.Framework.LabelGenerator.CartonBoxID>();
            SV.Framework.LabelGenerator.CartonBoxID cartonBoxID = default;

            List<CartonBoxID> cartonBoxIDs = containerOperation.GetPurchaseOrderBoxIDs(0, poNumber, 0, BoxID);

            if (cartonBoxIDs != null && cartonBoxIDs.Count > 0)
            {
                cartonBoxIDS = new List<SV.Framework.LabelGenerator.CartonBoxID>();
                foreach (CartonBoxID item in cartonBoxIDs)
                {
                    cartonBoxID = new SV.Framework.LabelGenerator.CartonBoxID();
                    cartonBoxID.BoxDesc = item.BoxDesc;
                    cartonBoxID.BoxID = item.BoxID;
                    cartonBoxID.FulfillmentNumber = item.FulfillmentNumber;
                    cartonBoxID.SKU = item.SKU;

                    cartonBoxIDS.Add(cartonBoxID);
                }
            }
            if (cartonBoxIDS != null && cartonBoxIDS.Count > 0)
            {               
                var memSt = dishLabelOperation.BOXLabelPdfTarCode(cartonBoxIDS);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "stop loader", "StopProgress()", true);
                //var memSt = slabel.ExportToPDF(models);
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
                    lblMsg.Text = "Label generated successfully.";
                }
            }

        }

        protected void btnPOSLabel_Click(object sender, EventArgs e)
        {
            DishLabelOperations dishLabelOperations = DishLabelOperations.CreateInstance<DishLabelOperations>();
            SV.Framework.LabelGenerator.DishLabelOperation dishLabelOperation = new SV.Framework.LabelGenerator.DishLabelOperation();
            SV.Framework.LabelGenerator.H3LabelOperation h3LabelOperation = new SV.Framework.LabelGenerator.H3LabelOperation();

            int poid = Convert.ToInt32(ViewState["poid"]);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string ESNs = "";
            foreach(RepeaterItem item in rptESN.Items)
            {
                Label lblESN = item.FindControl("lblESN") as Label;
                sb.Append(lblESN.Text + ",");
            }
            if (sb != null && sb.Length > 0)
            {
                ESNs = sb.ToString();
                ESNs = ESNs.Substring(0, ESNs.Length - 1);
                string[] esnList = ESNs.Split(',');
                List<SV.Framework.LabelGenerator.PosKitInfo> posKITs = default;// new List<SV.Framework.LabelGenerator.PosKitInfo>();
                List<SV.Framework.LabelGenerator.KitBoxInfo> kitBoxInfos = default;
                SV.Framework.LabelGenerator.PosKitInfo posKitInfo = default;
                SV.Framework.LabelGenerator.KitBoxInfo kitBoxInfo = default;

                List<PosKitInfo> posKITDB = dishLabelOperations.GetPosLabel(poid, esnList);
                if (posKITDB != null && posKITDB.Count > 0)
                {
                    posKITs = new List<SV.Framework.LabelGenerator.PosKitInfo>();
                    foreach (PosKitInfo item in posKITDB)
                    {
                        kitBoxInfos = new List<SV.Framework.LabelGenerator.KitBoxInfo>();
                        posKitInfo = new SV.Framework.LabelGenerator.PosKitInfo();
                        posKitInfo.CompanyName = item.CompanyName;
                        posKitInfo.ESN = item.ESN;
                        posKitInfo.HEX = item.HEX;
                        posKitInfo.HWVersion = item.HWVersion;
                        posKitInfo.ICCID = item.ICCID;
                        posKitInfo.ItemName = item.ItemName;
                        posKitInfo.MEID = item.MEID;
                        posKitInfo.OSType = item.OSType;
                        posKitInfo.ProductType = item.ProductType;
                        posKitInfo.SerialNum = item.SerialNum;
                        posKitInfo.ShipDate = item.ShipDate;
                        posKitInfo.SKU = item.SKU;
                        posKitInfo.SWVersion = item.SWVersion;
                        posKitInfo.UPC = item.UPC;
                        foreach (KitBoxInfo kitBoxInfodb in item.KitBoxList)
                        {
                            kitBoxInfo = new SV.Framework.LabelGenerator.KitBoxInfo();
                            kitBoxInfo.DisplayName = kitBoxInfodb.DisplayName;
                            kitBoxInfo.OriginCountry = kitBoxInfodb.OriginCountry;
                            kitBoxInfos.Add(kitBoxInfo);
                        }
                        posKitInfo.KitBoxList = kitBoxInfos;
                        posKITs.Add(posKitInfo);
                    }

                }

                if (posKITs != null && posKITs.Count > 0)
                {
                    string ProductType = posKITs[0].ProductType;
                    //if (ProductType.ToUpper() == "H3")
                    if (posKITs[0].OSType.ToUpper() == "ANDROID")
                    {
                        
                        var memSt = h3LabelOperation.POSKITLabelPdfTarCode(posKITs);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "stop loader", "StopProgress()", true);
                        //var memSt = slabel.ExportToPDF(models);
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
                            lblMsg.Text = "Label generated successfully.";
                        }
                    }
                    else
                    {
                        var memSt = dishLabelOperation.POSKITLabelPdfTarCode(posKITs);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "stop loader", "StopProgress()", true);
                        //var memSt = slabel.ExportToPDF(models);
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
                            lblMsg.Text = "Label generated successfully.";
                        }
                    }
                }
            }
        }

        protected void btnGetPallet_Click(object sender, EventArgs e)
        {

            GetPallets("");

        }
        private void GetPallets(string PalletID)
        {
            KittingOperations kittingOperations = KittingOperations.CreateInstance<KittingOperations>();

            lblMsg.Text = "";
            txtBoxNo.Text = "";
            txtESN.Text = "";
            lblPallet.Visible = false;
            ddlPallet.Visible = false;

            string poNumber = txtPONum.Text.Trim();
            List<POPallet> poPallets = kittingOperations.GetPurchaseOrderPallets(poNumber);
            if (poPallets != null && poPallets.Count > 0)
            {
                ddlPallet.DataSource = poPallets;
                ddlPallet.DataValueField = "PalletID";
                ddlPallet.DataTextField = "PalletID";
                ddlPallet.DataBind();
                ddlPallet.Items.Insert(0, new ListItem("", ""));
                if(!string.IsNullOrEmpty(PalletID))
                    ddlPallet.SelectedValue = PalletID;
                else
                    ddlPallet.SelectedIndex = 1;
                pnlSearch.Visible = true;
                pnlPO.Visible = false;
                lblPallet.Visible = true;
                ddlPallet.Visible = true;

            }
            else
            {
                pnlSearch.Visible = false;
                pnlPO.Visible = false;
                lblMsg.Text = "No pallets found!";
            }
        }

        private void GeneratePalletPDF()
        {
            try
            {
                DishLabelOperations dishLabelOperations = DishLabelOperations.CreateInstance<DishLabelOperations>();
                SV.Framework.LabelGenerator.DishLabelOperation dishLabelOperation = new SV.Framework.LabelGenerator.DishLabelOperation();
                SV.Framework.LabelGenerator.H3LabelOperation h3LabelOperation = new SV.Framework.LabelGenerator.H3LabelOperation();

                string poNumber = txtPONum.Text.Trim();
                string palletID = "";
                if (!string.IsNullOrEmpty(poNumber))
                {
                    if (ddlPallet.SelectedIndex > 0)
                    {
                        palletID = ddlPallet.SelectedValue;
                        int poid = 0;
                        var memoryStream = new MemoryStream();
                        List<SV.Framework.LabelGenerator.PalletModel> pallets = default;// new List<SV.Framework.LabelGenerator.PalletModel>();
                        SV.Framework.LabelGenerator.PalletModel palletModel = default;

                        List<PalletModel> palletsDb = dishLabelOperations.GetPalletLabelInfo(poid, poNumber, palletID);
                        if (palletsDb != null && palletsDb.Count > 0)
                        {
                            pallets = new List<SV.Framework.LabelGenerator.PalletModel>();
                            foreach (PalletModel item in palletsDb)
                            {
                                palletModel = new SV.Framework.LabelGenerator.PalletModel();
                                palletModel.AddressLine1 = item.AddressLine1;
                                palletModel.AddressLine2 = item.AddressLine2;
                                palletModel.CartonCount = item.CartonCount;
                                palletModel.City = item.City;
                                palletModel.Comments = item.Comments;
                                palletModel.CompanyName = item.CompanyName;
                                palletModel.CustomerName = item.CustomerName;
                                palletModel.FO = item.FO;
                                palletModel.ItemCount = item.ItemCount;
                                palletModel.OSType = item.OSType;
                                palletModel.PalletID = item.PalletID;
                                palletModel.PoNumber = item.PoNumber;
                                palletModel.ProductType = item.ProductType;
                                palletModel.TotalPallet = item.TotalPallet;
                                palletModel.ShipDate = item.ShipDate;
                                palletModel.ShippingAddressLine1 = item.ShippingAddressLine1;
                                palletModel.ShippingAddressLine2 = item.ShippingAddressLine2;
                                palletModel.ShippingCity = item.ShippingCity;
                                palletModel.ShippingCountry = item.ShippingCountry;
                                palletModel.ShippingState = item.ShippingState;
                                palletModel.ShippingZipCode = item.ShippingZipCode;
                                palletModel.SKU = item.SKU;
                                palletModel.SNo = item.SNo;
                                palletModel.State = item.State;
                                palletModel.TotalPallet = item.TotalPallet;
                                palletModel.ZipCode = item.ZipCode;
                                pallets.Add(palletModel);
                            }

                        }
                        if (pallets != null && pallets.Count > 0)
                        {
                            string productType = pallets[0].ProductType;

                            //btnHide.Click -= new EventHandler(btnHide_Click);

                            //btnHide.Click += new EventHandler(btnHide_Click);

                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
                            ////System.Threading.Tasks.Task.Delay(5000);

                            // IPostBackEventHandler pbh = btnHide as IPostBackEventHandler;
                            // pbh.RaisePostBackEvent(string.Empty);
                            //if (productType.ToUpper() == "H3")
                            if (pallets[0].OSType.ToUpper() == "ANDROID")
                            {
                                var memSt = h3LabelOperation.ExportPalletToPDFNew(pallets);
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
                                {
                                    lblMsg.Text = "Technical error!";
                                }
                            }
                            else
                            {
                                var memSt = dishLabelOperation.ExportPalletToPDFNew(pallets);
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
                                {
                                    lblMsg.Text = "Technical error!";
                                }
                            }
                        }
                        else
                        {
                            lblMsg.Text = "No pallet found";
                        }
                    }
                    else
                        lblMsg.Text = "Pallet is required!";
                }
                else
                {
                    lblMsg.Text = "Fulfillment number is required!";
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnPallet_Click(object sender, EventArgs e)
        {
            GeneratePalletPDF();
        }

        protected void btnKittingWithSamePO_Click(object sender, EventArgs e)
        {
            txtESN.Text = "";
            txtBoxNo.Text = "";
            lblMsg.Text = "";

            //pnlSearch.Visible = true;
            pnlPO.Visible = false;
            //lblPallet.Visible = true;
            //ddlPallet.Visible = true;
        }

        protected void btnKittingWithNewPO_Click_Click(object sender, EventArgs e)
        {
            lblPallet.Visible = false;
            ddlPallet.Items.Clear();
            ddlPallet.Visible = false;
            txtPONum.Text = "";
            txtESN.Text = "";
            txtBoxNo.Text = "";
            lblMsg.Text = "";

            pnlSearch.Visible = false;
            pnlPO.Visible = false;
        }
    }
}