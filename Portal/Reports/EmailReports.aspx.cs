using System;
using System.Data;
using System.Web.UI.WebControls;
namespace avii.Reports
{
    public partial class EmailReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    url = System.Configuration.ConfigurationSettings.AppSettings["LogonPage"].ToString();

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
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        { 
            SearchEmailReport(); 
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        { 
            ClearAll();
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmails.PageIndex = e.NewPageIndex;

            ReLoad();
        }
        private void ReLoad()
        {
            if (Session["emailreport"] != null)
            {
                DataTable reportTable = (DataTable)Session["emailreport"];
                gvEmails.DataSource = reportTable;
                gvEmails.DataBind();
            }
            else
                SearchEmailReport();
        }
        private void ClearAll()
        {
            lblMsg.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            gvEmails.DataSource = null;
            gvEmails.DataBind();
        }
        private void SearchEmailReport()
        {
            lblMsg.Text = string.Empty;
            bool validForm = true;
            string moduleName, fromDate, toDate;
            int companyID = 0;
            moduleName = fromDate = toDate = null;
            try
            {
                lblMsg.Text = string.Empty;
                moduleName = (ddlModule.SelectedIndex > 0 ? ddlModule.SelectedValue : null);
                if (txtFromDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtFromDate.Text, out dt))
                        fromDate = dt.ToShortDateString();
                    else
                        throw new Exception("Date From does not have correct format(MM/DD/YYYY)");
                }
                if (txtEndDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtEndDate.Text, out dt))
                        toDate = dt.ToShortDateString();
                    else
                        throw new Exception("Date To does not have correct format(MM/DD/YYYY)");

                }
                

                if (string.IsNullOrEmpty(moduleName) && string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
                {
                    validForm = false;
                }
                if (validForm)
                {
                    DataTable reportTable = avii.Classes.ReportOperations.GetEmailLogReport(fromDate, toDate, moduleName);
                    if (reportTable.Rows.Count > 0)
                    {
                        gvEmails.DataSource = reportTable;
                        gvEmails.DataBind();

                        Session["emailreport"] = reportTable;
                    }
                    else
                    {
                        lblMsg.Text = "No record exists";
                        Session["emailreport"] = null;
                        gvEmails.DataSource = null;
                        gvEmails.DataBind();
                    }
                }
                else
                {
                    lblMsg.Text = "Please select the search criteria";
                    Session["emailreport"] = null;
                    gvEmails.DataSource = null;
                    gvEmails.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void imgDelete_Commnad(object sender, CommandEventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                avii.Classes.ReportOperations.DeleteEmailReport(Convert.ToInt32(e.CommandArgument));
                
                SearchEmailReport();
                lblMsg.Text = "Deleted successfully";
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void imgResendEmail_Commnad(object sender, CommandEventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                int emailLogID = Convert.ToInt32(e.CommandArgument);

                DataTable orderTable = new DataTable();

                orderTable = (DataTable)Session["emailreport"];
                DataRow[] rows = orderTable.Select(string.Format("EmailLogID='{0}' ", emailLogID));
                DataRow searchedRow = null;
                if (rows.Length > 0)
                {
                    searchedRow = rows[0];
                    string emailbody = Convert.ToString(searchedRow["EmailBody"]);
                    int userID = Convert.ToInt32(searchedRow["EmailSentBy"]);
                    string custEmail = Convert.ToString(searchedRow["emailto"]);
                    string emailCC = Convert.ToString(searchedRow["emailcc"]);
                    SendClientMail(emailbody, custEmail, userID, emailCC);

                    lblMsg.Text = "Email sent.";

                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }
        private void SendClientMail(string smsg, string custEmail, int userID, string rmaemail)
        {
            
            try
            {
                if (custEmail.Length > 0)
                {
                    string subject = "Lan Global RMA Department";
                    avii.Classes.clsGeneral.SendEmail(custEmail, rmaemail, subject, smsg, userID, 1);
                    System.Diagnostics.EventLog.WriteEntry("Application", "Sending RMA Email " + custEmail);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "Sending RMA Email:" + ex.Message);
            }
        }
        protected void imgEditOrder_Commnad(object sender, CommandEventArgs e)
        {
            try
            {

                int emailLogID = Convert.ToInt32(e.CommandArgument);

                DataTable orderTable = new DataTable();

                orderTable = (DataTable)Session["emailreport"];
                DataRow[] rows = orderTable.Select(string.Format("EmailLogID='{0}' ", emailLogID));
                DataRow searchedRow = null;
                if (rows.Length > 0)
                {
                    searchedRow = rows[0];
                    string poXML = Convert.ToString(searchedRow["EmailBody"]);
                    //poXML = poXML.Replace("<", "&lt;");
                    //poXML = poXML.Replace("<", "&gt;");
                    lblPoXML.Text = poXML;


                }
                ModalPopupExtender1.Show();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
    }
}