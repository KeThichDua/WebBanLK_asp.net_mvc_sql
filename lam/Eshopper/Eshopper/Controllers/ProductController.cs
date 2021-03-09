using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshopper.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            DBModels db = new DBModels();
            return View(db);
        }

        // lọc sp theo loại sp, hãng sản xuất, bán chạy hoặc hạ giá
        public ActionResult Loc(string id, int loai)
        {
            while (id.Length < 10)
            {
                id += " ";
            }
            DBModels db = new DBModels();
            ViewBag.id = id;
            ViewBag.loai = loai;
            return View(db);
        }
    }
}