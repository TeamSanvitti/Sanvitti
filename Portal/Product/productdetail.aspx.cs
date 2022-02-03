using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace avii.product
{
    public partial class productdetail : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvItemImageBind();
                //DLnewitemsBind();
            }
        }
        protected void lnkMaker_Click(object sender, EventArgs e)
        {
            int makerguid = -1;
            if (ViewState["maker"] != null)
            {
                makerguid = Convert.ToInt32(ViewState["maker"]);
            }
            Session["model"] = null;
            Session["t"] = null;
            Response.Redirect("productlist.aspx?maker=" + makerguid);

        }
        protected void lnkCategory_Click(object sender, EventArgs e)
        {
            int makerguid = -1;
            int categoryID = -1;
            int carrierID = -1;
            if (ViewState["t"] != null)
                carrierID = Convert.ToInt32(ViewState["t"]);

            if (ViewState["cid"] != null)
                categoryID = Convert.ToInt32(ViewState["cid"]);
            if (ViewState["maker"] != null)
            {
                makerguid = Convert.ToInt32(ViewState["maker"]);
            }
            Session["model"] = null;
            //Session["t"] = null;
            Response.Redirect("productlist.aspx?maker=" + makerguid + "&cid=" + categoryID+ "&t=" + carrierID);
            //Response.Redirect("productlist.aspx?maker=" + makerguid + "&t=" + carrierID);

        }
        
        protected void gvItemImageBind()
        {
            ProductController objProductController = new ProductController();
            int itemGUID = Convert.ToInt32(Request["itemid"]);
            if (itemGUID > 0)
            {
                List<avii.Classes.InventoryItems> listItem = objProductController.getItemList(-1, itemGUID, -1, -1, -1, "", -1, "", "", "", -1, "","", -1, 0, 0, false);

                //gvItemImage.DataSource = listItem;
                //gvItemImage.DataBind();

                if (listItem != null && listItem.Count > 0)
                {
                    ViewState["maker"] = listItem[0].ItemMakerGUID;
                    lnkMaker.Text = listItem[0].ItemMaker;
                    //lblCID.Visible = true;
                    lnkCategory.Text = listItem[0].ItemCategory;
                    lbl_Item.Text = listItem[0].ItemName;
                    ViewState["cid"] = listItem[0].ItemCategoryGUID;
                    lblItemName.Text = listItem[0].ItemName;
                    lblCode.Text = listItem[0].ItemCode;
                    lblFullDesc.Text = listItem[0].ItemDesc2;
                    lblItemDesc.Text = listItem[0].ItemDesc1;
                    lblModel.Text = listItem[0].ModelNumber;
                    lblTechnology.Text = listItem[0].ItemTechnology;
                    lblProdCond.Text = listItem[0].ItemType.ToString();
                    lblUPC.Text = listItem[0].Upc;
                    lblColor.Text = listItem[0].ItemColor;
                    hdnDoc.Value = listItem[0].ItemDocument;
                    if (listItem[0].ItemDocument != null && listItem[0].ItemDocument != string.Empty)
                    {
                        imgDoc.Visible = true;
                    }
                    else
                        imgDoc.Visible = false;
                }


                List<InventoryItemImages> itemImageslist = objProductController.getItemImageList(itemGUID, -1, 1, 0);
                if (itemImageslist != null && itemImageslist.Count > 0)
                {
                    if (itemImageslist[0].ImageURL != string.Empty)
                        imgItem.ImageUrl = "~/images/products/L_" + itemImageslist[0].ImageURL;
                    else
                        imgItem.ImageUrl = "~/images/products/comingsoon.jpg";
                    //dlItemImage.DataSource = itemImageslist;
                    //dlItemImage.DataBind();
                    dlItemImages.DataSource = itemImageslist;
                    dlItemImages.DataBind();
                    imgItem.Visible = true;
                }
                else
                {
                    imgItem.ImageUrl = "~/images/products/comingsoon.jpg";
                    imgItem.Visible = true;
                    //dlItemImage.DataSource = null;
                    //dlItemImage.DataBind();
                    dlItemImages.DataSource = null;
                    dlItemImages.DataBind();
                }
                bindSpecifications(itemGUID);
                bindAttributes(itemGUID);
            }
        }
        //protected void DLnewitemsBind()
        //{
        //    int itemguid = Convert.ToInt32(Request["itemid"]);
        //    ProductController objProductController = new ProductController();
        //    List<avii.Classes.InventoryItems> newitems = objProductController.getItemList(-1, itemguid, -1, -1, -1, "", -1, "", "", "", -1, "");
        //    DLnewitems.DataSource = newitems;
        //    DLnewitems.DataBind();
        
        //}
        protected void bindSpecifications(int itemGUID)
        {
            ProductController objProductController = new ProductController();
            dlItemSpecifications.DataSource = objProductController.getItemSpecifications(itemGUID);
            dlItemSpecifications.DataBind();
            if (dlItemSpecifications.Items.Count == 0)
            {
                dlItemSpecifications.Visible = false;
            }
            else
                dlItemSpecifications.Visible = true;
        }
        protected void bindAttributes(int itemGUID)
        {
            int attvalCount = 0;
            avii.Classes.AttributeValueUtility objAttributeValue = new avii.Classes.AttributeValueUtility();
            List<avii.Classes.attributevalue> listattributevalue = objAttributeValue.getattributevalueList(-1,-1,itemGUID,"");
            dlItemAttributes.DataSource = listattributevalue;
            dlItemAttributes.DataBind();
            for (int i = 0; i < listattributevalue.Count; i++)
            {
                if (listattributevalue[i].AttributeValue == "" || listattributevalue[i].AttributeValue == null)
                {
                    attvalCount = attvalCount + 1;
                }
            }
            if (attvalCount == 0)
            {
                dlItemAttributes.Visible = false;
            }
            else
                dlItemAttributes.Visible = true;
        }
        //protected void gvItemImage_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        string imgpath = string.Empty;
        //        ProductController objProductController = new ProductController();
        //        DataList dlItemAttributes = (DataList)e.Row.FindControl("dlItemAttributes");

        //        DataList dlItemSpecifications = (DataList)e.Row.FindControl("dlItemSpecifications");
        //        Image imgItemDetail = (Image)e.Row.FindControl("imgItemDetail");
        //        Label lblImageGUID = (Label)e.Row.FindControl("lblImageGUID");
        //        //Label lblItemDesc2 = (Label)e.Row.FindControl("lblItemDesc2");
        //        //lblItemDesc2.Text = lblItemDesc2.Text.Replace("\n", "<br/>");

                
        //        List<InventoryItemImages> lsitemimageslist = objProductController.getItemImageList(Convert.ToInt32(lblImageGUID.Text.Trim()), -1, 1, 0);
        //        if (lsitemimageslist.Count > 0)
        //        {

        //            imgpath = lsitemimageslist[0].ImageURL.ToString();
        //            if (imgpath != string.Empty)
        //            {
        //                imgpath = "~/images/products/" + imgpath.Replace("_Main1", "_Main1_Medium");
        //                imgItemDetail.ImageUrl = imgpath;
        //            }
        //            else
        //                imgItemDetail.ImageUrl = "../images/products/comingsoon.jpg";
                
                   
        //        }
        //        else
        //            imgItemDetail.ImageUrl = "../images/products/comingsoon.jpg";
                

                
        //        //bindSpecifications(dlItemSpecifications, Convert.ToInt32(lblImageGUID.Text));
        //        //bindAttributes(dlItemAttributes,Convert.ToInt32(lblImageGUID.Text));
                
        //    }
        //}

        //protected void dlProducts_ItemDataBound(object sender, DataListItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        string imgpath = string.Empty;
                
        //        ProductController objProductController = new ProductController();
        //        HiddenField hdnItemID = (HiddenField)e.Item.FindControl("hdnItemID");
        //        Image img = (Image)e.Item.FindControl("imgItemThumb");
        //        List<InventoryItemImages> lsitemimageslist = objProductController.getItemImageList(Convert.ToInt32(hdnItemID.Value.Trim()), -1, 1, 0);
        //        string itemMainImage = hdnItemID.Value + "_Main1";
        //        if (lsitemimageslist.Count > 0)
        //        {
        //            imgpath = lsitemimageslist[0].ImageURL.ToString();


        //            img.ImageUrl = imgpath;
        //            if (imgpath != string.Empty)
        //            {
        //                {
        //                    imgpath = "../images/products/" + imgpath.Replace("_Main1", "_Main1_Medium");
        //                }

        //                img.ImageUrl = imgpath;
        //            }
        //            else
        //                img.ImageUrl = "../images/products/comingsoon.jgp";
        //        }
        //        else
        //            img.ImageUrl = "../images/products/comingSoon.jpg";


        //    }
        //}

    }
}
