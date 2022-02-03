using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
namespace avii.Controls
{
    public partial class ViewESN : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Reload();
        }

        public void BindSKUsESNsList(int statusID, int itemCompanyGUID, DateTime fromDate, DateTime toDate, bool isESN)
        {
            lblESN.Text = string.Empty;
            lblDate.Text = string.Empty;
            string date = string.Empty;
            //Session["esns"] = null;
            //lblDate.Text = DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString(); //esnList[esnList.Count - 1].UploadDate.ToShortDateString() + " - " + esnList[0].UploadDate.ToShortDateString();
            if (fromDate != DateTime.MinValue && toDate == DateTime.MinValue)
                date = fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString(); //esnList[esnList.Count - 1].UploadDate.ToShortDateString() + " - " + esnList[0].UploadDate.ToShortDateString()
            else
                if (fromDate != DateTime.MinValue && toDate != DateTime.MinValue)
                    date = fromDate.ToShortDateString() + " - " + toDate.ToShortDateString();
                else
                    if (fromDate == DateTime.MinValue && toDate == DateTime.MinValue)
                        date = DateTime.Now.AddDays(-365).ToShortDateString() + " - " + DateTime.Now.ToShortDateString();
            lblDate.Text = date;
            ViewState["date"] = date;

            List<MslESN> esnList = ReportOperations.GetSkusEsnList(statusID, itemCompanyGUID, fromDate, toDate, isESN);

            if (esnList != null && esnList.Count > 0)
            {

                gvESNs.DataSource = esnList;
                gvESNs.DataBind();
                Session["esns"] = esnList;
            }
            else
            {
                gvESNs.DataSource = null;
                gvESNs.DataBind();
                Session["esns"] = null;
                lblESN.Text = "No record found";
            }

        }
        public void BindSkuRmaESNsList(int statusID, int itemCompanyGUID, DateTime fromDate, DateTime toDate, bool isESN)
        {
            lblESN.Text = string.Empty;
            lblDate.Text = string.Empty;
            string date = string.Empty;
            //Session["esns"] = null;
            //lblDate.Text = DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString(); //esnList[esnList.Count - 1].UploadDate.ToShortDateString() + " - " + esnList[0].UploadDate.ToShortDateString();
            if (fromDate != DateTime.MinValue && toDate == DateTime.MinValue)
                date = fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString(); //esnList[esnList.Count - 1].UploadDate.ToShortDateString() + " - " + esnList[0].UploadDate.ToShortDateString()
            else
                if (fromDate != DateTime.MinValue && toDate != DateTime.MinValue)
                    date = fromDate.ToShortDateString() + " - " + toDate.ToShortDateString();
                else
                    if (fromDate == DateTime.MinValue && toDate == DateTime.MinValue)
                        date = DateTime.Now.AddDays(-365).ToShortDateString() + " - " + DateTime.Now.ToShortDateString();
            lblDate.Text = date;
            ViewState["date"] = date;

            List<MslESN> esnList = ReportOperations.GetSkusRmaEsnList(statusID, itemCompanyGUID, fromDate, toDate, isESN);

            if (esnList != null && esnList.Count > 0)
            {

                gvESNs.DataSource = esnList;
                gvESNs.DataBind();
                Session["esns"] = esnList;
            }
            else
            {
                gvESNs.DataSource = null;
                gvESNs.DataBind();
                Session["esns"] = null;
                lblESN.Text = "No record found";
            }

        }
        
        public void BindESN(int itemGUID, int itemCompanyGUID, DateTime fromDate, DateTime toDate)
        {
            lblESN.Text = string.Empty;
            lblDate.Text = string.Empty;
            string date = string.Empty;
            
            //Session["esns"] = null;
            //lblDate.Text = DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString(); //esnList[esnList.Count - 1].UploadDate.ToShortDateString() + " - " + esnList[0].UploadDate.ToShortDateString();
            if (fromDate != DateTime.MinValue && toDate == DateTime.MinValue)
                date = fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString(); //esnList[esnList.Count - 1].UploadDate.ToShortDateString() + " - " + esnList[0].UploadDate.ToShortDateString()
            else
                if (fromDate != DateTime.MinValue && toDate != DateTime.MinValue)
                    date = fromDate.ToShortDateString() + " - " + toDate.ToShortDateString();
                else
                    if (fromDate == DateTime.MinValue && toDate == DateTime.MinValue)
                        date = DateTime.Now.AddDays(-365).ToShortDateString() + " - " + DateTime.Now.ToShortDateString();
            lblDate.Text = date;
            ViewState["date"] = date;

            List<MslESN> esnList = ReportOperations.GetEsnList(itemGUID, itemCompanyGUID, fromDate, toDate);

            if (esnList != null && esnList.Count > 0)
            {
                
                gvESNs.DataSource = esnList;
                gvESNs.DataBind();
                Session["esns"] = esnList;
            }
            else
            {
                gvESNs.DataSource = null;
                gvESNs.DataBind();
                Session["esns"] = null;
                lblESN.Text = "No record found";
            }
            
        }
        private void Reload()
        {
            if (Session["esns"] != null)
            {
                List<MslESN> esnList = (List<MslESN>)Session["esns"];
                //lblDate.Text = lblDate.Text = "<b>Upload Date:</b> " + DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString(); //esnList[esnList.Count - 1].UploadDate.ToShortDateString() + " - " + esnList[0].UploadDate.ToShortDateString();
                //esnList[esnList.Count - 1].UploadDate.ToShortDateString() + " - " + esnList[0].UploadDate.ToShortDateString();
                gvESNs.DataSource = esnList;
                gvESNs.DataBind();
                if (ViewState["date"] != null)
                    lblDate.Text = ViewState["date"] as string;
            }
            //else
            //    lblESN.Text = "Session expire!";

        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvESNs.PageIndex = e.NewPageIndex;
            if (Session["esns"] != null)
            {
                List<MslESN> esnList = (List<MslESN>)Session["esns"];

                gvESNs.DataSource = esnList;
                gvESNs.DataBind();
                if (ViewState["date"] != null)
                    lblDate.Text = ViewState["date"] as string;
            }
            else
                lblESN.Text = "Session expire!";
        }
    }
}