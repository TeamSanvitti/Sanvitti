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
    public partial class Managewarehouse : System.Web.UI.Page
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
                BindCompany();
                BindWarehouse();
                if(Session["WarehouseStorageID"] != null)
                {                    
                    int warehouseStorageID = Convert.ToInt32(Session["WarehouseStorageID"]);
                    ViewState["WarehouseStorageID"] = warehouseStorageID;
                    GetWarehouseDetail(warehouseStorageID);
                    Session["WarehouseStorageID"] = null;
                    btBacktoSearch.Visible = true;
                    btnNewLocation.Visible = false;
                }
            }
        }
        private void GetWarehouseDetail(int warehouseStorageID)
        {
            SV.Framework.Inventory.WarehouseOperation warehouseOperation = SV.Framework.Inventory.WarehouseOperation.CreateInstance<SV.Framework.Inventory.WarehouseOperation>();
            WarehouseStorage warehouseStorage = warehouseOperation.GetWarehouseStorageInfo(warehouseStorageID);
            if(warehouseStorage != null && warehouseStorage.WarehouseStorageID > 0)
            {               
                ddlWarehouse.SelectedValue = warehouseStorage.WarehouseID.ToString();
                ddlCompany.SelectedValue = warehouseStorage.CompanyID.ToString();
                txtAisle.Text = warehouseStorage.Aisle;
                txtBay.Text = warehouseStorage.Bay;
                txtLevel.Text = warehouseStorage.RowLevel;
                txtWarehouseCode.Text = warehouseStorage.WarehouseLocation;
                txtSpecialInstructions.Text = warehouseStorage.Specialinstructions;
            }
        }

        private void BindCompany()
        {
            ddlCompany.DataSource = avii.Classes.RMAUtility.getRMAUserCompany(-1, "", 1, -1);
            ddlCompany.DataValueField = "companyID";
            ddlCompany.DataTextField = "companyName";
            ddlCompany.DataBind();
            ListItem item = new ListItem("", "0");
            ddlCompany.Items.Insert(0, item);
        }
        private void BindWarehouse()
        {
            SV.Framework.Inventory.WarehouseOperation warehouseOperation = SV.Framework.Inventory.WarehouseOperation.CreateInstance<SV.Framework.Inventory.WarehouseOperation>();
            ddlWarehouse.DataSource = warehouseOperation.GetWarehouse(0);
            ddlWarehouse.DataValueField = "WarehouseID";
            ddlWarehouse.DataTextField = "WarehouseCity";
            ddlWarehouse.DataBind();
            ListItem item = new ListItem("", "0");
            ddlWarehouse.Items.Insert(0, item);
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlCompany.SelectedIndex = 0;
            ddlWarehouse.SelectedIndex = 0;

            CrearForm();
        }
        private void CrearForm()
        {
            lblMsg.Text = "";
            txtAisle.Text = "";
            txtBay.Text = "";
            txtLevel.Text = "";
            txtWarehouseCode.Text = "";
            txtSpecialInstructions.Text = "";
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            WarehouseOperation warehouseOperation = WarehouseOperation.CreateInstance<WarehouseOperation>();
            WarehouseStorage warehouseInfo = default; //new WarehouseInfo();
            string errorMessage = default;
            int userID = Convert.ToInt32(Session["UserID"]);

            string warehouseLocation = txtWarehouseCode.Text.Trim();
            string aisle = txtAisle.Text.Trim();
            string bay = txtBay.Text.Trim();
            string level = txtLevel.Text.Trim();
            int warehouseID = Convert.ToInt32(ddlWarehouse.SelectedValue);
            int companyID = Convert.ToInt32(ddlCompany.SelectedValue);
            int WarehouseStorageID = 0;
            if (ViewState["WarehouseStorageID"] != null)
                WarehouseStorageID = Convert.ToInt32(ViewState["WarehouseStorageID"]);
            if(ddlWarehouse.SelectedIndex > 0)
            {
                warehouseInfo = new WarehouseStorage();
                warehouseInfo.CompanyID = companyID;
                warehouseInfo.UserID = userID;
                warehouseInfo.WarehouseStorageID = WarehouseStorageID;
                warehouseInfo.WarehouseID = warehouseID;
                warehouseInfo.WarehouseLocation = warehouseLocation;
                warehouseInfo.Aisle = aisle;
                warehouseInfo.Bay = bay;
                warehouseInfo.RowLevel = level;
                warehouseInfo.Specialinstructions = txtSpecialInstructions.Text.Trim();
                if (string.IsNullOrWhiteSpace(warehouseOperation.ValidateWarehouseStorage(warehouseInfo)))
                {
                    errorMessage = warehouseOperation.CreateWarehouseStorage(warehouseInfo);
                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        lblMsg.Text = "Submitted successfully";
                    }
                    else
                        lblMsg.Text = errorMessage;
                }
                else
                    lblMsg.Text = errorMessage;
            }
        }

        protected void btBacktoSearch_Click(object sender, EventArgs e)
        {
            Session["wh"] = 1;
            Response.Redirect("warehousesearch.aspx");
        }

        protected void btnNewLocation_Click(object sender, EventArgs e)
        {
            Session["wh"] = null;
            ViewState["WarehouseStorageID"] = null;
            CrearForm();
        }
    }
}