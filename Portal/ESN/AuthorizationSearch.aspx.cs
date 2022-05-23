//using avii.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml.Serialization;
using SV.Framework.Models.Inventory;
namespace avii.ESN
{
    public partial class AuthorizationSearch : System.Web.UI.Page
    {
        private SV.Framework.Inventory.EsnAuthorizationOperation EsnAuthorizationOperation = SV.Framework.Inventory.EsnAuthorizationOperation.CreateInstance<SV.Framework.Inventory.EsnAuthorizationOperation>();
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
            if (!IsPostBack)
            {

                BindCustomer();


            }
        }

        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();
        }

        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSKU.Items.Clear();
            lblMsg.Text = "";
            int companyID = 0;

            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
                List<KittedSKUs> skuList = EsnAuthorizationOperation.GetESNAuthorizedSKUs(companyID);
                if (skuList != null && skuList.Count > 0)
                {
                    ddlSKU.DataSource = skuList;
                    ddlSKU.DataTextField = "KittedSKU";
                    ddlSKU.DataValueField = "ItemCompanyGUID";
                    ddlSKU.DataBind();
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("", "0");
                    ddlSKU.Items.Insert(0, item);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // dpCompany.SelectedIndex = 0;
            //ddlSKU.Items.Clear();
            string runNumber = txtRunNumber.Text.Trim();
            lblMsg.Text = "";
            lblCount.Text = "";
            gvAuth.DataSource = null;
            gvAuth.DataBind();
            //btnDownload.Visible = false;
            Session["authList"] = null;
            string dateFrom = "", dateTo = "";
            int companyID = 0, ItemCompanyGUID = 0;
            string sortExpression = "CreateDate";
            string sortDirection = "DESC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;

            if (dpCompany.SelectedIndex > 0)
            {
                int.TryParse(dpCompany.SelectedValue, out companyID);
                if (ddlSKU.SelectedIndex > 0)
                    int.TryParse(ddlSKU.SelectedValue, out ItemCompanyGUID);

                dateFrom = txtDateFrom.Text.Trim();
                dateTo = txtDateTo.Text.Trim();
                List<ESNAuthorizatedInfo> authList = EsnAuthorizationOperation.GetESNAuthorizedSearch(companyID, ItemCompanyGUID, dateFrom, dateTo, runNumber);
                if (authList != null && authList.Count > 0)
                {
                    Session["authList"] = authList;

                    lblCount.Text = "<strong>Total Count:</strong> " + authList.Count;
                    gvAuth.DataSource = authList;
                    gvAuth.DataBind();

                    // btnDownload.Visible = true;

                }
                else
                    lblMsg.Text = "No record found";
            }
            else
            {
                lblMsg.Text = "Customer is required!";
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            dpCompany.SelectedIndex = 0;
            ddlSKU.Items.Clear();
            lblMsg.Text = "";
            lblCount.Text = "";
            txtRunNumber.Text = "";
            txtDateFrom.Text = "";
            txtDateTo.Text = "";
            gvAuth.DataSource = null;
            gvAuth.DataBind();
            // btnDownload.Visible = false;
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {

        }

        protected void gvAuth_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lblAE = e.Row.FindControl("lblAE") as Label;
                //if(lblAE != null)
                //    availableBalance += Convert.ToInt32(lblAE.Text);

                LinkButton lnkESNDATA = e.Row.FindControl("lnkESNDATA") as LinkButton;
                if (lnkESNDATA != null)
                {
                    lnkESNDATA.OnClientClick = "openRequestDialogAndBlock('ESN data', '" + lnkESNDATA.ClientID + "')";

                }



            }

        }

        //protected void imgView_Command(object sender, CommandEventArgs e)
        //{
        //    int ESNAuthorizationID = Convert.ToInt32(e.CommandArgument);
        //    if(ESNAuthorizationID > 0)
        //    {
        //        List<ESNAuthorizatedInfo> authList = Session["authList"] as List<ESNAuthorizatedInfo>;
        //        if(authList != null && authList.Count > 0)
        //        {
        //            lblESNData.Text = 
        //        }



        //    }
        //}

        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }
        protected void lnkESNDATA_Command(object sender, CommandEventArgs e)
        {
            try
            {
                lblESNData.Text = "";
                int ESNAuthorizationID = Convert.ToInt32(e.CommandArgument);

                List<ESNAuthorizatedInfo> authList = Session["authList"] as List<ESNAuthorizatedInfo>;

                if (authList != null && authList.Count > 0)
                {
                    var newList = (from item in authList where item.ESNAuthorizationID.Equals(ESNAuthorizationID) select item).ToList();


                    if (newList != null && newList.Count > 0)
                    {
                        lblESNData.Text = newList[0].ESNDATA;

                    }
                    else
                        lblESNData.Text = "No data found";
                }
                else
                    lblESNData.Text = "No data found";
                RegisterStartupScript("jsUnblockDialog", "unblockRequestDialog();");
            }
            catch (Exception ex)
            {
                lblESNData.Text = ex.Message;
            }

        }

        protected void lnkESNDownload_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int itr = 0;
                string ESNData = "", newLine="";
                string RunNumber = "";
                int ESNAuthorizationID = Convert.ToInt32(e.CommandArgument);

                List<ESNAuthorizatedInfo> authList = Session["authList"] as List<ESNAuthorizatedInfo>;

                if (authList != null && authList.Count > 0)
                {
                    var newList = (from item in authList where item.ESNAuthorizationID.Equals(ESNAuthorizationID) select item).ToList();


                    if (newList != null && newList.Count > 0)
                    {
                        ESNData = newList[0].ESNXml;
                        ViewState["RunNumber"] = newList[0].RunNumber;
                        ViewState["qty"] = newList[0].EsnCount;

                        var xmlSer = new XmlSerializer(typeof(List<EsnUploadNew>), new XmlRootAttribute("ArrayOfEsnUploadNew"));
                        var stringReader = new StringReader(ESNData);

                        List<EsnUploadNew> esnList = xmlSer.Deserialize(stringReader) as List<EsnUploadNew>;

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        //string string2CSV = "";// "BATCH,ESN,MeidHex,MeidDec,Location,MSL,OTKSL,SerialNumber" + Environment.NewLine;
                        //  sb.Append("ESN" + Environment.NewLine);
                        itr = 1;
                        foreach (EsnUploadNew item in esnList)
                        {
                            newLine = esnList.Count == itr ? "" : Environment.NewLine ;
                            sb.Append(item.ESN + newLine);
                            itr = itr + 1;
                        }

                        Session["string2ESNCSV"] = sb.ToString();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "temp1", "<script language='javascript'>RefreshESN();</script>", false);
                    }
                    else
                        lblMsg.Text = "No data found";
                }
                else
                    lblMsg.Text = "No data found";


            }
            catch (Exception ex)
            {
                lblESNData.Text = ex.Message;
            }

        }

        private void DownloadAuth(int ESNAuthorizationID)
        {
            int SequenceNumber = 0;
            string sku = "", SWVersion = "", productType = "";
            string ESNData = "";
            //int ESNAuthorizationID = Convert.ToInt32(e.CommandArgument);

            List<ESNAuthorizatedInfo> authList = Session["authList"] as List<ESNAuthorizatedInfo>;

            List<ESNAuthorization> ESNs = new List<ESNAuthorization>();
            ESNAuthorization esnInfo = null;

            if (authList != null && authList.Count > 0)
            {
                var newList = (from item in authList where item.ESNAuthorizationID.Equals(ESNAuthorizationID) select item).ToList();

                if (newList != null && newList.Count > 0)
                {
                    ESNData = newList[0].ESNXml;
                    sku = newList[0].KittedSKU;
                    SWVersion = newList[0].SWVersion;
                    SequenceNumber = newList[0].SquenceNumber;
                    productType = newList[0].ProductType;
                    
                    var xmlSer = new XmlSerializer(typeof(List<EsnUploadNew>), new XmlRootAttribute("ArrayOfEsnUploadNew"));
                    var stringReader = new StringReader(ESNData);

                    List<EsnUploadNew> esnList = xmlSer.Deserialize(stringReader) as List<EsnUploadNew>;
                    foreach (EsnUploadNew ITEM in esnList)
                    {
                        esnInfo = new ESNAuthorization();
                        esnInfo.ESN = ITEM.ESN;
                        esnInfo.IMEI2 = ITEM.IMEI2;
                        esnInfo.MeidDec = ITEM.MeidDec;
                        esnInfo.MeidHex = ITEM.MeidHex;
                        esnInfo.MSL = string.IsNullOrEmpty(ITEM.MSL) ? "000000" : ITEM.MSL;
                        esnInfo.OTKSL = string.IsNullOrEmpty(ITEM.OTKSL) ? "000000" : ITEM.OTKSL;
                        esnInfo.SKU = sku;
                        esnInfo.SKUName = sku;
                        esnInfo.SWVersion = SWVersion;
                        esnInfo.ManfId = newList[0].ManufactureCode;
                        esnInfo.ManfName = newList[0].DisplayName;
                        ESNs.Add(esnInfo);
                    }

                    var memoryStream = new MemoryStream();
                    //   System.Xml.XmlWriter write =  new   ;
                    string fileName;
                    string filePrefix = "spdish";
                    //string filePrefix = "spappledsh";
                    string transDate;

                    // string fileName = filePrefix + "_" + transDate + "_" + edfFileInfo.fileSequence.ToString() + ".xml";
                    DateTime dt = DateTime.Now;
                   // string currentDate = dt.ToString("yyyy-MM-dd");

                    DateTime currentUtcDateTime = DateTime.UtcNow;
                    DateTime currentCSTDateTime = TimeZoneInfo.ConvertTime(currentUtcDateTime, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));

                    transDate = currentCSTDateTime.ToString("yyyy-MM-dd");

                    // Int64 fileSequence = dt.Ticks;
                    string validateReturnMessage = "";
                    string filePath = Server.MapPath("~/UploadedData/");

                    if (ESNs != null && ESNs.Count > 0)
                    {
                        if (EsnAuthorizationOperation.ValidateUEDFFileData(ESNs, SequenceNumber, out validateReturnMessage))
                        {
                            //transDate = currentDate.Replace("-", "");
                            transDate = transDate.Replace("-", "");
                            fileName = filePrefix + "_" + transDate + "_" + SequenceNumber.ToString() + ".xml";
                            filePath = filePath + fileName;

                            XElement xmlElement = EsnAuthorizationOperation.CreateAuthorizationFile(ESNs, SequenceNumber.ToString(), transDate, productType);

                            xmlElement.Save(filePath);

                            lblMsg.Text = "Generated successfully";
                            // tring strFullPath = Server.MapPath("~/temp.xml");
                            string strContents = null;
                            System.IO.StreamReader objReader = default(System.IO.StreamReader);
                            objReader = new System.IO.StreamReader(filePath);
                            strContents = objReader.ReadToEnd();
                            objReader.Close();

                            string attachment = "attachment; filename=" + fileName;
                            Response.ClearContent();
                            Response.ContentType = "application/xml";
                            Response.AddHeader("content-disposition", attachment);
                            Response.Write(strContents);
                            Response.End();
                        }
                        else
                            lblMsg.Text = validateReturnMessage;

                    }
                    else
                    {
                        lblMsg.Text = "No data found.";
                    }

                    //Session["string2CSV"] = sb.ToString();


                }
                else
                    lblMsg.Text = "No data found";
            }
            else
                lblMsg.Text = "No data found";


        }
        protected void lnkDownload_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int ESNAuthorizationID = Convert.ToInt32(e.CommandArgument);
                ViewState["ESNAuthorizationID"] = ESNAuthorizationID;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp1", "<script language='javascript'>RefreshAuthXML();</script>", false);
                //  DownloadAuth(ESNAuthorizationID);


            }
            catch (Exception ex)
            {
                lblESNData.Text = ex.Message;
            }

        }

        protected void ESNData_Command(object sender, CommandEventArgs e)
        {
            try
            {
                string ESNData = "";
                int ESNAuthorizationID = Convert.ToInt32(e.CommandArgument);

                List<ESNAuthorizatedInfo> authList = Session["authList"] as List<ESNAuthorizatedInfo>;

                if (authList != null && authList.Count > 0)
                {
                    var newList = (from item in authList where item.ESNAuthorizationID.Equals(ESNAuthorizationID) select item).ToList();


                    if (newList != null && newList.Count > 0)
                    {
                        ESNData = newList[0].ESNXml;

                        var xmlSer = new XmlSerializer(typeof(List<EsnUploadNew>), new XmlRootAttribute("ArrayOfEsnUploadNew"));
                        var stringReader = new StringReader(ESNData);

                        List<EsnUploadNew> esnList = xmlSer.Deserialize(stringReader) as List<EsnUploadNew>;

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        //string string2CSV = "";// "BATCH,ESN,MeidHex,MeidDec,Location,MSL,OTKSL,SerialNumber" + Environment.NewLine;
                        sb.Append("Seq.No.,BATCH,ESN1,ESN2,MeidHex,MeidDec,Location,MSL,OTKSL,SerialNumber,BoxID" + Environment.NewLine);
                        foreach (EsnUploadNew item in esnList)
                        {
                            sb.Append(item.SNo + "," + item.MslNumber + "," + item.ESN + "," + item.IMEI2 + "," + item.MeidHex + "," + item.MeidDec + "," + item.Location + "," + item.MSL + "," + item.OTKSL + "," + item.SerialNumber + "," + item.BoxID + Environment.NewLine);
                        }

                        Session["string2CSV"] = sb.ToString();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "temp1", "<script language='javascript'>Refresh();</script>", false);
                    }
                    else
                        lblMsg.Text = "No data found";
                }
                else
                    lblMsg.Text = "No data found";


            }
            catch (Exception ex)
            {
                lblESNData.Text = ex.Message;
            }

        }

        protected void btnhdESNDownload_Click(object sender, EventArgs e)
        {
            string runNumber = "";// Convert.ToString(ViewState["RunNumber"]);
            if (ViewState["RunNumber"] != null)
                runNumber = Convert.ToString(ViewState["RunNumber"]);
            string fileName = "nms_bounce_dish_to_tmo_";
            //string runNumber = txtRunNumber.Text.Trim();
            DateTime currentDate = DateTime.Now;
            int qty = 0;
            if (ViewState["qty"] != null)
                qty = Convert.ToInt32(ViewState["qty"]);

            string date = Convert.ToDateTime(currentDate).ToString("yyyyMMdd");
            fileName = fileName + runNumber + "_" + qty + "_" + date + ".txt";
            string string2CSV = Session["string2ESNCSV"] as string;
            // string fileName = "";
            Session["string2ESNCSV"] = null;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(string2CSV);
            Response.Flush();
            Response.End();

            //Session["string2ESNCSV"]
        }
        protected void btnhdDownload_Click(object sender, EventArgs e)
        {
            string string2CSV = Session["string2CSV"] as string;
            Session["string2CSV"] = null;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=InventoryReceive.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(string2CSV);
            Response.Flush();
            Response.End();
        }
        protected void btnhdAuth_Click(object sender, EventArgs e)
        {
            int ESNAuthorizationID = Convert.ToInt32(ViewState["ESNAuthorizationID"]);
            //ViewState["ESNAuthorizationID"] = ESNAuthorizationID;
            DownloadAuth(ESNAuthorizationID);
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
        public List<ESNAuthorizatedInfo> Sort<TKey>(List<ESNAuthorizatedInfo> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<ESNAuthorizatedInfo>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<ESNAuthorizatedInfo>();
            }
        }
        protected void gvAuth_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["authList"] != null)
            {
                List<ESNAuthorizatedInfo> authList = (List<ESNAuthorizatedInfo>)Session["authList"];

                if (authList != null && authList.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        authList = Sort<ESNAuthorizatedInfo>(authList, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        authList = Sort<ESNAuthorizatedInfo>(authList, SortExp, SortDirection.Descending);

                    }
                    Session["authList"] = authList;
                    gvAuth.DataSource = authList;
                    gvAuth.DataBind();
                }
            }
        }

        protected void gvAuth_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAuth.PageIndex = e.NewPageIndex;
            if (Session["authList"] != null)
            {
                List<ESNAuthorizatedInfo> authList = (List<ESNAuthorizatedInfo>)Session["authList"];

                gvAuth.DataSource = authList;
                gvAuth.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }

        protected void lnkPOSLabel_Command(object sender, CommandEventArgs e)
        {
            string ESNData = "";
            int ESNAuthorizationID = Convert.ToInt32(e.CommandArgument);
            ViewState["ESNAuthorizationID"] = ESNAuthorizationID;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp1", "<script language='javascript'>RefreshPOS();</script>", false);


        }
        protected void btnhdPOS_Click(object sender, EventArgs e)
        {
            int ESNAuthorizationID = Convert.ToInt32(ViewState["ESNAuthorizationID"]);
            //ViewState["ESNAuthorizationID"] = ESNAuthorizationID;
            CreatePOSLabels(ESNAuthorizationID);
        }
        private void CreatePOSLabels(int ESNAuthorizationID)
        {
            int companyID = 0;
            string ESNData = "";
            SV.Framework.LabelGenerator.DishLabelOperation dishLabelOperation = new SV.Framework.LabelGenerator.DishLabelOperation();
            SV.Framework.Fulfillment.DishLabelOperations dishLabelOperations = SV.Framework.Fulfillment.DishLabelOperations.CreateInstance<SV.Framework.Fulfillment.DishLabelOperations>();

            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            }

            List<ESNAuthorizatedInfo> authList = Session["authList"] as List<ESNAuthorizatedInfo>;

            if (authList != null && authList.Count > 0)
            {
                var newList = (from item in authList where item.ESNAuthorizationID.Equals(ESNAuthorizationID) select item).ToList();


                if (newList != null && newList.Count > 0)
                {
                    ESNData = newList[0].ESNXml;

                    var xmlSer = new XmlSerializer(typeof(List<EsnUploadNew>), new XmlRootAttribute("ArrayOfEsnUploadNew"));
                    var stringReader = new StringReader(ESNData);

                    List<EsnUploadNew> esnList = xmlSer.Deserialize(stringReader) as List<EsnUploadNew>;
                    List<SV.Framework.LabelGenerator.PosKitInfo> posKITs = new List<SV.Framework.LabelGenerator.PosKitInfo>();
                    SV.Framework.LabelGenerator.PosKitInfo posKitInfo = default;
                    DataTable dt = dishLabelOperations.ESNDataNew(esnList);

                    List<SV.Framework.Models.Fulfillment.PosKitInfo> posKITsdb = dishLabelOperations.GetPosLabels(companyID, ESNAuthorizationID, dt);
                    List<SV.Framework.LabelGenerator.KitBoxInfo> kitBoxInfos = default;
                    SV.Framework.LabelGenerator.KitBoxInfo kitBoxInfo = default;

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "stop loader", "StopProgress()", true);
                    if (posKITsdb != null && posKITsdb.Count > 0)
                    {
                        foreach(SV.Framework.Models.Fulfillment.PosKitInfo item in posKITsdb)
                        {
                            kitBoxInfos = new List<SV.Framework.LabelGenerator.KitBoxInfo>();
                            
                            posKitInfo = new SV.Framework.LabelGenerator.PosKitInfo();
                            posKitInfo.CompanyName = item.CompanyName;
                            posKitInfo.ESN = item.ESN;
                            posKitInfo.IMEI2 = item.IMEI2;
                            posKitInfo.HEX = item.HEX;
                            posKitInfo.HWVersion = item.HWVersion;
                            posKitInfo.ICCID = item.ICCID;
                            posKitInfo.ItemName = item.ItemName;
                            posKitInfo.MEID = item.MEID;
                            posKitInfo.OSType = item.OSType;
                            posKitInfo.ProductType = item.ProductType;
                            posKitInfo.SerialNum = item.SerialNum;
                            posKitInfo.ShipDate = item.ShipDate;
                            posKitInfo.SKU = item.SKU;
                            posKitInfo.SWVersion = item.SWVersion;
                            posKitInfo.UPC = item.UPC;
                            foreach (SV.Framework.Models.Fulfillment.KitBoxInfo kitBoxInfodb in item.KitBoxList)
                            {
                                kitBoxInfo = new SV.Framework.LabelGenerator.KitBoxInfo();
                                kitBoxInfo.DisplayName = kitBoxInfodb.DisplayName;
                                kitBoxInfo.OriginCountry = kitBoxInfodb.OriginCountry;
                                kitBoxInfos.Add(kitBoxInfo);
                            }
                            posKitInfo.KitBoxList = kitBoxInfos;

                            posKITs.Add(posKitInfo);
                        }
                        if (posKITs != null && posKITs.Count > 0)
                        {
                            string ProductType = posKITs[0].ProductType;
                            //if (ProductType.ToUpper() == "H3")
                            MemoryStream memSt = null;// = new MemoryStream();

                            if (ProductType.ToUpper() == "H5")
                            {
                                SV.Framework.LabelGenerator.H5LabelOperation h5LabelOperation = new SV.Framework.LabelGenerator.H5LabelOperation();

                                memSt = h5LabelOperation.POSKITLabelPdfTarCode(posKITs);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "stop loader", "StopProgress()", true);
                            }
                            else if (posKITs[0].OSType.ToUpper() == "ANDROID")
                            {
                                SV.Framework.LabelGenerator.H3LabelOperation h3LabelOperation = new SV.Framework.LabelGenerator.H3LabelOperation();

                                memSt = h3LabelOperation.POSKITLabelPdfTarCode(posKITs);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "stop loader", "StopProgress()", true);
                                //var memSt = slabel.ExportToPDF(models);
                                
                            }
                            else
                            {
                                 memSt = dishLabelOperation.POSKITLabelPdfTarCode(posKITs);

                                //if (memSt != null)
                                //{
                                //    string fileType = ".pdf";
                                //    string filename = DateTime.Now.Ticks + fileType;
                                //    Response.Clear();
                                //    //Response.ContentType = "application/pdf";
                                //    Response.ContentType = "application/octet-stream";
                                //    Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                                //    Response.Buffer = true;
                                //    Response.Clear();
                                //    var bytes = memSt.ToArray();
                                //    Response.OutputStream.Write(bytes, 0, bytes.Length);
                                //    Response.OutputStream.Flush();
                                //    lblMsg.Text = "Label generated successfully.";
                                //}
                            }
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
                                lblMsg.Text = "Label generated successfully.";
                            }
                        }
                        else
                            lblMsg.Text = "No record found";
                    }
                    else
                        lblMsg.Text = "No record found";
                }
            }
        }
    }
}