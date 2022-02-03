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
    public partial class PoContainer : System.Web.UI.Page
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
                Response.Redirect(url);
            }
            if (!IsPostBack)
            {
                BindCustomers();
                tblPallet.Visible = false;
                tblContainer.Visible = false;
            }
        }

        protected void BindCustomers()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 1);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        private SV.Framework.Fulfillment.ContainerOperation containerOperation = SV.Framework.Fulfillment.ContainerOperation.CreateInstance<SV.Framework.Fulfillment.ContainerOperation>();


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPO();
        }
        private void BindPO()
        {
            
            tblContainer.Visible = false;
            tblPallet.Visible = false;
            rptPallets.DataSource = null;
            rptPallets.DataBind();
            string Code = "";
            int companyID = 0, contanierToGenrate=0, StatusID = 0, PalletQuantity = 0;
            if (dpCompany.SelectedIndex > 0)
            {
                int.TryParse(dpCompany.SelectedValue, out companyID);

                ViewState["containercount"] = null;
                ViewState["palletcount"] = null;
                lblMsg.Text = string.Empty;
                int numberOfContainers = 0, poid = 0, casePackQuantity = 0, numberOfPallets = 0;
                string fulfillmentNumber = txtPoNum.Text.Trim();
                string trackingNumber;//, fulfillmentNo;
                trackingNumber = txtTrackingNo.Text.Trim();
                //fulfillmentNo = string.Empty;
                if (string.IsNullOrEmpty(fulfillmentNumber) && string.IsNullOrEmpty(trackingNumber))
                {
                    lblMsg.Text = "Please select fulfillment number or tracking number";
                    return;
                }

                List<FulfillmentContainer> containers = null;
                List<FulfillmentPallet> pallets = null;
                //containerOperation

                List<ContainerInfo> skuList = containerOperation.GetContainerInfo(companyID, fulfillmentNumber, trackingNumber, out containers, out pallets);
                if (skuList != null && skuList.Count > 0)
                {
                    foreach (ContainerInfo item in skuList)
                    {
                        Code = item.Code;
                        StatusID = item.StatusID;
                        numberOfContainers += item.ContainerRequired;
                        numberOfPallets += item.PalletRequired;
                        poid = item.POID;
                        casePackQuantity = item.ContainerQuantity;
                        PalletQuantity = item.PalletQuantity;

                    }
                    hdPallets.Value = numberOfPallets.ToString();
                    ViewState["Code"] = Code;
                    ViewState["PalletQuantity"] = PalletQuantity;
                    gvPOSKUs.DataSource = skuList;
                    gvPOSKUs.DataBind();
                    btnSubmit.Visible = true;
                    btnCancel1.Visible = true;
                    lblReqContainer.Visible = true;
                    txtContainers.Visible = true;
                    btnGenContainerID.Visible = true;
                    btnSubmit.Enabled = true;
                    tblContainer.Visible = true;


                    if (containers != null && containers.Count > 0)
                    {
                        rptContainers.DataSource = containers;
                        rptContainers.DataBind();
                        Session["containerList"] = containers;
                        
                        if(numberOfContainers== containers.Count)
                        {
                            btnBoxID.Visible = true;

                            btnSubmit.Visible = true;
                            btnDelete.Visible = true;
                            btnGenContainerID.Enabled = true;
                            btnSubmit.Enabled = true;
                        }
                        //btnGenContainerID.Visible = false;

                        numberOfContainers = containers.Count;
                        ViewState["containercount"] = containers.Count;
                        if(pallets != null && pallets.Count > 0)
                        {
                            numberOfPallets = pallets.Count;
                            rptPallets.DataSource = pallets;
                            rptPallets.DataBind();
                            txtComment.Text = pallets[0].Comment;
                            ViewState["palletcount"] = numberOfPallets;

                        }
                        else
                        {
                            rptPallets.DataSource = null;
                            rptPallets.DataBind();

                            ViewState["palletcount"] = null;

                        }

                    }
                    else
                    {
                        btnDelete.Visible = false;
                        rptContainers.DataSource = null;
                        rptContainers.DataBind();
                    }

                    if (numberOfPallets > 0)
                        tblPallet.Visible = true;

                    txtContainers.Text = numberOfContainers.ToString();
                    txtPallets.Text = numberOfPallets.ToString();
                    hdContainers.Value = numberOfContainers.ToString();
                    hdPallets.Value = numberOfPallets.ToString();
                   // ViewState["palletcount"] = numberOfPallets;

                    ViewState["casePackQuantity"] = casePackQuantity;
                    ViewState["poid"] = poid;

                    if(StatusID == 3)
                    {
                        lblMsg.Text = "Fulfillment number already shipped";
                        btnDelete.Visible = false;
                        btnSubmit.Visible = false;
                        btnCancel1.Visible = false;
                        btnGenContainerID.Visible = false;
                        btnBoxID.Visible = true;
                    }
                    //}

                }
                else
                {
                    gvPOSKUs.DataSource = null;
                    gvPOSKUs.DataBind();
                    lblMsg.Text = "No record found";
                }
            }
            else
            {
                gvPOSKUs.DataSource = null;
                gvPOSKUs.DataBind();
                lblMsg.Text = "Please select customer";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            txtPoNum.Text = string.Empty;
            txtComment.Text = string.Empty;
            txtTrackingNo.Text = string.Empty;
            gvPOSKUs.DataSource = null;
            gvPOSKUs.DataBind();
            btnGenContainerID.Enabled = true;
            btnSubmit.Visible = false;
            tblPallet.Visible = false;
            btnGenContainerID.Visible = false;
            btnCancel1.Visible = false;
            lblReqContainer.Visible = false;
            txtContainers.Visible = false;
            btnDelete.Visible = false;
            rptContainers.DataSource = null;
            rptContainers.DataBind();
            rptPallets.DataSource = null;
            rptPallets.DataBind();

            btnSubmit.Enabled = true;
        }

        protected void btnGenContainerID_Click(object sender, EventArgs e)
        {
            int numberOfContainers =  0, poid = 0, containercountExists = 0, palletCount = 0, palletCountExists = 0;
            lblMsg.Text = string.Empty;
            int companyID = 0;
            if (dpCompany.SelectedIndex > 0)            
                int.TryParse(dpCompany.SelectedValue, out companyID);

                List<FulfillmentPallet> palletList = null;
            if (ViewState["palletcount"] != null)
                palletCountExists = Convert.ToInt32(ViewState["palletcount"]);

            if (ViewState["containercount"] != null)
                containercountExists = Convert.ToInt32(ViewState["containercount"]);

            if (ViewState["poid"] != null)
                poid = Convert.ToInt32(ViewState["poid"]);
            if (txtContainers.Text != string.Empty)
                numberOfContainers = Convert.ToInt32(txtContainers.Text);

            if (txtPallets.Text != string.Empty)
                palletCount = Convert.ToInt32(txtPallets.Text);


            if (numberOfContainers > 0 && containercountExists > 0 && containercountExists==numberOfContainers && palletCount > 0 && palletCountExists > 0 && palletCountExists == palletCount)
            {
                //lblMsg.Text = "Container ID(s) already generated";
                lblMsg.Text = "Container ID(s) & Pallet ID(s) already generated";
                return;
            }

            if (numberOfContainers > 0 && containercountExists > 0 && containercountExists > numberOfContainers)
            {
                lblMsg.Text = "Please delete Container ID(s) first then re-generate again";
                return;
            }
            if (palletCount > 0 && palletCountExists > 0 && palletCountExists > palletCount)
            {
                lblMsg.Text = "Please delete Pallet ID(s) first then re-generate again";
                return;
            }


            if (numberOfContainers > 0 && containercountExists > 0 && containercountExists < numberOfContainers)
            {
                numberOfContainers = numberOfContainers - containercountExists;
                btnDelete.Visible = false;
                btnSubmit.Visible = true;
                btnSubmit.Enabled = true;

            }
            if (palletCount > 0 && palletCountExists > 0 && palletCountExists == palletCount)
            {
                palletCount = palletCount - palletCountExists;
                btnDelete.Visible = false;
                btnSubmit.Visible = true;
                btnSubmit.Enabled = true;
            }
            if (palletCount > 0 && palletCountExists > 0 && palletCountExists < palletCount)
            {
                palletCount = palletCount - palletCountExists;
                btnDelete.Visible = false;
                btnSubmit.Visible = true;
                btnSubmit.Enabled = true;
            }
            if (numberOfContainers > 0 || palletCount > 0)
            {
                List<FulfillmentContainer> containerList = containerOperation.GenerateContainerIDs(numberOfContainers, poid, palletCount, companyID, out palletList);
                if (containerList != null && containerList.Count > 0)
                {
                    rptContainers.DataSource = containerList;
                    rptContainers.DataBind();
                    Session["containerList"] = containerList;

                }
                else
                {
                    rptContainers.DataSource = null;
                    rptContainers.DataBind();
                    Session["containerList"] = null;
                }
                if(palletList != null && palletList.Count > 0)
                {
                    rptPallets.DataSource = palletList;
                    rptPallets.DataBind();
                    Session["palletList"] = palletList;
                }
                else
                {
                    rptPallets.DataSource = null;
                    rptPallets.DataBind();
                    Session["palletList"] = null;
                }
            }
            else
            {
                lblMsg.Text = "Container count is required!";
                rptContainers.DataSource = null;
                rptContainers.DataBind();
                Session["containerList"] = null;
                rptPallets.DataSource = null;
                rptPallets.DataBind();
                Session["palletList"] = null;
            }
          //  RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        private List<PalletContainersMapping> GetMappings(List<FulfillmentContainer> containerList, List<FulfillmentPallet> pallets)
        {
            int containerCount, palletCount, itr, pitr, PalletQuantity;
            List<PalletContainersMapping> mappings = new List<PalletContainersMapping>();

            if (containerList != null && pallets != null)
            {
                containerCount = containerList.Count;
                palletCount = pallets.Count;
                itr = 0;
                pitr = 0;
                if (ViewState["PalletQuantity"] != null)
                {
                    PalletQuantity = Convert.ToInt32(ViewState["PalletQuantity"]);

                    PalletContainersMapping palletContainerMapping = null;

                    foreach (FulfillmentContainer item in containerList)
                    {
                        itr = itr + 1;
                        palletContainerMapping = new PalletContainersMapping();
                        palletContainerMapping.ContainerID = item.ContainerID;
                        palletContainerMapping.PalletID = pallets[pitr].PalletID;

                        mappings.Add(palletContainerMapping);
                        if (itr == PalletQuantity)
                        {
                            pitr = pitr + 1;
                            itr = 0;
                        }
                    }
                }
            }
            return mappings;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int casePackQuantity = 0, poid = 0, containerCount = 0, containerUpdatedCount = 0, PalletQuantity = 0;
            int userID = 0;
            string containerIDs = string.Empty, returnMessage = string.Empty, palletIDs = string.Empty, Code = "";

            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
            }
            lblMsg.Text = string.Empty;
            
            if (ViewState["poid"] != null)
                poid = Convert.ToInt32(ViewState["poid"]);
            if (ViewState["PalletQuantity"] != null)
                PalletQuantity = Convert.ToInt32(ViewState["PalletQuantity"]);
            if (ViewState["casePackQuantity"] != null)
                casePackQuantity = Convert.ToInt32(ViewState["casePackQuantity"]);

            if (ViewState["Code"] != null)
                Code = Convert.ToString(ViewState["Code"]);
            if (ViewState["containercount"] != null)
                containerUpdatedCount = Convert.ToInt32(ViewState["containercount"]);

            List<FulfillmentPallet> palletList = null;

            List<FulfillmentContainer> containerList = null;
            if (Session["containerList"] != null)
            {
                containerList = Session["containerList"] as List<FulfillmentContainer>;

                if(containerList != null && containerList.Count > 0)
                {
                    foreach (FulfillmentContainer row in containerList)
                    {
                        if (row.POID == -1)
                        {
                            if (string.IsNullOrEmpty(containerIDs))
                                containerIDs = row.ContainerID;
                            else
                                containerIDs = containerIDs + "," + row.ContainerID;
                        }
                    }
                    containerUpdatedCount = containerList.Count;
                }

                if (Session["palletList"] != null)
                {
                    palletList = Session["palletList"] as List<FulfillmentPallet>;
                    if (palletList != null && palletList.Count > 0)
                    {
                        foreach (FulfillmentPallet row in palletList)
                        {
                            if (row.POID == -1)
                            {
                                if (string.IsNullOrEmpty(palletIDs))
                                    palletIDs = row.PalletID;
                                else
                                    palletIDs = palletIDs + "," + row.PalletID;
                            }
                        }                        
                    }
                }
                List<PalletContainersMapping> mappings = GetMappings(containerList, palletList);

                string comment = txtComment.Text.Trim();
                //string Code = "";
                returnMessage = containerOperation.ContainerIDInsert(poid, casePackQuantity, userID, containerList, containerCount, containerUpdatedCount, palletIDs, palletList, comment, mappings, Code, PalletQuantity);
                if(string.IsNullOrEmpty(returnMessage))
                {
                    lblMsg.Text = "Submitted successfully";
                    btnSubmit.Enabled = false;
                    btnBoxID.Visible = true;
                }
                else
                    lblMsg.Text = returnMessage + " ContainerID(s) already exists. Please generate again and submit";
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int poid = 0;
            int userID = 0;
            int returnResult = 0;

            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
            }
            lblMsg.Text = string.Empty;

            if (ViewState["poid"] != null)
            {
                poid = Convert.ToInt32(ViewState["poid"]);
                returnResult = containerOperation.ContainerIDDelete(poid, userID);
                if (returnResult > 0)
                {
                    BindPO();
                    lblMsg.Text = "Deleted successfully";
                }
                else
                    lblMsg.Text = "ContainerID(s) in use cannot be deleted";

            }
        }

        protected void imdDel_Command(object sender, CommandEventArgs e)
        {
           string ContainerID = Convert.ToString(e.CommandArgument);
            int poid = 0;
            if (ViewState["poid"] != null)
                poid = Convert.ToInt32(ViewState["poid"]);
            if(poid > 0)
            {
                int userID = 0;
                int returnResult = 0;

                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    userID = userInfo.UserGUID;
                }
                lblMsg.Text = string.Empty;

                if (ViewState["poid"] != null)
                {
                    poid = Convert.ToInt32(ViewState["poid"]);
                    returnResult = containerOperation.ContainerIDDelete(poid, userID, ContainerID);
                    if (returnResult > 0)
                    {
                        BindPO();
                        lblMsg.Text = "Deleted successfully";
                    }
                    else
                        lblMsg.Text = "ContainerID(s) in use cannot be deleted";

                }
            }
        }

        protected void imgPalletDel_Command(object sender, CommandEventArgs e)
        {
            string PalletID = Convert.ToString(e.CommandArgument);
            int poid = 0;
            if (ViewState["poid"] != null)
                poid = Convert.ToInt32(ViewState["poid"]);
            if (poid > 0)
            {
                int userID = 0;
                int returnResult = 0;

                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    userID = userInfo.UserGUID;
                }
                lblMsg.Text = string.Empty;

                if (ViewState["poid"] != null)
                {
                    poid = Convert.ToInt32(ViewState["poid"]);
                    returnResult = containerOperation.PalletIDDelete(poid, userID, PalletID);
                    if (returnResult > 0)
                    {
                        BindPO();
                        lblMsg.Text = "Deleted successfully";
                    }
                    else
                        lblMsg.Text = "PalletID(s) in use cannot be deleted";

                }
            }
        }

        protected void gvPOSKUs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (e.Row.RowIndex >= 0)
                {
                    //HiddenField hdPallet = (HiddenField)e.Row.FindControl("hdPallet");
                    if (hdPallets.Value == "0")
                        e.Row.Cells[7].Visible = false;
                }
            }
        }

        protected void btnBoxID_Click(object sender, EventArgs e)
        {
            SV.Framework.Fulfillment.DishLabelOperations dishLabelOperations = SV.Framework.Fulfillment.DishLabelOperations.CreateInstance<SV.Framework.Fulfillment.DishLabelOperations>();

            string poNum = txtPoNum.Text.Trim();
            int companyID = 0;
            if(dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);

                List<SV.Framework.LabelGenerator.CartonBoxID> cartonBoxIDS = new List<SV.Framework.LabelGenerator.CartonBoxID>();
                SV.Framework.LabelGenerator.CartonBoxID cartonBoxID = null;
                List<CartonBoxID> cartonBoxIDs = dishLabelOperations.GetPurchaseOrderBoxIDs(companyID, poNum, 0, "");

                //List<SV.Framework.LabelGenerator.CartonBoxID> cartonBoxIDs = ContainerOperation.GetPurchaseOrderBoxIDs(companyID, poNum, 0,"");
                if(cartonBoxIDs != null && cartonBoxIDs.Count > 0)
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

        }
    }
}