using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using SV.Framework.Services;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Service;

namespace SanvittiServices
{
    /// <summary>
    /// Summary description for InventoryService
    /// </summary>
    /// 
    [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)]
    [WebService(Namespace = "http://sanvitti.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class InventoryService : System.Web.Services.WebService
    {
        [WebMethod]
        public RunningStockResponse GetRunningStock(RunningStockRequest serviceRequest)
        {
            InventoryServices service = new InventoryServices();
            return service.GetRunningStock(serviceRequest);
        }

        [WebMethod]
        public EsnRepositoryResponse GetEsnRepositoryList(EsnRepositoryRequest serviceRequest)
        {
            InventoryServices service = new InventoryServices();
            return service.GetEsnRepository(serviceRequest);

        }
        [WebMethod]
        public InventoryResponse GetInventorySKU(InventoryRequest inventoryRequest)
        {
            InventoryServices service = new InventoryServices();
            return service.GetInventoryList(inventoryRequest);
        }
        [WebMethod]
        public CurrentStockResponse GetCurrentStock(CurrentStockRequest serviceRequest)
        {
            InventoryServices service = new InventoryServices();
            return service.GetInventoryCurrentStock(serviceRequest);
        }
        //// [WebMethod]
        //public StockReceivalResponse GetInventoryStockFlow(StockReceivalRequest serviceRequest)
        //{
        //    StockOperation operation = new StockOperation();
        //    return operation.GetInventoryStockReceival(serviceRequest);
        //}
    }
}
