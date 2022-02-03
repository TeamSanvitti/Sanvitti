using System;
using System.Data;
using System.Collections;

namespace avii.Classes
{
    public class RMAOperations
    {

        public RMAInfoResponse GetRMAByTrackingNumber(RMAInfoRequest serviceRequest)
        {
            RMAInfoResponse serviceResponse = new RMAInfoResponse();
            //int returnTrackingNumberCount=0;
            //try
            //{
            //    SV.Framework.DataMembers.State.CredentialValidation credentialValidation = SV.Framework.DataOperations.AuthenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, SV.Framework.DataMembers.Enums.FulfillmentEnums.Source.API);
            //    if (credentialValidation != null && credentialValidation.UserID > 0)
            //    {
            //        RMAInfo rmaInfo = GetRMAByTrackingNumberDB(serviceRequest.TrackingNumber, out returnTrackingNumberCount);
            //        if (rmaInfo != null && returnTrackingNumberCount > -1)
            //        {
            //            serviceResponse.rmaInfo = rmaInfo;
            //            serviceResponse.Comment = string.Empty;
            //            serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
            //        }
            //        else
            //            if (returnTrackingNumberCount == -1)
            //            {
            //                serviceResponse.Comment = "Tracking number does not exists";
            //                serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
            //            }
            //    }
            //    else
            //    {
            //        serviceResponse.Comment = "Cannot Authenticate User";
            //        serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();

            //    }
            //}
            //catch (Exception ex)
            //{
            //    serviceResponse.Comment = ex.Message;
            //    serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

            //}
            return serviceResponse;
        }
        private static RMAInfo GetRMAByTrackingNumberDB(string trackingNumber, out int returnTrackingNumberCount)
        {
            RMAInfo rmaInfo = new RMAInfo();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {


                objCompHash.Add("@TrackingNumber", trackingNumber);
                
                arrSpFieldSeq = new string[] { "@TrackingNumber" };
                

                dataTable = db.GetTableRecords(objCompHash, "av_RMA_TrackingNumer_Select", arrSpFieldSeq, "@ReturnTrackingNumberCount", out returnTrackingNumberCount);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        //rmaInfo = new RMA();
                        rmaInfo.ESNCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ESNCount", 0, false));
                        rmaInfo.RMADate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RMADate", 0, false));
                        rmaInfo.MaxShipmentDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "MaxShipmentDate", 0, false));

                        rmaInfo.RMANumber = clsGeneral.getColumnData(dataRow, "RMANumber", string.Empty, false) as string;
                        rmaInfo.ContactName = clsGeneral.getColumnData(dataRow, "RmaContactName", string.Empty, false) as string;
                        rmaInfo.Address = clsGeneral.getColumnData(dataRow, "ContactAddress", string.Empty, false) as string;
                        rmaInfo.City = clsGeneral.getColumnData(dataRow, "ContactCity", string.Empty, false) as string;
                        rmaInfo.State = clsGeneral.getColumnData(dataRow, "ContactState", string.Empty, false) as string;
                        rmaInfo.Zip = clsGeneral.getColumnData(dataRow, "ContactZip", string.Empty, false) as string;
                        rmaInfo.RMAStatus = clsGeneral.getColumnData(dataRow, "RMAStatus", string.Empty, false) as string;
                        rmaInfo.Email = clsGeneral.getColumnData(dataRow, "ContactEmail", string.Empty, false) as string;
                        rmaInfo.Phone = clsGeneral.getColumnData(dataRow, "ContactPhone", string.Empty, false) as string;
                        rmaInfo.StoreID = clsGeneral.getColumnData(dataRow, "StoreID", string.Empty, false) as string;
                        rmaInfo.Country = clsGeneral.getColumnData(dataRow, "ContactCountry", string.Empty, false) as string;

                        

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return rmaInfo;
        }

    }
}