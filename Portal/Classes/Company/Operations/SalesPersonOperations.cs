using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
//using Sv.Framework.DataMembers.StateObject;

namespace avii.Classes
{
    public class SalesPersonOperations
    {
        public static List<SalesPerson> getSalesPersonList(int CompanyID)
        {
            List<SalesPerson> SalesPersonList = new List<SalesPerson>();
            
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", CompanyID);
                   //objCompHash.Add("@search",0);
                arrSpFieldSeq = new string[] { "@CompanyID"};

                dataTable = db.GetTableRecords(objCompHash, "av_salesPerson_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        SalesPerson objSalesPerson = new SalesPerson();
                        objSalesPerson.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UserID", 0, false));

                        objSalesPerson.EmployeeNumber = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                        SalesPersonList.Add(objSalesPerson);
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

            return SalesPersonList;
        }
    }
}
