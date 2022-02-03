using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using avii.Classes;
namespace Sanvitti1.RMA
{
    public partial class ESNImageUpload : System.Web.UI.Page
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
                hdnSize.Value = System.Configuration.ConfigurationSettings.AppSettings["maxfilesize"].ToString();
                int rmaDetGUID = 0;




                if (Request["rmaDetGUID"] != null)
                    int.TryParse(Request["rmaDetGUID"].ToString(), out rmaDetGUID);
                ViewState["rmaDetGUID"] = rmaDetGUID;
                GetRMADetail(rmaDetGUID);
                BindESNImages(rmaDetGUID);
            }
        }
        private void GetRMADetail(int rmaDetGUID)
        {
            int rmaGUID = 0;
            if (Request["rmaGUID"] != null)
                int.TryParse(Request["rmaGUID"].ToString(), out rmaGUID);
            List<avii.Classes.RMA> objRmaList = null;
            if (Session["result"] != null)
            {
                objRmaList = Session["result"] as List<avii.Classes.RMA>;

                if (objRmaList != null)
                {
                    var rmaInfoList = (from item in objRmaList where item.RmaGUID.Equals(rmaGUID) select item).ToList();
                    if (rmaInfoList != null && rmaInfoList.Count > 0)
                    {
                        lblRMA.Text = rmaInfoList[0].RmaNumber;
                        lblRMAStatus.Text = rmaInfoList[0].Status;
                        lblCompanyName.Text = rmaInfoList[0].RMAUserCompany.CompanyName;
                        lblRMADate.Text = Convert.ToDateTime(rmaInfoList[0].RmaDate).ToShortDateString();



                    }

                }
            }
            if (HttpContext.Current.Session["rmadetail"] != null)
            {
                DataTable objDataTable = (DataTable)HttpContext.Current.Session["rmadetail"];
                DataRow[] rows = objDataTable.Select(string.Format("rmaDetGUID='{0}' ", rmaDetGUID));

                if (rows.Length > 0)
                {
                    foreach (DataRow dataRow in rows)
                    {

                        lblESN.Text = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                        lblESNStatus.Text = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                        Hashtable reasonHashtable = RMAUtility.getReasonHashtable();

                       // lblreason.Text = reasonHashtable[hdnReason.Value].ToString();

                        lblReason.Text = reasonHashtable[Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Reason", 0, false)).ToString()].ToString();

                        lblSKU.Text = clsGeneral.getColumnData(dataRow, "itemcode", string.Empty, false) as string;


                    }
                }
            }
        
        }
        private void BindESNImages(int rmaDetGUID)
        {
            lblPic.Text = string.Empty;
            try
            {
                List<ESNImage> esnImageList = RMAUtility.GetESNImageList(rmaDetGUID);
                if (esnImageList != null && esnImageList.Count > 0)
                {

                    rptRMA.DataSource = esnImageList;
                    pnlDoc.Visible = true;
                }
                else
                {
                    rptRMA.DataSource = null;
                    pnlDoc.Visible = false;
                }
                rptRMA.DataBind();

            }
            catch (Exception ex)
            {
                lblPic.Text = ex.Message;
            }
        
        }
        protected void btnPicture_Click(object sender, EventArgs e)
        {
            hdnSize.Value = System.Configuration.ConfigurationSettings.AppSettings["maxfilesize"].ToString();
            int rmaDetGUID, pictureID, userID;
            rmaDetGUID = pictureID = userID = 0;
            string extension, fileName, filePath;
            string targetFolder = Server.MapPath("~");
            int randomNo = GetRandomNumber(1, 99999);
            lblPic.Text = string.Empty;
            fileName = extension = filePath = string.Empty;
            targetFolder = targetFolder + "\\Documents\\ESN\\";
            if (!Directory.Exists(Path.GetFullPath(targetFolder)))
            {
                Directory.CreateDirectory(Path.GetFullPath(targetFolder));
            }
            int.TryParse(Session["UserID"].ToString(), out userID);

            int.TryParse(ViewState["rmaDetGUID"].ToString(), out rmaDetGUID);
            //int.TryParse(ViewState["pictureID"].ToString(), out pictureID);
            if (fuESNPic.HasFile)
            {
                extension = Path.GetExtension(fuESNPic.PostedFile.FileName);

                fileName = "ESN" + randomNo.ToString() + rmaDetGUID.ToString() + extension;
                filePath = targetFolder + fileName;
                extension = extension.ToLower();
                if (extension == ".pdf" || extension == ".jpg" || extension == ".gif" || extension == ".png" || extension == ".jpeg" || extension == ".bmp" || extension == ".doc" || extension == ".docx")
                {
                    //if (!File.Exists(Path.GetFullPath(filePath)))
                    {

                        fuESNPic.PostedFile.SaveAs(filePath);

                        RMAUtility.RMA_Detail_Picture_InsertUpdate(pictureID, rmaDetGUID, fileName, userID);
                        //grid_bind(true);
                        BindESNImages(rmaDetGUID);
                        lblPic.Text = "Uploaded successfully";

                    }

                }
                else
                    lblPic.Text = "Invalid extension!";

            }
            else
                lblPic.Text = "Please select a file";


        }
        protected void imgDeleteImage_OnCommand(object sender, CommandEventArgs e)
        {

            int pictureID = Convert.ToInt32(e.CommandArgument);
            int rmaDetGUID = 0;
            lblPic.Text = string.Empty;
            
            try
            {
                int.TryParse(ViewState["rmaDetGUID"].ToString(), out rmaDetGUID);
            
                RMAUtility.RMA_Detail_Picture_Delete(pictureID);
                BindESNImages(rmaDetGUID);
                lblPic.Text = "Deleted successfully";

            }
            catch (Exception ex)
            {
                lblPic.Text = ex.Message.ToString();
            }

        }
        protected void btnCencel_Click(object sender, EventArgs e)
        {
            lblPic.Text = string.Empty;
            
        }
        //Function to get random number
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        private static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }
        protected void rptRMA_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnlImage = (LinkButton)e.Item.FindControl("lnlImage");
                if (lnlImage != null)
                {
                    if (lnlImage.Text.IndexOf("pdf") > -1 || lnlImage.Text.IndexOf("doc") > -1 )
                    {
                        var scriptManager = ScriptManager.GetCurrent(this.Page);
                        if (scriptManager != null)
                        {
                            scriptManager.RegisterPostBackControl(lnlImage);
                        }
                    }
                    else
                        lnlImage.OnClientClick = "openPictureDialogAndBlock('" + lnlImage.Text + "', '" + lnlImage.ClientID + "')";

                }
                
            }
        }
        protected void DownloadRmaDoc_Click(object sender, CommandEventArgs e)
        {

            string fileName = e.CommandArgument.ToString();
            
            System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath("~/Documents/ESN/" + fileName));

            string ss = fi.GetType().ToString();
            string ext = fi.Extension.ToString();
            ext = ext.ToLower();

            if (ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".png" || ext == ".bmp")
            {
                imgESNPic.ImageUrl = "~/Documents/ESN/" + fileName;
                RegisterStartupScript("jsUnblockDialog", "unblockPictureDialog();");
            }
            else
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fi.Name));
                Response.AddHeader("Content-Length", fi.Length.ToString());
                //Response.ContentType = "application/vnd.ms-excel";
                if (ext.ToString() == ".docx")
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    Response.WriteFile(fi.FullName);
                    Response.End();
                }
                else
                    if (ext == ".doc")
                    {
                        Response.ContentType = "application/ms-word";
                        Response.WriteFile(fi.FullName);
                        Response.End();
                    }
                    else
                        if (ext == ".xls")
                        {
                            Response.ContentType = "application/vnd.ms-excel";
                            Response.WriteFile(fi.FullName);
                            Response.End();
                        }
                        else
                            if (ext == ".xlsx")
                            {
                                Response.ContentType = "application/vnd.ms-excel";
                                Response.WriteFile(fi.FullName);
                                Response.End();
                            }
                            else
                                if (ext == ".pdf")
                                {
                                    Response.ContentType = "application/pdf";
                                    Response.WriteFile(fi.FullName);
                                    Response.End();
                                }
                //else
                //    if (ext == ".jpg" || ext != ".jpg")
                //    {
                //        Response.End();
                //        //Server.MapPath("~/Documents/ESN/" + fileName)
                //        imgESNPic.ImageUrl="~/Documents/ESN/" + fileName;
                //        RegisterStartupScript("jsUnblockDialog", "unblockPictureDialog();");
                //    }
                //  Response.ContentType = "image/jpeg";




                //Response.ContentType = "application/octet-stream";
                //Response.WriteFile(fi.FullName);
                //Response.End();
            }
        }

    }
}