using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace avii.Classes
{
    public class ESNAssignOperation
    {

        public static int ValidateAssignESN(ESNAssignment esnAssignment, out int poRecordCount, out string invalidLTEList, out string invalidESNList, out string invalidSkuEsnList, out string esnExistsMessage, out string badESNMessage)
        {
            int returnValue = 0;
            poRecordCount = 0;
            invalidLTEList = string.Empty;
            invalidESNList = string.Empty;
            invalidSkuEsnList = string.Empty;
            esnExistsMessage = string.Empty;
            badESNMessage = string.Empty;
            string esnXML = clsGeneral.SerializeObject(esnAssignment.EsnList);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyAccountNumber", esnAssignment.CustomerAccountNumber);
                objCompHash.Add("@piFulfillmentNumber", esnAssignment.FulfillmentNumber);
                objCompHash.Add("@piEsnXML", esnXML);

                arrSpFieldSeq = new string[] { "@piCompanyAccountNumber", "@piFulfillmentNumber", "@piEsnXML" };
                //db.ExCommand(objCompHash, "Aero_PurchaseOrderESN_UPDATE", arrSpFieldSeq, "@Output", "@InvalidESNMessage", "@InvalidSkuESNMessage", "@EsnExistsMessage", out esnList, out invalidESNList, out invalidSkuEsnList, out esnExistsMessage);
                returnValue = db.ExCommand(objCompHash, "av_PurchaseOrderAssignESN_Validate", arrSpFieldSeq, "@poRecordCount", "@InActiveSKUMessase", "@InvalidESNMessage", "@InvalidSkuESNMessage", "@EsnExistsMessage", "@BadEsnMessage", out poRecordCount, out invalidLTEList, out invalidESNList, out invalidSkuEsnList, out esnExistsMessage, out badESNMessage);
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
            return returnValue;
        }
        public static List<FulfillmentAssignESN> ValidateAssignESNNew(ESNAssignment esnAssignment, out int poRecordCount, out string invalidLTEList, 
            out string invalidESNList, out string invalidSkuEsnList, out string esnExistsMessage, out string badESNMessage, out int returnValue, 
            out string poStatus, out int statusId, out int maxQty)
        {
            //out string AlreadyInUseICCIDMessase, out string ICCIDNotExistsMessase, out string InvalidICCIDMessase, out string AlreadyMappedESNMessase, 
            List<FulfillmentAssignESN> esnList = new List<FulfillmentAssignESN>();
            FulfillmentAssignESN esnInfo = null;
            returnValue = 0;
            poRecordCount = 0;
            maxQty = 0;
            statusId = 0;
            poStatus = string.Empty;
            //AlreadyInUseICCIDMessase = string.Empty;
            //ICCIDNotExistsMessase = string.Empty;
            //InvalidICCIDMessase = string.Empty;
            //AlreadyMappedESNMessase = string.Empty;

            invalidLTEList = string.Empty;
            invalidESNList = string.Empty;
            invalidSkuEsnList = string.Empty;
            esnExistsMessage = string.Empty;
            badESNMessage = string.Empty;
            string esns = "<ItemCompanyGUID p3:nil=\"true\" xmlns:p3=\"http://www.w3.org/2001/XMLSchema-instance\" />";
            string esnXML = clsGeneral.SerializeObject(esnAssignment.EsnList);
            esnXML = esnXML.Replace(esns, "");
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyAccountNumber", esnAssignment.CustomerAccountNumber);
                objCompHash.Add("@piFulfillmentNumber", esnAssignment.FulfillmentNumber);
                objCompHash.Add("@piEsnXML", esnXML);

                arrSpFieldSeq = new string[] { "@piCompanyAccountNumber", "@piFulfillmentNumber", "@piEsnXML" };
                //db.ExCommand(objCompHash, "Aero_PurchaseOrderESN_UPDATE", arrSpFieldSeq, "@Output", "@InvalidESNMessage", "@InvalidSkuESNMessage", "@EsnExistsMessage", out esnList, out invalidESNList, out invalidSkuEsnList, out esnExistsMessage);
                //returnValue = db.ExCommand(objCompHash, "av_PurchaseOrderAssignESN_Validate", arrSpFieldSeq, "@poRecordCount", "@InActiveSKUMessase", "@InvalidESNMessage", "@InvalidSkuESNMessage", "@EsnExistsMessage", "@BadEsnMessage", out poRecordCount, out invalidLTEList, out invalidESNList, out invalidSkuEsnList, out esnExistsMessage, out badESNMessage);
                ds = db.GetDataSet(objCompHash, "av_PurchaseOrderAssignESN_ValidateNew", arrSpFieldSeq);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow row in ds.Tables[0].Rows)
                    {
                        invalidLTEList = Convert.ToString(clsGeneral.getColumnData(row, "InActiveSKUMessase", string.Empty, false));
                        esnExistsMessage = Convert.ToString(clsGeneral.getColumnData(row, "EsnExistsMessage", string.Empty, false));
                        invalidESNList = Convert.ToString(clsGeneral.getColumnData(row, "InvalidESNMessage", string.Empty, false));
                        invalidSkuEsnList = Convert.ToString(clsGeneral.getColumnData(row, "InvalidSkuESNMessage", string.Empty, false));
                        badESNMessage = Convert.ToString(clsGeneral.getColumnData(row, "BadEsnMessage", string.Empty, false));
                        returnValue = Convert.ToInt32(clsGeneral.getColumnData(row, "returnValue", 0, false));
                        maxQty = Convert.ToInt32(clsGeneral.getColumnData(row, "Maxqty", 0, false));
                        statusId = Convert.ToInt32(clsGeneral.getColumnData(row, "StatusID", 0, false));
                        poStatus = Convert.ToString(clsGeneral.getColumnData(row, "PoStatus", string.Empty, false));
                        //AlreadyInUseICCIDMessase = Convert.ToString(clsGeneral.getColumnData(row, "AlreadyInUseICCIDMessase", string.Empty, false));
                        //ICCIDNotExistsMessase = Convert.ToString(clsGeneral.getColumnData(row, "NotExistsICCIDMessase", string.Empty, false));
                        //InvalidICCIDMessase = Convert.ToString(clsGeneral.getColumnData(row, "InvalidICCIDMessase", string.Empty, false));
                        //AlreadyMappedESNMessase = Convert.ToString(clsGeneral.getColumnData(row, "AlreadyMappedESNMessase", string.Empty, false));
                    }
                }
                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        esnInfo = new FulfillmentAssignESN();
                        esnInfo.SKU = Convert.ToString(clsGeneral.getColumnData(row, "SKU", string.Empty, false));
                        esnInfo.ESN = Convert.ToString(clsGeneral.getColumnData(row, "ESN", string.Empty, false));

                        esnInfo.BatchNumber = Convert.ToString(clsGeneral.getColumnData(row, "BatchNumber", string.Empty, false));
                        esnInfo.FulfillmentNumber = esnAssignment.FulfillmentNumber;
                        esnInfo.CustomerAccountNumber = esnAssignment.CustomerAccountNumber;
                        esnInfo.LteICCID = Convert.ToString(clsGeneral.getColumnData(row, "ICCID", string.Empty, false));
                        // esnInfo.ESN = Convert.ToString(clsGeneral.getColumnData(row, "SKU", string.Empty, false));
                        esnList.Add(esnInfo);


                    }
                }
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
            return esnList;
        }

        public static List<FulfillmentAssignESN> ValidateAssignESNScan(ESNAssignment esnAssignment)
        {
            List<FulfillmentAssignESN> esnList = new List<FulfillmentAssignESN>();
            FulfillmentAssignESN esnInfo = null;
          //  returnValue = 0;
            string esnXML = clsGeneral.SerializeObject(esnAssignment.EsnList);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyAccountNumber", esnAssignment.CustomerAccountNumber);
                objCompHash.Add("@piFulfillmentNumber", esnAssignment.FulfillmentNumber);
                objCompHash.Add("@piEsnXML", esnXML);

                arrSpFieldSeq = new string[] { "@piCompanyAccountNumber", "@piFulfillmentNumber", "@piEsnXML" };
                
                dt = db.GetTableRecords(objCompHash, "av_PurchaseOrderAssignESN_Scan_ValidateNew", arrSpFieldSeq);
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        esnInfo = new FulfillmentAssignESN();
                        esnInfo.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(row, "ItemCompanyGUID", 0, false));
                        esnInfo.SKU = Convert.ToString(clsGeneral.getColumnData(row, "SKU", string.Empty, false));
                        esnInfo.ESN = Convert.ToString(clsGeneral.getColumnData(row, "ESN", string.Empty, false));
                        esnInfo.CategoryName = Convert.ToString(clsGeneral.getColumnData(row, "CategoryName", string.Empty, false));
                        esnInfo.ProductName = Convert.ToString(clsGeneral.getColumnData(row, "ItemName", string.Empty, false));
                        esnInfo.ErrorMessage = Convert.ToString(clsGeneral.getColumnData(row, "ErrorMessage", string.Empty, false));

                        esnInfo.BatchNumber = Convert.ToString(clsGeneral.getColumnData(row, "BatchNumber", string.Empty, false));
                        esnInfo.FulfillmentNumber = esnAssignment.FulfillmentNumber;
                        esnInfo.CustomerAccountNumber = esnAssignment.CustomerAccountNumber;
                        esnInfo.LteICCID = Convert.ToString(clsGeneral.getColumnData(row, "ICCID", string.Empty, false));
                        esnInfo.TrackingNumber = Convert.ToString(clsGeneral.getColumnData(row, "TrackingNumber", string.Empty, false));
                        esnList.Add(esnInfo);


                    }
                }
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
            return esnList;
        }
        public static List<FulfillmentAssignESN> ValidateAssignESNScanNew(ESNAssignment esnAssignment)
        {
            List<FulfillmentAssignESN> esnList = new List<FulfillmentAssignESN>();
            FulfillmentAssignESN esnInfo = null;
            //  returnValue = 0;
            string esnXML = clsGeneral.SerializeObject(esnAssignment.EsnList);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyAccountNumber", esnAssignment.CustomerAccountNumber);
                objCompHash.Add("@piFulfillmentNumber", esnAssignment.FulfillmentNumber);
                objCompHash.Add("@piEsnXML", esnXML);

                arrSpFieldSeq = new string[] { "@piCompanyAccountNumber", "@piFulfillmentNumber", "@piEsnXML" };

                dt = db.GetTableRecords(objCompHash, "av_PurchaseOrderAssignESN_Scan_Validate_New", arrSpFieldSeq);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        esnInfo = new FulfillmentAssignESN();
                        esnInfo.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(row, "ItemCompanyGUID", 0, false));
                        esnInfo.SKU = Convert.ToString(clsGeneral.getColumnData(row, "SKU", string.Empty, false));
                        esnInfo.ESN = Convert.ToString(clsGeneral.getColumnData(row, "ESN", string.Empty, false));
                        esnInfo.CategoryName = Convert.ToString(clsGeneral.getColumnData(row, "CategoryName", string.Empty, false));
                        esnInfo.ProductName = Convert.ToString(clsGeneral.getColumnData(row, "ItemName", string.Empty, false));
                        esnInfo.ErrorMessage = Convert.ToString(clsGeneral.getColumnData(row, "ErrorMessage", string.Empty, false));

                        esnInfo.BatchNumber = Convert.ToString(clsGeneral.getColumnData(row, "BatchNumber", string.Empty, false));
                        esnInfo.FulfillmentNumber = esnAssignment.FulfillmentNumber;
                        esnInfo.CustomerAccountNumber = esnAssignment.CustomerAccountNumber;
                        esnInfo.LteICCID = Convert.ToString(clsGeneral.getColumnData(row, "ICCID", string.Empty, false));
                        esnInfo.TrackingNumber = Convert.ToString(clsGeneral.getColumnData(row, "TrackingNumber", string.Empty, false));
                        esnList.Add(esnInfo);


                    }
                }
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
            return esnList;
        }
        public static List<FulfillmentAssignESN> ValidateAssignESN_New(ESNAssignment esnAssignment, out int poRecordCount, out string invalidLTEList,
            out string invalidESNList, out string invalidSkuEsnList, out string esnExistsMessage, out string KitEsnMessage, out int returnValue,
            out string poStatus, out int statusId, out int maxQty, out string ESNquarantine)
        {
            //out string AlreadyInUseICCIDMessase, out string ICCIDNotExistsMessase, out string InvalidICCIDMessase, out string AlreadyMappedESNMessase, 
            List<FulfillmentAssignESN> esnList = new List<FulfillmentAssignESN>();
            FulfillmentAssignESN esnInfo = null;
            returnValue = 0;
            poRecordCount = 0;
            maxQty = 0;
            statusId = 0;
            poStatus = string.Empty;
            //AlreadyInUseICCIDMessase = string.Empty;
            //ICCIDNotExistsMessase = string.Empty;
            //InvalidICCIDMessase = string.Empty;
            //AlreadyMappedESNMessase = string.Empty;

            invalidLTEList = string.Empty;
            invalidESNList = string.Empty;
            invalidSkuEsnList = string.Empty;
            esnExistsMessage = string.Empty;
            KitEsnMessage = string.Empty;
            ESNquarantine = string.Empty;
            string esns = "<ItemCompanyGUID p3:nil=\"true\" xmlns:p3=\"http://www.w3.org/2001/XMLSchema-instance\" />";
            string esnXML = clsGeneral.SerializeObject(esnAssignment.EsnList);
            esnXML = esnXML.Replace(esns, "");
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyAccountNumber", esnAssignment.CustomerAccountNumber);
                objCompHash.Add("@piFulfillmentNumber", esnAssignment.FulfillmentNumber);
                objCompHash.Add("@piEsnXML", esnXML);

                arrSpFieldSeq = new string[] { "@piCompanyAccountNumber", "@piFulfillmentNumber", "@piEsnXML" };
                //db.ExCommand(objCompHash, "Aero_PurchaseOrderESN_UPDATE", arrSpFieldSeq, "@Output", "@InvalidESNMessage", "@InvalidSkuESNMessage", "@EsnExistsMessage", out esnList, out invalidESNList, out invalidSkuEsnList, out esnExistsMessage);
                //returnValue = db.ExCommand(objCompHash, "av_PurchaseOrderAssignESN_Validate", arrSpFieldSeq, "@poRecordCount", "@InActiveSKUMessase", "@InvalidESNMessage", "@InvalidSkuESNMessage", "@EsnExistsMessage", "@BadEsnMessage", out poRecordCount, out invalidLTEList, out invalidESNList, out invalidSkuEsnList, out esnExistsMessage, out badESNMessage);
                ds = db.GetDataSet(objCompHash, "av_PurchaseOrderAssignESN_Validate_New", arrSpFieldSeq);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        invalidLTEList = Convert.ToString(clsGeneral.getColumnData(row, "InActiveSKUMessase", string.Empty, false));
                        esnExistsMessage = Convert.ToString(clsGeneral.getColumnData(row, "EsnExistsMessage", string.Empty, false));
                        invalidESNList = Convert.ToString(clsGeneral.getColumnData(row, "InvalidESNMessage", string.Empty, false));
                        invalidSkuEsnList = Convert.ToString(clsGeneral.getColumnData(row, "InvalidSkuESNMessage", string.Empty, false));
                        KitEsnMessage = Convert.ToString(clsGeneral.getColumnData(row, "KitEsnMessage", string.Empty, false));
                        ESNquarantine = Convert.ToString(clsGeneral.getColumnData(row, "ESNquarantine", string.Empty, false));
                        returnValue = Convert.ToInt32(clsGeneral.getColumnData(row, "returnValue", 0, false));
                        maxQty = Convert.ToInt32(clsGeneral.getColumnData(row, "Maxqty", 0, false));
                        statusId = Convert.ToInt32(clsGeneral.getColumnData(row, "StatusID", 0, false));
                        poStatus = Convert.ToString(clsGeneral.getColumnData(row, "PoStatus", string.Empty, false));
                        //AlreadyInUseICCIDMessase = Convert.ToString(clsGeneral.getColumnData(row, "AlreadyInUseICCIDMessase", string.Empty, false));
                        //ICCIDNotExistsMessase = Convert.ToString(clsGeneral.getColumnData(row, "NotExistsICCIDMessase", string.Empty, false));
                        //InvalidICCIDMessase = Convert.ToString(clsGeneral.getColumnData(row, "InvalidICCIDMessase", string.Empty, false));
                        //AlreadyMappedESNMessase = Convert.ToString(clsGeneral.getColumnData(row, "AlreadyMappedESNMessase", string.Empty, false));
                    }
                }
                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        esnInfo = new FulfillmentAssignESN();
                        esnInfo.SKU = Convert.ToString(clsGeneral.getColumnData(row, "SKU", string.Empty, false));
                        esnInfo.ESN = Convert.ToString(clsGeneral.getColumnData(row, "ESN", string.Empty, false));

                        esnInfo.BatchNumber = Convert.ToString(clsGeneral.getColumnData(row, "BatchNumber", string.Empty, false));
                        esnInfo.FulfillmentNumber = esnAssignment.FulfillmentNumber;
                        esnInfo.CustomerAccountNumber = esnAssignment.CustomerAccountNumber;
                        esnInfo.LteICCID = Convert.ToString(clsGeneral.getColumnData(row, "ICCID", string.Empty, false));
                        //esnInfo.ErrorMessage = Convert.ToString(clsGeneral.getColumnData(row, "KitError", string.Empty, false));
                        // esnInfo.ESN = Convert.ToString(clsGeneral.getColumnData(row, "SKU", string.Empty, false));
                        esnList.Add(esnInfo);


                    }
                }
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
            return esnList;
        }

        public static List<FulfillmentAssignESN> AssignEsnToFulfillmentOrder(ESNAssignment esnAssignment, int userID, string poSource, SqlConnection con, SqlTransaction sqlTrans, SqlCommand cmd, out int poRecordCount, out string invalidESNMessage, out string invalidSkuESNMessage, out string esnExistsNMessage)
        {
            poRecordCount = 0;
            cmd.CommandTimeout = 0;
            esnExistsNMessage = string.Empty;
            invalidSkuESNMessage = string.Empty;
            invalidESNMessage = string.Empty;
            //string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBServerName"].ToString();
            DataTable dt = new DataTable();
            string esnXML = clsGeneral.SerializeObject(esnAssignment.EsnList);

            string esns = "<ItemCompanyGUID p3:nil=\"true\" xmlns:p3=\"http://www.w3.org/2001/XMLSchema-instance\" />";
           // string esnXML = clsGeneral.SerializeObject(esnAssignment.EsnList);
            esnXML = esnXML.Replace(esns, "");

            //LOG info
            SV.Framework.Models.Fulfillment.FulfillmentLogModel logModel = new SV.Framework.Models.Fulfillment.FulfillmentLogModel();
            string response = "";

            logModel.ActionName = "Fulfillment B2C Provisioning";
            logModel.CreateUserID = userID;
            logModel.StatusID = 0;
            logModel.PO_ID = 0;
            logModel.FulfillmentNumber = esnAssignment.FulfillmentNumber;
            logModel.Comment = string.Empty;

            logModel.RequestData = esnXML;



            // string nonEsnXML = clsGeneral.SerializeObject(esnAssignment.NonEsnList);
            List<FulfillmentAssignESN> esnsList = null;
            Hashtable objCompHash = new Hashtable();
            //try
            //{

            //    try
            //    {
            //using (SqlCommand cmd = new SqlCommand("dbo.av_PurchaseOrderESN_Assign", con, sqlTrans))
            {
                if (cmd.Parameters.Contains("@piCompanyAccountNumber"))
                {

                }
                else
                {
                    cmd.Parameters.AddWithValue("@piCompanyAccountNumber", esnAssignment.CustomerAccountNumber);
                }
                if (cmd.Parameters.Contains("@piFulfillmentNumber"))
                {

                }
                else
                {
                    cmd.Parameters.AddWithValue("@piFulfillmentNumber", esnAssignment.FulfillmentNumber);
                }
                if (cmd.Parameters.Contains("@piUserID"))
                {

                }
                else
                {
                    cmd.Parameters.AddWithValue("@piUserID", userID);
                }
                if (cmd.Parameters.Contains("@piPoSource"))
                {

                }
                else
                {
                    cmd.Parameters.AddWithValue("@piPoSource", userID);
                }
                if (cmd.Parameters.Contains("@poRecordCount"))
                {

                }
                else
                {
                    cmd.Parameters.AddWithValue("@poRecordCount", SqlDbType.Int);
                    cmd.Parameters["@poRecordCount"].Direction = ParameterDirection.Output;

                }
                if (cmd.Parameters.Contains("@piNonEsnXML"))
                    cmd.Parameters.RemoveAt("@piNonEsnXML");

               // cmd.Parameters.RemoveAt("");
               // cmd.Parameters.RemoveAt("");
              //  cmd.Parameters.RemoveAt("");

                cmd.CommandText = "dbo.av_PurchaseOrderESN_AssignNew";
                cmd.CommandType = CommandType.StoredProcedure;
                //  cmd.Parameters.AddWithValue("@piCompanyAccountNumber", esnAssignment.FulfillmentNumber);
                //   cmd.Parameters.AddWithValue("@piFulfillmentNumber", esnAssignment.CustomerAccountNumber);
                if (cmd.Parameters.Contains("@piEsnXML"))
                {

                }
                else
                {
                    cmd.Parameters.AddWithValue("@piEsnXML", esnXML);
                }
                //cmd.Parameters.AddWithValue("@piEsnXML", esnXML);
                //   cmd.Parameters.AddWithValue("@piUserID", userID);
                //  cmd.Parameters.AddWithValue("@piPoSource", poSource);

                //cmd.Parameters.AddWithValue("@poRecordCount", SqlDbType.Int);
                //cmd.Parameters["@poRecordCount"].Direction = ParameterDirection.Output;
                if (cmd.Parameters.Contains("@InvalidESNMessage"))
                {

                }
                else
                {
                    cmd.Parameters.AddWithValue("@InvalidESNMessage", SqlDbType.VarChar);
                    cmd.Parameters["@InvalidESNMessage"].Direction = ParameterDirection.Output;

                }
                if (cmd.Parameters.Contains("@InvalidSkuESNMessage"))
                {

                }
                else
                {
                    cmd.Parameters.AddWithValue("@InvalidSkuESNMessage", SqlDbType.VarChar);
                    cmd.Parameters["@InvalidSkuESNMessage"].Direction = ParameterDirection.Output;

                }
                if (cmd.Parameters.Contains("@EsnExistsMessage"))
                {

                }
                else
                {
                    cmd.Parameters.AddWithValue("@EsnExistsMessage", SqlDbType.VarChar);
                    cmd.Parameters["@EsnExistsMessage"].Direction = ParameterDirection.Output;
                }
                

                //cmd.Parameters["@Email"].Direction = ParameterDirection.Output;
                //cmd.Parameters["@FirstName"].Direction = ParameterDirection.Output;

                SqlDataAdapter sqlDataAdptr = new SqlDataAdapter(cmd);
                //sqlDataAdptr.SelectCommand.CommandTimeout = 0;
                // sqlTrans.Commit();
                sqlDataAdptr.Fill(dt);

                poRecordCount = Convert.ToInt32(cmd.Parameters["@poRecordCount"].Value.ToString());
                //invalidESNMessage = cmd.Parameters["@InvalidESNMessage"].Value.ToString();
                //invalidSkuESNMessage = cmd.Parameters["@InvalidSkuESNMessage"].Value.ToString();
                //esnExistsNMessage = cmd.Parameters["@EsnExistsMessage"].Value.ToString();

                //FirstName = cmd.Parameters["@FirstName"].Value.ToString();


                response = poRecordCount + " record(s) submitted successfully";
                logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);

               // SV.Framework.Fulfillment.LogOperations.FulfillmentLogInsert(logModel);

            }
            //}
            //catch (Exception e)
            //{ }
            //finally
            //{
            //    //con.Close();
            //}



            //  esnsList = PopulateFulfillmentESN(dt);
            //db.ExeCommand(objCompHash, "av_PurchaseOrderESN_Assign", arrSpFieldSeq, "@poRecordCount", out poRecordCount, "@poErrorMessage", out poErrorMessage);
            //}
            //catch (Exception objExp)
            //{
            //    throw new Exception(objExp.Message.ToString());
            //}
            //finally
            //{

            //}
            return esnsList;
        }
        public static int AssignNonEsnItemToFulfillmentOrder(List<FulfillmentAssignNonESN> itemList, string piFulfillmentNumber, int userID, string customerAccountNumber, SqlConnection con, SqlTransaction sqlTrans, SqlCommand cmd)
        {
            int poRecordCount = 0;
            //  esnExistsNMessage = string.Empty;
            //  invalidSkuESNMessage = string.Empty;
            //  invalidESNMessage = string.Empty;
            //string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBServerName"].ToString();
            //DataTable dt = new DataTable();
            string itemXML = clsGeneral.SerializeObject(itemList);


            //LOG info
            SV.Framework.Models.Fulfillment.FulfillmentLogModel logModel = new SV.Framework.Models.Fulfillment.FulfillmentLogModel();
            string response = "";

            logModel.ActionName = "Fulfillment B2C Provisioning";
            logModel.CreateUserID = userID;
            logModel.StatusID = 0;
            logModel.PO_ID = 0;
            logModel.FulfillmentNumber = piFulfillmentNumber;
            logModel.Comment = string.Empty;

            logModel.RequestData = itemXML;

            // string nonEsnXML = clsGeneral.SerializeObject(esnAssignment.NonEsnList);
            //List<FulfillmentAssignESN> esnsList = null;
            // Hashtable objCompHash = new Hashtable();
            //try
            //{

            //    try
            //    {
            //using (SqlCommand cmd = new SqlCommand("dbo.Av_Inventory_AssignmentInsertUpdate", con, sqlTrans))
            {
                cmd.CommandText = "dbo.Av_Inventory_AssignmentInsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@piCompanyAccountNumber", customerAccountNumber);
                cmd.Parameters.AddWithValue("@piFulfillmentNumber", piFulfillmentNumber);

                cmd.Parameters.AddWithValue("@piNonEsnXML", itemXML);
                cmd.Parameters.AddWithValue("@piUserID", userID);


                cmd.Parameters.AddWithValue("@poRecordCount", SqlDbType.Int);
                cmd.Parameters["@poRecordCount"].Direction = ParameterDirection.Output;


                //cmd.Parameters["@Email"].Direction = ParameterDirection.Output;
                //cmd.Parameters["@FirstName"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                //SqlDataAdapter sqlDataAdptr = new SqlDataAdapter(cmd);
                //sqlDataAdptr.Fill(dt);

                poRecordCount = Convert.ToInt32(cmd.Parameters["@poRecordCount"].Value.ToString());

                response = poRecordCount + " record(s) submitted successfully";
                logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);

                //if (itemList != null && itemList.Count > 0)
                //    SV.Framework.Fulfillment.LogOperations.FulfillmentLogInsert(logModel);

            }
            //}
            //catch (Exception e)
            //{ }
            //finally
            //{
            //    //con.Close();
            //}



            // esnsList = PopulateFulfillmentESN(dt);
            //db.ExeCommand(objCompHash, "av_PurchaseOrderESN_Assign", arrSpFieldSeq, "@poRecordCount", out poRecordCount, "@poErrorMessage", out poErrorMessage);
            //}
            //catch (Exception objExp)
            //{
            //    throw new Exception(objExp.Message.ToString());
            //}
            //finally
            //{

            //}
            return poRecordCount;
        }

        public static List<FulfillmentAssignESN> AssignEsnToFulfillmentOrderOld(ESNAssignment esnAssignment, int userID, string poSource, out int poRecordCount, out string invalidESNMessage, out string invalidSkuESNMessage, out string esnExistsNMessage)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            string esnXML = clsGeneral.SerializeObject(esnAssignment.EsnList);
           // string nonEsnXML = clsGeneral.SerializeObject(esnAssignment.NonEsnList);
            List<FulfillmentAssignESN> esnsList = null;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyAccountNumber", esnAssignment.CustomerAccountNumber);
                objCompHash.Add("@piEsnXML", esnXML);
                //objCompHash.Add("@piNonEsnXML", nonEsnXML);
                objCompHash.Add("@piUserID", userID);
                objCompHash.Add("@piPoSource", poSource);

                arrSpFieldSeq = new string[] { "@CompanyAccountNumber", "@piEsnXML", "@piUserID", "@piPoSource" };
                dt = db.GetTableRecords(objCompHash, "av_PurchaseOrderESN_AssignNew", arrSpFieldSeq, "@poRecordCount", "@InvalidESNMessage", "@InvalidSkuESNMessage", "@EsnExistsMessage", out poRecordCount, out invalidESNMessage, out invalidSkuESNMessage, out esnExistsNMessage);

                esnsList = PopulateFulfillmentESN(dt);
                //db.ExeCommand(objCompHash, "av_PurchaseOrderESN_Assign", arrSpFieldSeq, "@poRecordCount", out poRecordCount, "@poErrorMessage", out poErrorMessage);
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

        public static List<FulfillmentAssignNonESN> GetFulfillmentNonEsnItems(string fulfillmentNumber, string companyAccountNumber, out bool IsOnlyNonEsnItems, out int IsFulfillmentNumberExists, out List<FulfillmentAssignNonESN> esnLineItems, out List<FulfillmentAssignESN> esnItems, out List<POTracking> trackingList)
        {
            trackingList = new List<POTracking>();
            POTracking tracking = null;
            IsFulfillmentNumberExists = 0;
            IsOnlyNonEsnItems = false;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet(); 
            List<FulfillmentAssignNonESN> lineItems = null;
            esnLineItems = new List<FulfillmentAssignNonESN>();
            esnItems = new List<FulfillmentAssignESN>();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyAccountNumber", companyAccountNumber);
                objCompHash.Add("@piFulfillmentNumber", fulfillmentNumber);
                
                arrSpFieldSeq = new string[] { "@piCompanyAccountNumber", "@piFulfillmentNumber" };
                ds = db.GetDataSetRecords(objCompHash, "aV_PurchaseOrderNonESNItems_SELECT", arrSpFieldSeq, "@poIsFulfillmentNumberExists", out IsFulfillmentNumberExists);
                if (ds != null && ds.Tables.Count > 0)
                    lineItems = PopulateLineItems(ds.Tables[0], out IsOnlyNonEsnItems);
                if (ds != null && ds.Tables.Count > 1)
                    esnLineItems = PopulateESNLineItems(ds.Tables[1]);
                if (ds != null && ds.Tables.Count > 2)
                    esnItems = PopulateESNItems(ds.Tables[2]);
                if (ds != null && ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[3].Rows)
                    {
                        tracking = new POTracking();
                        tracking.TrackingNumber = Convert.ToString(clsGeneral.getColumnData(row, "TrackingNumber", string.Empty, false));
                        trackingList.Add(tracking);
                    }
                }

                //db.ExeCommand(objCompHash, "av_PurchaseOrderESN_Assign", arrSpFieldSeq, "@poRecordCount", out poRecordCount, "@poErrorMessage", out poErrorMessage);
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
            return lineItems;
        }

        private static List<FulfillmentAssignNonESN> PopulateLineItems(DataTable dt, out bool IsOnlyNonEsnItems)
        {
            IsOnlyNonEsnItems = false;
            FulfillmentAssignNonESN lineItem = null;
            List<FulfillmentAssignNonESN> purchaseOrderItems = null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    purchaseOrderItems = new List<FulfillmentAssignNonESN>();
                    foreach (DataRow dRowItem in dt.Rows)
                    {
                        //purchaseOrderEsn = new FulfillmentAssignESN();

                        lineItem = new FulfillmentAssignNonESN();
                        lineItem.SKU = Convert.ToString(clsGeneral.getColumnData(dRowItem, "Item_Code", string.Empty, false));
                        lineItem.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "ItemCompanyGUID", 0, false));
                        lineItem.Qty = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "Qty", 0, false));
                        lineItem.CurrentStock = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "Stock_in_hand", 0, false));
                        lineItem.POD_ID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "POD_ID", 0, false));
                        lineItem.IsAssign = Convert.ToBoolean(clsGeneral.getColumnData(dRowItem, "IsAssign", false, false));
                        IsOnlyNonEsnItems = Convert.ToBoolean(clsGeneral.getColumnData(dRowItem, "IsOnlyNonEsnItems", false, false));
                        lineItem.CategoryName = Convert.ToString(clsGeneral.getColumnData(dRowItem, "CategoryName", string.Empty, false));
                        lineItem.ProductName = Convert.ToString(clsGeneral.getColumnData(dRowItem, "ItemName", string.Empty, false));
                        lineItem.ErrorMessage = Convert.ToString(clsGeneral.getColumnData(dRowItem, "ErrorMessage", string.Empty, false));


                       
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
        private static List<FulfillmentAssignESN> PopulateESNItems(DataTable dt)
        {

            FulfillmentAssignESN lineItem = null;
            List<FulfillmentAssignESN> purchaseOrderItems = null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    purchaseOrderItems = new List<FulfillmentAssignESN>();
                    foreach (DataRow dRowItem in dt.Rows)
                    {
                        //purchaseOrderEsn = new FulfillmentAssignESN();

                        lineItem = new FulfillmentAssignESN();
                        lineItem.SKU = Convert.ToString(clsGeneral.getColumnData(dRowItem, "Item_Code", string.Empty, false));
                        lineItem.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "ItemCompanyGUID", 0, false));
                        //lineItem.Qty = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "ESNQuantityRequired", 0, false));
                        //lineItem.CurrentStock = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "Stock_in_hand", 0, false));
                        //lineItem.POD_ID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "POD_ID", 0, false));
                        lineItem.CategoryName = Convert.ToString(clsGeneral.getColumnData(dRowItem, "CategoryName", string.Empty, false));
                        lineItem.ProductName = Convert.ToString(clsGeneral.getColumnData(dRowItem, "ItemName", string.Empty, false));
                        lineItem.LteICCID = Convert.ToString(clsGeneral.getColumnData(dRowItem, "ICCID", string.Empty, false));
                        lineItem.BatchNumber = Convert.ToString(clsGeneral.getColumnData(dRowItem, "BatchNumber", string.Empty, false));
                        lineItem.IsSim = Convert.ToBoolean(clsGeneral.getColumnData(dRowItem, "IsSIM", false, false));

                        lineItem.ErrorMessage = "";
                        lineItem.TrackingNumber = "";
                        // lineItem.IsAssign = Convert.ToBoolean(clsGeneral.getColumnData(dRowItem, "IsAssign", false, false));
                        // IsOnlyNonEsnItems = Convert.ToBoolean(clsGeneral.getColumnData(dRowItem, "IsOnlyNonEsnItems", false, false));
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

        private static List<FulfillmentAssignNonESN> PopulateESNLineItems(DataTable dt)
        {            
            FulfillmentAssignNonESN lineItem = null;
            List<FulfillmentAssignNonESN> purchaseOrderItems = null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    purchaseOrderItems = new List<FulfillmentAssignNonESN>();
                    foreach (DataRow dRowItem in dt.Rows)
                    {
                        //purchaseOrderEsn = new FulfillmentAssignESN();

                        lineItem = new FulfillmentAssignNonESN();
                        lineItem.SKU = Convert.ToString(clsGeneral.getColumnData(dRowItem, "Item_Code", string.Empty, false));
                        lineItem.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "ItemCompanyGUID", 0, false));
                        lineItem.Qty = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "ESNQuantityRequired", 0, false));
                        lineItem.CurrentStock = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "Stock_in_hand", 0, false));
                        lineItem.POD_ID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "POD_ID", 0, false));
                        lineItem.CategoryName = Convert.ToString(clsGeneral.getColumnData(dRowItem, "CategoryName", string.Empty, false));
                        lineItem.ProductName = Convert.ToString(clsGeneral.getColumnData(dRowItem, "ItemName", string.Empty, false));
                        lineItem.ErrorMessage = Convert.ToString(clsGeneral.getColumnData(dRowItem, "ErrorMessage", string.Empty, false));

                        // lineItem.IsAssign = Convert.ToBoolean(clsGeneral.getColumnData(dRowItem, "IsAssign", false, false));
                        // IsOnlyNonEsnItems = Convert.ToBoolean(clsGeneral.getColumnData(dRowItem, "IsOnlyNonEsnItems", false, false));
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

        private static List<FulfillmentAssignESN> PopulateFulfillmentESN(DataTable dt)
        {

            FulfillmentAssignESN esnItem = null;
            List<FulfillmentAssignESN> purchaseOrderEsns = null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    purchaseOrderEsns = new List<FulfillmentAssignESN>();
                    foreach (DataRow dRowItem in dt.Rows)
                    {
                        //purchaseOrderEsn = new FulfillmentAssignESN();

                        esnItem = new FulfillmentAssignESN();
                        esnItem.FulfillmentNumber = (string)clsGeneral.getColumnData(dRowItem, "FulfillmentNumber", string.Empty, false);
                        esnItem.CustomerAccountNumber = (string)clsGeneral.getColumnData(dRowItem, "CompanyAccountNumber", string.Empty, false);
                        esnItem.ESN = (string)clsGeneral.getColumnData(dRowItem, "ESN", string.Empty, false);
                        esnItem.MSL = (string)clsGeneral.getColumnData(dRowItem, "MSL", string.Empty, false);
                        esnItem.SKU = (string)clsGeneral.getColumnData(dRowItem, "SKU", string.Empty, false);
                        //esnItem.FMUPC = (string)clsGeneral.getColumnData(dRowItem, "fmupc", string.Empty, false);
                        //esnItem.OTKSL = (string)clsGeneral.getColumnData(dRowItem, "OTKSL", string.Empty, false);
                        //esnItem.AKEY = (string)clsGeneral.getColumnData(dRowItem, "AKEY", string.Empty, false);
                        esnItem.LteICCID = (string)clsGeneral.getColumnData(dRowItem, "ICC_ID", string.Empty, false);
                        //esnItem.LteIMSI = (string)clsGeneral.getColumnData(dRowItem, "LteIMSI", string.Empty, false);

                        purchaseOrderEsns.Add(esnItem);
                    }



                }
            }
            catch (Exception ex)
            {
                throw new Exception("PopulatePurchaseOrderItems : " + ex.Message);
            }


            return purchaseOrderEsns;
        }


    }
}