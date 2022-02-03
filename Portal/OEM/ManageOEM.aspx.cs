using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii
{
    public partial class ManageOEM : System.Web.UI.Page
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
                lnkView.Visible = false;
                BindStates();

                if (Request["m"] != null)
                { 
                    int makerGUID = Convert.ToInt32(Request["m"]);
                    btnCancel.Text = "Back to search";
                    ViewState["makerguid"] = makerGUID;
                    GetMakerDetails(makerGUID);
                }
            }
        }

        private void ClearAll()
        {
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtCellPhone.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtContactName.Text = string.Empty;
            txtCountry.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtEmail1.Text = string.Empty;
            txtEmail2.Text = string.Empty;
            txtHomePhone.Text = string.Empty;
            txtMakerName.Text = string.Empty;
            txtOfficePhone1.Text = string.Empty;
            txtOfficePhone2.Text = string.Empty;
            txtShortName.Text = string.Empty;
            txtZip.Text = string.Empty;
            ddlState.SelectedIndex = 0;
            lblMsg.Text = string.Empty;
            chkActive.Checked = false;

        }
        private void GetMakerDetails(int makerGUID)
        {
            ItemMaker makerObj = MakerOperations.GetMakerInfo(makerGUID);
            ViewState["addressid"] = makerObj.AddressID;
            txtMakerName.Text = makerObj.MakerName;
            txtShortName.Text = makerObj.ShortName;
            ViewState["image"] = makerObj.MakerImage;
            txtDescription.Text = makerObj.MakerDescription;
            Address address = makerObj.MakerAddresses;
            ContactInfo contactInfo = makerObj.MakerContactInfo;
            txtAddress1.Text = address.Address1;
            txtAddress2.Text = address.Address2;
            txtCity.Text = address.City;
            txtCountry.Text = address.Country;
            ddlState.SelectedValue = address.State;
            txtZip.Text = address.Zip;
            txtCellPhone.Text = contactInfo.CellPhone;
            txtContactName.Text = contactInfo.ContactName;
            txtEmail1.Text = contactInfo.Email1;
            txtEmail2.Text = contactInfo.Email2;
            txtHomePhone.Text = contactInfo.HomePhone;
            txtOfficePhone1.Text = contactInfo.OfficePhone1;
            txtOfficePhone2.Text = contactInfo.OfficePhone2;
            chkActive.Checked = makerObj.Active;
            chkShowCatolog.Checked = makerObj.ShowunderCatalog;
            lnkView.Visible = true;


        }
        private void BindStates()
        {
            DataTable dataTable = MakerOperations.GetStates();
            ddlState.DataSource = dataTable;
            ddlState.DataTextField = "statecode";
            ddlState.DataValueField = "statecode";
            ddlState.DataBind();
            ListItem newItem = new ListItem("", "0");
            ddlState.Items.Insert(0, newItem);

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                lblMsg.Text = string.Empty;
                int returnValue = 0;
                ItemMaker makerObj = new ItemMaker();
                int addressID = 0;
                int makerGUID = 0;
                string makerName, shortName;
                makerName = shortName = string.Empty;
                string fileName = string.Empty;
                string imagePath = string.Empty;
                makerName = txtMakerName.Text.Trim();
                shortName = txtShortName.Text.Trim();

                if (ViewState["makerguid"] != null)
                    makerGUID = Convert.ToInt32(ViewState["makerguid"]);
                if (ViewState["addressid"] != null)
                    addressID = Convert.ToInt32(ViewState["addressid"]);

                if (uploadMakerImg.HasFile)
                {
                    fileName = Path.GetFileName(uploadMakerImg.PostedFile.FileName);
                    string targetFolder = Server.MapPath("~");

                    targetFolder = targetFolder + "\\images\\Makers\\";

                    if (!Directory.Exists(Path.GetFullPath(targetFolder)))
                    {
                        Directory.CreateDirectory(Path.GetFullPath(targetFolder));
                    }
                    imagePath = targetFolder + fileName;
                    string ext = Path.GetExtension(uploadMakerImg.PostedFile.FileName);
                    if (ext == ".gif" || ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".png")
                        uploadMakerImg.PostedFile.SaveAs(imagePath);
                    else
                    {
                        lblMsg.Text = "Not a valid image";
                        return;
                    }

                }
                else
                    if (ViewState["image"] != null)
                    {
                        fileName = ViewState["image"].ToString();
                    }
                if (makerName != string.Empty && shortName != string.Empty)
                {
                    makerObj.MakerName = makerName;
                    makerObj.ShortName = shortName;
                    makerObj.MakerGUID = makerGUID;
                    makerObj.Active = chkActive.Checked;
                    makerObj.ShowunderCatalog = chkShowCatolog.Checked;
                    makerObj.AddressID = addressID;
                    Address makerAddresses = new Address();
                    ContactInfo makerContactInfo = new ContactInfo();

                    makerAddresses.Address1 = txtAddress1.Text.Trim();
                    makerAddresses.Address2 = txtAddress2.Text.Trim();
                    makerAddresses.AddressType = AddressType.Maker;
                    makerAddresses.City = txtCity.Text.Trim();
                    makerAddresses.State = ddlState.SelectedValue;
                    makerAddresses.Zip = txtZip.Text.Trim();
                    makerAddresses.Country = txtCountry.Text.Trim();
                    makerContactInfo.CellPhone = txtCellPhone.Text.Trim();
                    makerContactInfo.ContactName = txtContactName.Text.Trim();
                    makerContactInfo.Email1 = txtEmail1.Text.Trim();
                    makerContactInfo.Email2 = txtEmail2.Text.Trim();
                    makerContactInfo.HomePhone = txtHomePhone.Text.Trim();
                    makerContactInfo.OfficePhone2 = txtOfficePhone2.Text.Trim();
                    makerContactInfo.OfficePhone1 = txtOfficePhone1.Text.Trim();
                    makerObj.MakerDescription = txtDescription.Text.Trim();
                    makerObj.MakerImage = fileName;
                    makerObj.MakerAddresses = makerAddresses;
                    makerObj.MakerContactInfo = makerContactInfo;


                    returnValue = MakerOperations.CreateMaker(makerObj);
                    if (returnValue > 0)
                    {
                        if (returnValue == 1)
                            lblMsg.Text = "OEM name already exist";
                        else
                            if (returnValue == 2)
                                lblMsg.Text = "Short name already exist";
                            else
                                if (returnValue == 3)
                                    lblMsg.Text = "OEM name & Short name already exist";
                    }
                    else
                    {
                        if (makerGUID == 0)
                        {
                            ClearAll();
                            lblMsg.Text = "Submitted successfully";

                        }
                        else
                        {
                            Response.Redirect("OEMQuery.aspx?search=1", false);
                            lblMsg.Text = "Updatted successfully";
                        }
                    }
                }
                else
                    lblMsg.Text = "Required fields are missing";

            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (ViewState["makerguid"] != null)
                Response.Redirect("OEMQuery.aspx?search=1", false);
            else
                ClearAll();
        }
        protected void lnkView_Click(object sender, EventArgs e)
        {
            int makerGUID = 0;
            if (ViewState["makerguid"] != null)
            {
                makerGUID = Convert.ToInt32(ViewState["makerguid"]);
                if (makerGUID > 0)
                {
                    List<ItemMaker> makerList = (List<ItemMaker>)Session["maker"];
                    var maker = (from item in makerList where item.MakerGUID.Equals(makerGUID) select item).ToList();
                    if (maker != null && maker.Count > 0)
                    {
                        if (maker[0].MakerImage != string.Empty)
                        {
                            imgMaker.ImageUrl = "~/Images/Makers/" + maker[0].MakerImage;
                            ModalPopupExtender1.Show();
                        }
                        else
                        {
                            //imgMaker.Visible = false;
                            lblMsg.Text = "Image not uploaded yet";

                        }
                        

                    }
                }
            }
        }
    }
}