using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Inventory
{
    public class EsnReplacementOperation : BaseCreateInstance
    {
        public int ESNReplacementUpdate(string FulfillmentNumber, string AssignedESN, string ESN, string comment, int approvedBy, int userID, out string errorMessage)
        {
            errorMessage = "";
            int returnResult = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@FulfillmentNumber", FulfillmentNumber);
                    objCompHash.Add("@AssignedESN", AssignedESN);
                    objCompHash.Add("@ReplacedESN", ESN);
                    objCompHash.Add("@Comment", comment);
                    objCompHash.Add("@ApprovedBy", approvedBy);
                    objCompHash.Add("@UserID", userID);

                    arrSpFieldSeq = new string[] { "@FulfillmentNumber", "@AssignedESN", "@ReplacedESN", "@Comment", "@ApprovedBy", "@UserID" };
                    object obj = db.ExecuteScalar(objCompHash, "av_ESNReplacement_Update", arrSpFieldSeq);

                    if (obj != null)
                    {
                        returnResult = Convert.ToInt32(obj);
                    }
                }
                catch (Exception objExp)
                {
                    errorMessage = objExp.Message;
                    Logger.LogMessage(objExp, this);
                    // throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return returnResult;
        }

        public  List<ESNReplacedInfo> ESNReplacementValidate(string AssignedESN, string ESN)
        {
            List<ESNReplacedInfo> ESNInfo = default;//new List<ESNReplacedInfo>();
            using (DBConnect db = new DBConnect())
            {
                ESNReplacedInfo eSNReplacedInfo = default;//null;// new ESNReplacedInfo();
                string errorMessage = "";
                //DBConnect db = new DBConnect();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@AssignedESN", AssignedESN);
                    objCompHash.Add("@ESN", ESN);

                    arrSpFieldSeq = new string[] { "@AssignedESN", "@ESN" };
                    DataTable dt = db.GetTableRecords(objCompHash, "av_ESNReplacement_Validate", arrSpFieldSeq);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ESNInfo = new List<ESNReplacedInfo>();
                        foreach (DataRow dRowItem in dt.Rows)
                        {
                            eSNReplacedInfo = new ESNReplacedInfo();
                            eSNReplacedInfo.ESN = Convert.ToString(clsGeneral.getColumnData(dRowItem, "ESN", string.Empty, false));
                            eSNReplacedInfo.SKU = Convert.ToString(clsGeneral.getColumnData(dRowItem, "SKU", string.Empty, false));
                            eSNReplacedInfo.CategoryName = Convert.ToString(clsGeneral.getColumnData(dRowItem, "CategoryName", string.Empty, false));
                            eSNReplacedInfo.ErrorMessage = Convert.ToString(clsGeneral.getColumnData(dRowItem, "ErrorMessage", string.Empty, false));
                            eSNReplacedInfo.ItemName = Convert.ToString(clsGeneral.getColumnData(dRowItem, "ItemName", string.Empty, false));
                            eSNReplacedInfo.Location = Convert.ToString(clsGeneral.getColumnData(dRowItem, "Location", string.Empty, false));
                            eSNReplacedInfo.MeidDec = Convert.ToString(clsGeneral.getColumnData(dRowItem, "MeidDec", string.Empty, false));
                            eSNReplacedInfo.MeidHex = Convert.ToString(clsGeneral.getColumnData(dRowItem, "MeidHex", string.Empty, false));
                            eSNReplacedInfo.BOXID = Convert.ToString(clsGeneral.getColumnData(dRowItem, "BOXID", string.Empty, false));
                            eSNReplacedInfo.ESNType = Convert.ToString(clsGeneral.getColumnData(dRowItem, "ESNType", string.Empty, false));
                            eSNReplacedInfo.UploadDate = Convert.ToDateTime(clsGeneral.getColumnData(dRowItem, "UploadDate", string.Empty, false)).ToString("MM-dd-yyyy");
                            eSNReplacedInfo.KitID = Convert.ToInt64(clsGeneral.getColumnData(dRowItem, "KitID", 0, false));
                            //eSNReplacedInfo.SerialNumber = Convert.ToString(clsGeneral.getColumnData(dRowItem, "MeidHex", string.Empty, false));
                            //eSNReplacedInfo.SerialNumber = Convert.ToString(clsGeneral.getColumnData(dRowItem, "MeidHex", string.Empty, false));
                            ESNInfo.Add(eSNReplacedInfo);
                        }
                    }
                }
                catch (Exception objExp)
                {
                    errorMessage = objExp.Message;
                    Logger.LogMessage(objExp, this);// throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return ESNInfo;
        }
        public string ESNReplacementValidateOld(string AssignedESN, string ESN)
        {
            string errorMessage = "";
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@AssignedESN", AssignedESN);
                    objCompHash.Add("@ESN", ESN);

                    arrSpFieldSeq = new string[] { "@AssignedESN", "@ESN" };
                    object obj = db.ExecuteScalar(objCompHash, "av_ESNReplacement_Validate", arrSpFieldSeq);
                    if (obj != null)
                    {
                        errorMessage = Convert.ToString(obj);
                    }
                }
                catch (Exception objExp)
                {
                    errorMessage = objExp.Message;
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return errorMessage;
        }

        public  FulfillemntInfo GetFulfillmentInfo(string fulfillmentNumber, int companyID, string ESN, out string Status)
        {
            FulfillemntInfo fulfillemntInfo = default;//new FulfillemntInfo();
            Status = "";
            using (DBConnect db = new DBConnect())
            {
                List<FulfillmentLineItem> lineItems = null;
                string[] arrSpFieldSeq;
                DataSet ds = default;//new DataSet();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@piCompanyID", companyID);
                    objCompHash.Add("@piFulfillmentNumber", fulfillmentNumber);
                    objCompHash.Add("@piESN", ESN);

                    arrSpFieldSeq = new string[] { "@piCompanyID", "@piFulfillmentNumber", "@piESN" };
                    ds = db.GetDataSetRecords(objCompHash, "av_PurchaseOrderESNReplacement_Select", arrSpFieldSeq, "@poStatus", out Status);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            fulfillemntInfo = new FulfillemntInfo();
                            fulfillemntInfo.FulfillemntNumber = Convert.ToString(clsGeneral.getColumnData(row, "FulfillmentNumber", string.Empty, false));
                            fulfillemntInfo.PoStatus = Convert.ToString(clsGeneral.getColumnData(row, "PoStatus", string.Empty, false));
                            fulfillemntInfo.ESN = Convert.ToString(clsGeneral.getColumnData(row, "ESN", string.Empty, false));
                            fulfillemntInfo.ContactName = Convert.ToString(clsGeneral.getColumnData(row, "ContactName", string.Empty, false));
                            fulfillemntInfo.PoType = Convert.ToString(clsGeneral.getColumnData(row, "PoType", string.Empty, false));
                            fulfillemntInfo.FulfillemntDate = Convert.ToDateTime(clsGeneral.getColumnData(row, "PODate", DateTime.Now, false)).ToString("MM/dd/yyyy");

                            if (ds.Tables.Count > 2)
                            {
                                foreach (DataRow row2 in ds.Tables[2].Rows)

                                {
                                    fulfillemntInfo.Location = Convert.ToString(clsGeneral.getColumnData(row2, "Location", string.Empty, false));
                                    fulfillemntInfo.MeidDec = Convert.ToString(clsGeneral.getColumnData(row2, "MeidDec", string.Empty, false));
                                    fulfillemntInfo.MeidHex = Convert.ToString(clsGeneral.getColumnData(row2, "MeidHex", string.Empty, false));
                                    fulfillemntInfo.BOXID = Convert.ToString(clsGeneral.getColumnData(row2, "BOXID", string.Empty, false));
                                    fulfillemntInfo.ESNType = Convert.ToString(clsGeneral.getColumnData(row2, "ESNType", string.Empty, false));
                                    fulfillemntInfo.UploadDate = Convert.ToString(clsGeneral.getColumnData(row2, "UploadDate", string.Empty, false));
                                    fulfillemntInfo.AssignmentDate = Convert.ToString(clsGeneral.getColumnData(row2, "UploadDate", string.Empty, false));
                                    fulfillemntInfo.KitID = Convert.ToInt64(clsGeneral.getColumnData(row2, "KitID", string.Empty, false));
                                }
                            }
                        }
                        if (ds != null && ds.Tables.Count > 1)
                        {
                            lineItems = PopulateLineItems(ds.Tables[1]);
                            fulfillemntInfo.LineItems = lineItems;
                        }


                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //  db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return fulfillemntInfo;
        }

        private  List<FulfillmentLineItem> PopulateLineItems(DataTable dt)
        {

            FulfillmentLineItem lineItem = default;//null;
            List<FulfillmentLineItem> purchaseOrderItems = default;//null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    purchaseOrderItems = new List<FulfillmentLineItem>();
                    foreach (DataRow dRowItem in dt.Rows)
                    {
                        //purchaseOrderEsn = new FulfillmentAssignESN();

                        lineItem = new FulfillmentLineItem();
                        lineItem.SKU = Convert.ToString(clsGeneral.getColumnData(dRowItem, "Item_Code", string.Empty, false));
                        //lineItem.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "ItemCompanyGUID", 0, false));
                        lineItem.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "Qty", 0, false));
                        //lineItem.CurrentStock = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "Stock_in_hand", 0, false));
                        //lineItem.POD_ID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "POD_ID", 0, false));
                        lineItem.CategoryName = Convert.ToString(clsGeneral.getColumnData(dRowItem, "CategoryName", string.Empty, false));
                        lineItem.ProductName = Convert.ToString(clsGeneral.getColumnData(dRowItem, "ItemName", string.Empty, false));
                        purchaseOrderItems.Add(lineItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("purchaseOrderItems : " + ex.Message);
            }


            return purchaseOrderItems;
        }

    }
}
