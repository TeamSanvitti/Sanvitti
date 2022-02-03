using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SV.Framework.Fulfillment;
using SV.Framework.Models.Fulfillment;

namespace avii.PrintLabel
{
    public partial class BatchPrinting : System.Web.UI.Page
    {
        private SV.Framework.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.Fulfillment.PurchaseOrder.CreateInstance<SV.Framework.Fulfillment.PurchaseOrder>();

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
                BindShipBy();
                if (Session["adm"] != null)
                {
                    BindCustomer();
                
                    //companyID = 464;
                }
                else
                {
                    trCustomer.Visible = false;
                }
                //  txtEndDate.Text = DateTime.Now.ToShortDateString();

                //  txtFromDate.Text = DateTime.Now.AddDays(-1).ToShortDateString();

            }

        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();
        }

        private void BindShipBy()
        {
            try
            {
                List<ShipBy> shipBy = purchaseOrderOperation.GetShipByList();
                Session["shipby"] = shipBy;
                dpShipBy.DataSource = shipBy;
                dpShipBy.DataTextField = "ShipByText";
                dpShipBy.DataValueField = "ShipByCode";
                dpShipBy.DataBind();
                System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("", "");
                dpShipBy.Items.Insert(0, item);


            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void BindPO()
        {
            bool validForm = true;
            string fromDate, toDate, poNum, status, ShipVia, poType;
            fromDate = toDate = null;
            poNum = status = ShipVia = poType = string.Empty;
            int statusId = 0, companyID = 0;
            btnGeneratePDF.Visible = false;
            btnGenLable.Visible = false;

            try
            {
                if (Session["adm"] != null)
                {
                    if (dpCompany.SelectedIndex > 0)
                        companyID = Convert.ToInt32(dpCompany.SelectedValue);
                    
                }
                else
                {
                    if (Session["userInfo"] != null)
                    {
                        avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                        if (userInfo != null)
                        {
                            companyID = userInfo.CompanyGUID;
                           // ViewState["companyID"] = companyID;
                        }
                    }
                }

                poNum = txtPONum.Text.Trim();               
                fromDate = txtFromDate.Text.Trim();
                fromDate = string.IsNullOrWhiteSpace(fromDate) ? null : Convert.ToDateTime(fromDate).ToShortDateString(); 
                toDate = txtEndDate.Text.Trim();
                toDate = string.IsNullOrWhiteSpace(toDate) ? null : Convert.ToDateTime(toDate).ToShortDateString();

                statusId = Convert.ToInt32(ddlStatus.SelectedValue);
                ShipVia = dpShipBy.SelectedValue;
                poType = dpPOType.SelectedValue;
                lblMsg.Text = string.Empty;
                
                string sortExpression = "FulfillmentDate";
                string sortDirection = "DESC";
                ViewState["SortDirection"] = sortDirection;
                ViewState["SortExpression"] = sortExpression;
                if (companyID > 0)
                {
                    if (Session["adm"] != null)
                    {
                        if (companyID == 0 && string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(poNum) && statusId == 0 && string.IsNullOrEmpty(ShipVia) && string.IsNullOrWhiteSpace(poType))
                        {
                            validForm = false;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(poNum) && statusId == 0 && string.IsNullOrEmpty(ShipVia) && string.IsNullOrWhiteSpace(poType))
                        {
                            validForm = false;
                        }
                    }
                    if (validForm)
                    {
                        List<FulfillmentLabel> poList = FulfillmentLabelOperation.GetFulfillments(fromDate, toDate, poNum, statusId, ShipVia, poType, companyID);
                        if (poList != null && poList.Count > 0)
                        {
                            gvPO.DataSource = poList;
                            gvPO.DataBind();
                            
                            
                          //  if (Session["adm"] != null)
                            {
                                btnCancel2.Visible = true;
                                btnGeneratePDF.Visible = true;
                                btnGenLable.Visible = true;
                            }
                            if (Session["userInfo"] != null)
                            {
                                avii.Classes.UserInfo objUserInfo = Session["userInfo"] as avii.Classes.UserInfo;

                                if (objUserInfo.CompanyGUID == 470)
                                {
                                    btnCancel2.Visible = false;
                                    btnGeneratePDF.Visible = false;
                                    btnGenLable.Visible = false;
                                }
                            }
                            // if (ddlStatus.SelectedValue == "3")
                            //     btnGenLable.Visible = true;
                            Session["poList"] = poList;
                        }
                        else
                        {
                            lblMsg.Text = "No record exists";
                            Session["poList"] = null;
                            gvPO.DataSource = null;
                            gvPO.DataBind();
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Please select the search criteria";
                        Session["poList"] = null;
                        gvPO.DataSource = null;
                        gvPO.DataBind();
                    }
                }
                else
                {
                    if (Session["adm"] != null)                    
                        lblMsg.Text = "Please select customer";

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPO();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);

        }

        private void GeneratePDF(List<FulfillmentLabel> poList)
        {
            try
            {
                var memoryStream = FulfillmentLabelOperation.LabelsPdf(poList);
                byte[] bytes =  memoryStream.ToArray();
              //  bytes = Combine(bytes1, bytes2);
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
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        
        protected void btnCancel_Click(object sender, EventArgs e)
        {

            ClearAll();
        }
        private void ClearAll()
        {
            btnGeneratePDF.Visible = false;
            btnGenLable.Visible = false;
            dpPOType.SelectedIndex = 0;
            dpShipBy.SelectedIndex = 0;
            lblMsg.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            txtPONum.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            gvPO.DataSource = null;
            gvPO.DataBind();
            lblCount.Text = string.Empty;
            btnCancel2.Visible = false;
            if (Session["adm"] != null)
                dpCompany.SelectedIndex = 0;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);

        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }
        protected void gvPO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                ((CheckBox)e.Row.FindControl("allchk")).Attributes.Add("onclick",
                    "javascript:SelectAll('" +
                    ((CheckBox)e.Row.FindControl("allchk")).ClientID + "')");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                List<ShipBy> shipBy = null;
                DropDownList ddlShipVia = e.Row.FindControl("ddlShipVia") as DropDownList;
                HiddenField hdShipMethod = e.Row.FindControl("hdShipMethod") as HiddenField;
                HiddenField hdShipPackage = e.Row.FindControl("hdShipPackage") as HiddenField;

                


                if (ddlShipVia != null)
                {
                    if (Session["shipBy"] != null)
                    {
                        shipBy = Session["shipBy"] as List<ShipBy>;
                        
                    }
                    else
                    {
                        shipBy = purchaseOrderOperation.GetShipByList();
                        Session["shipby"] = shipBy;
                    }
                    ddlShipVia.DataSource = shipBy;
                    ddlShipVia.DataTextField = "ShipByCode";
                    ddlShipVia.DataValueField = "ShipByCode";
                    ddlShipVia.DataBind();

                    ddlShipVia.SelectedValue = hdShipMethod.Value;
                }
                DropDownList ddlShipPack = e.Row.FindControl("ddlShipPack") as DropDownList;
                if (ddlShipPack != null)
                {
                    ddlShipPack.Items.Clear();
                    string[] itemNames = System.Enum.GetNames(typeof(SV.Framework.Common.LabelGenerator.ShipPackageShape));
                    for (int i = 0; i <= itemNames.Length - 1; i++)
                    {
                        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(itemNames[i], itemNames[i]);
                        ddlShipPack.Items.Add(item);
                    }
                    if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), hdShipMethod.Value))
                    {
                        if (!string.IsNullOrWhiteSpace(hdShipPackage.Value))
                            ddlShipPack.SelectedValue = hdShipPackage.Value;
                        else
                            ddlShipPack.SelectedValue = SV.Framework.Common.LabelGenerator.ShipPackageShape.Letter.ToString();
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(hdShipPackage.Value))
                            ddlShipPack.SelectedValue = hdShipPackage.Value;
                        else
                            ddlShipPack.SelectedValue = SV.Framework.Common.LabelGenerator.ShipPackageShape.package.ToString();
                    }

                    }
                //    //Label lblAE = e.Row.FindControl("lblAE") as Label;
                //    //if(lblAE != null)
                //    //    availableBalance += Convert.ToInt32(lblAE.Text);

                //    LinkButton lnkRequest = e.Row.FindControl("lnkRequest") as LinkButton;
                //    if (lnkRequest != null)
                //    {
                //        lnkRequest.OnClientClick = "openRequestDialogAndBlock('Request data', '" + lnkRequest.ClientID + "')";

                //    }
                //    LinkButton lnkResponse = e.Row.FindControl("lnkResponse") as LinkButton;
                //    if (lnkResponse != null)
                //    {
                //        lnkResponse.OnClientClick = "openResponseDialogAndBlock('Response data', '" + lnkResponse.ClientID + "')";

                //    }


            }

        }
        protected void gvPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPO.PageIndex = e.NewPageIndex;


            if (Session["poList"] != null)
            {
                List<FulfillmentLabel> poList = (List<FulfillmentLabel>)Session["poList"];

                gvPO.DataSource = poList;
                gvPO.DataBind();
            }
            else
            {
                BindPO();
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
        public List<FulfillmentLabel> Sort<TKey>(List<FulfillmentLabel> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<FulfillmentLabel>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<FulfillmentLabel>();
            }
        }
        protected void gvPO_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["poList"] != null)
            {
                List<FulfillmentLabel> poList = (List<FulfillmentLabel>)Session["poList"];

                if (poList != null && poList.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        poList = Sort<FulfillmentLabel>(poList, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        poList = Sort<FulfillmentLabel>(poList, SortExp, SortDirection.Descending);
                    }
                    Session["poList"] = poList;
                    gvPO.DataSource = poList;
                    gvPO.DataBind();
                }
            }
        }

        protected void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            string poNumbers = string.Empty;
            int companyID = 0;
            if (Session["adm"] != null)
            {
                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
            }
            else
            {
                if (Session["userInfo"] != null)
                {
                    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    if (userInfo != null)
                    {
                        companyID = userInfo.CompanyGUID;
                        // ViewState["companyID"] = companyID;
                    }
                }
            }
            foreach (GridViewRow row in gvPO.Rows)
            {
                CheckBox chkPO = row.FindControl("chkPO") as CheckBox;
                HiddenField hdnPOID = row.FindControl("hdnPOID") as HiddenField;
                if (chkPO.Checked)
                {

                    Label lblPoNum = row.FindControl("lblPoNum") as Label;
                    if (string.IsNullOrWhiteSpace(poNumbers))
                        poNumbers = lblPoNum.Text;
                    else
                        poNumbers = poNumbers + "," + lblPoNum.Text;
                }
            }
            if (!string.IsNullOrWhiteSpace(poNumbers))
            {
                List<FulfillmentLabel> poList = FulfillmentLabelOperation.GetLabels(poNumbers, companyID);

                GeneratePDF(poList);
            }
            else
            {
                lblMsg.Text = "Fulfillment number(s) not selected!";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);

        }

        protected void btnGenLable_Click(object sender, EventArgs e)
        {
            bool isSelected = false;
            int PO_ID = 0;
            decimal ShippingWeight = 1;
            string TrackingNumber = string.Empty;
            if (dpCompany.SelectedIndex > 0)
            {
                foreach (GridViewRow row in gvPO.Rows)
                {
                    CheckBox chkPO = row.FindControl("chkPO") as CheckBox;
                    HiddenField hdnPOID = row.FindControl("hdnPOID") as HiddenField;
                    if (chkPO.Checked)
                    {
                        int.TryParse(hdnPOID.Value, out PO_ID);
                        Label lblTrackingNumber = row.FindControl("lblTrackingNumber") as Label;
                        DropDownList ddlShipVia = row.FindControl("ddlShipVia") as DropDownList;
                        DropDownList ddlShipPack = row.FindControl("ddlShipPack") as DropDownList;
                        TextBox txtWeight = row.FindControl("txtWeight") as TextBox;
                        ShippingWeight = Convert.ToDecimal(txtWeight.Text.Trim());
                        TrackingNumber = lblTrackingNumber.Text;
                        if (string.IsNullOrWhiteSpace(TrackingNumber))
                           lblTrackingNumber.Text = GenerateShipmentLabel(ddlShipVia.SelectedValue, ddlShipPack.SelectedValue, PO_ID, ShippingWeight);


                    }

                }

            }
            else
                lblMsg.Text = "Please select customer";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }
        private string GenerateShipmentLabel(string shipMethod, string package, int poid, decimal ShippingWeight)
        {
            string TrackingNumber = string.Empty;
            List<FulfillmentShippingLineItem> listitems = new List<FulfillmentShippingLineItem>();
           // int returnResult = 0;
            int userId = 0;
            int companyID = 0;
            companyID =  Convert.ToInt32(dpCompany.SelectedValue);
            double weight = 1;
            string shipFromContactName = ConfigurationSettings.AppSettings["ShipFromContactName"].ToString();
            string shipFromAddress = ConfigurationSettings.AppSettings["ShipFromAddress"].ToString();
            string shipFromCity = ConfigurationSettings.AppSettings["ShipFromCity"].ToString();
            string shipFromState = ConfigurationSettings.AppSettings["ShipFromState"].ToString();
            string shipFromZip = ConfigurationSettings.AppSettings["ShipFromZip"].ToString();
            string shipFromCountry = ConfigurationSettings.AppSettings["ShipFromCountry"].ToString();
            string shipFromAttn = ConfigurationSettings.AppSettings["ShipFromAttn"].ToString();
            string shipFromPhone = ConfigurationSettings.AppSettings["ShipFromPhone"].ToString();

            SV.Framework.Common.LabelGenerator.EndiciaPrintLabel labelInfo = new SV.Framework.Common.LabelGenerator.EndiciaPrintLabel();

            SV.Framework.Common.LabelGenerator.ShippingLabelOperation shipAccess = new SV.Framework.Common.LabelGenerator.ShippingLabelOperation();
            SV.Framework.Common.LabelGenerator.ShipInfo shipToInfo = new SV.Framework.Common.LabelGenerator.ShipInfo();
            SV.Framework.Common.LabelGenerator.ShipInfo shipFromInfo = new SV.Framework.Common.LabelGenerator.ShipInfo();
            SV.Framework.Fulfillment.ShippingLabelOperation shippingLabelOperation = SV.Framework.Fulfillment.ShippingLabelOperation.CreateInstance<SV.Framework.Fulfillment.ShippingLabelOperation>();


            try
            {
                string shipmentType = "S";
                //if (ViewState["poid"] != null)
                {
                  //  poid = Convert.ToInt32(ViewState["poid"]);

                    if (Session["UserID"] != null)
                    {
                        int.TryParse(Session["UserID"].ToString(), out userId);
                    }
                    List<FulfillmentLabel> poList = Session["poList"] as List<FulfillmentLabel>;
                   
                    if (poList != null && poList.Count > 0)
                    {

                        var po = (from item in poList where item.POID.Equals(poid) select item).ToList();
                        if (po != null && po.Count > 0)
                        {
                            labelInfo.FulfillmentNumber = po[0].FulfillmentNumber;
                            labelInfo.LabelPrintDateTime = DateTime.Today;
                            //shipToInfo
                            shipToInfo.ShipToAddress = po[0].StreetAddress1;
                            shipToInfo.ShipToAddress2 = po[0].StreetAddress2;
                            shipToInfo.ContactName = po[0].ContactName;
                            shipToInfo.ShipToCity = po[0].ShipToCity;
                            shipToInfo.ShipToState = po[0].ShipToState;
                            shipToInfo.ShipToZip = po[0].ShipToZip;
                            shipToInfo.ShipToAttn = po[0].ContactName;
                            shipToInfo.ContactPhone = po[0].ContactPhone;
                            labelInfo.ShipTo = shipToInfo;
                            shipFromContactName = po[0].CompanyName;
                            //ship From Info
                            shipFromInfo.ShipToAddress = shipFromAddress;
                            shipFromInfo.ShipToAddress2 = "";
                            shipFromInfo.ContactName = shipFromContactName;
                            shipFromInfo.ShipToCity = shipFromCity;
                            shipFromInfo.ShipToState = shipFromState;
                            shipFromInfo.ShipToZip = shipFromZip;
                            shipFromInfo.ShipToAttn = shipFromContactName;
                            shipFromInfo.ShipToCountry = shipFromCountry;
                            shipFromInfo.ContactPhone = shipFromPhone;

                            labelInfo.ShipFrom = shipFromInfo;
                            //
                            labelInfo.PackageWeight = new SV.Framework.Common.LabelGenerator.Weight { units = "ounces", value = weight };
                            labelInfo.PackageContent = "Description";

                            //labelInfo.ShippingMethod = SV.Framework.Common.LabelGenerator.ShipMethods.Priority;
                            if (!string.IsNullOrEmpty(shipMethod))
                            {
                                Enum.TryParse(shipMethod, out SV.Framework.Common.LabelGenerator.ShipMethods shipMethods);
                                labelInfo.ShippingMethod = shipMethods;
                            }

                            labelInfo.ShippingType = SV.Framework.Common.LabelGenerator.ShipType.Ship;


                            if (!string.IsNullOrEmpty(package))
                            {
                                Enum.TryParse(package, out SV.Framework.Common.LabelGenerator.ShipPackageShape shipPackage);
                                labelInfo.PackageShape = shipPackage;
                            }
                            else
                                labelInfo.PackageShape = SV.Framework.Common.LabelGenerator.ShipPackageShape.Flat;

                            SV.Framework.Admin.APIAddressInfo aPIAddressInfo = null;
                            //int companyID = Convert.ToInt32(po[0].CompanyID);
                            if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.EndiciaShipMethods), labelInfo.ShippingMethod.ToString()))
                            {
                                aPIAddressInfo = SV.Framework.Admin.CustomerOperations.GetCustomerAPIAddress(companyID, "Endicia");
                                SV.Framework.Common.LabelGenerator.iEndiciaLabelCredentials request2 = new SV.Framework.Common.LabelGenerator.EndiciaCredentials();
                                request2.ConnectionString = aPIAddressInfo.ConnectionString;

                                SV.Framework.Common.LabelGenerator.iEndiciaLabelCredentials iEndiciaLabelCredentials = shipAccess.GetIEndiciaLabelCredentials(request2);
                                labelInfo.AccountID = iEndiciaLabelCredentials.AccountID;
                                labelInfo.RequesterID = iEndiciaLabelCredentials.RequesterID;
                                labelInfo.PassPharase = iEndiciaLabelCredentials.PassPharase;
                                labelInfo.ApiURL = aPIAddressInfo.APIAddress;
                            }
                            else if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.ShipStationsShipMethods), labelInfo.ShippingMethod.ToString()))
                            {
                                aPIAddressInfo = SV.Framework.Admin.CustomerOperations.GetCustomerAPIAddress(companyID, "ShipStation");
                                labelInfo.ApiURL = aPIAddressInfo.APIAddress;
                                labelInfo.AuthString = aPIAddressInfo.ConnectionString;
                            }


                            string ShipDate = DateTime.Now.ToString();
                            string Package = package;
                            string ShipVia = shipMethod;
                            //decimal Weight = 1;
                            string Comments = string.Empty;
                            decimal FinalPostage = 0;
                            bool IsManualTracking = false;
                            SV.Framework.Common.LabelGenerator.iPrintLabel response = shipAccess.PrintShippingLabel(labelInfo);
                            List<CustomValues> customValues = new List<CustomValues>();
                            if (response != null && !string.IsNullOrWhiteSpace(response.TrackingNumber))
                            {
                                ShippingLabelInfo request = new ShippingLabelInfo();
                                request.FulfillmentNumber = labelInfo.FulfillmentNumber;
                                request.ShipmentType = shipmentType;
                                request.ShippingLabelImage = response.ShippingLabelImage;
                                request.TrackingNumber = response.TrackingNumber;
                                FinalPostage = response.FinalPostage;
                                //request.LineItems = listitems;
                                TrackingNumber = response.TrackingNumber;

                                ShippingLabelResponse setResponse = shippingLabelOperation.ShippingLabelUpdateNew(request, userId, listitems, ShipDate, Package, ShipVia, ShippingWeight, Comments, FinalPostage, IsManualTracking, poid, customValues);
                                //lblShipItem.Text = "Label generated successfully";
                               // returnResult = 1;
                            }
                            else
                            {
                                TrackingNumber = response.PackageContent;
                                //if (!string.IsNullOrWhiteSpace(response.PackageContent))
                                //    lblMsg.Text = response.PackageContent;
                                //else
                                //    lblMsg.Text = "Technical error please try again!";


                            }

                        }


                    }

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                TrackingNumber = ex.Message;
            }
            return TrackingNumber;
        }

        protected void btnCancel2_Click(object sender, EventArgs e)
        {
            btnGeneratePDF.Visible = false;
            btnGenLable.Visible = false;
            btnCancel2.Visible = false;
            lblMsg.Text = string.Empty;
            gvPO.DataSource = null;
            gvPO.DataBind();
            lblCount.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }
    }
}