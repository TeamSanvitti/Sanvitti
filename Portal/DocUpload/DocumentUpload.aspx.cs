using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Fulfillment;
using System.Configuration;
using System.IO;

namespace avii.DocUpload
{
    public partial class DocumentUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["adm"] == null)
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
                    BindCustomer();

                }
                else
                {
                    trCustomer.Visible = false;
                    avii.Classes.UserInfo objUserInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    ViewState["CompanyName"] = objUserInfo.CompanyName; 
                }
                LoadPODoc();

            }
        }
        private void LoadPODoc()
        {
            if(Session["poinfo"] != null)
            {
                string poinfo = Convert.ToString(Session["poinfo"]);
                string[] array = poinfo.Split(',');
                if(array.Length > 0)
                {
                    dpCompany.SelectedValue = array[0];
                    txtFulfillmentNo.Text = array[1];
                    LoadSearch();
                    Session["poinfo"] = null;
                }    
            }
        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();
        }
        private void Reset()
        {
            fupDoc1.Visible = true;
            fupDoc2.Visible = true;
            fupDoc3.Visible = true;
            fupDoc4.Visible = true;
            fupDoc5.Visible = true;

            img1.Visible = false;
            img2.Visible = false;
            img3.Visible = false;
            img4.Visible = false;
            img5.Visible = false;
            lblOrderDate.Text = "";
            lblPONum.Text = "";
            lblStatus.Text = "";
            lblOrderQty.Text = "";
            
            txtFile1Desc.Text = "";
            lnkDocfile1.Text = "";
            //lnkDocfile11.InnerHtml = "";
            //lnkDocfile11.HRef = "";
            //lnkDocfile11.InnerText = "";
            hdnDocID1.Value = "";

            txtFile2Desc.Text = "";
            lnkDocfile2.Text = "";
            hdnDocID2.Value = "";

            txtFile3Desc.Text = "";
            lnkDocfile3.Text = "";
            hdnDocID3.Value = "";

            txtFile4Desc.Text = "";
            lnkDocfile4.Text = "";
            hdnDocID4.Value = "";

            txtFile5Desc.Text = "";
            lnkDocfile5.Text = "";
            //lnkDocfile15.HRef = "";
            //lnkDocfile15.InnerHtml = "";
            //lnkDocfile15.InnerText = "";
            hdnDocID5.Value = "";
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadSearch();
        }
        private void LoadSearch()
        {
            int companyID = 0;
            string fulfillmentNumber = txtFulfillmentNo.Text.Trim();
            string customer = "";

            if (Session["adm"] != null)
            {
                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                ViewState["CompanyName"] = dpCompany.SelectedItem.Text;
                customer = dpCompany.SelectedItem.Text;
            }
            else
            {
                avii.Classes.UserInfo objUserInfo = Session["userInfo"] as avii.Classes.UserInfo;
                companyID = objUserInfo.CompanyGUID;
                ViewState["CompanyName"] = objUserInfo.CompanyName;
                customer = objUserInfo.CompanyName;
            }
            string fileStoreLocation = Server.MapPath("~/langlobal/UploadDocument/PO/" + customer + "/");


            pnlPO.Visible = false;
            pnlUpload.Visible = false;
            lblMsg.Text = "";
            Reset();
            string moduleName = "Fulfillment";
            if (companyID > 0)
            {
               // companyID = Convert.ToInt32(dpCompany.SelectedValue);

                if (!string.IsNullOrEmpty(fulfillmentNumber))
                {
                    SV.Framework.Fulfillment.DocumentFileOperation docOperations = SV.Framework.Fulfillment.DocumentFileOperation.CreateInstance<SV.Framework.Fulfillment.DocumentFileOperation>();

                    List<DocumentFile> documents = docOperations.GetDocuments(companyID, fulfillmentNumber, moduleName);
                    if (documents != null && documents.Count > 0)
                    {
                        pnlPO.Visible = true;
                        pnlUpload.Visible = true;
                        lblOrderDate.Text = documents[0].PODate;
                        lblPONum.Text = documents[0].FulfillmentNumber;
                        lblStatus.Text = documents[0].FulfillmentStatus;
                        lblOrderQty.Text = documents[0].LineItemCount.ToString();
                        ViewState["poid"] = documents[0].PO_ID;

                        if (documents[0].FileUploadID > 0)
                        {
                            fupDoc1.Visible = false;
                            txtFile1Desc.Text = documents[0].FileDescription;
                            lnkDocfile1.Text = documents[0].FileName;
                            //lnkDocfile11.HRef = fileStoreLocation + documents[0].FileName;
                            //lnkDocfile15.InnerHtml = documents[0].FileName;
                            //lnkDocfile11.InnerText = documents[0].FileName;
                            //lnkDocfile11.Attributes.Add("download", fileStoreLocation + documents[0].FileName);
                            //lnkDocfile15.Title = documents[0].FileName;

                            hdnDocID1.Value = documents[0].FileUploadID.ToString();
                            img1.Visible = true;
                        }
                        if (documents.Count > 1)
                        {
                            fupDoc2.Visible = false;
                            txtFile2Desc.Text = documents[1].FileDescription;
                            lnkDocfile2.Text = documents[1].FileName;
                            hdnDocID2.Value = documents[1].FileUploadID.ToString();
                            img2.Visible = true;
                        }
                        if (documents.Count > 2)
                        {
                            fupDoc3.Visible = false;
                            txtFile3Desc.Text = documents[2].FileDescription;
                            lnkDocfile3.Text = documents[2].FileName;
                            hdnDocID3.Value = documents[2].FileUploadID.ToString();
                            img3.Visible = true;
                        }
                        if (documents.Count > 3)
                        {
                            fupDoc4.Visible = false;
                            txtFile4Desc.Text = documents[3].FileDescription;
                            lnkDocfile4.Text = documents[3].FileName;
                            hdnDocID4.Value = documents[3].FileUploadID.ToString();

                            img4.Visible = true;
                        }
                        if (documents.Count > 4)
                        {
                            fupDoc5.Visible = false;
                            txtFile5Desc.Text = documents[4].FileDescription;
                            lnkDocfile5.Text = documents[4].FileName;
                            //lnkDocfile15.HRef = fileStoreLocation + documents[4].FileName;
                            //lnkDocfile15.InnerHtml = documents[4].FileName;
                            //lnkDocfile15.InnerText = documents[4].FileName;
                            img5.Visible = true;
                            hdnDocID5.Value = documents[4].FileUploadID.ToString();
                        }
                    }
                    else
                    {
                        lblMsg.Text = "No record found!";
                    }
                }
                else
                {
                    lblMsg.Text = "Fulfillment number is required!";
                }
            }
            else
            {
                lblMsg.Text = "Customer is required!";
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            Reset();
            pnlPO.Visible = false;
            pnlUpload.Visible = false;
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void btnDocUpload_Click(object sender, EventArgs e)
        {
            SV.Framework.Fulfillment.DocumentFileOperation docOperations = SV.Framework.Fulfillment.DocumentFileOperation.CreateInstance<SV.Framework.Fulfillment.DocumentFileOperation>();

            List<DocumentFile> documentFiles = new List<DocumentFile>();
            DocumentFile request = default;
            int poid = 0, userid = 0;
            //string File1Desc = default;
            string moduleName = "Fulfillment";

            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userid = userInfo.UserGUID;
            }
            if (ViewState["poid"] != null)
                poid = Convert.ToInt32(ViewState["poid"]);

            
            if (fupDoc1.HasFile)
            {
                string extension = Path.GetExtension(fupDoc1.PostedFile.FileName);
                extension = extension.ToLower();
                if (extension == ".pdf" || extension == ".doc" || extension == ".docx" || extension == ".txt" || extension == ".jpeg"|| extension == ".jpg" || extension == ".png")
                {
                    request = new DocumentFile();
                    request.FileName = UploadFile(fupDoc1);
                    if(request.FileName.Contains("File size is greater than"))
                    { return; }
                    request.FileDescription = txtFile1Desc.Text.Trim();
                    documentFiles.Add(request);
                }
                else
                {
                    lblMsg.Text = fupDoc1.PostedFile.FileName + " invalid file!";
                    return;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtFile1Desc.Text) & !string.IsNullOrEmpty(hdnDocID1.Value))
                {
                    request = new DocumentFile();
                    request.FileName = "";
                    request.FileDescription = txtFile1Desc.Text.Trim();
                    request.FileUploadID = Convert.ToInt64(hdnDocID1.Value);
                    documentFiles.Add(request);
                }
            }
            if (fupDoc2.HasFile)
            {
                string extension = Path.GetExtension(fupDoc2.PostedFile.FileName);
                extension = extension.ToLower();
                if (extension == ".pdf" || extension == ".doc" || extension == ".docx" || extension == ".txt" || extension == ".jpeg" || extension == ".jpg" || extension == ".png")
                {
                    request = new DocumentFile();
                    request.FileName = UploadFile(fupDoc2);
                    if (request.FileName.Contains("File size is greater than"))
                    { return; }

                    request.FileDescription = txtFile2Desc.Text.Trim();
                    documentFiles.Add(request);
                }
                else
                {
                    lblMsg.Text = lblMsg.Text +" " + fupDoc2.PostedFile.FileName + " invalid file!";
                    return;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtFile2Desc.Text.Trim()) & !string.IsNullOrEmpty(hdnDocID2.Value))
                {
                    request = new DocumentFile();
                    request.FileName = "";
                    request.FileDescription = txtFile2Desc.Text.Trim();
                    request.FileUploadID = Convert.ToInt64(hdnDocID2.Value);
                    documentFiles.Add(request);
                }
            }
            if (fupDoc3.HasFile)
            {
                string extension = Path.GetExtension(fupDoc3.PostedFile.FileName);
                extension = extension.ToLower();
                if (extension == ".pdf" || extension == ".doc" || extension == ".docx" || extension == ".txt" || extension == ".jpeg" || extension == ".jpg" || extension == ".png")
                {
                    request = new DocumentFile();
                    request.FileName = UploadFile(fupDoc3);
                    if (request.FileName.Contains("File size is greater than"))
                    { return; }

                    request.FileDescription = txtFile3Desc.Text.Trim();
                    documentFiles.Add(request);
                }
                else
                {
                    lblMsg.Text = lblMsg.Text + " " + fupDoc3.PostedFile.FileName + " invalid file!";
                    return;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtFile3Desc.Text.Trim()) & !string.IsNullOrEmpty(hdnDocID3.Value))
                {
                    request = new DocumentFile();
                    request.FileName = "";
                    request.FileDescription = txtFile3Desc.Text.Trim();
                    request.FileUploadID = Convert.ToInt64(hdnDocID3.Value);
                    documentFiles.Add(request);
                }
            }
            if (fupDoc4.HasFile)
            {
                string extension = Path.GetExtension(fupDoc4.PostedFile.FileName);
                extension = extension.ToLower();
                if (extension == ".pdf" || extension == ".doc" || extension == ".docx" || extension == ".txt" || extension == ".jpeg" || extension == ".jpg" || extension == ".png")
                {
                    request = new DocumentFile();
                    request.FileName = UploadFile(fupDoc4);
                    if (request.FileName.Contains("File size is greater than"))
                    { return; }

                    request.FileDescription = txtFile4Desc.Text.Trim();
                    documentFiles.Add(request);
                }
                else
                {
                    lblMsg.Text = lblMsg.Text + " " + fupDoc4.PostedFile.FileName + " invalid file!";
                    return;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtFile4Desc.Text.Trim()) & !string.IsNullOrEmpty(hdnDocID4.Value))
                {
                    request = new DocumentFile();
                    request.FileName = "";
                    request.FileDescription = txtFile4Desc.Text.Trim();
                    request.FileUploadID = Convert.ToInt64(hdnDocID4.Value);
                    documentFiles.Add(request);
                }
            }
            if (fupDoc5.HasFile)
            {
                string extension = Path.GetExtension(fupDoc5.PostedFile.FileName);
                extension = extension.ToLower();
                if (extension == ".pdf" || extension == ".doc" || extension == ".docx" || extension == ".txt" || extension == ".jpeg" || extension == ".jpg" || extension == ".png")
                {
                    request = new DocumentFile();
                    request.FileName = UploadFile(fupDoc5);
                    if (request.FileName.Contains("File size is greater than"))
                    { return; }

                    request.FileDescription = txtFile5Desc.Text.Trim();
                    documentFiles.Add(request);
                }
                else
                {
                    lblMsg.Text = lblMsg.Text + " " + fupDoc5.PostedFile.FileName + " invalid file!";
                    return;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtFile5Desc.Text.Trim()) & !string.IsNullOrEmpty(hdnDocID5.Value))
                {
                    request = new DocumentFile();
                    request.FileName = "";
                    request.FileDescription = txtFile5Desc.Text.Trim();
                    request.FileUploadID = Convert.ToInt64(hdnDocID5.Value);
                    documentFiles.Add(request);
                }
            }
            if (documentFiles != null && documentFiles.Count > 0)
            {
                int returnResult = docOperations.DocumentInsert(documentFiles, moduleName, Convert.ToInt64(poid), userid);
                if (returnResult > 0)
                {
                    LoadSearch();
                    lblMsg.Text = "Uploaded successfully";

                }
                else
                {

                }
            }
            else
            {
                if (string.IsNullOrEmpty(lblMsg.Text))
                    lblMsg.Text = "There is no file to upload!";
            }

        }
        private string UploadFile(FileUpload fu)
        {
            string fileStoreLocation = "";
            string actualFilename = "";
            Int32 maxFileSize = 1048576;
            actualFilename = System.IO.Path.GetFileName(fu.PostedFile.FileName);
            //if (ConfigurationManager.AppSettings["PurchaseOrderFilesStoreage"] != null)
            //{
            //    fileStoreLocation = ConfigurationManager.AppSettings["PurchaseOrderFilesStoreage"].ToString();
            //}
            string customerName = Convert.ToString(ViewState["CompanyName"]);

            fileStoreLocation = Server.MapPath("~/langlobal/UploadDocument/PO/" + customerName + "/");
            if (File.Exists(fileStoreLocation + actualFilename))
            {
                actualFilename = System.Guid.NewGuid().ToString() + actualFilename;
            }

            if(!System.IO.Directory.Exists(fileStoreLocation))
                System.IO.Directory.CreateDirectory(fileStoreLocation);

            fu.PostedFile.SaveAs(fileStoreLocation + actualFilename);

            FileInfo fileInfo = new FileInfo(fileStoreLocation + actualFilename);

            //if (ConfigurationManager.AppSettings["maxCSVfilesize"] != null)
            {
               // if (Int32.TryParse(ConfigurationManager.AppSettings["maxCSVfilesize"].ToString(), out maxFileSize))
                {
                    if (fileInfo.Length > maxFileSize)
                    {
                        fileInfo.Delete();
                        lblMsg.Text = "File size is greater than 1 MB";
                        return "File size is greater than 1 MB";

                        //throw new Exception("File size is greater than " + maxFileSize + " bytes");
                    }
                }
            }
            return  actualFilename;
        }


        protected void btnUloadCancel_Click(object sender, EventArgs e)
        {

        }

        protected void lnkDocfile1_Click(object sender, EventArgs e)
        {
            string customerName = Convert.ToString(ViewState["CompanyName"]);
            Int64 FileUploadID = 0;
            string fileStoreLocation = Server.MapPath("~/langlobal/UploadDocument/PO/" + customerName + "/")+lnkDocfile1.Text;

            SV.Framework.Fulfillment.DocumentFileOperation docOperations = SV.Framework.Fulfillment.DocumentFileOperation.CreateInstance<SV.Framework.Fulfillment.DocumentFileOperation>();
            if (!string.IsNullOrEmpty(hdnDocID1.Value))
            {
                FileUploadID = Convert.ToInt64(hdnDocID1.Value);

                int returnResult = docOperations.DocumentDelete(FileUploadID);
                
                if (returnResult > 0)
                {
                    if (File.Exists(fileStoreLocation))
                        File.Delete(fileStoreLocation);

                    LoadSearch();
                    lblMsg.Text = "Deleted successfully";
                }
                else
                {

                }
            }
        }
        protected void lnkDocfile2_Click(object sender, EventArgs e)
        {
            Int64 FileUploadID = 0;
            string customerName = Convert.ToString(ViewState["CompanyName"]);

            SV.Framework.Fulfillment.DocumentFileOperation docOperations = SV.Framework.Fulfillment.DocumentFileOperation.CreateInstance<SV.Framework.Fulfillment.DocumentFileOperation>();
            string fileStoreLocation = Server.MapPath("~/langlobal/UploadDocument/PO/" + customerName + "/") + lnkDocfile2.Text;

            if (!string.IsNullOrEmpty(hdnDocID2.Value))
            {
                FileUploadID = Convert.ToInt64(hdnDocID2.Value);
                int returnResult = docOperations.DocumentDelete(FileUploadID);
                if (returnResult > 0)
                {
                    if (File.Exists(fileStoreLocation))
                        File.Delete(fileStoreLocation);

                    LoadSearch();
                    lblMsg.Text = "Deleted successfully";
                }
                else
                {

                }
            }
        }
        protected void lnkDocfile3_Click(object sender, EventArgs e)
        {
            Int64 FileUploadID = 0;
            string customerName = Convert.ToString(ViewState["CompanyName"]);

            SV.Framework.Fulfillment.DocumentFileOperation docOperations = SV.Framework.Fulfillment.DocumentFileOperation.CreateInstance<SV.Framework.Fulfillment.DocumentFileOperation>();
            string fileStoreLocation = Server.MapPath("~/langlobal/UploadDocument/PO/" + customerName + "/") + lnkDocfile3.Text;

            if (!string.IsNullOrEmpty(hdnDocID3.Value))
            {
                FileUploadID = Convert.ToInt64(hdnDocID3.Value);
                int returnResult = docOperations.DocumentDelete(FileUploadID);
                if (returnResult > 0)
                {
                    if (File.Exists(fileStoreLocation))
                        File.Delete(fileStoreLocation);

                    LoadSearch();
                    lblMsg.Text = "Deleted successfully";
                }
                else
                {

                }
            }
        }

        protected void lnkDocfile4_Click(object sender, EventArgs e)
        {
            Int64 FileUploadID = 0;
            string customerName = Convert.ToString(ViewState["CompanyName"]);

            SV.Framework.Fulfillment.DocumentFileOperation docOperations = SV.Framework.Fulfillment.DocumentFileOperation.CreateInstance<SV.Framework.Fulfillment.DocumentFileOperation>();
            string fileStoreLocation = Server.MapPath("~/langlobal/UploadDocument/PO/" + customerName + "/") + lnkDocfile4.Text;

            if (!string.IsNullOrEmpty(hdnDocID4.Value))
            {
                FileUploadID = Convert.ToInt64(hdnDocID4.Value);
                int returnResult = docOperations.DocumentDelete(FileUploadID);
                if (returnResult > 0)
                {
                    if (File.Exists(fileStoreLocation))
                        File.Delete(fileStoreLocation);

                    LoadSearch();
                    lblMsg.Text = "Deleted successfully";
                }
                else
                {

                }
            }
        }

        protected void lnkDocfile5_Click(object sender, EventArgs e)
        {
            //pdf, DOC, docx, TXT, JPEG, PNG
            Int64 FileUploadID = 0;
            string customerName = Convert.ToString(ViewState["CompanyName"]);

            SV.Framework.Fulfillment.DocumentFileOperation docOperations = SV.Framework.Fulfillment.DocumentFileOperation.CreateInstance<SV.Framework.Fulfillment.DocumentFileOperation>();
            string fileStoreLocation = Server.MapPath("~/langlobal/UploadDocument/PO/" + customerName + "/") + lnkDocfile5.Text;

            if (!string.IsNullOrEmpty(hdnDocID5.Value))
            {
                FileUploadID = Convert.ToInt64(hdnDocID5.Value);
                int returnResult = docOperations.DocumentDelete(FileUploadID);
                if (returnResult > 0)
                {
                    if (File.Exists(fileStoreLocation))
                        File.Delete(fileStoreLocation);

                    LoadSearch();
                    lblMsg.Text = "Deleted successfully";
                }
                else
                {

                }
            }
        }

        protected void lnkDownload1_Click(object sender, EventArgs e)
        {
            string customerName = Convert.ToString(ViewState["CompanyName"]);

            SV.Framework.Fulfillment.DocumentFileOperation docOperations = SV.Framework.Fulfillment.DocumentFileOperation.CreateInstance<SV.Framework.Fulfillment.DocumentFileOperation>();
            string filePath = Server.MapPath("~/langlobal/UploadDocument/PO/" + customerName + "/") + lnkDocfile1.Text;
            string extension = Path.GetExtension(filePath);
            extension = extension.ToLower();
           // if (".pdf" == extension)
             //   extension = "octet-stream";
            string contentType = "";

            if (".pdf" == extension)
                contentType = "application/pdf";
            if (".doc" == extension)
                contentType = "application/msword";
            if (".docx" == extension)
                contentType = "application/msword";
            if (".jpg" == extension)
                contentType = "application/octet-stream";
            if (".jpeg" == extension)
                contentType = "application/octet-stream";
            if (".png" == extension)
                contentType = "image/png";


            if (".txt" == extension)
                contentType = "text/plain";

            string strContents = null;
            System.IO.StreamReader objReader = default(System.IO.StreamReader);
            objReader = new System.IO.StreamReader(filePath);
            strContents = objReader.ReadToEnd();
            objReader.Close();

            string attachment = "attachment; filename=" + lnkDocfile1.Text;
            Response.ClearContent();

            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + lnkDocfile1.Text);
            Response.TransmitFile(filePath);
            Response.End();

            //Response.ContentType = "application/" + extension;
            //Response.AddHeader("content-disposition", attachment);
            //Response.Write(strContents);
            //Response.End();
        }

        protected void lnkDownload2_Click(object sender, EventArgs e)
        {
            string customerName = Convert.ToString(ViewState["CompanyName"]);
            
            SV.Framework.Fulfillment.DocumentFileOperation docOperations = SV.Framework.Fulfillment.DocumentFileOperation.CreateInstance<SV.Framework.Fulfillment.DocumentFileOperation>();
            string filePath = Server.MapPath("~/langlobal/UploadDocument/PO/" + customerName + "/") + lnkDocfile2.Text;
            string extension = Path.GetExtension(filePath);
            extension = extension.ToLower();
            //if (".pdf" == extension)
            //    extension = "octet-stream";
            string contentType = "";

            if (".pdf" == extension)
                contentType = "application/pdf";
            if (".doc" == extension)
                contentType = "application/msword";
            if (".docx" == extension)
                contentType = "application/msword";
            if (".jpg" == extension)
                contentType = "application/octet-stream";
            if (".jpeg" == extension)
                contentType = "application/octet-stream";
            if (".png" == extension)
                contentType = "image/png";


            if (".txt" == extension)
                contentType = "text/plain";

            string strContents = null;
            System.IO.StreamReader objReader = default(System.IO.StreamReader);
            objReader = new System.IO.StreamReader(filePath);
            strContents = objReader.ReadToEnd();
            objReader.Close();

            string attachment = "attachment; filename=" + lnkDocfile2.Text;
            Response.ClearContent();
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + lnkDocfile2.Text);
            Response.TransmitFile(filePath);
            Response.End();

            //Response.ContentType = "application/"+ extension;
            //Response.AddHeader("content-disposition", attachment);
            //Response.Write(strContents);
            //Response.End();
        }
        protected void lnkDownload3_Click(object sender, EventArgs e)
        {
            string customerName = Convert.ToString(ViewState["CompanyName"]);

            SV.Framework.Fulfillment.DocumentFileOperation docOperations = SV.Framework.Fulfillment.DocumentFileOperation.CreateInstance<SV.Framework.Fulfillment.DocumentFileOperation>();
            string filePath = Server.MapPath("~/langlobal/UploadDocument/PO/" + customerName + "/") + lnkDocfile3.Text;
            string extension = Path.GetExtension(filePath);
            extension = extension.ToLower();
           // if (".pdf" == extension)
             //   extension = "octet-stream";
            string contentType = "";

            if (".pdf" == extension)
                contentType = "application/pdf";
            if (".doc" == extension)
                contentType = "application/msword";
            if (".docx" == extension)
                contentType = "application/msword";
            if (".jpg" == extension)
                contentType = "application/octet-stream";
            if (".jpeg" == extension)
                contentType = "application/octet-stream";
            if (".png" == extension)
                contentType = "image/png";


            if (".txt" == extension)
                contentType = "text/plain";

            string strContents = null;
            System.IO.StreamReader objReader = default(System.IO.StreamReader);
            objReader = new System.IO.StreamReader(filePath);
            strContents = objReader.ReadToEnd();
            objReader.Close();

            string attachment = "attachment; filename=" + lnkDocfile3.Text;
            Response.ClearContent();
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + lnkDocfile3.Text);
            Response.TransmitFile(filePath);
            Response.End();


            //Response.ContentType = "application/" + extension;
            //Response.AddHeader("content-disposition", attachment);
            //Response.Write(strContents);
            //Response.End();
        }

        protected void lnkDownload4_Click(object sender, EventArgs e)
        {
            string customerName = Convert.ToString(ViewState["CompanyName"]);

            SV.Framework.Fulfillment.DocumentFileOperation docOperations = SV.Framework.Fulfillment.DocumentFileOperation.CreateInstance<SV.Framework.Fulfillment.DocumentFileOperation>();
            string filePath = Server.MapPath("~/langlobal/UploadDocument/PO/" + customerName + "/") + lnkDocfile4.Text;
            string extension = Path.GetExtension(filePath);
            extension = extension.ToLower();
           // if (".pdf" == extension)
             //   extension = "octet-stream";
            string contentType = "";

            if (".pdf" == extension)
                contentType = "application/pdf";
            if (".doc" == extension)
                contentType = "application/msword";
            if (".docx" == extension)
                contentType = "application/msword";
            if (".jpg" == extension)
                contentType = "application/octet-stream";
            if (".jpeg" == extension)
                contentType = "application/octet-stream";
            if (".png" == extension)
                contentType = "image/png";


            if (".txt" == extension)
                contentType = "text/plain";

            string strContents = null;
            System.IO.StreamReader objReader = default(System.IO.StreamReader);
            objReader = new System.IO.StreamReader(filePath);
            strContents = objReader.ReadToEnd();
            objReader.Close();

            string attachment = "attachment; filename=" + lnkDocfile4.Text;
            Response.ClearContent();
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + lnkDocfile4.Text);
            Response.TransmitFile(filePath);
            Response.End();


            //Response.ContentType = "application/" + extension;
            //Response.AddHeader("content-disposition", attachment);
            //Response.Write(strContents);
            //Response.End();
        }

        protected void lnkDownload5_Click(object sender, EventArgs e)
        {
            string customerName = Convert.ToString(ViewState["CompanyName"]);

            SV.Framework.Fulfillment.DocumentFileOperation docOperations = SV.Framework.Fulfillment.DocumentFileOperation.CreateInstance<SV.Framework.Fulfillment.DocumentFileOperation>();
            string filePath = Server.MapPath("~/langlobal/UploadDocument/PO/" + customerName + "/") + lnkDocfile5.Text;
            string extension = Path.GetExtension(filePath);
            extension = extension.ToLower();
            string contentType = "";

            if (".pdf" == extension)
                contentType = "application/pdf";
            if (".doc" == extension)
                contentType = "application/msword";
            if (".docx" == extension)
                contentType = "application/msword";
            if (".jpg" == extension)
                contentType = "application/octet-stream";
            if (".jpeg" == extension)
                contentType = "application/octet-stream";
            if (".png" == extension)
                contentType = "image/png";


            if (".txt" == extension)
                contentType = "text/plain";

            string strContents = null;
            System.IO.StreamReader objReader = default(System.IO.StreamReader);
            objReader = new System.IO.StreamReader(filePath);
            strContents = objReader.ReadToEnd();
            objReader.Close();

            string attachment = "attachment; filename=" + lnkDocfile5.Text;
            Response.ClearContent();
            //Response.ContentType = "application/" + extension;
            //Response.AddHeader("content-disposition", attachment);
            //Response.Write(strContents);
            //Response.End();

            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename="+ lnkDocfile5.Text);
            Response.TransmitFile(filePath);
            Response.End();

        }


    }
}