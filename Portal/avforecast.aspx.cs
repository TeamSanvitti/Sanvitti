using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using avii.Classes;
namespace avii
{
    public partial class avForecast : System.Web.UI.Page
    {
        private static string FORECAST_ITEM_TYPE = "forecastitemtype";
        ItemForecastController objItemForecast = new ItemForecastController();
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = "./logon.aspx";
           if (Session["UserID"] == null)
            {
                Response.Redirect(url);
            }
            else
            {
                int userID = 0;
                int.TryParse(Session["UserID"].ToString(), out userID);
                objItemForecast.UserID = userID;
            }
            string forecastmodule = Request["i"] as string;
            ViewState[FORECAST_ITEM_TYPE] = forecastmodule;
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(forecastmodule))
                {
                    if (forecastmodule == "3")
                    {
                        lblHeader.Text = "Forecast Future";
                    }
                    else if (forecastmodule == "2")
                    {
                        lblHeader.Text = "Forecast Debrand";
                    }
                    else if (forecastmodule == "1")
                    {
                        lblHeader.Text = "Forecast OEM";
                    }
                    bindGrid();
                }
            }
        }

        protected void bindGrid()
        {
            try
            {
                bool showError = true;
                int itemTypeID = 0;
                int.TryParse(ViewState[FORECAST_ITEM_TYPE] as string, out itemTypeID);
                if (itemTypeID > 0)
                {
                    List<avii.Classes.Controller.ItemForecast> forecastList = objItemForecast.GetItemForecast(objItemForecast.UserID, itemTypeID);
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
                    CheckBox chkLocked = e.Item.FindControl("chkLocked") as CheckBox;
                    TextBox txtQty = e.Item.FindControl("txtQty") as TextBox;
                    HyperLink lnk = e.Item.FindControl("lnkQty") as HyperLink;
                    if (chkLocked != null)
                        chkLocked.Attributes.Add("onclick", "checkQty(this)");


                    if (txtQty != null)
                        txtQty.Attributes.Add("onkeypress", "isNumberKey(event)");

                    if (lnk != null)
                    {
                        lnk.NavigateUrl = lnk.NavigateUrl + "&fty=" + ViewState[FORECAST_ITEM_TYPE];
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "ItemBound: " + ex.Message;

                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow grv in gvForecast.Rows)
            {
                HiddenField hdnItemID = (HiddenField)grv.FindControl("hdnItemID");
                DataList dlsForecast = (DataList)grv.FindControl("dlsItemForecast");

                int qty = 0;
                if (dlsForecast != null)
                {
                    foreach (DataListItem dls in dlsForecast.Items)
                    {
                        qty = 0;
                        HiddenField hdnForecastGUID = (HiddenField)dls.FindControl("hidForecastGUID");
                        HiddenField hdnForecastDate = (HiddenField)dls.FindControl("hidForecastDate");
                        HiddenField hdnForecastStatusID = (HiddenField)dls.FindControl("hidForecastStatusID");
                        CheckBox chkLocked = (CheckBox)dls.FindControl("chkLocked");
                        TextBox txtQty = (TextBox)dls.FindControl("txtQty");

                        if (txtQty != null && !string.IsNullOrEmpty(txtQty.Text.Trim()) && int.TryParse(txtQty.Text.Trim(), out qty) && qty > 0)
                        {

                            //if ("" == txtQty.Text)
                            //    txtQty.Text = "0";
                            if (0 == Convert.ToInt32(hdnForecastStatusID.Value))
                            {
                                objItemForecast.insertUpdateItemForecast(
                                    Convert.ToInt32(hdnForecastGUID.Value)
                                    , Convert.ToInt32(hdnItemID.Value)
                                    , Convert.ToDateTime(hdnForecastDate.Value)
                                    , Convert.ToInt32(txtQty.Text)
                                    , chkLocked.Checked
                                    );
                            }
                        }
                    }
                }
            }
            bindGrid();
        }
    }

}
