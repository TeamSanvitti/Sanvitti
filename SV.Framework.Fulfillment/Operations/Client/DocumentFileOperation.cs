using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.Fulfillment
{
    public class DocumentFileOperation : BaseCreateInstance
    {
        public int DocumentInsert(List<DocumentFile> request, string ModuleName, Int64 DocumentSourceID, int UploadedBy)
        {
            SV.Framework.DAL.Fulfillment.DocumentFileOperation docOperation = SV.Framework.DAL.Fulfillment.DocumentFileOperation.CreateInstance<SV.Framework.DAL.Fulfillment.DocumentFileOperation>();
            return docOperation.DocumentInsert(request, ModuleName, DocumentSourceID, UploadedBy);
        }
        public List<DocumentFile> GetDocuments(int companyID, string fulfillmentNumber, string moduleName)
        {
            SV.Framework.DAL.Fulfillment.DocumentFileOperation docOperation = SV.Framework.DAL.Fulfillment.DocumentFileOperation.CreateInstance<SV.Framework.DAL.Fulfillment.DocumentFileOperation>();
            List<DocumentFile> documentList = docOperation.GetDocuments(companyID, fulfillmentNumber, moduleName);
            return documentList;
        }
        public int DocumentDelete(Int64 FileUploadID)
        {
            SV.Framework.DAL.Fulfillment.DocumentFileOperation docOperation = SV.Framework.DAL.Fulfillment.DocumentFileOperation.CreateInstance<SV.Framework.DAL.Fulfillment.DocumentFileOperation>();
            return docOperation.DocumentDelete(FileUploadID);
        }

    }
}
