using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Catalog;
using SV.Framework.Models.Common;

namespace SV.Framework.Catalog
{
    public class CarriersOperation: BaseCreateInstance
    {
        public  List<Carriers> GetCarriersList(int carriersGUID, bool active)
        {
            SV.Framework.DAL.Catalog.CarriersOperation carriersOperation = SV.Framework.DAL.Catalog.CarriersOperation.CreateInstance<SV.Framework.DAL.Catalog.CarriersOperation>();

            List<Carriers> carriersList = carriersOperation.GetCarriersList(carriersGUID, active);// new List<Carriers>();
            
            return carriersList;
        }
        public List<Carriers> GetCarriersList(int makerid, int carrierGUID)
        {
            SV.Framework.DAL.Catalog.CarriersOperation carriersOperation = SV.Framework.DAL.Catalog.CarriersOperation.CreateInstance<SV.Framework.DAL.Catalog.CarriersOperation>();

            List<Carriers> carriersList = carriersOperation.GetCarriersList(makerid, carrierGUID);// new List<Carriers>();
            
            return carriersList;
        }
        
        public int CreateUpdateCarrier(Carriers carriersObj)
        {
            SV.Framework.DAL.Catalog.CarriersOperation carriersOperation = SV.Framework.DAL.Catalog.CarriersOperation.CreateInstance<SV.Framework.DAL.Catalog.CarriersOperation>();

            int returnValue = carriersOperation.CreateUpdateCarrier(carriersObj);
            
            return returnValue;
        }
        public  int DeleteCarrier(int carrierGUID)
        {
            SV.Framework.DAL.Catalog.CarriersOperation carriersOperation = SV.Framework.DAL.Catalog.CarriersOperation.CreateInstance<SV.Framework.DAL.Catalog.CarriersOperation>();

            int returnValue = carriersOperation.DeleteCarrier(carrierGUID);

            return returnValue;
        }
    }
}
