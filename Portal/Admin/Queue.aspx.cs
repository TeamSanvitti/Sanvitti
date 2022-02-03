using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Admin
{
    public partial class Queue : System.Web.UI.Page
    {
        //avii.Controls.POList poList;
        //Control ctlEsn;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                bindCustomerDropDown();
            }

            //if (dpCompany.SelectedIndex > 0)
            //{
            //    PopulateForm();
            //}
        }

        #region generate code for delegate

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            //ctlEsn = LoadControl("../Controls/POList.ascx");
            //poList = ctlEsn as avii.Controls.POList;

            this.Load += new System.EventHandler(this.Page_Load);
           this.poList.GridRowCommand += new avii.Controls.POList.RowCommand(polist_RowCommand);
           // Inc_GridView1.GridRowDataBound += new Inc_GridView.RowDataBound(Inc_GridView1_GridRowDataBound);
        }
        #endregion

        protected void bindCustomerDropDown()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            dpCompany.SelectedIndex = 0;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            PopulateForm();
        }

        private void PopulateForm()
        {
            if (dpCompany.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select the Customer you want to view data";
            }
            else
            {
                if (chkESN.Checked == false && chkMSL.Checked == false && chkTracking.Checked == false)
                {
                    poList.Visible = false;
                    int companyId = 0;
                    int.TryParse(dpCompany.SelectedValue, out companyId);
                    if (companyId > 0)
                    {
                        Control ctl = LoadControl("../Controls/Esn_Asn_Queue.ascx");
                        avii.Controls.Esn_Asn_Queue e1 = ctl as avii.Controls.Esn_Asn_Queue;
                        e1.CustomerID = companyId;
                        e1.ESN_ASN_QueueType = avii.Controls.Esn_Asn_Queue.QueueType.ASN_QUEUE;
                        e1.LoadData();
                        pnlAsn.Controls.Add(ctl);

                        Control ctlEsn = LoadControl("../Controls/Esn_Asn_Queue.ascx");
                        avii.Controls.Esn_Asn_Queue e2 = ctlEsn as avii.Controls.Esn_Asn_Queue;
                        e2.CustomerID = companyId;
                        e2.ESN_ASN_QueueType = avii.Controls.Esn_Asn_Queue.QueueType.ESN_QUEUE;
                        e2.LoadData();
                        pnlEsn.Controls.Add(ctlEsn);
                    }
                    else
                    {
                        lblMsg.Text = "Please select Customer";
                    }
                }
                else
                {
                    poList.Visible = true;

                    avii.Classes.MissingList missingList = avii.Classes.MissingList.ESN_MSL;
                    if (chkESN.Checked && chkMSL.Checked == false && chkTracking.Checked == false)
                    {
                        missingList = avii.Classes.MissingList.ESN;
                    }
                    else if (chkMSL.Checked && chkESN.Checked == false && chkTracking.Checked == false)
                    {
                        missingList = avii.Classes.MissingList.MSL;
                    }
                    else if (chkESN.Checked && chkMSL.Checked && chkTracking.Checked == false)
                    {
                        missingList = avii.Classes.MissingList.ESN_MSL;
                    }
                    else if (chkESN.Checked == false && chkMSL.Checked == false && chkTracking.Checked)
                    {
                        missingList = avii.Classes.MissingList.ASN;
                    }
                    else if (chkESN.Checked && chkMSL.Checked && chkTracking.Checked)
                    {
                        missingList = avii.Classes.MissingList.ESN_MSL_ASN;
                    }
                    else if (chkESN.Checked == false && chkMSL.Checked && chkTracking.Checked)
                    {
                        missingList = avii.Classes.MissingList.MSL_ASN;
                    }

                    int companyId = 0;
                    int.TryParse(dpCompany.SelectedValue, out companyId);
                    if (companyId > 0)
                    {
                        poList.LoadControl(companyId, null, missingList);
                    }
                    else
                    {
                        lblMsg.Text = "Please select Customer";
                    }

                }
            }
        }

        void polist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Check if Add button clicked
            string gvChild;
            int iIndex = 0, poID;
            gvChild = string.Empty;

            if (e.CommandName == "sel")
            {
                GridView gv = e.CommandSource as GridView;
                Int32 rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                ImageButton img = (ImageButton)gv.Rows[rowIndex].Cells[0].Controls[0];
                GridView childgv = (GridView)gv.Rows[rowIndex].FindControl("GridView2");


                if (img.AlternateText == "-")
                {
                    img.AlternateText = "+";
                    childgv.Visible = false;
                    img.ImageUrl = "../images/plus.gif";
                }
                else
                {
                    poID = Convert.ToInt32(gv.DataKeys[rowIndex].Value);
                    childgv.DataSource = ChildDataSource(poID, string.Empty);
                    childgv.DataBind();
                    childgv.Visible = true;
                    img.AlternateText = "-";
                    img.ImageUrl = "../images/minus.gif";
                }
            }
        }


        //This procedure prepares the query to bind the child GridView
        private List<avii.Classes.BasePurchaseOrderItem> ChildDataSource(int poID, string strSort)
        {
            avii.Classes.PurchaseOrders purchaseOrders = (avii.Classes.PurchaseOrders)Session["missingrec"];
            if (purchaseOrders != null && purchaseOrders.PurchaseOrderList.Count > 0)
            {
                Classes.BasePurchaseOrder purchaseOrder = purchaseOrders.FindPurchaseOrder(poID);

                return purchaseOrder.PurchaseOrderItems;
            }
            else
            {
                return null;
            }
        }

    }
}
