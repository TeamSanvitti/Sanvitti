using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace  avii.Classes
{


    public class ErrorLog
    {
        private int logGuid;
        private string purchaseOrderNumber;
        private DateTime logDate;
        private string moduleName;
        private string description;

        public ErrorLog(int ErroLogGUID)
        {
            logGuid = ErroLogGUID;
        }

        public ErrorLog()
        {
        }

        public int ErrorLogGuid
        {
            get
            {
                return logGuid;
            }
        }

        public string PurhcaseOrdernumber
        {
            get
            {
                return purchaseOrderNumber;
            }
            set
            {
                purchaseOrderNumber = value;
            }
        }

        public string ModuleName
        {
            get
            {
                return moduleName;
            }
            set
            {
                moduleName = value;
            }
        }

        public DateTime LogDate
        {
            get
            {
                return logDate;
            }
            set
            {
                logDate = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

    }


    public class ErrorLogSearch
    {
        private int logGuid;
        private string purchaseOrderNumber;
        private DateTime logFromDate;
        private DateTime logToDate;
        private string moduleName;
        private string returnCode;
        private string description;


        public int ErrorLogGuid
        {
            get
            {
                return logGuid;
            }
        }

        public string PurhcaseOrdernumber
        {
            get
            {
                return purchaseOrderNumber;
            }
            set
            {
                purchaseOrderNumber = value;
            }
        }

        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string ModuleName
        {
            get
            {
                return moduleName;
            }
            set
            {
                moduleName = value;
            }
        }

        public DateTime LogFromDate
        {
            get
            {
                return logFromDate;
            }
            set
            {
                logFromDate = value;
            }
        }

        public DateTime LogToDate
        {
            get
            {
                return logToDate;
            }
            set
            {
                logToDate = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

    }


    public static class clsErrorLog
    {
        public static void ClearErrorLog()
        {
            DBConnect db = new DBConnect();
            try
            {
                db.ExeCommand("Aero_ErrorLog_Delete");
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                db.DBClose();
            }

        }

        public static void InsertErrorLog(ErrorLog errorLog)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@Po_Num", "@Description","@ModuleName" };
            try
            {
                objCompHash.Add("@Po_Num", errorLog.PurhcaseOrdernumber);
                objCompHash.Add("@Description", errorLog.Description);
                objCompHash.Add("@ModuleName", errorLog.ModuleName);

                db.ExeCommand(objCompHash, "Aero_ErrorLog_Insert", arrSpFieldSeq);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                db.DBClose();
            }
            
        }

        public static List<ErrorLog> GetErrorLog(ErrorLogSearch errorLog)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dataTable = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<ErrorLog> erroLogList = null;
            try
            {
                objCompHash.Add("@Po_Num", errorLog.PurhcaseOrdernumber);
                objCompHash.Add("@LogDate_From", (errorLog.LogFromDate.Year == 1 ? null : errorLog.LogFromDate.ToShortDateString()));
                objCompHash.Add("@LogDate_To", (errorLog.LogToDate.Year == 1 ? null : errorLog.LogToDate.ToShortDateString()));
                objCompHash.Add("@ModuleName", errorLog.ModuleName);
                objCompHash.Add("@ReturnCode", errorLog.ReturnCode);
                arrSpFieldSeq = new string[] { "@Po_Num", "@LogDate_From", "@LogDate_To", "@ModuleName", "@ReturnCode"  };

                erroLogList = PopulateErrorLog(db.GetTableRecords(objCompHash, "Aero_ErrorLog_Select", arrSpFieldSeq));
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return erroLogList;
        }


        private static List<ErrorLog> PopulateErrorLog(DataTable dataTable)
        {
            List<ErrorLog> errorLogList = null;
            int errorLogGUID;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                errorLogList = new List<ErrorLog>();
                ErrorLog errorLog = null;
                
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    errorLogGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "LogGUID", 0, true));
                    errorLog = new ErrorLog(errorLogGUID);
                    errorLog.PurhcaseOrdernumber = clsGeneral.getColumnData(dataRow, "PO_NUM", string.Empty, true) as string;
                    errorLog.ModuleName = clsGeneral.getColumnData(dataRow, "ModuleName", string.Empty, true) as string;
                    errorLog.LogDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "LogDate", string.Empty, true));
                    errorLog.Description = clsGeneral.getColumnData(dataRow, "Description", string.Empty, true) as string;
                    errorLogList.Add(errorLog);
                }
            }

            return errorLogList;
        }

    }
}
