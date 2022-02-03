using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
using System.Text.RegularExpressions;
using System.IO;

namespace avii
{
    public partial class CreateForecast : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    url = System.Configuration.ConfigurationSettings.AppSettings["LogonPage"].ToString();

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
                Session["forecastlineitems"] = null;
                hdnDateRange.Value = ConfigurationManager.AppSettings["daterange"].ToString();
                int companyID = 0;
                if (Session["adm"] == null)
                {
                    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    if (userInfo != null)
                    {
                        //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

                        companyID = userInfo.CompanyGUID;
                    }
                    BindForecastSKUs(companyID);
                    pnlCustomer.Visible = false;
                    //BindLineItems();

                }
                else
                {
                    BindCustomer();
                    pnlCustomer.Visible = true;
                }
                if (Request["fid"] != null)
                {
                    GetForecastInfo(Convert.ToInt32(Request["fid"]));
                    imgHistory.Visible = true;
                }
                else
                {
                    BindLineItems();
                    GenerateForecastNumber();
                    imgHistory.Visible = false;

                }
            }
        }
        private void GetForecastInfo(int forecastID)
        {
            try
            {
                string status = string.Empty;
                FulfillmentForecast forecast = SKUPricesOperations.GetFulfillmentForecastInfo(forecastID);
                if (forecast != null && forecast.ForecastID > 0)
                {
                    lblForecastNum.Text = forecast.ForecastNumber;
                    txtFcDate.Text = forecast.ForecastDate.ToShortDateString();
                    Session["forecastlineitems"] = forecast.ForecastLineItems;
                    rptItem.DataSource = forecast.ForecastLineItems;
                    rptItem.DataBind();
                    status = forecast.Status;
                    if (Session["adm"] != null)
                    {
                        BindForecastSKUs(forecast.CompanyID);
                        dpCompany.SelectedValue = forecast.CompanyID.ToString();
                        ddlStatus.SelectedItem.Text = forecast.Status;
                    }
                    if (status.ToLower() != "pending")
                    {
                        btnSKU.Visible = false;

                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;
            Session["forecastlineitems"] = null;
            //rptItem.DataSource = null;
            //rptItem.DataBind();
            BindLineItems();
            //rptESN.Visible = false;
            lblMsg.Text = string.Empty;
            //lblConfirm.Text = string.Empty;

            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            if (companyID > 0)
                BindForecastSKUs(companyID);
            else
            {
                //lblMsg.Text = "";
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
            }
        }
        
        private void BindLineItems()
        {
            List<ForecastLineItem> forecastLineItems = new List<ForecastLineItem>();
            ForecastLineItem forecastLineItem = new ForecastLineItem();
            forecastLineItem.SKU = string.Empty;
            //forecastLineItem.Quantity = 0;
            //forecastLineItem.Price = 0;
            //forecastLineItem.TotalPrice = 0;
            forecastLineItems.Add(forecastLineItem);
            rptItem.DataSource = forecastLineItems;
            rptItem.DataBind();


        }
        private void GenerateForecastNumber()
        {
            string month = string.Empty;
            string day = string.Empty;
            string avfc = System.Configuration.ConfigurationManager.AppSettings["avfc"].ToString();
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            try
            {
                month = dt.Month.ToString();
                day = dt.Day.ToString();
                if (month.Length < 2)
                    month = "0" + month;
                if (day.Length < 2)
                    day = "0" + day;

                lblForecastNum.Text = avfc + dt.Year.ToString() + month + day; //+ "-" + rmaGUID;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }
        private void BindForecastSKUs(int companyID)
        {
            lblMsg.Text = string.Empty;
            List<CompanySKUno> skuList = SKUPricesOperations.GetCustomerForecastSKUList(companyID);
            if (skuList != null && skuList.Count > 0)
            {
                ddlSKU.DataSource = skuList;
                ddlSKU.DataValueField = "MASSKU";
                ddlSKU.DataTextField = "SKU";
            
                ddlSKU.DataBind();
            }
            else
            {
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                lblMsg.Text = "No SKU price assigned. Please contact Lan Global Admin";
            }
        }
        protected void btnAddLineItem_Click(object sender, EventArgs e)
        {
            lblSKU.Text = string.Empty;
            try
            {
                int qty = 1;

                string sku = string.Empty;
                double price, totalPrice;
                price = totalPrice = 0;
                string skuInfo = ddlSKU.SelectedValue;
                string[] arr = skuInfo.Split(',');
                if (!string.IsNullOrEmpty(txtQuantity.Text))
                    qty = Convert.ToInt32(txtQuantity.Text.Trim());

                if (arr.Length > 0)
                {
                    sku = arr[0];
                    price = Convert.ToDouble(arr[1]);
                    totalPrice = price * qty;
                }

                List<ForecastLineItem> forecastLineItems = new List<ForecastLineItem>();
                if (Session["forecastlineitems"] != null)
                    forecastLineItems = (List<ForecastLineItem>)Session["forecastlineitems"];


                ForecastLineItem forecastLineItem = new ForecastLineItem();
                forecastLineItem.SKU = sku;
                forecastLineItem.Quantity = qty;
                forecastLineItem.Comments = txtFcComments.Text.Trim();
                forecastLineItem.Price = price;
                forecastLineItem.TotalPrice = totalPrice;
                forecastLineItem.LineItemStatus = ForecastStatuses.Pending;
                forecastLineItems.Add(forecastLineItem);

                //ViewState
                Session["forecastlineitems"] = forecastLineItems;

                //rptItem.DataSource = forecastLineItems;
                //rptItem.DataBind();

                TriggerClientGridRefresh();
                RegisterStartupScript("jsUnblockDialog", "closeSKUDialog();");
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
         
        }
        private void RefreshLineItems()
        {
            if (Session["forecastlineitems"] != null)
            {
                List<ForecastLineItem> forecastLineItems = (List<ForecastLineItem>)Session["forecastlineitems"];

                rptItem.DataSource = forecastLineItems;
                rptItem.DataBind();
                
            }
        }
        protected void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            RefreshLineItems();
        }
        private void TriggerClientGridRefresh()
        {
            string script = "__doPostBack(\"" + btnRefreshGrid.ClientID + "\", \"\");";
            RegisterStartupScript("jsGridRefresh", script);
        }
        protected void btnNewSKU_Click(object sender, EventArgs e)
        {
            lblSKU.Text = string.Empty;
            ddlSKU.SelectedIndex = 0;
            txtQuantity.Text = string.Empty;

            RegisterStartupScript("jsUnblockDialog", "unblockSKUDialog();");
        }
        protected void btnHistory_Click(object sender, EventArgs e)
        {

            int rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
            //ReloadRmahistory(rmaGUID);
            RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool isAdmin = false;
            string fcSource = "W";
            int companyID, userID, statusID;
            companyID = userID = 0;
            statusID = 1;
            List<ForecastLineItem> forecastLineItems = new List<ForecastLineItem>();
            FulfillmentForecast forecast = new FulfillmentForecast();

            ForecastLineItem forecastLineItem = null;


            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {

                userID = userInfo.UserGUID;
                
                companyID = userInfo.CompanyGUID;
                //ViewState["userid"] = userID;
                //companyID = 359;
                //userID = 327;
            }
            if (ddlStatus.SelectedIndex > 0)
            {
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);
            }
            if (Session["adm"] != null)
            {
                isAdmin = true;
                if (dpCompany.SelectedIndex > 0)
                {
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                }
                
            }
            try
            {
                if (!string.IsNullOrEmpty(txtFcDate.Text))
                {
                    forecast.ForecastNumber = lblForecastNum.Text;
                    forecast.ForecastDate = Convert.ToDateTime(txtFcDate.Text);
                    forecast.Comments = txtCommments.Text;

                    //if (Session["forecastlineitems"] != null)
                    //{
                    //    forecastLineItems = (List<ForecastLineItem>)Session["forecastlineitems"];

                    //}

                    foreach (RepeaterItem item in rptItem.Items)
                    {
                        CheckBox chkDel = item.FindControl("chkDel") as CheckBox;
                        if (!chkDel.Checked)
                        {
                            forecastLineItem = new ForecastLineItem();

                            Label lblSKU = item.FindControl("lblSKU") as Label;
                            TextBox txtQty = item.FindControl("txtQty") as TextBox;
                            Label lblComments = item.FindControl("lblComments") as Label;
                            
                            DropDownList ddlFDStatus = item.FindControl("ddlFDStatus") as DropDownList;
                            if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(lblSKU.Text))
                            {
                                forecastLineItem.SKU = lblSKU.Text;
                                forecastLineItem.Quantity = Convert.ToInt32(txtQty.Text);
                                forecastLineItem.Comments = lblComments.Text;
                                //ForecastStatuses status = new ForecastStatuses();
                                forecastLineItem.LineItemStatus = (ForecastStatuses)(Convert.ToInt32(ddlFDStatus.SelectedValue));

                                forecastLineItems.Add(forecastLineItem);
                            }
                        }
                    }
                    string successMessage = "Forecast is successfully added with <u><b>Forecast# {0}</b></u>.";
                    string forecastNumber = string.Empty;
                    if (forecastLineItems != null && forecastLineItems.Count > 0)
                    {
                        forecast.ForecastLineItems = forecastLineItems;

                        bool returnValue = SKUPricesOperations.FulfillmentForecastInsertUpdate(forecast, companyID, userID, fcSource, isAdmin, statusID, out forecastNumber);
                        if (returnValue)
                        {
                            if (Request["fid"] == null)
                            {
                                ClearData();
                                string msg = string.Format(successMessage, forecastNumber);
                                lblMsg.Text = msg;
                                
                            }
                            else
                            {

                                //lblMsg.Text = "Successfully updated";
                                Response.Redirect("ForecastQuery.aspx?search=1", false);
                            }
                        }
                        else
                            lblMsg.Text = "no  record updated";

                    }
                    else
                    {
                        lblMsg.Text = "There is no SKU to create Forecast";
                    }
                }
                else
                    lblMsg.Text = "Forecast date required!";
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        private void ClearData()
        {
            lblMsg.Text = string.Empty;
            txtFcDate.Text = string.Empty;
            //rptItem.DataSource = null;
            //rptItem.DataBind();
            BindLineItems();
            txtFcComments.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtCommments.Text = string.Empty;

            if (Session["adm"] != null)
            {
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                dpCompany.SelectedIndex = 0;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Request["fid"] != null)
            {
                Response.Redirect("ForecastQuery.aspx?search=1", false);
                //Response.Redirect("ForecastQuery.aspx", false);
            }
            else
                ClearData();
        }

        protected void rptItem_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lblFDStatus = (Label)e.Item.FindControl("lblFDStatus");
                DropDownList ddlFDStatus = (DropDownList)e.Item.FindControl("ddlFDStatus");

                if (Session["adm"] != null)
                {
                    lblFDStatus.Visible = false;
                    ddlFDStatus.Visible = true;
                }
                else
                {

                    lblFDStatus.Visible = true;
                    ddlFDStatus.Visible = false;
                }

            }
        }
    }
}