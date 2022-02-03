using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class SimOperations
    {
        public static bool FulfillmentSimInsert(List<FulfillmentSim> simList, int userID, string poSource, out int poRecordsCount, out string errorMessage)
        {
            bool returnValue = false;
            DBConnect db = new DBConnect();
            string simXML = clsGeneral.SerializeObject(simList);
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            //int retIntVal = 0;
            try
            {
                objCompHash.Add("@piXMLDATA", simXML);
                
                objCompHash.Add("@piUserID", userID);
                objCompHash.Add("@piPoSource", poSource);


                arrSpFieldSeq = new string[] { "@piXMLDATA", "@piUserID", "@piPoSource" };
                db.ExeCommand(objCompHash, "Av_Fulfillment_SIM_Insert", arrSpFieldSeq, "@poRecordsCount", out poRecordsCount, "@poErrorMessage", out errorMessage);
                //int.TryParse(retVal.ToString(), out retIntVal);
                //if (retIntVal == 0)
                if (errorMessage == string.Empty)
                    returnValue = true;
                else
                    returnValue = false;
            }
            catch (Exception objExp)
            {
                returnValue = false;
                throw new Exception(objExp.Message.ToString());

            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return returnValue;
        }

        public static List<FulfillmentSim> Validate_Fulfillment_SIM(List<FulfillmentSim> simList, out string errorMessage, out string simMessage, out string skuErrorMessage, out string esnSimErrorMessage, out int returnValue)
        {
            skuErrorMessage = string.Empty;
            esnSimErrorMessage = string.Empty;
            errorMessage = string.Empty;
            simMessage = string.Empty;
            returnValue = 0;
            string esnXML = clsGeneral.SerializeObject(simList);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            //List<SimList> esnList = null;
            try
            {
                objCompHash.Add("@piXMLDATA", esnXML);
                
                arrSpFieldSeq = new string[] { "@piXMLDATA" };
                dt = db.GetTableRecords(objCompHash, "Av_Fulfillment_SIM_Validation", arrSpFieldSeq, "@poErrorMessage", "@poSimMessage", "@poSKUErrorMessage", "@poEsnSimErrorMessage", out errorMessage, out simMessage, out skuErrorMessage, out esnSimErrorMessage, out returnValue);

                //esnList = PopulateMslESN(dt);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return simList;
        }

        public static List<SimInfo> GetSimList(int companyID, int itemCompanyGuid, string sim, string esn)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<SimInfo> simList = null;
            try
            {
                objCompHash.Add("@piCompanyID", companyID);
                objCompHash.Add("@piItemCompanyGUID", itemCompanyGuid);
                objCompHash.Add("@piSIM", sim);
                objCompHash.Add("@piESN", esn);

                arrSpFieldSeq = new string[] { "@piCompanyID", "@piItemCompanyGUID", "@piSIM", "@piESN" };
                dt = db.GetTableRecords(objCompHash, "Av_SIM_Select", arrSpFieldSeq);
                simList = PopulateSimInfo(dt);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return simList;

        }

        public static List<SimLog> GetSimLogList(string sim)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<SimLog> simList = null;
            try
            {
                objCompHash.Add("@piSIM", sim);
                
                arrSpFieldSeq = new string[] { "@piSIM" };
                dt = db.GetTableRecords(objCompHash, "Av_SIM_Log_Select", arrSpFieldSeq);
                simList = PopulateSimLog(dt);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return simList;

        }
       
        public static bool SimInsertUpdate(List<SimList> simList, int itemCompanyGUID, int userID, bool isDelete, string fileName, string comment, out int insertCout, out int updateCount, out string errorMessage)
        {
            bool returnValue = false;
            DBConnect db = new DBConnect();
            string simXML = clsGeneral.SerializeObject(simList);
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            //int retIntVal = 0;
            try
            {
                objCompHash.Add("@piXMLDATA", simXML);
                objCompHash.Add("@itemCompanyGUID", itemCompanyGUID);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@IsDelete", isDelete);
                objCompHash.Add("@FileName", fileName);
                objCompHash.Add("@Comment", comment);


                arrSpFieldSeq = new string[] { "@piXMLDATA", "@itemCompanyGUID", "@UserID", "@IsDelete", "@FileName", "@Comment" };
                db.ExeCommand(objCompHash, "Av_SIM_InsertUpdate", arrSpFieldSeq, "@poInsertCount", out insertCout, "@poUpdateCount", out updateCount, "@poErrorMessage", out errorMessage);
                //int.TryParse(retVal.ToString(), out retIntVal);
                //if (retIntVal == 0)
                if (errorMessage == string.Empty)
                    returnValue = true;
                else
                    returnValue = false;
            }
            catch (Exception objExp)
            {
                returnValue = false;
                throw new Exception(objExp.Message.ToString());

            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return returnValue;
        }

        public static List<SimList> ValidateSIM(List<SimList> simList, int itemCompanyGUID, out string errorMessage, out string simMessage, out string esnErrorMessage, out string esnSimErrorMessage, out int returnValue)
        {
            esnErrorMessage = string.Empty;
            esnSimErrorMessage = string.Empty;
            errorMessage = string.Empty;
            simMessage = string.Empty;
            returnValue = 0;
            string esnXML = clsGeneral.SerializeObject(simList);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<SimList> esnList = null;
            try
            {
                objCompHash.Add("@piXMLDATA", esnXML);
                objCompHash.Add("@itemCompanyGUID", itemCompanyGUID);

                arrSpFieldSeq = new string[] { "@piXMLDATA", "@itemCompanyGUID" };
                dt = db.GetTableRecords(objCompHash, "Av_SIM_Validation", arrSpFieldSeq, "@poErrorMessage", "@poSimMessage", "@poEsnErrorMessage", "@poEsnSimErrorMessage", out errorMessage, out simMessage, out esnErrorMessage, out esnSimErrorMessage, out returnValue);

                //esnList = PopulateMslESN(dt);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return esnList;
        }

        private static List<SimInfo> PopulateSimInfo(DataTable dt)
        {
            List<SimInfo> simList = new List<SimInfo>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    SimInfo simDetail = new SimInfo();
                    simDetail.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    simDetail.UploadDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "simUploadDate", DateTime.MinValue, false));
                    simDetail.ModifiedDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "SIMMODIFIEDDATE", DateTime.MinValue, false));
                    simDetail.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "po_num", string.Empty, false) as string;
                    simDetail.SIM = clsGeneral.getColumnData(dataRow, "sim", string.Empty, false) as string;
                    simDetail.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                    simDetail.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                    simDetail.SimID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "simid", 0, false));
                    simDetail.RmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                    simList.Add(simDetail);
                }
            }
            return simList;

        }
        private static List<SimLog> PopulateSimLog(DataTable dt)
        {
            List<SimLog> simList = new List<SimLog>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    SimLog simDetail = new SimLog();
                    simDetail.Source = clsGeneral.getColumnData(dataRow, "MODULENAME", string.Empty, false) as string;
                    simDetail.Status = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                    //simDetail.Status = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "simUploadDate", DateTime.MinValue, false));
                    simDetail.SimLogDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "SimLogDate", DateTime.MinValue, false));
                    simDetail.Action = clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false) as string;
                    simDetail.SIM = clsGeneral.getColumnData(dataRow, "sim", string.Empty, false) as string;
                    simDetail.ModifiedBy = clsGeneral.getColumnData(dataRow, "Username", string.Empty, false) as string;
                    simList.Add(simDetail);
                }
            }
            return simList;

        }

    
    }
}