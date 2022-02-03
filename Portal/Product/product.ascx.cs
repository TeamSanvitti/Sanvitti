using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.product
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                technologybind();
                    //ListItem item = new ListItem("", "0");
                    //ddlmaker.Items.Insert(0, item);
                if (Session["t"] != null)
                {
                    ddltechnology.SelectedValue = Session["t"].ToString();
                    BindMakers(Convert.ToInt32(ddltechnology.SelectedValue));
                }

            }
        }

        private void BindMakers(int technologyID)
        {
            
            ddlmaker.DataSource = avii.Classes.ProductUtility.GetMakerList(technologyID);
            ddlmaker.DataTextField = "maker";
            ddlmaker.DataValueField = "MakerGuid";
            ddlmaker.DataBind();
            ddlmaker.Enabled = true;
            if (Session["maker"] != null)
            {
                ddlmaker.SelectedValue = Session["maker"].ToString();
                modelbind();
            }
        }

        protected void ddmaker_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlmodel.SelectedIndex == 0)
            Session["model"] = null;
            modelbind();

            
        }

        protected void technologybind()
        {
            avii.Classes.ProductUtility obj = new avii.Classes.ProductUtility();
            int makerid = -1;// Convert.ToInt32(ddlmaker.SelectedValue);

            ddltechnology.DataSource = avii.Classes.CarriersOperation.GetCarriersList(makerid,-1);
            ddltechnology.DataTextField = "CarrierName";
            ddltechnology.DataValueField = "CarrierGUID";
            ddltechnology.DataBind();
            ListItem item = new ListItem("", "0");
            ddltechnology.Items.Insert(0, item);
            //ddltechnology.Enabled = true;
            //if (Session["t"] != null)
            //{
            //    ddltechnology.SelectedValue = Session["t"].ToString();
            //    modelbind();
            //}
        }
        protected void modelbind()
        {
            avii.Classes.ProductUtility obj = new avii.Classes.ProductUtility();
            ListItem item = new ListItem("", "0");
            int technologyid = Convert.ToInt32(ddltechnology.SelectedValue);
            int makerid = Convert.ToInt32(ddlmaker.SelectedValue);
            if (makerid > 0 && technologyid > 0)
            {
                ddlmodel.DataSource = obj.getModelList(technologyid, makerid);
                ddlmodel.DataTextField = "modelnum";
                ddlmodel.DataValueField = "modelnum";
                ddlmodel.DataBind();
                ddlmodel.Items.Insert(0, item);
                ddlmodel.Enabled = true;
                if (Session["model"] != null)
                {
                    ddlmodel.SelectedValue = Session["model"].ToString();
                }
            }
            else
            {
                ddlmodel.DataSource = null;
                ddlmodel.DataBind();
                ddlmodel.Enabled = false;
            }

        }
        protected void ddltechnology_SelectedIndexChanged(object sender, EventArgs e)
        {

            Session["model"] = null;
            Session["t"] = null;
            Session["maker"] = null;
            BindMakers(Convert.ToInt32(ddltechnology.SelectedValue));
            if (ddlmodel.Enabled == true)
            {
                ddlmodel.SelectedIndex = 0;
                ddlmodel.Enabled = false;
            }

            //if (ddlmodel.SelectedIndex == 0)
            //    Session["model"] = null;
            //modelbind();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            string modelnum=string.Empty;
            string technologyguid = string.Empty;
            string makerguid = string.Empty;
            if (ddlmodel.SelectedIndex > 0)
            {
                modelnum = ddlmodel.SelectedValue;
                Session["model"] = modelnum;
            }
            else
                Session["model"] = null;
           if (ddltechnology.SelectedIndex > 0)
            {
                technologyguid = ddltechnology.SelectedValue;
                Session["t"] = technologyguid;
            }
           if (ddlmaker.SelectedIndex > 0)
           {
               makerguid = ddlmaker.SelectedValue;
               Session["maker"] = makerguid;
           }
           else
               Session["maker"] = null;
            
            //string url= "productlist.aspx?";

            //if(Session["model"] != null)
            //{ url = url + "model=" + modelnum + "&t=" + technologyguid + "&maker=" + makerguid)}
           if (string.IsNullOrEmpty(searchText))
               Response.Redirect("productlist.aspx?model=" + modelnum + "&t=" + technologyguid + "&maker=" + makerguid);
           else
               Response.Redirect("productlist.aspx?search=" + searchText);

        }
    }
}