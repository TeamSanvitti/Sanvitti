using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;


namespace avii.Admin
{
    public partial class CustomerQuery : System.Web.UI.Page
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
                //if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if (!IsPostBack)
            {
                if (Request["search"] != null && Request["search"] != "")
                {
                    if (Session["searchcompany"] != null)
                    {
                        string searchCriteria = (string)Session["searchcompany"];
                        string[] searchArr = searchCriteria.Split('~');
                        txtCompanyName.Text = searchArr[0].ToString();
                        txtCompanyAC.Text = searchArr[1].ToString();
                        txtStoreID.Text = searchArr[2].ToString();
                        txtContactName.Text = searchArr[3].ToString();
                        ddlStatus.SelectedValue = searchArr[4].ToString();
                        BindCompanyGrid();
                    }
                }
            }

        }
        protected void Edit_click(object sender, CommandEventArgs e)
        {
            int companyID = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("customer-form.aspx?companyID=" + companyID);

        }

        protected void imgViewStores_Commnad(object sender, CommandEventArgs e)
        {
            //lblStore.Text = string.Empty;
            int companyID = Convert.ToInt32(e.CommandArgument);
            ViewState["companyID"] = companyID;


            Control tmp = LoadControl("~/controls/CustomerStores.ascx");
            avii.Controls.CustomerStores ctlCustomerStores = tmp as avii.Controls.CustomerStores;
            pnlStores.Controls.Clear();
            int rmaGUID = Convert.ToInt32(e.CommandArgument);
            // ViewState["rmaGUID"] = rmaGUID;
            if (tmp != null)
            {

                ctlCustomerStores.BindStores(companyID);
            }
            pnlStores.Controls.Add(ctlCustomerStores);
            

            //BindStores(companyID);
            //mdlPopup5.Show();
            RegisterStartupScript("jsUnblockDialog", "unblockDialog();");



        }
        protected void imgViewWarehouse_Commnad(object sender, CommandEventArgs e)
        {
            int companyID = Convert.ToInt32(e.CommandArgument);
            Control tmp = LoadControl("~/controls/WarehouseCode.ascx");
            avii.Controls.WarehouseCode ctlWarehouseCode = tmp as avii.Controls.WarehouseCode;
            pnlWhCode.Controls.Clear();
            int rmaGUID = Convert.ToInt32(e.CommandArgument);
            // ViewState["rmaGUID"] = rmaGUID;
            if (tmp != null)
            {

                ctlWarehouseCode.BindWarehouseCode(companyID);
            }
            pnlWhCode.Controls.Add(ctlWarehouseCode);
            
            //BindWarehouseCode(companyID);
            //mdlPopup5.Show();
            RegisterStartupScript("jsUnblockDialog", "unblockWarehouseDialog();");
            


        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomer.PageIndex = e.NewPageIndex;

            if (Session["company"] != null)
            {
                List<Company> companyList = (List<Company>)Session["company"];

                gvCustomer.DataSource = companyList;
                gvCustomer.DataBind();

            }
            else
                lblMsg.Text = "Session expire!";
        }
        private void BindCompanyGrid()
        {
            string companyName, companyAc, contactName,storeID;
            int companyAccountStatus = 0;
            lblMsg.Text = string.Empty;
            companyName = (txtCompanyName.Text.Trim().Length > 0 ? txtCompanyName.Text.Trim() : string.Empty);
            companyAc = (txtCompanyAC.Text.Trim().Length > 0 ? txtCompanyAC.Text.Trim() : string.Empty);
            contactName = (txtContactName.Text.Trim().Length > 0 ? txtContactName.Text.Trim() : string.Empty);
            storeID = (txtStoreID.Text.Trim().Length > 0 ? txtStoreID.Text.Trim() : string.Empty);

            if (ddlStatus.SelectedIndex > 0)
            {
                companyAccountStatus = Convert.ToInt32(ddlStatus.SelectedValue);
            }
            //if (string.IsNullOrEmpty(companyAc) && string.IsNullOrEmpty(companyName) && string.IsNullOrEmpty(contactName) && string.IsNullOrEmpty(storeID) && companyAccountStatus==0)
            //{
            //    lblMsg.Text = "Please select the search criteria";
            //    gvCompany.DataSource = null;
            //    gvCompany.Visible = false;
            //    searchPanel.Visible = false;
            //}
            //else
            {
                string searchCriteria = companyName + "~" + companyAc + "~" + storeID + "~" + contactName + "~" + companyAccountStatus;
                Session["searchcompany"] = searchCriteria;

                List<Company> getCompanyList = CompanyOperations.getCompanyList(0, companyName, companyAc, storeID, contactName, companyAccountStatus);

                if (getCompanyList != null)
                {
                    if (getCompanyList.Count > 0)
                    {
                        gvCustomer.DataSource = getCompanyList;
                        gvCustomer.DataBind();
                        Session["company"] = getCompanyList;
                        //gvCustomer.Visible = true;
                        searchPanel.Visible = true;
                    }
                    else
                    {
                        lblMsg.Text = "No matching record exists for selected search criteria";
                        gvCustomer.DataSource = null;
                        gvCustomer.DataBind();
                        Session["company"] = null;
                        //gvCustomer.Visible = false;
                        searchPanel.Visible = false;
                    }
                }
                else
                {
                    lblMsg.Text = "No matching record exists for selected search criteria";
                    gvCustomer.DataSource = null;
                    gvCustomer.DataBind();
                    //gvCustomer.Visible = false;
                    Session["company"] = null;
                    searchPanel.Visible = false;
                }
            }
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }
        protected void gvCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
                return;

            ImageButton imgStore = e.Row.FindControl("imgStore") as ImageButton;
            if (imgStore != null)
                imgStore.OnClientClick = "openDialogAndBlock('Stores', '" + imgStore.ClientID + "')";
            ImageButton imgView = e.Row.FindControl("imgView") as ImageButton;
            if (imgView != null)
                imgView.OnClientClick = "openWarehouseDialogAndBlock('Warehouse Code', '" + imgView.ClientID + "')";
        }
        
        protected void DeleteStore_click(object sender, CommandEventArgs e)
        {
            //ViewState["flag"] = 1;
            //if (ViewState["companyID"] != null)
            //{
            //    int addressID = Convert.ToInt32(e.CommandArgument);
            //    CompanyOperations.DeleteCompanyAndStore(0, addressID);
            //    //gvCompany.Rebind();
            //    BindStores(Convert.ToInt32(ViewState["companyID"]));

            //    lblStore.Text = "Store deleted successfully";
            //}
        }
        protected void lnkDelete_click(object sender, CommandEventArgs e)
        {
            ViewState["flag"] = 1;
            int companyID = Convert.ToInt32(e.CommandArgument);
            CompanyOperations.DeleteCompanyAndStore(companyID, 0);
            //gvCompany.Rebind();
            BindCompanyGrid();
            lblMsg.Text = "Company deleted successfully";
        }
        
        protected void btnSearch_click(object sender, EventArgs e)
        {
            ViewState["flag"] = 1;
            BindCompanyGrid();
            //gvCompany.Rebind();

        }
        protected void btnCancel_Click1(object sender, EventArgs e)
        {
            ViewState["flag"] = null;
            //gvCompany.Rebind();
            gvCustomer.DataSource = null;
            gvCustomer.DataBind();
            txtContactName.Text = string.Empty;
            txtCompanyName.Text = string.Empty;
            txtCompanyAC.Text = string.Empty;
            txtStoreID.Text = string.Empty;
            lblMsg.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
        }
        private void BindWarehouseCode(int companyID)
        {
            //List<CustomerWarehouseCode> warehouseCodeList = WarehouseCodeOperations.GetCompanyWarehouseCode(companyID, null, false);
            //if (warehouseCodeList != null && warehouseCodeList.Count > 0)
            //{
            //    gvWarehouse.DataSource = warehouseCodeList;
            //    gvWarehouse.DataBind();
            //    lblMessage.Text = string.Empty;
            //}
            //else
            //{
            //    lblMessage.Text = "No record exists";
            //    gvWarehouse.DataSource = null;
            //    gvWarehouse.DataBind();
            //}
        }
        
    }
}
