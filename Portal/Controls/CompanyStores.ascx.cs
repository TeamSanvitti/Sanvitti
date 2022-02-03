using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes; 


namespace avii.Controls
{
    public partial class CompanyStores : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["cid"] != null)
            {
                int companyID = 0;
                int.TryParse(Request.Params["cid"], out companyID);
                getCompanyDetails(companyID);
            }
        }

        private void getCompanyDetails(int companyID)
        {
            Company getCompanyInfo = CompanyOperations.getCompanyInfo(companyID, string.Empty, string.Empty);
            if (getCompanyInfo != null)
            {
                //txtBusinessType.Text = getCompanyInfo.BussinessType;
                //txtComment.Text = getCompanyInfo.Comment;
                //txtCompanyAccount.Text = getCompanyInfo.CompanyAccountNumber;
                //txtCompanyName.Text = getCompanyInfo.CompanyName;
                //txtEmail.Text = getCompanyInfo.Email;
                //txtGroupEmail.Text = getCompanyInfo.GroupEmail;
                //txtShortName.Text = getCompanyInfo.CompanyShortName;
                //txtWebsite.Text = getCompanyInfo.Website;
                //ddlCompanyType.SelectedValue = getCompanyInfo.CompanySType.ToString();
                //ddlStatus.SelectedValue = getCompanyInfo.CompanyAccountStatus.ToString();

                //List<SalesPerson> salesPerson = getCompanyInfo.AssingedSalesPerson;
                //if (salesPerson != null)
                //{
                //    if (salesPerson.Count > 0)
                //    {
                //        for (int i = 0; i < salesPerson.Count; i++)
                //        {
                //            for (int j = 0; j < lbSalesPerson.Items.Count; j++)
                //            {
                //                if (salesPerson[i].UserID == Convert.ToInt32(lbSalesPerson.Items[j].Value))
                //                {
                //                    lbSalesPerson.Items[j].Selected = true;
                //                }
                //            }
                //        }
                //    }
                //}
                //List<StoreLocation> OfficeNShippAddress = getCompanyInfo.officeAndShippAddress;
                //if (OfficeNShippAddress != null)
                //{
                //    if (OfficeNShippAddress.Count > 0)
                //    {
                //        txtCellPhone.Text = OfficeNShippAddress[0].StoreContact.CellPhone;
                //        txtHomePhone.Text = OfficeNShippAddress[0].StoreContact.HomePhone;
                //        txtOfficeAdd1.Text = OfficeNShippAddress[0].StoreAddress.Address1;
                //        txtOfficeAdd2.Text = OfficeNShippAddress[0].StoreAddress.Address2;
                //        txtOfficeCity.Text = OfficeNShippAddress[0].StoreAddress.City;
                //        txtOfficeContactName.Text = OfficeNShippAddress[0].StoreContact.ContactName;
                //        txtOfficeEmail1.Text = OfficeNShippAddress[0].StoreContact.Email1;
                //        txtOfficeEmail2.Text = OfficeNShippAddress[0].StoreContact.Email2;
                //        txtOfficePhone1.Text = OfficeNShippAddress[0].StoreContact.OfficePhone1;
                //        txtOfficePhone2.Text = OfficeNShippAddress[0].StoreContact.OfficePhone2;
                //        txtOfficeState.Text = OfficeNShippAddress[0].StoreAddress.State;
                //        txtOfficeZip.Text = OfficeNShippAddress[0].StoreAddress.Zip;

                //        txtShippCellPhone.Text = OfficeNShippAddress[1].StoreContact.CellPhone;
                //        txtShippHomePhone.Text = OfficeNShippAddress[1].StoreContact.HomePhone;
                //        txtShippAdd1.Text = OfficeNShippAddress[1].StoreAddress.Address1;
                //        txtShippAdd2.Text = OfficeNShippAddress[1].StoreAddress.Address2;
                //        txtShippCity.Text = OfficeNShippAddress[1].StoreAddress.City;
                //        txtShippContactName.Text = OfficeNShippAddress[1].StoreContact.ContactName;
                //        txtShippEmail.Text = OfficeNShippAddress[1].StoreContact.Email1;
                //        txtShippEmail2.Text = OfficeNShippAddress[1].StoreContact.Email2;
                //        txtShippOfficePhone1.Text = OfficeNShippAddress[1].StoreContact.OfficePhone1;
                //        txtShippOfficePhone2.Text = OfficeNShippAddress[1].StoreContact.OfficePhone2;
                //        txtShippState.Text = OfficeNShippAddress[1].StoreAddress.State;
                //        txtShippZip.Text = OfficeNShippAddress[1].StoreAddress.Zip;


                //    }
                //}
                List<StoreLocation> StoreLocationList = getCompanyInfo.Stores;
                if (StoreLocationList != null && StoreLocationList.Count > 0)
                {
                    rptStore.DataSource = StoreLocationList;
                    rptStore.DataBind();
                }
                else
                {
                    lblMsg.Text = "Company does not have Stores assigned";
                }


            }

        }
  
    }
}