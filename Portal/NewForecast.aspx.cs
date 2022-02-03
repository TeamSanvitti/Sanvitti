using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii
{
    public partial class NewForecast : System.Web.UI.Page
    {
        private static string FORECAST_ITEM_TYPE = "forecastitemtype";
        //ItemForecastController objItemForecast = new ItemForecastController();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["userInfo"] == null)
                Response.Redirect("/logon.aspx", false);

            if (!IsPostBack)
            {
                int forecastModule = 0, userID = 0, companyID = 0;

                if (Session["userInfo"] != null && ViewState["companyid"] == null)
                {
                    UserInfo userInfo = Session["userInfo"] as UserInfo;
                    if (userInfo != null && userInfo.UserGUID > 0)
                    {
                        userID = userInfo.UserGUID;
                        companyID = userInfo.CompanyGUID;
                        ViewState["userid"] = userInfo.UserGUID;
                        ViewState["companyid"] = userInfo.CompanyGUID;

                        //objItemForecast.UserID = userID;
                    }
                }
                if (Request["i"]  != null)
                {
                    int.TryParse(Request["i"].ToString(), out forecastModule);
                }

                if (forecastModule > 0 && companyID > 0)
                {
                    ViewState[FORECAST_ITEM_TYPE] = forecastModule;

                    if (forecastModule == 3)
                    {
                        lblHeader.Text = "Forecast Future";
                    }
                    else if (forecastModule == 2)
                    {
                        lblHeader.Text = "Forecast Debrand";
                    }
                    else if (forecastModule == 1)
                    {
                        lblHeader.Text = "Forecast OEM";
                    }

                    bindGrid(forecastModule, companyID);
                }
                else
                {
                    lblMsg.Text = "Missing required parameters, please select the forecast module again or contact administrator";
                }
            }
        }

        protected void bindGrid(int forecastModule, int companyID)
        {
            try
            {
                bool showError = true;
                if (forecastModule > 0)
                {
                    ItemForecastController objItemForecast = new ItemForecastController();
                    objItemForecast.CompanyID = companyID;
                    List<avii.Classes.Controller.ItemForecast> forecastList = objItemForecast.GetItemForecast(companyID, forecastModule);
                    if (forecastList != null && forecastList.Count > 0)
                    {
                        gvForecast.DataSource = forecastList;
                        gvForecast.DataBind();
                        showError = false;
                    }
                }

                if (showError)
                {
                    lblMsg.Text = "No inventory is assign to this Forecast Category, please select the another Forecast type from the menu";
                    btnCancel.Visible = false;
                    btnUpdate.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        protected void dlsItemForecast_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (ListItemType.Item == e.Item.ItemType || ListItemType.AlternatingItem == e.Item.ItemType)
            {
                try
                {
                    //CheckBox chkLocked = e.Item.FindControl("chkLocked") as CheckBox;
                    TextBox txtQty = e.Item.FindControl("txtQty") as TextBox;
                    //HyperLink lnk = e.Item.FindControl("lnkQty") as HyperLink;
                    //if (chkLocked != null)
                    //    chkLocked.Attributes.Add("onclick", "checkQty(this)");


                    if (txtQty != null)
                        txtQty.Attributes.Add("onkeypress", "isNumberKey(event)");

                    //if (lnk != null)
                    //{
                    //    lnk.NavigateUrl = lnk.NavigateUrl + "&fty=" + ViewState[FORECAST_ITEM_TYPE];
                    //}
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "ItemBound: " + ex.Message;

                }
            }

        }

        private void CreatePO()
        {
            List<avii.Classes.Controller.Forecast> forecastItemList = null;
            
            List<avii.Classes.PurchaseOrderItem> poItemList = new List<avii.Classes.PurchaseOrderItem>();
           
            int itemTypeID = 0;
                int.TryParse(ViewState[FORECAST_ITEM_TYPE] as string, out itemTypeID);
                if (itemTypeID > 0)
                {
                    ItemForecastController objItemForecast = new ItemForecastController();
                    List<avii.Classes.Controller.ItemForecast> forecastList = objItemForecast.GetItemForecast(objItemForecast.UserID, itemTypeID);
                    if (forecastList != null && forecastList.Count > 0)
                    {
                        for (int i = 0; i < forecastList.Count; i++)
                        {
                            avii.Classes.PurchaseOrderItem poItem = new PurchaseOrderItem();
                            forecastItemList = new List<avii.Classes.Controller.Forecast>();
                            forecastItemList = forecastList[i].ItemForecasts;
                            poItem.ItemID = forecastList[i].ItemID;
                            poItem.ItemCode = forecastList[i].ItemSKU;

                            var poInfoList = (from item in forecastItemList where item.ForecastDate.Equals(Convert.ToDateTime(hdnForCastDate.Value)) select item).ToList();

                        }
                    }
                }
        }
        protected void btnCreatePO_Click(object sender, EventArgs e)
        {
            
            avii.Classes.PurchaseOrderItem poItem = null;
            List<avii.Classes.PurchaseOrderItem> poItemList = new List<avii.Classes.PurchaseOrderItem>();
            int qty = 0;
            int forecastGUID = 0;
            int poForecastGUID = 0;
            foreach (GridViewRow grv in gvForecast.Rows)
            {
                HiddenField hdnItemID = grv.FindControl("hdnItemID") as HiddenField;
                Label lblItemSKU = grv.FindControl("lblItemSKU") as Label;


                DataList dlsForecast = grv.FindControl("dlsItemForecast") as DataList;

                
                if (dlsForecast != null)
                {
                    foreach (DataListItem dls in dlsForecast.Items)
                    {
                        qty = 0;
                        forecastGUID = 0;
                        poForecastGUID = 0;
                        HiddenField hdnForecastGUID = dls.FindControl("hidForecastGUID") as HiddenField;
                        HiddenField hdnForecastDate = dls.FindControl("hidForecastDate") as HiddenField;
                        HiddenField hdnForecastStatusID = dls.FindControl("hidForecastStatusID") as HiddenField;
                        HiddenField hdnPOForecastGUID = dls.FindControl("hdnPOForecastGUID") as HiddenField;
                        
                        CheckBox chkLocked = dls.FindControl("chkLocked") as CheckBox;
                        TextBox txtQty = dls.FindControl("txtQty") as TextBox;

                        if (lblItemSKU != null && !string.IsNullOrEmpty(lblItemSKU.Text.Trim()) && txtQty != null && !string.IsNullOrEmpty(txtQty.Text.Trim()) && int.TryParse(txtQty.Text.Trim(), out qty) && qty > 0 && hdnForecastGUID != null && !string.IsNullOrEmpty(hdnForecastGUID.Value.Trim()) && int.TryParse(hdnForecastGUID.Value.Trim(), out forecastGUID) && forecastGUID > 0 && hdnPOForecastGUID != null && !string.IsNullOrEmpty(hdnPOForecastGUID.Value.Trim()) && int.TryParse(hdnPOForecastGUID.Value.Trim(), out poForecastGUID) && poForecastGUID == 0)
                        {
                            //avii.Classes.Controller.Forecast forecastItems = new Classes.Controller.Forecast();


                            //if ("" == txtQty.Text)
                            //    txtQty.Text = "0";
                            if (Convert.ToDateTime(hdnForCastDate.Value).ToShortDateString() == Convert.ToDateTime(hdnForecastDate.Value).ToShortDateString())
                            //if (0 == Convert.ToInt32(hdnForecastStatusID.Value))
                            {
                                
                                poItem = new avii.Classes.PurchaseOrderItem();

                                poItem.ForecastGUID = Convert.ToInt32(hdnForecastGUID.Value);
                                //forecastItems.ForecastDate = Convert.ToDateTime(hdnForecastDate.Value);
                                poItem.ItemID = Convert.ToInt32(hdnItemID.Value);
                                poItem.ItemCode = lblItemSKU.Text.Trim();
                                poItem.Quantity = Convert.ToInt32(txtQty.Text);
                                poItemList.Add(poItem);
                                
                                //forecastItems.StatusID = 1;
                                //forecastItems.Qty = Convert.ToInt32(txtQty.Text);
                                
                                //objItemForecast.insertUpdateItemForecast(
                                //    Convert.ToInt32(hdnForecastGUID.Value)
                                //    , Convert.ToInt32(hdnItemID.Value)
                                //    , Convert.ToDateTime(hdnForecastDate.Value)
                                //    , Convert.ToInt32(txtQty.Text)
                                //    , chkLocked.Checked
                                //    );
                            }
                        }
                    }
                }
            }
            if (poItemList != null && poItemList.Count > 0)
            {
                Session["PoList"] = poItemList;
                Response.Redirect("po.aspx?fid=0", false);
            }
            else
                lblMsg.Text = "No records exist for this forecast";


        }
        
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            List<avii.Classes.Controller.Forecast> forecastItemList = new List<Classes.Controller.Forecast>();

            foreach (GridViewRow grv in gvForecast.Rows)
            {
                HiddenField hdnItemID = (HiddenField)grv.FindControl("hdnItemID");
                DataList dlsForecast = (DataList)grv.FindControl("dlsItemForecast");

                int qty = 0;
                int poForecastGUID = 0;
                if (dlsForecast != null)
                {
                    foreach (DataListItem dls in dlsForecast.Items)
                    {
                        qty = 0;
                        poForecastGUID = 0;
                        HiddenField hdnForecastGUID = (HiddenField)dls.FindControl("hidForecastGUID");
                        HiddenField hdnForecastDate = (HiddenField)dls.FindControl("hidForecastDate");
                        HiddenField hdnForecastStatusID = (HiddenField)dls.FindControl("hidForecastStatusID");
                        //CheckBox chkLocked = (CheckBox)dls.FindControl("chkLocked");
                        TextBox txtQty = (TextBox)dls.FindControl("txtQty");
                        HiddenField hdnPOForecastGUID = dls.FindControl("hdnPOForecastGUID") as HiddenField;

                        if (txtQty != null && !string.IsNullOrEmpty(txtQty.Text.Trim()) && int.TryParse(txtQty.Text.Trim(), out qty) && qty > 0 && hdnPOForecastGUID != null && !string.IsNullOrEmpty(hdnPOForecastGUID.Value.Trim()) && int.TryParse(hdnPOForecastGUID.Value.Trim(), out poForecastGUID) && poForecastGUID == 0)
                        {
                            avii.Classes.Controller.Forecast forecastItems = new Classes.Controller.Forecast();


                            //if ("" == txtQty.Text)
                            //    txtQty.Text = "0";
                            //if (Convert.ToDateTime(hdnForCastDate.Value).ToShortDateString() == Convert.ToDateTime(hdnForecastDate.Value).ToShortDateString())
                            //if (0 == Convert.ToInt32(hdnForecastStatusID.Value))
                            //{
                                forecastItems.ForecastGUID = Convert.ToInt32(hdnForecastGUID.Value);
                                forecastItems.ForecastDate = Convert.ToDateTime(hdnForecastDate.Value);
                                forecastItems.ItemID = Convert.ToInt32(hdnItemID.Value);
                                forecastItems.StatusID = 1;
                                forecastItems.Qty = Convert.ToInt32(txtQty.Text);
                                forecastItemList.Add(forecastItems);

                                //objItemForecast.insertUpdateItemForecast(
                                //    Convert.ToInt32(hdnForecastGUID.Value)
                                //    , Convert.ToInt32(hdnItemID.Value)
                                //    , Convert.ToDateTime(hdnForecastDate.Value)
                                //    , Convert.ToInt32(txtQty.Text)
                                //    , chkLocked.Checked
                                //    );
                            //}
                        }
                    }
                }
            }
            if (forecastItemList != null && forecastItemList.Count > 0)
            {

                int forecastModule = 0, userID = 0, companyID =0;
                if (ViewState["userid"] != null)
                    int.TryParse(ViewState["userid"].ToString(), out userID);
                if (ViewState["companyid"] != null)
                    int.TryParse(ViewState["companyid"].ToString(), out companyID);
                
                if (ViewState[FORECAST_ITEM_TYPE] != null)
                    int.TryParse(ViewState[FORECAST_ITEM_TYPE].ToString(), out forecastModule);

                
                if (companyID > 0 && forecastModule > 0)
                {

                    ItemForecastController.InsertUpdateForecast(forecastItemList, companyID);
                    lblMsg.Text = "Forecast is successfully saved";
                    bindGrid(forecastModule, companyID);
                }
                else
                    lblMsg.Text = "Missing required data, please log back to the site and try again";
            }
            else
            {
                lblMsg.Text = "Quatity not assigned";
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            int forecastModule = 0, companyID = 0;
            if (ViewState["companyid"] != null)
                int.TryParse(ViewState["companyid"].ToString(), out companyID);

            if (ViewState[FORECAST_ITEM_TYPE] != null)
                int.TryParse(ViewState[FORECAST_ITEM_TYPE].ToString(), out forecastModule);

            if (companyID > 0 && forecastModule > 0)
            {
                bindGrid(forecastModule, companyID);
            }
        }
        
    }

}
