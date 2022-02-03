using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
//using avii.Classes;
using System.Data;
using iTextSharp.text.pdf;
using iTextSharp.text;
using SV.Framework.Fulfillment;
using SV.Framework.Models.Fulfillment;

namespace avii
{
    public partial class FulfillmentDetail : System.Web.UI.Page
    {
        private SV.Framework.Fulfillment.ShippingLabelOperation shippingLabelOperation = SV.Framework.Fulfillment.ShippingLabelOperation.CreateInstance<SV.Framework.Fulfillment.ShippingLabelOperation>();

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
                int poid = 0;
                //string Nationality = "D";
                //bool IsInterNational = false;
                btnPrintlabel.Visible = false;
                if (Session["poid"] != null)
                {
                    poid = Convert.ToInt32(Session["poid"]);
                    ViewState["poid"] = poid;
                    Session["poid"] = null;

                    PurchaseOrders purchaseOrders = Session["POS"] as PurchaseOrders;
                    List<BasePurchaseOrder> purchaseOrderList = purchaseOrders.PurchaseOrderList;

                    var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(poid) select item).ToList();
                    if (poInfoList.Count > 0)
                    {
                        ViewState["IsInterNational"] = poInfoList[0].IsInterNational;
                    }
                    // BindStates();
                    //PODetail(poid);
                    Trackings(poid);
                    POShipment(poid);
                    
                    //if (ViewState["IsInterNational"] != null)
                    //{
                    //    IsInterNational = Convert.ToBoolean(ViewState["IsInterNational"]);
                    //    if (IsInterNational)
                    //        Nationality = "I";
                    //}
                    //BindShipBy(Nationality);
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
        private void Trackings(int poID)
        {
            SV.Framework.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.Fulfillment.PurchaseOrder.CreateInstance<SV.Framework.Fulfillment.PurchaseOrder>();

            List<TrackingDetail> trackingList = new List<TrackingDetail>();
            tblTracking.Visible = false;
            List<BasePurchaseOrderItem> purchaseOrderItemList = purchaseOrderOperation.GetPurchaseOrderItemsAndTrackingList(poID, out trackingList);
            if (trackingList != null && trackingList.Count > 0)
            {
                //Session["purchaseOrderItemList"] = purchaseOrderItemList;
                //lblShipItem.Text = "Label already generated";
                gvTracking.DataSource = trackingList;
                gvTracking.DataBind();
                btnPrintlabel.Visible = false;
                tblTracking.Visible = true;

            }
            if (purchaseOrderItemList != null && purchaseOrderItemList.Count > 0)
            {
                Session["purchaseOrderItemList"] = purchaseOrderItemList;
            }
        }
        private void BindShipBy(string Nationality)
        {
            try
            {
                SV.Framework.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.Fulfillment.PurchaseOrder.CreateInstance<SV.Framework.Fulfillment.PurchaseOrder>();

                List<ShipBy> shipBy = purchaseOrderOperation.GetShipByList(Nationality);
                // Session["shipby"] = shipBy;
                //dpShipVia.DataSource = shipBy;
                //dpShipVia.DataTextField = "ShipCodeNText";
                //dpShipVia.DataValueField = "ShipByCode";
                //dpShipVia.DataBind();


                ddlShipVia.DataSource = shipBy;
                ddlShipVia.DataTextField = "ShipCodeNText";
                ddlShipVia.DataValueField = "ShipByCode";
                ddlShipVia.DataBind();
            }
            catch (Exception ex)
            {
                lblShipItem.Text = ex.Message;
            }
        }
        //private void BindStates()
        //{
        //    DataTable dataTable = clsCompany.GetState(0);
        //    ViewState["state"] = dataTable;
        //    dpState.DataSource = dataTable;
        //    dpState.DataTextField = "Statecode";
        //    dpState.DataValueField = "Statecode";
        //    dpState.DataBind();
        //    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("", "");
        //    dpState.Items.Insert(0, item);

        //    //dpState.SelectedValue = "CA";

        //}

        private void BindShipShapes(bool IsNational)
        {
            try
            {
                List<SV.Framework.Common.LabelGenerator.ShipShapeInfo> shipShapes = SV.Framework.Common.LabelGenerator.ShipMethodOperation.GetShipPackageShapes(IsNational);

                ddlShape.DataSource = shipShapes;
                ddlShape.DataTextField = "ShipShapeText";
                ddlShape.DataValueField = "ShipShapeValue";
                ddlShape.DataBind();

                System.Web.UI.WebControls.ListItem item1 = new System.Web.UI.WebControls.ListItem("", "");
                ddlShape.Items.Insert(0, item1);

            }
            catch (Exception ex)
            {
                lblShipItem.Text = ex.Message;
            }
        }

        private void POShipment(int poID)
        {
            try
            {
                lblShipItem.Text = string.Empty;
                chkTracking.Checked = false;
                btnShip.Visible = true;
                // int poID = Convert.ToInt32(e.CommandArgument);
                // string poInfo = e.CommandArgument.ToString();
                txtTrackingNumber.Text = string.Empty;
                txtShippingDate.Text = DateTime.Now.ToShortDateString();

                DateTime dateNow = DateTime.Now.Date;

                txtShipComments.Text = string.Empty;
                ddlShipVia.SelectedIndex = 0;
                ddlShape.Items.Clear();

                //string[] itemNames = System.Enum.GetNames(typeof(SV.Framework.Common.LabelGenerator.ShipPackageShape));

                //for (int i = 0; i <= itemNames.Length - 1; i++)
                //{
                //    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(itemNames[i], itemNames[i]);
                //    ddlShape.Items.Add(item);
                //}
                //System.Web.UI.WebControls.ListItem item1 = new System.Web.UI.WebControls.ListItem("", "");
                //ddlShape.Items.Insert(0, item1);
                string Nationality = "D";
                PurchaseOrders purchaseOrders = Session["POS"] as PurchaseOrders;
                List<BasePurchaseOrder> purchaseOrderList = purchaseOrders.PurchaseOrderList;

                var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(poID) select item).ToList();
                if (poInfoList.Count > 0)
                {
                    string day = string.Empty;
                    string month = string.Empty;
                    string year = string.Empty;
                    ViewState["IsInterNational"] = poInfoList[0].IsInterNational;
                    if(poInfoList[0].IsInterNational)
                        Nationality = "I";
                    BindShipBy(Nationality);

                    DateTime RequestedShipDate = poInfoList[0].RequestedShipDate.Date;
                    if (RequestedShipDate < dateNow)
                    { //txtShippingDate.Text = dateNow.ToString("MM/dd/yyyy");
                        day = dateNow.Day.ToString();
                        month = dateNow.Month.ToString();
                        year = dateNow.Year.ToString();
                        txtShippingDate.Text = month + "/" + day + "/" + year;
                    }
                    else
                    { //txtShippingDate.Text = poInfoList[0].RequestedShipDate.ToString("MM/dd/yyyy");

                        day = poInfoList[0].RequestedShipDate.Day.ToString();
                        month = poInfoList[0].RequestedShipDate.Month.ToString();
                        year = poInfoList[0].RequestedShipDate.Year.ToString();
                        txtShippingDate.Text = month + "/" + day + "/" + year;
                    }

                    string poShipBy = poInfoList[0].Tracking.ShipToBy;
                    lblShipPO.Text = poInfoList[0].PurchaseOrderNumber;
                    ViewState["companyid"] = poInfoList[0].CompanyID;
                    ddlShipVia.SelectedValue = poShipBy;
                    if(poShipBy.ToLower()== "truckload")
                    {
                        txtTrackingNumber.Enabled = true;
                        txtPrice.Enabled = true;
                        chkTracking.Checked = true;
                    }

                    BindShipShapes(poInfoList[0].IsInterNational);

                    if(poInfoList[0].IsInterNational)
                    {
                        pnlCustom.Visible = true;
                        List<BasePurchaseOrderItem> purchaseOrderItemList = Session["purchaseOrderItemList"] as List<BasePurchaseOrderItem>;
                        if(purchaseOrderItemList != null && purchaseOrderItemList.Count > 0)
                        {
                            rptCustom.DataSource = purchaseOrderItemList;
                            rptCustom.DataBind();
                        }


                    }
                    if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), poShipBy))
                    {
                        ddlShape.SelectedValue = SV.Framework.Common.LabelGenerator.ShipPackageShape.Letter.ToString();
                    }
                    else
                        ddlShape.SelectedValue = SV.Framework.Common.LabelGenerator.ShipPackageShape.package.ToString();


                    if (poInfoList[0].PurchaseOrderStatusID == 3)
                        btnShip.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblShipItem.Text = ex.Message;
            }
        }
        //private void PODetail(int poID)
        //{
        //    //int poID = Convert.ToInt32(e.CommandArgument);
            
        //    PurchaseOrders purchaseOrders = Session["POS"] as PurchaseOrders;
        //    List<BasePurchaseOrder> purchaseOrderList = purchaseOrders.PurchaseOrderList;

        //    var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(poID) select item).ToList();
        //    if (poInfoList.Count > 0)
        //    {
        //        chkShipRequired.Checked = poInfoList[0].IsShipmentRequired;
        //        lblPONo.Text = poInfoList[0].PurchaseOrderNumber;
        //        //lblPoDate.Text = poInfoList[0].PurchaseOrderDate.ToShortDateString();
        //        txtPODate.Text = poInfoList[0].PurchaseOrderDate.ToShortDateString();
        //        lblCustomer.Text = poInfoList[0].CustomerName;
        //        txtContactName.Text = poInfoList[0].Shipping.ContactName;
        //        txtContactPhone.Text = poInfoList[0].Shipping.ContactPhone;
        //        //txtShipBy.Text = poInfoList[0].Tracking.ShipToBy;

        //        if (poInfoList[0].PurchaseOrderStatusID == 1 || poInfoList[0].PurchaseOrderStatusID == 2 || poInfoList[0].PurchaseOrderStatusID == 8 || poInfoList[0].PurchaseOrderStatusID == 10)
        //        {
        //            lblDShipvia.Visible = false;
        //            dpShipVia.Visible = true;
        //        }
        //        else
        //        {
        //            lblDShipvia.Visible = true;
        //            dpShipVia.Visible = false;

        //            //dpShipVia.SelectedIndex = 0;

        //        }
        //        string poShipBy = poInfoList[0].Tracking.ShipToBy;

        //        if (!string.IsNullOrEmpty(poShipBy))
        //        {

        //            List<ShipBy> shipViaList = (List<ShipBy>)Session["shipby"];
        //            var shipVia = (from item in shipViaList where item.ShipByCode.Equals(poShipBy) select item).ToList();
        //            //DataRow[] drs = null;
        //            //drs = dt.Select("[shipby] ='" + poShipBy + "'");

        //            if (shipVia != null && shipVia.Count > 0)
        //            {
        //                dpShipVia.SelectedValue = poShipBy;
        //                lblDShipvia.Text = poShipBy;
        //            }
        //            else
        //            {
        //                dpShipVia.SelectedIndex = 0;
        //                lblEditPO.Text = "Invalid Shipvia: " + poShipBy;
        //            }
        //        }
        //        else
        //            dpShipVia.SelectedIndex = 0;



        //        //lblAVSO.Text = poInfoList[0].AerovoiceSalesOrderNumber;
        //        //txtTrackingNo.Text = poInfoList[0].Tracking.ShipToTrackingNumber;
        //        // COMMENTED BECAUSE WE POPUP NOT TO VIEW COMMENTS
        //        //txtCommments.Text = poInfoList[0].Comments;
        //        txtCommments.Text = string.Empty;
        //        if (Session["adm"] == null)
        //        {
        //            //txtTrackingNo.ReadOnly = true;
        //            //lblPOStatus.Visible = true;
        //            ddlStatus.Visible = false;
        //            lblStatus.Text = poInfoList[0].PurchaseOrderStatus.ToString();
        //            ddlStatus.SelectedValue = poInfoList[0].PurchaseOrderStatusID.ToString();
        //        }
        //        else
        //        {
        //            ddlStatus.SelectedValue = poInfoList[0].PurchaseOrderStatusID.ToString();
        //            lblStatus.Visible = false;
        //        }

        //        //if ("1/1/0001" != poInfoList[0].Tracking.ShipToDate.ToShortDateString())
        //        //    lblShipDate.Text = poInfoList[0].Tracking.ShipToDate.ToShortDateString();

        //        lblStoreID.Text = poInfoList[0].StoreID;
        //        txtStreetAdd.Text = poInfoList[0].Shipping.ShipToAddress;
        //        txtAddress2.Text = poInfoList[0].Shipping.ShipToAddress2;

        //        txtCity.Text = poInfoList[0].Shipping.ShipToCity;
        //        string poState = poInfoList[0].Shipping.ShipToState.ToUpper();

        //        if (!string.IsNullOrEmpty(poState))
        //        {
        //            try
        //            {
        //                //poState = 
        //                DataTable dt = ViewState["state"] as DataTable;

        //                DataRow[] drs = null;
        //                drs = dt.Select("[statecode] ='" + poState + "'");

        //                if (drs != null && drs.Length > 0)
        //                {
        //                    dpState.SelectedValue = poState;
        //                }
        //                else
        //                {
        //                    dpState.SelectedIndex = 0;
        //                    lblEditPO.Text = "Invalid StateCode: " + poState;
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                lblEditPO.Text = ex.Message;
        //            }

        //        }
        //        //txtState.Text = poInfoList[0].Shipping.ShipToState;
        //        txtZip.Text = poInfoList[0].Shipping.ShipToZip;
        //        //lblStatus.Text = poInfoList[0].PurchaseOrderStatus.ToString();
        //        //lblSentESN.Text = poInfoList[0].SentESN;
        //        //lblSentASN.Text = poInfoList[0].SentASN;
        //        lblPONo.Text = poInfoList[0].PurchaseOrderNumber;

        //    }
        //}

        //protected void btnEditPO_Click(object sender, EventArgs e)
        //{
        //    lblEditPO.Text = string.Empty;
        //    PurchaseOrders purchaseOrders = (PurchaseOrders)Session["POS"];
        //    int uploadDateRange = 1095;
        //    bool IsShipmentRequired = chkShipRequired.Checked;

        //    uploadDateRange = Convert.ToInt32(ConfigurationSettings.AppSettings["UploadAdminDateRange"]);
        //    string poDate = string.Empty;
        //    DateTime uploadDate = Convert.ToDateTime(txtPODate.Text.Trim());
        //    string errorMessage = string.Empty;
        //    int poID = 0;
        //    int userID = 0;
        //    int statusID = 0;
        //    UserInfo userInfo = Session["userInfo"] as UserInfo;
        //    if (userInfo != null)
        //    {
        //        userID = userInfo.UserGUID;
        //        //ViewState["userid"] = userID;
        //    }
        //    if (ViewState["poid"] != null)
        //        poID = Convert.ToInt32(ViewState["poid"]);


        //    if (ViewState["adminrole"] != null)
        //    {
        //        poDate = txtPODate.Text.Trim();
        //        if (!string.IsNullOrEmpty(poDate))
        //        {
        //            if (poDate.Trim().Length > 0)
        //            {
        //                DateTime dt;
        //                if (DateTime.TryParse(poDate.Trim(), out dt))
        //                {
        //                    double days = (uploadDate - dt).TotalDays;
        //                    if (days > uploadDateRange)
        //                        errorMessage = "PODate(" + dt.ToShortDateString() + ") can not be more than " + uploadDateRange + " days before";
        //                    else
        //                        if (days < 0)
        //                        errorMessage = "PODate(" + dt.ToShortDateString() + ") can not be more than today date";
        //                    //else
        //                    //    if (days == 0)
        //                    //        uploadDate = Convert.ToDateTime("01/01/1900");

        //                    uploadDate = dt;

        //                }
        //                else
        //                    errorMessage = "PODate(" + poDate + ") does not have correct format(MM/DD/YYYY)";



        //            }
        //            // else
        //            //   uploadDate = Convert.ToDateTime("01/01/1900");
        //        }
        //        ////else
        //        //    uploadDate = Convert.ToDateTime("01/01/1900");
        //    }
        //    else
        //    {
        //        //uploadDate = Convert.ToDateTime("01/01/1900");
        //    }
        //    uploadDate = DateTime.SpecifyKind(uploadDate, DateTimeKind.Unspecified);

        //    if (poID > 0)
        //    {

        //        statusID = Convert.ToInt32(ddlStatus.SelectedValue);
        //        BasePurchaseOrder purchaseOrder = new BasePurchaseOrder(poID);
        //        purchaseOrder = purchaseOrders.FindPurchaseOrder(poID);
        //        purchaseOrder.PurchaseOrderDate = Convert.ToDateTime(txtPODate.Text.Trim());
        //        purchaseOrder.PurchaseOrderStatusID = statusID;
        //        purchaseOrder.PurchaseOrderStatus = (PurchaseOrderStatus)Convert.ToInt16(ddlStatus.SelectedValue);
        //        purchaseOrder.Shipping.ContactName = txtContactName.Text;
        //        purchaseOrder.Shipping.ShipToAttn = txtContactName.Text;
        //        purchaseOrder.Shipping.ContactPhone = txtContactPhone.Text;
        //        purchaseOrder.Shipping.ShipToAddress = txtStreetAdd.Text;
        //        purchaseOrder.Shipping.ShipToAddress2 = txtAddress2.Text;
        //        purchaseOrder.Shipping.ShipToCity = txtCity.Text;
        //        //purchaseOrder.Shipping.ShipToState = txtState.Text; ///dpState.SelectedValue;
        //        purchaseOrder.Shipping.ShipToState = dpState.SelectedValue;

        //        purchaseOrder.Shipping.ShipToZip = txtZip.Text;

        //        //default shipvia
        //        //if (statusID == 1 && statusID == 2 && statusID == 8)
        //        purchaseOrder.Tracking.ShipToBy = dpShipVia.SelectedValue; // txtShipBy.Text;// ;

        //        //purchaseOrder.Tracking.ShipToTrackingNumber = txtTrackingNo.Text;

        //        purchaseOrder.Tracking.PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber;
        //        purchaseOrder.Comments = txtCommments.Text.Trim();
        //        purchaseOrder.IsShipmentRequired = IsShipmentRequired;
        //        PurchaseOrder.UpdatePurchaseOrder(purchaseOrder, userID);
        //        // gvPOQuery.EditIndex = -1;
        //        Session["POS"] = purchaseOrders;
        //        //TriggerClientGridRefresh();
        //        lblEditPO.Text = "Updated successfully";
        //        //   RegisterStartupScript("jsUnblockDialog", "closeEditDialog();");
        //    }
        //}

        protected void btnAddShip_Click(object sender, EventArgs e)
        {
            lblShipItem.Text = string.Empty;

            FulfillmentShippingLine model = new FulfillmentShippingLine();
            List<FulfillmentShippingLineItem> listitems = new List<FulfillmentShippingLineItem>();
            FulfillmentShippingLineItem lineItem = null;
            string poDate = string.Empty;
            // txtShippingDate.Text = DateTime.Now.ToString("yyyy-MM-dd");// "2020-04-20";

            DateTime shipDate = DateTime.Now; // Convert.ToDateTime(txtShippingDate.Text.Trim());
            DateTime.TryParse(txtShippingDate.Text.Trim(), out shipDate);

            string errorMessage = string.Empty, trackingNumber = string.Empty, comments = string.Empty;
            int poID = 0, shipById = 0; ;
            int userID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
                //ViewState["userid"] = userID;
            }
            if (ViewState["poid"] != null)
                poID = Convert.ToInt32(ViewState["poid"]);

            //if (ddlShipVia.SelectedIndex > 0)
            //shipById = Convert.ToInt32(ddlShipVia.SelectedValue);
            comments = txtShipComments.Text.Trim();
            trackingNumber = txtTrackingNumber.Text.Trim();

            shipDate = DateTime.SpecifyKind(shipDate, DateTimeKind.Unspecified);
            model.ShipDate = shipDate.ToShortDateString();
            model.Comments = comments;
            model.POID = poID;
            model.ShipById = shipById;
            model.TrackingNumber = trackingNumber;
            //foreach(RepeaterItem item in rptShipItems.Items)
            //{
            //    CheckBox chkBox = item.FindControl("chkSKU") as CheckBox;
            //    if (chkBox.Checked)
            //    {
            //        lineItem = new FulfillmentShippingLineItem();
            //        TextBox txtQty = item.FindControl("txtQty") as TextBox;
            //        HiddenField hdnPODID = item.FindControl("hdnPODID") as HiddenField;
            //        lineItem.PODID = Convert.ToInt32(hdnPODID.Value);
            //        lineItem.Quantity = Convert.ToInt32(txtQty.Text.Trim());
            //        listitems.Add(lineItem);

            //    }
            //}
            //if (listitems != null && listitems.Count > 0)
            {
                model.LineItems = listitems;
                if (poID > 0)
                {
                    //if(chk)
                    int returnResult = GenerateShipmentLabel(listitems);

                    // int returnResult = FulfillmentShippingLineItemOperation.CreatePurchaseOrderShippingLineItems(model);
                    if (returnResult == 1)
                    {
                        //BindShipping(poID);
                        //lblShipItem.Text = "Label generated successfully";
                        btnShip.Visible = false;
                        Trackings(poID);
                    }

                    //RegisterStartupScript("jsUnblockDialog", "closeEditDialog();");
                }
            }
            //else
            //{ lblShipItem.Text = "Please select atleast one SKU"; }


        }
        private int GenerateShipmentLabel(List<FulfillmentShippingLineItem> listitems)
        {
            string shipFromContactName = ConfigurationSettings.AppSettings["ShipFromContactName"].ToString();
            string shipFromContactName2 = ConfigurationSettings.AppSettings["ShipFromContactName2"].ToString();
            string shipFromAddress = ConfigurationSettings.AppSettings["ShipFromAddress"].ToString();
            string shipFromCity = ConfigurationSettings.AppSettings["ShipFromCity"].ToString();
            string shipFromState = ConfigurationSettings.AppSettings["ShipFromState"].ToString();
            string shipFromZip = ConfigurationSettings.AppSettings["ShipFromZip"].ToString();
            string shipFromCountry = ConfigurationSettings.AppSettings["ShipFromCountry"].ToString();
            string shipFromAttn = ConfigurationSettings.AppSettings["ShipFromAttn"].ToString();
            string shipFromPhone = ConfigurationSettings.AppSettings["ShipFromPhone"].ToString();

            int returnResult = 0;
            int poid = 0, userId = 0;
            double weight = 0;
            string ShipDate = txtShippingDate.Text;
            DateTime LabelPrintDateTime = DateTime.Today;
            //if (!string.IsNullOrEmpty(ShipDate))
            //  LabelPrintDateTime = Convert.ToDateTime(ShipDate); //DateTime.TryParse(ShipDate, out LabelPrintDateTime); //
            double.TryParse(txtWeight.Text.Trim(), out weight);
            if (weight == 0)
                weight = 1;
          //  lblESN.Text = string.Empty;
           // lblTracking.Text = string.Empty;
            SV.Framework.Common.LabelGenerator.EndiciaPrintLabel labelInfo = new SV.Framework.Common.LabelGenerator.EndiciaPrintLabel();

            SV.Framework.Common.LabelGenerator.ShippingLabelOperation shipAccess = new SV.Framework.Common.LabelGenerator.ShippingLabelOperation();
            SV.Framework.Common.LabelGenerator.ShipInfo shipToInfo = new SV.Framework.Common.LabelGenerator.ShipInfo();
            SV.Framework.Common.LabelGenerator.ShipInfo shipFromInfo = new SV.Framework.Common.LabelGenerator.ShipInfo();
            List<CustomValues> customValues = new List<CustomValues>();

            labelInfo.LabelType = "Default";
            try
            {
                string shipmentType = "S";
                if (ViewState["poid"] != null)
                {
                    poid = Convert.ToInt32(ViewState["poid"]);

                    if (Session["UserID"] != null)
                    {
                        int.TryParse(Session["UserID"].ToString(), out userId);
                    }

                    PurchaseOrders pos = Session["POS"] as PurchaseOrders;

                    if (pos != null && pos.PurchaseOrderList.Count > 0)
                    {

                        var po = (from item in pos.PurchaseOrderList where item.PurchaseOrderID.Equals(poid) select item).ToList();
                        if (po != null && po.Count > 0)
                        {
                            
                            labelInfo.FulfillmentNumber = po[0].PurchaseOrderNumber;
                            labelInfo.LabelPrintDateTime = LabelPrintDateTime;
                            //shipToInfo
                            shipToInfo.ShipToAddress = po[0].Shipping.ShipToAddress;
                            shipToInfo.ShipToAddress2 = po[0].Shipping.ShipToAddress2;
                            shipToInfo.ContactName = po[0].Shipping.ContactName;
                            shipToInfo.ShipToCity = po[0].Shipping.ShipToCity;
                            shipToInfo.ShipToState = po[0].Shipping.ShipToState;
                            shipToInfo.ShipToZip = po[0].Shipping.ShipToZip;
                            shipToInfo.ShipToAttn = po[0].Shipping.ShipToAttn ?? po[0].Shipping.ContactName;
                            shipToInfo.ContactPhone = po[0].Shipping.ContactPhone;
                            if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.USStates), shipToInfo.ShipToState))
                            {
                                shipToInfo.ShipToCountry = "USA";
                            }
                            else if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.CanadaStates), shipToInfo.ShipToState))
                            {
                               // List<BasePurchaseOrderItem> purchaseOrderItemList = Session["purchaseOrderItemList"] as List<BasePurchaseOrderItem>;

                                SV.Framework.Common.LabelGenerator.CustomsInfo customsInfo = new SV.Framework.Common.LabelGenerator.CustomsInfo();
                                List<SV.Framework.Common.LabelGenerator.CustomsItem> customsItems = new List<SV.Framework.Common.LabelGenerator.CustomsItem>();
                                //if(purchaseOrderItemList != null && purchaseOrderItemList.Count > 0)
                                {
                                    customsInfo.ContentsType = "Documents";
                                    labelInfo.LabelType = "International";

                                    foreach(RepeaterItem item in rptCustom.Items)
                                    {
                                        Label lbProductName = item.FindControl("lbName") as Label;
                                        HiddenField hdQty = item.FindControl("hdQty") as HiddenField;
                                        HiddenField hdPODID = item.FindControl("hdPODID") as HiddenField;
                                        TextBox txtValue = item.FindControl("txtValue") as TextBox;

                                        if (string.IsNullOrEmpty(txtValue.Text.Trim()))
                                        {
                                            lblShipItem.Text = "Custom value is required!";
                                            return returnResult;
                                        }
                                        SV.Framework.Common.LabelGenerator.CustomsItem customsItem = new SV.Framework.Common.LabelGenerator.CustomsItem();
                                        CustomValues customValue1 = new CustomValues();
                                        
                                        customsItem.Description = lbProductName.Text;
                                        customsItem.Quantity = Convert.ToInt32(hdQty.Value);
                                        customsItem.Weight = Convert.ToDecimal(weight);
                                        customsItem.Value = Convert.ToDecimal(txtValue.Text);

                                        customsItems.Add(customsItem);

                                        customValue1.CustomValue = customsItem.Value;
                                        customValue1.POD_ID = Convert.ToInt32(hdPODID.Value);
                                        customValues.Add(customValue1);
                                    }
                                    //foreach (BasePurchaseOrderItem item in purchaseOrderItemList)
                                    //{
                                    //    SV.Framework.Common.LabelGenerator.CustomsItem customsItem = new SV.Framework.Common.LabelGenerator.CustomsItem();
                                    //    customsItem.Description = item.ItemCode;
                                    //    customsItem.Quantity = Convert.ToInt32(item.Quantity);
                                    //    customsItem.Weight = Convert.ToDecimal(weight);
                                    //    customsItem.Value = 1;
                                    //    customsItems.Add(customsItem);

                                    //}
                                    customsInfo.CustomsItems = customsItems;
                                }
                                labelInfo.CustomsInfo = customsInfo;
                                shipToInfo.ShipToCountry = "CANADA";
                            }
                            labelInfo.ShipTo = shipToInfo;

                            //ship From Info
                            shipFromInfo.ShipToAddress = shipFromAddress;
                            shipFromInfo.ShipToAddress2 = "";
                            shipFromInfo.ContactName = po[0].CustomerName;
                            shipFromInfo.ShipToCity = shipFromCity;
                            shipFromInfo.ShipToState = shipFromState;
                            shipFromInfo.ShipToZip = shipFromZip;
                            shipFromInfo.ShipToAttn = shipFromContactName;
                            shipFromInfo.ShipToCountry = shipFromCountry;
                            shipFromInfo.ContactPhone = shipFromPhone;

                            labelInfo.ShipFrom = shipFromInfo;
                            //
                            labelInfo.PackageWeight = new SV.Framework.Common.LabelGenerator.Weight { units = "ounces", value = weight };
                            labelInfo.PackageContent = "Description";

                            //labelInfo.ShippingMethod = SV.Framework.Common.LabelGenerator.ShipMethods.Priority;
                            // if (ddlShipVia.SelectedIndex > 0)
                            {
                                Enum.TryParse(ddlShipVia.SelectedValue, out SV.Framework.Common.LabelGenerator.ShipMethods shipMethods);
                                labelInfo.ShippingMethod = shipMethods;
                            }
                            labelInfo.ShippingType = SV.Framework.Common.LabelGenerator.ShipType.Ship;

                            if (ddlShape.SelectedIndex > 0)
                            {
                                Enum.TryParse(ddlShape.SelectedValue, out SV.Framework.Common.LabelGenerator.ShipPackageShape shipPackage);
                                labelInfo.PackageShape = shipPackage;
                            }
                            else
                                labelInfo.PackageShape = SV.Framework.Common.LabelGenerator.ShipPackageShape.Letter;


                            SV.Framework.Admin.APIAddressInfo aPIAddressInfo = null;
                            int companyID = Convert.ToInt32(po[0].CompanyID);
                            if (!chkTracking.Checked)
                            {
                                if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), labelInfo.ShippingMethod.ToString()))
                                {
                                    aPIAddressInfo = SV.Framework.Admin.CustomerOperations.GetCustomerAPIAddress(companyID, "Endicia");
                                    SV.Framework.Common.LabelGenerator.iEndiciaLabelCredentials request2 = new SV.Framework.Common.LabelGenerator.EndiciaCredentials();
                                    if (aPIAddressInfo != null)
                                    {
                                        request2.ConnectionString = aPIAddressInfo.ConnectionString;

                                        SV.Framework.Common.LabelGenerator.iEndiciaLabelCredentials iEndiciaLabelCredentials = shipAccess.GetIEndiciaLabelCredentials(request2);
                                        labelInfo.AccountID = iEndiciaLabelCredentials.AccountID;
                                        labelInfo.RequesterID = iEndiciaLabelCredentials.RequesterID;
                                        labelInfo.PassPharase = iEndiciaLabelCredentials.PassPharase;
                                        labelInfo.ApiURL = aPIAddressInfo.APIAddress;
                                    }
                                    else
                                    {
                                        lblShipItem.Text = "Endicia not configuired";
                                        return returnResult;
                                    }
                                }
                                else if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.ShipStationsShipMethods), labelInfo.ShippingMethod.ToString()))
                                {
                                    aPIAddressInfo = SV.Framework.Admin.CustomerOperations.GetCustomerAPIAddress(companyID, "ShipStation");
                                    if (aPIAddressInfo != null)
                                    {
                                        labelInfo.ApiURL = aPIAddressInfo.APIAddress;
                                        labelInfo.AuthString = aPIAddressInfo.ConnectionString;
                                    }
                                    else
                                    {
                                        lblShipItem.Text = "ShipStation not configuired";
                                        return returnResult;
                                    }
                                }
                            }

                            string Package = ddlShape.SelectedValue;
                            string ShipVia = ddlShipVia.SelectedValue;
                            decimal Weight = Convert.ToDecimal(txtWeight.Text);
                            string Comments = txtShipComments.Text.Trim();
                            decimal FinalPostage = 0;
                            bool IsManualTracking = chkTracking.Checked;
                            ShippingLabelInfo request = new ShippingLabelInfo();
                            request.FulfillmentNumber = labelInfo.FulfillmentNumber;
                            request.ShipmentType = shipmentType;

                            if (!IsManualTracking)
                            {
                                SV.Framework.Common.LabelGenerator.iPrintLabel response = shipAccess.PrintShippingLabel(labelInfo);

                                if (response != null && !string.IsNullOrWhiteSpace(response.TrackingNumber))
                                {
                                    request.ShippingLabelImage = response.ShippingLabelImage;
                                    request.TrackingNumber = response.TrackingNumber;
                                    FinalPostage = response.FinalPostage;
                                    //response.
                                    //request.LineItems = listitems;
                                    txtTrackingNumber.Text = response.TrackingNumber;
                                    ShippingLabelResponse setResponse = shippingLabelOperation.ShippingLabelUpdateNew(request, userId, listitems, ShipDate, Package, ShipVia, Weight, Comments, FinalPostage, IsManualTracking, poid, customValues);
                                    lblShipItem.Text = "Label generated successfully";
                                    returnResult = 1;
                                    btnPrintlabel.Visible = true;
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(response.PackageContent))
                                        lblShipItem.Text = response.PackageContent;
                                    else
                                        lblShipItem.Text = "Technical error please try again!";
                                }
                            }
                            else
                            {
                                request.ShippingLabelImage = null;
                                request.TrackingNumber = txtTrackingNumber.Text.Trim();
                                FinalPostage = 0;
                                if (!string.IsNullOrWhiteSpace(txtPrice.Text))
                                    FinalPostage = Convert.ToDecimal(txtPrice.Text);
                                //response.

                                ShippingLabelResponse setResponse = shippingLabelOperation.ShippingLabelUpdateNew(request, userId, listitems, ShipDate, Package, ShipVia, Weight, Comments, FinalPostage, IsManualTracking, poid, customValues);
                                lblShipItem.Text = "Submitted successfully";
                                returnResult = 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblShipItem.Text = ex.Message;
            }
            return returnResult;
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
        protected void imgDelTracking_Command(object sender, CommandEventArgs e)
        {
            SV.Framework.Fulfillment.TrackingOperations trackingOperations = SV.Framework.Fulfillment.TrackingOperations.CreateInstance<SV.Framework.Fulfillment.TrackingOperations>();

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
                    lblShipItem.Text = "Fulfillment Order Tracking can not be deleted";
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order Tracking is deleted successfully');</script>");

                }
                else
                {
                    gvTracking.DataSource = trackingList;
                    gvTracking.DataBind();
                    Session["potracking"] = trackingList;

 
                    lblShipItem.Text = "Fulfillment Order Tracking is deleted successfully";
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order Tracking is deleted successfully');</script>");


                }
            }
            catch (Exception ex)
            {
                lblShipItem.Text = ex.Message;
            }

        }

        protected void imgPrint_Command(object sender, CommandEventArgs e)
        {
            int lineNumber = Convert.ToInt32(e.CommandArgument);
            ViewState["linenumber"] = lineNumber;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp1", "<script language='javascript'>Refresh();</script>", false);

        }
        protected void btnhdnPrintlabel_Click(object sender, EventArgs e)
        {
            if (ViewState["linenumber"] != null)
            {
                PrintLabel(Convert.ToInt32(ViewState["linenumber"]));
            }
        }
        private void PrintLabel(int lineNumber)
        {
            try
            {
                float width = 320, height = 520, envHeight = 500;
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
                lblShipItem.Text = ex.Message;
            }
        }

        protected void btnhdPrintlabel_Click(object sender, EventArgs e)
        {
            //int companyID = 0;
            //string poNumber = lblShipPO.Text;
            //if (ViewState["companyid"] != null)
            //    companyID = Convert.ToInt32(ViewState["companyid"]);
            //if (!string.IsNullOrWhiteSpace(poNumber) && companyID > 0)
            //{
            //    List<FulfillmentLabel> poList = FulfillmentLabelOperation.GetLabels(poNumber, companyID);

            //    if (poList != null && poList.Count > 0)
            //    {
            //        GeneratePDF(poList);
            //        //PrintLabel(Convert.ToInt32(ViewState["linenumber"]));
            //    }
            //}
            //else
            //{
            //    lblShipItem.Text = "Couldn't generated label please try after some time";
            //}
        }
        //private void GeneratePDF(List<FulfillmentLabel> poList)
        //{
        //    try
        //    {
        //        var memoryStream = avii.Classes.FulfillmentLabelOperation.LabelsPdf(poList);
        //        byte[] bytes = memoryStream.ToArray();
        //        //  bytes = Combine(bytes1, bytes2);
        //        string fileType = ".pdf";
        //        string filename = DateTime.Now.Ticks + fileType;
        //        Response.Clear();
        //        //Response.ContentType = "application/pdf";
        //        Response.ContentType = "application/octet-stream";
        //        Response.AddHeader("content-disposition", "attachment;filename=" + filename);
        //        Response.Buffer = true;
        //        Response.Clear();
        //        // var bytes = memSt.ToArray();
        //        Response.OutputStream.Write(bytes, 0, bytes.Length);
        //        Response.OutputStream.Flush();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblShipItem.Text = ex.Message;
        //    }
        //}

        protected void lnkCustom_Command(object sender, CommandEventArgs e)
        {
            Session["poLabelID"] = Convert.ToInt32(e.CommandArgument);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('CustomDeclarations.aspx')</script>", false);

            //Response.Redirect("~/CustomDeclarations.aspx");

        }

    }
}
