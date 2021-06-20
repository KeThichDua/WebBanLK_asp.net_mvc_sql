using Eshopper.Commom;
using Eshopper.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshopper.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NguoiDungDetail()
        {
            var sessnd = Session[CommomConstants.NguoiDungSession];
            var nd = new NguoiDungLogin();
            nd = (NguoiDungLogin)sessnd;
            return View(nd);

        }
    }
}