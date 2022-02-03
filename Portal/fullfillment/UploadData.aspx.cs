using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace avii.fullfillment
{
    public partial class UploadData : System.Web.UI.Page
    {
        string query = @"Aero_InsertFileInfo";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/logon.aspx";
                try
                {
                    HeadUser.Visible = true;
                    HeadAdmin.Visible = false;
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
                else
                {
                    trUser.Visible = (Convert.ToString(Session["IsAdmin"]) == "True");
                    trStatus.Visible = (Convert.ToString(Session["IsAdmin"]) == "True");
                }
            }
            else
            {
                HeadUser.Visible = false;
                HeadAdmin.Visible = true;
                LoadFileStatus();
            }
            if (!IsPostBack)
            {
                lblMsg.Text = string.Empty;
                this.PopulateUsers();
            }
        }

        private void PopulateUsers()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
            DataSet ds = SQLLAYER.DataTransaction.SQLLAYER.ExecuteDataset(connectionString, CommandType.StoredProcedure,
                "Aero_GetAllUsers");
            ddlUsers.DataSource = ds;
            ddlUsers.DataTextField = "Username";
            ddlUsers.DataValueField = "UserID";
            ddlUsers.DataBind();
            Session["Users"] = ds;

        }

        private void Cleanform()
        {
            this.ddlUsers.SelectedIndex = -1;
            txtComments.Text = string.Empty;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sbMessage = new System.Text.StringBuilder();
            try
            {
                lblMsg.Text = string.Empty;

                string fileStoreLocation = "~/UploadedData/";
                if (ConfigurationManager.AppSettings["FullfillmentFilesStoreage"] != null)
                {
                    fileStoreLocation = ConfigurationManager.AppSettings["FullfillmentFilesStoreage"].ToString();
                }


                if (flnUpload.PostedFile.FileName.Trim().Length == 0)
                {
                    lblMsg.Text = "Select file to upload";
                }
                else
                {
                    int fileType = -1;
                    string strFileNameOnly = string.Empty, fileStatus = "1";
                    string actualFilename = string.Empty;
                    int userID = 0, uploadby = 0;
                    if (flnUpload.PostedFile.ContentLength > 0)
                    {
                        uploadby = Convert.ToInt32(Session["UserID"]);
                        string isAdmin = (Session["IsAdmin"] != null ? Session["IsAdmin"].ToString() : string.Empty);
                        if (isAdmin == "")
                        {
                            //userID = Convert.ToInt32(Session["UserID"]);
                            fileType = 0;
                        }
                        else
                        {
                            if (ddlStatus.SelectedIndex > 0)
                            {
                                fileStatus = ddlStatus.SelectedValue;
                            }

                            if (ddlUsers.SelectedIndex == 0)
                            {
                                userID = -1;
                                fileType = 1;
                            }
                            else
                            {
                                fileType = 0;
                                userID = Convert.ToInt32(ddlUsers.SelectedValue);
                            }
                            // 1 is for all users
                            // Here select for all for selective user

                        }
                        actualFilename = System.IO.Path.GetFileName(flnUpload.PostedFile.FileName);
                        string strExtension = System.IO.Path.GetExtension(flnUpload.PostedFile.FileName);
                        strFileNameOnly = System.Guid.NewGuid().ToString() + strExtension;
                        fileStoreLocation = Server.MapPath(fileStoreLocation);
                        flnUpload.PostedFile.SaveAs(fileStoreLocation + strFileNameOnly);

                        string connectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
                        SqlParameter[] param = new SqlParameter[7];
                        param[0] = new SqlParameter("@ActualFileName", actualFilename);
                        param[1] = new SqlParameter("@Filename", strFileNameOnly);
                        param[2] = new SqlParameter("@Comments", txtComments.Text);
                        param[3] = new SqlParameter("@UserID", userID);
                        param[4] = new SqlParameter("@FileType", fileType);
                        param[5] = new SqlParameter("@IsAdmin", (isAdmin == string.Empty ? 0 : 1));
                        param[6] = new SqlParameter("@UploadBy", uploadby);
                        if (fileStatus != null)
                        {
                            param[5] = new SqlParameter("@FileStatus", fileStatus);
                        }

                        //query = string.Format(query, actualFilename, strFileNameOnly,
                        //    txtComments.Text.Replace("'", "''"), Convert.ToInt32(Session["UserID"]));
                        SQLLAYER.DataTransaction.SQLLAYER.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, query, param);

                        if (Session["Users"] != null && uploadby > 0)
                        {
                            DataSet ds = (DataSet)Session["Users"];
                            DataRow[] dRows = ds.Tables[0].Select("userID = " + uploadby);
                            if (dRows.Length > 0)
                            {
                                SendClientMail(Convert.ToInt32(fileStatus), dRows[0]["Email"].ToString());
                                sbMessage.Append("\n - Notification is send to client");
                            }
                        }

                        if (isAdmin == "")
                        {
                            this.SendAdminMail();
                            sbMessage.Append("\n - Notification is send to administrator");
                        }

                        this.lblMsg.Text = "Data Successfully Uploaded" + sbMessage.ToString();
                        Cleanform();
                    }
                }
            }
            catch (Exception ex)
            {
                this.lblMsg.Text = ex.Message;
            }
        }


        private void LoadFileStatus()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
            DataSet dsStatus = new DataSet();
            dsStatus = SQLLAYER.DataTransaction.SQLLAYER.ExecuteDataset(
                connectionString, CommandType.StoredProcedure, "Aero_GetFileStatus");

            if (this.ddlStatus.Items.Count == 0)
            {
                this.ddlStatus.DataSource = dsStatus;
                this.ddlStatus.DataTextField = "StatusName";
                this.ddlStatus.DataValueField = "StatusMasterID";
                this.ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "- Select -");
            }
        }


        private void SendAdminMail()
        {
            //string email = System.Configuration.ConfigurationManager.AppSettings["FulfillmentAdminEmail"];
            //avii.Classes.clsGeneral.SendEmail(email, email, "New User Fullfillment: " + Session["Username"].ToString(), txtComments.Text);
        }


        private void SendClientMail(int status, string custEmail)
        {
            string smsg = string.Empty, filename = string.Empty, subject = string.Empty;
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

            string email = System.Configuration.ConfigurationManager.AppSettings["AdminEmail"];
            //avii.Classes.clsGeneral.SendEmail(custEmail, email, subject, smsg);

        }


    }
}
