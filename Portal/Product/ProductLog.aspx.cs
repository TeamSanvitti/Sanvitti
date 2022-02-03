using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Product
{
    public partial class ProductLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetProductLog();
            }
        }
        private void GetProductLog()
        {
            lblMsg.Text = "";
            lblCategoryName.Text = "";
            lblModelNumber.Text = "";
            lblProductName.Text = "";
            gvLog.DataSource = null;
            gvLog.DataBind();
            if (Session["itemguid"]!=null)
            {
                int itemGUID = Convert.ToInt32(Session["itemguid"]);
                if(itemGUID > 0)
                {
                    List<SV.Framework.Admin.ItemLog> logList = SV.Framework.Admin.ItemLogOperation.GetProductLog(itemGUID, 0);
                    if(logList != null && logList.Count > 0)
                    {
                        gvLog.DataSource = logList;
                        gvLog.DataBind();

                        lblCategoryName.Text = logList[0].CategoryName;
                        lblModelNumber.Text = logList[0].ModelNumber;
                        lblProductName.Text = logList[0].ProductName;


                    }
                    else
                    {
                        lblMsg.Text = "No log found!";
                    }
                }
                else
                {
                    lblMsg.Text = "Invalid request!";
                }
            }
        }
    }
}