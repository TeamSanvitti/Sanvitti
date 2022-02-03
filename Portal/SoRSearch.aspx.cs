using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using SV.Framework.Models.SOR;
using SV.Framework.Models.Common;
using SV.Framework.SOR;

namespace avii
{
    public partial class SoRSearch : System.Web.UI.Page
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
                int companyID = 0;
                BindSORStatus();
                if (Session["ServiceRequestID"] != null)
                {
                    ViewState["ServiceRequestID"] = Convert.ToInt32(Session["ServiceRequestID"]);
                    Session["ServiceRequestID"] = null;
                }
                if (Session["adm"] != null)
                {
                    BindCustomer();
                //    hdnCustomer.Value = "1";

                    //companyID = 464;
                }
                else
                {
                 //   hdnCustomer.Value = "";

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

                if (Request["search"] != null)
                {
                    if (Session["sorsearch"] != null)
                    {
                        string search = Session["sorsearch"] as string;
                        //Session["sorsearch"] = serviceOrderRequest + "~" + requestedBy.ToString() + "~" + dateFrom + "~" + dateTo + "~" + SKU + "~" + StatusID.ToString() + "~" + companyID.ToString();

                        string[] searchAray = search.Split('~');
                        string serviceOrderRequest = string.Empty, requestedBy = string.Empty, SKU = string.Empty, StatusID = string.Empty, CompanyID = string.Empty;
                        string dateFrom = string.Empty, dateTo = string.Empty;
                        serviceOrderRequest = searchAray[0];
                        requestedBy = searchAray[1];
                        dateFrom = searchAray[2];
                        dateTo = searchAray[3];
                        SKU = searchAray[4];
                        StatusID = searchAray[5];
                        
                        if (Session["adm"] != null)
                        {
                            int.TryParse(searchAray[6], out companyID);
                            BindCompanyKittedSKU(companyID);
                            BindUsers(companyID);

                            if (!string.IsNullOrEmpty(searchAray[6]))
                                dpCompany.SelectedValue = searchAray[6];
                            if (!string.IsNullOrEmpty(searchAray[5]))
                                ddlStatus.SelectedValue = StatusID;
                        }

                        txtSORNumber.Text = serviceOrderRequest;
                        ddlUser.SelectedValue = requestedBy;
                        txtDateFrom.Text = dateFrom;
                        txtDateTo.Text = dateTo;
                        ddlKitted.SelectedItem.Text = SKU;
                        
                        BindSO();

                    }
                }


            }
        }
        private void BindSORStatus()
        {
            ServiceRequestOperations serviceRequestOperations = ServiceOrderOperation.CreateInstance<ServiceRequestOperations>();

            List<ServiceRequestStatus> statusList = serviceRequestOperations.GetSoRStatusList();
            ddlStatus.DataSource = statusList;
            ddlStatus.DataValueField = "StatusID";
            ddlStatus.DataTextField = "Status";

            ddlStatus.DataBind();

            ListItem newList = new ListItem("", "0");
            ddlStatus.Items.Insert(0, newList);

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
       
            BindSO();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }
        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;

            lblMsg.Text = string.Empty;
            //btnSubmit.Visible = false;
            Clearform();

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
                ddlKitted.DataSource = null;
                ddlKitted.DataBind();
            }
        }
        private void BindSO()
        {
            ServiceRequestOperations serviceRequestOperations = ServiceOrderOperation.CreateInstance<ServiceRequestOperations>();

            btnSummary.Visible = false;
            btnSoRSummary.Visible = false;
            btnDownload.Visible = false;
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            string sortExpression = "CreateDate";
            string sortDirection = "DESC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;
            //ServiceOrderRequestModel request = new ServiceOrderRequestModel();
            ServiceOrderRequestModel serviceOrder = new ServiceOrderRequestModel();
            List<ServiceOrderRequestModel> soList = new List<ServiceOrderRequestModel>();
            string customerOrderNumber = string.Empty, serviceOrderRequest = string.Empty, SKU = string.Empty, ESN = string.Empty;
            string dateFrom = string.Empty, dateTo = string.Empty;
            int companyID = 0, requestedBy = 0, StatusID = 0;
            if (Session["adm"] != null)
            {
                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                else
                {
                    lblMsg.Text = "Please select customer!";
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

            dateFrom = txtDateFrom.Text.Trim();
            dateTo = txtDateTo.Text.Trim();
            if (ddlUser.SelectedIndex > 0)
                requestedBy = Convert.ToInt32(ddlUser.SelectedValue);

            StatusID = Convert.ToInt32(ddlStatus.SelectedValue);

           // customerOrderNumber = txtCustOrderNo.Text.Trim();
            serviceOrderRequest = txtSORNumber.Text.Trim();
            //  ESN = txtESN.Text.Trim();
            if (ddlKitted.SelectedIndex > 0)
                SKU = ddlKitted.SelectedItem.Text;
            if (Session["adm"] != null)
            {
                if (companyID == 0 && requestedBy == 0 && StatusID == 0 && string.IsNullOrEmpty(serviceOrderRequest) && string.IsNullOrEmpty(dateFrom) && string.IsNullOrEmpty(dateTo) && string.IsNullOrEmpty(SKU) )
                {
                    lblMsg.Text = "Please select the search criteria";
                    return;
                }
            }
            else
            {
                if (requestedBy == 0 && StatusID == 0 && string.IsNullOrEmpty(serviceOrderRequest) && string.IsNullOrEmpty(dateFrom) && string.IsNullOrEmpty(dateTo) && string.IsNullOrEmpty(SKU))
                {
                    lblMsg.Text = "Please select the search criteria";
                    return;
                }
            }

            Session["sorsearch"] = serviceOrderRequest + "~" + requestedBy.ToString() + "~" + dateFrom + "~" + dateTo + "~" + SKU + "~" + StatusID.ToString() + "~" + companyID.ToString();
            //serviceOrder.CustomerOrderNumber = customerOrderNumber;
            serviceOrder.SORNumber = serviceOrderRequest;
            serviceOrder.SKU = SKU;
            serviceOrder.RequestedBy = requestedBy;
            serviceOrder.FromDate = dateFrom;
            serviceOrder.ToDate = dateTo;
            serviceOrder.CompanyID = companyID;
            serviceOrder.StatusID = StatusID;

            soList = serviceRequestOperations.GetServiceOrderRequests(serviceOrder);

            if (soList != null && soList.Count > 0)
            {
                int totalQty = soList.Sum(x => x.Quantity);

                // aw.Visible = true;
                btnSoRSummary.Visible = true;
                btnDownload.Visible = true;
                gvSO.DataSource = soList;
                gvSO.DataBind();
                Session["sor"] = soList;
                //lnkSumary.Visible = true;
                // lblCount.Text = "<strong>Total count:</strong> " + soList.Count;
                lblCount.Text = "<strong>Total Requests:</strong> " + soList.Count + "&nbsp; &nbsp; &nbsp; <strong>Total Quantity:</strong> " + totalQty.ToString();
            }
            else
            {
                gvSO.DataSource = null;
                gvSO.DataBind();
                lblMsg.Text = "No record exists.";
            }
        }
        private void SORDownloadCSV()
        {
            //ServiceRequestOperations serviceRequestOperations = ServiceOrderOperation.CreateInstance<ServiceRequestOperations>();

            List<ServiceOrderRequestModel> soList = Session["sor"] as List<ServiceOrderRequestModel>;
            List<ServiceRequestDownload> sorList = null;
            ServiceRequestDownload model = null;
            if(soList != null && soList.Count > 0)
            {
                sorList = new List<ServiceRequestDownload>();
                foreach(ServiceOrderRequestModel item in soList)
                {
                    model = new ServiceRequestDownload();
                    model.CategoryName = item.CategoryName;
                    model.CreatedBy = item.CreatedUserBy;
                    model.Date = item.SORDate;
                    model.ProductName = item.ProductName;
                    model.Quantity = item.Quantity;
                    model.RequestedBy = item.RequestedUserBy;
                    model.ServiceRequestNumber = item.SORNumber;
                    model.SKU = item.SKU;
                    model.Status = item.Status;
                    sorList.Add(model);
                }
                if (sorList != null && sorList.Count > 0)
                {
                    string string2CSV = sorList.ToCSV();

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=ServiceOrderRequest.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(string2CSV);
                    Response.Flush();
                    Response.End();
                }

            }
            //ServiceRequestDownload
        }
        private void SORSummaryDownloadCSV()
        {
            List<ServiceRequestSummaryDownload> sorList = Session["sorsummary"] as List<ServiceRequestSummaryDownload>;

            if (sorList != null && sorList.Count > 0)
            {
                string string2CSV = sorList.ToCSV();

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=SoRSummary.csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(string2CSV);
                Response.Flush();
                Response.End();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clearform();
            if (Session["adm"] != null)
                dpCompany.SelectedIndex = 0;

        }
        protected void gvSO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSO.PageIndex = e.NewPageIndex;
            if (Session["sor"] != null)
            {
                List<ServiceOrderRequestModel> soList = (List<ServiceOrderRequestModel>)Session["sor"];

                gvSO.DataSource = soList;
                gvSO.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }
        private void Clearform()
        {
            btnSummary.Visible = false;
            btnDownload.Visible = false;

            lblMsg.Text = string.Empty;
            // lnkSumary.Visible = false;
            lblCount.Text = string.Empty;

            //txtCustOrderNo.Text = string.Empty;
            //txtESN.Text = string.Empty;
            //txtKittedSKU.Text = string.Empty;
            txtSORNumber.Text = string.Empty;
            gvSO.DataSource = null;
            gvSO.DataBind();
            txtDateFrom.Text = string.Empty;
            txtDateTo.Text = string.Empty;
            if (Session["adm"] != null)
            {
                
                ddlKitted.Items.Clear();
                ddlUser.Items.Clear();
                ddlStatus.SelectedIndex = 0;

            }
            else
            {
                ddlKitted.SelectedIndex = 0;
                ddlUser.SelectedIndex = 0;
                ddlStatus.SelectedIndex = 0;
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
        public List<ServiceOrderRequestModel> Sort<TKey>(List<ServiceOrderRequestModel> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<ServiceOrderRequestModel>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<ServiceOrderRequestModel>();
            }
        }
        protected void gvSO_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["sor"] != null)
            {
                List<ServiceOrderRequestModel> sos = (List<ServiceOrderRequestModel>)Session["sor"];

                if (sos != null && sos.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        sos = Sort<ServiceOrderRequestModel>(sos, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        sos = Sort<ServiceOrderRequestModel>(sos, SortExp, SortDirection.Descending);
                    }
                    Session["sor"] = sos;
                    gvSO.DataSource = sos;
                    gvSO.DataBind();
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

        private void BindUsers(int companyID)
        {
            avii.Classes.UserUtility objUser = new avii.Classes.UserUtility();
            List<avii.Classes.clsUserManagement> userList = objUser.getUserList("", companyID, "", -1, -1, -1, true);
            if (userList != null && userList.Count > 0)
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


        protected void lnkSOID_Command(object sender, CommandEventArgs e)
        {
            int sorID = Convert.ToInt32(e.CommandArgument);

            if(sorID > 0)
            {
                Session["ServiceRequestID"] = sorID;
                Response.Redirect("~/ServiceOrderRequest.aspx");
            }
        }

        protected void imgDel_Command(object sender, CommandEventArgs e)
        {
            ServiceRequestOperations serviceRequestOperations = ServiceOrderOperation.CreateInstance<ServiceRequestOperations>();

            int sorID = Convert.ToInt32(e.CommandArgument);
            int userID = Convert.ToInt32(Session["UserID"]);
            if (sorID > 0)
            {
                int returnResult = serviceRequestOperations.ServiceOrderRequestDelete(sorID, userID);
                if (returnResult > 0)
                {
                    BindSO();
                    lblMsg.Text = "Deleted successfully";
                }
            }
        }

        protected void lnkSO_Command(object sender, CommandEventArgs e)
        {
            string soRequestInfo = Convert.ToString(e.CommandArgument);
            Session["soRequestInfo"] = soRequestInfo;
            string[] array = soRequestInfo.Split(',');
            string categoryName = "kitted box";
            if (array.Length > 2)
                categoryName = array[2];
            if (categoryName.ToLower() == "kitted box")
                Response.Redirect("~/ManageServiceOrderNew.aspx");
            else
                Response.Redirect("~/so/ServiceOrderManage.aspx");

            //Response.Redirect("~/ManageServiceOrderNew.aspx");
            //string[] arr = soRequestInfo.Split(',');
            //if(arr.Length > 0)
            //{
            //}

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                ////Add delete confirmation message for Customer
                if (e.Row.RowIndex >= 0)
                {
                   
                    ImageButton imgLOG = (ImageButton)e.Row.FindControl("imgLOG");
                    imgLOG.OnClientClick = "openSORDialogAndBlock('Service Order Request Log', '" + imgLOG.ClientID + "')";
                }
            }
        }
        protected void imgLOG_Command(object sender, CommandEventArgs e)
        {
            int serviceRequestID = Convert.ToInt32(e.CommandArgument);
            lblSOR.Text = string.Empty;
            ServiceRequestOperations serviceRequestOperations = ServiceOrderOperation.CreateInstance<ServiceRequestOperations>();

            List<ServiceOrderRequestModel> soLogs = serviceRequestOperations.GetServiceOrderRequestLog(serviceRequestID);
            if(soLogs != null && soLogs.Count > 0)
            {
                lblSORNumber.Text = soLogs[0].SORNumber;
                lblSKU.Text = soLogs[0].SKU;

                rptSORLog.DataSource = soLogs;
                rptSORLog.DataBind();

            }
            else
            {
                lblSOR.Text = "No records found";
            }
            RegisterStartupScript("jsUnblockDialog", "unblockSORDialog();");
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        protected void btnSummary_Click(object sender, EventArgs e)
        {
            int itr = 1;
            List<ServiceOrderRequestModel> soList = Session["sor"] as List<ServiceOrderRequestModel>;
            List<ServiceRequestSummaryDownload> sorsummary = new List<ServiceRequestSummaryDownload>();
            ServiceRequestSummaryDownload model = null;
            var summary = soList.GroupBy(k => k.SKU).Select(g => new { Key = g.Key, SKU = g.First().SKU, Quantity = g.Sum(s => s.Quantity) }).ToList();
            if(summary != null && summary.Count > 0)
            {
                foreach(var item in summary)
                {
                    model = new ServiceRequestSummaryDownload();
                    model.SKU = item.SKU;
                    model.Quantity = item.Quantity;
                    sorsummary.Add(model);
                }
                Session["sorsummary"] = sorsummary;
                rptSummary.DataSource = sorsummary;
                rptSummary.DataBind();
                foreach (var item in soList)
                {
                    if (itr == 1)
                        lblToDate.Text = item.SORDate;

                    lblFromDate.Text = item.SORDate;
                    itr = itr + 1;
                }


            }



                // var res2 = soList.GroupBy(g => new { g.Quantity, g.SKU});


                RegisterStartupScript("jsUnblockDialog", "unblockSummaryDialog();");
        }

        protected void btnSummaryDownload_Click(object sender, EventArgs e)
        {
            SORSummaryDownloadCSV();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            SORDownloadCSV();
        }

        protected void btnSoRSummary_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SoRSummary.aspx");
        }
    }
}