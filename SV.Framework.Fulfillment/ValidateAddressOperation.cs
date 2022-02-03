using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SV.Framework.Fulfillment
{
    public class ValidateAddressOperation
    {
        public static void ValidateAddressInsert(ValidateAddressModel request)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            DataTable dt = LoadAddressTable(request.CandidateAddresses);
            try
            {
                objCompHash.Add("@PO_ID", request.PO_ID);
                objCompHash.Add("@CompanyName", request.CompanyName);
                objCompHash.Add("@Address1", request.Address1);
                objCompHash.Add("@Address2", request.Address2);
                objCompHash.Add("@City", request.City);
                objCompHash.Add("@State", request.State);
                objCompHash.Add("@Zip", request.PostalCode);
                objCompHash.Add("@StatusCode", request.Status);
                objCompHash.Add("@AddressMatch", request.AddressMatch);
                objCompHash.Add("@CityStateZipOK", request.CityStateZipOK);
                objCompHash.Add("@AddressCleansingResult", request.AddressCleansingResult);
                objCompHash.Add("@VerificationLevel", request.VerificationLevel);
                objCompHash.Add("@POAddressValidationType", dt);


                arrSpFieldSeq = new string[] { "@PO_ID", "@CompanyName", "@Address1", "@Address2", "@City", "@State", "@Zip", "@StatusCode", 
                    "@AddressMatch", "@CityStateZipOK","@AddressCleansingResult","@VerificationLevel", "@POAddressValidationType"};
                db.ExeCommand(objCompHash, "av_purchaseOrderAddressValidationInsert", arrSpFieldSeq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        public static List<FulfillmentModel> GetFulfillment(int companyID, int statusID, string poNum)
        {
            //List<FulfillmentTracking> shipments = null;
            List<FulfillmentModel> poList = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@StatusID", statusID);
                objCompHash.Add("@PO_Num", poNum);

                //arrSpFieldSeq = new string[] { "@piStatusID" };
                arrSpFieldSeq = new string[] { "@CompanyID", "@StatusID", "@PO_Num" };
                dt = db.GetTableRecords(objCompHash, "av_purchaseOrderAddressValidate_Search", arrSpFieldSeq);

                poList = PopulatePOList(dt);
                //HttpContext.Current.Session["shipmentDB"] = shipmentDB;
                // shipments = PopulateShipmentdb(shipmentDB);
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }


            return poList;
        }
        public static List<CandidateAddress> GetSuggestions(int POID)
        {
            //List<FulfillmentTracking> shipments = null;
            List<CandidateAddress> CandidateAddresses = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POID", POID);
                //arrSpFieldSeq = new string[] { "@piStatusID" };
                arrSpFieldSeq = new string[] { "@POID" };
                dt = db.GetTableRecords(objCompHash, "av_purchaseOrderAddressValidationLog_Select", arrSpFieldSeq);

                CandidateAddresses = PopulateCandidateAddresses(dt);
                //HttpContext.Current.Session["shipmentDB"] = shipmentDB;
                // shipments = PopulateShipmentdb(shipmentDB);
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
            return CandidateAddresses;
        }
        private static List<CandidateAddress> PopulateCandidateAddresses(DataTable dt)
        {
            List<CandidateAddress> CandidateAddresses = null;// new List<PurchaseOrderShipmentDB>();
            CandidateAddress po = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                CandidateAddresses = new List<CandidateAddress>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    po = new CandidateAddress();
                    po.Address1 = clsGeneral.getColumnData(dataRow, "Address1", string.Empty, false) as string;
                    po.City = clsGeneral.getColumnData(dataRow, "City", string.Empty, false) as string;
                    po.State = clsGeneral.getColumnData(dataRow, "State", string.Empty, false) as string;
                    po.PostalCode = clsGeneral.getColumnData(dataRow, "PostalCode", string.Empty, false) as string;
                    po.Zip4 = clsGeneral.getColumnData(dataRow, "Zip4", string.Empty, false) as string;
                    po.CountryCode = clsGeneral.getColumnData(dataRow, "CountryCode ", string.Empty, false) as string;
                    CandidateAddresses.Add(po);
                }
            }

            return CandidateAddresses;
        }

        private static List<FulfillmentModel> PopulatePOList(DataTable dt)
        {
            List<FulfillmentModel> poList = null;// new List<PurchaseOrderShipmentDB>();
            FulfillmentModel po = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                poList = new List<FulfillmentModel>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    po = new FulfillmentModel();
                    po.Address1 = clsGeneral.getColumnData(dataRow, "ShipTo_Address", string.Empty, false) as string;
                    po.Address2 = clsGeneral.getColumnData(dataRow, "ShipTo_Address2", string.Empty, false) as string;
                    po.ContactName = clsGeneral.getColumnData(dataRow, "ShipTo_Attn", string.Empty, false) as string;
                    po.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                    po.ConnectionString = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                    po.City = clsGeneral.getColumnData(dataRow, "ShipTo_City", string.Empty, false) as string;
                    po.State = clsGeneral.getColumnData(dataRow, "ShipTo_State", string.Empty, false) as string;
                    po.Zip = clsGeneral.getColumnData(dataRow, "ShipTo_Zip", string.Empty, false) as string;
                    po.ValidationStatus = clsGeneral.getColumnData(dataRow, "ValidationStatus", string.Empty, false) as string;
                    po.FulfillmentDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", string.Empty, false)).ToShortDateString();
                    po.POID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Po_ID", 0, false));
                    po.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_NUM", string.Empty, false) as string;
                    poList.Add(po);
                }
            }

            return poList;
        }

        private static DataTable LoadAddressTable(List<CandidateAddress> CandidateAddresses)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Address1", typeof(System.String));
            dt.Columns.Add("City", typeof(System.String));
            dt.Columns.Add("State", typeof(System.String));
            dt.Columns.Add("PostalCode", typeof(System.String));
            dt.Columns.Add("Zip4", typeof(System.String));
            dt.Columns.Add("CountryCode", typeof(System.String));


            DataRow row;

            if (CandidateAddresses != null && CandidateAddresses.Count > 0)
            {
                foreach (CandidateAddress item in CandidateAddresses)
                {
                    row = dt.NewRow();
                    row["Address1"] = item.Address1;
                    row["City"] = item.City;
                    row["State"] = item.State;
                    row["PostalCode"] = item.PostalCode;
                    row["Zip4"] = item.Zip4;
                    row["CountryCode"] = item.CountryCode;

                    dt.Rows.Add(row);
                }
            }

            return dt;
        }


    }
}
