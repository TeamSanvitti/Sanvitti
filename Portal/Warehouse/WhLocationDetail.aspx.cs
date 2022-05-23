using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;
using SV.Framework.Inventory;
//using System.Linq;

namespace avii.Warehouse
{
    public partial class WhLocationDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GetWhLocationDetail();
        }
        private void GetWhLocationDetail()
        { 
            lblMsg.Text = "";
            lblAccessory.Text = "";
            lblESN.Text = "";
            if (Session["whlocationsearchcriteria"] != null)
            {
                SV.Framework.Inventory.WarehouseOperation warehouseOperation = SV.Framework.Inventory.WarehouseOperation.CreateInstance<SV.Framework.Inventory.WarehouseOperation>();

                string whlocationsearchcriteria = Session["whlocationsearchcriteria"] as string; 
                if(whlocationsearchcriteria != null)
                {
                    string[] array = whlocationsearchcriteria.Split(',');
                    List<NonEsnStorage> accessoryLoactionList = default;
                    List<EsnInfo> esnList = warehouseOperation.GetWarehouseLocationDetail(Convert.ToInt32(array[1]), array[0], array[2], array[3], out accessoryLoactionList);
                    if (esnList != null && esnList.Count > 0)
                    {
                        lblESN.Text = "<b>Total ESN(s): </b>" + esnList.Count.ToString();

                        rptSKUESN.DataSource = esnList;
                        rptSKUESN.DataBind();
                        lblMsg.Text = "";
                    }
                    
                    else if (accessoryLoactionList != null && accessoryLoactionList.Count > 0)
                    {
                        var totalQty = accessoryLoactionList.Sum(x => x.Quantity);
                        lblAccessory.Text = "<b>Total Quatity: </b>" + totalQty.ToString();


                        rptAccessory.DataSource = accessoryLoactionList;
                        rptAccessory.DataBind();
                        lblMsg.Text = "";
                    }
                    else
                    {
                        {
                            rptSKUESN.DataSource = null;
                            rptSKUESN.DataBind();
                        //    tblESN.Visible = false;
                            lblMsg.Text = "No record found!";
                        }
                        rptAccessory.DataSource = accessoryLoactionList;
                        rptAccessory.DataBind();
                        lblMsg.Text = "No record found!";
                        tblAccessory.Visible = false;
                    }


                }

            }
        }
    }
}