using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Admin
{
    public partial class NewForecastAdmin : System.Web.UI.Page
    {
        //avii.Classes.ItemForecastController objItemForecast = new avii.Classes.ItemForecastController(0);
        public bool isEditable = true;     // -- used for displaying all slots as editable when accessing for a single user

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
            if (ddlCustomer.SelectedIndex > 0)
            {
                avii.Classes.ItemForecastController objItemForecast = new avii.Classes.ItemForecastController(0);
                //objItemForecast.UserID = Convert.ToInt32(ddlCustomer.SelectedValue);
                objItemForecast.CompanyID = Convert.ToInt32(ddlCustomer.SelectedValue);
                ViewState["companyid"] = Convert.ToInt32(ddlCustomer.SelectedValue);
                isEditable = true;
            }
            else
                isEditable = false;


            if (!IsPostBack)
            {
                // bindGrid();
                btnCreatePO.Visible = false;
                bindCustomerDropDown();
            }
        }
        protected void bindGrid()
        {
            avii.Classes.ItemForecastController objItemForecast = new avii.Classes.ItemForecastController(0);
            IFormatProvider format = new System.Globalization.CultureInfo("en-US");
            int custID = 0;
            int.TryParse(ddlCustomer.SelectedValue, out custID);
            this.lblMsg.Text = string.Empty;
            if (custID > -1)
            {
                objItemForecast.CompanyID = custID;
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
                    btnCreatePO.Visible = true;
                }
                else
                {
                    this.lblMsg.Text = "No forcast is created by selected user";
                    gvForecast.DataSource = null;
                    gvForecast.DataBind();
                    btnCancel.Visible = false;
                    btnUpdate.Visible = false;
                    btnCreatePO.Visible = false;
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
                    btnCreatePO.Visible = true;
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

            //Response.Write("filteruser =>" + filteruser);
            //Response.Write(searchParam);

            //avii.Classes.ItemForecastController objItemForecast = new avii.Classes.ItemForecastController();
            //Hashtable hstCustomer = objItemForecast.getCustomerList(searchParam);
            ddlCustomer.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            ddlCustomer.DataValueField = "CompanyID";
            ddlCustomer.DataTextField = "CompanyName";
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
                //CheckBox chkLocked = (CheckBox)e.Item.FindControl("chkLocked");
                TextBox txtQty = (TextBox)e.Item.FindControl("txtQty");
                //if (chkLocked != null)
                //    chkLocked.Attributes.Add("onclick", "checkQty(this)");

                if (txtQty != null)
                    txtQty.Attributes.Add("onkeypress", "isNumberKey(event)");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            IFormatProvider format = new System.Globalization.CultureInfo("en-US");

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
            if (dateCheck)
            {
                bindGrid();
            }
            else
            {
                lblMsg.Text = "Please check the format (MM/DD/YYYY) of the date columns";
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
                Response.Redirect("/po.aspx?fid=0&c="+ViewState["companyid"].ToString(), false);
            }
            else
                lblMsg.Text = "No records exist for this forecast";


        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            avii.Classes.ItemForecastController objItemForecast = new avii.Classes.ItemForecastController(0);
            //avii.Classes.ItemForecastController objItemForecast = new avii.Classes.ItemForecastController();
            List<avii.Classes.Controller.Forecast> forecastItemList = new List<Classes.Controller.Forecast>();

            //if (objItemForecast.CompanyID > 0)
            //{
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

                        //if (txtQty != null)
                        //{

                        //    if ("" == txtQty.Text)
                        //        txtQty.Text = "0";
                        //    if (0 == Convert.ToInt32(hdnForecastStatusID.Value))
                        //    {

                        //        objItemForecast.createForecastList(Convert.ToInt32(hdnForecastGUID.Value)
                        //            , Convert.ToInt32(hdnItemID.Value)
                        //            , Convert.ToDateTime(hdnForecastDate.Value)
                        //            , Convert.ToInt32(txtQty.Text)
                        //            , chkLocked.Checked);
                        //    }
                        //}
                        if (txtQty != null && !string.IsNullOrEmpty(txtQty.Text.Trim()) && int.TryParse(txtQty.Text.Trim(), out qty) && qty > 0 && hdnPOForecastGUID != null && !string.IsNullOrEmpty(hdnPOForecastGUID.Value.Trim()) && int.TryParse(hdnPOForecastGUID.Value.Trim(), out poForecastGUID) && poForecastGUID == 0)
                        {
                            avii.Classes.Controller.Forecast forecastItems = new Classes.Controller.Forecast();



                            forecastItems.ForecastGUID = Convert.ToInt32(hdnForecastGUID.Value);
                            forecastItems.ForecastDate = Convert.ToDateTime(hdnForecastDate.Value);
                            forecastItems.ItemID = Convert.ToInt32(hdnItemID.Value);
                            forecastItems.StatusID = 1;
                            forecastItems.Qty = Convert.ToInt32(txtQty.Text);
                            forecastItemList.Add(forecastItems);


                        }
                    }
                }
            }
            if (forecastItemList != null && forecastItemList.Count > 0)
            {

                int companyID = 0;

                if (ViewState["companyid"] != null)
                    int.TryParse(ViewState["companyid"].ToString(), out companyID);


                if (companyID > 0)
                {

                    ItemForecastController.InsertUpdateForecast(forecastItemList, companyID);
                    bindGrid();
                    lblMsg.Text = "Forecast is successfully saved";
                }
                else
                    lblMsg.Text = "Customer required";
            }
            else
            {
                lblMsg.Text = "Quatity not assigned";
            }
            //objItemForecast.createForecast();

            //}
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            avii.Classes.ItemForecastController objItemForecast = new avii.Classes.ItemForecastController(0);
            if (ddlCustomer.SelectedValue != null)
            {
                //avii.Classes.ItemForecastController objItemForecast = new avii.Classes.ItemForecastController();
                //objItemForecast.UserID = Convert.ToInt32(ddlCustomer.SelectedValue);
                objItemForecast.CompanyID = Convert.ToInt32(ddlCustomer.SelectedValue);
                if (objItemForecast.CompanyID == 0)
                {
                    btnUpdate.Enabled = false;
                }
                else
                {
                    btnUpdate.Enabled = true;
                }

                if (objItemForecast.CompanyID > -1)
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