using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Data;

namespace avii.Classes
{
    public class RMATrackingOperation
    {
        public static ShippingLabelResponse ShippingLabelUpdate(RMATrackning request, int userId)
        {

            ShippingLabelResponse serviceResponse = new ShippingLabelResponse();
            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            try
            {
                if (request != null)
                {
                    //if (userId > 0)
                    {
                        serviceResponse = ShippingLabelUpdateDB(request, userId);
                    }
                }
            }
            catch (Exception ex)
            {

                serviceResponse.Comment = ex.Message;
                serviceResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
            }
            return serviceResponse;

        }

        public static string GetLabelBase64(int TrackingId, out string shipMethod, out string ShipPackage, out bool IsManualTracking)
        {
            string labelBase64 = string.Empty;
            shipMethod = string.Empty;
            ShipPackage = string.Empty;
            IsManualTracking = false;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@TrackingId", TrackingId);


                arrSpFieldSeq = new string[] { "@TrackingId" };
                dt = db.GetTableRecords(objCompHash, "av_rma_ShippingLabel_Base64", arrSpFieldSeq);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        labelBase64 = Convert.ToString(row["ShippingLabel"]);
                        shipMethod = Convert.ToString(row["ShipByCode"]);
                        ShipPackage = Convert.ToString(row["ShipPackage"]);
                        IsManualTracking = Convert.ToBoolean(row["IsManualTracking"]);

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
            }

            return labelBase64;
        }

        private static ShippingLabelResponse ShippingLabelUpdateDB(RMATrackning shippingLabelInfo, int UserID )
        {

            string errorMessage = string.Empty;
           
            ShippingLabelResponse response = new ShippingLabelResponse();
            response.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            int returnValue = 0;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@RMANumber", shippingLabelInfo.RMANumber);
                objCompHash.Add("@UserId", UserID);
                objCompHash.Add("@TrackingNumber", shippingLabelInfo.TrackingNumber);
                objCompHash.Add("@ShipmentType", shippingLabelInfo.ShipmentType);
                objCompHash.Add("@ShipVia", shippingLabelInfo.ShipVia);
                objCompHash.Add("@ShippingLabel", shippingLabelInfo.ShippingLabelImage);
                objCompHash.Add("@ShipDate", shippingLabelInfo.ShipDate);
                objCompHash.Add("@Comments", shippingLabelInfo.Comments);
                objCompHash.Add("@Weight", shippingLabelInfo.Weight);
                objCompHash.Add("@Package", shippingLabelInfo.Package);
                objCompHash.Add("@FinalPostage", shippingLabelInfo.FinalPostage);
                objCompHash.Add("@IsManualTracking", shippingLabelInfo.IsManualTracking);
                objCompHash.Add("@Prepaid", shippingLabelInfo.Prepaid);


                arrSpFieldSeq = new string[] { "@RMANumber", "@UserId", "@TrackingNumber", "@ShipmentType", "@ShipVia",
                    "@ShippingLabel", "@ShipDate","@Comments","@Weight","@Package", "@FinalPostage","@IsManualTracking","@Prepaid"
                };
                returnValue = db.ExecCommand(objCompHash, "av_RMA_ShippingLabel_Update", arrSpFieldSeq);
                if (returnValue > 0)
                {
                    errorMessage = ResponseErrorCode.UpdatedSuccessfully.ToString();
                    response.ErrorCode = ResponseErrorCode.UpdatedSuccessfully.ToString();
                }
                else if (returnValue == -1)
                {
                    errorMessage = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                    response.ErrorCode = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                }
                else
                {
                    errorMessage = ResponseErrorCode.DataNotUpdated.ToString();
                    response.ErrorCode = ResponseErrorCode.DataNotUpdated.ToString();
                }
            }
            catch (Exception objExp)
            {
                response.ErrorCode = ResponseErrorCode.InternalError.ToString();
                errorMessage = objExp.Message.ToString();
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            response.Comment = errorMessage;
            return response;
        }

    }
}