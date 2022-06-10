using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
//using avii.Classes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SV.Framework.Fulfillment;
using SV.Framework.Models.Fulfillment;

namespace avii
{
    public partial class POQueryNew : System.Web.UI.Page
    {
        private SV.Framework.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.Fulfillment.PurchaseOrder.CreateInstance<SV.Framework.Fulfillment.PurchaseOrder>();

        string gvUniqueID = String.Empty;
        int gvNewPageIndex = 0;
        int gvEditIndex = -1;
        string gvSortExpr = String.Empty;
        string downLoadPath = string.Empty;
        string writer = "csv";
        bool grid1SelectCommand = false;
        string shipFromContactName = ConfigurationSettings.AppSettings["ShipFromContactName"].ToString();
        string shipFromContactName2 = ConfigurationSettings.AppSettings["ShipFromContactName2"].ToString();
        string shipFromAddress = ConfigurationSettings.AppSettings["ShipFromAddress"].ToString();
        string shipFromCity = ConfigurationSettings.AppSettings["ShipFromCity"].ToString();
        string shipFromState = ConfigurationSettings.AppSettings["ShipFromState"].ToString();
        string shipFromZip = ConfigurationSettings.AppSettings["ShipFromZip"].ToString();
        string shipFromCountry = ConfigurationSettings.AppSettings["ShipFromCountry"].ToString();
        string shipFromAttn = ConfigurationSettings.AppSettings["ShipFromAttn"].ToString();
        string shipFromPhone = ConfigurationSettings.AppSettings["ShipFromPhone"].ToString();
        private string gvSortDir
        {

            get { return ViewState["SortDirection"] as string ?? "ASC"; }

            set { ViewState["SortDirection"] = value; }

        }
        
        private void ReadOlnyAccess()
        {
            //string url = HttpContext.Current.Request.Url.AbsoluteUri;
            // http://localhost:1302/TESTERS/Default6.aspx

            string path = HttpContext.Current.Request.Url.AbsolutePath;
            // /TESTERS/Default6.aspx
            if(path != null)
            {
                path = path.ToLower();
                List<avii.Classes.MenuItem> menuItems = Session["MenuItems"] as List<avii.Classes.MenuItem>;
               foreach(avii.Classes.MenuItem item in menuItems)
                {
                    if (item.Url.ToLower().Contains(path) && item.IsReadOnly)
                    {
                        ViewState["IsReadOnly"] = true;

                    }
                }

            }

        }
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
            if (!IsPostBack)
            {
                ReadOlnyAccess();
                BindShipBy();
                BindStates();
                /// Enable podate for assigned users
                string uploadAdmin = ConfigurationSettings.AppSettings["UploadAdmin"].ToString();
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                List<avii.Classes.UserRole> userRoles = userInfo.ActiveRoles;
                if (userRoles != null && userRoles.Count > 0)
                {
                    var roles = (from item in userRoles where item.RoleName.Equals(uploadAdmin) select item).ToList();
                    if (roles != null && roles.Count > 0 && !string.IsNullOrEmpty(roles[0].RoleName))
                    {
                        ViewState["adminrole"] = roles[0].RoleName;
                        txtPODate.Enabled = true;
                        imgCal.Visible = true;
                    }
                    else
                    {
                        txtPODate.Enabled = false;
                        imgCal.Visible = false;
                    }
                }
                //txtPODate.Enabled = false;
                //imgCal.Visible = false;
                //imgCal.Visible = false;
                
                if (Session["adm"] == null)
                {
                    btnESNDelete.Visible = false;
                    // btnTracking.Visible = false;
                    chkRecieved.Visible = false;
                    lblRecieve.Visible = false;
                    foreach (DataControlField dc in gvPOQuery.Columns)
                    {
                        if (dc.HeaderText.Equals("Edit") || dc.HeaderText.Equals("Delete"))
                        {
                            dc.Visible = false;
                        }
                    }
                    int userID = 0;
                    //UserInfo userInfo = Session["userInfo"] as UserInfo;
                    if (userInfo != null)
                    {
                        userID = userInfo.UserGUID;
                        ViewState["userid"] = userID;
                    }
                    //if (Session["UserID"] != null)
                    //{
                    //    int.TryParse(Session["UserID"].ToString(), out userID);
                    //}
                    txtStoreID.Visible = false;
                    BindCustomerStores(userID);
                }
                else
                {
                   // btnTracking.Visible = true;
                    ddlUserStores.Visible = false;
                    pnlCompany.Visible = true;
                    bindCustomerDropDown();
                }

                lblMsg.Text = string.Empty;
                BindPOFromDashboard();
                BindStockInDemand();
            }
            downLoadPath = ConfigurationManager.AppSettings["FullfillmentFilesStoreage"].ToString();

        }
        private void BindShipBy()
        {
            try
            {
                List<ShipBy> shipBy = purchaseOrderOperation.GetShipByList();
                Session["shipby"] = shipBy;
                dpShipVia.DataSource = shipBy;
                dpShipVia.DataTextField = "ShipCodeNText";
                dpShipVia.DataValueField = "ShipByCode";
                dpShipVia.DataBind();

                dpShipBy.DataSource = shipBy;
                dpShipBy.DataTextField = "ShipByText";
                dpShipBy.DataValueField = "ShipByID";
                dpShipBy.DataBind();

                ddlShipVia.DataSource = shipBy;
                ddlShipVia.DataTextField = "ShipCodeNText";
                ddlShipVia.DataValueField = "ShipByCode";
                ddlShipVia.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        private void BindStates()
        {
            DataTable dataTable = avii.Classes.clsCompany.GetState(0);
            ViewState["state"] = dataTable; 
            dpState.DataSource = dataTable;
            dpState.DataTextField = "Statecode";
            dpState.DataValueField = "Statecode";
            dpState.DataBind();
          System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("", "");
            dpState.Items.Insert(0, item);

            //dpState.SelectedValue = "CA";

        }

        //Search PO based on dashboard status & PO date search critria
        private void BindStockInDemand()
        {
            string statusID = "1";
            string SKU = string.Empty;
            int companyID = 0;
            string sortExpression = "PurchaseOrderDate";
            string sortDirection = "ASC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;

            if (Session["sku"] != null)
            {
                SKU = Convert.ToString(Session["sku"]);
                ViewState["StockInDemand"] = 1;
                txtSKU.Text = SKU;
                if (Session["adm"] != null)
                {
                    if (Session["cid"] != null)
                        companyID = Convert.ToInt32(Session["cid"]);
                    if (companyID > 0)
                        dpCompany.SelectedValue = companyID.ToString();
                    Session["cid"] = null;
                }
                BindPO();
                Session["sku"] = null;
                
            }
        }
        //Search PO based on dashboard status & PO date search critria
        private void BindPOFromDashboard()
        {
            string statusID = "0";
            int days = 30;
            int companyID = 0;
            string sortExpression = "PurchaseOrderDate";
            string sortDirection = "ASC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;
            string poType = "";
            if (Session["postatus"] != null && Session["days"] != null)
            {
                days = Convert.ToInt32(Session["days"]);
                statusID = Convert.ToString(Session["postatus"]);
                poType = Convert.ToString(Session["type"]);

                DateTime today = DateTime.Today;
                DateTime poDate = today.AddDays(-days);
                dpStatusList.SelectedValue = statusID;
                dpPOType.SelectedValue = poType;
                txtFromDate.Text = poDate.ToShortDateString();

                if (Session["adm"] != null)
                {
                    if (Session["cid"] != null)
                        companyID = Convert.ToInt32(Session["cid"]);
                    if (companyID > 0)
                        dpCompany.SelectedValue = companyID.ToString();
                    Session["cid"] = null;
                }
                BindPO();
                Session["postatus"] = null;
                Session["days"] = null;
                Session["type"] = null;
            }
        }
        protected void bindCustomerDropDown()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 1);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        protected void BindCustomerStores(int userID)
        {
            List<SV.Framework.Admin.StoreLocation> storeList = SV.Framework.Admin.UserStoreOperation.GetUserStoreLocationList(0, userID);
            
            ddlUserStores.DataSource = storeList;//clsCompany.GetCompanyStores(userID);
            ddlUserStores.DataValueField = "StoreID";
            ddlUserStores.DataTextField = "StoreID";
            ddlUserStores.DataBind();
            System.Web.UI.WebControls.ListItem newList = new System.Web.UI.WebControls.ListItem("", "");
            ddlUserStores.Items.Insert(0, newList);

        }
        private void CleanSummary()
        {
            lblPendingPOs.Text = string.Empty;
            lblShippedPOs.Text = string.Empty;
            lblProcessedPOs.Text = string.Empty;
            lblTotalPOs.Text = string.Empty;

            lnkSumary.Visible = false;
            pnlSummary.Visible = false;
        }

        private void CleanForm()
        {
            dpPOType.SelectedIndex = 0;
            lblCount.Text = string.Empty;
            CleanSummary();
            btnDownloadData.Visible = false;
            trPopup.Visible = false;
            trUpload.Visible = false;
            btnUpload.Visible = false;
            btnDown.Visible = false;
            btnPoHeader.Visible = false;
            btnEsn_Excel.Visible = false;
            btnDownPO.Visible = false;
            btnPoDetail.Visible = false;
            btnPoDetailTrk.Visible = false;
            btnChangeStatus.Visible = false;
            lblMsg.Text = string.Empty;
            //txtFmUpc.Text = string.Empty;
            this.txtFromDate.Text = string.Empty;
            txtPONum.Text = string.Empty;
            txtStoreID.Text = string.Empty;
            txtToDate.Text = string.Empty;
            //txtItemCode.Text = string.Empty;
            //txtCustName.Text = string.Empty;
            this.//txtMslNumber.Text = string.Empty;
            //this.txtAvNumber.Text = string.Empty;
            txtEsn.Text = string.Empty;
            //dpPhoneCategory.SelectedIndex = -1;
            txtShipFrom.Text = string.Empty;
            txtShipTo.Text = string.Empty;
            trStatus.Visible = false;
            gvPOQuery.DataSource = null;
            gvPOQuery.DataBind();
            dpStatusList.SelectedIndex = 0;
            //dpZone.SelectedIndex = 0;
            if (Session["adm"] != null)
                dpCompany.SelectedIndex = 0;
            chkDownload.Checked = false;

        }

        private void BindGrid1(PurchaseOrders pos)
        {
            if (pos != null && pos.PurchaseOrderList.Count > 0)
            {
                gvPOQuery.DataSource = pos.PurchaseOrderList;
                gvPOQuery.DataBind();
            }
        }

        private void BindPO()
        {
            string timetaken;
            int StockInDemand = 0;
            bool validForm = true;
            string poNum, fromDate, toDate, storeID, statusID, esn, avOrder, mslNumber, phoneCategory, sku,  shipFrom, shipTo, trackingNumber, customerOrderNumber, contactName, POType;
            string sortExpression = "PurchaseOrderDate";
            string sortDirection = "ASC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;
            poNum = fromDate = toDate = shipFrom = shipTo = trackingNumber = customerOrderNumber = contactName = POType = sku = null;
            if (ViewState["StockInDemand"] != null)
            {
                StockInDemand = Convert.ToInt32(ViewState["StockInDemand"]);
                ViewState["StockInDemand"] = null;
            }

            trUpload.Visible = false;
            btnPackSlipAll.Visible = false;
            btnASN.Visible = false;
            lblMsg.Text = string.Empty;
            lnkSumary.Visible = false;
            int userID = 0, companyID = 0;
            try
            {
                if (Session["adm"] != null)
                {
                    userID = 0;
                }
                else
                    if (ViewState["userid"] != null)
                    {
                        userID = Convert.ToInt32(ViewState["userid"]);

                    }

                CleanSummary();
                contactName = (txtSContactName.Text.Trim().Length > 0 ? txtSContactName.Text.Trim() : null);
                sku = (txtSKU.Text.Trim().Length > 0 ? txtSKU.Text.Trim() : null);
                trackingNumber = (txtSTrackingNo.Text.Trim().Length > 0 ? txtSTrackingNo.Text.Trim() : null);
                customerOrderNumber = (txtCustOrderNo.Text.Trim().Length > 0 ? txtCustOrderNo.Text.Trim() : null);
                poNum = (txtPONum.Text.Trim().Length > 0 ? txtPONum.Text.Trim() : null);
                //contactName = (txtCustName.Text.Trim().Length > 0 ? txtCustName.Text.Trim() : null);
                storeID = (txtStoreID.Text.Trim().Length > 0 ? txtStoreID.Text.Trim() : null);
                //zoneGUID = dpZone.SelectedValue;
                if (txtFromDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtFromDate.Text, out dt))
                        fromDate = dt.ToShortDateString();
                    else
                        throw new Exception("PO Date From does not have correct format(MM/DD/YYYY)");
                }
                if (pnlCompany.Visible == true)
                {
                    if (dpCompany.SelectedIndex > 0)
                    {
                        companyID = Convert.ToInt32(dpCompany.SelectedValue);
                    }
                }
                if (txtToDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtToDate.Text, out dt))
                        toDate = dt.ToShortDateString();
                    else
                        throw new Exception("PO Date To does not have correct format(MM/DD/YYYY)");

                }

                if (txtShipFrom.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtShipFrom.Text, out dt))
                        shipFrom = dt.ToShortDateString();
                    else
                        throw new Exception("Shipping From Date To does not have correct format(MM/DD/YYYY)");

                }

                if (txtShipTo.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtShipTo.Text, out dt))
                        shipTo = dt.ToShortDateString();
                    else
                        throw new Exception("Shipping To Date To does not have correct format(MM/DD/YYYY)");
                }

                statusID = (dpStatusList.SelectedIndex > 0 ? dpStatusList.SelectedValue : null);
                POType = (dpPOType.SelectedIndex > 0 ? dpPOType.SelectedValue : null);

                timetaken = DateTime.Now.ToLongTimeString();

                esn = null;
                if (txtEsn.Text.Trim().Length > 0)
                {
                    esn = txtEsn.Text.Trim();
                }

                avOrder = null;
                //if (this.txtAvNumber.Text.Trim().Length > 0)
                //{
                //    avOrder = txtAvNumber.Text.Trim();
                //}

                mslNumber = null;
                //if (this.txtMslNumber.Text.Trim().Length > 0)
                //{
                //    mslNumber = this.txtMslNumber.Text.Trim();
                //}
                //itemCode = null;
               // if (this.txtItemCode.Text.Trim().Length > 0)
                //{
                //    itemCode = this.txtItemCode.Text.Trim();
                //}

                phoneCategory = null;
                //if (dpPhoneCategory.SelectedIndex > 0)
                //{
                //    phoneCategory = dpPhoneCategory.SelectedValue;
                //}

                if (string.IsNullOrEmpty(storeID) && string.IsNullOrEmpty(poNum) && 
                        string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate) && dpStatusList.SelectedIndex == 0 
                        && dpCompany.SelectedIndex == 0 && string.IsNullOrEmpty(esn) &&  string.IsNullOrEmpty(mslNumber) &&
                        string.IsNullOrEmpty(shipFrom) && string.IsNullOrEmpty(shipTo) && string.IsNullOrEmpty(trackingNumber)
                        && string.IsNullOrEmpty(customerOrderNumber) && string.IsNullOrEmpty(contactName) && string.IsNullOrEmpty(POType) && string.IsNullOrEmpty(sku)) 
                {
                    validForm = false;
                }


                if (validForm)
                {
                    try
                    {
                        Session["posearchcriteria"] = poNum + "~" +  fromDate + "~" + toDate + "~" + userID + "~" + statusID + "~" + companyID + "~" + esn + "~" +  mslNumber + "~" +  storeID + "~" + shipFrom + "~" + shipTo + "~" + trackingNumber + "~"+ customerOrderNumber + "~" + contactName + "~" + POType + "~" + sku;

                        PurchaseOrders pos = purchaseOrderOperation.GerPurchaseOrdersNew(poNum, contactName, fromDate, toDate, userID, statusID, companyID, esn, avOrder, mslNumber, phoneCategory, sku, storeID, null, null, shipFrom, shipTo, 0, trackingNumber, customerOrderNumber, POType, StockInDemand);
                        Session["POS"] = pos;
                        if (pos != null && pos.PurchaseOrderList.Count > 0)
                        {
                            lnkSumary.Visible = true;
                            //if (Session["adm"] != null)
                            {
                                btnPackSlipAll.Visible = true;
                                btnASN.Visible = true;
                            }
                            lblCount.Text = "<strong>Total count:</strong> " + pos.PurchaseOrderList.Count.ToString();
                            //btnDownPO.Visible = true;

                            //btnPoDetailTrk.Visible = true;
                            //btnEsn_Excel.Visible = true;
                            //btnPoHeader.Visible = true;
                            //btnDown.Visible = true;

                            btnDownloadData.Visible = true;
                            trPopup.Visible = true;
                            //if (Session["adm"] != null)
                            //{
                            //    btnChangeStatus.Visible = true;
                            //}

                            gvPOQuery.DataSource = pos.PurchaseOrderList;
                            gvPOQuery.DataBind();
                        }
                        else
                        {
                            lblCount.Text = string.Empty;
                            btnDownloadData.Visible = false;
                            trPopup.Visible = false;
                            btnPoHeader.Visible = false;
                            btnEsn_Excel.Visible = false;
                            btnDown.Visible = false;
                            btnDownPO.Visible = false;
                            btnPoDetail.Visible = false;
                            btnPoDetailTrk.Visible = false;
                            gvPOQuery.DataSource = null;
                            gvPOQuery.DataBind();
                            btnUpload.Visible = false;
                            btnChangeStatus.Visible = false;
                            trPopup.Visible = false;
                            lblMsg.Text = "No record exists";
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToString().Contains("Timeout expired"))
                            lblMsg.Text = "PO Query is taking longer time than usual, add more search criteria for faster result.";
                        else
                            lblMsg.Text = ex.Message;
                    }
                }
                else
                {
                    CleanForm();
                    lblMsg.Text = "Please select the search criteria";
                } 

            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().Contains("Timeout expired"))
                    lblMsg.Text = "PO Query is taking longer time than usual, add more search criteria for faster result.";
                else
                    lblMsg.Text = ex.Message;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }


        private PurchaseOrders GetPOwithDetail()
        {
            PurchaseOrders pos = null;
            string timetaken;

            bool validForm = true;
            string poNum, fromDate, toDate, contactName, storeID, statusID, esn, avOrder, mslNumber, phoneCategory, itemCode, fmUPC, zoneGUID, shipFrom, shipTo;
            poNum = fromDate = toDate = shipFrom = shipTo = null;
            trUpload.Visible = false;
            lblMsg.Text = string.Empty;
            lnkSumary.Visible = false;
            int userID = 0, companyID = 0;
            try
            {
                if (Session["adm"] != null)
                {
                    userID = 0;
                }
                else
                    if (ViewState["userid"] != null)
                    {
                        userID = Convert.ToInt32(ViewState["userid"]);

                    }

                ///CleanSummary();
                //fmUPC = (txtFmUpc.Text.Trim().Length > 0 ? txtFmUpc.Text.Trim() : null);
                poNum = (txtPONum.Text.Trim().Length > 0 ? txtPONum.Text.Trim() : null);
                //contactName = (txtCustName.Text.Trim().Length > 0 ? txtCustName.Text.Trim() : null);
                storeID = (txtStoreID.Text.Trim().Length > 0 ? txtStoreID.Text.Trim() : null);
                //zoneGUID = dpZone.SelectedValue;
                if (txtFromDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtFromDate.Text, out dt))
                        fromDate = dt.ToShortDateString();
                    else
                        throw new Exception("PO Date From does not have correct format(MM/DD/YYYY)");
                }
                if (pnlCompany.Visible == true)
                {
                    if (dpCompany.SelectedIndex > 0)
                    {
                        companyID = Convert.ToInt32(dpCompany.SelectedValue);
                    }
                }
                if (txtToDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtToDate.Text, out dt))
                        toDate = dt.ToShortDateString();
                    else
                        throw new Exception("PO Date To does not have correct format(MM/DD/YYYY)");

                }

                if (txtShipFrom.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtShipFrom.Text, out dt))
                        shipFrom = dt.ToShortDateString();
                    else
                        throw new Exception("Shipping From Date To does not have correct format(MM/DD/YYYY)");

                }

                if (txtShipTo.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtShipTo.Text, out dt))
                        shipTo = dt.ToShortDateString();
                    else
                        throw new Exception("Shipping To Date To does not have correct format(MM/DD/YYYY)");
                }

                statusID = (dpStatusList.SelectedIndex > 0 ? dpStatusList.SelectedValue : null);

                timetaken = DateTime.Now.ToLongTimeString();

                esn = null;
                if (txtEsn.Text.Trim().Length > 0)
                {
                    esn = txtEsn.Text.Trim();
                }

                avOrder = null;
                //if (this.txtAvNumber.Text.Trim().Length > 0)
                //{
                //    avOrder = txtAvNumber.Text.Trim();
                //}

                mslNumber = null;
                //if (this.txtMslNumber.Text.Trim().Length > 0)
                //{
                //    mslNumber = this.txtMslNumber.Text.Trim();
                //}
                itemCode = null;
                //if (this.txtItemCode.Text.Trim().Length > 0)
                //{
                //    itemCode = this.txtItemCode.Text.Trim();
                //}

                phoneCategory = null;
                //if (dpPhoneCategory.SelectedIndex > 0)
                //{
                //    phoneCategory = dpPhoneCategory.SelectedValue;
                //}

                if (string.IsNullOrEmpty(storeID) && string.IsNullOrEmpty(poNum) && string.IsNullOrEmpty(itemCode) &&
                        string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate) && dpStatusList.SelectedIndex == 0 
                        && dpCompany.SelectedIndex == 0 && string.IsNullOrEmpty(esn) && string.IsNullOrEmpty(avOrder) && string.IsNullOrEmpty(mslNumber) &&
                        string.IsNullOrEmpty(shipFrom) && string.IsNullOrEmpty(shipTo))
                {
                    validForm = false;
                }


                if (validForm)
                {
                    try
                    {
                        pos = purchaseOrderOperation.GerPurchaseOrders(poNum, "", fromDate, toDate, userID, statusID, companyID, esn, avOrder, mslNumber, phoneCategory, itemCode, storeID, "", "", shipFrom, shipTo, 0);
                        Session["POD"] = pos;

                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToString().Contains("Timeout expired"))
                            lblMsg.Text = "PO Query is taking longer time than usual, add more search criteria for faster result.";
                        else
                            lblMsg.Text = ex.Message;
                    }
                }
                else
                {
                    //CleanForm();
                    lblMsg.Text = "Please select the search criteria";
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().Contains("Timeout expired"))
                    lblMsg.Text = "PO Query is taking longer time than usual, add more search criteria for faster result.";
                else
                    lblMsg.Text = ex.Message;
            }
            return pos;
        }

        private PurchaseOrders GetSelectedPurchaseOrdes(int downloadFlag)
        {
            PurchaseOrders pos = null;

            StringBuilder sbIndex = new StringBuilder();
            if (chkDownload.Checked)
            {
                foreach (GridViewRow row in gvPOQuery.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");

                    if (chk.Checked)
                    {

                        //sbIndex.Append("#" + (gvPOQuery.PageSize * (gvPOQuery.PageIndex) + row.RowIndex) + "#");
                        string poid = gvPOQuery.DataKeys[row.RowIndex].Value.ToString();
                        if (sbIndex.Length > 0)
                            sbIndex.Append("," + poid);
                        else
                            sbIndex.Append(poid);

                    }
                }
                if (sbIndex.Length > 0)
                {
                    pos = purchaseOrderOperation.GerSelectedPurchaseOrders(sbIndex.ToString(), downloadFlag);

                }
                else
                    lblMsg.Text = "Please select Purchase Order(s)";


            }
            else
            {
                if (Session["POS"] != null)
                {
                    PurchaseOrders purchaseOrders = Session["POS"] as PurchaseOrders;
                    foreach (BasePurchaseOrder po in purchaseOrders.PurchaseOrderList)
                    {
                        string poid = po.PurchaseOrderID.ToString();
                        if (sbIndex.Length > 0)
                            sbIndex.Append("," + poid);
                        else
                            sbIndex.Append(poid);

                    }
                }
                if (sbIndex.Length > 0)
                {
                    pos = purchaseOrderOperation.GerSelectedPurchaseOrders(sbIndex.ToString(), downloadFlag);

                }
                //lblMsg.Text = "Please select Purchase Order(s)";
            }
            return pos;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Session["POS"] = null;
            Session["POD"] = null;
            
            BindPO();
            //string timetaken;

            //bool validForm = true;
            //string poNum, fromDate, toDate, contactName, storeID, statusID, esn, avOrder, mslNumber, phoneCategory, itemCode, fmUPC, zoneGUID, shipFrom, shipTo;
            //poNum = fromDate = toDate = shipFrom = shipTo = null;
            //trUpload.Visible = false;
            //lblMsg.Text = string.Empty;
            //lnkSumary.Visible = false;
            //int userID = 0, companyID = 0;
            //try
            //{
            //    if (Session["UserID"] != null)
            //    {
            //        int.TryParse(Session["UserID"].ToString(), out userID);
            //    }
            //    CleanSummary();
            //    fmUPC = (txtFmUpc.Text.Trim().Length > 0 ? txtFmUpc.Text.Trim() : null);
            //    poNum = (txtPONum.Text.Trim().Length > 0 ? txtPONum.Text.Trim() : null);
            //    contactName = (txtCustName.Text.Trim().Length > 0 ? txtCustName.Text.Trim() : null);
            //    storeID = (txtStoreID.Text.Trim().Length > 0 ? txtStoreID.Text.Trim() : null);
            //    zoneGUID = dpZone.SelectedValue;
            //    if (txtFromDate.Text.Trim().Length > 0)
            //    {
            //        DateTime dt;
            //        if (DateTime.TryParse(txtFromDate.Text, out dt))
            //            fromDate = dt.ToShortDateString();
            //        else
            //            throw new Exception("PO Date From does not have correct format(MM/DD/YYYY)");
            //    }
            //    if (pnlCompany.Visible == true)
            //    {
            //        if (dpCompany.SelectedIndex > 0)
            //        {
            //            companyID = Convert.ToInt32(dpCompany.SelectedValue);
            //        }
            //    }
            //    if (txtToDate.Text.Trim().Length > 0)
            //    {
            //        DateTime dt;
            //        if (DateTime.TryParse(txtToDate.Text, out dt))
            //            toDate = dt.ToShortDateString();
            //        else
            //            throw new Exception("PO Date To does not have correct format(MM/DD/YYYY)");

            //    }

            //    if (txtShipFrom.Text.Trim().Length > 0)
            //    {
            //        DateTime dt;
            //        if (DateTime.TryParse(txtShipFrom.Text, out dt))
            //            shipFrom = dt.ToShortDateString();
            //        else
            //            throw new Exception("Shipping From Date To does not have correct format(MM/DD/YYYY)");

            //    }

            //    if (txtShipTo.Text.Trim().Length > 0)
            //    {
            //        DateTime dt;
            //        if (DateTime.TryParse(txtShipTo.Text, out dt))
            //            shipTo = dt.ToShortDateString();
            //        else
            //            throw new Exception("Shipping To Date To does not have correct format(MM/DD/YYYY)");
            //    }

            //    statusID = (dpStatusList.SelectedIndex > 0 ? dpStatusList.SelectedValue : null);

            //    timetaken = DateTime.Now.ToLongTimeString();

            //    esn = null;
            //    if (txtEsn.Text.Trim().Length > 0)
            //    {
            //        esn = txtEsn.Text.Trim();
            //    }

            //    avOrder = null;
            //    if (this.txtAvNumber.Text.Trim().Length > 0)
            //    {
            //        avOrder = txtAvNumber.Text.Trim();
            //    }

            //    mslNumber = null;
            //    if (this.txtMslNumber.Text.Trim().Length > 0)
            //    {
            //        mslNumber = this.txtMslNumber.Text.Trim();
            //    }
            //    itemCode = null;
            //    if (this.txtItemCode.Text.Trim().Length > 0)
            //    {
            //        itemCode = this.txtItemCode.Text.Trim();
            //    }

            //    phoneCategory = null;
            //    if (dpPhoneCategory.SelectedIndex > 0)
            //    {
            //        phoneCategory = dpPhoneCategory.SelectedValue;
            //    }

            //    if (string.IsNullOrEmpty(storeID) && string.IsNullOrEmpty(poNum) && string.IsNullOrEmpty(contactName) && string.IsNullOrEmpty(itemCode) &&
            //            string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate) && dpStatusList.SelectedIndex == 0 && string.IsNullOrEmpty(fmUPC)
            //            && dpCompany.SelectedIndex == 0 && string.IsNullOrEmpty(esn) && string.IsNullOrEmpty(avOrder) && string.IsNullOrEmpty(mslNumber) &&
            //            string.IsNullOrEmpty(shipFrom) && string.IsNullOrEmpty(shipTo))
            //    {
            //        validForm = false;
            //    }


            //    if (validForm)
            //    {
            //        PurchaseOrders pos = PurchaseOrder.GerPurchaseOrders(poNum, contactName, fromDate, toDate, userID, statusID, companyID, esn, avOrder, mslNumber, phoneCategory, itemCode, storeID, fmUPC, zoneGUID, shipFrom, shipTo);
            //        Session["POS"] = pos;
            //        if (pos != null && pos.PurchaseOrderList.Count > 0)
            //        {
            //            lnkSumary.Visible = true;
            //            btnDownPO.Visible = true;

            //            btnPoDetailTrk.Visible = true;
            //            btnEsn_Excel.Visible = true;
            //            btnPoHeader.Visible = true;
            //            btnDown.Visible = true;

            //            if (Session["adm"] != null)
            //            {
            //                btnChangeStatus.Visible = true;
            //            }

            //            gvPOQuery.DataSource = pos.PurchaseOrderList;
            //            gvPOQuery.DataBind();

            //        }
            //        else
            //        {
            //            btnPoHeader.Visible = false;
            //            btnEsn_Excel.Visible = false;
            //            btnDown.Visible = false;
            //            btnDownPO.Visible = false;
            //            btnPoDetail.Visible = false;
            //            btnPoDetailTrk.Visible = false;
            //            gvPOQuery.DataSource = null;
            //            gvPOQuery.DataBind();
            //            btnUpload.Visible = false;
            //            btnChangeStatus.Visible = false;
            //            lblMsg.Text = "No record exists";
            //        }
            //    }
            //    else
            //    {
            //        CleanForm();
            //        lblMsg.Text = "Please select the search criteria";
            //    }

            //}
            //catch (Exception ex)
            //{
            //    lblMsg.Text = ex.Message;
            //}
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CleanForm();
        }
        //This procedure prepares the query to bind the child GridView
        private List<BasePurchaseOrderItem> ChildDataSource(int poID, string strSort)
        {
            PurchaseOrders purchaseOrders = Session["POS"] as PurchaseOrders;
            if (purchaseOrders != null && purchaseOrders.PurchaseOrderList.Count > 0)
            {
                BasePurchaseOrder purchaseOrder = purchaseOrders.FindPurchaseOrder(poID);

                return purchaseOrder.PurchaseOrderItems;
            }
            else
            {
                return null;
            }
        }

        private List<BasePurchaseOrderItem> ChildDataSourcebyPODID(int podID, string strSort)
        {
            PurchaseOrders purchaseOrders = (PurchaseOrders)Session["POS"];
            if (purchaseOrders != null && purchaseOrders.PurchaseOrderList.Count > 0)
            {
                BasePurchaseOrder purchaseOrder = purchaseOrders.FindPurchaseOrderbyPodID(podID);

                return purchaseOrder.PurchaseOrderItems;
            }
            else
            {
                return null;
            }
        }

        #region GridView1 Event Handlers



        //This event occurs for each row
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DateTime tempDate = (DataBinder.Eval(e.Row.DataItem, "Tracking.ShipToDate") == null ? DateTime.MinValue : Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Tracking.ShipToDate")));
                //if (tempDate.CompareTo(DateTime.MinValue) == 0)
                // {
                // e.Row.Cells[8].Text = string.Empty;
                // }

                ////Add delete confirmation message for Customer
                if (e.Row.RowIndex >= 0)
                {
                    ImageButton img = (ImageButton)e.Row.FindControl("imgDelPo");
                    img.Attributes.Add("onclick", "javascimgDelPoript:return " +
                    "confirm('Are you sure you want to delete this Customer " +
                    DataBinder.Eval(e.Row.DataItem, "PurchaseOrderID") + "')");

                    //ImageButton imgPO = (ImageButton)e.Row.FindControl("imgPO");
                    //imgPO.OnClientClick = "openDialogAndBlock('Fulfillment Detail', '" + imgPO.ClientID + "')";
                    //ImageButton imgHistory = (ImageButton)e.Row.FindControl("imgHistory");
                    //imgHistory.OnClientClick = "openHistoryDialogAndBlock('Fulfillment History', '" + imgHistory.ClientID + "')";
                    ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                    //imgEdit.OnClientClick = "openEditDialogAndBlock('Edit Fulfillment', '" + imgEdit.ClientID + "')";

                    LinkButton lnkStore = (LinkButton)e.Row.FindControl("lnkStore");
                    lnkStore.OnClientClick = "openStoreDialogAndBlock('Store Address', '" + lnkStore.ClientID + "')";

                    //ImageButton imgComments = (ImageButton)e.Row.FindControl("imgComments");
                    //imgComments.OnClientClick = "openCommentsDialogAndBlock('Fulfillment Order Comments', '" + imgComments.ClientID + "')";

                    //ImageButton imgPOA = (ImageButton)e.Row.FindControl("imgPOA");
                    //imgPOA.OnClientClick = "openPOADialogAndBlock('Fulfillment ESN Assign', '" + imgPOA.ClientID + "')";

                    //ImageButton imgShip = (ImageButton)e.Row.FindControl("imgShip");
                   // imgShip.OnClientClick = "openShipDialogAndBlock('Create Fulfillment Shipping', '" + imgShip.ClientID + "')";

                    HiddenField hdnStatus = (HiddenField)e.Row.FindControl("hdnStatus");
                    HiddenField hdOrderSent = (HiddenField)e.Row.FindControl("hdOrderSent");

                    bool IsReadOnly = Convert.ToBoolean(ViewState["IsReadOnly"]);
                    if(IsReadOnly)
                    {
                        ImageButton imgAdd = (ImageButton)e.Row.FindControl("imgAdd");
                        ImageButton imgShip = (ImageButton)e.Row.FindControl("imgShip");

                        img.Visible = false;
                        imgEdit.Visible = false;
                        imgAdd.Visible = false;
                        imgShip.Visible = false;
                    }
                    int statusID = 1, orderSent = 0;
                    if (hdnStatus != null && hdnStatus.Value != string.Empty)
                        statusID = Convert.ToInt32(hdnStatus.Value);

                    if (hdOrderSent != null)
                        int.TryParse(hdOrderSent.Value, out orderSent);


                    if (statusID == 4)
                    {
                        e.Row.BackColor = System.Drawing.Color.Red;
                    }
                    //if (Session["adm"] == null)
                    {


                        //ImageButton imgEditOrder = (ImageButton)e.Row.FindControl("imgEditOrder");
                        //ImageButton imgDelPo = (ImageButton)e.Row.FindControl("imgDelPo");
                        if (orderSent > 0)
                        {
                            imgEdit.Visible = false;
                            img.Visible = false;
                        }

                    }
                }
            }
        }

        protected void imgEdit_OnCommand(object sender, CommandEventArgs e)
        {
            int poID = Convert.ToInt32(e.CommandArgument);
            BindPODetails(poID);

            //ModalPopupExtender1.Show();
        }
        protected void imgPOHistory_OnCommand(object sender, CommandEventArgs e)
        {

            int poID = Convert.ToInt32(e.CommandArgument);
            ViewState["poid"] = poID;
            BindPOHistory(poID);
            RegisterStartupScript("jsUnblockDialog", "unblockHistoryDialog();");
            
            //PurchaseOrders purchaseOrders = Session["POS"] as PurchaseOrders;
            //List<BasePurchaseOrder> purchaseOrderList = purchaseOrders.PurchaseOrderList;

            //var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(poID) select item).ToList();
            //if (poInfoList.Count > 0)
            //{
            //    lblHPO.Text = poInfoList[0].PurchaseOrderNumber;
            //    lblHPoDate.Text = poInfoList[0].PurchaseOrderDate.ToShortDateString();
            //    lblHPOMDate.Text = poInfoList[0].ModifiedDate;
            //    lblHStatus.Text = poInfoList[0].PurchaseOrderStatus.ToString();
            //    ////lblCreatedby.Text = poInfoList[0].Shipping.ContactPhone;
            //    //mdlHistory.Show();
            //    RegisterStartupScript("jsUnblockDialog", "unblockHistoryDialog();");
            //}
        }

        private void BindPOHistory(int poID)
        {
            List<BasePurchaseOrder> purchaseOrderList = purchaseOrderOperation.GetPurchaseOrderHistory(poID);
            if (purchaseOrderList != null && purchaseOrderList.Count > 0)
            {
                lblPoNum.Text = purchaseOrderList[0].PurchaseOrderNumber;
                rptPO.DataSource = purchaseOrderList;
                rptPO.DataBind();
                lblHistory.Text = string.Empty;
            }
            else
            {
                rptPO.DataSource = null;
                rptPO.DataBind();
                lblPoNum.Text = string.Empty;
                lblHistory.Text = "No records found";
            }
        }
        
        protected void imgEditPO_OnCommand(object sender, CommandEventArgs e)
        {
            int poID = Convert.ToInt32(e.CommandArgument);
            Session["poid"] = poID;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('FulfillmentEdit.aspx')</script>", false);

            //lblMsg.Text = string.Empty;
            //lblEditPO.Text = string.Empty;
            //int poID = Convert.ToInt32(e.CommandArgument);
            //ViewState["poid"] = poID;
            //btnSubmit.Visible = false;
            //PurchaseOrders purchaseOrders = Session["POS"] as PurchaseOrders;
            //List<BasePurchaseOrder> purchaseOrderList = purchaseOrders.PurchaseOrderList;

            //var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(poID) select item).ToList();
            //if (poInfoList.Count > 0)
            //{
            //    chkShipRequired.Checked = poInfoList[0].IsShipmentRequired;
            //    lblPONo.Text = poInfoList[0].PurchaseOrderNumber;
            //    //lblPoDate.Text = poInfoList[0].PurchaseOrderDate.ToShortDateString();
            //    txtPODate.Text = poInfoList[0].PurchaseOrderDate.ToShortDateString();
            //    lblCustomer.Text = poInfoList[0].CustomerName;
            //    txtContactName.Text = poInfoList[0].Shipping.ContactName;
            //    txtContactPhone.Text = poInfoList[0].Shipping.ContactPhone;
            //    txtReqShipDate.Text = poInfoList[0].RequestedShipDate.ToString("MM/dd/yyyy");

            //    txtContainersPerPallet.Text = poInfoList[0].PurchaseOrderItems[0].ContainersPerPallet.ToString();
            //    txtItemsPerContainer.Text = poInfoList[0].PurchaseOrderItems[0].ItemsPerContainer.ToString();
            //    //txtShipBy.Text = poInfoList[0].Tracking.ShipToBy;
            //    if (Session["adm"] == null)
            //    {
            //        if (poInfoList[0].PurchaseOrderStatusID == 1)
            //        {
            //            btnSubmit.Visible = true;
            //        }
            //    }
            //    else
            //        btnSubmit.Visible = true;

            //    if (poInfoList[0].PurchaseOrderStatusID == 1 || poInfoList[0].PurchaseOrderStatusID == 2 || poInfoList[0].PurchaseOrderStatusID == 8 || poInfoList[0].PurchaseOrderStatusID == 10)
            //    {
            //        lblDShipvia.Visible = false;
            //        dpShipVia.Visible = true;
            //    }
            //    else
            //    {
            //        lblDShipvia.Visible = true;
            //        dpShipVia.Visible = false;

            //        //dpShipVia.SelectedIndex = 0;

            //    }
            //    string poShipBy = poInfoList[0].Tracking.ShipToBy;

            //    if (!string.IsNullOrEmpty(poShipBy))
            //    {

            //        List<ShipBy> shipViaList = (List<ShipBy>)Session["shipby"];
            //        var shipVia = (from item in shipViaList where item.ShipByCode.Equals(poShipBy) select item).ToList();
            //        //DataRow[] drs = null;
            //        //drs = dt.Select("[shipby] ='" + poShipBy + "'");

            //        if (shipVia != null && shipVia.Count > 0)
            //        {
            //            dpShipVia.SelectedValue = poShipBy;
            //            lblDShipvia.Text = poShipBy;
            //        }
            //        else
            //        {
            //            dpShipVia.SelectedIndex = 0;
            //            lblEditPO.Text = "Invalid Shipvia: " + poShipBy;
            //        }
            //    }
            //    else
            //        dpShipVia.SelectedIndex = 0;

                

            //    //lblAVSO.Text = poInfoList[0].AerovoiceSalesOrderNumber;
            //    //txtTrackingNo.Text = poInfoList[0].Tracking.ShipToTrackingNumber;
            //    // COMMENTED BECAUSE WE POPUP NOT TO VIEW COMMENTS
            //    //txtCommments.Text = poInfoList[0].Comments;
            //    txtCommments.Text = string.Empty;
            //    if (Session["adm"] == null)
            //    {
            //        //txtTrackingNo.ReadOnly = true;
            //        //lblPOStatus.Visible = true;
            //        ddlStatus.Visible = false;
            //        lblStatus.Text = poInfoList[0].PurchaseOrderStatus.ToString();
            //        ddlStatus.SelectedValue = poInfoList[0].PurchaseOrderStatusID.ToString();
            //    }
            //    else
            //    {
            //        ddlStatus.SelectedValue = poInfoList[0].PurchaseOrderStatusID.ToString();
            //        lblStatus.Visible = false;
            //    }
                
            //    //if ("1/1/0001" != poInfoList[0].Tracking.ShipToDate.ToShortDateString())
            //    //    lblShipDate.Text = poInfoList[0].Tracking.ShipToDate.ToShortDateString();

            //    lblStoreID.Text = poInfoList[0].StoreID;
            //    txtStreetAdd.Text = poInfoList[0].Shipping.ShipToAddress;
            //    txtAddress2.Text = poInfoList[0].Shipping.ShipToAddress2;

            //    txtCity.Text = poInfoList[0].Shipping.ShipToCity;
            //    string poState = poInfoList[0].Shipping.ShipToState.ToUpper();

            //    if (!string.IsNullOrEmpty(poState))
            //    {
            //        try
            //        {
            //            //poState = 
            //            DataTable dt = ViewState["state"] as DataTable;

            //            DataRow[] drs = null;
            //            drs = dt.Select("[statecode] ='" + poState + "'" );

            //            if (drs != null && drs.Length > 0)
            //            {
            //                dpState.SelectedValue = poState;
            //            }
            //            else
            //            {
            //                dpState.SelectedIndex = 0;
            //                lblEditPO.Text = "Invalid StateCode: " + poState;
            //            }

            //        }
            //        catch (Exception ex)
            //        {
            //            lblEditPO.Text = ex.Message;
            //        }
                    
            //    }
            //    //txtState.Text = poInfoList[0].Shipping.ShipToState;
            //    txtZip.Text = poInfoList[0].Shipping.ShipToZip;
            //    //lblStatus.Text = poInfoList[0].PurchaseOrderStatus.ToString();
            //    //lblSentESN.Text = poInfoList[0].SentESN;
            //    //lblSentASN.Text = poInfoList[0].SentASN;
            //    lblPONo.Text = poInfoList[0].PurchaseOrderNumber;




            //}
            //RegisterStartupScript("jsUnblockDialog", "unblockEditDialog();");
            //ModalPopupExtender2.Show();
        }

        private void BindPODetails(int poID)
        {
            //gvPODetail.DataSource = ChildDataSource(poID, string.Empty);
            //gvPODetail.DataBind();
        }
        //this event occurs for any operation on the row of the grid
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Check if Add button clicked
            string gvChild;
            int iIndex = 0, poID;
            gvChild = string.Empty;

            if (e.CommandName == "sel" && grid1SelectCommand == false)
            {
                grid1SelectCommand = true;
                GridView gv = (GridView)sender;
                Int32 rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                ImageButton img = (ImageButton)gv.Rows[rowIndex].Cells[0].Controls[0];
                GridView childgv = (GridView)gv.Rows[rowIndex].FindControl("GridView2");

                if (Session["adm"] == null)
                {
                    foreach (DataControlField dc in childgv.Columns)
                    {
                        if (dc.HeaderText.Equals("Delete")) //dc.HeaderText.Equals("Edit") ||
                        {
                            dc.Visible = false;
                        }
                    }
                }

                if (img.AlternateText == "-")
                {
                    img.AlternateText = "+";
                    childgv.Visible = false;
                    img.ImageUrl = "../images/plus.gif";
                }
                else
                {
                    poID = Convert.ToInt32(gv.DataKeys[rowIndex].Value);
                    ViewState["poid"] = poID;
                    ViewState["RowIndex"] = rowIndex;

                    childgv.DataSource = ChildDataSource(poID, string.Empty);
                    childgv.DataBind();
                    childgv.Visible = true;
                    img.AlternateText = "-";
                    img.ImageUrl = "../images/minus.gif";
                }
                gvPOQuery.RowCommand -= GridView1_RowCommand;
            }
        }

        
        protected void btnEditPO_Click(object sender, EventArgs e)
        {
            PurchaseOrders purchaseOrders = (PurchaseOrders)Session["POS"];
            int uploadDateRange = 1095;
            bool IsShipmentRequired = chkShipRequired.Checked;

            uploadDateRange = Convert.ToInt32(ConfigurationSettings.AppSettings["UploadAdminDateRange"]);
            string poDate = string.Empty;
            DateTime uploadDate = Convert.ToDateTime(txtPODate.Text.Trim());
            string errorMessage = string.Empty;
            int poID = 0;
            int userID = 0;
            int statusID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
                //ViewState["userid"] = userID;
            }
            if (ViewState["poid"] != null)
                poID = Convert.ToInt32(ViewState["poid"]);


            if (ViewState["adminrole"] != null)
            {
                poDate = txtPODate.Text.Trim();
                if (!string.IsNullOrEmpty(poDate))
                {
                    if (poDate.Trim().Length > 0)
                    {
                        DateTime dt;
                        if (DateTime.TryParse(poDate.Trim(), out dt))
                        {
                            double days = (uploadDate - dt).TotalDays;
                            if (days > uploadDateRange)
                                errorMessage = "PODate(" + dt.ToShortDateString() + ") can not be more than " + uploadDateRange + " days before";
                            else
                                if (days < 0)
                                    errorMessage = "PODate(" + dt.ToShortDateString() + ") can not be more than today date";
                                //else
                                //    if (days == 0)
                                //        uploadDate = Convert.ToDateTime("01/01/1900");

                            uploadDate = dt;

                        }
                        else
                            errorMessage = "PODate(" + poDate + ") does not have correct format(MM/DD/YYYY)";

                        

                    }
                   // else
                     //   uploadDate = Convert.ToDateTime("01/01/1900");
                }
                ////else
                //    uploadDate = Convert.ToDateTime("01/01/1900");
            }
            else
            {
                //uploadDate = Convert.ToDateTime("01/01/1900");
            }
            uploadDate = DateTime.SpecifyKind(uploadDate, DateTimeKind.Unspecified);

            if (poID > 0)
            {

                statusID = Convert.ToInt32(ddlStatus.SelectedValue);
                BasePurchaseOrder purchaseOrder = new BasePurchaseOrder(poID);
                purchaseOrder = purchaseOrders.FindPurchaseOrder(poID);
                purchaseOrder.PurchaseOrderDate = Convert.ToDateTime(txtPODate.Text.Trim());
                purchaseOrder.PurchaseOrderStatusID = statusID;
                purchaseOrder.PurchaseOrderStatus = (PurchaseOrderStatus)Convert.ToInt16(ddlStatus.SelectedValue);
                purchaseOrder.Shipping.ContactName = txtContactName.Text;
                purchaseOrder.Shipping.ShipToAttn = txtContactName.Text;
                purchaseOrder.Shipping.ContactPhone = txtContactPhone.Text;
                purchaseOrder.Shipping.ShipToAddress = txtStreetAdd.Text;
                purchaseOrder.Shipping.ShipToAddress2 = txtAddress2.Text;
                purchaseOrder.Shipping.ShipToCity = txtCity.Text;
                //purchaseOrder.Shipping.ShipToState = txtState.Text; ///dpState.SelectedValue;
                purchaseOrder.Shipping.ShipToState = dpState.SelectedValue;                                                   

                purchaseOrder.Shipping.ShipToZip = txtZip.Text;

                //default shipvia
                //if (statusID == 1 && statusID == 2 && statusID == 8)
                purchaseOrder.Tracking.ShipToBy = dpShipVia.SelectedValue; // txtShipBy.Text;// ;

                //purchaseOrder.Tracking.ShipToTrackingNumber = txtTrackingNo.Text;

                purchaseOrder.Tracking.PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber;
                purchaseOrder.Comments = txtCommments.Text.Trim();
                purchaseOrder.IsShipmentRequired = IsShipmentRequired;

                DateTime RequestedShipDate = DateTime.Now;
                DateTime.TryParse(txtReqShipDate.Text.Trim(), out RequestedShipDate);
                purchaseOrder.RequestedShipDate = RequestedShipDate;


                purchaseOrderOperation.UpdatePurchaseOrder(purchaseOrder, userID);
                gvPOQuery.EditIndex = -1;
                Session["POS"] = purchaseOrders;
                //gvPOQuery.DataSource = purchaseOrders.PurchaseOrderList;
                //gvPOQuery.DataBind();
                TriggerClientGridRefresh();
                lblMsg.Text = "Updated successfully";
                RegisterStartupScript("jsUnblockDialog", "closeEditDialog();");
            }
        }
        protected void btnAddShip_Click(object sender, EventArgs e)
        {
            FulfillmentShippingLine model = new FulfillmentShippingLine();
            List<FulfillmentShippingLineItem> listitems = new List<FulfillmentShippingLineItem>();
            FulfillmentShippingLineItem lineItem = null;
            string poDate = string.Empty;
            DateTime shipDate = Convert.ToDateTime(txtShippingDate.Text.Trim());
            string errorMessage = string.Empty, trackingNumber = string.Empty, comments = string.Empty;
            int poID = 0, shipById = 0; ;
            int userID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
                //ViewState["userid"] = userID;
            }
            if (ViewState["poid"] != null)
                poID = Convert.ToInt32(ViewState["poid"]);

            //if (ddlShipVia.SelectedIndex > 0)
            //shipById = Convert.ToInt32(ddlShipVia.SelectedValue);
            comments = txtShipComments.Text.Trim();
            trackingNumber = txtTrackingNumber.Text.Trim();
            
            shipDate = DateTime.SpecifyKind(shipDate , DateTimeKind.Unspecified);
            model.ShipDate = shipDate.ToShortDateString();
            model.Comments = comments;
            model.POID = poID;
            model.ShipById = shipById;
            model.TrackingNumber = trackingNumber;
            //foreach(RepeaterItem item in rptShipItems.Items)
            //{
            //    CheckBox chkBox = item.FindControl("chkSKU") as CheckBox;
            //    if (chkBox.Checked)
            //    {
            //        lineItem = new FulfillmentShippingLineItem();
            //        TextBox txtQty = item.FindControl("txtQty") as TextBox;
            //        HiddenField hdnPODID = item.FindControl("hdnPODID") as HiddenField;
            //        lineItem.PODID = Convert.ToInt32(hdnPODID.Value);
            //        lineItem.Quantity = Convert.ToInt32(txtQty.Text.Trim());
            //        listitems.Add(lineItem);

            //    }
            //}
            //if (listitems != null && listitems.Count > 0)
            {
                model.LineItems = listitems;
                if (poID > 0)
                {

                    //if(chk)
                    int returnResult = GenerateShipmentLabel(listitems);

                    // int returnResult = FulfillmentShippingLineItemOperation.CreatePurchaseOrderShippingLineItems(model);
                    if (returnResult == 1)
                    {
                        //BindShipping(poID);
                        //lblShipItem.Text = "Label generated successfully";
                        btnShip.Visible = false;
                    }

                    //RegisterStartupScript("jsUnblockDialog", "closeEditDialog();");
                }
            }
            //else
            //{ lblShipItem.Text = "Please select atleast one SKU"; }


        }
        private void GridDataBind()
        {
            if (Session["POS"] != null)
            {
                PurchaseOrders purchaseOrders = (PurchaseOrders)Session["POS"];

                gvPOQuery.DataSource = purchaseOrders.PurchaseOrderList;
                gvPOQuery.DataBind();
            }
        }
        protected void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            GridDataBind();
        }
        private void TriggerClientGridRefresh()
        {
            string script = "__doPostBack(\"" + btnRefreshGrid.ClientID + "\", \"\");";
            RegisterStartupScript("jsGridRefresh", script);
        }

        //This event occurs on click of the Delete button
        protected void imgDeletePO_OnCommand(object sender, CommandEventArgs e)
        {
            int poId = Convert.ToInt32(e.CommandArgument);
            int userID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
                //ViewState["userid"] = userID;
            }
            try
            {

                int index = 0;
                if (ViewState["RowIndex"] != null)
                    index = Convert.ToInt32(ViewState["RowIndex"]);
                purchaseOrderOperation.DeletePurchaseOrder(poId, userID);
                DataSet ds = (DataSet)Session["PO"];
                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.Rows[index];
                    if (dr != null)
                    {
                        dr.Delete();
                    }
                    dt = ds.Tables[1];

                    DataRow[] drs = dt.Select("POID = " + poId);
                    if (drs.Length > 0)
                    {
                        foreach (DataRow drd in drs)
                        {
                            drd.Delete();
                        }
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order deleted successfully');</script>", false);
                BindPO();
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Fullfilment deleted successfully');</script>");
                //gvPOQuery.DataSource = ds;
                //gvPOQuery.DataBind();
                //btnSearch_Click(sender, e);
            }
            catch { }
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string poId = gvPOQuery.DataKeys[e.RowIndex].Value.ToString();
            int userID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
                //ViewState["userid"] = userID;
            }
            try
            {

                purchaseOrderOperation.DeletePurchaseOrder(Convert.ToInt32(poId), userID);
                DataSet ds = (DataSet)Session["PO"];
                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.Rows[e.RowIndex];
                    if (dr != null)
                    {
                        dr.Delete();
                    }
                    dt = ds.Tables[1];

                    DataRow[] drs = dt.Select("POID = " + poId);
                    if (drs.Length > 0)
                    {
                        foreach (DataRow drd in drs)
                        {
                            drd.Delete();
                        }
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order deleted successfully');</script>", false);
                BindPO();
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order deleted successfully');</script>");
                //gvPOQuery.DataSource = ds;
                //gvPOQuery.DataBind();
                //btnSearch_Click(sender, e);
            }
            catch { }
        }

        //This event occurs after RowDeleting to catch any constraints while deleting
        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            //Check if there is any exception while deleting
            if (e.Exception != null)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");
                e.ExceptionHandled = true;
            }
        }

        //protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    GridView gvTemp = (GridView)sender;
        //    gvUniqueID = gvTemp.UniqueID;
        //    gvEditIndex = e.NewEditIndex;
        //    GridView1.EditIndex = gvEditIndex;

        //    GridView1.DataSource = ((PurchaseOrders)Session["POS"]).PurchaseOrderList;
        //    GridView1.DataBind();
        //}

        //protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    GridView1.EditIndex = -1;
        //    btnSearch_Click(sender, e);
        //}

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPOQuery.PageIndex = e.NewPageIndex;

            if (Session["POS"] != null)
            {
                PurchaseOrders pos = (PurchaseOrders)Session["POS"];
                BindGrid1(pos);
            }
        }
        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }
        public List<BasePurchaseOrder> Sort<TKey>(List<BasePurchaseOrder> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<BasePurchaseOrder>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<BasePurchaseOrder>();
            }
        }
        protected void gvPOQuery_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["POS"] != null)
            {
                PurchaseOrders pos = (PurchaseOrders)Session["POS"];

                if (pos != null && pos.PurchaseOrderList.Count > 0)
                {
                    var list = pos.PurchaseOrderList;
                    if (Sortdir == "ASC")
                    {
                        list = Sort<BasePurchaseOrder>(list, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        list = Sort<BasePurchaseOrder>(list, SortExp, SortDirection.Descending);
                    }
                    Session["POS"] = list;
                    gvPOQuery.DataSource = list;
                    gvPOQuery.DataBind();
                }
            }
        }

        #endregion

        #region GridView2 Event Handlers
        protected void gvPODetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPODetail.PageIndex = e.NewPageIndex;
            if (Session["poitems"] != null)
            {
                List<BasePurchaseOrderItem> purchaseOrderItemList = (List<BasePurchaseOrderItem>)Session["poitems"];
                gvPODetail.DataSource = purchaseOrderItemList;
                gvPODetail.DataBind();
            }
        }

        protected void btnEditPOD_Click(object sender, EventArgs e)
        {
            string esn, msl, fmupc, mdn, msid;
            esn = msl = fmupc = mdn = msid = string.Empty;
            int rowIndex = Convert.ToInt32(ViewState["RowIndex"]);
            int podStatusID = 1;
            int userID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
                //ViewState["userid"] = userID;
            }
            GridView gvTemp = (GridView)gvPOQuery.Rows[rowIndex].FindControl("GridView2");
            try
            {

                //Get the values stored in the viewsate & text boxes
                int podID = Convert.ToInt32(ViewState["podid"]);
                podStatusID = Convert.ToInt32(ddlPODStatus.SelectedValue);
                string returnMessage = string.Empty;
                if (Session["adm"] != null)
                {
                    esn = txtESNs.Text.Trim();
                    msl = txtMSLNo.Text.Trim();
                    fmupc = string.Empty;//txtFMUPCs.Text.Trim();
                    mdn = txtMDN.Text.Trim();
                    msid = txtMSID.Text.Trim();

                    purchaseOrderOperation.PurchaseOrderUpdateDetail(podID, esn, msl, msid, mdn, null, fmupc, podStatusID, userID, null, null, null, null, null, out returnMessage);
                }
                else
                {
                    mdn = txtMDN.Text.Trim();
                    msid = txtMSID.Text.Trim();

                    purchaseOrderOperation.PurchaseOrderUpdateDetail(podID, null, null, msid, mdn, null, null, podStatusID, userID, null, null, null, null, null, out returnMessage);
                }
                if (!string.IsNullOrEmpty(returnMessage))
                {
                    lblViewPO.Text = returnMessage;
                    return;
                }
                gvTemp.EditIndex = -1;
                List<BasePurchaseOrderItem> purchaseOrderList = ChildDataSourcebyPODID(podID, null);
                if (purchaseOrderList != null && purchaseOrderList.Count > 0)
                {
                    foreach (BasePurchaseOrderItem pitem in purchaseOrderList)
                    {
                        if (pitem.PodID == podID)
                        {
                            pitem.ESN = esn;
                            pitem.MslNumber = msl;
                            pitem.FmUPC = fmupc;
                            pitem.MdnNumber = mdn;
                            pitem.MsID = msid;
                            pitem.StatusID = podStatusID;
                        }
                    }
                }
                //if (Session["POS"] != null)
                //{
                //    PurchaseOrders purchaseOrders = (PurchaseOrders)Session["POS"];
                //    purchaseOrders.PurchaseOrderList = purchaseOrderList;
                //}

                gvTemp.DataSource = purchaseOrderList;
                gvTemp.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order Detail is updated successfully');</script>", false);
                gvPOQuery.EditIndex = -1;
                //}
            }
            catch { }
        }

        protected void imgEditPOD_Click(object sender, CommandEventArgs e)
        {
            int poID = 0;
            int podID = Convert.ToInt32(e.CommandArgument);
            ViewState["podid"] = podID;
            //ImageButton imgButton = (ImageButton)sender;

            //GridViewRow row = (GridViewRow)imgButton.NamingContainer;
            //ViewState["RowIndex"] = row.RowIndex;
            poID = Convert.ToInt32(ViewState["poid"]);

            PurchaseOrders purchaseOrders = Session["POS"] as PurchaseOrders;
            List<BasePurchaseOrder> purchaseOrderList = purchaseOrders.PurchaseOrderList;

            var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(poID) select item).ToList();
            //List<BasePurchaseOrderItem> purchaseOrderItemList = poInfoList[0].PurchaseOrderItems;
            if (poInfoList != null && poInfoList.Count > 0)
            {
                var podInfoList = (from items in poInfoList[0].PurchaseOrderItems where items.PodID.Equals(podID) select items).ToList();
                if (podInfoList != null && podInfoList.Count > 0)
                {
                    lblItemCode.Text = podInfoList[0].ItemCode;
                    lblQty.Text = podInfoList[0].Quantity.ToString();
                    txtESNs.Text = podInfoList[0].ESN;
                    //txtFMUPCs.Text = podInfoList[0].FmUPC;
                    txtMDN.Text = podInfoList[0].MdnNumber;
                    txtMSID.Text = podInfoList[0].MsID;
                    txtMSLNo.Text = podInfoList[0].MslNumber;
                    lblPassCode.Text = podInfoList[0].PassCode;
                    lblUPC.Text = podInfoList[0].UPC;
                    //lblPhoneType.Text = podInfoList[0].PhoneCategory.ToString();
                    if (Session["adm"] == null)
                    {
                        lblPODStatus.Text = podInfoList[0].PODStatus.ToString();
                        ddlPODStatus.SelectedValue = podInfoList[0].StatusID.ToString();
                        txtESNs.ReadOnly = true;
                        txtESNs.CssClass = "copy10greyb";
                        //txtFMUPCs.ReadOnly = true;
                        txtMSLNo.ReadOnly = true;
                        lblPODStatus.Visible = true;
                        ddlPODStatus.Visible = false;
                    }
                    else
                    {
                        lblPODStatus.Visible = false;
                        ddlPODStatus.Visible = true;
                        ddlPODStatus.SelectedValue = podInfoList[0].StatusID.ToString();
                    }

                }
            }
            RegisterStartupScript("jsUnblockDialog", "unblockEditPODDialog();");
            //ModalPopupExtender3.Show();
        }
        protected void imgDeletePOD_Click(object sender, CommandEventArgs e)
        {
            int podID = Convert.ToInt32(e.CommandArgument);
            //int rowIndex = Convert.ToInt32(ViewState["RowIndex"]);
            //GridView gvTemp = (GridView)gvPOQuery.Rows[rowIndex].FindControl("GridView2");
            int userID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
            }
            try
            {
                purchaseOrderOperation.DeletePurchaseOrderDetail(podID, userID);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order deleted successfully');</script>", false);
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Fullfilment deleted successfully');</script>");

                BindPO();
            }
            catch { }
        }
        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvNewPageIndex = e.NewPageIndex;
            gvPOQuery.DataBind();
        }
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //GridView gvTemp = (GridView)sender;
            //gvUniqueID = gvTemp.UniqueID;
            //gvEditIndex = e.NewEditIndex;
            //gvTemp.EditIndex = gvEditIndex;
            //gvTemp.DataSource = ChildDataSourcebyPODID(Convert.ToInt32(gvTemp.DataKeys[gvEditIndex].Value), string.Empty);
            //gvTemp.DataBind();
        }
        protected void GridView2_CancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //GridView gvTemp = (GridView)sender;
            //gvUniqueID = gvTemp.UniqueID;
            //gvEditIndex = -1;
            ////ds = (DataSet)Session["PO"];
            //GridView1.DataSource = ((PurchaseOrders)Session["POS"]).PurchaseOrderList;

            //GridView1.DataBind();
        }
        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //string esn, msl, fmupc, mdn, msid;
            //esn = msl = fmupc = mdn = msid = string.Empty;
            //try
            //{
            //    GridView gvTemp = (GridView)sender;
            //    gvUniqueID = gvTemp.UniqueID;

            //    //Get the values stored in the text boxes
            //    int podID = Convert.ToInt32(gvTemp.DataKeys[e.RowIndex].Value);

            //    if (Session["adm"] != null)
            //    {
            //        esn = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtEsn")).Text.Trim();
            //        msl = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMslNumber")).Text.Trim();
            //        fmupc = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtFMUPC")).Text.Trim();
            //        mdn = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMdn")).Text.Trim();
            //        msid = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMsid")).Text.Trim();

            //        PurchaseOrder.PurchaseOrderUpdateDetail(podID, esn, msl, msid, mdn, null, fmupc);
            //    }
            //    else
            //    {
            //        mdn = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMdn")).Text.Trim();
            //        msid = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMsid")).Text.Trim();

            //        PurchaseOrder.PurchaseOrderUpdateDetail(podID, null, null, msid, mdn, null, null);
            //    }

            //    gvTemp.EditIndex = -1;
            //    List<BasePurchaseOrderItem> purchaseOrderList = ChildDataSourcebyPODID(podID, null);
            //    if (purchaseOrderList != null && purchaseOrderList.Count > 0)
            //    {
            //        foreach (BasePurchaseOrderItem pitem in purchaseOrderList)
            //        {
            //            if (pitem.PodID == podID)
            //            {
            //                pitem.ESN = esn;
            //                pitem.MslNumber = msl;
            //                pitem.FmUPC = fmupc;
            //                pitem.MdnNumber = mdn;
            //                pitem.MsID = msid;
            //            }
            //        }
            //    }

            //    gvTemp.DataSource = purchaseOrderList;
            //    gvTemp.DataBind();
            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order Detail is updated successfully');</script>");
            //    GridView1.EditIndex = -1;
            //    //}
            //}
            //catch { }
        }
        protected void GridView2_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            //Check if there is any exception while deleting
            if (e.Exception != null)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");
                e.ExceptionHandled = true;
            }
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblViewPO.Text = string.Empty;
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
                    List<BasePurchaseOrderItem> purchaseOrderItemList = purchaseOrderOperation.GetPurchaseOrderItemList(po_id);
                    Session["poitems"] = purchaseOrderItemList;
                    gvPODetail.DataSource = purchaseOrderItemList;
                    gvPODetail.DataBind();

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order deleted successfully');</script>", false);
                    lblViewPO.Text = "Purchase Order deleted successfully";
                }
                else
                {
                    if (returnValue == -1)
                        lblViewPO.Text = "Purchase Order item can not be deleted it must be in panding or processed state";
                    else
                        lblViewPO.Text = "Purchase Order item can not be deleted there must be atleast one item";
                }
                //BindPO();

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
        protected void img2EditPOD_Click(object sender, CommandEventArgs e)
        {
            int poID = 0;
            int podID = Convert.ToInt32(e.CommandArgument);
            ViewState["podid"] = podID;
            //ImageButton imgButton = (ImageButton)sender;

            //GridViewRow row = (GridViewRow)imgButton.NamingContainer;
            //ViewState["RowIndex"] = row.RowIndex;
            poID = Convert.ToInt32(ViewState["poid"]);

            PurchaseOrders purchaseOrders = Session["POS"] as PurchaseOrders;
            List<BasePurchaseOrder> purchaseOrderList = purchaseOrders.PurchaseOrderList;

            var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(poID) select item).ToList();
            //List<BasePurchaseOrderItem> purchaseOrderItemList = poInfoList[0].PurchaseOrderItems;
            if (poInfoList != null && poInfoList.Count > 0)
            {
                var podInfoList = (from items in poInfoList[0].PurchaseOrderItems where items.PodID.Equals(podID) select items).ToList();
                if (podInfoList != null && podInfoList.Count > 0)
                {


                    lblItemCode.Text = podInfoList[0].ItemCode;
                    lblQty.Text = podInfoList[0].Quantity.ToString();
                    txtESNs.Text = podInfoList[0].ESN;
                    //txtFMUPCs.Text = podInfoList[0].FmUPC;
                    txtMDN.Text = podInfoList[0].MdnNumber;
                    txtMSID.Text = podInfoList[0].MsID;
                    txtMSLNo.Text = podInfoList[0].MslNumber;
                    lblPassCode.Text = podInfoList[0].PassCode;
                    lblUPC.Text = podInfoList[0].UPC;
                    //lblPhoneType.Text = podInfoList[0].PhoneCategory.ToString();
                    if (Session["adm"] == null)
                    {
                        lblPODStatus.Text = podInfoList[0].PODStatus.ToString();
                        ddlPODStatus.SelectedValue = podInfoList[0].StatusID.ToString();
                        txtESNs.ReadOnly = true;
                        txtESNs.CssClass = "copy10greyb";
                        //txtFMUPCs.ReadOnly = true;
                        txtMSLNo.ReadOnly = true;
                        lblPODStatus.Visible = true;
                        ddlPODStatus.Visible = false;
                    }
                    else
                    {
                        lblPODStatus.Visible = false;
                        ddlPODStatus.Visible = true;
                        ddlPODStatus.SelectedValue = podInfoList[0].StatusID.ToString();
                    }

                }
            }
            RegisterStartupScript("jsUnblockDialog", "unblockEditPODDialog();");
            //ModalPopupExtender3.Show();
        }
        protected void gvPODetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex >= 0)
                {

                    
                    //imgEditPOD.OnClientClick = "openEditPODDialogAndBlock('Edit Fulfillment Detail', '" + imgEditPOD.ClientID + "')";

                    //if (Session["adm"] == null)
                    {
                       // ImageButton imgDelPoD = (ImageButton)e.Row.FindControl("imgDelPoD");
                       // HiddenField hdnStatus = (HiddenField)e.Row.FindControl("hdnStatus");
                        //if (e.Row.RowState == DataControlRowState.Edit)
                        //{
                        //    TextBox txtESN = (TextBox)e.Row.FindControl("txtEsn");
                        //    TextBox txtMslNumber = (TextBox)e.Row.FindControl("txtMslNumber");
                        //    TextBox txtMsid = (TextBox)e.Row.FindControl("txtMsid");


                        //    TextBox txtLTEICCID = (TextBox)e.Row.FindControl("txtLTEICCID");
                        //    TextBox txtLTEIMSI = (TextBox)e.Row.FindControl("txtLTEIMSI");
                        //    TextBox txtAkey = (TextBox)e.Row.FindControl("txtAkey");
                        //    TextBox txtOTKSL = (TextBox)e.Row.FindControl("txtOTKSL");
                        //    TextBox txtSim = (TextBox)e.Row.FindControl("txtSim");
                        //    if (txtLTEICCID != null && txtLTEIMSI != null && txtAkey != null && txtOTKSL != null && txtSim != null)
                        //    {

                        //        txtESN.Visible = false;
                        //        txtMslNumber.Visible = false;
                        //        txtMsid.Visible = false;

                        //        txtLTEICCID.Visible = false;
                        //        txtLTEIMSI.Visible = false;
                        //        txtAkey.Visible = false;
                        //        txtOTKSL.Visible = false;
                        //        txtSim.Visible = false;
                        //    }
                        //}
                        //if (hdnStatus != null && hdnStatus.Value != string.Empty)
                        //{
                        //    int statusID = Convert.ToInt32(hdnStatus.Value);
                        //    //ImageButton imgEditOrder = (ImageButton)e.Row.FindControl("imgEditOrder");
                        //    //ImageButton imgDelPo = (ImageButton)e.Row.FindControl("imgDelPo");
                        //    if (statusID > 1)
                        //    {
                        //        LinkButton obj = (LinkButton)e.Row.Cells[14].Controls[0];
                        //        if (obj != null)
                        //        {
                        //            obj.Enabled = false;
                        //            obj.Visible = false;
                        //        }
                        //        //CommandField EditPOD = e.Row.FindControl("EditPOD")
                        //        imgDelPoD.Visible = false;

                        //        e.Row.Cells[14].Enabled = false;
                        //        //imgDelPoD.Visible = false;
                        //    }
                        //}
                    }
                }
            }
        }
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex >= 0)
                {
                    if (Session["adm"] == null && e.Row.Controls.Count > 0)
                    {
                        TextBox txESN = e.Row.FindControl("txtEsn") as TextBox;
                        if (txESN != null)
                        {
                            txESN.Visible = false;

                            TextBox txMsl = e.Row.FindControl("txtMslNumber") as TextBox;
                            if (txMsl != null)
                                txMsl.Visible = false;
                            TextBox txfmupc = e.Row.FindControl("txtFMUPC") as TextBox;
                            if (txfmupc != null)
                                txfmupc.Visible = false;
                        }


                    }
                    ImageButton imgEditPOD = (ImageButton)e.Row.FindControl("imgEditPOD");
                    imgEditPOD.OnClientClick = "openEditPODDialogAndBlock('Edit Fulfillment Detail', '" + imgEditPOD.ClientID + "')";

                }
            }
        }

        protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvSortExpr = e.SortExpression;
            gvPOQuery.DataBind();
        }
        #endregion

        protected void btnDownPO_Click(object sender, EventArgs e)
        {
            DownloadPOsNew();

        }

        protected void btnDown_Click(object sender, EventArgs e)
        {
            DownloadPO();
        }

        protected void btnEsn_Excel_Click(object sender, EventArgs e)
        {
            DownloadESNData_Excel();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            // CleanForm();
            if (trUpload.Visible)
            {
                trUpload.Visible = false;
            }
            else
            {
                //trUpload.Visible = true;
            }
        }


        protected void btn_UpdClick(object sender, EventArgs e)
        {
            SV.Framework.Inventory.clsInventoryDB clsInventoryDB = SV.Framework.Inventory.clsInventoryDB.CreateInstance<SV.Framework.Inventory.clsInventoryDB>();

            bool esnExists = false;
            bool esnIncorrectFormat = false;
            bool uploadEsn = false;
            if (flnUpload.PostedFile.FileName.Trim().Length == 0)
            {
                lblMsg.Text = "Select file to upload";
            }
            else
            {
                if (flnUpload.PostedFile.ContentLength > 0)
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath(downLoadPath) + flnUpload.FileName);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    flnUpload.PostedFile.SaveAs(file.FullName);

                    System.Text.StringBuilder sbEsns = new StringBuilder();
                    StringWriter stringWriter = new StringWriter();
                    using (XmlTextWriter xw = new XmlTextWriter(stringWriter))
                    {
                        try
                        {
                            xw.WriteStartElement("purchaseorder");
                            using (StreamReader sr = new StreamReader(file.FullName))
                            {
                                string line;
                                string esn = string.Empty;
                                long esnTest = 0;
                               SV.Framework.Models.Inventory.clsInventory inventory = new  SV.Framework.Models.Inventory.clsInventory();
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] arr = line.Split(',');
                                    try
                                    {
                                        if (Convert.ToInt32(arr[0]) > 0)
                                        {
                                            esn = arr[4];
                                            if (!string.IsNullOrEmpty(esn))
                                            {
                                                if (!long.TryParse(esn, out esnTest))
                                                {
                                                    sbEsns.Append(esn + " ");
                                                    esnIncorrectFormat = true;
                                                }
                                                else
                                                {
                                                    esnExists = clsInventoryDB.ValidateEsnExists(esn);
                                                    if (esnExists)
                                                    {
                                                        sbEsns.Append(esn + " ");
                                                        esnExists = false;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        uploadEsn = true;
                                                        xw.WriteStartElement("item");
                                                        xw.WriteElementString("ponumber", arr[0]);
                                                        xw.WriteElementString("podId", arr[1]);
                                                        xw.WriteElementString("itemcode", arr[2]);
                                                        xw.WriteElementString("esn", esn);
                                                        sbEsns.Append(esn + ",");
                                                        xw.WriteEndElement();
                                                    }
                                                    esn = string.Empty;
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception exception)
                                    {
                                        //lblMsg.Text = exception.Message;
                                    }
                                }
                            }
                            xw.Flush();
                        }
                        catch (Exception ex)
                        {
                            lblMsg.Text = ex.Message;
                        }
                    }

                    if (uploadEsn && esnExists == false && esnIncorrectFormat == false)
                    {
                        XmlDocument xdoc = new XmlDocument();
                        xdoc.LoadXml(stringWriter.ToString());
                        //UploadESNs(xdoc.OuterXml);
                        trUpload.Visible = false;
                        lblMsg.Text = "Uploaded successfully";
                        xdoc = null;
                    }
                    else if (esnIncorrectFormat)
                    {
                        lblMsg.Text = "Incorrect format " + sbEsns.ToString();
                    }
                    else if (esnExists)
                    {
                        lblMsg.Text = "Check your ESN List, some of the ESN(s) are already assigned " + sbEsns.ToString();
                    }
                }
            }
        }

        private void GenerateSummary(PurchaseOrders pos)
        {
            Hashtable hshItems = new Hashtable();
            int countItems = 0;
            int pendingCount, shippedCount, processedCount, total;
            pendingCount = shippedCount = processedCount = total = 0;
            foreach (BasePurchaseOrder po in pos.PurchaseOrderList)
            {
                if (po.PurchaseOrderStatus == PurchaseOrderStatus.Pending)
                {
                    pendingCount++;
                }
                else if (po.PurchaseOrderStatus == PurchaseOrderStatus.Shipped)
                {
                    shippedCount++;
                }
                else if (po.PurchaseOrderStatus == PurchaseOrderStatus.Processed)
                {
                    processedCount++;
                }

                foreach (BasePurchaseOrderItem pitems in po.PurchaseOrderItems)
                {
                    if (!hshItems.ContainsKey(pitems.ItemCode))
                    {
                        hshItems.Add(pitems.ItemCode, 1);
                    }
                    else
                    {
                        countItems = Convert.ToInt32(hshItems[pitems.ItemCode].ToString());
                        countItems = countItems + 1;
                        hshItems[pitems.ItemCode] = countItems;
                    }
                }
            }

            lblTotalPOs.Text = pos.PurchaseOrderList.Count.ToString();
            lblPendingPOs.Text = pendingCount.ToString();
            lblShippedPOs.Text = shippedCount.ToString();
            lblProcessedPOs.Text = processedCount.ToString();

        }

        //This procedure returns the Sort Direction
        private string GetSortDirection()
        {
            switch (gvSortDir)
            {
                case "ASC":
                    gvSortDir = "DESC";
                    break;

                case "DESC":
                    gvSortDir = "ASC";
                    break;
            }
            return gvSortDir;
        }
        //private void UploadESNs(string POXml)
        //{
        //    int userID = 0;
        //    UserInfo userInfo = Session["userInfo"] as UserInfo;
        //    if (userInfo != null)
        //    {
        //        userID = userInfo.UserGUID;
        //    }
        //    PurchaseOrder.UpLoadESN(POXml, userID);
        //}

        private string GetSelectedIndexs()
        {
            StringBuilder sbIndex = new StringBuilder();
            if (chkDownload.Checked)
            {
                foreach (GridViewRow row in gvPOQuery.Rows)
                {
                    if (((CheckBox)gvPOQuery.Rows[row.RowIndex].Cells[1].Controls[1]).Checked)
                    {
                        sbIndex.Append("#" + (gvPOQuery.PageSize * (gvPOQuery.PageIndex) + row.RowIndex) + "#");
                    }
                }
            }

            return sbIndex.ToString();
        }

        private void ExportPO()
        {
            try
            {
                Response.Clear();
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                System.IO.StringWriter stringWrite = new System.IO.StringWriter();

                System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

                //DataSet ds = new DataSet();
                //clscon.Return_DS(ds, Convert.ToString(ViewState["str"]));
                //GridView dg = new GridView();
                //dg.DataSource = ds.Tables[0];

                //dg.DataBind();

                gvPOQuery.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
                Response.End();
            }

            catch (Exception ex)
            {

                Response.Write(ex.Message);

            }

        }
        //FulfillmentNumber,ESN,TrackingNumber,ShipViaCode

        private void DownloadTrackingAssignment()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //StringBuilder stringBuilder = new StringBuilder();
            bool bSelected = true;
            if (bSelected == false)
            {
                ClientScript.RegisterStartupScript(GetType(), "Purchase Order", "<SCRIPT LANGUAGE='javascript'>alert('Please select Purchase Order(s)');</script>");
            }
            else
            {
                string path = Server.MapPath(downLoadPath).ToString();
                string fileName = Session.SessionID + ".csv";
                bool found = false;
                System.IO.FileInfo file = null;
               // file = new System.IO.FileInfo(path + fileName);
               // if (file.Exists)
               // {
               //     file.Delete();
              //  }
                PurchaseOrders pos = null;
                pos = GetSelectedPurchaseOrdes(0);

                if (writer == "csv")
                {
                    //string posItems = stringBuilder.ToString();
                    bool addRecord = true;
                    
                    int lineCounter = 0;
                    sb.Append("FulfillmentNumber,ESN,TrackingNumber,ShipViaCode,SalesOrder#\r\n");
                    lineCounter = 0;
                    foreach (BasePurchaseOrder po in pos.PurchaseOrderList)
                    {

                        if (addRecord)
                        {
                            found = true;
                            foreach (BasePurchaseOrderItem poitem in po.PurchaseOrderItems)
                            {
                                sb.Append(po.PurchaseOrderNumber + "," + poitem.ESN + "," + poitem.TrackingNumber + "," + "," + po.AerovoiceSalesOrderNumber + "," + "\r\n");

                            }
                        }

                        lineCounter++;
                    }

                    if (found)
                    {
                        //try
                        //{
                        //    using (StreamWriter sw = new StreamWriter(file.FullName))
                        //    {
                        //        sw.WriteLine(sb.ToString());
                        //        sw.Flush();
                        //        sw.Close();
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    lblMsg.Text = ex.Message;
                        //}
                    }
                }

                if (found)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(sb.ToString());
                    Response.Flush();
                    Response.End();
                    //Response.Clear();
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    //// Response.AddHeader("Content-Length", file.Length.ToString());
                    //Response.ContentType = "application/octet-stream";
                    //Response.WriteFile(file.FullName);
                    //Response.End();
                }
            }
        }

        private void DownloadPO()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            StringBuilder stringBuilder = new StringBuilder();
            bool bSelected = true;

            if (bSelected == false)
            {

                ClientScript.RegisterStartupScript(GetType(), "Purchase Order", "<SCRIPT LANGUAGE='javascript'>alert('Please select Purchase Order(s)');</script>");

            }
            else
            {
                string path = Server.MapPath(downLoadPath).ToString();
                string fileName = Session.SessionID + ".csv";
                bool found = false;
                System.IO.FileInfo file = null;
                file = new System.IO.FileInfo(path + fileName);
                if (file.Exists)
                {
                    file.Delete();
                }
                PurchaseOrders pos = null;
                //if (Session["POD"] != null)
                //    pos = (PurchaseOrders)Session["POD"];
                //else
                //    pos = GetPOwithDetail();
                pos = GetSelectedPurchaseOrdes(0);

                if (writer == "xml")
                {
                    XmlTextWriter xw = new XmlTextWriter(path + fileName, System.Text.Encoding.ASCII);
                    try
                    {
                        xw.WriteStartElement("purchaseorder");
                        foreach (BasePurchaseOrder po in pos.PurchaseOrderList)
                        //foreach (GridViewRow row in gvPOQuery.Rows)
                        {
                            found = true;
                            //GridView gridViewDetail = ((GridView)row.FindControl("GridView2"));
                            foreach (BasePurchaseOrderItem poitem in po.PurchaseOrderItems)
                            //foreach (GridViewRow row1 in gridViewDetail.Rows)
                            {
                                xw.WriteStartElement("item");
                                xw.WriteElementString("ponumber", po.PurchaseOrderNumber);

                                xw.WriteElementString("podId", poitem.PodID.ToString());
                                xw.WriteElementString("itemcode", poitem.ItemCode);

                                xw.WriteElementString("esn", poitem.ESN);
                               // xw.WriteElementString("fmupc", poitem.FmUPC);
                                //xw.WriteElementString("ShippDate", po.Tracking.ShipToDate.ToShortDateString().Replace("1/1/0001", ""));
                                xw.WriteElementString("iccid", poitem.LTEICCID);
                              //  xw.WriteElementString("lteimsi", poitem.LTEIMSI);
                              //  xw.WriteElementString("otksl", poitem.Otksal);
                              //  xw.WriteElementString("akey", poitem.Akey);
                                xw.WriteEndElement();
                            }

                        }
                        xw.Flush();
                        xw.Close();
                    }
                    catch (Exception ex)
                    {
                        xw.Close();
                        lblMsg.Text = ex.Message;
                    }
                }
                else if (writer == "csv")
                {
                    string posItems = stringBuilder.ToString();
                    bool addRecord = true;
                    
                    //PurchaseOrders pos = (PurchaseOrders)Session["POS"];
                    int lineCounter = 0;
                    //string gridIndexes = GetSelectedIndexs();

                    sb.Append("PoNum,PODID,ItemCode,Esn,ICCID\n");

                    lineCounter = 0;
                    foreach (BasePurchaseOrder po in pos.PurchaseOrderList)
                    {

                        if (addRecord)
                        {
                            found = true;
                            foreach (BasePurchaseOrderItem poitem in po.PurchaseOrderItems)
                            {
                                sb.Append(po.PurchaseOrderNumber + "," + poitem.PodID.ToString() + "," +
                                    poitem.ItemCode + "," + poitem.ESN 
                                   
                                    + "," + poitem.LTEICCID + "\n");

                            }
                        }

                        lineCounter++;
                    }

                    //if (found)
                    //{
                    //    try
                    //    {
                    //        using (StreamWriter sw = new StreamWriter(file.FullName))
                    //        {
                    //            sw.WriteLine(sb.ToString());
                    //            sw.Flush();
                    //            sw.Close();
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        lblMsg.Text = ex.Message;
                    //    }
                    //}
                }

                if (found)
                {
                    //Response.Clear();
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    //// Response.AddHeader("Content-Length", file.Length.ToString());
                    //Response.ContentType = "application/octet-stream";
                    //Response.WriteFile(file.FullName);
                    //Response.End();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(sb.ToString());
                    Response.Flush();
                    Response.End();
                } 
            }
        }
        private void DownloadPOEsn()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            StringBuilder stringBuilder = new StringBuilder();
            bool bSelected = true;

            if (bSelected == false)
            {

                ClientScript.RegisterStartupScript(GetType(), "Purchase Order", "<SCRIPT LANGUAGE='javascript'>alert('Please select Purchase Order(s)');</script>");

            }
            else
            {
                string path = Server.MapPath(downLoadPath).ToString();
                string fileName = Session.SessionID + ".csv";
                bool found = false;
                System.IO.FileInfo file = null;
                file = new System.IO.FileInfo(path + fileName);
                if (file.Exists)
                {
                    file.Delete();
                }
                PurchaseOrders pos = null;
                //if (Session["POD"] != null)
                //    pos = (PurchaseOrders)Session["POD"];
                //else
                //    pos = GetPOwithDetail();
                pos = GetSelectedPurchaseOrdes(0);

                if (writer == "xml")
                {
                    XmlTextWriter xw = new XmlTextWriter(path + fileName, System.Text.Encoding.ASCII);
                    try
                    {
                        xw.WriteStartElement("purchaseorder");
                        foreach (BasePurchaseOrder po in pos.PurchaseOrderList)
                        //foreach (GridViewRow row in gvPOQuery.Rows)
                        {
                            found = true;
                            //GridView gridViewDetail = ((GridView)row.FindControl("GridView2"));
                            foreach (BasePurchaseOrderItem poitem in po.PurchaseOrderItems)
                            //foreach (GridViewRow row1 in gridViewDetail.Rows)
                            {
                                xw.WriteStartElement("item");
                                xw.WriteElementString("ponumber", po.PurchaseOrderNumber);

                                xw.WriteElementString("podId", poitem.PodID.ToString());
                                xw.WriteElementString("itemcode", poitem.ItemCode);

                                xw.WriteElementString("esn", poitem.ESN);
                                //xw.WriteElementString("fmupc", poitem.FmUPC);
                                //xw.WriteElementString("ShippDate", po.Tracking.ShipToDate.ToShortDateString().Replace("1/1/0001", ""));
                                xw.WriteElementString("iccid", poitem.LTEICCID);
                              //  xw.WriteElementString("lteimsi", poitem.LTEIMSI);
                               // xw.WriteElementString("otksl", poitem.Otksal);
                               // xw.WriteElementString("akey", poitem.Akey);
                                xw.WriteEndElement();
                            }

                        }
                        xw.Flush();
                        xw.Close();
                    }
                    catch (Exception ex)
                    {
                        xw.Close();
                        lblMsg.Text = ex.Message;
                    }
                }
                else if (writer == "csv")
                {
                    string posItems = stringBuilder.ToString();
                    bool addRecord = true;
                   
                    //PurchaseOrders pos = (PurchaseOrders)Session["POS"];
                    int lineCounter = 0;
                    //string gridIndexes = GetSelectedIndexs();

                    sb.Append("FulfillmentNumber,CustomerAccountNumber,SKU,ESN,ICCID\n");

                    lineCounter = 0;
                    foreach (BasePurchaseOrder po in pos.PurchaseOrderList)
                    {

                        if (addRecord)
                        {
                            found = true;
                            foreach (BasePurchaseOrderItem poitem in po.PurchaseOrderItems)
                            {
                                sb.Append(po.PurchaseOrderNumber + "," + po.CustomerAccountNumber + "," +
                                    poitem.ItemCode + "," + poitem.ESN 
                                    //+ "," + po.Tracking.ShipToDate.ToShortDateString().Replace("1/1/0001", "") 
                                    + "," + poitem.LTEICCID + "\n");

                            }
                        }

                        lineCounter++;
                    }

                    //if (found)
                    //{
                    //    try
                    //    {
                    //        using (StreamWriter sw = new StreamWriter(file.FullName))
                    //        {
                    //            sw.WriteLine(sb.ToString());
                    //            sw.Flush();
                    //            sw.Close();
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        lblMsg.Text = ex.Message;
                    //    }
                    //}
                }

                if (found)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(sb.ToString());
                    Response.Flush();
                    Response.End();
                    //Response.Clear();
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    //// Response.AddHeader("Content-Length", file.Length.ToString());
                    //Response.ContentType = "application/octet-stream";
                    //Response.WriteFile(file.FullName);
                    //Response.End();
                }
            }
        }
        

        private void DownloadPOsNew()
        {

            bool bSelected = true;

            if (bSelected == false)
            {

                ClientScript.RegisterStartupScript(GetType(), "Purchase Order", "<SCRIPT LANGUAGE='javascript'>alert('Please select Purchase Order(s)');</script>");

            }
            else
            {
                string path = Server.MapPath(downLoadPath).ToString();
                string fileName = Session.SessionID + ".xls";

                bool found = false;
                System.IO.FileInfo file = null;
                file = new System.IO.FileInfo(path + fileName);
                if (file.Exists)
                {
                    file.Delete();
                }

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                this.EnableViewState = false;




                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                PurchaseOrders pos = null;
                //if (Session["POD"] != null)
                //    pos = (PurchaseOrders)Session["POD"];
                //else
                //    pos = GetPOwithDetail();
                pos = GetSelectedPurchaseOrdes(0);

                int lineCounter = 0;
                bool addRecord = true;

                //string gridIndexes = GetSelectedIndexs();

                //sb.Append("H,Customer,AerovoiceOrderNumber,PurchaseOrderNumber,CustomerNumber,PurchaseOrderDate,storeid,ContactName,ShipAddress,ShipCity,ShipState,ShipZip,Shipby,TrackingNumber\n");
                //sb.Append("D,Qty,ItemCode,ESN,MdnNumber,MslNumber,PassCode,UPC,PhoneCategory,PurchaseOrderNumber,Lineno,Fm-UPC\n");

                using (StringWriter sw = new StringWriter())
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a table to contain the grid
                    Table table = new Table();

                    //  Gridline to box the cells
                    table.GridLines = System.Web.UI.WebControls.GridLines.Both;

                    //string[] columns = { "H", "Customer", "AerovoiceOrderNumber", "PurchaseOrderNumber", "CustomerNumber", "PurchaseOrderDate", "storeid", "ContactName", "ShipAddress", "ShipCity", "ShipState", "ShipZip", "Shipby", "TrackingNumber" };

                    //string[] dColumns = { "D", "Qty", "ItemCode", "ESN", "MdnNumber", "MslNumber", "PassCode", "UPC", "PhoneCategory", "PurchaseOrderNumber", "Lineno", "Fm - UPC", "", "" };

                    string[] columns = { "H", "Customer", "AVOrderNumber", "FulfillmentNumber", "OrderDate", "StoreID", "ShipAddress", "ShipCity", "ShipState", "ShipZip", "ShipMethod", "ShipDate", "TrackingNumber", "ReturnLabel", "ReturnShipVia" };

                    string[] dColumns = { "D", "Line No", "SKU", "ESN", "ICC_ID", "TrackingNumber", "BatchNumber", "", "", "", "", "", "", "", "" };

                    TableRow tRow = new TableRow();
                    TableCell tCell;
                    foreach (string name in columns)
                    {
                        tCell = new TableCell();
                        tCell.Text = name;
                        tRow.Cells.Add(tCell);
                    }
                    table.Rows.Add(tRow);

                    tRow = new TableRow();
                    foreach (string dName in dColumns)
                    {
                        tCell = new TableCell();
                        tCell.Text = dName;
                        tRow.Cells.Add(tCell);
                    }

                    table.Rows.Add(tRow);

                    lineCounter = 0;
                    string address1 = string.Empty;
                    string address2 = string.Empty;
                    addRecord = true;
                    foreach (BasePurchaseOrder po in pos.PurchaseOrderList)
                    {

                        if (addRecord)
                        {
                            try
                            {
                                tRow = new TableRow();
                                tCell = new TableCell();
                                tCell.Text = "H";
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.CustomerName;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.AerovoiceSalesOrderNumber;
                                tRow.Cells.Add(tCell);


                                tCell = new TableCell();
                                tCell.Text = po.PurchaseOrderNumber;
                                tRow.Cells.Add(tCell);

                                //tCell = new TableCell();
                                //tCell.Text = po.CustomerNumber;
                                //tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.PurchaseOrderDate.ToShortDateString();
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = (string.IsNullOrEmpty(po.StoreID) ? string.Empty : po.StoreID);
                                tRow.Cells.Add(tCell);

                                //tCell = new TableCell();
                                //tCell.Text = po.Shipping.ContactName;
                                //tRow.Cells.Add(tCell);

                                address1 = (string.IsNullOrEmpty(po.Shipping.ShipToAddress) ? string.Empty : po.Shipping.ShipToAddress.Replace(',', ' '));
                                address2 = (string.IsNullOrEmpty(po.Shipping.ShipToAddress2) ? string.Empty : po.Shipping.ShipToAddress2.Replace(',', ' '));

                                tCell = new TableCell();

                                tCell.Text = address1 + " " + address2;

                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.Shipping.ShipToCity;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.Shipping.ShipToState;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.Shipping.ShipToZip;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.Tracking.ShipToBy;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.Tracking.ShipToDate.ToShortDateString() == "1/1/0001" ? "" : po.Tracking.ShipToDate.ToShortDateString();
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.Tracking.ShipToTrackingNumber;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.ReturnLabel;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.ReturnShipVia;
                                tRow.Cells.Add(tCell);

                                table.Rows.Add(tRow);
                                // Purchase Order Details
                                foreach (BasePurchaseOrderItem pItem in po.PurchaseOrderItems)
                                {


                                    tRow = new TableRow();
                                    tCell = new TableCell();
                                    tCell.Text = "D";
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = pItem.LineNo.ToString();
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = pItem.ItemCode;
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = pItem.ESN.ToString();
                                    tRow.Cells.Add(tCell);

                                    

                                    //tCell = new TableCell();
                                    //tCell.Text = pItem.UPC.ToString();
                                    //tRow.Cells.Add(tCell);

                                    //tCell = new TableCell();
                                    //tCell.Text = pItem.PhoneCategory.ToString();
                                    //tRow.Cells.Add(tCell);

                                    //tCell = new TableCell();
                                    //tCell.Text = po.PurchaseOrderNumber;
                                    //tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = pItem.LTEICCID;
                                    tRow.Cells.Add(tCell);

                                    

                                    tCell = new TableCell();
                                    tCell.Text = pItem.TrackingNumber;
                                    tRow.Cells.Add(tCell);
                                    
                                    tCell = new TableCell();
                                    tCell.Text = pItem.BatchNumber;
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = string.Empty;
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = string.Empty;
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = string.Empty;
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = string.Empty;
                                    tRow.Cells.Add(tCell);


                                    tCell = new TableCell();
                                    tCell.Text = string.Empty;
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = string.Empty;
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = string.Empty;
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = string.Empty;
                                    tRow.Cells.Add(tCell);

                                    table.Rows.Add(tRow);


                                }


                            }
                            catch (Exception ex)
                            {
                                System.Threading.Thread.Sleep(5000);
                                Response.Write(ex.Message);
                            }
                        }
                        lineCounter++;
                    }

                    //  Htmlwriter into the table
                    table.RenderControl(htw);

                    //  Htmlwriter into the response
                    // HttpContext.Current.Response.Write(sw.ToString());
                    // HttpContext.Current.Response.End();

                    //Response.Clear();
                    //Response.Buffer = true;
                    //Response.ContentType = "application/vnd.ms-excel";
                    //Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);

                    Response.Charset = "";
                   // Response.ContentType = "application/text";
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();

                }



            }
        }
        private void DownloadPOs()
        {
            bool bSelected = true;

            if (bSelected == false)
            {

                ClientScript.RegisterStartupScript(GetType(), "Purchase Order", "<SCRIPT LANGUAGE='javascript'>alert('Please select Purchase Order(s)');</script>");

            }
            else
            {
                string path = Server.MapPath(downLoadPath).ToString();
                string fileName = Session.SessionID + ".csv";
                bool found = false;
                System.IO.FileInfo file = null;
                file = new System.IO.FileInfo(path + fileName);
                if (file.Exists)
                {
                    file.Delete();
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                PurchaseOrders pos = null;
                if (Session["POD"] != null)
                    pos = (PurchaseOrders)Session["POD"];
                else
                    pos = GetPOwithDetail();
                int lineCounter = 0;
                bool addRecord = true;

                //string gridIndexes = GetSelectedIndexs();

                sb.Append("H,Customer,SalesOrderNumber,PurchaseOrderNumber,CustomerNumber,PurchaseOrderDate,storeid,ContactName,ShipAddress,ShipCity,ShipState,ShipZip,ShipVia,TrackingNumber,ReturnShipVia,ReturnLabel\n");
                sb.Append("D,Qty,ItemCode,ESN,ICCID,UPC,PurchaseOrderNumber,Lineno\n");

                lineCounter = 0;
                addRecord = true;
                foreach (BasePurchaseOrder po in pos.PurchaseOrderList)
                {


                    if (addRecord)
                    {
                        found = true;
                        sb.Append("H," + po.CustomerName + "," + po.AerovoiceSalesOrderNumber + "," + po.PurchaseOrderNumber + "," + po.CustomerNumber + "," + po.PurchaseOrderDate.ToShortDateString() + "," +
                            (string.IsNullOrEmpty(po.StoreID) ? string.Empty : po.StoreID) + "," + po.Shipping.ContactName + "," +
                            (string.IsNullOrEmpty(po.Shipping.ShipToAddress) ? string.Empty : po.Shipping.ShipToAddress.Replace(',', ' ')) + " " +
                            (string.IsNullOrEmpty(po.Shipping.ShipToAddress2) ? string.Empty : po.Shipping.ShipToAddress2.Replace(',', ' ')) + "," +
                            po.Shipping.ShipToCity + "," + po.Shipping.ShipToState + "," + po.Shipping.ShipToZip + "," +
                            po.Tracking.ShipToBy + "," + po.Tracking.ShipToTrackingNumber + "," + po.ReturnShipVia + "," + po.ReturnLabel +"\n");

                        foreach (BasePurchaseOrderItem pItem in po.PurchaseOrderItems)
                        {
                            sb.Append("D," + pItem.Quantity.ToString() + "," + pItem.ItemCode + "," + pItem.ESN + "," + pItem.LTEICCID + "," +  pItem.UPC + "," + po.PurchaseOrderNumber + "," + pItem.LineNo.ToString()  + "\n");
                        }

                    }

                    lineCounter++;
                }

                //try
                //{
                //    using (StreamWriter sw = new StreamWriter(file.FullName))
                //    {
                //        sw.WriteLine(sb.ToString());
                //        sw.Flush();
                //        sw.Close();
                //    }
                //}
                //catch (Exception ex)
                //{
                //    lblMsg.Text = ex.Message;
                //}

                if (found)
                {
                    //Response.Clear();
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    //// Response.AddHeader("Content-Length", file.Length.ToString());
                    //Response.ContentType = "application/octet-stream";
                    //Response.WriteFile(file.FullName);
                    //Response.End();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename="+fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(sb.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }
        protected void btnChangeStatus_Click(object sender, EventArgs e)
        {
            dpStatus.SelectedIndex = 0;
            if (trStatus.Visible == false)
            {
                trStatus.Visible = true;
            }
            else
            {
                trStatus.Visible = false;
            }
        }


        private bool UpdatePOsStatus()
        {
            bool returnFlag = true;
            PurchaseOrders purchaseOrders = new PurchaseOrders();

            int poID = 0;
            List<PurchaseOrderChangeStatus> poList = new List<PurchaseOrderChangeStatus>();
            int statusID = 1;
            int userID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;

            }
            if (chkDownload.Checked)
            {

                purchaseOrders = Session["POS"] as PurchaseOrders;
                foreach (GridViewRow row in gvPOQuery.Rows)
                {
                    if (((CheckBox)row.FindControl("chk")).Checked)
                    {
                        poID = Convert.ToInt32(gvPOQuery.DataKeys[row.RowIndex].Value);
                        BasePurchaseOrder purchaseOrder = new BasePurchaseOrder(poID);
                        purchaseOrder = purchaseOrders.FindPurchaseOrder(poID);
                        statusID = purchaseOrder.PurchaseOrderStatusID == 1 ? 8 : purchaseOrder.PurchaseOrderStatusID;
                        purchaseOrder.PurchaseOrderStatusID = statusID;
                        purchaseOrder.PurchaseOrderStatus = (PurchaseOrderStatus)statusID;
                    
                        //purchaseOrder.PurchaseOrderStatusID = Convert.ToInt32(dpStatus.SelectedValue);
                        //purchaseOrder.PurchaseOrderStatus = (PurchaseOrderStatus)Convert.ToInt16(dpStatus.SelectedValue);

                        PurchaseOrderChangeStatus po = new PurchaseOrderChangeStatus();
                        Label lblPoNum = (Label)row.FindControl("lblPoNum");
                        HiddenField hdnCANo = (HiddenField)row.FindControl("hdnCANo");
                        po.CompanyAccountNumber = hdnCANo.Value;
                        po.PurchaseOrderNumber = lblPoNum.Text;
                        //po.POID = Convert.ToInt32(gvPOQuery.DataKeys[row.RowIndex].Value);
                        poList.Add(po);
                        //bSelected = true;
                        //stringBuilder.Append(gvPOQuery.DataKeys[row.RowIndex].Value.ToString() + ",");
                    }
                }

                if (poList == null || poList.Count == 0)
                {
                    lblMsg.Text = "Please select Purchase Order(s)";
                    returnFlag = false;
                }

            }
            else
            {
                if (Session["POS"] != null)
                {
                    purchaseOrders = Session["POS"] as PurchaseOrders;

                    foreach (BasePurchaseOrder po in purchaseOrders.PurchaseOrderList)
                    {
                        poID = Convert.ToInt32(po.PurchaseOrderID);
                        BasePurchaseOrder purchaseOrder = new BasePurchaseOrder(poID);
                        purchaseOrder = purchaseOrders.FindPurchaseOrder(poID);
                        statusID = purchaseOrder.PurchaseOrderStatusID == 1 ? 8 : purchaseOrder.PurchaseOrderStatusID;
                        purchaseOrder.PurchaseOrderStatusID = statusID;
                        purchaseOrder.PurchaseOrderStatus = (PurchaseOrderStatus)statusID;


                        PurchaseOrderChangeStatus poc = new PurchaseOrderChangeStatus();
                        //Label lblPoNum = (Label)row.FindControl("lblPoNum");
                        //HiddenField hdnCANo = (HiddenField)row.FindControl("hdnCANo");
                        poc.CompanyAccountNumber = po.CustomerAccountNumber;
                        poc.PurchaseOrderNumber = po.PurchaseOrderNumber;
                        //po.POID = Convert.ToInt32(gvPOQuery.DataKeys[row.RowIndex].Value);
                        poList.Add(poc);

                    }
                }
                else
                {
                    lblMsg.Text = "Session expire!";
                    returnFlag = false;
                }
            }
            if (poList != null && poList.Count > 0)
            {
                int returnValue = purchaseOrderOperation.SetPurchaseOrderChangeStatusDB(poList, "In process", userID, 1);

                if (returnValue == 0)
                {
                    gvPOQuery.EditIndex = -1;
                    Session["POS"] = purchaseOrders;
                    TriggerClientGridRefresh();

                    lblMsg.Text = "Updated successfully";
                }
            }
            //else
            //{
            //    lblMsg.Text = "Session expire!";
            //}
            return returnFlag;
        }
        protected void btn_UpdStatusClick(object sender, EventArgs e)
        {
            PurchaseOrders purchaseOrders = (PurchaseOrders)Session["POS"];

            int poID = 0;
           
            int userID = 0;
           avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;

            }
            List<PurchaseOrderChangeStatus> poList = new List<PurchaseOrderChangeStatus>();
            if (trStatus.Visible == true && dpStatus.SelectedIndex > 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                bool bSelected = false;
                foreach (GridViewRow row in gvPOQuery.Rows)
                {
                    if (((CheckBox)row.FindControl("chk")).Checked)
                    {
                        poID = Convert.ToInt32(gvPOQuery.DataKeys[row.RowIndex].Value);
                        BasePurchaseOrder purchaseOrder = new BasePurchaseOrder(poID);
                        purchaseOrder = purchaseOrders.FindPurchaseOrder(poID);
                        purchaseOrder.PurchaseOrderStatusID = Convert.ToInt32(dpStatus.SelectedValue);
                        purchaseOrder.PurchaseOrderStatus = (PurchaseOrderStatus)Convert.ToInt16(dpStatus.SelectedValue);
                
                        PurchaseOrderChangeStatus po = new PurchaseOrderChangeStatus();
                        Label lblPoNum = (Label)row.FindControl("lblPoNum");
                        HiddenField hdnCANo = (HiddenField)row.FindControl("hdnCANo");
                        po.CompanyAccountNumber = hdnCANo.Value;
                        po.PurchaseOrderNumber = lblPoNum.Text;
                        //po.POID = Convert.ToInt32(gvPOQuery.DataKeys[row.RowIndex].Value);
                        poList.Add(po);
                        bSelected = true;
                        //stringBuilder.Append(gvPOQuery.DataKeys[row.RowIndex].Value.ToString() + ",");
                    }
                }

                if (bSelected == false)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Purchase Order", "<SCRIPT LANGUAGE='javascript'>alert('Please select Purchase Order(s)');</script>");
                }
                else
                {
                    int returnValue = purchaseOrderOperation.SetPurchaseOrderChangeStatusDB(poList, dpStatus.SelectedItem.Text, userID, 0);

                    if (returnValue == 0)
                    {
                        gvPOQuery.EditIndex = -1;
                        Session["POS"] = purchaseOrders;
                        TriggerClientGridRefresh();
                
                        lblMsg.Text = "Updated successfully";
                    }
                    
                    trStatus.Visible = false;
                }
            }
        }


        protected void lnkInventorySummary_Click(object sender, EventArgs e)
        {
            if (this.pnlSummary.Visible)
            {
                lnkSumary.Text = "Show Inventory Summary";
                pnlSummary.Visible = false;
            }
            else
            {
                lnkSumary.Text = "Hide Inventory Summary";
                pnlSummary.Visible = true;
            }
        }

        protected void lnkSumary_Click(object sender, EventArgs e)
        {
            if (this.pnlSummary.Visible)
            {
                lnkSumary.Text = "Show Purchase Order Summary";
                pnlSummary.Visible = false;
            }
            else
            {
                lnkSumary.Text = "Hide Purchase Order Summary";
                pnlSummary.Visible = true;
            }
        }

        protected void btnSendESN_Click(object sender, EventArgs e)
        {
            bool bSelected = false;
            foreach (GridViewRow row in gvPOQuery.Rows)
            {
                if (((CheckBox)row.FindControl("chk")).Checked)
                {
                    bSelected = true;
                    string poNum = gvPOQuery.DataKeys[row.RowIndex].Value.ToString();

                    if (!string.IsNullOrEmpty(poNum))
                    {
                        if (Session["POS"] != null)
                        {
                            PurchaseOrders purchaseOrders = Session["POS"] as PurchaseOrders;
                            BasePurchaseOrder po = purchaseOrders.FindPurchaseOrderbyNumber(poNum);
                            if (po != null)
                            {
                                SendEsn(po);
                            }
                            else
                            {
                                lblMsg.Text = "Could not find purchase order, please try again";
                            }
                        }
                    }

                }
            }

        }


        private void SendEsn(BasePurchaseOrder po)
        {
            //Qualution.ProblemResultResponse response = new Qualution.ProblemResultResponse();
            //Qualution.OrderDetail orderDetail = null;
            //Qualution.PurchaseOrder purchaseOrder = new Qualution.PurchaseOrder();
            //Qualution.AerovoiceService aerovoiceService = new Qualution.AerovoiceService();
            //List<Qualution.OrderDetail> orderDetails = null;

            //purchaseOrder.poNumber = Convert.ToInt32(po.PurchaseOrderNumber);
            //purchaseOrder.poNumberSpecified = true;
            //orderDetails = new List<Qualution.OrderDetail>();
            //if (po.PurchaseOrderItems != null && po.PurchaseOrderItems.Count > 0)
            //{
            //    foreach (BasePurchaseOrderItem esnItem in po.PurchaseOrderItems)
            //    {
            //        if (!string.IsNullOrEmpty(esnItem.ESN))
            //        {
            //            orderDetail = new Qualution.OrderDetail();
            //            orderDetail.esn = esnItem.ESN;

            //            orderDetail.itemId = esnItem.ItemCode;// "KYO-414-0";
            //            orderDetail.mslCode = esnItem.MslNumber;
            //            orderDetail.lineNo = esnItem.LineNo;

            //            orderDetail.lineNoSpecified = true;
            //            orderDetails.Add(orderDetail);
            //        }
            //    }

            //    purchaseOrder.orderDetail = orderDetails.ToArray();
            //    if (purchaseOrder.orderDetail != null && purchaseOrder.orderDetail.Length > 0)
            //    {
            //        response = aerovoiceService.setEsns(purchaseOrder);

            //        if (response.resultCode != null && response.resultCode == 0)
            //        {
            //            foreach (BasePurchaseOrderItem esnItem in po.PurchaseOrderItems)
            //            {
            //                PurchaseOrder.SetESNServiceLogging(esnItem.PodID, purchaseOrder.poNumber.ToString(), esnItem.ESN, esnItem.MslNumber);
            //            }
            //        }
            //        string msg = String.Format("CallESNService: Purchase Order#: {0} is processed with response status of {1} ({2})", po.PurchaseOrderNumber, response.resultCode, response.problemDesc);
            //        if (response.resultCode != null && response.resultCode > 0)
            //        {
            //            lblMsg.Text = msg;
            //        }

            //        //EventLog.WriteEntry("AeroSerivce", msg, EventLogEntryType.Information);
            //    }
            //}
            //else
            //{
            //    lblMsg.Text = String.Format("ESN: Purchase Order#: {0} has no ESN Item assigned)", po.PurchaseOrderNumber);

            //    //EventLog.WriteEntry("AeroSerivce", String.Format("ESN: Purchase Order#: {0} has no ESN Item assigned)", pItem.PurchaseOrderNumber), EventLogEntryType.Warning);
            //}


        }
        protected void btnDownloadData_Click(object sender, EventArgs e)
        {
            //ModalPopupExtender4.Show();
        }
        private void DownloadTrackingNumberInfo()
        {

            bool bSelected = true;

            if (bSelected == false)
            {

                ClientScript.RegisterStartupScript(GetType(), "Purchase Order", "<SCRIPT LANGUAGE='javascript'>alert('Please select Purchase Order(s)');</script>");

            }
            else
            {
                string path = Server.MapPath(downLoadPath).ToString();
                string fileName = Session.SessionID + ".xls";

                bool found = false;
                System.IO.FileInfo file = null;
                file = new System.IO.FileInfo(path + fileName);
                if (file.Exists)
                {
                    file.Delete();
                }

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                this.EnableViewState = false;




                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                PurchaseOrders pos = null;
                pos = GetSelectedPurchaseOrdes(0);
                //if (Session["POD"] != null)
                //    pos = (PurchaseOrders)Session["POD"];
                //else
                //    pos = GetPOwithDetail();

                int lineCounter = 0;
                bool addRecord = true;

                //string gridIndexes = GetSelectedIndexs();

                //sb.Append("H,Customer,AerovoiceOrderNumber,PurchaseOrderNumber,CustomerNumber,PurchaseOrderDate,storeid,ContactName,ShipAddress,ShipCity,ShipState,ShipZip,Shipby,TrackingNumber\n");
                //sb.Append("D,Qty,ItemCode,ESN,MdnNumber,MslNumber,PassCode,UPC,PhoneCategory,PurchaseOrderNumber,Lineno,Fm-UPC\n");

                using (StringWriter sw = new StringWriter())
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a table to contain the grid
                    Table table = new Table();

                    //  Gridline to box the cells
                    table.GridLines = System.Web.UI.WebControls.GridLines.Both;

                    string[] columns = { "FulfillmentNumber", "FulfillmentDate", "ContactName", "ShipBy", "SalesOrder#", "TrackingNumber", "ShipAddress", "ShipCity", "ShipState", "ShipZip","ReturnShipVia" , "ReturnLabel" };

                    //string[] columns = { "H", "Customer", "AerovoiceOrderNumber", "PurchaseOrderNumber", "CustomerNumber", "PurchaseOrderDate", "storeid", "ContactName", "ShipAddress", "ShipCity", "ShipState", "ShipZip", "Shipby", "TrackingNumber" };

                    //string[] dColumns = { "D", "Qty", "ItemCode", "ESN", "MdnNumber", "MslNumber", "PassCode", "UPC", "PhoneCategory", "PurchaseOrderNumber", "Lineno", "Fm - UPC", "", "" };

                    TableRow tRow = new TableRow();
                    TableCell tCell;
                    foreach (string name in columns)
                    {
                        tCell = new TableCell();
                        tCell.Text = name;
                        tRow.Cells.Add(tCell);
                    }
                    table.Rows.Add(tRow);

                    //tRow = new TableRow();
                    //foreach (string dName in dColumns)
                    //{
                    //    tCell = new TableCell();
                    //    tCell.Text = dName;
                    //    tRow.Cells.Add(tCell);
                    //}

                    //table.Rows.Add(tRow);

                    lineCounter = 0;
                    string address1 = string.Empty;
                    string address2 = string.Empty;


                    List<BasePurchaseOrder> poList = pos.PurchaseOrderList;

                    var poInfoList = (from item in poList where item.PurchaseOrderStatusID.Equals(3) select item).ToList();
                    var poInfoList2 = (from item in poList where item.PurchaseOrderStatusID.Equals(11) select item).ToList();
                    //var poInfoList2 = (from item in poList where (PurchaseOrderStatusID => PurchaseOrderStatusID.).ToList();
                    if (poInfoList2 != null)
                    {
                        if (poInfoList != null)
                            poInfoList.AddRange(poInfoList2);
                        else
                            poInfoList = poInfoList2;
                    }

                    //Where(s => s.In("x", "y", "z"))

                    foreach (BasePurchaseOrder po in poInfoList)
                    {
                        if (addRecord)
                        {
                            try
                            {
                                tRow = new TableRow();
                                //tCell = new TableCell();
                                //tCell.Text = "H";
                                //tRow.Cells.Add(tCell);



                                tCell = new TableCell();
                                tCell.Text = po.PurchaseOrderNumber;
                                tRow.Cells.Add(tCell);


                                tCell = new TableCell();
                                tCell.Text = po.PurchaseOrderDate.ToShortDateString();
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.Shipping.ContactName;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.Tracking.ShipToBy;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.AerovoiceSalesOrderNumber;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.Tracking.ShipToTrackingNumber;
                                tRow.Cells.Add(tCell);





                                address1 = (string.IsNullOrEmpty(po.Shipping.ShipToAddress) ? string.Empty : po.Shipping.ShipToAddress.Replace(',', ' '));
                                address2 = (string.IsNullOrEmpty(po.Shipping.ShipToAddress2) ? string.Empty : po.Shipping.ShipToAddress2.Replace(',', ' '));

                                tCell = new TableCell();
                                tCell.Text = address1 + " " + address2;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.Shipping.ShipToCity;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.Shipping.ShipToState;
                                tRow.Cells.Add(tCell);


                                tCell = new TableCell();
                                tCell.Text = po.Shipping.ShipToZip;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.ReturnShipVia;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.ReturnLabel;
                                tRow.Cells.Add(tCell);


                                table.Rows.Add(tRow);
                                // Purchase Order Details
                                //foreach (BasePurchaseOrderItem pItem in po.PurchaseOrderItems)
                                //{


                                //    tRow = new TableRow();
                                //    tCell = new TableCell();
                                //    tCell.Text = "D";
                                //    tRow.Cells.Add(tCell);

                                //    tCell = new TableCell();
                                //    tCell.Text = pItem.Quantity.ToString();
                                //    tRow.Cells.Add(tCell);

                                //    tCell = new TableCell();
                                //    tCell.Text = pItem.ItemCode;
                                //    tRow.Cells.Add(tCell);

                                //    tCell = new TableCell();
                                //    tCell.Text = pItem.ESN.ToString();
                                //    tRow.Cells.Add(tCell);

                                //    tCell = new TableCell();
                                //    tCell.Text = pItem.MdnNumber;
                                //    tRow.Cells.Add(tCell);

                                //    tCell = new TableCell();
                                //    tCell.Text = pItem.MslNumber;
                                //    tRow.Cells.Add(tCell);

                                //    tCell = new TableCell();
                                //    tCell.Text = pItem.PassCode;
                                //    tRow.Cells.Add(tCell);

                                //    tCell = new TableCell();
                                //    tCell.Text = pItem.UPC.ToString();
                                //    tRow.Cells.Add(tCell);

                                //    tCell = new TableCell();
                                //    tCell.Text = pItem.PhoneCategory.ToString();
                                //    tRow.Cells.Add(tCell);

                                //    tCell = new TableCell();
                                //    tCell.Text = po.PurchaseOrderNumber;
                                //    tRow.Cells.Add(tCell);

                                //    tCell = new TableCell();
                                //    tCell.Text = pItem.LineNo.ToString();
                                //    tRow.Cells.Add(tCell);

                                //    tCell = new TableCell();
                                //    tCell.Text = pItem.FmUPC;
                                //    tRow.Cells.Add(tCell);

                                //    tCell = new TableCell();
                                //    tCell.Text = string.Empty;
                                //    tRow.Cells.Add(tCell);

                                //    tCell = new TableCell();
                                //    tCell.Text = string.Empty;
                                //    tRow.Cells.Add(tCell);

                                //    table.Rows.Add(tRow);


                                //}


                            }
                            catch (Exception ex)
                            {
                                System.Threading.Thread.Sleep(5000);
                                Response.Write(ex.Message);
                            }
                        }
                        lineCounter++;
                    }

                    //  Htmlwriter into the table
                    table.RenderControl(htw);

                    //  Htmlwriter into the response
                  //  HttpContext.Current.Response.Write(sw.ToString());
                   // HttpContext.Current.Response.End();
                    Response.Charset = "";
                   // Response.ContentType = "application/text";
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();

                }



            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (chkRecieved.Checked)
            {
                if (!UpdatePOsStatus())
                    return;
            }
            if (dpDownloadDataList.SelectedIndex == 1)
            {
                DownloadPOsNew();
            }
            else
                if (dpDownloadDataList.SelectedIndex == 2)
                {
                    DownloadPO();
                }
                else
                    if (dpDownloadDataList.SelectedIndex == 3)
                    {
                        DownloadPoDetailsTracking();
                    }
                    else
                        if (dpDownloadDataList.SelectedIndex == 4)
                        {
                            DownloadESNData_Excel();
                        }
                        else
                            if (dpDownloadDataList.SelectedIndex == 5)
                            {
                                DownloadPoHeader();
                            }
                            else
                                if (dpDownloadDataList.SelectedIndex == 6)
                                {
                                    DownloadTrackingNumberInfo();
                                }
                                else
                                    if (dpDownloadDataList.SelectedIndex == 7)
                                    {
                                        DownloadPOEsn();
                                    }
                                    else
                                        if (dpDownloadDataList.SelectedIndex == 8)
                                        {
                                            DownloadTrackingAssignment();
                                        }
            return;
        }

        protected void btnPoDetail_Click(object sender, EventArgs e)
        {
            //DownloadPoDetails();

        }

        private void DownloadPoDetails()
        {
            bool bSelected = true;

            if (bSelected == false)
            {
                ClientScript.RegisterStartupScript(GetType(), "Purchase Order", "<SCRIPT LANGUAGE='javascript'>alert('Please select Purchase Order(s)');</script>");
            }
            else
            {
                string path = Server.MapPath(downLoadPath).ToString();
                string fileName = Session.SessionID + ".csv";
                bool found = false;
                System.IO.FileInfo file = null;
                file = new System.IO.FileInfo(path + fileName);
                if (file.Exists)
                {
                    file.Delete();
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                PurchaseOrders pos = (PurchaseOrders)Session["POS"];
                int lineCounter = 0;
                bool addRecord = true;

                //string gridIndexes = GetSelectedIndexs();

                sb.Append("PurchaseOrderNumber,LineNo,ItemCode,MslNumber,ESN,TrackingNumber,ShipVia,ShippingDate,ReturnLabel,ReturnShipVia\n");

                lineCounter = 0;
                foreach (BasePurchaseOrder po in pos.PurchaseOrderList)
                {
                    //if (chkDownload.Checked)
                    //{
                    //    if (gridIndexes.IndexOf('#' + lineCounter.ToString() + '#') < 0)
                    //    {
                    //        addRecord = false;
                    //    }
                    //    else
                    //        addRecord = true;
                    //}
                    //else
                    //    addRecord = true;

                    if (addRecord)
                    {
                        found = true;
                        foreach (BasePurchaseOrderItem pItem in po.PurchaseOrderItems)
                        {
                            sb.Append(po.PurchaseOrderNumber + ","
                                    + pItem.LineNo + ","
                                    + pItem.ItemCode + ","
                                    + pItem.MslNumber + ", "
                                    + pItem.ESN + ","
                                    + po.Tracking.ShipToTrackingNumber + ","
                                    + po.Tracking.ShipToBy + ","

                                    + (po.Tracking.ShipToDate.Year == 1 ? string.Empty : po.Tracking.ShipToDate.ToShortDateString()) + ","
                                    + po.ReturnLabel + ","
                                    + po.ReturnShipVia + ","
                                    + "\n");
                        }
                    }

                    lineCounter++;
                }

                //try
                //{
                //    using (StreamWriter sw = new StreamWriter(file.FullName))
                //    {
                //        sw.WriteLine(sb.ToString());
                //        sw.Flush();
                //        sw.Close();
                //    }
                //}
                //catch (Exception ex)
                //{
                //    lblMsg.Text = ex.Message;
                //}

                if (found)
                {
                    //Response.Clear();
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    //Response.ContentType = "application/octet-stream";
                    //Response.WriteFile(file.FullName);
                    //Response.End();

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(sb.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }


        protected void btnPoDetailTrk_Click(object sender, EventArgs e)
        {
            DownloadPoDetailsTracking();
        }


        private void DownloadExcel()
        {

        }

        private void DownloadESNData_Excel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=PoEsnData.xls");
            this.EnableViewState = false;

            PurchaseOrders pos = null;
            //if (Session["POD"] != null)
            //    pos = (PurchaseOrders)Session["POD"];
            //else
            //    pos = GetPOwithDetail();
            pos = GetSelectedPurchaseOrdes(0);

            using (StringWriter sw = new StringWriter())
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                //  Create a table to contain the grid
                Table table = new Table();

                //  Gridline to box the cells
                table.GridLines = System.Web.UI.WebControls.GridLines.Both;

                string[] columns = { "PoNum", "PODID", "ItemCode", "Esn", "UPC" };

                TableRow tRow = new TableRow();
                TableCell tCell;
                foreach (string name in columns)
                {
                    tCell = new TableCell();
                    tCell.Text = name;
                    tRow.Cells.Add(tCell);
                }

                table.Rows.Add(tRow);

                foreach (BasePurchaseOrder po in pos.PurchaseOrderList)
                {
                    foreach (BasePurchaseOrderItem poitem in po.PurchaseOrderItems)
                    {
                        try
                        {
                            tRow = new TableRow();
                            tCell = new TableCell();
                            tCell.Text = po.PurchaseOrderNumber;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = poitem.PodID.ToString();
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = poitem.ItemCode;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = "#"+poitem.ESN.ToString();
                            //tCell.Text = "=TEXT(D2,"s")"+ poitem.ESN.ToString();
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = poitem.UPC;
                            tRow.Cells.Add(tCell);

                            table.Rows.Add(tRow);
                        }
                        catch (Exception ex)
                        {
                            System.Threading.Thread.Sleep(5000);
                            Response.Write(ex.Message);
                        }
                    }
                }

                //  Htmlwriter into the table
                table.RenderControl(htw);

                //  Htmlwriter into the response
             //   HttpContext.Current.Response.Write(sw.ToString());
              //  HttpContext.Current.Response.End();

                //Response.Clear();
                //Response.Buffer = true;
                //Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                Response.Charset = "";
               // Response.ContentType = "application/text";
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }


        }

        private void DownloadPoDetailsTracking()
        {
            bool bSelected = true;

            if (bSelected == false)
            {
                ClientScript.RegisterStartupScript(GetType(), "Purchase Order", "<SCRIPT LANGUAGE='javascript'>alert('Please select Purchase Order(s)');</script>");
            }
            else
            {
                string path = Server.MapPath(downLoadPath).ToString();
                string fileName = Session.SessionID + ".csv";
                bool found = false;
                System.IO.FileInfo file = null;
                file = new System.IO.FileInfo(path + fileName);
                if (file.Exists)
                {
                    file.Delete();
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                PurchaseOrders pos = null;
                pos = GetSelectedPurchaseOrdes(0);
                //if (Session["POD"] != null)
                //    pos = (PurchaseOrders)Session["POD"];
                //else
                //    pos = GetPOwithDetail();

                int lineCounter = 0;
                bool addRecord = true;

                //string gridIndexes = GetSelectedIndexs();

                sb.Append("D,Qty,ItemCode,ESN,UPC,PurchaseOrderNumber,StoreID,SalesOrder#,TrackingNumber,ShipDate,ICCID,ReturnLabel\r\n");

                lineCounter = 0;
                string shipDate = string.Empty;
                foreach (BasePurchaseOrder po in pos.PurchaseOrderList)
                {

                    if (addRecord)
                    {
                        shipDate = po.Tracking.ShipToDate.ToShortDateString() == "1/1/0001" ? "" : po.Tracking.ShipToDate.ToShortDateString();
                        found = true;
                        foreach (BasePurchaseOrderItem pItem in po.PurchaseOrderItems)
                        {
                            sb.Append("D,"
                                    + pItem.Quantity.ToString() + ","
                                    + pItem.ItemCode + ","
                                    + pItem.ESN + ","
                                   // + pItem.MslNumber + ","
                                    + pItem.UPC + ","
                                 //   + pItem.FmUPC + ","
                                 //   + pItem.PhoneCategory + ","
                                    + po.PurchaseOrderNumber + ","
                                    + po.StoreID + ","
                                    + po.AerovoiceSalesOrderNumber + ","
                                    + pItem.TrackingNumber + ","
                                    + shipDate + ","
                                  //  + pItem.MdnNumber + ","
                                  //  + pItem.MsID + ","
                                  //  + pItem.PassCode + ","
                                    + pItem.LTEICCID + ","
                                  //  + pItem.LTEIMSI + ","
                                  //  + pItem.Akey + ","
                                  //  + pItem.Otksal + ","
                                  //  + pItem.SimNumber + ","
                                    + po.ReturnLabel + ","
                                    + "\r\n");
                        }
                    }

                    lineCounter++;
                }

                //try
                //{
                //    using (StreamWriter sw = new StreamWriter(file.FullName))
                //    {
                //        sw.WriteLine(sb.ToString());
                //        sw.Flush();
                //        sw.Close();
                //    }
                //}
                //catch (Exception ex)
                //{
                //    lblMsg.Text = ex.Message;
                //}

                if (found)
                {
                    Response.Clear();
                    Response.Buffer = true; 
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                   // Response.ContentType = "application/octet-stream";
                    // Response.WriteFile(file.FullName);
                    //  Response.End();
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(sb.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }
        private void DownloadPoHeader()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=PoEsnData.xls");
            this.EnableViewState = false;

            PurchaseOrders pos = null; //(PurchaseOrders)Session["POS"];
            pos = GetSelectedPurchaseOrdes(1);
            using (StringWriter sw = new StringWriter())
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                //  Create a table to contain the grid
                Table table = new Table();

                //  Gridline to box the cells
                table.GridLines = System.Web.UI.WebControls.GridLines.Both;

                string[] columns = { "SalesOrder#", "PurchaseOrderNumber", "CustomerAccountNumber", "PurchaseOrderDate", "storeid", "ContactName", "ShipAddress", "ShipCity", "ShipState", "ShipZip", "ShipMethod", "TrackingNumber", "ReturnShipVia", "ReturnLabel" };

                TableRow tRow = new TableRow();
                TableCell tCell;
                foreach (string name in columns)
                {
                    tCell = new TableCell();
                    tCell.Text = name;
                    tRow.Cells.Add(tCell);
                }

                table.Rows.Add(tRow);

                foreach (BasePurchaseOrder po in pos.PurchaseOrderList)
                {
                    //foreach (BasePurchaseOrderItem poitem in po.PurchaseOrderItems)
                    //{
                    try
                    {
                        tRow = new TableRow();
                        tCell = new TableCell();
                        tCell.Text = po.AerovoiceSalesOrderNumber;
                        tRow.Cells.Add(tCell);

                        tCell = new TableCell();
                        tCell.Text = po.PurchaseOrderNumber;
                        tRow.Cells.Add(tCell);

                        tCell = new TableCell();
                        tCell.Text = po.CustomerAccountNumber;
                        tRow.Cells.Add(tCell);

                        tCell = new TableCell();
                        tCell.Text = po.PurchaseOrderDate.ToShortDateString();
                        tRow.Cells.Add(tCell);

                        tCell = new TableCell();
                        tCell.Text = po.StoreID.ToString();
                        tRow.Cells.Add(tCell);

                        tCell = new TableCell();
                        tCell.Text = po.Shipping.ContactName;
                        tRow.Cells.Add(tCell);

                        tCell = new TableCell();
                        tCell.Text = po.Shipping.ShipToAddress + " " + po.Shipping.ShipToAddress2; ;
                        tRow.Cells.Add(tCell);

                        tCell = new TableCell();
                        tCell.Text = po.Shipping.ShipToCity;
                        tRow.Cells.Add(tCell);

                        tCell = new TableCell();
                        tCell.Text = po.Shipping.ShipToState;
                        tRow.Cells.Add(tCell);

                        tCell = new TableCell();
                        tCell.Text = po.Shipping.ShipToZip;
                        tRow.Cells.Add(tCell);

                        tCell = new TableCell();
                        tCell.Text = po.Tracking.ShipToBy;
                        tRow.Cells.Add(tCell);

                        tCell = new TableCell();
                        //tCell.Style = "string";
                        //  tCell.Attributes.Add("Number", "Text");

                        tCell.Text = po.Tracking.ShipToTrackingNumber.ToString();  // String.Format("{0}", po.Tracking.ShipToTrackingNumber).ToString();
                        tRow.Cells.Add(tCell);

                        tCell = new TableCell();
                        tCell.Text = po.ReturnShipVia;
                        tRow.Cells.Add(tCell);

                        tCell = new TableCell();
                        tCell.Text = po.ReturnLabel;
                        tRow.Cells.Add(tCell);

                        table.Rows.Add(tRow);

                    }
                    catch (Exception ex)
                    {
                        System.Threading.Thread.Sleep(5000);
                        Response.Write(ex.Message);
                    }
                    //}
                }

                //  Htmlwriter into the table
                table.RenderControl(htw);

                //  Htmlwriter into the response
                //  HttpContext.Current.Response.Write(sw.ToString());
                //  HttpContext.Current.Response.End();
                Response.Charset = "";
                // Response.ContentType = "application/text";
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        protected void btnPoHeader_Click(object sender, EventArgs e)
        {

            DownloadPoHeader();
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }
        
        protected void imgViewPO_OnCommand(object sender, CommandEventArgs e)
        {
            int poID = Convert.ToInt32(e.CommandArgument);

            Session["poid"] = poID;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('FulfillmentDetails.aspx')</script>", false);

            //BindPO(poID, true);
            //ViewState["poid"] = poID;
            //RegisterStartupScript("jsUnblockDialog", "unblockDialog();");


            //Commented on 21/2/2013 as this was for model popup
            //Control tmp2 = LoadControl("~/controls/PODetails.ascx");
            //avii.Controls.PODetails ctlPODetails = tmp2 as avii.Controls.PODetails;
            //pnlPO.Controls.Clear();
            //
            
            //if (tmp2 != null)
            //{

            //    ctlPODetails.BindPO(poID, true);
            //}
            //pnlPO.Controls.Add(ctlPODetails);
            //mdlPopup5.Show();
        }
        //public void BindPO(int poID, bool poQuery)
        //{
        //    lblViewPO.Text = string.Empty;
        //    btnContainerSlip.Visible = true;
        //    lblShipmentAck.Text = "PENDING";
        //    PurchaseOrders purchaseOrders = null;
        //    if (poQuery)
        //        purchaseOrders = Session["POS"] as PurchaseOrders;
        //    else
        //        purchaseOrders = PurchaseOrder.GerPurchaseOrdersNew(null, null, null, null, 0, "0", 0, null, null, null, null, null, null, null, null, null, null, poID, null, null, null, 0);
        //    List<TrackingDetail> trackingList = new List<TrackingDetail>();

        //    List<BasePurchaseOrder> purchaseOrderList = purchaseOrders.PurchaseOrderList;
        //    List<BasePurchaseOrderItem> purchaseOrderItemList = PurchaseOrder.GetPurchaseOrderItemsAndTrackingList(poID, out trackingList);
        //    Session["poitems"] = purchaseOrderItemList;
        //    Session["potracking"] = trackingList;

        //    var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(poID) select item).ToList();
        //    if (poInfoList != null && poInfoList.Count > 0)
        //    {
        //        lblPO.Text = poInfoList[0].PurchaseOrderNumber;
        //        lblvPODate.Text = poInfoList[0].PurchaseOrderDate.ToShortDateString();
        //        lblReqShipDate.Text = poInfoList[0].RequestedShipDate.ToShortDateString();

        //        lblAddress.Text = poInfoList[0].Shipping.ShipToAddress + " " + poInfoList[0].Shipping.ShipToAddress2;
        //        //lblvAvso.Text = poInfoList[0].AerovoiceSalesOrderNumber;
        //        lblContactName.Text = poInfoList[0].Shipping.ContactName;
        //        lblCustName.Text = poInfoList[0].CustomerName;
        //        //new tracking
        //        lblShipViaCode.Text = poInfoList[0].Tracking.ShipToBy;
        //        chkShipRequired.Checked = poInfoList[0].IsShipmentRequired;
        //        //if ("1/1/0001" != poInfoList[0].Tracking.ShipToDate.ToShortDateString())
        //        //    lblShippDate.Text = poInfoList[0].Tracking.ShipToDate.ToShortDateString();
        //        lblState.Text = poInfoList[0].Shipping.ShipToState;
        //        lblvStoreID.Text = poInfoList[0].StoreID;
        //        if (string.IsNullOrWhiteSpace(poInfoList[0].StoreID))
        //            btnContainerSlip.Visible = false;
        //        //new tracking
        //        //lblTrackNo.Text = poInfoList[0].Tracking.ShipToTrackingNumber;
        //        lblZip.Text = poInfoList[0].Shipping.ShipToZip;
        //        lblvStatus.Text = poInfoList[0].PurchaseOrderStatus.ToString();
        //        lblComment.Text = poInfoList[0].Comments;

        //        gvPODetail.DataSource = purchaseOrderItemList;
        //        gvPODetail.DataBind();
        //        if (purchaseOrderItemList != null && purchaseOrderItemList.Count > 0)
        //            lblPODCount.Text = "<strong>Total count:</strong> " + purchaseOrderItemList.Count;
        //        else
        //            lblPODCount.Text = string.Empty;
        //        //Bind tracking
        //        if (trackingList != null && trackingList.Count > 0)
        //            gvTracking.DataSource = trackingList;
        //        else
        //            gvTracking.DataSource = null;
        //        gvTracking.DataBind();


        //        if(trackingList != null && trackingList.Count > 0)
        //        {
        //            if ("1/1/0001" != trackingList[0].TrackingSentDateTime.ToShortDateString())
        //            {
        //                lblShipmentAck.Text = trackingList[0].TrackingSentDateTime.ToString();
        //            }
        //        }
                
        //    }



        //}
        private void BindPOItems()
        {
            List<BasePurchaseOrderItem> purchaseOrderItemList = (List<BasePurchaseOrderItem>)Session["poitems"];
            gvPODetail.DataSource = purchaseOrderItemList;// ChildDataSourcebyPODID(Convert.ToInt32(gvTemp.DataKeys[gvEditIndex].Value), string.Empty);
            gvPODetail.DataBind();
        }

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

        protected void gvPODetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string esn, msl, fmupc, mdn, msid, lteICCid, lteIMSI, akey, otksl, simnumber, sQty;
            esn = msl = fmupc = mdn = msid = lteICCid = lteIMSI = akey = otksl = simnumber = sQty = string.Empty;
            lblViewPO.Text = string.Empty;
            int userID = 0, qty=0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
                       
            }
            try
            {
                GridView gvTemp = (GridView)sender;
                gvUniqueID = gvTemp.UniqueID;

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
                    //    //    lblViewPO.Text = "LTE ICC id & LTE IMSI required!";
                    //    //    return;
                    //    //}
                    //    //if (lteICC == true && lteICCid == string.Empty)
                    //    //{
                    //    //    lblViewPO.Text = "LTE ICC id required!";
                    //    //    return;
                    //    //}
                    //    //if (lte_IMSI == true && lteIMSI == string.Empty)
                    //    //{
                    //    //    lblViewPO.Text = "LTE IMSI required!";
                    //    //    return;
                    //    //}
                    //    //if (iSSim && string.IsNullOrEmpty(simnumber))
                    //    //{
                    //    //    lblViewPO.Text = "Sim is required!";
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
                    lblViewPO.Text = returnMessage;
                    return;
                }
                gvTemp.EditIndex = -1;
                List<BasePurchaseOrderItem> purchaseOrderList = (List<BasePurchaseOrderItem>)Session["poitems"];

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
                lblViewPO.Text = "Purchase Order Detail is updated successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order Detail is updated successfully');</script>");
                //GridView1.EditIndex = -1;
                //}
            }
            catch { }
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
        protected void imgProvisioning_OnCommand(object sender, CommandEventArgs e)
        {

            int poID = Convert.ToInt32(e.CommandArgument);

            Session["poid"] = poID;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('FulfillmentEsnAssigned.aspx')</script>", false);


            //lnk_Print.Visible = false;
            //int poID = Convert.ToInt32(e.CommandArgument);
            //string poInfo = e.CommandArgument.ToString();
            

            //BindProvisioning(poID);
            //ViewState["poid"] = poID;
            //RegisterStartupScript("jsUnblockDialog", "unblockPOADialog();");



        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            lblPOA.Text = string.Empty;
            int po_id = 0;
            if (ViewState["poid"] != null)
            {
                po_id = Convert.ToInt32(ViewState["poid"]);

                List<SV.Framework.Common.LabelGenerator.Model> models = new List<SV.Framework.Common.LabelGenerator.Model>();
                List<BasePurchaseOrderItem> esnList = purchaseOrderOperation.GetPurchaseOrderItemList(po_id);

                //List<BasePurchaseOrderItem> esnList = Session["poa"] as List<BasePurchaseOrderItem>;
                SV.Framework.Common.LabelGenerator.ESNLabelOperation slabel = new SV.Framework.Common.LabelGenerator.ESNLabelOperation();
                if (esnList != null && esnList.Count > 0)
                {
                    foreach (BasePurchaseOrderItem poDetail in esnList)
                    {
                        if (!string.IsNullOrWhiteSpace(poDetail.ESN))
                            models.Add(new SV.Framework.Common.LabelGenerator.Model(poDetail.ItemCode, poDetail.ESN, poDetail.LTEICCID, poDetail.UPC));
                    }

                    var memSt = slabel.ExportToPDF(models);
                    if (memSt != null)
                    {
                        string fileType = ".pdf";
                        string filename = DateTime.Now.Ticks + fileType;
                        Response.Clear();
                        //Response.ContentType = "application/pdf";
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                        Response.Buffer = true;
                        Response.Clear();
                        var bytes = memSt.ToArray();
                        Response.OutputStream.Write(bytes, 0, bytes.Length);
                        Response.OutputStream.Flush();

                        //string imagePath = "~/pdffiles/";
                        //string fileType = ".pdf";
                        //string fileDirctory = HttpContext.Current.Server.MapPath(imagePath);
                        //string filename = DateTime.Now.Ticks + fileType;
                        //slabel.RetriveLabelFromMemory(memSt, filename, fileDirctory);


                        //string baseurl = System.Configuration.ConfigurationManager.AppSettings["url"].ToString();
                        //string filePath = baseurl + "/pdffiles/" + filename;
                        //lblPOA.Text = "Generated successfully";
                        ////ScriptManager.RegisterStartupScript(this, this.GetType(), "new window", "javascript:window.open(\""+ filePath + "\", \"_newtab\");", true);
                        //lnk_Print.HRef = filePath;
                        // lnk_Print.Visible = true;
                    }
                    else
                        lblPOA.Text = "Technical error!";
                }
            }
            else
                lblPOA.Text = "Session expired!";
        }
        private void BindProvisioning(int po_id)
        {
            //lblPOA.Text = string.Empty;


            //PurchaseOrders purchaseOrders = Session["POS"] as PurchaseOrders;
            //List<BasePurchaseOrder> purchaseOrderList = purchaseOrders.PurchaseOrderList;

            //var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(po_id) select item).ToList();
            //if (poInfoList.Count > 0)
            //{                
            //    lblAssignPO.Text = poInfoList[0].PurchaseOrderNumber;
            //}
            //    // int poRecordCount = 0;
            //    //  int poErrorLogNumber = 0;
            //    btnESNDelete.Visible = false;
            //btnPrint.Visible = false;
            //try
            //{
            //    List<POEsn> esns = new List<POEsn>();

            //    //List<FulfillemtSKU> GetPurchaseSKUList
            //    List<FulfillmentSKUs> poProvisioningLineItems =  PurchaseOrder.GetPurchaseSKUList(po_id, out esns);

            //    if (poProvisioningLineItems != null && poProvisioningLineItems.Count > 0)
            //    {
            //        foreach (FulfillmentSKUs item in poProvisioningLineItems)
            //        {
            //            if (item.IsDelete)
            //            {
            //                btnPrint.Visible = true;
            //                btnESNDelete.Visible = true;
            //                if (Session["adm"] == null)
            //                    btnESNDelete.Visible = false;

            //                if (ViewState["IsReadOnly"] != null)
            //                {
            //                    btnPrint.Visible = false;
            //                    btnESNDelete.Visible = false;

            //                }
            //            }
            //        }
            //        Session["poa"] = poProvisioningLineItems;
            //        gvPOA.DataSource = poProvisioningLineItems;
            //        gvPOA.DataBind();
            //    }
            //    else
            //    {
            //        gvPOA.DataSource = null;
            //        gvPOA.DataBind();
            //        lblPOA.Text = "No records exists!";
            //        Session["poa"] = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    lblPOA.Text = ex.Message;
            //}
        }

        protected void imgShip_OnCommand(object sender, CommandEventArgs e)
        {
            int poID = Convert.ToInt32(e.CommandArgument);
            Session["poid"] = poID;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('FulfillmentDetail.aspx')</script>", false);

          //  Response.Redirect("~/FulfillmentDetail.aspx", true);
           // "window.open('FulfillmentDetail.aspx');"

           // lblShipItem.Text = string.Empty;
           // chkTracking.Checked = false;
           // btnShip.Visible = true;
           // string poInfo = e.CommandArgument.ToString();
           // txtTrackingNumber.Text = string.Empty;
           // txtShippingDate.Text = DateTime.Now.ToShortDateString();

            // DateTime dateNow = DateTime.Now.Date;


            // txtShipComments.Text = string.Empty;
            // ddlShipVia.SelectedIndex = 0;
            // ddlShape.Items.Clear();
            // string[] itemNames = System.Enum.GetNames(typeof(SV.Framework.Common.LabelGenerator.ShipPackageShape));
            // for (int i = 0; i <= itemNames.Length - 1; i++)
            // {
            //     System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(itemNames[i], itemNames[i]);
            //     ddlShape.Items.Add(item);
            // }
            // System.Web.UI.WebControls.ListItem item1 = new System.Web.UI.WebControls.ListItem("", "");
            // ddlShape.Items.Insert(0, item1);

            // PurchaseOrders purchaseOrders = Session["POS"] as PurchaseOrders;
            // List<BasePurchaseOrder> purchaseOrderList = purchaseOrders.PurchaseOrderList;

            // var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(poID) select item).ToList();
            // if (poInfoList.Count > 0)
            // {
            //     string day = string.Empty;
            //     string month = string.Empty;
            //     string year = string.Empty;

            //     DateTime RequestedShipDate = poInfoList[0].RequestedShipDate.Date;
            //     if (RequestedShipDate < dateNow)
            //     { //txtShippingDate.Text = dateNow.ToString("MM/dd/yyyy");
            //         day = dateNow.Day.ToString();
            //         month = dateNow.Month.ToString();
            //         year = dateNow.Year.ToString();
            //         txtShippingDate.Text = month + "/" + day + "/" + year;

            //     }
            //     else
            //     { //txtShippingDate.Text = poInfoList[0].RequestedShipDate.ToString("MM/dd/yyyy");

            //         day = poInfoList[0].RequestedShipDate.Day.ToString();
            //         month = poInfoList[0].RequestedShipDate.Month.ToString();
            //         year = poInfoList[0].RequestedShipDate.Year.ToString();
            //         txtShippingDate.Text = month + "/" + day + "/" + year;
            //     }

            //     string poShipBy = poInfoList[0].Tracking.ShipToBy;
            //     lblShipPO.Text = poInfoList[0].PurchaseOrderNumber;

            //     ddlShipVia.SelectedValue = poShipBy;

            //     if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), poShipBy))
            //     {
            //         ddlShape.SelectedValue = SV.Framework.Common.LabelGenerator.ShipPackageShape.Letter.ToString();
            //     }
            //     else
            //         ddlShape.SelectedValue = SV.Framework.Common.LabelGenerator.ShipPackageShape.package.ToString();


            //     if (poInfoList[0].PurchaseOrderStatusID == 3)
            //         btnShip.Visible = false;
            // }
            // //string[] arr = poInfo.Split(',');
            // //string companyAccountNumber, poNum;
            // //companyAccountNumber = poNum = string.Empty;

            // //if (arr.Length > 2)
            // //{
            // //    poID = Convert.ToInt32(arr[0]);
            // //    poNum = arr[1].ToString();
            // //    companyAccountNumber = arr[2].ToString();
            // //    ViewState["ponum"] = poNum + "," + companyAccountNumber;

            // //}
            //// BindShipping(poID);
            // ViewState["poid"] = poID;
            // RegisterStartupScript("jsUnblockDialog", "unblockShipItemsDialog();");

        }
        //private void BindShipping(int po_id)
        //{
        //    lblShipItem.Text = string.Empty;
        //    // int poRecordCount = 0;
        //    //  int poErrorLogNumber = 0;

        //    try
        //    {
        //        List<FulfillmentShippingLineItem> poShippingLineItems = FulfillmentShippingLineItemOperation.GetShippingLineItems(po_id);

        //        if (poShippingLineItems != null && poShippingLineItems.Count > 0)
        //        {
        //            Session["ship"] = poShippingLineItems;
        //            rptShipItems.DataSource = poShippingLineItems;
        //            rptShipItems.DataBind();
        //        }
        //        else
        //        {
        //            rptShipItems.DataSource = null;
        //            rptShipItems.DataBind();
        //            lblShipItem.Text = "No records exists!";
        //            Session["ship"] = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblShipItem.Text = ex.Message;
        //    }




        //}

       
        protected void lnkStore_OnCommand(object sender, CommandEventArgs e)
        {
            lblStMsg.Text = string.Empty;
            lblStCountry.Text = string.Empty;
            lblStPhone.Text = string.Empty;
            lblStState.Text = string.Empty;
            lblStZip.Text = string.Empty;
            lblStCity.Text = string.Empty;
            lblStCName.Text = string.Empty;
            lblStAdd.Text = string.Empty;
            lblStID.Text = string.Empty;
            lblStName.Text = string.Empty;

            int companyID = 0;
            string storeID = string.Empty;
            string getIDs = Convert.ToString(e.CommandArgument);
            string[] arr = getIDs.Split(',');
            if (arr.Length > 0)
            {
                storeID = arr[0];
                if (arr.Length > 1)
                    companyID = Convert.ToInt32(arr[1]);
                if (!string.IsNullOrEmpty(storeID.Trim()) && companyID > 0)
                {
                    BindStroeAddress(storeID, companyID);

                }
                else
                {
                    lblStMsg.Text = "Missing parameters";
                }


            }
            else
            {
                lblStMsg.Text = "Missing parameters";
            }
            RegisterStartupScript("jsUnblockDialog", "unblockStoreDialog();");

            //mdlStore.Show();

        }

        private void BindStroeAddress(string storeID, int companyID)
        {
            try
            {
                List<SV.Framework.Admin.StoreLocation> storeAddress = SV.Framework.Admin.UserStoreOperation.GetStoreAddress(storeID, companyID);
                if (storeAddress.Count > 0)
                {

                    lblStAdd.Text = storeAddress[0].StoreAddress.Address1 + " " + storeAddress[0].StoreAddress.Address2;
                    lblStCity.Text = storeAddress[0].StoreAddress.City;
                    lblStState.Text = storeAddress[0].StoreAddress.State;
                    lblStZip.Text = storeAddress[0].StoreAddress.Zip;
                    lblStCountry.Text = storeAddress[0].StoreAddress.Country;
                    lblStCName.Text = storeAddress[0].StoreContact.ContactName;
                    lblStPhone.Text = storeAddress[0].StoreContact.OfficePhone1;
                    lblStID.Text = storeAddress[0].StoreID;
                    lblStName.Text = storeAddress[0].StoreName;



                }
                else
                {
                    //lblStMsg.Text = "No record found";
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }


        protected void gvTracking_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex >= 0)
                {
                    HiddenField hdTN = (HiddenField)e.Row.FindControl("hdTN");
                    HiddenField hdShipVia = (HiddenField)e.Row.FindControl("hdShipVia");
                    
                    LinkButton lnkESN = (LinkButton)e.Row.FindControl("lnkESN");
                    lnkESN.OnClientClick = "openESNDialogAndBlock('ESN LIST', '" + lnkESN.ClientID + "')";

                    ImageButton imgEditTr = (ImageButton)e.Row.FindControl("imgEditTr");
                    imgEditTr.OnClientClick = "openAddTrackingDialogAndBlock('Edit Tracking', '" + imgEditTr.ClientID + "')";
                    ImageButton imgLabl = (ImageButton)e.Row.FindControl("imgLabl");

                    ImageButton imgPrint = (ImageButton)e.Row.FindControl("imgPrint");

                   // ScriptManager.GetCurrent(this).RegisterPostBackControl(imgPrint);
                    if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), hdShipVia.Value))
                    {
                        //AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
                        //trigger.ControlID = imgPrint.ClientID;
                        //trigger.EventName = "Click";
                        //upnlView.Triggers.Add(trigger);
                        // imgPrint.OnClientClick = "openLabelDialogAndBlock('Shipping Label', '" + imgPrint.ClientID + "')";

                    }
                    if(string.IsNullOrEmpty(hdTN.Value))
                    {
                        
                        imgPrint.Visible = false;
                        
                    }
                    else
                        imgLabl.Visible = false;

                    if (Session["adm"] == null)
                    {
                        ImageButton imgDelTr = (ImageButton)e.Row.FindControl("imgDelTr");
                        //LinkButton obj = (LinkButton)e.Row.Cells[5].Controls[0];
                        //if (obj != null)
                        //{
                        //    obj.Enabled = false;
                        //    obj.Visible = false;
                        //}

                        imgDelTr.Visible = false;
                        imgEditTr.Visible = false;


                    }
                }
            }
        }
        protected void gvPOA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex >= 0)
                {
                    HiddenField hdTN = (HiddenField)e.Row.FindControl("hdTN");
                    LinkButton lnkQty = (LinkButton)e.Row.FindControl("lnkQty");
                    lnkQty.OnClientClick = "openSKUESNDialogAndBlock('Assigned ESN', '" + lnkQty.ClientID + "')";

                    CheckBox chkDel = (CheckBox)e.Row.FindControl("chkDel");
                    if (chkDel.Visible)
                        btnESNDelete.Visible = true;

                    if (Session["adm"] == null)
                        btnESNDelete.Visible = false;

                    if (ViewState["IsReadOnly"] != null)
                    {
                        btnPrint.Visible = false;
                        btnESNDelete.Visible = false;

                    }
                    //{
                    //    CheckBox chkDel = (CheckBox)e.Row.FindControl("chkDel");
                    //    chkDel.Visible = false;
                    //}
                }
            }
        }
        protected void btnTracking_Click(object sender, EventArgs e)
        {
            lblViewPO.Text = string.Empty;
            txtTrackings.Text = string.Empty;
            txtTrComment.Text = string.Empty;
            dpShipBy.SelectedIndex = 0;
            //ddlReturn.SelectedIndex = 0;
            lblReturn.Text = "Returned";

            ViewState["linenumber"] = null;
            RegisterStartupScript("jsUnblockDialog", "unblockAddTrackingDialog();");

        }
        protected void btnAddTrackings_Click(object sender, EventArgs e)
        {
            try
            {
                SV.Framework.Fulfillment.TrackingOperations trackingOperations = SV.Framework.Fulfillment.TrackingOperations.CreateInstance<SV.Framework.Fulfillment.TrackingOperations>();

                int poID = Convert.ToInt32(ViewState["poid"]);
                int userID = 0;
                int returnCount = 0;
                int linenumber = 0;

                string trackingNumber, returnLabel, comment, returnMessage;
                int shipByID = Convert.ToInt32(dpShipBy.SelectedValue);
                List<TrackingDetail> trackingList = new List<TrackingDetail>();

                trackingNumber = returnLabel = comment = returnMessage = string.Empty;
                trackingNumber = txtTrackings.Text.Trim();
                comment = txtTrComment.Text.Trim();

                //if (ddlReturn.SelectedIndex > 0)
                //only return label


                //else
                //    if (ddlReturn.SelectedIndex == 2)
                //        returnLabel = "R";

                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    userID = userInfo.UserGUID;
                }

                if (Session["potracking"] != null)
                    trackingList = (List<TrackingDetail>)Session["potracking"];

                //if (trackingList != null && trackingList.Count > 0)
                //{
                //    var item = trackingList.Max(x => x.LineNumber);
                //    linenumber = item + 1;
                //}
                //else
                //    linenumber = 1;

                if (ViewState["linenumber"] != null)
                {
                    linenumber = Convert.ToInt32(ViewState["linenumber"]);
                    int index = trackingList.FindIndex(x => x.LineNumber.Equals(linenumber));
                    returnLabel = trackingList[index].ReturnValue;
                    trackingList[index].ReturnValue = returnLabel;
                    trackingList[index].ShipByID = shipByID;
                    trackingList[index].TrackingNumber = trackingNumber;
                    trackingList[index].Comments = comment;
                    trackingList[index].ReturnValue = returnLabel;

                }
                else
                {
                    returnLabel = "R";
                    TrackingDetail tracking = new TrackingDetail();
                    tracking.ShipByID = shipByID;
                    tracking.ReturnValue = returnLabel;
                    tracking.TrackingNumber = trackingNumber;
                    tracking.Comments = comment;
                    tracking.LineNumber = linenumber;
                    trackingList.Add(tracking);
                }
                if (trackingList != null && trackingList.Count > 0)
                {
                    trackingList = trackingOperations.FulfillmentTrackingUpdate(trackingList, poID, "W", PurchaseOrderStatus.Shipped, userID, out returnCount, out returnMessage);
                    gvTracking.DataSource = trackingList;
                    gvTracking.DataBind();
                    Session["potracking"] = trackingList;
                    ReloadPOLineItems();

                    lblViewPO.Text = "Submitted successfully";


                    RegisterStartupScript("jsUnblockDialog", "closeAddTrackingDialog();");
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order Tracking is updated successfully');</script>");

                }
                //gvTracking.DataSource = trackingList;
                //gvTracking.DataBind();
            }
            catch (Exception ex)
            {
                lblViewPO.Text = ex.Message;
            }


        }
        protected void lnkTracking_Command(object sender, CommandEventArgs e)
        {
            //lblESN.Text = string.Empty;
            //lblTracking.Text = string.Empty;
            //lblViewPO.Text = "";
            //try
            //{
            //    int poID = 0;
            //    if (ViewState["poid"] != null)
            //    {
            //        poID = Convert.ToInt32(ViewState["poid"]);

            //        string trackingNumber = Convert.ToString(e.CommandArgument);
            //        lblTracking.Text = trackingNumber;
            //        List<EsnList> esnList = FulfillmentOperations.GetTrackingESNList(poID, trackingNumber);

            //        if (esnList != null && esnList.Count > 0)
            //        {
            //            rptESN.DataSource = esnList;
            //            rptESN.DataBind();
            //        }
            //        else
            //        {
            //            rptESN.DataSource = null;
            //            rptESN.DataBind();
            //            lblESN.Text = "No records found";
            //        }

            //    }
                

                

            //    RegisterStartupScript("jsUnblockDialog", "unblockESNDialog();");
            //}
            //catch (Exception ex)
            //{
            //    lblESN.Text = ex.Message;
            //}
                
        }
        
        protected void imgGeneratShipLabel_Command(object sender, CommandEventArgs e)
        {
            int poid = 0, userId = 0;
            lblESN.Text = string.Empty;
            lblTracking.Text = string.Empty;
            SV.Framework.Common.LabelGenerator.EndiciaPrintLabel labelInfo = new SV.Framework.Common.LabelGenerator.EndiciaPrintLabel();

            SV.Framework.Common.LabelGenerator.ShippingLabelOperation shipAccess = new SV.Framework.Common.LabelGenerator.ShippingLabelOperation();
            SV.Framework.Common.LabelGenerator.ShipInfo shipInfo = new SV.Framework.Common.LabelGenerator.ShipInfo();
            try
            {
                string shipmentType = Convert.ToString(e.CommandArgument);
                if (ViewState["poid"] != null)
                {
                    poid = Convert.ToInt32(ViewState["poid"]);

                    if (Session["UserID"] != null)
                    {
                        int.TryParse(Session["UserID"].ToString(), out userId);
                    }

                    PurchaseOrders pos = Session["POS"] as PurchaseOrders;

                    if (pos != null && pos.PurchaseOrderList.Count > 0)
                    {

                        var po = (from item in pos.PurchaseOrderList where item.PurchaseOrderID.Equals(poid) select item).ToList();
                        if (po != null && po.Count > 0)
                        {
                            labelInfo.FulfillmentNumber = po[0].PurchaseOrderNumber;
                            shipInfo.ShipToAddress = po[0].Shipping.ShipToAddress;
                            shipInfo.ShipToAddress2 = po[0].Shipping.ShipToAddress2;
                            shipInfo.ContactName = po[0].Shipping.ContactName;
                            shipInfo.ShipToCity = po[0].Shipping.ShipToCity;
                            shipInfo.ShipToState = po[0].Shipping.ShipToState;
                            shipInfo.ShipToZip = po[0].Shipping.ShipToZip;
                            labelInfo.ShipTo = shipInfo;
                            labelInfo.PackageWeight = new SV.Framework.Common.LabelGenerator.Weight { units = "Oz", value = 1 };
                            labelInfo.PackageContent = "";
                            labelInfo.ShippingMethod = SV.Framework.Common.LabelGenerator.ShipMethods.Priority;
                            labelInfo.PackageShape = SV.Framework.Common.LabelGenerator.ShipPackageShape.Letter;

                            SV.Framework.Common.LabelGenerator.iPrintLabel response = shipAccess.PrintShippingLabel(labelInfo);
                            if (response != null && !string.IsNullOrWhiteSpace(response.TrackingNumber))
                            {
                                ShippingLabelInfo request = new ShippingLabelInfo();
                                request.FulfillmentNumber = labelInfo.FulfillmentNumber;
                                request.ShipmentType = shipmentType;
                                request.ShippingLabelImage = response.ShippingLabelImage;
                                request.TrackingNumber = response.TrackingNumber;

                                //ShippingLabelResponse setResponse = ShippingLabelOperation.ShippingLabelUpdate(request, userId);
                                lblShipItem.Text = "Label generated successfully";
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(response.PackageContent))
                                    lblShipItem.Text = response.PackageContent;
                                else
                                    lblShipItem.Text = "Technical error please try again!";
                            }

                        }


                    }

                }
            }
            catch (Exception ex)
            { }


        }
        protected void imgGenerateLabel_Command(object sender, CommandEventArgs e)
        {
            int poid = 0, userId = 0;
            lblViewPO.Text = string.Empty;
            lblTracking.Text = string.Empty;
            SV.Framework.Common.LabelGenerator.EndiciaPrintLabel labelInfo = new SV.Framework.Common.LabelGenerator.EndiciaPrintLabel();

            SV.Framework.Common.LabelGenerator.ShippingLabelOperation shipAccess = new SV.Framework.Common.LabelGenerator.ShippingLabelOperation();
            SV.Framework.Common.LabelGenerator.ShipInfo shipToInfo = new SV.Framework.Common.LabelGenerator.ShipInfo();
            SV.Framework.Common.LabelGenerator.ShipInfo shipFromInfo = new SV.Framework.Common.LabelGenerator.ShipInfo();

            try
            {
                string shipmentType = Convert.ToString(e.CommandArgument);
                if (ViewState["poid"] != null)
                {
                    poid = Convert.ToInt32(ViewState["poid"]);

                    if (Session["UserID"] != null)
                    {
                        int.TryParse(Session["UserID"].ToString(), out userId);
                    }

                    PurchaseOrders pos = Session["POS"] as PurchaseOrders;

                    if (pos != null && pos.PurchaseOrderList.Count > 0)
                    {

                        var po = (from item in pos.PurchaseOrderList where item.PurchaseOrderID.Equals(poid) select item).ToList();
                        if (po != null && po.Count > 0)
                        {
                            labelInfo.FulfillmentNumber = po[0].PurchaseOrderNumber;
                            //shipToInfo
                            shipToInfo.ShipToAddress = po[0].Shipping.ShipToAddress;
                            shipToInfo.ShipToAddress2 = po[0].Shipping.ShipToAddress2;
                            shipToInfo.ContactName = po[0].Shipping.ContactName;
                            shipToInfo.ShipToCity = po[0].Shipping.ShipToCity;
                            shipToInfo.ShipToState = po[0].Shipping.ShipToState;
                            shipToInfo.ShipToZip = po[0].Shipping.ShipToZip;

                            labelInfo.ShipTo = shipToInfo;

                            //ship From Info
                            shipFromInfo.ShipToAddress = shipFromAddress;
                            shipFromInfo.ShipToAddress2 = "";
                            shipFromInfo.ContactName = shipFromContactName;
                            shipFromInfo.ShipToCity = shipFromCity;
                            shipFromInfo.ShipToState = shipFromState;
                            shipFromInfo.ShipToZip = shipFromZip;
                            shipFromInfo.ShipToAttn = shipFromContactName;
                            shipFromInfo.ShipToCountry = shipFromCountry;
                            shipFromInfo.ContactPhone = shipFromPhone;

                            labelInfo.ShipFrom = shipFromInfo;
                            //
                            labelInfo.PackageWeight = new SV.Framework.Common.LabelGenerator.Weight { units = "Oz", value = 1 };
                            labelInfo.PackageContent = "Description";

                            //labelInfo.ShippingMethod = SV.Framework.Common.LabelGenerator.ShipMethods.Priority;
                            //if (ddlShipVia.SelectedIndex > 0)
                            {
                                Enum.TryParse(po[0].Tracking.ShipToBy, out SV.Framework.Common.LabelGenerator.ShipMethods shipMethods);
                                labelInfo.ShippingMethod = shipMethods;
                            }

                            labelInfo.PackageShape = SV.Framework.Common.LabelGenerator.ShipPackageShape.Flat;

                            SV.Framework.Common.LabelGenerator.iPrintLabel response = shipAccess.PrintShippingLabel(labelInfo);
                            if (response != null && !string.IsNullOrWhiteSpace(response.TrackingNumber))
                            {
                                ShippingLabelInfo request = new ShippingLabelInfo();
                                request.FulfillmentNumber = labelInfo.FulfillmentNumber;
                                request.ShipmentType = shipmentType;
                                request.ShippingLabelImage = response.ShippingLabelImage;
                                request.TrackingNumber = response.TrackingNumber;

                                //ShippingLabelResponse setResponse = ShippingLabelOperation.ShippingLabelUpdate(request, userId);
                                lblViewPO.Text = "Label generated successfully";
                            }
                            else
                                lblViewPO.Text = "Technical error please try again!";
                        }


                    }

                }
            }
            catch (Exception ex)
            { }
            

        }

        private void PrintLabel(int lineNumber)
        {
            try
            {
                SV.Framework.Fulfillment.ShippingLabelOperation shippingLabelOperation = SV.Framework.Fulfillment.ShippingLabelOperation.CreateInstance<SV.Framework.Fulfillment.ShippingLabelOperation>();

                float width = 320, height = 520, envHeight = 500;
                string shipMethod = string.Empty, ShipPackage = string.Empty;
                string labelBase64 = shippingLabelOperation.GetLabelBase64(lineNumber, out shipMethod, out ShipPackage);
                if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), shipMethod))
                {
                    if (!string.IsNullOrWhiteSpace(labelBase64))
                    {
                        //  imgLabel.ImageUrl = "data:image;base64," + labelBase64;
                        //RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");

                        byte[] imageBytes = Convert.FromBase64String(labelBase64);
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);

                        if (shipMethod.ToLower() == "first" && ShipPackage.ToString().ToLower() == "letter")
                        {
                            width = 500;
                            height = 320;
                            envHeight = 320;
                        }
                        image.ScaleToFit(width, height);
                        
                        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                        {
                            Document document = new Document();
                            iTextSharp.text.Rectangle envelope = new iTextSharp.text.Rectangle(width, envHeight);
                            document.SetPageSize(envelope);
                            document.SetMargins(0, 0, 0, 0);

                            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                            document.Open();
                           
                            document.Add(image);
                            document.Close();
                            var bytes = memoryStream.ToArray();
                            // memoryStream.Close();

                            string fileType = ".pdf";
                            string filename = DateTime.Now.Ticks + fileType;
                            Response.Clear();
                            //Response.ContentType = "application/pdf";
                            Response.ContentType = "application/octet-stream";
                            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                            Response.Buffer = true;
                            Response.Clear();
                            // var bytes = memSt.ToArray();
                            Response.OutputStream.Write(bytes, 0, bytes.Length);
                            Response.OutputStream.Flush();
                        }
                    }
                }
                else
                {
                    // RegisterStartupScript("jsUnblockDialog", "closeLabelDialog();");
                    if (!string.IsNullOrWhiteSpace(labelBase64))
                    {
                        //var script = "OpenPDF('" + labelBase64 + "')"; //"window.open('" + pdfByteArray + "', '_blank');";
                        //ScriptManager.RegisterClientScriptBlock(Parent.Page, typeof(Page), "pdf", script, true);
                       // ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenPDF('" + labelBase64 + "')</script>", false);
                        // imgLabel.ImageUrl = "data:application/pdf;base64," + labelBase64;
                        //  RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");
                        //data: application/pdf; base64,
                        string fileType = ".pdf";
                        string filename = DateTime.Now.Ticks + fileType;
                        Response.Clear();
                        //Response.ContentType = "application/pdf";
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                        Response.Buffer = true;
                        Response.Clear();
                        var bytes = Convert.FromBase64String(labelBase64);
                        Response.OutputStream.Write(bytes, 0, bytes.Length);
                        Response.OutputStream.Flush();
                       // Response.End();

                    }
                }


            }
            catch (Exception ex)
            {
                lblViewPO.Text = ex.Message;
            }
        }
        protected void btnhdPrintlabel_Click(object sender, EventArgs e)
        {
            if(ViewState["linenumber"] != null)
            {
                PrintLabel(Convert.ToInt32(ViewState["linenumber"]));
            }
        }
        protected void imgPrint_Command(object sender, CommandEventArgs e)
        {
            int lineNumber = Convert.ToInt32(e.CommandArgument);
             ViewState["linenumber"] = lineNumber;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp1", "<script language='javascript'>Refresh();</script>", false);

            //btnhdPrintlabel.Click += new EventHandler(btnhdPrintlabel_Click);

            //try
            //{

            //    string shipMethod = string.Empty;
            //    string labelBase64 = ShippingLabelOperation.GetLabelBase64(lineNumber, out shipMethod);
            //    if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), shipMethod))
            //    {
            //        if (!string.IsNullOrWhiteSpace(labelBase64))
            //        {
            //            imgLabel.ImageUrl = "data:image;base64," + labelBase64;
            //            RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");




            //            //byte[] imageBytes = Convert.FromBase64String(labelBase64);
            //            //iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);

            //            //using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            //            //{
            //            //    Document document = new Document(PageSize.LEGAL, 1f, 1f, 1f, 1f);
            //            //    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            //            //    document.Open();
            //            //    document.Add(image);
            //            //    document.Close();
            //            //    byte[] bytes = memoryStream.ToArray();
            //            //    // memoryStream.Close();

            //            //    string fileType = ".pdf";
            //            //    string filename = DateTime.Now.Ticks + fileType;
            //            //    Response.Clear();
            //            //    //Response.ContentType = "application/pdf";
            //            //    Response.ContentType = "application/octet-stream";
            //            //    Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            //            //    Response.Buffer = true;
            //            //    Response.Clear();
            //            //    // var bytes = memSt.ToArray();
            //            //    Response.OutputStream.Write(bytes, 0, bytes.Length);
            //            //    Response.OutputStream.Flush();


            //                // Response.End();


            //                //Response.Clear();
            //                ////Response.ContentType = "application/pdf";
            //                //Response.ContentType = "application/octet-stream";
            //                //Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            //                //Response.Buffer = true;
            //                //Response.Clear();
            //                //var bytes = Convert.FromBase64String(labelBase64);
            //                //Response.OutputStream.Write(bytes, 0, bytes.Length);
            //                //Response.OutputStream.Flush()
            //            //}
            //        }
            //    }
            //    else
            //    {
            //        // RegisterStartupScript("jsUnblockDialog", "closeLabelDialog();");
            //        if (!string.IsNullOrWhiteSpace(labelBase64))
            //        {
            //            //var script = "OpenPDF('" + labelBase64 + "')"; //"window.open('" + pdfByteArray + "', '_blank');";
            //            //ScriptManager.RegisterClientScriptBlock(Parent.Page, typeof(Page), "pdf", script, true);
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenPDF('" + labelBase64 + "')</script>", false);
            //            // imgLabel.ImageUrl = "data:application/pdf;base64," + labelBase64;
            //            //  RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");
            //            //data: application/pdf; base64,
            //            //string fileType = ".pdf";
            //            //string filename = DateTime.Now.Ticks + fileType;
            //            //Response.Clear();
            //            ////Response.ContentType = "application/pdf";
            //            //Response.ContentType = "application/octet-stream";
            //            //Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            //            //Response.Buffer = true;
            //            //Response.Clear();
            //            //var bytes = Convert.FromBase64String(labelBase64);
            //            //Response.OutputStream.Write(bytes, 0, bytes.Length);
            //            //Response.OutputStream.Flush();
            //            //Response.End();

            //        }
            //    }


            //}
            //catch (Exception ex)
            //{
            //    lblViewPO.Text = ex.Message;
            //}
        }
        //    protected void lnkPrint_Command(object sender, CommandEventArgs e)
        //{


        //    try
        //    {
        //        int lineNumber = Convert.ToInt32(e.CommandArgument);

        //        string labelBase64 = ShippingLabelOperation.GetLabelBase64(lineNumber);
        //        if (!string.IsNullOrWhiteSpace(labelBase64))
        //            imgLabel.ImageUrl = "data:image;base64," + labelBase64;


        //        RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");
        //    }
        //    catch (Exception ex)
        //    {
        //        lblViewPO.Text = ex.Message;
        //    }
        //}
        protected void imgEditTracking_Command(object sender, CommandEventArgs e)
        {
            lblViewPO.Text = string.Empty;
            txtTrackings.Text = string.Empty;
            txtTrComment.Text = string.Empty;
            dpShipBy.SelectedIndex = 0;
            //ddlReturn.SelectedIndex = 0;

            try
            {
                int lineNumber = Convert.ToInt32(e.CommandArgument);
                List<TrackingDetail> trackingList = new List<TrackingDetail>();
                ViewState["linenumber"] = lineNumber;
                if (Session["potracking"] != null)
                    trackingList = (List<TrackingDetail>)Session["potracking"];

                var poTrackingList = (from item in trackingList where item.LineNumber.Equals(lineNumber) select item).ToList();
                if (poTrackingList != null && poTrackingList.Count > 0)
                {
                    txtTrackings.Text = poTrackingList[0].TrackingNumber;
                    txtTrComment.Text = poTrackingList[0].Comments;

                    dpShipBy.SelectedValue = poTrackingList[0].ShipByID.ToString();

                    lblReturn.Text = poTrackingList[0].ReturnValue == "S" ? "Ship" : "Return";
                    //ddlReturn.SelectedValue = poTrackingList[0].ReturnValue;

                }

                RegisterStartupScript("jsUnblockDialog", "unblockAddTrackingDialog();");
            }
            catch (Exception ex)
            {
                lblViewPO.Text = ex.Message;
            }
        }

        private void ReloadPOLineItemsold()
        {
            if (ViewState["poid"] != null)
            {
                int po_id = Convert.ToInt32(ViewState["poid"]);


                List<BasePurchaseOrderItem> purchaseOrderItemList = purchaseOrderOperation.GetPurchaseOrderItemList(po_id);
                Session["poitems"] = purchaseOrderItemList;
                gvPODetail.DataSource = purchaseOrderItemList;
                gvPODetail.DataBind();

            }
        }
        public void ReloadPOLineItems()
        {
            if (ViewState["poid"] != null)
            {
                int poID = Convert.ToInt32(ViewState["poid"]);


                List<TrackingDetail> trackingList = new List<TrackingDetail>();

                List<BasePurchaseOrderItem> purchaseOrderItemList = purchaseOrderOperation.GetPurchaseOrderItemsAndTrackingList(poID, out trackingList);
                Session["poitems"] = purchaseOrderItemList;
              //  Session["potracking"] = trackingList;
                gvPODetail.DataSource = purchaseOrderItemList;
                gvPODetail.DataBind();
                if (purchaseOrderItemList != null && purchaseOrderItemList.Count > 0)
                    lblPODCount.Text = "<strong>Total count:</strong> " + purchaseOrderItemList.Count;
                else
                    lblPODCount.Text = string.Empty;

                

            }

        }
        protected void imgDelTracking_Command(object sender, CommandEventArgs e)
        {
            SV.Framework.Fulfillment.TrackingOperations trackingOperations = SV.Framework.Fulfillment.TrackingOperations.CreateInstance<SV.Framework.Fulfillment.TrackingOperations>();

            int poID = Convert.ToInt32(ViewState["poid"]);
            int lineNumber = Convert.ToInt32(e.CommandArgument);
            int count = 0;
            int userID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
            }
            try
            {
                List<TrackingDetail> trackingList = trackingOperations.FulfillmentTrackingDelete(lineNumber, poID, "W", userID, out count);
                if (count == 1)
                {
                    lblViewPO.Text = "Fulfillment Order Tracking can not be deleted";
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order Tracking is deleted successfully');</script>");

                }
                else
                {
                    gvTracking.DataSource = trackingList;
                    gvTracking.DataBind();
                    Session["potracking"] = trackingList;
                   // ReloadPOLineItems();

                    lblViewPO.Text = "Fulfillment Order Tracking is deleted successfully";
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order Tracking is deleted successfully');</script>");


                }
            }
            catch (Exception ex)
            {
                lblViewPO.Text = ex.Message;
            }

        }

        protected void imgComments_OnCommand(object sender, CommandEventArgs e)
        {

            try
            {
                lblFNum.Text = string.Empty;
                Control tmp2 = LoadControl("~/controls/FulfillmentComments.ascx");
                avii.Controls.FulfillmentComments ctlFulfillmentComments = tmp2 as avii.Controls.FulfillmentComments;
                pnlComments.Controls.Clear();
                string info = e.CommandArgument.ToString();
                string[] arr = info.Split(',');
                if (arr.Length > 1)
                {
                    int poid = Convert.ToInt32(arr[0]);
                    string poNumber = arr[1];
                    lblFNum.Text = poNumber;
                    if (tmp2 != null)
                    {

                        ctlFulfillmentComments.BindComments(poid);
                    }
                    pnlComments.Controls.Add(ctlFulfillmentComments);

                    RegisterStartupScript("jsUnblockDialog", "unblockCommentsDialog();");
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        private int GenerateShipmentLabel(List<FulfillmentShippingLineItem> listitems)
        {
            SV.Framework.Fulfillment.ShippingLabelOperation shippingLabelOperation = SV.Framework.Fulfillment.ShippingLabelOperation.CreateInstance<SV.Framework.Fulfillment.ShippingLabelOperation>();

            int returnResult = 0;
            int poid = 0, userId = 0;
            double weight = 0;
            string ShipDate = txtShippingDate.Text;
            DateTime LabelPrintDateTime = DateTime.Today;
            if (!string.IsNullOrEmpty(ShipDate))
                LabelPrintDateTime = Convert.ToDateTime(ShipDate);

            double.TryParse(txtWeight.Text.Trim(), out weight);
            if (weight == 0)
                weight = 1;
            lblESN.Text = string.Empty;
            lblTracking.Text = string.Empty;
            SV.Framework.Common.LabelGenerator.EndiciaPrintLabel labelInfo = new SV.Framework.Common.LabelGenerator.EndiciaPrintLabel();

            SV.Framework.Common.LabelGenerator.ShippingLabelOperation shipAccess = new SV.Framework.Common.LabelGenerator.ShippingLabelOperation();
            SV.Framework.Common.LabelGenerator.ShipInfo shipToInfo = new SV.Framework.Common.LabelGenerator.ShipInfo();
            SV.Framework.Common.LabelGenerator.ShipInfo shipFromInfo = new SV.Framework.Common.LabelGenerator.ShipInfo();
            

            try
            {
                string shipmentType = "S";
                if (ViewState["poid"] != null)
                {
                    poid = Convert.ToInt32(ViewState["poid"]);

                    if (Session["UserID"] != null)
                    {
                        int.TryParse(Session["UserID"].ToString(), out userId);
                    }

                    PurchaseOrders pos = Session["POS"] as PurchaseOrders;

                    if (pos != null && pos.PurchaseOrderList.Count > 0)
                    {

                        var po = (from item in pos.PurchaseOrderList where item.PurchaseOrderID.Equals(poid) select item).ToList();
                        if (po != null && po.Count > 0)
                        {
                            labelInfo.FulfillmentNumber = po[0].PurchaseOrderNumber;
                            labelInfo.LabelPrintDateTime = LabelPrintDateTime;
                            //shipToInfo
                            shipToInfo.ShipToAddress = po[0].Shipping.ShipToAddress;
                            shipToInfo.ShipToAddress2 = po[0].Shipping.ShipToAddress2;
                            shipToInfo.ContactName = po[0].Shipping.ContactName;
                            shipToInfo.ShipToCity = po[0].Shipping.ShipToCity;
                            shipToInfo.ShipToState = po[0].Shipping.ShipToState;
                            shipToInfo.ShipToZip = po[0].Shipping.ShipToZip;
                            shipToInfo.ShipToAttn = po[0].Shipping.ShipToAttn ?? po[0].Shipping.ContactName;
                            shipToInfo.ContactPhone = po[0].Shipping.ContactPhone;
                            labelInfo.ShipTo = shipToInfo;

                            //ship From Info
                            shipFromInfo.ShipToAddress = shipFromAddress;
                            shipFromInfo.ShipToAddress2 = "";
                            shipFromInfo.ContactName = shipFromContactName;
                            shipFromInfo.ShipToCity = shipFromCity;
                            shipFromInfo.ShipToState = shipFromState;
                            shipFromInfo.ShipToZip = shipFromZip;
                            shipFromInfo.ShipToAttn = shipFromContactName;
                            shipFromInfo.ShipToCountry = shipFromCountry;
                            shipFromInfo.ContactPhone = shipFromPhone;

                            labelInfo.ShipFrom = shipFromInfo;
                            //
                            labelInfo.PackageWeight = new SV.Framework.Common.LabelGenerator.Weight { units = "ounces", value = weight };
                            labelInfo.PackageContent = "Description";

                            //labelInfo.ShippingMethod = SV.Framework.Common.LabelGenerator.ShipMethods.Priority;
                            // if (ddlShipVia.SelectedIndex > 0)
                            {
                                Enum.TryParse(ddlShipVia.SelectedValue, out SV.Framework.Common.LabelGenerator.ShipMethods shipMethods);
                                labelInfo.ShippingMethod = shipMethods;
                            }

                            labelInfo.ShippingType = SV.Framework.Common.LabelGenerator.ShipType.Ship;


                            if (ddlShape.SelectedIndex > 0)
                            {
                                Enum.TryParse(ddlShape.SelectedValue, out SV.Framework.Common.LabelGenerator.ShipPackageShape shipPackage);
                                labelInfo.PackageShape = shipPackage;
                            }
                            else
                                labelInfo.PackageShape = SV.Framework.Common.LabelGenerator.ShipPackageShape.Letter;

                            
                            string Package = ddlShape.SelectedValue;
                            string ShipVia = ddlShipVia.SelectedValue;
                            decimal Weight = Convert.ToDecimal(txtWeight.Text);
                            string Comments = txtShipComments.Text.Trim();
                            decimal FinalPostage = 0;
                            bool IsManualTracking = chkTracking.Checked;
                            ShippingLabelInfo request = new ShippingLabelInfo();
                            request.FulfillmentNumber = labelInfo.FulfillmentNumber;
                            request.ShipmentType = shipmentType;

                            if (!IsManualTracking)
                            {
                                SV.Framework.Common.LabelGenerator.iPrintLabel response = shipAccess.PrintShippingLabel(labelInfo);

                                if (response != null && !string.IsNullOrWhiteSpace(response.TrackingNumber))
                                {
                                    request.ShippingLabelImage = response.ShippingLabelImage;
                                    request.TrackingNumber = response.TrackingNumber;
                                    FinalPostage = response.FinalPostage;
                                    //response.
                                    //request.LineItems = listitems;
                                    txtTrackingNumber.Text = response.TrackingNumber;
                                    ShippingLabelResponse setResponse = shippingLabelOperation.ShippingLabelUpdate(request, userId, listitems, ShipDate, Package, ShipVia, Weight, Comments, FinalPostage, IsManualTracking);
                                    lblShipItem.Text = "Label generated successfully";
                                    returnResult = 1;
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(response.PackageContent))
                                        lblShipItem.Text = response.PackageContent;
                                    else
                                        lblShipItem.Text = "Technical error please try again!";


                                }
                            }
                            else
                            {
                                request.ShippingLabelImage = null;
                                request.TrackingNumber = txtTrackingNumber.Text.Trim();
                                FinalPostage = 0;
                                //response.

                                ShippingLabelResponse setResponse = shippingLabelOperation.ShippingLabelUpdate(request, userId, listitems, ShipDate, Package, ShipVia, Weight, Comments, FinalPostage, IsManualTracking);
                                lblShipItem.Text = "Submitted successfully";
                                returnResult = 1;
                            }

                        }


                    }

                }
            }
            catch (Exception ex)
            {
                lblShipItem.Text = ex.Message;
            }
            return returnResult;
        }
        protected void btnGenLabel_Click(object sender, EventArgs e)
        {
            

            int poid = 0, userId = 0;
            double weight = 0;
            double.TryParse(txtWeight.Text.Trim(), out weight);
            if (weight == 0)
                weight = 1;
            lblESN.Text = string.Empty;
            lblTracking.Text = string.Empty;
            SV.Framework.Common.LabelGenerator.EndiciaPrintLabel labelInfo = new SV.Framework.Common.LabelGenerator.EndiciaPrintLabel();

            SV.Framework.Common.LabelGenerator.ShippingLabelOperation shipAccess = new SV.Framework.Common.LabelGenerator.ShippingLabelOperation();
            SV.Framework.Common.LabelGenerator.ShipInfo shipToInfo = new SV.Framework.Common.LabelGenerator.ShipInfo();
            SV.Framework.Common.LabelGenerator.ShipInfo shipFromInfo = new SV.Framework.Common.LabelGenerator.ShipInfo();
            

            try
            {
                string shipmentType = "S";
                if (ViewState["poid"] != null)
                {
                    poid = Convert.ToInt32(ViewState["poid"]);

                    if (Session["UserID"] != null)
                    {
                        int.TryParse(Session["UserID"].ToString(), out userId);
                    }

                    PurchaseOrders pos = Session["POS"] as PurchaseOrders;

                    if (pos != null && pos.PurchaseOrderList.Count > 0)
                    {

                        var po = (from item in pos.PurchaseOrderList where item.PurchaseOrderID.Equals(poid) select item).ToList();
                        if (po != null && po.Count > 0)
                        {
                            labelInfo.FulfillmentNumber = po[0].PurchaseOrderNumber;
                            labelInfo.LabelPrintDateTime = DateTime.Today;
                            //shipToInfo
                            shipToInfo.ShipToAddress = po[0].Shipping.ShipToAddress;
                            shipToInfo.ShipToAddress2 = po[0].Shipping.ShipToAddress2;
                            shipToInfo.ContactName = po[0].Shipping.ContactName;
                            shipToInfo.ShipToCity = po[0].Shipping.ShipToCity;
                            shipToInfo.ShipToState = po[0].Shipping.ShipToState;
                            shipToInfo.ShipToZip = po[0].Shipping.ShipToZip;
                            shipToInfo.ShipToAttn = po[0].Shipping.ShipToAttn ?? po[0].Shipping.ContactName;
                            shipToInfo.ContactPhone = po[0].Shipping.ContactPhone;
                            labelInfo.ShipTo = shipToInfo;

                            //ship From Info
                            shipFromInfo.ShipToAddress = shipFromAddress;
                            shipFromInfo.ShipToAddress2 = "";
                            shipFromInfo.ContactName = shipFromContactName;
                            shipFromInfo.ShipToCity = shipFromCity;
                            shipFromInfo.ShipToState = shipFromState;
                            shipFromInfo.ShipToZip = shipFromZip;
                            shipFromInfo.ShipToAttn = shipFromContactName;
                            shipFromInfo.ShipToCountry = shipFromCountry;
                            shipFromInfo.ContactPhone = shipFromPhone;

                            labelInfo.ShipFrom = shipFromInfo;
                            //
                            labelInfo.PackageWeight = new SV.Framework.Common.LabelGenerator.Weight { units = "ounces", value = weight };
                            labelInfo.PackageContent = "Description";

                            //labelInfo.ShippingMethod = SV.Framework.Common.LabelGenerator.ShipMethods.Priority;
                           // if (ddlShipVia.SelectedIndex > 0)
                            {
                                Enum.TryParse(po[0].Tracking.ShipToBy, out SV.Framework.Common.LabelGenerator.ShipMethods shipMethods);
                                labelInfo.ShippingMethod = shipMethods;
                            }

                            labelInfo.ShippingType = SV.Framework.Common.LabelGenerator.ShipType.Ship;


                            if (ddlShape.SelectedIndex > 0)
                            {
                                Enum.TryParse(ddlShape.SelectedValue, out SV.Framework.Common.LabelGenerator.ShipPackageShape shipPackage);
                                labelInfo.PackageShape = shipPackage;
                            }
                            else
                                labelInfo.PackageShape = SV.Framework.Common.LabelGenerator.ShipPackageShape.Flat;


                            SV.Framework.Common.LabelGenerator.iPrintLabel response = shipAccess.PrintShippingLabel(labelInfo);

                            if (response != null && !string.IsNullOrWhiteSpace(response.TrackingNumber))
                            {
                                ShippingLabelInfo request = new ShippingLabelInfo();
                                request.FulfillmentNumber = labelInfo.FulfillmentNumber;
                                request.ShipmentType = shipmentType;
                                request.ShippingLabelImage = response.ShippingLabelImage;
                                request.TrackingNumber = response.TrackingNumber;
                                txtTrackingNumber.Text = response.TrackingNumber;
                                //ShippingLabelResponse setResponse = ShippingLabelOperation.ShippingLabelUpdate(request, userId);
                                lblShipItem.Text = "Label generated successfully";
                            }
                            else
                                lblShipItem.Text = "Technical error please try again!";

                        }


                    }

                }
            }
            catch (Exception ex)
            {
                lblShipItem.Text = ex.Message;
            }
        }

        public string ContainerID { get;set; }
        //protected void lnkESN_Command(object sender, CommandEventArgs e)
        //{
        //    lblSKUESN.Text = string.Empty;
        //    int podID = 0;
        //    try
        //    {
                
        //        podID = Convert.ToInt32(e.CommandArgument);

        //        List<POEsn> esnList = FulfillmentOperations.GetSKUESNList(podID);

        //        if (esnList != null && esnList.Count > 0)
        //        {
        //            ContainerID = esnList[0].ContainerID;
        //            rptSKUESN.DataSource = esnList;
        //            rptSKUESN.DataBind();
        //        }
        //        else
        //        {
        //            rptSKUESN.DataSource = null;
        //            rptSKUESN.DataBind();
        //            lblSKUESN.Text = "No records found";
        //        }

        //        RegisterStartupScript("jsUnblockDialog", "unblockSKUESNDialog();");
        //    }
        //    catch (Exception ex)
        //    {
        //        lblSKUESN.Text = ex.Message;
        //    }
        //}
        ////protected void btnESNDelete_Click(object sender, EventArgs e)
        //{
        //    lblPOA.Text = string.Empty;
        //    string pod_ids = string.Empty;
        //    bool flag = false;
        //    foreach(GridViewRow row in gvPOA.Rows)
        //    {
        //        HiddenField hdPODID = (HiddenField)row.FindControl("hdPODID");
        //        CheckBox chkDel = (CheckBox)row.FindControl("chkDel");
        //        if(chkDel.Checked)
        //        {
        //            if (string.IsNullOrEmpty(pod_ids))
        //                pod_ids = hdPODID.Value;
        //            else
        //                pod_ids = pod_ids + "," + hdPODID.Value;

        //            flag = true;
        //        }
        //    }
        //    if(flag)
        //    {
        //        int returnResult = FulfillmentOperations.FulifillmentSKUsESNDelete(pod_ids);
        //        if (returnResult == 1)
        //        {
        //            if (ViewState["poid"] != null)
        //            {
        //                int po_id = Convert.ToInt32(ViewState["poid"]);
        //                BindProvisioning(po_id);
        //            }
        //                lblPOA.Text = "Unassigned successfully";

        //        }
        //        else
        //            lblPOA.Text = "ESNs not deleted";
        //    }
        //    else
        //    {

        //        lblPOA.Text = "Please select atleast one SKU";
        //    }
        //}

        protected void btnPckSlip_Click(object sender, EventArgs e)
        {
            SV.Framework.Fulfillment.PackingslipOperation packingslipOperation = SV.Framework.Fulfillment.PackingslipOperation.CreateInstance<SV.Framework.Fulfillment.PackingslipOperation>();

            SV.Framework.LabelGenerator.PurchaseOrderInfo poInfo = new SV.Framework.LabelGenerator.PurchaseOrderInfo();
            SV.Framework.LabelGenerator.ProductModel productModel = null;
            List<SV.Framework.LabelGenerator.ProductModel> productList = null;
            SV.Framework.LabelGenerator.PackingSlipOperation operation = new SV.Framework.LabelGenerator.PackingSlipOperation();
            avii.Classes.UserInfo objUserInfo = (avii.Classes.UserInfo)Session["userInfo"];
            int poID = 0;
            try
            {
                if(ViewState["poid"]!= null)
                {
                    poID = Convert.ToInt32(ViewState["poid"]);
                }
                PurchaseOrders purchaseOrders = null;
                if (Session["POS"]!=null)
                    purchaseOrders = Session["POS"] as PurchaseOrders;

                List<BasePurchaseOrderItem> purchaseOrderItemList = new List<BasePurchaseOrderItem>();
                List<BasePurchaseOrder> purchaseOrderList = purchaseOrders.PurchaseOrderList;
                if (Session["poitems"] != null)
                    purchaseOrderItemList = Session["poitems"] as List<BasePurchaseOrderItem>;
                if (poID > 0 && purchaseOrders != null && purchaseOrderItemList != null && purchaseOrderItemList.Count > 0)
                {

                    var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(poID) select item).ToList();
                    if (poInfoList != null && poInfoList.Count > 0)
                    {
                        productList = new List<SV.Framework.LabelGenerator.ProductModel>();
                        
                        poInfo.PurchaseOrderNumber = poInfoList[0].CustomerOrderNumber;
                        poInfo.SalesOrder = poInfoList[0].PurchaseOrderNumber;
                        poInfo.DocumentDate = poInfoList[0].PurchaseOrderDate.ToString("MM-dd-yyyy");
                        poInfo.ReqShippingDate = poInfoList[0].RequestedShipDate.ToString("MM-dd-yyyy");
                        //Ship From

                        if(poInfoList[0].CompanyLogo == "redlogo.png")
                            poInfo.CompanyName = shipFromContactName2;
                        else
                            poInfo.CompanyName = shipFromContactName;
                        poInfo.CompanyName = poInfoList[0].CustomerName;

                        poInfo.AddressLine1 = shipFromAddress;
                        poInfo.AddressLine2 = "";
                        poInfo.City = shipFromCity;
                        poInfo.State = shipFromState;
                        poInfo.ZipCode = shipFromZip;
                        poInfo.Country = shipFromCountry;
                        //LOGO
                        poInfo.CompanyLogo = Server.MapPath("~/img/" + poInfoList[0].CompanyLogo.Replace(".","2."));
                       // poInfo.CompanyLogo = Server.MapPath("~/img/fplogo2.png");
                        //Ship To
                        poInfo.CustomerName = poInfoList[0].Shipping.ContactName;
                        poInfo.ShippingAddressLine1 = poInfoList[0].Shipping.ShipToAddress; 
                        poInfo.ShippingAddressLine2 = poInfoList[0].Shipping.ShipToAddress2;
                        poInfo.ShippingCity = poInfoList[0].Shipping.ShipToCity; 
                        poInfo.ShippingState = poInfoList[0].Shipping.ShipToState; 
                        poInfo.ShippingZipCode = poInfoList[0].Shipping.ShipToZip; 
                        poInfo.ShippingCountry = "USA"; 
                        poInfo.ShippingMethod = poInfoList[0].Tracking.ShipToBy;
                        poInfo.CustomerId = poInfoList[0].StoreID;
                        poInfo.DateTimePrinted = DateTime.Now.ToString("MM-dd-yyyy hh:mm tt");
                        poInfo.PackingSlip = "pck"+ poID.ToString();
                        poInfo.Page = 1;

                        if (objUserInfo != null)
                            poInfo.WhoPrinted = objUserInfo.UserName;
                        foreach (BasePurchaseOrderItem poItem in purchaseOrderItemList)
                        {
                            productModel = new SV.Framework.LabelGenerator.ProductModel();
                            productModel.Description = poItem.ItemCode;
                            productModel.ItemNumber = poItem.ItemCode;
                            productModel.QtyShipped = Convert.ToInt32(poItem.Quantity);
                            productModel.UnitsBO = productModel.QtyShipped;
                            productModel.UnitsOrdered = productModel.QtyShipped;
                            productModel.UnitsShipped = productModel.QtyShipped;
                            productList.Add(productModel);
                        }
                        poInfo.ProductsList = productList;
                    }

                    var memSt = operation.ExportToPDF(poInfo);
                    if (memSt != null)
                    {
                        string fileType = ".pdf";
                        string filename = DateTime.Now.Ticks + fileType;
                        Response.Clear();
                        //Response.ContentType = "application/pdf";
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                        Response.Buffer = true;
                        Response.Clear();
                        var bytes = memSt.ToArray();
                        Response.OutputStream.Write(bytes, 0, bytes.Length);
                        Response.OutputStream.Flush();


                        packingslipOperation.PackingSlipInsertUpdate(poID, poInfo.PackingSlip);
                    }
                    else
                        lblViewPO.Text = "Technical error!";

                }
            }
            catch (Exception ex)
            {
                lblViewPO.Text = ex.Message;
            }

        }

        protected void btnContainerSlip_Click(object sender, EventArgs e)
        {
            SV.Framework.Fulfillment.ContainerSlipOperation containerSlipOperation = SV.Framework.Fulfillment.ContainerSlipOperation.CreateInstance<SV.Framework.Fulfillment.ContainerSlipOperation>();

            SV.Framework.LabelGenerator.ContainerLabelOperation operation = new SV.Framework.LabelGenerator.ContainerLabelOperation();
            avii.Classes.UserInfo objUserInfo = (avii.Classes.UserInfo)Session["userInfo"];
            lblViewPO.Text = string.Empty;
            bool IsContainerExists = true;
            int poID = 0;
            try
            {
                if (ViewState["poid"] != null)
                {
                    poID = Convert.ToInt32(ViewState["poid"]);
                }
                List<SV.Framework.LabelGenerator.ContainerModel> containers = new List<SV.Framework.LabelGenerator.ContainerModel>();
                SV.Framework.LabelGenerator.ContainerModel container = null;
                List<ContainerModel> containerLabels = containerSlipOperation.GetContainerLabelInfo(poID);
                if(containerLabels != null && containerLabels.Count > 0)
                {

                    foreach(ContainerModel item in containerLabels)
                    {
                        container.AddressLine1 = item.AddressLine1;
                        container.AddressLine2 = item.AddressLine2;
                        container.ShippingAddressLine1 = item.ShippingAddressLine1;
                        container.ShippingAddressLine2 = item.ShippingAddressLine2;
                        container.ShippingCity = item.ShippingCity;
                        container.ShippingCountry = item.ShippingCountry;
                        container.ShippingState = item.ShippingState;
                        container.ShippingZipCode = item.ShippingZipCode;
                        container.State = item.State;
                        container.ESNCount = item.ESNCount;
                        container.Carrier = item.Carrier;
                        container.Casepack = item.Casepack;
                        container.City = item.City;
                        container.CompanyName = item.CompanyName;
                        container.ContainerCount = item.ContainerCount;
                        container.ContainerNumber = item.ContainerNumber;
                        container.Country = item.Country;
                        container.CustomerName = item.CustomerName;
                        container.DPCI = item.DPCI;
                        container.PoNumber = item.PoNumber;
                        container.PostalCode = item.PostalCode;
                        container.ZipCode = item.ZipCode;
                        containers.Add(container);
                        if (string.IsNullOrWhiteSpace(item.ContainerNumber))
                        {
                            IsContainerExists = false;
                            lblViewPO.Text = "Container ID not generated yet!";
                            //return;
                           // ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "alert('Container ID not generated yet!')", false);
                        }

                        //else if (item.Casepack == "0")
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Provisioning is not done yet!')</script>", false);
                        //    lblViewPO.Text = "Provisioning is not done yet!";
                        //}
                    }
                    if (IsContainerExists)
                    {
                        var memSt = operation.ExportToPDF(containers);
                        if (memSt != null)
                        {
                            string fileType = ".pdf";
                            string filename = DateTime.Now.Ticks + fileType;
                            Response.Clear();
                            //Response.ContentType = "application/pdf";
                            Response.ContentType = "application/octet-stream";
                            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                            Response.Buffer = true;
                            Response.Clear();
                            var bytes = memSt.ToArray();
                            Response.OutputStream.Write(bytes, 0, bytes.Length);
                            Response.OutputStream.Flush();


                            //PackingslipOperation.PackingSlipInsertUpdate(poID, poInfo.PackingSlip);
                        }
                        else
                            lblViewPO.Text = "Technical error!";
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Container ID not generated yet!')</script>", false);

                }
                else
                {
                    lblViewPO.Text = "Provisioning is not done yet!";
                }
            }
            catch (Exception ex)
            {
                lblViewPO.Text = ex.Message;
            }
        }

        protected void btnPackSlipAll_Click(object sender, EventArgs e)
        {
            SV.Framework.Fulfillment.PackingslipOperation packingslipOperation = SV.Framework.Fulfillment.PackingslipOperation.CreateInstance<SV.Framework.Fulfillment.PackingslipOperation>();

            List<SV.Framework.LabelGenerator.PurchaseOrderInfo> poList = new List<SV.Framework.LabelGenerator.PurchaseOrderInfo>();

            SV.Framework.LabelGenerator.PurchaseOrderInfo poInfo = null;
            SV.Framework.LabelGenerator.ProductModel productModel = null;
            List<SV.Framework.LabelGenerator.ProductModel> productList = null;
            SV.Framework.LabelGenerator.PackingSlipOperation operation = new SV.Framework.LabelGenerator.PackingSlipOperation();
            avii.Classes.UserInfo objUserInfo = (avii.Classes.UserInfo)Session["userInfo"];
            PurchaseOrders purchaseOrders = null;
            List<BasePurchaseOrderItem> purchaseOrderItemList = null;
            List<BasePurchaseOrder> purchaseOrderList = null;
            bool IsSeleted = false;
            int poID = 0;
            try
            {
                //if (ViewState["poid"] != null)
                //{
                //    poID = Convert.ToInt32(ViewState["poid"]);
                //}
                List<TrackingDetail> trackingList = new List<TrackingDetail>();

                foreach (GridViewRow row in gvPOQuery.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");

                    if (chk.Checked)
                    {
                        poID = Convert.ToInt32(gvPOQuery.DataKeys[row.RowIndex].Value);
                        IsSeleted = true;
                        poInfo = new SV.Framework.LabelGenerator.PurchaseOrderInfo();

                        if (Session["POS"] != null)
                            purchaseOrders = Session["POS"] as PurchaseOrders;

                        purchaseOrderItemList = new List<BasePurchaseOrderItem>();
                        purchaseOrderList = purchaseOrders.PurchaseOrderList;
                        //if (Session["poitems"] != null)
                        purchaseOrderItemList = purchaseOrderOperation.GetPurchaseOrderItemsAndTrackingList(poID, out trackingList);
                        if (poID > 0 && purchaseOrders != null && purchaseOrderItemList != null && purchaseOrderItemList.Count > 0)
                        {

                            var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(poID) select item).ToList();
                            if (poInfoList != null && poInfoList.Count > 0)
                            {
                                productList = new List<SV.Framework.LabelGenerator.ProductModel>();

                                poInfo.PurchaseOrderNumber = poInfoList[0].CustomerOrderNumber;
                                poInfo.SalesOrder = poInfoList[0].PurchaseOrderNumber;
                                poInfo.DocumentDate = poInfoList[0].PurchaseOrderDate.ToString("MM-dd-yyyy");
                                poInfo.ReqShippingDate = poInfoList[0].RequestedShipDate.ToString("MM-dd-yyyy");
                                //Ship From

                                if (poInfoList[0].CompanyLogo == "redlogo.png")
                                    poInfo.CompanyName = shipFromContactName2;
                                else
                                    poInfo.CompanyName = shipFromContactName;

                                poInfo.CompanyName = poInfoList[0].CustomerName;


                                poInfo.AddressLine1 = shipFromAddress;
                                poInfo.AddressLine2 = "";
                                poInfo.City = shipFromCity;
                                poInfo.State = shipFromState;
                                poInfo.ZipCode = shipFromZip;
                                poInfo.Country = shipFromCountry;
                                //LOGO
                                poInfo.CompanyLogo = Server.MapPath("~/img/" + poInfoList[0].CompanyLogo.Replace(".", "2."));
                                // poInfo.CompanyLogo = Server.MapPath("~/img/fplogo2.png");
                                //Ship To
                                poInfo.CustomerName = poInfoList[0].Shipping.ContactName;
                                poInfo.ShippingAddressLine1 = poInfoList[0].Shipping.ShipToAddress;
                                poInfo.ShippingAddressLine2 = poInfoList[0].Shipping.ShipToAddress2;
                                poInfo.ShippingCity = poInfoList[0].Shipping.ShipToCity;
                                poInfo.ShippingState = poInfoList[0].Shipping.ShipToState;
                                poInfo.ShippingZipCode = poInfoList[0].Shipping.ShipToZip;
                                poInfo.ShippingCountry = "USA";
                                poInfo.ShippingMethod = poInfoList[0].Tracking.ShipToBy;
                                poInfo.CustomerId = poInfoList[0].StoreID;
                                poInfo.DateTimePrinted = DateTime.Now.ToString("MM-dd-yyyy hh:mm tt");
                                poInfo.PackingSlip = "pck" + poID.ToString();
                                poInfo.Page = 1;

                                if (objUserInfo != null)
                                    poInfo.WhoPrinted = objUserInfo.UserName;
                                foreach (BasePurchaseOrderItem poItem in purchaseOrderItemList)
                                {
                                    productModel = new SV.Framework.LabelGenerator.ProductModel();
                                    productModel.Description = poItem.ItemCode;
                                    productModel.ItemNumber = poItem.ItemCode;
                                    productModel.QtyShipped = Convert.ToInt32(poItem.Quantity);
                                    productModel.UnitsBO = productModel.QtyShipped;
                                    productModel.UnitsOrdered = productModel.QtyShipped;
                                    productModel.UnitsShipped = productModel.QtyShipped;
                                    productList.Add(productModel);
                                }
                                poInfo.ProductsList = productList;
                                poList.Add(poInfo);
                            }
                            //PackingslipOperation.PackingSlipInsertUpdate(poID, poInfo.PackingSlip);

                        }
                    }
                }
                if (!IsSeleted)
                {
                    lblMsg.Text = "Please select fulfillment number";
                    return;
                }
                var memSt = operation.ExportToPDFNew(poList);
                if (memSt != null)
                {
                    string fileType = ".pdf";
                    string filename = DateTime.Now.Ticks + fileType;
                    Response.Clear();
                    //Response.ContentType = "application/pdf";
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                    Response.Buffer = true;
                    Response.Clear();
                    var bytes = memSt.ToArray();
                    Response.OutputStream.Write(bytes, 0, bytes.Length);
                    Response.OutputStream.Flush();

                    foreach (GridViewRow gvrow in gvPOQuery.Rows)
                    {
                        CheckBox chk1 = (CheckBox)gvrow.FindControl("chk");

                        if (chk1.Checked)
                        {
                            poID = Convert.ToInt32(gvPOQuery.DataKeys[gvrow.RowIndex].Value);
                            packingslipOperation.PackingSlipInsertUpdate(poID, "pck" + poID.ToString());
                        }
                    }
                }
                else
                    lblMsg.Text = "Technical error!";

            }
            catch (Exception ex)
            {
                lblViewPO.Text = ex.Message;
            }

        }

        protected void imgAddLineItem_Command(object sender, CommandEventArgs e)
        {
            int poID = Convert.ToInt32(e.CommandArgument);
            Session["poid"] = poID;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('AddNewItem.aspx')</script>", false);

        }
        

        protected void btnASN_Click(object sender, EventArgs e)
        {
            bool IsSeleted = false;
            string poIDs = "";
            int fileSequence = 0;
            string fileSufix = "";

            try
            {
                List<TrackingDetail> trackingList = new List<TrackingDetail>();
                StringBuilder sb = new StringBuilder();
                foreach (GridViewRow row in gvPOQuery.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chk");
                    if (chk.Checked)
                    {
                        //poID = Convert.ToString(gvPOQuery.DataKeys[row.RowIndex].Value);
                        sb.Append(Convert.ToString(gvPOQuery.DataKeys[row.RowIndex].Value) + ",");
                        IsSeleted = true;  
                    }
                }
                if (!IsSeleted)
                {
                    lblMsg.Text = "Please select fulfillment number";
                    return;
                }
                poIDs = sb.ToString();
                poIDs = poIDs.Substring(0, poIDs.Length - 1);
                if (!string.IsNullOrEmpty(poIDs))
                {
                    List<SV.Framework.Fulfillment.FulfillmentOrderASN> fulfillmentOrderASNs = SV.Framework.Fulfillment.FulfillmentOrderASNOperation.GetPurchaseOrderASNFile(poIDs);
                    if(fulfillmentOrderASNs != null && fulfillmentOrderASNs.Count > 0)
                    {
                        System.Text.StringBuilder sbcsv = new System.Text.StringBuilder();
                        //ARZM,PO,FO,Line,Model,Product Description,Qty,Cartons,Date Shipped,City,State,Carrier,Tracking/Pro Number,Supplier Name,Supplier AddressSupplier City,Supplier City,Supplier State,Pallets,Weight,UEDF File Name,UEDF Date Time
                        //string string2CSV = "";// "BATCH,ESN,MeidHex,MeidDec,Location,MSL,OTKSL,SerialNumber" + Environment.NewLine;
                        sbcsv.Append("ARZM,PO,FO,Line,Model,Product Description,Qty,Cartons,Date Shipped,City,State,Carrier,Tracking/Pro Number,Supplier Name,Supplier AddressSupplier City,Supplier City,Supplier State,Pallets,Weight,UEDF File Name,UEDF Date Time" + Environment.NewLine);
                        foreach (SV.Framework.Fulfillment.FulfillmentOrderASN item in fulfillmentOrderASNs)
                        {
                            fileSequence = item.FileSequence;

                            sbcsv.Append(item.ARZM + "," + item.PO + "," + item.FO + "," + item.Line + "," + item.Model + "," + 
                                item.ProductDescription + "," + item.Qty + "," + item.Cartons + "," + item.DateShipped + "," +
                                item.City + "," +item.State + "," +item.Carrier + "," +item.TrackingNumber + "," + 
                                item.SupplierName + "," + item.SupplierAddress + "," +item.SupplierCity + "," +item.SupplierState + "," + 
                                item.Pallets + "," +item.Weight + "," +item.UEDF_FileName + "," +item.UEDF_DateTime
                                + Environment.NewLine);
                        }

                        string string2CSV = sbcsv.ToString();
                        if (fileSequence < 1000)
                        {
                            fileSufix = fileSequence.ToString();
                            if (fileSufix.Length == 1)
                                fileSufix = "000" + fileSufix;
                            else if (fileSufix.Length == 2)
                                fileSufix = "00" + fileSufix;
                            else if (fileSufix.Length == 3)
                                fileSufix = "0" + fileSufix;

                        }
                        string filename = "ASN-1LAN" + fileSufix + ".csv";
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition", "attachment;filename= " + filename);
                        Response.Charset = "";
                        Response.ContentType = "application/text";
                        Response.Output.Write(string2CSV);
                        Response.Flush();
                        Response.End();

                    }
                }
                else
                {
                    lblMsg.Text = "Please select fulfillment number";

                }


            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void imgDoc_Command(object sender, CommandEventArgs e)
        {
            string poinfo = Convert.ToString(e.CommandArgument);


            Session["poinfo"] = poinfo;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('../docupload/DocumentUpload.aspx')</script>", false);

        }
    }
}