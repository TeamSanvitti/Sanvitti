using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace avii.Classes
{
    public class WarehouseCodeOperations
    {
        public static int CreateUpdateCompanyWarehouseCode(CustomerWarehouseCode warehouseCodeObj)
        {
            int returnValue = 0;

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@WarehouseCodeGUID", warehouseCodeObj.WarehouseCodeGUID);
                objCompHash.Add("@CompanyID", warehouseCodeObj.CompanyID);
                objCompHash.Add("@WarehouseCode", warehouseCodeObj.WarehouseCode);
                objCompHash.Add("@Active", warehouseCodeObj.Active);

                arrSpFieldSeq = new string[] { "@WarehouseCodeGUID", "@CompanyID", "@WarehouseCode", "@Active" };
                db.ExeCommand(objCompHash, "av_CompanyWarehouse_InsertUpdate", arrSpFieldSeq, "@CodeCount", out returnValue);

            }
            catch (Exception exp)
            {
                throw exp;
            }
            return returnValue;
        }
        public static void DeleteCompanyWarehouseCode(int warehouseCodeGUID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@WarehouseCodeGUID", warehouseCodeGUID);
                
                arrSpFieldSeq = new string[] { "@WarehouseCodeGUID" };
                db.ExeCommand(objCompHash, "av_CompanyWarehouse_Delete", arrSpFieldSeq);

            }
            catch (Exception exp)
            {
                throw exp;
            }
            
        }
        public static CustomerWarehouseCode GetWarehouseCodeInfo(int warehouseCodeGUID)
        {
            CustomerWarehouseCode warehouseCode = new CustomerWarehouseCode();

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@WarehouseCodeGUID", warehouseCodeGUID);
                objCompHash.Add("@CompanyID", 0);
                objCompHash.Add("@WarehouseCode", null);
                objCompHash.Add("@Active", false);

                arrSpFieldSeq = new string[] { "@WarehouseCodeGUID", "@CompanyID", "@WarehouseCode", "@Active" };

                dataTable = db.GetTableRecords(objCompHash, "av_CompanyWarehouseCode_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        
                        warehouseCode.WarehouseCodeGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WarehouseCodeGUID", 0, false));
                        warehouseCode.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        warehouseCode.WarehouseCode = clsGeneral.getColumnData(dataRow, "WarehouseCode", string.Empty, false) as string;
                        warehouseCode.CompanyName = clsGeneral.getColumnData(dataRow, "companyname", string.Empty, false) as string;
                        warehouseCode.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "Active", false, false));
                        
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

            return warehouseCode;
        }
        public static List<CustomerWarehouseCode> GetCompanyWarehouseCode(int CompanyID, string warehouseCode, bool active)
        {
            List<CustomerWarehouseCode> warehouseCodeList = new List<CustomerWarehouseCode>();

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@WarehouseCodeGUID", 0);
                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@WarehouseCode", warehouseCode);
                objCompHash.Add("@Active", active);

                arrSpFieldSeq = new string[] { "@WarehouseCodeGUID", "@CompanyID", "@WarehouseCode", "@Active" };

                dataTable = db.GetTableRecords(objCompHash, "av_CompanyWarehouseCode_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    warehouseCodeList = PopulateWarehouseCode(dataTable);
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

            return warehouseCodeList;
        }
        public static List<CustomerWarehouseCode> PopulateWarehouseCode(DataTable dataTable)
        {
            List<CustomerWarehouseCode> warehouseCodeList = new List<CustomerWarehouseCode>();
            
            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        CustomerWarehouseCode warehouseCode = new CustomerWarehouseCode();
                        warehouseCode.WarehouseCodeGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WarehouseCodeGUID", 0, false));
                        warehouseCode.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        warehouseCode.WarehouseCode = clsGeneral.getColumnData(dataRow, "WarehouseCode", string.Empty, false) as string;
                        warehouseCode.CompanyName = clsGeneral.getColumnData(dataRow, "companyname", string.Empty, false) as string;
                        warehouseCode.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "Active", false, false));
                        warehouseCode.WHCodecompanyName = clsGeneral.getColumnData(dataRow, "WHCodecompanyName", string.Empty, false) as string;
                        
                        warehouseCodeList.Add(warehouseCode);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return warehouseCodeList;
        }

        public static DataTable GetWarehouseCodeItems(string warehouseCode)
        {
            
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@WarehouseCode", warehouseCode);
                
                arrSpFieldSeq = new string[] { "@WarehouseCode" };

                dataTable = db.GetTableRecords(objCompHash, "av_CompanyWarehouseCodeItems_Select", arrSpFieldSeq);

                

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

            return dataTable;
        }
    }
}