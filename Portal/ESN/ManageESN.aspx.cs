using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;

namespace avii.ESN
{
    public partial class ManageESN : System.Web.UI.Page
    {
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
            if (!IsPostBack)
            {
                BindCustomer();
            }
        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyId";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            ValidateESN();
        }
        private void ValidateESN()
        {
            SV.Framework.Inventory.EsnReverseOperation esnReverseOperation = SV.Framework.Inventory.EsnReverseOperation.CreateInstance<SV.Framework.Inventory.EsnReverseOperation>();

            rptESN.DataSource = null;
            rptESN.DataBind();
            //int POID = 0;
            string errorMessage = string.Empty;
            btnCancel.Visible = false;
            btnSubmit.Visible = false;

            //POID = Convert.ToInt32(ViewState["POID"]);
            lblMsg.Text = string.Empty;
            Hashtable hshESN = new Hashtable();
            int CompanyID = Convert.ToInt32(dpCompany.SelectedValue);
            bool columnsIncorrectFormat = false;
            try
            {
                if (CompanyID > 0)
                {
                    if (fu.PostedFile.FileName.Trim().Length == 0)
                    {
                        lblMsg.Text = "Select file to upload";
                    }
                    else
                    {
                        if (fu.PostedFile.ContentLength > 0)
                        {
                            string fileName = UploadFile();
                            string extension = Path.GetExtension(fu.PostedFile.FileName);
                            extension = extension.ToLower();
                            string invalidColumns = string.Empty;
                            ESNReverse assignESN = null;
                            List<ESNReverse> esnList = new List<ESNReverse>();

                            if (extension == ".csv")
                            {
                                using (StreamReader sr = new StreamReader(fileName))
                                {
                                    string line;
                                    string esn;//;
                                    int i = 0;
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        if (!string.IsNullOrEmpty(line) && i == 0)
                                        {
                                            i = 1;
                                            line = line.ToLower();
                                            string[] headerArray = line.Split(',');
                                            if (headerArray.Length > 0)
                                            {
                                                if (headerArray[0].Trim() != "imei")
                                                {
                                                    invalidColumns = headerArray[0];
                                                    columnsIncorrectFormat = true;
                                                }
                                            }
                                            else
                                            {
                                                columnsIncorrectFormat = true;
                                                invalidColumns = string.Empty;
                                            }
                                        }
                                        else if (!string.IsNullOrEmpty(line) && i > 0)
                                        {
                                            esn = string.Empty;
                                            string[] arr = line.Split(',');
                                            try
                                            {
                                                assignESN = new ESNReverse();
                                                esn = arr[0].Trim();
                                                if (string.IsNullOrEmpty(esn))
                                                { }
                                                else
                                                {
                                                    assignESN.ESN = esn;
                                                    if (string.IsNullOrEmpty(esn))
                                                    {
                                                        if (string.IsNullOrEmpty(esn))
                                                            lblMsg.Text = "Missing ESN data";
                                                    }
                                                    if (hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                    {
                                                        lblMsg.Text = "Duplicate " + esn + " ESN(s) exists in the file";

                                                    }
                                                    else if (!hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                    {
                                                        hshESN.Add(esn, esn);
                                                    }
                                                    esnList.Add(assignESN);
                                                    esn = string.Empty;
                                                }
                                            }
                                            //catch (ApplicationException ex)
                                            //{
                                            //    throw ex;
                                            //}
                                            catch (Exception exception)
                                            {
                                                lblMsg.Text = exception.Message;
                                            }
                                        }
                                    }
                                    sr.Close();
                                }

                                int InValid = 0;
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                                if (esnList != null && esnList.Count > 0 && columnsIncorrectFormat == false)
                                {
                                    if (lblMsg.Text == "")
                                    {
                                        List<ESNskuReverse> skuList;
                                        List<ESNReverse> esnList2 = esnReverseOperation.ESNValidate(esnList, CompanyID, out skuList);
                                        if (esnList2 != null && esnList2.Count > 0)
                                        {
                                            pnlESN.Visible = true;
                                            rptSKU.DataSource = skuList;
                                            rptSKU.DataBind();


                                            rptESN.DataSource = esnList2;
                                            rptESN.DataBind();


                                            Session["esnList2"] = esnList2;

                                            foreach (ESNReverse item in esnList2)
                                            {
                                                if (!string.IsNullOrEmpty(item.ErrorMessage))
                                                {
                                                    InValid = 1;
                                                    sb.Append(item.ESN + " " + item.ErrorMessage + " ");
                                                }
                                            }
                                            if (lblMsg.Text == "")
                                            {
                                                if (InValid == 1)
                                                {
                                                    errorMessage = sb.ToString();
                                                    lblMsg.Text = sb.ToString();
                                                }
                                            }
                                            else
                                            {
                                                return;
                                            }
                                        }
                                        if (!string.IsNullOrEmpty(errorMessage))
                                        {
                                            lblMsg.Text = errorMessage;
                                            return;
                                        }

                                        if (lblMsg.Text == string.Empty)
                                        {
                                            btnCancel.Visible = true;
                                            btnSubmit.Visible = true;
                                            btnSubmit.Enabled = true;

                                            //pnlUpload.Visible = false;

                                        }
                                        else
                                        {
                                            //pnlESN.Visible = false;
                                            //pnlUpload.Visible = true;

                                        }
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                                else
                                {

                                    rptESN.DataSource = null;
                                    rptESN.DataBind();


                                    if (esnList != null && esnList.Count == 0)
                                    {
                                        if (columnsIncorrectFormat)
                                        {
                                            if (!string.IsNullOrEmpty(invalidColumns))
                                                lblMsg.Text = invalidColumns + " column name is not correct";
                                            else
                                                lblMsg.Text = "File format is not correct";
                                        }
                                        else
                                            lblMsg.Text = "There is no ESN to upload";

                                    }
                                    if (esnList != null)
                                    {
                                        if (columnsIncorrectFormat)
                                        {
                                            if (!string.IsNullOrEmpty(invalidColumns))
                                                lblMsg.Text = invalidColumns + " column name is not correct";
                                            else
                                                lblMsg.Text = "File format is not correct";
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
                else
                {
                    lblMsg.Text = "Customer is required!";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        private string UploadFile()
        {
            string actualFilename = string.Empty;
            Int32 maxFileSize = 3145728;
            actualFilename = System.IO.Path.GetFileName(fu.PostedFile.FileName);

            string fileStoreLocation = Server.MapPath("~/UploadedData/");

            if (File.Exists(fileStoreLocation + actualFilename))
            {
                actualFilename = System.Guid.NewGuid().ToString() + actualFilename;
            }

            fu.PostedFile.SaveAs(fileStoreLocation + actualFilename);


            ViewState["filename"] = actualFilename;

            FileInfo fileInfo = new FileInfo(fileStoreLocation + actualFilename);

            if (ConfigurationManager.AppSettings["maxCSVfilesize"] != null)
            {
                if (Int32.TryParse(ConfigurationManager.AppSettings["maxCSVfilesize"].ToString(), out maxFileSize))
                {
                    if (fileInfo.Length > maxFileSize)
                    {
                        fileInfo.Delete();
                        throw new Exception("File size is greater than 3 MB");// + maxFileSize + " bytes");
                    }
                }
            }

            return fileStoreLocation + actualFilename;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            SV.Framework.Inventory.EsnReverseOperation esnReverseOperation = SV.Framework.Inventory.EsnReverseOperation.CreateInstance<SV.Framework.Inventory.EsnReverseOperation>();

            List<ESNReverse> esnList2 = Session["esnList2"] as List<ESNReverse>;
            if(esnList2 != null && esnList2.Count > 0)
            {
                string returnMessage = esnReverseOperation.ESNReverseUpdate(esnList2, 0);
                if (string.IsNullOrEmpty(returnMessage))
                {
                    lblMsg.Text = "Submitted successfully";
                    btnSubmit.Enabled = false;
                }
                else
                {
                    lblMsg.Text = returnMessage;
                }
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            rptESN.DataSource = null;
            rptESN.DataBind();
            pnlESN.Visible = false;
            dpCompany.SelectedIndex = 0;
            btnSubmit.Enabled = true;
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            GenerateCSV();
        }
        private void GenerateCSV()
        {
            lblMsg.Text = string.Empty;

            string string2CSV = "IMEI" + Environment.NewLine;

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ReconditionESN.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(string2CSV);
            Response.Flush();
            Response.End();

        }

        protected void lnkPO_Command(object sender, CommandEventArgs e)
        {
            int poID = Convert.ToInt32(e.CommandArgument);
            Session["reconditioning"] = "yes";
            Session["poid"] = poID;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('../FulfillmentDetails.aspx')</script>", false);

        }
    }
}