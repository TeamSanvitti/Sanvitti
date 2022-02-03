using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Admin
{
    public partial class customer_Form : System.Web.UI.Page
    {
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
                ddlStatus.Enabled = false;
                //BindStates();
                BindIntegration();
                bindStore();
                bindSalePerson();
                if (Request["companyID"] != null)
                {
                    int companyID = Convert.ToInt32(Request["companyID"]);
                    ViewState["companyid"] = companyID;
                    getCompanyDetails(companyID);
                }
                else
                {
                    BindWarehouseCode();
                    BindCustomerEmail(0);
                }
                if (Session["adm"] == null)
                {
                    if (Session["userInfo"] != null)
                    {
                        avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                        if (userInfo != null && userInfo.UserGUID > 0)
                        {
                            ViewState["userid"] = userInfo.UserGUID;
                            ViewState["companyid"] = userInfo.CompanyGUID;
                            getCompanyDetails(userInfo.CompanyGUID);
                            EnableOnlyStores();
                        }


                    }
                }
                else
                {
                    ViewState["userid"] = Convert.ToInt32(Session["UserID"]);
                }
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
                        btnSubmit.Visible = false;
                    }
                }

            }

        }
        private void BindCustomerEmail(int companyID)
        {
            rptEmail.DataSource = CustomerEmailOperations.GetCustomerEmailList(companyID);
            rptEmail.DataBind();
        }
        private void EnableOnlyStores()
        {
            hdnTabIndex.Value = "1";
            txtBDAName.ReadOnly = true;
            txtCellPhone.ReadOnly = true;
            txtCompanyAccount.ReadOnly = true;
            txtCompanyName.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtGroupEmail.ReadOnly = true;
            txtHomePhone.ReadOnly = true;
            txtOfficeAdd1.ReadOnly = true;
            txtOfficeAdd2.ReadOnly = true;
            txtOfficeCity.ReadOnly = true;
            txtOfficeContactName.ReadOnly = true;
            txtOfficeCountry.ReadOnly = true;
            txtOfficeEmail1.ReadOnly = true;
            txtOfficeEmail2.ReadOnly = true;
            txtOfficePhone1.ReadOnly = true;
            txtOfficePhone2.ReadOnly = true;
            //txtOfficeState.ReadOnly = true;
            dpOfficeState.Enabled = false;
            txtOfficeZip.ReadOnly = true;
            txtShipCountry.ReadOnly = true;
            txtShippAdd1.ReadOnly = true;
            txtShippAdd2.ReadOnly = true;
            txtShippCellPhone.ReadOnly = true;
            txtShippCity.ReadOnly = true;
            txtShippContactName.ReadOnly = true;
            txtShippEmail.ReadOnly = true;
            txtShippEmail2.ReadOnly = true;
            txtShippHomePhone.ReadOnly = true;
            txtShippOfficePhone1.ReadOnly = true;
            txtShippOfficePhone2.ReadOnly = true;
            dpShipState.Enabled = false;
            //txtShippState.ReadOnly = true;
            txtShippZip.ReadOnly = true;
            txtShortName.ReadOnly = true;
            txtWebsite.ReadOnly = true;
            ddlStatus.Enabled = false;
            // lbSalesPerson.Enabled = false;
            chkSameAddess.Enabled = false;
            btnWhCode.Enabled = false;
            //rptWhCode.Visible = false;


        }
        private void getCompanyDetails(int companyID)
        {
            try
            {
                ViewState["companyid"] = companyID;
                Company getCompanyInfo = CompanyOperations.getCompanyInfo(companyID, string.Empty, string.Empty);
                if (getCompanyInfo != null)
                {

                    //  txtAPIAddress.Text = getCompanyInfo.APIAddress;
                    //  txtAPIName.Text = getCompanyInfo.APIName;
                    //  txtUserName.Text = getCompanyInfo.APIUserName;
                    //  txtPassword.Text = getCompanyInfo.APIPassword;
                    txtBDAName.Text = getCompanyInfo.BussinessType;
                    txtComment.Text = getCompanyInfo.Comment;
                    txtCompanyAccount.Text = getCompanyInfo.CompanyAccountNumber;
                    txtCompanyName.Text = getCompanyInfo.CompanyName;
                    txtEmail.Text = getCompanyInfo.Email;
                    txtGroupEmail.Text = getCompanyInfo.GroupEmail;
                    txtShortName.Text = getCompanyInfo.CompanyShortName;
                    txtWebsite.Text = getCompanyInfo.Website;
                    chkEmail.Checked = getCompanyInfo.IsEmail;

                    //ddlCompanyType.SelectedValue = getCompanyInfo.CompanySType.ToString();
                    ddlStatus.SelectedValue = getCompanyInfo.CompanyAccountStatus.ToString();
                    //chkActive.Checked = getCompanyInfo.Active;
                    List<SalesPerson> salesPerson = getCompanyInfo.AssingedSalesPerson;
                    //if (salesPerson != null)
                    //{
                    //    if (salesPerson.Count > 0)
                    //    {
                    //        for (int i = 0; i < salesPerson.Count - 1; i++)
                    //        {
                    //            for (int j = 0; j < lbSalesPerson.Items.Count - 1; j++)
                    //            {
                    //                if (salesPerson[i].UserID == Convert.ToInt32(lbSalesPerson.Items[j].Value))
                    //                {
                    //                    lbSalesPerson.Items[j].Selected = true;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    List<StoreLocation> OfficeNShippAddress = getCompanyInfo.officeAndShippAddress;
                    if (OfficeNShippAddress != null)
                    {
                        if (OfficeNShippAddress.Count > 0)
                        {
                            ViewState["addressid1"] = OfficeNShippAddress[0].CompanyAddressID;
                            txtCellPhone.Text = OfficeNShippAddress[0].StoreContact.CellPhone;
                            txtHomePhone.Text = OfficeNShippAddress[0].StoreContact.HomePhone;
                            txtOfficeAdd1.Text = OfficeNShippAddress[0].StoreAddress.Address1;
                            txtOfficeAdd2.Text = OfficeNShippAddress[0].StoreAddress.Address2;
                            txtOfficeCity.Text = OfficeNShippAddress[0].StoreAddress.City;
                            txtOfficeContactName.Text = OfficeNShippAddress[0].StoreContact.ContactName;
                            txtOfficeEmail1.Text = OfficeNShippAddress[0].StoreContact.Email1;
                            txtOfficeEmail2.Text = OfficeNShippAddress[0].StoreContact.Email2;
                            txtOfficePhone1.Text = OfficeNShippAddress[0].StoreContact.OfficePhone1;
                            txtOfficePhone2.Text = OfficeNShippAddress[0].StoreContact.OfficePhone2;
                            //txtOfficeState.Text = OfficeNShippAddress[0].StoreAddress.State;
                            dpOfficeState.SelectedValue = OfficeNShippAddress[0].StoreAddress.State;
                            txtOfficeZip.Text = OfficeNShippAddress[0].StoreAddress.Zip;

                            if (OfficeNShippAddress.Count > 1)
                            {
                                ViewState["addressid2"] = OfficeNShippAddress[1].CompanyAddressID;
                                txtShippCellPhone.Text = OfficeNShippAddress[1].StoreContact.CellPhone;
                                txtShippHomePhone.Text = OfficeNShippAddress[1].StoreContact.HomePhone;
                                txtShippAdd1.Text = OfficeNShippAddress[1].StoreAddress.Address1;
                                txtShippAdd2.Text = OfficeNShippAddress[1].StoreAddress.Address2;
                                txtShippCity.Text = OfficeNShippAddress[1].StoreAddress.City;
                                txtShippContactName.Text = OfficeNShippAddress[1].StoreContact.ContactName;
                                txtShippEmail.Text = OfficeNShippAddress[1].StoreContact.Email1;
                                txtShippEmail2.Text = OfficeNShippAddress[1].StoreContact.Email2;
                                txtShippOfficePhone1.Text = OfficeNShippAddress[1].StoreContact.OfficePhone1;
                                txtShippOfficePhone2.Text = OfficeNShippAddress[1].StoreContact.OfficePhone2;
                                //txtShippState.Text = OfficeNShippAddress[1].StoreAddress.State;
                                dpShipState.SelectedValue = OfficeNShippAddress[1].StoreAddress.State;
                                txtShippZip.Text = OfficeNShippAddress[1].StoreAddress.Zip;
                            }
                        }
                    }
                    List<StoreLocation> StoreLocationList = getCompanyInfo.Stores;

                    //Bind customer Stores
                    rptStore.DataSource = StoreLocationList;
                    rptStore.DataBind();

                    //Bind customer Email
                    rptEmail.DataSource = getCompanyInfo.CustomerEmailList;
                    rptEmail.DataBind();


                    //Bind customer Integration
                    List<IntegrationModel> integrationList = getCompanyInfo.IntegrationList;
                    if (integrationList != null && integrationList.Count > 0)
                    {
                        rptInt.DataSource = integrationList;
                        rptInt.DataBind();
                    }

                    //Bind customer Warehouse codes
                    if (getCompanyInfo.WarehouseCodeList != null && getCompanyInfo.WarehouseCodeList.Count > 0)
                    {
                        rptWhCode.DataSource = getCompanyInfo.WarehouseCodeList;
                        rptWhCode.DataBind();
                        if (Session["adm"] == null)
                        {
                            foreach (RepeaterItem item in rptWhCode.Items)
                            {
                                TextBox txt1 = item.FindControl("txtWhcode") as TextBox;
                                CheckBox chk1 = item.FindControl("chkActive2") as CheckBox;
                                txt1.ReadOnly = true;
                                chk1.Enabled = false;

                            }
                        }
                    }
                    else
                        BindWarehouseCode();


                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }
        private void bindSalePerson()
        {
            //lbSalesPerson.DataSource = SalesPersonOperations.getSalesPersonList(-1);
            //lbSalesPerson.DataTextField = "EmployeeNumber";
            //lbSalesPerson.DataValueField = "UserID";
            //lbSalesPerson.DataBind();
            //ListItem listItem = new ListItem("--Select--", "0");
            //lbSalesPerson.Items.Insert(0, listItem);

        }
        private void BindWarehouseCode()
        {
            List<CustomerWarehouseCode> warehouseCodeList = new List<CustomerWarehouseCode>();

            CustomerWarehouseCode warehouseCodeObj = new CustomerWarehouseCode();
            warehouseCodeObj.Active = false;
            warehouseCodeObj.WarehouseCode = string.Empty;
            warehouseCodeObj.WarehouseCodeGUID = 0;
            warehouseCodeList.Add(warehouseCodeObj);
            rptWhCode.DataSource = warehouseCodeList;
            rptWhCode.DataBind();

        }
        private void bindStore()
        {
            List<StoreLocation> listObj = new List<StoreLocation>();
            Address addrssObj = new Address();
            addrssObj.Address1 = string.Empty;
            addrssObj.Country = string.Empty;
            addrssObj.City = string.Empty;
            addrssObj.Zip = string.Empty;
            addrssObj.State = string.Empty;
            ContactInfo contactObj1 = new ContactInfo();
            contactObj1.ContactName = txtOfficeContactName.Text;
            contactObj1.CellPhone = string.Empty;
            contactObj1.Comment = string.Empty;
            contactObj1.Email1 = txtEmail.Text;
            contactObj1.Email2 = string.Empty;
            contactObj1.HomePhone = string.Empty;
            contactObj1.OfficePhone1 = string.Empty;
            contactObj1.OfficePhone2 = string.Empty;


            StoreLocation StoreLocationObj = new StoreLocation();
            StoreLocationObj.Active = true;
            StoreLocationObj.StoreAddress = addrssObj;
            StoreLocationObj.StoreContact = contactObj1;
            StoreLocationObj.StoreID = string.Empty;
            StoreLocationObj.StoreName = string.Empty;
            listObj.Add(StoreLocationObj);

            rptStore.DataSource = listObj;// Sv.Framework.DataMembers.DataMembers.StateObjects.customerUtility.getStoreLocation(0);
            rptStore.DataBind();
        }
        private void BindIntegrationModule(DropDownList ddlIntegration)
        {
            ddlIntegration.DataSource = CompanyOperations.GetIntegrationModules();
            ddlIntegration.DataTextField = "IntegrationName";
            ddlIntegration.DataValueField = "IntegrationModuleID";
            ddlIntegration.DataBind();
            ListItem listItem = new ListItem("--Select--", "0");
            ddlIntegration.Items.Insert(0, listItem);
        }
        private void BindIntegration()
        {
            List<IntegrationModel> listObj = new List<IntegrationModel>();
            IntegrationModel model = new IntegrationModel();
            model.IntegrationID = 0;
            model.IntegrationModuleID = 2;
            model.Active = true;
            model.APIAddress = string.Empty;
            model.Password = string.Empty;
            model.UserName = "AccountID=1353742,RequesterID=Lang,PassPhrase=12031Lan1@";
            listObj.Add(model);
            IntegrationModel model2 = new IntegrationModel();
            model2.IntegrationID = 0;
            model2.IntegrationModuleID = 3;
            model2.Active = true;
            model2.APIAddress = "https://ssapi.shipstation.com/";
            model2.Password = string.Empty;
            model2.UserName = "Basic M2NhOWRhM2ViZGIyNDM3MTgyNmJiY2FjMGY3YjMzY2Q6MjAzNDYyZTI0Y2EzNDAyYmFmNGE4ZmFlYmM1YTZkMTE=";
            listObj.Add(model2);



            rptInt.DataSource = listObj;// Sv.Framework.DataMembers.DataMembers.StateObjects.customerUtility.getStoreLocation(0);
            rptInt.DataBind();
        }

        //private List<SalesPerson> GetSelectesSaalesPerson()
        //{
        //    List<SalesPerson> salesPersonList = new List<SalesPerson>();
        //    for (int i = 0; i < lbSalesPerson.Items.Count; i++)
        //    {
        //        if (lbSalesPerson.SelectedIndex > 0)
        //        {
        //            if (lbSalesPerson.Items[i].Selected)
        //            {
        //                SalesPerson salesPersonObj = new SalesPerson();
        //                salesPersonObj.UserID = Convert.ToInt32(lbSalesPerson.Items[i].Value);
        //                salesPersonList.Add(salesPersonObj);
        //            }
        //        }
        //    }
        //    return salesPersonList;
        //}
        private List<CustomerEmail> GetCustomerEmailList()
        {
            List<CustomerEmail> customerEmailList = new List<CustomerEmail>();

            foreach (RepeaterItem item in rptEmail.Items)
            {
                CustomerEmail objCustomerEmail = new CustomerEmail();
                HiddenField hdnModuleGUID = item.FindControl("hdnModuleGUID") as HiddenField;
                TextBox txt_Email = item.FindControl("txt_Email") as TextBox;
                TextBox txtOeverrideEmail = item.FindControl("txtOvrdEmail") as TextBox;
                CheckBox chkNotification = item.FindControl("chkNotification") as CheckBox;
                objCustomerEmail.ModuleGUID = Convert.ToInt32(hdnModuleGUID.Value);
                objCustomerEmail.Email = txt_Email.Text.Trim();
                objCustomerEmail.IsNotification = chkNotification.Checked;
                customerEmailList.Add(objCustomerEmail);

            }
            return customerEmailList;
        }
        private List<CustomerWarehouseCode> GetWarehouseCodeList(out bool duplicateWarehouseCode)
        {
            duplicateWarehouseCode = false;
            System.Collections.Hashtable hshUnqStoreID = new System.Collections.Hashtable();
            string warehouseCode = string.Empty;
            List<CustomerWarehouseCode> warehouseCodeList = new List<CustomerWarehouseCode>();
            foreach (RepeaterItem item in rptWhCode.Items)
            {
                HiddenField hdnWhID = item.FindControl("hdnWhID") as HiddenField;
                TextBox txtWhcode = item.FindControl("txtWhcode") as TextBox;
                CheckBox chkActive2 = item.FindControl("chkActive2") as CheckBox;
                if (hdnWhID.Value == string.Empty)
                    hdnWhID.Value = "0";

                if (txtWhcode.Text != string.Empty)
                {
                    warehouseCode = txtWhcode.Text.Trim();
                    if (hshUnqStoreID.ContainsKey(warehouseCode.ToLower()))
                    {
                        lblMsg.Text = string.Format("Warehouse Code <b>{0}</b> can not be duplicate", warehouseCode);
                        txtWhcode.Focus();
                        duplicateWarehouseCode = true;
                    }
                    else
                    {

                        hshUnqStoreID.Add(warehouseCode.ToLower(), warehouseCode);
                        CustomerWarehouseCode warehouseCodeObj1 = new CustomerWarehouseCode();

                        warehouseCodeObj1.Active = chkActive2.Checked;
                        warehouseCodeObj1.WarehouseCode = txtWhcode.Text.Trim();
                        warehouseCodeObj1.WarehouseCodeGUID = Convert.ToInt32(hdnWhID.Value);
                        warehouseCodeList.Add(warehouseCodeObj1);
                    }

                }
            }
            return warehouseCodeList;
        }
        protected void btnWhCode_Click(object sender, EventArgs e)
        {
            List<CustomerWarehouseCode> warehouseCodeList = new List<CustomerWarehouseCode>();

            CustomerWarehouseCode warehouseCodeObj = new CustomerWarehouseCode();
            warehouseCodeObj.Active = false;
            warehouseCodeObj.WarehouseCode = string.Empty;
            warehouseCodeObj.WarehouseCodeGUID = 0;
            warehouseCodeList.Add(warehouseCodeObj);

            foreach (RepeaterItem item in rptWhCode.Items)
            {


                HiddenField hdnWhID = item.FindControl("hdnWhID") as HiddenField;
                TextBox txtWhcode = item.FindControl("txtWhcode") as TextBox;
                CheckBox chkActive2 = item.FindControl("chkActive2") as CheckBox;
                if (hdnWhID.Value == string.Empty)
                    hdnWhID.Value = "0";

                if (txtWhcode.Text != string.Empty)
                {

                    CustomerWarehouseCode warehouseCodeObj1 = new CustomerWarehouseCode();

                    warehouseCodeObj1.Active = chkActive2.Checked;
                    warehouseCodeObj1.WarehouseCode = txtWhcode.Text.Trim();
                    warehouseCodeObj1.WarehouseCodeGUID = Convert.ToInt32(hdnWhID.Value);
                    warehouseCodeList.Add(warehouseCodeObj1);

                }
            }



            rptWhCode.DataSource = warehouseCodeList;
            rptWhCode.DataBind();
            TextBox txtWhcode1 = (TextBox)rptWhCode.Items[0].FindControl("txtWhcode");
            txtWhcode1.Focus();
        }
        protected void btnAddStoreID_Click(object sender, EventArgs e)
        {
            List<StoreLocation> storeAddListObj = new List<StoreLocation>();

            //Add empty row
            Address addrssObj = new Address();
            addrssObj.Address1 = string.Empty;
            addrssObj.Country = string.Empty;
            addrssObj.City = string.Empty;
            addrssObj.Zip = string.Empty;
            addrssObj.State = string.Empty;
            ContactInfo contactObj1 = new ContactInfo();
            contactObj1.ContactName = txtOfficeContactName.Text;
            contactObj1.CellPhone = string.Empty;
            contactObj1.Comment = string.Empty;
            contactObj1.Email1 = txtEmail.Text;
            contactObj1.Email2 = string.Empty;
            contactObj1.HomePhone = string.Empty;
            contactObj1.OfficePhone1 = string.Empty;
            contactObj1.OfficePhone2 = string.Empty;


            StoreLocation StoreLocationObj = new StoreLocation();
            StoreLocationObj.CompanyAddressID = 0;
            StoreLocationObj.Active = true;
            StoreLocationObj.StoreAddress = addrssObj;
            StoreLocationObj.StoreContact = contactObj1;
            StoreLocationObj.StoreID = string.Empty;
            StoreLocationObj.StoreName = string.Empty;

            storeAddListObj.Add(StoreLocationObj);

            foreach (RepeaterItem item in rptStore.Items)
            {


                HiddenField hdnAddressID = item.FindControl("hdnAddressID") as HiddenField;
                TextBox txtStoreID = item.FindControl("txtStoreID") as TextBox;
                TextBox txtStoreName = item.FindControl("txtStoreName") as TextBox;
                TextBox txtAddress = item.FindControl("txtAddress") as TextBox;
                TextBox txtCity = item.FindControl("txtCity") as TextBox;
                //TextBox txtState = item.FindControl("txtState") as TextBox;
                DropDownList dpStState = item.FindControl("dpStState") as DropDownList;
                TextBox txtcountry1 = item.FindControl("txtcountry1") as TextBox;
                TextBox txtZip = item.FindControl("txtZip") as TextBox;
                CheckBox chkActive1 = item.FindControl("chkActive1") as CheckBox;
                if (hdnAddressID.Value == string.Empty)
                    hdnAddressID.Value = "0";

                if (txtStoreID.Text != string.Empty)
                {

                    Address addrssObj1 = new Address();
                    addrssObj1.AddressType = AddressType.Store;
                    addrssObj1.Address1 = txtAddress.Text;
                    addrssObj1.Country = txtcountry1.Text;
                    addrssObj1.City = txtCity.Text;
                    addrssObj1.Zip = txtZip.Text;
                    addrssObj1.State = dpStState.SelectedValue; //txtState.Text;
                    ContactInfo contactObj = new ContactInfo();
                    contactObj.ContactName = string.Empty;
                    contactObj.CellPhone = string.Empty;
                    contactObj.Comment = string.Empty;
                    contactObj.Email1 = txtEmail.Text;
                    contactObj.Email2 = string.Empty;
                    contactObj.HomePhone = string.Empty;
                    contactObj.OfficePhone1 = string.Empty;
                    contactObj.OfficePhone2 = string.Empty;

                    StoreLocation StoreLocationObj1 = new StoreLocation();
                    StoreLocationObj1.CompanyAddressID = Convert.ToInt32(hdnAddressID.Value);
                    StoreLocationObj1.StoreID = txtStoreID.Text.Trim();
                    StoreLocationObj1.StoreName = txtStoreName.Text.Trim();
                    StoreLocationObj1.Active = chkActive1.Checked;
                    StoreLocationObj1.StoreAddress = addrssObj1;
                    StoreLocationObj1.StoreContact = contactObj;
                    storeAddListObj.Add(StoreLocationObj1);

                }
            }
            // ViewState["storelocation"] = listObj;



            rptStore.DataSource = storeAddListObj;
            rptStore.DataBind();
            TextBox txtStorID = (TextBox)rptStore.Items[0].FindControl("txtStoreID");
            txtStorID.Focus();


        }
        private void Reset()
        {
            txtBDAName.Text = string.Empty;
            txtCellPhone.Text = string.Empty;
            txtComment.Text = string.Empty;
            txtCompanyAccount.Text = string.Empty;
            txtCompanyName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtGroupEmail.Text = string.Empty;
            txtHomePhone.Text = string.Empty;
            txtOfficeAdd1.Text = string.Empty;
            txtOfficeAdd2.Text = string.Empty;
            txtOfficeCity.Text = string.Empty;
            txtOfficeContactName.Text = string.Empty;
            txtOfficeEmail1.Text = string.Empty;
            txtOfficeEmail2.Text = string.Empty;
            txtOfficePhone1.Text = string.Empty;
            txtOfficePhone2.Text = string.Empty;
            //txtOfficeState.Text = string.Empty;
            dpOfficeState.SelectedIndex = 0;
            txtOfficeZip.Text = string.Empty;
            txtShippAdd1.Text = string.Empty;
            txtShippAdd2.Text = string.Empty;
            txtShippCellPhone.Text = string.Empty;
            txtShippCity.Text = string.Empty;
            txtShippContactName.Text = string.Empty;
            txtShippEmail.Text = string.Empty;
            txtShippEmail2.Text = string.Empty;
            txtShippHomePhone.Text = string.Empty;
            txtShippOfficePhone1.Text = string.Empty;
            txtShippOfficePhone2.Text = string.Empty;
            //txtShippState.Text = string.Empty;
            dpShipState.SelectedIndex = 0;
            txtShippZip.Text = string.Empty;
            txtShortName.Text = string.Empty;
            txtWebsite.Text = string.Empty;
            //ddlCompanyType.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            // lbSalesPerson.SelectedIndex = 0;
            txtShipCountry.Text = string.Empty;
            txtOfficeCountry.Text = string.Empty;
            //chkActive.Checked = false;
            chkSameAddess.Checked = false;
            bindStore();

        }
        private bool checkForDuplicateCompanyNEmail(int companyID)
        {
            string returnMessage = string.Empty;
            bool returnFlag = false;
            returnMessage = CompanyOperations.checkForDuplicateCompanyNemail(txtCompanyName.Text.Trim(), txtEmail.Text.Trim(), companyID);
            if (returnMessage != string.Empty)
            {
                lblMsg.Text = returnMessage;
                returnFlag = true;
            }
            return returnFlag;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int returnValue = 0;
            bool duplicateWarehouseCode = false;
            string actualFilename = txtShortName.Text.Trim() + "logo";
            string fileStoreLocation = Server.MapPath("~/img/");
            int userID = 0;
            if (ViewState["userid"] != null)
                userID = Convert.ToInt32(ViewState["userid"]);
            System.Collections.Hashtable hshUnqStoreID = new System.Collections.Hashtable();
            System.Collections.Hashtable hshUnqIntegration = new System.Collections.Hashtable();
            string storeID = string.Empty;
            try
            {
                if (Page.IsValid)
                {
                    bool isMail = chkEmail.Checked;
                    int companyID = 0;
                    string LogoPath = string.Empty;
                    if (fuLogo.HasFile)
                    {
                        string extension = Path.GetExtension(fuLogo.PostedFile.FileName);
                        //LogoPath = fuLogo.FileName;
                        actualFilename = actualFilename + extension; //System.IO.Path.GetFileName(fuLogo.PostedFile.FileName);
                        LogoPath = actualFilename;
                        fuLogo.PostedFile.SaveAs(fileStoreLocation + actualFilename);

                    }

                    if (ViewState["companyid"] != null)
                        companyID = Convert.ToInt32(ViewState["companyid"]);
                    lblMsg.Text = string.Empty;
                    List<StoreLocation> listObj = new List<StoreLocation>();
                    List<CustomerEmail> customerEmail = GetCustomerEmailList();
                    List<CustomerWarehouseCode> warehouseCodeList = GetWarehouseCodeList(out duplicateWarehouseCode);
                    if (duplicateWarehouseCode)
                        return;
                    Company companyObj = new Company();
                    ContactInfo contactObj1 = new ContactInfo();
                    StoreLocation StoreLocationObj = new StoreLocation();
                    // List<SalesPerson> salesPersonList = GetSelectesSaalesPerson();
                    Address addrssObj = new Address();
                    //Company getCompanyInfo = null;

                    //if (checkForDuplicateCompanyNEmail(companyID))
                    //    return;

                    //Add Corporate Office Address to list
                    addrssObj.AddressType = AddressType.Office1;
                    addrssObj.Address1 = txtOfficeAdd1.Text;
                    addrssObj.Address2 = txtOfficeAdd2.Text;
                    addrssObj.Country = string.Empty;
                    addrssObj.City = txtOfficeCity.Text;
                    addrssObj.Zip = txtOfficeZip.Text;
                    addrssObj.State = dpOfficeState.SelectedValue;//txtOfficeState.Text;// dpState.SelectedValue;
                    addrssObj.Country = txtOfficeCountry.Text;

                    contactObj1.ContactName = txtOfficeContactName.Text;
                    contactObj1.CellPhone = txtCellPhone.Text;
                    contactObj1.Comment = string.Empty;
                    contactObj1.Email1 = txtOfficeEmail1.Text;
                    contactObj1.Email2 = txtOfficeEmail2.Text;
                    contactObj1.HomePhone = txtHomePhone.Text;
                    contactObj1.OfficePhone1 = txtOfficePhone1.Text;
                    contactObj1.OfficePhone2 = txtOfficePhone2.Text;

                    if (ViewState["addressid1"] != null)
                        StoreLocationObj.CompanyAddressID = Convert.ToInt32(ViewState["addressid1"]);
                    else
                        StoreLocationObj.CompanyAddressID = 0;
                    StoreLocationObj.Active = true;
                    StoreLocationObj.StoreAddress = addrssObj;
                    StoreLocationObj.StoreContact = contactObj1;
                    StoreLocationObj.StoreID = string.Empty;
                    StoreLocationObj.StoreName = string.Empty;
                    listObj.Add(StoreLocationObj);
                    // End  of Corporate Office address


                    //Add Shipping Address to list
                    ContactInfo contactObj2 = new ContactInfo();
                    StoreLocation StoreLocationObj2 = new StoreLocation();
                    Address addrssObj2 = new Address();
                    addrssObj2.AddressType = AddressType.Shipping;
                    addrssObj2.Address1 = txtShippAdd1.Text;
                    addrssObj2.Address2 = txtShippAdd2.Text;
                    addrssObj2.Country = string.Empty;
                    addrssObj2.City = txtShippCity.Text;
                    addrssObj2.Zip = txtShippZip.Text;
                    addrssObj2.State = dpShipState.SelectedValue;  //txtShippState.Text; ////;
                    addrssObj2.Country = txtOfficeCountry.Text;

                    contactObj2.ContactName = txtShippContactName.Text;
                    contactObj2.CellPhone = txtShippCellPhone.Text;
                    contactObj2.Comment = string.Empty;
                    contactObj2.Email1 = txtShippEmail.Text;
                    contactObj2.Email2 = txtShippEmail2.Text;
                    contactObj2.HomePhone = txtShippHomePhone.Text;
                    contactObj2.OfficePhone1 = txtShippOfficePhone1.Text;
                    contactObj2.OfficePhone2 = txtShippOfficePhone2.Text;

                    if (ViewState["addressid2"] != null)
                        StoreLocationObj2.CompanyAddressID = Convert.ToInt32(ViewState["addressid2"]);
                    else
                        StoreLocationObj2.CompanyAddressID = 0;
                    StoreLocationObj2.Active = true;
                    StoreLocationObj2.StoreAddress = addrssObj2;
                    StoreLocationObj2.StoreContact = contactObj2;
                    StoreLocationObj2.StoreID = string.Empty;
                    StoreLocationObj2.StoreName = string.Empty;
                    listObj.Add(StoreLocationObj2);
                    // end of shipping addre

                    Company getCompanyInfo = null;
                    bool storeFlag = false;
                    bool activeFlag = true;

                    // Add Store location to the list
                    foreach (RepeaterItem item in rptStore.Items)
                    {

                        TextBox txtStoreID = item.FindControl("txtStoreID") as TextBox;
                        if (txtStoreID.Text.Trim() != string.Empty)
                        {
                            storeID = txtStoreID.Text.Trim();
                            if (hshUnqStoreID.ContainsKey(storeID.ToLower()))
                            {
                                lblMsg.Text = string.Format("StoreID <b>{0}</b> can not be duplicate", storeID);
                                txtStoreID.Focus();
                                storeFlag = true;
                            }
                            else
                            {

                                hshUnqStoreID.Add(storeID.ToLower(), storeID);
                                HiddenField hdnAddressID = item.FindControl("hdnAddressID") as HiddenField;
                                if (hdnAddressID.Value == string.Empty)
                                    hdnAddressID.Value = "0";

                                TextBox txtStoreName = item.FindControl("txtStoreName") as TextBox;
                                TextBox txtAddress = item.FindControl("txtAddress") as TextBox;
                                TextBox txtCity = item.FindControl("txtCity") as TextBox;
                                //TextBox txtState = item.FindControl("txtState") as TextBox;
                                DropDownList dpStState = item.FindControl("dpStState") as DropDownList;
                                TextBox txtcountry1 = item.FindControl("txtcountry1") as TextBox;
                                TextBox txtZip = item.FindControl("txtZip") as TextBox;
                                CheckBox chkActive1 = item.FindControl("chkActive1") as CheckBox;
                                Address addrssObj1 = new Address();

                                if (chkActive1.Checked)
                                    activeFlag = false;

                                if (rptStore.Items.Count == 1 && txtStoreID.Text == string.Empty && txtStoreName.Text == string.Empty && txtCity.Text == string.Empty && txtAddress.Text == string.Empty && txtcountry1.Text == string.Empty && txtZip.Text == string.Empty)
                                {
                                    if (companyID > 0)
                                    {
                                        lblMsg.Text = "Please add at least one store";
                                        return;
                                    }
                                }

                                if (txtStoreID.Text == string.Empty)
                                {
                                    lblMsg.Text = "StoreID can not be empty";
                                    txtStoreID.Focus();
                                    storeFlag = true;
                                }
                                //else
                                //    txtStoreID.CssClass = "inputnm";

                                if (txtStoreName.Text == string.Empty)
                                {
                                    lblMsg.Text = "Store name can not be empty";
                                    storeFlag = true;
                                }
                                //else
                                //    txtStoreName.CssClass = "inputnm";

                                if (txtAddress.Text == string.Empty)
                                {
                                    lblMsg.Text = "Store address can not be empty";
                                    storeFlag = true;
                                }
                                //else
                                //    txtAddress.CssClass = "inputnm";

                                if (txtCity.Text == string.Empty)
                                {
                                    lblMsg.Text = "Store city can not be empty";

                                    //    txtCity.CssClass = "inputnmy";
                                    //txtCity.Focus();
                                    //return;
                                    storeFlag = true;
                                }

                                if (dpStState.SelectedIndex == 0)
                                {

                                    lblMsg.Text = "Store state can not be empty";


                                    storeFlag = true;
                                }
                                //else
                                //    txtCity.CssClass = "inputnm";
                                //if (ddState.SelectedIndex == 0)
                                //    lblMsg.Text = "Store addres can not be empty";
                                if (txtcountry1.Text == string.Empty)
                                {
                                    lblMsg.Text = "Store country can not be empty";

                                    //txtcountry1.CssClass = "inputnmy";
                                    //txtcountry1.Focus();
                                    //return;
                                    storeFlag = true;
                                }
                                //else
                                //    txtcountry1.CssClass = "inputnm";

                                if (txtZip.Text == string.Empty)
                                {
                                    lblMsg.Text = "Store zip can not be empty";


                                    storeFlag = true;
                                }
                                //else
                                //   txtZip.CssClass = "inputnm";


                                addrssObj1.AddressType = AddressType.Store;
                                addrssObj1.Address1 = txtAddress.Text;
                                addrssObj1.Country = txtcountry1.Text;
                                addrssObj1.City = txtCity.Text;
                                addrssObj1.Zip = txtZip.Text;
                                addrssObj1.State = dpStState.SelectedValue;// txtState.Text;

                                StoreLocation StoreLocationObj1 = new StoreLocation();
                                StoreLocationObj1.CompanyAddressID = Convert.ToInt32(hdnAddressID.Value);
                                StoreLocationObj1.StoreID = txtStoreID.Text.Trim();
                                StoreLocationObj1.StoreName = txtStoreName.Text.Trim();
                                StoreLocationObj1.Active = chkActive1.Checked;
                                StoreLocationObj1.StoreAddress = addrssObj1;
                                // StoreLocationObj1.StoreContact = contactObj;


                                //if (txtStoreID.Text != string.Empty)
                                //{
                                //    for (int i = 0; i < listObj.Count; i++)
                                //    {
                                //        //if (i > 0)
                                //        {
                                //            if (txtStoreID.Text == listObj[i].StoreID)
                                //            {
                                //                txtStoreID.Text = "Duplicate";
                                //                lblMsg.Text = "Please check your StoreID is duplicate";
                                //                return;
                                //            }
                                //        }
                                //    }
                                //}
                                listObj.Add(StoreLocationObj1);
                            }
                        }

                    }


                    //Check for at least one store on edit customer
                    if (companyID > 0)
                    {
                        if (listObj.Count == 2)
                        {
                            lblMsg.Text = "Please add at least one store";
                            return;
                        }

                        if (storeFlag)
                        {
                            if (lblMsg.Text.Trim().Length == 0)
                                lblMsg.Text = "Some of store fields are empty";
                            return;
                        }
                        if (activeFlag)
                        {
                            lblMsg.Text = "None of store is not active";
                            return;
                        }
                    }
                    List<IntegrationModel> integrationList = new List<IntegrationModel>();
                    bool integrationFlag = false;
                    int integrationModuleID = 0;
                    string error = string.Empty;
                    
                    // Add integration to the list
                    foreach (RepeaterItem item in rptInt.Items)
                    {
                        DropDownList ddlIntigration = item.FindControl("ddlIntigration") as DropDownList;
                        if (ddlIntigration.SelectedIndex > 0)
                        {
                            integrationModuleID = Convert.ToInt32(ddlIntigration.SelectedValue);
                            // = txtAPIAddress.Text.Trim();
                            if (hshUnqIntegration.ContainsKey(integrationModuleID))
                            {
                                lblMsg.Text = string.Format("API Name <b>{0}</b> can not be duplicate", ddlIntigration.SelectedItem.Text);
                                ddlIntigration.Focus();
                                integrationFlag = true;
                            }
                            else
                            {
                                hshUnqIntegration.Add(integrationModuleID, integrationModuleID);
                                HiddenField hdnIntigrationID = item.FindControl("hdnIntigrationID") as HiddenField;
                                if (hdnIntigrationID.Value == string.Empty)
                                    hdnIntigrationID.Value = "0";

                                TextBox txtAPIAddress = item.FindControl("txtAPIAddress") as TextBox;
                                TextBox txtUserName = item.FindControl("txtUserName") as TextBox;
                               // TextBox txtPass = item.FindControl("txtPass") as TextBox;
                                CheckBox chkActive2 = item.FindControl("chkActive2") as CheckBox;
                                IntegrationModel model = new IntegrationModel();
                                //Address addrssObj1 = new Address();

                                // if (chkActive2.Checked)
                                //   activeFlag = false;
                                if (ddlIntigration.SelectedItem.Text == "Endicia")
                                {
                                    bool IsValid = CompanyOperations.ValidateEndiciaConnectionString(txtUserName.Text, out error);
                                    if (!IsValid)
                                    {
                                        lblMsg.Text = error;
                                        txtUserName.Focus();
                                        integrationFlag = true;
                                    }
                                }
                                else
                                {
                                    if (ddlIntigration.SelectedItem.Text == "ShipStation")
                                    {
                                        bool IsValid = CompanyOperations.ValidateShipStationConnectionString(txtAPIAddress.Text, txtUserName.Text, out error);
                                        if (!IsValid)
                                        {
                                            lblMsg.Text = error;
                                            txtAPIAddress.Focus();
                                            integrationFlag = true;
                                        }
                                    }
                                    else if (txtAPIAddress.Text == string.Empty)
                                    {
                                        lblMsg.Text = "API Address can not be empty!";
                                        txtAPIAddress.Focus();
                                        integrationFlag = true;
                                    }
                                }
                                ////else
                                ////    txtStoreID.CssClass = "inputnm";

                                //if (txtUserName.Text == string.Empty)
                                //{
                                //    lblMsg.Text = "User name can not be empty";
                                //    integrationFlag = true;
                                //}
                                ////else
                                ////    txtStoreName.CssClass = "inputnm";

                                //if (txtPass.Text == string.Empty)
                                //{
                                //    lblMsg.Text = "Password can not be empty";
                                //    integrationFlag = true;
                                //}
                                ////else
                                ////    txtAddress.CssClass = "inputnm";


                                model.Active = chkActive2.Checked;
                                model.APIAddress = txtAPIAddress.Text.Trim();
                                model.UserName = txtUserName.Text.Trim();
                               // model.Password = txtPass.Text.Trim();
                                model.IntegrationModuleID = integrationModuleID;
                                model.IntegrationID = Convert.ToInt32(hdnIntigrationID.Value);
                                integrationList.Add(model);
                            }
                        }
                        else
                        {
                           // lblMsg.Text = "API Name is required!";
                            //integrationFlag = true;
                        }

                    }
                    if (integrationFlag)
                    {
                        if (lblMsg.Text.Trim().Length == 0)
                            lblMsg.Text = "Some of Integration fields are empty!";
                        return;
                    }

                    companyObj.IntegrationList = integrationList;
                    string returnWarehouse = string.Empty;
                    companyObj.BussinessType = txtBDAName.Text;
                    companyObj.Comment = txtComment.Text;
                    companyObj.CompanyAccountNumber = txtCompanyAccount.Text;
                    companyObj.CompanyAccountStatus = Convert.ToInt32(ddlStatus.SelectedValue);
                    companyObj.CompanyID = companyID;
                    companyObj.CompanyName = txtCompanyName.Text;
                    companyObj.CompanyShortName = txtShortName.Text;
                    companyObj.CompanySType = 1;
                    companyObj.GroupEmail = txtGroupEmail.Text;
                    companyObj.Email = txtEmail.Text;
                    companyObj.Stores = listObj;
                    companyObj.Website = txtWebsite.Text;
                    companyObj.AssingedSalesPerson = new List<SalesPerson>();// salesPersonList;
                    companyObj.Active = true;
                    companyObj.CustomerEmailList = customerEmail;
                    companyObj.WarehouseCodeList = warehouseCodeList;
                    companyObj.IsEmail = isMail;
                    companyObj.LogoPath = LogoPath;
                    companyObj.UserID = userID;
                    //companyObj.APIName = txtAPIName.Text.Trim();
                    //companyObj.APIAddress = txtAPIAddress.Text.Trim();
                    //companyObj.APIUserName = txtUserName.Text.Trim();
                    //companyObj.APIPassword = txtPassword.Text.Trim();

                    // Add/edit company details
                    returnValue = CompanyOperations.CreateCompany(companyObj, out returnWarehouse);
                    if (returnValue > 0)
                    {
                        switch (returnValue)
                        {
                            case 1:
                                lblMsg.Text = "Company name already exists";
                                break;
                            case 2:
                                lblMsg.Text = "Email already exists";
                                break;
                            case 3:
                                lblMsg.Text = "Account number already exists";
                                break;
                            case 4:
                                lblMsg.Text = "Company short name already exists";
                                break;
                            case 5:
                                lblMsg.Text = "Company name and email are already exists";
                                break;
                            case 6:
                                lblMsg.Text = "Company name and account number are already exists";
                                break;
                            case 7:
                                lblMsg.Text = "Company account number and email are already exists";
                                break;
                            case 8:
                                lblMsg.Text = "Company name and short name are already exists";
                                break;
                            case 9:
                                lblMsg.Text = "Company short name and email are already exists";
                                break;
                            case 10:
                                lblMsg.Text = "Company account number and short name are already exists";
                                break;
                            case 11:
                                lblMsg.Text = "Company name, email and account number are already exists";
                                break;
                            case 12:
                                lblMsg.Text = "Company name, email and short name are already exists";
                                break;
                            case 13:
                                lblMsg.Text = "Company name, short name and account number are already exists";
                                break;
                            case 14:
                                lblMsg.Text = "Email, short name and account number are already exists";
                                break;
                            case 15:
                                lblMsg.Text = "Company name, email, short name and account number are already exists";
                                break;
                            case 16:
                                lblMsg.Text = "Following warehouse code " + returnWarehouse + " already exists";
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        if (companyID == 0)
                        {
                            if (warehouseCodeList != null && warehouseCodeList.Count > 0)
                                lblMsg.Text = "Submitted successfully <br /> with the following default informations: <br /> StoreID: MAIN <br /> username: " + txtShortName.Text + "user & password: " + txtShortName.Text + "user";
                            else
                                lblMsg.Text = "Submitted successfully <br /> with the following default informations: <br /> StoreID: MAIN <br /> username: " + txtShortName.Text + "user & password: " + txtShortName.Text + "user <br /> Warehousecode: " + txtShortName.Text;
                            Reset();
                        }
                        else
                        {
                            List<StoreLocation> storeList = avii.Classes.UserStoreOperation.GetStoreLocationList(companyID, 0);
                            rptStore.DataSource = storeList;
                            rptStore.DataBind();
                            //getStoreLocationList
                            lblMsg.Text = "Updated successfully";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                Response.Redirect("~/Index.aspx", false);
            }
            else
            {
                if (ViewState["companyid"] != null)
                    Response.Redirect("CustomerQuery.aspx?search=1", false);
                else
                {
                    Reset();
                    lblMsg.Text = string.Empty;
                }
            }
        }

        protected void btnIntegration_Click(object sender, EventArgs e)
        {
            List<IntegrationModel> integrationList = new List<IntegrationModel>();
            IntegrationModel model = new IntegrationModel();
            model.IntegrationID = 0;
            model.IntegrationModuleID = 0;
            model.Active = true;
            model.APIAddress = string.Empty;
            model.Password = string.Empty;
            model.UserName = string.Empty;
            integrationList.Add(model);

            int integrationModuleID = 0;
            // Add integration to the list
            foreach (RepeaterItem item in rptInt.Items)
            {
                DropDownList ddlIntigration = item.FindControl("ddlIntigration") as DropDownList;
                if (ddlIntigration.SelectedIndex > 0)
                {
                    integrationModuleID = Convert.ToInt32(ddlIntigration.SelectedValue);
                    // = txtAPIAddress.Text.Trim();

                    HiddenField hdnIntigrationID = item.FindControl("hdnIntigrationID") as HiddenField;
                    if (hdnIntigrationID.Value == string.Empty)
                        hdnIntigrationID.Value = "0";

                    TextBox txtAPIAddress = item.FindControl("txtAPIAddress") as TextBox;
                    TextBox txtUserName = item.FindControl("txtUserName") as TextBox;
                   // TextBox txtPass = item.FindControl("txtPass") as TextBox;
                    CheckBox chkActive2 = item.FindControl("chkActive2") as CheckBox;
                    model = new IntegrationModel();


                    model.Active = chkActive2.Checked;
                    model.APIAddress = txtAPIAddress.Text.Trim();
                    model.UserName = txtUserName.Text.Trim();
                    //model.Password = txtPass.Text.Trim();
                    model.IntegrationModuleID = integrationModuleID;
                    model.IntegrationID = Convert.ToInt32(hdnIntigrationID.Value);
                    integrationList.Add(model);

                }

            }

            rptInt.DataSource = integrationList;// Sv.Framework.DataMembers.DataMembers.StateObjects.customerUtility.getStoreLocation(0);
            rptInt.DataBind();
        }

        protected void rptInt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddlIntigration = (DropDownList)e.Item.FindControl("ddlIntigration");
                HiddenField hdIntegrationModuleID = (HiddenField)e.Item.FindControl("hdIntegrationModuleID");
                BindIntegrationModule(ddlIntigration);

                ddlIntigration.SelectedValue = hdIntegrationModuleID.Value;



            }
        }
    }
}
