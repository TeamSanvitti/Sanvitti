using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
namespace avii.product
{
    public partial class productlist : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int companyID = -1;
                if (Session["userInfo"] != null)
                {
                    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    if (userInfo != null && userInfo.UserGUID > 0)
                    {
                        companyID = userInfo.CompanyGUID;
                        
                    }
                    

                }
                int currentpageindex;
                int pagesize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pagesize"]);

                //string model = Request["model"];
                string model = string.Empty;
                List<avii.Classes.InventoryItems> listitem = new List<avii.Classes.InventoryItems>();
                int technologyguid = -1;
                int makerguid = -1;
                int categoryID = -1;
                string maker = string.Empty;
                string searchText = string.Empty;
                PagedDataSource pds = new PagedDataSource();
                ProductController objItem = new ProductController();
                if (Request["model"] != "" && Request["model"] != null)
                {
                    model = Request["model"];
                }
                if (Request["cid"] != "" && Request["cid"] != null)
                {
                    categoryID = Convert.ToInt32(Request["cid"]);
                    ViewState["cid"] = categoryID;

                }
                if (Request["t"] != "" && Request["t"] != null)
                {
                    technologyguid = Convert.ToInt32(Request["t"]);
                    ViewState["t"] = technologyguid;

                }
                if (Request["maker"] != "" && Request["maker"] != null)
                {
                    makerguid = Convert.ToInt32(Request["maker"]);
                    ViewState["maker"] = makerguid;

                }
                if (Request["brand"] != "" && Request["brand"] != null)
                {
                    maker = Convert.ToString(Request["brand"]);
                    ViewState["brand"] = maker;

                }
                if (Request["search"] != null && Request["search"] != string.Empty)
                {
                    searchText = Request["search"].ToString();

                }
                if (technologyguid == -1)
                {
                    //DropDownList dllCarrier = productid.FindControl("ddltechnology") as DropDownList;
                    if (Session["t"] != null)
                        technologyguid = Convert.ToInt32(Session["t"]);
                }
                if (technologyguid == -1 && makerguid == -1 && maker == string.Empty && model == string.Empty && searchText == string.Empty)
                {
                    BindMaker(0);
                    PanelMakers.Visible = true;
                    PanelProducts.Visible = false;
                        
                }
                else
                {

                    if (technologyguid > -1 && makerguid == -1 && maker == string.Empty && model == string.Empty && searchText == string.Empty)
                    {
                        BindMaker(technologyguid);
                        PanelMakers.Visible = true;
                        PanelProducts.Visible = false;
                        
                    }
                    else
                        if ((technologyguid > -1 || makerguid > -1) && categoryID == -1 && maker == string.Empty && model == string.Empty && searchText == string.Empty)
                        {
                            List<avii.Classes.Categories> categoryList = avii.Classes.CategoryOperations.GetItemCategoryList(technologyguid, makerguid);
                            PanelMakers.Visible = false;
                            PanelProducts.Visible = true;
                            if (categoryList.Count < 1)
                            {
                                lblCID.Visible = false;
                                lnkCategory.Text = string.Empty;
                                lblMessage.Text = "No Records Found!";
                                pnlMsg.Visible = true;
                                //Label3.Visible = false;
                                lnkMaker.Text = string.Empty;
                                dlCategory.DataSource = null;
                                dlCategory.DataBind();
                                
                                //PanelProducts.Visible = false;
                            }
                            else
                            {

                                lblOEM.Visible = true;
                                lnkMaker.Text = categoryList[0].MakerName;
                                dlCategory.DataSource = categoryList;
                                dlCategory.DataBind();
                                DLitems.DataSource = null;
                                DLitems.DataBind();
                                
                            }
                        }
                        else
                        {


                            listitem = objItem.GetProductList(categoryID, -1, 1, makerguid, technologyguid, model, -1, string.Empty, string.Empty, string.Empty, companyID, string.Empty, searchText);

                            pds.DataSource = listitem;
                            PanelMakers.Visible = false;
                            PanelProducts.Visible = true;
                            if (pds.Count < 1)
                            {
                                lblMessage.Text = "No Records Found!";
                                pnlMsg.Visible = true;
                               // Label3.Visible = false;

                                //PanelProducts.Visible = false;
                            }
                        }
                    if (listitem.Count > 0)
                    {
                        //if (makerguid > 0)
                        //{
                        //    lnkMaker.Text = listitem[0].ItemMaker;
                        //    if (string.IsNullOrEmpty(listitem[0].ItemMaker))
                        //        Label3.Visible = true;
                        //    else
                        //        Label3.Visible = false;

                        //}
                        //else if (!string.IsNullOrEmpty(maker) && maker.Trim().Length > 0)
                        //{
                        //    lnkMaker.Text = maker;
                        //    Label3.Visible = true;

                        //}
                        lblOEM.Visible = true;
                        lnkMaker.Text = listitem[0].ItemMaker;
                        lblCID.Visible = true;
                        lnkCategory.Text = listitem[0].ItemCategory;

                        //if (technologyguid > 0)
                        //{
                        //    lnkTechnology.Text = listitem[0].ItemTechnology;
                        //    if (string.IsNullOrEmpty(listitem[0].ItemTechnology))
                        //        Label1.Visible = true;
                        //    else
                        //        Label1.Visible = false;

                        //}
                        //else
                        //    Label1.Visible = false;
                        //if (!string.IsNullOrEmpty(model) && model.Trim().Length > 0)
                        //{
                        //    lnkModel.Text = listitem[0].ModelNumber;
                        //    if (string.IsNullOrEmpty(listitem[0].ModelNumber))
                        //        Label2.Visible = true;
                        //    else
                        //        Label2.Visible = false;

                        //}
                        //else
                        //    Label2.Visible = false;
                    }

                    int count = pds.Count;
                    int n = 0;
                    if (count % pagesize == 0)
                    {
                        n = count / pagesize;
                    }
                    else
                    {
                        n = count / pagesize;
                        n = n + 1;
                    }

                    if (count < pagesize)
                    {
                        btnPrev.Visible = false;
                        btnNext.Visible = false;
                        lblCurrentPage.Visible = false;
                    }
                    pds.AllowPaging = true;
                    pds.PageSize = pagesize;
                    if (Request["Page"] != null && Request["Page"] != "")
                    {
                        pds.CurrentPageIndex = Int32.Parse(Request["Page"].ToString());
                    }
                    else
                    {
                        pds.CurrentPageIndex = 0;
                    }
                    currentpageindex = pds.CurrentPageIndex;
                    ViewState["currentpageindex"] = currentpageindex;
                    btnPrev.Enabled = !pds.IsFirstPage;
                    btnNext.Enabled = !pds.IsLastPage;
                    setPagingDisplay(pds);
                    DlItemsbind(listitem, pds);


                    
                }
                //else
//BindMaker(0);
            }
            
           
        }

        private void BindMaker(int caarierID)
        {
            List<avii.Classes.ItemMaker> makerList = avii.Classes.MakerOperations.GetMakerList(caarierID);
            if (makerList.Count > 0)
            {
                //Session["maker"] = makerList;
                dlMaker.DataSource = makerList;
                dlMaker.DataBind();


            }
            else
            {
                lblMsg.Text = "No record exists";
                //Session["maker"] = null;
                dlMaker.DataSource = null;
                dlMaker.DataBind();
            }
        }

        private void DlItemsbind(List<avii.Classes.InventoryItems> listitem, PagedDataSource pds)
        {

            if (listitem != null && listitem.Count > 0)
            {
                Session["items"] = listitem;
                DLitems.DataSource = pds;
                DLitems.DataBind();
            }
            else
                Session["items"] = null;
        }
        public void Prev_Click(Object sender, EventArgs e)
        {
            string model = string.Empty;
            int makerguid = -1;
            string brand = string.Empty;
            string url = string.Empty;
            int technologyguid = -1;
            int currentpageindex = Convert.ToInt32(ViewState["currentpageindex"]);
            if (ViewState["model"] != null)
            {
                model = Convert.ToString(ViewState["model"]);
            }
            if (ViewState["maker"] != null)
            {
                makerguid = Convert.ToInt32(ViewState["maker"]);
            }
            if (ViewState["t"] != null)
            {
                technologyguid = Convert.ToInt32(ViewState["t"]);
            }
            if (ViewState["brand"] != null)
            {
                brand = Convert.ToString(ViewState["brand"]);

            }
            Response.Redirect(Request.CurrentExecutionFilePath + "?model=" + model + "&t=" + technologyguid + "&maker=" + makerguid + "&brand=" + brand + "&Page=" + (currentpageindex - 1));
        }
        protected void setPagingDisplay(PagedDataSource pds)
        {
            
            if (pds.PageCount > 1)
                lblCurrentPage.Text = "Page: " + (pds.CurrentPageIndex + 1).ToString() + "/" + (pds.PageCount).ToString();
            else
                lblCurrentPage.Text = "Page: " + (pds.CurrentPageIndex + 1).ToString();
        }
        public void Next_Click(Object sender, EventArgs e)
        {
            string model = string.Empty;
            int makerguid = -1;
            string brand = string.Empty;
            string url = string.Empty;
            int technologyguid = -1;
            int currentpageindex = Convert.ToInt32(ViewState["currentpageindex"]);
            if (ViewState["model"] != null)
            {
                model = Convert.ToString(ViewState["model"]);
            }
            if (ViewState["maker"] != null)
            {
                makerguid = Convert.ToInt32(ViewState["maker"]);
            }
            if (ViewState["t"] != null)
            {
                technologyguid = Convert.ToInt32(ViewState["t"]);
            }
            if (ViewState["brand"] != null)
            {
                brand = Convert.ToString(ViewState["brand"]);

            }
            Response.Redirect(Request.CurrentExecutionFilePath + "?model=" + model + "&t=" + technologyguid + "&maker=" + makerguid + "&brand=" + brand + "&Page=" + (currentpageindex + 1));

        }
        protected void dlProducts_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string imgpath = string.Empty;
                string baseurl = Server.MapPath("~");
                HiddenField hdnItemID = (HiddenField)e.Item.FindControl("hdnItemID");
                int itemGUID = Convert.ToInt32(hdnItemID.Value);
                List<InventoryItemImages> lsitemimageslist = null;
                List<avii.Classes.InventoryItems> listitem;
                if (Session["items"] != null)
                {
                    listitem = new List<avii.Classes.InventoryItems>();
                    listitem = (List<avii.Classes.InventoryItems>)Session["items"];
                    var itemInfoList = (from item in listitem where item.ItemGUID.Equals(itemGUID) select item).ToList();
                    if (itemInfoList != null && itemInfoList.Count > 0)
                        lsitemimageslist = itemInfoList[0].ItemImages;
                }
                
                Image img = (Image)e.Item.FindControl("imgItemThumb");
               
                //string itemMainImage = hdnItemID.Value + "_Main1";
                if (lsitemimageslist != null && lsitemimageslist.Count > 0)
                {
                    imgpath = lsitemimageslist[0].ImageURL.ToString();


                    img.ImageUrl = imgpath;
                    if (imgpath != string.Empty)
                    {
                        {
                            imgpath = "~/images/products/s_" + imgpath;
                        }

                        img.ImageUrl = imgpath;
                        //img.ImageUrl = "CreateThumbnails.aspx?file=" + imgpath+"&h=135&w=135";
                    }
                    else
                        img.ImageUrl = "~/images/products/ComingSoon.jpg";
                    //img.ImageUrl = "CreateThumbnails.aspx?file=" + "../images/products/2.gif" + "&h=135&w=135";
                }
                else
                    img.ImageUrl = "~/images/products/ComingSoon.jpg";
                

                
            }
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
            //Response.Redirect("productlist.aspx?maker=" + makerguid + "&cid=" + categoryID+ "&t=" + carrierID);
            Response.Redirect("productlist.aspx?maker=" + makerguid + "&t=" + carrierID);

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

        protected void lnkTechnology_Click(object sender, EventArgs e)
        {
            int technologyguid = -1;
            int makerguid = -1;
            if (Request["t"] != "" && Request["t"] != null)
            {
                technologyguid = Convert.ToInt32(Request["t"]);
            }
            if (ViewState["maker"] != null)
            {
                makerguid = Convert.ToInt32(ViewState["maker"]);
            }
            Session["model"] = null;
            
            Response.Redirect("productlist.aspx?t=" + technologyguid + "&maker=" + makerguid);

        }

        protected void lnkModel_Click(object sender, EventArgs e)
        {
            string model = ViewState["model"].ToString();
            int technologyguid = -1;
            int makerguid = -1;
            Response.Redirect("productlist.aspx?model=" + model + "&t=" + technologyguid + "&maker=" + makerguid);

        }
        
    }
}
