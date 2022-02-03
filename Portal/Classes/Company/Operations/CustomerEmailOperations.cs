using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace avii.Classes
{
    public class CustomerEmailOperations
    {
        public static void CustomerEmail_InsertUpdate(List<CustomerEmail> customerEmailList, int companyID)
        {
            string customerEmailXml = clsGeneral.SerializeObject(customerEmailList);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@CustomerEmailXml", customerEmailXml);
                arrSpFieldSeq = new string[] { "@CompanyID", "@CustomerEmailXml" };
                db.ExeCommand(objCompHash, "av_CustomerEmail_InsertUpdate", arrSpFieldSeq);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public static List<CustomerEmail> GetCustomerEmailList(int CompanyID)
        {
            List<CustomerEmail> customerEmailList = new List<CustomerEmail>();

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", CompanyID);
                arrSpFieldSeq = new string[] { "@CompanyID"};

                dataTable = db.GetTableRecords(objCompHash, "av_CustomerEmail_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        CustomerEmail objCustomerEmail = new CustomerEmail();

                        objCustomerEmail.IsEmail = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsEmail", false, false));
                        
                        objCustomerEmail.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UserID", 0, false));
                        objCustomerEmail.ModuleGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "moduleGUID", 0, false));
                        objCustomerEmail.IsNotification = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsNotification", false, false));
                        objCustomerEmail.CompanyEmail = clsGeneral.getColumnData(dataRow, "CompanyEmail", string.Empty, false) as string;
                        objCustomerEmail.Email = clsGeneral.getColumnData(dataRow, "Email", string.Empty, false) as string;
                        objCustomerEmail.OverrideEmail = clsGeneral.getColumnData(dataRow, "OverrideEmail", string.Empty, false) as string;
                        objCustomerEmail.ModuleName = clsGeneral.getColumnData(dataRow, "title", string.Empty, false) as string;
                        customerEmailList.Add(objCustomerEmail);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return customerEmailList;
        }

    }
}