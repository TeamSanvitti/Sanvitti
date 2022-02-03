using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI.WebControls;
using avii.Classes;
namespace avii
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userInfo"] == null)
                Response.Redirect("/logon.aspx", false);

            if (!IsPostBack)
            {
                if (Session["userInfo"] != null)
                {
                    UserInfo userInfo = Session["userInfo"] as UserInfo;
                    if (userInfo != null && userInfo.UserGUID > 0)
                    {
                        string sortExpression = "UserID";
                        string sortDirection = "ASC";
                        ViewState["companyid"] = userInfo.CompanyGUID;
                        BindUsers(sortExpression, sortDirection, 1);
                        BindCustomerStores(userInfo.CompanyGUID);

                    }
                }
            }
        }
        protected void BindCustomerStores(int companyID)
        {
            List<StoreLocation> storeList = RegistrationOperation.GetCompanyStoreList(companyID, 0);

            //DataTable dt = avii.Classes.clsCompany.GetCompanyStores(userID);
            if (storeList != null && storeList.Count > 0)
            {
                lbStoreID.DataSource = storeList;
                lbStoreID.DataValueField = "CompanyAddressID";
                lbStoreID.DataTextField = "StoreID";
                lbStoreID.DataBind();
                //lbStoreID.Items.Insert(0, new ListItem("", ""));
            }
            else
            {
                lbStoreID.DataSource = null;
                lbStoreID.DataBind();
                pnlCreateNew.Visible = false;
                lblMsgs.Text = "There is no store assigned to this company yet. You can not request an account without store. Please contact Administrator to get more infomration.";
                
            }
            

        }

        [WebMethod]
        public static string FetchCustomer(string userName)
        {
            string returnMessage = string.Empty;

            returnMessage = avii.Classes.RegistrationOperation.ValidateUserName(userName);
            
            return returnMessage;
        }

        private int CreateUser(int userID, string userName, string pwd, string customerName, string emailAddress, string phone, string userType, int companyID, int addressID, string userStores, int accountStatus)
        {
            //int returnValue = 0;
            List<StoreLocation> addressListObj = new List<StoreLocation>();

            UserRegistration userObj = new UserRegistration();
            Address addrssObj = new Address();
            addrssObj.Address1 = string.Empty;
            addrssObj.AddressType = AddressType.Office1;
            //addrssObj.AddressType = 3;
            addrssObj.Country = string.Empty;
            addrssObj.City = string.Empty;
            addrssObj.Zip = string.Empty;
            addrssObj.State = string.Empty;
            ContactInfo contactObj = new ContactInfo();
            contactObj.ContactName = customerName;
            contactObj.CellPhone = string.Empty;
            contactObj.Comment = string.Empty;
            contactObj.Email1 = emailAddress;
            contactObj.Email2 = string.Empty;
            contactObj.HomePhone = string.Empty;
            contactObj.OfficePhone1 = phone;
            contactObj.OfficePhone2 = string.Empty;

            StoreLocation storeLocationObj = new StoreLocation();
            storeLocationObj.CompanyAddressID = addressID;
            storeLocationObj.StoreID = string.Empty;
            storeLocationObj.StoreName = string.Empty;
            storeLocationObj.Active = true;
            storeLocationObj.StoreAddress = addrssObj;
            storeLocationObj.StoreContact = contactObj;

            addressListObj.Add(storeLocationObj);
            userObj.AccountStatusID = accountStatus;
            userObj.AccStatus = string.Empty;
            userObj.Active = true;
            userObj.AerovoiceAdminUserName = string.Empty;
            userObj.CompanyAccNo = string.Empty;
            userObj.CompanyID = companyID;
            userObj.Email = emailAddress;
            userObj.Password = pwd;
            userObj.UserID = userID;
            userObj.UserName = userName;
            userObj.UserType = userType;
            userObj.Stores = userStores;
            userObj.OfficeAndShippAddress = addressListObj;

            return RegistrationOperation.CreateUser(userObj);
        }

        protected void GridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            int accountStatus = 1;
            if (ViewState["statusID"] != null)
                accountStatus = Convert.ToInt32(ViewState["statusID"]);
            string sortExpression = e.SortExpression;

            
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;

                
                
                BindUsers(sortExpression, "DESC", accountStatus);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                
                BindUsers(sortExpression, "ASC", accountStatus);
            }

        }
        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }
        private void BindUsers(string sortExpression, string direction, int statusID)
        {
            ViewState["SortExpression"] = sortExpression;
            string sortBy = sortExpression + " " + direction;
            ViewState["statusID"] = statusID;
            int companyID = 0;
            if(ViewState["companyid"] != null)
                companyID = Convert.ToInt32(ViewState["companyid"]);

            lblMsgs.Text = string.Empty;
            List<UserRegistration> userList = RegistrationOperation.GetUserInfoList(companyID, string.Empty, sortBy, statusID);
            if (userList.Count > 0)
            {
                gvUsers.DataSource = userList;
                gvUsers.DataBind();
                btnViewAll.Visible = true;
            }

            else
            {
                if (statusID == 1)
                    lblMsgs.Text = "No pending users exist";
                else
                    lblMsgs.Text = "No users exist";
                gvUsers.DataSource = null;
                gvUsers.DataBind();
                // btnViewAll.Visible = false;
            }
            
            
        }
        protected void btnViewAll_Click(object sender, EventArgs e)
        {
            string sortExpression = string.Empty;

            if (ViewState["SortExpression"] != null)
                sortExpression = ViewState["SortExpression"].ToString();
            string sortDirection = "ASC";
            if (ViewState["sortDirection"] != null)
                sortDirection = ViewState["sortDirection"].ToString();

            int statusID = 0;


            if (btnViewAll.Text == "Show Pending only")
            {
                btnViewAll.Text = "Show All Users";
                statusID = 1;
            }
            else
            {
                btnViewAll.Text = "Show Pending only";
                statusID = 0;
            }

            BindUsers(sortExpression, sortDirection, statusID);
        }
        protected void btnSubmit_click(object sender, EventArgs e)
        {
            int userID = 0;
            int companyID = 0;
            int addressID = 0;
            int accountStatus = 0;
            int returnValue = 0;
            string returnMessage = string.Empty;
            lblMsgs.Text = string.Empty;
            string userName, pwd, customerName, emailAddress, phone, userType, userStores;
            userName = pwd = customerName = emailAddress = phone = userType = userStores = string.Empty;
            if (lbStoreID.Items.Count > 0)
            {
                if (ViewState["addressid"] != null)
                    addressID = Convert.ToInt32(ViewState["addressid"]);

                if (ViewState["userid"] != null)
                    userID = Convert.ToInt32(ViewState["userid"]);
                if (ViewState["companyid"] != null)
                    companyID = Convert.ToInt32(ViewState["companyid"]);
                userType = "Company";
                userName = txtUserName.Text.Trim();
                pwd = txtPassword.Text.Trim();
                customerName = txtCustomerName.Text.Trim();
                emailAddress = txtEmail.Text.Trim();
                phone = txtPhone.Text.Trim();
                userStores = UserStores();
                if (userStores != string.Empty)
                {
                    returnValue = CreateUser(userID, userName, pwd, customerName, emailAddress, phone, userType, companyID, addressID, userStores, accountStatus);
                    if (returnValue > 0)
                    {
                        lblMsgs.Text = "Username already exist";
                        txtUserName.Text = string.Empty;
                        txtUserName.Focus();
                        return;
                    }
                    
                    if (ViewState["statusID"] != null)
                        accountStatus = Convert.ToInt32(ViewState["statusID"]);
                    string sortExpression = string.Empty;

                    if (ViewState["SortExpression"] != null)
                        sortExpression = ViewState["SortExpression"].ToString();
                    string sortDirection = "ASC";
                    if (ViewState["sortDirection"] != null)
                        sortDirection = ViewState["sortDirection"].ToString();

                    BindUsers(sortExpression, sortDirection, accountStatus);
                    ClearAll();

                    if (userID == 0)
                        lblMsgs.Text = "Submitted successfuly";
                    else
                        lblMsgs.Text = "Updated successfuly";
                }
                else
                    lblMsgs.Text = "Please select atleast one store.";


            }
            else
                lblMsgs.Text = "There is no store assigned to this company yet. You can not request an account without store. Please contact Administrator to get more infomration.";




        
        }
        private string UserStores()
        {
            string stores = string.Empty;
            if (lbStoreID.Items.Count == 1)
                stores = lbStoreID.Items[0].Value;
            else
            {
                for (int i = 0; i < lbStoreID.Items.Count; i++)
                {
                    if (lbStoreID.Items[i].Selected)
                    {
                        if (stores == string.Empty)
                            stores = lbStoreID.Items[i].Value;
                        else
                            stores = stores + "," + lbStoreID.Items[i].Value;
                    }
                }
            }

            return stores;
        }

        protected void btnCreateNew_Click(object sender, EventArgs e)
        {
            ClearAll();
            ModalPopupExtender1.Show();

        }
        protected void btnCancel_click(object sender, EventArgs e)
        {
            ClearAll();
            lblMsgs.Text = string.Empty;
            
        }
        private void ClearAll()
        {
            lblMessage.Text = string.Empty;
            lblMsg.Text = string.Empty;
            ViewState["userid"] = null;
            ViewState["addressid"] = null;
            txtConfirmPwd.Attributes.Add("value", "");
            //txtPassword.Attributes.Add("value", "");
            txtConfirmEmail.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            //txtPassword.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtUserName.ReadOnly = false;
            txtConfirmPwd.ReadOnly = false;
            txtPassword.ReadOnly = false;
            btnVarify.Visible= true;
            hdnUserID.Value = string.Empty;

            for (int i = 0; i < lbStoreID.Items.Count; i++)
            {
                lbStoreID.Items[i].Selected = false;
            }
        }
        protected void imgDelete_Commnad(object sender, CommandEventArgs e)
        {
            lblMsgs.Text = string.Empty;
            int userID = Convert.ToInt32(e.CommandArgument);
            RegistrationOperation.DeleteUser(userID);

            lblMsgs.Text = "User deleted successfuly";
            int accountStatus = 0;
            if (ViewState["statusID"] != null)
                accountStatus = Convert.ToInt32(ViewState["statusID"]);

            string sortExpression = string.Empty;

            if (ViewState["SortExpression"] != null)
                sortExpression = ViewState["SortExpression"].ToString();
            string sortDirection = "ASC";
            if (ViewState["sortDirection"] != null)
                sortDirection = ViewState["sortDirection"].ToString();

            BindUsers(sortExpression, sortDirection, accountStatus);

        }
        protected void imgEdit_Commnad(object sender, CommandEventArgs e)
        {

            btnVarify.Visible = false;
            int userID = Convert.ToInt32(e.CommandArgument);
            ViewState["userid"] = userID;
            hdnUserID.Value = userID.ToString();
            UserRegistration userObj = RegistrationOperation.GetUserInfo(userID);
            if (userObj != null)
            {
                txtConfirmEmail.Text = userObj.Email;
                txtConfirmPwd.Attributes.Add("value", userObj.Password);
                txtPassword.Attributes.Add("value", userObj.Password);
                txtEmail.Text = userObj.Email;
                if (userObj.OfficeAndShippAddress != null && userObj.OfficeAndShippAddress.Count > 0)
                {
                    ViewState["addressid"] = userObj.OfficeAndShippAddress[0].CompanyAddressID;
                    txtCustomerName.Text = userObj.OfficeAndShippAddress[0].StoreContact.ContactName;
                    txtPhone.Text = userObj.OfficeAndShippAddress[0].StoreContact.OfficePhone1;
                }
                txtUserName.Text = userObj.UserName;
                txtUserName.ReadOnly = true;
                txtConfirmPwd.ReadOnly = true;
                txtPassword.ReadOnly = true;


                //lbStoreID.DataSource = userObj.UserStores;
                //lbStoreID.DataTextField = "storename";
                //lbStoreID.DataValueField = "addressid";
                //lbStoreID.DataBind();
                for (int i = 0; i < userObj.UserStores.Count; i++)
                {
                    for (int j = 0; j < lbStoreID.Items.Count; j++)
                    {
                        if (lbStoreID.Items[j].Value != string.Empty)
                        {
                            if (userObj.UserStores[i].CompanyAddressID == Convert.ToInt32(lbStoreID.Items[j].Value))
                            {
                                lbStoreID.Items[j].Selected = true;
                            }
                        }
                    }
                }
            }
            ModalPopupExtender1.Show();
        }
        


    }
}