using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.SOR;
using SV.Framework.Models.Common;
using SV.Framework.SOR;
using System.Configuration;

namespace avii.SO
{
    public partial class ServiceOrderManage : System.Web.UI.Page
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
                SV.Framework.SOR.ServiceOrderOperation serviceOrderOperation = SV.Framework.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.SOR.ServiceOrderOperation>();
                int soid = 0;
                BindCustomer();
                string soRequestInfo = string.Empty;

                if ((Request["soid"] != null && Request["soid"] != "") || Session["soid"] != null)
                {
                    if (Session["soid"] != null)
                    { soid = Convert.ToInt32(Session["soid"]);
                        Session["soid"] = null;
                    }
                    else
                        soid = Convert.ToInt32(Request["soid"]);
                    ViewState["soid"] = soid;
                    GetServiceOrderDetail(soid);
                    btnSubmit.Visible = false;
                    btnCancel.Visible = true;
                    pnlSearch.Visible = true;
                    
                }
                else
                {
                    if (Session["soRequestInfo"] != null)
                    {
                        int serviceOerderNo1 = serviceOrderOperation.GenerateServiceOrder();
                        txtSONumber.Text = serviceOerderNo1.ToString();

                        soRequestInfo = Session["soRequestInfo"] as string;
                        string[] arr = soRequestInfo.Split(',');
                        if (arr.Length > 0)
                        {
                            txtCustOrderNo.Text = arr[0];

                            if (arr.Length > 1)
                                dpCompany.SelectedValue = arr[1];

                            GetServiceRequestNumber();
                            txtCustOrderNo.Enabled = false;
                            dpCompany.Enabled = false;

                            Session["soRequestInfo"] = null;

                        }
                    }
                    else
                    {
                        btnSubmit.Visible = false;
                        //btValidate.Visible = false;
                        btnCancel.Visible = false;
                        pnlSearch.Visible = false;
                       
                        int serviceOerderNo = serviceOrderOperation.GenerateServiceOrder();
                        txtSONumber.Text = serviceOerderNo.ToString();
                        txtOrderDate.Text = DateTime.Now.ToShortDateString();
                        txtSONumber.Enabled = false;
                        txtOrderDate.Enabled = false;
                    }
                }
                ReadOlnyAccess();
            }
        }
        protected void GetServiceOrderDetail(int soid)
        {
            SV.Framework.SOR.ServiceOrderOperation serviceOrderOperation = SV.Framework.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.SOR.ServiceOrderOperation>();

            ViewState["soid"] = soid;
            //bool IsMappedSKU = false;
            ServiceOrders serviceOrder = serviceOrderOperation.GetServiceOrderDetail(soid);
            if (serviceOrder != null)
            {
                Session["serviceOrder"] = serviceOrder;
                dpCompany.Enabled = false;
                ddlKitted.Enabled = false;
                
                dpCompany.SelectedValue = serviceOrder.CompanyId.ToString();
                BindCompanySKU(serviceOrder.CompanyId);
                txtCustOrderNo.Text = serviceOrder.CustomerOrderNumber;
                txtOrderDate.Text = serviceOrder.OrderDate;
                txtOrderQty.Text = serviceOrder.Quantity.ToString();
                txtSONumber.Text = serviceOrder.ServiceOrderNumber;
                ddlKitted.SelectedValue = serviceOrder.KittedSKUId.ToString();

                BindRawSKUs(serviceOrder.CompanyId, serviceOrder.KittedSKUId);
                List<ServiceOrderDetail> esnList = serviceOrder.SODetail;
                var esnStartlist = (from item in esnList where item.Id.Equals(1) select item).ToList();

                int ItemCompanyGUID = 0;
                foreach (RepeaterItem item in rptESN.Items)
                {
                    TextBox txtICCID = item.FindControl("txtICCID") as TextBox;

                    HiddenField hdSKUId = item.FindControl("hdSKUId") as HiddenField;
                    int.TryParse(hdSKUId.Value, out ItemCompanyGUID);
                    if (esnStartlist != null && esnStartlist.Count > 0)
                    {
                        var esnStarts = (from items in esnStartlist where items.ItemCompanyGUID.Equals(ItemCompanyGUID) select items).ToList();
                        if (esnStarts != null && esnStarts.Count > 0 && esnStarts[0].ItemCompanyGUID > 0)
                        {
                            txtICCID.Text = esnStarts[0].ESN;
                        }
                    }
                }
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int userId = 0;
            SV.Framework.SOR.ServiceOrderOperation serviceOrderOperation = SV.Framework.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.SOR.ServiceOrderOperation>();

            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userId = userInfo.UserGUID;
            }
            string errorMessage = string.Empty;
            ServiceOrders serviceOrder = new ServiceOrders();
            List<ServiceOrderDetail> esnList = default;          
            
            serviceOrder.SODetail = esnList;
            serviceOrder.ServiceOrderId = 0;
            serviceOrder.CustomerOrderNumber = txtCustOrderNo.Text.Trim();
            serviceOrder.ServiceOrderNumber = txtSONumber.Text.Trim();
            serviceOrder.OrderDate = txtOrderDate.Text.Trim();
            serviceOrder.Quantity = Convert.ToInt32(txtOrderQty.Text);
            serviceOrder.KittedSKUId = Convert.ToInt32(ddlKitted.SelectedValue);

            int returnResult = serviceOrderOperation.ServiceOrder_NonESN_InsertUpdate(serviceOrder, userId, out errorMessage);
            if (returnResult > 0 && string.IsNullOrEmpty(errorMessage))
            {
                lblMsg.Text = "Submitted successfully";
                
                btnSubmit.Visible = false;
                
            }
            else
            {
                lblMsg.Text = errorMessage;
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (ViewState["soid"] != null)
            {
                Response.Redirect("~/ServiceOrderSearch.aspx?search=so", false);
            }
        }

        protected void ddlKitted_SelectedIndexChanged(object sender, EventArgs e)
        {
            rptESN.DataSource = null;
            rptESN.DataBind();

            int companyID = 0, itemCompanyGUID = 0;
            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
                if (ddlKitted != null && ddlKitted.Items.Count > 0)
                {
                    if (ddlKitted.SelectedIndex > 0)
                    {
                        itemCompanyGUID = Convert.ToInt32(ddlKitted.SelectedValue);
                        BindRawSKUs(companyID, itemCompanyGUID);
                    }
                    else
                    {
                        lblMsg.Text = "SKU is required!";
                        ddlKitted.DataSource = null;
                        ddlKitted.DataBind();
                    }
                }
                else
                {
                    lblMsg.Text = "No SKU/Product are assigned to selected Customer";
                    ddlKitted.Items.Clear();
                    ddlKitted.DataSource = null;
                    ddlKitted.DataBind();
                }
            }
            else
            {
                lblMsg.Text = "Customer is required!";
                dpCompany.DataSource = null;
                dpCompany.DataBind();
            }

        }

        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;
            lblMsg.Text = string.Empty;
            
            btnSubmit.Visible = false;
            ClearForm();
            txtCustOrderNo.Text = string.Empty;
            ddlKitted.Items.Clear();
            string CustInfo = string.Empty;
            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            if (companyID > 0)
                BindCompanySKU(companyID);
            else
            {
                //  trSKU.Visible = true;
                ddlKitted.DataSource = null;
                ddlKitted.DataBind();

            }
        }

        private void GetServiceRequestNumber()
        {
            ServiceRequestOperations serviceRequestOperations = SV.Framework.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.SOR.ServiceRequestOperations>();

            int companyID = 0;
            string sorNumber = string.Empty;
            lblCounts.Text = string.Empty;
            lblMsg.Text = string.Empty;
            ClearForm();

            ServiceRequestDetail serviceRequestDetail = null;
            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
                BindCompanySKU(companyID);
                sorNumber = txtCustOrderNo.Text.Trim();
                if (!string.IsNullOrWhiteSpace(sorNumber))
                {
                    serviceRequestDetail = serviceRequestOperations.ServiceRequestNumberSearch(companyID, sorNumber);

                    Session["RawSKU"] = serviceRequestDetail.RawSKUs;
                    if (serviceRequestDetail != null && !string.IsNullOrWhiteSpace(serviceRequestDetail.Status))
                    {
                        if (serviceRequestDetail.Status == "Received")
                        {
                            ddlKitted.SelectedValue = serviceRequestDetail.ItemcompanyGUID.ToString();
                            txtOrderQty.Text = serviceRequestDetail.Quantity.ToString();
                            ddlKitted.Enabled = false;
                            txtOrderQty.Enabled = false;

                            if (serviceRequestDetail.RawSKUs != null && serviceRequestDetail.RawSKUs.Count > 0)
                            {
                                rptESN.DataSource = serviceRequestDetail.RawSKUs;
                                rptESN.DataBind();
                                foreach (KittedRawSKU item in serviceRequestDetail.RawSKUs)
                                {
                                    if (!string.IsNullOrWhiteSpace(item.StockMsg))
                                    {
                                        lblMsg.Text = lblMsg.Text +  " Raw SKU# " + item.SKU + " - " + item.StockMsg;                                        
                                    }

                                }
                                if (lblMsg.Text == "")
                                {
                                    Session["RawSKUs"] = serviceRequestDetail.RawSKUs;
                                    btnCancel.Visible = true;
                                    btnSubmit.Visible = true;
                                    
                                }
                                pnlSearch.Visible = true;
                            }
                            else
                            {
                                rptESN.DataSource = null;
                                rptESN.DataBind();
                                lblMsg.Text = "No SKU are assigned!";
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Customer order number already " + serviceRequestDetail.Status;
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Customer order number not exists";
                    }
                }
                else
                {
                    lblMsg.Text = "Customer order number is required!";
                }
            }
            else
            {
                lblMsg.Text = "Customer is required!";
            }

        }

        private void BindRawSKUs(int companyID, int itemCompanyGUID)
        {
            SV.Framework.Catalog.FinishSKUOperations FinishSKUOperations = SV.Framework.Catalog.FinishSKUOperations.CreateInstance<SV.Framework.Catalog.FinishSKUOperations>();

            lblMsg.Text = string.Empty;

            //companyID = itemCompanyGUID = 0;
            if (companyID > 0)
            {
                if (itemCompanyGUID > 0)
                {
                    //companyID = Convert.ToInt32(dpCompany.SelectedValue);
                    //if (ddlKitted != null && ddlKitted.Items.Count > 0)
                    {
                        //if (ddlKitted.SelectedIndex > 0)
                        //   itemCompanyGUID = Convert.ToInt32(ddlKitted.SelectedValue);

                        List<SV.Framework.Models.Catalog.RawSKU> rawSKUList = FinishSKUOperations.GetKittedAssignedRawSKUs(companyID, itemCompanyGUID);
                        if (rawSKUList != null && rawSKUList.Count > 0)
                        {
                            rptESN.DataSource = rawSKUList;
                            //ddlSKU.DataValueField = "ItemcompanyGUID";
                            //ddlSKU.DataTextField = "SKU";
                            rptESN.DataBind();
                            // ListItem newList = new ListItem("", "");
                            // ddlSKU.Items.Insert(0, newList);
                            Session["RawSKUs"] = rawSKUList;
                            
                        }
                        else
                        {
                            rptESN.DataSource = null;
                            rptESN.DataBind();
                            lblMsg.Text = "No SKU are assigned!";
                        }
                    }                   

                }
                else
                {
                    lblMsg.Text = "SKU is required!";
                    ddlKitted.DataSource = null;
                    ddlKitted.DataBind();
                }
            }
            else
            {
                lblMsg.Text = "Customer is required!";
                dpCompany.DataSource = null;
                dpCompany.DataBind();
            }
        }

        private void BindCompanySKU(int companyID)
        {
            SV.Framework.Catalog.FinishSKUOperations FinishSKUOperations = SV.Framework.Catalog.FinishSKUOperations.CreateInstance<SV.Framework.Catalog.FinishSKUOperations>();

            lblMsg.Text = string.Empty;
            List<SV.Framework.Models.Catalog.CompanySKUno> skuList = FinishSKUOperations.GetCompanyFinalOrRawSKUList(companyID, true);
            if (skuList != null && skuList.Count > 0)
            {
                ddlKitted.DataSource = skuList;
                ddlKitted.DataValueField = "ItemcompanyGUID";
                ddlKitted.DataTextField = "SKU";

                ddlKitted.DataBind();
                ListItem newList = new ListItem("", "");
                ddlKitted.Items.Insert(0, newList);
            }
            if (skuList != null)
            {
                ViewState["skulist"] = skuList;
                ddlKitted.DataSource = skuList;
                ddlKitted.DataValueField = "ItemcompanyGUID";
                ddlKitted.DataTextField = "SKU";


                ddlKitted.DataBind();
                ListItem item = new ListItem("", "0");
                ddlKitted.Items.Insert(0, item);
            }
            else
            {
                ViewState["skulist"] = null;
                ddlKitted.DataSource = null;
                ddlKitted.DataBind();
                lblMsg.Text = "No SKU are assigned to selected Customer";

            }


        }
        private void ClearForm()
        {
            lblCounts.Text = string.Empty;
            //lblCount.Text = string.Empty;

            btnSubmit.Visible = false;
            //btnPrint.Visible = false;
            //btnValidate.Visible = false;
            //btValidate.Visible = false;
            btnCancel.Visible = false;
            pnlSearch.Visible = false;
            //btnAdd.Visible = false;
            //BindCustomer();
            //int serviceOerderNo = ServiceOrderOperation.GenerateServiceOrder();
            //txtSONumber.Text = serviceOerderNo.ToString();
            txtOrderDate.Text = DateTime.Now.ToShortDateString();
            txtSONumber.Enabled = false;
            txtOrderDate.Enabled = false;
            txtOrderQty.Text = string.Empty;
            //txtCustOrderNo.Text = string.Empty;
            txtCustOrderNo.Enabled = true;
            txtOrderQty.Enabled = true;

            rptESN.DataSource = null;
            rptESN.DataBind();
        }
    }
}