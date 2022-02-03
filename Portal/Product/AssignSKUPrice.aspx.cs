using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Product
{
    public partial class AssignSKUPrice : System.Web.UI.Page
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
                //if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if (!IsPostBack)
            {
                BindCustomer();
                trSKU.Visible = false;
            }
        }

        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;

            gvSKUPrice.DataSource = null;
            gvSKUPrice.DataBind();
            //rptESN.Visible = false;
            lblMsg.Text = string.Empty;
            //lblConfirm.Text = string.Empty;

            btnSubmit.Visible = false;
            btnSearch.Visible = true;
            btnSubmit2.Visible = false;
            pnlSubmit.Visible = false;
            //lblCount.Text = string.Empty;

            trSKU.Visible = true;
            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            if (companyID > 0)
                BindCompanySKU(companyID);
            else
            {
                trSKU.Visible = false;
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
            }
        }
        private void BindCompanySKU(int companyID)
        {
            lblMsg.Text = string.Empty;
            List<CompanySKUno> skuList = MslOperation.GetCompanySKUList(companyID, 2);
            if (skuList != null && skuList.Count > 0)
            {
                ddlSKU.DataSource = skuList;
                ddlSKU.DataValueField = "ItemcompanyGUID";
                ddlSKU.DataTextField = "SKU";

                ddlSKU.DataBind();
                ListItem newList = new ListItem("", "");
                ddlSKU.Items.Insert(0, newList);
            }
            else
            {
                trSKU.Visible = false;
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                lblMsg.Text = "No SIM SKU/Product are assigned to select Customer";

            }


        }
        
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSKUPrice.PageIndex = e.NewPageIndex;
            if (Session["SKUPrices"] != null)
            {
                List<SKUPrices> skuPricesList = (List<SKUPrices>)Session["SKUPrices"];

                gvSKUPrice.DataSource = skuPricesList;
                gvSKUPrice.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            int companyID = 0;
            //companyID = 0;
            int userID = 0;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
                //ViewState["userid"] = userID;
            }
            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            
                List<SKUPrices> skuPricesList = new List<SKUPrices>();
                SKUPrices skuPrice = null;
                try
                {
                    foreach (GridViewRow row in gvSKUPrice.Rows)
                    {
                        skuPrice = new SKUPrices();
                        Label lblSKU = row.FindControl("lblSKU") as Label;
                        TextBox txtPrice = row.FindControl("txtPrice") as TextBox;
                        if (lblSKU != null && txtPrice != null && !string.IsNullOrEmpty(txtPrice.Text))
                        {
                            skuPrice.SKU = lblSKU.Text;
                            skuPrice.SKUPrice = Convert.ToDouble(txtPrice.Text);
                            skuPricesList.Add(skuPrice);

                        }

                    }

                    if (skuPricesList != null && skuPricesList.Count > 0)
                    {
                        bool returnValue = SKUPricesOperations.SKUPriceInsertUpdate(skuPricesList, companyID, userID);
                        if (returnValue)
                        {
                            ClearForm();
                            lblMsg.Text = "Successfully updated";
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
                }

            }
            else
                lblMsg.Text = "Customer is required!";




        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindSKUPrices();
        }
        private void BindSKUPrices()
        {
            lblMsg.Text = string.Empty;
            int companyID, itemCompanyGUID;
            companyID = itemCompanyGUID = 0;
            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
                if (ddlSKU != null && ddlSKU.Items.Count > 0)
                {
                    if (ddlSKU.SelectedIndex > 0)
                        itemCompanyGUID = Convert.ToInt32(ddlSKU.SelectedValue);


                    List<SKUPrices> skuPricesList = SKUPricesOperations.GetCompanySKUPrices(companyID, itemCompanyGUID);
                    if (skuPricesList != null && skuPricesList.Count > 0)
                    {
                        gvSKUPrice.DataSource = skuPricesList;
                        gvSKUPrice.DataBind();
                        Session["SKUPrices"] = skuPricesList;

                        if (lblMsg.Text == string.Empty)
                        {
                            //lblMsg.CssClass = "errorGreenMsg";
                            //lblConfirm.Text = "SIM file is ready to upload";
                            btnSearch.Visible = false;
                            btnSubmit.Visible = true;
                            btnSubmit2.Visible = true;
                            pnlSubmit.Visible = true;
                            trSKU.Visible = false;

                        }
                        else
                        {
                            trSKU.Visible = false;
                            btnSearch.Visible = true;
                            btnSubmit.Visible = false;

                            btnSubmit2.Visible = false;
                            pnlSubmit.Visible = false;

                        }
                    }
                    else
                    {
                        gvSKUPrice.DataSource = null;
                        gvSKUPrice.DataBind();
                        lblMsg.Text = "No records found!";
                    }
                }
                else
                    lblMsg.Text = "No SIM SKU/Product are assigned to select Customer";


            }
            else
            {
                lblMsg.Text = "Customer is required!";
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            trSKU.Visible = false;
            trHr.Visible = true;
            dpCompany.SelectedIndex = 0;
            
            gvSKUPrice.DataSource = null;
            gvSKUPrice.DataBind();
            //rptESN.Visible = false;
            lblMsg.Text = string.Empty;
            //lblConfirm.Text = string.Empty;

            btnSubmit.Visible = false;
            btnSearch.Visible = true;
            btnSubmit2.Visible = false;
            pnlSubmit.Visible = false;
            //lblCount.Text = string.Empty;

        }
        
    }
}