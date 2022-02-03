using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Controls
{
    public partial class CustomerRmaStatus : System.Web.UI.UserControl
    {
        private int timeInterval = 30;
        private int companyID = 0;
        public int TimeInterval
        {
            get
            {
                return timeInterval;
            }
            set
            {
                timeInterval = value;
            }
        }
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
        public void LoadData()
        {
            PopulateData();
        }

        public void PopulateData()
        {
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    CompanyID = userInfo.CompanyGUID;

                }

            }
            if (TimeInterval == 0)
            {
                lblCount.Text = string.Empty;
                gvRMA.DataSource = null;
                lblRMA.Text = string.Empty;
                Session["rmastatus"] = null;
            }
            else
            {
                List<CompanyRmaStatuses> rmaStatusList = ReportOperations.GetCustomerRmaStatusReport(CompanyID, TimeInterval, 0);

                Session["rmastatus"] = rmaStatusList;
                if (rmaStatusList != null && rmaStatusList.Count > 0)
                {
                    gvRMA.DataSource = rmaStatusList;
                    lblCount.Text = "<strong>Total Customers:</strong> " + Convert.ToString(rmaStatusList.Count - 1);
                    lblRMA.Text = string.Empty;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    gvRMA.DataSource = null;
                    lblRMA.Text = "No records found";
                }
            }
            gvRMA.DataBind();
        }
        public void ReloadData()
        {
            //if (Session["adm"] == null)
            //{
            //    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            //    if (userInfo != null)
            //    {
            //        CompanyID = userInfo.CompanyGUID;

            //    }

            //}
            List<CompanyRmaStatuses> rmaStatusList = null;
            //if (Session["rmastatus"] == null)
            //    rmaStatusList = ReportOperations.GetCustomerRmaStatusReport(CompanyID, TimeInterval);
            //else
            if (Session["rmastatus"] != null)
            {
                rmaStatusList = (List<CompanyRmaStatuses>)Session["rmastatus"];


                Session["rmastatus"] = rmaStatusList;
                if (rmaStatusList != null && rmaStatusList.Count > 0)
                {
                    gvRMA.DataSource = rmaStatusList;
                    lblCount.Text = "<strong>Total Customers:</strong> " + Convert.ToString(rmaStatusList.Count - 1);
                    lblRMA.Text = string.Empty;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    gvRMA.DataSource = null;
                    lblRMA.Text = "No records found";
                }
            }
            else
            {
                lblCount.Text = string.Empty;
                gvRMA.DataSource = null;
                lblRMA.Text = string.Empty;
            }
            gvRMA.DataBind();
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRMA.PageIndex = e.NewPageIndex;
            ReloadData();

        }
    }
}