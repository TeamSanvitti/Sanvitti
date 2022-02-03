using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.fullfillment.User
{
    public partial class Manageuploads : System.Web.UI.Page
    {
        string connectionString = string.Empty;
        DataSet dsStatus = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            btnDelete.Attributes.Add("onclick", "validate()");
            int userID = 0;
            connectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
            this.LoadFileStatus();
            lblMsg.Text = string.Empty;
            if (!Page.IsPostBack)
            {
                if (Session["adm"] == null)
                {
                    trUsers.Visible = false;
                    HeadUser.Visible = true;
                    HeadAdmin.Visible = false;

                    string url = "/logon.aspx";
                    try
                    {
                        url = ConfigurationSettings.AppSettings["LogonPage"].ToString();
                    }
                    catch
                    {
                        url = "/logon.aspx";
                    }
                    if (Session["UserID"] == null)
                    {
                        Response.Redirect(url);
                    }
                }         
                else
                {
                    HeadUser.Visible = false;
                    HeadAdmin.Visible = true;
                    trUsers.Visible = true;
                }

                if (Request.Params["UserID"] != null)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["UserID"]))
                    {
                        userID = Convert.ToInt32(Request.QueryString["UserID"]);
                    }
                    else
                    {
                        userID = Convert.ToInt32(Session["UserID"]);
                    }
                    this.PopulateUserComments(userID);
                }
                this.LoadUsers();
                if (userID > 0)
                {
                    ddlUsername.SelectedValue = userID.ToString();
                }
            }
        }

        private void LoadUsers()
        {
            connectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
            ddlUsername.DataSource = SQLLAYER.DataTransaction.SQLLAYER.ExecuteDataset(connectionString, CommandType.StoredProcedure,
                "Aero_GetAllUsers");
            ddlUsername.DataTextField = "Username";
            ddlUsername.DataValueField = "UserID";
            ddlUsername.DataBind();
            ddlUsername.Items.Insert(0, "- Select -");
        }

        private void LoadFileStatus()
        {
            dsStatus = new DataSet();
            dsStatus = SQLLAYER.DataTransaction.SQLLAYER.ExecuteDataset(
                connectionString, CommandType.StoredProcedure, "Aero_GetFileStatus");

            if (!Page.IsPostBack)
            {
                this.ddlStatus.DataSource = dsStatus;
                this.ddlStatus.DataTextField = "StatusName";
                this.ddlStatus.DataValueField = "StatusMasterID";
                this.ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "- Select -");
            }
        }

        private void PopulateUserComments(int UserID)
        {
            string query = "Aero_GetUploadedFilesInfo";
            int isAdmin;
            int type = 0;

            if (string.IsNullOrEmpty(Convert.ToString(Session["IsAdmin"])) && !string.IsNullOrEmpty(Request.QueryString["UserID"]))
            {
                isAdmin = 0;
                type = 1;
            }
            else
            {
                isAdmin = Convert.ToInt16(Session["IsAdmin"]);
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", UserID);
            param[1] = new SqlParameter("@IsAdmin", isAdmin);
            param[1] = new SqlParameter("@ReadStatus", "U");

            DataSet ds = SQLLAYER.DataTransaction.SQLLAYER.ExecuteDataset(connectionString, CommandType.StoredProcedure, query, param);
            if (ds != null && ds.Tables.Count > 0)
            {
                gvUserComments.DataSource = ds;
                gvUserComments.DataBind();

                if (isAdmin > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    btnSubmit.Visible = true;
                    btnDelete.Visible = true;
                }
                else
                {
                    btnSubmit.Visible = false;
                    btnDelete.Visible = false;
                }
            }
        }

        protected void gvUserComments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((DropDownList)(e.Row.FindControl("ddlStatus"))).DataSource = dsStatus;
                ((DropDownList)(e.Row.FindControl("ddlStatus"))).DataTextField = "StatusName";
                ((DropDownList)(e.Row.FindControl("ddlStatus"))).DataValueField = "StatusMasterID";
                ((DropDownList)(e.Row.FindControl("ddlStatus"))).DataBind();
                ((DropDownList)(e.Row.FindControl("ddlStatus"))).SelectedValue = ((Label)(e.Row.FindControl("lblStatus"))).Text;
                if (string.IsNullOrEmpty(Convert.ToString(Session["IsAdmin"])) && Convert.ToString(Session["IsAdmin"]) != "1")
                {            
                    ((DropDownList)(e.Row.FindControl("ddlStatus"))).Enabled = false;
                    ((DropDownList)(e.Row.FindControl("ddlStatus"))).Visible = false;
                    ((Label)(e.Row.FindControl("lblUserStatus"))).Text = ((DropDownList)(e.Row.FindControl("ddlStatus"))).SelectedItem.Text;
                    ((Label)(e.Row.FindControl("lblUserStatus"))).Visible = true;
                    ((CheckBox)(e.Row.FindControl("chkStatus"))).Visible = false;
                    this.trUsers.Visible = false;
                }
            }
        }

        private void SendMail(string mailTo)
        {
            System.Web.Mail.MailMessage message = new System.Web.Mail.MailMessage();
            message.To = mailTo;
            message.Subject = "File Status Changed";
            message.Body = "File status changed";
            message.From = System.Configuration.ConfigurationManager.AppSettings["AdminEmail"];

            System.Web.Mail.SmtpMail.SmtpServer = System.Configuration.ConfigurationManager.AppSettings["MailServer"];
            System.Web.Mail.SmtpMail.Send(message);
        }

        protected void gvUserComments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string path = (string)e.CommandArgument;
            //get file object as FileInfo 
            System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath(path));
            //-- if the file exists on the server 
            if (file.Exists)
            {
                //set appropriate headers 
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string whereClause = string.Empty;
            string query = "Aero_GetUploadedFilesInfo";
            int isAdmin;
            int type = 5;
            string strAnd = "";
            int userID;
            lblMsg.Text = string.Empty;


            if (!string.IsNullOrEmpty(Request["UserID"]))
            {
                userID = Convert.ToInt32(Request.QueryString["UserID"]);
            }
            else
            {
                userID = Convert.ToInt32(Session["UserID"]);
            }

            if (string.IsNullOrEmpty(Convert.ToString(Session["IsAdmin"])) || !string.IsNullOrEmpty(Request.QueryString["UserID"]))
            {
                isAdmin = 0;
                type = 1;
            }
            else
            {
                isAdmin = Convert.ToInt16(Session["IsAdmin"]);
            }


            if (ddlUsername.SelectedIndex > 0)//!= 0 && ddlUsername.SelectedIndex != -1
            {
                userID = Convert.ToInt32(ddlUsername.SelectedValue);
            }

            string startDate, endDate, status;
            startDate = (txtStartDate.Text.Trim().Length > 0?txtStartDate.Text.Trim(): null);
            endDate = (txtEndDate.Text.Trim().Length > 0?txtEndDate.Text.Trim(): null);
            status  = (ddlStatus.SelectedIndex > 0 ? ddlStatus.SelectedValue.Trim() : null);

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@UserID", userID);
            param[1] = new SqlParameter("@StartDate", startDate);
            param[2] = new SqlParameter("@EndDate", endDate);
            param[3] = new SqlParameter("@Status", status);
            param[4] = new SqlParameter("@ReadStatus", this.ddlReadStatus.SelectedValue);

            connectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
            DataSet ds = SQLLAYER.DataTransaction.SQLLAYER.ExecuteDataset(
                connectionString, CommandType.StoredProcedure, query, param);
            if (ds != null && ds.Tables.Count > 0)
            {
                this.btnSearch.Visible = true;
                gvUserComments.DataSource = ds;
                gvUserComments.DataBind();

                if (!string.IsNullOrEmpty(Convert.ToString(Session["IsAdmin"])) && ds.Tables[0].Rows.Count > 0)
                {
                    btnSubmit.Visible = true;
                    btnDelete.Visible =  true;
                }
                else
                {
                    btnSubmit.Visible = false;
                    btnDelete.Visible = false;
                }
            }
            else
            {
                lblMsg.Text = "No record exists for selected criteria";

            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int status = 0;
            int fileID = 0;
            DataKey datakey = null;

            try
            {
                lblMsg.Text = string.Empty;
                connectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
                foreach (GridViewRow gridrow in gvUserComments.Rows)
                {
                    if (((CheckBox)(gridrow.FindControl("chkStatus"))).Checked)
                    {
                        datakey = gvUserComments.DataKeys[gridrow.RowIndex];
                        fileID = Convert.ToInt32(datakey.Value);
                        status = Convert.ToInt32(((DropDownList)(gridrow.FindControl("ddlStatus"))).SelectedValue);
                        SqlParameter[] param = new SqlParameter[2];
                        param[0] = new SqlParameter("@FileID", fileID);
                        param[1] = new SqlParameter("@Status", status);

                        SQLLAYER.DataTransaction.SQLLAYER.ExecuteNonQuery(connectionString,
                            CommandType.StoredProcedure, "Aero_UpdateFileStatus", param);

                        SendClientMail(status, ((Label)(gridrow.FindControl("lblEmail"))).Text.Trim());
                        //this.SendMail(((Label)(gridrow.FindControl("lblEmail"))).Text);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Server.Transfer("..//UploadData.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string filename = string.Empty;
            string path = Server.MapPath(ConfigurationManager.AppSettings["FullfillmentFilesStoreage"].ToString() + "\\");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            int rowIndex = 0;
            foreach (GridViewRow gridrow in gvUserComments.Rows)
            {
                
                if (((CheckBox)(gridrow.FindControl("chkStatus"))).Checked)
                {
                    if (gridrow.FindControl("fname") != null)
                    {
                        filename = ((Label)(gridrow.FindControl("fname"))).Text;
                    }

                    if (System.IO.File.Exists(path + filename))
                    {                        
                        if(Convert.ToInt32(gvUserComments.DataKeys[rowIndex].Value) > 0)
                        {
                            if (DeleteFileInfo(Convert.ToInt32(gvUserComments.DataKeys[rowIndex].Value)) > 0)
                            {
                                System.IO.File.Delete(path + filename);
                            }
                        }
                    }
                }

                rowIndex++;
            }
            btnSearch_Click(sender, e);
        }

        private int DeleteFileInfo(int UPloadedFileID)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UPloadedFileID", UPloadedFileID);

            return SQLLAYER.DataTransaction.SQLLAYER.ExecuteNonQuery(
                connectionString, CommandType.StoredProcedure, "Aero_DeleteFileInfo", param);
        }


        private void SendClientMail(int status, string custEmail)
        {
            string smsg = string.Empty, filename = string.Empty, subject = string.Empty;
            if (custEmail.Length > 0)
            {
                if (status == 1)
                {
                    filename = Server.MapPath("//Emails") + @"\" + "fulfilmentPending.htm";
                    smsg = avii.Classes.clsGeneral.fnEmailHTML(filename);
                    subject = ConfigurationManager.AppSettings["FulfillmentSubjectPending"].ToString();
                }
                if (status == 2)
                {
                    filename = Server.MapPath("//Emails") + @"\" + "FulfillmentOK.htm";
                    smsg = avii.Classes.clsGeneral.fnEmailHTML(filename);
                    subject = ConfigurationManager.AppSettings["FulfillmentSubjectOK"].ToString();
                }
                else if (status == 3)
                {
                    filename = Server.MapPath("//Emails") + @"\" + "FulfillmentCancel.htm";
                    smsg = avii.Classes.clsGeneral.fnEmailHTML(filename);
                    subject = ConfigurationManager.AppSettings["FulfillmentSubjectCancel"].ToString();
                }

                //string email = System.Configuration.ConfigurationManager.AppSettings["AdminEmail"];
                //avii.Classes.clsGeneral.SendEmail(custEmail, email, subject, smsg);

            }
        }

    }
}
