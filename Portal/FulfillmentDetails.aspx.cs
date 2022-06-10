//using avii.Classes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using SV.Framework.Fulfillment;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace avii
{
    public partial class FulfillmentDetails : System.Web.UI.Page
    {
        int gvEditIndex = -1;
        string gvUniqueID = String.Empty;
        int gvNewPageIndex = 0;
        //int gvEditIndex = -1;
        string gvSortExpr = String.Empty;
        string downLoadPath = string.Empty;
        string  writer = "csv";
        bool grid1SelectCommand = false;
        private  DishLabelOperations dishLabelOperations = DishLabelOperations.CreateInstance<DishLabelOperations>();
        private PurchaseOrder purchaseOrderOperation = PurchaseOrder.CreateInstance<PurchaseOrder>();

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
            if (!IsPostBack)
            {
                ReadOlnyAccess();
                int poid = 0;
                if (Session["poid"] != null)
                {
                    bool poQuery = true;
                    if (Session["unrep"] != null)
                    {
                        Session["unrep"] = null;
                        poQuery = false;
                    }
                    if (Session["reconditioning"] != null || Session["provisioning"] != null || Session["unused"] != null)
                    {
                        Session["reconditioning"] = null;
                        Session["provisioning"] = null;
                        Session["unused"] = null;
                        poQuery = false;
                    }
                    poid = Convert.ToInt32(Session["poid"]);
                    ViewState["poid"] = poid;
                    Session["poid"] = null;
                    //BindShipBy();
                    // BindStates();
                    //PODetail(poid);
                    BindPO(poid, poQuery);
                    BindComments(poid);
                    BindFulfillmentLog(poid, 1);

                }
                if (Session["unpoid"] != null)
                {
                    lblHeader.Text = "Unprovisioing";
                    poid = Convert.ToInt32(Session["unpoid"]);
                    ViewState["poid"] = poid;
                    Session["unpoid"] = null;
                    lblpoid.Text = poid.ToString();
                    //BindShipBy();
                    // BindStates();
                    //PODetail(poid);
                    BindPO(poid, false);
                    BindComments(poid);
                    BindFulfillmentLog(poid, 1);
                    btnContainerSlip.Visible = false;
                    btnPckSlip.Visible = false;
                    pnlUnprovision.Visible = true;
                    if(Session["unporequest"] != null)
                    {
                        Session["unporequest"] = null;
                        btnSubmit.Visible = true;
                    }

                }
                // int poid = 0;
                if (Session["po_id"] != null)
                {
                    int StockInDemand = 0;

                    int userID = 0, companyID = 0;
                    if (Session["adm"] != null)
                    {
                        userID = 0;
                    }
                    else
                        if (ViewState["userid"] != null)
                    {
                        userID = Convert.ToInt32(ViewState["userid"]);

                    }

                    poid = Convert.ToInt32(Session["po_id"]);
                    ViewState["poid"] = poid;
                    Session["po_id"] = null;
                    //BindShipBy();
                    // BindStates();
                    //PODetail(poid);
                    string poNum, fromDate, toDate, storeID, statusID, esn, avOrder, mslNumber, phoneCategory, sku, shipFrom, shipTo, trackingNumber, customerOrderNumber, contactName, POType;
                    poNum = fromDate = toDate = shipFrom = shipTo = trackingNumber = customerOrderNumber = contactName = POType = sku = statusID = esn = null;
                    poNum = Session["poNum"] as string;
                    PurchaseOrders pos = purchaseOrderOperation.GerPurchaseOrdersNew(poNum, contactName, fromDate, toDate, userID, statusID, companyID, esn, null, null, null, sku, null, null, null, shipFrom, shipTo, 0, trackingNumber, customerOrderNumber, POType, StockInDemand);
                    Session["POS"] = pos;

                    BindPO(poid, true);
                    BindComments(poid);
                }


                //if (ViewState["poid"] != null)
                //{
                //    poid = Convert.ToInt32(ViewState["poid"]);
                //    ViewState["poid"] = poid;
                //    BindShipBy();
                //    //BindStates();
                //    //PODetail(poid);
                //    POShipment(poid);
                //}
               
            }
        }
        private void ReadOlnyAccess()
        {
            //string url = HttpContext.Current.Request.Url.AbsoluteUri;
            // http://localhost:1302/TESTERS/Default6.aspx

            string path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            // /TESTERS/Default6.aspx
            if (path != null)
            {
                path = path.ToLower();
                List<avii.Classes.MenuItem> menuItems = Session["MenuItems"] as List<avii.Classes.MenuItem>;
                foreach (avii.Classes.MenuItem item in menuItems)
                {
                    if (item.Url.ToLower().Contains(path) && item.IsReadOnly)
                    {
                        ViewState["IsReadOnly"] = true;

                    }
                }

            }

        }
        private void BindUsers(int companyID)
        {
            avii.Classes.UserUtility objUser = new avii.Classes.UserUtility();
            List<avii.Classes.clsUserManagement> userList = objUser.getUserList("", companyID, "", -1, -1, -1, true);
            if (userList != null && userList.Count > 0)
            {
                ddlRequestedBy.DataSource = userList;
                ddlRequestedBy.DataValueField = "UserID";
                ddlRequestedBy.DataTextField = "UserName";
                ddlRequestedBy.DataBind();

                System.Web.UI.WebControls.ListItem newList = new System.Web.UI.WebControls.ListItem("", "0");
                ddlRequestedBy.Items.Insert(0, newList);
            }
            else
            {
                ddlRequestedBy.DataSource = null;
                ddlRequestedBy.DataBind();
                ddlRequestedBy.Text = "No users are assigned to selected Customer";
            }
        }
        protected void btnPckSlip_Click(object sender, EventArgs e)
        {
            PackingslipOperation packingslipOperation = PackingslipOperation.CreateInstance<PackingslipOperation>();
        
            string shipFromContactName = ConfigurationSettings.AppSettings["ShipFromContactName"].ToString();
            string shipFromContactName2 = ConfigurationSettings.AppSettings["ShipFromContactName2"].ToString();
            string shipFromAddress = ConfigurationSettings.AppSettings["ShipFromAddress"].ToString();
            string shipFromCity = ConfigurationSettings.AppSettings["ShipFromCity"].ToString();
            string shipFromState = ConfigurationSettings.AppSettings["ShipFromState"].ToString();
            string shipFromZip = ConfigurationSettings.AppSettings["ShipFromZip"].ToString();
            string shipFromCountry = ConfigurationSettings.AppSettings["ShipFromCountry"].ToString();
            string shipFromAttn = ConfigurationSettings.AppSettings["ShipFromAttn"].ToString();
            string shipFromPhone = ConfigurationSettings.AppSettings["ShipFromPhone"].ToString();

            SV.Framework.LabelGenerator.PurchaseOrderInfo poInfo = new SV.Framework.LabelGenerator.PurchaseOrderInfo();
            SV.Framework.LabelGenerator.ProductModel productModel = null;
            List<SV.Framework.LabelGenerator.ProductModel> productList = null;
            SV.Framework.LabelGenerator.PackingSlipOperation operation = new SV.Framework.LabelGenerator.PackingSlipOperation();
            avii.Classes.UserInfo objUserInfo = (avii.Classes.UserInfo)Session["userInfo"];
            int poID = 0;
            try
            {
                if (ViewState["poid"] != null)
                {
                    poID = Convert.ToInt32(ViewState["poid"]);
                }
                PurchaseOrders purchaseOrders = null;
                if (Session["POS"] != null)
                    purchaseOrders = Session["POS"] as PurchaseOrders;

                List<BasePurchaseOrderItem> purchaseOrderItemList = new List<BasePurchaseOrderItem>();
                List<BasePurchaseOrder> purchaseOrderList = purchaseOrders.PurchaseOrderList;
                if (Session["poitems"] != null)
                    purchaseOrderItemList = Session["poitems"] as List<BasePurchaseOrderItem>;
                if (poID > 0 && purchaseOrders != null && purchaseOrderItemList != null && purchaseOrderItemList.Count > 0)
                {

                    var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(poID) select item).ToList();
                    if (poInfoList != null && poInfoList.Count > 0)
                    {
                        productList = new List<SV.Framework.LabelGenerator.ProductModel>();

                        poInfo.PurchaseOrderNumber = poInfoList[0].CustomerOrderNumber;
                        poInfo.SalesOrder = poInfoList[0].CustomerOrderNumber;
                        poInfo.DocumentDate = poInfoList[0].PurchaseOrderDate.ToString("MM-dd-yyyy");
                        poInfo.ReqShippingDate = poInfoList[0].RequestedShipDate.ToString("MM-dd-yyyy");
                        //Ship From

                        //if (poInfoList[0].CompanyLogo == "redlogo.png")
                        //    poInfo.CompanyName = shipFromContactName2;
                        //else
                        //    poInfo.CompanyName = shipFromContactName;

                        poInfo.CompanyName = poInfoList[0].CustomerName;

                        poInfo.AddressLine1 = shipFromAddress;
                        poInfo.AddressLine2 = "";
                        poInfo.City = shipFromCity;
                        poInfo.State = shipFromState;
                        poInfo.ZipCode = shipFromZip;
                        poInfo.Country = shipFromCountry;
                        //LOGO
                        poInfo.CompanyLogo = Server.MapPath("~/img/" + poInfoList[0].CompanyLogo.Replace(".", "2."));
                        // poInfo.CompanyLogo = Server.MapPath("~/img/fplogo2.png");
                        //Ship To
                        poInfo.CustomerName = poInfoList[0].Shipping.ContactName;
                        poInfo.ShippingAddressLine1 = poInfoList[0].Shipping.ShipToAddress;
                        poInfo.ShippingAddressLine2 = poInfoList[0].Shipping.ShipToAddress2;
                        poInfo.ShippingCity = poInfoList[0].Shipping.ShipToCity;
                        poInfo.ShippingState = poInfoList[0].Shipping.ShipToState;
                        poInfo.ShippingZipCode = poInfoList[0].Shipping.ShipToZip;
                        poInfo.ShippingCountry = "USA";
                        poInfo.ShippingMethod = poInfoList[0].Tracking.ShipToBy;
                        poInfo.CustomerId = poInfoList[0].StoreID;
                        poInfo.DateTimePrinted = DateTime.Now.ToString("MM-dd-yyyy hh:mm tt");
                        poInfo.PackingSlip = "pck" + poID.ToString();
                        poInfo.Page = 1;

                        if (objUserInfo != null)
                            poInfo.WhoPrinted = objUserInfo.UserName;
                        foreach (BasePurchaseOrderItem poItem in purchaseOrderItemList)
                        {
                            productModel = new SV.Framework.LabelGenerator.ProductModel();
                            productModel.Description = poItem.ItemCode;
                            productModel.ItemNumber = poItem.ItemCode;
                            productModel.QtyShipped = Convert.ToInt32(poItem.Quantity);
                            productModel.UnitsBO = productModel.QtyShipped;
                            productModel.UnitsOrdered = productModel.QtyShipped;
                            productModel.UnitsShipped = productModel.QtyShipped;
                            productList.Add(productModel);
                        }
                        poInfo.ProductsList = productList;
                    }

                    var memSt = operation.ExportToPDF(poInfo);
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


                        packingslipOperation.PackingSlipInsertUpdate(poID, poInfo.PackingSlip);
                    }
                    else
                        lblViewPO.Text = "Technical error!";

                }
            }
            catch (Exception ex)
            {
                lblViewPO.Text = ex.Message;
            }

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }

        protected void btnContainerSlip_Click(object sender, EventArgs e)
        {
            ContainerSlipOperation containerSlipOperation = ContainerSlipOperation.CreateInstance<ContainerSlipOperation>();
            
            SV.Framework.LabelGenerator.ContainerLabelOperation operation = new SV.Framework.LabelGenerator.ContainerLabelOperation();
            avii.Classes.UserInfo objUserInfo = (avii.Classes.UserInfo)Session["userInfo"];
            lblViewPO.Text = string.Empty;
            bool IsContainerExists = true;
            int poID = 0;
            string errorMessage = string.Empty;
            try
            {
                if (ViewState["poid"] != null)
                {
                    poID = Convert.ToInt32(ViewState["poid"]);
                }

                List<SV.Framework.LabelGenerator.ContainerModel> containers = new List<SV.Framework.LabelGenerator.ContainerModel>();
                SV.Framework.LabelGenerator.ContainerModel container = null;
                List<ContainerModel> containerLabels = containerSlipOperation.GetContainerLabelInfo(poID);
                if (containerLabels != null && containerLabels.Count > 0)
                {
                    foreach (ContainerModel item in containerLabels)
                    {
                        container = new SV.Framework.LabelGenerator.ContainerModel();

                        container.AddressLine1 = item.AddressLine1;
                        container.AddressLine2 = item.AddressLine2;
                        container.ShippingAddressLine1 = item.ShippingAddressLine1;
                        container.ShippingAddressLine2 = item.ShippingAddressLine2;
                        container.ShippingCity = item.ShippingCity;
                        container.ShippingCountry = item.ShippingCountry;
                        container.ShippingState = item.ShippingState;
                        container.ShippingZipCode = item.ShippingZipCode;
                        container.State = item.State;
                        container.ESNCount = item.ESNCount;
                        container.Carrier = item.Carrier;
                        container.Casepack = item.Casepack;
                        container.City = item.City;
                        container.CompanyName = item.CompanyName;
                        container.ContainerCount = item.ContainerCount;
                        container.ContainerNumber = item.ContainerNumber;
                        container.Country = item.Country;
                        container.CustomerName = item.CustomerName;
                        container.DPCI = item.DPCI;
                        container.PoNumber = item.PoNumber;
                        container.PostalCode = item.PostalCode;
                        container.ZipCode = item.ZipCode;
                        containers.Add(container);

                        if (item.ContainerCount == "0" && item.ESNCount == "0")
                        {
                            IsContainerExists = false;
                            errorMessage = "Please generate Container ID and complete provisioning first!";
                            lblViewPO.Text = "Please generate Container ID and complete provisioning first!";

                        }
                        else if (item.ContainerCount == "0")
                        {
                            IsContainerExists = false;
                            errorMessage = "Container ID not generated yet!";
                            lblViewPO.Text = "Container ID not generated yet!";
                            //return;
                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "alert('Container ID not generated yet!')", false);
                        }
                        else if (item.ESNCount == "0")
                        {
                            IsContainerExists = false;
                            errorMessage = "Provisioning is not done yet!";
                            lblViewPO.Text = "Provisioning is not done yet!";

                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Provisioning is not done yet!')</script>", false);
                            //    lblViewPO.Text = "Provisioning is not done yet!";
                        }
                    }
                    if (IsContainerExists)
                    {

                        string customerName = "";
                        if(ViewState["CustomerName"] != null)
                        {
                            customerName = ViewState["CustomerName"] as string;
                        }
                        if (customerName == "DISH")
                        {
                            List<SV.Framework.LabelGenerator.MasterCartonInfo> cartons = default; // new List<SV.Framework.LabelGenerator.MasterCartonInfo>();
                            SV.Framework.LabelGenerator.MasterCartonInfo masterCartonInfo = default;
                            List<SV.Framework.LabelGenerator.CartonItem> CartonItems = default;
                            SV.Framework.LabelGenerator.CartonItem cartonItem = default;
                            List<MasterCartonInfo> cartonsdb = dishLabelOperations.GetMasterCartonLabelInfo(poID);
                            SV.Framework.LabelGenerator.DishLabelOperation dishLabelOperation = new SV.Framework.LabelGenerator.DishLabelOperation();
                            SV.Framework.LabelGenerator.H3LabelOperation h3LabelOperation = new SV.Framework.LabelGenerator.H3LabelOperation();
                            SV.Framework.LabelGenerator.H5LabelOperation h5LabelOperation = new SV.Framework.LabelGenerator.H5LabelOperation();

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
                                MemoryStream memSt = null;// = new MemoryStream();

                                string ProductType = cartonsdb[0].ProductType;
                                if (ProductType.ToUpper() == "H5")
                                {
                                    memSt = h5LabelOperation.ExportMasterCartonToPDFNew(cartons);
                                    
                                }
                                else
                                {
                                    if (cartons != null && cartons[0].OSType.ToUpper() == "ANDROID")
                                    {
                                        memSt = h3LabelOperation.ExportMasterCartonToPDFNew(cartons);
                                        //if (memSt != null)
                                        //{
                                        //    string fileType = ".pdf";
                                        //    string filename = DateTime.Now.Ticks + fileType;
                                        //    Response.Clear();
                                        //    //Response.ContentType = "application/pdf";
                                        //    Response.ContentType = "application/octet-stream";
                                        //    Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                                        //    Response.Buffer = true;
                                        //    Response.Clear();
                                        //    var bytes = memSt.ToArray();
                                        //    Response.OutputStream.Write(bytes, 0, bytes.Length);
                                        //    Response.OutputStream.Flush();
                                        //}
                                    }
                                    else
                                    {

                                        memSt = dishLabelOperation.ExportMasterCartonToPDFNew(cartons);

                                        //if (memSt != null)
                                        //{
                                        //    string fileType = ".pdf";
                                        //    string filename = DateTime.Now.Ticks + fileType;
                                        //    Response.Clear();
                                        //    //Response.ContentType = "application/pdf";
                                        //    Response.ContentType = "application/octet-stream";
                                        //    Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                                        //    Response.Buffer = true;
                                        //    Response.Clear();
                                        //    var bytes = memSt.ToArray();
                                        //    Response.OutputStream.Write(bytes, 0, bytes.Length);
                                        //    Response.OutputStream.Flush();
                                        //}
                                    }
                                    
                                }
                                
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
                                lblViewPO.Text = "No record found!";
                        }
                        else
                        {
                            var memSt = operation.ExportToPDF(containers);
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


                                //PackingslipOperation.PackingSlipInsertUpdate(poID, poInfo.PackingSlip);
                            }
                            else
                                lblViewPO.Text = "Technical error!";
                        }
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('"+ errorMessage + "')</script>", false);

                }
                else
                {
                    lblViewPO.Text = "Provisioning is not done yet!";
                }
            }
            catch (Exception ex)
            {
                lblViewPO.Text = ex.Message;
            }

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }
        protected void imgDelTracking_Command(object sender, CommandEventArgs e)
        {
            TrackingOperations trackingOperations = TrackingOperations.CreateInstance<TrackingOperations>();

            int poID = Convert.ToInt32(ViewState["poid"]);
            int lineNumber = Convert.ToInt32(e.CommandArgument);
            int count = 0;
            int userID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
            }
            try
            {
                List<TrackingDetail> trackingList = trackingOperations.FulfillmentTrackingDelete(lineNumber, poID, "W", userID, out count);
                if (count == 1)
                {
                    lblViewPO.Text = "Fulfillment Order Tracking can not be deleted";
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order Tracking is deleted successfully');</script>");

                }
                else
                {
                    gvTracking.DataSource = trackingList;
                    gvTracking.DataBind();
                    Session["potracking"] = trackingList;
                    // ReloadPOLineItems();

                    lblViewPO.Text = "Fulfillment Order Tracking is deleted successfully";
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order Tracking is deleted successfully');</script>");


                }
            }
            catch (Exception ex)
            {
                lblViewPO.Text = ex.Message;
            }

        }
        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblViewPO.Text = string.Empty;
            GridView gvTemp = (GridView)sender;
            string podId = (string)gvTemp.DataKeys[e.RowIndex].Value.ToString();

            //Prepare the Update Command of the DataSource control
            //string strSQL = "";
            int userID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
            }
            try
            {
                int po_id = Convert.ToInt32(ViewState["poid"]);

                int returnValue = purchaseOrderOperation.DeletePurchaseOrderDetail(Convert.ToInt32(podId), userID);
                if (returnValue > 1)
                {
                    List<BasePurchaseOrderItem> purchaseOrderItemList = purchaseOrderOperation.GetPurchaseOrderItemList(po_id);
                    Session["poitems"] = purchaseOrderItemList;
                    gvPODetail.DataSource = purchaseOrderItemList;
                    gvPODetail.DataBind();

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order deleted successfully');</script>", false);
                    lblViewPO.Text = "Purchase Order deleted successfully";
                }
                else
                {
                    if (returnValue == -1)
                        lblViewPO.Text = "Purchase Order item can not be deleted it must be in panding or processed state";
                    else
                        lblViewPO.Text = "Purchase Order item can not be deleted there must be atleast one item";
                }
                //BindPO();

            }
            catch { }
        }
        private void BindPOItems()
        {
            List<BasePurchaseOrderItem> purchaseOrderItemList = (List<BasePurchaseOrderItem>)Session["poitems"];
            gvPODetail.DataSource = purchaseOrderItemList;// ChildDataSourcebyPODID(Convert.ToInt32(gvTemp.DataKeys[gvEditIndex].Value), string.Empty);
            gvPODetail.DataBind();
        }
        protected void gvPODetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string esn, msl, fmupc, mdn, msid, lteICCid, lteIMSI, akey, otksl, simnumber, sQty;
            esn = msl = fmupc = mdn = msid = lteICCid = lteIMSI = akey = otksl = simnumber = sQty = string.Empty;
            lblViewPO.Text = string.Empty;
            int userID = 0, qty = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;

            }
            try
            {
                GridView gvTemp = (GridView)sender;
                gvUniqueID = gvTemp.UniqueID;

                //Get the values stored in the text boxes
                int podID = Convert.ToInt32(gvTemp.DataKeys[e.RowIndex].Value);
                bool lteICC = false;

                bool lte_IMSI = false;
                bool iSSim = false;

                string returnMessage = string.Empty;
                sQty = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtQty")).Text.Trim();

                int.TryParse(sQty, out qty);
                if (qty > 0)
                    purchaseOrderOperation.PurchaseOrderUpdateDetailNew(podID, qty, userID, out returnMessage);
                else
                    returnMessage = "Quantity can not be 0!";
                //if (Session["adm"] != null)
                //{
                //    esn = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtEsn")).Text.Trim();
                //    msl = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMslNumber")).Text.Trim();
                //    //fmupc = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtFMUPC")).Text.Trim();
                //    mdn = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMdn")).Text.Trim();
                //    msid = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMsid")).Text.Trim();

                //    TextBox txtLTEICCID = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtLTEICCID"));
                //    TextBox txtLTEIMSI = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtLTEIMSI"));
                //    TextBox txtAkey = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtAkey"));
                //    TextBox txtOTKSL = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtOTKSL"));
                //    TextBox txtSim = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtSim"));
                //    if (txtLTEICCID != null)
                //    {
                //        lteICC = txtLTEICCID.Visible;
                //        lteICCid = txtLTEICCID.Text.Trim();
                //    }
                //    if (txtLTEIMSI != null)
                //    {
                //        lte_IMSI = txtLTEIMSI.Visible;
                //        lteIMSI = txtLTEIMSI.Text.Trim();
                //    }
                //    if (txtAkey != null)
                //    {
                //        akey = txtAkey.Text.Trim();
                //    }
                //    if (txtOTKSL != null)
                //    {
                //        //lte_IMSI = txtLTEIMSI.Visible;
                //        otksl = txtOTKSL.Text.Trim();
                //    }
                //    if (txtSim != null)
                //    {
                //        iSSim = txtSim.Visible;
                //        simnumber = txtSim.Text.Trim();
                //    }

                //    //if (lteICC == true && lteICCid == string.Empty && lteICC == true && lteICCid == string.Empty)
                //    //{
                //    //    lblViewPO.Text = "LTE ICC id & LTE IMSI required!";
                //    //    return;
                //    //}
                //    //if (lteICC == true && lteICCid == string.Empty)
                //    //{
                //    //    lblViewPO.Text = "LTE ICC id required!";
                //    //    return;
                //    //}
                //    //if (lte_IMSI == true && lteIMSI == string.Empty)
                //    //{
                //    //    lblViewPO.Text = "LTE IMSI required!";
                //    //    return;
                //    //}
                //    //if (iSSim && string.IsNullOrEmpty(simnumber))
                //    //{
                //    //    lblViewPO.Text = "Sim is required!";
                //    //    return;
                //    //}

                //    ////lteICCid = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtLTEICCID")).Text.Trim();
                //    ////lteIMSI = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtLTEIMSI")).Text.Trim();

                //    PurchaseOrder.PurchaseOrderUpdateDetail(podID, esn, msl, msid, mdn, null, fmupc, 1, userID, lteICCid, lteIMSI, akey, otksl, simnumber, out returnMessage);
                //}
                //else
                //{
                //    mdn = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMdn")).Text.Trim();
                //    msid = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMsid")).Text.Trim();

                //    PurchaseOrder.PurchaseOrderUpdateDetail(podID, null, null, msid, mdn, null, null, 1, userID, null, null, null, null, null, out returnMessage);
                //}
                if (!string.IsNullOrEmpty(returnMessage))
                {
                    lblViewPO.Text = returnMessage;
                    return;
                }
                gvTemp.EditIndex = -1;
                List<BasePurchaseOrderItem> purchaseOrderList = (List<BasePurchaseOrderItem>)Session["poitems"];

                //List<BasePurchaseOrderItem> purchaseOrderList = purchaseOrderItemList;//ChildDataSourcebyPODID(podID, null);
                if (qty > 0)
                {
                    if (purchaseOrderList != null && purchaseOrderList.Count > 0)
                    {
                        foreach (BasePurchaseOrderItem pitem in purchaseOrderList)
                        {
                            if (pitem.PodID == podID)
                            {
                                pitem.Quantity = qty;
                                //pitem.ESN = esn;
                                //pitem.MslNumber = msl;
                                //pitem.FmUPC = fmupc;
                                //pitem.MdnNumber = mdn;
                                //pitem.MsID = msid;
                                //pitem.LTEICCID = lteICCid;
                                //pitem.LTEIMSI = lteIMSI;
                                //pitem.SimNumber = simnumber;
                            }
                        }
                    }
                }

                gvTemp.DataSource = purchaseOrderList;
                gvTemp.DataBind();
                lblViewPO.Text = "Purchase Order Detail is updated successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order Detail is updated successfully');</script>");
                //GridView1.EditIndex = -1;
                //}
            }
            catch { }
        }

        protected void gvPODetail_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            //Check if there is any exception while deleting
            if (e.Exception != null)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");
                e.ExceptionHandled = true;
            }
        }

        protected void gvPODetail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //GridView gvTemp = (GridView)sender;
            //gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = e.NewEditIndex;
            gvPODetail.EditIndex = gvEditIndex;
            BindPOItems();
        }
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //GridView gvTemp = (GridView)sender;
            //gvUniqueID = gvTemp.UniqueID;
            //gvEditIndex = e.NewEditIndex;
            //gvTemp.EditIndex = gvEditIndex;
            //gvTemp.DataSource = ChildDataSourcebyPODID(Convert.ToInt32(gvTemp.DataKeys[gvEditIndex].Value), string.Empty);
            //gvTemp.DataBind();
        }
        protected void GridView2_CancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //GridView gvTemp = (GridView)sender;
            //gvUniqueID = gvTemp.UniqueID;
            //gvEditIndex = -1;
            ////ds = (DataSet)Session["PO"];
            //GridView1.DataSource = ((PurchaseOrders)Session["POS"]).PurchaseOrderList;

            //GridView1.DataBind();
        }
        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //string esn, msl, fmupc, mdn, msid;
            //esn = msl = fmupc = mdn = msid = string.Empty;
            //try
            //{
            //    GridView gvTemp = (GridView)sender;
            //    gvUniqueID = gvTemp.UniqueID;

            //    //Get the values stored in the text boxes
            //    int podID = Convert.ToInt32(gvTemp.DataKeys[e.RowIndex].Value);

            //    if (Session["adm"] != null)
            //    {
            //        esn = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtEsn")).Text.Trim();
            //        msl = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMslNumber")).Text.Trim();
            //        fmupc = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtFMUPC")).Text.Trim();
            //        mdn = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMdn")).Text.Trim();
            //        msid = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMsid")).Text.Trim();

            //        PurchaseOrder.PurchaseOrderUpdateDetail(podID, esn, msl, msid, mdn, null, fmupc);
            //    }
            //    else
            //    {
            //        mdn = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMdn")).Text.Trim();
            //        msid = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMsid")).Text.Trim();

            //        PurchaseOrder.PurchaseOrderUpdateDetail(podID, null, null, msid, mdn, null, null);
            //    }

            //    gvTemp.EditIndex = -1;
            //    List<BasePurchaseOrderItem> purchaseOrderList = ChildDataSourcebyPODID(podID, null);
            //    if (purchaseOrderList != null && purchaseOrderList.Count > 0)
            //    {
            //        foreach (BasePurchaseOrderItem pitem in purchaseOrderList)
            //        {
            //            if (pitem.PodID == podID)
            //            {
            //                pitem.ESN = esn;
            //                pitem.MslNumber = msl;
            //                pitem.FmUPC = fmupc;
            //                pitem.MdnNumber = mdn;
            //                pitem.MsID = msid;
            //            }
            //        }
            //    }

            //    gvTemp.DataSource = purchaseOrderList;
            //    gvTemp.DataBind();
            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order Detail is updated successfully');</script>");
            //    GridView1.EditIndex = -1;
            //    //}
            //}
            //catch { }
        }
        private void PrintLabel(int lineNumber)
        {
            try
            {
                ShippingLabelOperation shippingLabelOperation = ShippingLabelOperation.CreateInstance<ShippingLabelOperation>();

                float width = 320, height = 520, envHeight = 500;

                string customerName="";
                if (ViewState["CustomerName"] != null)
                {
                    customerName = ViewState["CustomerName"] as string;
                }
                if (customerName == "FONUS")
                {
                    width = 500;
                    height = 320;
                    envHeight = 320;

                }

                string shipMethod = string.Empty, ShipPackage = string.Empty;
                string labelBase64 = shippingLabelOperation.GetLabelBase64(lineNumber, out shipMethod, out ShipPackage);
                if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), shipMethod))
                {
                    if (!string.IsNullOrWhiteSpace(labelBase64))
                    {
                        //  imgLabel.ImageUrl = "data:image;base64," + labelBase64;
                        //RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");

                        byte[] imageBytes = Convert.FromBase64String(labelBase64);
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);

                        if (shipMethod.ToLower() == "first" && ShipPackage.ToString().ToLower() == "letter")
                        {
                            width = 500;
                            height = 320;
                            envHeight = 320;
                        }
                        image.ScaleToFit(width, height);

                        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                        {
                            Document document = new Document();
                            iTextSharp.text.Rectangle envelope = new iTextSharp.text.Rectangle(width, envHeight);
                            document.SetPageSize(envelope);
                            document.SetMargins(0, 0, 0, 0);

                            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                            document.Open();

                            document.Add(image);
                            document.Close();
                            var bytes = memoryStream.ToArray();
                            // memoryStream.Close();

                            string fileType = ".pdf";
                            string filename = DateTime.Now.Ticks + fileType;
                            Response.Clear();
                            //Response.ContentType = "application/pdf";
                            Response.ContentType = "application/octet-stream";
                            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                            Response.Buffer = true;
                            Response.Clear();
                            // var bytes = memSt.ToArray();
                            Response.OutputStream.Write(bytes, 0, bytes.Length);
                            Response.OutputStream.Flush();
                        }
                    }
                }
                else
                {
                    // RegisterStartupScript("jsUnblockDialog", "closeLabelDialog();");
                    if (!string.IsNullOrWhiteSpace(labelBase64))
                    {
                        //var script = "OpenPDF('" + labelBase64 + "')"; //"window.open('" + pdfByteArray + "', '_blank');";
                        //ScriptManager.RegisterClientScriptBlock(Parent.Page, typeof(Page), "pdf", script, true);
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenPDF('" + labelBase64 + "')</script>", false);
                        // imgLabel.ImageUrl = "data:application/pdf;base64," + labelBase64;
                        //  RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");
                        //data: application/pdf; base64,
                        string fileType = ".pdf";
                        string filename = DateTime.Now.Ticks + fileType;
                        Response.Clear();
                        //Response.ContentType = "application/pdf";
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                        Response.Buffer = true;
                        Response.Clear();
                        var bytes = Convert.FromBase64String(labelBase64);
                        Response.OutputStream.Write(bytes, 0, bytes.Length);
                        Response.OutputStream.Flush();
                        // Response.End();

                    }
                }


            }
            catch (Exception ex)
            {
                lblViewPO.Text = ex.Message;
            }
        }

        protected void btnhdPrintlabel_Click(object sender, EventArgs e)
        {

            if (ViewState["linenumber"] != null)
            {
                PrintLabel(Convert.ToInt32(ViewState["linenumber"]));
            }
        }
        protected void gvPODetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ViewState["IsReadOnly"] != null)
                {
                    ImageButton imgDelPoD = (ImageButton)e.Row.FindControl("imgDelPoD");
                    if (imgDelPoD != null)
                        imgDelPoD.Visible = false;
                }

                if (e.Row.RowIndex >= 0)
                {
                    

                    //imgEditPOD.OnClientClick = "openEditPODDialogAndBlock('Edit Fulfillment Detail', '" + imgEditPOD.ClientID + "')";

                    //if (Session["adm"] == null)
                    {
                        // ImageButton imgDelPoD = (ImageButton)e.Row.FindControl("imgDelPoD");
                        // HiddenField hdnStatus = (HiddenField)e.Row.FindControl("hdnStatus");
                        //if (e.Row.RowState == DataControlRowState.Edit)
                        //{
                        //    TextBox txtESN = (TextBox)e.Row.FindControl("txtEsn");
                        //    TextBox txtMslNumber = (TextBox)e.Row.FindControl("txtMslNumber");
                        //    TextBox txtMsid = (TextBox)e.Row.FindControl("txtMsid");


                        //    TextBox txtLTEICCID = (TextBox)e.Row.FindControl("txtLTEICCID");
                        //    TextBox txtLTEIMSI = (TextBox)e.Row.FindControl("txtLTEIMSI");
                        //    TextBox txtAkey = (TextBox)e.Row.FindControl("txtAkey");
                        //    TextBox txtOTKSL = (TextBox)e.Row.FindControl("txtOTKSL");
                        //    TextBox txtSim = (TextBox)e.Row.FindControl("txtSim");
                        //    if (txtLTEICCID != null && txtLTEIMSI != null && txtAkey != null && txtOTKSL != null && txtSim != null)
                        //    {

                        //        txtESN.Visible = false;
                        //        txtMslNumber.Visible = false;
                        //        txtMsid.Visible = false;

                        //        txtLTEICCID.Visible = false;
                        //        txtLTEIMSI.Visible = false;
                        //        txtAkey.Visible = false;
                        //        txtOTKSL.Visible = false;
                        //        txtSim.Visible = false;
                        //    }
                        //}
                        //if (hdnStatus != null && hdnStatus.Value != string.Empty)
                        //{
                        //    int statusID = Convert.ToInt32(hdnStatus.Value);
                        //    //ImageButton imgEditOrder = (ImageButton)e.Row.FindControl("imgEditOrder");
                        //    //ImageButton imgDelPo = (ImageButton)e.Row.FindControl("imgDelPo");
                        //    if (statusID > 1)
                        //    {
                        //        LinkButton obj = (LinkButton)e.Row.Cells[14].Controls[0];
                        //        if (obj != null)
                        //        {
                        //            obj.Enabled = false;
                        //            obj.Visible = false;
                        //        }
                        //        //CommandField EditPOD = e.Row.FindControl("EditPOD")
                        //        imgDelPoD.Visible = false;

                        //        e.Row.Cells[14].Enabled = false;
                        //        //imgDelPoD.Visible = false;
                        //    }
                        //}
                    }
                }
            }
        }

        protected void GridView2_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            //Check if there is any exception while deleting
            if (e.Exception != null)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");
                e.ExceptionHandled = true;
            }
        }

        protected void gvPODetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPODetail.PageIndex = e.NewPageIndex;
            if (Session["poitems"] != null)
            {
                List<BasePurchaseOrderItem> purchaseOrderItemList = (List<BasePurchaseOrderItem>)Session["poitems"];
                gvPODetail.DataSource = purchaseOrderItemList;
                gvPODetail.DataBind();
            }
        }

        protected void GridView2_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            //Check if there is any exception while deleting
            if (e.Exception != null)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");
                e.ExceptionHandled = true;
            }
        }

        protected void imgPrint_Command(object sender, CommandEventArgs e)
        {
            int lineNumber = Convert.ToInt32(e.CommandArgument);
            ViewState["linenumber"] = lineNumber;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp1", "<script language='javascript'>Refresh();</script>", false);

            //btnhdPrintlabel.Click += new EventHandler(btnhdPrintlabel_Click);

            //try
            //{

            //    string shipMethod = string.Empty;
            //    string labelBase64 = ShippingLabelOperation.GetLabelBase64(lineNumber, out shipMethod);
            //    if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), shipMethod))
            //    {
            //        if (!string.IsNullOrWhiteSpace(labelBase64))
            //        {
            //            imgLabel.ImageUrl = "data:image;base64," + labelBase64;
            //            RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");




            //            //byte[] imageBytes = Convert.FromBase64String(labelBase64);
            //            //iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);

            //            //using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            //            //{
            //            //    Document document = new Document(PageSize.LEGAL, 1f, 1f, 1f, 1f);
            //            //    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            //            //    document.Open();
            //            //    document.Add(image);
            //            //    document.Close();
            //            //    byte[] bytes = memoryStream.ToArray();
            //            //    // memoryStream.Close();

            //            //    string fileType = ".pdf";
            //            //    string filename = DateTime.Now.Ticks + fileType;
            //            //    Response.Clear();
            //            //    //Response.ContentType = "application/pdf";
            //            //    Response.ContentType = "application/octet-stream";
            //            //    Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            //            //    Response.Buffer = true;
            //            //    Response.Clear();
            //            //    // var bytes = memSt.ToArray();
            //            //    Response.OutputStream.Write(bytes, 0, bytes.Length);
            //            //    Response.OutputStream.Flush();


            //                // Response.End();


            //                //Response.Clear();
            //                ////Response.ContentType = "application/pdf";
            //                //Response.ContentType = "application/octet-stream";
            //                //Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            //                //Response.Buffer = true;
            //                //Response.Clear();
            //                //var bytes = Convert.FromBase64String(labelBase64);
            //                //Response.OutputStream.Write(bytes, 0, bytes.Length);
            //                //Response.OutputStream.Flush()
            //            //}
            //        }
            //    }
            //    else
            //    {
            //        // RegisterStartupScript("jsUnblockDialog", "closeLabelDialog();");
            //        if (!string.IsNullOrWhiteSpace(labelBase64))
            //        {
            //            //var script = "OpenPDF('" + labelBase64 + "')"; //"window.open('" + pdfByteArray + "', '_blank');";
            //            //ScriptManager.RegisterClientScriptBlock(Parent.Page, typeof(Page), "pdf", script, true);
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenPDF('" + labelBase64 + "')</script>", false);
            //            // imgLabel.ImageUrl = "data:application/pdf;base64," + labelBase64;
            //            //  RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");
            //            //data: application/pdf; base64,
            //            //string fileType = ".pdf";
            //            //string filename = DateTime.Now.Ticks + fileType;
            //            //Response.Clear();
            //            ////Response.ContentType = "application/pdf";
            //            //Response.ContentType = "application/octet-stream";
            //            //Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            //            //Response.Buffer = true;
            //            //Response.Clear();
            //            //var bytes = Convert.FromBase64String(labelBase64);
            //            //Response.OutputStream.Write(bytes, 0, bytes.Length);
            //            //Response.OutputStream.Flush();
            //            //Response.End();

            //        }
            //    }


            //}
            //catch (Exception ex)
            //{
            //    lblViewPO.Text = ex.Message;
            //}
        }
        protected void gvTracking_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (ViewState["IsInterNational"] != null)
                //{
                //    bool IsInterNational = Convert.ToBoolean(ViewState["IsInterNational"]);
                //    if (!IsInterNational)
                //        e.Row.Cells[8].Visible = false;

                //}
                if (e.Row.RowIndex >= 0)
                {
                    HiddenField hdTN = (HiddenField)e.Row.FindControl("hdTN");
                    HiddenField hdShipVia = (HiddenField)e.Row.FindControl("hdShipVia");

                    //LinkButton lnkESN = (LinkButton)e.Row.FindControl("lnkESN");
                    //lnkESN.OnClientClick = "openESNDialogAndBlock('ESN LIST', '" + lnkESN.ClientID + "')";

                    //ImageButton imgEditTr = (ImageButton)e.Row.FindControl("imgEditTr");
                    //imgEditTr.OnClientClick = "openAddTrackingDialogAndBlock('Edit Tracking', '" + imgEditTr.ClientID + "')";
                    //ImageButton imgLabl = (ImageButton)e.Row.FindControl("imgLabl");

                    ImageButton imgPrint = (ImageButton)e.Row.FindControl("imgPrint");

                    // ScriptManager.GetCurrent(this).RegisterPostBackControl(imgPrint);
                    if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), hdShipVia.Value))
                    {
                        //AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
                        //trigger.ControlID = imgPrint.ClientID;
                        //trigger.EventName = "Click";
                        //upnlView.Triggers.Add(trigger);
                        // imgPrint.OnClientClick = "openLabelDialogAndBlock('Shipping Label', '" + imgPrint.ClientID + "')";

                    }
                    if (string.IsNullOrEmpty(hdTN.Value))
                    {

                        imgPrint.Visible = false;

                    }
                   // else
                     //   imgLabl.Visible = false;

                    if (Session["adm"] == null)
                    {
                        ImageButton imgDelTr = (ImageButton)e.Row.FindControl("imgDelTr");
                        //LinkButton obj = (LinkButton)e.Row.Cells[5].Controls[0];
                        //if (obj != null)
                        //{
                        //    obj.Enabled = false;
                        //    obj.Visible = false;
                        //}

                        imgDelTr.Visible = false;
                       // imgEditTr.Visible = false;


                    }
                }
            }
        }

        protected void btnTracking_Click(object sender, EventArgs e)
        {
            //lblViewPO.Text = string.Empty;
            //txtTrackings.Text = string.Empty;
            //txtTrComment.Text = string.Empty;
            //dpShipBy.SelectedIndex = 0;
            ////ddlReturn.SelectedIndex = 0;
            //lblReturn.Text = "Returned";

            //ViewState["linenumber"] = null;
            //RegisterStartupScript("jsUnblockDialog", "unblockAddTrackingDialog();");

        }
        //protected void btnAddTrackings_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int poID = Convert.ToInt32(ViewState["poid"]);
        //        int userID = 0;
        //        int returnCount = 0;
        //        int linenumber = 0;

        //        string trackingNumber, returnLabel, comment, returnMessage;
        //        int shipByID = Convert.ToInt32(dpShipBy.SelectedValue);
        //        List<TrackingDetail> trackingList = new List<TrackingDetail>();

        //        trackingNumber = returnLabel = comment = returnMessage = string.Empty;
        //        trackingNumber = txtTrackings.Text.Trim();
        //        comment = txtTrComment.Text.Trim();

        //        //if (ddlReturn.SelectedIndex > 0)
        //        //only return label


        //        //else
        //        //    if (ddlReturn.SelectedIndex == 2)
        //        //        returnLabel = "R";

        //        UserInfo userInfo = Session["userInfo"] as UserInfo;
        //        if (userInfo != null)
        //        {
        //            userID = userInfo.UserGUID;
        //        }

        //        if (Session["potracking"] != null)
        //            trackingList = (List<TrackingDetail>)Session["potracking"];

        //        //if (trackingList != null && trackingList.Count > 0)
        //        //{
        //        //    var item = trackingList.Max(x => x.LineNumber);
        //        //    linenumber = item + 1;
        //        //}
        //        //else
        //        //    linenumber = 1;

        //        if (ViewState["linenumber"] != null)
        //        {
        //            linenumber = Convert.ToInt32(ViewState["linenumber"]);
        //            int index = trackingList.FindIndex(x => x.LineNumber.Equals(linenumber));
        //            returnLabel = trackingList[index].ReturnValue;
        //            trackingList[index].ReturnValue = returnLabel;
        //            trackingList[index].ShipByID = shipByID;
        //            trackingList[index].TrackingNumber = trackingNumber;
        //            trackingList[index].Comments = comment;
        //            trackingList[index].ReturnValue = returnLabel;

        //        }
        //        else
        //        {
        //            returnLabel = "R";
        //            TrackingDetail tracking = new TrackingDetail();
        //            tracking.ShipByID = shipByID;
        //            tracking.ReturnValue = returnLabel;
        //            tracking.TrackingNumber = trackingNumber;
        //            tracking.Comments = comment;
        //            tracking.LineNumber = linenumber;
        //            trackingList.Add(tracking);
        //        }
        //        if (trackingList != null && trackingList.Count > 0)
        //        {
        //            trackingList = TrackingOperations.FulfillmentTrackingUpdate(trackingList, poID, "W", PurchaseOrderStatus.Shipped, userID, out returnCount, out returnMessage);
        //            gvTracking.DataSource = trackingList;
        //            gvTracking.DataBind();
        //            Session["potracking"] = trackingList;
        //            ReloadPOLineItems();

        //            lblViewPO.Text = "Submitted successfully";


        //            RegisterStartupScript("jsUnblockDialog", "closeAddTrackingDialog();");
        //            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order Tracking is updated successfully');</script>");

        //        }
        //        //gvTracking.DataSource = trackingList;
        //        //gvTracking.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblViewPO.Text = ex.Message;
        //    }


        //}
        ////protected void lnkTracking_Command(object sender, CommandEventArgs e)
        //{
        //    lblESN.Text = string.Empty;
        //    lblTracking.Text = string.Empty;
        //    lblViewPO.Text = "";
        //    try
        //    {
        //        int poID = 0;
        //        if (ViewState["poid"] != null)
        //        {
        //            poID = Convert.ToInt32(ViewState["poid"]);

        //            string trackingNumber = Convert.ToString(e.CommandArgument);
        //            lblTracking.Text = trackingNumber;
        //            List<EsnList> esnList = FulfillmentOperations.GetTrackingESNList(poID, trackingNumber);

        //            if (esnList != null && esnList.Count > 0)
        //            {
        //                rptESN.DataSource = esnList;
        //                rptESN.DataBind();
        //            }
        //            else
        //            {
        //                rptESN.DataSource = null;
        //                rptESN.DataBind();
        //                lblESN.Text = "No records found";
        //            }

        //        }




        //        RegisterStartupScript("jsUnblockDialog", "unblockESNDialog();");
        //    }
        //    catch (Exception ex)
        //    {
        //        lblESN.Text = ex.Message;
        //    }

        //}

        private void BindComments(int poid)
        {
            try
            {
               // lblFNum.Text = string.Empty;
                Control tmp2 = LoadControl("~/controls/FulfillmentComments.ascx");
                avii.Controls.FulfillmentComments ctlFulfillmentComments = tmp2 as avii.Controls.FulfillmentComments;
                pnlComments.Controls.Clear();
                if (poid > 0)
                {
                    if (tmp2 != null)
                    {

                        ctlFulfillmentComments.BindComments(poid);
                    }
                    pnlComments.Controls.Add(ctlFulfillmentComments);

                   // RegisterStartupScript("jsUnblockDialog", "unblockCommentsDialog();");
                }
            }
            catch (Exception ex)
            {
                lblCMsg.Text = ex.Message;
            }

        }
        public void BindPO(int poID, bool poQuery)
        {
            SV.Framework.Fulfillment.ShippingLabelOperation shippingLabelOperation = SV.Framework.Fulfillment.ShippingLabelOperation.CreateInstance<SV.Framework.Fulfillment.ShippingLabelOperation>();

            lblViewPO.Text = string.Empty;
            btnContainerSlip.Visible = true;
            lblShipmentAck.Text = "PENDING";

            PurchaseOrders purchaseOrders = null;
            if (poQuery)
                purchaseOrders = Session["POS"] as PurchaseOrders;
            else
                purchaseOrders = purchaseOrderOperation.GerPurchaseOrdersNew(null, null, null, null, 0, "0", 0, null, null, null, null, null, null, null, null, null, null, poID, null, null, null, 0);
            List<TrackingDetail> trackingList = new List<TrackingDetail>();

            List<BasePurchaseOrder> purchaseOrderList = purchaseOrders.PurchaseOrderList;
            List<BasePurchaseOrderItem> purchaseOrderItemList = purchaseOrderOperation.GetPurchaseOrderItemsAndTrackingList(poID, out trackingList);
            Session["poitems"] = purchaseOrderItemList;
            Session["potracking"] = trackingList;

            var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(poID) select item).ToList();
            if (poInfoList != null && poInfoList.Count > 0)
            {
                ViewState["CustomerName"] = poInfoList[0].CustomerName.ToUpper();
                if(!poQuery)
                {
                    BindUsers(Convert.ToInt32(poInfoList[0].CompanyID));
                }
                if (poInfoList[0].CustomerName.ToUpper() == "DISH" && poInfoList[0].PurchaseOrderStatusID == 3)
                {
                    //btnAuthorization.Visible = true;
                    if (lblpoid.Text == "")
                    {
                        if (Session["adm"] != null)
                        {
                            btnPallet.Visible = true;
                            btnPOSLabel.Visible = true;
                            btnMapping.Visible = true;
                            btnESN.Visible = true;
                            btnBox.Visible = true;
                            btnTXT.Visible = true;
                            btnUedf.Visible = true;
                        }
                        else
                        {
                            btnESN.Visible = true;
                            btnTXT.Visible = true;
                            btnUedf.Visible = true;
                            btnPckSlip.Visible = true;
                            btnContainerSlip.Visible = true;
                        }
                        //if (Session["adm"] == null)
                        //{
                        //    btnPckSlip.Visible = false;
                        //    btnContainerSlip.Visible = false;
                        //}
                    }
                    else
                    {
                        btnSubmit.Visible = true;
                        pnlUnprovision.Visible = true;
                    }
                }

                ViewState["IsInterNational"] = poInfoList[0].IsInterNational;
                if(poInfoList[0].IsInterNational)
                {
                    List<CustomValues> customValues = shippingLabelOperation.GetCustomdeclarations(poID);
                    if (customValues != null && customValues.Count > 0)
                    {
                        rptCustom.DataSource = customValues;
                        rptCustom.DataBind();
                        trCustom.Visible = true;
                    }                    
                }
                lblPO.Text = poInfoList[0].PurchaseOrderNumber;
                lblCustomerOrderNumber.Text = poInfoList[0].CustomerOrderNumber;
                lblFO.Text = poInfoList[0].FactOrderNumber;
                int ItemsPerContainer = 0, ContainersPerPallet = 0, requiredContainers=0;
                var totalContainers = 0;
                var totalPallets = 0;
                var n2 = 0;
                var d2 = 0;
                foreach(BasePurchaseOrderItem item in purchaseOrderItemList)
                {
                    if (item.ItemsPerContainer > 0)
                    {
                        ItemsPerContainer = item.ItemsPerContainer;
                        n2 = Convert.ToInt32(item.Quantity) / ItemsPerContainer;
                        d2 = Convert.ToInt32(item.Quantity) % ItemsPerContainer;
                        if (n2 == 0 && d2 > 0)
                            d2 = 1;
                        if (n2 > 0 && d2 > 1)
                            d2 = 1;

                        //requiredContainers = Convert.ToInt32(item.Quantity) / ItemsPerContainer + Convert.ToInt32(item.Quantity) % ItemsPerContainer;
                        //totalContainers = totalContainers + requiredContainers;
                        totalContainers = n2 + d2;
                    }
                    if (item.ContainersPerPallet > 0)
                    {
                        ContainersPerPallet = item.ContainersPerPallet;
                        //totalPallets = totalPallets + requiredContainers / ContainersPerPallet + requiredContainers % ContainersPerPallet;
                    }                   
                }
                if (ContainersPerPallet > 0)
                {
                    int n1 = totalContainers / ContainersPerPallet;
                    var d1 = totalContainers % ContainersPerPallet;
                    if (n1 == 0 && d1 > 0)
                        d1 = 1;
                    if (n1 > 0 && d1 > 1)
                        d1 = 1;

                    //numberOfPallets =  (numberOfContainers / PalletQuantity) + (numberOfContainers % PalletQuantity);
                    totalPallets = n1 + d1;

                }
                lblItemsPerContainer.Text = totalContainers == 0 ? "Not assigned": totalContainers.ToString();
                lblContainersPerPallet.Text = totalPallets == 0 ? "Not assigned" : totalPallets.ToString();

                lblvPODate.Text = poInfoList[0].PurchaseOrderDate.ToShortDateString();
                lblReqShipDate.Text = poInfoList[0].RequestedShipDate.ToShortDateString();

                lblAddress.Text = poInfoList[0].Shipping.ShipToAddress + " " + poInfoList[0].Shipping.ShipToAddress2;
                //lblvAvso.Text = poInfoList[0].AerovoiceSalesOrderNumber;
                lblTpye.Text = poInfoList[0].POType;
                lblContactName.Text = poInfoList[0].Shipping.ContactName;
                lblCustName.Text = poInfoList[0].CustomerName;
                //new tracking
                lblShipViaCode.Text = poInfoList[0].Tracking.ShipToBy;
              //  chkShipRequired.Checked = poInfoList[0].IsShipmentRequired;
                //if ("1/1/0001" != poInfoList[0].Tracking.ShipToDate.ToShortDateString())
                //    lblShippDate.Text = poInfoList[0].Tracking.ShipToDate.ToShortDateString();
                lblState.Text = poInfoList[0].Shipping.ShipToState;
                lblvStoreID.Text = poInfoList[0].StoreID;
                if (string.IsNullOrWhiteSpace(poInfoList[0].StoreID))
                    btnContainerSlip.Visible = false;
                //new tracking
                //lblTrackNo.Text = poInfoList[0].Tracking.ShipToTrackingNumber;
                lblZip.Text = poInfoList[0].Shipping.ShipToZip;
                lblvStatus.Text = poInfoList[0].PurchaseOrderStatus.ToString();
                lblComment.Text = poInfoList[0].Comments;

                gvPODetail.DataSource = purchaseOrderItemList;
                gvPODetail.DataBind();
                if (purchaseOrderItemList != null && purchaseOrderItemList.Count > 0)
                    lblPODCount.Text = "<strong>Total count:</strong> " + purchaseOrderItemList.Count;
                else
                    lblPODCount.Text = string.Empty;
                //Bind tracking
                if (trackingList != null && trackingList.Count > 0)
                    gvTracking.DataSource = trackingList;
                else
                    gvTracking.DataSource = null;
                gvTracking.DataBind();
                //ViewState["CompanyName"] = poInfoList[0].CustomerName;

                LoadFulfillmentDoc(Convert.ToInt32(poInfoList[0].CompanyID), poInfoList[0].PurchaseOrderNumber);

                if (trackingList != null && trackingList.Count > 0)
                {
                    if ("1/1/0001" != trackingList[0].TrackingSentDateTime.ToShortDateString())
                    {
                        lblShipmentAck.Text = trackingList[0].TrackingSentDateTime.ToString();
                    }
                }
            }
        }
        private void LoadFulfillmentDoc(int companyID, string fulfillmentNumber)
        {
            string moduleName = "Fulfillment";

            SV.Framework.Fulfillment.DocumentFileOperation docOperations = SV.Framework.Fulfillment.DocumentFileOperation.CreateInstance<SV.Framework.Fulfillment.DocumentFileOperation>();
            lblDoc.Text = "";
            List<DocumentFile> documents = docOperations.GetDocuments(companyID, fulfillmentNumber, moduleName);
            if (documents != null && documents.Count > 0)
            {
                var docs = (from item in documents where item.FileName != "" select item).ToList();
                if (docs != null && docs.Count > 0)
                {
                    rptDoc.DataSource = documents;
                    rptDoc.DataBind();
                }
                else
                {
                    rptDoc.DataSource = null;
                    rptDoc.DataBind();
                    lblDoc.Text = "No document found";
                }

            }
            else
            {
                rptDoc.DataSource = null;
                rptDoc.DataBind();
                lblDoc.Text = "No document found";
            }

        }
        protected void lnkTracking_Command(object sender, CommandEventArgs e)
        {
            //lblESN.Text = string.Empty;
            //lblTracking.Text = string.Empty;
            //lblViewPO.Text = "";
            //try
            //{
            //    int poID = 0;
            //    if (ViewState["poid"] != null)
            //    {
            //        poID = Convert.ToInt32(ViewState["poid"]);

            //        string trackingNumber = Convert.ToString(e.CommandArgument);
            //        lblTracking.Text = trackingNumber;
            //        List<EsnList> esnList = FulfillmentOperations.GetTrackingESNList(poID, trackingNumber);

            //        if (esnList != null && esnList.Count > 0)
            //        {
            //            rptESN.DataSource = esnList;
            //            rptESN.DataBind();
            //        }
            //        else
            //        {
            //            rptESN.DataSource = null;
            //            rptESN.DataBind();
            //            lblESN.Text = "No records found";
            //        }

            //    }




            //   // RegisterStartupScript("jsUnblockDialog", "unblockESNDialog();");
            //}
            //catch (Exception ex)
            //{
            //    lblESN.Text = ex.Message;
            //}

        }

        protected void gvPODetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPODetail.EditIndex = -1;
            //GridView gvTemp = (GridView)sender;
            //gvUniqueID = gvTemp.UniqueID;
            //gvEditIndex = -1;
            BindPOItems();
            //GridView1.DataSource = ((PurchaseOrders)Session["POS"]).PurchaseOrderList;

            //GridView1.DataBind();
        }


        //private void ReloadLog(int poid, int pages)
        //{
        //    Control tmpLog = LoadControl("./controls/UCPOLog.ascx");


        //    avii.Controls.UCPOLog ctlLog = tmpLog as avii.Controls.UCPOLog;
        //    pnlLog.Controls.Clear();

        //    if (ctlLog != null)
        //    {
        //        //ctlLog.Pages = pages;
        //       // ctlLog.POID = poid;
        //        ctlLog.BindFulfillmentLog(poid, pages);
        //    }

        //    pnlLog.Controls.Add(ctlLog);
        //}
        protected void lnkRequest_Click(object sender, CommandEventArgs e)
        {
            lblRequestData.Text = string.Empty;
            try
            {
                int POLogID = Convert.ToInt32(e.CommandArgument);
                List<FulfillmentLogInfo> logList = HttpContext.Current.Session["pologList"] as List<FulfillmentLogInfo>;

                var lgList = (from item in logList where item.POLogID.Equals(POLogID) select item).ToList();


                if (lgList != null && lgList.Count > 0)
                {
                    lblRequestData.Text = lgList[0].RequestData;

                }
                else
                    lblRequestData.Text = "No data found";
                RegisterStartupScript("jsUnblockDialog", "unblockRequestDialog();");
            }
            catch (Exception ex)
            {
                lblRequestData.Text = ex.Message;
            }


        }
        protected void lnkResponse_Click(object sender, CommandEventArgs e)
        {
            lblResponseData.Text = string.Empty;
            try
            {
                int POLogID = Convert.ToInt32(e.CommandArgument);
                List<FulfillmentLogInfo> logList = HttpContext.Current.Session["pologList"] as List<FulfillmentLogInfo>;

                var lgList = (from item in logList where item.POLogID.Equals(POLogID) select item).ToList();


                if (lgList != null && lgList.Count > 0)
                {
                    lblResponseData.Text = lgList[0].ResponseData;

                }
                else
                    lblResponseData.Text = "No data found";
                RegisterStartupScript("jsUnblockDialog", "unblockResponseDialog();");
            }
            catch (Exception ex)
            {
                lblResponseData.Text = ex.Message;
            }
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }
        protected void rptLog_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkRequest = e.Item.FindControl("lnkRequest") as LinkButton;
                if (lnkRequest != null)
                {
                    lnkRequest.OnClientClick = "openRequestDialogAndBlock('Request data', '" + lnkRequest.ClientID + "')";

                }
                LinkButton lnkResponse = e.Item.FindControl("lnkResponse") as LinkButton;
                if (lnkResponse != null)
                {
                    lnkResponse.OnClientClick = "openResponseDialogAndBlock('Response data', '" + lnkResponse.ClientID + "')";

                }

            }
        }
        public void BindFulfillmentLog(int poID, int pages)
        {
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            lblMsg2.Text = string.Empty;
            List<FulfillmentLogInfo> logList = logOperations.GetFulfillmentLog(poID, pages);
            if (logList != null && logList.Count > 0)
            {
                Session["pologList"] = logList;
                rptLog.DataSource = logList;
                rptLog.DataBind();
            }
            else
            {
                rptLog.DataSource = null;
                rptLog.DataBind();
                lblMsg2.Text = "No log found";
            }
        }

        protected void lnkCustom_Command(object sender, CommandEventArgs e)
        {
            Session["poLabelID"] = Convert.ToInt32(e.CommandArgument);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('CustomDeclarations.aspx')</script>", false);

            //Response.Redirect("~/CustomDeclarations.aspx");

        }

        protected void btnUedf_Click(object sender, EventArgs e)
        {
            if (ViewState["poid"] != null)
            {
                UEDFOperation urdfOperation = UEDFOperation.CreateInstance<UEDFOperation>();
                SV.Framework.Fulfillment.FulfillmentOperations fulfillmentOperations = SV.Framework.Fulfillment.FulfillmentOperations.CreateInstance<SV.Framework.Fulfillment.FulfillmentOperations>();

                int poid = Convert.ToInt32(ViewState["poid"]);
                var memoryStream = new MemoryStream();
            //   System.Xml.XmlWriter write =  new   ;
                string fileName;
                //string filePrefix = "spappledsh";
                //string filePrefix = "spdsh";
                string filePrefix = "spdish";
                string transDate;
               // DateTime currentCSTDateTime;
                DateTime currentUtcDateTime = DateTime.UtcNow;
                DateTime currentCSTDateTime = TimeZoneInfo.ConvertTime(currentUtcDateTime, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));

                transDate = currentCSTDateTime.ToString("yyyy-MM-dd");
                // string fileName = filePrefix + "_" + transDate + "_" + edfFileInfo.fileSequence.ToString() + ".xml";

                string filePath = Server.MapPath("~/UploadedData/");
                string validateReturnMessage = "";
                edfFileGeneric edfFile = urdfOperation.GetUedfFileDetail(poid, transDate);
                if (edfFile != null)
                {
                    //transDate = edfFile.date.Replace("-", "");
                    transDate = transDate.Replace("-", "");
                    fileName = filePrefix + "_" + transDate + "_" + edfFile.fileSequence.ToString() + ".xml";
                    filePath = filePath + fileName;

                    if (urdfOperation.ValidateUEDFFileData(edfFile, out validateReturnMessage))
                    {
                        XElement xmlElement = urdfOperation.CreateUEDFFile(edfFile);
                        xmlElement.Save(filePath);

                        string returnResult = fulfillmentOperations.UEDF_File_Update(poid, fileName, currentCSTDateTime, currentUtcDateTime);

                        // tring strFullPath = Server.MapPath("~/temp.xml");
                        if (string.IsNullOrEmpty(returnResult))
                        {
                            string strContents = null;
                            System.IO.StreamReader objReader = default(System.IO.StreamReader);
                            objReader = new System.IO.StreamReader(filePath);
                            strContents = objReader.ReadToEnd();
                            objReader.Close();

                            string attachment = "attachment; filename=" + fileName;
                            Response.ClearContent();
                            Response.ContentType = "application/xml";
                            Response.AddHeader("content-disposition", attachment);
                            Response.Write(strContents);
                            Response.End();
                        }
                        else
                        {
                            lblViewPO.Text = returnResult;
                        }
                    }
                    else
                        lblViewPO.Text = validateReturnMessage;

                }
                else
                {
                    lblViewPO.Text = "No data found.";
                }

                //memoryStream.Position = 0;


                //var bytes = memoryStream.ToArray();

                //string fileType = ".xml";
                //string filename = DateTime.Now.Ticks + fileType;
                //Response.Clear();
                ////Response.ContentType = "application/pdf";
                //Response.ContentType = "text/xml";
                ////Response.ContentType = "application/zip";
                ////Response.Charset = "utf-8";
                //Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                //// Response.AppendHeader("Content-Length", bytes.Length.ToString());
                //// Response.ContentType = "application/octet-stream";

                //Response.Buffer = true;
                //Response.Clear();
                //Response.OutputStream.Write(bytes, 0, bytes.Length);
                //Response.OutputStream.Flush();


                //using (MemoryStream ms = new MemoryStream())
                //{
                //    XmlWriterSettings xws = new XmlWriterSettings();
                //    xws.OmitXmlDeclaration = true;
                //    xws.Indent = true;

                //    using (XmlWriter xw = XmlWriter.Create(ms, xws))
                //    {
                //        XElement xmlElement = UEDFOperation.CreateUEDFFile(edfFile);
                //        xmlElement.Save(xw);
                //    }
                //    ms.Position = 0;
                //    var bytes = ms.ToArray();

                //    string fileType = ".xml";
                //    string filename = DateTime.Now.Ticks + fileType;
                //    Response.Clear();
                //    //Response.ContentType = "application/pdf";
                //    Response.ContentType = "application/xml";
                //    //Response.ContentType = "application/zip";
                //    //Response.Charset = "utf-8";
                //    Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                //   // Response.AppendHeader("Content-Length", bytes.Length.ToString());
                //    // Response.ContentType = "application/octet-stream";

                //    Response.Buffer = true;
                //    Response.Clear();
                //    Response.OutputStream.Write(bytes, 0, bytes.Length);
                //    Response.OutputStream.Flush();

                //}

                // xmlElement.wr(memoryStream);
                //memoryStream.Position = 0;

                //xmlElement.Save(filePath);

            }

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Company not selected!')</script>", false);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "<script language='javascript'>StopProgress()</script>", false); 
        }

        private void GeneratePalletPDF()
        {
            try
            {
                if (ViewState["poid"] != null)
                {
                    int poid = Convert.ToInt32(ViewState["poid"]);
                    var memoryStream = new MemoryStream();
                    string productType = "";
                    List<SV.Framework.LabelGenerator.PalletModel> pallets = default;// new List<SV.Framework.LabelGenerator.PalletModel>();
                    List<PalletModel> palletsDb = dishLabelOperations.GetPalletLabelInfo(poid, "", "");
                    SV.Framework.LabelGenerator.PalletModel palletModel = default;
                    SV.Framework.LabelGenerator.DishLabelOperation dishLabelOperation = new SV.Framework.LabelGenerator.DishLabelOperation();
                    SV.Framework.LabelGenerator.H3LabelOperation h3LabelOperation = new SV.Framework.LabelGenerator.H3LabelOperation();
                    if (palletsDb != null && palletsDb.Count > 0)
                    {
                        pallets = new List<SV.Framework.LabelGenerator.PalletModel>();
                           productType = palletsDb[0].ProductType;
                        foreach(PalletModel item in palletsDb)
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
                            palletModel.ShippingAddressLine1  = item.ShippingAddressLine1;
                            palletModel.ShippingAddressLine2  = item.ShippingAddressLine2;
                            palletModel.ShippingCity  = item.ShippingCity;
                            palletModel.ShippingCountry  = item.ShippingCountry;
                            palletModel.ShippingState  = item.ShippingState;
                            palletModel.ShippingZipCode  = item.ShippingZipCode;
                            palletModel.SKU  = item.SKU;
                            palletModel.SNo  = item.SNo;
                            palletModel.State  = item.State;
                            palletModel.TotalPallet  = item.TotalPallet;
                            palletModel.ZipCode  = item.ZipCode;
                            pallets.Add(palletModel);
                        }
                        //if (productType.ToUpper() == "H3")
                        if (palletsDb[0].OSType.ToUpper() == "ANDROID")
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
                                lblMsg2.Text = "Technical error!";
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
                                lblMsg2.Text = "Technical error!";
                            }
                        }
                    }
                    else
                    {
                        lblMsg2.Text = "No pallet found";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg2.Text = ex.Message;
            }
        }

        protected void btnPallet_Click(object sender, EventArgs e)
        {
            // btnHide.Click += btnHide_Click(null, null);
            //modalSending.Visible = false;

            GeneratePalletPDF();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "javascript:alert('Record is not updated');", true);
            // modalSending.Visible = false;
            //btnHide.PerformClick(); // (new object(), new EventArgs());
        }

        protected void btnPOSLabel_Click(object sender, EventArgs e)
        {
            if (ViewState["poid"] != null)
            {
                int poid = Convert.ToInt32(ViewState["poid"]);
                List<SV.Framework.LabelGenerator.PosKitInfo> posKITs = default; new List<SV.Framework.LabelGenerator.PosKitInfo>();
                List<SV.Framework.LabelGenerator.KitBoxInfo> kitBoxInfos = default;
                SV.Framework.LabelGenerator.PosKitInfo posKitInfo = default;
                List<PosKitInfo> posKITDB = dishLabelOperations.GetPosKitLabel(poid);
                SV.Framework.LabelGenerator.KitBoxInfo kitBoxInfo = default;
                SV.Framework.LabelGenerator.DishLabelOperation dishLabelOperation = new SV.Framework.LabelGenerator.DishLabelOperation();
                
                if (posKITDB != null && posKITDB.Count > 0)
                {
                    posKITs = new List<SV.Framework.LabelGenerator.PosKitInfo>();
                    foreach (PosKitInfo item in posKITDB)
                    {
                        kitBoxInfos = new List<SV.Framework.LabelGenerator.KitBoxInfo>();
                        posKitInfo = new SV.Framework.LabelGenerator.PosKitInfo();
                        posKitInfo.CompanyName = item.CompanyName;
                        posKitInfo.ESN = item.ESN; 
                        posKitInfo.IMEI2 = item.IMEI2; 
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
                        foreach(KitBoxInfo kitBoxInfodb in item.KitBoxList)
                        {
                            kitBoxInfo = new SV.Framework.LabelGenerator.KitBoxInfo();
                            kitBoxInfo.DisplayName = kitBoxInfodb.DisplayName;
                            kitBoxInfo.OriginCountry = kitBoxInfodb.OriginCountry;
                            kitBoxInfos.Add(kitBoxInfo);
                        }
                        posKitInfo.KitBoxList = kitBoxInfos;
                        posKITs.Add(posKitInfo);
                    }

                    string ProductType = posKITDB[0].ProductType;
                    string OSType = posKITDB[0].OSType;
                    MemoryStream memSt = null;// = new MemoryStream();

                    if (ProductType.ToUpper() == "H5")
                    {
                        SV.Framework.LabelGenerator.H5LabelOperation h5LabelOperation = new SV.Framework.LabelGenerator.H5LabelOperation();

                        memSt = h5LabelOperation.POSKITLabelPdfTarCode(posKITs);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "stop loader", "StopProgress()", true);
                        //var memSt = slabel.ExportToPDF(models);
                        //if (memSt != null)
                        //{
                        //    string fileType = ".pdf";
                        //    string filename = DateTime.Now.Ticks + fileType;
                        //    Response.Clear();
                        //    //Response.ContentType = "application/pdf";
                        //    Response.ContentType = "application/octet-stream";
                        //    Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                        //    Response.Buffer = true;
                        //    Response.Clear();
                        //    var bytes = memSt.ToArray();
                        //    Response.OutputStream.Write(bytes, 0, bytes.Length);
                        //    Response.OutputStream.Flush();
                        //    lblMsg2.Text = "Label generated successfully.";
                        //}
                    }
                    else

                    {
                        if (posKITDB[0].OSType.ToUpper() == "ANDROID")
                        {
                            SV.Framework.LabelGenerator.H3LabelOperation h3LabelOperation = new SV.Framework.LabelGenerator.H3LabelOperation();

                            memSt = h3LabelOperation.POSKITLabelPdfTarCode(posKITs);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "stop loader", "StopProgress()", true);
                            //var memSt = slabel.ExportToPDF(models);
                            //if (memSt != null)
                            //{
                            //    string fileType = ".pdf";
                            //    string filename = DateTime.Now.Ticks + fileType;
                            //    Response.Clear();
                            //    //Response.ContentType = "application/pdf";
                            //    Response.ContentType = "application/octet-stream";
                            //    Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                            //    Response.Buffer = true;
                            //    Response.Clear();
                            //    var bytes = memSt.ToArray();
                            //    Response.OutputStream.Write(bytes, 0, bytes.Length);
                            //    Response.OutputStream.Flush();
                            //    lblMsg2.Text = "Label generated successfully.";
                            //}
                        }
                        else
                        {
                            memSt = dishLabelOperation.POSKITLabelPdfTarCodeNew2(posKITs);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "stop loader", "StopProgress()", true);
                            //var memSt = slabel.ExportToPDF(models);
                            //if (memSt != null)
                            //{
                            //    string fileType = ".pdf";
                            //    string filename = DateTime.Now.Ticks + fileType;
                            //    Response.Clear();
                            //    //Response.ContentType = "application/pdf";
                            //    Response.ContentType = "application/octet-stream";
                            //    Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                            //    Response.Buffer = true;
                            //    Response.Clear();
                            //    var bytes = memSt.ToArray();
                            //    Response.OutputStream.Write(bytes, 0, bytes.Length);
                            //    Response.OutputStream.Flush();
                            //    lblMsg2.Text = "Label generated successfully.";
                            //}
                        }
                    }
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
                        lblMsg2.Text = "Label generated successfully.";
                    }
                }
            }
            //ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "StopProgress()", true);
           // ClientScript.RegisterStartupScript(this.GetType(), "stop loader", "<script language='javascript'>StopProgress()</script>", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "<script language='javascript'>StopProgress()</script>", false);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }

        protected void btnMapping_Click(object sender, EventArgs e)
        {
            if (ViewState["poid"] != null)
            {
                int poid = Convert.ToInt32(ViewState["poid"]);
                List<SV.Framework.LabelGenerator.PalletCartonMap> palletCartonMaps = new List<SV.Framework.LabelGenerator.PalletCartonMap>();
                List<SV.Framework.LabelGenerator.CartonModel> cartons = new List<SV.Framework.LabelGenerator.CartonModel>();
                SV.Framework.LabelGenerator.PalletCartonMap palletCartonMap = default;
                SV.Framework.LabelGenerator.CartonModel cartonModel = default;
                List<PalletCartonMap> palletsdb = dishLabelOperations.GetPalletCartonMappingLabel(poid);
                SV.Framework.LabelGenerator.DishLabelOperation dishLabelOperation = new SV.Framework.LabelGenerator.DishLabelOperation();
                if (palletsdb != null && palletsdb.Count > 0)
                {
                    foreach(PalletCartonMap item in palletsdb)
                    {
                        cartons = new List<SV.Framework.LabelGenerator.CartonModel>();
                        palletCartonMap = new SV.Framework.LabelGenerator.PalletCartonMap();
                        palletCartonMap.PalletID = item.PalletID;
                        foreach(CartonModel carton in item.Cartons)
                        {
                            cartonModel = new SV.Framework.LabelGenerator.CartonModel();
                            cartonModel.BOXColumn1 = carton.BOXColumn1;
                            cartonModel.BOXColumn2 = carton.BOXColumn2;
                            cartonModel.Column1 = carton.Column1;
                            cartonModel.Column2 = carton.Column2;
                            cartons.Add(cartonModel);
                        }
                        palletCartonMap.Cartons = cartons;
                        palletCartonMaps.Add(palletCartonMap);
                    }
                    if (palletCartonMaps != null)
                    {
                        var memSt = dishLabelOperation.PalletCartonMappingLabelPdf(palletCartonMaps);
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


                            lblMsg2.Text = "Label generated successfully.";

                        }
                    }
                    else
                        lblMsg2.Text = "No mapping found";
                }
                else
                    lblMsg2.Text = "No mapping found";
            }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
            //ClientScript.RegisterStartupScript(this.GetType(), "stop loader", "<script language='javascript'>StopProgress()</script>", false);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "<script language='javascript'>StopProgress()</script>", false);
        }


        protected void btnHide_Click(object sender, EventArgs e)
        {
            System.Threading.Tasks.Task.Delay(5000);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "stop loader", "StopProgress();", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "stop loader", "StopProgress();", true);
        }
        protected void btnTXT_Click(object sender, EventArgs e)
        {

            if (ViewState["poid"] != null)
            {
                SV.Framework.Fulfillment.FulfillmentOperations fulfillmentOperations = SV.Framework.Fulfillment.FulfillmentOperations.CreateInstance<SV.Framework.Fulfillment.FulfillmentOperations>();

                int poid = Convert.ToInt32(ViewState["poid"]);
                string ShipmentDate = "";
                List<FulfillmentDetailModel> poDetails = fulfillmentOperations.GetPurchaseOrderTxtFile(poid, out ShipmentDate);
                if (poDetails != null && poDetails.Count > 0)
                {
                    string fileName = "LANXX" + ShipmentDate + "-01.txt";

                    string string2TXT = poDetails.ToTXT().Replace("\"","");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename="+ fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(string2TXT);
                    Response.Flush();
                    Response.End();
                }
            }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }

        private void DownloadESN()
        {
            SV.Framework.Fulfillment.FulfillmentOperations fulfillmentOperations = SV.Framework.Fulfillment.FulfillmentOperations.CreateInstance<SV.Framework.Fulfillment.FulfillmentOperations>();
            int poid = Convert.ToInt32(ViewState["poid"]);
            int itr = 1;
            string newLine = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            FulfillmentEsNInfo fulfillmentEsNInfo = fulfillmentOperations.GetPurchaseOrderESNs(poid);
            if (fulfillmentEsNInfo != null && !string.IsNullOrEmpty(fulfillmentEsNInfo.FulfillmentNumber))
            {
                List<FulfillmentEsn> esnList = fulfillmentEsNInfo.EsnList;
                if (esnList != null && esnList.Count > 0)
                {
                    //string string2CSV = "";// "BATCH,ESN,MeidHex,MeidDec,Location,MSL,OTKSL,SerialNumber" + Environment.NewLine;
                    sb.Append("ESN" + Environment.NewLine);
                    foreach (FulfillmentEsn item in esnList)
                    {
                        newLine = esnList.Count == itr ? "" : Environment.NewLine;
                        sb.Append(item.ESN + newLine);
                        itr = itr + 1;
                    }
                    string string2CSV = sb.ToString();
                    if (!string.IsNullOrEmpty(string2CSV))
                    {
                        string fileName = "nms_bounce_dish_to_tmo_"+ fulfillmentEsNInfo.FulfillmentNumber +"_"+ fulfillmentEsNInfo.Qty.ToString()+"_"+ fulfillmentEsNInfo.ShippedDate+".txt";
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition", "attachment;filename="+ fileName);
                        Response.Charset = "";
                        Response.ContentType = "application/text";
                        Response.Output.Write(string2CSV);
                        Response.Flush();
                        Response.End();
                    }

                }
            }

        }

        protected void btnESN_Click(object sender, EventArgs e)
        {
            DownloadESN();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SV.Framework.Fulfillment.UnprovisioningOperation unprovisioningOperation = SV.Framework.Fulfillment.UnprovisioningOperation.CreateInstance<SV.Framework.Fulfillment.UnprovisioningOperation>();

            int poid = Convert.ToInt32(ViewState["poid"]);
            int userID = Convert.ToInt32(Session["UserID"]);
            string comment = txtUnComment.Text.Trim();
            int requestedBy = 0;
            if (ddlRequestedBy.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(comment))
                {
                    requestedBy = Convert.ToInt32(ddlRequestedBy.SelectedValue);

                    UnprovisionPORequest request = new UnprovisionPORequest();
                    request.POID = poid;
                    request.CreatedBy = userID;
                    request.RequestedBy = requestedBy;
                    request.Status = "Received";


                    //if (Session["adm"] != null)
                    //    request.AdminComment = txtUnComment.Text.Trim();            
                    //else
                    //    request.CustomerComment = txtUnComment.Text.Trim();

                    request.AdminComment = comment;
                    request.CustomerComment = comment;

                    string returnMessage = unprovisioningOperation.UnprovisioingRequestInsert(request);
                    if (string.IsNullOrEmpty(returnMessage))
                    {
                        lblViewPO.Text = "Submitted successfully!";
                        btnSubmit.Enabled = false;
                    }
                    else
                    {
                        lblViewPO.Text = returnMessage;
                    }
                }
                else
                    lblViewPO.Text = "Comment is required!";
            }
            else
                lblViewPO.Text = "Requested by is required!";


        }

        protected void btnBox_Click(object sender, EventArgs e)
        {
            int POID = 0, companyID=0;
            string poNum = "";
            POID = Convert.ToInt32(ViewState["poid"]);
            List<SV.Framework.LabelGenerator.CartonBoxID> cartonBoxIDS = default; // new List<SV.Framework.LabelGenerator.CartonBoxID>();
            SV.Framework.LabelGenerator.CartonBoxID cartonBoxID = default;

            List <CartonBoxID> cartonBoxIDs = dishLabelOperations.GetPurchaseOrderBoxIDs(companyID, poNum, POID, "");
            if (cartonBoxIDs != null && cartonBoxIDs.Count > 0)
            {
                cartonBoxIDS = new List<SV.Framework.LabelGenerator.CartonBoxID>();
                SV.Framework.LabelGenerator.DishLabelOperation dishLabelOperation = new SV.Framework.LabelGenerator.DishLabelOperation();
                foreach(CartonBoxID item in cartonBoxIDs)
                {
                    cartonBoxID = new SV.Framework.LabelGenerator.CartonBoxID();
                    cartonBoxID.BoxDesc = item.BoxDesc;
                    cartonBoxID.BoxID = item.BoxID;
                    cartonBoxID.FulfillmentNumber = item.FulfillmentNumber;
                    cartonBoxID.SKU = item.SKU;
                    
                    cartonBoxIDS.Add(cartonBoxID);
                }
                if (cartonBoxIDS != null)
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
                        lblMsg2.Text = "Label generated successfully.";
                    }
                }
                else
                {
                    lblMsg2.Text = "No data found.";
                }
            }
            else
            {
                lblMsg2.Text = "No data found.";
            }

        }

        protected void lnkDoc_Click(object sender, EventArgs e)
        {

            LinkButton lnkDoc =  sender as LinkButton;

                string customerName = Convert.ToString(ViewState["CustomerName"]);

                SV.Framework.Fulfillment.DocumentFileOperation docOperations = SV.Framework.Fulfillment.DocumentFileOperation.CreateInstance<SV.Framework.Fulfillment.DocumentFileOperation>();
                string filePath = Server.MapPath("~/langlobal/UploadDocument/PO/" + customerName + "/") + lnkDoc.Text;
                string extension = Path.GetExtension(filePath);
                extension = extension.ToLower();
                // if (".pdf" == extension)
                //   extension = "octet-stream";
                string contentType = "";

                if (".pdf" == extension)
                    contentType = "application/pdf";
                if (".doc" == extension)
                    contentType = "application/msword";
                if (".docx" == extension)
                    contentType = "application/msword";
                if (".jpg" == extension)
                    contentType = "application/octet-stream";
                if (".jpeg" == extension)
                    contentType = "application/octet-stream";
                if (".png" == extension)
                    contentType = "image/png";


                if (".txt" == extension)
                    contentType = "text/plain";

                string strContents = null;
                System.IO.StreamReader objReader = default(System.IO.StreamReader);
                objReader = new System.IO.StreamReader(filePath);
                strContents = objReader.ReadToEnd();
                objReader.Close();

                string attachment = "attachment; filename=" + lnkDoc.Text;
                Response.ClearContent();

                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + lnkDoc.Text);
                Response.TransmitFile(filePath);
                Response.End();

                //Response.ContentType = "application/" + extension;
                //Response.AddHeader("content-disposition", attachment);
                //Response.Write(strContents);
                //Response.End();
            
        }
    }
}