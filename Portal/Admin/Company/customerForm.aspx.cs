using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using avii.Classes;

namespace avii.Admin
{
    public partial class customerForm : System.Web.UI.Page
    {
        List<clsUserManagement> selectedUserList = new List<clsUserManagement>();
        static int companyID;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["adm"] == null)
            {
                Response.Redirect("~/logon.aspx");
            }

           
            if (!Page.IsPostBack)
            {
                
                BindSalesPersonList();
                if (Request["CompanyID"] != null && Request["CompanyID"] != "")
                {
                    companyID = Convert.ToInt32(Request["CompanyID"]);
                    lblHeader.Text = "Edit Customer";
                    btnCancel.Text = "Back to Search";
                    GetCustomerData();
                }
            }
        }
        protected void GenerateUserList()
        {
            try
            {
                for (int i = 0; i < lbSalesPerson.Items.Count; i++)
                {
                    if (lbSalesPerson.Items[i].Selected == true)
                    {
                        avii.Classes.clsUserManagement objuser = new avii.Classes.clsUserManagement();
                        objuser.UserID = Convert.ToInt32(lbSalesPerson.Items[i].Value);
                        selectedUserList.Add(objuser);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }
        protected void BindSalesPersonList()
        {
            UserUtility objuser = new UserUtility();
            List<clsUserManagement> userList = new List<clsUserManagement>();
            string UserType = ConfigurationManager.AppSettings["UserType"].ToString();
            try
            {
                userList = objuser.getUserList("", -1, UserType, -1, -1, -1);
    
                lbSalesPerson.DataSource = userList;
                lbSalesPerson.DataTextField = "username";
                lbSalesPerson.DataValueField = "userid";
                lbSalesPerson.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }
        protected void GetCustomerData()
        {
            CustomerUtility objcustomer = new CustomerUtility();
            clsCustomer customerInfo = new clsCustomer();
            List<salesPerson> salesPersonList = new List<salesPerson>();
            
            try
            {
                customerInfo = objcustomer.getCustomerInfo(companyID, "", "", "", "", 1, "");
                txtAccNum.Text = customerInfo.CompanyAccountNumber;
                txtAdd1.Text = customerInfo.Address1;
                txtAdd2.Text = customerInfo.Address2;
                txtAdd3.Text = customerInfo.Address3;
                txtCity.Text = customerInfo.City;
                txtCmpName.Text = customerInfo.CompanyName;
                txtCountry.Text = customerInfo.Country;
                txtEmail.Text = customerInfo.Email;
                txtState.Text = customerInfo.State;
                txtZip.Text = customerInfo.Zip;
                salesPersonList = customerInfo.SalesPersonlist;

                for (int i = 0; i < salesPersonList.Count; i++)
                {
                    for (int j = 0; j < lbSalesPerson.Items.Count; j++)
                    {
                        if (salesPersonList[i].UserID == Convert.ToInt32(lbSalesPerson.Items[j].Value))
                        {
                            lbSalesPerson.Items[j].Selected = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            
            string xmlString = string.Empty;

            
            CustomerUtility objcustomer = new CustomerUtility();
            clsGeneral objSerialize = new clsGeneral();

            try
            {
                if (companyID == 0)
                {
                    clsCustomer customerInfo = objcustomer.getCustomerInfo(-1, txtCmpName.Text.Trim(), txtAccNum.Text.Trim(), "", "", 2, txtEmail.Text.Trim());
                    if (customerInfo.CompanyName != string.Empty || customerInfo.CompanyName != "")
                    {
                        lblMsg.Text = customerInfo.CompanyName;
                        return;
                    }
                }

                GenerateUserList();

                if (selectedUserList.Count > 0)
                    xmlString = objSerialize.serializeObjetToXMLString((object)this.selectedUserList, "ArrayOfClsUserManagement", "clsUserManagement");
                objcustomer.InsertNewCustomer(companyID, txtCmpName.Text, txtAccNum.Text, txtAdd1.Text, txtAdd2.Text, txtAdd3.Text, txtCity.Text, txtState.Text, txtCountry.Text, txtZip.Text, txtEmail.Text, xmlString);
                CleanForm();
                
                if (companyID > 0)
                    lblMsg.Text = "Customer Updated Successfully!";
                else
                    lblMsg.Text = "Customer Submit Successfully!";

                companyID = 0;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (companyID==0)
                CleanForm();
            else
                Response.Redirect("CustomerQuery.aspx?search=1");
        }
        protected void CleanForm()
        {
            txtAccNum.Text = string.Empty;
            txtAdd1.Text = string.Empty;
            txtAdd2.Text = string.Empty;
            txtAdd3.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtCmpName.Text = string.Empty;
            txtCountry.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtState.Text = string.Empty;
            txtZip.Text = string.Empty;
            lblMsg.Text = string.Empty;
            
            for (int i = 0; i < lbSalesPerson.Items.Count; i++)
            {
                lbSalesPerson.Items[i].Selected = false;
            }
        }

        
    }
}
