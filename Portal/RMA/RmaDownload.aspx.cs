using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.RMA
{
    public partial class RmaDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["rmaguid"] != null)
                {
                    int rmaGUID = Convert.ToInt32(Request["rmaguid"]);
                    ViewState["rmaguid"] = rmaGUID;
                    BindRmaDocuments(rmaGUID);
                }
            }
        }
        protected void rptRmaDoc_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton imgDel = (ImageButton)e.Item.FindControl("imgDel");
                if (Session["adm"] == null)
                {
                    imgDel.Visible = false;
                
                }
            }
        }
        private void BindRmaDocuments(int rmaGuid)
        {
            DataSet rmaDocList = RMAUtility.GetRmaDocumentLists(rmaGuid);
            if (rmaDocList != null && rmaDocList.Tables.Count > 0)
            {
                if (rmaDocList.Tables[0] != null && rmaDocList.Tables[0].Rows.Count > 0)
                {
                    rptRMADoc.DataSource = rmaDocList.Tables[0];
                    rptRMADoc.DataBind();
                    lblDoc.Text = string.Empty;
                }
                else
                {
                    rptRMADoc.DataSource = null;
                    rptRMADoc.DataBind();
                    lblDoc.Text = "No records found";
                
                }
                if (rmaDocList.Tables[1] != null && rmaDocList.Tables[1].Rows.Count > 0)
                {
                    rptAdminRma.DataSource = rmaDocList.Tables[1];
                    rptAdminRma.DataBind();
                    lblMsgAdm.Text = string.Empty;
                }
                else
                {
                    rptAdminRma.DataSource = null;
                    rptAdminRma.DataBind();
                    lblMsgAdm.Text = "No records found";

                }




            }
            else
            {
                rptRMADoc.DataSource = null;
                rptRMADoc.DataBind();
                lblDoc.Text = "No records found";
                rptAdminRma.DataSource = null;
                rptAdminRma.DataBind();
                
                lblMsgAdm.Text = "No records found";
            }
            
        }

        protected void imgDeleteRMADoc_OnCommand(object sender, CommandEventArgs e)
        {
            int rmaDocID = Convert.ToInt32(e.CommandArgument);
            int rmaGUID = Convert.ToInt32(ViewState["rmaguid"]);

            int userID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                //ViewState["companyID"] = userInfo.CompanyGUID;
                userID = userInfo.UserGUID;
            }
            try
            {
                RMAUtility.Delete_RMA_Document(rmaGUID, rmaDocID, userID);
                BindRmaDocuments(rmaGUID);
                lblDoc.Text = "Deleted successfully";
            }
            catch (Exception ex)
            {
                lblDoc.Text = ex.Message.ToString();
            }
            
        }
        protected void DownloadRmaDoc_Click(object sender, CommandEventArgs e)
        {

            string fileName = e.CommandArgument.ToString();
            //try
            //{
            //    //string strURL = "~/documents/rma/Rolesdescription.docx";
            //    string strURL = "Rolesdescription.docx";
            //    WebClient req = new WebClient();
            //    HttpResponse response = HttpContext.Current.Response;
            //    response.Clear();
            //    response.ClearContent();
            //    response.ClearHeaders();
            //    response.Buffer = true;
            //    response.AddHeader("Content-Disposition", "attachment;filename=\"" + strURL + "\"");
            //    byte[] data = req.DownloadData(Server.MapPath("~/Rolesdescription.docx"));
            //    response.BinaryWrite(data);
            //    response.End();
            //}
            //catch (Exception ex)
            //{
            //}

            //System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath("~/Documents/RMA/RMA.xls"));

            //Response.Clear();
            //Response.Buffer = true;
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AddHeader("Content-Disposition", "attachment;filename=RMA.xls");
            ////HttpContext.Current.Response.Write(sw.ToString());
            //Response.WriteFile(fi.FullName);
            //HttpContext.Current.Response.End();

            System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath("~/Documents/RMA/" + fileName));

            string ext = fi.Extension.ToString();
            ext = ext.ToLower();


            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fi.Name));
            Response.AddHeader("Content-Length", fi.Length.ToString());
            //Response.ContentType = "application/vnd.ms-excel";
            if (ext.ToString() == ".docx")
                Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            else
                if (ext == ".doc")
                    Response.ContentType = "application/ms-word";
            else
                if (ext == ".xls")
                    Response.ContentType = "application/vnd.ms-excel";
            else
                if (ext == ".xlsx")
                    Response.ContentType = "application/vnd.ms-excel";
            else
                if (ext == ".pdf")
                    Response.ContentType = "application/pdf";
            else
                if (ext == ".jpg")
                    Response.ContentType = "image/jpeg";




            //Response.ContentType = "application/octet-stream";
            Response.WriteFile(fi.FullName);
            Response.End();
        }
        


    }
}