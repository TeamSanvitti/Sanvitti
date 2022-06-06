using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Fulfillment;
using SV.Framework.Models.Fulfillment;

namespace avii
{
    public partial class FulfillmentEdit : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                ReadOlnyAccess();
                BindStates();
                if (Session["poid"] != null)
                {
                    BindShipVia();
                    LoadPODetail();
                }
            }
        }
        private void BindShipVia()
        {
            try
            {
                PurchaseOrder purchaseOrderOperation = PurchaseOrder.CreateInstance<PurchaseOrder>();

                List<ShipBy> shipBy = purchaseOrderOperation.GetShipByList();
                Session["shipby"] = shipBy;
                dpShipVia.DataSource = shipBy;
                dpShipVia.DataTextField = "ShipCodeNText";
                dpShipVia.DataValueField = "ShipByCode";
                dpShipVia.DataBind();

               
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void BindStates()
        {
            DataTable dataTable = avii.Classes.clsCompany.GetState(0);
            ViewState["state"] = dataTable;
            dpState.DataSource = dataTable;
            dpState.DataTextField = "Statecode";
            dpState.DataValueField = "Statecode";
            dpState.DataBind();
            System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("", "");
            dpState.Items.Insert(0, item);

            //dpState.SelectedValue = "CA";

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
               
        private void LoadPODetail()
        {
            PurchaseOrder purchaseOrderOperation = PurchaseOrder.CreateInstance<PurchaseOrder>();

            lblMsg.Text = string.Empty;
            int poID = Convert.ToInt32(Session["poid"]);
            Session["poid"] = null;
            ViewState["poid"] = poID;
            btnSubmit.Visible = false;
            
            PurchaseOrders purchaseOrders = default; //Session["POS"] as PurchaseOrders;
            if(Session["POS"] == null)
            {
                purchaseOrders =  purchaseOrderOperation.GerPurchaseOrdersNew(null, null, null, null, 0, "0", 0, null, null, null, null, null, null, null, null, null, null, poID, null, null, null, 0);
                Session["POS"] = purchaseOrders;
            }
            if(Session["shipby"] == null)
                Session["shipby"] = purchaseOrderOperation.GetShipByList();
            

            purchaseOrders = Session["POS"] as PurchaseOrders;

            List<BasePurchaseOrder> purchaseOrderList = purchaseOrders.PurchaseOrderList;
            List<TrackingDetail> trackingList = new List<TrackingDetail>();

            List<BasePurchaseOrderItem> purchaseOrderItemList = purchaseOrderOperation.GetPurchaseOrderItemsAndTrackingList(poID, out trackingList);
            if (purchaseOrderItemList != null)
            {
                //Session["poitems"] = purchaseOrderItemList;
                gvPODetail.DataSource = purchaseOrderItemList;
                gvPODetail.DataBind();
            }
            //Session["potracking"] = trackingList;

            var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(poID) select item).ToList();
            if (poInfoList.Count > 0)
            {
                chkShipRequired.Checked = poInfoList[0].IsShipmentRequired;
                lblPONo.Text = poInfoList[0].PurchaseOrderNumber;
                txtCustomerOrderNumber.Text = poInfoList[0].CustomerOrderNumber;
                txtFactOrderNumber.Text = poInfoList[0].FactOrderNumber;
                lblPoTye.Text = poInfoList[0].POType;
                //lblPoDate.Text = poInfoList[0].PurchaseOrderDate.ToShortDateString();
                txtPODate.Text = poInfoList[0].PurchaseOrderDate.ToShortDateString();
                lblCustomer.Text = poInfoList[0].CustomerName;
                txtContactName.Text = poInfoList[0].Shipping.ContactName;
                txtContactPhone.Text = poInfoList[0].Shipping.ContactPhone;
                txtReqShipDate.Text = poInfoList[0].RequestedShipDate.ToString("MM/dd/yyyy");

               // txtContainersPerPallet.Text = poInfoList[0].PurchaseOrderItems[0].ContainersPerPallet.ToString();
                //txtItemsPerContainer.Text = poInfoList[0].PurchaseOrderItems[0].ItemsPerContainer.ToString();
                //txtShipBy.Text = poInfoList[0].Tracking.ShipToBy;

                //if (Session["adm"] == null)
                if (Convert.ToInt32(Session["UserID"]) != 81)
                {
                    if (poInfoList[0].PurchaseOrderStatusID == 1)
                    {
                        btnSubmit.Visible = true;
                    }
                }
                else if (Convert.ToInt32(Session["UserID"]) == 81)
                    btnSubmit.Visible = true;

                if (poInfoList[0].PurchaseOrderStatusID == 1 || poInfoList[0].PurchaseOrderStatusID == 2 || poInfoList[0].PurchaseOrderStatusID == 8 || poInfoList[0].PurchaseOrderStatusID == 10)
                {
                    lblDShipvia.Visible = false;
                    dpShipVia.Visible = true;
                }
                else
                {
                    lblDShipvia.Visible = true;
                    dpShipVia.Visible = false;

                    //dpShipVia.SelectedIndex = 0;

                }
                string poShipBy = poInfoList[0].Tracking.ShipToBy;

                if (!string.IsNullOrEmpty(poShipBy))
                {

                    List<ShipBy> shipViaList = (List<ShipBy>)Session["shipby"];
                    var shipVia = (from item in shipViaList where item.ShipByCode.Equals(poShipBy) select item).ToList();
                    //DataRow[] drs = null;
                    //drs = dt.Select("[shipby] ='" + poShipBy + "'");

                    if (shipVia != null && shipVia.Count > 0)
                    {
                        dpShipVia.SelectedValue = poShipBy;
                        lblDShipvia.Text = poShipBy;
                    }
                    else
                    {
                        dpShipVia.SelectedIndex = 0;
                        lblMsg.Text = "Invalid Shipvia: " + poShipBy;
                    }
                }
                else
                    dpShipVia.SelectedIndex = 0;



                //lblAVSO.Text = poInfoList[0].AerovoiceSalesOrderNumber;
                //txtTrackingNo.Text = poInfoList[0].Tracking.ShipToTrackingNumber;
                // COMMENTED BECAUSE WE POPUP NOT TO VIEW COMMENTS
                //txtCommments.Text = poInfoList[0].Comments;
                txtCommments.Text = string.Empty;
                if (Session["adm"] == null)
                {
                    //txtTrackingNo.ReadOnly = true;
                    //lblPOStatus.Visible = true;
                    ddlStatus.Visible = false;
                    lblStatus.Text = poInfoList[0].PurchaseOrderStatus.ToString();
                    ddlStatus.SelectedValue = poInfoList[0].PurchaseOrderStatusID.ToString();
                }
                else
                {
                    ddlStatus.SelectedValue = poInfoList[0].PurchaseOrderStatusID.ToString();
                    lblStatus.Visible = false;
                }

                //if ("1/1/0001" != poInfoList[0].Tracking.ShipToDate.ToShortDateString())
                //    lblShipDate.Text = poInfoList[0].Tracking.ShipToDate.ToShortDateString();

                lblStoreID.Text = poInfoList[0].StoreID;
                txtStreetAdd.Text = poInfoList[0].Shipping.ShipToAddress;
                txtAddress2.Text = poInfoList[0].Shipping.ShipToAddress2;

                txtCity.Text = poInfoList[0].Shipping.ShipToCity;
                string poState = poInfoList[0].Shipping.ShipToState.ToUpper();

                if (!string.IsNullOrEmpty(poState))
                {
                    try
                    {
                       // if()
                        //poState = 
                        DataTable dt = ViewState["state"] as DataTable;

                        DataRow[] drs = null;
                        drs = dt.Select("[statecode] ='" + poState + "'");

                        if (drs != null && drs.Length > 0)
                        {
                            dpState.SelectedValue = poState;
                        }
                        else
                        {
                            dpState.SelectedIndex = 0;
                            lblMsg.Text = "Invalid StateCode: " + poState;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = ex.Message;
                    }
                }
                txtZip.Text = poInfoList[0].Shipping.ShipToZip;
                lblPONo.Text = poInfoList[0].PurchaseOrderNumber;
            }
        }
        protected void btnEditPO_Click(object sender, EventArgs e)
        {
            PurchaseOrder purchaseOrderOperation = PurchaseOrder.CreateInstance<PurchaseOrder>();
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            PurchaseOrders purchaseOrders = (PurchaseOrders)Session["POS"];
            int uploadDateRange = 1095;
            bool POCustNoValidate = false;
            bool IsShipmentRequired = chkShipRequired.Checked;

            
                uploadDateRange = Convert.ToInt32(ConfigurationSettings.AppSettings["UploadAdminDateRange"]);
            string poDate = string.Empty;
            DateTime uploadDate = Convert.ToDateTime(txtPODate.Text.Trim());
            string errorMessage = string.Empty;
            int poID = 0;
            int userID = 0;
            int statusID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
                //ViewState["userid"] = userID;
            }
            if (ViewState["poid"] != null)
                poID = Convert.ToInt32(ViewState["poid"]);


            if (ViewState["adminrole"] != null)
            {
                poDate = txtPODate.Text.Trim();
                if (!string.IsNullOrEmpty(poDate))
                {
                    if (poDate.Trim().Length > 0)
                    {
                        DateTime dt;
                        if (DateTime.TryParse(poDate.Trim(), out dt))
                        {
                            double days = (uploadDate - dt).TotalDays;
                            if (days > uploadDateRange)
                                errorMessage = "PODate(" + dt.ToShortDateString() + ") can not be more than " + uploadDateRange + " days before";
                            else
                                if (days < 0)
                                errorMessage = "PODate(" + dt.ToShortDateString() + ") can not be more than today date";
                            //else
                            //    if (days == 0)
                            //        uploadDate = Convert.ToDateTime("01/01/1900");

                            uploadDate = dt;

                        }
                        else
                            errorMessage = "PODate(" + poDate + ") does not have correct format(MM/DD/YYYY)";



                    }
                    // else
                    //   uploadDate = Convert.ToDateTime("01/01/1900");
                }
                ////else
                //    uploadDate = Convert.ToDateTime("01/01/1900");
            }
            else
            {
                //uploadDate = Convert.ToDateTime("01/01/1900");
            }
            uploadDate = DateTime.SpecifyKind(uploadDate, DateTimeKind.Unspecified);

            if (poID > 0)
            {

                statusID = Convert.ToInt32(ddlStatus.SelectedValue);
                BasePurchaseOrder purchaseOrder = new BasePurchaseOrder(poID);
                purchaseOrder = purchaseOrders.FindPurchaseOrder(poID);
                purchaseOrder.PurchaseOrderDate = Convert.ToDateTime(txtPODate.Text.Trim());
                purchaseOrder.PurchaseOrderStatusID = statusID;
                purchaseOrder.FactOrderNumber = txtFactOrderNumber.Text.Trim();
                purchaseOrder.CustomerAccountNumber = txtCustomerOrderNumber.Text.Trim();
                purchaseOrder.PurchaseOrderStatus = (PurchaseOrderStatus)Convert.ToInt16(ddlStatus.SelectedValue);
                purchaseOrder.Shipping.ContactName = txtContactName.Text;
                purchaseOrder.Shipping.ShipToAttn = txtContactName.Text;
                purchaseOrder.Shipping.ContactPhone = txtContactPhone.Text;
                purchaseOrder.Shipping.ShipToAddress = txtStreetAdd.Text;
                purchaseOrder.Shipping.ShipToAddress2 = txtAddress2.Text;
                purchaseOrder.Shipping.ShipToCity = txtCity.Text;
                //purchaseOrder.Shipping.ShipToState = txtState.Text; ///dpState.SelectedValue;
                purchaseOrder.Shipping.ShipToState = dpState.SelectedValue;

                purchaseOrder.Shipping.ShipToZip = txtZip.Text;
                SV.Framework.Models.RMA.RMAUserCompany companyInfo = rmaUtility.getRMAUserCompanyInfo(Convert.ToInt32(purchaseOrder.CompanyID), string.Empty, -1, -1);
                if (companyInfo != null && companyInfo.UserID > 0)
                {
                    POCustNoValidate = companyInfo.POCustNoValidate;
                }
                if(POCustNoValidate && txtCustomerOrderNumber.Text.Trim().Length > 11)
                {
                    lblMsg.Text = "Customer order number length should be between 5 to 11 characters";
                    return;
                }
                else if (!POCustNoValidate && txtCustomerOrderNumber.Text.Trim().Length < 5)
                {
                    lblMsg.Text = "Customer order number length should be between 5 to 20 characters";
                    return;
                }
                //default shipvia
                //if (statusID == 1 && statusID == 2 && statusID == 8)
                purchaseOrder.Tracking.ShipToBy = dpShipVia.SelectedValue; // txtShipBy.Text;// ;

                //purchaseOrder.Tracking.ShipToTrackingNumber = txtTrackingNo.Text;

                purchaseOrder.Tracking.PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber;
                purchaseOrder.Comments = txtCommments.Text.Trim();
                purchaseOrder.IsShipmentRequired = IsShipmentRequired;

                DateTime RequestedShipDate = DateTime.Now;
                DateTime.TryParse(txtReqShipDate.Text.Trim(), out RequestedShipDate);
                purchaseOrder.RequestedShipDate = RequestedShipDate;
                List<BasePurchaseOrderItem> items = new List<BasePurchaseOrderItem>();
                BasePurchaseOrderItem item = default;
                foreach (GridViewRow row in gvPODetail.Rows)
                {
                    item = new BasePurchaseOrderItem();
                    HiddenField hdpodid = row.FindControl("hdpodid") as HiddenField;
                    TextBox txtItemsPerContainer = row.FindControl("txtItemsPerContainer") as TextBox;
                    TextBox txtContainersPerPallet = row.FindControl("txtContainersPerPallet") as TextBox;
                    item.PodID = Convert.ToInt32(hdpodid.Value);
                    item.ContainersPerPallet = Convert.ToInt32(txtContainersPerPallet.Text.Trim());
                    item.ItemsPerContainer = Convert.ToInt32(txtItemsPerContainer.Text.Trim());
                    items.Add(item);
                }
                purchaseOrder.PurchaseOrderItems = items;
                if (string.IsNullOrEmpty(purchaseOrder.CustomerAccountNumber))
                {
                    lblMsg.Text = "Customer order number required!";
                    return;
                }
                string response = purchaseOrderOperation.UpdatePurchaseOrder(purchaseOrder, userID);
                // gvPOQuery.EditIndex = -1;
                Session["POS"] = purchaseOrders;
                //gvPOQuery.DataSource = purchaseOrders.PurchaseOrderList;
                //gvPOQuery.DataBind();
                //TriggerClientGridRefresh();
                lblMsg.Text = response;// "Updated successfully";
                // RegisterStartupScript("jsUnblockDialog", "closeEditDialog();");
                //}
                //else
                    //lblMsg.Text = "Fact order number required!";
            }
        }

    }
}