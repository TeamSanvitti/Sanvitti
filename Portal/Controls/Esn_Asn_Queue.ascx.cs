using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using avii.Classes;

namespace avii.Controls
{
    public partial class Esn_Asn_Queue : System.Web.UI.UserControl
    {
        private List<PurchaseOrderESN> esnList = null;
        private List<PurchaseOrderESN> asnList = null;

        public enum QueueType
        {
            ASN_QUEUE,
            ESN_QUEUE,
            ALL_QUEUES
        }

        private int customerID = 0;
        private QueueType que;
        /*
        public List<PurchaseOrderESN> ESNList
        {
            get
            {
                 return esnList;             
            }
        }

        public List<PurchaseOrderESN> ASNList
        {
            get
            {
                return asnList;
            }
        }

         */ 
        public int CustomerID 
        {
            get
            {
                return customerID;
            }
            set
            {
                customerID = value;
            }
        }


        public QueueType ESN_ASN_QueueType
        {
            get
            {
                return que;
            }
            set
            {
                que = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        public void LoadData()
        {
            if (que == QueueType.ASN_QUEUE)
            {
                rptASN.Visible = true;
                rptASN.DataSource = GetASN(customerID);
                rptASN.DataBind();
            }

            else if (que == QueueType.ESN_QUEUE)
            {
                rptESN.Visible = true;
                rptESN.DataSource = GetESN(customerID);
                rptESN.DataBind();
            }
            else
            {
                rptASN.Visible = true;
                rptASN.DataSource = GetASN(customerID);
                rptASN.DataBind();

                rptESN.Visible = true;
                rptESN.DataSource = GetESN(customerID);
                rptESN.DataBind();
           
            }
        }

        public void Rebind()
        {
            rptASN.DataSource = asnList;
            rptASN.DataBind();

            rptESN.DataSource = esnList;
            rptESN.DataBind();
        
        }

        private List<PurchaseOrderESN> GetASN(int customerID)
        {
            asnList = avii.Classes.PurchaseOrder.GetAsnToSend(customerID);

            return asnList;
        }

        private List<PurchaseOrderESN> GetESN(int customerID)
        {
             esnList = avii.Classes.PurchaseOrder.GetEsnToSend(customerID);

            return esnList;
        }

        protected void rptESN_DataBound(object sender,
                    System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            PurchaseOrderESN esn = e.Item.DataItem as PurchaseOrderESN;
            if (esn != null)
            {
                Panel pnl = e.Item.FindControl("pnlEsn") as Panel;
                if (pnl != null)
                {
                    pnl.Controls.Add(new LiteralControl(GetRepeaterESNData(esn, e.Item.ItemType)));
                }
            }
        }

        private string GetRepeaterESNData(PurchaseOrderESN esn, ListItemType itemType)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
         //   sb.Append("<Table width='100%' cellspacing='1' cellpadding='1'>");
            foreach (PurchaseOrderESNItem eItem in esn.PurchaseOrderESNItems)
            {
                if (itemType == ListItemType.AlternatingItem)
                    sb.Append("<TR class='copy10grey' bgcolor = 'gainsboro'>");
                else
                    sb.Append("<TR>");

                sb.Append("<TD class='copy10grey'>" + esn.PurchaseOrderNumber + "</TD>");
                sb.Append("<TD class='copy10grey'>" + eItem.LineNo + "</TD>");
                sb.Append("<TD class='copy10grey'>" + eItem.ESN + "</TD>");
                sb.Append("<TD class='copy10grey'>" + eItem.MslNumber + "</TD>");
                sb.Append("<TD class='copy10grey'>" + eItem.FmUPC + "</TD>");
                sb.Append("</TR>");

            }
          //  sb.Append("</Table>");

            return sb.ToString();

        }
    }
}