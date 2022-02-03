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
    public partial class SorSummary : System.Web.UI.Page
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
                BindSorSummary();
            }
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
        private void BindSorSummary()
        {
            ServiceRequestOperations serviceRequestOperations = ServiceOrderOperation.CreateInstance<ServiceRequestOperations>();

            lblMsg.Text = string.Empty;
            int totalQty = 0, OrderCount = 0, totalOrderCount = 0;
            string SKU = string.Empty, RequestNos = string.Empty, ServiceRequestIDs = string.Empty;
            List<ServiceOrderRequestSummary> soSummary = new List<ServiceOrderRequestSummary>();
            List<ServiceOrderRequestModel> soList = Session["sor"] as List<ServiceOrderRequestModel>;
            //if (soList != null && soList.Count > 0)
            //{
            //    soList = Sort<ServiceOrderRequestModel>(soList, "SKU", SortDirection.Ascending);
            //}
            
            ServiceOrderRequestSummary model = null;
            if (soList != null && soList.Count > 0)
            {
                soList = Sort<ServiceOrderRequestModel>(soList, "SKU", SortDirection.Ascending);
                foreach (ServiceOrderRequestModel item in soList)
                {
                    if (SKU != item.SKU)
                    {
                        model = new ServiceOrderRequestSummary();
                        ServiceRequestIDs = string.Empty;
                        OrderCount = 0;
                        model.OrderCount = OrderCount;
                        soSummary.Add(model);
                    }
                    totalOrderCount = totalOrderCount + 1;

                    totalQty = totalQty + item.Quantity;
                    SKU = item.SKU;
                    if (SKU == item.SKU)
                    {                        
                        model.CategoryName = item.CategoryName;
                        model.ProductName = item.ProductName;
                        model.SKU = item.SKU;
                        model.Quantity = item.Quantity;

                        OrderCount = OrderCount + 1;
                        model.OrderCount = OrderCount;
                        if (string.IsNullOrEmpty(ServiceRequestIDs))
                            ServiceRequestIDs = "<a href='javascript: InitializeRequest("+ item.ServiceRequestID.ToString() + ")' >" + item.SORNumber + "</a>";
                        else
                            ServiceRequestIDs = ServiceRequestIDs + ", " + "<a href='javascript: InitializeRequest(" + item.ServiceRequestID.ToString() + ")' >" + item.SORNumber + "</a>";

                        model.ServiceRequestIDs = ServiceRequestIDs;
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
        public static void SetSession(string requestID)
        {
           // int ServiceRequestID = Convert.ToInt32(ServiceRequestID);
            HttpContext.Current.Session["ServiceRequestID"] = requestID;
            HttpContext.Current.Session["view"] = "y";
        }
    }
}