using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Product
{
    public partial class SKUPricesApprove : System.Web.UI.Page
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
                
            }
        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        
        

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSKUPrice.PageIndex = e.NewPageIndex;
            if (Session["SKUPricesapprove"] != null)
            {
                List<SKUPrices> skuPricesList = (List<SKUPrices>)Session["SKUPricesapprove"];

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
            bool isProduct = true;
            if (ddlOption.SelectedIndex > 0)
                isProduct = false;

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
            }
                List<SKUPrices> skuPricesList = new List<SKUPrices>();
                SKUPrices skuPrice = null;
                try
                {
                    foreach (GridViewRow row in gvSKUPrice.Rows)
                    {
                        CheckBox chkSKU = row.FindControl("chkSKU") as CheckBox;
                        //TextBox txtPrice = row.FindControl("txtPrice") as TextBox;
                        if (chkSKU.Checked)
                        {
                            Label lblSKU = row.FindControl("lblSKU") as Label;
                            Label lblProductCode = row.FindControl("lblPCode") as Label;
                            skuPrice = new SKUPrices();
                            skuPrice.SKU = lblSKU.Text;
                            skuPrice.ProductCode = lblProductCode.Text;
                            //skuPrice.SKUPrice = Convert.ToDouble(txtPrice.Text);
                            skuPricesList.Add(skuPrice);
                        }
                    }

                    if (skuPricesList != null && skuPricesList.Count > 0)
                    {
                        bool returnValue = SKUPricesOperations.SKUPriceApprove(skuPricesList, companyID, userID, isProduct);
                        if (returnValue)
                        {
                            ClearForm();
                            lblMsg.Text = "Approved successfully";
                        }
                        else
                            lblMsg.Text = "no  record updated";


                    }
                    else
                        lblMsg.Text = "No reords selected";
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                }

            //}
            //else
            //    lblMsg.Text = "Customer is required!";




        }
        protected void btnReject_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            int companyID = 0;
            //companyID = 0;
            bool isProduct = true;
            if (ddlOption.SelectedIndex > 0)
                isProduct = false;

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
            }
            List<SKUPrices> skuPricesList = new List<SKUPrices>();
            SKUPrices skuPrice = null;
            try
            {
                foreach (GridViewRow row in gvSKUPrice.Rows)
                {
                    CheckBox chkSKU = row.FindControl("chkSKU") as CheckBox;
                    //TextBox txtPrice = row.FindControl("txtPrice") as TextBox;
                    if (chkSKU.Checked)
                    {
                        Label lblSKU = row.FindControl("lblSKU") as Label;
                        Label lblProductCode = row.FindControl("lblPCode") as Label;
                        skuPrice = new SKUPrices();
                        skuPrice.SKU = lblSKU.Text;
                        skuPrice.ProductCode = lblProductCode.Text;
                        //skuPrice.SKUPrice = Convert.ToDouble(txtPrice.Text);
                        skuPricesList.Add(skuPrice);
                    }
                }

                if (skuPricesList != null && skuPricesList.Count > 0)
                {
                    bool returnValue = SKUPricesOperations.SKUPriceReject(skuPricesList, companyID, userID, isProduct);
                    if (returnValue)
                    {
                        ClearForm();
                        lblMsg.Text = "Rejected successfully";
                    }
                    else
                        lblMsg.Text = "no  record updated";


                }
                else
                    lblMsg.Text = "No reords selected";
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

            //}
            //else
            //    lblMsg.Text = "Customer is required!";




        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindSKUPrices();
        }
        private void BindSKUPrices()
        {
            lblMsg.Text = string.Empty;
            int companyID = 0;
            bool isProduct = true;
            if (ddlOption.SelectedIndex > 0)
                isProduct = false;

            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);

            }
            List<SKUPrices> skuPricesList = SKUPricesOperations.GetCompanySKUPricesToApprove(companyID, isProduct);
            if (skuPricesList != null && skuPricesList.Count > 0)
            {
                gvSKUPrice.DataSource = skuPricesList;
                gvSKUPrice.DataBind();
                Session["SKUPricesapprove"] = skuPricesList;

                if (lblMsg.Text == string.Empty)
                {
                    //lblMsg.CssClass = "errorGreenMsg";
                    //lblConfirm.Text = "SIM file is ready to upload";
                    btnSearch.Visible = false;
                    btnSubmit.Visible = true;
                    btnReject.Visible = true;
                    btnReject2.Visible = true;
                    btnSubmit2.Visible = true;
                    pnlSubmit.Visible = true;
                    trHr.Visible = false;

                }
                else
                {
                    trHr.Visible = true;
                    btnSearch.Visible = true;
                    btnSubmit.Visible = false;
                    btnReject.Visible = false;
                    btnReject2.Visible = false;
                    
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



            //}
            //else
            //{
            //    lblMsg.Text = "Customer is required!";
            //}
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            ddlOption.SelectedIndex = 0;
            trHr.Visible = true;
            dpCompany.SelectedIndex = 0;
            gvSKUPrice.DataSource = null;
            gvSKUPrice.DataBind();
            //rptESN.Visible = false;
            lblMsg.Text = string.Empty;
            //lblConfirm.Text = string.Empty;

            btnSubmit.Visible = false;
            btnReject.Visible = false;
            btnReject2.Visible = false;
                    
            btnSearch.Visible = true;
            btnSubmit2.Visible = false;
            pnlSubmit.Visible = false;
            //lblCount.Text = string.Empty;

        }

    }
}