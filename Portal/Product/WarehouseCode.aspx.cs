using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii
{
    public partial class WarehouseCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCompany();

                if (Request["w"] != null)
                { 
                    int warehouseCodeGUID = Convert.ToInt32(Request["w"]);

                    ViewState["warehouseCodeGUID"] = warehouseCodeGUID;
                    GetWarehouseCodeDetail(warehouseCodeGUID);
                }
            }
        }
        private void GetWarehouseCodeDetail(int warehouseCodeGUID)
        {

            CustomerWarehouseCode warehouseCode = WarehouseCodeOperations.GetWarehouseCodeInfo(warehouseCodeGUID);
            if (warehouseCode != null && warehouseCode.CompanyID > 0)
            {
                ddlCompany.SelectedValue = warehouseCode.CompanyID.ToString();
                txtWarehouseCode.Text = warehouseCode.WarehouseCode;
                chkActive.Checked = Convert.ToBoolean(warehouseCode.Active);

            }
        }
        private void BindCompany()
        {
            ddlCompany.DataSource = avii.Classes.RMAUtility.getRMAUserCompany(-1, "", 1, -1);
            ddlCompany.DataValueField = "companyID";
            ddlCompany.DataTextField = "companyName";
            ddlCompany.DataBind();
            ListItem item = new ListItem("--All--", "0");
            ddlCompany.Items.Insert(0, item);

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int companyID, warehouseCodeGUID, returnValue;
            companyID = warehouseCodeGUID = returnValue = 0;

            string warehouseCode = string.Empty;

            warehouseCode = txtWarehouseCode.Text.Trim();
            if (ViewState["warehouseCodeGUID"] != null)
                warehouseCodeGUID = Convert.ToInt32(ViewState["warehouseCodeGUID"]);

            //if (warehouseCodeGUID > 0)
            //{
            if (ddlCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(ddlCompany.SelectedValue);
            CustomerWarehouseCode customerWarehouseCode = new CustomerWarehouseCode();
            try
            {
                if (!string.IsNullOrEmpty(warehouseCode) && companyID > 0)
                {
                    customerWarehouseCode.CompanyID = companyID;
                    customerWarehouseCode.WarehouseCode = warehouseCode;
                    customerWarehouseCode.WarehouseCodeGUID = warehouseCodeGUID;
                    customerWarehouseCode.Active = chkActive.Checked;
                    returnValue = WarehouseCodeOperations.CreateUpdateCompanyWarehouseCode(customerWarehouseCode);
                    if (returnValue > 0)
                    {
                        lblMsg.Text = "Warehouse code already exists";
                    }
                    else
                    {
                        if (warehouseCodeGUID == 0)
                        {
                            ClearAll();
                            lblMsg.Text = "Submitted successfully";

                        }
                        else
                            lblMsg.Text = "Updated successfully";
                    }

                }
                else
                    lblMsg.Text = "Missing required fields";

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

            //}
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (ViewState["warehouseCodeGUID"] == null)
                ClearAll();
            else
                Response.Redirect("WarehousecodeQuery.aspx?search=s");
        }
        private void ClearAll()
        {
            lblMsg.Text = string.Empty;
            txtWarehouseCode.Text = string.Empty;
            ddlCompany.SelectedIndex = 0;
            chkActive.Checked = false;
        }
    }
}