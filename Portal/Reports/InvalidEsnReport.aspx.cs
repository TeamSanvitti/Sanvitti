using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace avii.Reports
{
    public partial class InvalidEsnReport : System.Web.UI.Page
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
                //string uploadAdmin = ConfigurationSettings.AppSettings["UploadAdmin"].ToString();
                //UserInfo userInfo = Session["userInfo"] as UserInfo;
                //List<UserRole> userRoles = userInfo.ActiveRoles;
                //if (userRoles != null && userRoles.Count > 0)
                //{
                //    var roles = (from item in userRoles where item.RoleName.Equals(uploadAdmin) select item).ToList();
                //    if (roles != null && roles.Count > 0 && !string.IsNullOrEmpty(roles[0].RoleName))
                //    {
                //        ViewState["adminrole"] = roles[0].RoleName;
                //        //pnlDate.Visible = true;
                //    }
                //    // else
                //    //   pnlDate.Visible = false;

                //}
                if (Session["adm"] == null)
                {
                    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    if (userInfo != null)
                    {
                        BindCompanySKU(userInfo.CompanyGUID);
                    }
                }
                //pnlDate.Visible = false;

                BindCustomer();
                BindCategory();
            }
            }
        private void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        private void BindCompanySKU(int companyID)
        {
            SV.Framework.Inventory.MslOperation mslOperation = SV.Framework.Inventory.MslOperation.CreateInstance<SV.Framework.Inventory.MslOperation>();
            lblMsg.Text = string.Empty;
            List<CompanySKUno> skuList = mslOperation.GetCompanySKUs(companyID, 0);
            if (skuList != null)
            {
                ViewState["skulist"] = skuList;
                ddlSKU.DataSource = skuList;
                ddlSKU.DataValueField = "SKU";
                ddlSKU.DataTextField = "SKU";


                ddlSKU.DataBind();
                ListItem item = new ListItem("", "");
                ddlSKU.Items.Insert(0, item);
            }
            else
            {
                ViewState["skulist"] = null;
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                lblMsg.Text = "No SKU are assigned to selected Customer";
            }


        }

        private void BindCategory()
        {
            SV.Framework.Inventory.ProductController productController = SV.Framework.Inventory.ProductController.CreateInstance<SV.Framework.Inventory.ProductController>();

           // ProductController objProductController = new ProductController();
            List<ItemCategory> categoryList = productController.GetItemCategoryTree(0, 0, 1, true, -1, -1, true, true, false, false);
            ViewState["categoryList"] = categoryList;
            ddlCategory.DataSource = categoryList;
            ddlCategory.DataTextField = "categoryname";
            ddlCategory.DataValueField = "CategoryGUID";
            ddlCategory.DataBind();
            ListItem li = new ListItem("--Select Category--", "0");
            ddlCategory.Items.Insert(0, li);
        }

        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;

            //gvMSL.DataSource = null;
            //gvMSL.DataBind();
            lblMsg.Text = string.Empty;
            string CustInfo;
            if (dpCompany.SelectedIndex > 0)
            {
              //  CustInfo = dpCompany.SelectedValue;
                //string[] arr = CustInfo.Split(',');
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
                ViewState["CompanyID"] = companyID;
            }
            if (companyID > 0)
                BindCompanySKU(companyID);
            else
            {
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
            }
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SV.Framework.Inventory.InvalidEsnOperation invalidEsnOperation = SV.Framework.Inventory.InvalidEsnOperation.CreateInstance<SV.Framework.Inventory.InvalidEsnOperation>();

            lblMsg.Text = "";
            lblCount.Text = "";
            btnDownload.Visible = false;
            int companyID = 0, categoryID = -1, IsAssignFlag = -1;
            string sku = "";
            gvESN.DataSource = null;
            gvESN.DataBind();
            string sortExpression = "SKU";
            string sortDirection = "ASC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;
            Session["InvalidEsnList"] = null;

            
            if (Session["adm"] != null)
            {
                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                else
                {
                    lblMsg.Text = "Customer is required!";
                    return;
                }
            }
            else
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    companyID = userInfo.CompanyGUID;
                }
            }
            if (ddlCategory.SelectedIndex > 0)
            {
                categoryID = Convert.ToInt32(ddlCategory.SelectedValue);
            }
            if (ddlSKU.SelectedIndex > 0)
            {
                sku = ddlSKU.SelectedValue;
            }
            if (chkAssigned.Checked)
                IsAssignFlag = 1;

            List<InvalidEsn> InvalidEsnList = invalidEsnOperation.GetInvalidEsnList(companyID, categoryID, sku, IsAssignFlag);
            if (InvalidEsnList != null && InvalidEsnList.Count > 0)
            {
                gvESN.DataSource = InvalidEsnList;
                gvESN.DataBind();
                Session["InvalidEsnList"] = InvalidEsnList;
                btnDownload.Visible = true;
                lblCount.Text = "<strong>Total record(s):</strong> " + InvalidEsnList.Count;
                //lblCount.Text = InvalidEsnList.Count.ToString();


            }
            else
            {
                lblMsg.Text = "No record found!";
            }


        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (Session["InvalidEsnList"] != null)
            {
                List<InvalidEsn> InvalidEsnList = new List<InvalidEsn>();
                List<InvalidEsnCSV> invalidEsnList = new List<InvalidEsnCSV>();
                //PurchaseOrderShipmentCSV shipment = null;
                InvalidEsnList = Session["InvalidEsnList"] as List<InvalidEsn>;
                InvalidEsnCSV invalidEsnCSV = null;
                if (InvalidEsnList != null && InvalidEsnList.Count > 0)
                {
                    foreach(InvalidEsn invalidEsn in InvalidEsnList)
                    {
                        invalidEsnCSV = new InvalidEsnCSV();
                        invalidEsnCSV.CategoryName = invalidEsn.CategoryName;
                        invalidEsnCSV.ESN = invalidEsn.ESN;
                        invalidEsnCSV.MEID = invalidEsn.MEID;
                        invalidEsnCSV.ProductName = invalidEsn.ProductName;
                        invalidEsnCSV.SKU = invalidEsn.SKU;
                        //invalidEsnCSV.CategoryName = invalidEsn.CategoryName;
                        invalidEsnList.Add(invalidEsnCSV);
                    }

                    string string2CSV = invalidEsnList.ToCSV();

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=InvalidEsnMeids.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(string2CSV);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            lblCount.Text = "";
            gvESN.DataSource = null;
            gvESN.DataBind();
            dpCompany.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            chkAssigned.Checked = false;
            btnDownload.Visible = false;
            ddlSKU.DataSource = null;
            ddlSKU.DataBind();
            //ddlSKU.Items.Clear();

        }

        protected void gvESN_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvESN.PageIndex = e.NewPageIndex;
            if (Session["InvalidEsnList"] != null)
            {
                List<InvalidEsn> InvalidEsnList = (List<InvalidEsn>)Session["InvalidEsnList"];

                gvESN.DataSource = InvalidEsnList;
                gvESN.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }
        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }
        public List<InvalidEsn> Sort<TKey>(List<InvalidEsn> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<InvalidEsn>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<InvalidEsn>();
            }
        }

        protected void gvESN_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["InvalidEsnList"] != null)
            {
                List<InvalidEsn> InvalidEsnList = (List<InvalidEsn>)Session["InvalidEsnList"];
               
                if (InvalidEsnList != null && InvalidEsnList.Count > 0)
                {
                    var list = InvalidEsnList;
                    if (Sortdir == "ASC")
                    {
                        list = Sort<InvalidEsn>(list, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        list = Sort<InvalidEsn>(list, SortExp, SortDirection.Descending);
                    }
                    Session["InvalidEsnList"] = InvalidEsnList;
                    gvESN.DataSource = list;
                    gvESN.DataBind();
                }
            }
        }
    }
}