using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
{
    public class ContainerProvisioningOperation
    {
        public static List<ContainerESNInfo> GetContainerESNsNew(int companyID, string fulfillmentNumber, List<ContainerESN> esnList) 
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            string esnXML = clsGeneral.SerializeObject(esnList);

            List<ContainerESNInfo> esnsList = null;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyID", companyID);
                objCompHash.Add("@piFulfillmentNumber", fulfillmentNumber);
                objCompHash.Add("@piEsnXML", esnXML);

                arrSpFieldSeq = new string[] { "@piCompanyID", "@piFulfillmentNumber", "@piEsnXML" };
                dt = db.GetTableRecords(objCompHash, "av_PurchaseOrderContainerIDAssign_Validate_New", arrSpFieldSeq);

                esnsList = PopulateContanerESN(dt);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return esnsList;
        }
        private static DataTable ESNData(List<ContainerESN> EsnList)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ContainerID", typeof(System.String));
            dt.Columns.Add("ESN", typeof(System.String));
            dt.Columns.Add("ICCID", typeof(System.String));
            dt.Columns.Add("TrackingNumber", typeof(System.String));
            dt.Columns.Add("SNo", typeof(System.Int32));
            dt.Columns.Add("Location", typeof(System.String));
            DataRow row;

            if (EsnList != null && EsnList.Count > 0)
            {
                foreach (ContainerESN item in EsnList)
                {
                    row = dt.NewRow();
                    row["ContainerID"] = item.ContainerID;
                    row["ESN"] = item.ESN;
                    row["ICCID"] = item.ICCID;
                    row["TrackingNumber"] = item.TrackingNumber;
                    row["SNo"] = item.SNo;
                    row["Location"] = item.Location;
                   
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        public static List<ContainerESNInfo> GetContainerESNsNew2(int companyID, string fulfillmentNumber, List<ContainerESN> esnList)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            //string esnXML = clsGeneral.SerializeObject(esnList);
            DataTable esnDT = ESNData(esnList);
            List<ContainerESNInfo> esnsList = null;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyID", companyID);
                objCompHash.Add("@piFulfillmentNumber", fulfillmentNumber);
                objCompHash.Add("@esnTable", esnDT);

                arrSpFieldSeq = new string[] { "@piCompanyID", "@piFulfillmentNumber", "@esnTable" };
                dt = db.GetTableRecords(objCompHash, "av_PurchaseOrderContainerIDAssign_Validate_New2", arrSpFieldSeq);

                esnsList = PopulateContanerESN(dt);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return esnsList;
        }

        public static List<ContainerESNInfo> GetContainerESNs(int companyID, string fulfillmentNumber, List<ContainerESN> esnList)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            string esnXML = clsGeneral.SerializeObject(esnList);
            
            List<ContainerESNInfo> esnsList = null;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyID", companyID);
                objCompHash.Add("@piFulfillmentNumber", fulfillmentNumber);
                objCompHash.Add("@piEsnXML", esnXML);
                
                arrSpFieldSeq = new string[] { "@piCompanyID", "@piFulfillmentNumber", "@piEsnXML" };
                dt = db.GetTableRecords(objCompHash, "av_PurchaseOrderContainerIDAssign_ValidateNew", arrSpFieldSeq);

                esnsList = PopulateContanerESN(dt);
                
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return esnsList;
        }

        public static int  PurchaseOrderContainerESNAssign(int companyID, string fulfillmentNumber, int userId, List<ContainerESNInfo> esnList)
        {
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();
            int poRecordCount = 0;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
           // DataTable dt = new DataTable();
            string esnXML = clsGeneral.SerializeObject(esnList);

            SV.Framework.Models.Fulfillment.FulfillmentLogModel logModel = new SV.Framework.Models.Fulfillment.FulfillmentLogModel();
            string response = "";

            logModel.ActionName = "Fulfillment B2B Provisioning";
            logModel.CreateUserID = userId;
            logModel.StatusID = 0;
            logModel.PO_ID = 0;
            logModel.FulfillmentNumber = fulfillmentNumber;
            logModel.Comment = string.Empty;
            
            logModel.RequestData = esnXML;

            //  List<ContainerESNInfo> esnsList = null;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyID", companyID);
                objCompHash.Add("@piFulfillmentNumber", fulfillmentNumber);
                objCompHash.Add("@piEsnXML", esnXML);
                objCompHash.Add("@piUserID", userId);
                objCompHash.Add("@piPoSource", "U");
               
                

                arrSpFieldSeq = new string[] { "@piCompanyID", "@piFulfillmentNumber", "@piEsnXML", "@piUserID", "@piPoSource" };
                db.ExeCommand(objCompHash, "av_PurchaseOrderContainerESN_AssignNew", arrSpFieldSeq, "@poRecordCount", out poRecordCount);
                if (poRecordCount > 0)
                    response = poRecordCount + " record(s) submitted successfully";
                else
                    response = "Data not saved";
            }
            catch (Exception objExp)
            {
                logModel.Comment = objExp.Message;
                response = objExp.Message;

                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
                logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);

                logOperations.FulfillmentLogInsert(logModel);

            }
            return poRecordCount;
        }

        private static DataTable ESNDataProv(List<ContainerESNInfo> EsnList)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ContainerID", typeof(System.String));
            dt.Columns.Add("ESN", typeof(System.String));
            dt.Columns.Add("ItemCompanyGUID", typeof(System.Int32));
            dt.Columns.Add("BatchNumber", typeof(System.String));
            dt.Columns.Add("ICC_ID", typeof(System.String));
            dt.Columns.Add("TrackingNumber", typeof(System.String));
            dt.Columns.Add("SNo", typeof(System.Int32));
            dt.Columns.Add("Locations", typeof(System.String));

            DataRow row;

            if (EsnList != null && EsnList.Count > 0)
            {
                foreach (ContainerESNInfo item in EsnList)
                {
                    row = dt.NewRow();
                    row["ContainerID"] = item.ContainerID;
                    row["ESN"] = item.ESN;
                    row["ItemCompanyGUID"] = item.ItemCompanyGUID;
                    row["BatchNumber"] = item.BatchNumber;
                    row["ICC_ID"] = item.ICCID;
                    row["TrackingNumber"] = item.TrackingNumber;
                    row["SNo"] = item.SNo;
                    row["Locations"] = item.Location;

                    dt.Rows.Add(row);
                }
            }

            return dt;
        }

        public static int PurchaseOrderContainerESNAssign2(int companyID, string fulfillmentNumber, int userId, List<ContainerESNInfo> esnList, int poid)
        {
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            int poRecordCount = 0;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable esndt = ESNDataProv(esnList);
            string esnXML = clsGeneral.SerializeObject(esnList);

            SV.Framework.Models.Fulfillment.FulfillmentLogModel logModel = new SV.Framework.Models.Fulfillment.FulfillmentLogModel();
            string response = "";

            logModel.ActionName = "Fulfillment B2B Provisioning";
            logModel.CreateUserID = userId;
            logModel.StatusID = 0;
            logModel.PO_ID = poid;
            logModel.FulfillmentNumber = fulfillmentNumber;
            logModel.Comment = string.Empty;

            logModel.RequestData = esnXML;

            //  List<ContainerESNInfo> esnsList = null;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyID", companyID);
                objCompHash.Add("@piFulfillmentNumber", fulfillmentNumber);
               // objCompHash.Add("@piEsnXML", esnXML);
                objCompHash.Add("@piUserID", userId);
                objCompHash.Add("@piPoSource", "U");
                objCompHash.Add("@esnTable", esndt);

                arrSpFieldSeq = new string[] { "@piCompanyID", "@piFulfillmentNumber", "@piUserID", "@piPoSource", "@esnTable" };
                db.ExeCommand(objCompHash, "av_PurchaseOrderContainerESN_AssignNew2", arrSpFieldSeq, "@poRecordCount", out poRecordCount);
                if (poRecordCount > 0)
                    response = poRecordCount + " record(s) submitted successfully";
                else
                    response = "Data not saved";
            }
            catch (Exception objExp)
            {
                logModel.Comment = objExp.Message;
                response = objExp.Message;

                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
                logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);

                logOperations.FulfillmentLogInsert(logModel);

            }
            return poRecordCount;
        }

        private static List<ContainerESNInfo> PopulateContanerESN(DataTable dt)
        {

            ContainerESNInfo esnItem = null;
            List<ContainerESNInfo> esnList = null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    esnList = new List<ContainerESNInfo>();
                    foreach (DataRow dRowItem in dt.Rows)
                    {
                        
                        esnItem = new ContainerESNInfo();
                        esnItem.CategoryName = (string)clsGeneral.getColumnData(dRowItem, "CategoryName", string.Empty, false);
                        esnItem.ProductName = (string)clsGeneral.getColumnData(dRowItem, "ItemName", string.Empty, false);
                        esnItem.ESN = (string)clsGeneral.getColumnData(dRowItem, "ESN", string.Empty, false);
                        esnItem.BatchNumber = (string)clsGeneral.getColumnData(dRowItem, "BatchNumber", string.Empty, false);
                        esnItem.SKU = (string)clsGeneral.getColumnData(dRowItem, "SKU", string.Empty, false);
                        esnItem.ContainerID = (string)clsGeneral.getColumnData(dRowItem, "ContainerID", string.Empty, false);
                        esnItem.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "ItemCompanyGUID", 0, false));
                        esnItem.SNo = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "SNo", 0, false));

                        esnItem.ErrorMessage = (string)clsGeneral.getColumnData(dRowItem, "ErrorMessage", string.Empty, false);
                        esnItem.ICCID = (string)clsGeneral.getColumnData(dRowItem, "ICCID", string.Empty, false);
                        esnItem.TrackingNumber = (string)clsGeneral.getColumnData(dRowItem, "TrackingNumber", string.Empty, false);
                        esnItem.Location = (string)clsGeneral.getColumnData(dRowItem, "Location", string.Empty, false);
                        esnItem.LocationMessage = (string)clsGeneral.getColumnData(dRowItem, "LocationMessage", string.Empty, false);
                        
                        esnList.Add(esnItem);
                    }



                }
            }
            catch (Exception ex)
            {
                throw new Exception("PopulatePurchaseOrderItems : " + ex.Message);
            }


            return esnList;
        }

        public static List<SV.Framework.LabelGenerator.ContainerModel> GetContainerLabelInfo(int poID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();

            List<SV.Framework.LabelGenerator.ContainerModel> containers = null;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POID", poID);
               
                arrSpFieldSeq = new string[] { "@POID"};
                dt = db.GetTableRecords(objCompHash, "av_PurchaseOrderContainers_Label", arrSpFieldSeq);

                containers = PopulateContanerLabel(dt);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return containers;
        }
        private static List<SV.Framework.LabelGenerator.ContainerModel> PopulateContanerLabel(DataTable dt)
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
            List<SV.Framework.LabelGenerator.ContainerModel> containers = new List<SV.Framework.LabelGenerator.ContainerModel>();
            SV.Framework.LabelGenerator.ContainerModel containerLabelInfo = null;
            
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dRowItem in dt.Rows)
                    {
                        //containers = new List<SV.Framework.LabelGenerator.ContainerModel>();
                        containerLabelInfo = new SV.Framework.LabelGenerator.ContainerModel();
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
                        containerLabelInfo.ShippingAddressLine1= (string)clsGeneral.getColumnData(dRowItem, "ShipTo_Address", string.Empty, false);
                        containerLabelInfo.ShippingAddressLine2 = (string)clsGeneral.getColumnData(dRowItem, "ShipTo_Address2", string.Empty, false);
                        containerLabelInfo.CustomerName = (string)clsGeneral.getColumnData(dRowItem, "Contact_Name", string.Empty, false); 
                        containerLabelInfo.ShippingCity= (string)clsGeneral.getColumnData(dRowItem, "ShipTo_City", string.Empty, false);
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

    public class ContainerESN
    {
        public string ContainerID { get; set; }
        public string ESN { get; set; }
        public string ICCID { get; set; }
        public string Location { get; set; }
        public string TrackingNumber { get; set; }
        [XmlIgnore]
        public int SNo { get; set; }

    }
    public class ContainerESNInfo
    {
        public string ContainerID { get; set; }
        public string ESN { get; set; }
        [XmlIgnore]
        public string SKU { get; set; }
        [XmlIgnore]
        public string CategoryName { get; set; }
        [XmlIgnore]
        public string ProductName { get; set; }
        public int? ItemCompanyGUID { get; set; }
        public string BatchNumber { get; set; }
        [XmlIgnore]
        public string ErrorMessage { get; set; }
        public string ICCID { get; set; }
        public string TrackingNumber { get; set; }
        public string Location { get; set; }
        [XmlIgnore]
        public string LocationMessage { get; set; }
        [XmlIgnore]

        public int SNo { get; set; }

    }
    public class ContainerLabelInfo
    {
        public string ContainerID { get; set; }
        public int CasePackQuantity { get; set; }
        
        public string PO_Num { get; set; }
        
        public string ShipContact_Name { get; set; }
        
        public string ShipTo_City { get; set; }
        public string ShipTo_Address1 { get; set; }
        public string ShipTo_Address2 { get; set; }
        
        public string ShipTo_State { get; set; }
        public string ShipTo_Zip { get; set; }
        public string Ship_Via { get; set; }
        public string DPCI { get; set; }
        //public string ShipTo_Zip { get; set; }
       // public string ShipTo_Zip { get; set; }

    }

}