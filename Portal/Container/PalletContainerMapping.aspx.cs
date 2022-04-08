using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using SV.Framework.Fulfillment;
using SV.Framework.Models.Fulfillment;

namespace avii.Container
{
    public partial class PalletContainerMapping : System.Web.UI.Page
    {
        private SV.Framework.Fulfillment.ContainerOperation containerOperation = SV.Framework.Fulfillment.ContainerOperation.CreateInstance<SV.Framework.Fulfillment.ContainerOperation>();

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
                Response.Redirect(url);
            }
            if (!IsPostBack)
            {
                BindCustomers();
               // tblPallet.Visible = false;
                //tblContainer.Visible = false;
            }

        }
        protected void BindCustomers()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 1);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPO();
        }
        private void BindPO()
        {
            rptMapping.DataSource = null;
            rptMapping.DataBind();
            lblMsg.Text = "";
            btnSubmit.Visible = false;
            btnSubmit.Enabled = true;
            btnDelete.Visible = false;
            btnMapping.Visible = false;
            btnBoxID.Visible = true;

            int companyID = 0, poid = 0;//, contanierToGenrate = 0, StatusID = 0;
            if (dpCompany.SelectedIndex > 0)
            {
                int.TryParse(dpCompany.SelectedValue, out companyID);

                //ViewState["containercount"] = null;
                //ViewState["palletcount"] = null;
                lblMsg.Text = string.Empty;
                //int numberOfContainers = 0, poid = 0, casePackQuantity = 0, numberOfPallets = 0;
                string fulfillmentNumber = txtPoNum.Text.Trim();
                string trackingNumber;//, fulfillmentNo;
                trackingNumber = txtTrackingNo.Text.Trim();
                //fulfillmentNo = string.Empty;
                if (string.IsNullOrEmpty(fulfillmentNumber) && string.IsNullOrEmpty(trackingNumber))
                {
                    lblMsg.Text = "Please select fulfillment number or tracking number";
                    return;
                }
                //List<FulfillmentContainer> containers = null;
                List<FulfillmentPallet> pallets = null;
                string palletID = string.Empty;
                List<FulfillmentContainer> containers = containerOperation.GetPalletContainersSearch(companyID, fulfillmentNumber, trackingNumber, out pallets);
                if (containers != null && containers.Count > 0)
                {
                    Session["pallets"] = pallets;
                    foreach (FulfillmentContainer item in containers)
                    {
                        poid = item.POID;
                        palletID = item.PalletID;
                    }                    
                    rptMapping.DataSource = containers;
                    rptMapping.DataBind();
                    if (poid > 1)
                    {
                        btnSubmit.Visible = true;
                        btnSubmit.Enabled = true;
                    }

                        btnCancel1.Visible = true;                    
                    

                    if(!string.IsNullOrEmpty(palletID) )
                    {

                        if (companyID == 470)
                        {
                            btnBoxID.Visible = true;
                            btnMapping.Visible = true;
                        }
                        if (poid > 1)
                            btnDelete.Visible = true;                        
                    }


                    if(pallets != null && pallets.Count > 0)
                    {
                        poid = pallets[0].POID;

                    }
                    ViewState["poid"] = poid;

                    //if (StatusID == 3)
                    //{
                    //    lblMsg.Text = "Fulfillment number already shipped";
                    //    btnDelete.Visible = false;
                    //    btnSubmit.Visible = false;
                    //    btnCancel1.Visible = false;
                    //}
                    //}
                }
                else
                {
                    rptMapping.DataSource = null;
                    rptMapping.DataBind();
                    lblMsg.Text = "No record found";
                }
            }
            else
            {
                rptMapping.DataSource = null;
                rptMapping.DataBind();
                lblMsg.Text = "Please select customer";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            txtPoNum.Text = string.Empty;
            txtTrackingNo.Text = string.Empty;
            btnSubmit.Enabled = true;
            btnSubmit.Visible = false;
            btnCancel1.Visible = false;
            btnDelete.Visible = false;
            rptMapping.DataSource = null;
            rptMapping.DataBind();
            dpCompany.SelectedIndex = 0;
            btnMapping.Visible = false;
            btnBoxID.Visible = false;

        }
        protected void rptMapping_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                List<FulfillmentPallet> pallets = new List<FulfillmentPallet>();
                pallets = Session["pallets"] as List<FulfillmentPallet>;
                DropDownList ddlPallet = (DropDownList)e.Item.FindControl("ddlPallet");
                HiddenField hdPalletID = (HiddenField)e.Item.FindControl("hdPalletID");
                if (ddlPallet != null)
                {

                    ddlPallet.DataSource = pallets;
                    ddlPallet.DataTextField = "PalletID";
                    ddlPallet.DataValueField = "PalletID";
                    ddlPallet.DataBind();
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("", "");
                    ddlPallet.Items.Insert(0, item);

                    if (pallets != null && pallets.Count == 1)
                        ddlPallet.SelectedIndex = 1;
                    else
                    {
                        if (hdPalletID != null)
                            ddlPallet.SelectedValue = hdPalletID.Value;
                    }

                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int poid = 0, userID = 0;
            List<PalletContainersMapping> mappings = new List<PalletContainersMapping>();
            PalletContainersMapping palletContainerMapping = null;
            if (ViewState["poid"] != null)
                poid = Convert.ToInt32(ViewState["poid"]);
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
            }
            foreach (RepeaterItem item in rptMapping.Items)
            {
                palletContainerMapping = new PalletContainersMapping();
                Label lblContainer = item.FindControl("lblContainer") as Label;
                DropDownList ddlPallet = item.FindControl("ddlPallet") as DropDownList;
                palletContainerMapping.ContainerID = lblContainer.Text;
                if (ddlPallet.SelectedIndex > 0)
                    palletContainerMapping.PalletID = ddlPallet.SelectedValue;
                else
                {
                    lblMsg.Text = "Pallet ID is required!";
                    return;
                }
                mappings.Add(palletContainerMapping);
            }
            if(mappings != null && mappings.Count > 0)
            {
                string response = containerOperation.PalletContainersMappingInsertUpdate(poid, userID, mappings);
                lblMsg.Text = response;
                if ("Submitted successfully" == response)
                    btnSubmit.Enabled = false;
            }
            else
            {
                lblMsg.Text = "No data to submit";

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int poid = 0, userID = 0;
            List<PalletContainersMapping> mappings = new List<PalletContainersMapping>();
            PalletContainersMapping palletContainerMapping = null;
            if (ViewState["poid"] != null)
                poid = Convert.ToInt32(ViewState["poid"]);
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
            }
            foreach (RepeaterItem item in rptMapping.Items)
            {
                palletContainerMapping = new PalletContainersMapping();
                Label lblContainer = item.FindControl("lblContainer") as Label;
                DropDownList ddlPallet = item.FindControl("ddlPallet") as DropDownList;
                palletContainerMapping.ContainerID = lblContainer.Text;
                if (ddlPallet.SelectedIndex > 0)
                    palletContainerMapping.PalletID = ddlPallet.SelectedValue;
                else
                {
                    lblMsg.Text = "No data to delete";
                    return;
                }
                mappings.Add(palletContainerMapping);
            }
            if (mappings != null && mappings.Count > 0)
            {
                string response = containerOperation.PalletContainersMappingDelete(poid, userID, mappings);
                lblMsg.Text = response;
                btnDelete.Visible = false;
            }
            else
            {
                lblMsg.Text = "No data to delete";

            }
        }

        protected void btnMapping_Click(object sender, EventArgs e)
        {
            if (ViewState["poid"] != null)
            {
                int poid = Convert.ToInt32(ViewState["poid"]);
                SV.Framework.Fulfillment.DishLabelOperations dishLabelOperations = SV.Framework.Fulfillment.DishLabelOperations.CreateInstance<SV.Framework.Fulfillment.DishLabelOperations>();

                List<SV.Framework.LabelGenerator.PalletCartonMap> palletCartonMaps = new List<SV.Framework.LabelGenerator.PalletCartonMap>();
                List<SV.Framework.LabelGenerator.CartonModel> cartons = new List<SV.Framework.LabelGenerator.CartonModel>();
                SV.Framework.LabelGenerator.PalletCartonMap palletCartonMap = default;
                SV.Framework.LabelGenerator.CartonModel cartonModel = default;
                List<PalletCartonMap> palletsdb = dishLabelOperations.GetPalletCartonMappingLabel(poid);
                SV.Framework.LabelGenerator.DishLabelOperation dishLabelOperation = new SV.Framework.LabelGenerator.DishLabelOperation();
                if (palletsdb != null && palletsdb.Count > 0)
                {
                    foreach (PalletCartonMap item in palletsdb)
                    {
                        cartons = new List<SV.Framework.LabelGenerator.CartonModel>();
                        palletCartonMap = new SV.Framework.LabelGenerator.PalletCartonMap();
                        palletCartonMap.PalletID = item.PalletID;
                        foreach (CartonModel carton in item.Cartons)
                        {
                            cartonModel = new SV.Framework.LabelGenerator.CartonModel();
                            cartonModel.BOXColumn1 = carton.BOXColumn1;
                            cartonModel.BOXColumn2 = carton.BOXColumn2;
                            cartonModel.Column1 = carton.Column1;
                            cartonModel.Column2 = carton.Column2;
                            cartons.Add(cartonModel);
                        }
                        palletCartonMap.Cartons = cartons;
                        palletCartonMaps.Add(palletCartonMap);
                    }
                    if (palletCartonMaps != null)
                    {
                        var memSt = dishLabelOperation.PalletCartonMappingLabelPdf(palletCartonMaps);
                        //var memSt = slabel.ExportToPDF(models);
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
                        lblMsg.Text = "No mapping found";

                }
                else
                {
                    lblMsg.Text = "No data found.";
                }
            }
        }


        protected void btnBoxID_Click(object sender, EventArgs e)
        {
            SV.Framework.Fulfillment.DishLabelOperations dishLabelOperations = SV.Framework.Fulfillment.DishLabelOperations.CreateInstance<SV.Framework.Fulfillment.DishLabelOperations>();

            string poNum = txtPoNum.Text.Trim();
            int companyID = 0;
            if (dpCompany.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(poNum))
                {
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                    List<SV.Framework.LabelGenerator.CartonBoxID> cartonBoxIDS = new List<SV.Framework.LabelGenerator.CartonBoxID>();
                    SV.Framework.LabelGenerator.CartonBoxID cartonBoxID = null;
                    List<CartonBoxID> cartonBoxIDs = dishLabelOperations.GetPurchaseOrderBoxIDs(companyID, poNum, 0, "");

                    //List<SV.Framework.LabelGenerator.CartonBoxID> cartonBoxIDs = ContainerOperation.GetPurchaseOrderBoxIDs(companyID, poNum, 0, "");
                    if (cartonBoxIDs != null && cartonBoxIDs.Count > 0)
                    {
                        SV.Framework.LabelGenerator.DishLabelOperation dishLabelOperation = new SV.Framework.LabelGenerator.DishLabelOperation();
                        foreach (CartonBoxID item in cartonBoxIDs)
                        {
                            cartonBoxID = new SV.Framework.LabelGenerator.CartonBoxID();
                            cartonBoxID.BoxDesc = item.BoxDesc;
                            cartonBoxID.BoxID = item.BoxID;
                            cartonBoxID.FulfillmentNumber = item.FulfillmentNumber;
                            cartonBoxID.SKU = item.SKU;

                            cartonBoxIDS.Add(cartonBoxID);
                        }
                        if (cartonBoxIDS != null)
                        {
                            var memSt = dishLabelOperation.BOXLabelPdfTarCode(cartonBoxIDS);
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "stop loader", "StopProgress()", true);
                            //var memSt = slabel.ExportToPDF(models);
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
                        {
                            lblMsg.Text = "No data found.";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "No data found.";
                    }

                }
                else
                {
                    lblMsg.Text = "Filfillment required!";
                }
            }
            else
            {
                lblMsg.Text = "Customer required!";
            }

        }
    }
}