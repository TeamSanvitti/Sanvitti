using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using System.Configuration;
using System.Reflection;
using System.IO;
using System.Xml.Linq;
using SV.Framework.Models.SOR;
using SV.Framework.Models.Common;
using SV.Framework.SOR;

namespace avii
{
    public partial class ServiceOrderSearch : System.Web.UI.Page
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
                if (Session["adm"] == null)
                {
                    trCustomer.Visible = false;
                }
                else
                    BindCustomer();

                if (Request["search"] != null)
                {
                    if (Session["sosearch"] != null)
                    {
                        string search = Session["sosearch"] as string;
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
                        txtSONumber.Text = serviceOrderNumber;
                        txtCustOrderNo.Text = customerOrderNumber;
                        txtDateFrom.Text = dateFrom;
                        txtDateTo.Text = dateTo;
                        txtKittedSKU.Text = SKU;
                        txtESN.Text = ESN;
                        BindSO();

                    }
                }
            }
        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 1);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindSO();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }
        private void BindSO()
        {
            SV.Framework.SOR.ServiceOrderOperation ServiceOrderOperation = SV.Framework.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.SOR.ServiceOrderOperation>();

            lnkSummary.Visible = false;
            lnkSumary.Visible = false;
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            string sortExpression = "OrderDate";
            string sortDirection = "ASC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;

            ServiceOrders serviceOrder = new ServiceOrders();
            List<ServiceOrders> soList = new List<ServiceOrders>();
            string customerOrderNumber = string.Empty, serviceOrderNumber = string.Empty, SKU = string.Empty, ESN = string.Empty;
            string dateFrom = string.Empty, dateTo = string.Empty;
            int companyID = 0;
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
            dateFrom = txtDateFrom.Text.Trim();
            dateTo = txtDateTo.Text.Trim();

            customerOrderNumber = txtCustOrderNo.Text.Trim();
            serviceOrderNumber = txtSONumber.Text.Trim();
            ESN = txtESN.Text.Trim();
            SKU = txtKittedSKU.Text.Trim();
            if (Session["adm"] != null)
            {
                if (companyID == 0 && string.IsNullOrEmpty(serviceOrderNumber) && string.IsNullOrEmpty(customerOrderNumber) && string.IsNullOrEmpty(dateFrom) && string.IsNullOrEmpty(dateTo) && string.IsNullOrEmpty(SKU) && string.IsNullOrEmpty(ESN))
                {
                    lblMsg.Text = "Please select the search criteria";
                    return;
                }
            }
            else
            {
                if ( string.IsNullOrEmpty(serviceOrderNumber) && string.IsNullOrEmpty(customerOrderNumber) && string.IsNullOrEmpty(dateFrom) && string.IsNullOrEmpty(dateTo) && string.IsNullOrEmpty(SKU) && string.IsNullOrEmpty(ESN))
                {
                    lblMsg.Text = "Please select the search criteria";
                    return;
                }
            }

            Session["sosearch"] = serviceOrderNumber + "~" + customerOrderNumber + "~" + dateFrom + "~" + dateTo + "~" + SKU + "~" + ESN + "~"+ companyID.ToString();
            serviceOrder.CustomerOrderNumber = customerOrderNumber;
            serviceOrder.ServiceOrderNumber = serviceOrderNumber;
            serviceOrder.SKU = SKU;
            serviceOrder.ESN = ESN;
            serviceOrder.DateFrom = dateFrom;
            serviceOrder.DateTo = dateTo;
            serviceOrder.CompanyId = companyID;
            soList = ServiceOrderOperation.GetServiceOrders(serviceOrder);

            if (soList != null && soList.Count > 0)
            {
                int totalQty = soList.Sum(x => x.Quantity);
                gvSO.DataSource = soList;
                gvSO.DataBind();
                Session["so"] = soList;
                lnkSummary.Visible = true;
                lblCount.Text = "<strong>Total Service Orders:</strong> " + soList.Count + "&nbsp; &nbsp; &nbsp; <strong>Total Quantity:</strong> " + totalQty.ToString();
            }
            else
            {
                gvSO.DataSource = null;
                gvSO.DataBind();
                lblMsg.Text = "No record exists.";
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clearform();
        }
        protected void gvSO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSO.PageIndex = e.NewPageIndex;
            if (Session["so"] != null)
            {
                List<ServiceOrders> soList = (List<ServiceOrders>)Session["so"];

                gvSO.DataSource = soList;
                gvSO.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }
        private void Clearform()
        {
            lblMsg.Text = string.Empty;
            lnkSumary.Visible = false;
            lblCount.Text = string.Empty;

            txtCustOrderNo.Text = string.Empty;
            txtESN.Text = string.Empty;
            txtKittedSKU.Text = string.Empty;
            txtSONumber.Text = string.Empty;
            gvSO.DataSource = null;
            gvSO.DataBind();
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
        public List<ServiceOrders> Sort<TKey>(List<ServiceOrders> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<ServiceOrders>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<ServiceOrders>();
            }
        }
        protected void gvSO_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["so"] != null)
            {
                List<ServiceOrders> sos = (List<ServiceOrders>)Session["so"];

                if (sos != null && sos.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        sos = Sort<ServiceOrders>(sos, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        sos = Sort<ServiceOrders>(sos, SortExp, SortDirection.Descending);
                    }
                    Session["so"] = sos;
                    gvSO.DataSource = sos;
                    gvSO.DataBind();
                }
            }
        }

        protected void imgAuth_Command(object sender, CommandEventArgs e)
        {
            int ESNHeaderID = 0, ItemCompanyGUID = 0;
            int ServiceOrderId = Convert.ToInt32(e.CommandArgument);
            ViewState["ServiceOrderId"] = ServiceOrderId;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp1", "<script language='javascript'>Refresh();</script>", false);


        }

        protected void btnhdPrintlabel_Click(object sender, EventArgs e)
        {
            SV.Framework.Inventory.EsnAuthorizationOperation esnAuthorizationOperation = SV.Framework.Inventory.EsnAuthorizationOperation.CreateInstance<SV.Framework.Inventory.EsnAuthorizationOperation>();

            int ESNHeaderID = 0, ItemCompanyGUID = 0;
            int ServiceOrderId = 0;

            string SequenceNumber = "", producttype="";

            if (ViewState["ServiceOrderId"] != null)
            {
                ServiceOrderId = Convert.ToInt32(ViewState["ServiceOrderId"]);

                List<SV.Framework.Models.Inventory.ESNAuthorization> ESNs = esnAuthorizationOperation.GetESNAuthorizations(ESNHeaderID, ItemCompanyGUID, ServiceOrderId, out SequenceNumber, out producttype);

                //List<SV.Framework.Inventory.ESNAuthorization> ESNs = Session["ESNs"] as List<SV.Framework.Inventory.ESNAuthorization>;
                var memoryStream = new MemoryStream();
                //   System.Xml.XmlWriter write =  new   ;
                string fileName;
                string filePrefix = "spdish";
                string transDate;


                // string fileName = filePrefix + "_" + transDate + "_" + edfFileInfo.fileSequence.ToString() + ".xml";
                DateTime dt = DateTime.Now;
                string currentDate = dt.ToString("yyyy-MM-dd");

                Int64 fileSequence = dt.Ticks;

                string filePath = Server.MapPath("~/UploadedData/");

                if (ESNs != null && ESNs.Count > 0)
                {

                    transDate = currentDate.Replace("-", "");
                    fileName = filePrefix + "_" + transDate + "_" + SequenceNumber + ".xml";
                    filePath = filePath + fileName;

                    XElement xmlElement = esnAuthorizationOperation.CreateAuthorizationFile(ESNs, SequenceNumber, currentDate, producttype);

                    xmlElement.Save(filePath);

                    // tring strFullPath = Server.MapPath("~/temp.xml");
                    string strContents = null;
                    System.IO.StreamReader objReader = default(System.IO.StreamReader);
                    objReader = new System.IO.StreamReader(filePath);
                    strContents = objReader.ReadToEnd();
                    objReader.Close();

                    string attachment = "attachment; filename=" + fileName;
                    Response.ClearContent();
                    Response.ContentType = "application/xml";
                    Response.AddHeader("content-disposition", attachment);
                    Response.Write(strContents);
                    Response.End();
                }
                else
                {
                    lblMsg.Text = "No data found.";
                }

            }
        }

        protected void lnkSO_Command(object sender, CommandEventArgs e)
        {
            string soInfo = Convert.ToString(e.CommandArgument);
            string[] array = soInfo.Split(',');
            string categoryName = "kitted box";
            if (array.Length > 1)
            {
                Session["soid"] = array[0];
                categoryName = array[1]; 
            }
            if (categoryName.ToLower() == "kitted box")
                Response.Redirect("~/ManageServiceOrderNew.aspx");
            else
                Response.Redirect("~/so/ServiceOrderManage.aspx");

        }
    }
}