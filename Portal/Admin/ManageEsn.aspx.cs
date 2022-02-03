using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using avii.Classes;
using System.Web.UI;

namespace avii.Admin
{
    public partial class ManageEsn : System.Web.UI.Page
    {
        private string fileStoreLocation = "~/UploadedData/CleanBadESN/";
        
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
                //if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
        }
        
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMSL.PageIndex = e.NewPageIndex;
            if (Session["esnlist"] != null)
            {
                List<EsnInfo> esnList = (List<EsnInfo>)Session["esnlist"];

                gvMSL.DataSource = esnList;
                gvMSL.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }
        
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //lblMsg.CssClass = "errormessage";
            txtComment.Text = string.Empty;
            gvMSL.DataSource = null;
            gvMSL.DataBind();
            //rptESN.Visible = false;
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;

            btnSubmit.Visible = false;
            btnUpload.Visible = true;
            btnSubmit2.Visible = false;
            pnlSubmit.Visible = false;
            lblCount.Text = string.Empty;

            //btnViewAssignedESN.Visible = false;

        }
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //lblMsg.CssClass = "errormessage";
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;
            int returnValue = 0;
            //int userID = 0;
            bool selectESN = false;
            string filename = string.Empty;
            string comment = txtComment.Text;

            int userID = 0;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
                //ViewState["userid"] = userID;
            }
            if (ViewState["filename"] != null)
                filename = ViewState["filename"] as string;

            List<EsnList> esnList = new List<EsnList>();
            EsnList esnInfo = null;
            foreach (GridViewRow row in gvMSL.Rows)
            {
                CheckBox chkEsn = row.FindControl("chkEsn") as CheckBox;
                if (chkEsn.Checked == true && chkEsn.Visible == true)
                {
                    Label lblESN = row.FindControl("lblESN") as Label;
                    esnInfo = new EsnList();
                    esnInfo.ESN = lblESN.Text;
                    esnList.Add(esnInfo);
                    selectESN = true;
                }
            }
            if (esnList != null && esnList.Count > 0)
            {
                returnValue = avii.Classes.clsInventoryDB.RepositoryESNCleanUp(esnList, userID, filename, comment);
                if (returnValue > 0)
                {
                    lblMsg.Text = "Deleted successfully <br /> Record count: " + returnValue;
                }
            }
            else
            {
                if (!selectESN)
                    lblMsg.Text = "ESN not selected";

            }
        }
        
        protected void btnValidateUploadedFile_Click(object sender, EventArgs e)
        {

            //to Bind ESN csv file to grid
            BindESNs();
        }
        
        private void BindESNs()
        {
            //lblMsg.CssClass = "errormessage";
            lblConfirm.Text = string.Empty;

            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            Hashtable hshESN = new Hashtable();
            //bool esnExists = false;
            //bool esnIncorrectFormat = false;
            bool columnsIncorrectFormat = false;

            //bool uploadEsn = false;
            try
            {
                if (flnUpload.PostedFile.FileName.Trim().Length == 0)
                {
                    lblMsg.Text = "Select file to upload";
                }
                else
                {
                    if (flnUpload.PostedFile.ContentLength > 0)
                    {
                        string fileName = UploadFile();
                        string extension = Path.GetExtension(flnUpload.PostedFile.FileName);
                        extension = extension.ToLower();
                        string invalidColumns = string.Empty;
                        EsnList assignESN = null;
                        List<EsnList> esnList = new List<EsnList>();

                        if (extension == ".csv")
                        {
                            try
                            {
                                using (StreamReader sr = new StreamReader(fileName))
                                {
                                    string line;
                                    string esn;
                                    int i = 0;
                                    while ((line = sr.ReadLine()) != null)
                                    {

                                        if (!string.IsNullOrEmpty(line) && i == 0)
                                        {
                                            i = 1;
                                            line = line.ToLower();
                                            string[] headerArray = line.Split(',');

                                            if (headerArray[0].Trim() != "esn")
                                            {
                                                invalidColumns = headerArray[0];
                                                columnsIncorrectFormat = true;
                                            }
                                            

                                        }
                                        else if (!string.IsNullOrEmpty(line) && i > 0)
                                        {
                                            esn = string.Empty;
                                            string[] arr = line.Split(',');
                                            try
                                            {

                                                if (arr.Length > 0)
                                                {
                                                    esn = arr[0].Trim();
                                                    
                                                }
                                                //if (string.IsNullOrEmpty(poNum) || string.IsNullOrEmpty(customerAccountNumber) || string.IsNullOrEmpty(sku) || string.IsNullOrEmpty(esn))
                                                //{
                                                //    lblMsg.Text = "Missing required data";
                                                //}

                                                assignESN = new EsnList();

                                                if (hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                {
                                                    //uploadEsn = false;
                                                    throw new ApplicationException("Duplicate ESN(s) exists in the file");
                                                }
                                                else if (!hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                {
                                                    hshESN.Add(esn, esn);
                                                }

                                                
                                                //uploadEsn = true;
                                                assignESN.ESN = esn;
                                                //assignESN.AKEY = string.Empty;
                                                //assignESN.MEID = string.Empty;
                                                
                                                esnList.Add(assignESN);
                                                esn = string.Empty;
                                            }
                                            catch (ApplicationException ex)
                                            {
                                                throw ex;
                                            }
                                            catch (Exception exception)
                                            {
                                                lblMsg.Text = exception.Message;
                                            }
                                        }
                                    }

                                    sr.Close();
                                }
                            }
                            catch (Exception ex)
                            {
                                lblMsg.Text = ex.Message;
                            }


                            if (esnList != null && esnList.Count > 0 && columnsIncorrectFormat == false)
                            {
                                List<EsnInfo> esnInfoList = null;
                                esnInfoList = avii.Classes.clsInventoryDB.GetESNCleanUpList(esnList);

                                gvMSL.DataSource = esnInfoList;
                                gvMSL.DataBind();
                                lblCount.Text = "Total count: " + esnInfoList.Count;
                                Session["esnlist"] = esnInfoList;
                                if (lblMsg.Text == string.Empty)
                                {
                                    //lblMsg.CssClass = "errorGreenMsg";
                                    lblConfirm.Text = "ESN file is ready to upload";
                                    btnUpload.Visible = false;
                                    btnSubmit.Visible = true;
                                    btnSubmit2.Visible = true;
                                    pnlSubmit.Visible = true;

                                }
                                else
                                {
                                    btnUpload.Visible = true;
                                    btnSubmit.Visible = false;

                                    btnSubmit2.Visible = false;
                                    pnlSubmit.Visible = false;

                                }

                            }
                            else
                            {
                                gvMSL.DataSource = null;
                                gvMSL.DataBind();

                                if (esnList != null && esnList.Count == 0)
                                {
                                    if (columnsIncorrectFormat)
                                    {
                                        lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                    }
                                    else
                                        lblMsg.Text = "There is no ESN to upload";

                                }
                                if (esnList != null)
                                {
                                    if (columnsIncorrectFormat)
                                    {
                                        lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                    }
                                    else
                                        lblMsg.Text = "There is no ESN to upload";
                                }
                            }
                        }
                        else
                            lblMsg.Text = "Invalid file!";
                    }
                    else
                        lblMsg.Text = "Invalid file!";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }
        
        protected void gvMSL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
                return;

            LinkButton lnkPO = e.Row.FindControl("lnkPO") as LinkButton;
            if (lnkPO != null)
                lnkPO.OnClientClick = "openDialogAndBlock('Fulfillment Detail', '" + lnkPO.ClientID + "')";
            LinkButton lnkRMA = e.Row.FindControl("lnkRMA") as LinkButton;
            if (lnkRMA != null)
                lnkRMA.OnClientClick = "openRMADialogAndBlock('RMA Detail', '" + lnkRMA.ClientID + "')";
        }
        protected void imgViewRMA_Click(object sender, CommandEventArgs e)
        {
            Control tmp = LoadControl("~/controls/RMADetails.ascx");
            avii.Controls.RMADetails ctlRMADetail = tmp as avii.Controls.RMADetails;
            pnlRMA.Controls.Clear();
            int rmaGUID = Convert.ToInt32(e.CommandArgument);
            // ViewState["rmaGUID"] = rmaGUID;
            if (tmp != null)
            {

                ctlRMADetail.BindRMA(rmaGUID, false);
            }
            pnlRMA.Controls.Add(ctlRMADetail);
            //ModalPopupExtender2.Show();
            RegisterStartupScript("jsUnblockDialog", "unblockRMADialog();");

        }
        protected void imgViewPO_Click(object sender, CommandEventArgs e)
        {
            Control tmp2 = LoadControl("~/controls/PODetails.ascx");
            avii.Controls.PODetails ctlPODetails = tmp2 as avii.Controls.PODetails;
            pnlPO.Controls.Clear();
            int poID = Convert.ToInt32(e.CommandArgument);
            //ViewState["poid"] = poID;
            if (tmp2 != null)
            {

                ctlPODetails.BindPO(poID, false);
            }
            pnlPO.Controls.Add(ctlPODetails);
            RegisterStartupScript("jsUnblockDialog", "unblockDialog();");

            //ModalPopupExtender1.Show();
        }
        private string UploadFile()
        {
            string actualFilename = string.Empty;
            Int32 maxFileSize = 1572864;
            actualFilename = System.IO.Path.GetFileName(flnUpload.PostedFile.FileName);
            if (ConfigurationManager.AppSettings["CleanBadESNFilesStoreage"] != null)
            {
                fileStoreLocation = ConfigurationManager.AppSettings["CleanBadESNFilesStoreage"].ToString();
            }

            fileStoreLocation = Server.MapPath(fileStoreLocation);
            if (File.Exists(fileStoreLocation + actualFilename))
            {
                actualFilename = System.Guid.NewGuid().ToString() + actualFilename;
            }

            flnUpload.PostedFile.SaveAs(fileStoreLocation + actualFilename);
            ViewState["filename"] = fileStoreLocation + actualFilename;

            FileInfo fileInfo = new FileInfo(fileStoreLocation + actualFilename);

            if (ConfigurationManager.AppSettings["maxCSVfilesize"] != null)
            {
                if (Int32.TryParse(ConfigurationManager.AppSettings["maxCSVfilesize"].ToString(), out maxFileSize))
                {
                    if (fileInfo.Length > maxFileSize)
                    {
                        fileInfo.Delete();
                        throw new Exception("File size is greater than " + maxFileSize + " bytes");
                    }
                }
            }



            return fileStoreLocation + actualFilename;
        }

    }
}