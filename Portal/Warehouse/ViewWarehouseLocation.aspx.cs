using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Inventory;
using SV.Framework.Models.Inventory;

namespace avii.Warehouse
{
    public partial class ViewWarehouseLocation : System.Web.UI.Page
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
                //if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if (!IsPostBack)
            {
                GetWarehouseLocation();
            }
        }
        private void GetWarehouseLocation()
        {
            tblMsg.Visible = false;
            if (Session["WarehouseLocation"] != null)
            {
                string warehouseLocation = Session["WarehouseLocation"] as string;

                SV.Framework.Inventory.WarehouseOperation warehouseOperation = SV.Framework.Inventory.WarehouseOperation.CreateInstance<SV.Framework.Inventory.WarehouseOperation>();
                WarehouseStorage warehouseStorage = warehouseOperation.GetWarehouseLocationInfo(warehouseLocation);
                if (warehouseStorage != null && !string.IsNullOrEmpty(warehouseStorage.WarehouseLocation))
                {
                    lblWarehouseCity.Text = warehouseStorage.WarehouseCity;
                    lblCustomer.Text = warehouseStorage.CompanyName;
                    lblAisle.Text = warehouseStorage.Aisle;
                    lblBay.Text = warehouseStorage.Bay;
                    lblLevel.Text = warehouseStorage.RowLevel;
                    lblLocation.Text = warehouseStorage.WarehouseLocation;
                    if (warehouseStorage.ESNs != null && warehouseStorage.ESNs.Count > 0)
                    {
                        gvWHLocation.DataSource = warehouseStorage.ESNs;
                        gvWHLocation.DataBind();
                    }
                    else
                        tblMsg.Visible = true;
                }
                else
                    lblMsg.Text = "No record found!";
            }
            else
                lblMsg.Text = "Session expire!";
        }
    }
}