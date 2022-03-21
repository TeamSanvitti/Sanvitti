using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Linq;
using System.Web.UI.WebControls;
//using avii.Classes;
using System.Web.UI;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Fulfillment;


namespace avii.Admin
{
    public partial class FulfillmentUpload : System.Web.UI.Page
    {
        private string fileStoreLocation = "~/UploadedData/Fulfillment/";
        private const char DELIMITER = ',';

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
            if (!this.IsPostBack)
            {
                // trStore.Visible = false;
                if (Session["adm"] != null)
                {
                    BindCustomer();
                    trCustomer.Visible = true;
                }
                else
                {
                    avii.Classes.UserInfo objUserInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    ViewState["CompanyAccountNumber"] = objUserInfo.CompanyAccountNumber;
                    BindUserStores(objUserInfo.CompanyAccountNumber);
                    trCustomer.Visible = false;
                }
                BindShipBy();
                ReadOlnyAccess();
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

        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyAccountNumber";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CompanyAccountNumber = string.Empty;
            ddlStoreID.Items.Clear();

            if (dpCompany.SelectedIndex > 0)
            {
                CompanyAccountNumber = dpCompany.SelectedValue;
                BindUserStores(CompanyAccountNumber);
            }
            else
            {
              //  trStore.Visible = false;

            }
        }
        private void BindUserStores(string CompanyAccountNumber)
        {
            List<SV.Framework.Admin.StoreLocation> storeList = SV.Framework.Admin.UserStoreOperation.GetUserStoreLocationList(CompanyAccountNumber);
            
            if (storeList != null && storeList.Count > 0)
            {
                Session["userstore"] = storeList;
                ddlStoreID.DataSource = storeList;
                ddlStoreID.DataValueField = "StoreID";
                ddlStoreID.DataTextField = "CompositeKeyStoreIdStoreName";
                ddlStoreID.DataBind();
                System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("", "");
                ddlStoreID.Items.Insert(0, item);

                //trStore.Visible = true;
            }
            else
            {
               // trStore.Visible = false;
                lblMsg.Text = "No store assigned to this customer, please contact administrator to get more infomration.";
            }
        }
        private void BindShipBy()
        {
            PurchaseOrder purchaseOrderOperation = PurchaseOrder.CreateInstance<PurchaseOrder>();
            List<ShipBy> shipViaList = purchaseOrderOperation.GetShipByList();
            Session["shipViaList"] = shipViaList;
            dpShipBy.DataSource = avii.Classes.PurchaseOrder.GetShipByList();
            dpShipBy.DataTextField = "ShipByText";
            dpShipBy.DataValueField = "ShipByCode";
            dpShipBy.DataBind();
            System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("", "");
            dpShipBy.Items.Insert(0, item);

        }
        //protected void btnShipVia_Click(object sender, EventArgs e)
        //{

        //    RegisterStartupScript("jsUnblockDialog", "unblockShipViaDialog();");
        //}
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        protected void rpt_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkPoNum = (LinkButton)e.Item.FindControl("lnkPoNum");
                lnkPoNum.OnClientClick = "openDialogAndBlock('Fulfillment Detail', '" + lnkPoNum.ClientID + "')";

            }
        }
        protected void lnkPoNum_OnCommand(object sender, CommandEventArgs e)
        {
            //if (dpCompany.SelectedIndex > 0)
            {
                string poNum = e.CommandArgument.ToString();
                string companyAccountNumber = string.Empty;//dpCompany.SelectedValue;
                if (!string.IsNullOrEmpty(poNum))
                {
                    Control tmp1 = LoadControl("../controls/POD.ascx");
                    avii.Controls.POD ctlPODetails = tmp1 as avii.Controls.POD;
                    pnlPO.Controls.Clear();
                    ctlPODetails.BindPO(poNum, companyAccountNumber);

                    pnlPO.Controls.Add(ctlPODetails);
                    RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
                }
            }
            //else
            //    lblMsg.Text = "Select a customer!";
        }
        protected void btnViewAssignedPos_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;

            if (Session["pos"] != null)
            {
                List<clsPurchaseOrder> posList = Session["pos"] as List<clsPurchaseOrder>;
                rptPO.DataSource = posList;
                rptPO.DataBind();
                lblCount.Text = "Total count: " + posList.Count;

            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            PurchaseOrder purchaseOrderOperation = PurchaseOrder.CreateInstance<PurchaseOrder>();
            string comment = txtComment.Text;
            string filename = string.Empty;

            if (ViewState["filename"] != null)
                filename = ViewState["filename"] as string;

            Session["pos"] = null;
            //lblMsg.CssClass = "errormessage";
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;

            int userID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;

            }
            if (Session["polist"] != null)
            {
                List<clsPurchaseOrder> posList = new List<clsPurchaseOrder>();
                List<clsPurchaseOrder> poToAdd = null;

                List<clsPurchaseOrder> poList = Session["polist"] as List<clsPurchaseOrder>;
                int n = 0;
                int poRecordCount = 0;
                int returnValue = 0;
                string poErrorMessage, poStoreIDErrorMessage, poShipViaErrorMessage, poSKUsErrorMessage, errorMessage;
                string companyAccountNumber = string.Empty;
                if (Session["adm"] == null)
                    companyAccountNumber = ViewState["CompanyAccountNumber"] as string;
                else
                    companyAccountNumber = dpCompany.SelectedValue;
                errorMessage = poErrorMessage = poStoreIDErrorMessage = poShipViaErrorMessage = poSKUsErrorMessage = string.Empty;
                double totalChunk = 0;
                StringBuilder sbDuplicatePoError = new StringBuilder();
                StringBuilder sbStoreIDError = new StringBuilder();
                StringBuilder sbSKUsError = new StringBuilder();
                StringBuilder sbShipViaError = new StringBuilder();
                try
                {

                    totalChunk = (double)poList.Count / 20;
                    n = Convert.ToInt16(Math.Ceiling(totalChunk));
                    int poCount = 20;
                    
                    //var esnToUpload;

                    for (int i = 0; i < n; i++)
                    {
                        poToAdd = new List<clsPurchaseOrder>();
                        if (poList.Count < 20)
                            poCount = poList.Count;
                        var poToUpload = (from item in poList.Take(poCount) select item).ToList();

                        errorMessage = poErrorMessage = poStoreIDErrorMessage = poShipViaErrorMessage = poSKUsErrorMessage = string.Empty;
                        //Upload POs
                        poToAdd = purchaseOrderOperation.SaveBulkPurchaseOrderDB(poToUpload, companyAccountNumber, userID, "U", filename, comment, out poRecordCount, out returnValue, out errorMessage);

                        if (string.IsNullOrEmpty(errorMessage))
                        {
                            if (returnValue == -2)
                            {
                                lblMsg.Text = "User not assigned to this customer please contact to langlobal administrator";
                                return;
                            }
                            
                            poErrorMessage = poStoreIDErrorMessage = poShipViaErrorMessage = poSKUsErrorMessage = string.Empty;
                            
                            posList.AddRange(poToAdd);
                            if (i != 0)
                                poRecordCount += poRecordCount;

                            poList.RemoveRange(0, poCount);
                        }
                        else
                        {
                            lblMsg.Text = errorMessage;
                            return;
                        }
                    }

                    //if (returnValue == 0 && poRecordCount > 0 && sbDuplicatePoError.Length == 0 && sbStoreIDError.Length == 0 && sbShipViaError.Length == 0 && sbSKUsError.Length == 0)
                    
                    if (returnValue == 0 && poRecordCount > 0 )
                    {
                        lblMsg.Text = "Uploaded successfully <br /> Record count: " + poRecordCount;
                        pnlSubmit.Visible = false;
                        Session["pos"] = posList;
                        rptPO.DataSource = null;
                        rptPO.DataBind();
                        Session["polist"] = null;
                        btnSubmit.Visible = false;
                        //pnlPO.Visible = false;
                        btnUpload.Visible = false;
                        btnViewPO.Visible = true;
                        lblCount.Text = string.Empty;
                    }
                    else
                    {
                        if (returnValue == -2)
                        {
                            lblMsg.Text = "User not assigned to this customer please contact to Lan Global administrator";
                            
                        }
                        else
                        {
                            string returnErrorMessage = string.Empty;
                            if (poRecordCount > 0)
                            {
                                if (sbDuplicatePoError.Length > 0)
                                {
                                    returnErrorMessage = "Partially Uploaded. <br /> " + sbDuplicatePoError + " Fulfillment already exists! "; ///+ "<br /> Record count: " + poRecordCount;
                                }
                                if (sbStoreIDError.Length > 0)
                                {
                                    if (returnErrorMessage == string.Empty)
                                        returnErrorMessage = "Partially Uploaded. <br /> Following Fulfillment order having invalid store " + sbStoreIDError;
                                    else
                                        returnErrorMessage = returnErrorMessage + "<br /> Following Fulfillment order having invalid store ID " + sbStoreIDError;
                                }
                                if (sbShipViaError.Length > 0)
                                {
                                    if (returnErrorMessage == string.Empty)
                                        returnErrorMessage = "Partially Uploaded. <br /> Following Fulfillment order having invalid ShipVia " + sbShipViaError;
                                    else
                                        returnErrorMessage = returnErrorMessage + "<br /> Following Fulfillment order having invalid ShipVia " + sbShipViaError;
                                }
                                if (sbSKUsError.Length > 0)
                                {
                                    if (returnErrorMessage == string.Empty)
                                        returnErrorMessage = "Partially Uploaded. <br /> Following Fulfillment order having invalid SKU " + sbSKUsError;
                                    else
                                        returnErrorMessage = returnErrorMessage + "<br /> Following Fulfillment order having invalid SKU " + sbSKUsError;
                                }

                                if (string.IsNullOrEmpty(returnErrorMessage))
                                    returnErrorMessage = "Uploaded successfully";
                                returnErrorMessage = returnErrorMessage + "<br /> Record count: " + poRecordCount;

                            }
                            else
                            {
                                
                                if (sbDuplicatePoError.Length > 0)
                                {
                                    returnErrorMessage = sbDuplicatePoError + " Fulfillment already exists! "; ///+ "<br /> Record count: " + poRecordCount;
                                }
                                if (sbStoreIDError.Length > 0)
                                {
                                    if (returnErrorMessage == string.Empty)
                                        returnErrorMessage = "Following Fulfillment order having invalid store " + sbStoreIDError;
                                    else
                                        returnErrorMessage = returnErrorMessage + "<br /> Following Fulfillment order having invalid store ID " + sbStoreIDError;
                                }
                                if (sbShipViaError.Length > 0)
                                {
                                    if (returnErrorMessage == string.Empty)
                                        returnErrorMessage = "Following Fulfillment order having invalid ShipVia " + sbShipViaError;
                                    else
                                        returnErrorMessage = returnErrorMessage + "<br /> Following Fulfillment order having invalid ShipVia " + sbShipViaError;
                                }
                                if (sbSKUsError.Length > 0)
                                {
                                    if (returnErrorMessage == string.Empty)
                                        returnErrorMessage = "Following Fulfillment order having invalid SKU " + sbSKUsError;
                                    else
                                        returnErrorMessage = returnErrorMessage + "<br /> Following Fulfillment order having invalid SKU " + sbSKUsError;
                                }
                                if (string.IsNullOrEmpty(returnErrorMessage))
                                    returnErrorMessage = "Fulfillment not uploaded";
                                returnErrorMessage = returnErrorMessage + "<br /> Record count: " + poRecordCount;

                            }
                            pnlSubmit.Visible = false;
                            Session["pos"] = posList;
                            rptPO.DataSource = null;
                            rptPO.DataBind();
                            Session["polist"] = null;
                            btnSubmit.Visible = false;
                            //pnlPO.Visible = false;
                            btnUpload.Visible = false;
                            btnViewPO.Visible = true;
                            lblCount.Text = string.Empty;
                            lblMsg.Text = returnErrorMessage;

                        }

                        //if (poRecordCount > 0 && !string.IsNullOrEmpty(poErrorMessage))
                        //{
                        //    lblMsg.Text = "Partially Updated. <br /> Fulfillment already exists " + poErrorMessage + "<br /> Record count: " + poRecordCount;
                        //    pnlSubmit.Visible = false;
                        //    Session["pos"] = posList;
                        //    rptPO.DataSource = null;
                        //    rptPO.DataBind();
                        //    Session["polist"] = null;
                        //    btnSubmit.Visible = false;
                        //    //pnlPO.Visible = false;
                        //    btnUpload.Visible = false;
                        //    btnViewPO.Visible = true;
                        //    lblCount.Text = string.Empty;
                        //}
                        //else if (poRecordCount == 0 && !string.IsNullOrEmpty(poErrorMessage))
                        //{
                        //    lblMsg.Text = "Fulfillment not found " + poErrorMessage;

                        //    pnlSubmit.Visible = false;
                        //    Session["pos"] = posList;
                        //    rptPO.DataSource = null;
                        //    rptPO.DataBind();
                        //    Session["polist"] = null;
                        //    btnSubmit.Visible = false;
                        //    //pnlPO.Visible = false;
                        //    btnUpload.Visible = true;
                        //    //btnViewPO.Visible = true;
                        //    lblCount.Text = string.Empty;
                        //}
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                }

            }
            else
            {
                lblMsg.Text = "session expire!";
            }

        }
        
        protected void btnValidatePOs_Click(object sender, EventArgs e)
        {
            PurchaseOrder purchaseOrderOperation = PurchaseOrder.CreateInstance<PurchaseOrder>();
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;

            if (Session["polist"] != null)
            {
                List<clsPurchaseOrder> poList = Session["polist"] as List<clsPurchaseOrder>;
                int poRecordCount = 0;
                int returnValue = 0;
                string poErrorMessage, poStoreIDErrorMessage, poShipViaErrorMessage, poSKUsErrorMessage, poStateErrorMessage, errorMessage;
                string companyAccountNumber = string.Empty;
                if (Session["adm"] == null)
                    companyAccountNumber = ViewState["CompanyAccountNumber"] as string;
                else
                    companyAccountNumber = dpCompany.SelectedValue;
                errorMessage = poErrorMessage = poStoreIDErrorMessage = poShipViaErrorMessage = poSKUsErrorMessage = poStateErrorMessage = string.Empty;
                
                try
                {
                    List<PurchaseOrderItem> PurchaseOrderItems = new List<PurchaseOrderItem>();
                    //var storeList = poList.Select(m => new StoreIDs { StoreID = m.StoreID }).Distinct().ToList();
                    //var shipViaList = poList.Select(m => new ShipVia { ShipThrough = m.ShipThrough }).Distinct().ToList();
                    var skusList = poList.Select(m => new { m.PurchaseOrderItems }).Distinct().ToList();
                    //var stateList = poList.Select(m => new avii.Classes.State { StateCode = m.Shipping.ShipToState }).Distinct().ToList();
                    foreach (clsPurchaseOrder po in poList)
                    {
                        PurchaseOrderItems.AddRange(po.PurchaseOrderItems);
                    }
                    var skuList = PurchaseOrderItems.Select(m => new FulfillmentSKU {SKU = m.ItemCode }).Distinct().ToList();

                    var poNumList = poList.Select(m => new FulfillmentNumber { FulfillmentOrder = m.PurchaseOrderNumber }).Distinct().ToList();
                    List<FulfillmentNumber> posList = new List<FulfillmentNumber>();

                    errorMessage = poErrorMessage = poStoreIDErrorMessage = poShipViaErrorMessage = poSKUsErrorMessage = poStateErrorMessage = string.Empty;
                    //validate POs
                    returnValue = purchaseOrderOperation.ValidateFulfillmentOrder(poNumList, skuList, companyAccountNumber, out poRecordCount, out poErrorMessage, out poStoreIDErrorMessage, out poShipViaErrorMessage, out poSKUsErrorMessage, out poStateErrorMessage, out errorMessage);

                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        if (returnValue == 0)
                        {
                            lblConfirm.Text = "Fulfillment file is ready to upload";
                            btnSubmit.Visible = true;
                            btnSubmit2.Visible = true;
                            pnlSubmit.Visible = true;
                            btnUpload.Visible = false;
                            btnValidate.Visible = false;
                            btnValidate2.Visible = false;


                        }
                        else
                        {
                            if (returnValue == -2)
                            {
                                lblMsg.Text = "User not assigned to this customer please contact to administrator";
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(poErrorMessage))
                                    errorMessage = poErrorMessage + " fulfillment order already exists!";

                                if (!string.IsNullOrEmpty(poStoreIDErrorMessage))
                                {
                                    if (string.IsNullOrEmpty(errorMessage))
                                        errorMessage = poStoreIDErrorMessage + " invalid store ID";
                                    else
                                        errorMessage = errorMessage + "<br />" + poStoreIDErrorMessage + " invalid store ID";
                                }                                if (!string.IsNullOrEmpty(poShipViaErrorMessage))
                                {
                                    if (string.IsNullOrEmpty(errorMessage))
                                        errorMessage = poShipViaErrorMessage + " invalid shipvia";
                                    else
                                        errorMessage = errorMessage + "<br />" + poShipViaErrorMessage + " invalid shipvia";
                                }
                                if (!string.IsNullOrEmpty(poSKUsErrorMessage))
                                {
                                    if (string.IsNullOrEmpty(errorMessage))
                                        errorMessage = poSKUsErrorMessage + " invalid sku";
                                    else
                                        errorMessage = errorMessage + "<br />" + poSKUsErrorMessage + " invalid sku";
                                }
                                if (!string.IsNullOrEmpty(poStateErrorMessage))
                                {
                                    if (string.IsNullOrEmpty(errorMessage))
                                        errorMessage = poStateErrorMessage + " invalid state code";
                                    else
                                        errorMessage = errorMessage + "<br />" + poStateErrorMessage + " invalid state code";
                                }
                                
                                lblMsg.Text = errorMessage;
                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = errorMessage;
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                }
            }
            else
            {
                lblMsg.Text = "session expire!";
            }
        }
        
        private void UploadPurchaseOrder()
        {
            //List<avii.Classes.StoreLocation> storeList = Session["userstore"] as List<avii.Classes.StoreLocation>;
            StringBuilder sbPOWriteError = new StringBuilder();
            //int companyID = 0;
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            lblConfirm.Text = string.Empty;

            string companyInfo = string.Empty;
            string companyAccountNumber = string.Empty;
            bool columnsIncorrectFormat = false, isRequired = false;
            string invalidColumns = string.Empty;
            bool IsFileSizeValid = true;
            List<SV.Framework.Admin.StoreLocation> userStoreList = (List<SV.Framework.Admin.StoreLocation>)Session["userstore"];

            
            if (Session["adm"] != null && dpCompany.SelectedIndex < 1)
            {
                lblMsg.Text = "Please select Company";
               // throw new Exception("Please select Company");
               
            }
            else
            {
                if (ddlPOType.SelectedIndex < 1)
                {
                    lblMsg.Text = "Please select fulfillment type";
                    return;
                }

                if(ddlPOType.SelectedValue == "B2B")
                {
                    if (ddlStoreID.SelectedIndex < 1)
                    {
                        lblMsg.Text = "Please select StoreID";
                        return;
                    }
                }
                if (dpShipBy.SelectedIndex < 1)
                {
                    lblMsg.Text = "Please select ship via";
                    return;
                }
                //bool saved = false;
                if (flnUpload.HasFile)
                {
                    //companyInfo = dpCompany.SelectedValue;
                    //string[] array = companyInfo.Split(',');
                    //companyID = Convert.ToInt32(array[0]);
                    //companyAccountNumber = array[1];
                    if (Session["adm"] == null)
                        companyAccountNumber = ViewState["CompanyAccountNumber"] as string;
                    else
                        companyAccountNumber = dpCompany.SelectedValue;
                    string fileName = UploadFile(out IsFileSizeValid);

                    if (IsFileSizeValid)
                    {
                        string extension = Path.GetExtension(flnUpload.PostedFile.FileName);
                        extension = extension.ToLower();
                        if (extension == ".csv")
                        {

                            if (!string.IsNullOrEmpty(fileName))
                            {
                                List<clsPurchaseOrder> poList = new List<clsPurchaseOrder>();
                                //int companyID = Convert.ToInt32(dpCompany.SelectedValue);
                                StringBuilder sbError = WritePurchaseOrder(fileName, ref poList, out columnsIncorrectFormat, out invalidColumns, out isRequired);
                                //if (lblMsg.Text != string.Empty)
                                //{

                                //}
                                //else 
                                if (poList != null && poList.Count > 0 && !columnsIncorrectFormat)
                                {
                                    rptPO.DataSource = poList;
                                    rptPO.DataBind();

                                }
                                if (isRequired)
                                {
                                    lblMsg.Text = "Missing data!";
                                }
                                else if (sbError != null && sbError.Length > 0)
                                {
                                    lblMsg.Text = sbError.ToString();
                                }
                                else if (poList != null && poList.Count > 0 && !columnsIncorrectFormat && !isRequired)
                                {
                                    rptPO.DataSource = poList;
                                    rptPO.DataBind();
                                    Session["polist"] = poList;
                                    lblCount.Text = "Record count: " + poList.Count;
                                    if (lblMsg.Text == string.Empty)
                                    {
                                        lblConfirm.Text = "Fulfillment order file is ready to validate";
                                        btnUpload.Visible = false;
                                        btnValidate.Visible = true;
                                        btnValidate2.Visible = true;

                                        btnSubmit.Visible = false;
                                        btnSubmit2.Visible = false;
                                        pnlSubmit.Visible = true;

                                    }
                                    else
                                    {
                                        btnUpload.Visible = true;
                                        btnSubmit.Visible = false;

                                        btnSubmit2.Visible = false;
                                        pnlSubmit.Visible = false;
                                    }

                                }
                                else
                                {

                                    rptPO.DataSource = null;
                                    rptPO.DataBind();
                                    Session["polist"] = null;
                                    lblCount.Text = string.Empty;
                                    if (columnsIncorrectFormat)
                                    {
                                        if (!string.IsNullOrEmpty(invalidColumns))
                                        {
                                            lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                        }
                                        else
                                        {
                                            lblMsg.Text = "File format not matching missing required columns";

                                        }
                                    }
                                    else
                                    {
                                        if(isRequired)
                                        {
                                            lblMsg.Text = "File format not matching, missing required columns";
                                        }
                                        else
                                            lblMsg.Text = "Uploaded file does not have any Purchase Order to save";
                                    }

                                }

                            }

                        }
                        else
                            lblMsg.Text = "Invalid file!";
                    }
                    else
                    {
                        //lblMsg.Text = "Invalid file!";
                    }
                }
                else
                {
                    lblMsg.Text = "Select a file!";
                }
            }

        }
        private bool ValidateStoreID(string StoreID, out string returnMessage)
        {
            bool invalid = false;
            returnMessage = string.Empty;
            List<SV.Framework.Admin.StoreLocation> userStoreList = (List<SV.Framework.Admin.StoreLocation>)Session["userstore"];

            var storeList = (from item in userStoreList where item.StoreID.Equals(StoreID) select item).ToList();
            if (storeList != null && storeList.Count > 0)
            {
                invalid = true;
            }
            else
            {
                returnMessage = StoreID + " invalid StoreID";
            }


            return invalid;
        }
        private bool ValidateShipVia(string ShipVia, out string returnMessage)
        {
            bool invalid = false;
            returnMessage = string.Empty;
            List<ShipBy> shipViaList = (List<ShipBy>)Session["shipViaList"];

            var shipmethodList = (from item in shipViaList where item.ShipByText.Equals(ShipVia) select item).ToList();
            if (shipmethodList != null && shipmethodList.Count > 0)
            {
                invalid = true;
            }
            else
            {
                returnMessage = ShipVia + " invalid shipment through";
            }


            return invalid;
        }
        private bool ValidateColumns(string[] headerArray, out string invalidColumns)
        {
            bool columnsIncorrectFormat = false;
            invalidColumns = string.Empty;
            //if (headerArray[0].Trim().ToLower() != "fulfillmentordertype")
            //{
            //    invalidColumns = headerArray[0];
            //    columnsIncorrectFormat = true;
            //}
            if (headerArray[0].Trim().ToLower() != "fulfillment#")
            {
                invalidColumns = headerArray[0];
                columnsIncorrectFormat = true;
            }
            if (headerArray[1].Trim().ToLower() != "sku#")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[1];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[1];
                columnsIncorrectFormat = true;
            }
            if (headerArray[2].Trim().ToLower() != "quantity")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[2];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[2];
                columnsIncorrectFormat = true;
            }

            //if (headerArray[4].Trim().ToLower() != "storeid")
            //{
            //    if (string.IsNullOrEmpty(invalidColumns))
            //        invalidColumns = headerArray[4];
            //    else
            //        invalidColumns = invalidColumns + ", " + headerArray[4];
            //    columnsIncorrectFormat = true;
            //}
            if (headerArray[3].Trim().ToLower() != "shiptoname")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[3];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[3];
                columnsIncorrectFormat = true;
            }

            //,,,,,,,,,
            if (headerArray[4].Trim() != "shiptoaddress1")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[4];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[4];
                columnsIncorrectFormat = true;
            }
            if (headerArray[5].Trim() != "shiptoaddress2")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[5];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[5];
                columnsIncorrectFormat = true;
            }

            if (headerArray[6].Trim() != "shiptocity")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[6];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[6];
                columnsIncorrectFormat = true;
            }
            if (headerArray[7].Trim() != "shiptostate")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[7];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[7];
                columnsIncorrectFormat = true;
            }
            if (headerArray[8].Trim() != "shiptozip")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[8];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[8];
                columnsIncorrectFormat = true;
            }
            if (headerArray[9].Trim() != "shiptophone")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[9];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[9];

                columnsIncorrectFormat = true;
            }
            //if (headerArray[12].Trim() != "shipmentthrough")
            //{
            //    if (string.IsNullOrEmpty(invalidColumns))
            //        invalidColumns = headerArray[12];
            //    else
            //        invalidColumns = invalidColumns + ", " + headerArray[12];
            //    columnsIncorrectFormat = true;
            //}
            //if (headerArray[13].Trim() != "requestedshipdate")
            //{
            //    if (string.IsNullOrEmpty(invalidColumns))
            //        invalidColumns = headerArray[13];
            //    else
            //        invalidColumns = invalidColumns + ", " + headerArray[13];
            //    columnsIncorrectFormat = true;
            //}

            //if (headerArray[11].Trim() != "zip")
            //{
            //    if (string.IsNullOrEmpty(invalidColumns))
            //        invalidColumns = headerArray[11];
            //    else
            //        invalidColumns = invalidColumns + ", " + headerArray[11];
            //    columnsIncorrectFormat = true;
            //}
            return columnsIncorrectFormat;

        }

        private bool ValidateColumnsold(string[] headerArray, out string invalidColumns)
        {
            bool columnsIncorrectFormat = false;
            invalidColumns = string.Empty;
            //if (headerArray[0].Trim().ToLower() != "fulfillmentordertype")
            //{
            //    invalidColumns = headerArray[0];
            //    columnsIncorrectFormat = true;
            //}
            if (headerArray[0].Trim().ToLower() != "fulfillment#")
            {
                invalidColumns = headerArray[1];
                columnsIncorrectFormat = true;
            }
            if (headerArray[2].Trim().ToLower() != "sku#")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[2];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[2];
                columnsIncorrectFormat = true;
            }
            if (headerArray[3].Trim().ToLower() != "quantity")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[3];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[3];
                columnsIncorrectFormat = true;
            }

            //if (headerArray[4].Trim().ToLower() != "storeid")
            //{
            //    if (string.IsNullOrEmpty(invalidColumns))
            //        invalidColumns = headerArray[4];
            //    else
            //        invalidColumns = invalidColumns + ", " + headerArray[4];
            //    columnsIncorrectFormat = true;
            //}
            if (headerArray[4].Trim().ToLower() != "shiptoname")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[4];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[4];
                columnsIncorrectFormat = true;
            }

            //,,,,,,,,,
            if (headerArray[5].Trim() != "shiptoaddress1")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[5];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[5];
                columnsIncorrectFormat = true;
            }
            if (headerArray[6].Trim() != "shiptoaddress2")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[6];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[6];
                columnsIncorrectFormat = true;
            }

            if (headerArray[7].Trim() != "shiptocity")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[7];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[7];
                columnsIncorrectFormat = true;
            }
            if (headerArray[8].Trim() != "shiptostate")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[8];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[8];
                columnsIncorrectFormat = true;
            }
            if (headerArray[9].Trim() != "shiptozip")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[9];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[9];
                columnsIncorrectFormat = true;
            }
            if (headerArray[10].Trim() != "shiptophone")
            {
                if (string.IsNullOrEmpty(invalidColumns))
                    invalidColumns = headerArray[10];
                else
                    invalidColumns = invalidColumns + ", " + headerArray[10];

                columnsIncorrectFormat = true;
            }
            //if (headerArray[12].Trim() != "shipmentthrough")
            //{
            //    if (string.IsNullOrEmpty(invalidColumns))
            //        invalidColumns = headerArray[12];
            //    else
            //        invalidColumns = invalidColumns + ", " + headerArray[12];
            //    columnsIncorrectFormat = true;
            //}
            //if (headerArray[13].Trim() != "requestedshipdate")
            //{
            //    if (string.IsNullOrEmpty(invalidColumns))
            //        invalidColumns = headerArray[13];
            //    else
            //        invalidColumns = invalidColumns + ", " + headerArray[13];
            //    columnsIncorrectFormat = true;
            //}

            //if (headerArray[11].Trim() != "zip")
            //{
            //    if (string.IsNullOrEmpty(invalidColumns))
            //        invalidColumns = headerArray[11];
            //    else
            //        invalidColumns = invalidColumns + ", " + headerArray[11];
            //    columnsIncorrectFormat = true;
            //}
            return columnsIncorrectFormat;
          
        }
        private StringBuilder WritePurchaseOrder(string fileName, ref List<clsPurchaseOrder> poList, out bool columnsIncorrectFormat, out string invalidColumns, out bool isRequired)
        {
            isRequired = false;
            string returnMessage = string.Empty;
            StringBuilder sbError = new StringBuilder();
            invalidColumns = string.Empty;
            columnsIncorrectFormat = false;
            clsPurchaseOrder po = null;
               string storeID = ddlStoreID.SelectedValue;
               string shipVia = dpShipBy.SelectedValue;
            string poType, contactName, address1, address2, city, state, zip, phoneNumber;//, storeID, shipVia;
            contactName = address1 = address2 = city = state = zip = poType = phoneNumber  = string.Empty; //= shipVia = storeID
            Hashtable hshPoList = new Hashtable();
            poType = ddlPOType.SelectedValue;
            //DateTime RequestedShipDate = Convert.ToDateTime(txtReqShipDate.Text.Trim());
            DateTime requestedShipDate = DateTime.Now;// Convert.ToDateTime(txtReqShipDate.Text.Trim());


            //List<avii.Classes.StoreLocation> userStoreList = (List<avii.Classes.StoreLocation>)Session["userstore"];

            //var storeList = (from item in userStoreList where item.StoreID.Equals(ddlStoreID.SelectedValue) select item).ToList();
            //if (storeList != null && storeList.Count > 0)
            //{
            //    address1 = storeList[0].StoreAddress.Address1;
            //    address2 = storeList[0].StoreAddress.Address2;
            //    city = storeList[0].StoreAddress.City;
            //    state = storeList[0].StoreAddress.State;
            //    zip = storeList[0].StoreAddress.Zip;

            //    contactName = storeList[0].ShipContactName;

            //}
            if (!string.IsNullOrEmpty(fileName))
            {
                if (poList == null)
                {
                    poList = new List<clsPurchaseOrder>();
                }
                try
                {
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        string sku = string.Empty;
                        string poNumber = string.Empty;
                        string poNum;//, contactName, address1, address2, city, state, zip, shipVia;
                        poNum = contactName = address1 = address2 = city = state = zip =  phoneNumber = string.Empty; //= shipVia = storeID = poType 
                        int qty = 0;
                        string line;
                        string[] tempData;
                        //int ctr = 0;
                        int i = 0;
                        while ((line = sr.ReadLine()) != null)
                        {
                            qty = 0;
                            contactName = address1 = address2 = city = state = zip =   phoneNumber = string.Empty; ///= shipVia = storeID 

                            if (!string.IsNullOrEmpty(line) && i == 0)
                            {
                                line = line.ToLower();
                                line = line.Trim();
                                string[] headerArray = line.Split(',');
                                if (headerArray.Length < 10)
                                {
                                    lblMsg.Text = "File format not matching missing required columns";
                                    columnsIncorrectFormat = true;
                                }
                                else
                                {
                                    i = 1;
                                    columnsIncorrectFormat = ValidateColumns(headerArray, out invalidColumns);
                                }
                            }
                            else if (!string.IsNullOrEmpty(line) && i > 0 && columnsIncorrectFormat == false)
                            {
                                line = "@" + line;

                                line = line.Replace(@"\,", "||");

                                tempData = line.Split(',');
                                //if (tempData.Length <= 14)
                                {
                                    if ((int.TryParse(tempData[2], out qty)))
                                    {
                                        if (!string.IsNullOrEmpty(tempData[0]) && tempData[0].Trim().Length > 0)
                                        {
                                            poNumber = tempData[0].Trim();
                                            poNumber = poNumber.Replace("@", "");

                                            //storeID = tempData[2].Trim();
                                            if (!hshPoList.ContainsKey(poNumber))
                                            {
                                                po = new clsPurchaseOrder();
                                            }
                                            else
                                            {
                                                po = hshPoList[poNumber] as clsPurchaseOrder;
                                            }
                                            //if (!string.IsNullOrEmpty(storeID))
                                            {
                                                po.PurchaseOrderNumber = poNumber;
                                                po.Comments = "None";
                                                if(txtReqShipDate.Text.Length == 0)
                                                    txtReqShipDate.Text = DateTime.Now.ToShortDateString();

                                                if (!string.IsNullOrEmpty(txtReqShipDate.Text))
                                                {
                                                    if (DateTime.TryParse(txtReqShipDate.Text, out requestedShipDate))
                                                    {
                                                        DateTime currentDate = DateTime.Now;
                                                        TimeSpan diffResult = currentDate - requestedShipDate;
                                                        int days = diffResult.Days;
                                                        if (diffResult.Days > 0)
                                                        {
                                                            sbError.Append("Invalid Requested ship date! Can not create Fulfillment order with days back.");

                                                            throw new Exception("Invalid Requested ship date! Can not create Fulfillment order with days back.");
                                                        }
                                                        else if (days < -10)
                                                        {
                                                            sbError.Append("Invalid Requested ship date! Can not create Fulfillment order after 10 from today date.");

                                                            throw new Exception("Invalid Requested ship date! Can not create Fulfillment order after 10 from today date.");
                                                        }
                                                        else
                                                            po.RequestedShipDate = requestedShipDate;
                                                    }
                                                    else
                                                    {
                                                        sbError.Append("Requested ship date does not have correct format(MM/DD/YYYY)");
                                                        throw new Exception("Requested ship date does not have correct format(MM/DD/YYYY)");

                                                    }
                                                }
                                                else
                                                {
                                                   // isRequired = true;
                                                }     
                                                    
                                               po.PurchaseOrderDate = DateTime.Now;



                                               // if (poNum != poNumber)
                                                {
                                                   // poType = tempData.Length > 0 ? tempData[0].ToUpper() : string.Empty;
                                                   // poType = poType.Replace("@", "");

                                                    //storeID = tempData.Length > 4 ? tempData[4] : string.Empty;
                                                    contactName = tempData.Length > 3 ? tempData[3] : string.Empty;
                                                    address1 = tempData.Length > 4 ? tempData[4] : string.Empty;
                                                    address2 = tempData.Length > 5 ? tempData[5] : string.Empty;
                                                    city = tempData.Length > 6 ? tempData[6] : string.Empty;
                                                    state = tempData.Length > 7 ? tempData[7] : string.Empty;
                                                    zip = tempData.Length > 8 ? tempData[8] : string.Empty;
                                                    phoneNumber = tempData.Length > 9 ? tempData[9] : string.Empty;
                                                    //shipVia = tempData.Length > 12 ? tempData[12] : string.Empty;

                                                    if(!ValidateShipVia(shipVia, out returnMessage))
                                                    {
                                                        sbError.Append(returnMessage+", ");
                                                        //throw new Exception(returnMessage);
                                                    }
                                                    if (poType.ToUpper() == "B2B" && !string.IsNullOrEmpty(storeID) && !ValidateStoreID(storeID, out returnMessage))
                                                    {
                                                        sbError.Append(returnMessage + ", ");
                                                        //throw new Exception(returnMessage);

                                                    }
                                                    if (state.Trim().Length != 2)
                                                    {
                                                        sbError.Append(state + " invalid state code!");
                                                        throw new Exception(state + " invalid state code!");

                                                    }
                                                    if (!isRequired)
                                                    {
                                                        if (string.IsNullOrEmpty(poType) || string.IsNullOrEmpty(contactName) || string.IsNullOrEmpty(address1) || string.IsNullOrEmpty(city)
                                                          || string.IsNullOrEmpty(state) || string.IsNullOrEmpty(zip) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(shipVia))
                                                        {
                                                            isRequired = true;
                                                        }
                                                        if (poType.ToUpper() == "B2B" && string.IsNullOrEmpty(storeID))
                                                        {
                                                            isRequired = true;
                                                        }
                                                        
                                                    }
                                                }
                                                //if (poType.ToUpper() != "B2B")
                                                //{
                                                //    storeID = string.Empty;
                                                //}

                                                poNum = poNumber;
                                                //shipVia = tempData[5].Trim();
                                                po.Shipping.ContactName = contactName.Trim();// sltemp.StoreContact.ContactName;
                                                po.Shipping.ShipToAddress = address1.Trim().Replace("||", ",");
                                                po.Shipping.ShipToAddress2 = address2.Trim().Replace("||", ",");
                                                po.Shipping.ShipToCity = city.Trim();
                                                po.Shipping.ShipToState = state.Trim();
                                                po.Shipping.ShipToZip = zip.Trim();
                                                po.Shipping.ContactPhone = phoneNumber.Trim();
                                                po.Shipping.ShipToAttn = contactName.Trim();//sltemp.StoreContact.ContactName;
                                                po.StoreID = poType.ToUpper() == "B2C" ? "" : storeID;
                                                po.POType = poType;

                                                po.ShipThrough = shipVia;
                                                sku = tempData[1].Trim();
                                                if (sku != string.Empty)
                                                {
                                                    if (qty > 0)
                                                    {
                                                        //for (int counter = 0; counter < qty; counter++)
                                                        //{
                                                        po.PurchaseOrderItems.Add(new PurchaseOrderItem() { LineNo = po.PurchaseOrderItems.Count + 1, ItemCode = sku, Quantity = qty });
                                                        //}
                                                    }
                                                    else
                                                    {
                                                        sbError.Append(string.Format("\nFulfillment# {0} does not have Quantity assigned ", po.PurchaseOrderNumber));
                                                    }
                                                }
                                                else
                                                {
                                                    sbError.Append(string.Format("\nFulfillment# {0} have empty SKU ", po.PurchaseOrderNumber));
                                                }

                                                if (po != null && po.PurchaseOrderItems != null && po.PurchaseOrderItems.Count > 0)
                                                {
                                                    if (hshPoList.ContainsKey(po.PurchaseOrderNumber))
                                                        hshPoList[po.PurchaseOrderNumber] = po;
                                                    else
                                                        hshPoList.Add(po.PurchaseOrderNumber, po);
                                                }
                                                else
                                                {

                                                }
                                            }
                                            //else
                                            //{
                                            //    throw new Exception("Store ID <b>" + storeID + "</b> information is missing");
                                            //}
                                        }
                                        else
                                        {
                                            throw new Exception("Uploaded file does not have correct Format");
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("Please check the file, Quantity should be greater than 0");

                                    }
                                }
                                //else
                                //{
                                //    lblMsg.Text = "File format not matching";
                                //    sbError.Append("File format not matching");
                                //}
                            }
                            //ctr = ctr + 1;
                        }
                        sr.Close();
                    }
                    if (hshPoList != null && hshPoList.Count > 0 && !columnsIncorrectFormat)
                    {
                        foreach (string key in hshPoList.Keys)
                        {
                            clsPurchaseOrder purchaseOrder = hshPoList[key] as clsPurchaseOrder;
                            if (purchaseOrder != null)
                            {
                                poList.Add(purchaseOrder);
                            }
                        }
                    }
                    else
                    {
                        if (columnsIncorrectFormat)
                        {
                            if (!string.IsNullOrEmpty(invalidColumns))
                            {
                                lblMsg.Text = invalidColumns + " column(s) name is not correct";
                            }
                            else
                            {
                                lblMsg.Text = "File format not matching missing required columns";

                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    lblMsg.Text = ex.Message;
                    //throw ex;
                }
            }
            return sbError;
        }

        private StringBuilder WritePurchaseOrder(string fileName, ref List<clsPurchaseOrder> poList, out bool columnsIncorrectFormat, out string invalidColumns)
        {
            StringBuilder sbError = new StringBuilder();
            invalidColumns = string.Empty;
            columnsIncorrectFormat = false;
            clsPurchaseOrder po = null;
         //   string storeID = ddlStoreID.SelectedValue;
         //   string shipVia = dpShipBy.SelectedValue;
            string poType, contactName, address1, address2, city, state, zip, phoneNumber, storeID, shipVia;
            contactName = address1 = address2 = city = state = zip = poType = phoneNumber = shipVia = storeID = string.Empty;
            Hashtable hshPoList = new Hashtable();
           // DateTime RequestedShipDate;


            //List<avii.Classes.StoreLocation> userStoreList = (List<avii.Classes.StoreLocation>)Session["userstore"];

            //var storeList = (from item in userStoreList where item.StoreID.Equals(ddlStoreID.SelectedValue) select item).ToList();
            //if (storeList != null && storeList.Count > 0)
            //{
            //    address1 = storeList[0].StoreAddress.Address1;
            //    address2 = storeList[0].StoreAddress.Address2;
            //    city = storeList[0].StoreAddress.City;
            //    state = storeList[0].StoreAddress.State;
            //    zip = storeList[0].StoreAddress.Zip;
                
            //    contactName = storeList[0].ShipContactName;

            //}
            if (!string.IsNullOrEmpty(fileName))
            {
                if (poList == null)
                {
                    poList = new List<clsPurchaseOrder>();
                }
                try
                {
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        string sku = string.Empty;
                        string poNumber = string.Empty;
                        string poNum;//, contactName, address1, address2, city, state, zip, shipVia;
                        poNum = contactName = address1 = address2 = city = state = zip = shipVia = poType = phoneNumber = shipVia = storeID = string.Empty;
                        int qty = 0;
                        string line;
                        string[] tempData;
                        //int ctr = 0;
                        int i = 0;
                        while ((line = sr.ReadLine()) != null)
                        {
                            qty = 0;
                            
                            if (!string.IsNullOrEmpty(line) && i == 0)
                            {
                                line = line.ToLower();
                                line = line.Trim();
                                string[] headerArray = line.Split(',');
                                if (headerArray.Length < 14 || headerArray.Length > 14)
                                {
                                    lblMsg.Text = "File format not matching missing required columns";
                                    columnsIncorrectFormat = true;
                                }
                                else
                                {
                                    i = 1;
                                    columnsIncorrectFormat = ValidateColumns(headerArray, out invalidColumns);
                                }
                            }
                            else if (!string.IsNullOrEmpty(line) && i > 0 && columnsIncorrectFormat == false)
                            {
                                line = "@" + line;

                                line = line.Replace(@"\,", "||");

                                tempData = line.Split(',');
                                if (tempData.Length == 14)
                                {
                                    if ((int.TryParse(tempData[3], out qty)))
                                    {
                                        if (!string.IsNullOrEmpty(tempData[1]) && tempData[1].Trim().Length > 0)
                                        {
                                            poNumber = tempData[1].Trim();
                                            poNumber= poNumber.Replace("@", "");
                                            //storeID = tempData[2].Trim();
                                            if (!hshPoList.ContainsKey(poNumber))
                                            {
                                                po = new clsPurchaseOrder();
                                            }
                                            else
                                            {
                                                po = hshPoList[poNumber] as clsPurchaseOrder;
                                            }
                                            if (!string.IsNullOrEmpty(storeID))
                                            {
                                                po.PurchaseOrderNumber = poNumber;
                                                po.StoreID = storeID;
                                                po.Comments = "None";
                                                //DateTime poDate;
                                                //if (!string.IsNullOrEmpty(tempData[1]))
                                                //{
                                                //    if (DateTime.TryParse(tempData[1], out poDate))
                                                //    {
                                                //        DateTime currentDate = DateTime.Now;
                                                //        TimeSpan diffResult = currentDate - poDate;
                                                //        int days = diffResult.Days;
                                                //        if (diffResult.Days > 90)
                                                //        {
                                                //            sbError.Append("Invalid Fulfillment Date! Can not create Fulfillment order before 90 days back.");

                                                //            throw new Exception("Invalid Fulfillment Date! Can not create Fulfillment order before 90 days back.");
                                                //        }
                                                //        else if(days < -30)
                                                //        {
                                                //            sbError.Append("Invalid Fulfillment Date! Can not create Fulfillment order after 30 from today date.");

                                                //            throw new Exception("Invalid Fulfillment Date! Can not create Fulfillment order after 30 from today date.");
                                                //        }
                                                //        else
                                                //            po.PurchaseOrderDate = poDate;
                                                //    }
                                                //    else
                                                //    {
                                                //        sbError.Append("Fulfillment Date does not have correct format(MM/DD/YYYY)");
                                                //        throw new Exception("Fulfillment Date does not have correct format(MM/DD/YYYY)");

                                                //    }
                                                //}
                                                //else
                                                po.PurchaseOrderDate = DateTime.Now;


                                                
                                                //if (poNum != poNumber)
                                                //{
                                                //    contactName = tempData.Length > 6 ? tempData[6] : string.Empty;
                                                //    address1 = tempData.Length > 7 ? tempData[7] : string.Empty;
                                                //    address2 = tempData.Length > 8 ? tempData[8] : string.Empty;
                                                //    city = tempData.Length > 9 ? tempData[9] : string.Empty;
                                                //    state = tempData.Length > 10 ? tempData[10] : string.Empty;
                                                //    zip = tempData.Length > 11 ? tempData[11] : string.Empty;

                                                //}
                                                //if (state.Trim().Length != 2)
                                                //{
                                                //    sbError.Append(state + " invalid state code!");
                                                //    throw new Exception(state + " invalid state code!");

                                                //}
                                                poNum = poNumber;
                                                //shipVia = tempData[5].Trim();
                                                po.Shipping.ContactName = contactName.Trim();// sltemp.StoreContact.ContactName;
                                                po.Shipping.ShipToAddress = address1.Trim().Replace("||", ",");
                                                po.Shipping.ShipToAddress2 = address2.Trim().Replace("||", ",");
                                                po.Shipping.ShipToCity = city.Trim();
                                                po.Shipping.ShipToState = state.Trim();
                                                po.Shipping.ShipToZip = zip.Trim();
                                                po.Shipping.ShipToAttn = contactName.Trim();//sltemp.StoreContact.ContactName;

                                                po.ShipThrough = shipVia;
                                                sku = tempData[1].Trim();
                                                if (sku != string.Empty)
                                                {
                                                    if (qty > 0)
                                                    {
                                                        //for (int counter = 0; counter < qty; counter++)
                                                        //{
                                                            po.PurchaseOrderItems.Add(new PurchaseOrderItem() { LineNo = po.PurchaseOrderItems.Count + 1, ItemCode = sku, Quantity = qty });
                                                        //}
                                                    }
                                                    else
                                                    {
                                                        sbError.Append(string.Format("\nFulfillment# {0} does not have Quantity assigned ", po.PurchaseOrderNumber));
                                                    }
                                                }
                                                else
                                                {
                                                    sbError.Append(string.Format("\nFulfillment# {0} have empty SKU ", po.PurchaseOrderNumber));
                                                }

                                                if (po != null && po.PurchaseOrderItems != null && po.PurchaseOrderItems.Count > 0)
                                                {
                                                    if (hshPoList.ContainsKey(po.PurchaseOrderNumber))
                                                        hshPoList[po.PurchaseOrderNumber] = po;
                                                    else
                                                        hshPoList.Add(po.PurchaseOrderNumber, po);
                                                }
                                                else
                                                {

                                                }
                                            }
                                            else
                                            {
                                                throw new Exception("Store ID <b>" + storeID + "</b> information is missing");
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Uploaded file does not have correct Format");
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("Please check the file, Quantity should be greater than 0");

                                    }
                                }
                                else
                                {
                                    lblMsg.Text = "File format not matching";
                                    sbError.Append("File format not matching");
                                }
                            }
                            //ctr = ctr + 1;
                        }
                        sr.Close();
                    }
                    if (hshPoList != null && hshPoList.Count > 0 && !columnsIncorrectFormat)
                    {
                        foreach (string key in hshPoList.Keys)
                        {
                            clsPurchaseOrder purchaseOrder = hshPoList[key] as clsPurchaseOrder;
                            if (purchaseOrder != null)
                            {
                                poList.Add(purchaseOrder);
                            }
                        }
                    }
                    else
                    {
                        if (columnsIncorrectFormat)
                        {
                            if (!string.IsNullOrEmpty(invalidColumns))
                            {
                                lblMsg.Text = invalidColumns + " column(s) name is not correct";
                            }
                            else
                            {
                                lblMsg.Text = "File format not matching missing required columns";

                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    lblMsg.Text = ex.Message;
                    //throw ex;
                }
            }
            return sbError;
        }
        
        protected void btnState_Click(object sender, EventArgs e)
        {
            RegisterStartupScript("jsUnblockDialog", "unblockStateDialog();");
        }
        protected void btnValidateUploadedFile_Click(object sender, EventArgs e)
        {
            UploadPurchaseOrder();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //lblMsg.CssClass = "errormessage";

            rptPO.DataSource = null;
            rptPO.DataBind();
            //rptPO.Visible = false;
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;

            btnSubmit.Visible = false;
            btnUpload.Visible = true;
            btnSubmit2.Visible = false;
            pnlSubmit.Visible = false;
            lblCount.Text = string.Empty;
            btnValidate.Visible = false;
            btnViewPO.Visible = false;
            if (Session["adm"] != null)
                dpCompany.SelectedIndex = 0;
            else
                trCustomer.Visible = false;
            dpShipBy.SelectedIndex = 0;
            ddlStoreID.SelectedIndex = 0;
            ddlStoreID.Items.Clear();
            ddlPOType.SelectedIndex = 0;
            txtReqShipDate.Text = "";
            //trStore.Visible = false;

        }
        private string UploadFile()
        {
            string actualFilename = string.Empty;
            Int32 maxFileSize = 1572864;
            actualFilename = System.IO.Path.GetFileName(flnUpload.PostedFile.FileName);
            if (ConfigurationManager.AppSettings["FulfillmentFilesStoreage"] != null)
            {
                fileStoreLocation = ConfigurationManager.AppSettings["FulfillmentFilesStoreage"].ToString();
            }

            fileStoreLocation = Server.MapPath(fileStoreLocation);
            if (File.Exists(fileStoreLocation + actualFilename))
            {
                actualFilename = System.Guid.NewGuid().ToString() + actualFilename;
            }

            flnUpload.PostedFile.SaveAs(fileStoreLocation + actualFilename);
            ViewState["filename"] = fileStoreLocation + actualFilename;

            FileInfo fileInfo = new FileInfo(fileStoreLocation + actualFilename);

            if (ConfigurationManager.AppSettings["maxCSVfilesize"] != null)
            {
                if (Int32.TryParse(ConfigurationManager.AppSettings["maxCSVfilesize"].ToString(), out maxFileSize))
                {
                    if (fileInfo.Length > maxFileSize)
                    {
                        fileInfo.Delete();
                        lblMsg.Text = "File size is greater than " + maxFileSize + " bytes";

                        //throw new Exception("File size is greater than " + maxFileSize + " bytes");
                    }
                }
            }



            return fileStoreLocation + actualFilename;
        }
        private string UploadFile(out bool IsValid)
        {
            IsValid = true;
            string actualFilename = string.Empty;
            Int32 maxFileSize = 1572864;
            actualFilename = System.IO.Path.GetFileName(flnUpload.PostedFile.FileName);
            if (ConfigurationManager.AppSettings["FulfillmentFilesStoreage"] != null)
            {
                fileStoreLocation = ConfigurationManager.AppSettings["FulfillmentFilesStoreage"].ToString();
            }

            fileStoreLocation = Server.MapPath(fileStoreLocation);
            if (File.Exists(fileStoreLocation + actualFilename))
            {
                actualFilename = System.Guid.NewGuid().ToString() + actualFilename;
            }


            flnUpload.PostedFile.SaveAs(fileStoreLocation + actualFilename);
            ViewState["filename"] = fileStoreLocation + actualFilename;

            FileInfo fileInfo = new FileInfo(fileStoreLocation + actualFilename);

            if (ConfigurationManager.AppSettings["maxCSVfilesize"] != null)
            {
                if (Int32.TryParse(ConfigurationManager.AppSettings["maxCSVfilesize"].ToString(), out maxFileSize))
                {
                    if (fileInfo.Length > maxFileSize)
                    {
                        fileInfo.Delete();
                        int fileSize = maxFileSize / (1024 * 1024);
                        lblMsg.Text = "File size is greater than " + fileSize  + " MB";
                        IsValid = false;
                        //throw new Exception("File size is greater than " + maxFileSize + " bytes");
                    }
                }
            }
            return fileStoreLocation + actualFilename;
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            GenerateCSV();
        }
        private void GenerateCSV()
        {
            lblMsg.Text = string.Empty;
            
            {
                string string2CSV = "Fulfillment#,SKU#,Quantity,ShipToName,ShipToAddress1,ShipToAddress2,ShipToCity,ShipToState,ShipToZip,ShipToPhone" + Environment.NewLine;

                //esnList.ToCSV();

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=FulfillmentUpload.csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(string2CSV);
                Response.Flush();
                Response.End();

            }
            
        }
        private void GenerateCSVOld()
        {
            lblMsg.Text = string.Empty;

            {
                string string2CSV = "FulfillmentOrderType,Fulfillment#,SKU#,Quantity,StoreID,ShipToName,ShipToAddress1,ShipToAddress2,ShipToCity,ShipToState,ShipToZip,ShipToPhone,ShipmentThrough,RequestedShipDate" + Environment.NewLine;

                //esnList.ToCSV();

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=FulfillmentUpload.csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(string2CSV);
                Response.Flush();
                Response.End();

            }

        }

    }
}