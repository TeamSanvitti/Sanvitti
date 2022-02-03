using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using SV.Framework.Services;
using SV.Framework.Models.Service;
//using SV.Framework.Models.;



namespace SanvittiServices
{
    /// <summary>
    /// Summary description for RMAService
    /// </summary>
    /// [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)]
    [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)]
    [WebService(Namespace = "http://sanvitti.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RMAService : System.Web.Services.WebService
    {

        //RMA ESN Listing
        [WebMethod]
        public RmaEsnListingResponse GetRmaEsnListing(RmaEsnListingRequest serviceRequest)
        {
            RMAServices service = new RMAServices();
            return service.GetRmaEsnListing(serviceRequest);
        }
        [WebMethod]
        public RmaEsnDetailResponse GetRmaEsnDetail(RmaEsnListingRequest serviceRequest)
        {
            RMAServices service = new RMAServices();
            return service.GetRmaEsnDetail(serviceRequest);
        }
        [WebMethod]
        public RMAResponse GetRMA(RMASearchCriteria rmaRequest)
        {
            RMAServices service = new RMAServices();
            return service.GetRMAList(rmaRequest);
        }
        [WebMethod(EnableSession = true)]
        public RMAAPIResponse SetRMA(RMAAPIRequest RMARequest)
        {
            RMAServices service = new RMAServices();
            RMAAPIResponse RMAResponse = new RMAAPIResponse();
            int userID = 0;
            RMAResponse = service.CreateNewRMA(RMARequest, true);

            return RMAResponse;
        }
        [WebMethod]
        public CancelRMAResponse CancelRMA(RMARequests rmaRequest)
        {
            RMAServices service = new RMAServices();
            return service.CancelRMA(rmaRequest);

        }

    }
}
