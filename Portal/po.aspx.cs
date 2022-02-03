using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace avii
{
    public partial class po : System.Web.UI.Page
    {
        private Int32 iEditIndex;
        //private int userID = 0;
        //private DataTable dtType;
        private List<avii.Classes.PurchaseOrderItem> dtType;
        protected void Page_Load(object sender, EventArgs e)
        {
            fnRedirect();
            //if (Session["adm"] == null)
            //{
            //    string url = "/avii/logon.aspx";
            //    try
            //    {
            //        HeadAdmin.Visible = false;
            //        url = ConfigurationSettings.AppSettings["LogonPage"].ToString();

            //    }
            //    catch
            //    {
            //        url = "/avii/logon.aspx";
            //    }
            //    if (Session["UserID"] == null)
            //    {
            //        Response.Redirect(url);
            //    }
            //}
            //else
            //{
            //    MenuHeader.Visible = false;
            //    HeadAdmin.Visible = true;
            //}


            if (!this.IsPostBack)
            {
                int userID = 0;
                txtPoDate.Text = DateTime.Now.ToShortDateString();
                BindStates();
                BindShipBy();
                if (Session["adm"] != null)
                {
                    BindCompanies();
                    pnlCustomer.Visible = true;
                    pnlStore.Visible = false;
                }
                else
                {
                    if (Session["userInfo"] != null)
                    {
                        avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                        if (userInfo != null && userInfo.UserGUID > 0)
                        {
                            userID = userInfo.UserGUID;
                            ViewState["userid"] = userID;
                            if (userID > 0)
                                BindUserStores(userID, 0);
                        }
                        pnlCustomer.Visible = false;
                        pnlStore.Visible = true;

                    }
                }
                

                avii.Classes.InventoryList inventoryList = GetInventoryItems(userID);
                if (inventoryList != null && inventoryList.CurrentInventory != null && inventoryList.CurrentInventory.Count > 0)
                {
                    if (Request.Params["fid"] != null)
                    {
                        int forecastId = 0;
                        if (Request.UrlReferrer != null)
                            ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                        else
                            ViewState["UrlReferrer"] = null;

                        int.TryParse(Request.Params["fid"], out forecastId);
                        //lnkf.Visible = true;
                        if (forecastId > 0)
                        {
                            ViewState["fid"] = forecastId;
                            ViewState["ftype"] = Request.Params["fty"];
                            Session["PoList"] = AddForecastData(forecastId);
                            this.lnkf.NavigateUrl = "./avforecast.aspx?i=" + Request.Params["fty"].ToString() ;
                        }
                        if (Request["c"] != null)
                        {
                            int companyID = 0;
                            int.TryParse(Request.Params["c"], out companyID);
                            ddlCustomer.SelectedValue = companyID.ToString();
                            BindUserStores(0, companyID);

                        }
                    }
                    else
                    {
                        lnkf.Visible = false;
                        Session.Remove("PoList");
                    }

                    datagridBind(1);
                    if (ViewState["storeCount"] == null)
                    {
                        btnSave.Enabled = false;
                        btnCancel.Enabled = false;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        btnCancel.Enabled = true;
                    }
                }
                else
                {
                    if(ViewState["storeCount"] != null)
                        lblMsg.Text = "No inventory is assigned to this user, please contact Lan Global administrator";
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                }
            }
        }
        protected void ddlStoreID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["userstore"] != null)
            {
                List<avii.Classes.StoreLocation> userStoreList = (List<avii.Classes.StoreLocation>)Session["userstore"];

                var storeList = (from item in userStoreList where item.StoreID.Equals(ddlStoreID.SelectedValue) select item).ToList();
                if (storeList != null && storeList.Count > 0)
                {
                    txtAddress1.Text = storeList[0].StoreAddress.Address1;
                    txtAddress2.Text = storeList[0].StoreAddress.Address2;
                    txtCity.Text = storeList[0].StoreAddress.City;
                    dpState.SelectedValue = storeList[0].StoreAddress.State;
                    txtZip.Text = storeList[0].StoreAddress.Zip;
                    //txtPhone.Text = storeList[0].StoreContact.OfficePhone1;
                    //txtContactName.Text = storeList[0].StoreContact.ContactName;

                }
                                    //item.st.Equals(poID) select item
            }
            else
                Response.Redirect("po.aspx", false);
        }
        private void SetStoreName()
        {
            if (ddlStoreID.SelectedIndex > 0)
            {
                string selectedValue = ddlStoreID.SelectedValue;
                string[] arr = selectedValue.Split('!');
                //if (arr.Length > 1)
                //    lblStoreName.Text = arr[1];
            }
            else
                lblMsg.Text = "Store is required";
        }
        private void BindUserStores(int userID, int companyID)
        {
            List<avii.Classes.StoreLocation> storeList = avii.Classes.UserStoreOperation.GetUserStoreLocationList(companyID, userID);

            //DataTable dt = avii.Classes.clsCompany.GetCompanyStores(userID);
            
            if (storeList != null && storeList.Count > 0)
            {
                Session["userstore"] = storeList;
                ddlStoreID.DataSource = storeList;
                ddlStoreID.DataValueField = "StoreID";
                ddlStoreID.DataTextField = "CompositeKeyStoreIdStoreName";
                ddlStoreID.DataBind();
                ddlStoreID.Items.Insert(0, new ListItem("", ""));
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                pnlStore.Visible = true;
                ViewState["storeCount"] = 1;
                lblMsg.Text = string.Empty;
            }
            else
            {
                Session["userstore"] = null;
                ddlStoreID.Items.Clear();
                ddlStoreID.DataSource = null;
                ddlStoreID.DataBind();
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                lblMsg.Text = "No store assigned to this user, please contact Aervoice administrator to get more infomration.";

            }
            
        }
        private void fnRedirect()
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    //HeadAdmin.Visible = false;
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
        }
        private void BindShipBy()
        {

            dpShipBy.DataSource = avii.Classes.PurchaseOrder.GetShipByList();
            dpShipBy.DataTextField = "ShipByText";
            dpShipBy.DataValueField = "ShipByCode";
            dpShipBy.DataBind();

        }
        private void BindCompanies()
        {
            DataTable dataTable = Session["companylist"] as DataTable;
            if (!(dataTable != null && dataTable.Rows.Count > 0))
            {
                dataTable = avii.Classes.clsCompany.GetCompany(0, 1);
            }

            ddlCustomer.DataSource = dataTable;
            ddlCustomer.DataTextField = "CompanyName";
            ddlCustomer.DataValueField = "CompanyID";
            ddlCustomer.DataBind();
            
        }
        private void BindStates()
        {
            DataTable dataTable = avii.Classes.clsCompany.GetState(0);
            
            dpState.DataSource = dataTable;
            dpState.DataTextField = "StateCodeName";
            dpState.DataValueField = "Statecode";
            dpState.DataBind();
            ListItem item = new ListItem("", "");
            dpState.Items.Insert(0, item);

            dpState.SelectedValue = "CA";

        }
        private List<avii.Classes.PurchaseOrderItem> AddForecastData(int forecastGUID)
        {
            avii.Classes.PurchaseOrderItem poItem = null;
            List<avii.Classes.PurchaseOrderItem> poItemList = null;
            avii.Classes.Forecast forecast= avii.Classes.ItemForecastController.GetForecast(forecastGUID);
            if (forecast != null)
            {
                poItemList = new List<avii.Classes.PurchaseOrderItem>();
                poItem = new avii.Classes.PurchaseOrderItem();
                poItem.ItemID = forecast.ForecastInternalData.ItemID;
                poItem.ItemCode = forecast.ForecastSku;
                poItem.Quantity = forecast.ForecastQty;
                poItemList.Add(poItem);
            }

            return poItemList;
        }

        private avii.Classes.InventoryList GetInventoryItems(int userID)
        {
            if (Session["inventory"] == null)
            {
                Session["inventory"] = avii.Classes.PurchaseOrder.GetInventoryItems(userID, -1, 0);
            }

            avii.Classes.InventoryList inventoryList = Session["inventory"] as avii.Classes.InventoryList;

            return inventoryList;
        }

        private List<avii.Classes.PurchaseOrderItem> CreateDataTable()
        {
            List<avii.Classes.PurchaseOrderItem> poList = null;
            if (Session["PoList"] != null)
            {
                poList = Session["PoList"] as List<avii.Classes.PurchaseOrderItem>;
            }
            else
            {
                poList = new List<avii.Classes.PurchaseOrderItem>();
            }

            return poList;
        }

        
        private void datagridBind(Int32 iRowAdd)
        {
            if (Session["PoList"] == null)
                dtType = CreateDataTable();
            else
                dtType = Session["PoList"] as List<avii.Classes.PurchaseOrderItem>;
            
            AddTableRows(iRowAdd);
            Session["PoList"] = dtType;
            dgPoItem.DataSource = dtType;
            dgPoItem.DataBind();

            
        }

        private void AddTableRows(Int32 iRowAdd)
        {
            if (Session["PoList"] == null)
                dtType = CreateDataTable();
            else
                dtType = Session["PoList"] as List<avii.Classes.PurchaseOrderItem>; //(DataTable)Session["PoList"];

            if (iRowAdd > 0)
            {
                avii.Classes.PurchaseOrderItem pitem = new avii.Classes.PurchaseOrderItem();
                dtType.Insert(0,pitem);
            }
        }

        protected void dg_ItemCreated(System.Object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            System.Web.UI.WebControls.LinkButton lb;
            if (dgPoItem.CurrentPageIndex == 0)
            {
                if (e.Item.ItemIndex >= 0)
                {
                    lb = (LinkButton)e.Item.Cells[0].Controls[0];
                    if (e.Item.ItemIndex == 0)
                    {
                        if (lb.Text == "Edit")
                            lb.Text = "Add";
                        else if (lb.Text == "Update")
                            lb.Text = "Insert";
                    }
                    else
                        lb.Visible = false;
                }
            }
        }

        protected void dg_Cancel(System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            dgPoItem.EditItemIndex = -1;
            ViewState["EditIndex"] = null;
            lblMsg.Text = string.Empty;
            datagridBind(0);
            //SetStoreName();
        }

        protected void dg_Edit(System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            iEditIndex = e.Item.ItemIndex;
            ViewState["EditIndex"] = iEditIndex;
            dgPoItem.EditItemIndex = iEditIndex;
            datagridBind(0);
            //SetStoreName();
        }

        protected void dg_Update(System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            string itemCode, qty, itemidval, phoneCatg;
            int iqty, itemID;
            try
            {
                lblMsg.Text = string.Empty;
                iEditIndex = (Int32)ViewState["EditIndex"];
                itemCode = ((DropDownList)e.Item.Cells[2].Controls[1]).SelectedItem.Text;
                itemidval = ((DropDownList)e.Item.Cells[2].Controls[1]).SelectedValue;
                qty = ((TextBox)e.Item.Cells[3].Controls[1]).Text;
                phoneCatg = ((DropDownList)e.Item.Cells[4].Controls[1]).SelectedValue;
                int.TryParse(qty, out iqty);
                if (string.IsNullOrEmpty(itemCode) || iqty <= 0)
                {
                    lblMsg.Text = "Item code and Quantity should be entered to save item";
                }
                else
                {
                    dtType = Session["PoList"] as List<avii.Classes.PurchaseOrderItem>;
                    if ((dtType != null && dtType.Count > 0) && iEditIndex <= dtType.Count)
                    {
                        avii.Classes.PurchaseOrderItem pItem = dtType[iEditIndex];
                        pItem.ItemCode = itemCode;
                        int.TryParse(itemidval, out itemID);
                        pItem.Quantity = iqty;
                        if  (phoneCatg == "H")
                            pItem.PhoneCategory = avii.Classes.PhoneCategoryType.Hot;
                        else
                            pItem.PhoneCategory = avii.Classes.PhoneCategoryType.Cold;

                        if (itemID > 0)
                        {
                            pItem.ItemID = itemID;
                            Session["PoList"] = dtType;
                            dgPoItem.EditItemIndex = -1;
                            datagridBind(1);
                        }
                    }
                    ViewState["EditIndex"] = null;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            //SetStoreName();
        }

        private void setData()
        {
            avii.Classes.clsPurchaseOrder po = new avii.Classes.clsPurchaseOrder();
            
        }

        protected void dg_ItemCommand(System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            lblMsg.Text = string.Empty;

            if (e.CommandName == "delete" && e.Item.ItemIndex > 0)
            {
                dtType = Session["PoList"] as List<avii.Classes.PurchaseOrderItem>;
                dtType.RemoveAt(e.Item.ItemIndex);
                Session["PoList"] = dtType;
                datagridBind(0);
                //SetStoreName();
            }
        }

        protected void dg_ItemBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                if (dgPoItem.EditItemIndex >= 0)
                {
                    int userID = 0;
                    if (ViewState["userid"] != null)
                        userID = Convert.ToInt32(ViewState["userid"]);
                    DropDownList dp = e.Item.FindControl("dpItem") as DropDownList;
                    if (dp != null)
                    {
                        dp.DataSource = GetInventoryItems(userID).CurrentInventory;
                        dp.DataTextField = "ItemCode";
                        dp.DataValueField = "ItemID";
                        dp.DataBind();
                    }
                }
            }
        }

        private List<avii.Classes.PurchaseOrderItem> CleanItemList(List<avii.Classes.PurchaseOrderItem> poItems)
        {
            if (poItems != null)
            {
                List<avii.Classes.PurchaseOrderItem> tempItems = new List<avii.Classes.PurchaseOrderItem>();
                List<avii.Classes.PurchaseOrderItem> Itemsgreater = new List<avii.Classes.PurchaseOrderItem>();
                foreach (avii.Classes.PurchaseOrderItem poItem in poItems)
                {
                    if (string.IsNullOrEmpty(poItem.ItemCode) || poItem.Quantity == 0 || poItem.Quantity == null)
                    {
                        tempItems.Add(poItem);
                    }

                    if (poItem.Quantity != null && poItem.Quantity > 1)
                    {
                        Itemsgreater.Add(poItem);
                        tempItems.Add(poItem);
                    }
                }

                foreach (avii.Classes.PurchaseOrderItem poItem in tempItems)
                {
                    poItems.Remove(poItem);

                }

                int? qty = 0;
                foreach (avii.Classes.PurchaseOrderItem poItem in Itemsgreater)
                {
                    qty = (poItem.Quantity!=null?poItem.Quantity:0);
                    poItem.Quantity = 1;
                    for (int ctr = 0; ctr <= qty-1; ctr++)
                    {
                        poItems.Add(poItem);
                    }
                }
            }

            return poItems;
        }
        
        private bool ValidateSave()
        {
            bool success = true;
            string errormessage = string.Empty;
            if (string.IsNullOrEmpty(txtPoNum.Text.Trim()))
            {
                success = false;
                errormessage = "Purchase Order Number is required";
            }
            else if (string.IsNullOrEmpty(txtPoDate.Text.Trim()))
            {
                success = false;
                errormessage = "Purchase Order Date is required";
            }
            else if (ddlStoreID.SelectedIndex==0)
            {
                success = false;
                errormessage = "StoreID is required";
            }
            else if (string.IsNullOrEmpty(txtContactName.Text.Trim()))
            {
                success = false;
                errormessage = "Contact Name is required";
            }
            else  if (string.IsNullOrEmpty(txtAddress1.Text.Trim()))
            {
                success = false;
                errormessage = "Address is required";
            }
            else if (string.IsNullOrEmpty(txtCity.Text.Trim()))
            {
                success = false;
                errormessage = "City name is required";
            }
            else if (string.IsNullOrEmpty(txtZip.Text.Trim()))
            {
                success = false;
                errormessage = "Zip code is required";
            }
            if (Session["adm"] != null)
            {
                if (ddlCustomer.SelectedIndex == 0)
                {
                    success = false;
                    errormessage = "Company name is required";
                }
            }

            lblMsg.Text = errormessage;

            return success;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int userid = 0;
            int forecastID = 0;
            if (ViewState["fid"] != null)
                forecastID = Convert.ToInt32(ViewState["fid"]);
           
            if (ViewState["userid"] != null)
                userid = Convert.ToInt32(ViewState["userid"]);
            else
                if (ViewState["companyID"] != null)
                {
                   avii.Classes.RMAUserCompany companyInfo  = avii.Classes.RMAUtility.getRMAUserCompanyInfo(Convert.ToInt32(ViewState["companyID"]), string.Empty, -1, -1);

                   if (companyInfo != null && companyInfo.UserID > 0)
                   {
                       userid = companyInfo.UserID; 
                   }

                }
            if (dgPoItem.EditItemIndex >= 0)
            {
                lblMsg.Text = "Purchase Order Phone section is in Edit mode, please click on cancel or update link";
            }
            else
            {
                if (userid > 0)
                {
                    lblMsg.Text = string.Empty;
                    avii.Classes.clsPurchaseOrder purchaseOrder = new avii.Classes.clsPurchaseOrder();
                    purchaseOrder.PurchaseOrderItems = Session["PoList"] as List<avii.Classes.PurchaseOrderItem>;
                    purchaseOrder.PurchaseOrderItems = CleanItemList(Session["PoList"] as List<avii.Classes.PurchaseOrderItem>);
                    if (purchaseOrder.PurchaseOrderItems.Count == 0)
                    {
                        lblMsg.Text = "Can not create purchase order without item";
                        datagridBind(1);
                    }
                    else if (ValidateSave())
                    {
                        purchaseOrder.PurchaseOrderNumber = txtPoNum.Text.Trim();
                        purchaseOrder.Comments = txtCommments.Text.Trim();
                        purchaseOrder.StoreID = ddlStoreID.SelectedValue;
                        purchaseOrder.CustomerNumber = string.Empty;
                        purchaseOrder.PurchaseOrderDate = Convert.ToDateTime(txtPoDate.Text.Trim());
                        purchaseOrder.Shipping.ContactName = txtContactName.Text.Trim();
                        purchaseOrder.Shipping.ContactPhone = txtPhone.Text.Trim();
                        purchaseOrder.Shipping.ShipToAddress = txtAddress1.Text.Trim();
                        purchaseOrder.Shipping.ShipToAddress2 = txtAddress2.Text.Trim();
                        purchaseOrder.Shipping.ShipToCity = txtCity.Text.Trim();
                        purchaseOrder.Shipping.ShipToState = dpState.SelectedValue;
                        purchaseOrder.Shipping.ShipToZip = txtZip.Text.Trim();
                        purchaseOrder.ShipThrough = dpShipBy.SelectedValue;
                        avii.Classes.PurchaseOrderResponse response = avii.Classes.PurchaseOrder.SaveNewPurchaseOrder(purchaseOrder, userid, forecastID, avii.Classes.PurchaseOrderFlag.W.ToString());
                        if (string.IsNullOrEmpty(response.ErrorCode))
                        {
                            CleanForm();
                            lblMsg.Text = "Purchase Order is successfully saved";
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(response.PurchaseOrderNumber))
                                lblMsg.Text = "Could not save the Purchase Order:" + response.ErrorCode;
                            else
                                lblMsg.Text = "Could not save the Purchase Order:" + response.PurchaseOrderNumber + " already exists";
                        }
                    }
                }
                else
                {
                    fnRedirect();
                }
            }
        }

        private void CleanForm()
        {
            //txtStoreID.Text = string.Empty;
            //lblStoreName.Text = string.Empty;
            txtCommments.Text = string.Empty;
            txtPoNum.Text = string.Empty;
            txtPhone.Text = string.Empty;                
            txtPoDate.Text = DateTime.Now.ToShortDateString();
            txtContactName.Text = string.Empty;
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtCity.Text = string.Empty;
            dpState.SelectedValue = string.Empty;
            txtZip.Text = string.Empty;
            lblMsg.Text = string.Empty;
            //txtCustNumber.Text = string.Empty;
            Session.Remove("PoList");
            datagridBind(1);
            if (Session["adm"] != null)
            {
                ddlCustomer.SelectedIndex = 0;
            }
            ddlStoreID.SelectedIndex = 0;
            dpShipBy.SelectedIndex = 0;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (ViewState["UrlReferrer"] == null)
                CleanForm();
            else
                if (ViewState["UrlReferrer"] != null)
                    Response.Redirect(ViewState["UrlReferrer"].ToString(), false);
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int userID = 0;
            //if (ViewState["userid"] != null)
            //    userID = Convert.ToInt32(ViewState["userid"]);
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtCity.Text = string.Empty;
            dpState.SelectedValue = string.Empty;
            txtZip.Text = string.Empty;

            int companyID = Convert.ToInt32(ddlCustomer.SelectedValue);
            BindUserStores(0, companyID);
            ViewState["companyID"] = companyID;
            avii.Classes.RMAUserCompany companyInfo = avii.Classes.RMAUtility.getRMAUserCompanyInfo(companyID, string.Empty, -1, -1);
            if (companyInfo != null && companyInfo.UserID > 0)
            {
                Session["inventory"] = null;
                ViewState["userid"] = companyInfo.UserID;
                avii.Classes.InventoryList inventoryList = GetInventoryItems(companyInfo.UserID);
                if (inventoryList != null && inventoryList.CurrentInventory != null && inventoryList.CurrentInventory.Count > 0)
                {
                    Session["PoList"] = null;
                    datagridBind(1);
                
                }
                else
                {
                    lblMsg.Text = "No inventory is assigned to this user, please contact administrator";
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                }
            }
            //pnlCustomer.Visible = true;
            pnlStore.Visible = true;
        }
        

    }
}
