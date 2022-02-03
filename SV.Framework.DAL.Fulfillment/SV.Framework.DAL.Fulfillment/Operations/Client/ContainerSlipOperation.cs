using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Fulfillment
{
    public class ContainerSlipOperation:BaseCreateInstance
    {
        public  List<ContainerModel> GetContainerLabelInfo(int poID)
        {
            List<ContainerModel> containers = default;//null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();


                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POID", poID);

                    arrSpFieldSeq = new string[] { "@POID" };
                    dt = db.GetTableRecords(objCompHash, "av_PurchaseOrderContainers_Label", arrSpFieldSeq);

                    containers = PopulateContanerLabel(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return containers;
        }
        private  List<ContainerModel> PopulateContanerLabel(DataTable dt)
        {
            string shipFromContactName = ConfigurationSettings.AppSettings["ShipFromContactName"].ToString();
            string shipFromContactNameR = ConfigurationSettings.AppSettings["ShipFromContactName2"].ToString();
            string shipFromAddress = ConfigurationSettings.AppSettings["ShipFromAddress"].ToString();
            string shipFromCity = ConfigurationSettings.AppSettings["ShipFromCity"].ToString();
            string shipFromState = ConfigurationSettings.AppSettings["ShipFromState"].ToString();
            string shipFromZip = ConfigurationSettings.AppSettings["ShipFromZip"].ToString();
            string shipFromCountry = ConfigurationSettings.AppSettings["ShipFromCountry"].ToString();
            string shipFromAttn = ConfigurationSettings.AppSettings["ShipFromAttn"].ToString();
            string shipFromPhone = ConfigurationSettings.AppSettings["ShipFromPhone"].ToString();

            int companyID = 0;
            string CompanyName = string.Empty;
            List<ContainerModel> containers = default;//new List<ContainerModel>();
            ContainerModel containerLabelInfo = default;//null;

            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    containers = new List<ContainerModel>();
                    foreach (DataRow dRowItem in dt.Rows)
                    {
                        //containers = new List<SV.Framework.LabelGenerator.ContainerModel>();
                        containerLabelInfo = new ContainerModel();
                        containerLabelInfo.DPCI = (string)clsGeneral.getColumnData(dRowItem, "DPCI", string.Empty, false);
                        containerLabelInfo.PoNumber = (string)clsGeneral.getColumnData(dRowItem, "PO_Num", string.Empty, false);
                        containerLabelInfo.ContainerNumber = (string)clsGeneral.getColumnData(dRowItem, "ContainerID", string.Empty, false);
                        containerLabelInfo.Casepack = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "CasePackQuantity", 0, false)).ToString();
                        containerLabelInfo.ContainerCount = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "ContainerCount", 0, false)).ToString();
                        containerLabelInfo.ESNCount = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "ESNCount", 0, false)).ToString();
                        containerLabelInfo.Carrier = (clsGeneral.getColumnData(dRowItem, "Ship_Via", 0, false)).ToString();
                        companyID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "CompanyID", 0, false));

                        CompanyName = (string)clsGeneral.getColumnData(dRowItem, "CompanyName", string.Empty, false);
                        //Ship FROM
                        if (companyID == 464)
                            containerLabelInfo.CompanyName = shipFromContactName;
                        else
                            containerLabelInfo.CompanyName = shipFromContactNameR;
                        containerLabelInfo.CompanyName = CompanyName;

                        containerLabelInfo.AddressLine1 = shipFromAddress;
                        containerLabelInfo.City = shipFromCity;
                        containerLabelInfo.State = shipFromState;
                        containerLabelInfo.ZipCode = shipFromZip;
                        containerLabelInfo.Country = shipFromCountry;
                        //  containerLabelInfo.CompanyName = shipFromContactName;
                        //   containerLabelInfo.CompanyName = shipFromContactName;



                        //SHIPPING to Address
                        containerLabelInfo.ShippingAddressLine1 = (string)clsGeneral.getColumnData(dRowItem, "ShipTo_Address", string.Empty, false);
                        containerLabelInfo.ShippingAddressLine2 = (string)clsGeneral.getColumnData(dRowItem, "ShipTo_Address2", string.Empty, false);
                        containerLabelInfo.CustomerName = (string)clsGeneral.getColumnData(dRowItem, "Contact_Name", string.Empty, false);
                        containerLabelInfo.ShippingCity = (string)clsGeneral.getColumnData(dRowItem, "ShipTo_City", string.Empty, false);
                        containerLabelInfo.ShippingCountry = "USA";// (string)clsGeneral.getColumnData(dRowItem, "ICCID", string.Empty, false);
                        containerLabelInfo.ShippingState = (string)clsGeneral.getColumnData(dRowItem, "ShipTo_State", string.Empty, false);
                        containerLabelInfo.ShippingZipCode = (string)clsGeneral.getColumnData(dRowItem, "ShipTo_Zip", string.Empty, false);
                        containerLabelInfo.PostalCode = (string)clsGeneral.getColumnData(dRowItem, "ShipTo_Zip", string.Empty, false);
                        // containerLabelInfo.ShipTo_State = (string)clsGeneral.getColumnData(dRowItem, "ICCID", string.Empty, false);
                        containers.Add(containerLabelInfo);

                    }



                }
            }
            catch (Exception ex)
            {
                throw new Exception("PopulatePurchaseOrderItems : " + ex.Message);
            }


            return containers;
        }

    }
}
