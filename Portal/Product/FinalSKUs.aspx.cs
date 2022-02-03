using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Catalog;
using SV.Framework.Models.Common;

namespace avii.Product
{
    public partial class FinalSKUs : System.Web.UI.Page
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
                BindCustomer();
                trSKU.Visible = false;
                btnSubmit.Visible = false;
                btnViewLog.Visible = false;
                btnCancel.Visible = false;
                //trHr.Visible = false;
                plnSKU.Visible = false;

                if (Session["adm"] == null)
                {
                    trCust.Visible = true;
                    trAdmin.Visible = false;
                    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    if (userInfo != null)
                    {
                        dpCompany.SelectedValue = userInfo.CompanyGUID.ToString();                       
                        BindCompanySKU(userInfo.CompanyGUID);
                        dpCompany.Enabled = false;
                        trSKU.Visible = true;
                        btnViewLog.Visible = false;
                        btnSubmit.Visible = false;
                        btnCancel.Visible = false;

                    }
                }

            }
        }

        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();
        }

        private void GetProductLog()
        {
            SV.Framework.Catalog.ItemLogOperation itemLogOperation = SV.Framework.Catalog.ItemLogOperation.CreateInstance<SV.Framework.Catalog.ItemLogOperation>();
           
            lblLogMsg.Text = "";
            gvLog.DataSource = null;
            gvLog.DataBind();
            lblCategoryName.Text = "";
            lblModelNumber.Text = "";
            lblProductName.Text = "";

            if (ddlSKU.SelectedIndex > 0)
            {
                int itemGUID = 0;
                int itemCompanyGUID = Convert.ToInt32(ddlSKU.SelectedValue);
                if (itemCompanyGUID > 0)
                {
                    List<ItemLog> logList = itemLogOperation.GetProductLog(itemGUID, itemCompanyGUID);
                    if (logList != null && logList.Count > 0)
                    {
                        gvLog.DataSource = logList;
                        gvLog.DataBind();

                        lblCategoryName.Text = logList[0].CategoryName;
                        lblModelNumber.Text = logList[0].ModelNumber;
                        lblProductName.Text = logList[0].ProductName;


                    }
                    else
                    {
                        lblLogMsg.Text = "No log found!";
                    }
                }
                else
                {
                    lblLogMsg.Text = "kitted SKU not selected!";
                }
            }
            else
            {
                lblLogMsg.Text = "kitted SKU not selected!";
            }
        }
        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;

           // rptSKUsPrice.DataSource = null;
          //  rptSKUsPrice.DataBind();
            //rptESN.Visible = false;
            lblMsg.Text = string.Empty;
            //lblConfirm.Text = string.Empty;

            btnSubmit.Visible = false;
            btnViewLog.Visible = false;
            btnCancel.Visible = false;
          //  trHr.Visible = false;
            btnSearch.Visible = true;
            plnSKU.Visible = false;
            //lblCount.Text = string.Empty;

            trSKU.Visible = true;
            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            if (companyID > 0)
                BindCompanySKU(companyID);
            else
            {
                trSKU.Visible = false;
                plnSKU.Visible = false;
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
            }
        }
        private void BindCompanySKU(int companyID)
        {
            SV.Framework.Catalog.FinishSKUOperations finishSKUOperations = SV.Framework.Catalog.FinishSKUOperations.CreateInstance<SV.Framework.Catalog.FinishSKUOperations>();
            
            lblMsg.Text = string.Empty;
            List<CompanySKUno> skuList = finishSKUOperations.GetCompanyFinalOrRawSKUList(companyID, true);
            if (skuList != null && skuList.Count > 0)
            {
                Session["skuList"] = skuList;
                ddlSKU.DataSource = skuList;
                ddlSKU.DataValueField = "ItemcompanyGUID";
                ddlSKU.DataTextField = "SKU";

                ddlSKU.DataBind();
                ListItem newList = new ListItem("", "");
                ddlSKU.Items.Insert(0, newList);
            }
            else
            {
                plnSKU.Visible = false;
                trSKU.Visible = false;
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                lblMsg.Text = "No SKU/Product are assigned to selected Customer";

            }


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();
            SV.Framework.Catalog.FinishSKUOperations finishSKUOperations = SV.Framework.Catalog.FinishSKUOperations.CreateInstance<SV.Framework.Catalog.FinishSKUOperations>();

            lblMsg.Text = string.Empty;
            int userId = 0;
            if (Session["userInfo"] != null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null && userInfo.UserGUID > 0)
                {
                    userId = userInfo.UserGUID;                    
                }
                
            }
            //companyID = 0;
            int itemCompanyGUID = 0;
            int displayNameCtr = 0;

            if (dpCompany.SelectedIndex > 0)
            {
                if (ddlSKU.SelectedIndex > 0)
                {
                    itemCompanyGUID = Convert.ToInt32(ddlSKU.SelectedValue);

                    DataTable skuTable = new DataTable();

                    // Adding Columns    
                    DataColumn COLUMN = new DataColumn();
                    COLUMN.ColumnName = "ItemCompanyGUID";
                    COLUMN.DataType = typeof(int);
                    skuTable.Columns.Add(COLUMN);

                    DataColumn COLUMN2 = new DataColumn();
                    COLUMN2.ColumnName = "Quantity";
                    COLUMN2.DataType = typeof(int);
                    skuTable.Columns.Add(COLUMN2);

                    DataColumn COLUMN3 = new DataColumn();
                    COLUMN3.ColumnName = "DisplayName";
                    COLUMN3.DataType = typeof(string);
                    skuTable.Columns.Add(COLUMN3);
                    Kittedsku kittedsku = new Kittedsku();
                    kittedsku.ItemCompanyGUID = itemCompanyGUID;
                    List<Kitrawsku> rawskulist = new List<Kitrawsku>();
                    Kitrawsku rawsku = new Kitrawsku();


                    try
                    {
                        foreach (RepeaterItem row in rptSKUs.Items)
                        {

                            CheckBox chkSel = row.FindControl("chkSel") as CheckBox;
                            HiddenField hdIsDisplayName = row.FindControl("hdIsDisplayName") as HiddenField;
                            HiddenField hdItemCompanyGUID = row.FindControl("hdItemCompanyGUID") as HiddenField;
                            TextBox txtQty = row.FindControl("txtQty") as TextBox;
                            TextBox txtDisplayName = row.FindControl("txtName") as TextBox;
                            if (chkSel.Checked)
                            {
                                DataRow dr = skuTable.NewRow();
                                if (!string.IsNullOrEmpty(txtQty.Text))
                                {
                                    if(hdIsDisplayName.Value.ToLower() == "true")
                                    {
                                        displayNameCtr += 1;
                                        if (displayNameCtr > 3)
                                        {
                                            lblMsg.Text = "Maximum to 3 display name allowed!";
                                            txtDisplayName.Focus();
                                            return;
                                        }                                      

                                        if (string.IsNullOrEmpty(txtDisplayName.Text))
                                        {
                                            lblMsg.Text = "Display name is required!";
                                            txtDisplayName.Focus();
                                            return;
                                        }
                                    }
                                    rawsku = new Kitrawsku();
                                    dr[0] = Convert.ToInt32(hdItemCompanyGUID.Value);
                                    dr[1] = Convert.ToInt32(txtQty.Text);
                                    dr[2] = txtDisplayName.Text.Trim();
                                    rawsku.ItemCompanyGUID = Convert.ToInt32(hdItemCompanyGUID.Value);
                                    rawsku.Quantity = Convert.ToInt32(txtQty.Text);
                                    rawsku.DisplayName = txtDisplayName.Text.Trim();
                                    
                                    skuTable.Rows.Add(dr);
                                    rawskulist.Add(rawsku);
                                }
                                else
                                {
                                    lblMsg.Text = "Quantity is required!";
                                    txtQty.Focus();
                                    return;
                                }
                            }
                        }
                        kittedsku.RawSKUs = rawskulist;
                        string ActionName = "Kitted SKU Create";
                        List<RawSKU> RawSKUs = Session["RawSKUs"] as List<RawSKU>;
                        if(RawSKUs != null && RawSKUs.Count > 0)
                        {
                            foreach(RawSKU rawSKU1 in RawSKUs)
                            {
                                if(rawSKU1.Quantity > 0)
                                    ActionName = "Kitted SKU Update";
                            }
                        }
                        if (skuTable != null && skuTable.Rows.Count > 0)
                        {
                            bool returnValue = finishSKUOperations.FinishedSKUInsertUpdate(skuTable, itemCompanyGUID, userId, kittedsku, ActionName);
                            if (returnValue)
                            {
                                //ClearForm();
                                btnSubmit.Enabled = false;
                                lblMsg.Text = "Submitted successfully";
                            }
                            else
                                lblMsg.Text = "no  record updated";


                        }
                        else
                            lblMsg.Text = "No reord to update";
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = ex.Message;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
                    }

                }
                else
                    lblMsg.Text = "Finished SKU is required!";

            }
            else
                lblMsg.Text = "Customer is required!";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            BindRawSKUs();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }

        private void BindRawSKUs()
        {   SV.Framework.Catalog.FinishSKUOperations finishSKUOperations = SV.Framework.Catalog.FinishSKUOperations.CreateInstance<SV.Framework.Catalog.FinishSKUOperations>();

            hdnIsESNRequired.Value = "";

            plnSKU.Visible = false;
            btnSubmit.Visible = false;
            //btnSubmit.Enabled = false;
            btnViewLog.Visible = false;
            btnCancel.Visible = false;
            //trHr.Visible = false;
            lblMsg.Text = string.Empty;
            int companyID, itemCompanyGUID;
            bool IsCustomer = false, IsESNRequired = true;
            if (!string.IsNullOrEmpty(hdIsESNRequired.Value))
                IsESNRequired = Convert.ToBoolean(hdIsESNRequired.Value);
            if (Session["adm"] == null)
            {
                IsCustomer = true;
            }
            companyID = itemCompanyGUID = 0;
            if (dpCompany.SelectedIndex > 0)
            {
                if (ddlSKU.SelectedIndex > 0)
                {
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                    if (ddlSKU != null && ddlSKU.Items.Count > 0)
                    {
                        if (ddlSKU.SelectedIndex > 0)
                            itemCompanyGUID = Convert.ToInt32(ddlSKU.SelectedValue);

                        int totalSKUs = 0;
                        List<RawSKU> rawSKUList = finishSKUOperations.GetCompanyAssignedRawSKUList(companyID, itemCompanyGUID, IsCustomer, IsESNRequired);
                        if (rawSKUList != null && rawSKUList.Count > 0)
                        {
                            rptSKUs.DataSource = rawSKUList;
                            rptSKUs.DataBind();

                            Session["RawSKUs"] = rawSKUList;
                            foreach(RawSKU item in rawSKUList)
                            {
                                if (item.Quantity > 0 && item.IsESNRequired)
                                {
                                    hdnIsESNRequired.Value = "1";                                    
                                }
                                if (item.Quantity > 0)
                                {
                                   // btnSubmit.Enabled = true;
                                    totalSKUs += 1;
                                    //lblCount
                                }
                            }
                            lblCount.Text = "<strong>Total selected SKU(s):</strong> " + totalSKUs;
                            if (lblMsg.Text == string.Empty)
                            {
                                if (Session["adm"] == null)
                                {
                                        btnViewLog.Visible = false;
                                        btnSubmit.Visible = false;
                                        btnCancel.Visible = false;                                    
                                }
                                else
                                {
                                    btnSubmit.Visible = true;
                                    btnViewLog.Visible = true;
                                    btnCancel.Visible = true;
                                }
                                plnSKU.Visible = true;
                                //trHr.Visible = true;
                                //lblMsg.CssClass = "errorGreenMsg";
                                //lblConfirm.Text = "SIM file is ready to upload";
                                //btnSearch.Visible = false;
                                //btnSubmit.Visible = true;
                                //btnViewLog.Visible = true;
                                /// btnSubmit2.Visible = true;
                                pnlSearch.Visible = true;
                                // trSKU.Visible = false;

                            }
                            else
                            {
                                // trSKU.Visible = false;
                                btnSearch.Visible = true;
                                btnSubmit.Visible = false;
                                btnViewLog.Visible = false;

                                //  btnSubmit2.Visible = false;
                                //pnlSearch.Visible = false;

                            }
                        }
                        else
                        {
                            btnViewLog.Visible = false;
                            btnSubmit.Visible = false;
                            btnCancel.Visible = false;
                            //trHr.Visible = false;
                            rptSKUs.DataSource = null;
                            rptSKUs.DataBind();
                            lblMsg.Text = "No records found!";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "No SKU/Product are assigned to selected Customer";
                        rptSKUs.DataSource = null;
                        rptSKUs.DataBind();
                    }

                }
                else
                {
                    lblMsg.Text = "SKU is required!";
                    rptSKUs.DataSource = null;
                    rptSKUs.DataBind();
                }
            }
            else
            {
                lblMsg.Text = "Customer is required!";
                rptSKUs.DataSource = null;
                rptSKUs.DataBind();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {            
           // ClearForm();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            //ddlSKU.DataSource = null;
            //ddlSKU.DataBind();
            btnSubmit.Enabled = true;
            btnSubmit.Visible = false;
            btnViewLog.Visible = false;
            btnCancel.Visible = false;
            rptSKUs.DataSource = null;
            plnSKU.Visible = false;
            rptSKUs.DataBind();
            if (Session["adm"] != null)
            {
                
                trSKU.Visible = false;
                dpCompany.SelectedIndex = 0;
                
            }
            else
            {
                ddlSKU.SelectedIndex = 0;
            }


            //BindRawSKUs();
            // ClearForm();
        }

        protected void btnViewLog_Click(object sender, EventArgs e)
        {
            GetProductLog();
            RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        protected void ddlSKU_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            pnlSearch.Visible = true;
            plnSKU.Visible = false;
            int itemCompanyGuid = 0;
            if(ddlSKU.SelectedIndex > 0)
            {
                itemCompanyGuid = Convert.ToInt32(ddlSKU.SelectedValue);
                List<CompanySKUno> skuList = Session["skuList"] as List<CompanySKUno>;
                if(skuList != null && skuList.Count > 0)
                {
                    List<CompanySKUno> SKUInfo = (from item in skuList where item.ItemcompanyGUID.Equals(itemCompanyGuid) select item).ToList();
                    if(SKUInfo != null && SKUInfo.Count > 0)
                    {
                        hdIsKittedBox.Value = SKUInfo[0].IsKittedBox.ToString();
                        hdIsESNRequired.Value = SKUInfo[0].IsESNRequired.ToString();
                    }
                }
            }
        }
    }
}