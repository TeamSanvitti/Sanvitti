using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Product
{
    public partial class ManageCarriers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillCarriers();
        }
        private void FillCarriers()
        {
            try
            {
                List<Carriers> carrierList = CarriersOperation.GetCarriersList(0, true);
                if (carrierList.Count > 0)
                {
                    Session["carrierlist"] = carrierList;
                    gvCarriers.DataSource = carrierList;
                    gvCarriers.DataBind();
                    lblMsg.Text = string.Empty;
                }
                else
                {
                    Carriers carrier = new Carriers();
                    carrier.Active = true;
                    carrier.CarrierGUID = 0;
                    carrier.CarrierLogo = string.Empty;
                    carrier.CarrierName = string.Empty;
                    carrierList.Add(carrier);
                    Session["carrierlist"] = carrierList;
                    gvCarriers.DataSource = carrierList;
                    gvCarriers.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;

            }
        }

        //protected void dg_ItemCreated(System.Object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        //{
        //    System.Web.UI.WebControls.LinkButton lb;
        //    if (dgCarriers.CurrentPageIndex == 0)
        //    {
        //        if (e.Item.ItemIndex >= 0)
        //        {
        //            lb = (LinkButton)e.Item.Cells[0].Controls[0];
        //            if (e.Item.ItemIndex == 0)
        //            {
        //                if (lb.Text == "Edit")
        //                    lb.Text = "Add";
        //                else if (lb.Text == "Update")
        //                    lb.Text = "Insert";
        //            }
        //            else
        //                lb.Visible = false;
        //        }
        //    }
        //}
        //protected void dg_Cancel(System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        //{
        //    //dgPoItem.EditItemIndex = -1;
        //    //ViewState["EditIndex"] = null;
        //    //lblMsg.Text = string.Empty;
        //    //datagridBind(0);
        //    //SetStoreName();
        //}

        //protected void dg_Edit(System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        //{
        //    //iEditIndex = e.Item.ItemIndex;
        //    //ViewState["EditIndex"] = iEditIndex;
        //    //dgPoItem.EditItemIndex = iEditIndex;
        //    //datagridBind(0);
        //    //SetStoreName();
        //}

        //protected void dg_Update(System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        //{
        //    //string itemCode, qty, itemidval, phoneCatg;
        //    //int iqty, itemID;
        //    //try
        //    //{
        //    //    lblMsg.Text = string.Empty;
        //    //    iEditIndex = (Int32)ViewState["EditIndex"];
        //    //    itemCode = ((DropDownList)e.Item.Cells[2].Controls[1]).SelectedItem.Text;
        //    //    itemidval = ((DropDownList)e.Item.Cells[2].Controls[1]).SelectedValue;
        //    //    qty = ((TextBox)e.Item.Cells[3].Controls[1]).Text;
        //    //    phoneCatg = ((DropDownList)e.Item.Cells[4].Controls[1]).SelectedValue;
        //    //    int.TryParse(qty, out iqty);
        //    //    if (string.IsNullOrEmpty(itemCode) || iqty <= 0)
        //    //    {
        //    //        lblMsg.Text = "Item code and Quantity should be entered to save item";
        //    //    }
        //    //    else
        //    //    {
        //    //        dtType = Session["PoList"] as List<avii.Classes.PurchaseOrderItem>;
        //    //        if ((dtType != null && dtType.Count > 0) && iEditIndex <= dtType.Count)
        //    //        {
        //    //            avii.Classes.PurchaseOrderItem pItem = dtType[iEditIndex];
        //    //            pItem.ItemCode = itemCode;
        //    //            int.TryParse(itemidval, out itemID);
        //    //            pItem.Quantity = iqty;
        //    //            if (phoneCatg == "H")
        //    //                pItem.PhoneCategory = avii.Classes.PhoneCategoryType.Hot;
        //    //            else
        //    //                pItem.PhoneCategory = avii.Classes.PhoneCategoryType.Cold;

        //    //            if (itemID > 0)
        //    //            {
        //    //                pItem.ItemID = itemID;
        //    //                Session["PoList"] = dtType;
        //    //                dgPoItem.EditItemIndex = -1;
        //    //                datagridBind(1);
        //    //            }
        //    //        }
        //    //        ViewState["EditIndex"] = null;
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    lblMsg.Text = ex.Message;
        //    //}
        //    //SetStoreName();
        //}

        //protected void dg_ItemCommand(System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        //{
        //    lblMsg.Text = string.Empty;

        //    //if (e.CommandName == "delete" && e.Item.ItemIndex > 0)
        //    //{
        //    //    dtType = Session["PoList"] as List<avii.Classes.PurchaseOrderItem>;
        //    //    dtType.RemoveAt(e.Item.ItemIndex);
        //    //    Session["PoList"] = dtType;
        //    //    datagridBind(0);
        //    //    //SetStoreName();
        //    //}
        //}






        //protected void fvCarriers_ItemDeleting(object sender, FormViewDeleteEventArgs e)
        //{
        //    DataKey key = fvCarriers.DataKey;
            
        //}
        //protected void fvCarriers_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        //{

        //    DataKey key = fvCarriers.DataKey;
            
        //    //TextBox txtFirstName = (TextBox)fvCarriers.FindControl("txtFirstName2");
        //    //TextBox txtLastName = (TextBox)fvCarriers.FindControl("txtLastName2");
        //    //TextBox txtAddress = (TextBox)fvCarriers.FindControl("txtAddress2");
        //    //TextBox txtDesignation = (TextBox)fvCarriers.FindControl("txtDesignation2");

            
        //    //bindgrid();
            
        //}
        //protected void fvCarriers_ModeChanging(object sender, FormViewModeEventArgs e)
        //{
        //    fvCarriers.ChangeMode(e.NewMode);
        //    //bindgrid();
        //    if (e.NewMode == FormViewMode.Edit)
        //    {
        //        fvCarriers.AllowPaging = false;
        //    }
        //    else
        //    {
        //        fvCarriers.AllowPaging = true;
        //    }
        //}
        //protected void fvCarriers_ItemInserted(object sender, FormViewInsertedEventArgs e)
        //{
        //    fvCarriers.ChangeMode(FormViewMode.ReadOnly);
        //}
        //protected void fvCarriers_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        //{
        //    fvCarriers.ChangeMode(FormViewMode.ReadOnly);
        //}
        //protected void fvCarriers_ItemInserting(object sender, FormViewInsertEventArgs e)
        //{

        //    //TextBox txtEmployeeID = (TextBox)fvCarriers.FindControl("txtEmployeeID1");
        //    //TextBox txtFirstName = (TextBox)fvCarriers.FindControl("txtFirstName1");
        //    //TextBox txtLastName = (TextBox)fvCarriers.FindControl("txtLastName1");
        //    //TextBox txtAddress = (TextBox)fvCarriers.FindControl("txtAddress1");
        //    //TextBox txtDesignation = (TextBox)fvCarriers.FindControl("txtDesignation1");

        //    //SqlConnection conn = new SqlConnection(connStr);
        //    //com.Connection = conn;
        //    //com.CommandText = "INSERT INTO Employees Values('" + txtEmployeeID.Text + "','" + txtFirstName.Text + "', '" + txtLastName.Text + "', '" + txtAddress.Text + "', '" + txtDesignation.Text + "')";
        //    //conn.Open();
        //    //com.ExecuteNonQuery();
        //    //Response.Write("Record inserted successfully");
        //    //bindgrid();
        //    //conn.Close();
        //}
        protected void imgDelete_Commnad(object sender, CommandEventArgs e)
        {
            try
            {
                int carrierGUID = Convert.ToInt32(e.CommandArgument);

                int returnValue = CarriersOperation.DeleteCarrier(carrierGUID);

                FillCarriers();
                if (returnValue > 0)
                    lblMsg.Text = "Deleted successfully";
                else
                    lblMsg.Text = "Carrier in use can not delete";
            }

            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;

            }
        }
        
        protected void imgEditCarrier_OnCommand(object sender, CommandEventArgs e)
        {

            lblMessage.Text = string.Empty;
            lblMsg.Text = string.Empty;
            lblHeader.Text = "Edit Carrier";

            try
            {
                int carrierID = Convert.ToInt32(e.CommandArgument);
                ViewState["carrierID"] = carrierID;
                if (Session["carrierlist"] != null)
                {
                    List<Carriers> carrierList = Session["carrierlist"] as List<Carriers>;
                    var carrier = (from item in carrierList where item.CarrierGUID.Equals(carrierID) select item).ToList();
                    if (carrier.Count > 0)
                    {
                        txtCarrier.Text = carrier[0].CarrierName;
                        ViewState["carrierlogo"] = carrier[0].CarrierLogo;
                        chkActive.Checked = carrier[0].Active;
                    }
                    mdlCarrier.Show();
                }

            }

            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;

            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblMsg.Text = string.Empty;
            lblHeader.Text = "Add Carrier";

            txtCarrier.Text = string.Empty;
            chkActive.Checked = true;
            mdlCarrier.Show();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblMsg.Text = string.Empty;
            
            string imagePath = string.Empty;
            string fileName = string.Empty;
            int carrierID = 0;
            try
            {
                if (ViewState["carrierID"] != null)
                    carrierID = Convert.ToInt32(ViewState["carrierID"]);

                if (ViewState["carrierlogo"] != null)
                    fileName = ViewState["carrierlogo"].ToString();

                if (txtCarrier.Text.Trim() != string.Empty)
                {
                    if (fuCarrier.HasFile)
                    {
                        fileName = Path.GetFileName(fuCarrier.PostedFile.FileName);
                        string targetFolder = Server.MapPath("~");

                        targetFolder = targetFolder + "\\images\\Carrier\\";

                        if (!Directory.Exists(Path.GetFullPath(targetFolder)))
                        {
                            Directory.CreateDirectory(Path.GetFullPath(targetFolder));
                        }
                        imagePath = targetFolder + fileName;
                        string ext = Path.GetExtension(imagePath);
                        if (ext == ".gif" || ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".png")
                            fuCarrier.PostedFile.SaveAs(imagePath);
                        else
                        {
                            lblMsg.Text = "Not a valid image";
                            return;
                        }
                        //filename = fuCarrier.FileName;
                    }
                    Carriers carrier = new Carriers();
                    carrier.Active = chkActive.Checked;
                    carrier.CarrierGUID = carrierID;
                    carrier.CarrierLogo = fileName;
                    carrier.CarrierName = txtCarrier.Text;
                    int returnValue = CarriersOperation.CreateUpdateCarrier(carrier);
                    if (returnValue == 0)
                    {
                        FillCarriers();
                        if (carrierID == 0)
                            lblMsg.Text = "Submitted successfully";
                        else
                            lblMsg.Text = "Updated successfully";
                    }
                    else
                    {
                        lblMessage.Text = "Carrier already exists";
                        mdlCarrier.Show();
                    }
                    //FillCarriers();
                }
                else
                    lblMsg.Text = "Carrier name can not be empty";
            }

            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                mdlCarrier.Show();

            }
        }


        //protected void gvCarriers_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName.Equals("AddNew"))
        //    {
        //        string imagePath = string.Empty;
        //        string fileName = string.Empty;
        //        TextBox txtNewCarrier = (TextBox)gvCarriers.FooterRow.FindControl("txtNewCarrier");
        //        FileUpload fuNewCarrier = (FileUpload)gvCarriers.FooterRow.FindControl("fuNewCarrier");
        //        CheckBox chkNewActive = (CheckBox)gvCarriers.FooterRow.FindControl("chkNewActive");
        //        if (txtNewCarrier.Text.Trim() != string.Empty)
        //        {
        //            if (fuNewCarrier.HasFile)
        //            {
        //                fileName = Path.GetFileName(fuNewCarrier.PostedFile.FileName);
        //                string targetFolder = Server.MapPath("~");

        //                targetFolder = targetFolder + "\\images\\Carrier\\";

        //                if (!Directory.Exists(Path.GetFullPath(targetFolder)))
        //                {
        //                    Directory.CreateDirectory(Path.GetFullPath(targetFolder));
        //                }
        //                imagePath = targetFolder + fileName;
        //                string ext = Path.GetExtension(imagePath);
        //                if (ext == ".gif" || ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".png")
        //                    fuNewCarrier.PostedFile.SaveAs(imagePath);
        //                else
        //                {
        //                    lblMsg.Text = "Not a valid image";
        //                    return;
        //                }
        //                //filename = fuCarrier.FileName;
        //            }
        //            Carriers carrier = new Carriers();
        //            carrier.Active = chkNewActive.Checked;
        //            carrier.CarrierGUID = 0;
        //            carrier.CarrierLogo = fileName;
        //            carrier.CarrierName = txtNewCarrier.Text;
        //            int returnValue = CarriersOperation.CreateUpdateCarrier(carrier);
        //            if (returnValue == 0)
        //            {
        //                FillCarriers();
        //                lblMsg.Text = "Submitted successfully";
        //                gvCarriers.EditIndex = -1;

        //            }
        //            else
        //            {
        //                lblMsg.Text = "Carrier already exists";
        //            }
        //            //FillCarriers();
        //        }
        //        else
        //            lblMsg.Text = "Carrier name can not be empty";
        //    }
        //}
        //protected void gvCarriers_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    gvCarriers.EditIndex = e.NewEditIndex;
        //    FillCarriers();
        //}
        //protected void gvCarriers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    gvCarriers.EditIndex = -1;
        //    FillCarriers();
        //}
        //protected void gvCarriers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    int carrierGUID = 0;
        //    string imagePath = string.Empty;
        //    string fileName = string.Empty;

            
        //    TextBox txtCarrier = (TextBox)gvCarriers.Rows[e.RowIndex].FindControl("txtCarrier");
        //    FileUpload fuCarrier = (FileUpload)gvCarriers.Rows[e.RowIndex].FindControl("fuCarrier");
        //    CheckBox chkActive = (CheckBox)gvCarriers.Rows[e.RowIndex].FindControl("chkActive");
        //    HiddenField hdnCarrierLogo = (HiddenField)gvCarriers.Rows[e.RowIndex].FindControl("hdnCarrierLogo");

        //    fileName = hdnCarrierLogo.Value;
        //    if (txtCarrier.Text.Trim() != string.Empty)
        //    {
        //        if (fuCarrier.HasFile)
        //        {
        //            fileName = Path.GetFileName(fuCarrier.PostedFile.FileName);
        //            string targetFolder = Server.MapPath("~");

        //            targetFolder = targetFolder + "\\images\\Carrier\\";

        //            if (!Directory.Exists(Path.GetFullPath(targetFolder)))
        //            {
        //                Directory.CreateDirectory(Path.GetFullPath(targetFolder));
        //            }
        //            imagePath = targetFolder + fileName;
        //            string ext = Path.GetExtension(imagePath);
        //            if (ext == ".gif" || ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".png")
        //                fuCarrier.PostedFile.SaveAs(imagePath);
        //            else
        //            {
        //                lblMsg.Text = "Not a valid image";
        //                return;
        //            }
        //            //filename = fuCarrier.FileName;
        //        }
        //        carrierGUID = Convert.ToInt32(gvCarriers.DataKeys[e.RowIndex].Values[0]);
        //        Carriers carrier = new Carriers();
        //        carrier.Active = chkActive.Checked;
        //        carrier.CarrierGUID = carrierGUID;
        //        carrier.CarrierLogo = fileName;
        //        carrier.CarrierName = txtCarrier.Text;


        //        int returnValue = CarriersOperation.CreateUpdateCarrier(carrier);

        //        //customer.Update(GridView1.DataKeys[e.RowIndex].Values[0].ToString(), txtName.Text, cmbGender.SelectedValue, txtCity.Text, cmbType.SelectedValue);
        //        if (returnValue == 0)
        //        {

        //            gvCarriers.EditIndex = -1;
        //            FillCarriers();
        //            lblMsg.Text = "Updated successfully";
        //        }
        //        else
        //        {
        //            lblMsg.Text = "Carrier already exists";
        //        }
        //    }
        //    else
        //        lblMsg.Text = "Carrier name can not be empty";

        //    //gvCarriers.EditIndex = -1;
        //    //FillCarriers();

        //}
        //protected void gvCarriers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    int carrierGUID = 0;
        //    string s = gvCarriers.DataKeys[e.RowIndex].Values[0].ToString();
        //    carrierGUID = Convert.ToInt32(gvCarriers.DataKeys[e.RowIndex].Values[0]);
        //    int returnValue = CarriersOperation.DeleteCarrier(carrierGUID);
        //    //customer.Delete(GridView1.DataKeys[e.RowIndex].Values[0].ToString());
        //    FillCarriers();
        //    if (returnValue > 0)
        //        lblMsg.Text = "Deleted successfully";
        //    else
        //        lblMsg.Text = "Carrier in use can not delete";
        //}
    }
}