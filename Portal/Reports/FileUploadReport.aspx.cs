using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Reports
{
    public partial class FileUploadReport : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                BindCustomer();
                BindModules();
            }

        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        
        private void BindModules()
        {
            List<ModulesName> moduleList = ReportOperations.GetModuleNameList(0);
            if (moduleList != null && moduleList.Count > 0)
            {
                ddlModule.DataSource = moduleList;
                ddlModule.DataValueField = "ModuleID";
                ddlModule.DataTextField = "ModuleName";
                ddlModule.DataBind();
                ListItem item = new ListItem("", "");
                ddlModule.Items.Insert(0, item);
            }
            else
            {
                ddlModule.DataSource = null;
                ddlModule.DataBind();
            }
            

        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgMapping = e.Row.FindControl("imgMapping") as ImageButton;
                if (imgMapping != null)
                {
                    imgMapping.OnClientClick = "openDialogAndBlock('Assignment Info', '" + imgMapping.ClientID + "')";

                }
            }
        }

        protected void imgViewMapping_Click(object sender, CommandEventArgs e)
        {
            try
            {
                Control tmp2 = LoadControl("~/controls/Assignment.ascx");
                avii.Controls.Assignment ctlAssignment = tmp2 as avii.Controls.Assignment;
                pnlESN.Controls.Clear();
                string info = e.CommandArgument.ToString();
                string[] arr = info.Split(',');
                if (arr.Length > 1)
                {
                    int uploadedID = Convert.ToInt32(arr[0]);
                    int moduleID = Convert.ToInt32(arr[1]);
                    //lblTracking.Text = trackingNumber;
                    if (tmp2 != null)
                    {

                        ctlAssignment.BindFileUploadMapping(uploadedID, moduleID);
                    }
                    pnlESN.Controls.Add(ctlAssignment);

                    RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
                }
            }
            catch (Exception ex)
            {
                lblEsnMsg.Text = ex.Message;
            }


        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int companyID = 0;
            int moduleID = 0;
            
            DateTime fromDate, toDate;
            lblMsg.Text = string.Empty;

            try
            {
                fromDate = toDate = Convert.ToDateTime("1/1/0001");
                if (Session["adm"] == null)
                {
                    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    if (userInfo != null)
                    {
                        //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

                        lblCompany.Text = userInfo.CompanyName;
                        dpCompany.Visible = false;
                        companyID = userInfo.CompanyGUID;
                    }

                }
                else
                {
                    lblCompany.Text = string.Empty;

                    if (dpCompany.SelectedIndex > 0)
                        companyID = Convert.ToInt32(dpCompany.SelectedValue);
                }

                
                if (txtFromDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtFromDate.Text, out dt))
                        fromDate = dt;
                    else
                        throw new Exception("Upload Date From does not have correct format(MM/DD/YYYY)");
                }
                if (txtToDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtToDate.Text, out dt))
                        toDate = dt;
                    else
                        throw new Exception("Upload Date To does not have correct format(MM/DD/YYYY)");
                }
                if (ddlModule.SelectedIndex > 0)
                    moduleID = Convert.ToInt32(ddlModule.SelectedValue);

                if (companyID == 0 && txtFromDate.Text.Trim().Length == 0 && txtToDate.Text.Trim().Length == 0 && moduleID==0 )
                {
                    lblMsg.Text = "Please select the search criteria";
                }
                else
                    PopulateData(companyID,moduleID, fromDate, toDate);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }
        private void PopulateData(int companyID, int moduleID, DateTime fromDate, DateTime toDate)
        {

            List<Fileupload> fileList = ReportOperations.GetFileUploadReport(companyID, moduleID, fromDate, toDate);


            if (fileList != null && fileList.Count > 0)
            {
                //btnDownload.Visible = true;
                Session["fileList"] = fileList;
                gvFileUpload.DataSource = fileList;
                lblCount.Text = "<strong>Total count:</strong> " + Convert.ToString(fileList.Count);
                lblMsg.Text = string.Empty;

            }
            else
            {
                //btnDownload.Visible = false;
                Session["fileList"] = null;
                lblCount.Text = string.Empty;
                gvFileUpload.DataSource = null;
                lblMsg.Text = "No records found";
            }

            gvFileUpload.DataBind();
        }
        
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            gvFileUpload.DataSource = null;
            gvFileUpload.DataBind();
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            ddlModule.SelectedIndex = 0;
            dpCompany.SelectedIndex = 0;
            

        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFileUpload.PageIndex = e.NewPageIndex;
            if (Session["fileList"] != null)
            {
                List<Fileupload> fileList = (List<Fileupload>)Session["fileList"];

                gvFileUpload.DataSource = fileList;
                gvFileUpload.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }
    }
}