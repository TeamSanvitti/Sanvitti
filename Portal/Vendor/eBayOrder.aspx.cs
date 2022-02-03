using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Vendor
{
    public partial class eBayOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // DateTime shipByDate = Convert.ToDateTime("2021-09-02T06:59:59.000Z");
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
                Response.Redirect(url);
            }
            if(!IsPostBack)
            {
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                string[] array = currentDate.Split('-');
                string year = array[0];
                string month = array[1];
                string day = array[2];

                txtDateFrom.Text = month + "/" + day + "/" + year;
                txtDateTo.Text = month + "/" + day + "/" + year;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblCount.Text = "";
                lblMsg.Text = "";
                btnSubmit.Visible = false;
                btnValidate.Visible = false;
                gvOrder.DataSource = null;
                gvOrder.DataBind();
                Session["eBayOrderInfo"] = null;
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime TodayDate = DateTime.Now;
                if (!string.IsNullOrEmpty(txtDateFrom.Text))
                {
                    if (!string.IsNullOrEmpty(txtDateTo.Text))
                    {
                        if (ddlStatus.SelectedIndex > 0)
                        {
                            //if (!string.IsNullOrEmpty(txtToken.Text))
                            {
                                string dateFrom1 = txtDateFrom.Text;
                                string dateTo1 = txtDateTo.Text;
                                string[] array1 = dateFrom1.Split('/');
                                string[] array2 = dateTo1.Split('/');

                                string yearFrom = array1[2];
                                string monthFrom = array1[0];
                                string dayFrom = array1[1];

                                string yearTo = array2[2];
                                string monthTo = array2[0];
                                string dayTo = array2[1];

                                DateTime dateFrom = Convert.ToDateTime(txtDateFrom.Text);
                                DateTime dateTo = Convert.ToDateTime(txtDateTo.Text);

                                int d1 = Convert.ToInt32((TodayDate - dateFrom).TotalDays);
                                int d2 = Convert.ToInt32((TodayDate - dateTo).TotalDays);
                                int d3 = Convert.ToInt32((dateTo - dateFrom).TotalDays);

                                //if (d1 <= 30)
                                //{
                                //if (d1 >= 0)
                                // {
                                //if (d2 >= 0)
                                //{
                                if (d3 >= 0)
                                {
                                    string orderfulfillmentstatus = ddlStatus.SelectedValue;// "NOT_STARTED";

                                    string authCode = txtToken.Text; //"v%5E1.1%23i%5E1%23f%5E0%23p%5E3%23r%5E1%23I%5E3%23t%5EUl41XzU6MDRCOTNCNTBFQzE3Qzg2QzZGRjY5RkRFRUM3NTk5RDdfMl8xI0VeMTI4NA%3D%3D";
                                    string url = System.Configuration.ConfigurationManager.AppSettings["ebayorderfunction"].ToString();

                                    SV.Framework.Vendor.eBayOrderRequest request = new SV.Framework.Vendor.eBayOrderRequest();
                                    request.dateFrom = yearFrom + "-" + monthFrom + "-" + dayFrom; //dateFrom.ToString("yyyy-MM-dd");
                                    request.dateTo = yearTo + "-" + monthTo + "-" + dayTo; //dateTo.ToString("yyyy-MM-dd");
                                    request.orderfulfillmentstatus = orderfulfillmentstatus;
                                    request.authCode = authCode;
                                    bool IsApi = false;
                                    if (!string.IsNullOrEmpty(authCode))
                                        IsApi = true;

                                    //Dictionary<string, string> headers = new Dictionary<string, string>();
                                    //headers.Add("dateFrom", dateFrom.ToString("yyyy-MM-dd"));
                                    //headers.Add("dateTo", dateTo.ToString("yyyy-MM-dd"));
                                    //headers.Add("orderfulfillmentstatus", orderfulfillmentstatus);
                                    //headers.Add("limit", "10");
                                    //headers.Add("offset", "0");
                                    //headers.Add("authCode", authCode);

                                    GetOrders(url, request, IsApi);
                                }
                                else
                                    lblMsg.Text = "To date cannot be less than From date";
                                //        }
                                //        else
                                //            lblMsg.Text = "To date cannot be greater than today's date";
                                //    }
                                //    else
                                //        lblMsg.Text = "From date cannot be greater than today's date";
                                //}
                                //else
                                //{
                                //    lblMsg.Text = "From date cannot be older than 30 days";
                                //}
                            }
                            //else
                            //   lblMsg.Text = "Auth token is required!";
                        }
                        else
                            lblMsg.Text = "Status is required!";
                    }
                    else
                        lblMsg.Text = "To date is required!";
                }
                else
                {
                    lblMsg.Text = "From date is required!";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }
        public async void GetOrders(string url, SV.Framework.Vendor.eBayOrderRequest request, bool IsApi)
        {
            try
            {
                SV.Framework.Vendor.eBayOrderInfo eBayOrderInfo = await SV.Framework.Vendor.eBayOperations.GeteBayOrders(url, request, IsApi);
                if (eBayOrderInfo != null && eBayOrderInfo.orders != null && eBayOrderInfo.orders.Count > 0)
                {
                    foreach(var item in eBayOrderInfo.orders)
                    {
                        item.legacyOrderId = "";
                        foreach (var lineitem in item.lineItems)
                        {
                            lineitem.listingMarketplaceId = "";
                        }

                    }
                    lblCount.Text = "Total Count: " + eBayOrderInfo.orders.Count;
                    gvOrder.DataSource = eBayOrderInfo.orders;
                    gvOrder.DataBind();
                    Session["eBayOrderInfo"] = eBayOrderInfo;
                    //btnSubmit.Visible = true;
                    btnValidate.Visible = true;
                }
                else
                    lblMsg.Text = "No record found!";

            }
            catch(Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }
        protected void btnGetToken_Click(object sender, EventArgs e)
        {
            string ebayUrl = ConfigurationSettings.AppSettings["ebayorderprod"].ToString();
            if (Request["op"] != null && Request["op"].ToString().ToLower() == "t")
            {
                ebayUrl = ConfigurationSettings.AppSettings["ebayordersandbox"].ToString();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('" + ebayUrl + "')</script>", false);

        }
        private void SubmitOrders()
        {
            int userID = Convert.ToInt32(Session["UserID"]);
            string errorMessage = "";
            int index = 0;
            bool IsValid = false;
            lblMsg.Text = "";
            

            SV.Framework.Vendor.eBayOrderInfo eBayOrderInfo = (SV.Framework.Vendor.eBayOrderInfo)Session["eBayOrderInfo"];            

            if (eBayOrderInfo != null && eBayOrderInfo.orders != null && eBayOrderInfo.orders.Count > 0)
            {
                foreach (GridViewRow row in gvOrder.Rows)
                {
                    CheckBox chkPO = row.FindControl("chkPO") as CheckBox;
                    if (chkPO.Checked)
                    {
                        IsValid = true;
                        break;
                    }                    
                }
                
                if (IsValid)
                {
                    if (eBayOrderInfo.orders.Count <= 50)
                    {
                        foreach (GridViewRow row in gvOrder.Rows)
                        {
                            CheckBox chkPO = row.FindControl("chkPO") as CheckBox;
                            if (!chkPO.Checked || !chkPO.Enabled)
                            {
                                eBayOrderInfo.orders.RemoveAt(index);
                                index = index - 1;
                            }
                            else if (!IsValid)
                            { IsValid = true; }
                            index = index + 1;
                        }
                        int logID = SV.Framework.Vendor.eBayOperations.eBayOrderRefreshLogInsert(eBayOrderInfo, userID, out errorMessage);
                        if (logID > 0)
                        {
                            int returnResult = SV.Framework.Vendor.eBayOperations.eBayOrderInsertUpdate(logID, eBayOrderInfo, userID, out errorMessage);
                            if (returnResult > 0)
                                lblMsg.Text = returnResult + " order(s) imported successfully!";
                            else
                            {
                                lblMsg.Text = errorMessage;
                            }
                            Session["eBayOrderInfo"] = null;
                        }
                        else
                        {
                            lblMsg.Text = errorMessage;
                        }
                    }
                    else
                    {
                        lblMsg.Text = "50 order can import at a time!";
                    }
                }
                else
                    lblMsg.Text = "Please select at least one record!";
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtDateFrom.Text = "";
            txtDateTo.Text = "";
            txtToken.Text = "";
            ddlStatus.SelectedIndex = 1;
            lblCount.Text = "";
            lblMsg.Text = "";
            btnSubmit.Visible = false;
            gvOrder.DataSource = null;
            gvOrder.DataBind();
            Session["eBayOrderInfo"] = null;

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SubmitOrders();
        }

        protected void gvOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //header select all function
            if (e.Row.RowType == DataControlRowType.Header)
            {
                ((CheckBox)e.Row.FindControl("allchk")).Attributes.Add("onclick",
                    "javascript:SelectAll('" +
                    ((CheckBox)e.Row.FindControl("allchk")).ClientID + "')");
            }
            if (e.Row.DataItem == null)
                return;

            ImageButton imgView = e.Row.FindControl("imgView") as ImageButton;
            imgView.OnClientClick = "openDialogAndBlock('Line Items', '" + imgView.ClientID + "')";

        }

        protected void gvOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrder.PageIndex = e.NewPageIndex;

            if (Session["eBayOrderInfo"] != null)
            {
                SV.Framework.Vendor.eBayOrderInfo eBayOrderInfo = Session["eBayOrderInfo"] as SV.Framework.Vendor.eBayOrderInfo;
                
                gvOrder.DataSource = eBayOrderInfo.orders;
                gvOrder.DataBind();
            }
        }

        protected void imgView_Command(object sender, CommandEventArgs e)
        {
            string orderId = e.CommandArgument.ToString();
            SV.Framework.Vendor.eBayOrderInfo eBayOrderInfo = Session["eBayOrderInfo"] as SV.Framework.Vendor.eBayOrderInfo;
            lblOrderId.Text = orderId;
            List<SV.Framework.Vendor.eBayOrders> orders = (from item in eBayOrderInfo.orders where item.orderId.Equals(orderId) select item).ToList();
            rptItems.DataSource = orders[0].lineItems;
            rptItems.DataBind();


            RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            try
            {                
                SV.Framework.Vendor.eBayOrderInfo eBayOrderInfo = Session["eBayOrderInfo"] as SV.Framework.Vendor.eBayOrderInfo;

                SV.Framework.Vendor.eBayOrderInfo eBayOrderInfoNew = SV.Framework.Vendor.eBayOperations.eBayOrderValidate(eBayOrderInfo, out errorMessage);
                if (eBayOrderInfoNew != null && eBayOrderInfoNew.orders != null && eBayOrderInfoNew.orders.Count > 0)
                {
                    //lblCount.Text = "Total Count: " + eBayOrderInfoNew.orders.Count;
                    gvOrder.DataSource = eBayOrderInfoNew.orders;
                    gvOrder.DataBind();
                    Session["eBayOrderInfo"] = eBayOrderInfoNew;
                    var orders = (from item in eBayOrderInfoNew.orders where item.legacyOrderId.Equals("") select item).ToList();
                    if (orders != null && orders.Count > 0)
                        btnSubmit.Visible = true;
                }
                else
                    lblMsg.Text = "No record found!";
            }
            catch(Exception ex)
            {
                lblMsg.Text = errorMessage;
            }
        }

        protected void btnGetToken_Click1(object sender, EventArgs e)
        {             
            string ebayUrl = "https://auth.ebay.com/oauth2/authorize?client_id=stkmanag-langloba-PRD-91d9af84c-be224c1b&response_type=code&redirect_uri=stkmanagement-stkmanag-langlo-vmasgpbws&scope=https://api.ebay.com/oauth/api_scope+https://api.ebay.com/oauth/api_scope/sell.fulfillment+https://api.ebay.com/oauth/api_scope/sell.fulfillment.readonly&state=current-page";
            if (Request["op"] != null && Request["op"].ToString().ToLower() == "t")
            {
                ebayUrl = "https://auth.sandbox.ebay.com/oauth2/authorize?client_id=stkmanag-langloba-SBX-77ffb2180-30f97435&response_type=code&redirect_uri=stkmanagement-stkmanag-langlo-akkji&scope=https://api.ebay.com/oauth/api_scope+https://api.ebay.com/oauth/api_scope/sell.fulfillment+https://api.ebay.com/oauth/api_scope/sell.fulfillment.readonly&state=current-page"; // ConfigurationSettings.AppSettings["ebayinventorysandbox"].ToString();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('" + ebayUrl + "')</script>", false);

        }
    }
}