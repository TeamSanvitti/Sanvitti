using System;
using System.Collections;
using System.Data;

namespace avii.Classes
{
    public class ReportOperations
    {
        public static DataTable GetCompanyList(int companyID)
        {
            DataTable dataTable = new DataTable();
            if (!(dataTable != null && dataTable.Rows.Count > 0))
            {
                dataTable = clsCompany.GetCompany(companyID);
            }

            return dataTable;
        } 
        public static DataTable GetFulfillmentLogReport(string poNumber, string sku, string fromDate, string toDate, int companyID)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {
                
                objParams.Add("@PoNum", poNumber);
                objParams.Add("@SKU", sku);
                objParams.Add("@FromDate", fromDate);
                objParams.Add("@ToDate", toDate);
                objParams.Add("@CompanyID", companyID);


                arrSpFieldSeq =
                new string[] { "@PoNum", "@SKU", "@FromDate", "@ToDate", "@CompanyID" };

                logTable = db.GetTableRecords(objParams, "av_FulfillmentLog_SELECT", arrSpFieldSeq);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return logTable;
        }
        public static void DeleteFulfillmentReport(int fulfillmentLogID)
        {
            DBConnect objDB = new DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;

            try
            {
                objCompHash.Add("@fulfillmentLogID", fulfillmentLogID);



                arrSpFieldSeq = new string[] { "@fulfillmentLogID" };
                objDB.ExeCommand(objCompHash, "av_FulfillmentLog_DELETE", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        public static DataTable GetUploadLogReport(string fromDate, string toDate, string moduleName, int statusID)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@UploadDateFrom", fromDate);
                objParams.Add("@UploadDateTo", toDate);
                objParams.Add("@ModuleName", moduleName);
                objParams.Add("@StatusID", statusID);
                

                arrSpFieldSeq =
                new string[] { "@UploadDateFrom", "@UploadDateTo", "@ModuleName", "@StatusID" };

                logTable = db.GetTableRecords(objParams, "av_UploadTasks_Select", arrSpFieldSeq);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return logTable;
        }
        public static void DeleteUploadReport(int uploadID)
        {
            DBConnect objDB = new DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;

            try
            {
                objCompHash.Add("@UploadID", uploadID);



                arrSpFieldSeq = new string[] { "@UploadID" };
                objDB.ExeCommand(objCompHash, "av_UploadTasks_Delete", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public static DataTable GetUserLoginLogReport(string userName, int companyID, string fromDate, string toDate)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@UserName", userName);
                objParams.Add("@CompanyID", companyID);
                objParams.Add("@FromDate", fromDate);
                objParams.Add("@ToDate", toDate);
                

                arrSpFieldSeq =
                new string[] { "@UserName", "@CompanyID", "@FromDate", "@ToDate" };

                logTable = db.GetTableRecords(objParams, "av_UserSignIn_SELECT", arrSpFieldSeq);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return logTable;
        }
        public static void DeleteLoginReport(int signInID)
        {
            DBConnect objDB = new DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;

            try
            {
                objCompHash.Add("@SignInID", signInID);



                arrSpFieldSeq = new string[] { "@SignInID" };
                objDB.ExeCommand(objCompHash, "av_loginLog_DELETE", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        
        //
        public static DataTable GetAuditESNLogReport(string esn, string module, string fromDate, string toDate)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@ESN", esn);
                objParams.Add("@Module", module);
                objParams.Add("@FromDate", fromDate);
                objParams.Add("@ToDate", toDate);
                


                arrSpFieldSeq =
                new string[] { "@ESN", "@Module", "@FromDate", "@ToDate" };

                logTable = db.GetTableRecords(objParams, "av_AuditESNLog_SELECT", arrSpFieldSeq);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return logTable;
        }

        public static void DeleteAuditESNReport(int esnLogGUID)
        {
            DBConnect objDB = new DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;

            try
            {
                objCompHash.Add("@EsnLogGUID", esnLogGUID);



                arrSpFieldSeq = new string[] { "@EsnLogGUID" };
                objDB.ExeCommand(objCompHash, "av_EsnMslLog_DELETE", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        public static DataTable GetMASLogReport(string fromDate, string toDate)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@FromDate", fromDate);
                objParams.Add("@ToDate", toDate);
                


                arrSpFieldSeq =
                new string[] { "@FromDate", "@ToDate"};

                logTable = db.GetTableRecords(objParams, "ToMAS_SO_SalesOrderHeader_Select", arrSpFieldSeq);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return logTable;
        }
        public static DataTable ChildDataSource(string salesNumber, string OrderDate)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@SalesOrderNo", salesNumber);
                objParams.Add("@OrderDate", OrderDate);



                arrSpFieldSeq =
                new string[] { "@SalesOrderNo", "@OrderDate" };

                logTable = db.GetTableRecords(objParams, "ToMas_SO_SalesOrderDetail_Select", arrSpFieldSeq);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return logTable;
        }

        public static DataTable GetPOFromMASReport(string fromDate, string toDate, string poNum, string salesOrderNum)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@FromDate", fromDate);
                objParams.Add("@ToDate", toDate);
                objParams.Add("@PONum", poNum);
                objParams.Add("@SalesOrderNum", salesOrderNum);



                arrSpFieldSeq =
                new string[] { "@FromDate", "@ToDate", "@PONum", "@SalesOrderNum" };

                logTable = db.GetTableRecords(objParams, "av_FromMAS_SO_SalesOrderHeader_Select", arrSpFieldSeq);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return logTable;
        }
        public static DataTable ChildDataSourceFromMas(string salesNumber, string OrderDate)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@SalesOrderNo", salesNumber);
                //objParams.Add("@OrderDate", OrderDate);



                arrSpFieldSeq =
                new string[] { "@SalesOrderNo" };

                logTable = db.GetTableRecords(objParams, "av_FromMas_SO_SalesOrderDetail_Select", arrSpFieldSeq);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return logTable;
        }

        public static void DeleteMasReport(string salesOrderNo)
        {
            DBConnect objDB = new DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;

            try
            {
                objCompHash.Add("@SalesOrderNo", salesOrderNo);



                arrSpFieldSeq = new string[] { "@SalesOrderNo" };
                objDB.ExeCommand3(objCompHash, "ToMas_SO_SalesOrder_Delete", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        public static DataTable CustomerInventoryReport(int companyID, string itemCode)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@CompanyID", companyID);
                objParams.Add("@ItemCode", itemCode);



                arrSpFieldSeq =
                new string[] { "@CompanyID", "@ItemCode" };

                dataTable = db.GetTableRecords(objParams, "AV_IM1_InventoryMasterfile_Select", arrSpFieldSeq);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return dataTable;
        }

        public static DataTable GetEmailLogReport(string fromDate, string toDate, string moduleName)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable ds = new DataTable();

            try
            {

                objParams.Add("@FromDate", fromDate);
                objParams.Add("@ToDate", toDate);
                objParams.Add("@moduleName", moduleName);


                arrSpFieldSeq =
                new string[] { "@FromDate", "@ToDate", "@moduleName" };

                ds = db.GetTableRecords(objParams, "av_EmailLog_SELECT", arrSpFieldSeq);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return ds;
        }
        public static void DeleteEmailReport(int emailLogID)
        {
            DBConnect objDB = new DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;

            try
            {
                objCompHash.Add("@EmailLogID", emailLogID);


                arrSpFieldSeq = new string[] { "@EmailLogID" };
                objDB.ExeCommand(objCompHash, "sv_EmailLog_DELETE", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        public static DataTable GetItemCode_ESN_Summary()
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@ESN", string.Empty);
                



                arrSpFieldSeq =
                new string[] { "@ESN"};

                logTable = db.GetTableRecords(objParams, "av_ItemCode_ESN_Summary", arrSpFieldSeq);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return logTable;
        }
        public static DataTable GetcustomErrorReport(string fromDate, string toDate, string searchText)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@FromDate", fromDate);
                objParams.Add("@ToDate", toDate);
                objParams.Add("@SearchText", searchText);



                arrSpFieldSeq =
                new string[] { "@FromDate", "@ToDate", "@SearchText" };

                logTable = db.GetTableRecords(objParams, "sv_CustomerError_Select", arrSpFieldSeq);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return logTable;
        }

    }
}
