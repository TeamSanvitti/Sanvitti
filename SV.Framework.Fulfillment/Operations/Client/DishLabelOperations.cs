using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.Fulfillment
{
    public class DishLabelOperations : BaseCreateInstance           
    {       

        public  List<PalletModel> GetPalletLabelInfo(int poid, string poNumber, string palletID)
        {
            SV.Framework.DAL.Fulfillment.DishLabelOperations dishLabelOperations = SV.Framework.DAL.Fulfillment.DishLabelOperations.CreateInstance<DAL.Fulfillment.DishLabelOperations>();
            List<PalletModel> palletList = dishLabelOperations.GetPalletLabelInfo(poid, poNumber, palletID);
            
            return palletList;
        }

        public  List<MasterCartonInfo> GetMasterCartonLabelInfo(int poid)
        {
            SV.Framework.DAL.Fulfillment.DishLabelOperations dishLabelOperations = SV.Framework.DAL.Fulfillment.DishLabelOperations.CreateInstance<DAL.Fulfillment.DishLabelOperations>();
            List<MasterCartonInfo> cartonList = dishLabelOperations.GetMasterCartonLabelInfo(poid);

            return cartonList;
        }
        public  List<MasterCartonInfo> GetMasterCartonLabelByContainerID(string ContainerID, int poid)
        {
            SV.Framework.DAL.Fulfillment.DishLabelOperations dishLabelOperations = SV.Framework.DAL.Fulfillment.DishLabelOperations.CreateInstance<DAL.Fulfillment.DishLabelOperations>();
            List<MasterCartonInfo> cartonList = dishLabelOperations.GetMasterCartonLabelByContainerID(ContainerID, poid);
                        
            return cartonList;
        }


        public List<PosKitInfo> GetPosLabels(int companyID, int ESNAuthorizationID, DataTable dt)
        {
            SV.Framework.DAL.Fulfillment.DishLabelOperations dishLabelOperations = SV.Framework.DAL.Fulfillment.DishLabelOperations.CreateInstance<DAL.Fulfillment.DishLabelOperations>();
            List<PosKitInfo> posKits = dishLabelOperations.GetPosLabels(companyID, ESNAuthorizationID, dt);
                        
            return posKits;
        }

        
        public  List<PosKitInfo> GetPosLabelByESN(string esn, string shippRequestDate)
        {
            SV.Framework.DAL.Fulfillment.DishLabelOperations dishLabelOperations = SV.Framework.DAL.Fulfillment.DishLabelOperations.CreateInstance<DAL.Fulfillment.DishLabelOperations>();
            List<PosKitInfo> posKits = dishLabelOperations.GetPosLabelByESN(esn, shippRequestDate);
           
            return posKits;
        }

        public  List<PosKitInfo> GetPosLabel(int poid, string[] esnList)
        {
            SV.Framework.DAL.Fulfillment.DishLabelOperations dishLabelOperations = SV.Framework.DAL.Fulfillment.DishLabelOperations.CreateInstance<DAL.Fulfillment.DishLabelOperations>();
            List<PosKitInfo> posKits = dishLabelOperations.GetPosLabel(poid, esnList);
            
            return posKits;
        }

        public  List<PosKitInfo> GetPosKitLabel(int poid)
        {
            SV.Framework.DAL.Fulfillment.DishLabelOperations dishLabelOperations = SV.Framework.DAL.Fulfillment.DishLabelOperations.CreateInstance<DAL.Fulfillment.DishLabelOperations>();
            List<PosKitInfo> posKits = dishLabelOperations.GetPosKitLabel(poid);

            return posKits;
        }
        public List<PalletCartonMap> GetPalletCartonMappingLabel(int poid)
        {
            SV.Framework.DAL.Fulfillment.DishLabelOperations dishLabelOperations = SV.Framework.DAL.Fulfillment.DishLabelOperations.CreateInstance<DAL.Fulfillment.DishLabelOperations>();
            List<PalletCartonMap> pallets = dishLabelOperations.GetPalletCartonMappingLabel(poid);

            return pallets;
        }
        public List<CartonBoxID> GetPurchaseOrderBoxIDs(int companyID, string poNum, int poID, string BoxNumber)
        {
            SV.Framework.DAL.Fulfillment.DishLabelOperations dishLabelOperations = SV.Framework.DAL.Fulfillment.DishLabelOperations.CreateInstance<DAL.Fulfillment.DishLabelOperations>();
            List<CartonBoxID> boxList = dishLabelOperations.GetPurchaseOrderBoxIDs(companyID, poNum, poID, BoxNumber);

            return boxList;
        }
        public DataTable ESNData(List<SV.Framework.Models.Inventory.EsnUploadNew> mslEsnList)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ESN", typeof(System.String));
            dt.Columns.Add("BatchNumber", typeof(System.String));
            dt.Columns.Add("ICC_ID", typeof(System.String));
            dt.Columns.Add("MeidHex", typeof(System.String));
            dt.Columns.Add("MeidDec", typeof(System.String));
            dt.Columns.Add("Location", typeof(System.String));
            dt.Columns.Add("MSL", typeof(System.String));
            dt.Columns.Add("OTKSL", typeof(System.String));
            dt.Columns.Add("SerialNumber", typeof(System.String));


            DataRow row;
            int rowNumber = 1;
            if (mslEsnList != null && mslEsnList.Count > 0)
            {
                foreach (SV.Framework.Models.Inventory.EsnUploadNew item in mslEsnList)
                {
                    row = dt.NewRow();
                    row["ESN"] = item.ESN;
                    row["BatchNumber"] = item.MslNumber;
                    row["ICC_ID"] = rowNumber;
                    row["MeidHex"] = item.MeidHex;
                    row["MeidDec"] = item.MeidDec;
                    row["Location"] = item.Location;
                    row["MSL"] = item.MSL;
                    row["OTKSL"] = item.OTKSL;
                    row["SerialNumber"] = item.SerialNumber;

                    dt.Rows.Add(row);
                    rowNumber = rowNumber + 1;
                }
            }
            return dt;
        }

    }
}