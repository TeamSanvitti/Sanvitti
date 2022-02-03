using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Admin;

namespace SV.Framework.Admin
{
    public class CustomerOperations
    {
        public static APIAddressInfo GetCustomerAPIAddress(int companyID, string apiMethodName)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            APIAddressInfo aPIAddressInfo = null;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@ComapanyID", companyID);
                objCompHash.Add("@APIMethod", apiMethodName);

                arrSpFieldSeq = new string[] { "@ComapanyID", "@APIMethod" };
                dt = db.GetTableRecords(objCompHash, "av_CustomerAPIAddress_Select", arrSpFieldSeq);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRowItem in dt.Rows)
                    {

                        aPIAddressInfo = new APIAddressInfo();
                        aPIAddressInfo.APIAddress = (string)clsGeneral.getColumnData(dataRowItem, "APIAddress", string.Empty, false);
                        aPIAddressInfo.ConnectionString = (string)clsGeneral.getColumnData(dataRowItem, "UserName", string.Empty, false);
                    }
                }
                return aPIAddressInfo;
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
        }

        public static List<CustomerModel> GetCustomers(int companyID, int withUser, int ShipTrackingCustomer)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<CustomerModel> customers = new List<CustomerModel>();
            CustomerModel customer = null;
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@WithUser", withUser);
                objCompHash.Add("@ShipTrackingCustomer", ShipTrackingCustomer);
                arrSpFieldSeq = new string[] { "@CompanyID", "@WithUser", "@ShipTrackingCustomer" };
                dt = db.GetTableRecords(objCompHash, "Aero_Company_Select", arrSpFieldSeq);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        customer = new CustomerModel();
                        customer.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        customer.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        customer.CompanyAccountNumber = clsGeneral.getColumnData(dataRow, "CompanyAccountNumber", string.Empty, false) as string;
                        customer.CustInfo = clsGeneral.getColumnData(dataRow, "CustInfo", string.Empty, false) as string;

                        customers.Add(customer);
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

            return customers;

        }
    }
}
