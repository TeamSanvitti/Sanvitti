using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
//using avii.Classes;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Reflection;
using SV.Framework.Models.Common;
using SV.Framework.Models.RMA;
using SV.Framework.Models.Fulfillment;

namespace avii.Admin.RMA
{
    public partial class RMAQueryNew : System.Web.UI.Page
    {

        bool rebind = false;
       
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
                if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
          
            rebind = false;
            if (!IsPostBack)
            {
                int userid=0;
                rebind = false;
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

                int companyID = 0;
                if (Session["adm"] == null)
                    companyID = userInfo.CompanyGUID;
                BindRMAStatuses(companyID);

                if (Session["userInfo"] != null)
                {
                   // avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    if (userInfo != null && userInfo.UserGUID > 0)
                    {
                        List<avii.Classes.UserRole> roleList = userInfo.ActiveRoles;
                        var allowcancellation = (from item in roleList where item.RoleName.ToLower().Equals("allowcancellation") select item).ToList();
                        if (allowcancellation != null && allowcancellation.Count > 0 && !string.IsNullOrEmpty(allowcancellation[0].RoleName))
                        {
                            ViewState["allowcancellation"] = allowcancellation[0].RoleName;
                        }
                    }
                }
                if (Session["adm"] == null)
                {
                    userid = Convert.ToInt32(Session["UserID"]);
                    BindUserStores(userid, 0);
                }
                ViewState["userid"]=userid;
                if (userid>0)
                {
                   
                    hdnUserID.Value = userid.ToString();
                    companyPanel.Visible = false;
                    lblComapny.Visible = false;
                }
                else
                {
                    companyPanel.Visible = true;
                    lblComapny.Visible = true;
                    bindCompany();
                }

                if (Request["search"] != null && Request["search"] != "")
                {
                    rebind = false;

                    bindsearchRMA();
                }
                BindRMA(userid);
                ddReason.DataSource = rmaUtility.getReasonHashtable();
                ddReason.DataTextField = "value";
                ddReason.DataValueField = "key";
                ddReason.DataBind();
                ddReason.Items.Insert(0, new System.Web.UI.WebControls.ListItem(string.Empty));
            }
            
        }

        private void ReadOlnyAccess()
        {
            //string url = HttpContext.Current.Request.Url.AbsoluteUri;
            // http://localhost:1302/TESTERS/Default6.aspx

            string path = HttpContext.Current.Request.Url.AbsolutePath;
            // /TESTERS/Default6.aspx
            if (path != null)
            {
                path = path.ToLower();
                List<avii.Classes.MenuItem> menuItems = Session["MenuItems"] as List<avii.Classes.MenuItem>;
                foreach (avii.Classes.MenuItem item in menuItems)
                {
                    if (item.Url.ToLower().Contains(path) && item.IsReadOnly)
                    {
                        ViewState["IsReadOnly"] = true;

                    }
                }

            }

        }

        protected void imgCComments_OnCommand(object sender, CommandEventArgs e)
        {
            try
            {
                Control tmp2 = LoadControl("~/controls/RMACommunication.ascx");
                avii.Controls.RMACommunication ctlRMAComment = tmp2 as avii.Controls.RMACommunication;
                pnlComments.Controls.Clear();
                string info = e.CommandArgument.ToString();
                string[] arr = info.Split(',');
                if (arr.Length > 1)
                {
                    int rmaguid = Convert.ToInt32(arr[0]);
                    string rmaNumber = arr[1];
                    lblRNum.Text = rmaNumber;
                    if (tmp2 != null)
                    {

                        ctlRMAComment.BindRMAComments(rmaguid);
                    }
                    pnlComments.Controls.Add(ctlRMAComment);

                    RegisterStartupScript("jsUnblockDialog", "unblockCommentsDialog();");
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

        public Byte[] GetFileContent(System.IO.Stream inputstm)
        {

            Stream fs = inputstm;

            BinaryReader br = new BinaryReader(fs);

            Int32 lnt = Convert.ToInt32(fs.Length);

            byte[] bytes = br.ReadBytes(lnt);

            return bytes;

        }

        private void SavePhotoContent()
        {

            HttpPostedFile postFile;

            string ImageName = string.Empty;

            byte[] path;

            string[] keys;

            try
            {
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
                int.TryParse(ViewState["pictureID"].ToString(), out pictureID);
            
                string contentType = string.Empty;

                byte[] imgContent = null;

                string[] PhotoTitle;

                string PhotoTitlenme;

                HttpFileCollection files = Request.Files;

                keys = files.AllKeys;

                for (int i = 0; i < files.Count; i++)
                {

                    postFile = files[i];

                    if (postFile.ContentLength > 0)
                    {

                        // postFile.SaveAs(Server.MapPath(“Uploads”) + “\\” + System.IO.Path.GetFileName(postFile.FileName));

                        contentType = postFile.ContentType;

                        path = GetFileContent(postFile.InputStream);

                        ImageName = System.IO.Path.GetFileName(postFile.FileName);
                        fileName = ImageName;
                        PhotoTitle = ImageName.Split('.');

                        PhotoTitlenme = PhotoTitle[0];
                        extension = PhotoTitle[1];
                        fileName = "ESN" + randomNo.ToString() + rmaDetGUID.ToString() + extension;
                        filePath = targetFolder + fileName;
                        

                    }

                }

            }

            catch (Exception ex)
            {

                ex.Message.ToString();

            }

        }

        protected void btnAddPhoto_Click1(object sender, EventArgs e)
        {

            SavePhotoContent();

        }

        private void BindShipBy()
        {
            try
            {
               SV.Framework.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.Fulfillment.PurchaseOrder.CreateInstance<SV.Framework.Fulfillment.PurchaseOrder>();

                List<ShipBy> shipBy = purchaseOrderOperation.GetShipByList();
                
                ddlShipVia.DataSource = shipBy;
                ddlShipVia.DataTextField = "ShipCodeNText";
                ddlShipVia.DataValueField = "ShipByCode";
                ddlShipVia.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void imgShip_OnCommand(object sender, CommandEventArgs e)
        {

            Session["rmaguid"] = Convert.ToInt32(e.CommandArgument);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('../RMA/RmaShipLabel.aspx')</script>", false);

            ////btnShip.Visible = true;
            //int rmaGUID = Convert.ToInt32(e.CommandArgument);
            //ViewState["rmaGUID"] = rmaGUID;
            //List<SV.Framework.Models.RMA.RMA> objRmaList = null;
            //if (Session["result"] != null)
            //{
            //    objRmaList = Session["result"] as List<SV.Framework.Models.RMA.RMA>;

            //    if (objRmaList != null)
            //    {
            //        var rmaInfoList = (from item in objRmaList where item.RmaGUID.Equals(rmaGUID) select item).ToList();
            //        if (rmaInfoList != null && rmaInfoList.Count > 0)
            //        {
            //            if (!string.IsNullOrWhiteSpace(rmaInfoList[0].TrackingNumber))
            //            {
            //                btnGenLabel.Visible = false;
            //                txtTrackingNumber.Text = rmaInfoList[0].TrackingNumber;
            //            }
            //        }

            //    }
            //}
            //// string poInfo = e.CommandArgument.ToString();
            //txtTrackingNumber.Text = string.Empty;
            //txtShippingDate.Text = DateTime.Now.ToShortDateString();
            //txtShipComments.Text = string.Empty;
            //BindShipBy();
            //ddlShipVia.SelectedIndex = 0;
            //ddlShape.Items.Clear();
            //string[] itemNames = System.Enum.GetNames(typeof(SV.Framework.Common.LabelGenerator.ShipPackageShape));
            //for (int i = 0; i <= itemNames.Length - 1; i++)
            //{
            //    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(itemNames[i], itemNames[i]);
            //    ddlShape.Items.Add(item);
            //}
            //System.Web.UI.WebControls.ListItem item1 = new System.Web.UI.WebControls.ListItem("", "");
            //ddlShape.Items.Insert(0, item1);

            ////avii.Classes.PurchaseOrders purchaseOrders = Session["POS"] as avii.Classes.PurchaseOrders;
            ////List<avii.Classes.BasePurchaseOrder> purchaseOrderList = purchaseOrders.PurchaseOrderList;

            ////var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(poID) select item).ToList();
            ////if (poInfoList.Count > 0)
            ////{
            ////    string poShipBy = poInfoList[0].Tracking.ShipToBy;
            ////    ddlShipVia.SelectedValue = poShipBy;

            ////    if (poInfoList[0].PurchaseOrderStatusID == 3)
            ////        btnShip.Visible = false;
            ////}
            ////string[] arr = poInfo.Split(',');
            ////string companyAccountNumber, poNum;
            ////companyAccountNumber = poNum = string.Empty;

            ////if (arr.Length > 2)
            ////{
            ////    poID = Convert.ToInt32(arr[0]);
            ////    poNum = arr[1].ToString();
            ////    companyAccountNumber = arr[2].ToString();
            ////    ViewState["ponum"] = poNum + "," + companyAccountNumber;

            ////}

            //// ViewState["rmaGUID"] = rmaGUID;
            //RegisterStartupScript("jsUnblockDialog", "unblockShipItemsDialog();");

        }
        protected void btnGenerateLabel(object sender, EventArgs e)
        {
            GenerateShipmentLabel();

        }
        private int GenerateShipmentLabel()
        {
            SV.Framework.RMA.RMATrackingOperation rmaTrackingOperation = SV.Framework.RMA.RMATrackingOperation.CreateInstance<SV.Framework.RMA.RMATrackingOperation>();

            string shipFromContactName = ConfigurationSettings.AppSettings["ShipFromContactName"].ToString();
            string shipFromAddress = ConfigurationSettings.AppSettings["ShipFromAddress"].ToString();
            string shipFromCity = ConfigurationSettings.AppSettings["ShipFromCity"].ToString();
            string shipFromState = ConfigurationSettings.AppSettings["ShipFromState"].ToString();
            string shipFromZip = ConfigurationSettings.AppSettings["ShipFromZip"].ToString();
            string shipFromCountry = ConfigurationSettings.AppSettings["ShipFromCountry"].ToString();
            string shipFromAttn = ConfigurationSettings.AppSettings["ShipFromAttn"].ToString();
            string shipFromPhone = ConfigurationSettings.AppSettings["ShipFromPhone"].ToString();
            int returnResult = 0;
            int rmaGUID = 0, userId = 0;
            double weight = 0;
            double.TryParse(txtWeight.Text.Trim(), out weight);
            if (weight == 0)
                weight = 1;
            //lblESN.Text = string.Empty;
            //lblTracking.Text = string.Empty;
            SV.Framework.Common.LabelGenerator.EndiciaPrintLabel labelInfo = new SV.Framework.Common.LabelGenerator.EndiciaPrintLabel();

            SV.Framework.Common.LabelGenerator.ShippingLabelOperation shipAccess = new SV.Framework.Common.LabelGenerator.ShippingLabelOperation();
            SV.Framework.Common.LabelGenerator.ShipInfo shipToInfo = new SV.Framework.Common.LabelGenerator.ShipInfo();
            SV.Framework.Common.LabelGenerator.ShipInfo shipFromInfo = new SV.Framework.Common.LabelGenerator.ShipInfo();


            try
            {
                string shipmentType = "S";
                if (ViewState["rmaGUID"] != null)
                {
                    rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);

                    if (Session["UserID"] != null)
                    {
                        int.TryParse(Session["UserID"].ToString(), out userId);
                    }
                    //objRmaList = Session["result"] as List<SV.Framework.Models.RMA.RMA>;
                    List<SV.Framework.Models.RMA.RMA> rmaList = Session["result"] as List<SV.Framework.Models.RMA.RMA>;

                    if (rmaList != null && rmaList.Count > 0)
                    {
                        var rma = (from item in rmaList where item.RmaGUID.Equals(rmaGUID) select item).ToList();
                        if (rma != null && rma.Count > 0)
                        {
                            labelInfo.FulfillmentNumber = rma[0].RmaNumber;
                            labelInfo.LabelPrintDateTime = DateTime.Today;
                            //shipToInfo
                            shipToInfo.ShipToAddress = shipFromAddress;
                            shipToInfo.ShipToAddress2 = "";
                            shipToInfo.ContactName = shipFromContactName;
                            shipToInfo.ShipToCity = shipFromCity;
                            shipToInfo.ShipToState = shipFromState;
                            shipToInfo.ShipToZip = shipFromZip;
                            shipToInfo.ShipToAttn = shipFromContactName;
                            shipToInfo.ContactPhone = shipFromPhone;
                            shipToInfo.ShipToCountry = shipFromCountry;
                            labelInfo.ShipTo = shipToInfo;

                            //string[] array = rma[0].State.Split('-');
                            //ship From Info
                            shipFromInfo.ShipToAddress = rma[0].Address;
                            shipFromInfo.ShipToAddress2 = "";
                            shipFromInfo.ContactName = rma[0].RmaContactName;
                            shipFromInfo.ShipToCity = rma[0].City;
                            shipFromInfo.ShipToState = rma[0].State;
                            shipFromInfo.ShipToZip = rma[0].Zip;
                            shipFromInfo.ShipToAttn = rma[0].RmaContactName;
                            shipFromInfo.ShipToCountry = rma[0].ContactCountry;
                            shipFromInfo.ContactPhone = rma[0].Phone;

                            labelInfo.ShipFrom = shipFromInfo;
                            //
                            labelInfo.PackageWeight = new SV.Framework.Common.LabelGenerator.Weight { units = "ounces", value = weight };
                            labelInfo.PackageContent = "Description";

                            //labelInfo.ShippingMethod = SV.Framework.Common.LabelGenerator.ShipMethods.Priority;
                            // if (ddlShipVia.SelectedIndex > 0)
                            {
                                Enum.TryParse(ddlShipVia.SelectedValue, out SV.Framework.Common.LabelGenerator.ShipMethods shipMethods);
                                labelInfo.ShippingMethod = shipMethods;
                            }

                            labelInfo.ShippingType = SV.Framework.Common.LabelGenerator.ShipType.Ship;


                            if (ddlShape.SelectedIndex > 0)
                            {
                                Enum.TryParse(ddlShape.SelectedValue, out SV.Framework.Common.LabelGenerator.ShipPackageShape shipPackage);
                                labelInfo.PackageShape = shipPackage;
                            }
                            else
                                labelInfo.PackageShape = SV.Framework.Common.LabelGenerator.ShipPackageShape.Flat;

                            string ShipDate = txtShippingDate.Text;
                            string Package = ddlShape.SelectedValue;
                            string ShipVia = ddlShipVia.SelectedValue;
                            double Weight = Convert.ToDouble(txtWeight.Text);
                            string Comments = txtShipComments.Text.Trim();
                            decimal FinalPostage = 0;

                            SV.Framework.Common.LabelGenerator.iPrintLabel response = shipAccess.PrintShippingLabel(labelInfo);

                            if (response != null && !string.IsNullOrWhiteSpace(response.TrackingNumber))
                            {
                                RMATrackning request = new RMATrackning();
                                request.RMANumber = labelInfo.FulfillmentNumber;
                                request.ShipmentType = shipmentType;
                                request.ShippingLabelImage = response.ShippingLabelImage;
                                request.TrackingNumber = response.TrackingNumber;
                                request.ShipDate = ShipDate;
                                request.Package = Package;
                                request.ShipVia = ShipVia;
                                request.Weight = Weight;
                                request.Comments = Comments;
                                request.FinalPostage = response.FinalPostage;

                                //request.LineItems = listitems;
                                txtTrackingNumber.Text = response.TrackingNumber;
                                ShippingLabelResponse setResponse = rmaTrackingOperation.ShippingLabelUpdate(request, userId);
                                lblShipItem.Text = "Label generated successfully";
                                returnResult = 1;
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(response.PackageContent))
                                    lblShipItem.Text = response.PackageContent;
                                else
                                    lblShipItem.Text = "Technical error please try again!";


                            }

                        }


                    }

                }
            }
            catch (Exception ex)
            {
                lblShipItem.Text = ex.Message;
            }
            return returnResult;
        }

        protected void btnPicture_Click(object sender, EventArgs e)
        {
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();
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
            int.TryParse(ViewState["pictureID"].ToString(), out pictureID);
            if (fuESNPic.HasFile)
            {
                extension = Path.GetExtension(fuESNPic.PostedFile.FileName);

                fileName = "ESN" + randomNo.ToString() + rmaDetGUID.ToString() + extension;
                filePath = targetFolder + fileName;
                extension = extension.ToLower();
                if (extension == ".pdf" || extension == ".jpg" || extension == ".gif" || extension == ".png" || extension == ".jpeg" || extension == ".bmp" || extension == ".tiff" || extension == ".tif")
                {
                    //if (!File.Exists(Path.GetFullPath(filePath)))
                    {

                        fuESNPic.PostedFile.SaveAs(filePath);

                        rmaUtility.RMA_Detail_Picture_InsertUpdate(pictureID, rmaDetGUID, fileName, userID);
                        grid_bind(true);

                        lblMsg.Text = "Uploaded successfully";

                    }

                }
                else
                    lblPic.Text = "Invalid extension!";

            }
            else
                lblPic.Text = "Please select a file";


        }

        protected void imgPictire_click(object sender, CommandEventArgs e)
        {
            lblESN.Text = string.Empty;
            lblPic.Text = string.Empty;

            try
            {
                if (e.CommandArgument != null)
                {
                    string rmainfo = Convert.ToString(e.CommandArgument);
                    string[] str = rmainfo.Split(',');
                    int pictureID, rmaDetGUID;

                    int.TryParse(str[0], out pictureID);
                    int.TryParse(str[1], out rmaDetGUID);
                    string esn = str[2];
                    string esnPic = str[3];
                    ViewState["rmaDetGUID"] = rmaDetGUID;
                    ViewState["pictureID"] = pictureID;
                    if (rmaDetGUID > 0 )
                    {
                        lblESN.Text = esn;
                    }
                    else
                    {
                        //pnlTriage.Visible = false;
                        //lblTriage.Text = "No record found";
                    }

                    RegisterStartupScript("jsUnblockDialog", "unblockPictureDialog();");

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

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

        protected void imgTriage_click(object sender, CommandEventArgs e)
        {
            //string test = lblRMA.Text;
            int rmaGUID = 0;

            
            int.TryParse(ViewState["rmaGUID"].ToString(), out rmaGUID);
            if (rmaGUID > 0)
            {
                List<SV.Framework.Models.RMA.RMA> objRmaList = null;
                if (Session["result"] != null)
                {
                    objRmaList = Session["result"] as List<SV.Framework.Models.RMA.RMA>;

                    if (objRmaList != null)
                    {
                        var rmaInfoList = (from item in objRmaList where item.RmaGUID.Equals(rmaGUID) select item).ToList();
                        if (rmaInfoList != null && rmaInfoList.Count > 0)
                        {
                            lbRma.Text = rmaInfoList[0].RmaNumber;
                          //  lblRmaStatus.Text = rmaInfoList[0].Status;


                        }

                    }
                }
            }
            txtTriageNotes.Text = string.Empty;
            ddlTriage.SelectedIndex = 0;
            lblTriage.Text = string.Empty;
            //pnlTriage.Visible = true;

            //SV.Framework.Models.RMA.RMAUtility rmaObj = new SV.Framework.Models.RMA.RMAUtility();
            try
            {
                if (e.CommandArgument != null)
                {
                    string rmainfo = Convert.ToString(e.CommandArgument);
                    string[] str = rmainfo.Split('~');
                    int triageStatusID = Convert.ToInt32(str[0]);
                    string traigeNotes = str[1];
                    if (triageStatusID > 0 && !string.IsNullOrEmpty(traigeNotes))
                    {

                        txtTriageNotes.Text = traigeNotes;
                        ddlTriage.SelectedValue = triageStatusID.ToString();
                    }
                    else
                    {
                        //pnlTriage.Visible = false;
                        //lblTriage.Text = "No record found";
                    }

                    RegisterStartupScript("jsUnblockDialog", "unblockTriageDialog();");

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        
        private void BindRMAStatuses(int companyID)
        {
            try
            {
                SV.Framework.RMA.RmaOperation rmaUtility = SV.Framework.RMA.RmaOperation.CreateInstance<SV.Framework.RMA.RmaOperation>();

                List<CustomerRMAStatus> customerRMAStatusList = rmaUtility.GetCustomerRMAStatusList(companyID, true);
                if (customerRMAStatusList != null && customerRMAStatusList.Count > 0)
                {
                    ddlStatus.DataSource = customerRMAStatusList;
                    ddlStatus.DataValueField = "StatusID";
                    ddlStatus.DataTextField = "StatusDescription";

                    ddlStatus.DataBind();
                    ddlStatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        
        private void BindUserStores(int userID, int companyID)
        {
            //List<avii.Classes.StoreLocation> storeList = avii.Classes.StoreOperations.GetCompanyStoreList(companyID, userID);

          
            //if (storeList != null && storeList.Count > 0)
            //{
            //    ddlStoreID.Visible = true;
            //    Session["userstore"] = storeList;
            //    ddlStoreID.DataSource = storeList;
            //    ddlStoreID.DataValueField = "StoreID";
            //    ddlStoreID.DataTextField = "StoreName";
            //    ddlStoreID.DataBind();
            //    ddlStoreID.Items.Insert(0, new ListItem("", ""));
            //    //ddlStoreID.SelectedIndex = 1;
            //    lblStore.Visible = true;
                

            //}
            //else
            //{
            //    Session["userstore"] = null;
            //    ddlStoreID.Items.Clear();
            //    ddlStoreID.DataSource = null;
            //    ddlStoreID.DataBind();
            //    ddlStoreID.Visible = false;
            //    lblStore.Visible = false;
                
            //    lblMsg.Text = "No store assigned to this user, please contact administrator to get more information.";

            //}
        }
        
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;
            
            BindRMAStatuses(companyID);
            companyID = Convert.ToInt32(ddlCompany.SelectedValue.Trim());
            BindUserStores(0, companyID);
        }
        //Maintain search criteria for back to search
        protected void bindsearchRMA()
        {
            int userid = 0;
            int companyID = -1;
            string searchCriteria;
            string[] searchArr;
            if (ViewState["userid"] != null)
                userid = Convert.ToInt32(ViewState["userid"]);
            if (Request["search"] != null && Request["search"] != "")
            {
                searchCriteria = (string)Session["searchRma"];
                if (Session["searchRma"] != null)
                {

                    searchArr = searchCriteria.Split('~');
                    companyID = Convert.ToInt32(searchArr[0]);
                    if (userid == 0)
                    {
                        ddlCompany.SelectedValue = searchArr[0].ToString();
                        BindUserStores(0, companyID);
                    }
                    rmanumber.Text = searchArr[1].ToString();
                    txtRMADate.Text = searchArr[2].ToString();
                    ddlStatus.SelectedValue = searchArr[3].ToString();
                    txtUPC.Text = searchArr[4].ToString();
                    txtESNSearch.Text = searchArr[5].ToString();
                    txtRMADateTo.Text = searchArr[6].ToString();
                    //if (string.IsNullOrEmpty(searchArr[7].ToString().Trim()))
                    //    ddlStoreID.SelectedValue = searchArr[7].ToString();
                        
                    

                    grid_bind(true);
                    //if (userid > 0)
                    //{
                    //    radGridRmaDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                    //}
                    //else
                    //    radGridRmaDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                }
                else
                {
                    lblMsg.Text = "Session Expire!";
                }
            }
        }
        //Maintain search criteria from dashboard
        protected void BindRMA(int userid)
        {

            string statusID = "0";


            

            if (Session["rmastatus"] != null)
            {
                int days = 30;
                int companyID = 0;
                if (Session["days"] != null)
                {
                    days = Convert.ToInt32(Session["days"]);
                }
                if (Session["adm"] != null)
                {
                    if (Session["cid"] != null)
                        companyID = Convert.ToInt32(Session["cid"]);
                    if (companyID > 0)
                        ddlCompany.SelectedValue = companyID.ToString();
                    Session["cid"] = null;
                }
                statusID = Convert.ToString(Session["rmastatus"]);
                DateTime today = DateTime.Today;
                DateTime rmaDate = today.AddDays(-days);
                txtRMADate.Text = rmaDate.ToShortDateString();
                ddlStatus.SelectedValue = statusID;

                grid_bind(true);
                Session["rmastatus"] = null;
                Session["days"] = null;

                //if (userid > 0)
                //{
                //    radGridRmaDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                //}
                //else
                //    radGridRmaDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
            }

        }
        //Bind Company DopdownList
        protected void bindCompany()
        {
            //SV.Framework.Models.RMA.RMAUtility rmaObj = new SV.Framework.Models.RMA.RMAUtility();
            ddlCompany.DataSource = avii.Classes.clsCompany.GetCompany(0,0);
            ddlCompany.DataValueField = "companyID";
            ddlCompany.DataTextField = "companyName";
            ddlCompany.DataBind();
            //ListItem item = new ListItem("", "0");
            //ddlCompany.Items.Insert(0, item);
        }
        
        protected void search_click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            
            //radGridRmaDetails.MasterTableView.Rebind();

            grid_bind(true);
        }
        //Maintain list to be serialised to xml for bulk update of status
        //private List<SV.Framework.Models.RMA.RMA_Status> generateRMAStatusList()
        //{
        //    List<SV.Framework.Models.RMA.RMA_Status> rmalist = new List<SV.Framework.Models.RMA.RMA_Status>();
        //   //string vxml = string.Empty;
        //   int userid = 0;
        //   //SV.Framework.Models.RMA.RMAUtility rmautilityobj = new SV.Framework.Models.RMA.RMAUtility();
        //   if (ViewState["userid"] != null)
        //       userid = Convert.ToInt32(ViewState["userid"]);
        //   try
        //   {
        //       foreach (GridDataItem items in radGridRmaDetails.Items)
        //       {
        //           if (items.OwnerTableView.Name == "RMAMaster")
        //           {
        //               HiddenField hdnrmaGUID = (HiddenField)items.FindControl("hdnrmaGUID");
        //               if (items.Selected)
        //               {
        //                   SV.Framework.Models.RMA.RMA_Status rmaobj = new SV.Framework.Models.RMA.RMA_Status();
        //                   rmaobj.RmaGUID = Convert.ToInt32(hdnrmaGUID.Value);
        //                   rmaobj.RmaStatusID = Convert.ToInt32(ddlchangestatus.SelectedValue);
        //                   rmaobj.UserID = userid;
        //                   rmalist.Add(rmaobj);

        //               }
        //           }
        //       }

        //       //vxml = rmautilityobj.serializeObjetToXMLString((object)this.rmalist, "ArrayOfRMA_Status", "RMA_Status");
        //   }
        //   catch (Exception ex)
        //   {
        //       lblMsg.Text = ex.Message.ToString();
        //   }
        //   return rmalist;

            
        //}

        protected void btnSubmit_click(object sender, EventArgs e)
        {
            //SV.Framework.Models.RMA.RMAUtility rmautilityobj = new SV.Framework.Models.RMA.RMAUtility();
           // string objxml;
            //List<SV.Framework.Models.RMA.RMA_Status> rmaStatusList = null;
            try
            {
                //rmaStatusList = generateRMAStatusList();
                ////objxml = generatexml();
                //RMAUtility.update_RMA_batchupdate(rmaStatusList);
                //radGridRmaDetails.MasterTableView.Rebind();
                //lblMsg.Text = "RMA Updated Successfully";
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        //Bind Grid with RMA and RMA Details based on the search
        protected void grid_bind(bool rebind)
        {
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            btnExport.Visible = false;
            DataTable detailDT = default;
            int companyID = -1;
            int userid = 0;
            string Status = "-1";
            string RmaNumber, rmaDate,rmaDateTo,UPC, esn, avso, poNum, returnReason, storeID;
            string searchCriteria;
            bool validForm = true;
            DateTime rma_Date, rmaDtTo;
            if (ViewState["userid"] != null)
                userid = Convert.ToInt32(ViewState["userid"]);
            try
            {
                if (ddlStatus.SelectedIndex > 0)
                    Status = ddlStatus.SelectedValue;
                //SV.Framework.Models.RMA.RMAUtility rmaObj = new SV.Framework.Models.RMA.RMAUtility();
                if (userid > 0)
                {
                    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    // SV.Framework.Models.RMA.RMAUserCompany objRMAcompany = RMAUtility.getRMAUserCompanyInfo(-1, "", -1, userid);
                    companyID = userInfo.CompanyGUID;
                }
                else
                {
                    if (Session["adm"] != null)
                    {
                        if (ddlCompany.SelectedIndex > 0)
                        {
                            companyID = Convert.ToInt32(ddlCompany.SelectedValue);
                        }
                        else
                        {
                            lblMsg.Text = "Customer is required!";
                            return;
                        }
                    }
                }

               
              //  storeID = ddlStoreID.SelectedIndex > 0 ? ddlStoreID.SelectedValue: string.Empty;

                rmaDate = rmaDateTo = null;
                RmaNumber = (rmanumber.Text.Trim().Length > 0 ? rmanumber.Text.Trim() : null);
                UPC = (txtUPC.Text.Trim().Length > 0 ? txtUPC.Text.Trim() : string.Empty);
                esn = (txtESNSearch.Text.Trim().Length > 0 ? txtESNSearch.Text.Trim() : string.Empty);
               // avso = (txtAVSO.Text.Trim().Length > 0 ? txtAVSO.Text.Trim() : string.Empty);
                poNum = (txtPONum.Text.Trim().Length > 0 ? txtPONum.Text.Trim() : string.Empty);
                returnReason = ddReason.SelectedValue;


                if (txtRMADate.Text.Trim().Length > 0)
                {
                    if (DateTime.TryParse(txtRMADate.Text, out rma_Date))
                        rmaDate = rma_Date.ToShortDateString();
                    else
                        throw new Exception("RMA Date does not have correct format(MM/DD/YYYY)");
                }
                if (txtRMADateTo.Text.Trim().Length > 0)
                {

                    if (DateTime.TryParse(txtRMADateTo.Text, out rmaDtTo))
                        rmaDateTo = rmaDtTo.ToShortDateString();
                    else
                        throw new Exception("RMA Date does not have correct format(MM/DD/YYYY)");
                }

                if (userid>0)
                {
                    if (string.IsNullOrEmpty(UPC) && string.IsNullOrEmpty(esn) && string.IsNullOrEmpty(RmaNumber) && string.IsNullOrEmpty(rmaDate) && ddlStatus.SelectedIndex == 0) // && string.IsNullOrEmpty(storeID))
                    {
                        validForm = false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(UPC) && string.IsNullOrEmpty(esn) && string.IsNullOrEmpty(RmaNumber) && string.IsNullOrEmpty(rmaDate) && string.IsNullOrEmpty(rmaDateTo) && ddlStatus.SelectedIndex == 0
                            && ddlCompany.SelectedIndex == 0 && string.IsNullOrEmpty(poNum) && ddReason.SelectedIndex == 0) // && string.IsNullOrEmpty(avso)  && string.IsNullOrEmpty(storeID))
                    {
                        validForm = true;
                    }
                }
                if (validForm)
                {
                    string sortExpression = "RmaGUID";
                    string sortDirection = "DESC";
                    ViewState["SortDirection"] = sortDirection;
                    ViewState["SortExpression"] = sortExpression;


                    List<SV.Framework.Models.RMA.RMA> objRmaList = null;
                    if (rebind)//(Context.Items["result"] != null)
                    {
                        objRmaList = rmaUtility.GetRMAList(0, rmanumber.Text, txtRMADate.Text, txtRMADateTo.Text, Convert.ToInt32(Status), companyID, "", UPC, esn, "", poNum, returnReason, "", out detailDT);
                        HttpContext.Current.Session["rmadetail"] = detailDT;
                        Session["result"] = objRmaList;
                    }
                    else
                    {
                        objRmaList = Session["result"] as List<SV.Framework.Models.RMA.RMA>;
                    }

                    if (objRmaList != null && objRmaList.Count > 0)
                    {
                        lblCount.Text = "<strong>Total count:</strong> " + objRmaList.Count.ToString();
                        btnRMAReport.Visible = true;
                        btnExport.Visible = true;
                        searchCriteria = companyID + "~" + rmanumber.Text + "~" + txtRMADate.Text + "~" + Status + "~" + UPC + "~" + esn + "~" + this.txtRMADateTo.Text; // + "~" + storeID;
                        gvRma.DataSource = objRmaList;
                        gvRma.DataBind(); 
                        Session["searchRma"] = searchCriteria;
                        //if (radGridRmaDetails.MasterTableView.Items.Count > 0)
                        {


                            //radGridRmaDetails.Visible = true;
                            //btnExport.Visible = true;
                            hlkRMASummary.Visible = true;
                            if (companyID > 0)
                                hlkRMASummary.NavigateUrl = "/rma/rmaSummary.aspx?c=" + companyID.ToString();
                        }
                        //radGridRmaDetails.Visible = true;
                        //btnExport.Visible = true;
                        pnlGrid.Visible = true;
                    }
                    else
                    {
                        lblCount.Text = string.Empty;
                        pnlGrid.Visible = false;
                        btnRMAReport.Visible = false;
                        gvRma.DataSource = null;
                        gvRma.DataBind();
                        //radGridRmaDetails.Visible = false;
                        btnExport.Visible = false;
                        hlkRMASummary.Visible = false;
                        
                        
                        lblMsg.Text = "No matching record exists for selected search criteria";
                    }
                }
                else
                {
                    lblMsg.Text = "Please select the search criteria";
                    
                    //radGridRmaDetails.Visible = false;
                    gvRma.DataSource = null;
                    gvRma.DataBind();
                        
                    btnExport.Visible = false;
                    lblCount.Text = string.Empty;
                }

                //if (userid>0)
                //{
                //    radGridRmaDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                //}
                //else
                //    radGridRmaDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();
            }
        }

        protected void Edit_click(object sender, CommandEventArgs e)
        {
            //SV.Framework.Models.RMA.RMAUtility rmaObj = new SV.Framework.Models.RMA.RMAUtility();
            try
            {
                if (e.CommandArgument != null)
                {

                    //int rmaguid = Convert.ToInt32(e.CommandArgument);
                    string rmainfo = Convert.ToString(e.CommandArgument);
                    string[] str = rmainfo.Split(',');
                    int rmaguid = Convert.ToInt32(str[0]);
                    int companyID = Convert.ToInt32(str[1]);
                   
                    string url = string.Empty;
                    int userid = 0;
                    if (ViewState["userid"] != null)
                        Int32.TryParse(ViewState["userid"].ToString(), out userid);
                    if (userid > 0)
                        url = "~/RMA-Form.aspx?rmaGUID=" + rmaguid;
                    else
                    {

                        //List<SV.Framework.Models.RMA.RMAUserCompany> rmaUserCompany = RMAUtility.getRMAUserCompany(-1, hdnCustomerEmail.Value, -1, -1);
                        //if (rmaUserCompany.Count > 0)
                        url = "~/RMA-Form.aspx?rmaGUID=" + rmaguid + "&companyID=" + companyID;
                    }

                    Response.Redirect(url);
                }
                else
                    lblMsg.Text = "Invalid parameter is passed, please contact your administrator";
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        
        protected void LinkButton1_Click(object sender, CommandEventArgs e)
        {
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            int rmaguid = Convert.ToInt32(e.CommandArgument);
            //SV.Framework.Models.RMA.RMAUtility rmaObj = new SV.Framework.Models.RMA.RMAUtility();
            int userID = Convert.ToInt32(Session["UserID"]);
            try
            {
               rmaUtility.delete_RMA_RMADETAIL(rmaguid, 0, userID);
                grid_bind(false);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
               
       
        }
        
        protected void LinkButton2_Click(object sender, CommandEventArgs e)
        {
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            int rmadetguid = Convert.ToInt32(e.CommandArgument);
            //SV.Framework.Models.RMA.RMAUtility rmaObj = new SV.Framework.Models.RMA.RMAUtility();
            int userID = Convert.ToInt32(Session["UserID"]);
            try
            {
                rmaUtility.delete_RMA_RMADETAIL(0, rmadetguid, userID);
                grid_bind(false);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }
        
        protected void Button2_Click1(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            int userid = 0;
            txtESNSearch.Text = string.Empty;
            txtUPC.Text = string.Empty;                
            if (ViewState["userid"] != null)
                userid = Convert.ToInt32(ViewState["userid"]);

            rmanumber.Text = string.Empty;
            txtRMADate.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            if (userid==0)
            {
                ddlCompany.SelectedIndex = 0;
            }
            lblMsg.Text = string.Empty;
            rebind = true;
            btnRMAReport.Visible = false;
            btnExport.Visible = false;
            hlkRMASummary.Visible = false;
            // radGridRmaDetails.MasterTableView.Rebind();
            gvRma.DataSource = null;
            gvRma.DataBind();
                        
            Session["searchRma"] = null;
            Session["result"] = null;
            lblCount.Text = string.Empty;
        }

        //protected void RadGrid1_PreRender(object sender, EventArgs e)
        //{
        //    if (!Page.IsPostBack)
        //    {
        //        if (radGridRmaDetails.MasterTableView.Items.Count > 0)
        //        {
        //        }
        //    }
        //}

        //protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        //{
        //    pnlGrid.Visible = false;
        //    //SV.Framework.Models.RMA.RMAUtility rmaObj = new SV.Framework.Models.RMA.RMAUtility();
        //    //if (rebind)
        //    //{
        //    //    radGridRmaDetails.DataSource = RMAUtility.getRMAList(0, "", "", "", -1, 0, "", "", "", string.Empty, string.Empty, string.Empty);
        //    //    radGridRmaDetails.Visible = false;
        //    //    btnExport.Visible = false;
        //    //}
        //    //else
        //    //{
        //    //    if (!string.IsNullOrEmpty(hdnrmaGUIDs.Value))
        //    //    {
        //    //        radGridRmaDetails.DataSource = RMAUtility.getRMAList(0, "", "", "", -1, -1, hdnrmaGUIDs.Value, "", "", string.Empty, string.Empty, string.Empty);
        //    //        hdnrmaGUIDs.Value = string.Empty;
        //    //    }
        //    //    else
        //    //        grid_bind();

        //    //}
        //}
        
        
        
        //protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        //{

        //    GridEditableItem editedItem = e.Item as GridEditableItem;
        //}
        
        
        //protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        //{
        //    grid_bind(false);
        //}
        
        //protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        //{
        //    //SV.Framework.Models.RMA.RMAUtility rmaObj = new SV.Framework.Models.RMA.RMAUtility();
        //    string RmaGUID;
        //    string rmaDetGUID;
        //    GridDataItem item = (GridDataItem)e.Item;
        //    if (item.OwnerTableView.Name == "RMAMaster")
        //    {
        //        RmaGUID = item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RmaGUID"].ToString();

        //        RMAUtility.delete_RMA_RMADETAIL(Convert.ToInt32(RmaGUID), 0);
        //    }
        //    if (item.OwnerTableView.Name == "rmaDetails")
        //    {
        //        rmaDetGUID = item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["rmaDetGUID"].ToString();

        //        RMAUtility.delete_RMA_RMADETAIL(0, Convert.ToInt32(rmaDetGUID));
        //    }

        //}
        
        protected void btnExport_click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            //int rmaGUID = 0;
            if (Session["result"] != null)
            {
                List<SV.Framework.Models.RMA.RMA> objRmaList = Session["result"] as List<SV.Framework.Models.RMA.RMA>;

                List<RMACSV> rmaList = new List<RMACSV>();
                RMACSV rma = null;
                if (objRmaList != null && objRmaList.Count>0)
                {
                    foreach (SV.Framework.Models.RMA.RMA item in objRmaList)
                    {
                        rma = new RMACSV();
                        
                        rma.CustomerName = item.RMAUserCompany.CompanyName;
                        rma.RMANumber = item.RmaNumber;
                        rma.RMADate = item.RmaDate.ToShortDateString();
                        rma.TrackingNumber = item.TrackingNumber ?? string.Empty;
                        rma.RMAStatus = item.Status;

                        DataTable objDataTable = (DataTable)HttpContext.Current.Session["rmadetail"];
                        if (objDataTable != null)
                        {
                            DataRow[] rows = objDataTable.Select(string.Format("rmaGUID='{0}' ", item.RmaGUID));
                            
                            if (rows.Length > 0)
                            {
                                foreach (DataRow dataRow in rows)
                                {
                                    rma.ESN = Convert.ToString(dataRow["ESN"]); //clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                                    rma.ESNStatus = Convert.ToString(dataRow["Status"]); //clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                                    rma.TriageNotes = Convert.ToString(dataRow["TriageNotes"]); //clsGeneral.getColumnData(dataRow, "TriageNotes", string.Empty, false) as string;
                                    rma.TriageStatus = Convert.ToString(dataRow["TriageStatus"]); //clsGeneral.getColumnData(dataRow, "TriageStatus", string.Empty, false) as string;
                                }
                                
                            }
                        }
                        rmaList.Add(rma);
                    }
                }


                string string2CSV = rmaList.ToCSV();

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=RMAExport.csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(string2CSV);
                Response.Flush();
                Response.End();
            }
            else
            {
                lblMsg.Text = "Session exired!";
            }

            //string filename = string.Empty;
            //bool selectflag = false;
            //DateTime dt = DateTime.Now;
            //int userid = 0;
            //if (ViewState["userid"] != null)
            //    userid = Convert.ToInt32(ViewState["userid"]);
            //hdnrmaGUIDs.Value = string.Empty;
            //foreach (GridDataItem items in radGridRmaDetails.Items)
            //{
            //    if (items.OwnerTableView.Name == "RMAMaster")
            //    {
            //        HiddenField hdnrmaGUID = (HiddenField)items.FindControl("hdnrmaGUID");
            //        if (items.Selected)
            //        {
            //            selectflag = true;
            //            if (hdnrmaGUIDs.Value == string.Empty)
            //                hdnrmaGUIDs.Value = hdnrmaGUID.Value;
            //            else
            //                hdnrmaGUIDs.Value = hdnrmaGUIDs.Value + "," + hdnrmaGUID.Value;

            //        }
            //    }
            //}
            //if (selectflag)
            //{
            //    radGridRmaDetails.MasterTableView.Rebind();
            //}

            //foreach (GridDataItem item in radGridRmaDetails.Items)
            //{
            //   if (item.OwnerTableView.Name == "RMAMaster")
            //    {
            //        radGridRmaDetails.MasterTableView.Columns.FindByUniqueName("colSelect").Visible = false;
            //        LinkButton lnkedit = (LinkButton)item.FindControl("lnkedit");
            //        lnkedit.Visible = false;

            //        if (radGridRmaDetails.Columns.Count > 0)
            //            radGridRmaDetails.Columns[6].Visible = false;


            //    }
            //    if (item.OwnerTableView.Name == "rmaDetails")
            //    {
            //        Label lblESN = (Label)item.FindControl("lblESN");

            //        lblESN.Text = "#" + lblESN.Text;

            //    }

            //}
            //if (userid==0)
            //    filename = hdncompany.Value + "_";
            //else
            //    filename = "WSA_";
            //radGridRmaDetails.MasterTableView.HierarchyDefaultExpanded = true;

            //radGridRmaDetails.ExportSettings.FileName = filename + dt.ToString();

            //radGridRmaDetails.ExportSettings.ExportOnlyData = true;
            //radGridRmaDetails.ExportSettings.IgnorePaging = true;
            //radGridRmaDetails.ExportSettings.OpenInNewWindow = true;
            //radGridRmaDetails.MasterTableView.ExportToExcel();
        }

        protected void gvRma_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
                return;

            ImageButton imgView = e.Row.FindControl("imgView") as ImageButton;
            imgView.OnClientClick = "openESNDialogAndBlock('RMA Detail', '" + imgView.ClientID + "')";
            
            ImageButton imgComments = e.Row.FindControl("imgComments") as ImageButton;
            imgComments.OnClientClick = "openCommentsDialogAndBlock('RMA Customer Comments', '" + imgComments.ClientID + "')";


            ImageButton imgHistory = e.Row.FindControl("imgHistory") as ImageButton;
            imgHistory.OnClientClick = "openHistoryDialogAndBlock('RMA History', '" + imgHistory.ClientID + "')";

            ImageButton imgDoc = e.Row.FindControl("imgDoc") as ImageButton;
            imgDoc.OnClientClick = "openDocDialogAndBlock('RMA Documents', '" + imgDoc.ClientID + "')";

            //ImageButton imgShip = (ImageButton)e.Row.FindControl("imgShip");
            // imgShip.OnClientClick = "openShipDialogAndBlock('Generate Shipping Label', '" + imgShip.ClientID + "')";

            //HiddenField hdShipVia = (HiddenField)e.Row.FindControl("hdShipVia");
            //ImageButton imgPrint = (ImageButton)e.Row.FindControl("imgPrint");
            //if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), hdShipVia.Value))
            //{
            //    imgPrint.OnClientClick = "openLabelDialogAndBlock('Shipping Label', '" + imgPrint.ClientID + "')";

            //}
            //if (string.IsNullOrEmpty(hdTN.Value))
            //{

            //    imgPrint.Visible = false;

            //}

            //ImageButton imgAComments = e.Row.FindControl("imgAComments") as ImageButton;
            //imgAComments.OnClientClick = "openCommentsDialogAndBlock('RMA AV Comments', '" + imgAComments.ClientID + "')";
            ImageButton imgEditRma = e.Row.FindControl("imgEditRma") as ImageButton;
            ImageButton imgDel = e.Row.FindControl("imgDel") as ImageButton;

            if (Session["adm"] == null)
            {
                string status = ((DataBoundLiteralControl)e.Row.Cells[5].Controls[0]).Text;
                status = status.Replace("\r\n", "");
                status = status.Replace("\n", "");
                status = status.Trim();
                status = status.ToLower();
                if ("pending" != status)
                {
                    
                    if (imgEditRma != null)
                        imgEditRma.Visible = false;
                    if (imgDel != null)
                        imgDel.Visible = false;
                }



            }

            if(ViewState["IsReadOnly"] != null)
            {
                ImageButton imgShip = e.Row.FindControl("imgShip") as ImageButton;
                
                if (imgShip != null)
                    imgShip.Visible = false;

                if (imgEditRma != null)
                    imgEditRma.Visible = false;
                if (imgDel != null)
                    imgDel.Visible = false;
            }

        }
        protected void imgRMAHistory_Click(object sender, CommandEventArgs e)
        {
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            lblHistory.Text = string.Empty;
            int rmaGUID = Convert.ToInt32(e.CommandArgument);
           // lblHistory.Text = "No RMA history exists for this RMA";
            rptRma.DataSource = null;
            rptRma.DataBind();
            lblRMANum.Text = string.Empty;
            //ReloadRmahistory(rmaGUID);
            //RegisterStartupScript("jsUnblockDialog", "unblockHistoryDialog();");
            if (rmaGUID > 0)
            {

                List<SV.Framework.Models.RMA.RMA> objRmaList = null;
                if (Session["result"] != null)
                {
                    objRmaList = Session["result"] as List<SV.Framework.Models.RMA.RMA>;

                    if (objRmaList != null)
                    {
                        var rmaInfoList = (from item in objRmaList where item.RmaGUID.Equals(rmaGUID) select item).ToList();
                        if (rmaInfoList != null && rmaInfoList.Count > 0)
                        {
                            lblRMANum.Text = rmaInfoList[0].RmaNumber;


                            List<SV.Framework.Models.RMA.RmaHistory> rmaList = rmaUtility.GetRMAHistory(rmaGUID);
                            if (rmaList.Count > 0)
                            {
                                rptRma.DataSource = rmaList;
                                rptRma.DataBind();

                                //mdlPopup2.Show();
                            }
                            else
                            {
                                lblHistory.Text = "No RMA history exists for this RMA";
                                rptRma.DataSource = null;
                                rptRma.DataBind();
                                lblRMANum.Text = string.Empty;
                            }
                        }
                    }
                }
            }
            RegisterStartupScript("jsUnblockDialog", "unblockHistoryDialog();");


        }
        protected void imgPrint_Command(object sender, CommandEventArgs e)
        {
            try
            {
                SV.Framework.Fulfillment.ShippingLabelOperation ShippingLabelOperation = SV.Framework.Fulfillment.ShippingLabelOperation.CreateInstance<SV.Framework.Fulfillment.ShippingLabelOperation>();
                int lineNumber = Convert.ToInt32(e.CommandArgument);
                string shipMethod = string.Empty;
                string labelBase64 = avii.Classes. ShippingLabelOperation.GetLabelBase64(lineNumber, out shipMethod, out shipMethod);
                if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), shipMethod))
                {
                    if (!string.IsNullOrWhiteSpace(labelBase64))
                    {
                        imgLabel.ImageUrl = "data:image;base64," + labelBase64;
                        RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");
                    }
                }
                else
                {
                    // RegisterStartupScript("jsUnblockDialog", "closeLabelDialog();");
                    if (!string.IsNullOrWhiteSpace(labelBase64))
                    {
                        //var script = "OpenPDF('" + labelBase64 + "')"; //"window.open('" + pdfByteArray + "', '_blank');";
                        //ScriptManager.RegisterClientScriptBlock(Parent.Page, typeof(Page), "pdf", script, true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenPDF('" + labelBase64 + "')</script>", false);
                        // imgLabel.ImageUrl = "data:application/pdf;base64," + labelBase64;
                        //  RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");
                        //data: application/pdf; base64,
                        //string fileType = ".pdf";
                        //string filename = DateTime.Now.Ticks + fileType;
                        //Response.Clear();
                        ////Response.ContentType = "application/pdf";
                        //Response.ContentType = "application/octet-stream";
                        //Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                        //Response.Buffer = true;
                        //Response.Clear();
                        //var bytes = Convert.FromBase64String(labelBase64);
                        //Response.OutputStream.Write(bytes, 0, bytes.Length);
                        //Response.OutputStream.Flush();
                    }
                }


            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRma.PageIndex = e.NewPageIndex;

            if (Session["result"] != null)
            {
                List<SV.Framework.Models.RMA.RMA> objRmaList = (List<SV.Framework.Models.RMA.RMA>)Session["result"];
                gvRma.DataSource = objRmaList;
                gvRma.DataBind();

                // avii.Classes.PurchaseOrders pos = (avii.Classes.PurchaseOrders)Session["POS"];
                // BindGrid1(pos);
            }
        }
        protected void imgEditRMA_OnCommand(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandArgument != null)
                {
                    //int rmaguid = Convert.ToInt32(e.CommandArgument);
                    string rmainfo = Convert.ToString(e.CommandArgument);
                    string[] str = rmainfo.Split(',');
                    int rmaguid = Convert.ToInt32(str[0]);
                    int companyID = Convert.ToInt32(str[1]);
                    string url = string.Empty;
                    int userid = 0;
                    bool byPO = false;
                    string rmaSource = string.Empty;
                    if (str.Length > 2)
                    {
                        byPO = Convert.ToBoolean(str[2]);
                    }
                    if (str.Length > 3)
                    {
                        rmaSource = str[3];
                    }
                    //if(byPO)
                    {
                        Session["companyID"] = companyID;
                        Session["rmaguid"] = rmaguid;
                        if (ViewState["userid"] != null)
                            Int32.TryParse(ViewState["userid"].ToString(), out userid);
                        if (userid > 0)
                        {
                            if (rmaSource.ToLower() == "bulkrma")
                                url = "~/RMA/ManageRMAUpload.aspx";
                            if (rmaSource.ToLower() == "rmawithpo")
                                url = "~/RMA/ManageRMA.aspx";
                            if (rmaSource.ToLower() == "rmaexternal")
                                url = "~/RMA-Form.aspx?rmaGUID=" + rmaguid;



                        }
                        else
                        {
                            if (rmaSource.ToLower() == "bulkrma")
                                url = "~/RMA/ManageRMAUpload.aspx";
                            if (rmaSource.ToLower() == "rmawithpo")
                                url = "~/RMA/ManageRMA.aspx";
                            if (rmaSource.ToLower() == "rmaexternal")
                                url = "~/RMA-Form.aspx?rmaGUID=" + rmaguid;

                            //url = "~/RMA/ManageRMA.aspx";
                        }
                    }
                    //else
                    //{
                    //    if (ViewState["userid"] != null)
                    //        Int32.TryParse(ViewState["userid"].ToString(), out userid);
                    //    if (userid > 0)
                    //        url = "~/RMA-Form.aspx?rmaGUID=" + rmaguid;
                    //    else
                    //    {
                    //        //List<SV.Framework.Models.RMA.RMAUserCompany> rmaUserCompany = RMAUtility.getRMAUserCompany(-1, hdnCustomerEmail.Value, -1, -1);
                    //        //if (rmaUserCompany.Count > 0)
                    //        url = "~/RMA-Form.aspx?rmaGUID=" + rmaguid + "&companyID=" + companyID;
                    //    }
                    //}
                    Response.Redirect(url);
                }
                else
                    lblMsg.Text = "Invalid parameter is passed, please contact your administrator";
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            //try
            //{
            //    if (e.CommandArgument != null)
            //    {

            //        int rmaguid = Convert.ToInt32(e.CommandArgument);
            //        string url = string.Empty;
            //        int userid = 0;
            //        if (ViewState["userid"] != null)
            //            Int32.TryParse(ViewState["userid"].ToString(), out userid);
            //        if (userid > 0)
            //            url = "NewRMAForm.aspx?rmaGUID=" + rmaguid;
            //        else
            //        {

            //            //List<SV.Framework.Models.RMA.RMAUserCompany> rmaUserCompany = RMAUtility.getRMAUserCompany(-1, hdnCustomerEmail.Value, -1, -1);
            //            //if (rmaUserCompany.Count > 0)
            //            url = "NewRMAForm.aspx?rmaGUID=" + rmaguid + "&companyID=" + hdnCompanyId.Value;
            //        }

            //        Response.Redirect(url);
            //    }
            //    else
            //        lblMsg.Text = "Invalid parameter is passed, please contact your administrator";
            //}
            //catch (Exception ex)
            //{
            //    lblMsg.Text = ex.Message;
            //}
        }
        protected void imgDeleteRMA_OnCommand(object sender, CommandEventArgs e)
        {
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            int rmaguid = Convert.ToInt32(e.CommandArgument);
            //SV.Framework.Models.RMA.RMAUtility rmaObj = new SV.Framework.Models.RMA.RMAUtility();
            int userID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                //ViewState["companyID"] = userInfo.CompanyGUID;
                userID = userInfo.UserGUID;
            }
            try
            {
                rmaUtility.delete_RMA_RMADETAIL(rmaguid, 0, userID);
                grid_bind(true);

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

        }
       
        protected void imgViewRMA_Click(object sender, CommandEventArgs e)
        {
            int rmaGUID = Convert.ToInt32(e.CommandArgument);
            ViewState["rmaGUID"] = rmaGUID;

            BindRMADetails(rmaGUID);
            
            
            RegisterStartupScript("jsUnblockDialog", "unblockESNDialog();");
        }

        private void BindRMADetails(int rmaGUID)
        {
            lblShipDate.Text = string.Empty;
            lblShipMethod.Text = string.Empty;
            lblWeight.Text = string.Empty;
            lblTrackingNo.Text = string.Empty;
            lblShipComment.Text = string.Empty;
            lblPrice.Text = string.Empty;
            lblLineNo.Text = "";
            // tblShipment.Visible = false;
            lblMsgDetail.Text = string.Empty;
            btnPrintLabel.Visible = false;
            if (rmaGUID > 0)
            {

                List<SV.Framework.Models.RMA.RMA> objRmaList = null;
                if (Session["result"] != null)
                {
                    objRmaList = Session["result"] as List<SV.Framework.Models.RMA.RMA>;

                    if (objRmaList != null)
                    {
                        var rmaInfoList = (from item in objRmaList where item.RmaGUID.Equals(rmaGUID) select item).ToList();
                        if (rmaInfoList != null && rmaInfoList.Count > 0)
                        {
                            lblRMA.Text = rmaInfoList[0].RmaNumber;
                            lblStatus.Text = rmaInfoList[0].Status;
                            lblCustomer.Text = rmaInfoList[0].RmaContactName;
                            lblStoreID.Text = rmaInfoList[0].StoreID;
                            lblAddress.Text = rmaInfoList[0].Address;
                            lblCity.Text = rmaInfoList[0].City;
                            lblState.Text = rmaInfoList[0].State;
                            lblZip.Text = rmaInfoList[0].Zip;


                            lblComment.Text = rmaInfoList[0].Comment;
                            lblAVComment.Text = rmaInfoList[0].AVComments;
                            lblCompanyName.Text = rmaInfoList[0].RMAUserCompany.CompanyName;
                            lblRMADate.Text = Convert.ToDateTime(rmaInfoList[0].RmaDate).ToShortDateString();
                            if (rmaInfoList[0].ShipDate == "01-01-0001" || rmaInfoList[0].ShipDate == "01/01/0001" || rmaInfoList[0].ShipDate == "1/1/0001")
                                lblShipDate.Text = "";
                            else
                                lblShipDate.Text = rmaInfoList[0].ShipDate;
                            
                            if (!string.IsNullOrWhiteSpace(rmaInfoList[0].TrackingNumber))
                            {
                                lblLineNo.Text = "1";
                                lblShipMethod.Text = rmaInfoList[0].ShipMethod;
                                lblWeight.Text = rmaInfoList[0].ShipWeight;
                                lblTrackingNo.Text = rmaInfoList[0].TrackingNumber;
                                lblShipComment.Text = rmaInfoList[0].ShipComment;
                                lblPrice.Text = rmaInfoList[0].FinalPostage.ToString(); ;

                                btnPrintLabel.Visible = true;
                               // tblShipment.Visible = true;
                            }
                        }

                    }
                }
                List<SV.Framework.Models.Old.RMA.RMADetail> rmaDetailList = default;// new List<SV.Framework.Models.Old.RMA.RMADetail>();


                DataTable objDataTable = (DataTable)HttpContext.Current.Session["rmadetail"];
                if (objDataTable != null)
                {
                    rmaDetailList = new List<SV.Framework.Models.Old.RMA.RMADetail>();
                       DataRow[] rows = objDataTable.Select(string.Format("rmaGUID='{0}' ", rmaGUID));
                    DataRow searchedRow = null;
                    if (rows.Length > 0)
                    {
                        foreach (DataRow dataRow in rows)
                        {
                            SV.Framework.Models.Old.RMA.RMADetail objRMADETAIL = new SV.Framework.Models.Old.RMA.RMADetail();
                            objRMADETAIL.rmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                            objRMADETAIL.rmaDetGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "rmaDetGUID", 0, false));
                            objRMADETAIL.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                            objRMADETAIL.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "SalesOrderNumber", string.Empty, false) as string;
                            objRMADETAIL.Reason = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Reason", 0, false));
                            objRMADETAIL.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));
                            objRMADETAIL.CallTime = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CallTime", 0, false));
                            objRMADETAIL.Notes = clsGeneral.getColumnData(dataRow, "Notes", string.Empty, false) as string;
                            objRMADETAIL.WaferSealed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WaferSealed", 0, false));
                            objRMADETAIL.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;
                            objRMADETAIL.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                            objRMADETAIL.Po_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "poid", 0, false));
                            objRMADETAIL.Pod_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "pod_id", 0, false));
                            objRMADETAIL.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "SalesOrderNumber", string.Empty, false) as string;
                            objRMADETAIL.PurchaseOrderNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;

                            objRMADETAIL.ItemCode = clsGeneral.getColumnData(dataRow, "itemcode", string.Empty, false) as string;

                            objRMADETAIL.TriageStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TriageStatusID", 0, false));
                            objRMADETAIL.TriageNotes = clsGeneral.getColumnData(dataRow, "TriageNotes", string.Empty, false) as string;
                            objRMADETAIL.ShippingTrackingNumber = clsGeneral.getColumnData(dataRow, "ShippingTrackingNumber", string.Empty, false) as string;
                            objRMADETAIL.NewSKU = clsGeneral.getColumnData(dataRow, "NewSKU", string.Empty, false) as string;
                            objRMADETAIL.CreateDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CreateDate", DateTime.MinValue, false));


                            rmaDetailList.Add(objRMADETAIL);
                        }
                        gvRMADetail.DataSource = rmaDetailList;
                        gvRMADetail.DataBind();
                    }
                }
                else
                {
                    lblMsgDetail.Text = "No records found";
                    gvRMADetail.DataSource = null;
                    gvRMADetail.DataBind();

                }

            }

            
        }
        protected void btnGeneratePackingSlip_Click(object sender, EventArgs e)
        {
            int rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
            CreateRmaPackageSlipPDF(rmaGUID);
        }
       private void CreateRmaPackageSlipPDF(int rmaGUID)
        {
            if (rmaGUID > 0)
            {
                SV.Framework.LabelGenerator.RmaInfo rmaInfo = new SV.Framework.LabelGenerator.RmaInfo();
                SV.Framework.LabelGenerator.RmaDetailModel rmaDetailModel = null;
                List<SV.Framework.LabelGenerator.RmaDetailModel> rmaDetails = null;
                SV.Framework.LabelGenerator.RmaPackingSlipOperation operation = new SV.Framework.LabelGenerator.RmaPackingSlipOperation();
                string shipFromContactName = ConfigurationSettings.AppSettings["ShipFromContactName"].ToString();
                string shipFromAddress = ConfigurationSettings.AppSettings["ShipFromAddress"].ToString();
                string shipFromCity = ConfigurationSettings.AppSettings["ShipFromCity"].ToString();
                string shipFromState = ConfigurationSettings.AppSettings["ShipFromState"].ToString();
                string shipFromZip = ConfigurationSettings.AppSettings["ShipFromZip"].ToString();
                string shipFromCountry = ConfigurationSettings.AppSettings["ShipFromCountry"].ToString();
                string shipFromAttn = ConfigurationSettings.AppSettings["ShipFromAttn"].ToString();
                string shipFromPhone = ConfigurationSettings.AppSettings["ShipFromPhone"].ToString();
                SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();


                List<SV.Framework.Models.RMA.RMA> objRmaList = null;
                if (Session["result"] != null)
                {
                    objRmaList = Session["result"] as List<SV.Framework.Models.RMA.RMA>;

                    if (objRmaList != null)
                    {
                        var rmaInfoList = (from item in objRmaList where item.RmaGUID.Equals(rmaGUID) select item).ToList();
                        if (rmaInfoList != null && rmaInfoList.Count > 0)
                        {
                            rmaInfo.RMANumber = rmaInfoList[0].RmaNumber;
                            rmaInfo.Comments = rmaInfoList[0].Comment;
                            //lblAVComment.Text = rmaInfoList[0].AVComments;
                            rmaInfo.ContactName = rmaInfoList[0].RmaContactName;
                            rmaInfo.PhoneNumber = rmaInfoList[0].Phone;
                            rmaInfo.Email = rmaInfoList[0].Email;
                            rmaInfo.CompanyLogo = Server.MapPath("~/img/" + rmaInfoList[0].CompanyLogo.Replace(".", "2."));
                            // rmaInfo.CompanyLogo = Server.MapPath("~/img/fplogo2.png"); ;
                            rmaInfo.RmaDate = rmaInfoList[0].RmaDate.ToShortDateString();
                            rmaInfo.PackingSlip = rmaInfoList[0].RmaNumber;
                            rmaInfo.StoreID = rmaInfoList[0].StoreID;
                            rmaInfo.StoreName = string.Empty;//rmaInfoList[0].StoreID;

                            

                            rmaInfo.CompanyName = shipFromContactName;
                            rmaInfo.ShippingAddressLine1 = shipFromAddress;
                            rmaInfo.ShippingCity = shipFromCity;
                            rmaInfo.ShippingState = shipFromState;
                            rmaInfo.ShippingZipCode = shipFromZip;
                            rmaInfo.ShippingCountry = shipFromCountry;
                            //rmaInfo.s = shipFromContactName;


                        }

                    }
                }

                rmaDetails = new List<SV.Framework.LabelGenerator.RmaDetailModel>();

                DataTable objDataTable = (DataTable)HttpContext.Current.Session["rmadetail"];
                if (objDataTable != null)
                {
                    DataRow[] rows = objDataTable.Select(string.Format("rmaGUID='{0}' ", rmaGUID));
                    DataRow searchedRow = null;
                    if (rows.Length > 0)
                    {
                        foreach (DataRow dataRow in rows)
                        {
                            rmaDetailModel  = new SV.Framework.LabelGenerator.RmaDetailModel();
                            int warrantyInt = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Warranty", 0, false));
                            int dispositionId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Disposition", 0, false));
                            int ReasonId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Reason", 0, false));

                            Warranty warranty = (Warranty)warrantyInt;

                            Disposition disposition = (Disposition)dispositionId;
                            RMAReason reason = (RMAReason)ReasonId;


                            rmaDetailModel.Disposition = disposition.ToString();
                            rmaDetailModel.Warranty = warranty.ToString();
                            rmaDetailModel.RMAReason = reason.ToString();



                            rmaDetailModel.ItemDescription = clsGeneral.getColumnData(dataRow, "ItemDescription", string.Empty, false) as string;

                            rmaDetailModel.ItemCode = clsGeneral.getColumnData(dataRow, "itemcode", string.Empty, false) as string;

                            rmaDetailModel.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;


                            

                            rmaDetails.Add(rmaDetailModel);
                        }
                        rmaInfo.RmaDetails = rmaDetails;
                        var memSt = operation.ExportToPDF(rmaInfo);
                        if (memSt != null)
                        {
                            string fileType = ".pdf";
                            string filename = DateTime.Now.Ticks + fileType;
                            Response.Clear();
                            //Response.ContentType = "application/pdf";
                            Response.ContentType = "application/octet-stream";
                            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                            Response.Buffer = true;
                            Response.Clear();
                            var bytes = memSt.ToArray();
                            Response.OutputStream.Write(bytes, 0, bytes.Length);
                            Response.OutputStream.Flush();

                            string packingSlip = "pck" + rmaGUID.ToString();
                            rmaUtility.RmaPackingSlipInsertUpdate(rmaGUID, packingSlip);
                        }
                        else
                            lblMsgDetail.Text = "Technical error!";


                    }
                }
                else
                {
                    
                }

            }


        }

        protected void imgRMADoc_OnCommand(object sender, CommandEventArgs e)
        {
            int rmaGUID = Convert.ToInt32(e.CommandArgument);
            ViewState["rmaGUID"] = rmaGUID;
            BindRmaDocuments(rmaGUID);

            RegisterStartupScript("jsUnblockDialog", "unblockDocDialog();");
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
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            DataSet rmaDocList =rmaUtility.GetRmaDocumentLists(rmaGuid);
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
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            int rmaDocID = Convert.ToInt32(e.CommandArgument);
            int rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);

            int userID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                //ViewState["companyID"] = userInfo.CompanyGUID;
                userID = userInfo.UserGUID;
            }
            try
            {
                rmaUtility.Delete_RMA_Document(rmaGUID, rmaDocID, userID);
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

        protected void gvRmaDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
                return;
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();

            ImageButton imgTriage = (ImageButton)e.Row.FindControl("imgTriage"); 
            if(imgTriage != null)
                imgTriage.OnClientClick = "openTriageDialogAndBlock('RMA Triage', '" + imgTriage.ClientID + "')";

            //ImageButton imgPic = (ImageButton)e.Row.FindControl("imgPic");
            //if (imgPic != null)
            //    imgPic.OnClientClick = "openPictureDialogAndBlock('RMA Picture', '" + imgPic.ClientID + "')";

            Label lblreason = (Label)e.Row.FindControl("lblreason");
            HiddenField hdnReason = (HiddenField)e.Row.FindControl("hdnReason");
            try
            {
                if (hdnReason.Value != null && hdnReason.Value != string.Empty && hdnReason.Value != "0")
                {
                    Hashtable reasonHashtable = rmaUtility.getReasonHashtable();

                    lblreason.Text = reasonHashtable[hdnReason.Value].ToString();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }

            //if (Session["adm"] == null)
            //{
            //    string status = "pending"; // ((DataBoundLiteralControl)e.Row.Cells[4].Controls[0]).Text;
            //    //status = status.Replace("\r\n", "");
            //    //status = status.Replace("\n", "");
            //    //status = status.Trim();
            //    status = lblStatus.Text.Trim();
            //    status = status.Replace("\r\n", "");
            //    status = status.Replace("\n", "");

            //    status = status.ToLower();
            //    if ("pending" != status)
            //    {
            //        ImageButton imgDelDetail1 = e.Row.FindControl("imgDelDetail1") as ImageButton;
            //        if (imgDelDetail1 != null)
            //            imgDelDetail1.Visible = false;

            //    }

            //}

        }

        protected void btnPrintLabel_Click(object sender, EventArgs e)
        {
            int rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
            PrintLabel(rmaGUID);
            //try
            //{
            //    int rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);
            //    string shipMethod = string.Empty;
            //    string labelBase64 = RMATrackingOperation.GetLabelBase64(rmaGUID, out shipMethod);
            //    if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), shipMethod))
            //    {
            //        if (!string.IsNullOrWhiteSpace(labelBase64))
            //        {
            //            imgLabel.ImageUrl = "data:image;base64," + labelBase64;
            //            RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");
            //        }
            //    }
            //    else
            //    {
            //        // RegisterStartupScript("jsUnblockDialog", "closeLabelDialog();");
            //        if (!string.IsNullOrWhiteSpace(labelBase64))
            //        {
            //            //var script = "OpenPDF('" + labelBase64 + "')"; //"window.open('" + pdfByteArray + "', '_blank');";
            //            //ScriptManager.RegisterClientScriptBlock(Parent.Page, typeof(Page), "pdf", script, true);
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenPDF('" + labelBase64 + "')</script>", false);
            //            // imgLabel.ImageUrl = "data:application/pdf;base64," + labelBase64;
            //            //  RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");
            //            //data: application/pdf; base64,
            //            //string fileType = ".pdf";
            //            //string filename = DateTime.Now.Ticks + fileType;
            //            //Response.Clear();
            //            ////Response.ContentType = "application/pdf";
            //            //Response.ContentType = "application/octet-stream";
            //            //Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            //            //Response.Buffer = true;
            //            //Response.Clear();
            //            //var bytes = Convert.FromBase64String(labelBase64);
            //            //Response.OutputStream.Write(bytes, 0, bytes.Length);
            //            //Response.OutputStream.Flush();
            //        }
            //    }


            //}
            //catch (Exception ex)
            //{
            //    lblESN.Text = ex.Message;
            //}
        }

        private void PrintLabel(int rmaGUID)
        {
            try
            {
                SV.Framework.RMA.RMATrackingOperation rmaTrackingOperation = SV.Framework.RMA.RMATrackingOperation.CreateInstance<SV.Framework.RMA.RMATrackingOperation>();

                float width = 320, height = 520, envHeight = 500;
                bool IsManualTracking;

                string shipMethod = string.Empty, ShipPackage = string.Empty;
                string labelBase64 = rmaTrackingOperation.GetLabelBase64(rmaGUID, out shipMethod, out ShipPackage, out IsManualTracking);
                if (!IsManualTracking)
                {
                    lblShipItem.Text = "Cannot print manually created label!";
                }
                else
                {
                    if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), shipMethod))
                    {
                        if (!string.IsNullOrWhiteSpace(labelBase64))
                        {
                            //  imgLabel.ImageUrl = "data:image;base64," + labelBase64;
                            //RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");

                            byte[] imageBytes = Convert.FromBase64String(labelBase64);
                            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);
                            if (shipMethod.ToLower() == "first" && ShipPackage.ToString().ToLower() == "letter")
                            {
                                width = 500;
                                height = 320;
                                envHeight = 320;
                            }
                            //image.ScaleToFit(320, 520);
                            image.ScaleToFit(width, height);
                            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                            {
                                Document document = new Document();
                                iTextSharp.text.Rectangle envelope = new iTextSharp.text.Rectangle(320, 500);
                                document.SetPageSize(envelope);
                                document.SetMargins(0, 0, 0, 0);

                                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                                document.Open();

                                document.Add(image);
                                document.Close();
                                var bytes = memoryStream.ToArray();
                                // memoryStream.Close();

                                string fileType = ".pdf";
                                string filename = DateTime.Now.Ticks + fileType;
                                Response.Clear();
                                //Response.ContentType = "application/pdf";
                                Response.ContentType = "application/octet-stream";
                                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                                Response.Buffer = true;
                                Response.Clear();
                                // var bytes = memSt.ToArray();
                                Response.OutputStream.Write(bytes, 0, bytes.Length);
                                Response.OutputStream.Flush();
                            }
                        }
                    }
                    else
                    {
                        // RegisterStartupScript("jsUnblockDialog", "closeLabelDialog();");
                        if (!string.IsNullOrWhiteSpace(labelBase64))
                        {
                            //var script = "OpenPDF('" + labelBase64 + "')"; //"window.open('" + pdfByteArray + "', '_blank');";
                            //ScriptManager.RegisterClientScriptBlock(Parent.Page, typeof(Page), "pdf", script, true);
                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenPDF('" + labelBase64 + "')</script>", false);
                            // imgLabel.ImageUrl = "data:application/pdf;base64," + labelBase64;
                            //  RegisterStartupScript("jsUnblockDialog", "unblockLabelDialog();");
                            //data: application/pdf; base64,
                            string fileType = ".pdf";
                            string filename = DateTime.Now.Ticks + fileType;
                            Response.Clear();
                            //Response.ContentType = "application/pdf";
                            Response.ContentType = "application/octet-stream";
                            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                            Response.Buffer = true;
                            Response.Clear();
                            var bytes = Convert.FromBase64String(labelBase64);
                            Response.OutputStream.Write(bytes, 0, bytes.Length);
                            Response.OutputStream.Flush();
                            // Response.End();

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                lblESN.Text = ex.Message;
            }
        }

        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }
        public List<SV.Framework.Models.RMA.RMA> Sort<TKey>(List<SV.Framework.Models.RMA.RMA> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<SV.Framework.Models.RMA.RMA>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<SV.Framework.Models.RMA.RMA>();
            }
        }
        protected void gvRma_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["result"] != null)
            {
                List<SV.Framework.Models.RMA.RMA> rmaList = (List<SV.Framework.Models.RMA.RMA>)Session["result"];

                if (rmaList != null && rmaList.Count > 0)
                {
                    
                    if (Sortdir == "ASC")
                    {
                        rmaList = Sort<SV.Framework.Models.RMA.RMA>(rmaList, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        rmaList = Sort<SV.Framework.Models.RMA.RMA>(rmaList, SortExp, SortDirection.Descending);
                    }
                    Session["result"] = rmaList;
                    gvRma.DataSource = rmaList;
                    gvRma.DataBind();
                }
            }
        }

        protected void imgViewRMA_Command(object sender, CommandEventArgs e)
        {
            int rmaGUID = Convert.ToInt32(e.CommandArgument);
            Session["rmaGUID"] = rmaGUID;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('../RMA/RmaView.aspx')</script>", false);

            // Response.Redirect("~/RMA/RmaView.aspx", false);
        }
    }



}
