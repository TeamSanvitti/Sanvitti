using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using avii.Classes;

namespace avii
{
    public partial class frmErrorLog : System.Web.UI.Page
    {
        private string downLoadPath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnClearLog.Attributes.Add("onclick", "return ErrorLogValidate()");

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
                if (Session["adm"] != null)
                {
                    btnClearLog.Visible = true;
                }        
            }
            downLoadPath = ConfigurationManager.AppSettings["FullfillmentFilesStoreage"].ToString();
        }

        private void GenerateErrorLog(string poNumber, DateTime fromDate, DateTime toDate, string moduleName, string ReturnCode)
        {
            CultureInfo enUS = new CultureInfo("en-US");
            ErrorLogSearch search = new ErrorLogSearch();

            search.PurhcaseOrdernumber = (string.IsNullOrEmpty(poNumber)?null:poNumber.Trim());
            search.LogFromDate = fromDate;
            search.LogToDate = toDate;
            search.ReturnCode = (string.IsNullOrEmpty(ReturnCode) ? null : ReturnCode); ;
            search.ModuleName = (string.IsNullOrEmpty(moduleName)?null:moduleName);

            List<ErrorLog> errorLogList = clsErrorLog.GetErrorLog(search);

            if (errorLogList != null && errorLogList.Count > 0)
            {
                Session["ErrorLog"] = errorLogList;
                gvErrorLog.DataSource = errorLogList;
                gvErrorLog.DataBind();
            }
            else
            {
                gvErrorLog.DataSource = null; 
                gvErrorLog.DataBind();
                lblMsg.Text = "No record exists for selected criteria";
            }

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            string poNumber = txtPONum.Text.Trim();
            string retCode = txtCode.Text.Trim();
            DateTime fromDate, toDate;
            DateTime.TryParse(txtFromDate.Text.Trim(),out fromDate);
            DateTime.TryParse(txtToDate.Text.Trim(),out toDate);

            GenerateErrorLog(poNumber, fromDate, toDate, null, retCode);
        }

        private void ClearForm()
        {
            this.txtFromDate.Text = string.Empty;
            this.txtToDate.Text = string.Empty;
            this.txtPONum.Text = string.Empty;
            txtCode.Text = string.Empty;
            btnDownload.Visible = false;
            if (Session["adm"] != null)
            {
                btnClearLog.Visible = true;
            }
            else
                btnClearLog.Visible = false;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        protected void btnClearLog_Click(object sender, EventArgs e)
        {

            clsErrorLog.ClearErrorLog();
            ClearForm();
            lblMsg.Text = "Error Log is cleared";

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;

            string path = Server.MapPath(downLoadPath).ToString();
            string fileName = "ErrorLog" + Session.SessionID + ".csv";
            bool found = false;
            System.IO.FileInfo file = null;
            file = new System.IO.FileInfo(path + fileName);
            if (file.Exists)
            {
                file.Delete();
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            List<ErrorLog> errorLogList =  Session["ErrorLog"] as List<ErrorLog>;
            if (errorLogList != null && errorLogList.Count > 0)
            {
                found = true;
                sb.Append("ModuleName,LogDate,PurhcaseOrdernumber,Description\n");
                foreach (ErrorLog errorLog in errorLogList)
                {

                    sb.Append(errorLog.ModuleName + "," + errorLog.LogDate.ToShortDateString() + "," + errorLog.PurhcaseOrdernumber + "," + errorLog.Description + "\n");
                }

                try
                {
                    using (StreamWriter sw = new StreamWriter(file.FullName))
                    {
                        sw.WriteLine(sb.ToString());
                        sw.Flush();
                        sw.Close();
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                }
            }

            if (found)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                // Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();
            }
        }
    }
}
