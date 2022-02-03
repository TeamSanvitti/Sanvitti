using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.OEM
{
    public partial class OEMQuery : System.Web.UI.Page
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
                if (Request["search"] != null && Request["search"] != "")
                {
                    if (Session["searchcompany"] != null)
                    {
                        string searchCriteria = (string)Session["searchcompany"];
                        string[] searchArr = searchCriteria.Split('~');
                        txtMakerName.Text = searchArr[0].ToString();
                        txtShortName.Text = searchArr[1].ToString();
                        ddlActive.SelectedValue = searchArr[2].ToString();
                        ddlCatalog.SelectedValue = searchArr[3].ToString();
                        SearchMakers();
                    }
                }
            }
        }
        protected void btnSearch_click(object sender, EventArgs e)
        { 
            SearchMakers();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtMakerName.Text = string.Empty;
            txtShortName.Text = string.Empty;
            gvMakers.DataSource = null;
            gvMakers.DataBind();
            lblMsg.Text = string.Empty;
        
        }

        private void SearchMakers()
        {
            bool validForm = true;
            string makerName, shortName;
            int showunderCatalog = -1;
            int active = -1;
            makerName = shortName = null;
            try
            {
                lblMsg.Text = string.Empty;
                makerName = (txtMakerName.Text.Trim().Length > 0 ? txtMakerName.Text.Trim() : null);
                shortName = (txtShortName.Text.Trim().Length > 0 ? txtShortName.Text.Trim() : null);

                if (ddlActive.SelectedIndex > 0)
                {
                    active = Convert.ToInt32(ddlActive.SelectedValue);
                }
                if (ddlCatalog.SelectedIndex > 0)
                    showunderCatalog = Convert.ToInt32(ddlCatalog.SelectedValue); ;

                

                //if (string.IsNullOrEmpty(makerName) && string.IsNullOrEmpty(shortName))
                //{
                //    validForm = false;
                //}
                if (validForm)
                {
                    string searchCriteria = makerName + "~" + shortName + "~" + active + "~" +  showunderCatalog;
                    Session["searchcompany"] = searchCriteria;
                    List<ItemMaker> makerList = MakerOperations.GetMakerList(0, makerName, shortName, active, showunderCatalog);
                    if (makerList.Count > 0)
                    {
                        Session["maker"] = makerList;
                        gvMakers.DataSource = makerList;
                        gvMakers.DataBind();

                        
                    }
                    else
                    {
                        lblMsg.Text = "No record exists";
                        Session["maker"] = null;
                        gvMakers.DataSource = null;
                        gvMakers.DataBind();
                    }
                }
                else
                {
                    lblMsg.Text = "Please select the search criteria";
                    Session["maker"] = null;
                    gvMakers.DataSource = null;
                    gvMakers.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void imgDelete_Commnad(object sender, CommandEventArgs e)
        {
            int makerGUID = Convert.ToInt32(e.CommandArgument);
            MakerOperations.DeleteMaker(makerGUID);
            SearchMakers();
            lblMsg.Text = "Deleted successfully";


        }
        protected void imgEdit_Commnad(object sender, CommandEventArgs e)
        {
            int makerGUID = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("ManageOEM.aspx?m=" + makerGUID, false);

        }
        protected void imgView_Commnad(object sender, CommandEventArgs e)
        {
            int makerGUID = Convert.ToInt32(e.CommandArgument);
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