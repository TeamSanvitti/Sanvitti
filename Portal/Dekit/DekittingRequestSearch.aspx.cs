using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SV.Framework.Models.SOR;
using SV.Framework.Models.Common;
using SV.Framework.SOR;

namespace avii.Dekit
{
    public partial class DekittingRequestSearch : System.Web.UI.Page
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
                BindSORStatus();
                if (Session["adm"] == null)
                {
                    trCustomer.Visible = false;
                }
                else
                    BindCustomer();

                if (Session["dekitsearch"] != null)
                {
                    if (Session["dekitsearch"] != null)
                    {
                        string search = Session["dekitsearch"] as string;
                        Session["dekitsearch"] = null;
                        string[] searchAray = search.Split('~');
                        string customerOrderNumber = string.Empty, serviceOrderNumber = string.Empty, SKU = string.Empty, ESN = string.Empty;
                        string dateFrom = string.Empty, dateTo = string.Empty;
                        serviceOrderNumber = searchAray[0];
                        customerOrderNumber = searchAray[1];
                        dateFrom = searchAray[2];
                        dateTo = searchAray[3];
                        SKU = searchAray[4];
                        ESN = searchAray[5];
                        if (Session["adm"] != null)
                        {
                            if (!string.IsNullOrEmpty(searchAray[6]))
                                dpCompany.SelectedValue = searchAray[6];
                        }
                        txtDekitRequestNo.Text = serviceOrderNumber;
                        txtCustomerRequestNo.Text = customerOrderNumber;
                        txtDateFrom.Text = dateFrom;
                        txtDateTo.Text = dateTo;
                        txtKittedSKU.Text = SKU;
                        ddlStatus.SelectedValue = searchAray[7];
                        txtESN.Text = ESN;
                        BindDekittingRequest();

                    }
                }
            }
        }

        private void ReloadSearch()
        {
            if (Session["dekitsearch"] != null)
            {
                string searchCriteria = Session["dekitsearch"] as string;
                Session["dekitsearch"] = null;
                string[] array = searchCriteria.Split('~');
                if (array != null && array.Length > 0)
                {
                    txtDekitRequestNo.Text = array[0];
                    txtCustomerRequestNo.Text = array[1];
                    txtDateFrom.Text = array[2];
                    txtDateTo.Text = array[3];
                    txtKittedSKU.Text = array[4];
                    txtESN.Text = array[5];
                    if (Session["adm"] != null)
                    {
                        dpCompany.SelectedValue = array[6];
                    }

                }
                //Session["dekitsearch"] = dekitRequestNumber + "~" + customerRequestNumber + "~" + dateFrom + "~" + dateTo + "~" + SKU + "~" + ESN + "~" + companyID.ToString();
            }
        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 1);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        private void BindSORStatus()
        {
            SV.Framework.SOR.DekitOperations dekitOperations = SV.Framework.SOR.DekitOperations.CreateInstance<SV.Framework.SOR.DekitOperations>();

            List<ServiceRequestStatus> statusList = dekitOperations.GetDeKitStatusList();
            ddlStatus.DataSource = statusList;
            ddlStatus.DataValueField = "StatusID";
            ddlStatus.DataTextField = "Status";
            
            ddlStatus.DataBind();
            ListItem li = new ListItem("", "0");
            ddlStatus.Items.Insert(0, li);

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindDekittingRequest();
        }
        private void BindDekittingRequest()
        {
            SV.Framework.SOR.DekitOperations dekitOperations = SV.Framework.SOR.DekitOperations.CreateInstance<SV.Framework.SOR.DekitOperations>();

            Session["dekitList"] = null;
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            string sortExpression = "DeKitDate";
            string sortDirection = "DESC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;

            List<DekittingDetail> dekitList = new List<DekittingDetail>();
            string customerRequestNumber = string.Empty, dekitRequestNumber = string.Empty, SKU = string.Empty, ESN = string.Empty;
            string dateFrom = string.Empty, dateTo = string.Empty;
            int companyID = 0, dekittingStatusID = 0;
            if (Session["adm"] != null)
            {
                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
            }
            else
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    companyID = userInfo.CompanyGUID;
                }
            }
            if(ddlStatus.SelectedIndex > 0)
            {
                dekittingStatusID = Convert.ToInt32(ddlStatus.SelectedValue);
            }
            dateFrom = txtDateFrom.Text.Trim();
            dateTo = txtDateTo.Text.Trim();

            customerRequestNumber = txtCustomerRequestNo.Text.Trim();
            dekitRequestNumber = txtDekitRequestNo.Text.Trim();
            ESN = txtESN.Text.Trim();
            SKU = txtKittedSKU.Text.Trim();
            if (Session["adm"] != null)
            {
                if (companyID == 0 && dekittingStatusID == 0 && string.IsNullOrEmpty(dekitRequestNumber) && string.IsNullOrEmpty(customerRequestNumber) && string.IsNullOrEmpty(dateFrom) && string.IsNullOrEmpty(dateTo) && string.IsNullOrEmpty(SKU))
                {
                    lblMsg.Text = "Please select the search criteria";
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(dekitRequestNumber) && dekittingStatusID == 0 && string.IsNullOrEmpty(customerRequestNumber) && string.IsNullOrEmpty(dateFrom) && string.IsNullOrEmpty(dateTo) && string.IsNullOrEmpty(SKU))
                {
                    lblMsg.Text = "Please select the search criteria";
                    return;
                }
            }

            Session["dekitsearch"] = dekitRequestNumber + "~" + customerRequestNumber + "~" + dateFrom + "~" + dateTo + "~" + SKU  + "~" + ESN + "~" +  companyID.ToString() + "~" + ESN + "~" +  ddlStatus.SelectedValue;
            dekitList = dekitOperations.GetDekittingSearch(dekitRequestNumber, customerRequestNumber, companyID, dateFrom, dateTo, SKU, ESN, dekittingStatusID);

            if (dekitList != null && dekitList.Count > 0)
            {
                //int totalQty = soList.Sum(x => x.Quantity);
                gvDekit.DataSource = dekitList;
                gvDekit.DataBind();
                Session["dekitList"] = dekitList;
               // lnkSummary.Visible = true;
                lblCount.Text = "<strong>Total Request:</strong> " + dekitList.Count;
               // lblCount.Text = "<strong>Total Service Orders:</strong> " + soList.Count + "&nbsp; &nbsp; &nbsp; <strong>Total Quantity:</strong> " + totalQty.ToString();
            }
            else
            {
                gvDekit.DataSource = null;
                gvDekit.DataBind();
                lblMsg.Text = "No record exists.";
            }
        }

        private void Clearform()
        {
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            txtCustomerRequestNo.Text = string.Empty;
            txtESN.Text = string.Empty;
            txtKittedSKU.Text = string.Empty;
            txtDekitRequestNo.Text = string.Empty;
            gvDekit.DataSource = null;
            gvDekit.DataBind();
            Session["dekitList"] = null;
            if (Session["adm"] != null)
            {
                dpCompany.SelectedIndex = 0;
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clearform();
        }

        protected void gvDekit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDekit.PageIndex = e.NewPageIndex;
            if (Session["dekitList"] != null)
            {
                List<DekittingDetail> soList = (List<DekittingDetail>)Session["dekitList"];

                gvDekit.DataSource = soList;
                gvDekit.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }

        protected void gvDekit_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["dekitList"] != null)
            {
                List<DekittingDetail> sos = (List<DekittingDetail>)Session["dekitList"];

                if (sos != null && sos.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        sos = Sort<DekittingDetail>(sos, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        sos = Sort<DekittingDetail>(sos, SortExp, SortDirection.Descending);
                    }
                    Session["dekitList"] = sos;
                    gvDekit.DataSource = sos;
                    gvDekit.DataBind();
                }
            }
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
        public List<DekittingDetail> Sort<TKey>(List<DekittingDetail> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<DekittingDetail>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<DekittingDetail>();
            }
        }

        protected void lnkDekit_Command(object sender, CommandEventArgs e)
        {
            Int64 DeKittingID = Convert.ToInt64(e.CommandArgument);
            Session["dekittingID"] = DeKittingID;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('../DeKit/DeKittingView.aspx')</script>", false);

            //Response.Redirect("~/DeKit/DeKittingView.aspx");

        }

        protected void imgDel_Command(object sender, CommandEventArgs e)
        {
            SV.Framework.SOR.DekitOperations dekitOperations = SV.Framework.SOR.DekitOperations.CreateInstance<SV.Framework.SOR.DekitOperations>();

            Int64 DeKittingID = Convert.ToInt64(e.CommandArgument);
            int returnResult = dekitOperations.DeKittingDelete(DeKittingID);
            if(returnResult > 0)
            {
                BindDekittingRequest();
                lblMsg.Text = "Deleted Successfully";

            }
            else
            {
                lblMsg.Text = "Could not delete!";
            }

        }
    }
}