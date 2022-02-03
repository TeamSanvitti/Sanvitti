using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using SV.Framework.Models.SOR;
using SV.Framework.Models.Common;
using SV.Framework.SOR;

namespace avii.Controls
{
    public partial class SORWidget : System.Web.UI.UserControl
    {
        private int companyID = 0;

        public int CompanyID
        {
            get
            {
                return companyID;
            }
            set
            {
                companyID = value;
            }
        }
        private string sku = "";
        private string status = "";
        public string SKU
        {
            get
            {
                return sku;
            }
            set
            {
                sku = value;
            }
        }
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                PopulateData();
            }
            else
            {
                ReloadData();
            }
        }

        public void PopulateData()
        {
            ServiceRequestOperations serviceRequestOperations = ServiceOrderOperation.CreateInstance<ServiceRequestOperations>();

            string sortExpression = "SKU";
            string sortDirection = "ASC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;
            try
            {
                if (Session["adm"] == null)
                {
                    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    if (userInfo != null)
                    {
                        CompanyID = userInfo.CompanyGUID;
                    }
                }
                List<ServiceOrderRequestModel> sorList = serviceRequestOperations.GetServiceOrderRequestWidget(CompanyID, SKU, Status);

                Session["sorlist"] = sorList;
                if (sorList != null && sorList.Count > 0)
                {
                    gvSor.DataSource = sorList;
                    lblSoR.Text = string.Empty;
                }
                else
                {
                    gvSor.DataSource = null;
                    lblSoR.Text = "No records found";
                }
                gvSor.DataBind();
            }
            catch(Exception ex)
            {
                lblSoR.Text = ex.Message;
            }
        }
        public void ReloadData()
        {
            ServiceRequestOperations serviceRequestOperations = ServiceOrderOperation.CreateInstance<ServiceRequestOperations>();

            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    CompanyID = userInfo.CompanyGUID;
                }
            }
            List<ServiceOrderRequestModel> sorList = default;
            if (Session["sorlist"] == null)
                sorList = serviceRequestOperations.GetServiceOrderRequestWidget(CompanyID, SKU, Status);
            else
                sorList = (List<ServiceOrderRequestModel>)Session["sorlist"];

            Session["sorlist"] = sorList;
            if (sorList != null && sorList.Count > 0)
            {
                gvSor.DataSource = sorList;
                lblSoR.Text = string.Empty;
            }
            else
            {
                // lblSKUCount.Text = string.Empty;
                gvSor.DataSource = null;
                Session["sorlist"] = null;
                lblSoR.Text = "No records found";
            }
            gvSor.DataBind();
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSor.PageIndex = e.NewPageIndex;
            ReloadData();

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
        protected void gvSor_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["sorlist"] != null)
            {
                List<ServiceOrderRequestModel> sorlist = (List<ServiceOrderRequestModel>)Session["sorlist"];

                if (sorlist != null && sorlist.Count > 0)
                {
                    var list = sorlist;
                    if (Sortdir == "ASC")
                    {
                        list = Sort<ServiceOrderRequestModel>(list, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        list = Sort<ServiceOrderRequestModel>(list, SortExp, SortDirection.Descending);
                    }
                    Session["sorlist"] = list;
                    gvSor.DataSource = list;
                    gvSor.DataBind();
                }
            }
        }
    }
}