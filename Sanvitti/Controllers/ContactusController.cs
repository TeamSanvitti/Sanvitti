using Microsoft.AspNetCore.Mvc;
using Sanvitti.Models;
using SV.Framework.Sanvitti;

namespace Sanvitti.Controllers
{
    public class ContactusController : Controller
    {
        private IConfiguration configuration;

        public ContactusController(IConfiguration _configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Contactus contactus)
        {
            var body = "Name: " + contactus.Name + "<br>Email Address: " + contactus.Email + "<br>Phone: " + contactus.Mobile + "<br>" + contactus.Comment;

            MailHelper mailHelper = new MailHelper(configuration);
            if(mailHelper.SendEmail(contactus.Email, "Contact us", body))
            {
                ViewBag.msg = "Sent Mail Successfully";
            }
            else
            {
                ViewBag.msg = "Failed";
            }

            return View(contactus);

        }
    }
}
