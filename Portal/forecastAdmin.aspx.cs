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

namespace avii
{
    public partial class forecastAdmin : System.Web.UI.Page
    {
        avii.Classes.ItemForecastController objItemForecast = new avii.Classes.ItemForecastController(0);
        public bool isEditable = true;     // -- used for displaying all slots as editable when accessing for a single user

        protected void Page_Load(object sender, EventArgs e)
        {
//            Response.Write("====>");
//            Response.Write(Session["admuser"].ToString());
            //gvForecast.DataSource = objItemForecast.GetForecastList(8);

            //---------- update the userID for the itemforecastcontroller with the present selected client in the drop down ---
            //________ this is required when the update is clicked for the oject to reflect the correct userID throughout
            if (ddlCustomer.SelectedIndex > 0)
            {
                objItemForecast.UserID = Convert.ToInt32(ddlCustomer.SelectedValue);
                isEditable = true;
            }
            else
                isEditable = false;


            if (!IsPostBack)
            {
               // bindGrid();
                bindCustomerDropDown();
            }
        }

        protected void bindGrid()
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-US");
            int custID = 0;
            int.TryParse(ddlCustomer.SelectedValue, out custID);
            this.lblMsg.Text = string.Empty;
            if (custID > -1)
            {
                //avii.Classes.ItemForecastController objItemForecast = new avii.Classes.ItemForecastController();
                //List<avii.Classes.Controller.ItemForecast> listItem = objItemForecast.GetItemForecast(custID);
                if (txtDateFrom.Text.Trim() != "")
                {
                    //objItemForecast.DateFrom = Convert.ToDateTime(txtDateFrom.Text);
                    objItemForecast.DateFrom = DateTime.Parse(txtDateFrom.Text, format, System.Globalization.DateTimeStyles.None);

                }

                if (txtDateTo.Text.Trim() != "")
                {
                    //objItemForecast.DateTo = Convert.ToDateTime(txtDateTo.Text);
                    objItemForecast.DateTo = DateTime.Parse(txtDateTo.Text, format, System.Globalization.DateTimeStyles.None);
                }

                if (!string.IsNullOrEmpty(txtSKU.Text.Trim()))
                {
                    objItemForecast.SKU = txtSKU.Text.Trim();
                }

                if (dpBrand.SelectedIndex >= 0)
                {
                    objItemForecast.Brand = dpBrand.SelectedValue;
                }

                List<avii.Classes.Controller.ItemForecast> listItem = objItemForecast.GetItemForecast();

                if (listItem != null && listItem.Count > 0)
                {
                    gvForecast.DataSource = listItem;
                    gvForecast.DataBind();

                    btnCancel.Visible = true;
                    btnUpdate.Visible = true;
                }
                else
                {
                    this.lblMsg.Text = "No forcast is created by selected user";
                    gvForecast.DataSource = null;
                    gvForecast.DataBind();
                    btnCancel.Visible = false;
                    btnUpdate.Visible = false;
                }
            }
            else
            {
                List<avii.Classes.Controller.ItemForecast> listItem = objItemForecast.GetItemForecast();

                if (listItem != null && listItem.Count > 0)
                {
                    gvForecast.DataSource = listItem;
                    gvForecast.DataBind();

                    btnCancel.Visible = true;
                    btnUpdate.Visible = true;
                }
            }
        }

        protected void bindCustomerDropDown()
        {
            string admuser = string.Empty;
            string filteruser = string.Empty;
            string searchParam = string.Empty;

            if (Session["admuser"] != null)
            {
                admuser = Session["admuser"] as string;
            }

            if (Session["adminfilteruser"] != null)
            {
                filteruser = Session["adminfilteruser"] as string;
            }

            if (!string.IsNullOrEmpty(filteruser) && filteruser == "1")
            {
                searchParam = admuser;
            }

            Response.Write(searchParam);

            avii.Classes.ItemForecastController objItemForecast = new avii.Classes.ItemForecastController();
            Hashtable hstCustomer = objItemForecast.getCustomerList(searchParam);
            ddlCustomer.DataSource = hstCustomer;
            ddlCustomer.DataValueField = "value";
            ddlCustomer.DataTextField = "key";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("-- SELECT --", "-1"));
        }

        protected void gvForecast_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataList dls = (DataList)e.Row.FindControl("dlsItemForecast");
                //if(dls!=null)
                //    dls.RepeatColumns = dls.DataSource
            }
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            IFormatProvider format = new System.Globalization.CultureInfo( "en-US");

            DateTime fromdate, todate;
            bool dateCheck = true;
          
            if (txtDateFrom.Text.Trim().Length > 0)
            {
                //dateCheck = DateTime.TryParse(txtDateFrom.Text.Trim(), out fromdate);
                dateCheck = DateTime.TryParse(txtDateFrom.Text.Trim(), format, System.Globalization.DateTimeStyles.None, out fromdate);
            }

            if (txtDateTo.Text.Trim().Length > 0)
            {
                //dateCheck = DateTime.TryParse(txtDateTo.Text.Trim(), out todate);
                dateCheck = DateTime.TryParse(txtDateTo.Text.Trim(), format, System.Globalization.DateTimeStyles.None, out todate);
            }

            //if (fromdate.Year > 01 && todate.Year > 01 && fromdate < todate)
            if(dateCheck)
            {
                bindGrid();
            }
            else
            {
                lblMsg.Text = "Please check the format (MM/DD/YYYY) of the date columns";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //avii.Classes.ItemForecastController objItemForecast = new avii.Classes.ItemForecastController();
            if (objItemForecast.UserID > 0)
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
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomer.SelectedValue != null)
            {
                //avii.Classes.ItemForecastController objItemForecast = new avii.Classes.ItemForecastController();
                objItemForecast.UserID = Convert.ToInt32(ddlCustomer.SelectedValue);

                if (objItemForecast.UserID == 0)
                {
                    btnUpdate.Enabled = false;
                }
                else
                {
                    btnUpdate.Enabled = true;
                }

                if (objItemForecast.UserID > -1)
                    bindGrid();
                else
                    ClearForm();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            btnCancel.Visible = false;
            btnUpdate.Visible = false;
            gvForecast.DataSource = null;
            gvForecast.DataBind();
            txtDateFrom.Text = string.Empty;
            txtDateTo.Text = string.Empty;
            this.lblMsg.Text = string.Empty;
            ddlCustomer.SelectedIndex = 0;
        }
        protected void btnSearchCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}