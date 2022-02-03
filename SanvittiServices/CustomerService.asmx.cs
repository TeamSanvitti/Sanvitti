using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using SV.Framework.Services;
using SV.Framework.Models.Service;

namespace SanvittiServices
{
    /// <summary>
    /// Summary description for CustomerService
    /// </summary>
    /// 
    [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)]
    [WebService(Namespace = "http://sanvitti.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CustomerService : System.Web.Services.WebService
    {

        [WebMethod]
        public CompanyStoreResponse GetCompanyStores(CompanyStoreRequest companyStoreRequest)
        {
            CustomerServices service = new CustomerServices();

            return service.GetCompanyStore(companyStoreRequest);
        }
        [WebMethod]
        public UsersResponse GetAssignedUsers(UsersRequest userRequest)
        {
            CustomerServices service = new CustomerServices();

            return service.GetAssignedUsers(userRequest);
        }
    }
}
