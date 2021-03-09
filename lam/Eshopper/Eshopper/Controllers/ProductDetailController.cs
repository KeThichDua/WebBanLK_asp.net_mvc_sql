using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshopper.Controllers
{
    public class ProductDetailController : Controller
    {
        // GET: ProductDetail
        public ActionResult Index(string id)
        {
            while (id.Length < 10)
            {
                id += " ";
            }
            DBModels db = new DBModels();
            ViewBag.id = id;
            return View(db);
        }
    }
}