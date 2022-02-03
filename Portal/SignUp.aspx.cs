using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int userID = 0;
                int companyID = 0;
                int addressID = 0;
                int accountStatus = 0;
                int returnValue = 0;
                string returnMessage = string.Empty;
                lblMsg.Text = string.Empty;
                string userName, pwd, customerName, emailAddress, phone, userType, userStores, CompanyShortName;
                userName = pwd = customerName = emailAddress = phone = userType = userStores = CompanyShortName = string.Empty;
                //if (lbStoreID.Items.Count > 0)
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
                    CompanyShortName = txtCompanyCode.Text.Trim();
                    //  if (userStores != string.Empty)
                    {
                        returnValue = CreateUser(userID, userName, pwd, customerName, emailAddress, phone, userType, companyID, addressID, userStores, accountStatus, CompanyShortName);
                        if (returnValue > 0)
                        {
                            lblMsg.Text = "Username already exist";
                            txtUserName.Text = string.Empty;
                            txtUserName.Focus();
                            return;
                        }
                        else
                        if (returnValue == -1)
                        {
                            lblMsg.Text = CompanyShortName + " company code not exist";
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

                        //BindUsers(sortExpression, sortDirection, accountStatus);
                        ClearAll();

                        if (userID == 0)
                            lblMsg.Text = "Submitted successfuly";
                        else
                            lblMsg.Text = "Updated successfuly";
                    }
                    //else
                    //  lblMsg.Text = "Please select atleast one store.";


                }
                //else
                //  lblMsg.Text = "There is no store assigned to this company yet. You can not request an account without store. Please contact Administrator to get more infomration.";
            }
            else
            {
               // lblMsg.Text = "Invalid form!";

            }
        }
        private string UserStores()
        {
            string stores = string.Empty;
            //if (lbStoreID.Items.Count == 1)
            //    stores = lbStoreID.Items[0].Value;
            //else
            //{
            //    for (int i = 0; i < lbStoreID.Items.Count; i++)
            //    {
            //        if (lbStoreID.Items[i].Selected)
            //        {
            //            if (stores == string.Empty)
            //                stores = lbStoreID.Items[i].Value;
            //            else
            //                stores = stores + "," + lbStoreID.Items[i].Value;
            //        }
            //    }
            //}

            return stores;
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
            btnVarify.Visible = true;
            //hdnUserID.Value = string.Empty;

            //for (int i = 0; i < lbStoreID.Items.Count; i++)
            //{
            //    lbStoreID.Items[i].Selected = false;
            //}
        }
        private int CreateUser(int userID, string userName, string pwd, string customerName, string emailAddress, string phone, string userType, int companyID, int addressID, string userStores, int accountStatus, string CompanyShortName)
        {
            int returnResult = 0;
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
            userObj.CompanyShortName = CompanyShortName;
            userObj.OfficeAndShippAddress = addressListObj;
            returnResult = RegistrationOperation.CreateUser(userObj);
            return returnResult;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
    }
}