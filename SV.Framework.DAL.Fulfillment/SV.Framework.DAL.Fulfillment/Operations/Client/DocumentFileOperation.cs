using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;
using System.Data;

namespace SV.Framework.DAL.Fulfillment
{
    public class DocumentFileOperation : BaseCreateInstance
    {
        private DataTable LoadFiles(List<DocumentFile> request)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FileName", typeof(System.String));
            dt.Columns.Add("FileDescription", typeof(System.String));
            dt.Columns.Add("FileUploadID", typeof(System.Int64));
            DataRow row;

            if (request != null && request.Count > 0)
            {
                foreach (DocumentFile item in request)
                {
                    row = dt.NewRow();
                    row["FileName"] = item.FileName;
                    row["FileDescription"] = item.FileDescription;
                    row["FileUploadID"] = item.FileUploadID;                    
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
        public int DocumentInsert(List<DocumentFile> request, string ModuleName, Int64 DocumentSourceID, int UploadedBy)
        {
            int returnResult = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                string sCode = string.Empty;
                Hashtable objCompHash = new Hashtable();
                DataTable dt = LoadFiles(request);
                try
                {
                    objCompHash.Add("@ModuleName", ModuleName);
                    objCompHash.Add("@DocumentSourceID", DocumentSourceID);
                    objCompHash.Add("@UploadedBy", UploadedBy);
                    objCompHash.Add("@DocumentsType", dt);
                    
                    arrSpFieldSeq = new string[] { "@ModuleName", "@DocumentSourceID", "@DocumentsType", "@UploadedBy" };
                    returnResult = db.ExecuteNonQuery(objCompHash, "av_Documents_Insert", arrSpFieldSeq);
                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return returnResult;

        }
        public int DocumentDelete(Int64 FileUploadID)
        {
            int returnResult = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                
                try
                {
                    objCompHash.Add("@FileUploadID", FileUploadID);
                    
                    arrSpFieldSeq = new string[] { "@FileUploadID" };
                    returnResult = db.ExecuteNonQuery(objCompHash, "av_Documents_Delete", arrSpFieldSeq);
                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return returnResult;

        }

        public List<DocumentFile> GetDocuments(int companyID, string fulfillmentNumber, string moduleName)
        {
            List<DocumentFile> documentList = default;//null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objParams = new Hashtable();
                DataTable dataTable = default;// new DataTable();
                try
                {
                    objParams.Add("@CompanyID", companyID);
                    objParams.Add("@FulfillmentNumber", fulfillmentNumber);
                    objParams.Add("@ModuleName", moduleName);

                    arrSpFieldSeq =
                    new string[] { "@CompanyID", "@FulfillmentNumber", "@ModuleName" };

                    dataTable = db.GetTableRecords(objParams, "av_Documents_Select", arrSpFieldSeq);
                    documentList = PopulateDocuments(dataTable);
                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    db.DBClose();
                    objParams = null;
                    arrSpFieldSeq = null;
                }
            }
            return documentList;
        }

        private List<DocumentFile> PopulateDocuments(DataTable dataTable)
        {
            List<DocumentFile> docList = default;//new List<FulfillmentLogInfo>();
            DocumentFile document = default;//null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                docList = new List<DocumentFile>();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    document = new DocumentFile();
                    document.ModuleName = clsGeneral.getColumnData(dataRow, "ModuleName", string.Empty, false) as string;
                    document.FileDescription = clsGeneral.getColumnData(dataRow, "FileDescription", string.Empty, false) as string;
                    document.FileName = clsGeneral.getColumnData(dataRow, "FileName", string.Empty, false) as string;
                    document.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                    document.FulfillmentStatus = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                    document.PODate = clsGeneral.getColumnData(dataRow, "PO_Date", string.Empty, false) as string;
                    document.UploadedBy = clsGeneral.getColumnData(dataRow, "Username", string.Empty, false) as string;
                    document.DocumentSourceID = Convert.ToInt64(clsGeneral.getColumnData(dataRow, "DocumentSourceID", 0, false));
                    document.PO_ID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, false));
                    document.FileUploadID = Convert.ToInt64(clsGeneral.getColumnData(dataRow, "FileUploadID", 0, false));
                    document.LineItemCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "LineItemCount", 0, false));

                    docList.Add(document);
                }
            }
            return docList;
        }


    }
}
