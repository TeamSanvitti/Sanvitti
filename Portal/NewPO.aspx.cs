using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using SV.Framework.Fulfillment;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Inventory;

namespace avii
{
    public partial class NewPO : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            fnRedirect();

            if (!this.IsPostBack)
            {
                ReadOlnyAccess();
                int userID = 0;
                txtPoNum.Text = DateTime.Now.Ticks.ToString();
                txtPoDate.Text = DateTime.Now.ToShortDateString();
                txtRequestedshipdate.Text = DateTime.Now.ToShortDateString();
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
                            ViewState["POCustNoValidate"] = userInfo.POCustNoValidate;
                            userID = userInfo.UserGUID;
                            ViewState["userid"] = userID;
                            if (userID > 0)
                                BindUserStores(userID, 0);
                        }
                        pnlCustomer.Visible = false;
                        pnlStore.Visible = true;

                    }
                }

                if (userID > 0)
                {
                    InventoryList inventoryList = GetInventoryItems(userID, 1);
                    if (inventoryList != null && inventoryList.CurrentInventory != null && inventoryList.CurrentInventory.Count > 0)
                    {
                        //if (Request.Params["fid"] != null)
                        //{
                        //    int forecastId = 0;
                        //    if (Request.UrlReferrer != null)
                        //        ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                        //    else
                        //        ViewState["UrlReferrer"] = null;

                        //    int.TryParse(Request.Params["fid"], out forecastId);
                        //    //lnkf.Visible = true;
                        //    if (forecastId > 0)
                        //    {
                        //        ViewState["fid"] = Request.Params["fid"];
                        //        ViewState["ftype"] = Request.Params["fty"];
                        //        Session["NewPoList"] = AddForecastData(forecastId);
                        //        this.lnkf.NavigateUrl = "./avforecast.aspx?i=" + Request.Params["fty"].ToString();
                        //    }
                        //    if (Request["c"] != null)
                        //    {
                        //        int companyID = 0;
                        //        int.TryParse(Request.Params["c"], out companyID);
                        //        ddlCustomer.SelectedValue = companyID.ToString();
                        //        BindUserStores(0, companyID);

                        //    }
                        //}
                        //else
                        //{
                        //    lnkf.Visible = false;
                        //    Session.Remove("NewPoList");
                        //}


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
                        //if (ViewState["storeCount"] == null)
                         //   lblMsg.Text = "No inventory is assigned to this user, please contact Lan Global administrator";
                        btnSave.Enabled = false;
                        btnCancel.Enabled = false;
                    }

                }
                datagridBind(1);
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
        private void BindShipBy()
        {
            PurchaseOrder purchaseOrderOperation = PurchaseOrder.CreateInstance<PurchaseOrder>();

            dpShipBy.DataSource = purchaseOrderOperation.GetShipByList();
            dpShipBy.DataTextField = "ShipByText";
            dpShipBy.DataValueField = "ShipByCode";
            dpShipBy.DataBind();

        }
        protected void ddlStoreID_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtCity.Text = string.Empty;
            dpState.SelectedIndex = 0;
            txtZip.Text = string.Empty;
            //txtPhone.Text = storeList[0].StoreContact.OfficePhone1;
            txtContactName.Text = string.Empty;
            try
            {
                if (Session["userstore"] != null)
                {
                    List<SV.Framework.Admin.StoreLocation> userStoreList = (List<SV.Framework.Admin.StoreLocation>)Session["userstore"];

                    var storeList = (from item in userStoreList where item.StoreID.Equals(ddlStoreID.SelectedValue) select item).ToList();
                    if (storeList != null && storeList.Count > 0)
                    {
                        txtAddress1.Text = storeList[0].StoreAddress.Address1;
                        txtAddress2.Text = storeList[0].StoreAddress.Address2;
                        txtCity.Text = storeList[0].StoreAddress.City;
                        dpState.SelectedValue = storeList[0].StoreAddress.State;
                        txtZip.Text = storeList[0].StoreAddress.Zip;
                        //txtPhone.Text = storeList[0].StoreContact.OfficePhone1;
                        txtContactName.Text = storeList[0].ShipContactName;

                    }
                    //item.st.Equals(poID) select item
                }
                else
                    Response.Redirect("po.aspx", false);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
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
            List<SV.Framework.Admin.StoreLocation> storeList = SV.Framework.Admin.UserStoreOperation.GetUserStoreLocationList(companyID, userID);

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
                lblMsg.Text = "No store assigned to this user, please contact administrator to get more infomration.";

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
        //private List<PurchaseOrderItem> AddForecastData(int forecastGUID)
        //{
        //    PurchaseOrderItem poItem = null;
        //    List<PurchaseOrderItem> poItemList = null;
        //    avii.Classes.Forecast forecast = avii.Classes.ItemForecastController.GetForecast(forecastGUID);
        //    if (forecast != null)
        //    {
        //        poItemList = new List<PurchaseOrderItem>();
        //        poItem = new PurchaseOrderItem();
        //        poItem.ItemID = forecast.ForecastInternalData.ItemID;
        //        poItem.ItemCode = forecast.ForecastSku;
        //        poItem.Quantity = forecast.ForecastQty;
        //        poItemList.Add(poItem);
        //    }

        //    return poItemList;
        //}

        private InventoryList GetInventoryItems(int userID, int sim)
        {
            SV.Framework.Inventory.InventoryOperation InventoryOperation = SV.Framework.Inventory.InventoryOperation.CreateInstance<SV.Framework.Inventory.InventoryOperation>();
            if (Session["inventory"] == null)
            {
                if (sim == -1)
                    Session["inventory"] = InventoryOperation.GetInventoryItems(userID, sim, 0);
                else
                    Session["inventory"] = InventoryOperation.GetInventoryItems(userID, sim, 0);
            }

            InventoryList inventoryList = Session["inventory"] as InventoryList;

            return inventoryList;
        }

        private List<PurchaseOrderItem> CreateDataTable()
        {
            List<PurchaseOrderItem> poList = null;
            if (Session["NewPoList"] != null)
            {
                poList = Session["NewPoList"] as List<PurchaseOrderItem>;
            }
            else
            {
                poList = new List<PurchaseOrderItem>();
            }

            return poList;
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            int itemID, iqty;
            string itemCode, qty, itemidval, phoneCatg;
            List<PurchaseOrderItem> poList = new List<PurchaseOrderItem>();
            PurchaseOrderItem pitem = new PurchaseOrderItem();
            poList.Insert(0, pitem);
            itemID = 0;
            itemCode = string.Empty;
            foreach (RepeaterItem item in rptItem.Items)
            {

                pitem = new PurchaseOrderItem();
                //DropDownList dpItem = item.FindControl("dpItem") as DropDownList;
                TextBox txtQty = item.FindControl("txtQty") as TextBox;
                DropDownList dpCategory = item.FindControl("dpCategory") as DropDownList;
                qty = txtQty.Text.Trim();
                int.TryParse(qty, out iqty);
                //int.TryParse(dpItem.SelectedValue, out itemID);
                phoneCatg = dpCategory.SelectedValue;
                //itemCode = dpItem.SelectedItem.Text;

                if (string.IsNullOrEmpty(itemCode) || iqty <= 0)
                {
                    lblMsg.Text = "Item code and Quantity should be entered to save item";
                }
                else
                {
                    pitem.ItemCode = itemCode;
                    pitem.Quantity = iqty;
                    if (phoneCatg == "H")
                        pitem.PhoneCategory = PhoneCategoryType.Hot;
                    else
                        pitem.PhoneCategory = PhoneCategoryType.Cold;


                    if (itemID > 0)
                    {
                        pitem.ItemID = itemID;
                        poList.Add(pitem);
                        //Session["NewPoList"] = poList;
                        //dgPoItem.EditItemIndex = -1;
                        //datagridBind(1);
                    }
                }
            }
            Session["NewPoList"] = poList;
            rptItem.DataSource = poList;
            rptItem.DataBind();

        }
        private void datagridBind(Int32 iRowAdd)
        {
            List<PurchaseOrderItem> poList = null;
            if (Session["NewPoList"] == null)
                poList = CreateDataTable();
            else
                poList = Session["NewPoList"] as List<PurchaseOrderItem>;

            AddTableRows(iRowAdd, ref poList);
            Session["NewPoList"] = poList;
            //dgPoItem.DataSource = dtType;
            //dgPoItem.DataBind();

            rptItem.DataSource = poList;
            rptItem.DataBind();
        }

        private void AddTableRows(Int32 iRowAdd, ref List<PurchaseOrderItem> poList)
        {
            if (Session["NewPoList"] == null)
                poList = CreateDataTable();
            else
                poList = Session["NewPoList"] as List<PurchaseOrderItem>; //(DataTable)Session["NewPoList"];

            if (iRowAdd > 0)
            {
                poList.Clear();
                btnSave.Enabled = false;
                PurchaseOrderItem pitem = new PurchaseOrderItem();
                poList.Insert(0, pitem);
            }
        }

        #region CREATE PO OLD GRID code
        //protected void dg_ItemCreated(System.Object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        //{
        //    System.Web.UI.WebControls.LinkButton lb;
        //    if (dgPoItem.CurrentPageIndex == 0)
        //    {
        //        if (e.Item.ItemIndex >= 0)
        //        {
        //            lb = (LinkButton)e.Item.Cells[0].Controls[0];
        //            if (e.Item.ItemIndex == 0)
        //            {
        //                if (lb.Text == "Edit")
        //                    lb.Text = "Add";
        //                else if (lb.Text == "Update")
        //                    lb.Text = "Insert";
        //            }
        //            else
        //                lb.Visible = false;
        //        }
        //    }
        //}

        //protected void dg_Cancel(System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        //{
        //    dgPoItem.EditItemIndex = -1;
        //    ViewState["EditIndex"] = null;
        //    lblMsg.Text = string.Empty;
        //    datagridBind(0);
        //    //SetStoreName();
        //}

        //protected void dg_Edit(System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        //{
        //    iEditIndex = e.Item.ItemIndex;
        //    ViewState["EditIndex"] = iEditIndex;
        //    dgPoItem.EditItemIndex = iEditIndex;
        //    datagridBind(0);
        //    //SetStoreName();
        //}

        //protected void dg_Update(System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        //{
        //    string itemCode, qty, itemidval, phoneCatg;
        //    int iqty, itemID;
        //    try
        //    {
        //        lblMsg.Text = string.Empty;
        //        iEditIndex = (Int32)ViewState["EditIndex"];
        //        itemCode = ((DropDownList)e.Item.Cells[2].Controls[1]).SelectedItem.Text;
        //        itemidval = ((DropDownList)e.Item.Cells[2].Controls[1]).SelectedValue;
        //        qty = ((TextBox)e.Item.Cells[3].Controls[1]).Text;
        //        phoneCatg = ((DropDownList)e.Item.Cells[4].Controls[1]).SelectedValue;
        //        int.TryParse(qty, out iqty);
        //        if (string.IsNullOrEmpty(itemCode) || iqty <= 0)
        //        {
        //            lblMsg.Text = "Item code and Quantity should be entered to save item";
        //        }
        //        else
        //        {
        //            dtType = Session["NewPoList"] as List<PurchaseOrderItem>;
        //            if ((dtType != null && dtType.Count > 0) && iEditIndex <= dtType.Count)
        //            {
        //                PurchaseOrderItem pItem = dtType[iEditIndex];
        //                pItem.ItemCode = itemCode;
        //                int.TryParse(itemidval, out itemID);
        //                pItem.Quantity = iqty;
        //                if  (phoneCatg == "H")
        //                    pItem.PhoneCategory = avii.Classes.PhoneCategoryType.Hot;
        //                else
        //                    pItem.PhoneCategory = avii.Classes.PhoneCategoryType.Cold;

        //                if (itemID > 0)
        //                {
        //                    pItem.ItemID = itemID;
        //                    Session["NewPoList"] = dtType;
        //                    dgPoItem.EditItemIndex = -1;
        //                    datagridBind(1);
        //                }
        //            }
        //            ViewState["EditIndex"] = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Text = ex.Message;
        //    }
        //    //SetStoreName();
        //}

        //private void setData()
        //{
        //    avii.Classes.clsPurchaseOrder po = new avii.Classes.clsPurchaseOrder();

        //}

        //protected void dg_ItemCommand(System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        //{
        //    lblMsg.Text = string.Empty;

        //    if (e.CommandName == "delete" && e.Item.ItemIndex > 0)
        //    {
        //        dtType = Session["NewPoList"] as List<PurchaseOrderItem>;
        //        dtType.RemoveAt(e.Item.ItemIndex);
        //        Session["NewPoList"] = dtType;
        //        datagridBind(0);
        //        //SetStoreName();
        //    }
        //}

        //protected void dg_ItemBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.EditItem)
        //    {
        //        if (dgPoItem.EditItemIndex >= 0)
        //        {
        //            int userID = 0;
        //            if (ViewState["userid"] != null)
        //                userID = Convert.ToInt32(ViewState["userid"]);
        //            DropDownList dp = e.Item.FindControl("dpItem") as DropDownList;
        //            if (dp != null)
        //            {
        //                dp.DataSource = GetInventoryItems(userID).CurrentInventory;
        //                dp.DataTextField = "ItemCode";
        //                dp.DataValueField = "ItemID";
        //                dp.DataBind();
        //            }
        //        }
        //    }
        //}
        #endregion
        private List<PurchaseOrderItem> CleanItemList(List<PurchaseOrderItem> poItems)
        {
            if (poItems != null)
            {
                List<PurchaseOrderItem> tempItems = new List<PurchaseOrderItem>();
                List<PurchaseOrderItem> Itemsgreater = new List<PurchaseOrderItem>();
                foreach (PurchaseOrderItem poItem in poItems)
                {
                    if (string.IsNullOrEmpty(poItem.ItemCode) || poItem.Quantity == 0 || poItem.Quantity == null)
                    {
                        tempItems.Add(poItem);
                    }

                    //if (poItem.Quantity != null && poItem.Quantity > 1)
                    //{
                    //    Itemsgreater.Add(poItem);
                    //    tempItems.Add(poItem);
                    //}
                }

                foreach (PurchaseOrderItem poItem in tempItems)
                {
                    poItems.Remove(poItem);

                }

                //int? qty = 0;
                //foreach (PurchaseOrderItem poItem in Itemsgreater)
                //{
                //    qty = (poItem.Quantity != null ? poItem.Quantity : 0);
                //    poItem.Quantity = 1;
                //    for (int ctr = 0; ctr <= qty - 1; ctr++)
                //    {
                //        poItems.Add(poItem);
                //    }
                //}
            }

            return poItems;
        }
        private List<PurchaseOrderItem> LoadItemListFromGrid()
        {
            int itemID, iqty, sim;
            ViewState["ItemCodeError"] = null;
            string itemCode, qty, phoneCatg, mdn;
            itemID = sim = 0;
            itemCode = mdn = string.Empty;
            List<PurchaseOrderItem> poList = new List<PurchaseOrderItem>();
            PurchaseOrderItem pitem;
            int userID = 0;
            if (ddlSKUType.SelectedValue == "SIM")
                sim = 1;
            if (ViewState["userid"] != null)
                userID = Convert.ToInt32(ViewState["userid"]);
            List<clsInventory> inventoryList = GetInventoryItems(userID, sim).CurrentInventory;
           
            foreach (RepeaterItem item in rptItem.Items)
            {
                pitem = new PurchaseOrderItem();
                
                TextBox txtItemCode = item.FindControl("txtItemCode") as TextBox;
                //TextBox txtMDN = item.FindControl("txtMDN") as TextBox;
                TextBox txtQty = item.FindControl("txtQty") as TextBox;
                //DropDownList dpCategory = item.FindControl("dpCategory") as DropDownList;
                CheckBox chkDel = item.FindControl("chkDel") as CheckBox;
                itemCode = txtItemCode.Text.Trim();
               // mdn = txtMDN.Text.Trim();
                if (!chkDel.Checked && !string.IsNullOrEmpty(itemCode))
                {
                    qty = txtQty.Text.Trim();
                    int.TryParse(qty, out iqty);
                    
                    //phoneCatg = dpCategory.SelectedValue;
                   
                    var itemCodeInfo = (from items in inventoryList where items.ItemCode.ToUpper().Equals(itemCode.ToUpper()) select items).ToList();
                    if (itemCodeInfo != null && itemCodeInfo.Count > 0)
                    {
                        //var duplicateItems = (from itemcodes in poList where itemcodes.ItemCode.ToUpper().Equals(itemCode.ToUpper()) select itemcodes).ToList();
                        //if (duplicateItems != null && duplicateItems.Count > 0 && !string.IsNullOrEmpty(duplicateItems[0].ItemCode))
                        //{
                        //    ViewState["ItemCodeError"] = itemCode + " already exists!";
                        //    txtItemCode.Text = string.Empty;
                        //}
                        //else
                        {
                            itemID = Convert.ToInt32(itemCodeInfo[0].ItemID);
                            if (iqty > 0)
                            {
                                if (itemID > 0)
                                {

                                    pitem.ItemCode = itemCode;
                                    pitem.Quantity = iqty;
                                    pitem.MdnNumber = mdn;
                                    //if (phoneCatg == "H")
                                    //    pitem.PhoneCategory = Classes.PhoneCategoryType.Hot;
                                    //else
                                    //    pitem.PhoneCategory = Classes.PhoneCategoryType.Cold;
                                    pitem.ItemID = itemID;
                                    poList.Add(pitem);
                                }
                            }
                            else
                                ViewState["qty"] = "Quantity can not be zero or empty";
                        }
                    }
                    else
                    {
                        ViewState["ItemCodeError"] = itemCode + " not assigned to this customer";
                    }
                }
            }
            Session["NewPoList"] = poList;
            return poList;
        }

        private bool ValidateSave()
        {
            bool success = true, POCustNoValidate = false;
            int companyID = 0;
            string errormessage = string.Empty;
            if(ViewState["POCustNoValidate"] != null)
            {
                POCustNoValidate = Convert.ToBoolean(ViewState["POCustNoValidate"]);
            }
            if (Session["adm"] != null)
            {
                if (ddlCustomer.SelectedIndex == 0)
                {
                    success = false;
                    errormessage = "Company name is required";
                }
                else
                    int.TryParse(ddlCustomer.SelectedValue, out companyID);
            }
            else
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null && userInfo.UserGUID > 0)
                    companyID = userInfo.CompanyGUID;

            }
            if (string.IsNullOrEmpty(txtPoNum.Text.Trim()))
            {
                success = false;
                errormessage = "Purchase Order Number is required";
            }
            else if (string.IsNullOrEmpty(txtCustOrderNo.Text.Trim()))
            {
                success = false;
                errormessage = "Customer Order number is required";
            }
            else if (txtCustOrderNo.Text.Trim().Length < 5)
            {
                success = false;
                if (POCustNoValidate)
                    errormessage = "Customer order number length should be between 8 to 11 characters";
                else
                    errormessage = "Customer order number length should be between 5 to 20 characters";
            }
            else if (POCustNoValidate && txtCustOrderNo.Text.Trim().Length > 11)
            {
                success = false;
                errormessage = "Customer order number length should be between 8 to 11 characters";
            }

            else if (string.IsNullOrEmpty(txtPoDate.Text.Trim()))
            {
                success = false;
                errormessage = "Purchase Order Date is required";
            }
            else if (ddlStoreID.SelectedIndex == 0)
            {
                success = false;
                errormessage = "StoreID is required";
            }
            else if (string.IsNullOrEmpty(txtContactName.Text.Trim()))
            {
                success = false;
                errormessage = "Contact Name is required";
            }
            else if (string.IsNullOrEmpty(txtAddress1.Text.Trim()))
            {
                success = false;
                errormessage = "Address is required";
            }
            else if (string.IsNullOrEmpty(txtPhone.Text.Trim()))
            {
                success = false;
                errormessage = "Customer Phone is required";
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
            
 
            DateTime currentDate = DateTime.Now;
            DateTime podate = new DateTime();
                
            podate = Convert.ToDateTime(txtPoDate.Text.Trim());
            TimeSpan diffResult = currentDate - podate;
            if (diffResult.Days > 90)
                errormessage = "Invalid Fulfillment Date! Can not create Fulfillment order before 90 days back.";

            lblMsg.Text = errormessage;

            return success;
        }
        protected void ddlSKUType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SV.Framework.Inventory.InventoryOperation InventoryOperation = SV.Framework.Inventory.InventoryOperation.CreateInstance<SV.Framework.Inventory.InventoryOperation>();

            int sim = 0, userID = 0;
            if (ddlSKUType.SelectedValue == "SIM")
                sim = 1;

            if (ViewState["userid"] != null)
                userID = Convert.ToInt32(ViewState["userid"]);
            Session["inventory"] = InventoryOperation.GetInventoryItems(userID, sim, 0);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            PurchaseOrder purchaseOrderOperation = PurchaseOrder.CreateInstance<PurchaseOrder>();

            int userid = 0;
            int forecastID = 0;

            if (ViewState["fid"] != null)
                forecastID = Convert.ToInt32(ViewState["fid"]);

            if (ViewState["userid"] != null)
                userid = Convert.ToInt32(ViewState["userid"]);
            else
                if (ViewState["companyID"] != null)
                {
                SV.Framework.Models.RMA.RMAUserCompany companyInfo = rmaUtility.getRMAUserCompanyInfo(Convert.ToInt32(ViewState["companyID"]), string.Empty, -1, -1);

                    if (companyInfo != null && companyInfo.UserID > 0)
                    {
                        userid = companyInfo.UserID;
                    }

                }
            if (userid > 0)
            {
                lblMsg.Text = string.Empty;
                if (string.IsNullOrWhiteSpace(txtPoNum.Text))
                    txtPoNum.Text = DateTime.Now.Ticks.ToString();

                if (string.IsNullOrWhiteSpace(txtRequestedshipdate.Text))
                    txtRequestedshipdate.Text = DateTime.Now.ToShortDateString();

                clsPurchaseOrder purchaseOrder = new clsPurchaseOrder();
                //purchaseOrder.PurchaseOrderItems = LoadItemListFromGrid();

                List<PurchaseOrderItem> purchaseOrderItems = LoadItemListFromGrid();
                //purchaseOrder.PurchaseOrderItems = Session["NewPoList"] as List<PurchaseOrderItem>;
                if (ViewState["ItemCodeError"] == null)
                {
                    if (ViewState["qty"] == null)
                    {
                        purchaseOrder.PurchaseOrderItems = CleanItemList(purchaseOrderItems);

                        if (purchaseOrder.PurchaseOrderItems.Count == 0)
                        {
                            lblMsg.Text = "Can not create purchase order without item";
                            datagridBind(1);
                        }
                        else if (ValidateSave())
                        {
                            purchaseOrder.FactOrderNumber = "";// txtFactOrderNumber.Text.Trim();
                            purchaseOrder.RequestedShipDate = Convert.ToDateTime(txtRequestedshipdate.Text.Trim());
                            purchaseOrder.PurchaseOrderStatus = PurchaseOrderStatus.Pending;
                            purchaseOrder.PurchaseOrderNumber = txtPoNum.Text.Trim();
                            purchaseOrder.CustomerOrderNumber = txtCustOrderNo.Text.Trim();
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
                            purchaseOrder.B2B = true;
                            purchaseOrder.POType = "B2B";

                            PurchaseOrderResponse response = purchaseOrderOperation.SaveNewPurchaseOrder(purchaseOrder, userid, forecastID, PurchaseOrderFlag.W.ToString());
                            if (string.IsNullOrEmpty(response.ErrorCode))
                            {
                                CleanForm();
                                lblMsg.Text = "<b>" + response.PurchaseOrderNumber + "</b> Purchase Order is successfully saved";
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(response.PurchaseOrderNumber))
                                    lblMsg.Text = "Could not save the Purchase Order:" + response.ErrorCode;
                                else
                                    lblMsg.Text = "Could not save the Purchase Order:" + purchaseOrder.CustomerOrderNumber + " already exists";

                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Quantity can not be zero or empty";
                        ViewState["qty"] = null;
                    }

                }
                else
                {
                    lblMsg.Text = "Could not save the Purchase Order: " + ViewState["ItemCodeError"].ToString();
                    ViewState["ItemCodeError"] = null;
                }
            }
            else
            {
                fnRedirect();
            }

        }

        private void CleanForm()
        {
            //txtStoreID.Text = string.Empty;
            //lblStoreName.Text = string.Empty;
            txtRequestedshipdate.Text = DateTime.Now.ToShortDateString();

           // txtFactOrderNumber.Text = string.Empty;
            txtCustOrderNo.Text = string.Empty;
            txtCommments.Text = string.Empty;
            txtPoNum.Text = DateTime.Now.Ticks.ToString();
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
            ViewState["ItemCodeError"] = null;
            ViewState["qty"] = null;
            Session["NewPoList"] = null;
            Session.Remove("NewPoList");
            datagridBind(1);
            if (Session["adm"] != null)
            {
                Session["inventory"] = null;
                Session.Remove("inventory");
                ViewState["userid"] = null;
                if (ddlCustomer.SelectedIndex > 0)
                    ddlStoreID.SelectedIndex = 0;
                ddlCustomer.SelectedIndex = 0;            
            }
            else
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
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            int sim = 0;
            if (ddlSKUType.SelectedValue == "SIM")
                sim = 1;
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtCity.Text = string.Empty;
            dpState.SelectedIndex = 0;
            txtZip.Text = string.Empty;
            //txtPhone.Text = storeList[0].StoreContact.OfficePhone1;
            txtContactName.Text = string.Empty;
            //int userID = 0;
            //if (ViewState["userid"] != null)
            //    userID = Convert.ToInt32(ViewState["userid"]);
            int companyID = Convert.ToInt32(ddlCustomer.SelectedValue);
            bool POCustNoValidate = false;
            BindUserStores(0, companyID);
            ViewState["companyID"] = companyID;
            SV.Framework.Models.RMA.RMAUserCompany companyInfo = rmaUtility.getRMAUserCompanyInfo(companyID, string.Empty, -1, -1);
            if (companyInfo != null && companyInfo.UserID > 0)
            {
                POCustNoValidate = companyInfo.POCustNoValidate;
                ViewState["POCustNoValidate"] = POCustNoValidate;
                Session["inventory"] = null;
                ViewState["userid"] = companyInfo.UserID;
                InventoryList inventoryList = GetInventoryItems(companyInfo.UserID, sim);
                if (inventoryList != null && inventoryList.CurrentInventory != null && inventoryList.CurrentInventory.Count > 0)
                {
                    Session["NewPoList"] = null;
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
        protected void lnkCode_Click(object sender, EventArgs e)
        {
            int userID = 0, sim = 0;
            //if(ddlCustomer.SelectedIndex > 0)
                
            if (ViewState["userid"] != null)
                userID = Convert.ToInt32(ViewState["userid"]);
            rptItemCode.DataSource = GetInventoryItems(userID, sim).CurrentInventory;
            rptItemCode.DataBind();
            ModalPopupExtender3.Show();

        }

        protected void rptItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int userID = 0;
                if (ViewState["userid"] != null)
                    userID = Convert.ToInt32(ViewState["userid"]);
                ImageButton img = e.Item.FindControl("img") as ImageButton;
                if (userID > 0)
                    img.Visible = true;
                else
                    img.Visible = false;
                //DropDownList dpItem = (DropDownList)e.Item.FindControl("dpItem");
                //HiddenField hdnItemID = (HiddenField)e.Item.FindControl("hdnItemID");
                //if (dpItem != null)
                //{
                //    dpItem.DataSource = GetInventoryItems(userID).CurrentInventory;
                //    dpItem.DataTextField = "ItemCode";
                //    dpItem.DataValueField = "ItemID";
                //    dpItem.DataBind();
                //    dpItem.SelectedValue = hdnItemID.Value;
                //}


                //HiddenField hdnPhoneCategory = e.Item.FindControl("hdnPhCat") as HiddenField;
                //DropDownList dpCategory = e.Item.FindControl("dpCategory") as DropDownList;
                //if (dpCategory != null)
                //{
                //    if (hdnPhoneCategory != null)
                //    {
                //        if (hdnPhoneCategory.Value == "Cold")
                //            dpCategory.SelectedValue = "C";
                //        else
                //            dpCategory.SelectedValue = "H";
                //    }
                //}

            }
        }
        protected void txtItemCode_SelectedIndexChanged(object sender, EventArgs e)
         {
            int sim = 0;
            if (ddlSKUType.SelectedValue == "SIM")
                sim = 1;
            lblMsg.Text = string.Empty;
            TextBox txtItemCode = (TextBox)sender;
            //int qty = 1;
            RepeaterItem item = (RepeaterItem)txtItemCode.NamingContainer;
            //TextBox txtQty = (TextBox)item.FindControl("txtQty");
            Label lblCode = item.FindControl("lblCode") as Label;
            int itemIndex = item.ItemIndex;
            string itemCode = txtItemCode.Text.Trim();
            
            if (!string.IsNullOrWhiteSpace(itemCode))
            {
                List<PurchaseOrderItem> polist = Session["NewPoList"] as List<PurchaseOrderItem>;
                var duplicateItems = (from itemcodes in polist where itemcodes.ItemCode.ToUpper().Equals(itemCode.ToUpper()) select itemcodes).ToList();
                if (duplicateItems != null && duplicateItems.Count > 0 && !string.IsNullOrEmpty(duplicateItems[0].ItemCode))
                {
                    txtItemCode.Text = string.Empty;
                    lblMsg.Text = itemCode + " already added!";
                    //ViewState["ItemCodeError"] = itemCode + " already exists!";
                    // txtItemCode.Text = string.Empty;
                }
                else
                {

                    //DropDownList dpCategory = (DropDownList)item.FindControl("dpCategory");
                    List<PurchaseOrderItem> poList = new List<PurchaseOrderItem>();

                    PurchaseOrderItem pItem;
                    int userID = 0;
                    if (ViewState["userid"] != null)
                        userID = Convert.ToInt32(ViewState["userid"]);
                    List<clsInventory> inventoryList = GetInventoryItems(userID, sim).CurrentInventory;
                    pItem = new PurchaseOrderItem();

                    var itemCodeInfo = (from items in inventoryList where items.ItemCode.ToUpper().Equals(txtItemCode.Text.Trim().ToUpper()) select items).ToList();
                    if (itemCodeInfo != null && itemCodeInfo.Count > 0)
                    {
                        pItem.ItemCode = itemCodeInfo[0].ItemCode;
                        pItem.ItemID = Convert.ToInt32(itemCodeInfo[0].ItemID);
                        
                        poList = FillPOList(pItem, itemIndex);
                        if (poList.Count - 1 == itemIndex)
                        {
                            pItem = new PurchaseOrderItem();
                            poList.Add(pItem);
                        }
                        btnSave.Enabled = true;

                    }
                    else
                    {
                        pItem.ItemCode = txtItemCode.Text;
                        pItem.ESN = txtItemCode.Text.Trim() + " not assigned to this customer";
                        poList = FillPOList(pItem, itemIndex);
                        //lblCode.Text = txtItemCode.Text.Trim() + " does not assigned to this user";
                    }
                    Session["NewPoList"] = poList;
                    rptItem.DataSource = poList;
                    rptItem.DataBind();
                }
                //if (rptItem.Items.Count == itemIndex)
                {
                    TextBox txtCode = (TextBox)rptItem.Items[itemIndex].FindControl("txtItemCode");
                    txtCode.Focus();
                }
            }
            else
            //if (rptItem.Items.Count == itemIndex)
            {
                Label lblsCode = (Label)rptItem.Items[itemIndex].FindControl("lblCode");
                lblsCode.Text = "SKU can not be empty";
            }






        }
        private List<PurchaseOrderItem> FillPOList(PurchaseOrderItem pItem, int selectedItemIndex)
        {
            // List<RMAEsnLookUp> rmaEsnLookups = ViewState["rmadetailslist"] as List<RMAEsnLookUp>;
            List<PurchaseOrderItem> poList = LoadItemListFromGrid();//Session["NewPoList"] as List<PurchaseOrderItem>;
            if (poList == null)
            {
                poList = new List<PurchaseOrderItem>();
            }

            if (poList.Count == selectedItemIndex)
            {
                poList.Add(pItem);
            }
            else
            {
                poList[selectedItemIndex] = pItem;
            }

            return poList;
        }

    }
}
