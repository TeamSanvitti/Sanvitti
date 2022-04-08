//using avii.Classes;
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
namespace avii
{
    public partial class SOSummary : System.Web.UI.Page
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
                BindSOSummary();
            }
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
        private void BindSOSummary()
        {
            lblMsg.Text = string.Empty;
            int totalQty = 0, OrderCount = 0, totalOrderCount = 0;
            string SKU = string.Empty, RequestNos = string.Empty, ServiceOrderIds = string.Empty;
            List<ServiceOrderRequestSummary> soSummary = new List<ServiceOrderRequestSummary>();
            List<ServiceOrders> soList = Session["so"] as List<ServiceOrders>;
            //if (soList != null && soList.Count > 0)
            //{
            //    soList = Sort<ServiceOrderRequestModel>(soList, "SKU", SortDirection.Ascending);
            //}

            ServiceOrderRequestSummary model = null;
            if (soList != null && soList.Count > 0)
            {
                soList = Sort<ServiceOrders>(soList, "SKU", SortDirection.Ascending);
                foreach (ServiceOrders item in soList)
                {
                    if (SKU != item.SKU)
                    {
                        model = new ServiceOrderRequestSummary();
                        ServiceOrderIds = string.Empty;
                        OrderCount = 0;
                        model.OrderCount = OrderCount;
                        soSummary.Add(model);
                    }

                    totalQty = totalQty + item.Quantity;
                    totalOrderCount = totalOrderCount + 1;
                    SKU = item.SKU;
                    if (SKU == item.SKU)
                    {
                        model.CategoryName = item.CategoryName;
                        model.Quantity = item.Quantity;
                        model.ProductName = item.ProductName;
                        model.SKU = item.SKU;
                        OrderCount = OrderCount + 1;
                        model.OrderCount = OrderCount;
                        if (item.CategoryName.ToLower() == "kitted box")
                        {
                            if (string.IsNullOrEmpty(ServiceOrderIds))
                                ServiceOrderIds = "<a href='javascript: InitializeRequest(" + item.ServiceOrderId.ToString() + ")' >" + item.ServiceOrderNumber + "</a>";
                            else
                                ServiceOrderIds = ServiceOrderIds + ", " + "<a href='javascript: InitializeRequest(" + item.ServiceOrderId.ToString() + ")' >" + item.ServiceOrderNumber + "</a>";
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(ServiceOrderIds))
                                ServiceOrderIds = "<a href='javascript: InitializeRequest2(" + item.ServiceOrderId.ToString() + ")' >" + item.ServiceOrderNumber + "</a>";
                            else
                                ServiceOrderIds = ServiceOrderIds + ", " + "<a href='javascript: InitializeRequest2(" + item.ServiceOrderId.ToString() + ")' >" + item.ServiceOrderNumber + "</a>";
                        }
                        model.ServiceRequestIDs = ServiceOrderIds;
                    }
                }
                ViewState["qty"] = totalQty;
                ViewState["totalOrderCount"] = totalOrderCount;
                rptSoR.DataSource = soSummary;
                rptSoR.DataBind();

            }
            else
            {
                lblMsg.Text = "No record found!";
                rptSoR.DataSource = null;
                rptSoR.DataBind();
            }
        }
        protected void rpt_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                int totalQty = 0, totalOrderCount = 0;
                int.TryParse(Convert.ToString(ViewState["qty"]), out totalQty);
                int.TryParse(Convert.ToString(ViewState["totalOrderCount"]), out totalOrderCount);
                Label lblQty = (Label)e.Item.FindControl("lblQty");
                Label lblOrderCount = (Label)e.Item.FindControl("lblOrderCount");
                lblQty.Text = totalQty.ToString();
                lblOrderCount.Text = totalOrderCount.ToString();
            }
        }
        [System.Web.Services.WebMethod]
        public static void SetSession(string soid)
        {
            // int ServiceRequestID = Convert.ToInt32(ServiceRequestID);
            HttpContext.Current.Session["soid"] = soid;
        }

    }
}