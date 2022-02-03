using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii
{
    public partial class WarehouseCodeQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    url = System.Configuration.ConfigurationSettings.AppSettings["LogonPage"].ToString();

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
                BindCompany();
                if (Request["search"] != null && Request["search"] != "")
                {
                    if (Session["searchcompany"] != null)
                    {
                        string searchCriteria = (string)Session["searchcompany"];
                        string[] searchArr = searchCriteria.Split('~');
                        ddlCompany.SelectedValue = searchArr[0].ToString();
                        txtWarehouseCode.Text = searchArr[1].ToString();
                        SearchWarehouseCode();
                    }
                }
            }
        }
        private void BindCompany()
        {
            List<RMAUserCompany> companyList = avii.Classes.RMAUtility.getRMAUserCompany(-1, "", 1, -1);
            Session["companylist"] = companyList;
            ddlCompany.DataSource = companyList;
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

            
            if (ViewState["warehouseCodeGUID"] != null)
            {
                warehouseCode = txtWhCode.Text.Trim();
                warehouseCodeGUID = Convert.ToInt32(ViewState["warehouseCodeGUID"]);

                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
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
                            SearchWarehouseCode();
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
            }
            else
                lblMsg.Text = "Missing required fields";

        }
        protected void btnCancel2_Click(object sender, EventArgs e)
        {
            ViewState["warehouseCodeGUID"] = null;
            ClearAll();
            
        }
        private void ClearAll()
        {
            lblMsg.Text = string.Empty;
            txtWhCode.Text = string.Empty;
            dpCompany.SelectedIndex = 0;
            chkActive.Checked = false;
        }
        protected void btnSearch_click(object sender, EventArgs e)
        {
            SearchWarehouseCode();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtWarehouseCode.Text = string.Empty;
            ddlCompany.SelectedIndex = 0;
            gvWarehouse.DataSource = null;
            gvWarehouse.DataBind();
            lblMsg.Text = string.Empty;

        }

        private void SearchWarehouseCode()
        {
            bool validForm = true;
            string warehouseCode;
            int companyID = 0;

            warehouseCode = null;
            try
            {
                lblMsg.Text = string.Empty;
                warehouseCode = (txtWarehouseCode.Text.Trim().Length > 0 ? txtWarehouseCode.Text.Trim() : null);

                if (ddlCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(ddlCompany.SelectedValue);



                if (string.IsNullOrEmpty(warehouseCode) && companyID == 0)
                {
                    validForm = false;
                }
                if (validForm)
                {
                    string searchCriteria = companyID + "~" + warehouseCode;
                    Session["searchcompany"] = searchCriteria;
                    List<CustomerWarehouseCode> warehouseCodeList = WarehouseCodeOperations.GetCompanyWarehouseCode(companyID, warehouseCode, false);
                    if (warehouseCodeList.Count > 0)
                    {
                        Session["warehousecode"] = warehouseCodeList;
                        gvWarehouse.DataSource = warehouseCodeList;
                        gvWarehouse.DataBind();
                    }
                    else
                    {
                        lblMsg.Text = "No record exists";
                        Session["warehousecode"] = null;
                        gvWarehouse.DataSource = null;
                        gvWarehouse.DataBind();
                    }
                }
                else
                {
                    lblMsg.Text = "Please select the search criteria";
                    Session["warehousecode"] = null;
                    gvWarehouse.DataSource = null;
                    gvWarehouse.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void imgDelete_Commnad(object sender, CommandEventArgs e)
        {
            int warehoudeCodeGUID = Convert.ToInt32(e.CommandArgument);
            WarehouseCodeOperations.DeleteCompanyWarehouseCode(warehoudeCodeGUID);
            SearchWarehouseCode();
            lblMsg.Text = "Deleted successfully";


        }
        private void GetWarehouseCodeDetail(int warehouseCodeGUID)
        {

            CustomerWarehouseCode warehouseCode = WarehouseCodeOperations.GetWarehouseCodeInfo(warehouseCodeGUID);
            if (warehouseCode != null && warehouseCode.CompanyID > 0)
            {
                dpCompany.SelectedValue = warehouseCode.CompanyID.ToString();
                txtWhCode.Text = warehouseCode.WarehouseCode;
                chkActive.Checked = Convert.ToBoolean(warehouseCode.Active);

            }
        }
        private void BindCompany2()
        {
            List<RMAUserCompany> companyList = null;
            if (Session["companylist"] != null)
                companyList = (List<RMAUserCompany>)Session["companylist"];
            else
                companyList = avii.Classes.RMAUtility.getRMAUserCompany(-1, "", 1, -1);
            Session["companylist"] = companyList;
            dpCompany.DataSource = companyList;
            dpCompany.DataValueField = "companyID";
            dpCompany.DataTextField = "companyName";
            dpCompany.DataBind();
            ListItem item = new ListItem("--All--", "0");
            dpCompany.Items.Insert(0, item);

        }
        protected void imgEdit_Commnad(object sender, CommandEventArgs e)
        {
            int warehoudeCodeGUID = Convert.ToInt32(e.CommandArgument);
            BindCompany2();
            ViewState["warehouseCodeGUID"] = warehoudeCodeGUID;
            GetWarehouseCodeDetail(warehoudeCodeGUID);
            mdlPopup5.Show();

            //Response.Redirect("WarehouseCode.aspx?w=" + warehoudeCodeGUID, false);

        }

    }
}