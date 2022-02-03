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
    public partial class ServiceOrderSummary : System.Web.UI.Page
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
                //if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }

            if (!IsPostBack)
            {
                SV.Framework.SOR.ServiceOrderOperation ServiceOrderOperation = SV.Framework.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.SOR.ServiceOrderOperation>();

                lblCount.Text = string.Empty;
                if (Session["sosearch"] != null)
                {
                    ServiceOrders serviceOrder = new ServiceOrders();
                    List<SOSKUSummary> skuList = new List<SOSKUSummary>();
                    int companyID = 0;

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
                    int.TryParse(searchAray[6], out companyID);
                    
                    serviceOrder.CustomerOrderNumber = customerOrderNumber;
                    serviceOrder.ServiceOrderNumber = serviceOrderNumber;
                    serviceOrder.SKU = SKU;
                    serviceOrder.ESN = ESN;
                    serviceOrder.DateFrom = dateFrom;
                    serviceOrder.DateTo = dateTo;
                    serviceOrder.CompanyId = companyID;
                    string sortExpression = "SKU";
                    string sortDirection = "ASC";
                    ViewState["SortDirection"] = sortDirection;
                    ViewState["SortExpression"] = sortExpression;

                    skuList = ServiceOrderOperation.GetServiceOrderSummary(serviceOrder);

                    if (skuList != null && skuList.Count > 0)
                    {
                        gvSO.DataSource = skuList;
                        gvSO.DataBind();
                        Session["skuList"] = skuList;
                        
                        lblCount.Text = "<strong>Total SKUs:</strong> " + skuList.Count;
                    }
                    else
                    {
                        gvSO.DataSource = null;
                        gvSO.DataBind();
                        lblMsg.Text = "No record exists.";
                    }
                }

            }

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (Session["skuList"] != null)
            {
                List<SOSKUSummary> skuList = new List<SOSKUSummary>();
                //PurchaseOrderShipmentCSV shipment = null;
                skuList = Session["skuList"] as List<SOSKUSummary>;
                if (skuList != null && skuList.Count > 0)
                {

                    string string2CSV = skuList.ToCSV();

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=kittedskureport.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(string2CSV);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void gvSO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSO.PageIndex = e.NewPageIndex;
            if (Session["skuList"] != null)
            {
                List<SOSKUSummary> skuList = (List<SOSKUSummary>)Session["skuList"];

                gvSO.DataSource = skuList;
                gvSO.DataBind();
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
        public List<SOSKUSummary> Sort<TKey>(List<SOSKUSummary> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<SOSKUSummary>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<SOSKUSummary>();
            }
        }
        protected void gvSO_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["skuList"] != null)
            {
                List<SOSKUSummary> skuList = (List<SOSKUSummary>)Session["skuList"];

                if (skuList != null && skuList.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        skuList = Sort<SOSKUSummary>(skuList, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        skuList = Sort<SOSKUSummary>(skuList, SortExp, SortDirection.Descending);
                    }
                    Session["skuList"] = skuList;
                    gvSO.DataSource = skuList;
                    gvSO.DataBind();
                }
            }
        }

    }
}