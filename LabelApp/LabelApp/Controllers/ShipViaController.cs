using LabelApp.Models;
using LabelApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelApp.Controllers
{
    public class ShipViaController : Controller
    {
        private readonly AppDbContext _db;
        public ShipViaController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<av_ShipVia> objList = _db.av_ShipVia;
            return View();
        }
    }
}
