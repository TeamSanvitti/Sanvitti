using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.States;

namespace avii.Category
{
    public partial class ManageCategory : System.Web.UI.Page
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
                if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if (!IsPostBack)
            {
                BindPrentCategories();
                if (Session["CategoryGUID"] != null)
                {
                    int categoryGUID = Convert.ToInt32(Session["CategoryGUID"]);
                    ViewState["CategoryGUID"] = categoryGUID;
                    Session["CategoryGUID"] = null;
                    GetCategoryInfo(categoryGUID);
                }
            }
        }

        private void GetCategoryInfo(int categoryGUID)
        {
            CategoryModel model = SV.Framework.Operations.CategoryOperation.GetCategoryInfo(categoryGUID);
            if (model != null && !string.IsNullOrWhiteSpace(model.CategoryName))
            {
                txtCategoryName.Text = model.CategoryName;
                txtDesc.Text = model.CategoryDesc;
                ddlParent.SelectedValue = model.ParentCategoryGUID.ToString();
                ddlStatus.SelectedValue = model.Status;
                chkESN.Checked = model.ESNRequired;
                chkForecasting.Checked = model.Forecasting;
                chkIsSIM.Checked = model.IsSIM;
                chkKittedBox.Checked = model.KittedBox;
                chkReStocking.Checked = model.ReStocking;
                chkRMAAllowed.Checked = model.RMAAllowed;
                chkSIMRequired.Checked = model.SIMRequired;
                chkSKUMapping.Checked = model.SKUMapping;
                //ViewState["CategoryImage"] = model.CategoryImage;

            }
        }
        private void BindPrentCategories()
        {
            try
            {
                CategoryModel model = new CategoryModel();
                model.CategoryName = string.Empty;
                model.BaseCategories = true;
                model.LeafCategories = false;
                model.ParentCategoryGUID = -1;
                model.CategoryStatusID = -1;

                List<CategoryModel> categoryList = SV.Framework.Operations.CategoryOperation.GetCategoryList(model);
                if (categoryList != null && categoryList.Count > 0)
                {
                    ddlParent.DataSource = categoryList;
                    ddlParent.DataTextField = "CategoryName";
                    ddlParent.DataValueField = "CategoryGUID";
                    ddlParent.DataBind();
                    ListItem listItem = new ListItem("", "-1");
                    ddlParent.Items.Insert(0, listItem);

                }
            }
            catch(Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int CategoryGUID = 0;
            bool categoryStatus = Convert.ToBoolean(ddlStatus.SelectedValue);
            lblMsg.Text = string.Empty;
            string categoryImage = string.Empty, errorMessage = string.Empty;
            string actualFilename = string.Empty;
            string fileStoreLocation = Server.MapPath("~/images/Category/");

            if (ViewState["CategoryGUID"] != null)
                CategoryGUID = Convert.ToInt32(ViewState["CategoryGUID"]);

            if (fu.HasFile)
            {
                categoryImage = fu.FileName;
                actualFilename = System.IO.Path.GetFileName(fu.PostedFile.FileName);
                fu.PostedFile.SaveAs(fileStoreLocation + actualFilename);

            }
            else
            {

            }
            
            CategoryModel model = new CategoryModel();
            model.CategoryDesc = txtDesc.Text.Trim();
            model.CategoryGUID = CategoryGUID;
            model.CategoryImage = categoryImage;
            model.CategoryName = txtCategoryName.Text.Trim();
            if (ddlParent.SelectedIndex > 0)
                model.ParentCategoryGUID = Convert.ToInt32(ddlParent.SelectedValue);
            model.CategoryStatus = categoryStatus;
            model.ESNRequired = chkESN.Checked;
            model.Forecasting = chkForecasting.Checked;
            model.IsSIM = chkIsSIM.Checked;
            model.KittedBox = chkKittedBox.Checked;
            model.ReStocking = chkReStocking.Checked;
            model.RMAAllowed = chkRMAAllowed.Checked;
            model.SIMRequired = chkSIMRequired.Checked;
            model.SKUMapping = chkSKUMapping.Checked;
            if (!string.IsNullOrWhiteSpace(model.CategoryName))
            {
                int returnResult = SV.Framework.Operations.CategoryOperation.CategoryInsertUpdate(model, out errorMessage);
                if (returnResult > 0 && string.IsNullOrWhiteSpace(errorMessage))
                {
                    lblMsg.Text = "Submitted successfully";
                    btnSubmit.Enabled = false;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(errorMessage))
                        lblMsg.Text = errorMessage;
                    else
                        lblMsg.Text = "Data could not saved";
                }
            }
            else
            {
                lblMsg.Text = "Category name is required!";
            }
        }
        private void ClearForm()
        {
            btnSubmit.Enabled = true;
            lblMsg.Text = string.Empty;
            txtCategoryName.Text = string.Empty;
            txtDesc.Text = string.Empty;
            ddlParent.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            chkESN.Checked = false;
            chkForecasting.Checked = false;
            chkIsSIM.Checked = false;
            chkKittedBox.Checked = false;
            chkReStocking.Checked = false;
            chkRMAAllowed.Checked = false;
            chkSIMRequired.Checked = false;
            chkSKUMapping.Checked = false;

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (ViewState["CategoryGUID"] != null)
            {
                Response.Redirect("~/Category/CategorySearch.aspx?search=1");
            }
            else
                ClearForm();

        }

    }
}