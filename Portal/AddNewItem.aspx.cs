using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Inventory;
using SV.Framework.Fulfillment;

namespace avii
{
    public partial class AddNewItem : System.Web.UI.Page
    {
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
            int poid = 0;
            if (Session["poid"] != null)
            {
                poid = Convert.ToInt32(Session["poid"]);
                ViewState["poid"] = poid;

            }
            if (!IsPostBack)
            {
                if (poid > 0)
                {
                    //Session["poid"] = null;
                    int companyid = BindPO(poid, true);
                    GetInventoryItems(companyid);
                    datagridBind(1);
                }
                
            }

        }
        
        public int BindPO(int poID, bool poQuery)
        {
            
            int companyid = 0;
            lblMsg.Text = string.Empty;
            PurchaseOrders purchaseOrders = null;
            if (poQuery)
                purchaseOrders = Session["POS"] as PurchaseOrders;
            else
                purchaseOrders = purchaseOrderOperation.GerPurchaseOrdersNew(null, null, null, null, 0, "0", 0, null, null, null, null, null, null, null, null, null, null, poID, null, null, null, 0);
            List<TrackingDetail> trackingList = new List<TrackingDetail>();

            List<BasePurchaseOrder> purchaseOrderList = purchaseOrders.PurchaseOrderList;
            List<BasePurchaseOrderItem> purchaseOrderItemList = purchaseOrderOperation.GetPurchaseOrderItemsAndTrackingList(poID, out trackingList);
            Session["poitems2"] = purchaseOrderItemList;
            //Session["potracking"] = trackingList;

            var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(poID) select item).ToList();
            if (poInfoList != null && poInfoList.Count > 0)
            {
               
                lblPO.Text = poInfoList[0].PurchaseOrderNumber;
                lblvStatus.Text = poInfoList[0].PurchaseOrderStatus.ToString();
                companyid = Convert.ToInt32(poInfoList[0].CompanyID);
                ViewState["companyid"] = poInfoList[0].CompanyID;
                gvPODetail.DataSource = purchaseOrderItemList;
                gvPODetail.DataBind();
                if (purchaseOrderItemList != null && purchaseOrderItemList.Count > 0)
                    lblPODCount.Text = "<strong>Total count:</strong> " + purchaseOrderItemList.Count;
                else
                    lblPODCount.Text = string.Empty;
                

            }

            return companyid;

        }

        private void BindPOItems()
        {
            List<BasePurchaseOrderItem> purchaseOrderItemList = (List<BasePurchaseOrderItem>)Session["poitems2"];
            gvPODetail.DataSource = purchaseOrderItemList;// ChildDataSourcebyPODID(Convert.ToInt32(gvTemp.DataKeys[gvEditIndex].Value), string.Empty);
            gvPODetail.DataBind();
        }
        protected void gvPODetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPODetail.PageIndex = e.NewPageIndex;
            if (Session["poitems2"] != null)
            {
                List<BasePurchaseOrderItem> purchaseOrderItemList = (List<BasePurchaseOrderItem>)Session["poitems2"];
                gvPODetail.DataSource = purchaseOrderItemList;
                gvPODetail.DataBind();
            }
        }
        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblMsg.Text = string.Empty;
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
                    //List<BasePurchaseOrderItem> purchaseOrderItemList = PurchaseOrder.GetPurchaseOrderItemList(po_id);
                    //Session["poitems2"] = purchaseOrderItemList;
                    //gvPODetail.DataSource = purchaseOrderItemList;
                    //gvPODetail.DataBind();
                    BindPO(po_id, true);
                    datagridBind(1);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order deleted successfully');</script>", false);
                    lblMsg.Text = "Deleted successfully";
                }
                else
                {
                    if (returnValue == -1)
                        lblMsg.Text = "Purchase Order item can not be deleted it must be in panding or processed state";
                    else
                        lblMsg.Text = "Purchase Order item can not be deleted there must be atleast one item";
                }
                //BindPO();

            }
            catch { }
        }
        protected void gvPODetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string esn, msl, fmupc, mdn, msid, lteICCid, lteIMSI, akey, otksl, simnumber, sQty;
            esn = msl = fmupc = mdn = msid = lteICCid = lteIMSI = akey = otksl = simnumber = sQty = string.Empty;
            lblMsg.Text = string.Empty;
            int userID = 0, qty = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;

            }
            try
            {
                GridView gvTemp = (GridView)sender;
              //  gvUniqueID = gvTemp.UniqueID;

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
                //    //    lblMsg.Text = "LTE ICC id & LTE IMSI required!";
                //    //    return;
                //    //}
                //    //if (lteICC == true && lteICCid == string.Empty)
                //    //{
                //    //    lblMsg.Text = "LTE ICC id required!";
                //    //    return;
                //    //}
                //    //if (lte_IMSI == true && lteIMSI == string.Empty)
                //    //{
                //    //    lblMsg.Text = "LTE IMSI required!";
                //    //    return;
                //    //}
                //    //if (iSSim && string.IsNullOrEmpty(simnumber))
                //    //{
                //    //    lblMsg.Text = "Sim is required!";
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
                    lblMsg.Text = returnMessage;
                    return;
                }
                gvTemp.EditIndex = -1;
                List<BasePurchaseOrderItem> purchaseOrderList = (List<BasePurchaseOrderItem>)Session["poitems2"];

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
                lblMsg.Text = "Purchase Order Detail is updated successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order Detail is updated successfully');</script>");
                //GridView1.EditIndex = -1;
                //}
            }
            catch { }
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

        protected void gvPODetail_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            //Check if there is any exception while deleting
            if (e.Exception != null)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");
                e.ExceptionHandled = true;
            }
        }
        int gvEditIndex = -1;

        protected void gvPODetail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //GridView gvTemp = (GridView)sender;
            //gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = e.NewEditIndex;
            gvPODetail.EditIndex = gvEditIndex;
            BindPOItems();
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
        protected void gvPODetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex >= 0)
                {


                }
            }
        }
        
        private InventoryList GetInventoryItems(int companyID)
        {
            SV.Framework.Inventory.InventoryOperation inventoryOperation = SV.Framework.Inventory.InventoryOperation.CreateInstance<SV.Framework.Inventory.InventoryOperation>();
            if (Session["inventory2"] == null)
            {
                Session["inventory2"] = inventoryOperation.GetInventoryItems(0, -1, companyID);                
            }

            InventoryList inventoryList = Session["inventory2"] as InventoryList;

            return inventoryList;
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
                        //Session["NewPoList2"] = poList;
                        //dgPoItem.EditItemIndex = -1;
                        //datagridBind(1);
                    }
                }
            }
            Session["NewPoList2"] = poList;
            rptItem.DataSource = poList;
            rptItem.DataBind();

        }
        private void datagridBind(Int32 iRowAdd)
        {
            List<PurchaseOrderItem> poList = null;
            if (Session["NewPoList2"] == null)
                poList = CreateDataTable();
            else
                poList = Session["NewPoList2"] as List<PurchaseOrderItem>;

            AddTableRows(iRowAdd, ref poList);
            Session["NewPoList2"] = poList;
            //dgPoItem.DataSource = dtType;
            //dgPoItem.DataBind();

            rptItem.DataSource = poList;
            rptItem.DataBind();
        }

        private void AddTableRows(Int32 iRowAdd, ref List<PurchaseOrderItem> poList)
        {
            if (Session["NewPoList2"] == null)
                poList = CreateDataTable();
            else
                poList = Session["NewPoList2"] as List<PurchaseOrderItem>; //(DataTable)Session["NewPoList2"];

            if (iRowAdd > 0)
            {
                poList.Clear();
                btnSave.Enabled = false;
                PurchaseOrderItem pitem = new PurchaseOrderItem();
                poList.Insert(0, pitem);
            }
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
            int companyid = 0;
            if (ViewState["companyid"] != null)
                companyid = Convert.ToInt32(ViewState["companyid"]);
            List<clsInventory> inventoryList = GetInventoryItems(companyid).CurrentInventory;

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
            Session["NewPoList2"] = poList;
            return poList;
        }

        private List<FulfillmentItem> LoadLineItems()
        {
            int itemID, iqty, sim;
            List<PurchaseOrderItem> lineItems =  Session["NewPoList2"] as List<PurchaseOrderItem>;
            ViewState["ItemCodeError"] = null;
            string itemCode, qty, phoneCatg, mdn;
            itemID = sim = 0;
            itemCode = mdn = string.Empty;
            List<FulfillmentItem> poList = new List<FulfillmentItem>();
            FulfillmentItem pitem;
            int companyid = 0, linenumber = 1;
            if (ViewState["companyid"] != null)
                companyid = Convert.ToInt32(ViewState["companyid"]);
            List<clsInventory> inventoryList = GetInventoryItems(companyid).CurrentInventory;

            foreach (PurchaseOrderItem item in lineItems)
            {
                pitem = new FulfillmentItem();

                pitem.SKU = item.ItemCode;
                pitem.Quantity = item.Quantity;
                pitem.LineNumber = linenumber;
                poList.Add(pitem);
                linenumber += 1;
            }
            
            return poList;
        }
        protected void lnkCode_Click(object sender, EventArgs e)
        {
            int companyid = 0;
            
            if (ViewState["companyid"] != null)
                companyid = Convert.ToInt32(ViewState["companyid"]);
            rptItemCode.DataSource = GetInventoryItems(companyid).CurrentInventory;
            rptItemCode.DataBind();
            ModalPopupExtender3.Show();

        }

        protected void rptItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    int userID = 0;
            //    if (ViewState["userid"] != null)
            //        userID = Convert.ToInt32(ViewState["userid"]);
            //    ImageButton img = e.Item.FindControl("img") as ImageButton;
            //    if (userID > 0)
            //        img.Visible = true;
            //    else
            //        img.Visible = false;
                
            //}
        }
        private List<PurchaseOrderItem> CreateDataTable()
        {
            List<PurchaseOrderItem> poList = null;
            if (Session["NewPoList2"] != null)
            {
                poList = Session["NewPoList2"] as List<PurchaseOrderItem>;
            }
            else
            {
                poList = new List<PurchaseOrderItem>();
            }

            return poList;
        }

        protected void txtItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sim = -1;
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
                List<PurchaseOrderItem> polist = Session["NewPoList2"] as List<PurchaseOrderItem>;
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
                    int companyid = 0;
                    if (ViewState["companyid"] != null)
                        companyid = Convert.ToInt32(ViewState["companyid"]);

                    List<clsInventory> inventoryList = GetInventoryItems(companyid).CurrentInventory;
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
                    Session["NewPoList2"] = poList;
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
            List<PurchaseOrderItem> poList = LoadItemListFromGrid();//Session["NewPoList2"] as List<PurchaseOrderItem>;
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
        private List<FulfillmentItem> CleanItemList(List<FulfillmentItem> poItems)
        {
            if (poItems != null)
            {
                List<FulfillmentItem> tempItems = new List<FulfillmentItem>();
                List<FulfillmentItem> Itemsgreater = new List<FulfillmentItem>();
                foreach (FulfillmentItem poItem in poItems)
                {
                    if (string.IsNullOrEmpty(poItem.SKU) || poItem.Quantity == 0 || poItem.Quantity == null)
                    {
                        tempItems.Add(poItem);
                    }
                }
                foreach (FulfillmentItem poItem in tempItems)
                {
                    poItems.Remove(poItem);
                }
            }
            return poItems;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int userid = 0, companyID = 0, poid = 0, returnCount = 0, recordCount = 0;
            FulfillmentOperations fulfillmentOperations = FulfillmentOperations.CreateInstance<FulfillmentOperations>();
            poid = Convert.ToInt32(ViewState["poid"]);

            if (ViewState["companyid"] != null)
                companyID = Convert.ToInt32(ViewState["companyid"]);

            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null && userInfo.UserGUID > 0)
            {
                userid = userInfo.UserGUID;
            }
            if (userid > 0)
            {
                lblMsg.Text = string.Empty;
                
                clsPurchaseOrder purchaseOrder = new clsPurchaseOrder();
                //purchaseOrder.PurchaseOrderItems = LoadItemListFromGrid();

                List<PurchaseOrderItem> purchaseOrderItems = LoadItemListFromGrid();
                List<FulfillmentItem> lineItems = LoadLineItems();

                
                if (ViewState["ItemCodeError"] == null)
                {
                    if (ViewState["qty"] == null)
                    {
                        //purchaseOrder.PurchaseOrderItems = CleanItemList(purchaseOrderItems);
                        lineItems = CleanItemList(lineItems);

                        if (lineItems.Count == 0)
                        {
                            lblMsg.Text = "Can not create purchase order without item";
                            datagridBind(1);
                        }
                        else 
                        {
                            string errorMessage = fulfillmentOperations.FulfillmentAddLineItems(lineItems, companyID, poid, userid, out recordCount, out returnCount);
                            if (returnCount == 0 && string.IsNullOrEmpty(errorMessage) && recordCount > 0)
                            {
                                // lblMsg.Text = "Fulfillment Order item count is: " + recordCount;
                                lblMsg.Text = "Submitted successfully";//ResponseErrorCode.SubmittedSuccessfully.ToString();
                                BindPO(poid, true);
                                datagridBind(1);
                            }
                            else if (returnCount == 2)
                            {
                                lblMsg.Text = "Line item(S) can added only when fulfillment number in pending state!";
                            }
                            else
                                lblMsg.Text = errorMessage;
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
                //fnRedirect();
            }

        }

    }
}