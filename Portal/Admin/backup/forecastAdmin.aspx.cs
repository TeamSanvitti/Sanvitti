using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using avii.Classes;
/*
public partial class forecastAdmin : System.Web.UI.Page
{

    protected System.Web.UI.WebControls.GridView gvForecast;
    protected System.Web.UI.WebControls.DropDownList ddlCustomer;
    
    ItemForecastController objItemForecast = new ItemForecastController(6);
    protected void Page_Load(object sender, EventArgs e)
    {
        //gvForecast.DataSource = objItemForecast.GetForecastList(8);
        if (ddlCustomer.SelectedValue != null && ddlCustomer.SelectedValue!="")
            objItemForecast.UserID = Convert.ToInt32( ddlCustomer.SelectedValue );

        if (!IsPostBack)
        {
            bindGrid();
            bindCustomerDropDown();
        }
    }

    protected void bindGrid()
    {
        gvForecast.DataSource = objItemForecast.GetItemForecast(objItemForecast.UserID);
        gvForecast.DataBind();
    }

    protected void bindCustomerDropDown()
    {
        Hashtable hstCustomer = objItemForecast.getCustomerList();
        ddlCustomer.DataSource = hstCustomer;
        ddlCustomer.DataValueField = "key";
        ddlCustomer.DataTextField = "value";
        ddlCustomer.DataBind();
    }

    protected void dlsItemForecast_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            CheckBox chkLocked = (CheckBox)e.Item.FindControl("chkLocked");
            TextBox txtQty = (TextBox)e.Item.FindControl("txtQty");
            if (chkLocked != null)
                chkLocked.Attributes.Add("onclick", "checkQty(this)");

            if (txtQty != null)
                txtQty.Attributes.Add("onkeypress", "isNumberKey(event)");
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow grv in gvForecast.Rows)
        {
            HiddenField hdnItemID = (HiddenField)grv.FindControl("hdnItemID");
            DataList dlsForecast = (DataList)grv.FindControl("dlsItemForecast");

            if (dlsForecast != null)
            {
                foreach (DataListItem dls in dlsForecast.Items)
                {
                    HiddenField hdnForecastGUID = (HiddenField)dls.FindControl("hidForecastGUID");
                    HiddenField hdnForecastDate = (HiddenField)dls.FindControl("hidForecastDate");
                    HiddenField hdnForecastStatusID = (HiddenField)dls.FindControl("hidForecastStatusID");
                    CheckBox chkLocked = (CheckBox)dls.FindControl("chkLocked");
                    TextBox txtQty = (TextBox)dls.FindControl("txtQty");

                    if (txtQty != null)
                    {

                        if ("" == txtQty.Text)
                            txtQty.Text = "0";
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
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCustomer.SelectedValue != null)
        {
            objItemForecast.UserID = Convert.ToInt32(ddlCustomer.SelectedValue);
            bindGrid();
        }
    }
}
*/

namespace avii
{
    public partial class forecastAdmin : System.Web.UI.Page
    {

        //protected System.Web.UI.WebControls.GridView gvForecast;
        //protected System.Web.UI.WebControls.DropDownList ddlCustomer;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            //gvForecast.DataSource = objItemForecast.GetForecastList(8);
            //if (ddlCustomer.SelectedValue != null && ddlCustomer.SelectedValue != "")
            //    objItemForecast.UserID = Convert.ToInt32(ddlCustomer.SelectedValue);

            if (!IsPostBack)
            {
                bindGrid();
                bindCustomerDropDown();
            }
        }

        protected void bindGrid()
        {
            btnCancel.Visible = false;
            btnUpdate.Visible = false;
            int custID = 0;
            int.TryParse(ddlCustomer.SelectedValue, out custID);
            if (custID >= 0)
            {
                avii.Classes.ItemForecastController objItemForecast = new avii.Classes.ItemForecastController();
                List<avii.Classes.Controller.ItemForecast> listItem = objItemForecast.GetItemForecast(custID);
                if (listItem != null && listItem.Count > 0)
                {
                    lblMsg.Text = string.Empty;
                    gvForecast.DataSource = listItem;
                    gvForecast.DataBind();

                    if (custID > 0)
                    {
                        btnCancel.Visible = true;
                        btnUpdate.Visible = true;
                    }
                }
                else
                {
                    lblMsg.Text = "Inventory is not assigned to this customer, please contact administrator";
                    gvForecast.DataSource = null;
                    gvForecast.DataBind();
                    
                }
            }
        }

        protected void bindCustomerDropDown()
        {
            avii.Classes.ItemForecastController objItemForecast = new avii.Classes.ItemForecastController();
            Hashtable hstCustomer = objItemForecast.getCustomerList();
            ddlCustomer.DataSource = hstCustomer;
            ddlCustomer.DataValueField = "value";
            ddlCustomer.DataTextField = "key";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0,new ListItem("-- SELECT --","0"));
        }

        protected void dlsItemForecast_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkLocked = (CheckBox)e.Item.FindControl("chkLocked");
                TextBox txtQty = (TextBox)e.Item.FindControl("txtQty");
                if (chkLocked != null)
                    chkLocked.Attributes.Add("onclick", "checkQty(this)");

                if (txtQty != null)
                    txtQty.Attributes.Add("onkeypress", "isNumberKey(event)");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            avii.Classes.ItemForecastController objItemForecast = new avii.Classes.ItemForecastController();
            foreach (GridViewRow grv in gvForecast.Rows)
            {
                HiddenField hdnItemID = (HiddenField)grv.FindControl("hdnItemID");
                DataList dlsForecast = (DataList)grv.FindControl("dlsItemForecast");

                if (dlsForecast != null)
                {
                    foreach (DataListItem dls in dlsForecast.Items)
                    {
                        HiddenField hdnForecastGUID = (HiddenField)dls.FindControl("hidForecastGUID");
                        HiddenField hdnForecastDate = (HiddenField)dls.FindControl("hidForecastDate");
                        HiddenField hdnForecastStatusID = (HiddenField)dls.FindControl("hidForecastStatusID");
                        CheckBox chkLocked = (CheckBox)dls.FindControl("chkLocked");
                        TextBox txtQty = (TextBox)dls.FindControl("txtQty");

                        if (txtQty != null)
                        {

                            if ("" == txtQty.Text)
                                txtQty.Text = "0";
                            if (0 == Convert.ToInt32(hdnForecastStatusID.Value))
                            {
                                
                                objItemForecast.createForecastList(Convert.ToInt32(hdnForecastGUID.Value)
                                    , Convert.ToInt32(hdnItemID.Value)
                                    , Convert.ToDateTime(hdnForecastDate.Value)
                                    , Convert.ToInt32(txtQty.Text)
                                    , chkLocked.Checked);
                            }
                        }
                    }
                }
            }
            objItemForecast.createForecast();
            bindGrid();
        }
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomer.SelectedValue != null)
            {
                //avii.Classes.ItemForecastController objItemForecast = new avii.Classes.ItemForecastController();
                //objItemForecast.UserID = Convert.ToInt32(ddlCustomer.SelectedValue);
                bindGrid();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            lblMsg.Text = string.Empty;
            btnCancel.Visible = false;
            btnUpdate.Visible = false;
            gvForecast.DataSource = null;
            gvForecast.DataBind();
            txtDateFrom.Text = string.Empty;
            txtDateTo.Text = string.Empty;
            ddlCustomer.SelectedIndex = 0;
        }
        protected void btnSearchCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
}
}