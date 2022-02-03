using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace avii.Controls
{
    public partial class ItemForm : System.Web.UI.UserControl
    {

        protected void bindCustomerDropDown()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();
            
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            btnAdd.Attributes.Add("onclick", "return confirmuser();");
            if (!IsPostBack)
            {
                bindCustomerDropDown();
                if (Request["id"] != null && !string.IsNullOrEmpty(Request.Params["id"]))
                {
                    int itemID = 0;

                    hdnItemID.Value = Request["id"];
                    int.TryParse(hdnItemID.Value, out itemID);
                    if (itemID > 0)
                    {
                        PopulateForm(itemID);
                        btnAdd.Visible = true;
                    }
                    else
                    {
                        lblError.Text = "Wrong parameter";
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtItemCode.Text = string.Empty;
            txtPhoneModel.Text = string.Empty;
            txtDesc.Text = string.Empty;
            txtColor.Text = string.Empty;
            txtWhCode.Text = string.Empty;
            this.dpPhoneMaker.SelectedIndex = 0;
            txtUPC.Text = string.Empty;
            chkPhone.Checked = true;
            chkActive.Checked = true;
            btnAdd.Visible = false;
            dpCompany.SelectedIndex = 0;
            dpTechnology.SelectedIndex = 0;
            hdnItemID.Value = string.Empty;
            txtSKU.Text = string.Empty;
            dpDeviceType.SelectedValue = string.Empty;
            chkCloseout.Checked = false;
            
        }

        private void PopulateForm(int ItemID)
        {
            avii.Classes.clsInventory invInfo = avii.Classes.PurchaseOrder.GetInventoryItem(ItemID);

            if (invInfo != null)
            {
                txtColor.Text = invInfo.Color;
                txtDesc.Text = invInfo.ItemDescription;
                txtItemCode.Text = invInfo.ItemCode;
                dpPhoneMaker.SelectedValue = invInfo.PhoneMaker;
                txtPhoneModel.Text = invInfo.PhoneModel;
                txtUPC.Text = invInfo.UPC;
                dpDeviceType.SelectedValue = invInfo.DeviceType;
                txtWhCode.Text = invInfo.WarehouseCode;
                chkActive.Checked = Convert.ToBoolean(invInfo.Active);
                chkPhone.Checked =  Convert.ToBoolean(invInfo.Phone);
                dpTechnology.SelectedValue = invInfo.Technology;
                txtSKU.Text = invInfo.SKU;
                dpDeviceType.SelectedValue = invInfo.DeviceType;
                dpCompany.SelectedValue = (invInfo.CompanyID != null && invInfo.CompanyID>0 ? invInfo.CompanyID.ToString(): string.Empty );
                chkCloseout.Checked = invInfo.Closeout;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                double tempValue = 0;
                int companyId = 0;
                int success = 0;
                //double.TryParse(txtWhCost.Text.Trim(), out tempValue);
                int.TryParse(dpCompany.SelectedValue, out companyId);

                avii.Classes.clsInventory invInfo = new avii.Classes.clsInventory();

                if (hdnItemID.Value.Trim().Length > 0)
                {
                    int itemId = 0;
                    int.TryParse(hdnItemID.Value.Trim(), out itemId);
                    invInfo.ItemID = itemId;
                }
                invInfo.Active = chkActive.Checked;
                invInfo.Color = txtColor.Text.Trim();
                invInfo.ItemDescription = txtDesc.Text.Trim();
                invInfo.ItemCode = txtItemCode.Text.Trim();
                invInfo.PhoneMaker = dpPhoneMaker.SelectedValue;
                invInfo.PhoneModel = txtPhoneModel.Text.Trim();
                invInfo.DeviceType = dpDeviceType.SelectedValue;
                invInfo.UPC = txtUPC.Text.Trim();
                invInfo.WarehouseCode = txtWhCode.Text.Trim();
                invInfo.WarehouseCost = tempValue;
                invInfo.Technology = dpTechnology.SelectedValue;
                invInfo.CompanyID = companyId;
                invInfo.DeviceType = dpDeviceType.SelectedValue;
                invInfo.SKU = txtSKU.Text.Trim();
                invInfo.Closeout = (chkCloseout.Checked ? true : false);
                if (dpDeviceType.SelectedValue.Equals("Accessory"))
                {
                    invInfo.Phone = false;
                }
                else
                {
                    invInfo.Phone = true;
                }

                if (invInfo.ItemID == 0)
                {
                    success = avii.Classes.PurchaseOrder.SetInventoryItem(invInfo);
                }
                else
                {
                    success = avii.Classes.PurchaseOrder.UpdateInventoryItem(invInfo);
                }

                if (success > 0)
                {
                    ClearForm();
                    lblError.Text = "Successfully modified";
                }
                else
                {
                    lblError.Text = "Please try again or contact administrator";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void chkActive_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
        }


    }
}