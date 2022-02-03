using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Controls
{
    public partial class RmaESNs : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //PopulateData();
            }
            else
            {
                ReloadData();
            }
        }
        
        public void PopulateData(int statusID, int companyID, int timeInterval)
        {
            //if (Session["adm"] == null)
            //{
            //    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            //    if (userInfo != null)
            //    {
            //        companyID = userInfo.CompanyGUID;

            //    }

            //}
            if (timeInterval == 0)
            {
                lblCount.Text = string.Empty;
                gvRmaEsn.DataSource = null;
                lblRMA.Text = string.Empty;
                Session["rmaEsnList"] = null;
            }
            else
            {
                List<RmaEsn> rmaEsnList = ReportOperations.GetCustomerRmaEsn(statusID, companyID, timeInterval);

                Session["rmaEsnList"] = rmaEsnList;
                if (rmaEsnList != null && rmaEsnList.Count > 0)
                {
                    gvRmaEsn.DataSource = rmaEsnList;
                    lblCount.Text = "<strong>Total RMA:</strong> " + Convert.ToString(rmaEsnList.Count);
                    lblRMA.Text = string.Empty;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    gvRmaEsn.DataSource = null;
                    lblRMA.Text = "No records found";
                }
            }
            gvRmaEsn.DataBind();
        }
        public void ReloadData()
        {
            
            List<RmaEsn> rmaEsnList = null;
            if (Session["rmaEsnList"] != null)
            {
                rmaEsnList = (List<RmaEsn>)Session["rmaEsnList"];


                Session["rmaEsnList"] = rmaEsnList;
                if (rmaEsnList != null && rmaEsnList.Count > 0)
                {
                    gvRmaEsn.DataSource = rmaEsnList;
                    lblCount.Text = "<strong>Total RMA:</strong> " + Convert.ToString(rmaEsnList.Count);
                    lblRMA.Text = string.Empty;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    gvRmaEsn.DataSource = null;
                    lblRMA.Text = "No records found";
                }
            }
            else
            {
                lblCount.Text = string.Empty;
                gvRmaEsn.DataSource = null;
                lblRMA.Text = string.Empty;
            }
            gvRmaEsn.DataBind();
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRmaEsn.PageIndex = e.NewPageIndex;
            ReloadData();

        }
    }
}