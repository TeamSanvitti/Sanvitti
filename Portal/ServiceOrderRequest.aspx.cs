using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using SV.Framework.Models.SOR;
using SV.Framework.Models.Common;
using SV.Framework.SOR;

namespace avii
{
    public partial class ServiceOrder_Request : System.Web.UI.Page
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
                int companyID = 0, ServiceRequestID = 0;
                btnSubmit.Visible = true;
                if (Session["adm"] != null)
                {
                    BindCustomer();
                    hdnCustomer.Value = "1";

                    //companyID = 464;
                }
                else
                {
                    hdnCustomer.Value = "";
                    trCustomer.Visible = false;
                    if (Session["userInfo"] != null)
                    {
                        avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                        if (userInfo != null)
                        {
                            companyID = userInfo.CompanyGUID;
                            ViewState["companyID"] = companyID;
                        }
                    }
                    BindCompanyKittedSKU(companyID);
                    BindUsers(companyID);
                }
                if (Session["ServiceRequestID"] != null)
                {
                    ServiceRequestID = Convert.ToInt32(Session["ServiceRequestID"]);
                    ViewState["ServiceRequestID"] = ServiceRequestID;
                    GetServiceOrderRequestDetail(ServiceRequestID);
                    Session["ServiceRequestID"] = null;
                    if(Session["view"] != null)
                    {
                        btnCancel.Visible = false;
                        btnSubmit.Visible = false;
                        Session["view"] = null;
                    }
                }
                else
                {
                    ddlStatus.Visible = false;
                    lblStatus.Visible = false;
                }
                ReadOlnyAccess();
            }
        }

        private void ReadOlnyAccess()
        {
            //string url = HttpContext.Current.Request.Url.AbsoluteUri;
            // http://localhost:1302/TESTERS/Default6.aspx

            string path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            // /TESTERS/Default6.aspx
            if (path != null)
            {
                path = path.ToLower();
                List<avii.Classes.MenuItem> menuItems = Session["MenuItems"] as List<avii.Classes.MenuItem>;
                foreach (avii.Classes.MenuItem item in menuItems)
                {
                    if (item.Url.ToLower().Contains(path) && item.IsReadOnly)
                    {
                        ViewState["IsReadOnly"] = true;
                        btnSubmit.Visible = false;
                    }
                }

            }

        }
        private void GetServiceOrderRequestDetail(int serviceRequestID)
        {
            ServiceRequestOperations serviceRequestOperations = ServiceOrderOperation.CreateInstance<ServiceRequestOperations>();

            ServiceOrderRequestModel sorModel = serviceRequestOperations.GetServiceOrderRequest(serviceRequestID);
            if(sorModel != null && sorModel.ServiceRequestID > 0)
            {
                txtSORNumber.Text = sorModel.SORNumber;
                txtOrderQty.Text = sorModel.Quantity.ToString();
                txtComments.Text = sorModel.Comments;
                dpCompany.SelectedValue = sorModel.CompanyID.ToString();
                if (Session["adm"] != null)
                {

                    BindCompanyKittedSKU(sorModel.CompanyID);
                    BindUsers(sorModel.CompanyID);
                }
                else
                {
                    if (sorModel.StatusID > 1)
                        btnSubmit.Visible = false;
                }
                ddlKitted.SelectedValue = sorModel.ItemCompanyGUID.ToString();
                ddlUser.SelectedValue = sorModel.RequestedBy.ToString();
                BindSORStatus();
                ddlStatus.SelectedValue = sorModel.StatusID.ToString();



            }
        }

        private void BindSORStatus()
        {
            ServiceRequestOperations serviceRequestOperations = SV.Framework.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.SOR.ServiceRequestOperations>();

            List<ServiceRequestStatus> statusList = serviceRequestOperations.GetSoRStatusList();
            ddlStatus.DataSource = statusList;
            ddlStatus.DataValueField = "StatusID";
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataBind();
        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();
        }

        private void BindUsers(int companyID)
        {
            avii.Classes.UserUtility objUser = new avii.Classes.UserUtility();
            List<avii.Classes.clsUserManagement> userList = objUser.getUserList("", companyID, "", -1, -1, -1, true);
            if(userList!=null && userList.Count > 0)
            {
                ddlUser.DataSource = userList;
                ddlUser.DataValueField = "UserID";
                ddlUser.DataTextField = "UserName";
                ddlUser.DataBind();

                ListItem newList = new ListItem("", "0");
                ddlUser.Items.Insert(0, newList);
            }
            else
            {
                ddlUser.DataSource = null;
                ddlUser.DataBind();
                lblMsg.Text = "No users are assigned to selected Customer";
            }
        }

        private void BindCompanyKittedSKU(int companyID)
        {
           SV.Framework.Catalog.FinishSKUOperations FinishSKUOperations = SV.Framework.Catalog.FinishSKUOperations.CreateInstance<SV.Framework.Catalog.FinishSKUOperations>();

            lblMsg.Text = string.Empty;
            List<SV.Framework.Models.Catalog.CompanySKUno> skuList = FinishSKUOperations.GetCompanyFinalOrRawSKUList(companyID, true);
            if (skuList != null && skuList.Count > 0)
            {
                //ViewState["skulist"] = skuList;
                ddlKitted.DataSource = skuList;
                ddlKitted.DataValueField = "ItemcompanyGUID";
                ddlKitted.DataTextField = "SKU";

                ddlKitted.DataBind();
                ListItem newList = new ListItem("", "0");
                ddlKitted.Items.Insert(0, newList);
            }
            else
            {
                //ViewState["skulist"] = null;
                ddlKitted.DataSource = null;
                ddlKitted.DataBind();
                lblMsg.Text = "No SKUs are assigned to selected Customer";
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ServiceRequestOperations serviceRequestOperations = SV.Framework.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.SOR.ServiceRequestOperations>();

            string errorMessage = string.Empty;
            int userId = 0, createdBy = 0, companyID = 0, qty = 0, itemCompanyGUID = 0, ServiceRequestID = 0, StatusID = 1;
            string sorNumber = string.Empty;
            ServiceOrderRequestModel request = new ServiceOrderRequestModel();
            sorNumber = txtSORNumber.Text.Trim();
            createdBy = Convert.ToInt32(Session["UserID"]);

            if (ViewState["ServiceRequestID"] != null)
            {
                ServiceRequestID = Convert.ToInt32(ViewState["ServiceRequestID"]);
                StatusID = Convert.ToInt32(ddlStatus.SelectedValue);
            }

            if (ViewState["companyID"] == null)
            {
                if(dpCompany.SelectedIndex==0)
                {
                    lblMsg.Text = "Please select Customer!";
                    return;
                }
                int.TryParse(ddlKitted.SelectedValue, out companyID);
            }
            else
            {
                companyID = Convert.ToInt32(ViewState["companyID"]);
            }

            int.TryParse(txtOrderQty.Text.Trim(), out qty);
            if (!string.IsNullOrWhiteSpace(sorNumber))
            {
                if (ddlKitted.SelectedIndex > 0)
                {
                    if (qty > 0)
                    {
                        int.TryParse(ddlKitted.SelectedValue, out itemCompanyGUID);

                        if (ddlUser.SelectedIndex > 0)
                        {
                            int.TryParse(ddlUser.SelectedValue, out userId);
                            request.UserID = createdBy;
                            request.Comments = txtComments.Text.Trim();
                            request.ItemCompanyGUID = itemCompanyGUID;
                            request.Quantity = qty;
                            request.RequestedBy = userId;
                            request.ServiceRequestID = ServiceRequestID;
                            request.SORNumber = sorNumber;
                            request.StatusID = StatusID;

                            int returnResult = serviceRequestOperations.ServiceOrderRequestInsertUpdate(request, out errorMessage);
                            if (returnResult > 0 && string.IsNullOrWhiteSpace(errorMessage))
                            {
                                //if (ServiceRequestID == 0)
                                lblMsg.Text = "Submitted successfully";
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
                            lblMsg.Text = "Please select requested by!";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Quantity is required!";

                    }
                }
                else
                {
                    lblMsg.Text = "Please select kitted SKU!";

                }
            }
            else
            {
                lblMsg.Text = "Service order request number is required!";
            }

        }
        private void Clear()
        {
            txtOrderQty.Text = string.Empty;
            txtSORNumber.Text = string.Empty;
            txtComments.Text = string.Empty;
            if (ddlKitted != null && ddlKitted.Items.Count > 0)
                ddlKitted.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (ViewState["ServiceRequestID"] != null)
            {
                Response.Redirect("~/sorsearch.aspx?search=1");
            }
            else
            {
                lblMsg.Text = string.Empty;
                Clear();
            }
        }

        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;

            lblMsg.Text = string.Empty;
            //btnSubmit.Visible = false;
            Clear();
            
            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            if (companyID > 0)
            {
                BindCompanyKittedSKU(companyID);
                BindUsers(companyID);
            }
            else
            {
                ddlKitted.DataSource = null;
                ddlKitted.DataBind();
                ddlUser.DataSource = null;
                ddlUser.DataBind();
            }
        }
    }
}