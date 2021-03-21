using Eshopper.Commom;
using Eshopper.Models.Dao;
using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshopper.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string searchString = "")
        {

            DBModels db = new DBModels();
            var items = db.SanPhams.ToList();
            if (searchString != string.Empty)
            {
                items = items.Where(x => x.TenSP.Contains(searchString)).ToList();
            }
            ViewBag.lsp = db.LoaiSPs;
            ViewBag.ncc = db.NhaCCs;
            return View(items);
        }

       
    }
}