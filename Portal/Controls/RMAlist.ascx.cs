using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
namespace avii.Controls
{
    public partial class RMAlist : System.Web.UI.UserControl
    {
        bool grid1SelectCommand = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["cid"] != null)
            {
                int companyID = Convert.ToInt32(Request["cid"]);
                ViewState["companyid"] = companyID;
                GetRMASummary();
            }
        }
        private void GetRMASummary()
        {
            int companyID = 0;
            int statusID = 0;
           // gvRMA.DataSource = RMAUtility.GetRMASummary(companyID, statusID);
            gvRMA.DataBind();
        }
        private List<RMADetail> ChildDataSource(int rmaGUID)
        {
            List<RMADetail> rmaDetailList = new List<RMADetail>();


            DataTable objDataTable = (DataTable)HttpContext.Current.Session["rmadetail"];
            DataRow[] rows = objDataTable.Select(string.Format("rmaGUID='{0}' ", rmaGUID));
            //DataRow searchedRow = null;
            if (rows.Length > 0)
            {
                foreach (DataRow dataRow in rows)
                {
                    RMADetail objRMADETAIL = new RMADetail();
                    objRMADETAIL.rmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                    objRMADETAIL.rmaDetGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "rmaDetGUID", 0, false));
                    objRMADETAIL.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    objRMADETAIL.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false) as string;
                   // objRMADETAIL.Reason = clsGeneral.getColumnData(dataRow, "Reason", 0, false).ToString();
                    objRMADETAIL.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));
                    objRMADETAIL.CallTime = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CallTime", 0, false));
                    objRMADETAIL.Notes = clsGeneral.getColumnData(dataRow, "Notes", string.Empty, false) as string;
                    objRMADETAIL.WaferSealed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WaferSealed", 0, false));
                    objRMADETAIL.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;
                    objRMADETAIL.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                    objRMADETAIL.Po_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "poid", 0, false));
                    objRMADETAIL.Pod_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "pod_id", 0, false));
                    objRMADETAIL.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false) as string;
                    objRMADETAIL.PurchaseOrderNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;

                    objRMADETAIL.ItemCode = clsGeneral.getColumnData(dataRow, "itemcode", string.Empty, false) as string;
                    rmaDetailList.Add(objRMADETAIL);
                }
            }
            return rmaDetailList;
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRMA.PageIndex = e.NewPageIndex;

            GetRMASummary();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Check if Add button clicked
            string gvChild;
            int iIndex = 0, rmaGUID;
            gvChild = string.Empty;

            if (e.CommandName == "sel" && grid1SelectCommand == false)
            {
                grid1SelectCommand = true;
                GridView gv = (GridView)sender;
                Int32 rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                ImageButton img = (ImageButton)gv.Rows[rowIndex].Cells[0].Controls[0];
                GridView childgv = (GridView)gv.Rows[rowIndex].FindControl("GridView2");

                //if (Session["adm"] == null)
                //{
                //    foreach (DataControlField dc in childgv.Columns)
                //    {
                //        if (dc.HeaderText.Equals("Delete")) //dc.HeaderText.Equals("Edit") ||
                //        {
                //            dc.Visible = false;
                //        }
                //    }
                //}

                if (img.AlternateText == "-")
                {
                    img.AlternateText = "+";
                    childgv.Visible = false;
                    img.ImageUrl = "../images/plus.gif";
                }
                else
                {
                    rmaGUID = Convert.ToInt32(gv.DataKeys[rowIndex].Value);
                    ViewState["rmaGUID"] = rmaGUID;
                    ViewState["RowIndex"] = rowIndex;

                    childgv.DataSource = ChildDataSource(rmaGUID);
                    childgv.DataBind();
                    childgv.Visible = true;
                    img.AlternateText = "-";
                    img.ImageUrl = "../images/minus.gif";
                }
                gvRMA.RowCommand -= GridView1_RowCommand;
            }
        }
    }
}